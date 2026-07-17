using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using MainUI.Global;
using MainUI.Fault.Model;

namespace MainUI.Fault.Engine
{
    /// <summary>
    /// 降载/卸载动作元数据（透出给现有降载执行通道使用，引擎本身不执行降载）。
    /// </summary>
    public class DerateMeta
    {
        public string RuleName { get; set; }
        public double DeratePercent { get; set; }
        public int UnloadAfterSec { get; set; }
    }

    /// <summary>
    /// 数据驱动 ECM 故障引擎。
    ///
    /// 用法（在 UnifiedFaultDetectionService 中）：
    ///   1) 型号切换：engine.LoadProfile(model)；若成功 → _ecmFaultConditions = engine.BuildConditions()
    ///      并重新 InitializeECMFaults()；失败 → 回退现有 CreateECMFaultConditions()（240 老路径作安全兜底）。
    ///   2) 检测线程每轮：engine.RefreshSnapshot()（取代 ucWarnList 里硬编码的取值桥）。
    ///   3) DetectFault 不变：它调用 FaultCondition.CheckXxx(CurrentData)，本引擎生成的委托忽略
    ///      CurrentData，改读引擎内部快照。
    ///
    /// 这样：检测主循环、持续时间去抖、入库、UI 全部复用；判据来自 JSON，新增型号只加文件。
    /// </summary>
    public class EcmFaultEngine
    {
        private readonly EcmSignalProvider _provider = new EcmSignalProvider();

        private EcmFaultProfile _profile;
        private Dictionary<string, List<string>> _groups = new Dictionary<string, List<string>>();
        private readonly Dictionary<string, DerateMeta> _derate = new Dictionary<string, DerateMeta>();

        // 实时快照：信号规范名 -> 当前值。volatile 引用整体替换，读侧无锁。
        private volatile Dictionary<string, double> _snap = new Dictionary<string, double>();

        public bool HasProfile { get { return _profile != null; } }
        public string CurrentModel { get { return _profile != null ? _profile.Model : null; } }

        /// <summary>
        /// 加载 FaultProfiles/{model}.faults.json。不存在或解析失败返回 false（调用方回退老逻辑）。
        /// </summary>
        public bool LoadProfile(string model)
        {
            try
            {
                if (string.IsNullOrEmpty(model)) { _profile = null; return false; }

                string path = Path.Combine(Application.StartupPath, "FaultProfiles", model + ".faults.json");
                if (!File.Exists(path))
                {
                    Var.LogInfo($"[ECM引擎] 未找到型号判据文件：{path}，回退内置判据。");
                    _profile = null;
                    return false;
                }

                string json = File.ReadAllText(path, Encoding.UTF8);
                var profile = JsonConvert.DeserializeObject<EcmFaultProfile>(json);
                if (profile == null || profile.Rules == null || profile.Rules.Count == 0)
                {
                    Var.LogInfo($"[ECM引擎] 判据文件为空或无规则：{path}，回退内置判据。");
                    _profile = null;
                    return false;
                }

                _profile = profile;

                // 预编译信号组
                _groups = new Dictionary<string, List<string>>();
                if (profile.Groups != null)
                    foreach (var g in profile.Groups)
                        if (!string.IsNullOrEmpty(g.Name) && g.Members != null)
                            _groups[g.Name] = g.Members;

                // 预编译降载元数据
                _derate.Clear();
                foreach (var rule in profile.Rules)
                {
                    if (rule.Checks == null) continue;
                    var sh = rule.Checks.FirstOrDefault(c => Lvl(c.Level) == "Shedding" &&
                                                             (c.DeratePercent > 0 || c.UnloadAfterSec > 0));
                    if (sh != null)
                        _derate[rule.Name] = new DerateMeta
                        {
                            RuleName = rule.Name,
                            DeratePercent = sh.DeratePercent,
                            UnloadAfterSec = sh.UnloadAfterSec
                        };
                }

                Var.LogInfo($"[ECM引擎] 已加载型号 {profile.Model} 判据：信号 {profile.Signals?.Count ?? 0} 个，规则 {profile.Rules.Count} 条。");
                return true;
            }
            catch (Exception ex)
            {
                Var.LogInfo($"[ECM引擎] 加载判据文件异常：{ex.Message}，回退内置判据。");
                _profile = null;
                return false;
            }
        }

        /// <summary>
        /// 刷新实时快照（每个检测周期调用一次）。整体替换字典引用，保证读侧拿到一致快照。
        /// </summary>
        public void RefreshSnapshot()
        {
            if (_profile == null || _profile.Signals == null) return;
            var d = new Dictionary<string, double>(_profile.Signals.Count);
            foreach (var sig in _profile.Signals)
            {
                if (string.IsNullOrEmpty(sig.Name)) continue;
                d[sig.Name] = _provider.Read(sig);
            }
            _snap = d;
        }

        /// <summary>当前快照只读副本（调试/记录用）</summary>
        public Dictionary<string, double> SnapshotCopy()
        {
            return new Dictionary<string, double>(_snap);
        }

        /// <summary>
        /// 故障入库描述：规则名 + 级别 + 触发时实测值。
        /// </summary>
        public string BuildTriggerDesc(string ruleName, WarnTypeEnum warnType)
        {
            try
            {
                if (_profile == null) return ruleName;
                var rule = _profile.Rules?.FirstOrDefault(r => r.Name == ruleName);
                if (rule == null) return ruleName;

                string level = warnType == WarnTypeEnum.Alarm ? "报警"
                             : warnType == WarnTypeEnum.Shedding ? "降载"
                             : warnType == WarnTypeEnum.Stop ? "停机" : "提示";

                // 取触发级别第一个 Term 的左值作为"当前值"
                var check = rule.Checks?.FirstOrDefault(c => Lvl(c.Level) == Lvl(warnType.ToString()));
                var term = check?.Terms?.FirstOrDefault();
                if (term == null) return $"{ruleName} [{level}]";

                double val = ResolveOperand(term.Left ?? "", _snap);
                return $"{ruleName} [{level}]，实测值：{val:F1}";
            }
            catch
            {
                return ruleName;
            }
        }

        /// <summary>取某条规则的降载元数据（执行降载时读取百分比/卸载延时）。无则返回 null。</summary>
        public DerateMeta GetDerate(string ruleName)
        {
            return _derate.TryGetValue(ruleName, out DerateMeta m) ? m : null;
        }

        /// <summary>
        /// 由当前型号判据生成 FaultCondition 字典，接入现有 DetectFault 主循环。
        /// 生成的委托忽略传入的 SensorData，改读引擎快照。
        /// </summary>
        public Dictionary<string, FaultCondition> BuildConditions()
        {
            var dict = new Dictionary<string, FaultCondition>();
            if (_profile == null) return dict;

            foreach (var rule in _profile.Rules)
            {
                if (string.IsNullOrEmpty(rule.Name)) continue;

                var fc = new FaultCondition { Name = rule.Name };

                if (rule.Vote != null)
                {
                    // 表决型规则：把表决结果挂到对应级别
                    VoteDef vote = rule.Vote;
                    Func<SensorData, bool> eval = (data) => EvalVote(vote);
                    AssignLevel(fc, Lvl(vote.Level), eval, vote.Duration);
                }
                else if (rule.Checks != null)
                {
                    // 普通规则：分别为 Stop / Shedding / Alarm / Record(Tip) 生成“或”委托
                    AssignLevel(fc, "Stop", MakeLevelEval(rule, "Stop"), LevelDuration(rule, "Stop"));
                    AssignLevel(fc, "Shedding", MakeLevelEval(rule, "Shedding"), LevelDuration(rule, "Shedding"));
                    AssignLevel(fc, "Alarm", MakeLevelEval(rule, "Alarm"), LevelDuration(rule, "Alarm"));
                    AssignLevel(fc, "Record", MakeLevelEval(rule, "Record"), LevelDuration(rule, "Record"));
                }

                dict[rule.Name] = fc;
            }
            return dict;
        }

        // 级别装配
        private static void AssignLevel(FaultCondition fc, string level, Func<SensorData, bool> eval, int duration)
        {
            if (eval == null) return;
            switch (level)
            {
                case "Stop":
                    fc.CheckStop = eval; fc.StopDuration = duration; break;
                case "Shedding":
                    fc.CheckShedding = eval; fc.SheddingDuration = duration; break;
                case "Alarm":
                    fc.CheckAlarm = eval; fc.AlarmDuration = duration; break;
                case "Record":
                    // 仅记录不报警 → Tip（WarnTypeEnum.Tip，严重度4“只提示不蜂鸣”）
                    // 需 FaultCondition 含 CheckTip / TipDuration（见集成说明补丁）。
                    fc.CheckTip = eval; fc.TipDuration = duration; break;
            }
        }

        /// <summary>同级别多条 Check 的“或”委托；无该级别返回 null。</summary>
        private Func<SensorData, bool> MakeLevelEval(RuleDef rule, string level)
        {
            var checks = rule.Checks.Where(c => Lvl(c.Level) == level).ToList();
            if (checks.Count == 0) return null;
            return (data) =>
            {
                var snap = _snap;
                for (int i = 0; i < checks.Count; i++)
                    if (EvalCheck(checks[i], snap)) return true;
                return false;
            };
        }

        private int LevelDuration(RuleDef rule, string level)
        {
            var checks = rule.Checks.Where(c => Lvl(c.Level) == level).ToList();
            if (checks.Count == 0) return 0;
            // 同级别多条取最大持续时间（保守）
            return checks.Max(c => c.Duration);
        }

        // ─────────────────────────────────────────────────────────────
        // 判定求值
        // ─────────────────────────────────────────────────────────────

        private bool EvalCheck(CheckDef check, Dictionary<string, double> snap)
        {
            if (check.Terms == null || check.Terms.Count == 0) return false;
            // Terms 之间为“与”
            for (int i = 0; i < check.Terms.Count; i++)
                if (!EvalTerm(check.Terms[i], snap)) return false;
            return true;
        }

        private bool EvalTerm(TermDef term, Dictionary<string, double> snap)
        {
            double left = ResolveOperand(term.Left, snap);
            if (double.IsNaN(left)) return false;

            double right;
            if (term.Value.HasValue) right = term.Value.Value;
            else if (!string.IsNullOrEmpty(term.RightSignal)) right = GetSignal(term.RightSignal, snap);
            else return false;

            if (double.IsNaN(right)) return false;
            return Compare(left, term.Op, right);
        }

        /// <summary>解析左操作数：信号名 / MAX(组) / MIN(组) / SPREAD(组) / ABSDIFF(a,b)</summary>
        private double ResolveOperand(string expr, Dictionary<string, double> snap)
        {
            if (string.IsNullOrEmpty(expr)) return double.NaN;
            expr = expr.Trim();

            int p = expr.IndexOf('(');
            if (p > 0 && expr.EndsWith(")"))
            {
                string fn = expr.Substring(0, p).Trim().ToUpperInvariant();
                string arg = expr.Substring(p + 1, expr.Length - p - 2).Trim();

                if (fn == "ABSDIFF")
                {
                    var parts = arg.Split(',');
                    if (parts.Length != 2) return double.NaN;
                    double a = GetSignal(parts[0].Trim(), snap);
                    double b = GetSignal(parts[1].Trim(), snap);
                    if (double.IsNaN(a) || double.IsNaN(b)) return double.NaN;
                    return Math.Abs(a - b);
                }

                var vals = GroupValues(arg, snap);
                if (vals.Count == 0) return double.NaN;
                switch (fn)
                {
                    case "MAX": return vals.Max();
                    case "MIN": return vals.Min();
                    case "SPREAD": return vals.Max() - vals.Min();
                    default: return double.NaN;
                }
            }

            // 普通信号
            return GetSignal(expr, snap);
        }

        private List<double> GroupValues(string groupName, Dictionary<string, double> snap)
        {
            var result = new List<double>();
            List<string> members;
            if (!_groups.TryGetValue(groupName, out members)) return result;
            foreach (var m in members)
            {
                double v = GetSignal(m, snap);
                if (!double.IsNaN(v)) result.Add(v);
            }
            return result;
        }

        private static double GetSignal(string name, Dictionary<string, double> snap)
        {
            if (string.IsNullOrEmpty(name)) return double.NaN;
            double v;
            return snap.TryGetValue(name, out v) ? v : double.NaN; // 该型号无此信号 → NaN → 条件不成立
        }

        private static bool Compare(double l, string op, double r)
        {
            switch ((op ?? "").Trim().ToUpperInvariant())
            {
                case "GE": return l >= r;
                case "GT": return l > r;
                case "LE": return l <= r;
                case "LT": return l < r;
                case "EQ": return l == r;
                case "NE": return l != r;
                default: return false;
            }
        }

        // ─────────────────────────────────────────────────────────────
        // 多传感器表决（规范 11.1 / 11.2）
        // 剔除故障传感器后，剩余有效传感器全部满足比较条件才触发；无有效传感器不触发。
        // ─────────────────────────────────────────────────────────────
        private bool EvalVote(VoteDef vote)
        {
            if (vote == null || vote.Sensors == null || vote.Sensors.Count == 0) return false;
            var snap = _snap;

            var validValues = new List<double>();
            for (int i = 0; i < vote.Sensors.Count; i++)
            {
                string sName = vote.Sensors[i];
                double v = GetSignal(sName, snap);
                if (double.IsNaN(v)) continue; // 信号缺失视为不可用

                bool faulted = false;

                // 1) 显式故障标志
                if (vote.FaultFlags != null && i < vote.FaultFlags.Count && !string.IsNullOrEmpty(vote.FaultFlags[i]))
                {
                    double f = GetSignal(vote.FaultFlags[i], snap);
                    if (!double.IsNaN(f) && f != 0) faulted = true;
                }

                // 2) 量程越界判故障
                if (!faulted && (vote.ValidLow.HasValue || vote.ValidHigh.HasValue))
                {
                    if (vote.ValidLow.HasValue && v < vote.ValidLow.Value) faulted = true;
                    if (vote.ValidHigh.HasValue && v > vote.ValidHigh.Value) faulted = true;
                }

                if (!faulted) validValues.Add(v);
            }

            if (validValues.Count == 0) return false; // 全故障 → 不停机（仅报警在别处处理）

            for (int i = 0; i < validValues.Count; i++)
                if (!Compare(validValues[i], vote.Op, vote.Value)) return false; // 有效传感器全部满足才触发

            return true;
        }

        private static string Lvl(string level)
        {
            if (string.IsNullOrEmpty(level)) return "";
            switch (level.Trim().ToLowerInvariant())
            {
                case "stop": return "Stop";
                case "shedding": return "Shedding";
                case "alarm": return "Alarm";
                case "record":
                case "tip": return "Alarm";
                default: return level.Trim();
            }
        }

        /// <summary>
        /// 新增：型式试验启动映射自检（方案②）。
        /// 在型式试验模式、profile 加载成功后调用一次，输出四段日志：
        ///   TRDP保留清单 / 已映射清单 / 休眠清单（含所属规则） / 健康门控挂接清单。
        /// 休眠清单非空且 showPopup=true 时弹一次性提示，配合工艺确认留痕。
        /// 例行试验或无 profile 时不做任何事。
        /// </summary>
        public void LogTypeTestMappingSelfCheck(bool showPopup)
        {
            try
            {
                if (_profile == null || _profile.Signals == null) return;
                if (Var.SysConfig == null || Var.SysConfig.LastTrialType != 1) return;

                var trdpKept = new List<string>();
                var mapped = new List<string>();
                var dormant = new List<string>();

                foreach (var sig in _profile.Signals)
                {
                    if (sig == null) continue;
                    string src = (sig.Source ?? "").Trim().ToUpperInvariant();
                    if (src != "TRDP") continue;

                    string opcSrc = (sig.OpcSource ?? "").Trim().ToUpperInvariant();
                    if (opcSrc == "TRDP")
                    {
                        trdpKept.Add(sig.Name + "（TRDP:" + sig.Label + "，ECM仍发送）");
                    }
                    else if (!string.IsNullOrEmpty(sig.OpcLabel))
                    {
                        mapped.Add(sig.Name + " → " + (opcSrc == "" ? "AI2" : opcSrc) + ":" + sig.OpcLabel);
                    }
                    else
                    {
                        var rules = FindRulesReferencing(sig.Name);
                        dormant.Add(sig.Name + "（所属规则：" +
                            (rules.Count > 0 ? string.Join("、", rules.ToArray()) : "未检索到") + "）");
                    }
                }

                Var.LogInfo("[型式试验自检] TRDP保留清单(" + trdpKept.Count + ")：" + string.Join("；", trdpKept.ToArray()));
                Var.LogInfo("[型式试验自检] 已映射清单(" + mapped.Count + ")：" + string.Join("；", mapped.ToArray()));
                Var.LogInfo("[型式试验自检] 休眠清单(" + dormant.Count + ")：" + string.Join("；", dormant.ToArray()));
                Var.LogInfo("[型式试验自检] 健康门控：AI2 重定向挂 AI2Grp.NoError；SPEED 重定向挂 speedGrp.IsNoError；转速取值挂 speedGrp.NoError[0]。");

                if (showPopup && dormant.Count > 0)
                {
                    System.Windows.Forms.MessageBox.Show(
                        "型式试验模式下，以下信号无台位点位、ECM 亦停发，本次试验期间其保护规则休眠（值恒为 0）：\r\n\r\n" +
                        string.Join("\r\n", dormant.ToArray()) +
                        "\r\n\r\n请确认已按工艺规程落实替代监护。",
                        "型式试验信号休眠清单",
                        System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Var.LogInfo("[型式试验自检] 执行异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 新增：检索引用了指定信号（含经由信号组）的规则名，用于休眠清单显性化。
        /// </summary>
        /// <param name="sigName"></param>
        /// <returns></returns>
        private List<string> FindRulesReferencing(string sigName)
        {
            var result = new List<string>();
            if (_profile == null || _profile.Rules == null || string.IsNullOrEmpty(sigName)) return result;

            // 该信号所属的组名
            var groupNames = new List<string>();
            foreach (var g in _groups)
                if (g.Value != null && g.Value.Contains(sigName))
                    groupNames.Add(g.Key);

            foreach (var rule in _profile.Rules)
            {
                if (rule == null || string.IsNullOrEmpty(rule.Name)) continue;
                bool hit = false;

                if (rule.Vote != null && rule.Vote.Sensors != null && rule.Vote.Sensors.Contains(sigName))
                    hit = true;

                if (!hit && rule.Checks != null)
                {
                    foreach (var c in rule.Checks)
                    {
                        if (c == null || c.Terms == null) continue;
                        foreach (var t in c.Terms)
                        {
                            if (t == null || string.IsNullOrEmpty(t.Left)) continue;
                            if (t.Left.Contains(sigName)) { hit = true; break; }
                            foreach (var gn in groupNames)
                                if (t.Left.Contains(gn)) { hit = true; break; }
                            if (hit) break;
                        }
                        if (hit) break;
                    }
                }

                if (hit && !result.Contains(rule.Name)) result.Add(rule.Name);
            }
            return result;
        }
    }
}
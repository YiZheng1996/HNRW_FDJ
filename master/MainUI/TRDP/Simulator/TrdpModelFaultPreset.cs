using System;
using System.Collections.Generic;
using System.Linq;
using MainUI.Fault.Engine;            // EcmProfileStore / EcmFaultProfile / SignalDef / RuleDef / CheckDef / TermDef / VoteDef / GroupDef
using MainUI.Global;                  // Var
using MetorSignalSimulator.UI.Model;  // FullTags

namespace MainUI.Simulate
{
    /// <summary>
    /// 型号驱动的 TRDP 故障预设引擎（纯加法、与现有模拟器完全解耦）。
    ///
    /// 设计：直接复用与故障检测引擎同一份 FaultProfiles\{型号}.faults.json（单一真相源），
    ///       对每条规则反推出“刚好越限”的 TRDP 注入值，一键强制触发该故障。
    ///       不需要任何额外配置文件。
    ///
    /// 约束：
    ///   - 本类不修改 TRDPSimulatorService，只调用其 public InjectValue(string, decimal)。
    ///   - 只能驱动 Source=TRDP 的信号；AI2/Speed/DI/Power 等非 TRDP 条件会被识别并如实报告，
    ///     提示需要在主控真实加载或用对应 Tab 单独满足（例如“发动机功率≥280”这类前置门限）。
    ///   - 240 等无 JSON 的型号：HasProfileFor=false，UI 不构建动态按钮，老模拟器行为不变。
    /// </summary>
    public class TrdpModelFaultPreset
    {
        /// <summary>SPREAD/ABSDIFF 自动派生时使用的基准值（仅影响差值的绝对位置，不影响差值本身）。
        /// 取值偏保守，避免顺带触发同组的 MAX 报警。</summary>
        private const double SPREAD_BASE = 300.0;

        /// <summary>本引擎本轮注入过的 TRDP 信号名（用于一键复位）。</summary>
        private readonly HashSet<string> _lastInjected = new HashSet<string>();

        // ─────────────────────────────────────────────────────────────────────
        // 配置加载
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>该型号是否由数据驱动引擎接管（存在 {型号}.faults.json）。</summary>
        public bool HasProfileFor(string model)
        {
            return EcmProfileStore.Exists(model);
        }

        private EcmFaultProfile LoadProfile(string model)
        {
            return EcmProfileStore.Load(model);
        }

        // ─────────────────────────────────────────────────────────────────────
        // 给 UI 用：列出当前型号可触发的规则（用于动态生成按钮）
        // ─────────────────────────────────────────────────────────────────────

        public class RuleInfo
        {
            public string Name;
            public bool HasNonTrdpDependency;   // 含非 TRDP 前置条件（如功率门限）
            public bool HasComplexTerm;         // 含 SPREAD/ABSDIFF/RightSignal（自动派生为近似）
            public string Note;                 // 给按钮 ToolTip 的说明
        }

        /// <summary>枚举当前型号所有规则，并预先分析其触发可行性，供 UI 生成按钮与提示。</summary>
        public List<RuleInfo> GetTriggerableRules(string model)
        {
            var list = new List<RuleInfo>();
            var profile = LoadProfile(model);
            if (profile == null || profile.Rules == null) return list;

            foreach (var rule in profile.Rules)
            {
                if (rule == null || string.IsNullOrEmpty(rule.Name)) continue;

                var info = new RuleInfo { Name = rule.Name };
                var notes = new List<string>();
                AnalyzeRule(profile, rule, info, notes);
                info.Note = notes.Count > 0 ? string.Join("；", notes) : "自动派生注入";
                list.Add(info);
            }
            return list;
        }

        // ─────────────────────────────────────────────────────────────────────
        // 强制触发
        // ─────────────────────────────────────────────────────────────────────

        public class TriggerResult
        {
            public string RuleName;
            public readonly List<string> Injected = new List<string>();     // 已注入的 TRDP 信号描述
            public readonly List<string> SkippedNonTrdp = new List<string>();// 非 TRDP、本模拟器无法驱动
            public readonly List<string> Notes = new List<string>();        // 其它提示
        }

        /// <summary>
        /// 强制触发某条规则对应的故障，按 faults.json 自动反推 TRDP 注入值。
        /// </summary>
        /// <param name="level">可选：指定按哪个级别的 Check 触发（Stop/Shedding/Alarm/Record）；为空取第一条 Check。</param>
        /// <param name="margin">可选：自定义越限余量；&lt;0 时按阈值的 2% 自动取（下限 0.01）。</param>
        public TriggerResult ForceTriggerRule(string model, string ruleName, string level = null, double margin = -1)
        {
            var r = new TriggerResult { RuleName = ruleName };
            var profile = LoadProfile(model);
            if (profile == null) { r.Notes.Add("当前型号无 faults.json，无法触发。"); return r; }

            var rule = profile.Rules?.FirstOrDefault(x => x != null && x.Name == ruleName);
            if (rule == null) { r.Notes.Add("未找到规则：" + ruleName); return r; }

            if (rule.Vote != null)
            {
                TriggerVote(profile, rule.Vote, r, margin);
                return r;
            }

            var check = PickCheck(rule, level);
            if (check == null || check.Terms == null || check.Terms.Count == 0)
            {
                r.Notes.Add("规则无可用判定项。");
                return r;
            }

            foreach (var term in check.Terms)
                TriggerTerm(profile, term, r, margin);

            return r;
        }

        /// <summary>把本引擎本轮注入过的 TRDP 信号全部置 0，用于一键复位。</summary>
        public int RestoreLastTrigger()
        {
            int n = 0;
            foreach (var sig in _lastInjected.ToList())
            {
                TRDPSimulatorService.Instance.InjectValue(sig, 0m);
                n++;
            }
            _lastInjected.Clear();
            return n;
        }

        /// <summary>
        /// 按型号清除所有故障位：表决规则的 FaultFlags + 协议里 DataType=B1 的布尔信号，全部置 0。
        /// 取代写死的 240 故障位列表；240 无 JSON 时返回 0、不动作。
        /// </summary>
        public int ClearAllFaultFlagsForModel(string model)
        {
            var profile = LoadProfile(model);
            if (profile == null) return 0;

            var bools = new HashSet<string>();

            // a) 表决规则的故障屏蔽位
            if (profile.Rules != null)
                foreach (var rule in profile.Rules)
                    if (rule?.Vote?.FaultFlags != null)
                        foreach (var ff in rule.Vote.FaultFlags)
                        {
                            string lbl = TrdpLabelOf(profile, ff);
                            if (lbl != null) bools.Add(lbl);
                        }

            // b) 协议里 B1（布尔）类型且被 profile 引用为 TRDP 源的信号
            var trdpLabels = new HashSet<string>(
                (profile.Signals ?? new List<SignalDef>())
                .Where(s => IsTrdp(s) && !string.IsNullOrEmpty(s.Label))
                .Select(s => s.Label));

            var tags = Var.TRDP?.tags;
            if (tags != null)
            {
                for (int i = 0; i < tags.Count; i++)
                {
                    var t = tags[i];
                    if (t?.DataLabel != null && t.DataType == "B1" && trdpLabels.Contains(t.DataLabel))
                        bools.Add(t.DataLabel);
                }
            }

            int n = 0;
            foreach (var lbl in bools)
            {
                TRDPSimulatorService.Instance.InjectValue(lbl, 0m);
                _lastInjected.Remove(lbl);
                n++;
            }
            return n;
        }

        // ─────────────────────────────────────────────────────────────────────
        // 触发：表决 / 单项
        // ─────────────────────────────────────────────────────────────────────

        private void TriggerVote(EcmFaultProfile profile, VoteDef vote, TriggerResult r, double margin)
        {
            if (vote == null) return;
            double m = ResolveMargin(margin, vote.Value);
            double val = TargetValue(vote.Op, vote.Value, m);

            // 所有参与表决的传感器都注成越限值
            if (vote.Sensors != null)
                foreach (var s in vote.Sensors)
                    InjectByCanonical(profile, s, val, r);

            // 故障屏蔽位清零，保证传感器被视为“有效”，表决才会成立
            if (vote.FaultFlags != null)
                foreach (var ff in vote.FaultFlags)
                    InjectByCanonical(profile, ff, 0, r);

            r.Notes.Add("双传感器表决：已将所有传感器注入越限值并清除屏蔽位。");
        }

        private void TriggerTerm(EcmFaultProfile profile, TermDef term, TriggerResult r, double margin)
        {
            if (term == null) return;

            if (!term.Value.HasValue)
            {
                r.Notes.Add("条件“" + (term.Left ?? "?") + "”比较的是另一信号(RightSignal)，无法自动取值。");
                return;
            }

            double v = term.Value.Value;
            double m = ResolveMargin(margin, v);
            string op = (term.Op ?? "GE").Trim().ToUpperInvariant();

            ParseLeft(term.Left, out string fn, out List<string> args);

            switch (fn)
            {
                case "":      // 普通信号
                    if (args.Count == 1)
                        InjectByCanonical(profile, args[0], TargetValue(op, v, m), r);
                    break;

                case "MAX":   // 让最大值越限 → 全员注到越限值
                case "MIN":   // 让最小值越限 → 全员注到越限值
                    foreach (var member in GroupMembers(profile, args.FirstOrDefault()))
                        InjectByCanonical(profile, member, TargetValue(op, v, m), r);
                    break;

                case "SPREAD": // 让组内极差越限：首个成员抬高，其余压到基准
                    {
                        var members = GroupMembers(profile, args.FirstOrDefault());
                        for (int i = 0; i < members.Count; i++)
                        {
                            double val = (i == 0) ? (SPREAD_BASE + v + m) : SPREAD_BASE;
                            InjectByCanonical(profile, members[i], val, r);
                        }
                        r.Notes.Add("温差(SPREAD)按基准 " + SPREAD_BASE + " 派生。");
                    }
                    break;

                case "ABSDIFF": // |A-B| 越限：A 抬高，B 压到基准
                    if (args.Count == 2)
                    {
                        InjectByCanonical(profile, args[0], SPREAD_BASE + v + m, r);
                        InjectByCanonical(profile, args[1], SPREAD_BASE, r);
                        r.Notes.Add("差值(ABSDIFF)按基准 " + SPREAD_BASE + " 派生。");
                    }
                    break;

                default:
                    r.Notes.Add("未识别的左操作数函数：" + fn + "(...)。");
                    break;
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // 注入：把“规范名”落到 TRDP 字典
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// 按规范名(Signal.Name)查 SignalDef：TRDP 源就注入其 Label；非 TRDP 源记入 SkippedNonTrdp；
        /// 未登记的名字记一条提示（避免静默注入到错误的 Label）。
        /// </summary>
        private void InjectByCanonical(EcmFaultProfile profile, string name, double value, TriggerResult r)
        {
            if (string.IsNullOrEmpty(name)) return;

            var sig = profile.Signals?.FirstOrDefault(s => s != null && s.Name == name);
            if (sig == null)
            {
                r.Notes.Add("信号未在 Signals 登记，跳过：" + name);
                return;
            }

            if (IsTrdp(sig))
            {
                if (string.IsNullOrEmpty(sig.Label)) { r.Notes.Add("信号无 Label，跳过：" + name); return; }
                TRDPSimulatorService.Instance.InjectValue(sig.Label, (decimal)value);
                _lastInjected.Add(sig.Label);
                r.Injected.Add(name + " → " + value);
            }
            else
            {
                r.SkippedNonTrdp.Add(name + "（来源 " + (sig.Source ?? "?") + "，非 TRDP，需在对应 Tab/主控满足）");
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // 解析与小工具
        // ─────────────────────────────────────────────────────────────────────

        private static bool IsTrdp(SignalDef s)
        {
            return s != null && (s.Source ?? "").Trim().ToUpperInvariant() == "TRDP";
        }

        private string TrdpLabelOf(EcmFaultProfile profile, string canonicalName)
        {
            var sig = profile.Signals?.FirstOrDefault(s => s != null && s.Name == canonicalName);
            if (sig != null && IsTrdp(sig)) return sig.Label;
            return null;
        }

        private List<string> GroupMembers(EcmFaultProfile profile, string groupName)
        {
            if (string.IsNullOrEmpty(groupName)) return new List<string>();
            var g = profile.Groups?.FirstOrDefault(x => x != null && x.Name == groupName);
            return g?.Members ?? new List<string>();
        }

        /// <summary>解析左操作数：fn 为空表示普通信号；否则为 MAX/MIN/SPREAD/ABSDIFF，args 为参数（逗号分隔）。</summary>
        private static void ParseLeft(string expr, out string fn, out List<string> args)
        {
            fn = "";
            args = new List<string>();
            if (string.IsNullOrEmpty(expr)) return;
            expr = expr.Trim();

            int p = expr.IndexOf('(');
            if (p > 0 && expr.EndsWith(")"))
            {
                fn = expr.Substring(0, p).Trim().ToUpperInvariant();
                string inner = expr.Substring(p + 1, expr.Length - p - 2);
                args = inner.Split(',').Select(a => a.Trim()).Where(a => a.Length > 0).ToList();
            }
            else
            {
                args.Add(expr);
            }
        }

        private static double TargetValue(string op, double threshold, double margin)
        {
            switch ((op ?? "GE").Trim().ToUpperInvariant())
            {
                case "GE":
                case "GT": return threshold + margin;
                case "LE":
                case "LT": return threshold - margin;
                case "EQ": return threshold;
                case "NE": return threshold + margin;
                default: return threshold + margin;
            }
        }

        /// <summary>
        /// 越限余量：阈值的 2%，下限 0.01。
        /// 纯相对取值，不再设 1.0 下限——保证小量程信号(如曲轴箱压力 0–1，阈值 0.6 → 0.612)不会过冲出量程。
        /// </summary>
        private static double ResolveMargin(double margin, double threshold)
        {
            if (margin >= 0) return margin;
            double m = Math.Abs(threshold) * 0.02;
            return m < 0.01 ? 0.01 : m;
        }

        private CheckDef PickCheck(RuleDef rule, string level)
        {
            if (rule?.Checks == null || rule.Checks.Count == 0) return null;
            if (!string.IsNullOrEmpty(level))
            {
                var byLevel = rule.Checks.FirstOrDefault(c =>
                    c != null && string.Equals(c.Level, level, StringComparison.OrdinalIgnoreCase));
                if (byLevel != null) return byLevel;
            }
            return rule.Checks[0];
        }

        private void AnalyzeRule(EcmFaultProfile profile, RuleDef rule, RuleInfo info, List<string> notes)
        {
            if (rule.Vote != null)
            {
                if (rule.Vote.Sensors != null)
                    foreach (var s in rule.Vote.Sensors)
                    {
                        var sig = profile.Signals?.FirstOrDefault(x => x != null && x.Name == s);
                        if (sig != null && !IsTrdp(sig)) { info.HasNonTrdpDependency = true; notes.Add("含非 TRDP 传感器"); break; }
                    }
                return;
            }

            var check = PickCheck(rule, null);
            if (check?.Terms == null) return;

            foreach (var term in check.Terms)
            {
                if (!term.Value.HasValue) { info.HasComplexTerm = true; notes.Add("含 RightSignal 比较"); }

                ParseLeft(term.Left, out string fn, out List<string> args);
                if (fn == "SPREAD" || fn == "ABSDIFF") { info.HasComplexTerm = true; notes.Add(fn + " 派生(近似)"); }

                IEnumerable<string> names = (fn == "")
                    ? args
                    : (fn == "ABSDIFF" ? args : GroupMembers(profile, args.FirstOrDefault()));
                foreach (var n in names)
                {
                    var sig = profile.Signals?.FirstOrDefault(x => x != null && x.Name == n);
                    if (sig != null && !IsTrdp(sig)) { info.HasNonTrdpDependency = true; notes.Add("含非 TRDP 前置(" + sig.Source + ")"); }
                }
            }
        }
    }
}
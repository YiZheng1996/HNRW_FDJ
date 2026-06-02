using MainUI.Fault.Engine;
using MainUI.Fault.Model;
using MainUI.Global;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MainUI.Fault
{
    /// <summary>
    /// 数据驱动的“故障状态详情列表”弹窗（型号存在 {型号}.faults.json 时使用）。
    /// 直接复用 ucWarn 作为每行：Title/Key=故障名，按该 rule 涉及的 Level 开关三个灯，
    /// CurrentFault 一设灯就变色 —— 与 240 的 frmCurrentWarn 行为完全一致。
    ///
    /// 列由每条 rule 涉及的 Level 反推：
    ///   Checks[].Level=="Alarm"                         → 报警灯
    ///   Checks[].Level=="Shedding"                      → 降载灯
    ///   Checks[].Level=="Stop" 或 Vote.Level=="Stop"    → 停机灯
    ///   Checks[].Level=="Record"（仅记录/Tip）          → 不开任何灯
    /// Section 字段（如“机车电气硬接线采集”）作整行标题。
    ///
    /// 不触碰 frmCurrentWarn(240) / ucWarn / 引擎的任何现有逻辑。
    /// 表头坐标(290/391/490)与行左边距(11)、行宽(561)沿用 240 frmCurrentWarn，
    /// 正好压在 ucWarn 内部 rbtAlarm/rbtShedding/rbtStop 三个按钮上方。
    /// </summary>
    public partial class frmCurrentWarnDynamic : Form
    {
        private const int LEFT = 11;     // 行左边距（= 240 弹窗 ucWarn 的 X）
        private const int ROW_W = 561;   // 行宽（= 240 弹窗 ucWarn 的宽）

        /// <summary>故障名(=rule.Name=faultId) → 该行 ucWarn。</summary>
        private readonly Dictionary<string, ucWarn> _map =
            new Dictionary<string, ucWarn>(StringComparer.Ordinal);

        private readonly string _model;
        private FlowLayoutPanel _flow;

        public frmCurrentWarnDynamic(string model)
        {
            _model = model;

            BuildShell();
            BuildRows();

            if (this.DesignMode) return;

            Var.FaultService.FaultDetected += OnFaultDetected;
            this.FormClosed += (s, e) =>
            {
                try { Var.FaultService.FaultDetected -= OnFaultDetected; } catch { }
            };

            // 一开窗即反映当前型号当前故障态（幂等、无副作用）
            try { Var.FaultService.FaultCheckResend(); } catch { }
        }

        // ── 外壳：标题 / 冻结表头 / 行容器 / 返回 ──
        private void BuildShell()
        {
            this.SuspendLayout();

            this.Text = "故障状态详情列表 — " + _model;
            this.BackColor = Color.FromArgb(243, 249, 255);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ClientSize = new Size(LEFT + ROW_W + 40, 820);

            // 表头（与 240 弹窗同坐标，正压在 ucWarn 三个按钮上方）
            var head = new Panel { Dock = DockStyle.Top, Height = 40 };
            head.Controls.Add(HeadLabel("报警", 290));
            head.Controls.Add(HeadLabel("降载", 391));
            head.Controls.Add(HeadLabel("停机", 490));

            // 行容器：竖向流式，行多时纵向滚动（不换列）
            _flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(0, 4, 0, 4)
            };

            var btnExit = new Sunny.UI.UIButton
            {
                Text = "返回",
                Dock = DockStyle.Bottom,
                Height = 48,
                Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134)
            };
            btnExit.Click += (s, e) => this.Close();

            // 先 Fill 后 Top/Bottom，保证 Fill 占中间剩余区
            this.Controls.Add(_flow);
            this.Controls.Add(head);
            this.Controls.Add(btnExit);

            this.ResumeLayout(false);
        }

        // ── 按 Rules 逐行生成 ──
        private void BuildRows()
        {
            JArray rules = LoadRules();
            string lastSection = null;

            foreach (JObject rule in rules.OfType<JObject>())
            {
                // 标题行（Section 变化时插一条整行标题）
                string section = (string)rule["Section"];
                if (!string.IsNullOrEmpty(section) && section != lastSection)
                {
                    _flow.Controls.Add(SectionLabel(section));
                    lastSection = section;
                }

                string name = (string)rule["Name"] ?? "(未命名)";
                var checks = rule["Checks"] as JArray ?? new JArray();
                var vote = rule["Vote"] as JObject;

                bool hasAlarm = checks.OfType<JObject>().Any(c => (string)c["Level"] == "Alarm");
                bool hasShed = checks.OfType<JObject>().Any(c => (string)c["Level"] == "Shedding");
                bool hasStop = checks.OfType<JObject>().Any(c => (string)c["Level"] == "Stop")
                               || (vote != null && (string)vote["Level"] == "Stop");

                var w = new ucWarn
                {
                    Key = name,
                    Title = name,
                    Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134),
                    Size = new Size(ROW_W, 37),
                    Margin = new Padding(LEFT, 2, 0, 2),
                    ShowAlarmButton = hasAlarm,
                    ShowSheddingButton = hasShed,
                    ShowStopButton = hasStop
                };

                _flow.Controls.Add(w);
                _map[name] = w;   // faultId == rule.Name（引擎 BuildConditions: dict[rule.Name]=fc）
            }
        }

        // ── 故障回调：仅设 CurrentFault，由 ucWarn 自行点灯（同 240） ──
        private void OnFaultDetected(string faultId, FaultState faultState, WarnTypeEnum warnType)
        {
            if (this.IsDisposed) return;

            if (this.InvokeRequired)
            {
                try
                {
                    this.BeginInvoke(new Action<string, FaultState, WarnTypeEnum>(OnFaultDetected),
                        faultId, faultState, warnType);
                }
                catch { /* 窗体已关闭，忽略 */ }
                return;
            }

            // 只处理 ECM 故障（与 ucWarnList 一致）；faultState 为空时不强制过滤
            if (faultState != null && faultState.FaultType != FaultTypeEnum.ecm) return;

            if (faultId != null && _map.TryGetValue(faultId, out ucWarn w))
            {
                w.RestartSwitch();          // 先全灭，保证只反映当前级别（None 即全灭）
                w.CurrentFault = warnType;  // Alarm/Shedding/Stop → 对应灯亮
            }
        }

        // ── helpers ──
        private JArray LoadRules()
        {
            try
            {
                string path = EcmProfileStore.PathOf(_model);
                if (string.IsNullOrEmpty(path) || !File.Exists(path)) return new JArray();
                var root = JObject.Parse(File.ReadAllText(path, System.Text.Encoding.UTF8));
                return root["Rules"] as JArray ?? new JArray();
            }
            catch (Exception ex)
            {
                try { Var.LogInfo("frmCurrentWarnDynamic.LoadRules 失败: " + ex.Message); } catch { }
                return new JArray();
            }
        }

        private Label HeadLabel(string text, int x)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Font = new Font("微软雅黑", 14F, FontStyle.Regular, GraphicsUnit.Point, 134),
                Location = new Point(x, 8)
            };
        }

        private Label SectionLabel(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = false,
                Size = new Size(ROW_W, 30),
                Margin = new Padding(LEFT, 6, 0, 2),
                Font = new Font("宋体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134),
                ForeColor = Color.FromArgb(60, 90, 130),
                BackColor = Color.FromArgb(225, 233, 245),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };
        }
    }
}
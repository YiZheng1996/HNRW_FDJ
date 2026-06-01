using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using MainUI.Fault.Engine;
using MainUI.Global;

namespace MainUI.Fault
{
    /// <summary>
    /// 数据驱动型号(任何存在 {型号}.faults.json 的型号,如 Z12V280ZJ/Z12V265ZJ...)的通用报警阈值图形编辑器。
    /// 完全按 {型号}.faults.json 动态生成界面：每条规则一个分组框，
    /// 组内把每个 Check 的每个 Term 的阈值(Value)、Duration、DeratePercent、
    /// 表决的 Value/ValidLow 渲染成可编辑的数字框。工艺员只改数字，结构只读。
    /// 保存时把数字写回内存 profile 再持久化到 JSON，并刷新引擎。
    ///
    /// 该窗体只服务“有 JSON 的型号”。240 仍走原 frmWarnParaConfig，互不影响。
    /// 全代码构建，无需 Designer 文件。
    /// </summary>
    public partial class frmWarnParaDynamic : Form
    {
        private readonly string _model;
        private EcmFaultProfile _profile;

        // 记录每个数字框 -> 写回目标（用委托封装“写回哪个字段”）
        private readonly List<Binding> _bindings = new List<Binding>();

        private class Binding
        {
            public Control Editor;            // UIDoubleUpDown 或 NumericUpDown
            public Func<double> Getter;       // 当前模型值
            public Action<double> Setter;     // 写回模型
        }

        private FlowLayoutPanel _root;
        private Button _btnSave;
        private Button _btnClose;

        public frmWarnParaDynamic(string model)
        {
            _model = model;
            BuildShell();
            if (this.DesignMode) return;
            LoadAndRender();
        }

        // ───────────────────────── 外壳 ─────────────────────────
        private void BuildShell()
        {
            this.Text = "报警参数设置 - " + _model;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(900, 760);
            this.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));

            _root = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(12)
            };
            this.Controls.Add(_root);

            var bottom = new Panel { Dock = DockStyle.Bottom, Height = 56 };
            _btnSave = new Button
            {
                Text = "保存",
                Size = new Size(120, 38),
                Location = new Point(620, 9),
                Anchor = AnchorStyles.Right | AnchorStyles.Top
            };
            _btnSave.Click += BtnSave_Click;
            _btnClose = new Button
            {
                Text = "关闭",
                Size = new Size(120, 38),
                Location = new Point(750, 9),
                Anchor = AnchorStyles.Right | AnchorStyles.Top
            };
            _btnClose.Click += (s, e) => this.Close();
            bottom.Controls.Add(_btnSave);
            bottom.Controls.Add(_btnClose);
            this.Controls.Add(bottom);
        }

        // ───────────────────────── 加载并渲染 ─────────────────────────
        private void LoadAndRender()
        {
            _profile = EcmProfileStore.Load(_model);
            if (_profile == null)
            {
                MessageBox.Show(this, "未找到型号配置文件：" + EcmProfileStore.PathOf(_model),
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _root.SuspendLayout();
            _root.Controls.Clear();
            _bindings.Clear();

            foreach (var rule in _profile.Rules)
            {
                _root.Controls.Add(BuildRulePanel(rule));
            }

            _root.ResumeLayout();
        }

        // 一条规则 -> 一个分组框
        private Control BuildRulePanel(RuleDef rule)
        {
            var box = new GroupBox
            {
                Text = rule.Name,
                Width = 840,
                Font = new Font("微软雅黑", 11F, FontStyle.Bold),
                Margin = new Padding(0, 0, 0, 10),
                Padding = new Padding(8)
            };

            var inner = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                Font = new Font("微软雅黑", 10.5F, FontStyle.Regular)
            };

            int rows = 0;

            // 普通 Checks
            if (rule.Checks != null)
            {
                foreach (var chk in rule.Checks)
                {
                    string levelCn = LevelCn(chk.Level);
                    // 每个 Term 渲染一行：  [级别] 左量 比较符 [数字框]
                    for (int ti = 0; ti < chk.Terms.Count; ti++)
                    {
                        var term = chk.Terms[ti];
                        // RightSignal 项没有可改数字，显示为只读说明
                        if (term.Value.HasValue)
                        {
                            inner.Controls.Add(MakeValueRow(
                                levelCn, term.Left, OpCn(term.Op),
                                () => term.Value.Value, v => term.Value = v));
                            rows++;
                        }
                        else
                        {
                            inner.Controls.Add(MakeReadonlyRow(
                                levelCn + "  " + term.Left + " " + OpCn(term.Op) + " " + (term.RightSignal ?? "")));
                        }
                    }
                    // Duration
                    if (chk.Duration > 0 || NeedDuration(rule, chk))
                    {
                        var c = chk; // 闭包捕获
                        inner.Controls.Add(MakeValueRow(
                            levelCn, "持续时间", "(秒)",
                            () => c.Duration, v => c.Duration = (int)Math.Round(v)));
                        rows++;
                    }
                    // 降载百分比(仅元数据，可改)
                    if (chk.DeratePercent > 0)
                    {
                        var c = chk;
                        inner.Controls.Add(MakeValueRow(
                            levelCn, "降载百分比", "(%)",
                            () => c.DeratePercent, v => c.DeratePercent = v));
                        rows++;
                    }
                    if (chk.UnloadAfterSec > 0)
                    {
                        var c = chk;
                        inner.Controls.Add(MakeValueRow(
                            levelCn, "持续后卸载", "(秒)",
                            () => c.UnloadAfterSec, v => c.UnloadAfterSec = (int)Math.Round(v)));
                        rows++;
                    }
                }
            }

            // 表决规则
            if (rule.Vote != null)
            {
                var v = rule.Vote;
                string levelCn = LevelCn(v.Level);
                inner.Controls.Add(MakeReadonlyRow(
                    levelCn + "  传感器表决：" + string.Join(" / ", v.Sensors.ToArray())));
                inner.Controls.Add(MakeValueRow(
                    levelCn, "判定阈值(任一有效传感器)", OpCn(v.Op),
                    () => v.Value, x => v.Value = x));
                rows++;
                if (v.ValidLow.HasValue)
                {
                    inner.Controls.Add(MakeValueRow(
                        levelCn, "传感器有效下限(低于视为故障屏蔽)", ">=",
                        () => v.ValidLow.Value, x => v.ValidLow = x));
                    rows++;
                }
                if (v.ValidHigh.HasValue)
                {
                    inner.Controls.Add(MakeValueRow(
                        levelCn, "传感器有效上限(高于视为故障屏蔽)", "<=",
                        () => v.ValidHigh.Value, x => v.ValidHigh = x));
                    rows++;
                }
                if (v.Duration > 0)
                {
                    inner.Controls.Add(MakeValueRow(
                        levelCn, "持续时间", "(秒)",
                        () => v.Duration, x => v.Duration = (int)Math.Round(x)));
                    rows++;
                }
            }

            if (!string.IsNullOrEmpty(rule.Note))
            {
                inner.Controls.Add(MakeReadonlyRow("说明：" + rule.Note, true));
            }

            box.Controls.Add(inner);
            box.Height = 46 + Math.Max(1, inner.Controls.Count) * 38;
            return box;
        }

        // 一行可编辑：  级别  左量  比较符  [数字框]
        private Control MakeValueRow(string level, string left, string op, Func<double> getter, Action<double> setter)
        {
            var row = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                Margin = new Padding(0, 2, 0, 2)
            };

            var lblLevel = new Label { Text = "[" + level + "]", AutoSize = true, Width = 70, Margin = new Padding(0, 6, 8, 0), ForeColor = LevelColor(level) };
            var lblDesc = new Label { Text = left + " " + op, AutoSize = true, Margin = new Padding(0, 6, 8, 0) };

            // 用 Sunny.UI.UIDoubleUpDown 与现有参数页风格一致；若类型不可用可改回 NumericUpDown
            var ed = new Sunny.UI.UIDoubleUpDown
            {
                Width = 160,
                DecimalPlaces = 2,
                Minimum = -100000,
                Maximum = 1000000,
                Value = getter()
            };

            _bindings.Add(new Binding { Editor = ed, Getter = getter, Setter = setter });

            row.Controls.Add(lblLevel);
            row.Controls.Add(lblDesc);
            row.Controls.Add(ed);
            return row;
        }

        private Control MakeReadonlyRow(string text, bool dim = false)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Margin = new Padding(0, 4, 0, 4),
                ForeColor = dim ? Color.Gray : Color.DimGray
            };
        }

        // ───────────────────────── 保存 ─────────────────────────
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) 把所有编辑框的值写回内存模型
                foreach (var b in _bindings)
                {
                    double val = Convert.ToDouble(((dynamic)b.Editor).Value);
                    b.Setter(val);
                }

                // 2) 持久化到 JSON
                if (!EcmProfileStore.Save(_profile))
                {
                    MessageBox.Show(this, "保存失败，请检查文件权限或日志。", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 3) 通知引擎重载该型号判据（仅当前型号即时生效）
                try
                {
                    if (Var.FaultService != null &&
                        string.Equals(Var.SysConfig.LastModel, _model, StringComparison.OrdinalIgnoreCase))
                    {
                        Var.FaultService.ReloadEcmProfileIfActive();
                    }
                }
                catch { /* 重载失败不影响保存结果，下次启动也会生效 */ }

                MessageBox.Show(this, "保存成功。", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Var.LogInfo("保存型号报警参数出错: " + ex.Message);
                MessageBox.Show(this, "保存出错：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ───────────────────────── 文案 ─────────────────────────
        private static string LevelCn(string level)
        {
            switch ((level ?? "").ToLowerInvariant())
            {
                case "stop": return "停机";
                case "shedding": return "降载";
                case "alarm": return "报警";
                case "record": return "仅记录";
                default: return level;
            }
        }
        private static Color LevelColor(string levelCn)
        {
            switch (levelCn)
            {
                case "停机": return Color.Firebrick;
                case "降载": return Color.DarkOrange;
                case "报警": return Color.Goldenrod;
                case "仅记录": return Color.SteelBlue;
                default: return Color.DimGray;
            }
        }
        private static string OpCn(string op)
        {
            switch ((op ?? "").ToUpperInvariant())
            {
                case "GE": return ">=";
                case "GT": return ">";
                case "LE": return "<=";
                case "LT": return "<";
                case "EQ": return "==";
                case "NE": return "!=";
                default: return op;
            }
        }
        private static bool NeedDuration(RuleDef rule, CheckDef chk) { return false; }
    }
}
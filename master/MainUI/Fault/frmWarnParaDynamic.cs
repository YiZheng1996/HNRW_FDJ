using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Sunny.UI;

namespace MainUI.Fault
{
    /// <summary>
    /// 数据驱动的报警参数编辑窗体。
    /// 设计原则（与 frmWarnParaConfig 对应，但不再手摆坐标）：
    ///   1. 结构只读：参数名 / 列归属 / 段数 / 算子 全部由 {型号}.faults.json 决定，界面不可改；
    ///   2. 只放开数字：每个数值叶子（Value / Duration / DeratePercent / UnloadAfterSec / Vote.Value / ValidLow）
    ///      对应一个 UIDoubleUpDown，保存时写回该 JSON 节点；
    ///   3. 直接绑 JObject 节点，不依赖任何 POCO 类——对任意型号的 JSON 都通用，未知字段原样保留；
    ///   4. 加法、不阻断：240 等无 JSON 的型号根本不会进这个窗体（由调用方判断），原 frmWarnParaConfig 不动。
    ///
    /// 列归属：Alarm/Record→报警列(Record 标"仅记录")，Shedding→降载列，Stop 与 Vote→停机列。
    /// </summary>
    public partial class frmWarnParaDynamic : Form
    {
        // ---- 列宽（沿用参考窗体的大画布尺度，宋体 16pt + UpDown 121px） ----
        private const int COL_NAME = 240;
        private const int COL_ALARM = 460;
        private const int COL_DERATE = 360;
        private const int COL_STOP = 360;

        private static readonly Font FontName = new Font("宋体", 14F, FontStyle.Bold, GraphicsUnit.Point, 134);
        private static readonly Font FontRange = new Font("宋体", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        private static readonly Font FontLab = new Font("宋体", 13F, FontStyle.Regular, GraphicsUnit.Point, 134);
        private static readonly Font FontUd = new Font("宋体", 16F, FontStyle.Regular, GraphicsUnit.Point, 134);

        private readonly string _jsonPath;
        private JObject _root;
        // 每个编辑框 → 它要写回的 (父节点, 键名)。保存时统一回写，无需反射、无需闭包。
        private readonly List<Binding> _bindings = new List<Binding>();

        /// <summary>保存成功后触发，参数为型号名，供调用方让引擎热重载该型号配置（即时生效）。</summary>
        public event Action<string> ProfileSaved;

        private struct Binding
        {
            public JObject Parent;
            public string Key;
            public UIDoubleUpDown Editor;
        }

        public frmWarnParaDynamic(string jsonPath)
        {
            _jsonPath = jsonPath;
            BuildShell();
            LoadAndRender();
        }

        // 外壳：标题 / 冻结表头 / 滚动区 / 按钮
        private TableLayoutPanel _headerTlp;
        private Panel _scroll;
        private TableLayoutPanel _body;

        private void BuildShell()
        {
            SuspendLayout();
            Text = "报警参数设置";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(COL_NAME + COL_ALARM + COL_DERATE + COL_STOP + 24, 880);
            Font = FontLab;

            // 冻结表头（与内容区用同一套列宽，右侧补一个滚动条宽度以对齐）
            _headerTlp = MakeColumnedTlp(rowCount: 1);
            _headerTlp.Dock = DockStyle.Top;
            _headerTlp.Height = 56;
            _headerTlp.Padding = new Padding(0, 0, SystemInformation.VerticalScrollBarWidth, 0);
            _headerTlp.BackColor = Color.FromArgb(243, 246, 250);
            _headerTlp.Controls.Add(MakeHeadCell("报警参数名称"), 0, 0);
            _headerTlp.Controls.Add(MakeHeadCell("报警条件"), 1, 0);
            _headerTlp.Controls.Add(MakeHeadCell("降载条件"), 2, 0);
            _headerTlp.Controls.Add(MakeHeadCell("停机条件"), 3, 0);

            // 底部按钮条
            var foot = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 64,
                Padding = new Padding(0, 12, 16, 12),
                BackColor = Color.FromArgb(243, 249, 255)
            };
             
            var btnExit = new UIButton
            {
                Text = "退出",
                Width = 140,
                Height = 40,
                Dock = DockStyle.Right,
                FillColor = Color.FromArgb(231, 76, 60),
                RectColor = Color.FromArgb(231, 76, 60)
            };

            var btnSave = new UIButton
            {
                Text = "保存",
                Width = 140,
                Height = 40,
                Dock = DockStyle.Right,
                Margin = new Padding(0, 0, 12, 0)
            };
            btnExit.Click += (s, e) => Close();
            btnSave.Click += (s, e) => Save();
            foot.Controls.Add(btnSave);
            foot.Controls.Add(new Panel { Width = 12, Dock = DockStyle.Right });
            foot.Controls.Add(btnExit);

            // 中间滚动区
            _scroll = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            EnableDoubleBuffer(_scroll);

            Controls.Add(_scroll);
            Controls.Add(_headerTlp);
            Controls.Add(foot);
            ResumeLayout(false);
        }

        private void LoadAndRender()
        {
            _root = JObject.Parse(File.ReadAllText(_jsonPath));
            var model = (string)_root["Model"] ?? Path.GetFileNameWithoutExtension(_jsonPath);
            Text = "报警参数设置 — " + model;

            _scroll.SuspendLayout();
            _scroll.Controls.Clear();
            _bindings.Clear();

            var rules = _root["Rules"] as JArray ?? new JArray();

            // 先数要插几条标题（Section 变化次数），好把行数算够
            int headerCount = 0; string scan = null;
            foreach (var rt in rules)
            {
                var s = (string)((JObject)rt)["Section"];
                if (!string.IsNullOrEmpty(s) && s != scan) { headerCount++; scan = s; }
            }

            _body = MakeColumnedTlp(rowCount: rules.Count + headerCount);   // ← 行数 += 标题数
            _body.AutoSize = true;
            _body.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            int row = 0; string lastSection = null;
            foreach (var ruleTok in rules)
            {
                var rule = (JObject)ruleTok;
                var section = (string)rule["Section"];
                if (!string.IsNullOrEmpty(section) && section != lastSection)
                {
                    AddSectionRow(row, section);     // 插标题行
                    lastSection = section;
                    row++;
                }

                var checks = rule["Checks"] as JArray ?? new JArray();
                var vote = rule["Vote"] as JObject;
                _body.Controls.Add(BuildNameCell(rule), 0, row);
                _body.Controls.Add(BuildCheckColumn(checks, new[] { "Alarm", "Record" }), 1, row);
                _body.Controls.Add(BuildCheckColumn(checks, new[] { "Shedding" }), 2, row);
                _body.Controls.Add(BuildStopColumn(checks, vote, rule), 3, row);
                row++;
            }

            _scroll.Controls.Add(_body);
            _scroll.ResumeLayout(true);
        }

        // 分组，如硬线信号
        private void AddSectionRow(int row, string text)
        {
            var lab = new Label
            {
                Text = text,
                Font = new Font("宋体", 13F, FontStyle.Bold, GraphicsUnit.Point, 134),
                ForeColor = Color.FromArgb(60, 90, 130),
                BackColor = Color.FromArgb(225, 233, 245),   // 标题底色，按需调
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0),
                Dock = DockStyle.Fill,
                AutoSize = false,
                Height = 32
            };
            _body.Controls.Add(lab, 0, row);
            _body.SetColumnSpan(lab, 4);   // ← 跨满 4 列，成为整行标题
        }

        // 单元格构建
        private Control BuildNameCell(JObject rule)
        {
            var p = NewCellFlow();
            var name = (string)rule["Name"] ?? "(未命名)";
            p.Controls.Add(new Label { Text = name, Font = FontName, AutoSize = true, Margin = new Padding(3, 4, 3, 0) });

            var range = ResolveRange(FirstSignalOf(rule));
            if (range != null)
                p.Controls.Add(new Label
                {
                    Text = string.Format("量程 {0}", range.Display),
                    Font = FontRange,
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Margin = new Padding(3, 0, 3, 4)
                });
            return p;
        }

        /// <summary>报警/降载列：把命中给定 Level 的 Check 各画一块；同列多块自动编 ①②。</summary>
        private Control BuildCheckColumn(JArray checks, string[] levels)
        {
            var hit = checks.Cast<JObject>().Where(c => levels.Contains((string)c["Level"])).ToList();
            if (hit.Count == 0) return DashCell();

            var col = NewCellFlow();
            for (int i = 0; i < hit.Count; i++)
            {
                var block = NewCellFlow();
                block.Margin = new Padding(0, i == 0 ? 4 : 8, 0, 0);
                var tags = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight, AutoSize = true, WrapContents = false, Margin = new Padding(0) };
                if (hit.Count > 1) tags.Controls.Add(Tag("①②③④".Substring(i, 1), Color.FromArgb(55, 138, 221)));
                if ((string)hit[i]["Level"] == "Record") tags.Controls.Add(Tag("仅记录", Color.Gray));
                if (tags.Controls.Count > 0) block.Controls.Add(tags);

                AddTermLines(block, hit[i]);
                AddMetaLines(block, hit[i]);
                col.Controls.Add(block);
            }
            return col;
        }

        /// <summary>停机列：Stop 级 Check + Vote（双传感器表决）。</summary>
        private Control BuildStopColumn(JArray checks, JObject vote, JObject rule)
        {
            var stops = checks.Cast<JObject>().Where(c => (string)c["Level"] == "Stop").ToList();
            if (stops.Count == 0 && vote == null) return DashCell();

            var col = NewCellFlow();
            foreach (var c in stops) { var b = NewCellFlow(); AddTermLines(b, c); AddMetaLines(b, c); col.Controls.Add(b); }

            if (vote != null)
            {
                var b = NewCellFlow();
                b.Margin = new Padding(0, stops.Count > 0 ? 8 : 4, 0, 0);
                var tagLine = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight, AutoSize = true, WrapContents = false, Margin = new Padding(0) };
                tagLine.Controls.Add(Tag("双传感器表决", Color.FromArgb(186, 117, 23)));
                b.Controls.Add(tagLine);

                string unit = UnitOf(vote["Sensors"]?.FirstOrDefault()?.ToString());
                b.Controls.Add(TermLine("任一/两路 " + OpSym((string)vote["Op"]), vote, "Value", ResolveRange((string)vote["Sensors"]?.FirstOrDefault()), unit));
                if (vote["ValidLow"] != null)
                    b.Controls.Add(TermLine("屏蔽阈值 ＜", vote, "ValidLow", ResolveRange((string)vote["Sensors"]?.FirstOrDefault()), unit));
                if (vote["Duration"] != null && (double)vote["Duration"] > 0)
                    b.Controls.Add(TermLine("持续", vote, "Duration", Range.Seconds, "s"));
                col.Controls.Add(b);
            }
            return col;
        }

        /// <summary>一个 Check 内的所有 Term 各成一行。</summary>
        private void AddTermLines(FlowLayoutPanel block, JObject check)
        {
            var terms = check["Terms"] as JArray;
            if (terms == null) return;
            foreach (var t in terms.Cast<JObject>())
            {
                string left = (string)t["Left"];
                string label = DescribeLeft(left) + " " + OpSym((string)t["Op"]);
                block.Controls.Add(TermLine(label, t, "Value", ResolveRange(InnerName(left)), UnitOf(left)));
            }
        }

        /// <summary>Check 级的附加数值：降载% / 持续s / 持续后卸载s。</summary>
        private void AddMetaLines(FlowLayoutPanel block, JObject check)
        {
            if (check["DeratePercent"] != null) block.Controls.Add(TermLine("降载", check, "DeratePercent", Range.Percent, "%"));
            if (check["Duration"] != null && (double)check["Duration"] > 0) block.Controls.Add(TermLine("持续", check, "Duration", Range.Seconds, "s"));
            if (check["UnloadAfterSec"] != null) block.Controls.Add(TermLine("持续后卸载", check, "UnloadAfterSec", Range.Seconds, "s"));
        }

        /// <summary>标签 + UIDoubleUpDown + 单位，并登记回写绑定。</summary>
        private FlowLayoutPanel TermLine(string label, JObject parent, string key, Range range, string unit)
        {
            var line = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight, AutoSize = true, WrapContents = false, Margin = new Padding(0, 2, 0, 2) };
            line.Controls.Add(new Label { Text = label, Font = FontLab, ForeColor = Color.DimGray, AutoSize = true, TextAlign = ContentAlignment.MiddleRight, Margin = new Padding(0, 7, 6, 0) });

            double val = parent[key] != null ? (double)parent[key] : 0D;
            var ud = new UIDoubleUpDown
            {
                Font = FontUd,
                Width = 121,
                Height = 30,
                Minimum = range != null ? range.Min : 0D,
                Maximum = range != null ? range.Max : 100000D,
                DecimalPlaces = range != null ? range.Decimals : 0,
                Step = range != null ? range.Step : 1D,
                ShowText = false,
                TextAlignment = ContentAlignment.MiddleCenter,
                Style = UIStyle.Custom,
                Value = val
            };
            line.Controls.Add(ud);
            if (!string.IsNullOrEmpty(unit))
                line.Controls.Add(new Label { Text = unit, Font = FontLab, ForeColor = Color.Gray, AutoSize = true, Margin = new Padding(6, 7, 0, 0) });

            _bindings.Add(new Binding { Parent = parent, Key = key, Editor = ud });
            return line;
        }

        // 保存
        private void Save()
        {
            // 仅回写数值，结构与未知字段原样保留
            foreach (var b in _bindings)
            {
                double v = b.Editor.Value;
                // DecimalPlaces==0 的字段(Duration/降载%/卸载s/ValidLow 及整数阈值)写成整数，
                // 避免 3 → 3.0 让引擎的 int 字段反序列化报错
                if (b.Editor.DecimalPlaces == 0)
                    b.Parent[b.Key] = (long)Math.Round(v);
                else
                    b.Parent[b.Key] = v;     // 0.6 这类真小数照常
            }

            // 轻量合理性提示：报警阈值不应比停机更严苛等——只警告、不阻断（保持加法风格）
            string warn = SanityCheck();
            if (!string.IsNullOrEmpty(warn))
            {
                var r = MessageBox.Show(warn + "\r\n\r\n仍要保存吗？", "数值提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r != DialogResult.Yes) return;
            }

            File.WriteAllText(_jsonPath, _root.ToString(Newtonsoft.Json.Formatting.Indented));
            ProfileSaved?.Invoke((string)_root["Model"]);
            UIMessageTip.ShowOk("已保存并即时生效");
        }

        private string SanityCheck()
        {
            // 占位：可在此按规则名比较 报警 < 降载 < 停机 的阈值方向，发现倒挂则提示。
            // 默认不拦截，返回空串表示无异常。
            return string.Empty;
        }

        // 量程 / 单位 / 算子 / 备注
        private class Range
        {
            public double Min, Max, Step;
            public int Decimals;
            public string Display;
            public static readonly Range Percent = new Range { Min = 0, Max = 100, Step = 5, Decimals = 0, Display = "0–100 %" };
            public static readonly Range Seconds = new Range { Min = 0, Max = 600, Step = 1, Decimals = 0, Display = "0–600 s" };
        }

        /// <summary>
        /// 解析信号量程。优先读 JSON 中 Signal 上声明的 Min/Max/Decimals/Unit（推荐做法，最显式）；
        /// 缺失则按名称关键字给保守缺省值。量程只作输入护栏，不影响判据。
        /// </summary>
        private Range ResolveRange(string signalName)
        {
            if (string.IsNullOrEmpty(signalName)) return null;
            var sig = (_root["Signals"] as JArray)?.Cast<JObject>()
                .FirstOrDefault(s => (string)s["Name"] == signalName);
            if (sig == null)
            {
                // 可能是组名（如“增压器转速”“A缸排气”“轴温”），取首成员
                var grp = (_root["Groups"] as JArray)?.Cast<JObject>()
                    .FirstOrDefault(g => (string)g["Name"] == signalName);
                var first = grp?["Members"]?.FirstOrDefault()?.ToString();
                if (first != null) return ResolveRange(first);
            }

            double? min = sig != null ? (double?)sig["Min"] : null;
            double? max = sig != null ? (double?)sig["Max"] : null;
            int? dec = sig != null ? (int?)sig["Decimals"] : null;
            if (min.HasValue && max.HasValue)
                return new Range { Min = min.Value, Max = max.Value, Decimals = dec ?? DecimalsFor(max.Value), Step = max.Value <= 2 ? 0.1 : 1, Display = string.Format("{0}–{1} {2}", min.Value, max.Value, UnitOf(signalName)) };

            // 关键字兜底
            if (signalName.Contains("增压") && signalName.Contains("转速"))
                return new Range { Min = 0, Max = 60000, Step = 100, Decimals = 0, Display = "0–60000 rpm" };
            if (signalName.Contains("转速"))
                return new Range { Min = 0, Max = 1200, Step = 10, Decimals = 0, Display = "0–1200 rpm" };
            if (signalName.Contains("排气") || signalName.Contains("涡前")) return new Range { Min = 0, Max = 800, Step = 1, Decimals = 0, Display = "0–800 ℃" };
            if (signalName.Contains("温度") || signalName.Contains("油温") || signalName.Contains("轴温")) return new Range { Min = 0, Max = 150, Step = 1, Decimals = 0, Display = "0–150 ℃" };
            if (signalName.Contains("曲轴箱")) return new Range { Min = 0, Max = 1, Step = 0.1, Decimals = 2, Display = "0–1 kPa" };
            if (signalName.Contains("油压") || signalName.Contains("压力")) return new Range { Min = 0, Max = 1000, Step = 5, Decimals = 0, Display = "0–1000 kPa" };
            if (signalName.Contains("功率")) return new Range { Min = 0, Max = 5000, Step = 10, Decimals = 0, Display = "0–5000 kW" };
            return null;
        }

        private static int DecimalsFor(double max) { return max <= 2 ? 2 : (max <= 10 ? 1 : 0); }

        private string UnitOf(string name)
        {
            if (string.IsNullOrEmpty(name)) return "";
            var sig = (_root["Signals"] as JArray)?.Cast<JObject>().FirstOrDefault(s => (string)s["Name"] == InnerName(name));
            var u = sig != null ? (string)sig["Unit"] : null;
            if (!string.IsNullOrEmpty(u)) return u;
            name = InnerName(name);
            if (name.Contains("转速")) return "rpm";
            if (name.Contains("功率")) return "kW";
            if (name.Contains("排气") || name.Contains("涡前") || name.Contains("温度") || name.Contains("油温") || name.Contains("轴温")) return "℃";
            if (name.Contains("油压") || name.Contains("压力") || name.Contains("曲轴箱")) return "kPa";
            return "";
        }

        private static string OpSym(string op)
        {
            switch (op) { case "GT": return "＞"; case "GE": return "≥"; case "LT": return "＜"; case "LE": return "≤"; case "EQ": return "＝"; default: return op; }
        }

        // 把 MAX(增压器转速) → 增压器转速(最大)；SPREAD(A缸排气) → A缸排气(温差)
        private static string DescribeLeft(string left)
        {
            if (string.IsNullOrEmpty(left)) return "";
            if (left.StartsWith("MAX(")) return InnerName(left) + "(最大)";
            if (left.StartsWith("MIN(")) return InnerName(left) + "(最小)";
            if (left.StartsWith("SPREAD(")) return InnerName(left) + "(温差)";
            return left;
        }

        private static string InnerName(string left)
        {
            if (string.IsNullOrEmpty(left)) return left;
            int a = left.IndexOf('('), b = left.LastIndexOf(')');
            return (a >= 0 && b > a) ? left.Substring(a + 1, b - a - 1) : left;
        }

        private string FirstSignalOf(JObject rule)
        {
            var t = (rule["Checks"] as JArray)?.Cast<JObject>().SelectMany(c => (c["Terms"] as JArray)?.Cast<JObject>() ?? Enumerable.Empty<JObject>()).FirstOrDefault();
            if (t != null) return InnerName((string)t["Left"]);
            return (rule["Vote"]?["Sensors"]?.FirstOrDefault())?.ToString();
        }

        // 小部件 / 布局工具
        private TableLayoutPanel MakeColumnedTlp(int rowCount)
        {
            var tlp = new TableLayoutPanel
            {
                ColumnCount = 4,
                RowCount = Math.Max(1, rowCount),
                Margin = new Padding(0),
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                GrowStyle = TableLayoutPanelGrowStyle.AddRows,
                BackColor = Color.FromArgb(243, 249, 255)
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, COL_NAME));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, COL_ALARM));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, COL_DERATE));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, COL_STOP));
            for (int i = 0; i < Math.Max(1, rowCount); i++) tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            return tlp;
        }

        private static FlowLayoutPanel NewCellFlow()
        {
            return new FlowLayoutPanel { FlowDirection = FlowDirection.TopDown, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, WrapContents = false, Margin = new Padding(4, 4, 4, 4), Dock = DockStyle.Fill };
        }

        private static Control DashCell()
        {
            var p = NewCellFlow();
            p.Controls.Add(new Label { Text = "—", Font = FontLab, ForeColor = Color.Silver, AutoSize = true, Margin = new Padding(6, 6, 0, 0) });
            return p;
        }

        private static Label MakeHeadCell(string text)
        {
            return new Label { Text = text, Font = new Font("宋体", 14F, FontStyle.Bold, GraphicsUnit.Point, 134), TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill };
        }

        private static Control Tag(string text, Color color)
        {
            return new Label { Text = text, Font = new Font("宋体", 10F, FontStyle.Regular, GraphicsUnit.Point, 134), ForeColor = color, AutoSize = true, Margin = new Padding(0, 3, 6, 0) };
        }

        private void PaintRow(int row, Color color)
        {
            for (int c = 0; c < 4; c++)
            {
                var ctl = _body.GetControlFromPosition(c, row);
                if (ctl != null) ctl.BackColor = color;
            }
        }

        private static void EnableDoubleBuffer(Control c)
        {
            typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(c, true, null);
        }
    }
}
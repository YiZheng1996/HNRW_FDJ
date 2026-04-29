using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MainUI.Global;
using MainUI.Simulate;
using MetorSignalSimulator.UI.Model;

namespace MainUI
{
    /// <summary>
    /// TRDP 数据模拟调试窗口。
    ///
    /// 使用方式：new frmTRDPSimulator().Show();
    ///
    /// 动态内容（模拟量/数字量/轴温 Tab）在打开时及每次
    /// EventTriggerModel.OnModelNameChanged 触发后自动从
    /// Var.TRDP.tags 重建，确保与 LoadTRDPConfig 加载的 Excel 完全同步。
    /// 通讯状态 Tab 为静态内容，不随型号变化。
    /// </summary>
    public partial class frmTRDPSimulator : Form
    {
        // ── 颜色常量 ────────────────────────────────────────────────────
        private static readonly Color C_NORMAL = Color.FromArgb(220, 237, 220);
        private static readonly Color C_FAULT = Color.FromArgb(255, 200, 200);

        // ── 动态控件字典（每次重建时清空重建） ──────────────────────────
        /// <summary>信号名 → (TrackBar, 值Label, NUD) 三元组，模拟量用</summary>
        private readonly Dictionary<string, AnalogEntry> _analogControls
            = new Dictionary<string, AnalogEntry>();

        /// <summary>信号名 → Button，数字量用</summary>
        private readonly Dictionary<string, Button> _digitalButtons
            = new Dictionary<string, Button>();

        // 生命信号自动递增开关状态（跨重建保持）
        private bool _lifeAuto = true;

        // ────────────────────────────────────────────────────────────────
        // 构造函数
        // ────────────────────────────────────────────────────────────────

        public frmTRDPSimulator()
        {
            InitializeComponent();

            // 绑定静态控件事件
            _btnStart.Click += btnStart_Click;
            _btnStop.Click += btnStop_Click;
            _btnSync.Click += btnSync_Click;
            btnClrLog.Click += (s, e) => _rtbLog.Clear();
            _btnLifeAuto.Click += btnLifeAuto_Click;

            // 定时器刷新 UI 状态栏
            _uiTimer.Tick += uiTimer_Tick;

            // 订阅型号切换事件
            EventTriggerModel.OnModelNameChanged += OnModelNameChanged;

            // 绑定 TRDPSimulatorService 日志输出
            BindServiceLog();

            // 初次构建动态 Tab（如果已有型号加载过 Excel）
            RebuildDynamicTabs();
        }

        // ────────────────────────────────────────────────────────────────
        // 工具栏事件
        // ────────────────────────────────────────────────────────────────

        private void btnStart_Click(object sender, EventArgs e)
        {
            TRDPSimulatorService.Instance.Start();
            _uiTimer.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            TRDPSimulatorService.Instance.Stop();
            _uiTimer.Stop();
        }

        /// <summary>手动触发与当前 Var.TRDP.tags 同步</summary>
        private void btnSync_Click(object sender, EventArgs e)
        {
            RebuildDynamicTabs();
        }

        private void uiTimer_Tick(object sender, EventArgs e)
        {
            var svc = TRDPSimulatorService.Instance;
            _lblTick.Text = string.Format("帧: {0}", svc.TickCount);
            _lblStatus.Text = svc.IsRunning ? "● 运行中" : "● 已停止";
            _lblStatus.ForeColor = svc.IsRunning
                ? Color.FromArgb(100, 230, 120)
                : Color.FromArgb(220, 100, 100);

            // 刷新生命信号显示
            _lblLifeVal.Text = string.Format("生命信号值：{0}", svc.LifeCounter);
        }

        private void btnLifeAuto_Click(object sender, EventArgs e)
        {
            _lifeAuto = !_lifeAuto;
            TRDPSimulatorService.Instance.SetLifeAuto(_lifeAuto);
            _btnLifeAuto.BackColor = _lifeAuto
                ? Color.FromArgb(40, 120, 60)
                : Color.FromArgb(160, 80, 20);
            _btnLifeAuto.Text = _lifeAuto
                ? "● 自动递增（已启用）"
                : "○ 自动递增（已停止）";
        }

        // ────────────────────────────────────────────────────────────────
        // 型号切换事件 → 重建动态 Tab
        // ────────────────────────────────────────────────────────────────

        private void OnModelNameChanged(string modelName)
        {
            if (IsDisposed || !IsHandleCreated) return;
            try
            {
                Invoke(new Action(RebuildDynamicTabs));
            }
            catch { /* 窗体已关闭，忽略 */ }
        }

        // ────────────────────────────────────────────────────────────────
        // 动态 Tab 重建（核心方法）
        // ────────────────────────────────────────────────────────────────

        /// <summary>
        /// 从 Var.TRDP.tags 读取当前已加载的信号列表，动态重建
        /// 模拟量/数字量/轴温三个 Tab 的全部控件。
        /// 必须在 UI 线程调用。
        /// </summary>
        private void RebuildDynamicTabs()
        {
            var tags = GetCurrentTags();

            // 清空旧控件及字典
            ClearTabContent(tpAnalog);
            ClearTabContent(tpDigital);
            ClearTabContent(tpAxis);
            _analogControls.Clear();
            _digitalButtons.Clear();

            if (tags.Count == 0)
            {
                AppendLog("当前未加载任何 TRDP 信号，请先在主界面选择型号。",
                    TRDPSimulatorService.LogLevel.Warn);
                return;
            }

            // 按数据类型分组
            var analogTags = tags.Where(t => IsAnalogType(t.DataType)).ToList();
            var digitalTags = tags.Where(t => t.DataType == "B1").ToList();
            var axisTags = tags.Where(t => t.DataLabel != null
                                          && t.DataLabel.Contains("轴温")).ToList();

            BuildAnalogTab(analogTags);
            BuildDigitalTab(digitalTags);
            BuildAxisTab(axisTags);

            AppendLog(
                string.Format("已同步 {0} 个信号（模拟量:{1} / 数字量:{2} / 轴温:{3}）",
                    tags.Count, analogTags.Count, digitalTags.Count, axisTags.Count),
                TRDPSimulatorService.LogLevel.OK);
        }

        // ────────────────────────────────────────────────────────────────
        // Tab A：模拟量
        // ────────────────────────────────────────────────────────────────

        private void BuildAnalogTab(List<FullTags> tags)
        {
            var panel = MakeScrollPanel();
            int y = 8;

            if (tags.Count == 0)
            {
                panel.Controls.Add(MakeLabel("当前型号无模拟量信号。", 9, Color.Gray, pt: new Point(8, 8)));
                tpAnalog.Controls.Add(panel);
                return;
            }

            AddSectionHeader(panel, ref y,
                string.Format("共 {0} 个模拟量信号（物理值范围来自协议定义）", tags.Count));

            foreach (var tag in tags)
            {
                if (tag.DataLabel != null && tag.DataLabel.Contains("轴温")) continue;
                string unit = tag.DataUnit ?? "";
                AddAnalogRow(panel, ref y, tag, unit);
            }

            tpAnalog.Controls.Add(panel);
        }

        /// <summary>
        /// 添加一行模拟量控件。
        /// TrackBar / NUD 全部操作【物理值】，InjectValue 直接注入物理值。
        /// </summary>
        private void AddAnalogRow(Panel parent, ref int y, FullTags tag, string unit)
        {
            string sigName = tag.DataLabel;
            decimal scale = tag.dataFormat <= 0m ? 1m : tag.dataFormat;
            int decPlaces = scale >= 1m ? 0 : (scale >= 0.1m ? 1 : 2);

            // ── 物理值范围（直接来自协议，不从数据类型反推）──────────────────
            decimal engMax = GetPhysicalMax(sigName, unit, scale);
            decimal engMin = 0m;

            // TrackBar 存物理值整数部分（步长1），NUD 提供精确小数
            int tbMax = (int)engMax;
            if (tbMax < 1) tbMax = 1;

            // ── 控件创建 ──────────────────────────────────────────────────────
            var lblName = MakeLabel(
                string.Format("{0}  [offset:{1} {2} ×{3}  max:{4}]",
                    sigName, tag.Offset, tag.DataType, scale, engMax),
                8.5f, Color.FromArgb(60, 60, 80), pt: new Point(8, y + 4));
            lblName.Size = new Size(290, 18);

            var tb = new TrackBar
            {
                Location = new Point(302, y),
                Size = new Size(340, 30),
                Minimum = 0,
                Maximum = tbMax,
                Value = 0,
                TickFrequency = Math.Max(1, tbMax / 20),
                SmallChange = 1,
                LargeChange = Math.Max(1, tbMax / 10)
            };

            var lblVal = MakeLabel(
                string.Format("0 {0}", unit),
                9f, Color.FromArgb(20, 80, 160), bold: true, pt: new Point(650, y + 5));
            lblVal.Size = new Size(95, 18);
            lblVal.TextAlign = ContentAlignment.MiddleRight;

            var nud = new NumericUpDown
            {
                Location = new Point(752, y + 4),
                Size = new Size(88, 22),
                Minimum = engMin,
                Maximum = engMax,
                DecimalPlaces = decPlaces,
                Increment = scale < 1m ? scale : 1m,
                Value = engMin,
                Font = new Font("Consolas", 9f)
            };

            // ── 双向同步 & 注入物理值 ─────────────────────────────────────────
            bool syncing = false;

            tb.ValueChanged += (s, e) =>
            {
                if (syncing) return;
                syncing = true;
                try
                {
                    // TrackBar.Value 直接就是物理值整数部分
                    decimal phyVal = tb.Value;
                    lblVal.Text = string.Format("{0} {1}",
                        phyVal.ToString("F" + decPlaces), unit);
                    if (phyVal >= nud.Minimum && phyVal <= nud.Maximum)
                        nud.Value = phyVal;
                    // 注入物理值
                    TRDPSimulatorService.Instance.InjectValue(sigName, phyVal);
                }
                finally { syncing = false; }
            };

            nud.ValueChanged += (s, e) =>
            {
                if (syncing) return;
                syncing = true;
                try
                {
                    decimal phyVal = nud.Value;
                    lblVal.Text = string.Format("{0} {1}",
                        phyVal.ToString("F" + decPlaces), unit);
                    int tbV = (int)phyVal;
                    if (tbV >= tb.Minimum && tbV <= tb.Maximum)
                        tb.Value = tbV;
                    // 注入物理值
                    TRDPSimulatorService.Instance.InjectValue(sigName, phyVal);
                }
                finally { syncing = false; }
            };

            parent.Controls.AddRange(new Control[] { lblName, tb, lblVal, nud });
            _analogControls[sigName] = new AnalogEntry
            { TrackBar = tb, ValueLabel = lblVal, Nud = nud };

            y += 34;
        }

        /// <summary>
        /// 根据协议定义返回信号的物理值上限。
        /// 来源：出口机车以太网TRDP通信_ECM通信模板_V1_0_1_.xlsx
        /// 协议规定：物理值 = 通信值 × scale
        /// </summary>
        private static decimal GetPhysicalMax(string sigName, string unit, decimal scale)
        {
            if (sigName == null) sigName = "";
            if (unit == null) unit = "";

            // ── 转速类（scale=0.1，物理最大约1500rpm；增压器最大60000rpm）────
            if (sigName.Contains("增压器转速"))
                return 60000m;
            if (sigName.Contains("转速") || sigName.Contains("相位传感器"))
                return 1500m;

            // ── 排气温度（scale=0.1，物理最大约1000℃）────────────────────────
            if (sigName.Contains("排气温度") || sigName.Contains("涡前排气"))
                return 1000m;

            // ── 普通温度（scale=0.1或1，物理最大约350℃）─────────────────────
            if (sigName.Contains("温度") || sigName.Contains("Temp") || unit.Contains("℃"))
                return 350m;

            // ── 电源电压（scale=0.01，物理最大36V）───────────────────────────
            if (sigName.Contains("电源") || sigName.Contains("Pwr"))
                return 36m;

            // ── 放大器（scale=0.01，物理最大约20A/V）─────────────────────────
            if (sigName.Contains("放大器") || sigName.Contains("Amplifier"))
                return 20m;

            // ── 压力类（scale=1，物理最大2500kPa）────────────────────────────
            if (sigName.Contains("压力") || sigName.Contains("油压") ||
                sigName.Contains("Pre") || unit.Contains("kPa"))
                return 2500m;

            // ── 燃油量（scale=0.1，物理最大120mm3/Str）───────────────────────
            if (sigName.Contains("燃油量") || sigName.Contains("Fuel"))
                return 120m;

            // ── 提前角（scale=0.01，物理最大25°BTDC）────────────────────────
            if (sigName.Contains("提前角") || sigName.Contains("Lead"))
                return 25m;

            // ── 持续期（scale=0.01，物理最大约6°crank）───────────────────────
            if (sigName.Contains("持续期") || sigName.Contains("Continue"))
                return 6m;

            // ── 运行时间（scale=1，最大9999h）────────────────────────────────
            if (sigName.Contains("运行时间") || sigName.Contains("RunTime"))
                return 9999m;

            // ── 默认兜底：按 scale 给合理范围 ────────────────────────────────
            if (scale >= 1m) return 2000m;
            if (scale >= 0.1m) return 500m;
            return 100m;
        }

        // ────────────────────────────────────────────────────────────────
        // Tab B：数字量
        // ────────────────────────────────────────────────────────────────

        private void BuildDigitalTab(List<FullTags> tags)
        {
            var panel = MakeScrollPanel();
            int y = 8;

            if (tags.Count == 0)
            {
                panel.Controls.Add(MakeLabel("当前型号无数字量（B1）信号。", 9, Color.Gray, pt: new Point(8, 8)));
                tpDigital.Controls.Add(panel);
                return;
            }

            AddSectionHeader(panel, ref y,
                string.Format("共 {0} 个数字量信号（B1），点击按钮切换0/1注入", tags.Count));

            foreach (var tag in tags)
            {
                AddDigitalRow(panel, ref y, tag);
            }

            // 全部清零按钮
            y += 6;
            var btnReset = new Button
            {
                Text = "全部清零（置0）",
                Location = new Point(8, y),
                Size = new Size(160, 28),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(140, 140, 140),
                ForeColor = Color.White,
                Font = new Font("微软雅黑", 9f),
                Cursor = Cursors.Hand
            };
            btnReset.Click += (s, e) => ResetAllDigitalButtons();
            panel.Controls.Add(btnReset);

            tpDigital.Controls.Add(panel);
        }

        private void AddDigitalRow(Panel parent, ref int y, FullTags tag)
        {
            string sigName = tag.DataLabel;
            bool isSyncSig = sigName != null && sigName.Contains("同步状态");
            bool defState = isSyncSig; // 同步状态默认1，其余默认0

            var lblName = MakeLabel(
                string.Format("{0}  [offset:{1} bit:{2}]", sigName, tag.Offset, tag.Bit),
                8.5f, Color.FromArgb(60, 60, 80), pt: new Point(8, y + 6));
            lblName.Size = new Size(280, 18);

            var btn = new Button
            {
                Location = new Point(296, y),
                Size = new Size(160, 26),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("微软雅黑", 8.5f),
                Cursor = Cursors.Hand,
                Tag = defState ? 1 : 0
            };
            btn.FlatAppearance.BorderColor = Color.Gray;
            ApplyDigitalButtonStyle(btn, defState ? 1 : 0, sigName);

            // 初始注入默认值
            TRDPSimulatorService.Instance.InjectValue(sigName, defState ? 1m : 0m);

            btn.Click += (s, e) =>
            {
                int cur = (int)btn.Tag;
                int next = cur == 0 ? 1 : 0;
                btn.Tag = next;
                ApplyDigitalButtonStyle(btn, next, sigName);
                TRDPSimulatorService.Instance.InjectValue(sigName, (decimal)next);
            };

            _digitalButtons[sigName] = btn;
            parent.Controls.AddRange(new Control[] { lblName, btn });
            y += 34;
        }

        private void ApplyDigitalButtonStyle(Button btn, int val, string sigName)
        {
            bool isSyncSig = sigName != null && sigName.Contains("同步状态");
            if (isSyncSig)
            {
                btn.BackColor = val == 1 ? Color.FromArgb(40, 130, 60) : Color.FromArgb(160, 50, 50);
                btn.ForeColor = Color.White;
                btn.Text = val == 1 ? "1  已同步（正常）" : "0  未同步（异常）";
            }
            else
            {
                bool isFault = val == 1;
                btn.BackColor = isFault ? C_FAULT : C_NORMAL;
                btn.ForeColor = isFault ? Color.FromArgb(160, 0, 0) : Color.FromArgb(30, 80, 30);
                btn.Text = isFault ? "1  ▲ 故障" : "0  正常";
            }
        }

        private void ResetAllDigitalButtons()
        {
            foreach (var kv in _digitalButtons)
            {
                bool isSyncSig = kv.Key != null && kv.Key.Contains("同步状态");
                int defVal = isSyncSig ? 1 : 0;
                kv.Value.Tag = defVal;
                ApplyDigitalButtonStyle(kv.Value, defVal, kv.Key);
                TRDPSimulatorService.Instance.InjectValue(kv.Key, (decimal)defVal);
            }
        }

        // ────────────────────────────────────────────────────────────────
        // Tab C：轴温
        // ────────────────────────────────────────────────────────────────

        private void BuildAxisTab(List<FullTags> tags)
        {
            var panel = MakeScrollPanel();
            int y = 8;

            if (tags.Count == 0)
            {
                panel.Controls.Add(MakeLabel(
                    "当前型号未检测到包含 [轴温] 的信号，请确认 Excel 中信号名包含 [轴温]关键字。",
                    9, Color.Gray, pt: new Point(8, 8)));
                tpAxis.Controls.Add(panel);
                return;
            }

            AddSectionHeader(panel, ref y,
                string.Format("共 {0} 个轴温信号（来自当前型号 Excel）", tags.Count));
            AddSectionHeader(panel, ref y,
                "降载阈值 / 停机阈值由 FaultConfig 配置，此处仅注入工程值（℃）",
                headerColor: Color.Gray, fontSize: 8f);

            foreach (var tag in tags)
            {
                AddAxisRow(panel, ref y, tag);
            }

            tpAxis.Controls.Add(panel);
        }

        private void AddAxisRow(Panel parent, ref int y, FullTags tag)
        {
            string sigName = tag.DataLabel;
            int defTemp = 70;
            decimal scale = tag.dataFormat == 0 ? 0.1m : tag.dataFormat;

            var lblName = MakeLabel(
                string.Format("{0}  [offset:{1}  ×{2}]", sigName, tag.Offset, scale),
                9f, Color.FromArgb(30, 56, 100), bold: true, pt: new Point(8, y + 8));
            lblName.AutoSize = true;

            var tb = new TrackBar
            {
                Location = new Point(240, y),
                Size = new Size(450, 30),
                Minimum = 20,
                Maximum = 160,
                Value = defTemp,
                TickFrequency = 10,
                SmallChange = 1,
                LargeChange = 5
            };

            var lblVal = MakeLabel(defTemp + ".0 ℃", 10f, Color.FromArgb(20, 80, 160),
                bold: true, pt: new Point(698, y + 5));
            lblVal.Size = new Size(90, 20);
            lblVal.TextAlign = ContentAlignment.MiddleLeft;

            var lblWarn = MakeLabel("", 8f, Color.FromArgb(160, 40, 40),
                pt: new Point(794, y + 7));
            lblWarn.AutoSize = true;

            var nud = new NumericUpDown
            {
                Location = new Point(890, y + 4),
                Size = new Size(75, 22),
                Minimum = 20,
                Maximum = 160,
                DecimalPlaces = 1,
                Increment = 0.5m,
                Value = defTemp,
                Font = new Font("Consolas", 9f)
            };

            Action<decimal> updateColor = v =>
            {
                bool isStop = v >= 125;
                bool isShed = v >= 108 && v < 125;
                lblVal.ForeColor = isStop
                    ? Color.FromArgb(180, 20, 20)
                    : isShed ? Color.FromArgb(160, 90, 0)
                    : Color.FromArgb(20, 80, 160);
                lblWarn.Text = isStop ? "▲ 超停机阈值"
                    : isShed ? "△ 超降载阈值" : "";
            };

            bool syncing = false;

            tb.ValueChanged += (s, e) =>
            {
                if (syncing) return; syncing = true;
                decimal v = tb.Value;
                lblVal.Text = v.ToString("F1") + " ℃";
                if (v >= nud.Minimum && v <= nud.Maximum) nud.Value = v;
                updateColor(v);
                TRDPSimulatorService.Instance.InjectValue(sigName, v);
                syncing = false;
            };

            nud.ValueChanged += (s, e) =>
            {
                if (syncing) return; syncing = true;
                int tbV = (int)nud.Value;
                if (tbV >= tb.Minimum && tbV <= tb.Maximum) tb.Value = tbV;
                lblVal.Text = nud.Value.ToString("F1") + " ℃";
                updateColor(nud.Value);
                TRDPSimulatorService.Instance.InjectValue(sigName, nud.Value);
                syncing = false;
            };

            parent.Controls.AddRange(new Control[] { lblName, tb, lblVal, lblWarn, nud });
            y += 38;
        }

        // ────────────────────────────────────────────────────────────────
        // 服务日志绑定
        // ────────────────────────────────────────────────────────────────

        private void BindServiceLog()
        {
            TRDPSimulatorService.Instance.OnLog += (msg, level) =>
            {
                if (IsDisposed || !IsHandleCreated) return;
                try { Invoke(new Action(() => AppendLog(msg, level))); }
                catch { }
            };
        }

        private void AppendLog(string msg, TRDPSimulatorService.LogLevel level)
        {
            if (_rtbLog == null || _rtbLog.IsDisposed) return;

            Color col = level == TRDPSimulatorService.LogLevel.OK
                ? Color.FromArgb(80, 200, 120)
                : level == TRDPSimulatorService.LogLevel.Fault
                    ? Color.FromArgb(255, 100, 100)
                    : level == TRDPSimulatorService.LogLevel.Warn
                        ? Color.FromArgb(255, 210, 80)
                        : Color.FromArgb(180, 180, 180);

            string line = string.Format("[{0}] {1}\n",
                DateTime.Now.ToString("HH:mm:ss"), msg);

            _rtbLog.SelectionStart = _rtbLog.TextLength;
            _rtbLog.SelectionLength = 0;
            _rtbLog.SelectionColor = col;
            _rtbLog.AppendText(line);
            _rtbLog.ScrollToCaret();

            // 超 500 行自动清头部
            if (_rtbLog.Lines.Length > 500)
            {
                _rtbLog.Select(0, _rtbLog.GetFirstCharIndexFromLine(100));
                _rtbLog.SelectedText = "";
            }
        }

        // ────────────────────────────────────────────────────────────────
        // 辅助：从 Var.TRDP.tags 获取当前信号列表
        // ────────────────────────────────────────────────────────────────

        private static List<FullTags> GetCurrentTags()
        {
            try
            {
                if (Var.TRDP == null || Var.TRDP.tags == null)
                    return new List<FullTags>();
                return Var.TRDP.tags.Where(t => t != null && !string.IsNullOrEmpty(t.DataLabel))
                                    .ToList();
            }
            catch
            {
                return new List<FullTags>();
            }
        }

        // ────────────────────────────────────────────────────────────────
        // 辅助：控件工厂方法
        // ────────────────────────────────────────────────────────────────

        private static bool IsAnalogType(string dataType)
        {
            return dataType == "U16" || dataType == "I16"
                || dataType == "U32" || dataType == "I32"
                || dataType == "F32" || dataType == "U8"
                || dataType == "I8";
        }

        private static int GetDefaultMax(string dataType)
        {
            switch (dataType)
            {
                case "U8": return 255;
                case "I8": return 127;
                case "U16": return 65535;
                case "I16": return 32767;
                case "U32": return 100000;
                case "I32": return 100000;
                default: return 10000;
            }
        }

        /// <summary>清空 TabPage 内所有子控件</summary>
        private static void ClearTabContent(TabPage tp)
        {
            foreach (Control c in tp.Controls)
            {
                c.Controls.Clear();
                c.Dispose();
            }
            tp.Controls.Clear();
        }

        private static Panel MakeScrollPanel()
        {
            return new Panel { Dock = DockStyle.Fill, AutoScroll = true };
        }

        private static Label MakeLabel(string text, float fontSize,
            Color color, bool bold = false, Point? pt = null)
        {
            var lbl = new Label
            {
                Text = text,
                Font = new Font("微软雅黑", fontSize,
                    bold ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = color,
                AutoSize = true
            };
            if (pt.HasValue) lbl.Location = pt.Value;
            return lbl;
        }

        private static void AddSectionHeader(Panel panel, ref int y, string title,
            Color? headerColor = null, float fontSize = 8f)
        {
            var lbl = new Label
            {
                Text = title,
                Font = new Font("微软雅黑", fontSize, FontStyle.Bold),
                ForeColor = headerColor ?? Color.FromArgb(30, 56, 100),
                Location = new Point(6, y),
                AutoSize = true
            };
            panel.Controls.Add(lbl);
            y += 22;
        }

        // ────────────────────────────────────────────────────────────────
        // 内部数据结构
        // ────────────────────────────────────────────────────────────────

        private class AnalogEntry
        {
            public TrackBar TrackBar { get; set; }
            public Label ValueLabel { get; set; }
            public NumericUpDown Nud { get; set; }
        }
    }
}
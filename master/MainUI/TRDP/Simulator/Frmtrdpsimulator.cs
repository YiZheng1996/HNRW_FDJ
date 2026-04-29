using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MainUI.Simulate;

namespace MainUI
{
    /// <summary>
    /// TRDP 数据模拟调试窗口
    ///
    /// 使用方式（在任意按钮的 Click 事件里）：
    ///     new frmTRDPSimulator().Show();
    ///
    /// 所有控件均由代码构建，不依赖 .Designer.cs。
    /// </summary>
    public class frmTRDPSimulator : Form
    {
        // ── UI 控件引用（需要在事件里访问）────────────────────────────────
        private Label _lblStatus;
        private Label _lblTick;
        private Button _btnStart;
        private Button _btnStop;
        private RichTextBox _rtbLog;
        private System.Windows.Forms.Timer _uiTimer;

        // TrackBar + Label 配对字典（信号名 → (trackBar, valueLabel)）
        private readonly Dictionary<string, AnalogEntry> _analogControls = new Dictionary<string, AnalogEntry>();

        // 数字量按钮字典（信号名 → Button）
        private readonly Dictionary<string, Button> _digitalButtons
            = new Dictionary<string, Button>();

        // 轴温 NumericUpDown 数组
        private readonly NumericUpDown[] _axisNud = new NumericUpDown[7];

        // 渐增控件
        private ComboBox _rampGearCbo;
        private NumericUpDown _rampFromNud, _rampToNud, _rampStepNud;

        private static readonly Color C_NORMAL = Color.FromArgb(220, 237, 220);
        private static readonly Color C_FAULT = Color.FromArgb(255, 200, 200);
        private static readonly Color C_WARN = Color.FromArgb(255, 235, 150);
        private static readonly Color C_TOOLBAR = Color.FromArgb(30, 56, 100);

        // ═══════════════════════════════════════════════════════════════════
        // 构造 & 初始化
        // ═══════════════════════════════════════════════════════════════════

        public frmTRDPSimulator()
        {
            Text = "TRDP 模拟控制台";
            Size = new Size(1060, 780);
            MinimumSize = new Size(900, 650);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("微软雅黑", 9f);
            BackColor = Color.FromArgb(245, 245, 248);

            BuildUI();
            BindService();

            FormClosed += (s, e) =>
            {
                _uiTimer?.Stop();
                TRDPSimulatorService.Instance.Stop();
            };
        }

        // ═══════════════════════════════════════════════════════════════════
        // 构建界面
        // ═══════════════════════════════════════════════════════════════════

        private void BuildUI()
        {
            // ── 顶部工具栏 ───────────────────────────────────────────────
            var pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = C_TOOLBAR,
                Padding = new Padding(10, 0, 10, 0)
            };

            var lblTitle = MakeLabel("TRDP 模拟控制台", 14, Color.White, bold: true);
            lblTitle.Location = new Point(12, 14);
            lblTitle.AutoSize = true;

            _lblStatus = MakeLabel("● 未启动", 9, Color.FromArgb(180, 200, 180));
            _lblStatus.Location = new Point(220, 18);
            _lblStatus.AutoSize = true;

            _btnStart = MakeBtn("▶ 启动模拟", Color.FromArgb(56, 140, 70), Color.White, btnStart_Click);
            _btnStop = MakeBtn("■ 停止", Color.FromArgb(160, 40, 40), Color.White, btnStop_Click);
            var btnClr = MakeBtn("✕ 清除故障", Color.FromArgb(200, 140, 20), Color.White,
                (s, e) => { TRDPSimulatorService.Instance.ClearAllFaults(); ResetAllDigitalButtons(); });

            _lblTick = MakeLabel("帧: 0", 9, Color.FromArgb(180, 200, 180));
            _lblTick.TextAlign = ContentAlignment.MiddleRight;
            _lblTick.Dock = DockStyle.Right;
            _lblTick.Width = 80;

            pnlTop.Controls.AddRange(new Control[]
                { lblTitle, _lblStatus, _btnStart, _btnStop, btnClr, _lblTick });
            _btnStart.Location = new Point(450, 10);
            _btnStop.Location = new Point(555, 10);
            btnClr.Location = new Point(640, 10);

            // ── 底部日志 ─────────────────────────────────────────────────
            var pnlLog = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 130,
                Padding = new Padding(6, 4, 6, 4)
            };
            var lblLogTitle = MakeLabel("▼ 注入日志", 8, Color.Gray);
            lblLogTitle.Dock = DockStyle.Top; lblLogTitle.Height = 16;

            _rtbLog = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Font = new Font("Consolas", 8.5f),
                BackColor = Color.FromArgb(30, 30, 35),
                ForeColor = Color.FromArgb(200, 200, 200),
                BorderStyle = BorderStyle.None,
                ScrollBars = RichTextBoxScrollBars.Vertical
            };
            var btnClrLog = new Button
            {
                Text = "清空",
                Dock = DockStyle.Right,
                Width = 50,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.Gray,
                Font = new Font("微软雅黑", 8f)
            };
            btnClrLog.Click += (s, e) => _rtbLog.Clear();
            pnlLog.Controls.AddRange(new Control[] { _rtbLog, lblLogTitle, btnClrLog });

            // ── TabControl ───────────────────────────────────────────────
            var tab = new TabControl { Dock = DockStyle.Fill, Font = Font };

            tab.TabPages.Add(BuildTabA());
            tab.TabPages.Add(BuildTabB());
            tab.TabPages.Add(BuildTabC());
            tab.TabPages.Add(BuildTabD());
            tab.TabPages.Add(BuildTabPreset());

            Controls.AddRange(new Control[] { tab, pnlLog, pnlTop });

            // ── UI 刷新定时器 ─────────────────────────────────────────────
            _uiTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _uiTimer.Tick += (s, e) =>
            {
                var svc = TRDPSimulatorService.Instance;
                _lblTick.Text = $"帧: {svc.TickCount}";
                _lblStatus.Text = svc.IsRunning
                    ? $"● 运行中  生命={svc.LifeCounter}"
                    : "● 未启动";
            };
            _uiTimer.Start();
        }

        // ═══════════════════════════════════════════════════════════════════
        // Tab A — 模拟量（TrackBar + 精确 NumericUpDown）
        // ═══════════════════════════════════════════════════════════════════

        private TabPage BuildTabA()
        {
            var tp = new TabPage("A类 模拟量（浮点）");
            var panel = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            tp.Controls.Add(panel);

            int y = 8;

            void AddGroup(string title)
            {
                var lbl = MakeLabel(title, 8, Color.FromArgb(30, 56, 100), bold: true);
                lbl.Location = new Point(6, y);
                lbl.AutoSize = true;
                panel.Controls.Add(lbl);
                y += 20;
                var sep = new Panel
                {
                    Location = new Point(6, y - 3),
                    Size = new Size(980, 1),
                    BackColor = Color.FromArgb(30, 56, 100)
                };
                panel.Controls.Add(sep);
            }

            // 转速组
            AddGroup("─── 转速信号");
            AddAnalogRow(panel, ref y, "柴油机转速", 0, "0.1", "r/min", 0, 1200, 1000, 1);
            AddAnalogRow(panel, ref y, "转速传感器1#", 2, "0.1", "r/min", 0, 1300, 1000, 1);
            AddAnalogRow(panel, ref y, "转速传感器2#", 4, "0.1", "r/min", 0, 1300, 1000, 1);
            AddAnalogRow(panel, ref y, "相位传感器", 6, "0.1", "r/min", 0, 1300, 1000, 1);
            AddAnalogRow(panel, ref y, "转速设定", 8, "1", "r/min", 0, 1100, 1000, 1);
            AddAnalogRow(panel, ref y, "电喷转速1#", 2, "0.1", "r/min", 0, 1300, 1000, 1);
            AddAnalogRow(panel, ref y, "电喷转速2#", 4, "0.1", "r/min", 0, 1300, 1000, 1);

            // 燃油 & 喷射
            AddGroup("─── 燃油 & 喷射");
            AddAnalogRow(panel, ref y, "燃油量", 10, "0.1", "mg/循环", 0, 120, 47, 1);
            AddAnalogRow(panel, ref y, "提前角", 22, "0.01", "°CA", 0, 25, 10, 100);
            AddAnalogRow(panel, ref y, "持续期", 20, "0.01", "ms", 0, 6, 2, 100);

            // 电源
            AddGroup("─── 电源");
            AddAnalogRow(panel, ref y, "电源A", 12, "0.01", "V", 18, 36, 27, 10);
            AddAnalogRow(panel, ref y, "电源B", 14, "0.01", "V", 18, 36, 27, 10);

            // 放大器
            AddGroup("─── 电源放大器");
            AddAnalogRow(panel, ref y, "电源放大器A滤值", 26, "0.01", "A", 0, 15, 6, 100);
            AddAnalogRow(panel, ref y, "电源放大器B滤值", 28, "0.01", "A", 0, 15, 6, 100);
            AddAnalogRow(panel, ref y, "电源放大器C滤值", 30, "0.01", "A", 0, 15, 6, 100);
            AddAnalogRow(panel, ref y, "电源放大器A实际值", 32, "0.01", "A", 0, 15, 6, 100);
            AddAnalogRow(panel, ref y, "电源放大器B实际值", 34, "0.01", "A", 0, 15, 6, 100);
            AddAnalogRow(panel, ref y, "电源放大器C实际值", 36, "0.01", "A", 0, 15, 6, 100);

            // 温度
            AddGroup("─── 温度");
            AddAnalogRow(panel, ref y, "前压气机出口空气温度", 78, "0.1", "℃", 20, 120, 80, 1);
            AddAnalogRow(panel, ref y, "后压气机出口空气温度", 80, "0.1", "℃", 20, 120, 80, 1);

            panel.Height = y + 20;
            return tp;
        }

        /// <summary>
        /// 添加一行模拟量控件：
        ///   [信号名+offset]  [TrackBar]  [NumericUpDown]  [单位]
        ///   TrackBar 和 NUD 双向同步，变化时立即调用 InjectValue
        /// </summary>
        private void AddAnalogRow(Panel parent, ref int y,
            string sigName, int byteOffset, string fmt, string unit,
            int minRaw, int maxRaw, int defRaw, decimal scale)
        {
            // 标签
            var lblName = MakeLabel(
                $"{sigName}  [offset:{byteOffset}]", 8.5f, Color.FromArgb(60, 60, 80));
            lblName.Location = new Point(8, y + 4);
            lblName.Size = new Size(210, 18);

            // TrackBar（整数范围 = minRaw*scale ~ maxRaw*scale，不适合小步长用 NUD）
            int tbMin = (int)(minRaw * scale);
            int tbMax = (int)(maxRaw * scale);
            int tbDef = (int)(defRaw * scale);
            var tb = new TrackBar
            {
                Location = new Point(220, y),
                Size = new Size(380, 30),
                Minimum = tbMin,
                Maximum = tbMax,
                Value = Math.Min(Math.Max(tbDef, tbMin), tbMax),
                TickFrequency = Math.Max(1, (tbMax - tbMin) / 20),
                SmallChange = 1,
                LargeChange = Math.Max(1, (tbMax - tbMin) / 10)
            };

            // 值显示标签
            var lblVal = MakeLabel(
                FormatVal(tbDef, scale, fmt), 9, Color.FromArgb(20, 80, 160), bold: true);
            lblVal.Location = new Point(608, y + 5);
            lblVal.Size = new Size(70, 18);
            lblVal.TextAlign = ContentAlignment.MiddleRight;

            // 精确输入 NUD
            decimal nudMin = (decimal)minRaw;
            decimal nudMax = (decimal)maxRaw;
            decimal nudDec = scale == 1 ? 0 : (scale == 10 ? 1 : scale == 100 ? 2 : 2);
            decimal nudStp = scale == 1 ? 1m : (scale == 10 ? 0.1m : 0.01m);
            var nud = new NumericUpDown
            {
                Location = new Point(682, y + 4),
                Size = new Size(78, 22),
                Minimum = nudMin,
                Maximum = nudMax,
                DecimalPlaces = (int)nudDec,
                Increment = nudStp,
                Value = Math.Min(Math.Max((decimal)defRaw, nudMin), nudMax),
                Font = new Font("Consolas", 9f)
            };

            // 单位
            var lblUnit = MakeLabel(unit, 8, Color.Gray);
            lblUnit.Location = new Point(764, y + 6);
            lblUnit.AutoSize = true;

            // 同步事件
            bool syncing = false;
            tb.ValueChanged += (s, e) =>
            {
                if (syncing) return;
                syncing = true;
                decimal realVal = tb.Value / scale;
                lblVal.Text = FormatVal(tb.Value, scale, fmt);
                nud.Value = Math.Min(Math.Max(realVal, nudMin), nudMax);
                TRDPSimulatorService.Instance.InjectValue(sigName, realVal);
                syncing = false;
            };
            nud.ValueChanged += (s, e) =>
            {
                if (syncing) return;
                syncing = true;
                int tbVal = (int)(nud.Value * scale);
                lblVal.Text = nud.Value.ToString($"F{(int)nudDec}");
                if (tbVal >= tb.Minimum && tbVal <= tb.Maximum) tb.Value = tbVal;
                TRDPSimulatorService.Instance.InjectValue(sigName, nud.Value);
                syncing = false;
            };

            parent.Controls.AddRange(new Control[]
                { lblName, tb, lblVal, nud, lblUnit });
            _analogControls[sigName] = new AnalogEntry { Tb = tb, Lbl = lblVal, Scale = scale };
            y += 32;
        }

        private static string FormatVal(int rawVal, decimal scale, string fmt)
        {
            decimal v = rawVal / scale;
            int dec = fmt == "0.01" ? 2 : fmt == "0.1" ? 1 : 0;
            return v.ToString($"F{dec}");
        }

        // ═══════════════════════════════════════════════════════════════════
        // Tab B — 数字量（故障位切换按钮）
        // ═══════════════════════════════════════════════════════════════════

        private TabPage BuildTabB()
        {
            var tp = new TabPage("B类 数字量（故障位）");
            var panel = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            tp.Controls.Add(panel);

            int y = 8;
            void AddGroup(string title)
            {
                var l = MakeLabel(title, 8, Color.FromArgb(30, 56, 100), bold: true);
                l.Location = new Point(6, y); l.AutoSize = true; panel.Controls.Add(l);
                y += 20;
            }

            AddGroup("ECM 报警位");
            AddDigitalRow(panel, ref y, "紧急报警", 16, "—", "1=触发停机", false);
            AddDigitalRow(panel, ref y, "公共报警", 18, "—", "1=触发降载", false);
            AddDigitalRow(panel, ref y, "同步状态", 24, "—", "1=已同步(正常)", true);

            AddGroup("电磁阀故障位 (B01-B12)  byteOffset: 38-62");
            for (int i = 1; i <= 12; i++)
            {
                string row = i <= 6 ? $"A{i}缸" : $"B{i - 6}缸";
                AddDigitalRow(panel, ref y, $"电磁阀故障{i}#",
                    36 + i * 2, $"byteOffset:{36 + i * 2}", $"0=正常 1=故障 [{row}]", false);
            }

            AddGroup("传感器故障位 (B13-B19)");
            AddDigitalRow(panel, ref y, "供电电源故障", 64, "byteOffset:64", "0=正常 1=故障", false);
            AddDigitalRow(panel, ref y, "转速传感器故障1#", 66, "byteOffset:66", "0=正常 1=故障", false);
            AddDigitalRow(panel, ref y, "转速传感器故障2#", 68, "byteOffset:68", "0=正常 1=故障", false);
            AddDigitalRow(panel, ref y, "相位传感器故障", 70, "byteOffset:70", "0=正常 1=故障", false);
            AddDigitalRow(panel, ref y, "超速故障", 72, "byteOffset:72", "0=正常 1=停机", false);
            AddDigitalRow(panel, ref y, "同步输入故障", 74, "byteOffset:74", "0=正常 1=故障", false);
            AddDigitalRow(panel, ref y, "硬件故障", 76, "byteOffset:76", "0=正常 1=故障", false);

            panel.Height = y + 20;
            return tp;
        }

        private void AddDigitalRow(Panel parent, ref int y,
            string sigName, int byteOffset, string tag, string desc, bool initTrue)
        {
            bool isActive = initTrue; // false=0正常  true=1激活（同步状态特殊）

            // 信号名
            var lblName = MakeLabel(
                $"{sigName}  [{tag}]", 8.5f, Color.FromArgb(60, 60, 80));
            lblName.Location = new Point(8, y + 6);
            lblName.Size = new Size(240, 18);

            // 描述
            var lblDesc = MakeLabel(desc, 8, Color.Gray);
            lblDesc.Location = new Point(680, y + 7);
            lblDesc.AutoSize = true;

            // 切换按钮
            bool isSyncState = (sigName == "同步状态");
            var btn = new Button
            {
                Location = new Point(250, y + 2),
                Size = new Size(400, 28),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("微软雅黑", 9f),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
            btn.FlatAppearance.BorderSize = 1;

            void Refresh()
            {
                if (isSyncState)
                {
                    // 同步状态：1=绿(正常)，0=红(异常)
                    btn.BackColor = isActive ? C_NORMAL : C_FAULT;
                    btn.Text = isActive ? "1  已同步（正常）" : "0  未同步（异常）";
                    btn.ForeColor = isActive
                        ? Color.FromArgb(30, 80, 30) : Color.FromArgb(140, 20, 20);
                }
                else
                {
                    // 普通故障位：0=绿(正常)，1=红(故障)
                    btn.BackColor = isActive ? C_FAULT : C_NORMAL;
                    btn.Text = isActive ? "1  故障" : "0  正常";
                    btn.ForeColor = isActive
                        ? Color.FromArgb(140, 20, 20) : Color.FromArgb(30, 80, 30);
                }
            }

            isActive = initTrue;
            Refresh();

            btn.Click += (s, e) =>
            {
                isActive = !isActive;
                Refresh();
                decimal injectVal = isSyncState
                    ? (isActive ? 1m : 0m)
                    : (isActive ? 1m : 0m);
                TRDPSimulatorService.Instance.InjectValue(sigName, injectVal);
            };

            parent.Controls.AddRange(new Control[] { lblName, btn, lblDesc });
            _digitalButtons[sigName] = btn;
            y += 34;
        }

        private void ResetAllDigitalButtons()
        {
            // 让按钮视觉回到初始状态（让用户看到已清零）
            foreach (var kv in _digitalButtons)
            {
                bool isSyncState = (kv.Key == "同步状态");
                kv.Value.BackColor = isSyncState ? C_NORMAL : C_NORMAL;
                kv.Value.Text = isSyncState ? "1  已同步（正常）" : "0  正常";
                kv.Value.ForeColor = Color.FromArgb(30, 80, 30);
            }
        }

        // ═══════════════════════════════════════════════════════════════════
        // Tab C — 轴温
        // ═══════════════════════════════════════════════════════════════════

        private TabPage BuildTabC()
        {
            var tp = new TabPage("C类 轴温（一~七档）");
            var panel = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            tp.Controls.Add(panel);

            int[] offsets = { 170, 172, 174, 176, 178, 180, 182 };
            int[] defTemps = { 70, 72, 74, 76, 78, 80, 82 };
            int y = 8;

            var lblHdr = MakeLabel(
                "轴温来源：变速箱轴温监控装置（从站1-6），经 TRDP 转发。" +
                "降载阈值 / 停机阈值由 FaultConfig.F28V1/F28V2 配置。",
                8, Color.Gray);
            lblHdr.Location = new Point(8, y); lblHdr.AutoSize = true;
            panel.Controls.Add(lblHdr);
            y += 24;

            for (int i = 0; i < 7; i++)
            {
                int idx = i;
                string name = TRDPSimulatorService.GearName(i);
                int offset = offsets[i];
                int defTemp = defTemps[i];

                var lblName = MakeLabel($"{name}  [byteOffset:{offset}, 格式:0.1]",
                    9, Color.FromArgb(30, 56, 100), bold: true);
                lblName.Location = new Point(8, y + 8);
                lblName.AutoSize = true;

                var tb = new TrackBar
                {
                    Location = new Point(230, y),
                    Size = new Size(450, 30),
                    Minimum = 20,
                    Maximum = 160,
                    Value = defTemp,
                    TickFrequency = 10,
                    SmallChange = 1,
                    LargeChange = 5
                };

                var lblVal = MakeLabel(defTemp + ".0 ℃", 10, Color.FromArgb(20, 80, 160), bold: true);
                lblVal.Location = new Point(688, y + 5);
                lblVal.Size = new Size(90, 20);
                lblVal.TextAlign = ContentAlignment.MiddleLeft;

                var nud = new NumericUpDown
                {
                    Location = new Point(784, y + 4),
                    Size = new Size(75, 22),
                    Minimum = 20,
                    Maximum = 160,
                    DecimalPlaces = 1,
                    Increment = 0.5m,
                    Value = defTemp,
                    Font = new Font("Consolas", 9f)
                };

                var lblWarn = MakeLabel("", 8, Color.FromArgb(160, 40, 40));
                lblWarn.Location = new Point(864, y + 7);
                lblWarn.AutoSize = true;

                void UpdateColor(decimal v)
                {
                    bool isStop = v >= 125;
                    bool isShed = v >= 108 && v < 125;
                    lblVal.ForeColor = isStop ? Color.FromArgb(180, 20, 20)
                                     : isShed ? Color.FromArgb(160, 90, 0)
                                     : Color.FromArgb(20, 80, 160);
                    lblWarn.Text = isStop ? "▲ 超停机阈值"
                                 : isShed ? "△ 超降载阈值" : "";
                }

                _axisNud[i] = nud;

                bool syncing = false;
                tb.ValueChanged += (s, e) =>
                {
                    if (syncing) return; syncing = true;
                    decimal v = tb.Value;
                    lblVal.Text = v.ToString("F1") + " ℃";
                    nud.Value = v;
                    UpdateColor(v);
                    TRDPSimulatorService.Instance.InjectValue(name, v / 10m * 10m);
                    // 实际工程值 = tb.Value（整数℃），dataFormat=0.1意味着原始值×0.1=工程值
                    // 所以注入工程值 = tb.Value
                    TRDPSimulatorService.Instance.InjectValue(name, v);
                    syncing = false;
                };
                nud.ValueChanged += (s, e) =>
                {
                    if (syncing) return; syncing = true;
                    int tbV = (int)nud.Value;
                    if (tbV >= 20 && tbV <= 160) tb.Value = tbV;
                    lblVal.Text = nud.Value.ToString("F1") + " ℃";
                    UpdateColor(nud.Value);
                    TRDPSimulatorService.Instance.InjectValue(name, nud.Value);
                    syncing = false;
                };

                panel.Controls.AddRange(new Control[]
                    { lblName, tb, lblVal, nud, lblWarn });
                y += 38;
            }

            // 渐增控制行
            y += 10;
            var lblRamp = MakeLabel("── 渐进注入（模拟轴温缓慢过热）", 8, Color.FromArgb(30, 56, 100), bold: true);
            lblRamp.Location = new Point(8, y); lblRamp.AutoSize = true;
            panel.Controls.Add(lblRamp);
            y += 22;

            var lblG = MakeLabel("档位：", 8, Color.Gray);
            lblG.Location = new Point(8, y + 5); lblG.AutoSize = true;
            _rampGearCbo = new ComboBox
            {
                Location = new Point(50, y + 2),
                Width = 80,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            for (int i = 0; i < 7; i++)
                _rampGearCbo.Items.Add(TRDPSimulatorService.GearName(i));
            _rampGearCbo.SelectedIndex = 2;

            var lblFrom = MakeLabel("起始：", 8, Color.Gray); lblFrom.Location = new Point(140, y + 5); lblFrom.AutoSize = true;
            _rampFromNud = MakeSmallNud(180, y + 2, 20, 160, 70, 1);
            var lblTo = MakeLabel("目标：", 8, Color.Gray); lblTo.Location = new Point(280, y + 5); lblTo.AutoSize = true;
            _rampToNud = MakeSmallNud(320, y + 2, 20, 180, 125, 1);
            var lblStep = MakeLabel("步进(℃/s)：", 8, Color.Gray); lblStep.Location = new Point(420, y + 5); lblStep.AutoSize = true;
            _rampStepNud = MakeSmallNud(510, y + 2, 1, 30, 2, 1);

            var btnRampStart = MakeBtn("▶ 开始渐增", Color.FromArgb(160, 90, 0), Color.White,
                (s, e) => TRDPSimulatorService.Instance.StartRamp(
                    _rampGearCbo.SelectedIndex,
                    (double)_rampFromNud.Value, (double)_rampToNud.Value,
                    (double)_rampStepNud.Value));
            btnRampStart.Location = new Point(600, y);
            var btnRampStop = MakeBtn("■ 停止", Color.FromArgb(100, 100, 100), Color.White,
                (s, e) => TRDPSimulatorService.Instance.StopRamp());
            btnRampStop.Location = new Point(700, y);

            panel.Controls.AddRange(new Control[]
            {
                lblG, _rampGearCbo,
                lblFrom, _rampFromNud, lblTo, _rampToNud, lblStep, _rampStepNud,
                btnRampStart, btnRampStop
            });

            return tp;
        }

        private NumericUpDown MakeSmallNud(int x, int y, decimal min, decimal max, decimal def, int dec)
            => new NumericUpDown
            {
                Location = new Point(x, y),
                Size = new Size(70, 22),
                Minimum = min,
                Maximum = max,
                Value = def,
                DecimalPlaces = dec,
                Font = new Font("Consolas", 9f)
            };

        // ═══════════════════════════════════════════════════════════════════
        // Tab D — 通讯状态
        // ═══════════════════════════════════════════════════════════════════

        private TabPage BuildTabD()
        {
            var tp = new TabPage("D类 通讯状态");
            var panel = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            tp.Controls.Add(panel);

            int y = 10;

            // 生命信号
            var lbl1 = MakeLabel("设备生命信号 (D01)  [byteOffset:196, U16, 0-255, 周期递增]",
                9, Color.FromArgb(30, 56, 100), bold: true);
            lbl1.Location = new Point(8, y); lbl1.AutoSize = true;
            panel.Controls.Add(lbl1); y += 22;

            var lblLifeVal = MakeLabel("生命信号值：0", 9, Color.FromArgb(20, 80, 160), bold: true);
            lblLifeVal.Location = new Point(8, y); lblLifeVal.AutoSize = true;
            panel.Controls.Add(lblLifeVal);

            var btnLifeAuto = MakeBtn("● 自动递增（已启用）",
                Color.FromArgb(40, 120, 60), Color.White, null);
            btnLifeAuto.Location = new Point(200, y - 3);
            bool lifeAuto = true;
            btnLifeAuto.Click += (s, e) =>
            {
                lifeAuto = !lifeAuto;
                TRDPSimulatorService.Instance.SetLifeAuto(lifeAuto);
                btnLifeAuto.BackColor = lifeAuto
                    ? Color.FromArgb(40, 120, 60) : Color.FromArgb(160, 80, 20);
                btnLifeAuto.Text = lifeAuto
                    ? "● 自动递增（已启用）" : "■ 已冻结（模拟通讯断开）";
            };
            panel.Controls.Add(btnLifeAuto);

            // 用一个局部 Timer 每秒刷新生命信号显示值
            var lifeRefreshTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            lifeRefreshTimer.Tick += (s, e) =>
            {
                if (!IsDisposed)
                    lblLifeVal.Text = $"生命信号值：{TRDPSimulatorService.Instance.LifeCounter}";
            };
            lifeRefreshTimer.Start();
            FormClosed += (s, e) => lifeRefreshTimer.Stop();

            y += 40;

            // 通讯故障位
            var lbl2 = MakeLabel("── 网口与从站通讯状态 (D02-D06)", 9, Color.FromArgb(30, 56, 100), bold: true);
            lbl2.Location = new Point(8, y); lbl2.AutoSize = true;
            panel.Controls.Add(lbl2); y += 22;

            AddDigitalRow(panel, ref y, "网口0故障", 198, "byteOffset:198, B1",
                "0=正常 1=以太网口故障", false);
            AddDigitalRow(panel, ref y, "从站1串口故障", 199, "byteOffset:199, bit0",
                "电喷控制从站，0=正常 1=故障", false);
            AddDigitalRow(panel, ref y, "从站2串口故障", 199, "byteOffset:199, bit1",
                "0=正常 1=故障", false);
            AddDigitalRow(panel, ref y, "从站3串口故障", 199, "byteOffset:199, bit2",
                "0=正常 1=故障", false);
            AddDigitalRow(panel, ref y, "从站6串口故障", 200, "byteOffset:200, bit5",
                "轴温监控主站，断线触发 F29 报警", false);

            panel.Height = y + 20;
            return tp;
        }

        // ═══════════════════════════════════════════════════════════════════
        // Tab 故障预设
        // ═══════════════════════════════════════════════════════════════════

        private TabPage BuildTabPreset()
        {
            var tp = new TabPage("故障预设");
            var panel = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            tp.Controls.Add(panel);

            int y = 10;
            var lbl = MakeLabel("一键故障场景注入 — 点击后立即生效，ECMFaultDetectionService 下一轮轮询（≤1s）触发对应保护逻辑",
                8, Color.Gray);
            lbl.Location = new Point(8, y); lbl.AutoSize = true;
            panel.Controls.Add(lbl); y += 22;

            var presetKeys = new string[]
{
    "shed_axis", "stop_axis", "overspeed", "inj_overspeed",
    "slave6_comm", "solenoid", "power_low", "normal"
};
            var presetTitles = new string[]
            {
    "① 轴温超限 → 降载",
    "② 轴温超限 → 停机",
    "③ ECM 超速故障 → 停机",
    "④ 电喷转速超限 → 停机",
    "⑤ 从站6通讯断 → 报警",
    "⑥ 电磁阀故障 → 公共报警",
    "⑦ 电源电压低 → 故障",
    "⑧ 恢复全部正常"
            };
            var presetDescs = new string[]
            {
    "三档轴温 108℃ > F28V1\n公共报警=1  →  WarnTypeEnum.Shedding",
    "三档轴温 125℃ > F28V2\n紧急报警=1  →  WarnTypeEnum.Stop",
    "超速故障位=1 + 传感器1150 r/min\n→  F17/F30  WarnTypeEnum.Stop",
    "电喷转速1#=1150 r/min\n→  F30/F31  WarnTypeEnum.Stop",
    "从站6串口故障=1\n→  F29 轴温监控装置通讯故障",
    "电磁阀5#+6# 故障位=1\n公共报警=1  →  F05/F06",
    "电源A/B 降至21V\n供电电源故障=1 + 公共报警=1",
    "清除所有故障位\n恢复额定1000 r/min，轴温正常范围"
            };
            var presetColors = new Color[]
            {
    C_WARN, C_FAULT, C_FAULT, C_FAULT,
    C_WARN, C_WARN,  C_FAULT, C_NORMAL
            };


            int col = 0, rowY = y;
for (int i = 0; i < presetKeys.Length; i++)
{
    string capturedKey = presetKeys[i];   // 闭包捕获，必须用局部变量
    var btn = new Button
    {
        Text = presetTitles[i] + "\n\n" + presetDescs[i],
        Location = new Point(8 + col * 250, rowY),
        Size = new Size(238, 100),
        FlatStyle = FlatStyle.Flat,
        BackColor = presetColors[i],
        TextAlign = ContentAlignment.TopLeft,
        Font = new Font("微软雅黑", 8.5f),
        Cursor = Cursors.Hand,
        Tag = capturedKey,
        Padding = new Padding(8)
    };
    btn.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 180);
    btn.FlatAppearance.BorderSize = 1;
    btn.Click += (s, e) => TRDPSimulatorService.Instance.RunPreset(capturedKey);
    panel.Controls.Add(btn);

    col++;
    if (col >= 4) { col = 0; rowY += 108; }
}

            return tp;
        }

        // ═══════════════════════════════════════════════════════════════════
        // 服务绑定（日志事件）
        // ═══════════════════════════════════════════════════════════════════

        private void BindService()
        {
            TRDPSimulatorService.Instance.OnLog += (msg, level) =>
            {
                if (IsDisposed || !IsHandleCreated) return;
                try
                {
                    Invoke(new Action(() => AppendLog(msg, level)));
                }
                catch { }
            };
        }

        private void AppendLog(string msg, TRDPSimulatorService.LogLevel level)
        {
            if (_rtbLog.IsDisposed) return;
            var now = DateTime.Now.ToString("HH:mm:ss");
            Color col = level == TRDPSimulatorService.LogLevel.OK ? Color.FromArgb(80, 200, 120)
                      : level == TRDPSimulatorService.LogLevel.Fault ? Color.FromArgb(255, 100, 100)
                      : level == TRDPSimulatorService.LogLevel.Warn ? Color.FromArgb(255, 200, 80)
                      : Color.FromArgb(180, 180, 180);
            _rtbLog.SelectionStart = _rtbLog.TextLength;
            _rtbLog.SelectionLength = 0;
            _rtbLog.SelectionColor = col;
            _rtbLog.AppendText($"[{now}] {msg}{Environment.NewLine}");
            _rtbLog.SelectionColor = _rtbLog.ForeColor;
            _rtbLog.ScrollToCaret();

            // 超过 300 行清空一半
            if (_rtbLog.Lines.Length > 300)
                _rtbLog.Clear();
        }

        // ═══════════════════════════════════════════════════════════════════
        // 按钮事件
        // ═══════════════════════════════════════════════════════════════════

        private void btnStart_Click(object sender, EventArgs e)
        {
            TRDPSimulatorService.Instance.Start();
            _btnStart.BackColor = Color.FromArgb(80, 160, 80);
            _btnStop.BackColor = Color.FromArgb(160, 40, 40);
            _lblStatus.ForeColor = Color.FromArgb(150, 230, 150);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            TRDPSimulatorService.Instance.Stop();
            _btnStart.BackColor = Color.FromArgb(56, 140, 70);
            _lblStatus.Text = "● 已停止";
            _lblStatus.ForeColor = Color.FromArgb(200, 150, 150);
        }

        // ═══════════════════════════════════════════════════════════════════
        // 辅助工厂方法
        // ═══════════════════════════════════════════════════════════════════

        private static Label MakeLabel(string text, float size, Color fore, bool bold = false)
            => new Label
            {
                Text = text,
                Font = new Font("微软雅黑", size, bold ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = fore,
                AutoSize = true
            };

        private static Button MakeBtn(string text, Color bg, Color fg,
            EventHandler onClick)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(100, 30),
                BackColor = bg,
                ForeColor = fg,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("微软雅黑", 8.5f),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(255, 255, 255, 50);
            btn.FlatAppearance.BorderSize = 1;
            if (onClick != null) btn.Click += onClick;
            return btn;
        }
    }

    public class AnalogEntry
    {
        public TrackBar Tb;
        public Label Lbl;
        public decimal Scale;
    }
}
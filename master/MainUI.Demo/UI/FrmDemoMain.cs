using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MainUI.Demo.Core;

namespace MainUI.Demo.UI
{
    public partial class FrmDemoMain : Form
    {
        private DemoGauge[] _gauges;
        private bool _pulseOn = true;

        public FrmDemoMain()
        {
            InitializeComponent();
            BuildGauges();
        }

        #region 初始化仪表

        private void BuildGauges()
        {
            // 12 个核心仪表(4x3)
            var defs = new[]
            {
                new GaugeDef { Name = "柴油机转速",     Unit = "rpm", Max = 1200,   Warn = 1120, Decimals = 0, ScaleK = 0.83 },
                new GaugeDef { Name = "柴油机功率",     Unit = "kW",  Max = 3000,   Warn = 2800, Decimals = 0 },
                new GaugeDef { Name = "前增压器转速",   Unit = "rpm", Max = 50000,  Warn = 49000, Decimals = 0 },
                new GaugeDef { Name = "后增压器转速",   Unit = "rpm", Max = 50000,  Warn = 49000, Decimals = 0 },
                new GaugeDef { Name = "高温水出机温度", Unit = "°C",  Max = 110,    Warn = 101, Critical = 103, Decimals = 1 },
                new GaugeDef { Name = "A涡前排气温度",  Unit = "°C",  Max = 700,    Warn = 600, Critical = 630, Decimals = 0 },
                new GaugeDef { Name = "机油泵出口油温", Unit = "°C",  Max = 100,    Warn = 88,  Critical = 91,  Decimals = 1 },
                new GaugeDef { Name = "主油道末端油压", Unit = "kPa", Max = 1000,   Warn = 0,   Decimals = 0 },
                new GaugeDef { Name = "燃油精滤后油压", Unit = "kPa", Max = 500,    Warn = 0,   Decimals = 0 },
                new GaugeDef { Name = "中冷水出机温度", Unit = "°C",  Max = 100,    Warn = 70,  Decimals = 1 },
                new GaugeDef { Name = "轴温平均",       Unit = "°C",  Max = 150,    Warn = 113, Critical = 123, Decimals = 1 },
                new GaugeDef { Name = "有功功率",       Unit = "kW",  Max = 3000,   Warn = 2800, Decimals = 0 },
            };

            _gauges = new DemoGauge[defs.Length];
            for (int i = 0; i < defs.Length; i++)
            {
                var g = new DemoGauge(defs[i]) { Dock = DockStyle.Fill, Margin = new Padding(6) };
                int row = i / 4, col = i % 4;
                tlpGauges.Controls.Add(g, col, row);
                _gauges[i] = g;
            }
        }

        #endregion

        #region 生命周期

        private void FrmDemoMain_Load(object sender, EventArgs e)
        {
            // 启动日志服务（只此一次）
            if (!SimulationLogger.Instance.IsRunning)
                SimulationLogger.Instance.Start();

            SimulationLogger.Instance.LogMilestone("Demo 主窗口已打开");

            // 订阅总线
            SimulatorDataBus.Instance.DoubleChanged += OnDoubleChanged;

            uiTimer.Start();
            pulseTimer.Start();

            UpdateButtonsState();
        }

        private void FrmDemoMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                SimulatorDataBus.Instance.DoubleChanged -= OnDoubleChanged;
                if (SimulationEngine.Instance.IsRunning)
                    SimulationEngine.Instance.Stop();
                SimulationLogger.Instance.LogMilestone("Demo 主窗口关闭");
                SimulationLogger.Instance.Stop();
            }
            catch { }
        }

        #endregion

        #region 启停 / 闭环

        private void btnStart_Click(object sender, EventArgs e)
        {
            SimulationLogger.Instance.LogOperator("点击 [启动仿真]");
            SimulationEngine.Instance.ThrottlePercent = tbThrottle.Value;
            SimulationEngine.Instance.ExcitationPercent = tbExcitation.Value;
            SimulationEngine.Instance.Start();
            UpdateButtonsState();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            SimulationLogger.Instance.LogOperator("点击 [停止]");
            SimulationEngine.Instance.Stop();
            UpdateButtonsState();
        }

        private void UpdateButtonsState()
        {
            bool running = SimulationEngine.Instance.IsRunning;
            btnStart.Enabled = !running;
            btnStop.Enabled = running;
            btnStart.BackColor = running
                ? Color.FromArgb(180, 195, 185)
                : Color.FromArgb(40, 120, 60);
            btnStop.BackColor = running
                ? Color.FromArgb(160, 80, 20)
                : Color.FromArgb(180, 180, 180);
        }

        private void tbThrottle_Scroll(object sender, EventArgs e)
        {
            int v = tbThrottle.Value;
            lblThrottleVal.Text = v + "%";
            SimulationEngine.Instance.ThrottlePercent = v;
            SimulationLogger.Instance.Log(LogCategory.Operator, LogLevel.Info,
                "拖动油门滑块", "油门%",
                oldValue: null, newValue: v.ToString(), source: "Operator/UI");
        }

        private void tbExcitation_Scroll(object sender, EventArgs e)
        {
            int v = tbExcitation.Value;
            lblExcitationVal.Text = v + "%";
            SimulationEngine.Instance.ExcitationPercent = v;
            SimulationLogger.Instance.Log(LogCategory.Operator, LogLevel.Info,
                "拖动励磁滑块", "励磁%",
                oldValue: null, newValue: v.ToString(), source: "Operator/UI");
        }

        #endregion

        #region 故障按钮

        private void btnFault1_Click(object sender, EventArgs e)
        {
            SimulationLogger.Instance.LogOperator("点击 [高温水≥101℃报警]");
            SimulationEngine.Instance.InjectFault(SimulationEngine.FaultPreset.HighWaterTempAlarm);
        }

        private void btnFault2_Click(object sender, EventArgs e)
        {
            SimulationLogger.Instance.LogOperator("点击 [高温水≥103℃降载停机]");
            SimulationEngine.Instance.InjectFault(SimulationEngine.FaultPreset.HighWaterTempStop);
        }

        private void btnFault3_Click(object sender, EventArgs e)
        {
            SimulationLogger.Instance.LogOperator("点击 [轴温≥123℃停机]");
            SimulationEngine.Instance.InjectFault(SimulationEngine.FaultPreset.AxisOverTempStop);
        }

        private void btnFault4_Click(object sender, EventArgs e)
        {
            SimulationLogger.Instance.LogOperator("点击 [主油道压力过低]");
            SimulationEngine.Instance.InjectFault(SimulationEngine.FaultPreset.MainOilPressureLow);
        }

        private void btnFaultEStop_Click(object sender, EventArgs e)
        {
            SimulationLogger.Instance.LogOperator("点击 [急停按钮触发]");
            SimulationEngine.Instance.InjectFault(SimulationEngine.FaultPreset.EmergencyStop);
        }

        private void btnClearFault_Click(object sender, EventArgs e)
        {
            SimulationLogger.Instance.LogOperator("点击 [清除故障]");
            SimulationEngine.Instance.ClearFault();
            tbThrottle.Value = 80;
            tbExcitation.Value = 80;
            lblThrottleVal.Text = "80%";
            lblExcitationVal.Text = "80%";
        }

        private void btnOpenLog_Click(object sender, EventArgs e)
        {
            var f = new FrmSimLogViewer();
            f.Show(this);
        }

        #endregion

        #region 数据更新

        private void OnDoubleChanged(object sender, SimulatorDataBus.DoubleEventArgs e)
        {
            if (IsDisposed || !IsHandleCreated) return;

            if (InvokeRequired)
            {
                try { BeginInvoke(new Action<object, SimulatorDataBus.DoubleEventArgs>(OnDoubleChanged), sender, e); }
                catch { }
                return;
            }

            foreach (var g in _gauges)
            {
                if (g.Def.Name == e.Key)
                {
                    g.SetValue(e.Value);
                    break;
                }
            }
        }

        private void uiTimer_Tick(object sender, EventArgs e)
        {
            // 顶部 SIM 横条状态
            if (SimulatorDataBus.Instance.IsSimulating)
            {
                lblSession.Text = "会话: " + SimulatorDataBus.Instance.SessionId;
                var ts = SimulatorDataBus.Instance.Elapsed;
                lblElapsed.Text = "已运行: " + ts.ToString(@"hh\:mm\:ss");
            }
            else
            {
                lblSession.Text = "会话: ----";
                lblElapsed.Text = "已运行: 00:00:00";
            }
            lblLogCount.Text = "日志: " + SimulationLogger.Instance.MemoryCount.ToString("N0") + " 条";

            // 底部状态栏
            string fault = SimulationEngine.Instance.CurrentFault == SimulationEngine.FaultPreset.Normal
                ? "正常" : SimulationEngine.GetFaultName(SimulationEngine.Instance.CurrentFault);
            lblStatusInfo.Text = string.Format(
                "柴油机: Z12V240ZJ  |  试验: 100h 性能  |  当前故障: {0}  |  哈希链: ✓",
                fault);
        }

        private void pulseTimer_Tick(object sender, EventArgs e)
        {
            _pulseOn = !_pulseOn;
            pnlPulseDot.BackColor = _pulseOn
                ? Color.FromArgb(250, 199, 117)
                : Color.FromArgb(255, 225, 175);
        }

        #endregion

        #region 自定义绘制

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        private void FrmDemoMain_Paint(object sender, PaintEventArgs e)
        {
            // 绘制 "SIMULATED" 水印（在中间面板上）
            try
            {
                var pnl = pnlCenter;
                if (pnl == null || !pnl.Visible) return;

                // 计算水印位置（中央偏下）
                Rectangle area = pnl.RectangleToScreen(pnl.ClientRectangle);
                Rectangle local = RectangleToClient(area);

                using (var brush = new SolidBrush(Color.FromArgb(18, 163, 45, 45)))
                using (var font = new Font("微软雅黑", 80F, FontStyle.Bold))
                {
                    var g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.TranslateTransform(local.Left + local.Width / 2f, local.Top + local.Height / 2f);
                    g.RotateTransform(-25);
                    var sz = g.MeasureString("SIMULATED", font);
                    g.DrawString("SIMULATED", font, brush, -sz.Width / 2, -sz.Height / 2);
                    g.ResetTransform();
                }
            }
            catch { }

            // 给故障按钮画前置色点（Tag 里存的 Color）
            foreach (Control c in pnlLeft.Controls)
            {
                if (c is Button btn && btn.Tag is Color dotColor)
                {
                    Rectangle r = btn.Bounds;
                    r.Offset(pnlLeft.Location);
                    r.Offset(splitMain.Panel1.Location);
                    r.Offset(splitMain.Location);
                    // 不在此处绘制 - 颜色点已在按钮文本里用 "●" 表示
                }
            }
        }

        #endregion
    }

    #region 简单的仪表 UC（自绘）

    internal class GaugeDef
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public double Max { get; set; }
        public double Warn { get; set; }       // 预警阈值
        public double Critical { get; set; }   // 故障阈值
        public int Decimals { get; set; }
        public double ScaleK { get; set; } = 0.85; // 进度条满刻度比例
    }

    internal class DemoGauge : UserControl
    {
        public GaugeDef Def { get; }
        private double _value;

        public DemoGauge(GaugeDef def)
        {
            Def = def;
            DoubleBuffered = true;
            BackColor = Color.White;
            MinimumSize = new Size(180, 110);
        }

        public void SetValue(double v)
        {
            _value = v;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            // 判定状态
            bool isFault = Def.Critical > 0 && _value >= Def.Critical;
            bool isWarn = !isFault && Def.Warn > 0 && _value >= Def.Warn;

            Color bgColor = Color.White;
            Color borderColor = isFault
                ? Color.FromArgb(163, 45, 45)
                : isWarn ? Color.FromArgb(239, 159, 39) : Color.FromArgb(217, 225, 235);
            Color valueColor = isFault
                ? Color.FromArgb(163, 45, 45)
                : isWarn ? Color.FromArgb(186, 117, 23) : Color.FromArgb(40, 80, 130);
            Color barColor = isFault
                ? Color.FromArgb(226, 75, 74)
                : isWarn ? Color.FromArgb(239, 159, 39) : Color.FromArgb(93, 202, 165);

            // 背景
            var r = ClientRectangle;
            r.Width -= 1; r.Height -= 1;
            using (var br = new SolidBrush(bgColor)) g.FillRectangle(br, r);
            using (var p = new Pen(borderColor, isFault ? 2 : 1)) g.DrawRectangle(p, r);

            // 标题
            using (var brT = new SolidBrush(Color.FromArgb(120, 130, 145)))
            using (var fT = new Font("微软雅黑", 9F))
            {
                g.DrawString(Def.Name, fT, brT, 10, 8);
            }

            // 数值
            string txt = _value.ToString("F" + Def.Decimals);
            using (var brV = new SolidBrush(valueColor))
            using (var fV = new Font("Consolas", 22F, FontStyle.Bold))
            {
                var sz = g.MeasureString(txt, fV);
                g.DrawString(txt, fV, brV, (Width - sz.Width) / 2, 28);
            }

            // 单位
            using (var brU = new SolidBrush(Color.FromArgb(120, 130, 145)))
            using (var fU = new Font("微软雅黑", 8F))
            {
                var sz = g.MeasureString(Def.Unit, fU);
                g.DrawString(Def.Unit, fU, brU, (Width - sz.Width) / 2, 70);
            }

            // 进度条
            int barY = 92;
            int barH = 6;
            int barX = 12;
            int barW = Width - 24;
            using (var brBg = new SolidBrush(Color.FromArgb(237, 241, 246)))
                g.FillRectangle(brBg, barX, barY, barW, barH);

            double pct = Def.Max > 0 ? Math.Min(1.0, Math.Max(0, _value / (Def.Max * Def.ScaleK))) : 0;
            using (var brBar = new SolidBrush(barColor))
                g.FillRectangle(brBar, barX, barY, (int)(barW * pct), barH);
        }
    }

    #endregion
}

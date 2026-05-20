namespace MainUI.Demo.UI
{
    partial class FrmDemoMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        // 顶部 SIM 横条
        private System.Windows.Forms.Panel pnlSimBar;
        private System.Windows.Forms.Label lblSimBarText;
        private System.Windows.Forms.Label lblSession;
        private System.Windows.Forms.Label lblElapsed;
        private System.Windows.Forms.Label lblLogCount;
        private System.Windows.Forms.Timer pulseTimer;
        private System.Windows.Forms.Panel pnlPulseDot;

        // 主区域
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.SplitContainer splitCenter;

        // 左侧控制面板
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblCtrlSection;
        private System.Windows.Forms.GroupBox grpThrottle;
        private System.Windows.Forms.TrackBar tbThrottle;
        private System.Windows.Forms.Label lblThrottleVal;
        private System.Windows.Forms.GroupBox grpExcitation;
        private System.Windows.Forms.TrackBar tbExcitation;
        private System.Windows.Forms.Label lblExcitationVal;
        private System.Windows.Forms.GroupBox grpWorkPoint;
        private System.Windows.Forms.Label lblWorkPoint;
        private System.Windows.Forms.Label lblFaultSection;
        private System.Windows.Forms.Button btnFault1;
        private System.Windows.Forms.Button btnFault2;
        private System.Windows.Forms.Button btnFault3;
        private System.Windows.Forms.Button btnFault4;
        private System.Windows.Forms.Button btnFaultEStop;
        private System.Windows.Forms.Button btnClearFault;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnOpenLog;

        // 中间仪表盘
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.Label lblDashSection;
        private System.Windows.Forms.TableLayoutPanel tlpGauges;

        // 底部状态栏
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Label lblStatusMode;
        private System.Windows.Forms.Label lblStatusInfo;

        // 刷新
        private System.Windows.Forms.Timer uiTimer;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.pnlSimBar = new System.Windows.Forms.Panel();
            this.lblSimBarText = new System.Windows.Forms.Label();
            this.lblSession = new System.Windows.Forms.Label();
            this.lblElapsed = new System.Windows.Forms.Label();
            this.lblLogCount = new System.Windows.Forms.Label();
            this.pnlPulseDot = new System.Windows.Forms.Panel();
            this.pulseTimer = new System.Windows.Forms.Timer(this.components);

            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlCenter = new System.Windows.Forms.Panel();

            this.lblCtrlSection = new System.Windows.Forms.Label();
            this.grpThrottle = new System.Windows.Forms.GroupBox();
            this.tbThrottle = new System.Windows.Forms.TrackBar();
            this.lblThrottleVal = new System.Windows.Forms.Label();
            this.grpExcitation = new System.Windows.Forms.GroupBox();
            this.tbExcitation = new System.Windows.Forms.TrackBar();
            this.lblExcitationVal = new System.Windows.Forms.Label();
            this.grpWorkPoint = new System.Windows.Forms.GroupBox();
            this.lblWorkPoint = new System.Windows.Forms.Label();

            this.lblFaultSection = new System.Windows.Forms.Label();
            this.btnFault1 = new System.Windows.Forms.Button();
            this.btnFault2 = new System.Windows.Forms.Button();
            this.btnFault3 = new System.Windows.Forms.Button();
            this.btnFault4 = new System.Windows.Forms.Button();
            this.btnFaultEStop = new System.Windows.Forms.Button();
            this.btnClearFault = new System.Windows.Forms.Button();

            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnOpenLog = new System.Windows.Forms.Button();

            this.lblDashSection = new System.Windows.Forms.Label();
            this.tlpGauges = new System.Windows.Forms.TableLayoutPanel();

            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblStatusMode = new System.Windows.Forms.Label();
            this.lblStatusInfo = new System.Windows.Forms.Label();

            this.uiTimer = new System.Windows.Forms.Timer(this.components);

            // ── 顶部 SIM 横条 ──
            this.pnlSimBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSimBar.Height = 38;
            this.pnlSimBar.BackColor = System.Drawing.Color.FromArgb(163, 45, 45);
            this.pnlSimBar.Controls.Add(this.pnlPulseDot);
            this.pnlSimBar.Controls.Add(this.lblSimBarText);
            this.pnlSimBar.Controls.Add(this.lblSession);
            this.pnlSimBar.Controls.Add(this.lblElapsed);
            this.pnlSimBar.Controls.Add(this.lblLogCount);

            this.pnlPulseDot.Location = new System.Drawing.Point(16, 15);
            this.pnlPulseDot.Size = new System.Drawing.Size(10, 10);
            this.pnlPulseDot.BackColor = System.Drawing.Color.FromArgb(250, 199, 117);

            this.lblSimBarText.Location = new System.Drawing.Point(36, 9);
            this.lblSimBarText.Size = new System.Drawing.Size(700, 22);
            this.lblSimBarText.ForeColor = System.Drawing.Color.White;
            this.lblSimBarText.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.lblSimBarText.Text = "⚠ 仿真模式运行中 — 当前数据为软件生成，非真机采集，仅供测试验证";

            this.lblSession.Location = new System.Drawing.Point(820, 11);
            this.lblSession.Size = new System.Drawing.Size(130, 18);
            this.lblSession.ForeColor = System.Drawing.Color.FromArgb(247, 193, 193);
            this.lblSession.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblSession.Text = "会话: ----";

            this.lblElapsed.Location = new System.Drawing.Point(950, 11);
            this.lblElapsed.Size = new System.Drawing.Size(160, 18);
            this.lblElapsed.ForeColor = System.Drawing.Color.FromArgb(247, 193, 193);
            this.lblElapsed.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblElapsed.Text = "已运行: 00:00:00";

            this.lblLogCount.Location = new System.Drawing.Point(1110, 11);
            this.lblLogCount.Size = new System.Drawing.Size(120, 18);
            this.lblLogCount.ForeColor = System.Drawing.Color.FromArgb(247, 193, 193);
            this.lblLogCount.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblLogCount.Text = "日志: 0 条";

            this.pulseTimer.Interval = 700;
            this.pulseTimer.Tick += new System.EventHandler(this.pulseTimer_Tick);

            // ── 中部 SplitContainer ──
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitMain.SplitterDistance = 260;
            this.splitMain.SplitterWidth = 1;
            this.splitMain.IsSplitterFixed = true;
            this.splitMain.Panel1.Controls.Add(this.pnlLeft);
            this.splitMain.Panel2.Controls.Add(this.pnlCenter);

            // ── 左侧 ──
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(243, 246, 250);
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(12);
            this.pnlLeft.AutoScroll = true;

            this.lblCtrlSection.Text = "仿真控制";
            this.lblCtrlSection.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblCtrlSection.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
            this.lblCtrlSection.Location = new System.Drawing.Point(12, 12);
            this.lblCtrlSection.Size = new System.Drawing.Size(200, 18);

            // 启停按钮
            this.btnStart.Location = new System.Drawing.Point(12, 32);
            this.btnStart.Size = new System.Drawing.Size(108, 32);
            this.btnStart.Text = "▶ 启动仿真";
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(40, 120, 60);
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);

            this.btnStop.Location = new System.Drawing.Point(124, 32);
            this.btnStop.Size = new System.Drawing.Size(108, 32);
            this.btnStop.Text = "■ 停止";
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(140, 140, 140);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);

            // 油门 GroupBox
            this.grpThrottle.Text = "  油门调节";
            this.grpThrottle.Location = new System.Drawing.Point(12, 76);
            this.grpThrottle.Size = new System.Drawing.Size(220, 70);
            this.grpThrottle.BackColor = System.Drawing.Color.White;
            this.grpThrottle.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.grpThrottle.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
            this.grpThrottle.Controls.Add(this.tbThrottle);
            this.grpThrottle.Controls.Add(this.lblThrottleVal);

            this.tbThrottle.Location = new System.Drawing.Point(8, 32);
            this.tbThrottle.Size = new System.Drawing.Size(160, 30);
            this.tbThrottle.Minimum = 0; this.tbThrottle.Maximum = 100; this.tbThrottle.Value = 80;
            this.tbThrottle.TickFrequency = 10;
            this.tbThrottle.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbThrottle.Scroll += new System.EventHandler(this.tbThrottle_Scroll);

            this.lblThrottleVal.Location = new System.Drawing.Point(172, 35);
            this.lblThrottleVal.Size = new System.Drawing.Size(46, 22);
            this.lblThrottleVal.Text = "80%";
            this.lblThrottleVal.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.lblThrottleVal.ForeColor = System.Drawing.Color.FromArgb(40, 120, 60);
            this.lblThrottleVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 励磁 GroupBox
            this.grpExcitation.Text = "  励磁调节";
            this.grpExcitation.Location = new System.Drawing.Point(12, 152);
            this.grpExcitation.Size = new System.Drawing.Size(220, 70);
            this.grpExcitation.BackColor = System.Drawing.Color.White;
            this.grpExcitation.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.grpExcitation.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
            this.grpExcitation.Controls.Add(this.tbExcitation);
            this.grpExcitation.Controls.Add(this.lblExcitationVal);

            this.tbExcitation.Location = new System.Drawing.Point(8, 32);
            this.tbExcitation.Size = new System.Drawing.Size(160, 30);
            this.tbExcitation.Minimum = 0; this.tbExcitation.Maximum = 100; this.tbExcitation.Value = 80;
            this.tbExcitation.TickFrequency = 10;
            this.tbExcitation.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbExcitation.Scroll += new System.EventHandler(this.tbExcitation_Scroll);

            this.lblExcitationVal.Location = new System.Drawing.Point(172, 35);
            this.lblExcitationVal.Size = new System.Drawing.Size(46, 22);
            this.lblExcitationVal.Text = "80%";
            this.lblExcitationVal.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.lblExcitationVal.ForeColor = System.Drawing.Color.FromArgb(40, 120, 60);
            this.lblExcitationVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 工况
            this.grpWorkPoint.Text = "  当前工况";
            this.grpWorkPoint.Location = new System.Drawing.Point(12, 228);
            this.grpWorkPoint.Size = new System.Drawing.Size(220, 60);
            this.grpWorkPoint.BackColor = System.Drawing.Color.White;
            this.grpWorkPoint.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.grpWorkPoint.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
            this.grpWorkPoint.Controls.Add(this.lblWorkPoint);

            this.lblWorkPoint.Location = new System.Drawing.Point(10, 24);
            this.lblWorkPoint.Size = new System.Drawing.Size(200, 30);
            this.lblWorkPoint.Text = "标定功率\n100h 性能试验 · 1/3";
            this.lblWorkPoint.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblWorkPoint.ForeColor = System.Drawing.Color.FromArgb(50, 60, 75);

            // 故障注入分区
            this.lblFaultSection.Text = "故障注入";
            this.lblFaultSection.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblFaultSection.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
            this.lblFaultSection.Location = new System.Drawing.Point(12, 300);
            this.lblFaultSection.Size = new System.Drawing.Size(200, 18);

            int fy = 322;
            StyleFaultBtn(this.btnFault1, "● 高温水 ≥101℃ 报警",
                System.Drawing.Color.FromArgb(239, 159, 39), 12, fy);
            this.btnFault1.Click += new System.EventHandler(this.btnFault1_Click);

            StyleFaultBtn(this.btnFault2, "● 高温水 ≥103℃ 降载",
                System.Drawing.Color.FromArgb(226, 75, 74), 12, fy + 36);
            this.btnFault2.Click += new System.EventHandler(this.btnFault2_Click);

            StyleFaultBtn(this.btnFault3, "● 轴温 ≥123℃ 停机",
                System.Drawing.Color.FromArgb(226, 75, 74), 12, fy + 72);
            this.btnFault3.Click += new System.EventHandler(this.btnFault3_Click);

            StyleFaultBtn(this.btnFault4, "● 主油道压力过低",
                System.Drawing.Color.FromArgb(226, 75, 74), 12, fy + 108);
            this.btnFault4.Click += new System.EventHandler(this.btnFault4_Click);

            StyleFaultBtn(this.btnFaultEStop, "● 急停按钮触发",
                System.Drawing.Color.FromArgb(163, 45, 45), 12, fy + 144);
            this.btnFaultEStop.BackColor = System.Drawing.Color.FromArgb(252, 235, 235);
            this.btnFaultEStop.Click += new System.EventHandler(this.btnFaultEStop_Click);

            this.btnClearFault.Location = new System.Drawing.Point(12, fy + 188);
            this.btnClearFault.Size = new System.Drawing.Size(220, 32);
            this.btnClearFault.Text = "✓ 清除故障，恢复正常";
            this.btnClearFault.BackColor = System.Drawing.Color.FromArgb(40, 120, 60);
            this.btnClearFault.ForeColor = System.Drawing.Color.White;
            this.btnClearFault.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.btnClearFault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearFault.FlatAppearance.BorderSize = 0;
            this.btnClearFault.Click += new System.EventHandler(this.btnClearFault_Click);

            this.btnOpenLog.Location = new System.Drawing.Point(12, fy + 232);
            this.btnOpenLog.Size = new System.Drawing.Size(220, 30);
            this.btnOpenLog.Text = "📋 打开日志查看器";
            this.btnOpenLog.BackColor = System.Drawing.Color.White;
            this.btnOpenLog.ForeColor = System.Drawing.Color.FromArgb(55, 138, 221);
            this.btnOpenLog.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnOpenLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenLog.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(181, 212, 244);
            this.btnOpenLog.Click += new System.EventHandler(this.btnOpenLog_Click);

            this.pnlLeft.Controls.Add(this.lblCtrlSection);
            this.pnlLeft.Controls.Add(this.btnStart);
            this.pnlLeft.Controls.Add(this.btnStop);
            this.pnlLeft.Controls.Add(this.grpThrottle);
            this.pnlLeft.Controls.Add(this.grpExcitation);
            this.pnlLeft.Controls.Add(this.grpWorkPoint);
            this.pnlLeft.Controls.Add(this.lblFaultSection);
            this.pnlLeft.Controls.Add(this.btnFault1);
            this.pnlLeft.Controls.Add(this.btnFault2);
            this.pnlLeft.Controls.Add(this.btnFault3);
            this.pnlLeft.Controls.Add(this.btnFault4);
            this.pnlLeft.Controls.Add(this.btnFaultEStop);
            this.pnlLeft.Controls.Add(this.btnClearFault);
            this.pnlLeft.Controls.Add(this.btnOpenLog);

            // ── 中间仪表盘 ──
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.pnlCenter.Padding = new System.Windows.Forms.Padding(16);

            this.lblDashSection.Text = "柴油机仪表盘  ·  Z12V240ZJ  ·  标定功率工况";
            this.lblDashSection.Location = new System.Drawing.Point(16, 16);
            this.lblDashSection.Size = new System.Drawing.Size(900, 24);
            this.lblDashSection.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.lblDashSection.ForeColor = System.Drawing.Color.FromArgb(50, 60, 75);

            this.tlpGauges.Location = new System.Drawing.Point(16, 48);
            this.tlpGauges.Size = new System.Drawing.Size(960, 520);
            this.tlpGauges.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.tlpGauges.ColumnCount = 4;
            this.tlpGauges.RowCount = 3;
            for (int i = 0; i < 4; i++)
                this.tlpGauges.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            for (int i = 0; i < 3; i++)
                this.tlpGauges.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlpGauges.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
            this.tlpGauges.BackColor = System.Drawing.Color.Transparent;

            this.pnlCenter.Controls.Add(this.tlpGauges);
            this.pnlCenter.Controls.Add(this.lblDashSection);

            // ── 底部状态栏 ──
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatus.Height = 26;
            this.pnlStatus.BackColor = System.Drawing.Color.FromArgb(237, 241, 246);
            this.pnlStatus.Controls.Add(this.lblStatusMode);
            this.pnlStatus.Controls.Add(this.lblStatusInfo);

            this.lblStatusMode.Location = new System.Drawing.Point(12, 5);
            this.lblStatusMode.Size = new System.Drawing.Size(180, 18);
            this.lblStatusMode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatusMode.ForeColor = System.Drawing.Color.FromArgb(186, 117, 23);
            this.lblStatusMode.Text = "● Demo 模式";

            this.lblStatusInfo.Location = new System.Drawing.Point(200, 5);
            this.lblStatusInfo.Size = new System.Drawing.Size(700, 18);
            this.lblStatusInfo.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblStatusInfo.ForeColor = System.Drawing.Color.FromArgb(100, 110, 125);
            this.lblStatusInfo.Text = "柴油机: Z12V240ZJ  |  试验: 100h 性能  |  工况: 1/3 标定功率  |  哈希链: 等待启动";

            // ── UI 刷新 ──
            this.uiTimer.Interval = 250;
            this.uiTimer.Tick += new System.EventHandler(this.uiTimer_Tick);

            // ── 窗体 ──
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 760);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlSimBar);
            this.Controls.Add(this.pnlStatus);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.BackColor = System.Drawing.Color.White;
            this.MinimumSize = new System.Drawing.Size(1100, 700);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "柴油机认证试验台 - 仿真演示版 (MainUI.Demo)";
            this.Load += new System.EventHandler(this.FrmDemoMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDemoMain_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmDemoMain_Paint);
        }

        private static void StyleFaultBtn(System.Windows.Forms.Button btn, string text,
            System.Drawing.Color dotColor, int x, int y)
        {
            btn.Location = new System.Drawing.Point(x, y);
            btn.Size = new System.Drawing.Size(220, 32);
            btn.Text = text;
            btn.BackColor = System.Drawing.Color.White;
            btn.ForeColor = System.Drawing.Color.FromArgb(50, 60, 75);
            btn.Font = new System.Drawing.Font("微软雅黑", 9F);
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(217, 225, 235);
            btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btn.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            btn.Tag = dotColor; // 给 Paint 用
        }
    }
}

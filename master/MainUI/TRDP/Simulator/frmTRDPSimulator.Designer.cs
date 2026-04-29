namespace MainUI
{
    partial class frmTRDPSimulator
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();

                // 反订阅型号切换事件，防止窗体关闭后回调已销毁控件
                MainUI.Global.EventTriggerModel.OnModelNameChanged -= OnModelNameChanged;

                if (_uiTimer != null)
                {
                    _uiTimer.Stop();
                    _uiTimer.Dispose();
                    _uiTimer = null;
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改此方法的内容。
        /// 静态骨架控件在此初始化，动态 Tab 内容由 frmTRDPSimulator.cs 的
        /// RebuildDynamicTabs() 方法负责填充。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // ── 顶部工具栏 ──────────────────────────────────────────
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this._lblStatus = new System.Windows.Forms.Label();
            this._lblTick = new System.Windows.Forms.Label();
            this._btnStart = new System.Windows.Forms.Button();
            this._btnStop = new System.Windows.Forms.Button();
            this._btnSync = new System.Windows.Forms.Button();

            // ── 底部日志区 ──────────────────────────────────────────
            this.pnlLog = new System.Windows.Forms.Panel();
            this.lblLogTitle = new System.Windows.Forms.Label();
            this._rtbLog = new System.Windows.Forms.RichTextBox();
            this.btnClrLog = new System.Windows.Forms.Button();

            // ── 主 TabControl ───────────────────────────────────────
            this.tabMain = new System.Windows.Forms.TabControl();

            // ── 各 TabPage（静态外壳，内容动态生成）────────────────
            this.tpAnalog = new System.Windows.Forms.TabPage();
            this.tpDigital = new System.Windows.Forms.TabPage();
            this.tpAxis = new System.Windows.Forms.TabPage();
            this.tpStatus = new System.Windows.Forms.TabPage();

            // ── 状态 Tab 内的静态控件 ───────────────────────────────
            this.pnlStatus = new System.Windows.Forms.Panel();
            this._lblLifeVal = new System.Windows.Forms.Label();
            this._btnLifeAuto = new System.Windows.Forms.Button();

            // ── 定时器 ──────────────────────────────────────────────
            this._uiTimer = new System.Windows.Forms.Timer(this.components);

            // ────────────────────────────────────────────────────────
            // pnlTop
            // ────────────────────────────────────────────────────────
            this.pnlTop.SuspendLayout();
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 50;
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(30, 56, 100);
            this.pnlTop.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 14f, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Text = "TRDP 模拟控制台";

            this._lblStatus.AutoSize = true;
            this._lblStatus.Font = new System.Drawing.Font("微软雅黑", 9f);
            this._lblStatus.ForeColor = System.Drawing.Color.FromArgb(180, 200, 180);
            this._lblStatus.Location = new System.Drawing.Point(240, 17);
            this._lblStatus.Text = "● 未启动";
            this._lblStatus.Name = "_lblStatus";

            this._lblTick.AutoSize = true;
            this._lblTick.Font = new System.Drawing.Font("Consolas", 9f);
            this._lblTick.ForeColor = System.Drawing.Color.FromArgb(160, 200, 160);
            this._lblTick.Location = new System.Drawing.Point(360, 17);
            this._lblTick.Text = "帧: 0";
            this._lblTick.Name = "_lblTick";

            // 按钮辅助
            System.Action<System.Windows.Forms.Button, string, System.Drawing.Color> styleBtn =
                (b, txt, clr) =>
                {
                    b.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    b.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(80, 120, 160);
                    b.BackColor = clr;
                    b.ForeColor = System.Drawing.Color.White;
                    b.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Bold);
                    b.Size = new System.Drawing.Size(80, 30);
                    b.Cursor = System.Windows.Forms.Cursors.Hand;
                    b.Text = txt;
                };

            styleBtn(_btnStart, "▶ 启动", System.Drawing.Color.FromArgb(40, 130, 60));
            this._btnStart.Location = new System.Drawing.Point(460, 10);
            this._btnStart.Name = "_btnStart";

            styleBtn(_btnStop, "■ 停止", System.Drawing.Color.FromArgb(160, 50, 50));
            this._btnStop.Location = new System.Drawing.Point(548, 10);
            this._btnStop.Name = "_btnStop";

            styleBtn(_btnSync, "⟳ 同步信号", System.Drawing.Color.FromArgb(60, 100, 160));
            this._btnSync.Size = new System.Drawing.Size(96, 30);
            this._btnSync.Location = new System.Drawing.Point(636, 10);
            this._btnSync.Name = "_btnSync";

            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Controls.Add(this._lblStatus);
            this.pnlTop.Controls.Add(this._lblTick);
            this.pnlTop.Controls.Add(this._btnStart);
            this.pnlTop.Controls.Add(this._btnStop);
            this.pnlTop.Controls.Add(this._btnSync);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();

            // ────────────────────────────────────────────────────────
            // pnlLog（底部日志）
            // ────────────────────────────────────────────────────────
            this.pnlLog.SuspendLayout();
            this.pnlLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLog.Height = 150;
            this.pnlLog.BackColor = System.Drawing.Color.FromArgb(25, 25, 30);

            this.lblLogTitle.AutoSize = true;
            this.lblLogTitle.Font = new System.Drawing.Font("微软雅黑", 8f, System.Drawing.FontStyle.Bold);
            this.lblLogTitle.ForeColor = System.Drawing.Color.FromArgb(140, 160, 180);
            this.lblLogTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLogTitle.Text = "  运行日志";
            this.lblLogTitle.Height = 20;

            this._rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rtbLog.Font = new System.Drawing.Font("Consolas", 8.5f);
            this._rtbLog.BackColor = System.Drawing.Color.FromArgb(30, 30, 35);
            this._rtbLog.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this._rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._rtbLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this._rtbLog.ReadOnly = true;
            this._rtbLog.Name = "_rtbLog";

            this.btnClrLog.Text = "清空";
            this.btnClrLog.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClrLog.Width = 50;
            this.btnClrLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClrLog.ForeColor = System.Drawing.Color.Gray;
            this.btnClrLog.Font = new System.Drawing.Font("微软雅黑", 8f);
            this.btnClrLog.Name = "btnClrLog";

            this.pnlLog.Controls.Add(this._rtbLog);
            this.pnlLog.Controls.Add(this.lblLogTitle);
            this.pnlLog.Controls.Add(this.btnClrLog);
            this.pnlLog.ResumeLayout(false);
            this.pnlLog.PerformLayout();

            // ────────────────────────────────────────────────────────
            // tabMain 及各 TabPage
            // ────────────────────────────────────────────────────────
            this.tpAnalog.Text = "模拟量";
            this.tpAnalog.Name = "tpAnalog";
            this.tpAnalog.UseVisualStyleBackColor = true;

            this.tpDigital.Text = "数字量（故障位）";
            this.tpDigital.Name = "tpDigital";
            this.tpDigital.UseVisualStyleBackColor = true;

            this.tpAxis.Text = "轴温";
            this.tpAxis.Name = "tpAxis";
            this.tpAxis.UseVisualStyleBackColor = true;

            // ────────────────────────────────────────────────────────
            // tpStatus：静态内容，不随型号变化
            // ────────────────────────────────────────────────────────
            this.tpStatus.Text = "通讯状态";
            this.tpStatus.Name = "tpStatus";
            this.tpStatus.UseVisualStyleBackColor = true;

            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStatus.AutoScroll = true;

            this._lblLifeVal.AutoSize = true;
            this._lblLifeVal.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Bold);
            this._lblLifeVal.ForeColor = System.Drawing.Color.FromArgb(20, 80, 160);
            this._lblLifeVal.Location = new System.Drawing.Point(8, 40);
            this._lblLifeVal.Text = "生命信号值：0";
            this._lblLifeVal.Name = "_lblLifeVal";

            this._btnLifeAuto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnLifeAuto.BackColor = System.Drawing.Color.FromArgb(40, 120, 60);
            this._btnLifeAuto.ForeColor = System.Drawing.Color.White;
            this._btnLifeAuto.Font = new System.Drawing.Font("微软雅黑", 9f);
            this._btnLifeAuto.Location = new System.Drawing.Point(200, 36);
            this._btnLifeAuto.Size = new System.Drawing.Size(160, 28);
            this._btnLifeAuto.Text = "● 自动递增（已启用）";
            this._btnLifeAuto.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnLifeAuto.Name = "_btnLifeAuto";

            var lblStatusHdr = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(30, 56, 100),
                Location = new System.Drawing.Point(8, 10),
                Text = "设备生命信号（D01）  [byteOffset: 来自当前型号 Excel, U16, 0-255, 周期递增]"
            };

            this.pnlStatus.Controls.Add(lblStatusHdr);
            this.pnlStatus.Controls.Add(this._lblLifeVal);
            this.pnlStatus.Controls.Add(this._btnLifeAuto);
            this.tpStatus.Controls.Add(this.pnlStatus);

            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("微软雅黑", 9f);
            this.tabMain.Name = "tabMain";
            this.tabMain.TabPages.Add(this.tpAnalog);
            this.tabMain.TabPages.Add(this.tpDigital);
            this.tabMain.TabPages.Add(this.tpAxis);
            this.tabMain.TabPages.Add(this.tpStatus);

            // ────────────────────────────────────────────────────────
            // 定时器
            // ────────────────────────────────────────────────────────
            this._uiTimer.Interval = 1000;
            this._uiTimer.Enabled = false;

            // ────────────────────────────────────────────────────────
            // Form
            // ────────────────────────────────────────────────────────
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 248);
            this.ClientSize = new System.Drawing.Size(1060, 780);
            this.MinimumSize = new System.Drawing.Size(900, 650);
            this.Font = new System.Drawing.Font("微软雅黑", 9f);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TRDP 模拟控制台";
            this.Name = "frmTRDPSimulator";

            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.pnlLog);
            this.Controls.Add(this.pnlTop);
            this.ResumeLayout(false);
        }

        #endregion

        // ── 控件字段声明（Designer 管理） ───────────────────────────────
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label _lblStatus;
        private System.Windows.Forms.Label _lblTick;
        private System.Windows.Forms.Button _btnStart;
        private System.Windows.Forms.Button _btnStop;
        private System.Windows.Forms.Button _btnSync;

        private System.Windows.Forms.Panel pnlLog;
        private System.Windows.Forms.Label lblLogTitle;
        private System.Windows.Forms.RichTextBox _rtbLog;
        private System.Windows.Forms.Button btnClrLog;

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tpAnalog;
        private System.Windows.Forms.TabPage tpDigital;
        private System.Windows.Forms.TabPage tpAxis;
        private System.Windows.Forms.TabPage tpStatus;

        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Label _lblLifeVal;
        private System.Windows.Forms.Button _btnLifeAuto;

        private System.Windows.Forms.Timer _uiTimer;
    }
}
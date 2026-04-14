namespace MainUI
{
    partial class ucALLPipelineHMI
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucALLPipelineHMI));
            this.btnWater = new Sunny.UI.UIButton();
            this.btnJY = new Sunny.UI.UIButton();
            this.btnRY = new Sunny.UI.UIButton();
            this.btnMainContronl = new Sunny.UI.UIButton();
            this.btnContronl = new Sunny.UI.UIButton();
            this.panelSystem = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picRunStatus = new System.Windows.Forms.PictureBox();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnCycle = new Sunny.UI.UIButton();
            this.btnExit = new Sunny.UI.UIImageButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslblUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblPLC = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblCKPLC = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblStart = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblWeight = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblElectrical = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblJYOK = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblJYNG = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblJYSim = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblRYOK = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblRYNG = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblRYSim = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblRYHY = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblWaterOK = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblWaterNG = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblWaterSim = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblTRDP = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblCommunication = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerPLC = new System.Windows.Forms.Timer(this.components);
            this.tslblJYBC = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRunStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnWater
            // 
            this.btnWater.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWater.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnWater.Location = new System.Drawing.Point(5, 5);
            this.btnWater.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnWater.Name = "btnWater";
            this.btnWater.Size = new System.Drawing.Size(83, 76);
            this.btnWater.Style = Sunny.UI.UIStyle.Custom;
            this.btnWater.StyleCustomMode = true;
            this.btnWater.TabIndex = 524;
            this.btnWater.Tag = "高温水/低温水系统";
            this.btnWater.Text = "水系统";
            this.btnWater.TipsFont = new System.Drawing.Font("微软雅黑", 13F);
            this.btnWater.Click += new System.EventHandler(this.btnWater_Click);
            // 
            // btnJY
            // 
            this.btnJY.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnJY.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnJY.Location = new System.Drawing.Point(5, 95);
            this.btnJY.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnJY.Name = "btnJY";
            this.btnJY.Size = new System.Drawing.Size(83, 76);
            this.btnJY.StyleCustomMode = true;
            this.btnJY.TabIndex = 525;
            this.btnJY.Tag = "机油系统";
            this.btnJY.Text = "机油系统";
            this.btnJY.TipsFont = new System.Drawing.Font("微软雅黑", 13F);
            this.btnJY.Click += new System.EventHandler(this.btnWater_Click);
            // 
            // btnRY
            // 
            this.btnRY.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRY.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnRY.Location = new System.Drawing.Point(5, 184);
            this.btnRY.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnRY.Name = "btnRY";
            this.btnRY.Size = new System.Drawing.Size(83, 76);
            this.btnRY.StyleCustomMode = true;
            this.btnRY.TabIndex = 526;
            this.btnRY.Tag = "燃油系统";
            this.btnRY.Text = "燃油系统";
            this.btnRY.TipsFont = new System.Drawing.Font("微软雅黑", 13F);
            this.btnRY.Click += new System.EventHandler(this.btnWater_Click);
            // 
            // btnMainContronl
            // 
            this.btnMainContronl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMainContronl.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnMainContronl.Location = new System.Drawing.Point(5, 446);
            this.btnMainContronl.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnMainContronl.Name = "btnMainContronl";
            this.btnMainContronl.Size = new System.Drawing.Size(54, 35);
            this.btnMainContronl.StyleCustomMode = true;
            this.btnMainContronl.TabIndex = 530;
            this.btnMainContronl.Text = "发动机相关控制";
            this.btnMainContronl.TipsFont = new System.Drawing.Font("微软雅黑", 13F);
            this.btnMainContronl.Visible = false;
            this.btnMainContronl.Click += new System.EventHandler(this.btnWater_Click);
            // 
            // btnContronl
            // 
            this.btnContronl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnContronl.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnContronl.Location = new System.Drawing.Point(5, 273);
            this.btnContronl.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnContronl.Name = "btnContronl";
            this.btnContronl.Size = new System.Drawing.Size(83, 76);
            this.btnContronl.StyleCustomMode = true;
            this.btnContronl.TabIndex = 529;
            this.btnContronl.Tag = "管路控制";
            this.btnContronl.Text = "管路控制";
            this.btnContronl.TipsFont = new System.Drawing.Font("微软雅黑", 13F);
            this.btnContronl.Click += new System.EventHandler(this.btnWater_Click);
            // 
            // panelSystem
            // 
            this.panelSystem.Location = new System.Drawing.Point(3, 3);
            this.panelSystem.Name = "panelSystem";
            this.panelSystem.Size = new System.Drawing.Size(1825, 1006);
            this.panelSystem.TabIndex = 528;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Controls.Add(this.picRunStatus);
            this.panel2.Controls.Add(this.Logo);
            this.panel2.Controls.Add(this.lblDateTime);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1920, 41);
            this.panel2.TabIndex = 529;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(327, -2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1266, 44);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "油、水管路系统控制界面";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picRunStatus
            // 
            this.picRunStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picRunStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.picRunStatus.Image = global::MainUI.Properties.Resources.normal;
            this.picRunStatus.Location = new System.Drawing.Point(1879, 1);
            this.picRunStatus.Name = "picRunStatus";
            this.picRunStatus.Size = new System.Drawing.Size(41, 38);
            this.picRunStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRunStatus.TabIndex = 1;
            this.picRunStatus.TabStop = false;
            // 
            // Logo
            // 
            this.Logo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.Logo.Image = global::MainUI.Properties.Resources.logo;
            this.Logo.Location = new System.Drawing.Point(2, 3);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(137, 36);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Logo.TabIndex = 2;
            this.Logo.TabStop = false;
            // 
            // lblDateTime
            // 
            this.lblDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.lblDateTime.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.lblDateTime.ForeColor = System.Drawing.Color.White;
            this.lblDateTime.Location = new System.Drawing.Point(1658, 9);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(193, 24);
            this.lblDateTime.TabIndex = 4;
            this.lblDateTime.Text = "2016-09-13 00:00:00";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 41);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnCycle);
            this.splitContainer1.Panel1.Controls.Add(this.btnMainContronl);
            this.splitContainer1.Panel1.Controls.Add(this.btnExit);
            this.splitContainer1.Panel1.Controls.Add(this.btnContronl);
            this.splitContainer1.Panel1.Controls.Add(this.btnRY);
            this.splitContainer1.Panel1.Controls.Add(this.btnWater);
            this.splitContainer1.Panel1.Controls.Add(this.btnJY);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("宋体", 10.5F);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.panelSystem);
            this.splitContainer1.Size = new System.Drawing.Size(1920, 1039);
            this.splitContainer1.SplitterDistance = 91;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 531;
            // 
            // btnCycle
            // 
            this.btnCycle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCycle.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnCycle.Location = new System.Drawing.Point(5, 364);
            this.btnCycle.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCycle.Name = "btnCycle";
            this.btnCycle.Size = new System.Drawing.Size(83, 76);
            this.btnCycle.StyleCustomMode = true;
            this.btnCycle.TabIndex = 531;
            this.btnCycle.Tag = "一键循环";
            this.btnCycle.Text = "一键循环";
            this.btnCycle.TipsFont = new System.Drawing.Font("微软雅黑", 13F);
            this.btnCycle.Click += new System.EventHandler(this.btnWater_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnExit.ImageHover")));
            this.btnExit.ImageOffset = new System.Drawing.Point(12, 5);
            this.btnExit.ImagePress = ((System.Drawing.Image)(resources.GetObject("btnExit.ImagePress")));
            this.btnExit.Location = new System.Drawing.Point(5, 946);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 90);
            this.btnExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnExit.TabIndex = 115;
            this.btnExit.TabStop = false;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.statusStrip1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblUser,
            this.tslblPLC,
            this.tslblCKPLC,
            this.tslblStart,
            this.tslblWeight,
            this.tslblElectrical,
            this.tslblJYOK,
            this.tslblJYNG,
            this.tslblJYSim,
            this.tslblRYOK,
            this.tslblRYNG,
            this.tslblRYSim,
            this.tslblRYHY,
            this.tslblJYBC,
            this.tslblWaterOK,
            this.tslblWaterNG,
            this.tslblWaterSim,
            this.tslblTRDP,
            this.tslblCommunication,
            this.tslblVersion});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1009);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1828, 30);
            this.statusStrip1.TabIndex = 529;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslblUser
            // 
            this.tslblUser.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblUser.Margin = new System.Windows.Forms.Padding(0, 3, 3, 2);
            this.tslblUser.Name = "tslblUser";
            this.tslblUser.Size = new System.Drawing.Size(78, 25);
            this.tslblUser.Text = "用户名称";
            // 
            // tslblPLC
            // 
            this.tslblPLC.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblPLC.ForeColor = System.Drawing.Color.Black;
            this.tslblPLC.Margin = new System.Windows.Forms.Padding(0, 3, 3, 2);
            this.tslblPLC.Name = "tslblPLC";
            this.tslblPLC.Size = new System.Drawing.Size(107, 25);
            this.tslblPLC.Text = "台位控制PLC";
            // 
            // tslblCKPLC
            // 
            this.tslblCKPLC.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblCKPLC.Name = "tslblCKPLC";
            this.tslblCKPLC.Size = new System.Drawing.Size(123, 25);
            this.tslblCKPLC.Text = "发动机测量PLC";
            // 
            // tslblStart
            // 
            this.tslblStart.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblStart.Name = "tslblStart";
            this.tslblStart.Size = new System.Drawing.Size(91, 25);
            this.tslblStart.Text = "启动柜PLC";
            // 
            // tslblWeight
            // 
            this.tslblWeight.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblWeight.Name = "tslblWeight";
            this.tslblWeight.Size = new System.Drawing.Size(62, 25);
            this.tslblWeight.Text = "称重仪";
            // 
            // tslblElectrical
            // 
            this.tslblElectrical.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblElectrical.Name = "tslblElectrical";
            this.tslblElectrical.Size = new System.Drawing.Size(110, 25);
            this.tslblElectrical.Text = "发动机电参数";
            // 
            // tslblJYOK
            // 
            this.tslblJYOK.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblJYOK.Name = "tslblJYOK";
            this.tslblJYOK.Size = new System.Drawing.Size(104, 25);
            this.tslblJYOK.Text = "(机油)机油箱";
            // 
            // tslblJYNG
            // 
            this.tslblJYNG.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblJYNG.Name = "tslblJYNG";
            this.tslblJYNG.Size = new System.Drawing.Size(104, 25);
            this.tslblJYNG.Text = "(机油)流量计";
            // 
            // tslblJYSim
            // 
            this.tslblJYSim.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblJYSim.Name = "tslblJYSim";
            this.tslblJYSim.Size = new System.Drawing.Size(88, 25);
            this.tslblJYSim.Text = "(机油)液位";
            // 
            // tslblRYOK
            // 
            this.tslblRYOK.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblRYOK.Name = "tslblRYOK";
            this.tslblRYOK.Size = new System.Drawing.Size(62, 25);
            this.tslblRYOK.Text = "燃油箱";
            // 
            // tslblRYNG
            // 
            this.tslblRYNG.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblRYNG.Name = "tslblRYNG";
            this.tslblRYNG.Size = new System.Drawing.Size(94, 25);
            this.tslblRYNG.Text = "燃油箱错误";
            // 
            // tslblRYSim
            // 
            this.tslblRYSim.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblRYSim.Name = "tslblRYSim";
            this.tslblRYSim.Size = new System.Drawing.Size(94, 25);
            this.tslblRYSim.Text = "燃油箱模拟";
            // 
            // tslblRYHY
            // 
            this.tslblRYHY.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblRYHY.Name = "tslblRYHY";
            this.tslblRYHY.Size = new System.Drawing.Size(78, 25);
            this.tslblRYHY.Text = "燃油耗仪";
            // 
            // tslblWaterOK
            // 
            this.tslblWaterOK.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblWaterOK.Name = "tslblWaterOK";
            this.tslblWaterOK.Size = new System.Drawing.Size(94, 25);
            this.tslblWaterOK.Text = "水系统数据";
            // 
            // tslblWaterNG
            // 
            this.tslblWaterNG.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblWaterNG.Name = "tslblWaterNG";
            this.tslblWaterNG.Size = new System.Drawing.Size(94, 25);
            this.tslblWaterNG.Text = "水系统错误";
            // 
            // tslblWaterSim
            // 
            this.tslblWaterSim.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblWaterSim.Name = "tslblWaterSim";
            this.tslblWaterSim.Size = new System.Drawing.Size(94, 25);
            this.tslblWaterSim.Text = "水系统模拟";
            // 
            // tslblTRDP
            // 
            this.tslblTRDP.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblTRDP.Name = "tslblTRDP";
            this.tslblTRDP.Size = new System.Drawing.Size(110, 25);
            this.tslblTRDP.Text = "柴油机控制器";
            // 
            // tslblCommunication
            // 
            this.tslblCommunication.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblCommunication.Name = "tslblCommunication";
            this.tslblCommunication.Size = new System.Drawing.Size(110, 25);
            this.tslblCommunication.Text = "台位主从通讯";
            // 
            // tslblVersion
            // 
            this.tslblVersion.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblVersion.Name = "tslblVersion";
            this.tslblVersion.Size = new System.Drawing.Size(62, 25);
            this.tslblVersion.Text = "版本号";
            // 
            // timerPLC
            // 
            this.timerPLC.Interval = 1000;
            this.timerPLC.Tick += new System.EventHandler(this.timerPLC_Tick);
            // 
            // tslblJYBC
            // 
            this.tslblJYBC.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblJYBC.Name = "tslblJYBC";
            this.tslblJYBC.Size = new System.Drawing.Size(94, 25);
            this.tslblJYBC.Text = "机油耗磅秤";
            // 
            // ucALLPipelineHMI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("宋体", 11F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucALLPipelineHMI";
            this.Size = new System.Drawing.Size(1920, 1080);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRunStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIButton btnWater;
        private Sunny.UI.UIButton btnJY;
        private Sunny.UI.UIButton btnRY;
        private System.Windows.Forms.Panel panelSystem;
        private Sunny.UI.UIButton btnContronl;
        private Sunny.UI.UIButton btnMainContronl;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picRunStatus;
        private System.Windows.Forms.Label lblDateTime;
        public System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Sunny.UI.UIImageButton btnExit;
        private System.Windows.Forms.Timer timerPLC;
        private Sunny.UI.UIButton btnCycle;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslblUser;
        private System.Windows.Forms.ToolStripStatusLabel tslblPLC;
        private System.Windows.Forms.ToolStripStatusLabel tslblCKPLC;
        private System.Windows.Forms.ToolStripStatusLabel tslblRYHY;
        private System.Windows.Forms.ToolStripStatusLabel tslblStart;
        private System.Windows.Forms.ToolStripStatusLabel tslblElectrical;
        private System.Windows.Forms.ToolStripStatusLabel tslblJYOK;
        private System.Windows.Forms.ToolStripStatusLabel tslblJYNG;
        private System.Windows.Forms.ToolStripStatusLabel tslblJYSim;
        private System.Windows.Forms.ToolStripStatusLabel tslblRYOK;
        private System.Windows.Forms.ToolStripStatusLabel tslblRYNG;
        private System.Windows.Forms.ToolStripStatusLabel tslblRYSim;
        private System.Windows.Forms.ToolStripStatusLabel tslblWaterOK;
        private System.Windows.Forms.ToolStripStatusLabel tslblWaterNG;
        private System.Windows.Forms.ToolStripStatusLabel tslblWaterSim;
        private System.Windows.Forms.ToolStripStatusLabel tslblTRDP;
        private System.Windows.Forms.ToolStripStatusLabel tslblCommunication;
        private System.Windows.Forms.ToolStripStatusLabel tslblVersion;
        private System.Windows.Forms.ToolStripStatusLabel tslblWeight;
        private System.Windows.Forms.ToolStripStatusLabel tslblJYBC;
    }
}

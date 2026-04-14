namespace MainUI
{
    partial class frmMainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainMenu));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picRunStatus = new System.Windows.Forms.PictureBox();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timerPLC = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslblUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblPLC = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblCKPLC = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblStart = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblElectrical = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblWeight = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblJYOK = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblJYNG = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblJYSim = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblRYOK = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblRYNG = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblRYHY = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblRYSim = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblWaterOK = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblWaterNG = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblWaterSim = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblTRDP = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblCommunication = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelContronl = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExit = new Sunny.UI.UIImageButton();
            this.btnChangePwd = new Sunny.UI.UIImageButton();
            this.btnReports = new Sunny.UI.UIImageButton();
            this.btnMainData = new Sunny.UI.UIImageButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.uiImageButtonMain = new Sunny.UI.UIImageButton();
            this.uiImageButtonAutoMain = new Sunny.UI.UIImageButton();
            this.btnHardwareTest = new Sunny.UI.UIImageButton();
            this.btnDuctHeating = new Sunny.UI.UIImageButton();
            this.tslblJYBC = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRunStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelContronl.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnChangePwd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMainData)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButtonMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButtonAutoMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHardwareTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDuctHeating)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.picRunStatus);
            this.panel1.Controls.Add(this.Logo);
            this.panel1.Controls.Add(this.lblDateTime);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1920, 41);
            this.panel1.TabIndex = 0;
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
            this.lblTitle.Text = "试验台名称";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseDown);
            // 
            // picRunStatus
            // 
            this.picRunStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picRunStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.picRunStatus.Image = ((System.Drawing.Image)(resources.GetObject("picRunStatus.Image")));
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
            this.Logo.Image = global::MainUI.Properties.Resources.复选框_无;
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
            this.timer1.Enabled = true;
            this.timer1.Interval = 800;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timerPLC
            // 
            this.timerPLC.Interval = 1000;
            this.timerPLC.Tick += new System.EventHandler(this.timerPLC_Tick);
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
            this.tslblElectrical,
            this.tslblWeight,
            this.tslblJYOK,
            this.tslblJYNG,
            this.tslblJYSim,
            this.tslblRYOK,
            this.tslblRYNG,
            this.tslblRYHY,
            this.tslblRYSim,
            this.tslblWaterOK,
            this.tslblWaterNG,
            this.tslblWaterSim,
            this.tslblTRDP,
            this.tslblJYBC,
            this.tslblCommunication,
            this.tslblVersion});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1050);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1920, 30);
            this.statusStrip1.TabIndex = 4;
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
            // tslblElectrical
            // 
            this.tslblElectrical.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblElectrical.Name = "tslblElectrical";
            this.tslblElectrical.Size = new System.Drawing.Size(110, 25);
            this.tslblElectrical.Text = "发动机电参数";
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
            // tslblRYHY
            // 
            this.tslblRYHY.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblRYHY.Name = "tslblRYHY";
            this.tslblRYHY.Size = new System.Drawing.Size(78, 25);
            this.tslblRYHY.Text = "燃油耗仪";
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
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 41);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelContronl);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.splitContainer1.Size = new System.Drawing.Size(1920, 1009);
            this.splitContainer1.SplitterDistance = 91;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 5;
            // 
            // panelContronl
            // 
            this.panelContronl.Controls.Add(this.flowLayoutPanel2);
            this.panelContronl.Controls.Add(this.flowLayoutPanel1);
            this.panelContronl.Location = new System.Drawing.Point(1, 3);
            this.panelContronl.Name = "panelContronl";
            this.panelContronl.Size = new System.Drawing.Size(87, 1002);
            this.panelContronl.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnExit);
            this.flowLayoutPanel2.Controls.Add(this.btnChangePwd);
            this.flowLayoutPanel2.Controls.Add(this.btnReports);
            this.flowLayoutPanel2.Controls.Add(this.btnMainData);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 617);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(86, 386);
            this.flowLayoutPanel2.TabIndex = 122;
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
            this.btnExit.Location = new System.Drawing.Point(3, 293);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 90);
            this.btnExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnExit.TabIndex = 115;
            this.btnExit.TabStop = false;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnChangePwd
            // 
            this.btnChangePwd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnChangePwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnChangePwd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangePwd.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnChangePwd.Image = ((System.Drawing.Image)(resources.GetObject("btnChangePwd.Image")));
            this.btnChangePwd.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnChangePwd.ImageHover")));
            this.btnChangePwd.ImageOffset = new System.Drawing.Point(12, 5);
            this.btnChangePwd.ImagePress = ((System.Drawing.Image)(resources.GetObject("btnChangePwd.ImagePress")));
            this.btnChangePwd.Location = new System.Drawing.Point(3, 197);
            this.btnChangePwd.Name = "btnChangePwd";
            this.btnChangePwd.Size = new System.Drawing.Size(80, 90);
            this.btnChangePwd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnChangePwd.TabIndex = 116;
            this.btnChangePwd.TabStop = false;
            this.btnChangePwd.Text = "修改密码";
            this.btnChangePwd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnChangePwd.Click += new System.EventHandler(this.btnChangePwd_Click);
            // 
            // btnReports
            // 
            this.btnReports.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnReports.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReports.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnReports.Image = ((System.Drawing.Image)(resources.GetObject("btnReports.Image")));
            this.btnReports.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnReports.ImageHover")));
            this.btnReports.ImageOffset = new System.Drawing.Point(12, 5);
            this.btnReports.ImagePress = ((System.Drawing.Image)(resources.GetObject("btnReports.ImagePress")));
            this.btnReports.Location = new System.Drawing.Point(3, 101);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(80, 90);
            this.btnReports.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnReports.TabIndex = 112;
            this.btnReports.TabStop = false;
            this.btnReports.Text = "试验数据";
            this.btnReports.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // btnMainData
            // 
            this.btnMainData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnMainData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMainData.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnMainData.Image = ((System.Drawing.Image)(resources.GetObject("btnMainData.Image")));
            this.btnMainData.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnMainData.ImageHover")));
            this.btnMainData.ImageOffset = new System.Drawing.Point(12, 5);
            this.btnMainData.ImagePress = ((System.Drawing.Image)(resources.GetObject("btnMainData.ImagePress")));
            this.btnMainData.Location = new System.Drawing.Point(3, 5);
            this.btnMainData.Name = "btnMainData";
            this.btnMainData.Size = new System.Drawing.Size(80, 90);
            this.btnMainData.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnMainData.TabIndex = 114;
            this.btnMainData.TabStop = false;
            this.btnMainData.Text = "参数管理";
            this.btnMainData.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMainData.Click += new System.EventHandler(this.btnMainData_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.uiImageButtonMain);
            this.flowLayoutPanel1.Controls.Add(this.uiImageButtonAutoMain);
            this.flowLayoutPanel1.Controls.Add(this.btnHardwareTest);
            this.flowLayoutPanel1.Controls.Add(this.btnDuctHeating);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(86, 386);
            this.flowLayoutPanel1.TabIndex = 121;
            // 
            // uiImageButtonMain
            // 
            this.uiImageButtonMain.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.uiImageButtonMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uiImageButtonMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiImageButtonMain.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.uiImageButtonMain.Image = ((System.Drawing.Image)(resources.GetObject("uiImageButtonMain.Image")));
            this.uiImageButtonMain.ImageHover = ((System.Drawing.Image)(resources.GetObject("uiImageButtonMain.ImageHover")));
            this.uiImageButtonMain.ImageOffset = new System.Drawing.Point(12, 5);
            this.uiImageButtonMain.ImagePress = ((System.Drawing.Image)(resources.GetObject("uiImageButtonMain.ImagePress")));
            this.uiImageButtonMain.Location = new System.Drawing.Point(3, 3);
            this.uiImageButtonMain.Name = "uiImageButtonMain";
            this.uiImageButtonMain.Size = new System.Drawing.Size(80, 90);
            this.uiImageButtonMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.uiImageButtonMain.TabIndex = 119;
            this.uiImageButtonMain.TabStop = false;
            this.uiImageButtonMain.Text = "手动控制";
            this.uiImageButtonMain.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.uiImageButtonMain.Click += new System.EventHandler(this.uiImageButtonMain_Click);
            // 
            // uiImageButtonAutoMain
            // 
            this.uiImageButtonAutoMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uiImageButtonAutoMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiImageButtonAutoMain.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.uiImageButtonAutoMain.Image = global::MainUI.Properties.Resources.Auto;
            this.uiImageButtonAutoMain.ImageHover = global::MainUI.Properties.Resources.AutoHover;
            this.uiImageButtonAutoMain.ImageOffset = new System.Drawing.Point(12, 5);
            this.uiImageButtonAutoMain.ImagePress = global::MainUI.Properties.Resources.Auto;
            this.uiImageButtonAutoMain.Location = new System.Drawing.Point(3, 99);
            this.uiImageButtonAutoMain.Name = "uiImageButtonAutoMain";
            this.uiImageButtonAutoMain.Size = new System.Drawing.Size(80, 90);
            this.uiImageButtonAutoMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.uiImageButtonAutoMain.TabIndex = 120;
            this.uiImageButtonAutoMain.TabStop = false;
            this.uiImageButtonAutoMain.Text = "自动试验";
            this.uiImageButtonAutoMain.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.uiImageButtonAutoMain.Click += new System.EventHandler(this.uiImageButtonAutoMain_Click);
            // 
            // btnHardwareTest
            // 
            this.btnHardwareTest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnHardwareTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHardwareTest.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnHardwareTest.Image = ((System.Drawing.Image)(resources.GetObject("btnHardwareTest.Image")));
            this.btnHardwareTest.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnHardwareTest.ImageHover")));
            this.btnHardwareTest.ImageOffset = new System.Drawing.Point(12, 5);
            this.btnHardwareTest.ImagePress = ((System.Drawing.Image)(resources.GetObject("btnHardwareTest.ImagePress")));
            this.btnHardwareTest.Location = new System.Drawing.Point(3, 195);
            this.btnHardwareTest.Name = "btnHardwareTest";
            this.btnHardwareTest.Size = new System.Drawing.Size(80, 90);
            this.btnHardwareTest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnHardwareTest.TabIndex = 113;
            this.btnHardwareTest.TabStop = false;
            this.btnHardwareTest.Text = "硬件校准";
            this.btnHardwareTest.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnHardwareTest.Click += new System.EventHandler(this.btnHardwareTest_Click);
            // 
            // btnDuctHeating
            // 
            this.btnDuctHeating.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnDuctHeating.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDuctHeating.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnDuctHeating.Image = global::MainUI.Properties.Resources.风道加热;
            this.btnDuctHeating.ImageHover = global::MainUI.Properties.Resources.风道加热按下;
            this.btnDuctHeating.ImageOffset = new System.Drawing.Point(12, 5);
            this.btnDuctHeating.ImagePress = global::MainUI.Properties.Resources.风道加热;
            this.btnDuctHeating.Location = new System.Drawing.Point(3, 291);
            this.btnDuctHeating.Name = "btnDuctHeating";
            this.btnDuctHeating.Size = new System.Drawing.Size(80, 90);
            this.btnDuctHeating.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnDuctHeating.TabIndex = 121;
            this.btnDuctHeating.TabStop = false;
            this.btnDuctHeating.Text = "进排气系统";
            this.btnDuctHeating.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDuctHeating.Click += new System.EventHandler(this.btnDuctHeating_Click);
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
            // frmMainMenu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmMainMenu_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMainMenu_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRunStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelContronl.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnChangePwd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMainData)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButtonMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButtonAutoMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHardwareTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDuctHeating)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picRunStatus;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timerPLC;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslblUser;
        private System.Windows.Forms.ToolStripStatusLabel tslblPLC;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Sunny.UI.UIImageButton btnReports;
        private Sunny.UI.UIImageButton btnExit;
        private Sunny.UI.UIImageButton btnMainData;
        private Sunny.UI.UIImageButton btnHardwareTest;
        private Sunny.UI.UIImageButton btnChangePwd;
        public System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.ToolStripStatusLabel tslblVersion;
        private System.Windows.Forms.ToolStripStatusLabel tslblStart;
        private System.Windows.Forms.ToolStripStatusLabel tslblElectrical;
        private System.Windows.Forms.ToolStripStatusLabel tslblJYOK;
        private System.Windows.Forms.ToolStripStatusLabel tslblJYNG;
        private System.Windows.Forms.ToolStripStatusLabel tslblJYSim;
        private System.Windows.Forms.ToolStripStatusLabel tslblRYOK;
        private System.Windows.Forms.ToolStripStatusLabel tslblRYNG;
        private System.Windows.Forms.ToolStripStatusLabel tslblWaterOK;
        private Sunny.UI.UIImageButton uiImageButtonMain;
        private System.Windows.Forms.ToolStripStatusLabel tslblCKPLC;
        private System.Windows.Forms.ToolStripStatusLabel tslblTRDP;
        private System.Windows.Forms.Label lblTitle;
        private Sunny.UI.UIImageButton uiImageButtonAutoMain;
        private System.Windows.Forms.Panel panelContronl;
        private System.Windows.Forms.ToolStripStatusLabel tslblCommunication;
        private System.Windows.Forms.ToolStripStatusLabel tslblRYSim;
        private System.Windows.Forms.ToolStripStatusLabel tslblWaterNG;
        private System.Windows.Forms.ToolStripStatusLabel tslblWaterSim;
        private System.Windows.Forms.ToolStripStatusLabel tslblRYHY;
        private System.Windows.Forms.ToolStripStatusLabel tslblWeight;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Sunny.UI.UIImageButton btnDuctHeating;
        private System.Windows.Forms.ToolStripStatusLabel tslblJYBC;
    }
}
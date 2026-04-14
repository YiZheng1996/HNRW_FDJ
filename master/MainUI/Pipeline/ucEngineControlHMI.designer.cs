
namespace MainUI
{
    partial class ucEngineControlHMI
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.uiLight120 = new Sunny.UI.UILight();
            this.label153 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.uiLight70 = new Sunny.UI.UILight();
            this.rButton38 = new RW.UI.Controls.RButton();
            this.label80 = new System.Windows.Forms.Label();
            this.uiLight72 = new Sunny.UI.UILight();
            this.rButton39 = new RW.UI.Controls.RButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelManual = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.uiLedBulb2 = new Sunny.UI.UILedBulb();
            this.rButton2 = new RW.UI.Controls.RButton();
            this.rButton5 = new RW.UI.Controls.RButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.uiLedBulb1 = new Sunny.UI.UILedBulb();
            this.rButton1 = new RW.UI.Controls.RButton();
            this.rButton3 = new RW.UI.Controls.RButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label182 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.uiButton5 = new Sunny.UI.UIButton();
            this.lblSZ = new System.Windows.Forms.Label();
            this.lblJQY = new System.Windows.Forms.Label();
            this.uiButton4 = new Sunny.UI.UIButton();
            this.uiButton1 = new Sunny.UI.UIButton();
            this.label8 = new System.Windows.Forms.Label();
            this.lblJQZ = new System.Windows.Forms.Label();
            this.lblPQZ = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.uiButton3 = new Sunny.UI.UIButton();
            this.label6 = new System.Windows.Forms.Label();
            this.uiButton2 = new Sunny.UI.UIButton();
            this.lblPQY = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.uiLedBulb6 = new Sunny.UI.UILedBulb();
            this.rButton50 = new RW.UI.Controls.RButton();
            this.rButton95 = new RW.UI.Controls.RButton();
            this.groupBox5.SuspendLayout();
            this.panelManual.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.uiLight120);
            this.groupBox5.Controls.Add(this.label153);
            this.groupBox5.Controls.Add(this.label77);
            this.groupBox5.Controls.Add(this.uiLight70);
            this.groupBox5.Controls.Add(this.rButton38);
            this.groupBox5.Controls.Add(this.label80);
            this.groupBox5.Controls.Add(this.uiLight72);
            this.groupBox5.Controls.Add(this.rButton39);
            this.groupBox5.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(397, 92);
            this.groupBox5.TabIndex = 538;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "水极板控制";
            // 
            // uiLight120
            // 
            this.uiLight120.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.uiLight120.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.uiLight120.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLight120.Location = new System.Drawing.Point(253, 0);
            this.uiLight120.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLight120.Name = "uiLight120";
            this.uiLight120.OffCenterColor = System.Drawing.Color.Silver;
            this.uiLight120.OffColor = System.Drawing.SystemColors.ControlText;
            this.uiLight120.OnCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.uiLight120.OnColor = System.Drawing.Color.Red;
            this.uiLight120.Radius = 22;
            this.uiLight120.Size = new System.Drawing.Size(23, 22);
            this.uiLight120.State = Sunny.UI.UILightState.Off;
            this.uiLight120.TabIndex = 752;
            this.uiLight120.Tag = "水阻升降电机过流";
            this.uiLight120.Text = "uiLight3";
            this.uiLight120.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // label153
            // 
            this.label153.AutoSize = true;
            this.label153.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label153.Location = new System.Drawing.Point(271, 2);
            this.label153.Name = "label153";
            this.label153.Size = new System.Drawing.Size(123, 19);
            this.label153.TabIndex = 753;
            this.label153.Text = "升降电机过流";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Font = new System.Drawing.Font("宋体", 14F);
            this.label77.Location = new System.Drawing.Point(190, 25);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(66, 19);
            this.label77.TabIndex = 664;
            this.label77.Text = "下限位";
            // 
            // uiLight70
            // 
            this.uiLight70.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.uiLight70.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.uiLight70.Font = new System.Drawing.Font("宋体", 14F);
            this.uiLight70.Location = new System.Drawing.Point(168, 23);
            this.uiLight70.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLight70.Name = "uiLight70";
            this.uiLight70.OffCenterColor = System.Drawing.Color.Silver;
            this.uiLight70.OffColor = System.Drawing.SystemColors.ControlText;
            this.uiLight70.OnCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.uiLight70.OnColor = System.Drawing.Color.Red;
            this.uiLight70.Radius = 22;
            this.uiLight70.Size = new System.Drawing.Size(23, 22);
            this.uiLight70.State = Sunny.UI.UILightState.Off;
            this.uiLight70.TabIndex = 662;
            this.uiLight70.Tag = "水阻升降下极限检测";
            this.uiLight70.Text = "uiLight3";
            this.uiLight70.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // rButton38
            // 
            this.rButton38.BackColor = System.Drawing.Color.Silver;
            this.rButton38.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.rButton38.FalseColor = System.Drawing.Color.Silver;
            this.rButton38.Font = new System.Drawing.Font("宋体", 14F);
            this.rButton38.Location = new System.Drawing.Point(169, 48);
            this.rButton38.Name = "rButton38";
            this.rButton38.Size = new System.Drawing.Size(119, 33);
            this.rButton38.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.rButton38.TabIndex = 661;
            this.rButton38.Tag = "水阻下降控制";
            this.rButton38.Text = "水极板降";
            this.rButton38.TrueColor = System.Drawing.Color.Lime;
            this.rButton38.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnWaterPlateDown_MouseDown);
            this.rButton38.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnWaterPlateDown_MouseUp);
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Font = new System.Drawing.Font("宋体", 14F);
            this.label80.Location = new System.Drawing.Point(36, 25);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(66, 19);
            this.label80.TabIndex = 660;
            this.label80.Text = "上限位";
            // 
            // uiLight72
            // 
            this.uiLight72.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.uiLight72.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.uiLight72.Font = new System.Drawing.Font("宋体", 14F);
            this.uiLight72.Location = new System.Drawing.Point(14, 23);
            this.uiLight72.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLight72.Name = "uiLight72";
            this.uiLight72.OffCenterColor = System.Drawing.Color.Silver;
            this.uiLight72.OffColor = System.Drawing.SystemColors.ControlText;
            this.uiLight72.OnCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.uiLight72.OnColor = System.Drawing.Color.Red;
            this.uiLight72.Radius = 22;
            this.uiLight72.Size = new System.Drawing.Size(23, 22);
            this.uiLight72.State = Sunny.UI.UILightState.Off;
            this.uiLight72.TabIndex = 658;
            this.uiLight72.Tag = "水阻升降上极限检测";
            this.uiLight72.Text = "uiLight3";
            this.uiLight72.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // rButton39
            // 
            this.rButton39.BackColor = System.Drawing.Color.Silver;
            this.rButton39.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.rButton39.FalseColor = System.Drawing.Color.Silver;
            this.rButton39.Font = new System.Drawing.Font("宋体", 14F);
            this.rButton39.Location = new System.Drawing.Point(13, 48);
            this.rButton39.Name = "rButton39";
            this.rButton39.Size = new System.Drawing.Size(119, 33);
            this.rButton39.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.rButton39.TabIndex = 657;
            this.rButton39.Tag = "水阻上升控制";
            this.rButton39.Text = "水极板升";
            this.rButton39.TrueColor = System.Drawing.Color.Lime;
            this.rButton39.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnWaterPlateUp_MouseDown);
            this.rButton39.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnWaterPlateUp_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panelManual
            // 
            this.panelManual.Controls.Add(this.groupBox6);
            this.panelManual.Controls.Add(this.groupBox4);
            this.panelManual.Controls.Add(this.groupBox2);
            this.panelManual.Controls.Add(this.groupBox1);
            this.panelManual.Controls.Add(this.groupBox5);
            this.panelManual.Location = new System.Drawing.Point(3, 3);
            this.panelManual.Name = "panelManual";
            this.panelManual.Size = new System.Drawing.Size(418, 994);
            this.panelManual.TabIndex = 665;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.uiLedBulb2);
            this.groupBox6.Controls.Add(this.rButton2);
            this.groupBox6.Controls.Add(this.rButton5);
            this.groupBox6.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox6.Location = new System.Drawing.Point(4, 255);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(209, 71);
            this.groupBox6.TabIndex = 766;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "抽油泵控制";
            // 
            // uiLedBulb2
            // 
            this.uiLedBulb2.Blink = true;
            this.uiLedBulb2.Color = System.Drawing.Color.Red;
            this.uiLedBulb2.Location = new System.Drawing.Point(178, 36);
            this.uiLedBulb2.Name = "uiLedBulb2";
            this.uiLedBulb2.On = false;
            this.uiLedBulb2.Size = new System.Drawing.Size(20, 20);
            this.uiLedBulb2.TabIndex = 759;
            this.uiLedBulb2.Tag = "抽油泵过流";
            this.uiLedBulb2.Text = "uiLedBulb2";
            this.uiLedBulb2.Visible = false;
            this.uiLedBulb2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // rButton2
            // 
            this.rButton2.BackColor = System.Drawing.Color.Silver;
            this.rButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.rButton2.FalseColor = System.Drawing.Color.Silver;
            this.rButton2.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rButton2.InputDriverName = "";
            this.rButton2.InputTagName = "";
            this.rButton2.Location = new System.Drawing.Point(14, 30);
            this.rButton2.Name = "rButton2";
            this.rButton2.OutputTagName = "抽油泵合闸控制";
            this.rButton2.Size = new System.Drawing.Size(75, 33);
            this.rButton2.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.rButton2.TabIndex = 759;
            this.rButton2.Tag = "1";
            this.rButton2.Text = "开";
            this.rButton2.TrueColor = System.Drawing.Color.Lime;
            // 
            // rButton5
            // 
            this.rButton5.BackColor = System.Drawing.Color.Silver;
            this.rButton5.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.rButton5.FalseColor = System.Drawing.Color.Silver;
            this.rButton5.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rButton5.InputTagName = "";
            this.rButton5.Location = new System.Drawing.Point(97, 30);
            this.rButton5.Name = "rButton5";
            this.rButton5.OutputTagName = "抽油泵合闸控制";
            this.rButton5.Size = new System.Drawing.Size(75, 33);
            this.rButton5.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.rButton5.TabIndex = 760;
            this.rButton5.Tag = "0";
            this.rButton5.Text = "关";
            this.rButton5.TrueColor = System.Drawing.Color.Lime;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.uiLedBulb1);
            this.groupBox4.Controls.Add(this.rButton1);
            this.groupBox4.Controls.Add(this.rButton3);
            this.groupBox4.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox4.Location = new System.Drawing.Point(4, 178);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(209, 71);
            this.groupBox4.TabIndex = 765;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "主发通风机2控制";
            // 
            // uiLedBulb1
            // 
            this.uiLedBulb1.Blink = true;
            this.uiLedBulb1.Color = System.Drawing.Color.Red;
            this.uiLedBulb1.Location = new System.Drawing.Point(178, 36);
            this.uiLedBulb1.Name = "uiLedBulb1";
            this.uiLedBulb1.On = false;
            this.uiLedBulb1.Size = new System.Drawing.Size(20, 20);
            this.uiLedBulb1.TabIndex = 759;
            this.uiLedBulb1.Tag = "主发通风机2过流";
            this.uiLedBulb1.Text = "uiLedBulb1";
            this.uiLedBulb1.Visible = false;
            this.uiLedBulb1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // rButton1
            // 
            this.rButton1.BackColor = System.Drawing.Color.Silver;
            this.rButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.rButton1.FalseColor = System.Drawing.Color.Silver;
            this.rButton1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rButton1.InputDriverName = "";
            this.rButton1.InputTagName = "";
            this.rButton1.Location = new System.Drawing.Point(14, 30);
            this.rButton1.Name = "rButton1";
            this.rButton1.OutputTagName = "主发通风机2合闸控制";
            this.rButton1.Size = new System.Drawing.Size(75, 33);
            this.rButton1.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.rButton1.TabIndex = 759;
            this.rButton1.Tag = "1";
            this.rButton1.Text = "开";
            this.rButton1.TrueColor = System.Drawing.Color.Lime;
            // 
            // rButton3
            // 
            this.rButton3.BackColor = System.Drawing.Color.Silver;
            this.rButton3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.rButton3.FalseColor = System.Drawing.Color.Silver;
            this.rButton3.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rButton3.InputTagName = "";
            this.rButton3.Location = new System.Drawing.Point(97, 30);
            this.rButton3.Name = "rButton3";
            this.rButton3.OutputTagName = "主发通风机2合闸控制";
            this.rButton3.Size = new System.Drawing.Size(75, 33);
            this.rButton3.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.rButton3.TabIndex = 760;
            this.rButton3.Tag = "0";
            this.rButton3.Text = "关";
            this.rButton3.TrueColor = System.Drawing.Color.Lime;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label182);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.uiButton5);
            this.groupBox2.Controls.Add(this.lblSZ);
            this.groupBox2.Controls.Add(this.lblJQY);
            this.groupBox2.Controls.Add(this.uiButton4);
            this.groupBox2.Controls.Add(this.uiButton1);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.lblJQZ);
            this.groupBox2.Controls.Add(this.lblPQZ);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.uiButton3);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.uiButton2);
            this.groupBox2.Controls.Add(this.lblPQY);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox2.Location = new System.Drawing.Point(3, 392);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(397, 253);
            this.groupBox2.TabIndex = 702;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "流量控制";
            // 
            // label182
            // 
            this.label182.AutoSize = true;
            this.label182.Font = new System.Drawing.Font("宋体", 14F);
            this.label182.Location = new System.Drawing.Point(18, 34);
            this.label182.Name = "label182";
            this.label182.Size = new System.Drawing.Size(104, 19);
            this.label182.TabIndex = 689;
            this.label182.Text = "进气风道右";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 14F);
            this.label10.Location = new System.Drawing.Point(18, 209);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 19);
            this.label10.TabIndex = 701;
            this.label10.Text = "水阻箱进水";
            // 
            // uiButton5
            // 
            this.uiButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton5.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.uiButton5.Location = new System.Drawing.Point(241, 28);
            this.uiButton5.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton5.Name = "uiButton5";
            this.uiButton5.Size = new System.Drawing.Size(86, 30);
            this.uiButton5.StyleCustomMode = true;
            this.uiButton5.TabIndex = 687;
            this.uiButton5.Tag = "进气风道右调节阀控制";
            this.uiButton5.Text = "流量给定";
            this.uiButton5.TipsFont = new System.Drawing.Font("微软雅黑", 13F);
            this.uiButton5.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiButton5.Click += new System.EventHandler(this.SetFlow_Click);
            // 
            // lblSZ
            // 
            this.lblSZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblSZ.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSZ.ForeColor = System.Drawing.Color.Black;
            this.lblSZ.Location = new System.Drawing.Point(141, 204);
            this.lblSZ.Name = "lblSZ";
            this.lblSZ.Size = new System.Drawing.Size(75, 28);
            this.lblSZ.TabIndex = 700;
            this.lblSZ.Tag = "水阻箱进水电动调节阀";
            this.lblSZ.Text = "0.0";
            this.lblSZ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblJQY
            // 
            this.lblJQY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblJQY.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblJQY.ForeColor = System.Drawing.Color.Black;
            this.lblJQY.Location = new System.Drawing.Point(141, 29);
            this.lblJQY.Name = "lblJQY";
            this.lblJQY.Size = new System.Drawing.Size(75, 28);
            this.lblJQY.TabIndex = 688;
            this.lblJQY.Tag = "进气风道右调节阀控制";
            this.lblJQY.Text = "0.0";
            this.lblJQY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiButton4
            // 
            this.uiButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton4.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.uiButton4.Location = new System.Drawing.Point(241, 203);
            this.uiButton4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton4.Name = "uiButton4";
            this.uiButton4.Size = new System.Drawing.Size(86, 30);
            this.uiButton4.StyleCustomMode = true;
            this.uiButton4.TabIndex = 699;
            this.uiButton4.Tag = "水阻箱进水电动调节阀";
            this.uiButton4.Text = "流量给定";
            this.uiButton4.TipsFont = new System.Drawing.Font("微软雅黑", 13F);
            this.uiButton4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiButton4.Click += new System.EventHandler(this.SetFlow_Click);
            // 
            // uiButton1
            // 
            this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton1.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.uiButton1.Location = new System.Drawing.Point(241, 74);
            this.uiButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(86, 30);
            this.uiButton1.StyleCustomMode = true;
            this.uiButton1.TabIndex = 690;
            this.uiButton1.Tag = "进气风道左调节阀控制";
            this.uiButton1.Text = "流量给定";
            this.uiButton1.TipsFont = new System.Drawing.Font("微软雅黑", 13F);
            this.uiButton1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiButton1.Click += new System.EventHandler(this.SetFlow_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 14F);
            this.label8.Location = new System.Drawing.Point(18, 164);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 19);
            this.label8.TabIndex = 698;
            this.label8.Text = "排气风道左";
            // 
            // lblJQZ
            // 
            this.lblJQZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblJQZ.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblJQZ.ForeColor = System.Drawing.Color.Black;
            this.lblJQZ.Location = new System.Drawing.Point(141, 75);
            this.lblJQZ.Name = "lblJQZ";
            this.lblJQZ.Size = new System.Drawing.Size(75, 28);
            this.lblJQZ.TabIndex = 691;
            this.lblJQZ.Tag = "进气风道左调节阀控制";
            this.lblJQZ.Text = "0.0";
            this.lblJQZ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPQZ
            // 
            this.lblPQZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblPQZ.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPQZ.ForeColor = System.Drawing.Color.Black;
            this.lblPQZ.Location = new System.Drawing.Point(141, 159);
            this.lblPQZ.Name = "lblPQZ";
            this.lblPQZ.Size = new System.Drawing.Size(75, 28);
            this.lblPQZ.TabIndex = 697;
            this.lblPQZ.Tag = "排气风道左调节阀控制";
            this.lblPQZ.Text = "0.0";
            this.lblPQZ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 14F);
            this.label4.Location = new System.Drawing.Point(18, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 19);
            this.label4.TabIndex = 692;
            this.label4.Text = "进气风道左";
            // 
            // uiButton3
            // 
            this.uiButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton3.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.uiButton3.Location = new System.Drawing.Point(241, 158);
            this.uiButton3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton3.Name = "uiButton3";
            this.uiButton3.Size = new System.Drawing.Size(86, 30);
            this.uiButton3.StyleCustomMode = true;
            this.uiButton3.TabIndex = 696;
            this.uiButton3.Tag = "排气风道左调节阀控制";
            this.uiButton3.Text = "流量给定";
            this.uiButton3.TipsFont = new System.Drawing.Font("微软雅黑", 13F);
            this.uiButton3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiButton3.Click += new System.EventHandler(this.SetFlow_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 14F);
            this.label6.Location = new System.Drawing.Point(18, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 19);
            this.label6.TabIndex = 695;
            this.label6.Text = "排气风道右";
            // 
            // uiButton2
            // 
            this.uiButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton2.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.uiButton2.Location = new System.Drawing.Point(241, 116);
            this.uiButton2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton2.Name = "uiButton2";
            this.uiButton2.Size = new System.Drawing.Size(86, 30);
            this.uiButton2.StyleCustomMode = true;
            this.uiButton2.TabIndex = 693;
            this.uiButton2.Tag = "排气风道右调节阀控制";
            this.uiButton2.Text = "流量给定";
            this.uiButton2.TipsFont = new System.Drawing.Font("微软雅黑", 13F);
            this.uiButton2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiButton2.Click += new System.EventHandler(this.SetFlow_Click);
            // 
            // lblPQY
            // 
            this.lblPQY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblPQY.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPQY.ForeColor = System.Drawing.Color.Black;
            this.lblPQY.Location = new System.Drawing.Point(141, 117);
            this.lblPQY.Name = "lblPQY";
            this.lblPQY.Size = new System.Drawing.Size(75, 28);
            this.lblPQY.TabIndex = 694;
            this.lblPQY.Tag = "排气风道右调节阀控制";
            this.lblPQY.Text = "0.0";
            this.lblPQY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.uiLedBulb6);
            this.groupBox1.Controls.Add(this.rButton50);
            this.groupBox1.Controls.Add(this.rButton95);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox1.Location = new System.Drawing.Point(7, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 71);
            this.groupBox1.TabIndex = 539;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "主发通风机1控制";
            // 
            // uiLedBulb6
            // 
            this.uiLedBulb6.Blink = true;
            this.uiLedBulb6.Color = System.Drawing.Color.Red;
            this.uiLedBulb6.Location = new System.Drawing.Point(178, 36);
            this.uiLedBulb6.Name = "uiLedBulb6";
            this.uiLedBulb6.Size = new System.Drawing.Size(20, 20);
            this.uiLedBulb6.TabIndex = 759;
            this.uiLedBulb6.Tag = "主发通风机1过流";
            this.uiLedBulb6.Text = "uiLedBulb6";
            this.uiLedBulb6.Visible = false;
            this.uiLedBulb6.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // rButton50
            // 
            this.rButton50.BackColor = System.Drawing.Color.Silver;
            this.rButton50.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.rButton50.FalseColor = System.Drawing.Color.Silver;
            this.rButton50.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rButton50.InputDriverName = "";
            this.rButton50.InputTagName = "";
            this.rButton50.Location = new System.Drawing.Point(14, 30);
            this.rButton50.Name = "rButton50";
            this.rButton50.OutputTagName = "主发通风机1合闸控制";
            this.rButton50.Size = new System.Drawing.Size(75, 33);
            this.rButton50.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.rButton50.TabIndex = 759;
            this.rButton50.Tag = "1";
            this.rButton50.Text = "开";
            this.rButton50.TrueColor = System.Drawing.Color.Lime;
            // 
            // rButton95
            // 
            this.rButton95.BackColor = System.Drawing.Color.Silver;
            this.rButton95.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.rButton95.FalseColor = System.Drawing.Color.Silver;
            this.rButton95.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rButton95.InputTagName = "";
            this.rButton95.Location = new System.Drawing.Point(97, 30);
            this.rButton95.Name = "rButton95";
            this.rButton95.OutputTagName = "主发通风机1合闸控制";
            this.rButton95.Size = new System.Drawing.Size(75, 33);
            this.rButton95.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.rButton95.TabIndex = 760;
            this.rButton95.Tag = "0";
            this.rButton95.Text = "关";
            this.rButton95.TrueColor = System.Drawing.Color.Lime;
            // 
            // ucEngineControlHMI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.panelManual);
            this.Name = "ucEngineControlHMI";
            this.Size = new System.Drawing.Size(1828, 997);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panelManual.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label77;
        private Sunny.UI.UILight uiLight70;
        private RW.UI.Controls.RButton rButton38;
        private System.Windows.Forms.Label label80;
        private Sunny.UI.UILight uiLight72;
        private RW.UI.Controls.RButton rButton39;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panelManual;
        private System.Windows.Forms.GroupBox groupBox1;
        private Sunny.UI.UILight uiLight120;
        private System.Windows.Forms.Label label153;
        private System.Windows.Forms.Label label182;
        private System.Windows.Forms.Label lblJQY;
        private Sunny.UI.UIButton uiButton5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblJQZ;
        private Sunny.UI.UIButton uiButton1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPQY;
        private Sunny.UI.UIButton uiButton2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPQZ;
        private Sunny.UI.UIButton uiButton3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblSZ;
        private Sunny.UI.UIButton uiButton4;
        private System.Windows.Forms.GroupBox groupBox2;
        private RW.UI.Controls.RButton rButton50;
        private RW.UI.Controls.RButton rButton95;
        private Sunny.UI.UILedBulb uiLedBulb6;
        private System.Windows.Forms.GroupBox groupBox4;
        private Sunny.UI.UILedBulb uiLedBulb1;
        private RW.UI.Controls.RButton rButton1;
        private RW.UI.Controls.RButton rButton3;
        private System.Windows.Forms.GroupBox groupBox6;
        private Sunny.UI.UILedBulb uiLedBulb2;
        private RW.UI.Controls.RButton rButton2;
        private RW.UI.Controls.RButton rButton5;
    }
}

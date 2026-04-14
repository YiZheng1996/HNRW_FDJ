
namespace MainUI.Widget
{
    partial class FrmScarmOpen
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
            this.btnClose = new Sunny.UI.UIButton();
            this.timerFast = new System.Windows.Forms.Timer(this.components);
            this.timerSlow = new System.Windows.Forms.Timer(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn24Close = new RW.UI.Controls.RButton();
            this.btn24Open = new RW.UI.Controls.RButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFuelCycleClose = new RW.UI.Controls.RButton();
            this.btnFuelCycleOpen = new RW.UI.Controls.RButton();
            this.groupBox48 = new System.Windows.Forms.GroupBox();
            this.btnY181Close = new RW.UI.Controls.RButton();
            this.btnY181Open = new RW.UI.Controls.RButton();
            this.groupBox49 = new System.Windows.Forms.GroupBox();
            this.btnY182Close = new RW.UI.Controls.RButton();
            this.btnY182Open = new RW.UI.Controls.RButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnEoClose = new RW.UI.Controls.RButton();
            this.btnEoOpen = new RW.UI.Controls.RButton();
            this.nudEoTemp = new Sunny.UI.UIDoubleUpDown();
            this.rButton3 = new RW.UI.Controls.RButton();
            this.label3 = new System.Windows.Forms.Label();
            this.lblEoTemp = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.LCCurrentValue = new System.Windows.Forms.Label();
            this.LCVoltageValue = new System.Windows.Forms.Label();
            this.lblEoSetTemp = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox48.SuspendLayout();
            this.groupBox49.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnClose.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnClose.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnClose.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClose.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Location = new System.Drawing.Point(20, 440);
            this.btnClose.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnClose.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnClose.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClose.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClose.Size = new System.Drawing.Size(1037, 49);
            this.btnClose.Style = Sunny.UI.UIStyle.Red;
            this.btnClose.TabIndex = 733;
            this.btnClose.Text = "退出";
            this.btnClose.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // timerFast
            // 
            this.timerFast.Interval = 500;
            this.timerFast.Tick += new System.EventHandler(this.timerFast_Tick);
            // 
            // timerSlow
            // 
            this.timerSlow.Interval = 1000;
            this.timerSlow.Tick += new System.EventHandler(this.timerSlow_Tick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 18F);
            this.label10.Location = new System.Drawing.Point(24, 333);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(202, 24);
            this.label10.TabIndex = 811;
            this.label10.Text = "机油温度调节(℃)";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(20, 422);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1037, 1);
            this.label11.TabIndex = 817;
            this.label11.Text = "label11";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn24Close);
            this.groupBox3.Controls.Add(this.btn24Open);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 16F);
            this.groupBox3.Location = new System.Drawing.Point(516, 103);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(332, 91);
            this.groupBox3.TabIndex = 831;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "发动机控制盒电源DC24V控制";
            // 
            // btn24Close
            // 
            this.btn24Close.BackColor = System.Drawing.Color.Silver;
            this.btn24Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn24Close.FalseColor = System.Drawing.Color.Silver;
            this.btn24Close.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn24Close.Location = new System.Drawing.Point(158, 43);
            this.btn24Close.Name = "btn24Close";
            this.btn24Close.OutputTagName = "发动机DC24V供电";
            this.btn24Close.Size = new System.Drawing.Size(85, 33);
            this.btn24Close.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btn24Close.TabIndex = 828;
            this.btn24Close.Tag = "0";
            this.btn24Close.Text = "关";
            this.btn24Close.TrueColor = System.Drawing.Color.Lime;
            this.btn24Close.Click += new System.EventHandler(this.sw_Valve_Click);
            // 
            // btn24Open
            // 
            this.btn24Open.BackColor = System.Drawing.Color.Silver;
            this.btn24Open.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn24Open.FalseColor = System.Drawing.Color.Silver;
            this.btn24Open.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn24Open.Location = new System.Drawing.Point(57, 43);
            this.btn24Open.Name = "btn24Open";
            this.btn24Open.OutputTagName = "发动机DC24V供电";
            this.btn24Open.Size = new System.Drawing.Size(85, 33);
            this.btn24Open.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btn24Open.TabIndex = 828;
            this.btn24Open.Tag = "1";
            this.btn24Open.Text = "开";
            this.btn24Open.TrueColor = System.Drawing.Color.Lime;
            this.btn24Open.Click += new System.EventHandler(this.sw_Valve_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFuelCycleClose);
            this.groupBox1.Controls.Add(this.btnFuelCycleOpen);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 16F);
            this.groupBox1.Location = new System.Drawing.Point(20, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 91);
            this.groupBox1.TabIndex = 835;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "燃油循环";
            // 
            // btnFuelCycleClose
            // 
            this.btnFuelCycleClose.BackColor = System.Drawing.Color.Silver;
            this.btnFuelCycleClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFuelCycleClose.FalseColor = System.Drawing.Color.Silver;
            this.btnFuelCycleClose.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFuelCycleClose.Location = new System.Drawing.Point(120, 43);
            this.btnFuelCycleClose.Name = "btnFuelCycleClose";
            this.btnFuelCycleClose.OutputTagName = "燃油循环";
            this.btnFuelCycleClose.Size = new System.Drawing.Size(85, 33);
            this.btnFuelCycleClose.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnFuelCycleClose.TabIndex = 828;
            this.btnFuelCycleClose.Tag = "0";
            this.btnFuelCycleClose.Text = "停止";
            this.btnFuelCycleClose.TrueColor = System.Drawing.Color.Lime;
            this.btnFuelCycleClose.Click += new System.EventHandler(this.sw_Valve_Click);
            // 
            // btnFuelCycleOpen
            // 
            this.btnFuelCycleOpen.BackColor = System.Drawing.Color.Silver;
            this.btnFuelCycleOpen.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFuelCycleOpen.FalseColor = System.Drawing.Color.Silver;
            this.btnFuelCycleOpen.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFuelCycleOpen.Location = new System.Drawing.Point(18, 43);
            this.btnFuelCycleOpen.Name = "btnFuelCycleOpen";
            this.btnFuelCycleOpen.OutputTagName = "燃油循环";
            this.btnFuelCycleOpen.Size = new System.Drawing.Size(85, 33);
            this.btnFuelCycleOpen.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnFuelCycleOpen.TabIndex = 828;
            this.btnFuelCycleOpen.Tag = "1";
            this.btnFuelCycleOpen.Text = "开始";
            this.btnFuelCycleOpen.TrueColor = System.Drawing.Color.Lime;
            this.btnFuelCycleOpen.Click += new System.EventHandler(this.sw_Valve_Click);
            // 
            // groupBox48
            // 
            this.groupBox48.Controls.Add(this.btnY181Close);
            this.groupBox48.Controls.Add(this.btnY181Open);
            this.groupBox48.Font = new System.Drawing.Font("宋体", 16F);
            this.groupBox48.Location = new System.Drawing.Point(22, 215);
            this.groupBox48.Name = "groupBox48";
            this.groupBox48.Size = new System.Drawing.Size(220, 91);
            this.groupBox48.TabIndex = 836;
            this.groupBox48.TabStop = false;
            this.groupBox48.Text = "181-停车装置1阀";
            // 
            // btnY181Close
            // 
            this.btnY181Close.BackColor = System.Drawing.Color.Silver;
            this.btnY181Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnY181Close.FalseColor = System.Drawing.Color.Silver;
            this.btnY181Close.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnY181Close.InputTagName = "紧急停车1关到位-181";
            this.btnY181Close.Location = new System.Drawing.Point(117, 45);
            this.btnY181Close.Name = "btnY181Close";
            this.btnY181Close.OutputTagName = "Y181阀控制";
            this.btnY181Close.Size = new System.Drawing.Size(85, 33);
            this.btnY181Close.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnY181Close.TabIndex = 754;
            this.btnY181Close.Tag = "0";
            this.btnY181Close.Text = "关";
            this.btnY181Close.TrueColor = System.Drawing.Color.Lime;
            this.btnY181Close.Click += new System.EventHandler(this.sw_Valve_Click);
            // 
            // btnY181Open
            // 
            this.btnY181Open.BackColor = System.Drawing.Color.Silver;
            this.btnY181Open.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnY181Open.FalseColor = System.Drawing.Color.Silver;
            this.btnY181Open.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnY181Open.InputDriverName = "";
            this.btnY181Open.InputTagName = "紧急停车1开到位-181";
            this.btnY181Open.Location = new System.Drawing.Point(16, 45);
            this.btnY181Open.Name = "btnY181Open";
            this.btnY181Open.OutputTagName = "Y181阀控制";
            this.btnY181Open.Size = new System.Drawing.Size(85, 33);
            this.btnY181Open.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnY181Open.TabIndex = 753;
            this.btnY181Open.Tag = "1";
            this.btnY181Open.Text = "开";
            this.btnY181Open.TrueColor = System.Drawing.Color.Lime;
            this.btnY181Open.Click += new System.EventHandler(this.sw_Valve_Click);
            // 
            // groupBox49
            // 
            this.groupBox49.Controls.Add(this.btnY182Close);
            this.groupBox49.Controls.Add(this.btnY182Open);
            this.groupBox49.Font = new System.Drawing.Font("宋体", 16F);
            this.groupBox49.Location = new System.Drawing.Point(272, 215);
            this.groupBox49.Name = "groupBox49";
            this.groupBox49.Size = new System.Drawing.Size(220, 91);
            this.groupBox49.TabIndex = 837;
            this.groupBox49.TabStop = false;
            this.groupBox49.Text = "182-停车装置2阀";
            // 
            // btnY182Close
            // 
            this.btnY182Close.BackColor = System.Drawing.Color.Silver;
            this.btnY182Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnY182Close.FalseColor = System.Drawing.Color.Silver;
            this.btnY182Close.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnY182Close.InputTagName = "紧急停车2关到位-182";
            this.btnY182Close.Location = new System.Drawing.Point(120, 45);
            this.btnY182Close.Name = "btnY182Close";
            this.btnY182Close.OutputTagName = "Y182阀控制";
            this.btnY182Close.Size = new System.Drawing.Size(85, 33);
            this.btnY182Close.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnY182Close.TabIndex = 754;
            this.btnY182Close.Tag = "0";
            this.btnY182Close.Text = "关";
            this.btnY182Close.TrueColor = System.Drawing.Color.Lime;
            this.btnY182Close.Click += new System.EventHandler(this.sw_Valve_Click);
            // 
            // btnY182Open
            // 
            this.btnY182Open.BackColor = System.Drawing.Color.Silver;
            this.btnY182Open.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnY182Open.FalseColor = System.Drawing.Color.Silver;
            this.btnY182Open.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnY182Open.InputDriverName = "";
            this.btnY182Open.InputTagName = "紧急停车2开到位-182";
            this.btnY182Open.Location = new System.Drawing.Point(19, 45);
            this.btnY182Open.Name = "btnY182Open";
            this.btnY182Open.OutputTagName = "Y182阀控制";
            this.btnY182Open.Size = new System.Drawing.Size(85, 33);
            this.btnY182Open.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnY182Open.TabIndex = 753;
            this.btnY182Open.Tag = "1";
            this.btnY182Open.Text = "开";
            this.btnY182Open.TrueColor = System.Drawing.Color.Lime;
            this.btnY182Open.Click += new System.EventHandler(this.sw_Valve_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnEoClose);
            this.groupBox2.Controls.Add(this.btnEoOpen);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 16F);
            this.groupBox2.Location = new System.Drawing.Point(272, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 91);
            this.groupBox2.TabIndex = 838;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "预供机油循环";
            // 
            // btnEoClose
            // 
            this.btnEoClose.BackColor = System.Drawing.Color.Silver;
            this.btnEoClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnEoClose.FalseColor = System.Drawing.Color.Silver;
            this.btnEoClose.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEoClose.Location = new System.Drawing.Point(120, 43);
            this.btnEoClose.Name = "btnEoClose";
            this.btnEoClose.OutputTagName = "预供机油循环";
            this.btnEoClose.Size = new System.Drawing.Size(85, 33);
            this.btnEoClose.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnEoClose.TabIndex = 828;
            this.btnEoClose.Tag = "0";
            this.btnEoClose.Text = "停止";
            this.btnEoClose.TrueColor = System.Drawing.Color.Lime;
            this.btnEoClose.Click += new System.EventHandler(this.sw_Valve_Click);
            // 
            // btnEoOpen
            // 
            this.btnEoOpen.BackColor = System.Drawing.Color.Silver;
            this.btnEoOpen.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnEoOpen.FalseColor = System.Drawing.Color.Silver;
            this.btnEoOpen.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEoOpen.Location = new System.Drawing.Point(19, 43);
            this.btnEoOpen.Name = "btnEoOpen";
            this.btnEoOpen.OutputTagName = "预供机油循环";
            this.btnEoOpen.Size = new System.Drawing.Size(85, 33);
            this.btnEoOpen.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnEoOpen.TabIndex = 828;
            this.btnEoOpen.Tag = "1";
            this.btnEoOpen.Text = "开始";
            this.btnEoOpen.TrueColor = System.Drawing.Color.Lime;
            this.btnEoOpen.Click += new System.EventHandler(this.sw_Valve_Click);
            // 
            // nudEoTemp
            // 
            this.nudEoTemp.DecimalPlaces = 0;
            this.nudEoTemp.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudEoTemp.Location = new System.Drawing.Point(26, 368);
            this.nudEoTemp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudEoTemp.Maximum = 150D;
            this.nudEoTemp.Minimum = 150D;
            this.nudEoTemp.MinimumSize = new System.Drawing.Size(100, 0);
            this.nudEoTemp.Name = "nudEoTemp";
            this.nudEoTemp.ShowText = false;
            this.nudEoTemp.Size = new System.Drawing.Size(105, 29);
            this.nudEoTemp.Step = 1D;
            this.nudEoTemp.TabIndex = 833;
            this.nudEoTemp.Text = "uiDoubleUpDown2";
            this.nudEoTemp.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.nudEoTemp.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // rButton3
            // 
            this.rButton3.BackColor = System.Drawing.Color.Silver;
            this.rButton3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.rButton3.FalseColor = System.Drawing.Color.Silver;
            this.rButton3.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rButton3.Location = new System.Drawing.Point(138, 366);
            this.rButton3.Name = "rButton3";
            this.rButton3.Size = new System.Drawing.Size(85, 33);
            this.rButton3.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.rButton3.TabIndex = 834;
            this.rButton3.Tag = "";
            this.rButton3.Text = "设置";
            this.rButton3.TrueColor = System.Drawing.Color.Lime;
            this.rButton3.Click += new System.EventHandler(this.rButton3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 16F);
            this.label3.Location = new System.Drawing.Point(380, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 22);
            this.label3.TabIndex = 811;
            this.label3.Text = "机油进口油温";
            // 
            // lblEoTemp
            // 
            this.lblEoTemp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEoTemp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEoTemp.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEoTemp.ForeColor = System.Drawing.Color.Black;
            this.lblEoTemp.Location = new System.Drawing.Point(524, 47);
            this.lblEoTemp.Name = "lblEoTemp";
            this.lblEoTemp.Size = new System.Drawing.Size(91, 28);
            this.lblEoTemp.TabIndex = 839;
            this.lblEoTemp.Tag = "T21主油道进口油温";
            this.lblEoTemp.Text = "0.0";
            this.lblEoTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(20, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1037, 1);
            this.label5.TabIndex = 840;
            this.label5.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 16F);
            this.label4.Location = new System.Drawing.Point(624, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 22);
            this.label4.TabIndex = 832;
            this.label4.Text = "℃";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 16F);
            this.label1.Location = new System.Drawing.Point(284, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 22);
            this.label1.TabIndex = 859;
            this.label1.Text = "rpm";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 16F);
            this.label17.Location = new System.Drawing.Point(29, 12);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(120, 22);
            this.label17.TabIndex = 860;
            this.label17.Text = "发动机转速";
            // 
            // lblSpeed
            // 
            this.lblSpeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblSpeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSpeed.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSpeed.ForeColor = System.Drawing.Color.Black;
            this.lblSpeed.Location = new System.Drawing.Point(185, 9);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(91, 28);
            this.lblSpeed.TabIndex = 858;
            this.lblSpeed.Tag = "";
            this.lblSpeed.Text = "0.0";
            this.lblSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 16F);
            this.label8.Location = new System.Drawing.Point(944, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 22);
            this.label8.TabIndex = 856;
            this.label8.Text = "A";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 16F);
            this.label14.Location = new System.Drawing.Point(739, 12);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(98, 22);
            this.label14.TabIndex = 857;
            this.label14.Text = "励磁电流";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 16F);
            this.label15.Location = new System.Drawing.Point(623, 12);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(21, 22);
            this.label15.TabIndex = 854;
            this.label15.Text = "V";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 16F);
            this.label16.Location = new System.Drawing.Point(380, 12);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(98, 22);
            this.label16.TabIndex = 855;
            this.label16.Text = "励磁电压";
            // 
            // LCCurrentValue
            // 
            this.LCCurrentValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LCCurrentValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LCCurrentValue.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LCCurrentValue.ForeColor = System.Drawing.Color.Black;
            this.LCCurrentValue.Location = new System.Drawing.Point(843, 9);
            this.LCCurrentValue.Name = "LCCurrentValue";
            this.LCCurrentValue.Size = new System.Drawing.Size(91, 28);
            this.LCCurrentValue.TabIndex = 853;
            this.LCCurrentValue.Tag = "";
            this.LCCurrentValue.Text = "0.0";
            this.LCCurrentValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LCVoltageValue
            // 
            this.LCVoltageValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LCVoltageValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LCVoltageValue.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LCVoltageValue.ForeColor = System.Drawing.Color.Black;
            this.LCVoltageValue.Location = new System.Drawing.Point(524, 9);
            this.LCVoltageValue.Name = "LCVoltageValue";
            this.LCVoltageValue.Size = new System.Drawing.Size(91, 28);
            this.LCVoltageValue.TabIndex = 852;
            this.LCVoltageValue.Tag = "";
            this.LCVoltageValue.Text = "0.0";
            this.LCVoltageValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEoSetTemp
            // 
            this.lblEoSetTemp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEoSetTemp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEoSetTemp.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEoSetTemp.ForeColor = System.Drawing.Color.Black;
            this.lblEoSetTemp.Location = new System.Drawing.Point(185, 47);
            this.lblEoSetTemp.Name = "lblEoSetTemp";
            this.lblEoSetTemp.Size = new System.Drawing.Size(91, 28);
            this.lblEoSetTemp.TabIndex = 863;
            this.lblEoSetTemp.Tag = "机油温度设置PID";
            this.lblEoSetTemp.Text = "0.0";
            this.lblEoSetTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 16F);
            this.label6.Location = new System.Drawing.Point(285, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 22);
            this.label6.TabIndex = 862;
            this.label6.Text = "℃";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 16F);
            this.label7.Location = new System.Drawing.Point(29, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(142, 22);
            this.label7.TabIndex = 861;
            this.label7.Text = "机油设定温度";
            // 
            // FrmScarmOpen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1087, 500);
            this.ControlBox = false;
            this.Controls.Add(this.lblEoSetTemp);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.LCCurrentValue);
            this.Controls.Add(this.LCVoltageValue);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblEoTemp);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox49);
            this.Controls.Add(this.groupBox48);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rButton3);
            this.Controls.Add(this.nudEoTemp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnClose);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmScarmOpen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "急停-实时监控";
            this.TopMost = true;
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox48.ResumeLayout(false);
            this.groupBox49.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Sunny.UI.UIButton btnClose;
        private System.Windows.Forms.Timer timerFast;
        private System.Windows.Forms.Timer timerSlow;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox3;
        private RW.UI.Controls.RButton btn24Close;
        private RW.UI.Controls.RButton btn24Open;
        private System.Windows.Forms.GroupBox groupBox1;
        private RW.UI.Controls.RButton btnFuelCycleClose;
        private RW.UI.Controls.RButton btnFuelCycleOpen;
        private System.Windows.Forms.GroupBox groupBox48;
        private RW.UI.Controls.RButton btnY181Close;
        private RW.UI.Controls.RButton btnY181Open;
        private System.Windows.Forms.GroupBox groupBox49;
        private RW.UI.Controls.RButton btnY182Close;
        private RW.UI.Controls.RButton btnY182Open;
        private System.Windows.Forms.GroupBox groupBox2;
        private RW.UI.Controls.RButton btnEoClose;
        private RW.UI.Controls.RButton btnEoOpen;
        private Sunny.UI.UIDoubleUpDown nudEoTemp;
        private RW.UI.Controls.RButton rButton3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblEoTemp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label LCCurrentValue;
        private System.Windows.Forms.Label LCVoltageValue;
        private System.Windows.Forms.Label lblEoSetTemp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}
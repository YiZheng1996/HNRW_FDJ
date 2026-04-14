
namespace MainUI.Widget
{
    partial class ucShutDown
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
            this.btnEoOpen = new RW.UI.Controls.RButton();
            this.btn24Close = new RW.UI.Controls.RButton();
            this.btn24Open = new RW.UI.Controls.RButton();
            this.btnEoClose = new RW.UI.Controls.RButton();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.LCCurrentValue = new System.Windows.Forms.Label();
            this.LCVoltageValue = new System.Windows.Forms.Label();
            this.lblEoPressureOut = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblEoTempOut = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFuelCycleClose = new RW.UI.Controls.RButton();
            this.btnFuelCycleOpen = new RW.UI.Controls.RButton();
            this.btnSetBeginLC = new RW.UI.Controls.RButton();
            this.btnSetBeginSpeed = new RW.UI.Controls.RButton();
            this.nudBeginCurrent = new Sunny.UI.UIDoubleUpDown();
            this.nudBeginInvertSpeed = new Sunny.UI.UIDoubleUpDown();
            this.label31 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lblEoPressure = new System.Windows.Forms.Label();
            this.lblEoTemp = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblWaterTemp = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timerState = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblTorque = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPower = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.timerFast = new System.Windows.Forms.Timer(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEoOpen
            // 
            this.btnEoOpen.BackColor = System.Drawing.Color.Silver;
            this.btnEoOpen.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnEoOpen.FalseColor = System.Drawing.Color.Silver;
            this.btnEoOpen.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEoOpen.Location = new System.Drawing.Point(21, 40);
            this.btnEoOpen.Name = "btnEoOpen";
            this.btnEoOpen.OutputTagName = "预供机油循环";
            this.btnEoOpen.Size = new System.Drawing.Size(98, 38);
            this.btnEoOpen.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnEoOpen.TabIndex = 828;
            this.btnEoOpen.Tag = "1";
            this.btnEoOpen.Text = "开始";
            this.btnEoOpen.TrueColor = System.Drawing.Color.Lime;
            this.btnEoOpen.Click += new System.EventHandler(this.sw_Valve_Click);
            // 
            // btn24Close
            // 
            this.btn24Close.BackColor = System.Drawing.Color.Silver;
            this.btn24Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn24Close.FalseColor = System.Drawing.Color.Silver;
            this.btn24Close.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn24Close.Location = new System.Drawing.Point(139, 40);
            this.btn24Close.Name = "btn24Close";
            this.btn24Close.OutputTagName = "发动机DC24V供电";
            this.btn24Close.Size = new System.Drawing.Size(98, 38);
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
            this.btn24Open.Location = new System.Drawing.Point(28, 40);
            this.btn24Open.Name = "btn24Open";
            this.btn24Open.OutputTagName = "发动机DC24V供电";
            this.btn24Open.Size = new System.Drawing.Size(98, 38);
            this.btn24Open.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btn24Open.TabIndex = 828;
            this.btn24Open.Tag = "1";
            this.btn24Open.Text = "开";
            this.btn24Open.TrueColor = System.Drawing.Color.Lime;
            this.btn24Open.Click += new System.EventHandler(this.sw_Valve_Click);
            // 
            // btnEoClose
            // 
            this.btnEoClose.BackColor = System.Drawing.Color.Silver;
            this.btnEoClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnEoClose.FalseColor = System.Drawing.Color.Silver;
            this.btnEoClose.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEoClose.Location = new System.Drawing.Point(135, 40);
            this.btnEoClose.Name = "btnEoClose";
            this.btnEoClose.OutputTagName = "预供机油循环";
            this.btnEoClose.Size = new System.Drawing.Size(98, 38);
            this.btnEoClose.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnEoClose.TabIndex = 828;
            this.btnEoClose.Tag = "0";
            this.btnEoClose.Text = "停止";
            this.btnEoClose.TrueColor = System.Drawing.Color.Lime;
            this.btnEoClose.Click += new System.EventHandler(this.sw_Valve_Click);
            // 
            // lblSpeed
            // 
            this.lblSpeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblSpeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSpeed.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSpeed.ForeColor = System.Drawing.Color.Black;
            this.lblSpeed.Location = new System.Drawing.Point(63, 28);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(91, 28);
            this.lblSpeed.TabIndex = 884;
            this.lblSpeed.Tag = "";
            this.lblSpeed.Text = "0.0";
            this.lblSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LCCurrentValue
            // 
            this.LCCurrentValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LCCurrentValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LCCurrentValue.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LCCurrentValue.ForeColor = System.Drawing.Color.Black;
            this.LCCurrentValue.Location = new System.Drawing.Point(99, 61);
            this.LCCurrentValue.Name = "LCCurrentValue";
            this.LCCurrentValue.Size = new System.Drawing.Size(91, 28);
            this.LCCurrentValue.TabIndex = 879;
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
            this.LCVoltageValue.Location = new System.Drawing.Point(99, 28);
            this.LCVoltageValue.Name = "LCVoltageValue";
            this.LCVoltageValue.Size = new System.Drawing.Size(91, 28);
            this.LCVoltageValue.TabIndex = 878;
            this.LCVoltageValue.Tag = "";
            this.LCVoltageValue.Text = "0.0";
            this.LCVoltageValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEoPressureOut
            // 
            this.lblEoPressureOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEoPressureOut.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEoPressureOut.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEoPressureOut.ForeColor = System.Drawing.Color.Black;
            this.lblEoPressureOut.Location = new System.Drawing.Point(158, 61);
            this.lblEoPressureOut.Name = "lblEoPressureOut";
            this.lblEoPressureOut.Size = new System.Drawing.Size(91, 28);
            this.lblEoPressureOut.TabIndex = 877;
            this.lblEoPressureOut.Tag = "P20机油泵出口压力";
            this.lblEoPressureOut.Text = "0.0";
            this.lblEoPressureOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 14F);
            this.label12.Location = new System.Drawing.Point(254, 66);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 19);
            this.label12.TabIndex = 875;
            this.label12.Text = "kPa";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 14F);
            this.label13.Location = new System.Drawing.Point(9, 66);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(142, 19);
            this.label13.TabIndex = 876;
            this.label13.Text = "机油泵出口压力";
            // 
            // lblEoTempOut
            // 
            this.lblEoTempOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEoTempOut.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEoTempOut.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEoTempOut.ForeColor = System.Drawing.Color.Black;
            this.lblEoTempOut.Location = new System.Drawing.Point(466, 61);
            this.lblEoTempOut.Name = "lblEoTempOut";
            this.lblEoTempOut.Size = new System.Drawing.Size(91, 28);
            this.lblEoTempOut.TabIndex = 874;
            this.lblEoTempOut.Tag = "T20机油泵出口油温";
            this.lblEoTempOut.Text = "0.0";
            this.lblEoTempOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 14F);
            this.label9.Location = new System.Drawing.Point(562, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 19);
            this.label9.TabIndex = 872;
            this.label9.Text = "℃";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 14F);
            this.label10.Location = new System.Drawing.Point(319, 66);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(142, 19);
            this.label10.TabIndex = 873;
            this.label10.Text = "机油泵出口油温";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn24Close);
            this.groupBox3.Controls.Add(this.btn24Open);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox3.Location = new System.Drawing.Point(537, 149);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(267, 93);
            this.groupBox3.TabIndex = 870;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "发动机控制盒电源DC24V控制";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnEoClose);
            this.groupBox2.Controls.Add(this.btnEoOpen);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox2.Location = new System.Drawing.Point(270, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(253, 93);
            this.groupBox2.TabIndex = 869;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "预供机油循环";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFuelCycleClose);
            this.groupBox1.Controls.Add(this.btnFuelCycleOpen);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox1.Location = new System.Drawing.Point(4, 149);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 93);
            this.groupBox1.TabIndex = 868;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "燃油循环";
            // 
            // btnFuelCycleClose
            // 
            this.btnFuelCycleClose.BackColor = System.Drawing.Color.Silver;
            this.btnFuelCycleClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFuelCycleClose.FalseColor = System.Drawing.Color.Silver;
            this.btnFuelCycleClose.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFuelCycleClose.Location = new System.Drawing.Point(139, 40);
            this.btnFuelCycleClose.Name = "btnFuelCycleClose";
            this.btnFuelCycleClose.OutputTagName = "燃油循环";
            this.btnFuelCycleClose.Size = new System.Drawing.Size(98, 38);
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
            this.btnFuelCycleOpen.Location = new System.Drawing.Point(17, 40);
            this.btnFuelCycleOpen.Name = "btnFuelCycleOpen";
            this.btnFuelCycleOpen.OutputTagName = "燃油循环";
            this.btnFuelCycleOpen.Size = new System.Drawing.Size(98, 38);
            this.btnFuelCycleOpen.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnFuelCycleOpen.TabIndex = 828;
            this.btnFuelCycleOpen.Tag = "1";
            this.btnFuelCycleOpen.Text = "开始";
            this.btnFuelCycleOpen.TrueColor = System.Drawing.Color.Lime;
            this.btnFuelCycleOpen.Click += new System.EventHandler(this.sw_Valve_Click);
            // 
            // btnSetBeginLC
            // 
            this.btnSetBeginLC.BackColor = System.Drawing.Color.Silver;
            this.btnSetBeginLC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSetBeginLC.FalseColor = System.Drawing.Color.Silver;
            this.btnSetBeginLC.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetBeginLC.Location = new System.Drawing.Point(155, 139);
            this.btnSetBeginLC.Name = "btnSetBeginLC";
            this.btnSetBeginLC.Size = new System.Drawing.Size(81, 33);
            this.btnSetBeginLC.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnSetBeginLC.TabIndex = 867;
            this.btnSetBeginLC.Tag = "";
            this.btnSetBeginLC.Text = "设置";
            this.btnSetBeginLC.TrueColor = System.Drawing.Color.Lime;
            this.btnSetBeginLC.Click += new System.EventHandler(this.btnSetBeginLC_Click);
            // 
            // btnSetBeginSpeed
            // 
            this.btnSetBeginSpeed.BackColor = System.Drawing.Color.Silver;
            this.btnSetBeginSpeed.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSetBeginSpeed.FalseColor = System.Drawing.Color.Silver;
            this.btnSetBeginSpeed.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetBeginSpeed.Location = new System.Drawing.Point(155, 57);
            this.btnSetBeginSpeed.Name = "btnSetBeginSpeed";
            this.btnSetBeginSpeed.Size = new System.Drawing.Size(81, 33);
            this.btnSetBeginSpeed.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnSetBeginSpeed.TabIndex = 866;
            this.btnSetBeginSpeed.Tag = "";
            this.btnSetBeginSpeed.Text = "设置";
            this.btnSetBeginSpeed.TrueColor = System.Drawing.Color.Lime;
            this.btnSetBeginSpeed.Click += new System.EventHandler(this.btnSetBeginSpeed_Click);
            // 
            // nudBeginCurrent
            // 
            this.nudBeginCurrent.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nudBeginCurrent.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudBeginCurrent.Location = new System.Drawing.Point(21, 141);
            this.nudBeginCurrent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudBeginCurrent.Maximum = 400D;
            this.nudBeginCurrent.Minimum = 0D;
            this.nudBeginCurrent.MinimumSize = new System.Drawing.Size(100, 0);
            this.nudBeginCurrent.Name = "nudBeginCurrent";
            this.nudBeginCurrent.Padding = new System.Windows.Forms.Padding(5);
            this.nudBeginCurrent.ShowText = false;
            this.nudBeginCurrent.Size = new System.Drawing.Size(128, 31);
            this.nudBeginCurrent.Step = 1D;
            this.nudBeginCurrent.TabIndex = 865;
            this.nudBeginCurrent.Text = "0.00";
            this.nudBeginCurrent.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.nudBeginCurrent.Value = 0D;
            this.nudBeginCurrent.ValueChanged += new Sunny.UI.UIDoubleUpDown.OnValueChanged(this.nudBeginCurrent_ValueChanged);
            // 
            // nudBeginInvertSpeed
            // 
            this.nudBeginInvertSpeed.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nudBeginInvertSpeed.DecimalPlaces = 0;
            this.nudBeginInvertSpeed.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudBeginInvertSpeed.Location = new System.Drawing.Point(21, 59);
            this.nudBeginInvertSpeed.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudBeginInvertSpeed.Maximum = 1000D;
            this.nudBeginInvertSpeed.Minimum = 0D;
            this.nudBeginInvertSpeed.MinimumSize = new System.Drawing.Size(100, 0);
            this.nudBeginInvertSpeed.Name = "nudBeginInvertSpeed";
            this.nudBeginInvertSpeed.Padding = new System.Windows.Forms.Padding(5);
            this.nudBeginInvertSpeed.ShowText = false;
            this.nudBeginInvertSpeed.Size = new System.Drawing.Size(128, 31);
            this.nudBeginInvertSpeed.Step = 1D;
            this.nudBeginInvertSpeed.TabIndex = 864;
            this.nudBeginInvertSpeed.Text = "0";
            this.nudBeginInvertSpeed.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.nudBeginInvertSpeed.Value = 0D;
            this.nudBeginInvertSpeed.ValueChanged += new Sunny.UI.UIDoubleUpDown.OnValueChanged(this.nudBeginInvertSpeed_ValueChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("宋体", 14F);
            this.label31.Location = new System.Drawing.Point(21, 119);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(115, 19);
            this.label31.TabIndex = 862;
            this.label31.Text = "励磁电流(A)";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("宋体", 14F);
            this.label26.Location = new System.Drawing.Point(21, 36);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(154, 19);
            this.label26.TabIndex = 863;
            this.label26.Text = "发动机转速(rpm)";
            // 
            // lblEoPressure
            // 
            this.lblEoPressure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEoPressure.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEoPressure.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEoPressure.ForeColor = System.Drawing.Color.Black;
            this.lblEoPressure.Location = new System.Drawing.Point(158, 28);
            this.lblEoPressure.Name = "lblEoPressure";
            this.lblEoPressure.Size = new System.Drawing.Size(91, 28);
            this.lblEoPressure.TabIndex = 860;
            this.lblEoPressure.Tag = "P21主油道进口油压";
            this.lblEoPressure.Text = "0.0";
            this.lblEoPressure.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEoTemp
            // 
            this.lblEoTemp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEoTemp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEoTemp.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEoTemp.ForeColor = System.Drawing.Color.Black;
            this.lblEoTemp.Location = new System.Drawing.Point(466, 28);
            this.lblEoTemp.Name = "lblEoTemp";
            this.lblEoTemp.Size = new System.Drawing.Size(91, 28);
            this.lblEoTemp.TabIndex = 861;
            this.lblEoTemp.Tag = "T21主油道进口油温";
            this.lblEoTemp.Text = "0.0";
            this.lblEoTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 14F);
            this.label7.Location = new System.Drawing.Point(254, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 19);
            this.label7.TabIndex = 853;
            this.label7.Text = "kPa";
            // 
            // lblWaterTemp
            // 
            this.lblWaterTemp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblWaterTemp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblWaterTemp.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWaterTemp.ForeColor = System.Drawing.Color.Black;
            this.lblWaterTemp.Location = new System.Drawing.Point(158, 96);
            this.lblWaterTemp.Name = "lblWaterTemp";
            this.lblWaterTemp.Size = new System.Drawing.Size(91, 28);
            this.lblWaterTemp.TabIndex = 859;
            this.lblWaterTemp.Tag = "T1高温水出机温度";
            this.lblWaterTemp.Text = "0.0";
            this.lblWaterTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 14F);
            this.label4.Location = new System.Drawing.Point(562, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 19);
            this.label4.TabIndex = 854;
            this.label4.Text = "℃";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 14F);
            this.label6.Location = new System.Drawing.Point(9, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 19);
            this.label6.TabIndex = 858;
            this.label6.Text = "机油进口压力";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14F);
            this.label2.Location = new System.Drawing.Point(254, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 19);
            this.label2.TabIndex = 856;
            this.label2.Text = "℃";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 14F);
            this.label3.Location = new System.Drawing.Point(319, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 19);
            this.label3.TabIndex = 857;
            this.label3.Text = "机油进口油温";
            // 
            // timerState
            // 
            this.timerState.Tick += new System.EventHandler(this.timerState_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14F);
            this.label1.Location = new System.Drawing.Point(9, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 19);
            this.label1.TabIndex = 855;
            this.label1.Text = "高温水出机温度";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Controls.Add(this.label23);
            this.groupBox5.Controls.Add(this.LCVoltageValue);
            this.groupBox5.Controls.Add(this.LCCurrentValue);
            this.groupBox5.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox5.Location = new System.Drawing.Point(4, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(229, 138);
            this.groupBox5.TabIndex = 887;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "励磁柜输出";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 14F);
            this.label18.Location = new System.Drawing.Point(197, 66);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(19, 19);
            this.label18.TabIndex = 855;
            this.label18.Text = "A";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 14F);
            this.label19.Location = new System.Drawing.Point(197, 33);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(19, 19);
            this.label19.TabIndex = 854;
            this.label19.Text = "V";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("宋体", 14F);
            this.label22.Location = new System.Drawing.Point(8, 66);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(85, 19);
            this.label22.TabIndex = 638;
            this.label22.Text = "励磁电流";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("宋体", 14F);
            this.label23.Location = new System.Drawing.Point(8, 33);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(85, 19);
            this.label23.TabIndex = 636;
            this.label23.Text = "励磁电压";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblTorque);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.lblPower);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Controls.Add(this.lblSpeed);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Controls.Add(this.label25);
            this.groupBox6.Controls.Add(this.label27);
            this.groupBox6.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox6.Location = new System.Drawing.Point(239, 5);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(205, 138);
            this.groupBox6.TabIndex = 888;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "发动机数据";
            // 
            // lblTorque
            // 
            this.lblTorque.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTorque.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTorque.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTorque.ForeColor = System.Drawing.Color.Black;
            this.lblTorque.Location = new System.Drawing.Point(62, 61);
            this.lblTorque.Name = "lblTorque";
            this.lblTorque.Size = new System.Drawing.Size(90, 28);
            this.lblTorque.TabIndex = 859;
            this.lblTorque.Tag = "";
            this.lblTorque.Text = "0.0";
            this.lblTorque.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 14F);
            this.label8.Location = new System.Drawing.Point(156, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 19);
            this.label8.TabIndex = 858;
            this.label8.Text = "kW";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPower
            // 
            this.lblPower.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblPower.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPower.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPower.ForeColor = System.Drawing.Color.Black;
            this.lblPower.Location = new System.Drawing.Point(62, 96);
            this.lblPower.Name = "lblPower";
            this.lblPower.Size = new System.Drawing.Size(90, 28);
            this.lblPower.TabIndex = 857;
            this.lblPower.Tag = "";
            this.lblPower.Text = "0.0";
            this.lblPower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 14F);
            this.label14.Location = new System.Drawing.Point(8, 101);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 19);
            this.label14.TabIndex = 856;
            this.label14.Text = "功率";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 14F);
            this.label15.Location = new System.Drawing.Point(156, 66);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(48, 19);
            this.label15.TabIndex = 855;
            this.label15.Text = "N·m";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 14F);
            this.label16.Location = new System.Drawing.Point(156, 33);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(39, 19);
            this.label16.TabIndex = 854;
            this.label16.Text = "rpm";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("宋体", 14F);
            this.label25.Location = new System.Drawing.Point(8, 66);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(47, 19);
            this.label25.TabIndex = 638;
            this.label25.Text = "扭矩";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("宋体", 14F);
            this.label27.Location = new System.Drawing.Point(8, 33);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(47, 19);
            this.label27.TabIndex = 636;
            this.label27.Text = "转速";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerFast
            // 
            this.timerFast.Interval = 500;
            this.timerFast.Tick += new System.EventHandler(this.timerFast_Tick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblEoPressure);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.lblEoTempOut);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.lblEoPressureOut);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.lblEoTemp);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.lblWaterTemp);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox4.Location = new System.Drawing.Point(449, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(617, 138);
            this.groupBox4.TabIndex = 889;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "机油系统数据";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.nudBeginInvertSpeed);
            this.groupBox7.Controls.Add(this.label26);
            this.groupBox7.Controls.Add(this.btnSetBeginSpeed);
            this.groupBox7.Controls.Add(this.label31);
            this.groupBox7.Controls.Add(this.nudBeginCurrent);
            this.groupBox7.Controls.Add(this.btnSetBeginLC);
            this.groupBox7.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox7.Location = new System.Drawing.Point(4, 259);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(249, 188);
            this.groupBox7.TabIndex = 890;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "停机参数设置";
            // 
            // ucShutDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ucShutDown";
            this.Size = new System.Drawing.Size(1074, 532);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private RW.UI.Controls.RButton btnEoOpen;
        private RW.UI.Controls.RButton btn24Close;
        private RW.UI.Controls.RButton btn24Open;
        private RW.UI.Controls.RButton btnEoClose;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label LCCurrentValue;
        private System.Windows.Forms.Label LCVoltageValue;
        private System.Windows.Forms.Label lblEoPressureOut;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblEoTempOut;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private RW.UI.Controls.RButton btnFuelCycleClose;
        private RW.UI.Controls.RButton btnFuelCycleOpen;
        private RW.UI.Controls.RButton btnSetBeginLC;
        private RW.UI.Controls.RButton btnSetBeginSpeed;
        private Sunny.UI.UIDoubleUpDown nudBeginCurrent;
        private Sunny.UI.UIDoubleUpDown nudBeginInvertSpeed;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label lblEoPressure;
        private System.Windows.Forms.Label lblEoTemp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblWaterTemp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timerState;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lblTorque;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPower;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Timer timerFast;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox7;
    }
}

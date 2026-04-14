
namespace MainUI.Widget
{
    partial class ucStartup
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
            this.lblTorque = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPower = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.LightInvertReady = new Sunny.UI.UILight();
            this.lblInverterCurrent = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.LightInvertRunning = new Sunny.UI.UILight();
            this.label16 = new System.Windows.Forms.Label();
            this.LCCurrentValue = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblInverterPower = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.LCVoltageValue = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.uiLightDV24Close = new Sunny.UI.UILight();
            this.label20 = new System.Windows.Forms.Label();
            this.uiLightDV24Open = new Sunny.UI.UILight();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.btnManualStart = new RW.UI.Controls.RButton();
            this.uiLightFuelCycle = new Sunny.UI.UILight();
            this.label32 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.uiLightStart = new Sunny.UI.UILight();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.uiLightPC = new Sunny.UI.UILight();
            this.uiLightWaterUP = new Sunny.UI.UILight();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.uiLightEO = new Sunny.UI.UILight();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnDC24VClose = new RW.UI.Controls.RButton();
            this.btnDC24VOpen = new RW.UI.Controls.RButton();
            this.btnManualShake = new RW.UI.Controls.RButton();
            this.uiLightFuelCycleClose = new Sunny.UI.UILight();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.uiLightShake = new Sunny.UI.UILight();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.nudBeginInvertSpeed = new Sunny.UI.UIDoubleUpDown();
            this.nudBeginCurrent = new Sunny.UI.UIDoubleUpDown();
            this.btnSetBeginSpeed = new RW.UI.Controls.RButton();
            this.btnSetBeginLC = new RW.UI.Controls.RButton();
            this.lblInverterVoltage = new System.Windows.Forms.Label();
            this.timerSlow = new System.Windows.Forms.Timer(this.components);
            this.timerFast = new System.Windows.Forms.Timer(this.components);
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.label42 = new System.Windows.Forms.Label();
            this.LightInvertFault = new Sunny.UI.UILight();
            this.label39 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.lblFaultCode = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnFuelCycleClose = new RW.UI.Controls.RButton();
            this.btnFuelCycleOpen = new RW.UI.Controls.RButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEoOpen = new RW.UI.Controls.RButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnEoClose = new RW.UI.Controls.RButton();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTorque
            // 
            this.lblTorque.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTorque.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTorque.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTorque.ForeColor = System.Drawing.Color.Black;
            this.lblTorque.Location = new System.Drawing.Point(62, 63);
            this.lblTorque.Name = "lblTorque";
            this.lblTorque.Size = new System.Drawing.Size(106, 28);
            this.lblTorque.TabIndex = 859;
            this.lblTorque.Tag = "";
            this.lblTorque.Text = "0.0";
            this.lblTorque.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblTorque);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.lblPower);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.lblSpeed);
            this.groupBox6.Controls.Add(this.label25);
            this.groupBox6.Controls.Add(this.label27);
            this.groupBox6.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox6.Location = new System.Drawing.Point(614, 5);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(228, 138);
            this.groupBox6.TabIndex = 865;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "发动机数据";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14F);
            this.label2.Location = new System.Drawing.Point(174, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 19);
            this.label2.TabIndex = 858;
            this.label2.Text = "kW";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPower
            // 
            this.lblPower.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblPower.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPower.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPower.ForeColor = System.Drawing.Color.Black;
            this.lblPower.Location = new System.Drawing.Point(62, 96);
            this.lblPower.Name = "lblPower";
            this.lblPower.Size = new System.Drawing.Size(106, 28);
            this.lblPower.TabIndex = 857;
            this.lblPower.Tag = "";
            this.lblPower.Text = "0.0";
            this.lblPower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 14F);
            this.label5.Location = new System.Drawing.Point(8, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 19);
            this.label5.TabIndex = 856;
            this.label5.Text = "功率";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 14F);
            this.label11.Location = new System.Drawing.Point(174, 68);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 19);
            this.label11.TabIndex = 855;
            this.label11.Text = "N·m";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 14F);
            this.label12.Location = new System.Drawing.Point(174, 34);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 19);
            this.label12.TabIndex = 854;
            this.label12.Text = "rpm";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSpeed
            // 
            this.lblSpeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblSpeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSpeed.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSpeed.ForeColor = System.Drawing.Color.Black;
            this.lblSpeed.Location = new System.Drawing.Point(62, 29);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(106, 28);
            this.lblSpeed.TabIndex = 853;
            this.lblSpeed.Tag = "";
            this.lblSpeed.Text = "0.0";
            this.lblSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("宋体", 14F);
            this.label25.Location = new System.Drawing.Point(8, 68);
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
            this.label27.Location = new System.Drawing.Point(8, 34);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(47, 19);
            this.label27.TabIndex = 636;
            this.label27.Text = "转速";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LightInvertReady
            // 
            this.LightInvertReady.CenterColor = System.Drawing.Color.Lime;
            this.LightInvertReady.Font = new System.Drawing.Font("宋体", 12F);
            this.LightInvertReady.Location = new System.Drawing.Point(232, 33);
            this.LightInvertReady.MinimumSize = new System.Drawing.Size(1, 1);
            this.LightInvertReady.Name = "LightInvertReady";
            this.LightInvertReady.OffCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.LightInvertReady.OffColor = System.Drawing.Color.Red;
            this.LightInvertReady.OnCenterColor = System.Drawing.Color.Lime;
            this.LightInvertReady.OnColor = System.Drawing.Color.MediumSeaGreen;
            this.LightInvertReady.Radius = 23;
            this.LightInvertReady.Size = new System.Drawing.Size(23, 23);
            this.LightInvertReady.State = Sunny.UI.UILightState.Off;
            this.LightInvertReady.TabIndex = 850;
            this.LightInvertReady.Tag = "";
            this.LightInvertReady.Text = "uiLight3";
            // 
            // lblInverterCurrent
            // 
            this.lblInverterCurrent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblInverterCurrent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInverterCurrent.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInverterCurrent.ForeColor = System.Drawing.Color.Black;
            this.lblInverterCurrent.Location = new System.Drawing.Point(99, 63);
            this.lblInverterCurrent.Name = "lblInverterCurrent";
            this.lblInverterCurrent.Size = new System.Drawing.Size(90, 28);
            this.lblInverterCurrent.TabIndex = 853;
            this.lblInverterCurrent.Tag = "";
            this.lblInverterCurrent.Text = "0.0";
            this.lblInverterCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 14F);
            this.label13.Location = new System.Drawing.Point(251, 35);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 19);
            this.label13.TabIndex = 621;
            this.label13.Text = "就绪";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 14F);
            this.label17.Location = new System.Drawing.Point(190, 68);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(19, 19);
            this.label17.TabIndex = 855;
            this.label17.Text = "A";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 14F);
            this.label18.Location = new System.Drawing.Point(190, 34);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(19, 19);
            this.label18.TabIndex = 854;
            this.label18.Text = "V";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LightInvertRunning
            // 
            this.LightInvertRunning.CenterColor = System.Drawing.Color.Lime;
            this.LightInvertRunning.Font = new System.Drawing.Font("宋体", 14F);
            this.LightInvertRunning.Location = new System.Drawing.Point(233, 64);
            this.LightInvertRunning.MinimumSize = new System.Drawing.Size(1, 1);
            this.LightInvertRunning.Name = "LightInvertRunning";
            this.LightInvertRunning.OffCenterColor = System.Drawing.Color.Silver;
            this.LightInvertRunning.OffColor = System.Drawing.Color.Black;
            this.LightInvertRunning.OnCenterColor = System.Drawing.Color.Lime;
            this.LightInvertRunning.OnColor = System.Drawing.Color.MediumSeaGreen;
            this.LightInvertRunning.Radius = 22;
            this.LightInvertRunning.Size = new System.Drawing.Size(23, 22);
            this.LightInvertRunning.State = Sunny.UI.UILightState.Off;
            this.LightInvertRunning.TabIndex = 633;
            this.LightInvertRunning.Tag = "";
            this.LightInvertRunning.Text = "uiLight3";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 14F);
            this.label16.Location = new System.Drawing.Point(195, 101);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 19);
            this.label16.TabIndex = 857;
            this.label16.Text = "kW";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LCCurrentValue
            // 
            this.LCCurrentValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LCCurrentValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LCCurrentValue.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LCCurrentValue.ForeColor = System.Drawing.Color.Black;
            this.LCCurrentValue.Location = new System.Drawing.Point(95, 63);
            this.LCCurrentValue.Name = "LCCurrentValue";
            this.LCCurrentValue.Size = new System.Drawing.Size(90, 28);
            this.LCCurrentValue.TabIndex = 853;
            this.LCCurrentValue.Tag = "";
            this.LCCurrentValue.Text = "0.0";
            this.LCCurrentValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("宋体", 14F);
            this.label22.Location = new System.Drawing.Point(4, 68);
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
            this.label23.Location = new System.Drawing.Point(4, 35);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(85, 19);
            this.label23.TabIndex = 636;
            this.label23.Text = "励磁电压";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInverterPower
            // 
            this.lblInverterPower.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblInverterPower.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInverterPower.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInverterPower.ForeColor = System.Drawing.Color.Black;
            this.lblInverterPower.Location = new System.Drawing.Point(99, 96);
            this.lblInverterPower.Name = "lblInverterPower";
            this.lblInverterPower.Size = new System.Drawing.Size(90, 28);
            this.lblInverterPower.TabIndex = 856;
            this.lblInverterPower.Tag = "";
            this.lblInverterPower.Text = "0.0";
            this.lblInverterPower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 14F);
            this.label14.Location = new System.Drawing.Point(195, 68);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(19, 19);
            this.label14.TabIndex = 855;
            this.label14.Text = "A";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LCVoltageValue
            // 
            this.LCVoltageValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LCVoltageValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LCVoltageValue.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LCVoltageValue.ForeColor = System.Drawing.Color.Black;
            this.LCVoltageValue.Location = new System.Drawing.Point(95, 30);
            this.LCVoltageValue.Name = "LCVoltageValue";
            this.LCVoltageValue.Size = new System.Drawing.Size(90, 28);
            this.LCVoltageValue.TabIndex = 853;
            this.LCVoltageValue.Tag = "";
            this.LCVoltageValue.Text = "0.0";
            this.LCVoltageValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.LCCurrentValue);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Controls.Add(this.label23);
            this.groupBox5.Controls.Add(this.LCVoltageValue);
            this.groupBox5.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox5.Location = new System.Drawing.Point(390, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(217, 138);
            this.groupBox5.TabIndex = 864;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "励磁柜输出";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 14F);
            this.label15.Location = new System.Drawing.Point(195, 35);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(19, 19);
            this.label15.TabIndex = 854;
            this.label15.Text = "V";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiLightDV24Close
            // 
            this.uiLightDV24Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.uiLightDV24Close.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.uiLightDV24Close.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLightDV24Close.Location = new System.Drawing.Point(198, 29);
            this.uiLightDV24Close.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLightDV24Close.Name = "uiLightDV24Close";
            this.uiLightDV24Close.OffCenterColor = System.Drawing.Color.Silver;
            this.uiLightDV24Close.OffColor = System.Drawing.SystemColors.ControlText;
            this.uiLightDV24Close.OnCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.uiLightDV24Close.OnColor = System.Drawing.Color.Lime;
            this.uiLightDV24Close.Radius = 23;
            this.uiLightDV24Close.Size = new System.Drawing.Size(23, 23);
            this.uiLightDV24Close.State = Sunny.UI.UILightState.Off;
            this.uiLightDV24Close.TabIndex = 850;
            this.uiLightDV24Close.Tag = "";
            this.uiLightDV24Close.Text = "uiLight3";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(216, 31);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(161, 19);
            this.label20.TabIndex = 851;
            this.label20.Text = "控制盒电源【关】";
            // 
            // uiLightDV24Open
            // 
            this.uiLightDV24Open.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.uiLightDV24Open.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.uiLightDV24Open.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLightDV24Open.Location = new System.Drawing.Point(201, 31);
            this.uiLightDV24Open.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLightDV24Open.Name = "uiLightDV24Open";
            this.uiLightDV24Open.OffCenterColor = System.Drawing.Color.Silver;
            this.uiLightDV24Open.OffColor = System.Drawing.SystemColors.ControlText;
            this.uiLightDV24Open.OnCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.uiLightDV24Open.OnColor = System.Drawing.Color.Lime;
            this.uiLightDV24Open.Radius = 23;
            this.uiLightDV24Open.Size = new System.Drawing.Size(23, 23);
            this.uiLightDV24Open.State = Sunny.UI.UILightState.Off;
            this.uiLightDV24Open.TabIndex = 848;
            this.uiLightDV24Open.Tag = "";
            this.uiLightDV24Open.Text = "uiLight3";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.Location = new System.Drawing.Point(220, 33);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(161, 19);
            this.label19.TabIndex = 849;
            this.label19.Text = "控制盒电源【开】";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.uiLightDV24Open);
            this.groupBox10.Controls.Add(this.label19);
            this.groupBox10.Controls.Add(this.btnManualStart);
            this.groupBox10.Controls.Add(this.uiLightFuelCycle);
            this.groupBox10.Controls.Add(this.label32);
            this.groupBox10.Controls.Add(this.label1);
            this.groupBox10.Controls.Add(this.uiLightStart);
            this.groupBox10.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox10.Location = new System.Drawing.Point(275, 270);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(383, 188);
            this.groupBox10.TabIndex = 869;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "启机控制";
            // 
            // btnManualStart
            // 
            this.btnManualStart.BackColor = System.Drawing.Color.Silver;
            this.btnManualStart.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnManualStart.FalseColor = System.Drawing.Color.Silver;
            this.btnManualStart.Font = new System.Drawing.Font("宋体", 18F);
            this.btnManualStart.Location = new System.Drawing.Point(113, 134);
            this.btnManualStart.Name = "btnManualStart";
            this.btnManualStart.Size = new System.Drawing.Size(193, 47);
            this.btnManualStart.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnManualStart.TabIndex = 812;
            this.btnManualStart.Tag = "启机";
            this.btnManualStart.Text = "长按 启机";
            this.btnManualStart.TrueColor = System.Drawing.Color.Lime;
            this.btnManualStart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnManualRun_MouseDown);
            this.btnManualStart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnManualRun_MouseUp);
            // 
            // uiLightFuelCycle
            // 
            this.uiLightFuelCycle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.uiLightFuelCycle.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.uiLightFuelCycle.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLightFuelCycle.Location = new System.Drawing.Point(5, 31);
            this.uiLightFuelCycle.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLightFuelCycle.Name = "uiLightFuelCycle";
            this.uiLightFuelCycle.OffCenterColor = System.Drawing.Color.Silver;
            this.uiLightFuelCycle.OffColor = System.Drawing.SystemColors.ControlText;
            this.uiLightFuelCycle.OnCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.uiLightFuelCycle.OnColor = System.Drawing.Color.Lime;
            this.uiLightFuelCycle.Radius = 23;
            this.uiLightFuelCycle.Size = new System.Drawing.Size(23, 23);
            this.uiLightFuelCycle.State = Sunny.UI.UILightState.Off;
            this.uiLightFuelCycle.TabIndex = 743;
            this.uiLightFuelCycle.Tag = "";
            this.uiLightFuelCycle.Text = "uiLight3";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("宋体", 14F);
            this.label32.Location = new System.Drawing.Point(178, 107);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(85, 19);
            this.label32.TabIndex = 847;
            this.label32.Text = "启机准备";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(24, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 19);
            this.label1.TabIndex = 744;
            this.label1.Text = "燃油循环【开】";
            // 
            // uiLightStart
            // 
            this.uiLightStart.CenterColor = System.Drawing.Color.Lime;
            this.uiLightStart.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLightStart.Location = new System.Drawing.Point(155, 105);
            this.uiLightStart.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLightStart.Name = "uiLightStart";
            this.uiLightStart.OffCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.uiLightStart.OffColor = System.Drawing.Color.Red;
            this.uiLightStart.OnCenterColor = System.Drawing.Color.Lime;
            this.uiLightStart.OnColor = System.Drawing.Color.MediumSeaGreen;
            this.uiLightStart.Radius = 23;
            this.uiLightStart.Size = new System.Drawing.Size(23, 23);
            this.uiLightStart.State = Sunny.UI.UILightState.Off;
            this.uiLightStart.TabIndex = 846;
            this.uiLightStart.Tag = "";
            this.uiLightStart.Text = "uiLight3";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.uiLightPC);
            this.groupBox9.Controls.Add(this.uiLightWaterUP);
            this.groupBox9.Controls.Add(this.label3);
            this.groupBox9.Controls.Add(this.label10);
            this.groupBox9.Controls.Add(this.uiLightEO);
            this.groupBox9.Controls.Add(this.label6);
            this.groupBox9.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox9.Location = new System.Drawing.Point(847, 5);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(220, 138);
            this.groupBox9.TabIndex = 868;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "启机/甩车前状态检查";
            // 
            // uiLightPC
            // 
            this.uiLightPC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.uiLightPC.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.uiLightPC.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLightPC.Location = new System.Drawing.Point(12, 34);
            this.uiLightPC.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLightPC.Name = "uiLightPC";
            this.uiLightPC.OffCenterColor = System.Drawing.Color.Silver;
            this.uiLightPC.OffColor = System.Drawing.SystemColors.ControlText;
            this.uiLightPC.OnCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.uiLightPC.OnColor = System.Drawing.Color.Lime;
            this.uiLightPC.Radius = 23;
            this.uiLightPC.Size = new System.Drawing.Size(23, 23);
            this.uiLightPC.State = Sunny.UI.UILightState.Off;
            this.uiLightPC.TabIndex = 848;
            this.uiLightPC.Tag = "";
            this.uiLightPC.Text = "uiLight3";
            // 
            // uiLightWaterUP
            // 
            this.uiLightWaterUP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.uiLightWaterUP.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.uiLightWaterUP.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLightWaterUP.Location = new System.Drawing.Point(12, 63);
            this.uiLightWaterUP.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLightWaterUP.Name = "uiLightWaterUP";
            this.uiLightWaterUP.OffCenterColor = System.Drawing.Color.Silver;
            this.uiLightWaterUP.OffColor = System.Drawing.SystemColors.ControlText;
            this.uiLightWaterUP.OnCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.uiLightWaterUP.OnColor = System.Drawing.Color.Lime;
            this.uiLightWaterUP.Radius = 23;
            this.uiLightWaterUP.Size = new System.Drawing.Size(23, 23);
            this.uiLightWaterUP.State = Sunny.UI.UILightState.Off;
            this.uiLightWaterUP.TabIndex = 743;
            this.uiLightWaterUP.Tag = "";
            this.uiLightWaterUP.Text = "uiLight3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(33, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 19);
            this.label3.TabIndex = 744;
            this.label3.Text = "水极板到达上限位";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(33, 36);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(161, 19);
            this.label10.TabIndex = 849;
            this.label10.Text = "盘车连锁开关闭合";
            // 
            // uiLightEO
            // 
            this.uiLightEO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.uiLightEO.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.uiLightEO.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLightEO.Location = new System.Drawing.Point(12, 93);
            this.uiLightEO.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLightEO.Name = "uiLightEO";
            this.uiLightEO.OffCenterColor = System.Drawing.Color.Silver;
            this.uiLightEO.OffColor = System.Drawing.SystemColors.ControlText;
            this.uiLightEO.OnCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.uiLightEO.OnColor = System.Drawing.Color.Lime;
            this.uiLightEO.Radius = 23;
            this.uiLightEO.Size = new System.Drawing.Size(23, 23);
            this.uiLightEO.State = Sunny.UI.UILightState.Off;
            this.uiLightEO.TabIndex = 750;
            this.uiLightEO.Tag = "";
            this.uiLightEO.Text = "uiLight3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(31, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(180, 19);
            this.label6.TabIndex = 751;
            this.label6.Text = "预供机油循环【开】";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnDC24VClose);
            this.groupBox7.Controls.Add(this.btnDC24VOpen);
            this.groupBox7.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox7.Location = new System.Drawing.Point(517, 149);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(231, 102);
            this.groupBox7.TabIndex = 866;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "发动机控制盒电源DC24V";
            // 
            // btnDC24VClose
            // 
            this.btnDC24VClose.BackColor = System.Drawing.Color.Silver;
            this.btnDC24VClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDC24VClose.FalseColor = System.Drawing.Color.Silver;
            this.btnDC24VClose.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDC24VClose.Location = new System.Drawing.Point(121, 54);
            this.btnDC24VClose.Name = "btnDC24VClose";
            this.btnDC24VClose.OutputTagName = "发动机DC24V供电";
            this.btnDC24VClose.Size = new System.Drawing.Size(98, 38);
            this.btnDC24VClose.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnDC24VClose.TabIndex = 828;
            this.btnDC24VClose.Tag = "0";
            this.btnDC24VClose.Text = "关";
            this.btnDC24VClose.TrueColor = System.Drawing.Color.Lime;
            this.btnDC24VClose.Click += new System.EventHandler(this.sw_Valve_Click);
            // 
            // btnDC24VOpen
            // 
            this.btnDC24VOpen.AccessibleDescription = "";
            this.btnDC24VOpen.BackColor = System.Drawing.Color.Silver;
            this.btnDC24VOpen.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDC24VOpen.FalseColor = System.Drawing.Color.Silver;
            this.btnDC24VOpen.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDC24VOpen.Location = new System.Drawing.Point(12, 54);
            this.btnDC24VOpen.Name = "btnDC24VOpen";
            this.btnDC24VOpen.OutputTagName = "发动机DC24V供电";
            this.btnDC24VOpen.Size = new System.Drawing.Size(98, 38);
            this.btnDC24VOpen.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnDC24VOpen.TabIndex = 828;
            this.btnDC24VOpen.Tag = "1";
            this.btnDC24VOpen.Text = "开";
            this.btnDC24VOpen.TrueColor = System.Drawing.Color.Lime;
            this.btnDC24VOpen.Click += new System.EventHandler(this.sw_Valve_Click);
            // 
            // btnManualShake
            // 
            this.btnManualShake.BackColor = System.Drawing.Color.Silver;
            this.btnManualShake.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnManualShake.FalseColor = System.Drawing.Color.Silver;
            this.btnManualShake.Font = new System.Drawing.Font("宋体", 18F);
            this.btnManualShake.Location = new System.Drawing.Point(99, 134);
            this.btnManualShake.Name = "btnManualShake";
            this.btnManualShake.Size = new System.Drawing.Size(193, 47);
            this.btnManualShake.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnManualShake.TabIndex = 812;
            this.btnManualShake.Tag = "甩车";
            this.btnManualShake.Text = "长按 甩车";
            this.btnManualShake.TrueColor = System.Drawing.Color.Lime;
            this.btnManualShake.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnManualRun_MouseDown);
            this.btnManualShake.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnManualRun_MouseUp);
            // 
            // uiLightFuelCycleClose
            // 
            this.uiLightFuelCycleClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.uiLightFuelCycleClose.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.uiLightFuelCycleClose.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLightFuelCycleClose.Location = new System.Drawing.Point(4, 31);
            this.uiLightFuelCycleClose.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLightFuelCycleClose.Name = "uiLightFuelCycleClose";
            this.uiLightFuelCycleClose.OffCenterColor = System.Drawing.Color.Silver;
            this.uiLightFuelCycleClose.OffColor = System.Drawing.SystemColors.ControlText;
            this.uiLightFuelCycleClose.OnCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.uiLightFuelCycleClose.OnColor = System.Drawing.Color.Lime;
            this.uiLightFuelCycleClose.Radius = 23;
            this.uiLightFuelCycleClose.Size = new System.Drawing.Size(23, 23);
            this.uiLightFuelCycleClose.State = Sunny.UI.UILightState.Off;
            this.uiLightFuelCycleClose.TabIndex = 743;
            this.uiLightFuelCycleClose.Tag = "";
            this.uiLightFuelCycleClose.Text = "uiLight3";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 14F);
            this.label8.Location = new System.Drawing.Point(164, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 19);
            this.label8.TabIndex = 847;
            this.label8.Text = "甩车准备";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(24, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(142, 19);
            this.label9.TabIndex = 744;
            this.label9.Text = "燃油循环【关】";
            // 
            // uiLightShake
            // 
            this.uiLightShake.CenterColor = System.Drawing.Color.Lime;
            this.uiLightShake.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLightShake.Location = new System.Drawing.Point(141, 105);
            this.uiLightShake.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLightShake.Name = "uiLightShake";
            this.uiLightShake.OffCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.uiLightShake.OffColor = System.Drawing.Color.Red;
            this.uiLightShake.OnCenterColor = System.Drawing.Color.Lime;
            this.uiLightShake.OnColor = System.Drawing.Color.MediumSeaGreen;
            this.uiLightShake.Radius = 23;
            this.uiLightShake.Size = new System.Drawing.Size(23, 23);
            this.uiLightShake.State = Sunny.UI.UILightState.Off;
            this.uiLightShake.TabIndex = 846;
            this.uiLightShake.Tag = "";
            this.uiLightShake.Text = "uiLight3";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label26);
            this.groupBox8.Controls.Add(this.label31);
            this.groupBox8.Controls.Add(this.nudBeginInvertSpeed);
            this.groupBox8.Controls.Add(this.nudBeginCurrent);
            this.groupBox8.Controls.Add(this.btnSetBeginSpeed);
            this.groupBox8.Controls.Add(this.btnSetBeginLC);
            this.groupBox8.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox8.Location = new System.Drawing.Point(4, 270);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(249, 188);
            this.groupBox8.TabIndex = 867;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "启机参数设置";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("宋体", 14F);
            this.label26.Location = new System.Drawing.Point(15, 36);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(154, 19);
            this.label26.TabIndex = 819;
            this.label26.Text = "变频器转速(rpm)";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("宋体", 14F);
            this.label31.Location = new System.Drawing.Point(14, 116);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(115, 19);
            this.label31.TabIndex = 818;
            this.label31.Text = "励磁电流(A)";
            // 
            // nudBeginInvertSpeed
            // 
            this.nudBeginInvertSpeed.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nudBeginInvertSpeed.DecimalPlaces = 0;
            this.nudBeginInvertSpeed.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudBeginInvertSpeed.Location = new System.Drawing.Point(15, 62);
            this.nudBeginInvertSpeed.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudBeginInvertSpeed.Maximum = 150D;
            this.nudBeginInvertSpeed.Minimum = 0D;
            this.nudBeginInvertSpeed.MinimumSize = new System.Drawing.Size(100, 0);
            this.nudBeginInvertSpeed.Name = "nudBeginInvertSpeed";
            this.nudBeginInvertSpeed.Padding = new System.Windows.Forms.Padding(5);
            this.nudBeginInvertSpeed.ShowText = false;
            this.nudBeginInvertSpeed.Size = new System.Drawing.Size(128, 31);
            this.nudBeginInvertSpeed.Step = 1D;
            this.nudBeginInvertSpeed.TabIndex = 820;
            this.nudBeginInvertSpeed.Text = "0";
            this.nudBeginInvertSpeed.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.nudBeginInvertSpeed.Value = 0D;
            this.nudBeginInvertSpeed.ValueChanged += new Sunny.UI.UIDoubleUpDown.OnValueChanged(this.nudBeginInvertSpeed_ValueChanged);
            // 
            // nudBeginCurrent
            // 
            this.nudBeginCurrent.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nudBeginCurrent.DecimalPlaces = 1;
            this.nudBeginCurrent.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudBeginCurrent.Location = new System.Drawing.Point(15, 140);
            this.nudBeginCurrent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudBeginCurrent.Maximum = 200D;
            this.nudBeginCurrent.Minimum = 0D;
            this.nudBeginCurrent.MinimumSize = new System.Drawing.Size(100, 0);
            this.nudBeginCurrent.Name = "nudBeginCurrent";
            this.nudBeginCurrent.Padding = new System.Windows.Forms.Padding(5);
            this.nudBeginCurrent.ShowText = false;
            this.nudBeginCurrent.Size = new System.Drawing.Size(128, 31);
            this.nudBeginCurrent.Step = 1D;
            this.nudBeginCurrent.TabIndex = 821;
            this.nudBeginCurrent.Text = "0.0";
            this.nudBeginCurrent.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.nudBeginCurrent.Value = 0D;
            this.nudBeginCurrent.ValueChanged += new Sunny.UI.UIDoubleUpDown.OnValueChanged(this.nudBeginCurrent_ValueChanged);
            // 
            // btnSetBeginSpeed
            // 
            this.btnSetBeginSpeed.BackColor = System.Drawing.Color.Silver;
            this.btnSetBeginSpeed.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSetBeginSpeed.FalseColor = System.Drawing.Color.Silver;
            this.btnSetBeginSpeed.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetBeginSpeed.Location = new System.Drawing.Point(150, 60);
            this.btnSetBeginSpeed.Name = "btnSetBeginSpeed";
            this.btnSetBeginSpeed.Size = new System.Drawing.Size(81, 33);
            this.btnSetBeginSpeed.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnSetBeginSpeed.TabIndex = 822;
            this.btnSetBeginSpeed.Tag = "";
            this.btnSetBeginSpeed.Text = "设置";
            this.btnSetBeginSpeed.TrueColor = System.Drawing.Color.Lime;
            this.btnSetBeginSpeed.Click += new System.EventHandler(this.btnSetBeginSpeed_Click);
            // 
            // btnSetBeginLC
            // 
            this.btnSetBeginLC.BackColor = System.Drawing.Color.Silver;
            this.btnSetBeginLC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSetBeginLC.FalseColor = System.Drawing.Color.Silver;
            this.btnSetBeginLC.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetBeginLC.Location = new System.Drawing.Point(150, 138);
            this.btnSetBeginLC.Name = "btnSetBeginLC";
            this.btnSetBeginLC.Size = new System.Drawing.Size(81, 33);
            this.btnSetBeginLC.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnSetBeginLC.TabIndex = 823;
            this.btnSetBeginLC.Tag = "";
            this.btnSetBeginLC.Text = "设置";
            this.btnSetBeginLC.TrueColor = System.Drawing.Color.Lime;
            this.btnSetBeginLC.Click += new System.EventHandler(this.btnSetBeginLC_Click);
            // 
            // lblInverterVoltage
            // 
            this.lblInverterVoltage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblInverterVoltage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInverterVoltage.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInverterVoltage.ForeColor = System.Drawing.Color.Black;
            this.lblInverterVoltage.Location = new System.Drawing.Point(99, 30);
            this.lblInverterVoltage.Name = "lblInverterVoltage";
            this.lblInverterVoltage.Size = new System.Drawing.Size(90, 28);
            this.lblInverterVoltage.TabIndex = 853;
            this.lblInverterVoltage.Tag = "";
            this.lblInverterVoltage.Text = "0.0";
            this.lblInverterVoltage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerSlow
            // 
            this.timerSlow.Interval = 1000;
            this.timerSlow.Tick += new System.EventHandler(this.timerSlow_Tick);
            // 
            // timerFast
            // 
            this.timerFast.Interval = 500;
            this.timerFast.Tick += new System.EventHandler(this.timerFast_Tick);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.uiLightDV24Close);
            this.groupBox11.Controls.Add(this.label20);
            this.groupBox11.Controls.Add(this.btnManualShake);
            this.groupBox11.Controls.Add(this.uiLightFuelCycleClose);
            this.groupBox11.Controls.Add(this.label8);
            this.groupBox11.Controls.Add(this.label9);
            this.groupBox11.Controls.Add(this.uiLightShake);
            this.groupBox11.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox11.Location = new System.Drawing.Point(678, 270);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(383, 188);
            this.groupBox11.TabIndex = 870;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "甩车控制";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("宋体", 14F);
            this.label42.Location = new System.Drawing.Point(8, 101);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(85, 19);
            this.label42.TabIndex = 640;
            this.label42.Text = "输出功率";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LightInvertFault
            // 
            this.LightInvertFault.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.LightInvertFault.Font = new System.Drawing.Font("宋体", 14F);
            this.LightInvertFault.Location = new System.Drawing.Point(307, 33);
            this.LightInvertFault.MinimumSize = new System.Drawing.Size(1, 1);
            this.LightInvertFault.Name = "LightInvertFault";
            this.LightInvertFault.OffCenterColor = System.Drawing.Color.Silver;
            this.LightInvertFault.OffColor = System.Drawing.Color.Black;
            this.LightInvertFault.OnCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.LightInvertFault.OnColor = System.Drawing.Color.Red;
            this.LightInvertFault.Radius = 22;
            this.LightInvertFault.Size = new System.Drawing.Size(23, 22);
            this.LightInvertFault.State = Sunny.UI.UILightState.Off;
            this.LightInvertFault.TabIndex = 628;
            this.LightInvertFault.Tag = "";
            this.LightInvertFault.Text = "uiLight3";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("宋体", 14F);
            this.label39.Location = new System.Drawing.Point(8, 68);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(85, 19);
            this.label39.TabIndex = 638;
            this.label39.Text = "输出电流";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("宋体", 14F);
            this.label30.Location = new System.Drawing.Point(8, 35);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(85, 19);
            this.label30.TabIndex = 636;
            this.label30.Text = "输出电压";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("宋体", 14F);
            this.label29.Location = new System.Drawing.Point(324, 35);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(47, 19);
            this.label29.TabIndex = 629;
            this.label29.Text = "故障";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("宋体", 14F);
            this.label33.Location = new System.Drawing.Point(251, 65);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(66, 19);
            this.label33.TabIndex = 634;
            this.label33.Text = "运行中";
            // 
            // lblFaultCode
            // 
            this.lblFaultCode.AutoSize = true;
            this.lblFaultCode.Font = new System.Drawing.Font("宋体", 14F);
            this.lblFaultCode.ForeColor = System.Drawing.Color.Red;
            this.lblFaultCode.Location = new System.Drawing.Point(232, 101);
            this.lblFaultCode.Name = "lblFaultCode";
            this.lblFaultCode.Size = new System.Drawing.Size(105, 19);
            this.lblFaultCode.TabIndex = 631;
            this.lblFaultCode.Text = "故障代码:-";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.LightInvertRunning);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.lblInverterPower);
            this.groupBox4.Controls.Add(this.LightInvertReady);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.lblInverterCurrent);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.lblInverterVoltage);
            this.groupBox4.Controls.Add(this.label42);
            this.groupBox4.Controls.Add(this.LightInvertFault);
            this.groupBox4.Controls.Add(this.label39);
            this.groupBox4.Controls.Add(this.label30);
            this.groupBox4.Controls.Add(this.label29);
            this.groupBox4.Controls.Add(this.label33);
            this.groupBox4.Controls.Add(this.lblFaultCode);
            this.groupBox4.Font = new System.Drawing.Font("宋体", 14F);
            this.groupBox4.Location = new System.Drawing.Point(4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(378, 138);
            this.groupBox4.TabIndex = 863;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "启动柜输出";
            // 
            // btnFuelCycleClose
            // 
            this.btnFuelCycleClose.BackColor = System.Drawing.Color.Silver;
            this.btnFuelCycleClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFuelCycleClose.FalseColor = System.Drawing.Color.Silver;
            this.btnFuelCycleClose.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFuelCycleClose.Location = new System.Drawing.Point(121, 54);
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
            this.btnFuelCycleOpen.Location = new System.Drawing.Point(12, 54);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFuelCycleClose);
            this.groupBox1.Controls.Add(this.btnFuelCycleOpen);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(4, 149);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 102);
            this.groupBox1.TabIndex = 862;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "一键燃油循环";
            // 
            // btnEoOpen
            // 
            this.btnEoOpen.BackColor = System.Drawing.Color.Silver;
            this.btnEoOpen.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnEoOpen.FalseColor = System.Drawing.Color.Silver;
            this.btnEoOpen.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEoOpen.Location = new System.Drawing.Point(9, 54);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnEoClose);
            this.groupBox2.Controls.Add(this.btnEoOpen);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(259, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(233, 102);
            this.groupBox2.TabIndex = 861;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "一键预供机油循环";
            // 
            // btnEoClose
            // 
            this.btnEoClose.BackColor = System.Drawing.Color.Silver;
            this.btnEoClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnEoClose.FalseColor = System.Drawing.Color.Silver;
            this.btnEoClose.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEoClose.Location = new System.Drawing.Point(122, 54);
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
            // ucStartup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "ucStartup";
            this.Size = new System.Drawing.Size(1074, 532);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTorque;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPower;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label27;
        private Sunny.UI.UILight LightInvertReady;
        private System.Windows.Forms.Label lblInverterCurrent;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private Sunny.UI.UILight LightInvertRunning;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label LCCurrentValue;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblInverterPower;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label LCVoltageValue;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label15;
        private Sunny.UI.UILight uiLightDV24Close;
        private System.Windows.Forms.Label label20;
        private Sunny.UI.UILight uiLightDV24Open;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox groupBox10;
        private RW.UI.Controls.RButton btnManualStart;
        private Sunny.UI.UILight uiLightFuelCycle;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label1;
        private Sunny.UI.UILight uiLightStart;
        private System.Windows.Forms.GroupBox groupBox9;
        private Sunny.UI.UILight uiLightPC;
        private System.Windows.Forms.Label label3;
        private Sunny.UI.UILight uiLightWaterUP;
        private System.Windows.Forms.Label label10;
        private Sunny.UI.UILight uiLightEO;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox7;
        private RW.UI.Controls.RButton btnDC24VClose;
        private RW.UI.Controls.RButton btnDC24VOpen;
        private RW.UI.Controls.RButton btnManualShake;
        private Sunny.UI.UILight uiLightFuelCycleClose;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private Sunny.UI.UILight uiLightShake;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label31;
        private Sunny.UI.UIDoubleUpDown nudBeginInvertSpeed;
        private Sunny.UI.UIDoubleUpDown nudBeginCurrent;
        private RW.UI.Controls.RButton btnSetBeginSpeed;
        private RW.UI.Controls.RButton btnSetBeginLC;
        private System.Windows.Forms.Label lblInverterVoltage;
        private System.Windows.Forms.Timer timerSlow;
        private System.Windows.Forms.Timer timerFast;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Label label42;
        private Sunny.UI.UILight LightInvertFault;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label lblFaultCode;
        private System.Windows.Forms.GroupBox groupBox4;
        private RW.UI.Controls.RButton btnFuelCycleClose;
        private RW.UI.Controls.RButton btnFuelCycleOpen;
        private System.Windows.Forms.GroupBox groupBox1;
        private RW.UI.Controls.RButton btnEoOpen;
        private System.Windows.Forms.GroupBox groupBox2;
        private RW.UI.Controls.RButton btnEoClose;
    }
}

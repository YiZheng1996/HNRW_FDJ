namespace RW.Testbed.UI.Equip
{
    partial class ucTH9320
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpJYpara = new System.Windows.Forms.GroupBox();
            this.nudJYDZlow = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudJYDZtime = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudJYDZVol = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnJYStart = new System.Windows.Forms.Button();
            this.btnJYStop = new System.Windows.Forms.Button();
            this.serialPortTH9320 = new System.IO.Ports.SerialPort(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtRvalue = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDJS_JY = new System.Windows.Forms.Label();
            this.lblJYDZrst = new System.Windows.Forms.TextBox();
            this.lblJYDZState = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.grpJYDZ = new System.Windows.Forms.GroupBox();
            this.grpNY = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnNYstop = new System.Windows.Forms.Button();
            this.btnNYStart = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtIvalue = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblDJS_NY = new System.Windows.Forms.Label();
            this.lblNYrst = new System.Windows.Forms.TextBox();
            this.lblNYstate = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.grpNYpara = new System.Windows.Forms.GroupBox();
            this.nudNYLDLUP = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.nudNYtime = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.nudNYVol = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.rbnJYDZ = new System.Windows.Forms.RadioButton();
            this.rbnJYNY = new System.Windows.Forms.RadioButton();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.timerJYDZ = new System.Windows.Forms.Timer(this.components);
            this.timerNY = new System.Windows.Forms.Timer(this.components);
            this.grpJYpara.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudJYDZlow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudJYDZtime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudJYDZVol)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpJYDZ.SuspendLayout();
            this.grpNY.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.grpNYpara.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNYLDLUP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNYtime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNYVol)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 20F);
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(927, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "绝缘测试";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpJYpara
            // 
            this.grpJYpara.Controls.Add(this.nudJYDZlow);
            this.grpJYpara.Controls.Add(this.label3);
            this.grpJYpara.Controls.Add(this.nudJYDZtime);
            this.grpJYpara.Controls.Add(this.label2);
            this.grpJYpara.Controls.Add(this.nudJYDZVol);
            this.grpJYpara.Controls.Add(this.label1);
            this.grpJYpara.Location = new System.Drawing.Point(22, 22);
            this.grpJYpara.Name = "grpJYpara";
            this.grpJYpara.Size = new System.Drawing.Size(293, 186);
            this.grpJYpara.TabIndex = 1;
            this.grpJYpara.TabStop = false;
            this.grpJYpara.Text = "绝缘电阻试验参数";
            // 
            // nudJYDZlow
            // 
            this.nudJYDZlow.Location = new System.Drawing.Point(146, 141);
            this.nudJYDZlow.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.nudJYDZlow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudJYDZlow.Name = "nudJYDZlow";
            this.nudJYDZlow.Size = new System.Drawing.Size(105, 23);
            this.nudJYDZlow.TabIndex = 2;
            this.nudJYDZlow.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 14);
            this.label3.TabIndex = 1;
            this.label3.Text = "电阻下限(MΩ)";
            // 
            // nudJYDZtime
            // 
            this.nudJYDZtime.Location = new System.Drawing.Point(146, 86);
            this.nudJYDZtime.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.nudJYDZtime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudJYDZtime.Name = "nudJYDZtime";
            this.nudJYDZtime.Size = new System.Drawing.Size(105, 23);
            this.nudJYDZtime.TabIndex = 2;
            this.nudJYDZtime.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "测试时间(S)";
            // 
            // nudJYDZVol
            // 
            this.nudJYDZVol.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudJYDZVol.Location = new System.Drawing.Point(146, 34);
            this.nudJYDZVol.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudJYDZVol.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudJYDZVol.Name = "nudJYDZVol";
            this.nudJYDZVol.Size = new System.Drawing.Size(105, 23);
            this.nudJYDZVol.TabIndex = 2;
            this.nudJYDZVol.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "测试电压DC(V)";
            // 
            // btnJYStart
            // 
            this.btnJYStart.Location = new System.Drawing.Point(18, 19);
            this.btnJYStart.Name = "btnJYStart";
            this.btnJYStart.Size = new System.Drawing.Size(128, 41);
            this.btnJYStart.TabIndex = 0;
            this.btnJYStart.Text = "开始绝缘电阻试验";
            this.btnJYStart.UseVisualStyleBackColor = true;
            this.btnJYStart.Click += new System.EventHandler(this.btnJYStart_Click);
            // 
            // btnJYStop
            // 
            this.btnJYStop.Location = new System.Drawing.Point(164, 19);
            this.btnJYStop.Name = "btnJYStop";
            this.btnJYStop.Size = new System.Drawing.Size(128, 41);
            this.btnJYStop.TabIndex = 0;
            this.btnJYStop.Text = "停止绝缘电阻测试";
            this.btnJYStop.UseVisualStyleBackColor = true;
            this.btnJYStop.Click += new System.EventHandler(this.btnJYStop_Click);
            // 
            // serialPortTH9320
            // 
            this.serialPortTH9320.PortName = "COM3";
            this.serialPortTH9320.ReadTimeout = 2000;
            this.serialPortTH9320.WriteTimeout = 2000;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnJYStop);
            this.panel1.Controls.Add(this.btnJYStart);
            this.panel1.Location = new System.Drawing.Point(13, 254);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(310, 70);
            this.panel1.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtRvalue);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lblDJS_JY);
            this.groupBox2.Controls.Add(this.lblJYDZrst);
            this.groupBox2.Controls.Add(this.lblJYDZState);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(13, 336);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(310, 144);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "测试结果";
            // 
            // txtRvalue
            // 
            this.txtRvalue.Location = new System.Drawing.Point(155, 78);
            this.txtRvalue.Name = "txtRvalue";
            this.txtRvalue.ReadOnly = true;
            this.txtRvalue.Size = new System.Drawing.Size(95, 23);
            this.txtRvalue.TabIndex = 7;
            this.txtRvalue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 14);
            this.label6.TabIndex = 6;
            this.label6.Text = "绝缘电阻测试值：";
            // 
            // lblDJS_JY
            // 
            this.lblDJS_JY.AutoSize = true;
            this.lblDJS_JY.Font = new System.Drawing.Font("宋体", 11F);
            this.lblDJS_JY.Location = new System.Drawing.Point(155, 19);
            this.lblDJS_JY.Name = "lblDJS_JY";
            this.lblDJS_JY.Size = new System.Drawing.Size(131, 15);
            this.lblDJS_JY.TabIndex = 5;
            this.lblDJS_JY.Text = "倒计时：00:00:00";
            this.lblDJS_JY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblJYDZrst
            // 
            this.lblJYDZrst.Location = new System.Drawing.Point(155, 112);
            this.lblJYDZrst.Name = "lblJYDZrst";
            this.lblJYDZrst.ReadOnly = true;
            this.lblJYDZrst.Size = new System.Drawing.Size(137, 23);
            this.lblJYDZrst.TabIndex = 4;
            this.lblJYDZrst.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblJYDZState
            // 
            this.lblJYDZState.BackColor = System.Drawing.SystemColors.Control;
            this.lblJYDZState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblJYDZState.Location = new System.Drawing.Point(155, 39);
            this.lblJYDZState.Name = "lblJYDZState";
            this.lblJYDZState.Size = new System.Drawing.Size(137, 24);
            this.lblJYDZState.TabIndex = 3;
            this.lblJYDZState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 14);
            this.label5.TabIndex = 2;
            this.label5.Text = "绝缘电阻测试结果：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "绝缘测试状态：";
            // 
            // grpJYDZ
            // 
            this.grpJYDZ.Controls.Add(this.panel1);
            this.grpJYDZ.Controls.Add(this.groupBox2);
            this.grpJYDZ.Controls.Add(this.grpJYpara);
            this.grpJYDZ.Location = new System.Drawing.Point(34, 37);
            this.grpJYDZ.Name = "grpJYDZ";
            this.grpJYDZ.Size = new System.Drawing.Size(337, 499);
            this.grpJYDZ.TabIndex = 3;
            this.grpJYDZ.TabStop = false;
            this.grpJYDZ.Text = "绝缘电阻测试";
            // 
            // grpNY
            // 
            this.grpNY.Controls.Add(this.panel2);
            this.grpNY.Controls.Add(this.groupBox5);
            this.grpNY.Controls.Add(this.grpNYpara);
            this.grpNY.Location = new System.Drawing.Point(393, 37);
            this.grpNY.Name = "grpNY";
            this.grpNY.Size = new System.Drawing.Size(337, 499);
            this.grpNY.TabIndex = 3;
            this.grpNY.TabStop = false;
            this.grpNY.Text = "绝缘耐压测试";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnNYstop);
            this.panel2.Controls.Add(this.btnNYStart);
            this.panel2.Location = new System.Drawing.Point(19, 254);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(298, 70);
            this.panel2.TabIndex = 2;
            // 
            // btnNYstop
            // 
            this.btnNYstop.Location = new System.Drawing.Point(158, 19);
            this.btnNYstop.Name = "btnNYstop";
            this.btnNYstop.Size = new System.Drawing.Size(128, 41);
            this.btnNYstop.TabIndex = 0;
            this.btnNYstop.Text = "停止绝缘耐压测试";
            this.btnNYstop.UseVisualStyleBackColor = true;
            this.btnNYstop.Click += new System.EventHandler(this.btnNYstop_Click);
            // 
            // btnNYStart
            // 
            this.btnNYStart.Location = new System.Drawing.Point(12, 19);
            this.btnNYStart.Name = "btnNYStart";
            this.btnNYStart.Size = new System.Drawing.Size(128, 41);
            this.btnNYStart.TabIndex = 0;
            this.btnNYStart.Text = "开始绝缘耐压试验";
            this.btnNYStart.UseVisualStyleBackColor = true;
            this.btnNYStart.Click += new System.EventHandler(this.btnNYStart_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtIvalue);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.lblDJS_NY);
            this.groupBox5.Controls.Add(this.lblNYrst);
            this.groupBox5.Controls.Add(this.lblNYstate);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Location = new System.Drawing.Point(9, 336);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(319, 144);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "测试结果";
            this.groupBox5.Enter += new System.EventHandler(this.groupBox5_Enter);
            // 
            // txtIvalue
            // 
            this.txtIvalue.Location = new System.Drawing.Point(166, 77);
            this.txtIvalue.Name = "txtIvalue";
            this.txtIvalue.ReadOnly = true;
            this.txtIvalue.Size = new System.Drawing.Size(95, 23);
            this.txtIvalue.TabIndex = 8;
            this.txtIvalue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(41, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 14);
            this.label7.TabIndex = 7;
            this.label7.Text = "绝缘耐压测试值：";
            // 
            // lblDJS_NY
            // 
            this.lblDJS_NY.AutoSize = true;
            this.lblDJS_NY.Font = new System.Drawing.Font("宋体", 11F);
            this.lblDJS_NY.Location = new System.Drawing.Point(167, 19);
            this.lblDJS_NY.Name = "lblDJS_NY";
            this.lblDJS_NY.Size = new System.Drawing.Size(131, 15);
            this.lblDJS_NY.TabIndex = 6;
            this.lblDJS_NY.Text = "倒计时：00:00:00";
            this.lblDJS_NY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNYrst
            // 
            this.lblNYrst.Location = new System.Drawing.Point(166, 112);
            this.lblNYrst.Name = "lblNYrst";
            this.lblNYrst.ReadOnly = true;
            this.lblNYrst.Size = new System.Drawing.Size(137, 23);
            this.lblNYrst.TabIndex = 5;
            this.lblNYrst.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblNYstate
            // 
            this.lblNYstate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNYstate.Location = new System.Drawing.Point(166, 39);
            this.lblNYstate.Name = "lblNYstate";
            this.lblNYstate.Size = new System.Drawing.Size(137, 24);
            this.lblNYstate.TabIndex = 3;
            this.lblNYstate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 118);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 14);
            this.label8.TabIndex = 2;
            this.label8.Text = "绝缘耐压测试结果：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(59, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 14);
            this.label9.TabIndex = 2;
            this.label9.Text = "绝缘测试状态：";
            // 
            // grpNYpara
            // 
            this.grpNYpara.Controls.Add(this.nudNYLDLUP);
            this.grpNYpara.Controls.Add(this.label10);
            this.grpNYpara.Controls.Add(this.nudNYtime);
            this.grpNYpara.Controls.Add(this.label11);
            this.grpNYpara.Controls.Add(this.nudNYVol);
            this.grpNYpara.Controls.Add(this.label12);
            this.grpNYpara.Location = new System.Drawing.Point(19, 22);
            this.grpNYpara.Name = "grpNYpara";
            this.grpNYpara.Size = new System.Drawing.Size(298, 186);
            this.grpNYpara.TabIndex = 1;
            this.grpNYpara.TabStop = false;
            this.grpNYpara.Text = "绝缘耐压试验参数";
            // 
            // nudNYLDLUP
            // 
            this.nudNYLDLUP.Location = new System.Drawing.Point(156, 146);
            this.nudNYLDLUP.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudNYLDLUP.Name = "nudNYLDLUP";
            this.nudNYLDLUP.Size = new System.Drawing.Size(105, 23);
            this.nudNYLDLUP.TabIndex = 2;
            this.nudNYLDLUP.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(45, 150);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(105, 14);
            this.label10.TabIndex = 1;
            this.label10.Text = "漏电流上限(mA)";
            // 
            // nudNYtime
            // 
            this.nudNYtime.Location = new System.Drawing.Point(156, 86);
            this.nudNYtime.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.nudNYtime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNYtime.Name = "nudNYtime";
            this.nudNYtime.Size = new System.Drawing.Size(105, 23);
            this.nudNYtime.TabIndex = 2;
            this.nudNYtime.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(66, 91);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 14);
            this.label11.TabIndex = 1;
            this.label11.Text = "测试时间(S)";
            // 
            // nudNYVol
            // 
            this.nudNYVol.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudNYVol.Location = new System.Drawing.Point(156, 34);
            this.nudNYVol.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudNYVol.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudNYVol.Name = "nudNYVol";
            this.nudNYVol.Size = new System.Drawing.Size(105, 23);
            this.nudNYVol.TabIndex = 2;
            this.nudNYVol.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(38, 38);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(112, 14);
            this.label12.TabIndex = 1;
            this.label12.Text = "测试电压AC（V）";
            // 
            // rbnJYDZ
            // 
            this.rbnJYDZ.AutoSize = true;
            this.rbnJYDZ.Font = new System.Drawing.Font("宋体", 13F);
            this.rbnJYDZ.Location = new System.Drawing.Point(131, 5);
            this.rbnJYDZ.Name = "rbnJYDZ";
            this.rbnJYDZ.Size = new System.Drawing.Size(134, 22);
            this.rbnJYDZ.TabIndex = 4;
            this.rbnJYDZ.TabStop = true;
            this.rbnJYDZ.Text = "绝缘电阻测试";
            this.rbnJYDZ.UseVisualStyleBackColor = true;
            this.rbnJYDZ.CheckedChanged += new System.EventHandler(this.rbnJYDZ_CheckedChanged);
            // 
            // rbnJYNY
            // 
            this.rbnJYNY.AutoSize = true;
            this.rbnJYNY.Font = new System.Drawing.Font("宋体", 13F);
            this.rbnJYNY.Location = new System.Drawing.Point(499, 5);
            this.rbnJYNY.Name = "rbnJYNY";
            this.rbnJYNY.Size = new System.Drawing.Size(134, 22);
            this.rbnJYNY.TabIndex = 4;
            this.rbnJYNY.TabStop = true;
            this.rbnJYNY.Text = "绝缘耐压测试";
            this.rbnJYNY.UseVisualStyleBackColor = true;
            this.rbnJYNY.CheckedChanged += new System.EventHandler(this.rbnJYDZ_CheckedChanged);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(302, 544);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(140, 41);
            this.btnExportExcel.TabIndex = 5;
            this.btnExportExcel.Text = "导出到Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnSaveRst_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.btnExportExcel);
            this.panel3.Controls.Add(this.rbnJYNY);
            this.panel3.Controls.Add(this.rbnJYDZ);
            this.panel3.Controls.Add(this.grpNY);
            this.panel3.Controls.Add(this.grpJYDZ);
            this.panel3.Location = new System.Drawing.Point(81, 43);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(765, 605);
            this.panel3.TabIndex = 6;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(297, 455);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(42, 14);
            this.label14.TabIndex = 8;
            this.label14.Text = "(MΩ)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(663, 453);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 14);
            this.label13.TabIndex = 9;
            this.label13.Text = "(mA)";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // timerJYDZ
            // 
            this.timerJYDZ.Interval = 1000;
            this.timerJYDZ.Tick += new System.EventHandler(this.timerJYDZ_Tick);
            // 
            // timerNY
            // 
            this.timerNY.Interval = 1000;
            this.timerNY.Tick += new System.EventHandler(this.timerNY_Tick);
            // 
            // ucTH9320
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.Name = "ucTH9320";
            this.Size = new System.Drawing.Size(927, 668);
            this.Load += new System.EventHandler(this.ucJYNYTest_Load);
            this.grpJYpara.ResumeLayout(false);
            this.grpJYpara.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudJYDZlow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudJYDZtime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudJYDZVol)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpJYDZ.ResumeLayout(false);
            this.grpNY.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.grpNYpara.ResumeLayout(false);
            this.grpNYpara.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNYLDLUP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNYtime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNYVol)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpJYpara;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnJYStart;
        private System.Windows.Forms.Button btnJYStop;
        private System.IO.Ports.SerialPort serialPortTH9320;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblJYDZState;
        private System.Windows.Forms.GroupBox grpJYDZ;
        private System.Windows.Forms.GroupBox grpNY;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnNYstop;
        private System.Windows.Forms.Button btnNYStart;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblNYstate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox grpNYpara;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton rbnJYDZ;
        private System.Windows.Forms.RadioButton rbnJYNY;
        private System.Windows.Forms.Button btnExportExcel;
        public System.Windows.Forms.NumericUpDown nudJYDZVol;
        public System.Windows.Forms.NumericUpDown nudJYDZtime;
        public System.Windows.Forms.NumericUpDown nudJYDZlow;
        public System.Windows.Forms.NumericUpDown nudNYLDLUP;
        public System.Windows.Forms.NumericUpDown nudNYtime;
        public System.Windows.Forms.NumericUpDown nudNYVol;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox lblJYDZrst;
        private System.Windows.Forms.TextBox lblNYrst;
        private System.Windows.Forms.Timer timerJYDZ;
        private System.Windows.Forms.Timer timerNY;
        private System.Windows.Forms.Label lblDJS_JY;
        private System.Windows.Forms.Label lblDJS_NY;
        private System.Windows.Forms.TextBox txtRvalue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtIvalue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
    }
}

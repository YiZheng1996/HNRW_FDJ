namespace MainUI.Widget
{
    partial class ucGD350
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
            this.btnRun = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblVoltage = new System.Windows.Forms.Label();
            this.lblFreq = new System.Windows.Forms.Label();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.btnFreq = new System.Windows.Forms.Button();
            this.numFreq = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.ltReady = new Sunny.UI.UILight();
            this.label1 = new System.Windows.Forms.Label();
            this.ltFault = new Sunny.UI.UILight();
            this.label12 = new System.Windows.Forms.Label();
            this.ltRun = new Sunny.UI.UILight();
            this.label13 = new System.Windows.Forms.Label();
            this.ltCW = new Sunny.UI.UILight();
            this.label14 = new System.Windows.Forms.Label();
            this.ltCCW = new Sunny.UI.UILight();
            this.rbnCW = new System.Windows.Forms.RadioButton();
            this.rbnCCW = new System.Windows.Forms.RadioButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblFault = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.itUnReady = new Sunny.UI.UILight();
            this.label4 = new System.Windows.Forms.Label();
            this.lblOutputPower = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numFreq)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.SystemColors.Control;
            this.btnRun.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.btnRun.Location = new System.Drawing.Point(169, 251);
            this.btnRun.Margin = new System.Windows.Forms.Padding(5);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(100, 43);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "启动";
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.btnStop.Location = new System.Drawing.Point(283, 251);
            this.btnStop.Margin = new System.Windows.Forms.Padding(5);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 43);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 126);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 21);
            this.label2.TabIndex = 12;
            this.label2.Text = "转速(rpm)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 165);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 21);
            this.label7.TabIndex = 9;
            this.label7.Text = "电流(A)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 196);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 21);
            this.label6.TabIndex = 10;
            this.label6.Text = "频率(Hz)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 87);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 21);
            this.label5.TabIndex = 11;
            this.label5.Text = "电压(V)";
            // 
            // lblVoltage
            // 
            this.lblVoltage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVoltage.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lblVoltage.Location = new System.Drawing.Point(98, 84);
            this.lblVoltage.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblVoltage.Name = "lblVoltage";
            this.lblVoltage.Size = new System.Drawing.Size(83, 28);
            this.lblVoltage.TabIndex = 11;
            this.lblVoltage.Text = "0.0";
            this.lblVoltage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFreq
            // 
            this.lblFreq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFreq.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lblFreq.Location = new System.Drawing.Point(98, 197);
            this.lblFreq.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblFreq.Name = "lblFreq";
            this.lblFreq.Size = new System.Drawing.Size(83, 28);
            this.lblFreq.TabIndex = 10;
            this.lblFreq.Text = "0.0";
            this.lblFreq.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrent
            // 
            this.lblCurrent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrent.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lblCurrent.Location = new System.Drawing.Point(98, 160);
            this.lblCurrent.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(83, 28);
            this.lblCurrent.TabIndex = 9;
            this.lblCurrent.Text = "0.0";
            this.lblCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSpeed
            // 
            this.lblSpeed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSpeed.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lblSpeed.Location = new System.Drawing.Point(98, 121);
            this.lblSpeed.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(83, 28);
            this.lblSpeed.TabIndex = 12;
            this.lblSpeed.Text = "0.0";
            this.lblSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnFreq
            // 
            this.btnFreq.Location = new System.Drawing.Point(283, 189);
            this.btnFreq.Margin = new System.Windows.Forms.Padding(5);
            this.btnFreq.Name = "btnFreq";
            this.btnFreq.Size = new System.Drawing.Size(100, 35);
            this.btnFreq.TabIndex = 0;
            this.btnFreq.Text = "确定";
            this.btnFreq.UseVisualStyleBackColor = true;
            this.btnFreq.Click += new System.EventHandler(this.btnFreq_Click);
            // 
            // numFreq
            // 
            this.numFreq.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.numFreq.Location = new System.Drawing.Point(191, 191);
            this.numFreq.Name = "numFreq";
            this.numFreq.Size = new System.Drawing.Size(84, 34);
            this.numFreq.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(49, 8);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 21);
            this.label10.TabIndex = 15;
            this.label10.Text = "就绪";
            // 
            // ltReady
            // 
            this.ltReady.CenterColor = System.Drawing.Color.Green;
            this.ltReady.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ltReady.Location = new System.Drawing.Point(11, 0);
            this.ltReady.MinimumSize = new System.Drawing.Size(1, 1);
            this.ltReady.Name = "ltReady";
            this.ltReady.OnCenterColor = System.Drawing.Color.Green;
            this.ltReady.OnColor = System.Drawing.Color.Green;
            this.ltReady.Radius = 35;
            this.ltReady.Size = new System.Drawing.Size(35, 38);
            this.ltReady.State = Sunny.UI.UILightState.Off;
            this.ltReady.StyleCustomMode = true;
            this.ltReady.TabIndex = 16;
            this.ltReady.Tag = "1";
            this.ltReady.Text = "uiLight1";
            this.ltReady.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(333, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 21);
            this.label1.TabIndex = 15;
            this.label1.Text = "停机";
            // 
            // ltFault
            // 
            this.ltFault.CenterColor = System.Drawing.Color.Red;
            this.ltFault.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ltFault.Location = new System.Drawing.Point(295, -1);
            this.ltFault.MinimumSize = new System.Drawing.Size(1, 1);
            this.ltFault.Name = "ltFault";
            this.ltFault.OnCenterColor = System.Drawing.Color.Red;
            this.ltFault.OnColor = System.Drawing.Color.Red;
            this.ltFault.Radius = 35;
            this.ltFault.Size = new System.Drawing.Size(35, 38);
            this.ltFault.State = Sunny.UI.UILightState.Off;
            this.ltFault.StyleCustomMode = true;
            this.ltFault.TabIndex = 16;
            this.ltFault.Tag = "1";
            this.ltFault.Text = "uiLight1";
            this.ltFault.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(49, 43);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 21);
            this.label12.TabIndex = 15;
            this.label12.Text = "运行状态";
            // 
            // ltRun
            // 
            this.ltRun.CenterColor = System.Drawing.Color.Green;
            this.ltRun.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ltRun.Location = new System.Drawing.Point(11, 35);
            this.ltRun.MinimumSize = new System.Drawing.Size(1, 1);
            this.ltRun.Name = "ltRun";
            this.ltRun.OnCenterColor = System.Drawing.Color.Green;
            this.ltRun.OnColor = System.Drawing.Color.Green;
            this.ltRun.Radius = 35;
            this.ltRun.Size = new System.Drawing.Size(35, 38);
            this.ltRun.State = Sunny.UI.UILightState.Off;
            this.ltRun.StyleCustomMode = true;
            this.ltRun.TabIndex = 16;
            this.ltRun.Tag = "1";
            this.ltRun.Text = "uiLight1";
            this.ltRun.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(194, 43);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 21);
            this.label13.TabIndex = 15;
            this.label13.Text = "正转";
            // 
            // ltCW
            // 
            this.ltCW.CenterColor = System.Drawing.Color.Green;
            this.ltCW.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ltCW.Location = new System.Drawing.Point(156, 35);
            this.ltCW.MinimumSize = new System.Drawing.Size(1, 1);
            this.ltCW.Name = "ltCW";
            this.ltCW.OnCenterColor = System.Drawing.Color.Green;
            this.ltCW.OnColor = System.Drawing.Color.Green;
            this.ltCW.Radius = 35;
            this.ltCW.Size = new System.Drawing.Size(35, 38);
            this.ltCW.State = Sunny.UI.UILightState.Off;
            this.ltCW.StyleCustomMode = true;
            this.ltCW.TabIndex = 16;
            this.ltCW.Tag = "1";
            this.ltCW.Text = "uiLight1";
            this.ltCW.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(333, 43);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(42, 21);
            this.label14.TabIndex = 15;
            this.label14.Text = "反转";
            // 
            // ltCCW
            // 
            this.ltCCW.CenterColor = System.Drawing.Color.Green;
            this.ltCCW.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ltCCW.Location = new System.Drawing.Point(295, 35);
            this.ltCCW.MinimumSize = new System.Drawing.Size(1, 1);
            this.ltCCW.Name = "ltCCW";
            this.ltCCW.OnCenterColor = System.Drawing.Color.Green;
            this.ltCCW.OnColor = System.Drawing.Color.Green;
            this.ltCCW.Radius = 35;
            this.ltCCW.Size = new System.Drawing.Size(35, 38);
            this.ltCCW.State = Sunny.UI.UILightState.Off;
            this.ltCCW.StyleCustomMode = true;
            this.ltCCW.TabIndex = 16;
            this.ltCCW.Tag = "1";
            this.ltCCW.Text = "uiLight1";
            this.ltCCW.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // rbnCW
            // 
            this.rbnCW.AutoSize = true;
            this.rbnCW.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.rbnCW.Location = new System.Drawing.Point(12, 257);
            this.rbnCW.Name = "rbnCW";
            this.rbnCW.Size = new System.Drawing.Size(70, 31);
            this.rbnCW.TabIndex = 17;
            this.rbnCW.TabStop = true;
            this.rbnCW.Text = "正转";
            this.rbnCW.UseVisualStyleBackColor = true;
            this.rbnCW.CheckedChanged += new System.EventHandler(this.rbnCW_CheckedChanged);
            // 
            // rbnCCW
            // 
            this.rbnCCW.AutoSize = true;
            this.rbnCCW.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.rbnCCW.Location = new System.Drawing.Point(87, 258);
            this.rbnCCW.Name = "rbnCCW";
            this.rbnCCW.Size = new System.Drawing.Size(70, 31);
            this.rbnCCW.TabIndex = 18;
            this.rbnCCW.TabStop = true;
            this.rbnCCW.Text = "反转";
            this.rbnCCW.UseVisualStyleBackColor = true;
            this.rbnCCW.CheckedChanged += new System.EventHandler(this.rbnCW_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblFault
            // 
            this.lblFault.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFault.ForeColor = System.Drawing.Color.Firebrick;
            this.lblFault.Location = new System.Drawing.Point(3, 225);
            this.lblFault.Name = "lblFault";
            this.lblFault.Size = new System.Drawing.Size(391, 20);
            this.lblFault.TabIndex = 19;
            this.lblFault.Text = "故障代码:";
            this.lblFault.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(194, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 21);
            this.label3.TabIndex = 15;
            this.label3.Text = "未就绪";
            // 
            // itUnReady
            // 
            this.itUnReady.CenterColor = System.Drawing.Color.Green;
            this.itUnReady.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.itUnReady.Location = new System.Drawing.Point(156, -1);
            this.itUnReady.MinimumSize = new System.Drawing.Size(1, 1);
            this.itUnReady.Name = "itUnReady";
            this.itUnReady.OnCenterColor = System.Drawing.Color.Green;
            this.itUnReady.OnColor = System.Drawing.Color.Green;
            this.itUnReady.Radius = 35;
            this.itUnReady.Size = new System.Drawing.Size(35, 38);
            this.itUnReady.State = Sunny.UI.UILightState.Off;
            this.itUnReady.StyleCustomMode = true;
            this.itUnReady.TabIndex = 16;
            this.itUnReady.Tag = "1";
            this.itUnReady.Text = "uiLight1";
            this.itUnReady.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(223, 90);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 21);
            this.label4.TabIndex = 11;
            this.label4.Text = "功率(W)";
            // 
            // lblOutputPower
            // 
            this.lblOutputPower.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOutputPower.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lblOutputPower.Location = new System.Drawing.Point(295, 87);
            this.lblOutputPower.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblOutputPower.Name = "lblOutputPower";
            this.lblOutputPower.Size = new System.Drawing.Size(83, 28);
            this.lblOutputPower.TabIndex = 11;
            this.lblOutputPower.Text = "0.0";
            this.lblOutputPower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucGD350
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.lblFault);
            this.Controls.Add(this.rbnCCW);
            this.Controls.Add(this.rbnCW);
            this.Controls.Add(this.ltCCW);
            this.Controls.Add(this.ltFault);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ltCW);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.itUnReady);
            this.Controls.Add(this.ltReady);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ltRun);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.numFreq);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCurrent);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblFreq);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblOutputPower);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblVoltage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnFreq);
            this.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ucGD350";
            this.Size = new System.Drawing.Size(396, 306);
            this.Load += new System.EventHandler(this.ucGD350_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numFreq)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblVoltage;
        private System.Windows.Forms.Label lblFreq;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Button btnFreq;
        private System.Windows.Forms.NumericUpDown numFreq;
        private System.Windows.Forms.Label label10;
        private Sunny.UI.UILight ltReady;
        private System.Windows.Forms.Label label1;
        private Sunny.UI.UILight ltFault;
        private System.Windows.Forms.Label label12;
        private Sunny.UI.UILight ltRun;
        private System.Windows.Forms.Label label13;
        private Sunny.UI.UILight ltCW;
        private System.Windows.Forms.Label label14;
        private Sunny.UI.UILight ltCCW;
        private System.Windows.Forms.RadioButton rbnCW;
        private System.Windows.Forms.RadioButton rbnCCW;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblFault;
        private System.Windows.Forms.Label label3;
        private Sunny.UI.UILight itUnReady;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblOutputPower;
    }
}

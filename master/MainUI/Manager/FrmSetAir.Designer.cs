namespace RW.ModuleTest.UI.Manager
{
    partial class FrmSetAir
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.numAOValue = new System.Windows.Forms.NumericUpDown();
            this.Btn_Exit = new System.Windows.Forms.Button();
            this.Btn_Ok = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAOValue)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Controls.Add(this.numAOValue);
            this.groupBox1.Location = new System.Drawing.Point(27, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(495, 185);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "压力值设定(KPa)";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(51, 122);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(401, 45);
            this.trackBar1.TabIndex = 5;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // numAOValue
            // 
            this.numAOValue.DecimalPlaces = 1;
            this.numAOValue.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numAOValue.Location = new System.Drawing.Point(51, 63);
            this.numAOValue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numAOValue.Name = "numAOValue";
            this.numAOValue.Size = new System.Drawing.Size(401, 47);
            this.numAOValue.TabIndex = 1;
            this.numAOValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numAOValue.ValueChanged += new System.EventHandler(this.NpdAir_ValueChanged);
            this.numAOValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numAOValue_KeyPress);
            // 
            // Btn_Exit
            // 
            this.Btn_Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Btn_Exit.Location = new System.Drawing.Point(436, 233);
            this.Btn_Exit.Name = "Btn_Exit";
            this.Btn_Exit.Size = new System.Drawing.Size(85, 38);
            this.Btn_Exit.TabIndex = 4;
            this.Btn_Exit.Text = "取消";
            this.Btn_Exit.UseVisualStyleBackColor = true;
            // 
            // Btn_Ok
            // 
            this.Btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_Ok.Location = new System.Drawing.Point(317, 233);
            this.Btn_Ok.Name = "Btn_Ok";
            this.Btn_Ok.Size = new System.Drawing.Size(85, 38);
            this.Btn_Ok.TabIndex = 3;
            this.Btn_Ok.Text = "确定";
            this.Btn_Ok.UseVisualStyleBackColor = true;
            this.Btn_Ok.Click += new System.EventHandler(this.Btn_Ok_Click);
            // 
            // FrmSetAir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 286);
            this.ControlBox = false;
            this.Controls.Add(this.Btn_Exit);
            this.Controls.Add(this.Btn_Ok);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmSetAir";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "输出值设定";
            this.Load += new System.EventHandler(this.FrmSetAir_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAOValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Btn_Exit;
        private System.Windows.Forms.Button Btn_Ok;
        public System.Windows.Forms.NumericUpDown numAOValue;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
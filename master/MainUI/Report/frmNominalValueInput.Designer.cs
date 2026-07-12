namespace MainUI.Report
{
    partial class frmNominalValueInput
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblNominalRPM;
        private System.Windows.Forms.TextBox txtNominalRPM;
        private System.Windows.Forms.Label lblNominalPower;
        private System.Windows.Forms.TextBox txtNominalPower;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblNominalRPM = new System.Windows.Forms.Label();
            this.txtNominalRPM = new System.Windows.Forms.TextBox();
            this.lblNominalPower = new System.Windows.Forms.Label();
            this.txtNominalPower = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(45, 85, 130);
            this.lblTitle.Location = new System.Drawing.Point(40, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(220, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "请输入本次记录的名义值";
            //
            // lblNominalRPM
            //
            this.lblNominalRPM.AutoSize = true;
            this.lblNominalRPM.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblNominalRPM.Location = new System.Drawing.Point(40, 90);
            this.lblNominalRPM.Name = "lblNominalRPM";
            this.lblNominalRPM.Size = new System.Drawing.Size(150, 24);
            this.lblNominalRPM.TabIndex = 1;
            this.lblNominalRPM.Text = "名义转速 rpm：";
            //
            // txtNominalRPM
            //
            this.txtNominalRPM.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtNominalRPM.Location = new System.Drawing.Point(220, 86);
            this.txtNominalRPM.Name = "txtNominalRPM";
            this.txtNominalRPM.Size = new System.Drawing.Size(180, 32);
            this.txtNominalRPM.TabIndex = 2;
            this.txtNominalRPM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // lblNominalPower
            //
            this.lblNominalPower.AutoSize = true;
            this.lblNominalPower.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblNominalPower.Location = new System.Drawing.Point(40, 150);
            this.lblNominalPower.Name = "lblNominalPower";
            this.lblNominalPower.Size = new System.Drawing.Size(150, 24);
            this.lblNominalPower.TabIndex = 3;
            this.lblNominalPower.Text = "名义功率 kW：";
            //
            // txtNominalPower
            //
            this.txtNominalPower.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtNominalPower.Location = new System.Drawing.Point(220, 146);
            this.txtNominalPower.Name = "txtNominalPower";
            this.txtNominalPower.Size = new System.Drawing.Size(180, 32);
            this.txtNominalPower.TabIndex = 4;
            this.txtNominalPower.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // btnOK
            //
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnOK.Location = new System.Drawing.Point(100, 220);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(130, 46);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            //
            // btnCancel
            //
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnCancel.Location = new System.Drawing.Point(250, 220);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(130, 46);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // frmNominalValueInput
            //
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtNominalPower);
            this.Controls.Add(this.lblNominalPower);
            this.Controls.Add(this.txtNominalRPM);
            this.Controls.Add(this.lblNominalRPM);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNominalValueInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "本次记录 - 名义值";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
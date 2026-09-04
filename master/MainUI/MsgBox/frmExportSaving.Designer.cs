using System;
using System.Drawing;
using System.Windows.Forms;

namespace MainUI
{
    partial class frmExportSaving
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Label lblStatus;
        private Button btnClose;

        private void InitializeComponent()
        {
            this.lblStatus = new Label();
            this.btnClose = new Button();
            this.SuspendLayout();
            //
            // lblStatus
            //
            this.lblStatus.Font = new Font("微软雅黑", 14F);
            this.lblStatus.Location = new Point(20, 20);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(340, 60);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "正在保存，请稍候...";
            this.lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            //
            // btnClose
            //
            this.btnClose.Enabled = false;
            this.btnClose.Font = new Font("微软雅黑", 12F);
            this.btnClose.Location = new Point(125, 92);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(130, 40);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            //
            // frmExportSaving
            //
            this.AutoScaleDimensions = new SizeF(6F, 12F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(243, 249, 255);
            this.ClientSize = new Size(380, 156);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblStatus);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExportSaving";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "保存中";
            this.Shown += new EventHandler(this.frmExportSaving_Shown);
            this.ResumeLayout(false);
        }
    }
}

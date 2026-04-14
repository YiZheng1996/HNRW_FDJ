namespace MainUI
{
    partial class frmDispReport
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
            this.grpDI = new Sunny.UI.UIGroupBox();
            this.rwReport1 = new RW.UI.Controls.Report.RWReport();
            this.BtnPrint = new Sunny.UI.UIButton();
            this.BtnClose = new Sunny.UI.UIButton();
            this.grpDI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rwReport1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpDI
            // 
            this.grpDI.Controls.Add(this.rwReport1);
            this.grpDI.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpDI.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.grpDI.Location = new System.Drawing.Point(0, 0);
            this.grpDI.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpDI.MinimumSize = new System.Drawing.Size(1, 1);
            this.grpDI.Name = "grpDI";
            this.grpDI.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.grpDI.Size = new System.Drawing.Size(1484, 632);
            this.grpDI.TabIndex = 390;
            this.grpDI.Text = "试验报表";
            this.grpDI.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.grpDI.TitleAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.grpDI.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // rwReport1
            // 
            this.rwReport1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rwReport1.Location = new System.Drawing.Point(0, 32);
            this.rwReport1.Margin = new System.Windows.Forms.Padding(33, 49, 33, 49);
            this.rwReport1.Name = "rwReport1";
            this.rwReport1.ShowBox = true;
            this.rwReport1.Size = new System.Drawing.Size(1484, 600);
            this.rwReport1.TabIndex = 1;
            // 
            // BtnPrint
            // 
            this.BtnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnPrint.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.BtnPrint.Location = new System.Drawing.Point(1212, 640);
            this.BtnPrint.MinimumSize = new System.Drawing.Size(1, 1);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(120, 40);
            this.BtnPrint.TabIndex = 392;
            this.BtnPrint.Text = "打  印";
            this.BtnPrint.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnPrint.TipsText = "1";
            this.BtnPrint.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnClose.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.BtnClose.Location = new System.Drawing.Point(1355, 640);
            this.BtnClose.MinimumSize = new System.Drawing.Size(1, 1);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(120, 40);
            this.BtnClose.TabIndex = 393;
            this.BtnClose.Text = "退  出";
            this.BtnClose.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnClose.TipsText = "1";
            this.BtnClose.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // frmDispReport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1484, 859);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnPrint);
            this.Controls.Add(this.grpDI);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDispReport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "试验结果";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmDispReport_Load);
            this.grpDI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rwReport1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIGroupBox grpDI;
        private RW.UI.Controls.Report.RWReport rwReport1;
        private Sunny.UI.UIButton BtnPrint;
        private Sunny.UI.UIButton BtnClose;
    }
}
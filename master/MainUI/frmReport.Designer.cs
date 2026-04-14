using System;

namespace MainUI
{
    partial class frmReport
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
            this.btnClose = new Sunny.UI.UIButton();
            this.btnPrint = new Sunny.UI.UIButton();
            this.btnSave = new Sunny.UI.UIButton();
            this.grpDO = new Sunny.UI.UIGroupBox();
            this.btnPageUp = new Sunny.UI.UIButton();
            this.btnPageDown = new Sunny.UI.UIButton();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.grpDI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rwReport1)).BeginInit();
            this.grpDO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpDI
            // 
            this.grpDI.Controls.Add(this.rwReport1);
            this.grpDI.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpDI.Enabled = false;
            this.grpDI.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.grpDI.Location = new System.Drawing.Point(0, 0);
            this.grpDI.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpDI.MinimumSize = new System.Drawing.Size(1, 1);
            this.grpDI.Name = "grpDI";
            this.grpDI.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.grpDI.Size = new System.Drawing.Size(876, 599);
            this.grpDI.TabIndex = 391;
            this.grpDI.Text = "试验报表";
            this.grpDI.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.grpDI.TitleAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.grpDI.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // rwReport1
            // 
            this.rwReport1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rwReport1.Location = new System.Drawing.Point(0, 32);
            this.rwReport1.Margin = new System.Windows.Forms.Padding(3688, 6548, 3688, 6548);
            this.rwReport1.Name = "rwReport1";
            this.rwReport1.ShowBox = true;
            this.rwReport1.Size = new System.Drawing.Size(876, 567);
            this.rwReport1.TabIndex = 1;
            this.rwReport1.Opened += new RW.UI.Controls.Report.OpenHandler(this.rwReport1_Opened);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnClose.Location = new System.Drawing.Point(231, 26);
            this.btnClose.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(99, 47);
            this.btnClose.TabIndex = 395;
            this.btnClose.Text = "退出";
            this.btnClose.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.TipsText = "1";
            this.btnClose.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnPrint.Location = new System.Drawing.Point(6, 26);
            this.btnPrint.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(99, 47);
            this.btnPrint.TabIndex = 394;
            this.btnPrint.Text = "打印";
            this.btnPrint.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrint.TipsText = "1";
            this.btnPrint.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnSave.Location = new System.Drawing.Point(118, 26);
            this.btnSave.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(99, 47);
            this.btnSave.TabIndex = 395;
            this.btnSave.Text = "保存";
            this.btnSave.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.TipsText = "1";
            this.btnSave.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpDO
            // 
            this.grpDO.Controls.Add(this.btnSave);
            this.grpDO.Controls.Add(this.btnClose);
            this.grpDO.Controls.Add(this.btnPrint);
            this.grpDO.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.grpDO.Location = new System.Drawing.Point(534, 597);
            this.grpDO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpDO.MinimumSize = new System.Drawing.Size(1, 1);
            this.grpDO.Name = "grpDO";
            this.grpDO.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.grpDO.Size = new System.Drawing.Size(336, 81);
            this.grpDO.TabIndex = 392;
            this.grpDO.Text = "操作";
            this.grpDO.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.grpDO.TitleAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.grpDO.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnPageUp
            // 
            this.btnPageUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPageUp.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnPageUp.Location = new System.Drawing.Point(294, 623);
            this.btnPageUp.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnPageUp.Name = "btnPageUp";
            this.btnPageUp.Size = new System.Drawing.Size(99, 47);
            this.btnPageUp.TabIndex = 395;
            this.btnPageUp.Text = "上翻";
            this.btnPageUp.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPageUp.TipsText = "1";
            this.btnPageUp.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnPageUp.Click += new System.EventHandler(this.btnPageUp_Click);
            // 
            // btnPageDown
            // 
            this.btnPageDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPageDown.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnPageDown.Location = new System.Drawing.Point(420, 623);
            this.btnPageDown.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnPageDown.Name = "btnPageDown";
            this.btnPageDown.Size = new System.Drawing.Size(99, 47);
            this.btnPageDown.TabIndex = 396;
            this.btnPageDown.Text = "下翻";
            this.btnPageDown.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPageDown.TipsText = "1";
            this.btnPageDown.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnPageDown.Click += new System.EventHandler(this.btnPageDown_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numericUpDown1.Location = new System.Drawing.Point(12, 636);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(55, 27);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown1.Visible = false;
            // 
            // frmReport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(876, 691);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btnPageDown);
            this.Controls.Add(this.btnPageUp);
            this.Controls.Add(this.grpDO);
            this.Controls.Add(this.grpDI);
            this.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "报表结果";
            this.Load += new System.EventHandler(this.frmReport_Load);
            this.grpDI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rwReport1)).EndInit();
            this.grpDO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

       

        #endregion
        private Sunny.UI.UIGroupBox grpDI;
        private RW.UI.Controls.Report.RWReport rwReport1;
        private Sunny.UI.UIButton btnClose;
        private Sunny.UI.UIButton btnPrint;
        private Sunny.UI.UIButton btnSave;
        private Sunny.UI.UIGroupBox grpDO;
        private Sunny.UI.UIButton btnPageUp;
        private Sunny.UI.UIButton btnPageDown;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}
namespace MainUI
{
    partial class FrmStartupConfirm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        private void InitializeComponent()
        {
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.lblModel = new System.Windows.Forms.Label();
            this.cboModel = new System.Windows.Forms.ComboBox();
            this.lblTrialType = new System.Windows.Forms.Label();
            this.cboTrialType = new System.Windows.Forms.ComboBox();
            this.lblWarn = new System.Windows.Forms.Label();
            this.lblTestNo = new System.Windows.Forms.Label();
            this.txtTestNo = new System.Windows.Forms.TextBox();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.panelContent);
            this.panelMain.Controls.Add(this.panelHeader);
            this.panelMain.Controls.Add(this.panelFooter);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(600, 395);
            this.panelMain.TabIndex = 0;
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.lblModel);
            this.panelContent.Controls.Add(this.cboModel);
            this.panelContent.Controls.Add(this.lblTrialType);
            this.panelContent.Controls.Add(this.cboTrialType);
            this.panelContent.Controls.Add(this.lblWarn);
            this.panelContent.Controls.Add(this.lblTestNo);
            this.panelContent.Controls.Add(this.txtTestNo);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 85);
            this.panelContent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(51, 21, 51, 0);
            this.panelContent.Size = new System.Drawing.Size(600, 246);
            this.panelContent.TabIndex = 1;
            // 
            // lblModel
            // 
            this.lblModel.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.lblModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblModel.Location = new System.Drawing.Point(51, 28);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(120, 31);
            this.lblModel.TabIndex = 0;
            this.lblModel.Text = "型     号  *";
            this.lblModel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboModel
            // 
            this.cboModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModel.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.cboModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cboModel.Location = new System.Drawing.Point(189, 28);
            this.cboModel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboModel.Name = "cboModel";
            this.cboModel.Size = new System.Drawing.Size(292, 34);
            this.cboModel.TabIndex = 1;
            this.cboModel.SelectedIndexChanged += new System.EventHandler(this.CboModel_SelectedIndexChanged);
            // 
            // lblTrialType
            // 
            this.lblTrialType.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.lblTrialType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTrialType.Location = new System.Drawing.Point(51, 82);
            this.lblTrialType.Name = "lblTrialType";
            this.lblTrialType.Size = new System.Drawing.Size(120, 31);
            this.lblTrialType.TabIndex = 2;
            this.lblTrialType.Text = "试验类型 *";
            this.lblTrialType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTrialType
            // 
            this.cboTrialType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrialType.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.cboTrialType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cboTrialType.Location = new System.Drawing.Point(189, 82);
            this.cboTrialType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboTrialType.Name = "cboTrialType";
            this.cboTrialType.Size = new System.Drawing.Size(292, 34);
            this.cboTrialType.TabIndex = 3;
            this.cboTrialType.SelectedIndexChanged += new System.EventHandler(this.CboTrialType_SelectedIndexChanged);
            // 
            // lblWarn
            // 
            this.lblWarn.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lblWarn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblWarn.Location = new System.Drawing.Point(189, 118);
            this.lblWarn.Name = "lblWarn";
            this.lblWarn.Size = new System.Drawing.Size(326, 20);
            this.lblWarn.TabIndex = 4;
            this.lblWarn.Text = "⚠ 试验类型影响关键参数下发，请务必核实";
            // 
            // lblTestNo
            // 
            this.lblTestNo.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblTestNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblTestNo.Location = new System.Drawing.Point(51, 151);
            this.lblTestNo.Name = "lblTestNo";
            this.lblTestNo.Size = new System.Drawing.Size(120, 31);
            this.lblTestNo.TabIndex = 5;
            this.lblTestNo.Text = "试验编号";
            this.lblTestNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTestNo
            // 
            this.txtTestNo.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtTestNo.Location = new System.Drawing.Point(189, 152);
            this.txtTestNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTestNo.MaxLength = 50;
            this.txtTestNo.Name = "txtTestNo";
            this.txtTestNo.Size = new System.Drawing.Size(292, 32);
            this.txtTestNo.TabIndex = 6;
            this.txtTestNo.TextChanged += new System.EventHandler(this.TxtTestNo_TextChanged);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.lblSubTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(600, 85);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(600, 42);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "启动确认";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSubTitle.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.lblSubTitle.Location = new System.Drawing.Point(0, 60);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(600, 25);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "请确认型号、试验类型及试验编号后方可操作软件";
            this.lblSubTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelFooter.Controls.Add(this.btnConfirm);
            this.panelFooter.Controls.Add(this.btnCancel);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 331);
            this.panelFooter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(600, 64);
            this.panelFooter.TabIndex = 2;
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnConfirm.Enabled = false;
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(111, 14);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(154, 37);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "确  认";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(334, 14);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(154, 37);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取  消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmStartupConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 395);
            this.ControlBox = false;
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmStartupConfirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.FrmStartupConfirm_Load);
            this.panelMain.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.ComboBox cboModel;
        private System.Windows.Forms.Label lblTrialType;
        private System.Windows.Forms.ComboBox cboTrialType;
        private System.Windows.Forms.Label lblWarn;
        private System.Windows.Forms.Label lblTestNo;
        private System.Windows.Forms.TextBox txtTestNo;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
    }
}
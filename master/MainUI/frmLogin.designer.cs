namespace MainUI
{
    partial class frmLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.pnlHero = new System.Windows.Forms.Panel();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.lblSoftName = new System.Windows.Forms.Label();
            this.pnlHeroBorder = new System.Windows.Forms.Panel();
            this.uiLblSectionLogin = new Sunny.UI.UILabel();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.cboUsername = new Sunny.UI.UIComboBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.txtPassword = new Sunny.UI.UITextBox();
            this.pnlDivTrial = new System.Windows.Forms.Panel();
            this.uiLblSectionTrial = new Sunny.UI.UILabel();
            this.uiLblModel = new Sunny.UI.UILabel();
            this.cboModel = new Sunny.UI.UIComboBox();
            this.uiLblTrialType = new Sunny.UI.UILabel();
            this.cboTrialType = new Sunny.UI.UIComboBox();
            this.uiLblTestNo = new Sunny.UI.UILabel();
            this.txtTestNo = new Sunny.UI.UITextBox();
            this.pnlDivBottom = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnExit = new Sunny.UI.UIButton();
            this.btnSignIn = new Sunny.UI.UIButton();
            this.lblHint = new System.Windows.Forms.Label();
            this.pnlHero.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHero
            // 
            this.pnlHero.BackColor = System.Drawing.Color.White;
            this.pnlHero.Controls.Add(this.Logo);
            this.pnlHero.Controls.Add(this.lblSoftName);
            resources.ApplyResources(this.pnlHero, "pnlHero");
            this.pnlHero.Name = "pnlHero";
            this.pnlHero.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmLogin_MouseDown);
            // 
            // Logo
            // 
            this.Logo.Image = global::MainUI.Properties.Resources.logo;
            resources.ApplyResources(this.Logo, "Logo");
            this.Logo.Name = "Logo";
            this.Logo.TabStop = false;
            this.Logo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmLogin_MouseDown);
            // 
            // lblSoftName
            // 
            this.lblSoftName.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblSoftName, "lblSoftName");
            this.lblSoftName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(82)))), ((int)(((byte)(118)))));
            this.lblSoftName.Name = "lblSoftName";
            this.lblSoftName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmLogin_MouseDown);
            // 
            // pnlHeroBorder
            // 
            this.pnlHeroBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            resources.ApplyResources(this.pnlHeroBorder, "pnlHeroBorder");
            this.pnlHeroBorder.Name = "pnlHeroBorder";
            // 
            // uiLblSectionLogin
            // 
            resources.ApplyResources(this.uiLblSectionLogin, "uiLblSectionLogin");
            this.uiLblSectionLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(179)))), ((int)(((byte)(211)))));
            this.uiLblSectionLogin.Name = "uiLblSectionLogin";
            this.uiLblSectionLogin.Style = Sunny.UI.UIStyle.Custom;
            // 
            // uiLabel3
            // 
            resources.ApplyResources(this.uiLabel3, "uiLabel3");
            this.uiLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.uiLabel3.Name = "uiLabel3";
            // 
            // cboUsername
            // 
            this.cboUsername.DataSource = null;
            this.cboUsername.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cboUsername.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(this.cboUsername, "cboUsername");
            this.cboUsername.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cboUsername.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cboUsername.Name = "cboUsername";
            this.cboUsername.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            this.cboUsername.SymbolSize = 24;
            this.cboUsername.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboUsername.Watermark = "请选择用户";
            this.cboUsername.SelectedIndexChanged += new System.EventHandler(this.AnyField_Changed);
            // 
            // uiLabel1
            // 
            resources.ApplyResources(this.uiLabel1, "uiLabel1");
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.uiLabel1.Name = "uiLabel1";
            // 
            // txtPassword
            // 
            this.txtPassword.ButtonWidth = 100;
            this.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.ShowText = false;
            this.txtPassword.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtPassword.Watermark = "请输入密码";
            this.txtPassword.TextChanged += new System.EventHandler(this.AnyField_Changed);
            // 
            // pnlDivTrial
            // 
            this.pnlDivTrial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(218)))), ((int)(((byte)(234)))));
            resources.ApplyResources(this.pnlDivTrial, "pnlDivTrial");
            this.pnlDivTrial.Name = "pnlDivTrial";
            // 
            // uiLblSectionTrial
            // 
            resources.ApplyResources(this.uiLblSectionTrial, "uiLblSectionTrial");
            this.uiLblSectionTrial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(179)))), ((int)(((byte)(211)))));
            this.uiLblSectionTrial.Name = "uiLblSectionTrial";
            this.uiLblSectionTrial.Style = Sunny.UI.UIStyle.Custom;
            // 
            // uiLblModel
            // 
            resources.ApplyResources(this.uiLblModel, "uiLblModel");
            this.uiLblModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.uiLblModel.Name = "uiLblModel";
            // 
            // cboModel
            // 
            this.cboModel.DataSource = null;
            this.cboModel.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cboModel.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(this.cboModel, "cboModel");
            this.cboModel.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cboModel.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cboModel.Name = "cboModel";
            this.cboModel.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            this.cboModel.SymbolSize = 24;
            this.cboModel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboModel.Watermark = "请选择";
            this.cboModel.SelectedIndexChanged += new System.EventHandler(this.AnyField_Changed);
            // 
            // uiLblTrialType
            // 
            resources.ApplyResources(this.uiLblTrialType, "uiLblTrialType");
            this.uiLblTrialType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.uiLblTrialType.Name = "uiLblTrialType";
            // 
            // cboTrialType
            // 
            this.cboTrialType.DataSource = null;
            this.cboTrialType.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cboTrialType.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(this.cboTrialType, "cboTrialType");
            this.cboTrialType.ItemHeight = 28;
            this.cboTrialType.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cboTrialType.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cboTrialType.Name = "cboTrialType";
            this.cboTrialType.SymbolSize = 24;
            this.cboTrialType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboTrialType.Watermark = "";
            this.cboTrialType.SelectedIndexChanged += new System.EventHandler(this.AnyField_Changed);
            // 
            // uiLblTestNo
            // 
            resources.ApplyResources(this.uiLblTestNo, "uiLblTestNo");
            this.uiLblTestNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.uiLblTestNo.Name = "uiLblTestNo";
            // 
            // txtTestNo
            // 
            this.txtTestNo.Cursor = System.Windows.Forms.Cursors.IBeam;
            resources.ApplyResources(this.txtTestNo, "txtTestNo");
            this.txtTestNo.Name = "txtTestNo";
            this.txtTestNo.ShowText = false;
            this.txtTestNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtTestNo.Watermark = "请输入试验编号，如 2024-001";
            this.txtTestNo.TextChanged += new System.EventHandler(this.AnyField_Changed);
            // 
            // pnlDivBottom
            // 
            this.pnlDivBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(218)))), ((int)(((byte)(234)))));
            resources.ApplyResources(this.pnlDivBottom, "pnlDivBottom");
            this.pnlDivBottom.Name = "pnlDivBottom";
            // 
            // lblMessage
            // 
            resources.ApplyResources(this.lblMessage, "lblMessage");
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.lblMessage.Name = "lblMessage";
            // 
            // btnExit
            // 
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.FillColor = System.Drawing.Color.IndianRed;
            this.btnExit.FillColor2 = System.Drawing.Color.IndianRed;
            resources.ApplyResources(this.btnExit, "btnExit");
            this.btnExit.Name = "btnExit";
            this.btnExit.RectColor = System.Drawing.Color.IndianRed;
            this.btnExit.RectDisableColor = System.Drawing.Color.IndianRed;
            this.btnExit.TipsFont = new System.Drawing.Font("微软雅黑", 9F);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSignIn
            // 
            this.btnSignIn.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnSignIn, "btnSignIn");
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.TipsFont = new System.Drawing.Font("微软雅黑", 9F);
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            // 
            // lblHint
            // 
            resources.ApplyResources(this.lblHint, "lblHint");
            this.lblHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(184)))), ((int)(((byte)(200)))));
            this.lblHint.Name = "lblHint";
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnSignIn;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.CancelButton = this.btnExit;
            this.Controls.Add(this.pnlHero);
            this.Controls.Add(this.pnlHeroBorder);
            this.Controls.Add(this.uiLblSectionLogin);
            this.Controls.Add(this.uiLabel3);
            this.Controls.Add(this.cboUsername);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.pnlDivTrial);
            this.Controls.Add(this.uiLblSectionTrial);
            this.Controls.Add(this.uiLblModel);
            this.Controls.Add(this.cboModel);
            this.Controls.Add(this.uiLblTrialType);
            this.Controls.Add(this.cboTrialType);
            this.Controls.Add(this.uiLblTestNo);
            this.Controls.Add(this.txtTestNo);
            this.Controls.Add(this.pnlDivBottom);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSignIn);
            this.Controls.Add(this.lblHint);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.Shown += new System.EventHandler(this.frmLogin_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmLogin_MouseDown);
            this.pnlHero.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // ── 控件声明 ──────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlHero;
        private System.Windows.Forms.Panel pnlHeroBorder;
        public System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.Label lblSoftName;
        private Sunny.UI.UILabel uiLblSectionLogin;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UIComboBox cboUsername;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UITextBox txtPassword;
        private System.Windows.Forms.Panel pnlDivTrial;
        private Sunny.UI.UILabel uiLblSectionTrial;
        private Sunny.UI.UILabel uiLblModel;
        private Sunny.UI.UIComboBox cboModel;
        private Sunny.UI.UILabel uiLblTrialType;
        private Sunny.UI.UIComboBox cboTrialType;
        private Sunny.UI.UILabel uiLblTestNo;
        private Sunny.UI.UITextBox txtTestNo;
        private System.Windows.Forms.Panel pnlDivBottom;
        private System.Windows.Forms.Label lblMessage;
        private Sunny.UI.UIButton btnExit;
        private Sunny.UI.UIButton btnSignIn;
        private System.Windows.Forms.Label lblHint;
    }
}
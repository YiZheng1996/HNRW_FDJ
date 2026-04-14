namespace MainUI
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.lblSoftName = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.btnSignIn = new Sunny.UI.UIButton();
            this.btnExit = new Sunny.UI.UIButton();
            this.cboUsername = new Sunny.UI.UIComboBox();
            this.txtPassword = new Sunny.UI.UITextBox();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSoftName
            // 
            resources.ApplyResources(this.lblSoftName, "lblSoftName");
            this.lblSoftName.Name = "lblSoftName";
            // 
            // lblMessage
            // 
            resources.ApplyResources(this.lblMessage, "lblMessage");
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Name = "lblMessage";
            // 
            // uiLabel3
            // 
            this.uiLabel3.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.uiLabel3, "uiLabel3");
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel1
            // 
            this.uiLabel1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.uiLabel1, "uiLabel1");
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnSignIn
            // 
            resources.ApplyResources(this.btnSignIn, "btnSignIn");
            this.btnSignIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSignIn.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            // 
            // btnExit
            // 
            resources.ApplyResources(this.btnExit, "btnExit");
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Name = "btnExit";
            this.btnExit.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cboUsername
            // 
            this.cboUsername.DataSource = null;
            this.cboUsername.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cboUsername.FillColor = System.Drawing.Color.White;
            this.cboUsername.FilterMaxCount = 50;
            resources.ApplyResources(this.cboUsername, "cboUsername");
            this.cboUsername.Items.AddRange(new object[] {
            resources.GetString("cboUsername.Items"),
            resources.GetString("cboUsername.Items1"),
            resources.GetString("cboUsername.Items2"),
            resources.GetString("cboUsername.Items3"),
            resources.GetString("cboUsername.Items4"),
            resources.GetString("cboUsername.Items5"),
            resources.GetString("cboUsername.Items6"),
            resources.GetString("cboUsername.Items7"),
            resources.GetString("cboUsername.Items8"),
            resources.GetString("cboUsername.Items9"),
            resources.GetString("cboUsername.Items10"),
            resources.GetString("cboUsername.Items11")});
            this.cboUsername.Name = "cboUsername";
            this.cboUsername.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.cboUsername.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            this.cboUsername.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboUsername.Watermark = "请选择";
            this.cboUsername.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
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
            this.txtPassword.Watermark = "请输入";
            this.txtPassword.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // Logo
            // 
            this.Logo.Image = global::MainUI.Properties.Resources.logo;
            resources.ApplyResources(this.Logo, "Logo");
            this.Logo.Name = "Logo";
            this.Logo.TabStop = false;
            // 
            // uiLabel2
            // 
            this.uiLabel2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.uiLabel2, "uiLabel2");
            this.uiLabel2.ForeColor = System.Drawing.Color.Gray;
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnSignIn;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.CancelButton = this.btnExit;
            this.Controls.Add(this.uiLabel3);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.cboUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblSoftName);
            this.Controls.Add(this.btnSignIn);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmLogin_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblSoftName;
        private System.Windows.Forms.Label lblMessage;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIButton btnSignIn;
        private Sunny.UI.UIButton btnExit;
        private Sunny.UI.UIComboBox cboUsername;
        private Sunny.UI.UITextBox txtPassword;
        private Sunny.UI.UILabel uiLabel2;
        public System.Windows.Forms.PictureBox Logo;
    }
}
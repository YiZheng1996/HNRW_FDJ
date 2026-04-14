
namespace MainUI.Widget
{
    partial class FrmTimePick
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
            this.dtpEndTime = new Sunny.UI.UIDatetimePicker();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.dtpStartTime = new Sunny.UI.UIDatetimePicker();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.btnExit = new Sunny.UI.UIButton();
            this.btnSelct = new RW.UI.Controls.RButton();
            this.SuspendLayout();
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CanEmpty = true;
            this.dtpEndTime.DateCultureInfo = new System.Globalization.CultureInfo("zh-CN");
            this.dtpEndTime.FillColor = System.Drawing.Color.White;
            this.dtpEndTime.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpEndTime.Location = new System.Drawing.Point(125, 73);
            this.dtpEndTime.Margin = new System.Windows.Forms.Padding(7, 9, 7, 9);
            this.dtpEndTime.MaxLength = 19;
            this.dtpEndTime.MinimumSize = new System.Drawing.Size(104, 0);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Padding = new System.Windows.Forms.Padding(0, 0, 50, 4);
            this.dtpEndTime.Size = new System.Drawing.Size(331, 35);
            this.dtpEndTime.SymbolDropDown = 61555;
            this.dtpEndTime.SymbolNormal = 61555;
            this.dtpEndTime.SymbolSize = 24;
            this.dtpEndTime.TabIndex = 655;
            this.dtpEndTime.Text = "2020-06-02 17:57:28";
            this.dtpEndTime.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.dtpEndTime.Value = new System.DateTime(2020, 6, 2, 17, 57, 28, 203);
            this.dtpEndTime.Watermark = "";
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel1.Location = new System.Drawing.Point(32, 73);
            this.uiLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(92, 35);
            this.uiLabel1.TabIndex = 654;
            this.uiLabel1.Text = "结束时间";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CanEmpty = true;
            this.dtpStartTime.DateCultureInfo = new System.Globalization.CultureInfo("zh-CN");
            this.dtpStartTime.FillColor = System.Drawing.Color.White;
            this.dtpStartTime.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpStartTime.Location = new System.Drawing.Point(125, 18);
            this.dtpStartTime.Margin = new System.Windows.Forms.Padding(7, 9, 7, 9);
            this.dtpStartTime.MaxLength = 19;
            this.dtpStartTime.MinimumSize = new System.Drawing.Size(104, 0);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Padding = new System.Windows.Forms.Padding(0, 0, 50, 4);
            this.dtpStartTime.Size = new System.Drawing.Size(331, 35);
            this.dtpStartTime.SymbolDropDown = 61555;
            this.dtpStartTime.SymbolNormal = 61555;
            this.dtpStartTime.SymbolSize = 24;
            this.dtpStartTime.TabIndex = 653;
            this.dtpStartTime.Text = "2020-06-02 17:57:28";
            this.dtpStartTime.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.dtpStartTime.Value = new System.DateTime(2020, 6, 2, 17, 57, 28, 203);
            this.dtpStartTime.Watermark = "";
            // 
            // uiLabel4
            // 
            this.uiLabel4.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel4.Location = new System.Drawing.Point(32, 18);
            this.uiLabel4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(92, 35);
            this.uiLabel4.TabIndex = 652;
            this.uiLabel4.Text = "开始时间";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExit
            // 
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnExit.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnExit.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnExit.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExit.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.LightColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.btnExit.Location = new System.Drawing.Point(265, 130);
            this.btnExit.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnExit.Name = "btnExit";
            this.btnExit.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnExit.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnExit.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExit.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExit.Size = new System.Drawing.Size(221, 38);
            this.btnExit.Style = Sunny.UI.UIStyle.Custom;
            this.btnExit.TabIndex = 656;
            this.btnExit.Text = "退出";
            this.btnExit.TipsFont = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSelct
            // 
            this.btnSelct.BackColor = System.Drawing.Color.Silver;
            this.btnSelct.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSelct.FalseColor = System.Drawing.Color.Silver;
            this.btnSelct.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelct.Location = new System.Drawing.Point(24, 130);
            this.btnSelct.Name = "btnSelct";
            this.btnSelct.Size = new System.Drawing.Size(209, 38);
            this.btnSelct.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnSelct.TabIndex = 676;
            this.btnSelct.Tag = "";
            this.btnSelct.Text = "查询";
            this.btnSelct.TrueColor = System.Drawing.Color.Lime;
            this.btnSelct.Click += new System.EventHandler(this.btnSelct_Click);
            // 
            // FrmTimePick
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(498, 190);
            this.ControlBox = false;
            this.Controls.Add(this.btnSelct);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dtpEndTime);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.dtpStartTime);
            this.Controls.Add(this.uiLabel4);
            this.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmTimePick";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "时间选择";
            this.Load += new System.EventHandler(this.FrmTimePick_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIDatetimePicker dtpEndTime;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIDatetimePicker dtpStartTime;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UIButton btnExit;
        private RW.UI.Controls.RButton btnSelct;
    }
}
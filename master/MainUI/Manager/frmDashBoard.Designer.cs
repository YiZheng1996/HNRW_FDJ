
namespace MainUI.Manager
{
    partial class frmDashBoard
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
            this.txtName = new Sunny.UI.UITextBox();
            this.txtPoint = new Sunny.UI.UITextBox();
            this.uiLabel6 = new Sunny.UI.UILabel();
            this.btnselect = new Sunny.UI.UIButton();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.uiRadioButtonGroup1 = new Sunny.UI.UIRadioButtonGroup();
            this.rdo1 = new Sunny.UI.UIRadioButton();
            this.rdo0 = new Sunny.UI.UIRadioButton();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.btnReturn = new Sunny.UI.UIButton();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.btnSave = new Sunny.UI.UIButton();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.nudMin = new Sunny.UI.UIDoubleUpDown();
            this.nudMax = new Sunny.UI.UIDoubleUpDown();
            this.nudScarm = new Sunny.UI.UIDoubleUpDown();
            this.txtUnit = new Sunny.UI.UITextBox();
            this.uiLabel7 = new Sunny.UI.UILabel();
            this.grpDI.SuspendLayout();
            this.uiRadioButtonGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDI
            // 
            this.grpDI.Controls.Add(this.txtUnit);
            this.grpDI.Controls.Add(this.uiLabel7);
            this.grpDI.Controls.Add(this.nudScarm);
            this.grpDI.Controls.Add(this.nudMax);
            this.grpDI.Controls.Add(this.nudMin);
            this.grpDI.Controls.Add(this.txtName);
            this.grpDI.Controls.Add(this.txtPoint);
            this.grpDI.Controls.Add(this.uiLabel6);
            this.grpDI.Controls.Add(this.btnselect);
            this.grpDI.Controls.Add(this.uiLabel5);
            this.grpDI.Controls.Add(this.uiRadioButtonGroup1);
            this.grpDI.Controls.Add(this.uiLabel4);
            this.grpDI.Controls.Add(this.btnReturn);
            this.grpDI.Controls.Add(this.uiLabel3);
            this.grpDI.Controls.Add(this.btnSave);
            this.grpDI.Controls.Add(this.uiLabel2);
            this.grpDI.Controls.Add(this.uiLabel1);
            this.grpDI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDI.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.grpDI.Location = new System.Drawing.Point(0, 0);
            this.grpDI.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.grpDI.MinimumSize = new System.Drawing.Size(1, 1);
            this.grpDI.Name = "grpDI";
            this.grpDI.Padding = new System.Windows.Forms.Padding(0, 53, 0, 0);
            this.grpDI.Size = new System.Drawing.Size(530, 595);
            this.grpDI.TabIndex = 391;
            this.grpDI.Text = "参数设置";
            this.grpDI.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.grpDI.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txtName
            // 
            this.txtName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Location = new System.Drawing.Point(209, 169);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtName.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtName.Name = "txtName";
            this.txtName.ShowText = false;
            this.txtName.Size = new System.Drawing.Size(150, 29);
            this.txtName.TabIndex = 402;
            this.txtName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtName.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txtPoint
            // 
            this.txtPoint.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPoint.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPoint.Location = new System.Drawing.Point(209, 45);
            this.txtPoint.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPoint.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtPoint.Name = "txtPoint";
            this.txtPoint.ReadOnly = true;
            this.txtPoint.ShowText = false;
            this.txtPoint.Size = new System.Drawing.Size(150, 29);
            this.txtPoint.TabIndex = 401;
            this.txtPoint.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtPoint.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel6
            // 
            this.uiLabel6.AutoSize = true;
            this.uiLabel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.uiLabel6.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel6.Location = new System.Drawing.Point(42, 170);
            this.uiLabel6.Name = "uiLabel6";
            this.uiLabel6.Size = new System.Drawing.Size(126, 25);
            this.uiLabel6.TabIndex = 398;
            this.uiLabel6.Text = "界面显示名称";
            this.uiLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiLabel6.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnselect
            // 
            this.btnselect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnselect.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnselect.Location = new System.Drawing.Point(366, 41);
            this.btnselect.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnselect.Name = "btnselect";
            this.btnselect.Size = new System.Drawing.Size(120, 36);
            this.btnselect.TabIndex = 397;
            this.btnselect.Text = "选择";
            this.btnselect.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnselect.TipsText = "1";
            this.btnselect.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnselect.Click += new System.EventHandler(this.btnselect_Click);
            // 
            // uiLabel5
            // 
            this.uiLabel5.AutoSize = true;
            this.uiLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.uiLabel5.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel5.Location = new System.Drawing.Point(80, 47);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(88, 25);
            this.uiLabel5.TabIndex = 396;
            this.uiLabel5.Text = "点位选择";
            this.uiLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiLabel5.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiRadioButtonGroup1
            // 
            this.uiRadioButtonGroup1.Controls.Add(this.rdo1);
            this.uiRadioButtonGroup1.Controls.Add(this.rdo0);
            this.uiRadioButtonGroup1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiRadioButtonGroup1.Location = new System.Drawing.Point(209, 406);
            this.uiRadioButtonGroup1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiRadioButtonGroup1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRadioButtonGroup1.Name = "uiRadioButtonGroup1";
            this.uiRadioButtonGroup1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiRadioButtonGroup1.Size = new System.Drawing.Size(196, 66);
            this.uiRadioButtonGroup1.TabIndex = 395;
            this.uiRadioButtonGroup1.Text = null;
            this.uiRadioButtonGroup1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiRadioButtonGroup1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // rdo1
            // 
            this.rdo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.rdo1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdo1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdo1.Location = new System.Drawing.Point(108, 24);
            this.rdo1.MinimumSize = new System.Drawing.Size(1, 1);
            this.rdo1.Name = "rdo1";
            this.rdo1.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.rdo1.Size = new System.Drawing.Size(71, 29);
            this.rdo1.TabIndex = 395;
            this.rdo1.Text = "停机";
            this.rdo1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // rdo0
            // 
            this.rdo0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.rdo0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdo0.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdo0.Location = new System.Drawing.Point(25, 24);
            this.rdo0.MinimumSize = new System.Drawing.Size(1, 1);
            this.rdo0.Name = "rdo0";
            this.rdo0.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.rdo0.Size = new System.Drawing.Size(71, 29);
            this.rdo0.TabIndex = 394;
            this.rdo0.Text = "报警";
            this.rdo0.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel4
            // 
            this.uiLabel4.AutoSize = true;
            this.uiLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.uiLabel4.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel4.Location = new System.Drawing.Point(23, 431);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(145, 25);
            this.uiLabel4.TabIndex = 393;
            this.uiLabel4.Text = "超过报警值处理";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiLabel4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnReturn
            // 
            this.btnReturn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReturn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnReturn.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnReturn.Location = new System.Drawing.Point(366, 524);
            this.btnReturn.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(120, 40);
            this.btnReturn.TabIndex = 392;
            this.btnReturn.Text = "返回";
            this.btnReturn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReturn.TipsText = "1";
            this.btnReturn.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // uiLabel3
            // 
            this.uiLabel3.AutoSize = true;
            this.uiLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel3.Location = new System.Drawing.Point(99, 358);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(69, 25);
            this.uiLabel3.TabIndex = 390;
            this.uiLabel3.Text = "报警值";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnSave.Location = new System.Drawing.Point(65, 524);
            this.btnSave.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.TabIndex = 387;
            this.btnSave.Text = "保存";
            this.btnSave.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.TipsText = "1";
            this.btnSave.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // uiLabel2
            // 
            this.uiLabel2.AutoSize = true;
            this.uiLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.Location = new System.Drawing.Point(99, 296);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(69, 25);
            this.uiLabel2.TabIndex = 74;
            this.uiLabel2.Text = "最大值";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel1
            // 
            this.uiLabel1.AutoSize = true;
            this.uiLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(99, 233);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(69, 25);
            this.uiLabel1.TabIndex = 72;
            this.uiLabel1.Text = "最小值";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // nudMin
            // 
            this.nudMin.DecimalPlaces = 0;
            this.nudMin.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudMin.Location = new System.Drawing.Point(209, 231);
            this.nudMin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudMin.Maximum = 200D;
            this.nudMin.Minimum = 0D;
            this.nudMin.MinimumSize = new System.Drawing.Size(100, 0);
            this.nudMin.Name = "nudMin";
            this.nudMin.ShowText = false;
            this.nudMin.Size = new System.Drawing.Size(151, 29);
            this.nudMin.Step = 1D;
            this.nudMin.TabIndex = 403;
            this.nudMin.Text = "uiDoubleUpDown1";
            this.nudMin.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.nudMin.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // nudMax
            // 
            this.nudMax.DecimalPlaces = 0;
            this.nudMax.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudMax.Location = new System.Drawing.Point(209, 293);
            this.nudMax.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudMax.Maximum = 999D;
            this.nudMax.Minimum = 0D;
            this.nudMax.MinimumSize = new System.Drawing.Size(100, 0);
            this.nudMax.Name = "nudMax";
            this.nudMax.ShowText = false;
            this.nudMax.Size = new System.Drawing.Size(151, 29);
            this.nudMax.Step = 1D;
            this.nudMax.TabIndex = 404;
            this.nudMax.Text = "uiDoubleUpDown1";
            this.nudMax.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.nudMax.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // nudScarm
            // 
            this.nudScarm.DecimalPlaces = 0;
            this.nudScarm.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudScarm.Location = new System.Drawing.Point(209, 355);
            this.nudScarm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudScarm.Maximum = 999D;
            this.nudScarm.Minimum = 0D;
            this.nudScarm.MinimumSize = new System.Drawing.Size(100, 0);
            this.nudScarm.Name = "nudScarm";
            this.nudScarm.ShowText = false;
            this.nudScarm.Size = new System.Drawing.Size(151, 29);
            this.nudScarm.Step = 1D;
            this.nudScarm.TabIndex = 405;
            this.nudScarm.Text = "uiDoubleUpDown1";
            this.nudScarm.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.nudScarm.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txtUnit
            // 
            this.txtUnit.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUnit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUnit.Location = new System.Drawing.Point(209, 107);
            this.txtUnit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtUnit.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.ReadOnly = true;
            this.txtUnit.ShowText = false;
            this.txtUnit.Size = new System.Drawing.Size(150, 29);
            this.txtUnit.TabIndex = 409;
            this.txtUnit.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtUnit.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel7
            // 
            this.uiLabel7.AutoSize = true;
            this.uiLabel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.uiLabel7.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel7.Location = new System.Drawing.Point(118, 110);
            this.uiLabel7.Name = "uiLabel7";
            this.uiLabel7.Size = new System.Drawing.Size(50, 25);
            this.uiLabel7.TabIndex = 408;
            this.uiLabel7.Text = "单位";
            this.uiLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiLabel7.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // frmDashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 595);
            this.ControlBox = false;
            this.Controls.Add(this.grpDI);
            this.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmDashBoard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "仪表参数设置";
            this.grpDI.ResumeLayout(false);
            this.grpDI.PerformLayout();
            this.uiRadioButtonGroup1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIGroupBox grpDI;
        private Sunny.UI.UIButton btnSave;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UIButton btnReturn;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UILabel uiLabel5;
        private Sunny.UI.UIRadioButtonGroup uiRadioButtonGroup1;
        private Sunny.UI.UIRadioButton rdo0;
        private Sunny.UI.UIButton btnselect;
        private Sunny.UI.UILabel uiLabel6;
        private Sunny.UI.UIRadioButton rdo1;
        private Sunny.UI.UITextBox txtName;
        private Sunny.UI.UITextBox txtPoint;
        private Sunny.UI.UIDoubleUpDown nudMin;
        private Sunny.UI.UIDoubleUpDown nudMax;
        private Sunny.UI.UIDoubleUpDown nudScarm;
        private Sunny.UI.UITextBox txtUnit;
        private Sunny.UI.UILabel uiLabel7;
    }
}
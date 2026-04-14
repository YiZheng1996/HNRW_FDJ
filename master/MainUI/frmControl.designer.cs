namespace MainUI
{
    partial class frmControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmControl));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClose = new Sunny.UI.UIButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.switchPictureBox1 = new RW.UI.Controls.SwitchPictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.switchPictureBox2 = new RW.UI.Controls.SwitchPictureBox();
            this.switchPictureBox3 = new RW.UI.Controls.SwitchPictureBox();
            this.switchPictureBox4 = new RW.UI.Controls.SwitchPictureBox();
            this.switchPictureBox5 = new RW.UI.Controls.SwitchPictureBox();
            this.switchPictureBox6 = new RW.UI.Controls.SwitchPictureBox();
            this.switchPictureBox7 = new RW.UI.Controls.SwitchPictureBox();
            this.switchPictureBox8 = new RW.UI.Controls.SwitchPictureBox();
            this.switchPictureBox9 = new RW.UI.Controls.SwitchPictureBox();
            this.switchPictureBox10 = new RW.UI.Controls.SwitchPictureBox();
            this.switchPictureBox11 = new RW.UI.Controls.SwitchPictureBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox11)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleCustomMode = true;
            this.btnClose.TipsFont = new System.Drawing.Font("微软雅黑", 13F);
            this.btnClose.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.switchPictureBox11);
            this.groupBox2.Controls.Add(this.switchPictureBox10);
            this.groupBox2.Controls.Add(this.switchPictureBox9);
            this.groupBox2.Controls.Add(this.switchPictureBox8);
            this.groupBox2.Controls.Add(this.switchPictureBox7);
            this.groupBox2.Controls.Add(this.switchPictureBox6);
            this.groupBox2.Controls.Add(this.switchPictureBox5);
            this.groupBox2.Controls.Add(this.switchPictureBox4);
            this.groupBox2.Controls.Add(this.switchPictureBox3);
            this.groupBox2.Controls.Add(this.switchPictureBox2);
            this.groupBox2.Controls.Add(this.switchPictureBox1);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // switchPictureBox1
            // 
            this.switchPictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox1.CanClick = true;
            this.switchPictureBox1.ClickSwitch = false;
            this.switchPictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.switchPictureBox1.FalseImage = global::MainUI.Properties.Resources.mv_Hor_OFF;
            resources.ApplyResources(this.switchPictureBox1, "switchPictureBox1");
            this.switchPictureBox1.Image = global::MainUI.Properties.Resources.mv_Hor_OFF;
            this.switchPictureBox1.Index = 13;
            this.switchPictureBox1.Name = "switchPictureBox1";
            this.switchPictureBox1.Switch = false;
            this.switchPictureBox1.TabStop = false;
            this.switchPictureBox1.Tag = "Y90阀控制";
            this.switchPictureBox1.TextBackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox1.TextLayout = RW.UI.Controls.TextLayout.Bottom;
            this.switchPictureBox1.TrueImage = global::MainUI.Properties.Resources.mv_Hor_ON;
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // switchPictureBox2
            // 
            this.switchPictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox2.CanClick = true;
            this.switchPictureBox2.ClickSwitch = false;
            this.switchPictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.switchPictureBox2.FalseImage = global::MainUI.Properties.Resources.mv_Hor_OFF;
            resources.ApplyResources(this.switchPictureBox2, "switchPictureBox2");
            this.switchPictureBox2.Image = global::MainUI.Properties.Resources.mv_Hor_OFF;
            this.switchPictureBox2.Index = 13;
            this.switchPictureBox2.Name = "switchPictureBox2";
            this.switchPictureBox2.Switch = false;
            this.switchPictureBox2.TabStop = false;
            this.switchPictureBox2.Tag = "Y91阀控制";
            this.switchPictureBox2.TextBackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox2.TextLayout = RW.UI.Controls.TextLayout.Bottom;
            this.switchPictureBox2.TrueImage = global::MainUI.Properties.Resources.mv_Hor_ON;
            // 
            // switchPictureBox3
            // 
            this.switchPictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox3.CanClick = true;
            this.switchPictureBox3.ClickSwitch = false;
            this.switchPictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.switchPictureBox3.FalseImage = global::MainUI.Properties.Resources.mv_Hor_OFF;
            resources.ApplyResources(this.switchPictureBox3, "switchPictureBox3");
            this.switchPictureBox3.Image = global::MainUI.Properties.Resources.mv_Hor_OFF;
            this.switchPictureBox3.Index = 13;
            this.switchPictureBox3.Name = "switchPictureBox3";
            this.switchPictureBox3.Switch = false;
            this.switchPictureBox3.TabStop = false;
            this.switchPictureBox3.Tag = "Y92阀控制";
            this.switchPictureBox3.TextBackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox3.TextLayout = RW.UI.Controls.TextLayout.Bottom;
            this.switchPictureBox3.TrueImage = global::MainUI.Properties.Resources.mv_Hor_ON;
            // 
            // switchPictureBox4
            // 
            this.switchPictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox4.CanClick = true;
            this.switchPictureBox4.ClickSwitch = false;
            this.switchPictureBox4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.switchPictureBox4.FalseImage = global::MainUI.Properties.Resources.mv_Hor_OFF;
            resources.ApplyResources(this.switchPictureBox4, "switchPictureBox4");
            this.switchPictureBox4.Image = global::MainUI.Properties.Resources.mv_Hor_OFF;
            this.switchPictureBox4.Index = 13;
            this.switchPictureBox4.Name = "switchPictureBox4";
            this.switchPictureBox4.Switch = false;
            this.switchPictureBox4.TabStop = false;
            this.switchPictureBox4.Tag = "Y93阀控制";
            this.switchPictureBox4.TextBackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox4.TextLayout = RW.UI.Controls.TextLayout.Bottom;
            this.switchPictureBox4.TrueImage = global::MainUI.Properties.Resources.mv_Hor_ON;
            // 
            // switchPictureBox5
            // 
            this.switchPictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox5.CanClick = true;
            this.switchPictureBox5.ClickSwitch = false;
            this.switchPictureBox5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.switchPictureBox5.FalseImage = global::MainUI.Properties.Resources.mv_Hor_OFF;
            resources.ApplyResources(this.switchPictureBox5, "switchPictureBox5");
            this.switchPictureBox5.Image = global::MainUI.Properties.Resources.mv_Hor_OFF;
            this.switchPictureBox5.Index = 13;
            this.switchPictureBox5.Name = "switchPictureBox5";
            this.switchPictureBox5.Switch = false;
            this.switchPictureBox5.TabStop = false;
            this.switchPictureBox5.Tag = "Y95阀控制";
            this.switchPictureBox5.TextBackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox5.TextLayout = RW.UI.Controls.TextLayout.Bottom;
            this.switchPictureBox5.TrueImage = global::MainUI.Properties.Resources.mv_Hor_ON;
            // 
            // switchPictureBox6
            // 
            this.switchPictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox6.CanClick = true;
            this.switchPictureBox6.ClickSwitch = false;
            this.switchPictureBox6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.switchPictureBox6.FalseImage = global::MainUI.Properties.Resources.mv_Hor_OFF;
            resources.ApplyResources(this.switchPictureBox6, "switchPictureBox6");
            this.switchPictureBox6.Image = global::MainUI.Properties.Resources.mv_Hor_OFF;
            this.switchPictureBox6.Index = 13;
            this.switchPictureBox6.Name = "switchPictureBox6";
            this.switchPictureBox6.Switch = false;
            this.switchPictureBox6.TabStop = false;
            this.switchPictureBox6.Tag = "Y96阀控制";
            this.switchPictureBox6.TextBackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox6.TextLayout = RW.UI.Controls.TextLayout.Bottom;
            this.switchPictureBox6.TrueImage = global::MainUI.Properties.Resources.mv_Hor_ON;
            // 
            // switchPictureBox7
            // 
            this.switchPictureBox7.BackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox7.CanClick = true;
            this.switchPictureBox7.ClickSwitch = false;
            this.switchPictureBox7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.switchPictureBox7.FalseImage = global::MainUI.Properties.Resources.mv_Hor_OFF;
            resources.ApplyResources(this.switchPictureBox7, "switchPictureBox7");
            this.switchPictureBox7.Image = global::MainUI.Properties.Resources.mv_Hor_OFF;
            this.switchPictureBox7.Index = 13;
            this.switchPictureBox7.Name = "switchPictureBox7";
            this.switchPictureBox7.Switch = false;
            this.switchPictureBox7.TabStop = false;
            this.switchPictureBox7.Tag = "Y100阀控制";
            this.switchPictureBox7.TextBackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox7.TextLayout = RW.UI.Controls.TextLayout.Bottom;
            this.switchPictureBox7.TrueImage = global::MainUI.Properties.Resources.mv_Hor_ON;
            // 
            // switchPictureBox8
            // 
            this.switchPictureBox8.BackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox8.CanClick = true;
            this.switchPictureBox8.ClickSwitch = false;
            this.switchPictureBox8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.switchPictureBox8.FalseImage = global::MainUI.Properties.Resources.mv_Hor_OFF;
            resources.ApplyResources(this.switchPictureBox8, "switchPictureBox8");
            this.switchPictureBox8.Image = global::MainUI.Properties.Resources.mv_Hor_OFF;
            this.switchPictureBox8.Index = 13;
            this.switchPictureBox8.Name = "switchPictureBox8";
            this.switchPictureBox8.Switch = false;
            this.switchPictureBox8.TabStop = false;
            this.switchPictureBox8.Tag = "Y111阀控制";
            this.switchPictureBox8.TextBackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox8.TextLayout = RW.UI.Controls.TextLayout.Bottom;
            this.switchPictureBox8.TrueImage = global::MainUI.Properties.Resources.mv_Hor_ON;
            // 
            // switchPictureBox9
            // 
            this.switchPictureBox9.BackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox9.CanClick = true;
            this.switchPictureBox9.ClickSwitch = false;
            this.switchPictureBox9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.switchPictureBox9.FalseImage = global::MainUI.Properties.Resources.mv_Hor_OFF;
            resources.ApplyResources(this.switchPictureBox9, "switchPictureBox9");
            this.switchPictureBox9.Image = global::MainUI.Properties.Resources.mv_Hor_OFF;
            this.switchPictureBox9.Index = 13;
            this.switchPictureBox9.Name = "switchPictureBox9";
            this.switchPictureBox9.Switch = false;
            this.switchPictureBox9.TabStop = false;
            this.switchPictureBox9.Tag = "Y115阀控制";
            this.switchPictureBox9.TextBackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox9.TextLayout = RW.UI.Controls.TextLayout.Bottom;
            this.switchPictureBox9.TrueImage = global::MainUI.Properties.Resources.mv_Hor_ON;
            // 
            // switchPictureBox10
            // 
            this.switchPictureBox10.BackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox10.CanClick = true;
            this.switchPictureBox10.ClickSwitch = false;
            this.switchPictureBox10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.switchPictureBox10.FalseImage = global::MainUI.Properties.Resources.mv_Hor_OFF;
            resources.ApplyResources(this.switchPictureBox10, "switchPictureBox10");
            this.switchPictureBox10.Image = global::MainUI.Properties.Resources.mv_Hor_OFF;
            this.switchPictureBox10.Index = 13;
            this.switchPictureBox10.Name = "switchPictureBox10";
            this.switchPictureBox10.Switch = false;
            this.switchPictureBox10.TabStop = false;
            this.switchPictureBox10.Tag = "Y116阀控制";
            this.switchPictureBox10.TextBackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox10.TextLayout = RW.UI.Controls.TextLayout.Bottom;
            this.switchPictureBox10.TrueImage = global::MainUI.Properties.Resources.mv_Hor_ON;
            // 
            // switchPictureBox11
            // 
            this.switchPictureBox11.BackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox11.CanClick = true;
            this.switchPictureBox11.ClickSwitch = false;
            this.switchPictureBox11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.switchPictureBox11.FalseImage = global::MainUI.Properties.Resources.mv_Hor_OFF;
            resources.ApplyResources(this.switchPictureBox11, "switchPictureBox11");
            this.switchPictureBox11.Image = global::MainUI.Properties.Resources.mv_Hor_OFF;
            this.switchPictureBox11.Index = 13;
            this.switchPictureBox11.Name = "switchPictureBox11";
            this.switchPictureBox11.Switch = false;
            this.switchPictureBox11.TabStop = false;
            this.switchPictureBox11.Tag = "Y116阀控制";
            this.switchPictureBox11.TextBackColor = System.Drawing.Color.Transparent;
            this.switchPictureBox11.TextLayout = RW.UI.Controls.TextLayout.Bottom;
            this.switchPictureBox11.TrueImage = global::MainUI.Properties.Resources.mv_Hor_ON;
            // 
            // frmControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmControl";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmChangePwd_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchPictureBox11)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Sunny.UI.UIButton btnClose;
        private System.Windows.Forms.GroupBox groupBox2;
        private RW.UI.Controls.SwitchPictureBox switchPictureBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private RW.UI.Controls.SwitchPictureBox switchPictureBox3;
        private RW.UI.Controls.SwitchPictureBox switchPictureBox2;
        private RW.UI.Controls.SwitchPictureBox switchPictureBox4;
        private RW.UI.Controls.SwitchPictureBox switchPictureBox6;
        private RW.UI.Controls.SwitchPictureBox switchPictureBox5;
        private RW.UI.Controls.SwitchPictureBox switchPictureBox7;
        private RW.UI.Controls.SwitchPictureBox switchPictureBox8;
        private RW.UI.Controls.SwitchPictureBox switchPictureBox9;
        private RW.UI.Controls.SwitchPictureBox switchPictureBox10;
        private RW.UI.Controls.SwitchPictureBox switchPictureBox11;
    }
}
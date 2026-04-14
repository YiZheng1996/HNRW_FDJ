namespace MainUI.Widget
{
    partial class ucWarning
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.uiGroupBox2 = new Sunny.UI.UIGroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnFaultLog = new Sunny.UI.UIButton();
            this.btnFaultReset = new Sunny.UI.UIButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uiGroupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.flowLayoutPanel1);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiGroupBox2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.uiGroupBox2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Padding = new System.Windows.Forms.Padding(0, 40, 0, 0);
            this.uiGroupBox2.Size = new System.Drawing.Size(1525, 134);
            this.uiGroupBox2.TabIndex = 488;
            this.uiGroupBox2.Text = "故障状态列表";
            this.uiGroupBox2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiGroupBox2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 26);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1519, 97);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnFaultLog
            // 
            this.btnFaultLog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFaultLog.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnFaultLog.Location = new System.Drawing.Point(9, 26);
            this.btnFaultLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFaultLog.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnFaultLog.Name = "btnFaultLog";
            this.btnFaultLog.Size = new System.Drawing.Size(195, 38);
            this.btnFaultLog.StyleCustomMode = true;
            this.btnFaultLog.TabIndex = 489;
            this.btnFaultLog.Text = "故障日志";
            this.btnFaultLog.TipsFont = new System.Drawing.Font("微软雅黑", 13F);
            this.btnFaultLog.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnFaultLog.Click += new System.EventHandler(this.btnFaultLog_Click);
            // 
            // btnFaultReset
            // 
            this.btnFaultReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFaultReset.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnFaultReset.Location = new System.Drawing.Point(9, 86);
            this.btnFaultReset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFaultReset.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnFaultReset.Name = "btnFaultReset";
            this.btnFaultReset.Size = new System.Drawing.Size(195, 38);
            this.btnFaultReset.Style = Sunny.UI.UIStyle.Custom;
            this.btnFaultReset.StyleCustomMode = true;
            this.btnFaultReset.TabIndex = 490;
            this.btnFaultReset.Text = "故障复位";
            this.btnFaultReset.TipsFont = new System.Drawing.Font("微软雅黑", 13F);
            this.btnFaultReset.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnFaultReset.Click += new System.EventHandler(this.btnFaultReset_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFaultLog);
            this.panel1.Controls.Add(this.btnFaultReset);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1534, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(283, 134);
            this.panel1.TabIndex = 491;
            // 
            // ucWarning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.uiGroupBox2);
            this.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucWarning";
            this.Size = new System.Drawing.Size(1817, 134);
            this.Load += new System.EventHandler(this.ucWarning_Load);
            this.uiGroupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Sunny.UI.UIButton btnFaultLog;
        private Sunny.UI.UIButton btnFaultReset;
        private System.Windows.Forms.Panel panel1;
    }
}

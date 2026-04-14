namespace MainUI.HMI_Auto
{
    partial class ucStepStatus
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ucStepItem2 = new MainUI.HMI_Auto.ucStepItem();
            this.ucStepItem4 = new MainUI.HMI_Auto.ucStepItem();
            this.flowLayoutPanelTitle = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanelTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.ucStepItem2);
            this.flowLayoutPanel1.Controls.Add(this.ucStepItem4);
            this.flowLayoutPanel1.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 81);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(877, 471);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // ucStepItem2
            // 
            this.ucStepItem2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ucStepItem2.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucStepItem2.Location = new System.Drawing.Point(4, 4);
            this.ucStepItem2.Margin = new System.Windows.Forms.Padding(4);
            this.ucStepItem2.Name = "ucStepItem2";
            this.ucStepItem2.Size = new System.Drawing.Size(428, 463);
            this.ucStepItem2.TabIndex = 0;
            this.ucStepItem2.Title = "";
            // 
            // ucStepItem4
            // 
            this.ucStepItem4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ucStepItem4.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucStepItem4.Location = new System.Drawing.Point(440, 4);
            this.ucStepItem4.Margin = new System.Windows.Forms.Padding(4);
            this.ucStepItem4.Name = "ucStepItem4";
            this.ucStepItem4.Size = new System.Drawing.Size(428, 463);
            this.ucStepItem4.TabIndex = 2;
            this.ucStepItem4.Title = "";
            // 
            // flowLayoutPanelTitle
            // 
            this.flowLayoutPanelTitle.Controls.Add(this.label1);
            this.flowLayoutPanelTitle.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelTitle.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelTitle.Name = "flowLayoutPanelTitle";
            this.flowLayoutPanelTitle.Size = new System.Drawing.Size(897, 78);
            this.flowLayoutPanelTitle.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(890, 70);
            this.label1.TabIndex = 0;
            this.label1.Text = "请先选择型号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucStepStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.flowLayoutPanelTitle);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucStepStatus";
            this.Size = new System.Drawing.Size(877, 552);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanelTitle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ucStepItem ucStepItem1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTitle;
        private System.Windows.Forms.Label label1;
        private ucStepItem ucStepItem2;
        private ucStepItem ucStepItem4;
    }
}
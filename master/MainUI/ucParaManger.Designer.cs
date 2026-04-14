
namespace MainUI
{
    partial class ucParaManger
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
            this.treeView1 = new Sunny.UI.UITreeView();
            this.panelMain = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.FillColor = System.Drawing.Color.White;
            this.treeView1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.treeView1.MinimumSize = new System.Drawing.Size(1, 1);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowText = false;
            this.treeView1.Size = new System.Drawing.Size(203, 997);
            this.treeView1.Style = Sunny.UI.UIStyle.Custom;
            this.treeView1.TabIndex = 1;
            this.treeView1.Text = "uiTreeView1";
            this.treeView1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.treeView1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.treeView1.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeSelect);
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(203, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1625, 997);
            this.panelMain.TabIndex = 2;
            // 
            // ucParaManger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.treeView1);
            this.Name = "ucParaManger";
            this.Size = new System.Drawing.Size(1828, 997);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITreeView treeView1;
        private System.Windows.Forms.Panel panelMain;
    }
}

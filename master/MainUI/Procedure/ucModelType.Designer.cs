namespace MainUI.Procedure
{
    partial class ucModelType
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
            this.lstType = new System.Windows.Forms.ListBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.uiButton1 = new Sunny.UI.UIButton();
            this.btnEdit = new Sunny.UI.UIButton();
            this.uiButton2 = new Sunny.UI.UIButton();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.txtName = new Sunny.UI.UITextBox();
            this.txtID = new Sunny.UI.UITextBox();
            this.uiGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstType
            // 
            this.lstType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.lstType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstType.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lstType.FormattingEnabled = true;
            this.lstType.ItemHeight = 21;
            this.lstType.Location = new System.Drawing.Point(0, 53);
            this.lstType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstType.Name = "lstType";
            this.lstType.Size = new System.Drawing.Size(346, 701);
            this.lstType.TabIndex = 1;
            this.lstType.SelectedIndexChanged += new System.EventHandler(this.lstType_SelectedIndexChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("宋体", 11F);
            this.lblName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblName.Location = new System.Drawing.Point(376, 143);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(67, 15);
            this.lblName.TabIndex = 10;
            this.lblName.Text = "型号类别";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("宋体", 11F);
            this.lblID.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblID.Location = new System.Drawing.Point(378, 70);
            this.lblID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(113, 15);
            this.lblID.TabIndex = 10;
            this.lblID.Text = "编号[自动生成]";
            // 
            // uiButton1
            // 
            this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.uiButton1.Location = new System.Drawing.Point(378, 243);
            this.uiButton1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiButton1.MinimumSize = new System.Drawing.Size(2, 2);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(120, 40);
            this.uiButton1.TabIndex = 394;
            this.uiButton1.Text = "添加";
            this.uiButton1.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton1.TipsText = "1";
            this.uiButton1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiButton1.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnEdit.Location = new System.Drawing.Point(378, 343);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEdit.MinimumSize = new System.Drawing.Size(2, 2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(120, 40);
            this.btnEdit.TabIndex = 396;
            this.btnEdit.Text = "更改";
            this.btnEdit.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEdit.TipsText = "1";
            this.btnEdit.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnEdit.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // uiButton2
            // 
            this.uiButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.uiButton2.Location = new System.Drawing.Point(378, 443);
            this.uiButton2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiButton2.MinimumSize = new System.Drawing.Size(2, 2);
            this.uiButton2.Name = "uiButton2";
            this.uiButton2.Size = new System.Drawing.Size(120, 40);
            this.uiButton2.TabIndex = 397;
            this.uiButton2.Text = "删除";
            this.uiButton2.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton2.TipsText = "1";
            this.uiButton2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiButton2.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.lstType);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiGroupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(2, 2);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 53, 0, 0);
            this.uiGroupBox1.Size = new System.Drawing.Size(346, 754);
            this.uiGroupBox1.TabIndex = 398;
            this.uiGroupBox1.Text = "类别列表";
            this.uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiGroupBox1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txtName
            // 
            this.txtName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtName.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.txtName.Location = new System.Drawing.Point(378, 163);
            this.txtName.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.txtName.MinimumSize = new System.Drawing.Size(2, 27);
            this.txtName.Name = "txtName";
            this.txtName.ShowText = false;
            this.txtName.Size = new System.Drawing.Size(188, 29);
            this.txtName.TabIndex = 405;
            this.txtName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtName.Watermark = "请输入";
            this.txtName.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txtID
            // 
            this.txtID.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtID.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.txtID.Location = new System.Drawing.Point(378, 90);
            this.txtID.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.txtID.MinimumSize = new System.Drawing.Size(2, 27);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.ShowText = false;
            this.txtID.Size = new System.Drawing.Size(188, 29);
            this.txtID.TabIndex = 405;
            this.txtID.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtID.Watermark = "请输入";
            this.txtID.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // ucModelType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.uiButton2);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.uiButton1);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.lblName);
            this.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ucModelType";
            this.Size = new System.Drawing.Size(1178, 754);
            this.Load += new System.EventHandler(this.ucModelType_Load);
            this.uiGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ListBox lstType;
        protected System.Windows.Forms.Label lblName;
        protected System.Windows.Forms.Label lblID;
        private Sunny.UI.UIButton uiButton1;
        private Sunny.UI.UIButton btnEdit;
        private Sunny.UI.UIButton uiButton2;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UITextBox txtName;
        private Sunny.UI.UITextBox txtID;
    }
}

namespace MainUI
{
    partial class frmSetOutValue
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
            this.Btn_Ok = new Sunny.UI.UIButton();
            this.Btn_Exit = new Sunny.UI.UIButton();
            this.grpDI = new Sunny.UI.UIGroupBox();
            this.btnSub10 = new Sunny.UI.UIButton();
            this.btnSub = new Sunny.UI.UIButton();
            this.btnAdd10 = new Sunny.UI.UIButton();
            this.btnAdd = new Sunny.UI.UIButton();
            this.nudOutputValue = new Sunny.UI.UITextBox();
            this.btnKey = new Sunny.UI.UIButton();
            this.grpDI.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Ok
            // 
            this.Btn_Ok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Ok.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.Btn_Ok.Location = new System.Drawing.Point(225, 139);
            this.Btn_Ok.MinimumSize = new System.Drawing.Size(1, 1);
            this.Btn_Ok.Name = "Btn_Ok";
            this.Btn_Ok.Size = new System.Drawing.Size(127, 58);
            this.Btn_Ok.TabIndex = 1;
            this.Btn_Ok.Text = "确定";
            this.Btn_Ok.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Ok.TipsText = "1";
            this.Btn_Ok.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.Btn_Ok.Click += new System.EventHandler(this.Btn_Ok_Click);
            // 
            // Btn_Exit
            // 
            this.Btn_Exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Btn_Exit.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.Btn_Exit.Location = new System.Drawing.Point(448, 139);
            this.Btn_Exit.MinimumSize = new System.Drawing.Size(1, 1);
            this.Btn_Exit.Name = "Btn_Exit";
            this.Btn_Exit.Size = new System.Drawing.Size(127, 58);
            this.Btn_Exit.TabIndex = 2;
            this.Btn_Exit.Text = "取消";
            this.Btn_Exit.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Exit.TipsText = "1";
            this.Btn_Exit.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.Btn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // grpDI
            // 
            this.grpDI.Controls.Add(this.btnSub10);
            this.grpDI.Controls.Add(this.btnSub);
            this.grpDI.Controls.Add(this.btnAdd10);
            this.grpDI.Controls.Add(this.btnAdd);
            this.grpDI.Controls.Add(this.nudOutputValue);
            this.grpDI.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.grpDI.Location = new System.Drawing.Point(12, 14);
            this.grpDI.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpDI.MinimumSize = new System.Drawing.Size(1, 1);
            this.grpDI.Name = "grpDI";
            this.grpDI.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.grpDI.Size = new System.Drawing.Size(563, 105);
            this.grpDI.TabIndex = 391;
            this.grpDI.Text = "请输入数值";
            this.grpDI.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.grpDI.TitleAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.grpDI.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnSub10
            // 
            this.btnSub10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSub10.Font = new System.Drawing.Font("微软雅黑", 28F);
            this.btnSub10.Location = new System.Drawing.Point(3, 37);
            this.btnSub10.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSub10.Name = "btnSub10";
            this.btnSub10.Size = new System.Drawing.Size(72, 54);
            this.btnSub10.TabIndex = 3;
            this.btnSub10.Tag = "10";
            this.btnSub10.Text = "-10";
            this.btnSub10.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSub10.TipsText = "1";
            this.btnSub10.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnSub10.Click += new System.EventHandler(this.btnSub_Click);
            // 
            // btnSub
            // 
            this.btnSub.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSub.Font = new System.Drawing.Font("微软雅黑", 28F);
            this.btnSub.Location = new System.Drawing.Point(81, 37);
            this.btnSub.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSub.Name = "btnSub";
            this.btnSub.Size = new System.Drawing.Size(72, 54);
            this.btnSub.TabIndex = 3;
            this.btnSub.Tag = "1";
            this.btnSub.Text = "-";
            this.btnSub.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSub.TipsText = "1";
            this.btnSub.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnSub.Click += new System.EventHandler(this.btnSub_Click);
            // 
            // btnAdd10
            // 
            this.btnAdd10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd10.Font = new System.Drawing.Font("微软雅黑", 28F);
            this.btnAdd10.Location = new System.Drawing.Point(487, 37);
            this.btnAdd10.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnAdd10.Name = "btnAdd10";
            this.btnAdd10.Size = new System.Drawing.Size(72, 54);
            this.btnAdd10.TabIndex = 3;
            this.btnAdd10.Tag = "10";
            this.btnAdd10.Text = "+10";
            this.btnAdd10.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd10.TipsText = "1";
            this.btnAdd10.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnAdd10.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 28F);
            this.btnAdd.Location = new System.Drawing.Point(409, 37);
            this.btnAdd.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(72, 54);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Tag = "1";
            this.btnAdd.Text = "+";
            this.btnAdd.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.TipsText = "1";
            this.btnAdd.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // nudOutputValue
            // 
            this.nudOutputValue.ButtonWidth = 100;
            this.nudOutputValue.CanEmpty = true;
            this.nudOutputValue.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nudOutputValue.FocusedSelectAll = true;
            this.nudOutputValue.Font = new System.Drawing.Font("微软雅黑", 26F);
            this.nudOutputValue.Location = new System.Drawing.Point(160, 37);
            this.nudOutputValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudOutputValue.Maximum = 150D;
            this.nudOutputValue.MaxLength = 5;
            this.nudOutputValue.Minimum = 0D;
            this.nudOutputValue.MinimumSize = new System.Drawing.Size(1, 1);
            this.nudOutputValue.Name = "nudOutputValue";
            this.nudOutputValue.Padding = new System.Windows.Forms.Padding(5);
            this.nudOutputValue.ShowText = false;
            this.nudOutputValue.Size = new System.Drawing.Size(242, 54);
            this.nudOutputValue.TabIndex = 0;
            this.nudOutputValue.Text = "0.00";
            this.nudOutputValue.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.nudOutputValue.Type = Sunny.UI.UITextBox.UIEditType.Double;
            this.nudOutputValue.Watermark = "请输入";
            this.nudOutputValue.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.nudOutputValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nudOutputValue_KeyPress);
            // 
            // btnKey
            // 
            this.btnKey.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKey.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.btnKey.Location = new System.Drawing.Point(12, 139);
            this.btnKey.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnKey.Name = "btnKey";
            this.btnKey.Size = new System.Drawing.Size(127, 58);
            this.btnKey.TabIndex = 392;
            this.btnKey.Text = "小键盘";
            this.btnKey.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnKey.TipsText = "1";
            this.btnKey.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnKey.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // frmSetOutValue
            // 
            this.AcceptButton = this.Btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.CancelButton = this.Btn_Exit;
            this.ClientSize = new System.Drawing.Size(588, 211);
            this.ControlBox = false;
            this.Controls.Add(this.btnKey);
            this.Controls.Add(this.grpDI);
            this.Controls.Add(this.Btn_Exit);
            this.Controls.Add(this.Btn_Ok);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.MaximizeBox = false;
            this.Name = "frmSetOutValue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "输出值设定";
            this.Load += new System.EventHandler(this.frmSetOutValue_Load);
            this.grpDI.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIButton Btn_Ok;
        private Sunny.UI.UIButton Btn_Exit;
        private Sunny.UI.UIGroupBox grpDI;
        private Sunny.UI.UITextBox nudOutputValue;
        private Sunny.UI.UIButton btnSub;
        private Sunny.UI.UIButton btnAdd;
        private Sunny.UI.UIButton btnKey;
        private Sunny.UI.UIButton btnSub10;
        private Sunny.UI.UIButton btnAdd10;
    }
}
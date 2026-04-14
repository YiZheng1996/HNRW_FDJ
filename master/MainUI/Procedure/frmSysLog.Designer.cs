namespace MainUI
{
    partial class frmSysLog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.dtpTo = new Sunny.UI.UIDatePicker();
            this.dtpFrom = new Sunny.UI.UIDatePicker();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.btnSearch1 = new Sunny.UI.UIButton();
            this.btnClose1 = new Sunny.UI.UIButton();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.cboTester = new Sunny.UI.UIComboBox();
            this.grpRecord = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new Sunny.UI.UIDataGridView();
            this.uiGroupBox1.SuspendLayout();
            this.grpRecord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // uiLabel5
            // 
            this.uiLabel5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel5.Location = new System.Drawing.Point(533, 41);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(25, 23);
            this.uiLabel5.TabIndex = 396;
            this.uiLabel5.Text = "至";
            this.uiLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel5.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // dtpTo
            // 
            this.dtpTo.DateFormat = "yyyy-MM-dd HH:mm";
            this.dtpTo.FillColor = System.Drawing.Color.White;
            this.dtpTo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpTo.Location = new System.Drawing.Point(561, 38);
            this.dtpTo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpTo.MaxLength = 16;
            this.dtpTo.MinimumSize = new System.Drawing.Size(63, 0);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.dtpTo.Size = new System.Drawing.Size(165, 29);
            this.dtpTo.SymbolDropDown = 61555;
            this.dtpTo.SymbolNormal = 61555;
            this.dtpTo.TabIndex = 395;
            this.dtpTo.Text = "2023-02-01 16:20";
            this.dtpTo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.dtpTo.Value = new System.DateTime(2023, 2, 1, 16, 20, 20, 721);
            this.dtpTo.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // dtpFrom
            // 
            this.dtpFrom.DateFormat = "yyyy-MM-dd HH:mm";
            this.dtpFrom.FillColor = System.Drawing.Color.White;
            this.dtpFrom.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpFrom.Location = new System.Drawing.Point(359, 38);
            this.dtpFrom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpFrom.MaxLength = 16;
            this.dtpFrom.MinimumSize = new System.Drawing.Size(63, 0);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.dtpFrom.Size = new System.Drawing.Size(165, 29);
            this.dtpFrom.SymbolDropDown = 61555;
            this.dtpFrom.SymbolNormal = 61555;
            this.dtpFrom.TabIndex = 394;
            this.dtpFrom.Text = "2023-02-01 16:20";
            this.dtpFrom.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.dtpFrom.Value = new System.DateTime(2023, 2, 1, 16, 20, 20, 721);
            this.dtpFrom.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel4
            // 
            this.uiLabel4.AutoSize = true;
            this.uiLabel4.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel4.Location = new System.Drawing.Point(282, 41);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(74, 21);
            this.uiLabel4.TabIndex = 393;
            this.uiLabel4.Text = "操作时间";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnSearch1
            // 
            this.btnSearch1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnSearch1.Location = new System.Drawing.Point(739, 26);
            this.btnSearch1.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSearch1.Name = "btnSearch1";
            this.btnSearch1.Size = new System.Drawing.Size(106, 51);
            this.btnSearch1.TabIndex = 397;
            this.btnSearch1.Text = "搜索";
            this.btnSearch1.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch1.TipsText = "1";
            this.btnSearch1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnSearch1.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClose1
            // 
            this.btnClose1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnClose1.Location = new System.Drawing.Point(880, 26);
            this.btnClose1.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnClose1.Name = "btnClose1";
            this.btnClose1.Size = new System.Drawing.Size(106, 51);
            this.btnClose1.TabIndex = 397;
            this.btnClose1.Text = "关闭";
            this.btnClose1.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose1.TipsText = "1";
            this.btnClose1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnClose1.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.uiLabel3);
            this.uiGroupBox1.Controls.Add(this.cboTester);
            this.uiGroupBox1.Controls.Add(this.btnClose1);
            this.uiGroupBox1.Controls.Add(this.btnSearch1);
            this.uiGroupBox1.Controls.Add(this.uiLabel5);
            this.uiGroupBox1.Controls.Add(this.dtpTo);
            this.uiGroupBox1.Controls.Add(this.uiLabel4);
            this.uiGroupBox1.Controls.Add(this.dtpFrom);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox1.Size = new System.Drawing.Size(1008, 105);
            this.uiGroupBox1.TabIndex = 398;
            this.uiGroupBox1.Text = "查询条件";
            this.uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiGroupBox1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel3
            // 
            this.uiLabel3.AutoSize = true;
            this.uiLabel3.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.uiLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.uiLabel3.Location = new System.Drawing.Point(18, 41);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(69, 20);
            this.uiLabel3.TabIndex = 398;
            this.uiLabel3.Text = "操作人员";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cboTester
            // 
            this.cboTester.DataSource = null;
            this.cboTester.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cboTester.FillColor = System.Drawing.Color.White;
            this.cboTester.FilterMaxCount = 50;
            this.cboTester.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.cboTester.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.cboTester.Location = new System.Drawing.Point(88, 38);
            this.cboTester.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboTester.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboTester.Name = "cboTester";
            this.cboTester.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cboTester.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.cboTester.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            this.cboTester.Size = new System.Drawing.Size(165, 29);
            this.cboTester.TabIndex = 399;
            this.cboTester.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboTester.Watermark = "请选择";
            this.cboTester.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // grpRecord
            // 
            this.grpRecord.Controls.Add(this.dataGridView1);
            this.grpRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRecord.Font = new System.Drawing.Font("宋体", 11F);
            this.grpRecord.Location = new System.Drawing.Point(0, 105);
            this.grpRecord.Name = "grpRecord";
            this.grpRecord.Size = new System.Drawing.Size(1008, 541);
            this.grpRecord.TabIndex = 399;
            this.grpRecord.TabStop = false;
            this.grpRecord.Text = "记录列表";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridView1.ColumnHeadersHeight = 32;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.dataGridView1.Location = new System.Drawing.Point(3, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectedIndex = -1;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1002, 518);
            this.dataGridView1.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.dataGridView1.TabIndex = 392;
            this.dataGridView1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // frmSysLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1008, 646);
            this.Controls.Add(this.grpRecord);
            this.Controls.Add(this.uiGroupBox1);
            this.Name = "frmSysLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统日志";
            this.Load += new System.EventHandler(this.frmSysLog_Load);
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.grpRecord.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UILabel uiLabel5;
        private Sunny.UI.UIDatePicker dtpTo;
        private Sunny.UI.UIDatePicker dtpFrom;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UIButton btnClose1;
        private Sunny.UI.UIButton btnSearch1;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UIComboBox cboTester;
        private System.Windows.Forms.GroupBox grpRecord;
        private Sunny.UI.UIDataGridView dataGridView1;
    }
}
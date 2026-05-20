namespace MainUI.Demo.UI
{
    partial class FrmSimLogViewer
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.ComboBox cboLevel;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox chkAutoScroll;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.Label lblTip;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.lblFilter = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.cboLevel = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.chkAutoScroll = new System.Windows.Forms.CheckBox();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblStats = new System.Windows.Forms.Label();
            this.lblTip = new System.Windows.Forms.Label();
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);

            // ── 工具栏 ──
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Height = 46;
            this.pnlToolbar.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);

            this.lblFilter.Text = "筛选:";
            this.lblFilter.Location = new System.Drawing.Point(12, 14);
            this.lblFilter.Size = new System.Drawing.Size(45, 22);
            this.lblFilter.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblFilter.ForeColor = System.Drawing.Color.FromArgb(80, 90, 105);

            this.cboCategory.Location = new System.Drawing.Point(56, 11);
            this.cboCategory.Size = new System.Drawing.Size(110, 24);
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cboCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCategory.SelectedIndexChanged += new System.EventHandler(this.OnFilterChanged);

            this.cboLevel.Location = new System.Drawing.Point(172, 11);
            this.cboLevel.Size = new System.Drawing.Size(95, 24);
            this.cboLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLevel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cboLevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboLevel.SelectedIndexChanged += new System.EventHandler(this.OnFilterChanged);

            this.txtSearch.Location = new System.Drawing.Point(275, 12);
            this.txtSearch.Size = new System.Drawing.Size(220, 24);
            this.txtSearch.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.TextChanged += new System.EventHandler(this.OnFilterChanged);

            this.chkAutoScroll.Text = "自动滚动";
            this.chkAutoScroll.Checked = true;
            this.chkAutoScroll.Location = new System.Drawing.Point(510, 13);
            this.chkAutoScroll.Size = new System.Drawing.Size(85, 22);
            this.chkAutoScroll.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.chkAutoScroll.ForeColor = System.Drawing.Color.FromArgb(80, 90, 105);

            StyleToolBtn(this.btnPause, "⏸ 暂停刷新", 605, 10, 95);
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);

            StyleToolBtn(this.btnExport, "📥 导出 CSV", 705, 10, 95);
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(40, 120, 60);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);

            StyleToolBtn(this.btnClear, "清空显示", 805, 10, 85);
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            this.lblStats.Location = new System.Drawing.Point(900, 14);
            this.lblStats.Size = new System.Drawing.Size(260, 22);
            this.lblStats.Text = "总计 0 条";
            this.lblStats.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblStats.ForeColor = System.Drawing.Color.FromArgb(40, 120, 60);

            this.pnlToolbar.Controls.Add(this.lblFilter);
            this.pnlToolbar.Controls.Add(this.cboCategory);
            this.pnlToolbar.Controls.Add(this.cboLevel);
            this.pnlToolbar.Controls.Add(this.txtSearch);
            this.pnlToolbar.Controls.Add(this.chkAutoScroll);
            this.pnlToolbar.Controls.Add(this.btnPause);
            this.pnlToolbar.Controls.Add(this.btnExport);
            this.pnlToolbar.Controls.Add(this.btnClear);
            this.pnlToolbar.Controls.Add(this.lblStats);

            // 提示行
            this.lblTip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTip.Height = 24;
            this.lblTip.Text = "  💡 双击任意一行可弹出完整因果链详情（同一 TraceId 的全部日志串联展示）";
            this.lblTip.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblTip.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
            this.lblTip.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.lblTip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── DataGridView ──
            this.dgvLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLogs.AllowUserToAddRows = false;
            this.dgvLogs.AllowUserToDeleteRows = false;
            this.dgvLogs.AllowUserToResizeRows = false;
            this.dgvLogs.ReadOnly = true;
            this.dgvLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLogs.MultiSelect = false;
            this.dgvLogs.RowHeadersVisible = false;
            this.dgvLogs.AutoGenerateColumns = false;
            this.dgvLogs.BackgroundColor = System.Drawing.Color.White;
            this.dgvLogs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLogs.GridColor = System.Drawing.Color.FromArgb(237, 241, 246);
            this.dgvLogs.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvLogs.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(40, 50, 65);
            this.dgvLogs.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(217, 232, 250);
            this.dgvLogs.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(20, 40, 80);
            this.dgvLogs.DefaultCellStyle.Font = new System.Drawing.Font("Consolas", 9F);
            this.dgvLogs.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 252, 254);
            this.dgvLogs.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(243, 246, 250);
            this.dgvLogs.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(60, 70, 85);
            this.dgvLogs.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.dgvLogs.EnableHeadersVisualStyles = false;
            this.dgvLogs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvLogs.ColumnHeadersHeight = 30;
            this.dgvLogs.RowTemplate.Height = 24;
            this.dgvLogs.SelectionChanged += new System.EventHandler(this.dgvLogs_SelectionChanged);
            this.dgvLogs.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLogs_CellDoubleClick);
            this.dgvLogs.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvLogs_CellFormatting);

            // ── 定时刷新 ──
            this.refreshTimer.Interval = 300;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);

            // ── 窗体 ──
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 680);
            this.Controls.Add(this.dgvLogs);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.pnlToolbar);
            this.BackColor = System.Drawing.Color.White;
            this.Text = "仿真器日志查看器 — 测试报告原始数据";
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FrmSimLogViewer_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSimLogViewer_FormClosing);
        }

        private static void StyleToolBtn(System.Windows.Forms.Button btn, string text, int x, int y, int w)
        {
            btn.Location = new System.Drawing.Point(x, y);
            btn.Size = new System.Drawing.Size(w, 28);
            btn.Text = text;
            btn.Font = new System.Drawing.Font("微软雅黑", 9F);
            btn.BackColor = System.Drawing.Color.White;
            btn.ForeColor = System.Drawing.Color.FromArgb(60, 70, 85);
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(217, 225, 235);
        }
    }
}

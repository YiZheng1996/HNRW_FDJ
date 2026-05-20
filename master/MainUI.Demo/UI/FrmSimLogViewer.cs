using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MainUI.Demo.Core;

namespace MainUI.Demo.UI
{
    public partial class FrmSimLogViewer : Form
    {
        private readonly ConcurrentQueue<LogEntry> _incoming = new ConcurrentQueue<LogEntry>();
        private readonly List<LogEntry> _allRows = new List<LogEntry>();
        private List<LogRowVm> _displayed = new List<LogRowVm>();
        private bool _paused;

        public FrmSimLogViewer()
        {
            InitializeComponent();
            BuildColumns();
            BuildFilters();
        }

        #region 初始化

        private void BuildColumns()
        {
            dgvLogs.Columns.Clear();
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "时间", DataPropertyName = "TimeText", Width = 110 });
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "类别", DataPropertyName = "CategoryText", Width = 80 });
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "等级", DataPropertyName = "LevelText", Width = 70 });
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "TraceId", DataPropertyName = "TraceId", Width = 90 });
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "信号", DataPropertyName = "Signal", Width = 160 });
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "旧→新", DataPropertyName = "ValueText", Width = 140 });
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "描述", DataPropertyName = "Message", Width = 380,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "来源", DataPropertyName = "Source", Width = 110 });
        }

        private void BuildFilters()
        {
            cboCategory.Items.Clear();
            cboCategory.Items.Add("全部类别");
            foreach (var c in Enum.GetNames(typeof(LogCategory))) cboCategory.Items.Add(c);
            cboCategory.SelectedIndex = 0;

            cboLevel.Items.Clear();
            cboLevel.Items.Add("全部等级");
            foreach (var l in Enum.GetNames(typeof(LogLevel))) cboLevel.Items.Add(l);
            cboLevel.SelectedIndex = 0;
        }

        #endregion

        #region 生命周期

        private void FrmSimLogViewer_Load(object sender, EventArgs e)
        {
            try
            {
                var recent = SimulationLogger.Instance.RecentLogs(2000);
                recent.Reverse();
                _allRows.AddRange(recent);
                RefreshGrid();
            }
            catch { }

            SimulationLogger.Instance.OnLogAppended += OnLogAppended;
            refreshTimer.Start();
        }

        private void FrmSimLogViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            refreshTimer.Stop();
            try { SimulationLogger.Instance.OnLogAppended -= OnLogAppended; } catch { }
        }

        #endregion

        #region 数据流入

        private void OnLogAppended(LogEntry entry) { _incoming.Enqueue(entry); }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (_paused) return;
            if (_incoming.IsEmpty) return;

            bool gotNew = false;
            while (_incoming.TryDequeue(out var entry))
            {
                _allRows.Add(entry);
                gotNew = true;
            }

            const int MAX = 3000;
            if (_allRows.Count > MAX) _allRows.RemoveRange(0, _allRows.Count - MAX);

            if (gotNew) RefreshGrid();
        }

        #endregion

        #region 渲染

        private void RefreshGrid()
        {
            var filtered = ApplyFilter(_allRows);
            _displayed = filtered.Select(e => new LogRowVm(e)).ToList();

            dgvLogs.DataSource = null;
            dgvLogs.DataSource = _displayed;

            lblStats.Text = $"显示 {_displayed.Count:N0} / 总 {_allRows.Count:N0} 条";

            if (chkAutoScroll.Checked && dgvLogs.RowCount > 0)
                dgvLogs.FirstDisplayedScrollingRowIndex = dgvLogs.RowCount - 1;
        }

        private IEnumerable<LogEntry> ApplyFilter(IEnumerable<LogEntry> src)
        {
            int catIdx = cboCategory.SelectedIndex;
            int lvlIdx = cboLevel.SelectedIndex;
            string kw = (txtSearch.Text ?? "").Trim().ToLowerInvariant();

            return src.Where(e =>
            {
                if (catIdx > 0 && (int)e.Category != (catIdx - 1)) return false;
                if (lvlIdx > 0 && (int)e.Level != (lvlIdx - 1)) return false;
                if (kw.Length > 0)
                {
                    string hay = ((e.Message ?? "") + " " + (e.Signal ?? "") + " " +
                                  (e.TraceId ?? "") + " " + (e.Source ?? "")).ToLowerInvariant();
                    if (!hay.Contains(kw)) return false;
                }
                return true;
            });
        }

        private void dgvLogs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _displayed.Count) return;
            var vm = _displayed[e.RowIndex];

            // 类别列染底色
            if (e.ColumnIndex == 1)
            {
                Color bg, fg;
                switch (vm.Entry.Category)
                {
                    case LogCategory.Injection:     bg = Color.FromArgb(217, 232, 250); fg = Color.FromArgb(12, 68, 124); break;
                    case LogCategory.Response:      bg = Color.FromArgb(213, 240, 224); fg = Color.FromArgb(15, 110, 86); break;
                    case LogCategory.Scenario:      bg = Color.FromArgb(255, 230, 200); fg = Color.FromArgb(133, 79, 11); break;
                    case LogCategory.Operator:      bg = Color.FromArgb(255, 240, 210); fg = Color.FromArgb(133, 79, 11); break;
                    case LogCategory.Audit:         bg = Color.FromArgb(225, 213, 242); fg = Color.FromArgb(60, 52, 137); break;
                    case LogCategory.Business:      bg = Color.FromArgb(225, 245, 238); fg = Color.FromArgb(15, 110, 86); break;
                    case LogCategory.Communication: bg = Color.FromArgb(213, 232, 240); fg = Color.FromArgb(12, 68, 124); break;
                    default:                        bg = Color.FromArgb(241, 239, 232); fg = Color.FromArgb(95, 94, 90); break;
                }
                e.CellStyle.BackColor = bg;
                e.CellStyle.ForeColor = fg;
                e.CellStyle.Font = new Font("微软雅黑", 8.5F, FontStyle.Bold);
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            // 等级列染色
            else if (e.ColumnIndex == 2)
            {
                Color fg;
                switch (vm.Entry.Level)
                {
                    case LogLevel.Critical: fg = Color.FromArgb(163, 45, 45); break;
                    case LogLevel.Fault:    fg = Color.FromArgb(216, 90, 48); break;
                    case LogLevel.Warn:     fg = Color.FromArgb(186, 117, 23); break;
                    case LogLevel.OK:       fg = Color.FromArgb(27, 122, 63); break;
                    case LogLevel.Info:     fg = Color.FromArgb(80, 90, 105); break;
                    default:                fg = Color.FromArgb(140, 140, 140); break;
                }
                e.CellStyle.ForeColor = fg;
                e.CellStyle.Font = new Font("微软雅黑", 8.5F, FontStyle.Bold);
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            // TraceId 用紫色
            else if (e.ColumnIndex == 3 && !string.IsNullOrEmpty(vm.Entry.TraceId))
            {
                e.CellStyle.ForeColor = Color.FromArgb(83, 74, 183);
            }
            // 故障级整行加底色
            if (vm.Entry.Level >= LogLevel.Fault && e.ColumnIndex >= 4)
            {
                var current = e.CellStyle.BackColor;
                if (current == Color.White || current == Color.FromArgb(250, 252, 254))
                {
                    e.CellStyle.BackColor = Color.FromArgb(252, 235, 235);
                }
            }
        }

        #endregion

        #region 选中/双击

        private void dgvLogs_SelectionChanged(object sender, EventArgs e) { /* 暂留 */ }

        private void dgvLogs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _displayed.Count) return;
            var entry = _displayed[e.RowIndex].Entry;

            if (string.IsNullOrEmpty(entry.TraceId))
            {
                MessageBox.Show(this, "该日志无 TraceId，不属于任何因果链。",
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var chain = SimulationLogger.Instance.QueryByTrace(entry.TraceId);
            if (chain == null || chain.Count == 0)
            {
                MessageBox.Show(this, "未找到对应的因果链。", "提示");
                return;
            }

            using (var dlg = new FrmTraceDetail(entry.TraceId, chain))
            {
                dlg.ShowDialog(this);
            }
        }

        #endregion

        #region 工具栏

        private void OnFilterChanged(object sender, EventArgs e) { RefreshGrid(); }

        private void btnPause_Click(object sender, EventArgs e)
        {
            _paused = !_paused;
            btnPause.Text = _paused ? "▶ 继续刷新" : "⏸ 暂停刷新";
            btnPause.BackColor = _paused
                ? Color.FromArgb(255, 240, 210)
                : Color.White;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "确定清空当前显示的日志吗？\n（仅清空显示，不影响存盘和数据库）",
                "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            _allRows.Clear();
            _displayed.Clear();
            dgvLogs.DataSource = null;
            lblStats.Text = "已清空";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog
            {
                Filter = "CSV 文件 (*.csv)|*.csv",
                FileName = $"SimLog_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
            })
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                var entries = ApplyFilter(_allRows).ToList();
                bool ok = SimulationLogger.Instance.ExportCsv(dlg.FileName, entries);
                if (ok)
                {
                    SimulationLogger.Instance.LogOperator("导出日志 CSV", $"{entries.Count}条 → {dlg.FileName}");
                    MessageBox.Show(this, $"已导出 {entries.Count:N0} 条日志:\n{dlg.FileName}",
                        "导出成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "导出失败。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        #endregion

        #region ViewModel

        private class LogRowVm
        {
            public LogEntry Entry { get; }
            public LogRowVm(LogEntry e) { Entry = e; }
            public string TimeText => Entry.Timestamp.ToString("HH:mm:ss.fff");
            public string CategoryText => Entry.Category.ToString();
            public string LevelText => Entry.Level.ToString();
            public string TraceId => Entry.TraceId ?? "";
            public string Signal => Entry.Signal ?? "";
            public string ValueText => (string.IsNullOrEmpty(Entry.OldValue) && string.IsNullOrEmpty(Entry.NewValue))
                ? "" : $"{Entry.OldValue ?? "-"} → {Entry.NewValue ?? "-"}";
            public string Message => Entry.Message ?? "";
            public string Source => Entry.Source ?? "";
        }

        #endregion
    }
}

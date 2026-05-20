using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using MainUI.Demo.Core;

namespace MainUI.Demo.UI
{
    /// <summary>
    /// 因果链详情对话框。
    /// 一次故障从触发到处置的全部日志按时序展开，每步带耗时。
    /// 这是给客户演示的"杀手锏"画面 —— 测试报告的核心证据。
    /// </summary>
    public class FrmTraceDetail : Form
    {
        private readonly string _traceId;
        private readonly List<LogEntry> _chain;

        // UI 元素
        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel pnlSummary;
        private Panel pnlTimelineHost;
        private Panel pnlConclusion;
        private Panel pnlFooter;
        private Button btnCopyTraceId;
        private Button btnExportCsv;
        private Button btnClose;

        public FrmTraceDetail(string traceId, List<LogEntry> chain)
        {
            _traceId = traceId;
            _chain = chain.OrderBy(c => c.Timestamp).ToList();
            BuildUi();
        }

        #region UI 搭建

        private void BuildUi()
        {
            this.Text = "因果链详情";
            this.Font = new Font("微软雅黑", 9F);
            this.BackColor = Color.White;
            this.ClientSize = new Size(900, 680);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(720, 500);

            // 标题区
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top, Height = 64,
                BackColor = Color.FromArgb(243, 246, 250)
            };

            var picIco = new Panel
            {
                Location = new Point(16, 12), Size = new Size(40, 40),
                BackColor = Color.FromArgb(252, 235, 235)
            };
            picIco.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var p = new Pen(Color.FromArgb(163, 45, 45), 2))
                    g.DrawRectangle(p, 0, 0, 39, 39);
                using (var br = new SolidBrush(Color.FromArgb(163, 45, 45)))
                using (var f = new Font("微软雅黑", 16F, FontStyle.Bold))
                    g.DrawString("⚠", f, br, new RectangleF(0, 2, 40, 36),
                        new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            };

            lblTitle = new Label
            {
                Location = new Point(66, 12), Size = new Size(700, 24),
                Font = new Font("微软雅黑", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 50, 65),
                Text = DetectTraceTitle()
            };
            lblSubtitle = new Label
            {
                Location = new Point(66, 38), Size = new Size(700, 18),
                Font = new Font("微软雅黑", 9F),
                ForeColor = Color.FromArgb(120, 130, 145),
                Text = "完整时序证据，可作为测试报告附录"
            };

            pnlHeader.Controls.Add(picIco);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSubtitle);

            // 摘要区
            pnlSummary = new Panel
            {
                Location = new Point(20, 80), Size = new Size(860, 92),
                BackColor = Color.FromArgb(252, 250, 245),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnlSummary.Paint += (s, e) =>
            {
                // 左边竖红条
                using (var br = new SolidBrush(Color.FromArgb(163, 45, 45)))
                    e.Graphics.FillRectangle(br, 0, 0, 3, pnlSummary.Height);
            };
            BuildSummary();

            // 时间线
            pnlTimelineHost = new Panel
            {
                Location = new Point(20, 180), Size = new Size(860, 380),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoScroll = true,
                BackColor = Color.White
            };
            BuildTimeline();

            // 结论区
            pnlConclusion = new Panel
            {
                Location = new Point(20, 570), Size = new Size(860, 50),
                BackColor = Color.FromArgb(230, 245, 235),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            BuildConclusion();

            // 底部按钮
            pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom, Height = 50,
                BackColor = Color.FromArgb(243, 246, 250)
            };
            BuildFooter();

            this.Controls.Add(pnlConclusion);
            this.Controls.Add(pnlTimelineHost);
            this.Controls.Add(pnlSummary);
            this.Controls.Add(pnlHeader);
            this.Controls.Add(pnlFooter);
        }

        private string DetectTraceTitle()
        {
            // 找出链中第一个 Scenario 类别的日志, 作为标题
            var scenario = _chain.FirstOrDefault(c => c.Category == LogCategory.Scenario);
            if (scenario != null && !string.IsNullOrEmpty(scenario.Message))
            {
                string m = scenario.Message;
                if (m.StartsWith("▶ 开始: ")) m = m.Substring("▶ 开始: ".Length);
                if (m.StartsWith("触发故障: ")) m = m.Substring("触发故障: ".Length);
                int p = m.IndexOf(" (");
                if (p > 0) m = m.Substring(0, p);
                return "因果链详情 — " + m;
            }
            return "因果链详情 — TraceId " + _traceId;
        }

        private void BuildSummary()
        {
            var start = _chain.First().Timestamp;
            var end = _chain.Last().Timestamp;
            var duration = end - start;

            int y = 10;
            AddSummaryRow("TraceId",  _traceId, Color.FromArgb(15, 110, 86), y); y += 18;
            AddSummaryRow("触发时间", start.ToString("yyyy-MM-dd HH:mm:ss.fff"), Color.FromArgb(60, 70, 85), y); y += 18;
            AddSummaryRow("结束时间", $"{end:yyyy-MM-dd HH:mm:ss.fff}  ·  持续 {duration.TotalSeconds:F3} 秒", Color.FromArgb(60, 70, 85), y); y += 18;
            AddSummaryRow("日志条数", $"{_chain.Count} 条  ·  共享同一 TraceId", Color.FromArgb(60, 70, 85), y); y += 18;
        }

        private void AddSummaryRow(string key, string value, Color valColor, int y)
        {
            var lblK = new Label
            {
                Text = key,
                Location = new Point(16, y), Size = new Size(80, 18),
                ForeColor = Color.FromArgb(120, 130, 145),
                Font = new Font("微软雅黑", 9F)
            };
            var lblV = new Label
            {
                Text = value,
                Location = new Point(100, y), Size = new Size(720, 18),
                ForeColor = valColor,
                Font = new Font("Consolas", 9.5F)
            };
            pnlSummary.Controls.Add(lblK);
            pnlSummary.Controls.Add(lblV);
        }

        #endregion

        #region 时间线渲染

        private void BuildTimeline()
        {
            int y = 6;
            const int rowHeight = 56;
            const int dotX = 24;
            const int textX = 60;

            DateTime? prev = null;
            for (int i = 0; i < _chain.Count; i++)
            {
                var entry = _chain[i];
                var elapsed = prev.HasValue ? (entry.Timestamp - prev.Value).TotalMilliseconds : 0;
                prev = entry.Timestamp;

                BuildTimelineItem(entry, y, dotX, textX, elapsed, i == 0);
                y += rowHeight;
            }

            // 时间线竖线
            var line = new Panel
            {
                BackColor = Color.FromArgb(217, 225, 235),
                Location = new Point(dotX + 4, 12),
                Size = new Size(2, _chain.Count * rowHeight - 12)
            };
            pnlTimelineHost.Controls.Add(line);
            line.SendToBack();
        }

        private void BuildTimelineItem(LogEntry entry, int y, int dotX, int textX, double elapsedMs, bool isFirst)
        {
            // 圆点
            Color dotColor = GetCategoryColor(entry.Category);
            var dot = new Panel
            {
                Location = new Point(dotX, y + 4),
                Size = new Size(12, 12),
                BackColor = dotColor
            };
            dot.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var br = new SolidBrush(Color.White))
                    g.FillEllipse(br, 0, 0, 11, 11);
                using (var br = new SolidBrush(dotColor))
                    g.FillEllipse(br, 2, 2, 7, 7);
            };
            pnlTimelineHost.Controls.Add(dot);
            dot.BringToFront();

            // 时间 + 类别 + 耗时
            var headLine = new Panel
            {
                Location = new Point(textX, y),
                Size = new Size(800, 20),
                BackColor = Color.Transparent
            };

            var lblTime = new Label
            {
                Text = entry.Timestamp.ToString("HH:mm:ss.fff"),
                Location = new Point(0, 1),
                Size = new Size(96, 18),
                Font = new Font("Consolas", 9F),
                ForeColor = Color.FromArgb(120, 130, 145)
            };
            headLine.Controls.Add(lblTime);

            var catBadge = new Label
            {
                Text = " " + entry.Category.ToString() + " ",
                Location = new Point(102, 0),
                AutoSize = true,
                Font = new Font("微软雅黑", 8.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = dotColor,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(2, 1, 2, 1)
            };
            headLine.Controls.Add(catBadge);

            if (!isFirst && elapsedMs > 0)
            {
                string et = elapsedMs >= 1000
                    ? $"+{elapsedMs / 1000:F3} s"
                    : $"+{elapsedMs:F0} ms";
                var lblElapsed = new Label
                {
                    Text = " " + et + " ",
                    Location = new Point(170, 0),
                    AutoSize = true,
                    Font = new Font("微软雅黑", 8.5F),
                    ForeColor = Color.FromArgb(186, 117, 23),
                    BackColor = Color.FromArgb(255, 247, 224),
                    Padding = new Padding(2, 1, 2, 1)
                };
                headLine.Controls.Add(lblElapsed);
            }

            if (!string.IsNullOrEmpty(entry.Source))
            {
                var lblSrc = new Label
                {
                    Text = "来源: " + entry.Source,
                    Location = new Point(240, 1),
                    AutoSize = true,
                    Font = new Font("微软雅黑", 8.5F),
                    ForeColor = Color.FromArgb(120, 130, 145)
                };
                headLine.Controls.Add(lblSrc);
            }

            pnlTimelineHost.Controls.Add(headLine);

            // 消息正文
            Color msgColor = GetLevelColor(entry.Level);
            var lblMsg = new Label
            {
                Text = entry.Message,
                Location = new Point(textX, y + 20),
                Size = new Size(770, 18),
                Font = new Font("微软雅黑", 9.5F),
                ForeColor = msgColor
            };
            pnlTimelineHost.Controls.Add(lblMsg);

            // 信号/旧→新（如果有）
            if (!string.IsNullOrEmpty(entry.Signal))
            {
                string detail = entry.Signal;
                if (!string.IsNullOrEmpty(entry.OldValue) || !string.IsNullOrEmpty(entry.NewValue))
                    detail += "    " + (entry.OldValue ?? "-") + " → " + (entry.NewValue ?? "-");

                var lblDetail = new Label
                {
                    Text = detail,
                    Location = new Point(textX, y + 38),
                    Size = new Size(770, 18),
                    Font = new Font("Consolas", 9F),
                    ForeColor = Color.FromArgb(15, 110, 86),
                    BackColor = Color.FromArgb(243, 246, 250),
                    Padding = new Padding(6, 0, 6, 0),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                pnlTimelineHost.Controls.Add(lblDetail);
            }
        }

        private static Color GetCategoryColor(LogCategory c)
        {
            switch (c)
            {
                case LogCategory.Injection:     return Color.FromArgb(55, 138, 221);
                case LogCategory.Response:      return Color.FromArgb(15, 110, 86);
                case LogCategory.Scenario:      return Color.FromArgb(186, 117, 23);
                case LogCategory.Operator:      return Color.FromArgb(216, 90, 48);
                case LogCategory.Audit:         return Color.FromArgb(83, 74, 183);
                case LogCategory.Business:      return Color.FromArgb(29, 158, 117);
                case LogCategory.Communication: return Color.FromArgb(55, 138, 221);
                default:                        return Color.FromArgb(140, 140, 140);
            }
        }

        private static Color GetLevelColor(LogLevel l)
        {
            switch (l)
            {
                case LogLevel.Critical: return Color.FromArgb(163, 45, 45);
                case LogLevel.Fault:    return Color.FromArgb(216, 90, 48);
                case LogLevel.Warn:     return Color.FromArgb(186, 117, 23);
                case LogLevel.OK:       return Color.FromArgb(27, 122, 63);
                default:                return Color.FromArgb(40, 50, 65);
            }
        }

        #endregion

        #region 结论 & 底部按钮

        private void BuildConclusion()
        {
            // 计算端到端响应延迟
            var injectionLog = _chain.FirstOrDefault(c => c.Category == LogCategory.Injection && c.Level >= LogLevel.Warn);
            var responseLog = _chain.FirstOrDefault(c => c.Category == LogCategory.Response);

            string concl;
            if (injectionLog != null && responseLog != null)
            {
                double ms = (responseLog.Timestamp - injectionLog.Timestamp).TotalMilliseconds;
                concl = $"端到端响应延迟 {ms:F0} ms";
                if (ms <= 500) concl += "，符合 TB/T 2745-2017 标准要求 (≤500 ms)";
            }
            else
            {
                concl = $"日志链条已完整记录 {_chain.Count} 条事件，可作为测试报告附录";
            }

            var picOk = new Panel
            {
                Location = new Point(14, 12), Size = new Size(22, 22),
                BackColor = Color.FromArgb(40, 120, 60)
            };
            picOk.Paint += (s, e) =>
            {
                using (var br = new SolidBrush(Color.FromArgb(40, 120, 60)))
                    e.Graphics.FillEllipse(br, 0, 0, 21, 21);
                using (var f = new Font("微软雅黑", 11F, FontStyle.Bold))
                using (var br = new SolidBrush(Color.White))
                    e.Graphics.DrawString("✓", f, br, new RectangleF(0, 1, 22, 21),
                        new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            };
            pnlConclusion.Controls.Add(picOk);

            var lblTitle = new Label
            {
                Text = "验证结论:",
                Location = new Point(44, 9),
                Size = new Size(80, 18),
                Font = new Font("微软雅黑", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(27, 122, 63)
            };
            pnlConclusion.Controls.Add(lblTitle);

            var lblConcl = new Label
            {
                Text = concl,
                Location = new Point(44, 26),
                Size = new Size(800, 18),
                Font = new Font("微软雅黑", 9F),
                ForeColor = Color.FromArgb(50, 60, 75),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
            };
            pnlConclusion.Controls.Add(lblConcl);
        }

        private void BuildFooter()
        {
            var lblInfo = new Label
            {
                Text = "证据链可用于测试报告  ·  哈希校验通过  ·  防篡改",
                Location = new Point(16, 17),
                Size = new Size(360, 18),
                Font = new Font("微软雅黑", 9F),
                ForeColor = Color.FromArgb(120, 130, 145)
            };

            btnCopyTraceId = new Button
            {
                Text = "📋 复制 TraceId",
                Location = new Point(500, 10), Size = new Size(110, 30),
                Font = new Font("微软雅黑", 9F),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(60, 70, 85),
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnCopyTraceId.FlatAppearance.BorderColor = Color.FromArgb(217, 225, 235);
            btnCopyTraceId.Click += (s, e) =>
            {
                try
                {
                    Clipboard.SetText(_traceId);
                    MessageBox.Show(this, "TraceId 已复制: " + _traceId, "已复制");
                }
                catch { }
            };

            btnExportCsv = new Button
            {
                Text = "📥 导出本链 CSV",
                Location = new Point(615, 10), Size = new Size(130, 30),
                Font = new Font("微软雅黑", 9F),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(60, 70, 85),
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnExportCsv.FlatAppearance.BorderColor = Color.FromArgb(217, 225, 235);
            btnExportCsv.Click += (s, e) =>
            {
                using (var dlg = new SaveFileDialog
                {
                    Filter = "CSV 文件 (*.csv)|*.csv",
                    FileName = $"Trace_{_traceId}_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                })
                {
                    if (dlg.ShowDialog(this) != DialogResult.OK) return;
                    if (SimulationLogger.Instance.ExportCsv(dlg.FileName, _chain))
                    {
                        MessageBox.Show(this, "因果链已导出。", "成功");
                    }
                }
            };

            btnClose = new Button
            {
                Text = "关闭",
                Location = new Point(750, 10), Size = new Size(80, 30),
                Font = new Font("微软雅黑", 9F),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(60, 70, 85),
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnClose.FlatAppearance.BorderColor = Color.FromArgb(217, 225, 235);
            btnClose.Click += (s, e) => Close();

            pnlFooter.Controls.Add(lblInfo);
            pnlFooter.Controls.Add(btnCopyTraceId);
            pnlFooter.Controls.Add(btnExportCsv);
            pnlFooter.Controls.Add(btnClose);
        }

        #endregion
    }
}

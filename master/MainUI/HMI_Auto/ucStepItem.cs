using MainUI.Config.Test;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.HMI_Auto
{
    public partial class ucStepItem : UserControl
    {
        private string _title = "";
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                lblTitle.Text = value;
            }
        }

        // 当前高亮的行索引，-1表示没有高亮
        private int _highlightedRowIndex = -1;

        public ucStepItem()
        {
            InitializeComponent();
        }

        private void ucStepItem_Load(object sender, EventArgs e)
        {
            // 禁用所有选择模式
            this.dataGridLoopCode.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.dataGridLoopCode.ReadOnly = true;
            this.dataGridLoopCode.ClearSelection();

            // 禁用点击事件
            this.dataGridLoopCode.CellClick += (s, ev) =>
            {
                this.dataGridLoopCode.ClearSelection();
            };

            // 禁用鼠标和键盘的选择
            this.dataGridLoopCode.MouseDown += (s, ev) =>
            {
                if (ev.Button == MouseButtons.Left || ev.Button == MouseButtons.Right)
                {
                    // 阻止默认行为
                    // 可以留空或添加其他逻辑
                }
            };

            // 禁用键盘导航
            this.dataGridLoopCode.KeyDown += (s, ev) =>
            {
                // 阻止所有方向键、空格键等可能引起选择的操作
                if (ev.KeyCode == Keys.Up || ev.KeyCode == Keys.Down ||
                    ev.KeyCode == Keys.Left || ev.KeyCode == Keys.Right ||
                    ev.KeyCode == Keys.Space || ev.KeyCode == Keys.Enter)
                {
                    ev.Handled = true;
                }
            };
        }

        /// <summary>
        /// 加载表格
        /// </summary>
        public void InitItem(string SectionName, List<TestBasePara> testBasePara)
        {
            // 必须走 Title 属性，FindStepIndexByName 依赖 _title
            this.Title = $"{SectionName}  总步数:{testBasePara.Count}";

            this.dataGridLoopCode.Rows.Clear();
            foreach (var item in testBasePara)
            {
                this.dataGridLoopCode.Rows.Add(item.Index, item.GKNo, item.Torque, item.RPM, item.RunTime);
            }

            // 清除之前的高亮
            ClearHighlight();
        }

        /// <summary>
        /// 高亮指定行（黄色背景）
        /// </summary>
        /// <param name="rowIndex">行索引（从0开始）</param>
        public void HighlightRow(int rowIndex)
        {
            if (dataGridLoopCode.Rows.Count > 0 && rowIndex >= 0 && rowIndex < dataGridLoopCode.Rows.Count)
            {
                // 只清掉上一处黄色高亮，已完成的灰色行保留
                ClearHighlight();

                _highlightedRowIndex = rowIndex;
                this.dataGridLoopCode.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Yellow;

                this.dataGridLoopCode.FirstDisplayedScrollingRowIndex = rowIndex;
            }
        }

        /// <summary>
        /// 将指定行标为已完成（灰色）；若该行正是黄高亮则取消黄高亮索引
        /// </summary>
        public void MarkRowGray(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dataGridLoopCode.Rows.Count) return;

            this.dataGridLoopCode.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGray;
            if (_highlightedRowIndex == rowIndex)
                _highlightedRowIndex = -1;
            this.dataGridLoopCode.ClearSelection();
        }

        /// <summary>
        /// 整表恢复白色（切换循环代码离开本表时用）
        /// </summary>
        public void ResetAllRowsWhite()
        {
            for (int i = 0; i < dataGridLoopCode.Rows.Count; i++)
            {
                this.dataGridLoopCode.Rows[i].DefaultCellStyle.BackColor = Color.White;
                this.dataGridLoopCode.Rows[i].DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            }
            this.dataGridLoopCode.ClearSelection();
            _highlightedRowIndex = -1;
        }

        /// <summary>
        /// 清除黄色高亮；已是灰色的完成行不改回白色
        /// </summary>
        public void ClearHighlight()
        {
            if (_highlightedRowIndex >= 0 && _highlightedRowIndex < dataGridLoopCode.Rows.Count)
            {
                var row = this.dataGridLoopCode.Rows[_highlightedRowIndex];
                // 仅清除黄色；灰色表示已完成，保留
                if (row.DefaultCellStyle.BackColor.ToArgb() == Color.Yellow.ToArgb())
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                }
            }

            this.dataGridLoopCode.ClearSelection();
            _highlightedRowIndex = -1;
        }

        /// <summary>
        /// 获取当前高亮的行索引
        /// </summary>
        public int GetHighlightedRow()
        {
            return _highlightedRowIndex;
        }

        /// <summary>
        /// 检查是否有高亮行
        /// </summary>
        public bool HasHighlightedRow()
        {
            return _highlightedRowIndex >= 0;
        }

      
    }
}

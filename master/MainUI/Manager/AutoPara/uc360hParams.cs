using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RW.UI.Manager;
using MainUI.Model;
using MainUI.BLL;
using MainUI.Config;
using MainUI.Properties;
using MainUI.Config.Test;
using MainUI.Procedure.Test;
using MainUI.Global;
using Sunny.UI;
using MainUI.Helper;
using static MainUI.Config.PubConfig;

namespace MainUI.Procedure
{
    public partial class uc360hParams : ucBaseManagerUI
    {
        // 全局常量定义
        private const double MAX_TORQUE = 110.0; // 最大扭矩
        private const double MAX_RPM = 110.0; // 最大转速
        private const double MAX_RUN_TIME = 9999.0; //最长时间

        // 360小时流程配置参数
        Test360hConfig durabilityTestConfig = new Test360hConfig();

        // 存储系统配置的循环代码列表
        public List<string> testStep360List = new List<string>();

        public string Model { get; set; }

        public uc360hParams()
        {
            InitializeComponent();
        }

        private void ucTestParams_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;
        }

        public void LoadGridView(string model)
        {
            Model = model;
            LoadGridView();
        }

        /// <summary>
        /// 加载系统配置
        /// </summary>
        public void LoadSysConfig()
        {
            try
            {
                testStep360List = Var.SysConfig.TestStep360;
                // 确保 ComboBox 列可以编辑
                if (dgvMHDur.Columns["ColCode"] is DataGridViewComboBoxColumn codeColumn)
                {
                    codeColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing; // 重要：确保显示为下拉框
                    codeColumn.FlatStyle = FlatStyle.Flat;
                }
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, $"加载系统配置失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 加载表格参数
        /// </summary>
        private void LoadGridView()
        {
            try
            {
                if (string.IsNullOrEmpty(Model))
                {
                    dgvMHDur.Rows.Clear();
                    return;
                }

                // 加载360小时流程基础参数
                durabilityTestConfig = new Test360hConfig(Model);

                // 确保循环代码列有数据源
                if (dgvMHDur.Columns["ColCode"] is DataGridViewComboBoxColumn codeColumn &&
                    (codeColumn.DataSource == null || codeColumn.Items.Count == 0))
                {
                    codeColumn.DataSource = testStep360List;
                }

                this.dgvMHDur.Rows.Clear();
                foreach (var item in durabilityTestConfig.DurabilityDatas)
                {
                    int rowIndex = this.dgvMHDur.Rows.Add(item.Index, item.PhaseName, item.CycleName, item.NodeName, item.DayNum);
                }
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "加载数据失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 获取最大序号
        /// </summary>
        private int GetNextIndex()
        {
            if (durabilityTestConfig.DurabilityDatas.Count == 0)
                return 1;

            return durabilityTestConfig.DurabilityDatas.Max(d => d.Index) + 1;
        }

        /// <summary>
        /// 保存360小时流程数据
        /// </summary>
        private void SaveDurabilityData()
        {
            try
            {
                if (string.IsNullOrEmpty(Model))
                {
                    Var.MsgBoxWarn(this, "请先选择产品型号");
                    return;
                }

                durabilityTestConfig = new Test360hConfig(Model);
                durabilityTestConfig.DurabilityDatas.Clear();

                for (int i = 0; i < dgvMHDur.Rows.Count; i++)
                {
                    DataGridViewRow row = dgvMHDur.Rows[i];

                    // 跳过空行（新行）
                    if (row.IsNewRow) continue;

                    // 检查必填字段
                    string phaseName = row.Cells["ColJD"].Value?.ToString()?.Trim();
                    string cycleName = row.Cells["ColZQ"].Value?.ToString()?.Trim();
                    string nodeName = row.Cells["ColCode"].Value?.ToString()?.Trim();
                    string dayNum = row.Cells["ColDay"].Value?.ToString()?.Trim();

                    // 验证必填字段
                    if (string.IsNullOrEmpty(phaseName) || string.IsNullOrEmpty(cycleName))
                    {
                        // 如果阶段或周期名为空，提供默认值
                        if (string.IsNullOrEmpty(phaseName))
                            phaseName = $"阶段{i + 1}";
                        if (string.IsNullOrEmpty(cycleName))
                            cycleName = $"周期{i + 1}";
                    }

                    DurabilityData data = new DurabilityData
                    {
                        Index = i + 1, // 重新排序
                        PhaseName = phaseName,
                        CycleName = cycleName,
                        NodeName = nodeName,
                        DayNum = dayNum
                    };

                    durabilityTestConfig.DurabilityDatas.Add(data);
                }

                durabilityTestConfig.Save();
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "保存失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 表格单元格开始编辑
        /// </summary>
        private void dgvMHDur_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // 不允许编辑序号列
            if (e.ColumnIndex == dgvMHDur.Columns["ColIndex"].Index)
            {
                e.Cancel = true;
                return;
            }

            // 如果正在编辑循环代码列，确保有数据源
            if (e.ColumnIndex == dgvMHDur.Columns["ColCode"].Index)
            {
                if (dgvMHDur.Columns["ColCode"] is DataGridViewComboBoxColumn codeColumn &&
                    (codeColumn.DataSource == null || codeColumn.Items.Count == 0))
                {
                    codeColumn.DataSource = testStep360List;
                }
            }
        }

        /// <summary>
        /// 表格单元格内容点击事件
        /// </summary>
        private void dgvMHDur_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 如果点击的是删除按钮列（假设第5列是删除按钮）
            if (e.RowIndex >= 0 && e.ColumnIndex == 5) // 注意：需要根据实际情况调整列索引
            {
                // 防止事件冒泡
                dgvMHDur.EndEdit();

                // 调用删除方法
                DeleteDurabilityDataByRowIndex(e.RowIndex);
            }
        }

        /// <summary>
        /// 根据行索引删除数据
        /// </summary>
        private void DeleteDurabilityDataByRowIndex(int rowIndex)
        {
            try
            {
                DataGridViewRow row = dgvMHDur.Rows[rowIndex];

                // 跳过新行
                if (row.IsNewRow) return;

                var result = Var.MsgBoxYesNo(this, "确定要删除该行数据吗？");
                if (!result) return;

                // 删除行
                dgvMHDur.Rows.RemoveAt(rowIndex);

                // 重新排序并保存
                SaveDurabilityData();

                // 如果删除后还有行，自动选中上一行或第一行
                if (dgvMHDur.Rows.Count > 0)
                {
                    int selectRowIndex = rowIndex - 1;
                    if (selectRowIndex < 0) selectRowIndex = 0;

                    if (selectRowIndex < dgvMHDur.Rows.Count)
                    {
                        dgvMHDur.CurrentCell = dgvMHDur.Rows[selectRowIndex].Cells[1]; // 选中阶段名称列
                    }
                }
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "删除失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 表格单元格编辑结束事件
        /// </summary>
        private void dgvMHDur_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvMHDur.Rows.Count)
            {
                try
                {
                    DataGridViewRow row = dgvMHDur.Rows[e.RowIndex];

                    // 如果是新行，不保存
                    if (row.IsNewRow) return;

                    // 自动保存数据
                    SaveDurabilityData();
                }
                catch (Exception ex)
                {
                    Var.MsgBoxWarn(this, "保存失败：" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 表格单元格进入事件
        /// </summary>
        private void dgvMHDur_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // 当用户选中某行时，可以在这里填充编辑区域的控件（如果有的话）
            // 但现在我们直接在表格中编辑，所以这个事件可以留空
        }

        /// <summary>
        /// 表格单元格验证事件
        /// </summary>
        private void dgvMHDur_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvMHDur.Rows.Count)
            {
                string columnName = dgvMHDur.Columns[e.ColumnIndex].Name;

            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd360hStep_Click(object sender, EventArgs e)
        {
            if (StepNum.Value <= 0)
            {
                Var.MsgBoxWarn(this, "请设置有效的步骤数量");
                return;
            }

            try
            {
                // 获取当前最大步骤序号
                int currentMaxIndex = GetNextIndex();

                // 新增指定数量的步骤
                int stepsToAdd = (int)StepNum.Value;
                for (int i = 0; i < stepsToAdd; i++)
                {
                    DurabilityData newStep = new DurabilityData
                    {
                        Index = currentMaxIndex + i,
                        PhaseName = "A",
                        CycleName = "A",
                        NodeName = "A",
                        DayNum = "1"
                    };

                    durabilityTestConfig.DurabilityDatas.Add(newStep);
                    durabilityTestConfig.Save();
                }
                // 刷新显示
                LoadGridView();
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "添加详细步骤失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 表格编辑控件显示事件
        /// </summary>
        private void dgvMHDur_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // 如果当前编辑的是循环代码列，设置下拉框数据源和样式
            if (dgvMHDur.CurrentCell.ColumnIndex == dgvMHDur.Columns["ColCode"].Index)
            {
                ComboBox comboBox = e.Control as ComboBox;
                if (comboBox != null)
                {
                    // 确保数据源已设置
                    if (comboBox.Items.Count == 0)
                    {
                        comboBox.DataSource = testStep360List;
                    }

                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList; // 只能选择，不能输入
                }
            }

        }

        /// <summary>
        /// 表格数据错误事件
        /// </summary>
        private void dgvMHDur_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // 处理数据验证错误
            if (e.Exception != null)
            {
                Var.MsgBoxWarn(this, "数据输入错误：" + e.Exception.Message);
                e.ThrowException = false;
            }
        }

        /// <summary>
        /// 验证输入是否为有效数字
        /// </summary>
        private bool IsValidNumber(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            // 检查是否包含中文字符
            foreach (char c in input)
            {
                if (c >= 0x4E00 && c <= 0x9FA5) // Unicode中文字符范围
                {
                    return false;
                }
            }

            // 尝试转换为double
            double result;
            return double.TryParse(input, out result);
        }

        /// <summary>
        /// 文本框按键事件处理
        /// </summary>
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 允许退格键
            if (e.KeyChar == (char)8)
                return;

            // 允许小数点（只能有一个）
            if (e.KeyChar == '.')
            {
                TextBox textBox = sender as TextBox;
                if (textBox != null && textBox.Text.Contains("."))
                {
                    e.Handled = true; // 阻止输入多个小数点
                }
                return;
            }

            // 允许数字
            if (char.IsDigit(e.KeyChar))
                return;

            // 阻止其他所有字符
            e.Handled = true;
        }

        // 存储复制的数据
        private List<DurabilityData> copiedDataList = new List<DurabilityData>();

        /// <summary>
        /// 复制选中的360h步骤数据
        /// </summary>
        private void tsmCopy_Click(object sender, EventArgs e)
        {
            if (dgvMHDur.SelectedRows.Count == 0)
            {
                Var.MsgBoxWarn(this, "请选择要复制的数据行！");
                return;
            }

            try
            {
                // 清空之前的复制数据
                copiedDataList.Clear();

                // 按行索引排序（升序），保持原始顺序
                var selectedRows = dgvMHDur.SelectedRows
                    .Cast<DataGridViewRow>()
                    .Where(r => !r.IsNewRow)
                    .OrderBy(r => r.Index)  // 按行索引排序，保持原始显示顺序
                    .ToList();

                // 复制选中的行数据
                foreach (DataGridViewRow row in selectedRows)
                {
                    // 确保基本数据有值
                    if (row.Cells["ColIndex"].Value == null ||
                        row.Cells["ColJD"].Value == null ||
                        row.Cells["ColZQ"].Value == null ||
                        row.Cells["ColCode"].Value == null)
                    {
                        continue;
                    }

                    DurabilityData copiedData = new DurabilityData
                    {
                        // 序号会在粘贴时重新生成
                        Index = Convert.ToInt32(row.Cells["ColIndex"].Value),
                        PhaseName = row.Cells["ColJD"].Value?.ToString() ?? "A",
                        CycleName = row.Cells["ColZQ"].Value?.ToString() ?? "A",
                        NodeName = row.Cells["ColCode"].Value?.ToString() ?? "A",
                        DayNum = row.Cells["ColDay"].Value?.ToString() ?? "1"
                    };

                    copiedDataList.Add(copiedData);
                }

                if (copiedDataList.Count > 0)
                {
                    Var.MsgBoxInfo(this, $"已复制 {copiedDataList.Count} 行数据");
                }
                else
                {
                    Var.MsgBoxWarn(this, "没有有效数据可复制");
                }
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "复制失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 粘贴复制的360h步骤数据
        /// </summary>
        private void tsmPaste_Click(object sender, EventArgs e)
        {
            if (copiedDataList == null || copiedDataList.Count == 0)
            {
                Var.MsgBoxWarn(this, "没有可粘贴的数据，请先复制数据");
                return;
            }

            if (string.IsNullOrEmpty(Model))
            {
                Var.MsgBoxWarn(this, "请先选择产品型号");
                return;
            }

            try
            {
                // 重新加载配置，确保使用最新数据
                durabilityTestConfig = new Test360hConfig(Model);

                // 获取当前最大步骤序号
                int currentMaxIndex = durabilityTestConfig.DurabilityDatas.Count > 0 ?
                    durabilityTestConfig.DurabilityDatas.Max(d => d.Index) : 0;

                // 添加复制的数据（保持copiedDataList中的原始顺序）
                foreach (var copiedData in copiedDataList)
                {
                    // 创建新的数据对象，避免引用问题
                    DurabilityData newData = new DurabilityData
                    {
                        Index = ++currentMaxIndex, // 重新生成序号
                        PhaseName = copiedData.PhaseName,
                        CycleName = copiedData.CycleName,
                        NodeName = copiedData.NodeName,
                        DayNum = copiedData.DayNum
                    };

                    durabilityTestConfig.DurabilityDatas.Add(newData);
                }

                // 保存配置
                durabilityTestConfig.Save();

                // 刷新显示
                LoadGridView();

                Var.MsgBoxInfo(this, $"已粘贴 {copiedDataList.Count} 行数据");
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "粘贴失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 删除选中的360h步骤数据
        /// </summary>
        private void tsmDelete_Click(object sender, EventArgs e)
        {
            if (dgvMHDur.SelectedRows.Count == 0)
            {
                Var.MsgBoxWarn(this, "请选择要删除的数据行！");
                return;
            }

            if (string.IsNullOrEmpty(Model))
            {
                Var.MsgBoxWarn(this, "请先选择产品型号");
                return;
            }

            try
            {
                // 确认删除
                var result = Var.MsgBoxYesNo(this, $"确定要删除选中的 {dgvMHDur.SelectedRows.Count} 行数据吗？");
                if (!result) return;

                // 获取选中的行索引（降序排序，避免删除时索引变化）
                List<int> rowIndexes = new List<int>();
                foreach (DataGridViewRow row in dgvMHDur.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        rowIndexes.Add(row.Index);
                    }
                }

                // 按降序排序，从后往前删除
                rowIndexes.Sort((a, b) => b.CompareTo(a));

                // 重新加载配置
                durabilityTestConfig = new Test360hConfig(Model);

                // 删除选中的数据
                foreach (int rowIndex in rowIndexes)
                {
                    if (rowIndex >= 0 && rowIndex < durabilityTestConfig.DurabilityDatas.Count)
                    {
                        // 注意：这里根据行索引删除，而不是步骤号
                        if (rowIndex < durabilityTestConfig.DurabilityDatas.Count)
                        {
                            durabilityTestConfig.DurabilityDatas.RemoveAt(rowIndex);
                        }
                    }
                }

                // 重新排序（按列表顺序重新生成序号）
                for (int i = 0; i < durabilityTestConfig.DurabilityDatas.Count; i++)
                {
                    durabilityTestConfig.DurabilityDatas[i].Index = i + 1;
                }

                // 保存配置
                durabilityTestConfig.Save();

                // 刷新显示
                LoadGridView();

                Var.MsgBoxInfo(this, $"已删除 {rowIndexes.Count} 行数据");
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "删除失败：" + ex.Message);
            }
        }

    }
}
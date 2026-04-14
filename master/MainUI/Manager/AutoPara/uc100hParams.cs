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
using System.IO;

namespace MainUI.Procedure
{
    public partial class uc100hParams : ucBaseManagerUI
    {
        // 全局常量定义
        private const double MAX_TORQUE = 110.0; // 最大扭矩
        private const double MAX_RPM = 110.0; // 最大转速
        private const double MAX_RUN_TIME = 9999.0; //最长时间

        public string Model { get; set; }

        /// <summary>
        /// 100h 主流程配置
        /// </summary>
        Test100hConfig testConfig = new Test100hConfig();

        // 存储系统配置的循环代码列表
        public List<string> testStep100List = new List<string>();

        public uc100hParams()
        {
            InitializeComponent();

            testStep100List = new List<string>() { "标定功率试验", "超负荷试验", "部分负荷试验", "交替突变负荷试验" };
            // 确保 ComboBox 列可以编辑
            if (dgvMH.Columns["TestName"] is DataGridViewComboBoxColumn codeColumn)
            {
                codeColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing; // 重要：确保显示为下拉框
                codeColumn.FlatStyle = FlatStyle.Flat;
            }
        }

        private void ucTestParams_Load(object sender, EventArgs e)
        {

        }

        #region 100小时主流程

        public void LoadGridView(string model)
        {
            Model = model;
            LoadGridView();
        }

        /// <summary>
        /// 加载表格参数
        /// </summary>
        private void LoadGridView()
        {
            // 确保 ComboBox 列数据源已设置
            if (dgvMH.Columns["TestName"] is DataGridViewComboBoxColumn codeColumn)
            {
                codeColumn.DataSource = testStep100List;
                codeColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                codeColumn.FlatStyle = FlatStyle.Flat;
            }

            // 加载性能试验表格
            testConfig = new Test100hConfig(Model.ToString(), "性能试验");
            this.dgvMH.Rows.Clear();
            var para = testConfig.testStepLists;

            for (int i = 0; i < para.Count; i++)
            {
                // 确保值在数据源中
                string testName = para[i].TestName;
                if (!testStep100List.Contains(testName))
                {
                    testName = testStep100List.FirstOrDefault(); // 使用默认值
                }

                this.dgvMH.Rows.Add(para[i].Index, testName, para[i].CollectIntervalTime,
                                   para[i].ForeachNum, para[i].DayNum);
            }
        }

        /// <summary>
        /// 新增步骤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd100h_Click(object sender, EventArgs e)
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
                int stepsToAdd = (int)nud100hNum.Value;
                for (int i = 0; i < stepsToAdd; i++)
                {
                    TestStepList para1 = new TestStepList();
                    para1.Index = testConfig.testStepLists.Count() + 1;
                    para1.TestName = "标定功率试验";
                    para1.DayNum = "1";
                    testConfig.testStepLists.Add(para1);
                    testConfig.Save();
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
        /// 获取最大序号
        /// </summary>
        private int GetNextIndex()
        {
            if (testConfig.testStepLists.Count == 0)
                return 1;

            return testConfig.testStepLists.Max(d => d.Index) + 1;
        }

        /// <summary>
        /// 编辑表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMH_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // 如果当前编辑的是循环代码列，设置下拉框数据源和样式
            if (dgvMH.CurrentCell.ColumnIndex == dgvMH.Columns["TestName"].Index)
            {
                ComboBox comboBox = e.Control as ComboBox;
                if (comboBox != null)
                {
                    // 确保数据源已设置
                    if (comboBox.Items.Count == 0)
                    {
                        comboBox.DataSource = testStep100List;
                    }

                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList; // 只能选择，不能输入
                }
            }
        }

        /// <summary>
        /// 100h开始编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMH_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // 不允许编辑序号列
            if (e.ColumnIndex == dgvMH.Columns["Index"].Index)
            {
                e.Cancel = true;
                return;
            }

            // 如果正在编辑循环代码列，确保有数据源
            if (e.ColumnIndex == dgvMH.Columns["TestName"].Index)
            {
                if (dgvMH.Columns["TestName"] is DataGridViewComboBoxColumn codeColumn &&
                    (codeColumn.DataSource == null || codeColumn.Items.Count == 0))
                {
                    codeColumn.DataSource = testStep100List;
                }
            }
        }

        /// <summary>
        /// 点击单元格内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 如果点击的是删除按钮列（假设第5列是删除按钮）
            if (e.RowIndex >= 0 && e.ColumnIndex == 5) // 注意：需要根据实际情况调整列索引
            {
                // 防止事件冒泡
                dgvMH.EndEdit();

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
                DataGridViewRow row = dgvMH.Rows[rowIndex];

                // 跳过新行
                if (row.IsNewRow) return;

                var result = Var.MsgBoxYesNo(this, "确定要删除该行数据吗？");
                if (!result) return;

                // 删除行
                dgvMH.Rows.RemoveAt(rowIndex);

                // 重新排序并保存
                SaveDurabilityData();

                // 如果删除后还有行，自动选中上一行或第一行
                if (dgvMH.Rows.Count > 0)
                {
                    int selectRowIndex = rowIndex - 1;
                    if (selectRowIndex < 0) selectRowIndex = 0;

                    if (selectRowIndex < dgvMH.Rows.Count)
                    {
                        dgvMH.CurrentCell = dgvMH.Rows[selectRowIndex].Cells[1]; // 选中阶段名称列
                    }
                }
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "删除失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 100h 主流程选中步骤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMH_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// 保存100小时主流程数据
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

                // 创建新的配置对象，而不是清空原有数据
                var newTestConfig = new Test100hConfig(Model, "性能试验");

                // 从表格构建新的步骤列表
                var newStepList = new List<TestStepList>();
                int currentIndex = 1;

                for (int i = 0; i < dgvMH.Rows.Count; i++)
                {
                    DataGridViewRow row = dgvMH.Rows[i];

                    // 跳过空行（新行）
                    if (row.IsNewRow) continue;

                    // 检查必填字段
                    string testName = row.Cells["TestName"].Value?.ToString()?.Trim();
                    string day = row.Cells["Col100Day"].Value?.ToString()?.Trim();

                    // 验证必填字段
                    if (string.IsNullOrEmpty(day))
                    {
                        Var.MsgBoxWarn(this, $"第{i + 1}行：请输入天数");
                        day = "0";
                    }

                    // 创建新的步骤对象
                    TestStepList para1 = new TestStepList();
                    para1.Index = currentIndex++;
                    para1.TestName = testName;
                    para1.DayNum = day;

                    // 保留原有的详细步骤数据
                    int originalIndex = Convert.ToInt32(row.Cells["Index"].Value);
                    var originalStep = testConfig.testStepLists.FirstOrDefault(d => d.Index == originalIndex);
                    if (originalStep != null)
                    {
                        para1.testBasePara = originalStep.testBasePara; // 保留详细步骤
                    }
                    else
                    {
                        para1.testBasePara = new List<TestBasePara>(); // 新步骤创建空列表
                    }

                    newStepList.Add(para1);
                }

                // 更新配置对象
                newTestConfig.testStepLists = newStepList;
                newTestConfig.Save();

                // 更新当前配置引用
                testConfig = newTestConfig;
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "保存失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 单元格编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMH_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvMH.Rows.Count)
            {
                try
                {
                    DataGridViewRow row = dgvMH.Rows[e.RowIndex];

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


        #endregion

        #region 100小时流程详细步骤编辑
        /// <summary>
        /// 点击 删除按钮
        /// </summary>
        private void dgv100hMH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 确保点击的是删除按钮（第8列是删除按钮）
            if (e.RowIndex < 0 || e.ColumnIndex != 8) return;

            // 防止事件冒泡
            dgv100hMH.EndEdit();

            // 调用删除方法
            DeleteDetailStepByRowIndex(e.RowIndex);
        }

        /// <summary>
        /// 加载100小时流程详细步骤
        /// </summary>
        private void Load100hStepDetails()
        {
            if (dgvMH.SelectedRows.Count == 0) return;

            try
            {
                DataGridViewRow selectedRow = dgvMH.SelectedRows[0];
                int index = Convert.ToInt32(selectedRow.Cells["Index"].Value);

                // 更新标签显示当前选中的试验阶段
                this.lbl100hStepName.Text = selectedRow.Cells["TestName"].Value.ToString();
                this.lbl100hSore.Text = index.ToString();

                // 加载详细步骤数据
                //testConfig = new Test100hConfig(Model, "性能试验");
                var stepData = testConfig.testStepLists.FirstOrDefault(d => d.Index == index);

                if (stepData != null)
                {
                    dgv100hMH.Rows.Clear();
                    foreach (var item in stepData.testBasePara)
                    {
                        dgv100hMH.Rows.Add(item.Index, item.TestName, item.CycleName,
                                            item.StepName, item.GKNo, item.Torque, item.RPM, item.RunTime);
                    }
                }
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "加载详细步骤失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 100小时流程表格选择变化事件
        /// </summary>
        private void dgvMH_SelectionChanged(object sender, EventArgs e)
        {
            Load100hStepDetails();
        }

        /// <summary>
        /// 开始编辑100小时流程表格单元格
        /// </summary>
        private void dgv100hStep_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // 只允许编辑 工况编号、转速、扭矩、运行时间列
            if (e.ColumnIndex < 4 || e.ColumnIndex > 7)
            {
                e.Cancel = true;
                return;
            }

            // 允许编辑，清空输入法状态（防止中文输入）
            this.ImeMode = ImeMode.Disable;
        }

        /// <summary>
        /// 100小时详细步骤表格单元格值编辑结束后触发
        /// </summary>
        private void dgv100hMH_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgv100hMH.Rows.Count && dgvMH.SelectedRows.Count > 0)
            {
                try
                {
                    // 恢复输入法状态
                    this.ImeMode = ImeMode.NoControl;

                    DataGridViewRow mainRow = dgvMH.SelectedRows[0];
                    int mainIndex = Convert.ToInt32(mainRow.Cells["Index"].Value);

                    DataGridViewRow detailRow = dgv100hMH.Rows[e.RowIndex];
                    int detailIndex = Convert.ToInt32(detailRow.Cells["StepIndex100"].Value);

                    // 获取单元格
                    DataGridViewCell cell = detailRow.Cells[e.ColumnIndex];
                    string columnName = dgv100hMH.Columns[e.ColumnIndex].Name;

                    // 验证输入
                    string inputValue = cell.Value?.ToString() ?? "";

                    if (columnName == "RPM" || columnName == "Torque" || columnName == "RunTime" || columnName == "GK")
                    {
                        // 验证是否为数字（防止中文输入）
                        if (!IsValidNumber(inputValue))
                        {
                            Var.MsgBoxWarn(this, $"请输入有效的数字！当前输入：{inputValue}");
                            cell.Value = 0;
                        }

                        double value = Convert.ToDouble(inputValue);

                        // 验证范围
                        if (columnName == "RPM" && (value < 0 || value > MAX_RPM))
                        {
                            Var.MsgBoxWarn(this, $"转速范围：0-{MAX_RPM}！当前输入：{value}");
                            cell.Value = 0;
                        }
                        else if (columnName == "Torque" && (value < 0 || value > MAX_TORQUE))
                        {
                            Var.MsgBoxWarn(this, $"扭矩范围：0-{MAX_TORQUE}！当前输入：{value}");
                            cell.Value = 0;
                        }
                        else if (columnName == "RunTime" && (value < 0 || value > MAX_RUN_TIME))
                        {
                            Var.MsgBoxWarn(this, $"运行时间范围：0-{MAX_RUN_TIME}！当前输入：{value}");
                            cell.Value = 0;
                        }
                        else if (columnName == "GK")
                        {
                            // todo 需要检测是否在工况编号表，如果不在则说明输入错误？

                            //return;
                        }
                    }

                    // 获取修改后的值
                    double rpm = Convert.ToDouble(detailRow.Cells["RPM"].Value);
                    double torque = Convert.ToDouble(detailRow.Cells["Torque"].Value);
                    double runTime = Convert.ToDouble(detailRow.Cells["RunTime"].Value);
                    string gkNo = detailRow.Cells["GK"].Value.ToString();

                    // 更新配置数据
                    var stepData = testConfig.testStepLists.FirstOrDefault(d => d.Index == mainIndex);

                    if (stepData != null)
                    {
                        var detailData = stepData.testBasePara.FirstOrDefault(d => d.Index == detailIndex);
                        if (detailData != null)
                        {
                            // 更新修改的值
                            detailData.RPM = rpm;
                            detailData.Torque = torque;
                            detailData.RunTime = runTime;
                            detailData.GKNo = gkNo;
                            testConfig.Save();
                        }
                    }
                }
                catch (FormatException)
                {
                    Var.MsgBoxWarn(this, "输入格式错误，请输入有效的数字！");
                    Load100hStepDetails(); // 重新加载恢复原值
                }
                catch (Exception ex)
                {
                    Var.MsgBoxWarn(this, "保存失败：" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 键盘输入验证（防止输入非数字字符）
        /// </summary>
        private void dgv100hMH_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // 获取当前编辑的单元格
            if (dgv100hMH.CurrentCell == null) return;

            int columnIndex = dgv100hMH.CurrentCell.ColumnIndex;
            string columnName = dgv100hMH.Columns[columnIndex].Name;

            // 只对RPM、Torque、RunTime列进行输入限制
            if (columnName == "RPM" || columnName == "Torque" || columnName == "RunTime")
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox != null)
                {
                    // 移除之前的事件处理器（防止重复添加）
                    textBox.KeyPress -= new KeyPressEventHandler(TextBox_KeyPress);
                    // 添加新的事件处理器
                    textBox.KeyPress += new KeyPressEventHandler(TextBox_KeyPress);

                    // 设置输入法为关闭状态
                    textBox.ImeMode = ImeMode.Disable;
                }
            }
        }

        /// <summary>
        /// 单元格验证事件
        /// </summary>
        private void dgv100hMH_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgv100hMH.Rows.Count)
            {
                string columnName = dgv100hMH.Columns[e.ColumnIndex].Name;

                // 只验证RPM、Torque、RunTime列
                if (columnName == "RPM" || columnName == "Torque" || columnName == "RunTime")
                {
                    string value = e.FormattedValue?.ToString() ?? "";

                    if (string.IsNullOrWhiteSpace(value))
                    {
                        dgv100hMH.Rows[e.RowIndex].ErrorText = "不能为空！";
                        e.Cancel = true;
                        return;
                    }

                    // 验证是否为数字
                    if (!IsValidNumber(value))
                    {
                        dgv100hMH.Rows[e.RowIndex].ErrorText = "请输入有效的数字！";
                        e.Cancel = true;
                        return;
                    }

                    double doubleValue;
                    if (!double.TryParse(value, out doubleValue))
                    {
                        dgv100hMH.Rows[e.RowIndex].ErrorText = "请输入有效的数字！";
                        e.Cancel = true;
                        return;
                    }

                    // 验证范围
                    if (columnName == "RPM")
                    {
                        if (doubleValue < 0 || doubleValue > MAX_RPM)
                        {
                            dgv100hMH.Rows[e.RowIndex].ErrorText = $"转速范围：0-{MAX_RPM} %";
                            Var.MsgBoxWarn(this, dgv100hMH.Rows[e.RowIndex].ErrorText);
                            e.Cancel = true;
                        }
                    }
                    else if (columnName == "Torque")
                    {
                        if (doubleValue < 0 || doubleValue > MAX_TORQUE)
                        {
                            dgv100hMH.Rows[e.RowIndex].ErrorText = $"扭矩范围：0-{MAX_TORQUE} %";
                            Var.MsgBoxWarn(this, dgv100hMH.Rows[e.RowIndex].ErrorText);
                            e.Cancel = true;
                        }
                    }
                    else if (columnName == "RunTime")
                    {
                        if (doubleValue < 0 || doubleValue > MAX_RUN_TIME)
                        {
                            dgv100hMH.Rows[e.RowIndex].ErrorText = $"运行时间范围：0-{MAX_RUN_TIME} min";
                            Var.MsgBoxWarn(this, dgv100hMH.Rows[e.RowIndex].ErrorText);
                            e.Cancel = true;
                        }
                    }

                    // 清除错误提示
                    dgv100hMH.Rows[e.RowIndex].ErrorText = "";
                }
            }
        }

        /// <summary>
        /// 新增100h步骤数
        /// </summary>
        private void btnAdd100hStep_Click(object sender, EventArgs e)
        {
            if (dgvMH.SelectedRows.Count == 0)
            {
                Var.MsgBoxWarn(this, "请先选择一个试验阶段");
                return;
            }

            if (StepNum.Value <= 0)
            {
                Var.MsgBoxWarn(this, "请设置有效的步骤数量");
                return;
            }

            try
            {
                // 获取选中的主试验阶段
                DataGridViewRow mainRow = dgvMH.SelectedRows[0];
                int mainIndex = Convert.ToInt32(mainRow.Cells["Index"].Value);
                string mainTestName = mainRow.Cells["TestName"].Value?.ToString() ?? "";

                // 加载配置
                var stepData = testConfig.testStepLists.FirstOrDefault(d => d.Index == mainIndex);

                if (stepData == null)
                {
                    Var.MsgBoxWarn(this, "未找到对应的试验阶段数据");
                    return;
                }

                // 获取当前最大步骤序号
                int currentMaxIndex = stepData.testBasePara.Count > 0 ?
                    stepData.testBasePara.Max(p => p.Index) : 0;

                // 新增指定数量的步骤
                int stepsToAdd = (int)StepNum.Value;
                for (int i = 1; i <= stepsToAdd; i++)
                {
                    TestBasePara newStep = new TestBasePara
                    {
                        Index = currentMaxIndex + i,
                        TestName = mainTestName,
                        CycleName = "",
                        StepName = $"步骤{currentMaxIndex + i}",
                        RPM = 0,  // 默认转速
                        GKNo = "0", //
                        Torque = 0, // 默认扭矩
                        RunTime = 1 // 默认运行时间1分钟
                    };

                    stepData.testBasePara.Add(newStep);
                }

                // 保存配置
                testConfig.Save();

                // 刷新显示
                Load100hStepDetails();
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "添加详细步骤失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 根据行索引删除详细步骤
        /// </summary>
        private void DeleteDetailStepByRowIndex(int rowIndex)
        {
            try
            {
                DataGridViewRow detailRow = dgv100hMH.Rows[rowIndex];

                if (detailRow.Cells["StepIndex100"].Value == null)
                {
                    Var.MsgBoxWarn(this, "该行为空行，无法删除");
                    return;
                }

                int detailIndex = Convert.ToInt32(detailRow.Cells["StepIndex100"].Value);

                var result = Var.MsgBoxYesNo(this, $"确定要删除步骤{detailIndex}吗？");
                if (!result) return;

                DataGridViewRow mainRow = dgvMH.SelectedRows[0];
                int mainIndex = Convert.ToInt32(mainRow.Cells["Index"].Value);

                // 先找100h主流程
                var stepData = testConfig.testStepLists.FirstOrDefault(d => d.Index == mainIndex);

                if (stepData == null)
                {
                    Var.MsgBoxWarn(this, "未找到对应的试验阶段数据");
                    return;
                }

                // 再找具体工况步骤
                var detailData = stepData.testBasePara.FirstOrDefault(d => d.Index == detailIndex);
                if (detailData == null)
                {
                    Var.MsgBoxWarn(this, $"未找到步骤{detailIndex}的数据");
                    return;
                }

                stepData.testBasePara.Remove(detailData);

                // 重新排序
                for (int i = 0; i < stepData.testBasePara.Count; i++)
                {
                    stepData.testBasePara[i].Index = i + 1;
                }
                testConfig.Save();

                // 重新加载表格
                Load100hStepDetails();

                // 如果删除后还有行，自动选中上一行或第一行
                if (dgv100hMH.Rows.Count > 0)
                {
                    int selectRowIndex = rowIndex - 1;
                    if (selectRowIndex < 0) selectRowIndex = 0;

                    if (selectRowIndex < dgv100hMH.Rows.Count)
                    {
                        dgv100hMH.CurrentCell = dgv100hMH.Rows[selectRowIndex].Cells[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "删除失败：" + ex.Message);
            }
        }

        #endregion

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

        /// <summary>
        /// 复制时的存储类
        /// </summary>
        public List<TestBasePara> StepsList { get; set; } = new List<TestBasePara>();

        /// <summary>
        /// 复制选中的100h步骤数据
        /// </summary>
        private void tsmCopy_Click(object sender, EventArgs e)
        {
            if (dgv100hMH.SelectedRows.Count == 0)
            {
                Var.MsgBoxWarn(this, "请选择要复制的数据行！");
                return;
            }

            try
            {
                StepsList = new List<TestBasePara>();
                // 清空之前的复制数据
                StepsList.Clear();

                // 按行索引排序（升序），保持原始顺序
                var selectedRows = dgv100hMH.SelectedRows
                    .Cast<DataGridViewRow>()
                    .Where(r => !r.IsNewRow)
                    .OrderBy(r => r.Index)  // 按行索引排序，保持原始显示顺序
                    .ToList();

                // 复制选中的行数据
                foreach (DataGridViewRow row in selectedRows)
                {
                    // 确保单元格有值
                    if (row.Cells["Torque"].Value == null ||
                        row.Cells["RPM"].Value == null ||
                        row.Cells["RunTime"].Value == null ||
                        row.Cells["GK"].Value == null)
                    {
                        continue;
                    }

                    TestBasePara stepData = new TestBasePara
                    {
                        GKNo = row.Cells["GK"].Value.ToString(),
                        Torque = Convert.ToDouble(row.Cells["Torque"].Value),
                        RPM = Convert.ToDouble(row.Cells["RPM"].Value),
                        RunTime = Convert.ToDouble(row.Cells["RunTime"].Value)
                    };

                    StepsList.Add(stepData);
                }

                Var.MsgBoxInfo(this, $"已复制 {StepsList.Count} 行数据");
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "复制失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 粘贴复制的100h步骤数据
        /// </summary>
        private void tsmPaste_Click(object sender, EventArgs e)
        {
            if (dgvMH.SelectedRows.Count == 0)
            {
                Var.MsgBoxWarn(this, "请先选择一个试验阶段");
                return;
            }

            if (StepsList == null || StepsList.Count == 0)
            {
                Var.MsgBoxWarn(this, "没有可粘贴的数据，请先复制数据");
                return;
            }

            try
            {
                // 获取选中的主试验阶段
                DataGridViewRow mainRow = dgvMH.SelectedRows[0];
                int mainIndex = Convert.ToInt32(mainRow.Cells["Index"].Value);
                string mainTestName = mainRow.Cells["TestName"].Value?.ToString() ?? "";

                // 加载配置
                testConfig = new Test100hConfig(Model, "性能试验");
                var stepData = testConfig.testStepLists.FirstOrDefault(d => d.Index == mainIndex);

                if (stepData == null)
                {
                    Var.MsgBoxWarn(this, "未找到对应的试验阶段数据");
                    return;
                }

                // 获取当前最大步骤序号
                int currentMaxIndex = stepData.testBasePara.Count > 0 ?
                    stepData.testBasePara.Max(p => p.Index) : 0;

                // 添加复制的数据（保持StepsList中的原始顺序）
                foreach (var step in StepsList)
                {
                    TestBasePara newStep = new TestBasePara
                    {
                        Index = ++currentMaxIndex,
                        TestName = mainTestName,
                        CycleName = "",
                        StepName = $"步骤{currentMaxIndex}",
                        Torque = step.Torque,
                        RPM = step.RPM,
                        GKNo = step.GKNo,
                        RunTime = step.RunTime
                    };

                    stepData.testBasePara.Add(newStep);
                }

                // 保存配置
                testConfig.Save();

                // 刷新显示
                Load100hStepDetails();

                Var.MsgBoxInfo(this, $"已粘贴 {StepsList.Count} 行数据");
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "粘贴失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 删除选中的100h步骤
        /// </summary>
        private void tsmDelete_Click(object sender, EventArgs e)
        {
            if (dgv100hMH.SelectedRows.Count == 0)
            {
                Var.MsgBoxWarn(this, "请选择要删除的数据行！");
                return;
            }

            try
            {
                // 确认删除
                var result = Var.MsgBoxYesNo(this, $"确定要删除选中的 {dgv100hMH.SelectedRows.Count} 行数据吗？");
                if (!result) return;

                // 获取选中的主试验阶段
                if (dgvMH.SelectedRows.Count == 0)
                {
                    Var.MsgBoxWarn(this, "未找到对应的试验阶段");
                    return;
                }

                DataGridViewRow mainRow = dgvMH.SelectedRows[0];
                int mainIndex = Convert.ToInt32(mainRow.Cells["Index"].Value);

                // 加载配置
                var stepData = testConfig.testStepLists.FirstOrDefault(d => d.Index == mainIndex);

                if (stepData == null)
                {
                    Var.MsgBoxWarn(this, "未找到对应的试验阶段数据");
                    return;
                }

                // 收集要删除的步骤索引
                List<int> stepIndexesToDelete = new List<int>();
                foreach (DataGridViewRow row in dgv100hMH.SelectedRows)
                {
                    if (row.Cells["StepIndex100"].Value != null)
                    {
                        stepIndexesToDelete.Add(Convert.ToInt32(row.Cells["StepIndex100"].Value));
                    }
                }

                // 删除步骤（从后往前删除，避免索引变动问题）
                stepIndexesToDelete.Sort((a, b) => b.CompareTo(a)); // 降序排序
                int deletedCount = 0;
                foreach (int stepIndex in stepIndexesToDelete)
                {
                    var detailData = stepData.testBasePara.FirstOrDefault(d => d.Index == stepIndex);
                    if (detailData != null)
                    {
                        stepData.testBasePara.Remove(detailData);
                        deletedCount++;
                    }
                }

                // 重新排序
                for (int i = 0; i < stepData.testBasePara.Count; i++)
                {
                    stepData.testBasePara[i].Index = i + 1;
                }

                // 保存配置
                testConfig.Save();

                // 重新加载表格
                Load100hStepDetails();

                Var.MsgBoxInfo(this, $"已删除 {deletedCount} 行数据");
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "删除失败：" + ex.Message);
            }
        }

        private void dgvMH_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // 处理数据验证错误
            if (e.Exception != null)
            {
                Var.MsgBoxWarn(this, "数据输入错误：" + e.Exception.Message);
                e.ThrowException = false;
            }
        }


    }
}


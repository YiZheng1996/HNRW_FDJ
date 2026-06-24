using MainUI.BLL;
using MainUI.FSql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniExcelLibs;
using System.IO;
using MainUI.Global;
using MainUI.FSql.Model;
using MainUI.FSql.AllCollectData;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sunny.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using NPOI.SS.Formula.Functions;
using MainUI.Modules;
using System.Collections;

namespace MainUI.Report
{
    /// <summary>
    /// pym
    /// </summary>
    public partial class ucAllDataRecord : UserControl
    {

        private bool _syncingCheckState = false;

        /// <summary>
        /// 查询名称
        /// </summary>
        private string searchName { get; set; }
        private DataTable dt = new DataTable();
        private List<TestParaAllData> _allData = new List<TestParaAllData>();
        private int _currentPage = 1;
        private int _pageSize = 200;
        private int _totalPages = 0;
        private readonly Dictionary<int, string> _columnNameCache = new Dictionary<int, string>();
        private bool _columnsInitialized = false;
        AllDataRecordDB allDataRecordDB = new AllDataRecordDB();

        List<string> KeyNameList = new List<string>();

        ucAllDataRecord_Method ucAllDataRecord_Method = new ucAllDataRecord_Method();

        Dictionary<int,Dictionary<string, object>> RowDictionary = new Dictionary<int, Dictionary<string, object>>();


        // 预定义的列顺序和显示名称
        private List<ColumnDefinition> _columnDefinitions = new List<ColumnDefinition>
        {
            // 基本信息和序号列
            new ColumnDefinition("Index", "序号"),
            new ColumnDefinition("RecordName", "记录点"),
            new ColumnDefinition("TestName", "试验类型"),
            new ColumnDefinition("TestStage", "试验阶段"),
            new ColumnDefinition("TestCycle", "试验周期"),
            new ColumnDefinition("TestStep", "试验循环节点"),
            new ColumnDefinition("DataTime", "日期"),
            new ColumnDefinition("Time", "时间"),
            new ColumnDefinition("HourNum", "小时数"),
            new ColumnDefinition("RecordDataTime", "采集时间"),
            new ColumnDefinition("DieselEngineModel", "柴油机型号"),
            new ColumnDefinition("DieselEngineNo", "发动机编号"),
            new ColumnDefinition("UserName", "操作人员"),
        };


        // 列定义类
        //private class ColumnDefinition
        //{
        //    public string PropertyName { get; set; }
        //    public string DisplayName { get; set; }
        //    /// <summary>模块列在 RowDictionary 内层字典的 key；基础列为 null</summary>
        //    public string GroupName { get; set; }
        //    public Type SourceType { get; set; }
        //    public PropertyInfo PropertyInfo { get; set; }

        //    public ColumnDefinition(string propertyName, string displayName)
        //    {
        //        PropertyName = propertyName;
        //        DisplayName = displayName;
        //        SourceType = typeof(TestParaAllData);
        //    }
        //}

        public ucAllDataRecord()
        {
            InitializeComponent();

            InitializeColumnDefinitions();

            // 初始化DataGridView列（只执行一次）
            InitializeDataGridViewColumns();

            // 初始化时禁用分页按钮
            UpdatePaginationButtons();

        }

        private void ucAutoRecord_Load(object sender, EventArgs e)
        {
            this.dtpStartTime.Value = DateTime.Now.AddHours(-1);
            this.dtpEndTime.Value = DateTime.Now;

            this.cboModel.Text = Var.SysConfig.LastModel;
        }

        /// <summary>
        /// 初始化列定义，获取属性信息
        /// </summary>
        private void InitializeColumnDefinitions()
        {
            foreach (var column in _columnDefinitions)
            {
                var type = string.IsNullOrEmpty(column.GroupName)
                    ? typeof(TestParaAllData)
                    : (column.SourceType ?? typeof(TestParaAllData));
                column.SourceType = type;
                column.PropertyInfo = type.GetProperty(column.PropertyName, BindingFlags.Public | BindingFlags.Instance);
            }
        }

        /// <summary>
        /// 为动态模块列绑定 GroupName、SourceType、PropertyInfo
        /// </summary>
        private void TagModuleColumnDefinitions(List<string> keyNameList)
        {
            if (keyNameList == null || keyNameList.Count == 0)
                return;

            var moduleTypes = new Dictionary<string, Type>
            {
                { "BaseDataGrp", typeof(BaseDataGrp) },
                { "TRDPDataGrp", typeof(TRDPDataGrp) },
                { "AIDataGrp", typeof(AIDataGrp) },
                { "AODataGrp", typeof(AODataGrp) },
                { "DIDataGrp", typeof(DIDataGrp) },
                { "DODataGrp", typeof(DODataGrp) },
                { "ExChangeDataGrpDouble", typeof(ExChangeDataGrpDouble) },
                { "ExChangeDataGrpBool", typeof(ExChangeDataGrpBool) },
                { "PipelineFaultDataGrp", typeof(PipelineFaultDataGrp) },
                { "EngineOilDataGrp", typeof(EngineOilDataGrp) },
                { "FuelDataGrp", typeof(FuelDataGrp) },
                { "ThreePhaseElectricData", typeof(ThreePhaseElectricData) },
                { "WaterDataGrp", typeof(WaterDataGrp) },
                { "PLC2AIDataGrp", typeof(PLC2AIDataGrp) },
                { "StartPLCDataGrp", typeof(StartPLCDataGrp) },
                { "SpeedDataGrp", typeof(SpeedDataGrp) },
                { "GD350_1Data", typeof(GD350_1Data) },
            };

            foreach (var column in _columnDefinitions)
            {
                if (column.PropertyName == "Index" || !string.IsNullOrEmpty(column.GroupName))
                    continue;

                foreach (var module in moduleTypes)
                {
                    if (!keyNameList.Contains(module.Key))
                        continue;

                    var property = module.Value.GetProperty(column.PropertyName, BindingFlags.Public | BindingFlags.Instance);
                    if (property == null)
                        continue;

                    column.GroupName = module.Key;
                    column.SourceType = module.Value;
                    column.PropertyInfo = property;
                    break;
                }
            }
        }

        /// <summary>
        /// 初始化DataGridView列（只执行一次）
        /// </summary>
        private void InitializeDataGridViewColumns()
        {
            if (_columnsInitialized) return;

            // 设置表头样式
            allDataRecord.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            allDataRecord.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 15F, FontStyle.Regular);
            allDataRecord.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False; // 禁止换行

            // 设置行高和列宽
            allDataRecord.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            allDataRecord.RowTemplate.Height = 25;

            // 添加列（只添加一次）
            foreach (var column in _columnDefinitions)
            {
                var dataColumn = new DataGridViewTextBoxColumn
                {
                    Name = column.PropertyName,
                    HeaderText = column.DisplayName,
                    SortMode = DataGridViewColumnSortMode.NotSortable,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    Width = GetOptimalColumnWidth(column.DisplayName),
                    MinimumWidth = 80
                };
                allDataRecord.Columns.Add(dataColumn);
            }

            // 标记列已初始化
            _columnsInitialized = true;
        }

        /// <summary>
        /// 型号切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelctModel_Click(object sender, EventArgs e)
        {
            frmSpec fs = new frmSpec();
            fs.ShowDialog();
            if (fs.DialogResult != DialogResult.Yes) return;

            this.cboModel.Text = fs.SelectModel;
        }


        /// <summary>
        /// 数据查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {

            KeyNameList.Clear();

            //更新列名
            _columnDefinitions.Clear();
            _columnDefinitions.Add(new ColumnDefinition("Index", "序号"));
            _columnDefinitions.Add(new ColumnDefinition("RecordName", "记录点"));
            _columnDefinitions.Add(new ColumnDefinition("TestName", "试验类型"));
            _columnDefinitions.Add(new ColumnDefinition("TestStage", "试验阶段"));
            _columnDefinitions.Add(new ColumnDefinition("TestCycle", "试验周期"));
            _columnDefinitions.Add(new ColumnDefinition("TestStep", "试验循环节点"));
            _columnDefinitions.Add(new ColumnDefinition("DataTime", "日期"));
            _columnDefinitions.Add(new ColumnDefinition("Time", "时间"));
            _columnDefinitions.Add(new ColumnDefinition("HourNum", "小时数"));
            _columnDefinitions.Add(new ColumnDefinition("RecordDataTime", "采集时间"));
            _columnDefinitions.Add(new ColumnDefinition("DieselEngineModel", "柴油机型号"));
            _columnDefinitions.Add(new ColumnDefinition("DieselEngineNo", "发动机编号"));
            _columnDefinitions.Add(new ColumnDefinition("UserName", "操作人员"));

            // 修正时间比较逻辑
            if (this.dtpStartTime.Value > this.dtpEndTime.Value)
            {
                Var.MsgBoxInfo(this, "结束时间小于开始时间，请重新选择查询时间。");
                return;
            }
            try
            {
                
                List<TestParaAllData> _allDataOld = allDataRecordDB.selectData(dtpStartTime.Value, dtpEndTime.Value, cboModel.Text,txtNumber.Text);

                if (_allDataOld == null || _allDataOld.Count == 0)
                {
                    Var.MsgBoxInfo(this, "未查询到相关数据。");
                    ClearDataGridViewRows(); // 只清空行，不清空列
                    lblTotalNum.Text = "共 0 条";
                    _allDataOld = new List<TestParaAllData>();
                    UpdatePaginationButtons();
                    return;
                }

                List<Dictionary<string, object>> _allDataList = allDataRecordDB.jsonToDictionary(_allDataOld);
                
                if (_allDataList == null || _allDataList.Count == 0)
                {
                    Var.MsgBoxInfo(this, "没有对应采集参数。");
                    // 更新总条数显示
                    lblTotalNum.Text = $"共 {_allDataOld.Count} 条";

                    // 计算总页数
                    _totalPages = (int)Math.Ceiling((double)_allDataOld.Count / _pageSize);

                    //显示页数
                    pageNO.Text = $"第1页/{_totalPages}页";

                    // 重置当前页
                    _currentPage = 1;
                    _allData = _allDataOld;

                    // 根据数据量决定显示方式
                    if (_allDataOld.Count <= _pageSize)
                    {
                        // 数据量小，直接显示全部
                        DisplayData(_allDataOld, KeyNameList);
                    }
                    else
                    {
                        // 数据量大，显示第一页
                        var pageData = _allDataOld.Take(_pageSize).ToList();
                        DisplayData(pageData, KeyNameList);
                        Var.MsgBoxInfo(this, $"查询到 {_allDataOld.Count} 条数据，超过 {_pageSize} 条，已启用分页显示。当前显示第 1 页，共 {_totalPages} 页。");
                    }
                    UpdatePaginationButtons();
                    return;
                }


                //----------------------------------------------------------------------------------------------------------------------------------------

                //获取复选框选中的对象的test集合
                GetCheckBoxGroup();

                //添加需要添加的列，清除原先初始化的列
                _columnDefinitions = ucAllDataRecord_Method.AddtcolumnDefinitions(KeyNameList, _columnDefinitions);
                TagModuleColumnDefinitions(KeyNameList);
                InitializeColumnDefinitions();

                //给对应实体对象赋值
                RowDictionary = ucAllDataRecord_Method.AddRowValue(_allDataList, KeyNameList, RowDictionary);

                _allData = _allDataOld;

                //------------------------------------------------------------------------------------------------------------------------------------------------------------
                //清除原先添加的列
                allDataRecord.Columns.Clear();

                // 设置表头样式
                allDataRecord.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                allDataRecord.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 15F, FontStyle.Regular);
                allDataRecord.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False; // 禁止换行

                // 设置行高和列宽
                allDataRecord.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                allDataRecord.RowTemplate.Height = 25;

                //添加列
                foreach (var column in _columnDefinitions)
                {
                    var dataColumn = new DataGridViewTextBoxColumn
                    {
                        Name = column.PropertyName,
                        HeaderText = column.DisplayName,
                        SortMode = DataGridViewColumnSortMode.NotSortable,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                        Width = GetOptimalColumnWidth(column.DisplayName),
                        MinimumWidth = 80
                    };
                    allDataRecord.Columns.Add(dataColumn);
                }
                // 标记列已初始化
                _columnsInitialized = true;

                // 更新总条数显示
                lblTotalNum.Text = $"共 {_allDataOld.Count} 条";

                // 计算总页数
                _totalPages = (int)Math.Ceiling((double)_allDataOld.Count / _pageSize);

                //显示页数
                pageNO.Text = $"第1页/{_totalPages}页";

                // 重置当前页
                _currentPage = 1;

                // 根据数据量决定显示方式
                if (_allDataOld.Count <= _pageSize)
                {
                    // 数据量小，直接显示全部
                    DisplayData(_allDataOld, KeyNameList);
                }
                else
                {
                    // 数据量大，显示第一页
                    var pageData = _allDataOld.Take(_pageSize).ToList();
                    DisplayData(pageData, KeyNameList);
                    Var.MsgBoxInfo(this, $"查询到 {_allDataOld.Count} 条数据，超过 {_pageSize} 条，已启用分页显示。当前显示第 1 页，共 {_totalPages} 页。");
                }

                UpdatePaginationButtons();
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, $"查询数据时发生错误：{ex.Message}");
            }
        }


        /// <summary>
        /// 上一页按钮点击事件
        /// </summary>
        private void btnUpPage_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                LoadPageData();
            }
        }

        /// <summary>
        /// 下一页按钮点击事件
        /// </summary>
        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                LoadPageData();
            }
        }

        /// <summary>
        /// 加载当前页数据
        /// </summary>
        private void LoadPageData()
        {
            if (_allData == null || _allData.Count == 0)
                return;

            int skip = (_currentPage - 1) * _pageSize;
            var pageData = _allData.Skip(skip).Take(_pageSize).ToList();

            //显示页数
            pageNO.Text = $"第{_currentPage}页/{_totalPages}页";

            DisplayData(pageData, KeyNameList);
            UpdatePaginationButtons();
        }

        /// <summary>
        /// 在DataGridView中显示数据
        /// </summary>
        private void DisplayData(List<TestParaAllData> data, List<string> KeyNameList)
        {
            ClearDataGridViewRows();

            if (data == null || data.Count == 0)
                return;

            if (!_columnsInitialized)
            {
                InitializeDataGridViewColumns();
            }

            int recordNumber = (_currentPage - 1) * _pageSize + 1;
            foreach (var record in data)
            {
                var row = allDataRecord.Rows[allDataRecord.Rows.Add()];
                int rowIndex = recordNumber - 1;
                int columnIndex = 0;

                foreach (var column in _columnDefinitions)
                {
                    object value = null;

                    if (column.PropertyName == "Index")
                    {
                        value = recordNumber;
                    }
                    else if (string.IsNullOrEmpty(column.GroupName))
                    {
                        if (column.PropertyInfo != null)
                        {
                            value = column.PropertyInfo.GetValue(record);
                        }
                    }
                    else if (RowDictionary != null
                             && RowDictionary.TryGetValue(rowIndex, out var modules)
                             && modules != null
                             && modules.TryGetValue(column.GroupName, out object moduleObj)
                             && moduleObj != null
                             && column.PropertyInfo != null)
                    {
                        value = column.PropertyInfo.GetValue(moduleObj);
                    }

                    row.Cells[columnIndex].Value = FormatValueForDisplay(value);
                    columnIndex++;
                }

                recordNumber++;
            }

            AutoAdjustColumns();
        }

        /// <summary>
        /// 根据文字长度计算最佳列宽
        /// </summary>
        private int GetOptimalColumnWidth(string text)
        {
            using (Graphics graphics = allDataRecord.CreateGraphics())
            {
                SizeF textSize = graphics.MeasureString(text, allDataRecord.ColumnHeadersDefaultCellStyle.Font);
                return (int)Math.Ceiling(textSize.Width) + 20; // 增加20像素的边距
            }
        }

        /// <summary>
        /// 自动调整列宽（确保内容完全显示）
        /// </summary>
        private void AutoAdjustColumns()
        {
            // 如果DataGridView为空，直接返回
            if (allDataRecord.Rows.Count == 0) return;

            // 暂停布局
            allDataRecord.SuspendLayout();

            try
            {
                // 使用AllCells模式计算所有列的最佳宽度
                allDataRecord.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                // 强制重新布局
                allDataRecord.PerformLayout();

                // 检查并修复列宽
                for (int i = 0; i < allDataRecord.Columns.Count; i++)
                {
                    var column = allDataRecord.Columns[i];

                    // 确保每列都有足够的最小宽度
                    if (column.Width < 100)
                    {
                        column.Width = 100;
                    }

                    // 特殊处理采集时间列
                    if (column.Name == "RecordDataTime")
                    {
                        // 计算时间内容需要的宽度
                        string sampleTime = "XXXX-XX-XX XX:XX:XX"; // 最长时间示例
                        int contentWidth = TextRenderer.MeasureText(
                            sampleTime,
                            allDataRecord.DefaultCellStyle.Font).Width + 20; // 增加边距

                        // 取表头宽度和内容宽度的最大值
                        int headerWidth = TextRenderer.MeasureText(
                            column.HeaderText,
                            allDataRecord.ColumnHeadersDefaultCellStyle.Font).Width + 20;

                        column.Width = Math.Max(Math.Max(headerWidth, contentWidth), 150); // 最小150像素
                    }
                    else if (column.Name == "Index")
                    {
                        // 序号列可以窄一些
                        column.Width = Math.Max(column.Width, 60);
                    }
                    else if (column.Name == "RecordName")
                    {
                        // 序号列可以窄一些
                        column.Width = Math.Max(column.Width, 150);
                    }

                    // 特别注意最后一列
                    if (i == allDataRecord.Columns.Count - 1)
                    {
                        // 确保最后一列足够宽
                        int requiredWidth = TextRenderer.MeasureText(
                            column.HeaderText,
                            allDataRecord.ColumnHeadersDefaultCellStyle.Font).Width + 40;

                        if (column.Width < requiredWidth)
                        {
                            column.Width = requiredWidth;
                        }
                    }
                }

                // 关闭自动调整模式，设置固定宽度
                allDataRecord.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            }
            finally
            {
                // 恢复布局
                allDataRecord.ResumeLayout(true);
            }
        }


        /// <summary>
        /// 格式化值用于显示
        /// </summary>
        private object FormatValueForDisplay(object value)
        {
            if (value == null) return "";

            if (value is double doubleValue)
            {
                return doubleValue.ToString();
            }
            else if (value is DateTime dateTimeValue)
            {
                return dateTimeValue.ToString("yyyy-MM-dd HH:mm:ss");
            }
            //pym
            else if (value is Boolean booleanValue)
            {
                string state = "关";
                if (booleanValue is true)
                {
                    state = "开";
                }
                return state;
            }


            return value;
        }

     

        /// <summary>
        /// 清空DataGridView行（保留列头）
        /// </summary>
        private void ClearDataGridViewRows()
        {
            allDataRecord.Rows.Clear();
        }

        /// <summary>
        /// 清空DataGridView（包括列头）- 用于特殊情况
        /// </summary>
        private void ClearDataGridView()
        {
            allDataRecord.Rows.Clear();
            allDataRecord.Columns.Clear();
            _columnsInitialized = false;
        }

        /// <summary>
        /// 更新分页按钮状态
        /// </summary>
        private void UpdatePaginationButtons()
        {
            bool hasData = _allData != null && _allData.Count > _pageSize;

            btnUpPage.Enabled = hasData && _currentPage > 1;
            btnNextPage.Enabled = hasData && _currentPage < _totalPages;

            // 如果数据量小于等于pageSize，隐藏分页按钮
            if (_allData != null && _allData.Count <= _pageSize)
            {
                btnUpPage.Visible = false;
                btnNextPage.Visible = false;
            }
            else
            {
                btnUpPage.Visible = true;
                btnNextPage.Visible = true;
            }
        }

        /// <summary>
        /// 将数字列索引转换为Excel列名（A-Z, AA-ZZ格式）
        /// </summary>
        private string GetColumnName(int columnIndex)
        {
            if (_columnNameCache.TryGetValue(columnIndex, out string cachedName))
            {
                return cachedName;
            }

            string columnName = "";
            int tempIndex = columnIndex;

            while (tempIndex >= 0)
            {
                columnName = (char)('A' + tempIndex % 26) + columnName;
                tempIndex = tempIndex / 26 - 1;
            }

            _columnNameCache[columnIndex] = columnName;
            return columnName;
        }


        /// <summary>
        ///全选多选框触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void Choice_All(object sender, EventArgs e)
        {
            if (!(sender is System.Windows.Forms.CheckBox choiceAll)) return;
            SetGroupBoxCheckAll(choiceAll.Checked, choiceAll);
        }




        /// <summary>
        /// 设置 groupBox1 内所有 UICheckBox 的选中状态
        /// </summary>
        /// <param name="isChecked">是否选中</param>
        /// <param name="exclude">要排除的复选框（一般是“全选”本身）</param>
        private void SetGroupBoxCheckAll(bool isChecked, System.Windows.Forms.CheckBox exclude = null)
        {
            // 全选：选中 0 ~ Items.Count-1 所有索引
            foreach (Control ctrl in groupBox1.Controls)
            {
                if (ctrl is System.Windows.Forms.CheckBox cb && cb != exclude)
                    cb.Checked = isChecked;
            }

        }


        /// <summary>
        /// 获取所有选择的复选框的集合
        /// </summary>
        private void GetCheckBoxGroup()
        {

            foreach (Control ctrl in groupBox1.Controls)
            {
                if (ctrl is System.Windows.Forms.CheckBox cb && cb != ChoiceAll && cb.Checked)
                {
                    string keyName = cb.Name;
                    KeyNameList.Add(keyName);
                }
            }
        }
        

        //给pageSzie赋值
        private void pageSize_Change(object sender, EventArgs e)
        {
            _pageSize = this.pageSize.Text.ToInt();
        }


        /// <summary>
        /// 数据分析导出excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Report_Save(object sender, EventArgs e)
        {
            ucAllDataRecord_Method.Report_Excel(this.allDataRecord);
        }
    }
}
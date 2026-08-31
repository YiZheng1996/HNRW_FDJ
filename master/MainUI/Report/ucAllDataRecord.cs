using MainUI.BLL;
using MainUI.FSql;
using MainUI.FSql.AllCollectData;
using MainUI.FSql.Model;
using MainUI.Global;
using MainUI.Modules;
using MiniExcelLibs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.SS.Formula.Functions;
using NPOI.Util.Collections;
using Sunny.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MainUI.Report
{
    /// <summary>
    /// 数据分析页：总数据记录表 + 启动柜数据表
    /// </summary>
    public partial class ucAllDataRecord : UserControl
    {
        #region 公用字段

        /// <summary>数据库访问</summary>
        AllDataRecordDB allDataRecordDB = new AllDataRecordDB();

        /// <summary>列定义、JSON 解析、导出等辅助方法</summary>
        ucAllDataRecord_Method ucAllDataRecord_Method = new ucAllDataRecord_Method();

        /// <summary>是否已执行过查询，翻页和导出时用于判断</summary>
        private bool _hasSearched = false;

        /// <summary>最近一次查询的开始时间</summary>
        private DateTime _searchBeginTime;

        /// <summary>最近一次查询的结束时间</summary>
        private DateTime _searchEndTime;

        /// <summary>最近一次查询的柴油机型号</summary>
        private string _searchModel;

        /// <summary>最近一次查询的发动机编号</summary>
        private string _searchNumber;

        /// <summary>查询名称（预留）</summary>
        private string searchName { get; set; }

        private DataTable dt = new DataTable();

        private bool _syncingCheckState = false;

        #endregion

        #region 总数据记录字段

        /// <summary>当前页总数据，仅保存本页记录，不缓存全量结果</summary>
        private List<TestParaAllData> _currentPageData = new List<TestParaAllData>();

        /// <summary>总数据当前页码</summary>
        private int _currentPage = 1;

        /// <summary>总数据每页条数</summary>
        private int _pageSize = 200;

        /// <summary>总数据总页数</summary>
        private int _totalPages = 0;

        /// <summary>符合查询条件的总数据记录总数</summary>
        private long _totalCount = 0;

        /// <summary>勾选的动态模块名称列表</summary>
        List<string> KeyNameList = new List<string>();

        /// <summary>当前页行索引 -> 模块解析结果，供动态列取值</summary>
        Dictionary<int, Dictionary<string, object>> RowDictionary = new Dictionary<int, Dictionary<string, object>>();

        /// <summary>总数据表格列是否已初始化</summary>
        private bool _columnsInitialized = false;

        /// <summary>Excel 列名缓存</summary>
        private readonly Dictionary<int, string> _columnNameCache = new Dictionary<int, string>();

        /// <summary>
        /// 总数据表预定义列顺序和显示名称
        /// </summary>
        private List<ColumnDefinition> _columnDefinitions = new List<ColumnDefinition>
        {
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

        /// <summary>
        /// 动态模块表头背景色色板（HTML 颜色值）。
        /// 同一数据组（GroupName）共用同一编号 Tag_num，再从此数组取色；
        /// 模块超过色板长度时对 Tag_num 取模轮询。
        /// </summary>
        private static readonly string[] HeaderColorPalette =
        {
            "#DDFA5E", "#5EFA79", "#FA5E68", "#5EB8FA",
            "#FA9E5E", "#C05EFA", "#5EFAE8", "#FA5EC8",
            "#A8FA5E", "#5E7AFA", "#FAD45E", "#FA7A5E",
            "#5EFAAF", "#E05EFA", "#5ED0FA", "#FA5E9A",
            "#7AFA5E", "#8A5EFA"
        };

        /// <summary>
        /// 超过此列数时跳过 AllCells 自动列宽，避免 FillWeight 总和超过 65535 上限
        /// </summary>
        private const int MaxAutoAdjustColumnCount = 600;

        #endregion

        #region 启动柜数据字段

        /// <summary>是否显示启动柜 Tab（勾选启动柜相关模块时）</summary>
        private bool _isStartUp = false;

        /// <summary>是否已执行过启动柜查询</summary>
        private bool _hasStartupSearched = false;

        /// <summary>当前页启动柜数据</summary>
        private List<StartupTestPara> _currentStartupPageData = new List<StartupTestPara>();

        /// <summary>启动柜当前页码</summary>
        private int _startupCurrentPage = 1;

        /// <summary>启动柜每页条数</summary>
        private int _startupPageSize = 200;

        /// <summary>启动柜总页数</summary>
        private int _startupTotalPages = 0;

        /// <summary>符合查询条件的启动柜记录总数</summary>
        private long _startupTotalCount = 0;

        #endregion

        #region 初始化

        public ucAllDataRecord()
        {
            InitializeComponent();

            InitializeColumnDefinitions();
            InitializeDataGridViewColumns();
            allDataRecord.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            UpdateAllDataPaginationButtons();
            UpdateStartupPaginationButtons();
        }

        private void ucAutoRecord_Load(object sender, EventArgs e)
        {
            this.dtpStartTime.Value = DateTime.Now.AddHours(-1);
            this.dtpEndTime.Value = DateTime.Now;
            this.cboModel.Text = Var.SysConfig.LastModel;
        }

        #endregion

        #region 总数据记录查询

        /// <summary>
        /// 执行总数据查询：统计总数、构建动态列、加载第一页
        /// </summary>
        private void SearchAllDataRecords()
        {
            _totalCount = allDataRecordDB.SelectDataCount(_searchBeginTime, _searchEndTime, _searchModel, _searchNumber);

            if (_totalCount == 0)
            {
                ClearAllDataGridViewRows();
                lblTotalNum.Text = "共 0 条";
                _currentPageData = new List<TestParaAllData>();
                _hasSearched = false;
                _totalPages = 0;
                pageNO.Text = "第0页/共0页";
                UpdateAllDataPaginationButtons();
                return;
            }

            BuildAllDataGridColumns();

            _hasSearched = true;
            _currentPage = 1;
            _totalPages = (int)Math.Ceiling((double)_totalCount / _pageSize);
            lblTotalNum.Text = $"共 {_totalCount} 条";
            pageNO.Text = $"第1页/{_totalPages}页";

            LoadAllDataPage();
        }

        /// <summary>
        /// 总数据上一页
        /// </summary>
        private void btnUpPage_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                LoadAllDataPage();
            }
        }

        /// <summary>
        /// 总数据下一页
        /// </summary>
        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                LoadAllDataPage();
            }
        }

        /// <summary>
        /// 总数据每页条数变更
        /// </summary>
        private void pageSize_Change(object sender, EventArgs e)
        {
            int newPageSize = this.pageSize.Text.ToInt();
            if (newPageSize <= 0)
            {
                return;
            }

            _pageSize = newPageSize;
            if (!_hasSearched || _totalCount == 0)
            {
                return;
            }

            _currentPage = 1;
            _totalPages = (int)Math.Ceiling((double)_totalCount / _pageSize);
            LoadAllDataPage();
        }

        /// <summary>
        /// 从数据库加载总数据当前页，解析 JSON 并刷新表格
        /// </summary>
        private void LoadAllDataPage()
        {
            if (!_hasSearched || _totalCount == 0)
                return;

            _currentPageData = allDataRecordDB.SelectDataPage(
                _searchBeginTime,
                _searchEndTime,
                _searchModel,
                _searchNumber,
                _currentPage,
                _pageSize);

            RowDictionary = ucAllDataRecord_Method.jsonToObject(_currentPageData, RowDictionary, KeyNameList);

            pageNO.Text = $"第{_currentPage}页/{_totalPages}页";
            DisplayAllData(_currentPageData, KeyNameList);
            UpdateAllDataPaginationButtons();
        }

        /// <summary>
        /// 导出本页：Sheet1 为总数据，Sheet2 为启动柜数据
        /// </summary>
        private void Report_Save(object sender, EventArgs e)
        {
            if (!_hasSearched && !_hasStartupSearched)
            {
                Var.MsgBoxInfo(this, "请先查询数据后再导出。");
                return;
            }

            ucAllDataRecord_Method.Report_Excel(this.allDataRecord, this.dgvStartupRecord);
        }

        /// <summary>
        /// 导出全部：Sheet1 为总数据，Sheet2 为启动柜数据
        /// </summary>
        private void Report_Save_All(object sender, EventArgs e)
        {
            using (var dlg = new frmMessageYesNO())
            {
                dlg.Msg = "请确认时间和项点后导出\r\n数据量较大，请在实验完成后导出";
                dlg.TipsFont = new Font("宋体", 18F, FontStyle.Regular); // 改这里的大小
                dlg.ShowDialog(this);
                if (!dlg.IsYes) return;
                if (dlg.IsYes)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        var allData = allDataRecordDB.selectData(dtpStartTime.Value, dtpEndTime.Value, cboModel.Text, txtNumber.Text);
                        //查询所有被选中的项点
                        GetCheckBoxGroup();
                        //为动态模块列绑定 GroupName、SourceType、PropertyInfo。
                        if (KeyNameList != null && KeyNameList.Count != 0)
                        {
                            _columnDefinitions = ucAllDataRecord_Method.AddtcolumnDefinitions(KeyNameList, _columnDefinitions);

                            TagModuleColumnDefinitions(KeyNameList);
                        }
                        var exportRowDictionary = ucAllDataRecord_Method.jsonToObject(
                            allData,
                            new Dictionary<int, Dictionary<string, object>>(),
                            KeyNameList);
                        var allStartupData = allDataRecordDB.selectStartupData(dtpStartTime.Value, dtpEndTime.Value, txtNumber.Text);
                        Cursor = Cursors.Default;
                        ucAllDataRecord_Method.Report_Excel_All(
                            _columnDefinitions,
                            allData,
                            exportRowDictionary,
                            allStartupData,
                            this.FindForm());
                    }
                    catch (Exception ex)
                    {
                        Var.MsgBoxWarn(this, $"导出全部数据时发生错误：{ex.Message}");
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
            
        }

        /// <summary>
        /// 重置总数据基础列定义
        /// </summary>
        private void ResetAllDataColumnDefinitions()
        {
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
        }

        /// <summary>
        /// 根据勾选模块构建总数据表格列
        /// </summary>
        private void BuildAllDataGridColumns()
        {
            if (KeyNameList != null && KeyNameList.Count != 0)
            {
                _columnDefinitions = ucAllDataRecord_Method.AddtcolumnDefinitions(KeyNameList, _columnDefinitions);
                // 表头着色步骤1：为动态列绑定所属模块（GroupName）
                TagModuleColumnDefinitions(KeyNameList);
                // 表头着色步骤2：按 GroupName 分配颜色编号 Tag_num
                AssignDynamicHeaderColorTags();
            }

            InitializeColumnDefinitions();

            bool manyColumns = _columnDefinitions.Count > MaxAutoAdjustColumnCount;

            allDataRecord.SuspendLayout();
            // 必须先关闭 Fill，再批量 Add 列，否则第 656 列起 FillWeight 总和会超过 65535
            allDataRecord.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            allDataRecord.Columns.Clear();

            allDataRecord.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            allDataRecord.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 15F, FontStyle.Regular);
            allDataRecord.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            allDataRecord.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            allDataRecord.RowTemplate.Height = 25;

            // 关闭系统默认表头样式，否则自定义 BackColor 不生效
            allDataRecord.EnableHeadersVisualStyles = false;
            foreach (var column in _columnDefinitions)
            {
                var dataColumn = new DataGridViewTextBoxColumn
                {
                    Name = column.PropertyName,
                    HeaderText = column.DisplayName,
                    SortMode = DataGridViewColumnSortMode.NotSortable,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    Width = GetOptimalColumnWidth(column.DisplayName, allDataRecord),
                    MinimumWidth = 80,
                    FillWeight = manyColumns ? 1 : 100,
                };
                // 表头着色步骤3：Tag_num>0 为动态模块列，从色板取背景色；基础列 Tag_num=0 不着色
                if (column.Tag_num > 0)
                {
                    int colorIndex = (column.Tag_num - 1) % HeaderColorPalette.Length;
                    dataColumn.HeaderCell.Style.BackColor = ColorTranslator.FromHtml(HeaderColorPalette[colorIndex]);
                }
                allDataRecord.Columns.Add(dataColumn);
            }

            EnsureSafeAllDataGridColumnSizing();
            allDataRecord.ResumeLayout();
            _columnsInitialized = true;
        }

        /// <summary>
        /// 初始化总数据列定义，获取属性反射信息
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
        /// 为动态模块列绑定 GroupName、SourceType、PropertyInfo。
        /// GroupName 同时用于表头分组着色（见 AssignDynamicHeaderColorTags）。
        /// 注意：按属性名匹配模块，若多模块存在同名字段（如 FaultReset），
        /// 会命中 moduleTypes 字典中靠前的模块，可能导致颜色与列来源不一致。
        /// </summary>
        private void TagModuleColumnDefinitions(List<string> keyNameList)
        {
            if (keyNameList == null || keyNameList.Count == 0)
                return;

            var moduleTypes = new Dictionary<string, Type>
            {
                { "BaseDataGrp", typeof(BaseDataGrp) },
                { "TRDPDataGrp", typeof(TRDPDataGrp) },
                { "TRDPData1Grp", typeof(TRDPData1Grp) },
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
                { "AirDuctData1Grp", typeof(AirDuctData1Grp) },
                { "AirDuctData2Grp", typeof(AirDuctData2Grp) },
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
        /// 初始化总数据 DataGridView 列（首次加载时）
        /// </summary>
        private void InitializeDataGridViewColumns()
        {
            if (_columnsInitialized) return;

            allDataRecord.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            allDataRecord.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 15F, FontStyle.Regular);
            allDataRecord.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            allDataRecord.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            allDataRecord.RowTemplate.Height = 25;

            foreach (var column in _columnDefinitions)
            {
                var dataColumn = new DataGridViewTextBoxColumn
                {
                    Name = column.PropertyName,
                    HeaderText = column.DisplayName,
                    SortMode = DataGridViewColumnSortMode.NotSortable,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    Width = GetOptimalColumnWidth(column.DisplayName, allDataRecord),
                    MinimumWidth = 80
                };
                allDataRecord.Columns.Add(dataColumn);
            }

            _columnsInitialized = true;
        }

        /// <summary>
        /// 按列定义顺序为各模块分配表头颜色编号 Tag_num。
        /// 规则：同一 GroupName 共用同一 Tag_num；基础列无 GroupName 时 Tag_num=0（不着色）；
        /// 模块首次出现时占用下一个编号，编号与勾选顺序及列出现顺序有关。
        /// </summary>
        private void AssignDynamicHeaderColorTags()
        {
            // GroupName -> 颜色编号（1 起）
            var groupColorMap = new Dictionary<string, int>();
            int nextTag = 1;

            foreach (var column in _columnDefinitions)
            {
                // 序号、日期等基础列不参与模块着色
                if (string.IsNullOrEmpty(column.GroupName))
                {
                    column.Tag_num = 0;
                    continue;
                }

                // 同一模块只分配一次颜色编号，该模块下所有列共用
                if (!groupColorMap.TryGetValue(column.GroupName, out int tag))
                {
                    tag = nextTag++;
                    groupColorMap[column.GroupName] = tag;
                }

                column.Tag_num = tag;
            }
        }

        /// <summary>
        /// 在总数据 DataGridView 中显示当前页
        /// </summary>
        private void DisplayAllData(List<TestParaAllData> data, List<string> keyNameList)
        {
            ClearAllDataGridViewRows();

            if (data == null || data.Count == 0)
                return;

            if (!_columnsInitialized)
            {
                InitializeDataGridViewColumns();
            }

            EnsureSafeAllDataGridColumnSizing();

            int recordNumber = (_currentPage - 1) * _pageSize + 1;
            int localIndex = 0;
            foreach (var record in data)
            {
                var row = allDataRecord.Rows[allDataRecord.Rows.Add()];
                int rowIndex = localIndex;
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
                localIndex++;
            }

            AutoAdjustAllDataColumns();
        }

        /// <summary>
        /// 自动调整总数据表格列宽；列数过多时跳过，防止 FillWeight 超限报错
        /// </summary>
        private void AutoAdjustAllDataColumns()
        {
            if (allDataRecord.Rows.Count == 0)
            {
                return;
            }

            EnsureSafeAllDataGridColumnSizing();

            if (allDataRecord.Columns.Count > MaxAutoAdjustColumnCount)
            {
                return;
            }

            allDataRecord.SuspendLayout();

            try
            {
                allDataRecord.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                allDataRecord.PerformLayout();

                for (int i = 0; i < allDataRecord.Columns.Count; i++)
                {
                    var column = allDataRecord.Columns[i];

                    if (column.Width < 100)
                    {
                        column.Width = 100;
                    }

                    if (column.Name == "RecordDataTime")
                    {
                        string sampleTime = "XXXX-XX-XX XX:XX:XX";
                        int contentWidth = TextRenderer.MeasureText(
                            sampleTime,
                            allDataRecord.DefaultCellStyle.Font).Width + 20;

                        int headerWidth = TextRenderer.MeasureText(
                            column.HeaderText,
                            allDataRecord.ColumnHeadersDefaultCellStyle.Font).Width + 20;

                        column.Width = Math.Max(Math.Max(headerWidth, contentWidth), 150);
                    }
                    else if (column.Name == "Index")
                    {
                        column.Width = Math.Max(column.Width, 60);
                    }
                    else if (column.Name == "RecordName")
                    {
                        column.Width = Math.Max(column.Width, 150);
                    }

                    if (i == allDataRecord.Columns.Count - 1)
                    {
                        int requiredWidth = TextRenderer.MeasureText(
                            column.HeaderText,
                            allDataRecord.ColumnHeadersDefaultCellStyle.Font).Width + 40;

                        if (column.Width < requiredWidth)
                        {
                            column.Width = requiredWidth;
                        }
                    }
                }

                allDataRecord.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            }
            finally
            {
                allDataRecord.ResumeLayout(true);
            }
        }

        /// <summary>
        /// 强制关闭 Fill 列宽，并在列数过多时将 FillWeight 降为 1，避免总和超过 65535
        /// </summary>
        private void EnsureSafeAllDataGridColumnSizing()
        {
            allDataRecord.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            if (allDataRecord.Columns.Count <= MaxAutoAdjustColumnCount)
            {
                return;
            }

            foreach (DataGridViewColumn column in allDataRecord.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.FillWeight = 1;
            }
        }

        /// <summary>
        /// 更新总数据分页按钮状态
        /// </summary>
        private void UpdateAllDataPaginationButtons()
        {
            bool hasData = _totalCount > _pageSize;

            btnUpPage.Enabled = hasData && _currentPage > 1;
            btnNextPage.Enabled = hasData && _currentPage < _totalPages;

            if (_totalCount <= _pageSize)
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
        /// 清空总数据表格行（保留列头）
        /// </summary>
        private void ClearAllDataGridViewRows()
        {
            allDataRecord.Rows.Clear();
        }

        /// <summary>
        /// 清空总数据表格（包括列头）
        /// </summary>
        private void ClearAllDataGridView()
        {
            allDataRecord.Rows.Clear();
            allDataRecord.Columns.Clear();
            _columnsInitialized = false;
        }

        #endregion

        #region 启动柜数据查询

        /// <summary>
        /// 执行启动柜数据查询：统计总数并加载第一页
        /// </summary>
        private void SearchStartupRecords()
        {
            _startupTotalCount = allDataRecordDB.SelectStartupDataCount(_searchBeginTime, _searchEndTime, _searchNumber);

            if (_startupTotalCount == 0)
            {
                ClearStartupGridViewRows();
                lblTotalNum1.Text = "共 0 条";
                _currentStartupPageData = new List<StartupTestPara>();
                _hasStartupSearched = false;
                _startupTotalPages = 0;
                pageNO1.Text = "第0页/共0页";
                UpdateStartupPaginationButtons();
                return;
            }

            _hasStartupSearched = true;
            _startupCurrentPage = 1;
            _startupTotalPages = (int)Math.Ceiling((double)_startupTotalCount / _startupPageSize);
            lblTotalNum1.Text = $"共 {_startupTotalCount} 条";
            pageNO1.Text = $"第1页/{_startupTotalPages}页";

            LoadStartupDataPage();
        }

        /// <summary>
        /// 启动柜上一页
        /// </summary>
        private void btnUpPage1_Click(object sender, EventArgs e)
        {
            if (_startupCurrentPage > 1)
            {
                _startupCurrentPage--;
                LoadStartupDataPage();
            }
        }

        /// <summary>
        /// 启动柜下一页
        /// </summary>
        private void btnDownPag1_Click(object sender, EventArgs e)
        {
            if (_startupCurrentPage < _startupTotalPages)
            {
                _startupCurrentPage++;
                LoadStartupDataPage();
            }
        }

        /// <summary>
        /// 启动柜每页条数变更
        /// </summary>
        private void pageSize1_Change(object sender, EventArgs e)
        {
            int newPageSize = this.pageSize1.Text.ToInt();
            if (newPageSize <= 0)
            {
                return;
            }

            _startupPageSize = newPageSize;
            if (!_hasStartupSearched || _startupTotalCount == 0)
            {
                return;
            }

            _startupCurrentPage = 1;
            _startupTotalPages = (int)Math.Ceiling((double)_startupTotalCount / _startupPageSize);
            LoadStartupDataPage();
        }

        /// <summary>
        /// 从数据库加载启动柜当前页并刷新表格
        /// </summary>
        private void LoadStartupDataPage()
        {
            if (!_hasStartupSearched || _startupTotalCount == 0)
                return;

            _currentStartupPageData = allDataRecordDB.SelectStartupDataPage(
                _searchBeginTime,
                _searchEndTime,
                _searchNumber,
                _startupCurrentPage,
                _startupPageSize);

            pageNO1.Text = $"第{_startupCurrentPage}页/{_startupTotalPages}页";
            DisplayStartupData(_currentStartupPageData);
            UpdateStartupPaginationButtons();
        }

        /// <summary>
        /// 在启动柜 DataGridView 中显示当前页
        /// </summary>
        private void DisplayStartupData(List<StartupTestPara> data)
        {
            ClearStartupGridViewRows();

            if (data == null || data.Count == 0)
                return;

            int recordNumber = (_startupCurrentPage - 1) * _startupPageSize + 1;
            foreach (var record in data)
            {
                dgvStartupRecord.Rows.Add(
                    recordNumber,
                    record.RecordDataTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    record.TestName,
                    record.RPM,
                    record.Torque,
                    record.Power,
                    record.ExcitationVoltage,
                    record.ExcitationCurrent,
                    record.InvertVoltage,
                    record.InvertCurrent,
                    record.InvertRPM,
                    record.InvertPower,
                    record.InvertFaultCode);

                recordNumber++;
            }
        }

        /// <summary>
        /// 更新启动柜分页按钮状态
        /// </summary>
        private void UpdateStartupPaginationButtons()
        {
            bool hasData = _startupTotalCount > _startupPageSize;

            btnUpPage1.Enabled = hasData && _startupCurrentPage > 1;
            btnDownPag1.Enabled = hasData && _startupCurrentPage < _startupTotalPages;

            if (_startupTotalCount <= _startupPageSize)
            {
                btnUpPage1.Visible = false;
                btnDownPag1.Visible = false;
            }
            else
            {
                btnUpPage1.Visible = true;
                btnDownPag1.Visible = true;
            }
        }

        /// <summary>
        /// 清空启动柜表格行
        /// </summary>
        private void ClearStartupGridViewRows()
        {
            dgvStartupRecord.Rows.Clear();
        }

        #endregion

        #region 公用方法

        /// <summary>
        /// 查询按钮：同时执行总数据查询和启动柜查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.dtpStartTime.Value > this.dtpEndTime.Value)
            {
                Var.MsgBoxInfo(this, "结束时间小于开始时间，请重新选择查询时间。");
                return;
            }

            try
            {
                KeyNameList.Clear();
                ResetAllDataColumnDefinitions();
                CacheSearchConditions();
                GetCheckBoxGroup();

                SearchAllDataRecords();
                SearchStartupRecords();

                if (!_hasSearched && !_hasStartupSearched)
                {
                    Var.MsgBoxInfo(this, "未查询到相关数据。");
                }
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, $"查询数据时发生错误：{ex.Message}");
            }
        }

        /// <summary>
        /// 缓存当前查询条件，供两个 Tab 翻页和导出复用
        /// </summary>
        private void CacheSearchConditions()
        {
            _searchBeginTime = dtpStartTime.Value;
            _searchEndTime = dtpEndTime.Value;
            _searchModel = cboModel.Text;
            _searchNumber = txtNumber.Text;
        }

        /// <summary>
        /// 型号选择
        /// </summary>
        private void btnSelctModel_Click(object sender, EventArgs e)
        {
            frmSpec fs = new frmSpec();
            fs.ShowDialog();
            if (fs.DialogResult != DialogResult.Yes) return;

            this.cboModel.Text = fs.SelectModel;
        }

        /// <summary>
        /// 全选复选框触发
        /// </summary>
        private void Choice_All(object sender, EventArgs e)
        {
            if (!(sender is System.Windows.Forms.CheckBox choiceAll)) return;
            SetGroupBoxCheckAll(choiceAll.Checked, choiceAll);
        }

        /// <summary>
        /// 设置 groupBox1 内所有 CheckBox 的选中状态
        /// </summary>
        private void SetGroupBoxCheckAll(bool isChecked, System.Windows.Forms.CheckBox exclude = null)
        {
            foreach (Control ctrl in groupBox1.Controls)
            {
                if (ctrl is System.Windows.Forms.CheckBox cb && cb != exclude)
                    cb.Checked = isChecked;
            }
        }

        /// <summary>
        /// 获取 groupBox1 中所有已勾选的模块名称
        /// </summary>
        private void GetCheckBoxGroup()
        {
            foreach (Control ctrl in groupBox1.Controls)
            {
                if (ctrl is System.Windows.Forms.CheckBox cb && cb != ChoiceAll && cb.Checked)
                {
                    if (cb.Name == "TRDPDataGrp" && Var.SysConfig.LastModel == "12V280")
                    {
                        KeyNameList.Add("TRDPData1Grp");
                    }
                    else
                    {
                        KeyNameList.Add(cb.Name);
                    }
                }
            }

        }

        /// <summary>
        /// 格式化单元格值用于界面显示
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
            else if (value is Boolean booleanValue)
            {
                return booleanValue ? "1" : "0";
            }

            return value;
        }

        /// <summary>
        /// 根据表头文字计算列宽
        /// </summary>
        private int GetOptimalColumnWidth(string text, DataGridView dgv)
        {
            using (Graphics graphics = dgv.CreateGraphics())
            {
                SizeF textSize = graphics.MeasureString(text, dgv.ColumnHeadersDefaultCellStyle.Font);
                return (int)Math.Ceiling(textSize.Width) + 20;
            }
        }

        /// <summary>
        /// 将数字列索引转换为 Excel 列名（A-Z, AA-ZZ 格式）
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

        #endregion
    }
}


    }
}
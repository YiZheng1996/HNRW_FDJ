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

namespace MainUI.Report
{
    /// <summary>
    /// 自动试验报表导出界面
    /// </summary>
    public partial class ucAutoRecord : UserControl
    {
        /// <summary>
        /// 查询名称
        /// </summary>
        private string searchName { get; set; }
        private DataTable dt = new DataTable();
        private List<IRecordData> _allData = new List<IRecordData>();
        private int _currentPage = 1;
        private int _pageSize = 500;
        private int _totalPages = 0;
        private readonly Dictionary<int, string> _columnNameCache = new Dictionary<int, string>();
        private bool _columnsInitialized = false;

        // 预定义的列顺序和显示名称
        private readonly List<ColumnDefinition> _columnDefinitions = new List<ColumnDefinition>
        {
            // 基本信息和序号列
            new ColumnDefinition("Index", "序号"),
            new ColumnDefinition("DieselEngineNo", "发动机编号"),
            new ColumnDefinition("RecordDataTime", "采集时间"),
            new ColumnDefinition("RecordName", "记录点"),
            new ColumnDefinition("DataTime", "日期"),
            new ColumnDefinition("Time", "时间"),
            new ColumnDefinition("HourNum", "小时数"),
            //new ColumnDefinition("RecordNo", "记录号"), // 新增
            //new ColumnDefinition("TestType", "试验类型"), // 新增
            //new ColumnDefinition("Stage", "阶段"), // 新增
            //new ColumnDefinition("Cycle", "周期"), // 新增
            //new ColumnDefinition("CycleNode", "循环节点"), // 新增
        
            // 发动机基本参数
            new ColumnDefinition("RPM", "发动机转速 rpm"),
            new ColumnDefinition("Torque", "扭矩 N.m"),
            new ColumnDefinition("Power", "功率 kW"),
            new ColumnDefinition("FrontTurbochargerRPM", "前增压器转速 rpm"),
            new ColumnDefinition("AfterTurbochargerRPM", "后增压器转速 rpm"),

            // 环境参数
            new ColumnDefinition("AT", "环境温度 ℃"),
            new ColumnDefinition("AP", "大气压力 kPa"),
            new ColumnDefinition("AH", "空气湿度 %RH"),
            
            // 冷却水温度
            new ColumnDefinition("LWaterTempIn", "中冷水进机温度 ℃"),
            new ColumnDefinition("LWaterTempOut", "中冷水出机温度 ℃"),
            new ColumnDefinition("HWaterTempIn", "高温水进机温度 ℃"),
            new ColumnDefinition("HWaterTempOut", "高温水出机温度 ℃"),
            
            // 冷却水压力
            new ColumnDefinition("LPressureIn", "中冷水泵进口压力 kPa"),
            new ColumnDefinition("LPressureOut", "中冷水泵出口压力 kPa"),
            new ColumnDefinition("HPressureIn", "高温水泵进口压力 kPa"),
            new ColumnDefinition("HPressureOut", "高温水泵出口压力 kPa"),
            //new ColumnDefinition("LWaterOutPressure", "中冷水出机压力"), // 新增
            //new ColumnDefinition("HWaterOutPressure", "高温水出机压力"), // 新增
            
            // 机油参数
            new ColumnDefinition("HeatExchangerTempIn", "机油热交换器进口油温 ℃"),
            new ColumnDefinition("HeatExchangerTempOut", "机油热交换器出口油温 ℃"),
            //new ColumnDefinition("HeatExchangerWaterIn", "机油热交换器进口水温"), // 新增
            //new ColumnDefinition("HeatExchangerWaterOut", "机油热交换器出口水温"), // 新增
            new ColumnDefinition("EOPressure2", "主油道末端油压 kPa"),
            new ColumnDefinition("EOPressure1", "机油泵出口油压 kPa"),
            new ColumnDefinition("EngineOilOutletTemp", "机油泵出口油温 ℃"),
            //new ColumnDefinition("EOAnalysis", "机油分析"), // 新增
            new ColumnDefinition("EOConsumption", "机油消耗"), // 新增
            
            // 燃油参数
            //new ColumnDefinition("ECOTime", "燃油消耗时间"), // 新增
            //new ColumnDefinition("ECOQuantity", "燃油消耗量"), // 新增
            new ColumnDefinition("ECORate", "燃油消耗率"),
            new ColumnDefinition("OilTempIn", "燃油泵进口油温 ℃"),
            //new ColumnDefinition("InjectionParam", "喷射参数"), // 新增
            //new ColumnDefinition("Smoke", "烟度"),              // 新增
            
            // 中冷空气参数
            new ColumnDefinition("FrontAirTempIn", "前中冷前空气温度 ℃"),
            new ColumnDefinition("FrontAirTempOut", "前中冷后空气温度 ℃"),
            new ColumnDefinition("AfterAirTempIn", "后中冷前空气温度 ℃"),
            new ColumnDefinition("AfterAirTempOut", "后中冷后空气温度 ℃"),
            new ColumnDefinition("FrontAirPressureIn", "前中冷前空气压力 kPa"),
            new ColumnDefinition("FrontAirPressureOut", "前中冷后空气压力 kPa"),
            new ColumnDefinition("AfterAirPressureIn", "后中冷前空气压力 kPa"),
            new ColumnDefinition("AfterAirPressureOut", "后中冷后空气压力 kPa"),
            
            // 增压器参数
            new ColumnDefinition("FrontTurbochargerPressureIn", "前增压器进气真空度 kPa"),
            new ColumnDefinition("AfterTurbochargerPressureIn", "后增压器进气真空度 kPa"),
            new ColumnDefinition("FrontTurbochargerPressureOut", "前增压器排气背压 kPa"),
            new ColumnDefinition("AfterTurbochargerPressureOut", "后增压器排气背压 kPa"),
            
            // 涡轮废气参数
            new ColumnDefinition("FrontTurbochargerTempIn", "前涡轮进口废气温度 ℃"),
            new ColumnDefinition("AfterTurbochargerTempIn", "后涡轮进口废气温度 ℃"),
            new ColumnDefinition("FrontTurbochargerTempOut", "前涡轮出口废气温度 ℃"),
            new ColumnDefinition("AfterTurbochargerTempOut", "后涡轮出口废气温度 ℃"),
            new ColumnDefinition("FrontTurbochargerPressureIn2", "前涡轮进口废气压力 kPa"),
            new ColumnDefinition("AfterTurbochargerPressureIn2", "后涡轮进口废气压力 kPa"),
            
            // 排气温度 - A列
            new ColumnDefinition("EGTempA1", "A1缸排气温度 ℃"),
            new ColumnDefinition("EGTempA2", "A2缸排气温度 ℃"),
            new ColumnDefinition("EGTempA3", "A3缸排气温度 ℃"),
            new ColumnDefinition("EGTempA4", "A4缸排气温度 ℃"),
            new ColumnDefinition("EGTempA5", "A5缸排气温度 ℃"),
            new ColumnDefinition("EGTempA6", "A6缸排气温度 ℃"),
            new ColumnDefinition("EGTempA7", "A7缸排气温度 ℃"),
            new ColumnDefinition("EGTempA8", "A8缸排气温度 ℃"),
            
            // 排气温度 - B列
            new ColumnDefinition("EGTempB1", "B1缸排气温度 ℃"),
            new ColumnDefinition("EGTempB2", "B2缸排气温度 ℃"),
            new ColumnDefinition("EGTempB3", "B3缸排气温度 ℃"),
            new ColumnDefinition("EGTempB4", "B4缸排气温度 ℃"),
            new ColumnDefinition("EGTempB5", "B5缸排气温度 ℃"),
            new ColumnDefinition("EGTempB6", "B6缸排气温度 ℃"),
            new ColumnDefinition("EGTempB7", "B7缸排气温度 ℃"),
            new ColumnDefinition("EGTempB8", "B8缸排气温度 ℃"),
        };

        // 列定义类
        private class ColumnDefinition
        {
            public string PropertyName { get; set; }
            public string DisplayName { get; set; }
            public PropertyInfo PropertyInfo { get; set; }

            public ColumnDefinition(string propertyName, string displayName)
            {
                PropertyName = propertyName;
                DisplayName = displayName;
            }
        }

        public ucAutoRecord()
        {
            InitializeComponent();

            InitializeColumnDefinitions();

            // 初始化DataGridView列（只执行一次）默认自动模式
            InitializeDataGridViewColumns(_columnDefinitions);

            // 初始化时禁用分页按钮
            UpdatePaginationButtons();

            // 更改编号时
            EventTriggerModel.OnModelNumberChanged += (string no) =>
            {
                this.txtNumber.Text = no;
            };

            // 手动/自动切换事件
            this.rdoManual.CheckedChanged += RdoMode_CheckedChanged;
            this.rdoAuto.CheckedChanged += RdoMode_CheckedChanged;
        }

        // 模式切换处理：重建列、清空数据
        private void RdoMode_CheckedChanged(object sender, EventArgs e)
        {
            // CheckedChanged 在两个按钮上都会触发，只处理"刚被选中"的那次即可
            var rb = sender as RadioButton;
            if (rb == null || !rb.Checked) return;

            InitializeDataGridViewColumns(CurrentColumnDefinitions);
            ClearDataGridViewRows();
            _allData = new List<IRecordData>();
            lblTotalNum.Text = "共 0 条";
            UpdatePaginationButtons();
        }

        /// <summary>
        /// 在DataGridView中显示数据
        /// </summary>
        private void DisplayData(List<IRecordData> data)
        {
            ClearDataGridViewRows();

            if (data == null || data.Count == 0)
                return;

            if (!_columnsInitialized)
            {
                InitializeDataGridViewColumns(CurrentColumnDefinitions);
            }

            int recordNumber = (_currentPage - 1) * _pageSize + 1;
            foreach (var record in data)
            {
                var row = dgvRecord.Rows[dgvRecord.Rows.Add()];
                int columnIndex = 0;

                foreach (var column in CurrentColumnDefinitions)
                {
                    object value = null;

                    if (column.PropertyName == "Index")
                    {
                        value = recordNumber;
                    }
                    else if (column.PropertyInfo != null)
                    {
                        value = column.PropertyInfo.GetValue(record);
                    }

                    row.Cells[columnIndex].Value = FormatValueForDisplay(value);
                    columnIndex++;
                }

                recordNumber++;
            }

            AutoAdjustColumns();
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
            var autoType = typeof(AutoRecordPara);
            foreach (var column in _columnDefinitions)
                column.PropertyInfo = autoType.GetProperty(column.PropertyName);

            var manualType = typeof(ManualRecordPara);
            foreach (var column in _manualColumnDefinitions)
                column.PropertyInfo = manualType.GetProperty(column.PropertyName);
        }

        /// <summary>
        /// 初始化DataGridView列（只执行一次）
        /// </summary>
        private void InitializeDataGridViewColumns(List<ColumnDefinition> columns)
        {
            dgvRecord.Columns.Clear();

            // 设置表头样式
            dgvRecord.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRecord.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 15F, FontStyle.Regular);
            dgvRecord.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dgvRecord.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvRecord.RowTemplate.Height = 25;

            foreach (var column in columns)
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
                dgvRecord.Columns.Add(dataColumn);
            }

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
            // 修正时间比较逻辑
            if (this.dtpStartTime.Value > this.dtpEndTime.Value)
            {
                Var.MsgBoxInfo(this, "结束时间小于开始时间，请重新选择查询时间。");
                return;
            }
            if (string.IsNullOrEmpty(this.txtNumber.Text))
            {
                Var.MsgBoxInfo(this, "编号为空，请输入编号后再进行查询。");
                return;
            }
            try
            {
                if (rdoAuto.Checked)
                {
                    searchName = "自动记录";
                    var allRecordData = AutoRecordService.instnce.GetAllRecord(cboModel.Text, txtNumber.Text, dtpStartTime.Value, dtpEndTime.Value, false);
                    _allData = allRecordData.Cast<IRecordData>().ToList();
                }
                else
                {
                    searchName = "手动记录";
                    var allRecordData = ManualRecordService.instnce.GetAllRecord(cboModel.Text, txtNumber.Text, dtpStartTime.Value, dtpEndTime.Value, false);
                    _allData = allRecordData.Cast<IRecordData>().ToList();
                }

                if (_allData == null || _allData.Count == 0)
                {
                    Var.MsgBoxInfo(this, "未查询到相关数据。");
                    ClearDataGridViewRows(); // 只清空行，不清空列
                    lblTotalNum.Text = "共 0 条";
                    _allData = new List<IRecordData>();
                    UpdatePaginationButtons();
                    return;
                }

                // 更新总条数显示
                lblTotalNum.Text = $"共 {_allData.Count} 条";

                // 计算总页数
                _totalPages = (int)Math.Ceiling((double)_allData.Count / _pageSize);

                // 重置当前页
                _currentPage = 1;

                // 根据数据量决定显示方式
                if (_allData.Count <= _pageSize)
                {
                    // 数据量小，直接显示全部
                    DisplayData(_allData);
                }
                else
                {
                    // 数据量大，显示第一页
                    var pageData = _allData.Take(_pageSize).ToList();
                    DisplayData(pageData);
                    Var.MsgBoxInfo(this, $"查询到 {_allData.Count} 条数据，超过 {_pageSize} 条，已启用分页显示。当前显示第 1 页，共 {_totalPages} 页。");
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
        /// 导出按钮点击事件
        /// </summary>
        private async void btnOut_Click(object sender, EventArgs e)
        {
            if (_allData == null || _allData.Count == 0)
            {
                Var.MsgBoxInfo(this, "没有数据可导出。");
                return;
            }

            // 手动模式：按出厂记录表模板填充
            if (rdoManual.Checked)
            {
                ExportManualToExcel();
                return;
            }

            // ── 自动模式：保持原有 CSV 导出逻辑，完全不变 ───────────────────
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "CSV 文件 (*.csv)|*.csv|Excel 文件 (*.xlsx)|*.xlsx";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.FileName = $"{searchName}_{this.cboModel.Text}_{this.txtNumber.Text}_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                    saveFileDialog.DefaultExt = "csv";
                    saveFileDialog.AddExtension = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;
                        WriteDataToCsv(filePath);   // xlsx/csv 都走这个（与原代码一致）
                        Var.MsgBoxInfo(this, $"成功导出 {_allData.Count} 条记录到：{filePath}");
                    }
                }
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, $"导出数据时发生错误：{ex.Message}");
            }
        }

        /// <summary>
        /// 直接将数据写入CSV文件
        /// </summary>
        private void WriteDataToCsv(string filePath)
        {
            // 如有需要 定义要排除的字段 不写进报表
            var excludedFields = new HashSet<string> { "DieselEngineNo" };

            // 筛选要导出的列定义
            var exportColumns = _columnDefinitions
                .Where(col => !excludedFields.Contains(col.PropertyName))
                .ToList();

            using (var writer = new StreamWriter(filePath, false, new UTF8Encoding(false)))
            {
                // 写入列标题（只写入需要的列）
                var headers = exportColumns.Select(c => c.DisplayName);
                writer.WriteLine(string.Join(",", headers.Select(h => EscapeCsvField(h))));

                // 写入数据行
                int recordNumber = 1;
                foreach (var record in _allData)
                {
                    var rowValues = new List<string>();

                    foreach (var column in _columnDefinitions)
                    {
                        object value = null;

                        if (column.PropertyName == "Index")
                        {
                            value = recordNumber;
                        }
                        else if (column.PropertyName == "RecordDataTime")
                        {
                            var dateValue = column.PropertyInfo.GetValue(record);
                            if (dateValue is DateTime dateTime)
                            {
                                value = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            else
                            {
                                value = dateValue;
                            }
                        }
                        else if (column.PropertyInfo != null)
                        {
                            value = column.PropertyInfo.GetValue(record);
                        }

                        string stringValue = FormatValueForDisplay(value).ToString();
                        rowValues.Add(EscapeCsvField(stringValue));
                    }

                    writer.WriteLine(string.Join(",", rowValues));
                    recordNumber++;
                }
            }
        }
        /// <summary>
        /// 转义CSV字段（处理逗号、引号等特殊字符）
        /// </summary>
        private string EscapeCsvField(string field)
        {
            if (field == null) return "";

            // 如果字段包含逗号、双引号或换行符，需要用双引号包裹
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            {
                // 替换双引号为两个双引号
                field = field.Replace("\"", "\"\"");
                return $"\"{field}\"";
            }

            return field;
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

            DisplayData(pageData);
            UpdatePaginationButtons();
        }

        /// <summary>
        /// 根据文字长度计算最佳列宽
        /// </summary>
        private int GetOptimalColumnWidth(string text)
        {
            using (Graphics graphics = dgvRecord.CreateGraphics())
            {
                SizeF textSize = graphics.MeasureString(text, dgvRecord.ColumnHeadersDefaultCellStyle.Font);
                return (int)Math.Ceiling(textSize.Width) + 20; // 增加20像素的边距
            }
        }

        /// <summary>
        /// 自动调整列宽（确保内容完全显示）
        /// </summary>
        private void AutoAdjustColumns()
        {
            // 如果DataGridView为空，直接返回
            if (dgvRecord.Rows.Count == 0) return;

            // 暂停布局
            dgvRecord.SuspendLayout();

            try
            {
                // 使用AllCells模式计算所有列的最佳宽度
                dgvRecord.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                // 强制重新布局
                dgvRecord.PerformLayout();

                // 检查并修复列宽
                for (int i = 0; i < dgvRecord.Columns.Count; i++)
                {
                    var column = dgvRecord.Columns[i];

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
                            dgvRecord.DefaultCellStyle.Font).Width + 20; // 增加边距

                        // 取表头宽度和内容宽度的最大值
                        int headerWidth = TextRenderer.MeasureText(
                            column.HeaderText,
                            dgvRecord.ColumnHeadersDefaultCellStyle.Font).Width + 20;

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
                    if (i == dgvRecord.Columns.Count - 1)
                    {
                        // 确保最后一列足够宽
                        int requiredWidth = TextRenderer.MeasureText(
                            column.HeaderText,
                            dgvRecord.ColumnHeadersDefaultCellStyle.Font).Width + 40;

                        if (column.Width < requiredWidth)
                        {
                            column.Width = requiredWidth;
                        }
                    }
                }

                // 关闭自动调整模式，设置固定宽度
                dgvRecord.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            }
            finally
            {
                // 恢复布局
                dgvRecord.ResumeLayout(true);
            }
        }

        ///// <summary>
        ///// 自动调整列宽（确保内容完全显示）
        ///// </summary>
        //private void AutoAdjustColumns()
        //{
        //    // 如果DataGridView为空，直接返回
        //    if (dgvRecord.Rows.Count == 0) return;

        //    // 临时启用自动调整
        //    dgvRecord.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        //    // 保存计算后的宽度
        //    foreach (DataGridViewColumn column in dgvRecord.Columns)
        //    {
        //        int headerWidth = column.HeaderCell.Style.Font != null ?
        //            TextRenderer.MeasureText(column.HeaderText, column.HeaderCell.Style.Font).Width + 20 :
        //            column.Width;

        //        // 取表头宽度和内容宽度的最大值
        //        column.Width = Math.Max(headerWidth, column.Width);
        //        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; // 固定宽度
        //    }

        //    // 关闭自动调整模式
        //    dgvRecord.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

        //    // 可选：为最后一列设置填充模式，防止出现水平滚动条
        //    if (dgvRecord.Columns.Count > 0)
        //    {
        //        dgvRecord.Columns[dgvRecord.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        //    }
        //}

        /// <summary>
        /// 准备导出数据（转换为字典列表）
        /// </summary>
        private List<Dictionary<string, object>> PrepareExportData()
        {
            var exportData = new List<Dictionary<string, object>>();
            int recordNumber = 1;

            foreach (var record in _allData)
            {
                var rowData = new Dictionary<string, object>();

                foreach (var column in _columnDefinitions)
                {
                    object value = null;

                    // 处理序号列
                    if (column.PropertyName == "Index")
                    {
                        value = recordNumber;
                    }
                    else if (column.PropertyName == "RecordDataTime")
                    {
                        // 特殊处理采集时间列
                        var dateValue = column.PropertyInfo.GetValue(record);
                        if (dateValue is DateTime dateTime)
                        {
                            // 转换为带时分秒的字符串
                            value = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            value = dateValue;
                        }
                    }
                    else if (column.PropertyInfo != null)
                    {
                        value = column.PropertyInfo.GetValue(record);
                    }

                    // 格式化值
                    value = FormatValueForExport(value);
                    rowData[column.DisplayName] = value;
                }

                exportData.Add(rowData);
                recordNumber++;
            }

            return exportData;
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

            return value;
        }

        /// <summary>
        /// 格式化值用于导出
        /// </summary>
        private object FormatValueForExport(object value)
        {
            if (value == null) return "";

            if (value is double doubleValue)
            {
                return doubleValue;
            }
            else if (value is DateTime dateTimeValue)
            {
                return dateTimeValue;
            }

            return value;
        }

        /// <summary>
        /// 清空DataGridView行（保留列头）
        /// </summary>
        private void ClearDataGridViewRows()
        {
            dgvRecord.Rows.Clear();
        }

        /// <summary>
        /// 清空DataGridView（包括列头）- 用于特殊情况
        /// </summary>
        private void ClearDataGridView()
        {
            dgvRecord.Rows.Clear();
            dgvRecord.Columns.Clear();
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

        #region 磨合试验数据改造

        // ── 1. 新增手动模式列定义（与现有 _columnDefinitions 并列存在）──────────
        // 字段名对齐 ManualRecordPara 实际属性（含直接继承自 IRecordData 基类的字段）
        private readonly List<ColumnDefinition> _manualColumnDefinitions = new List<ColumnDefinition>
        {
            new ColumnDefinition("Index",          "序号"),
            new ColumnDefinition("RecordDataTime", "采集时间"),
            new ColumnDefinition("TestHour",       "时"),
            new ColumnDefinition("TestMinute",     "分"),
            new ColumnDefinition("NominalRPM",     "名义转速 rpm"),
            new ColumnDefinition("RPM",            "实测转速 rpm"),
            new ColumnDefinition("NominalPower",   "名义功率 kW"),
            new ColumnDefinition("Power",          "实测功率 kW"),
            new ColumnDefinition("ECOQuantity",    "测油耗量 g"),
            new ColumnDefinition("ECORate",        "燃油消耗率 g/kWh"),
            new ColumnDefinition("PFuelInlet",          "燃油进口压力 kPa"),
            new ColumnDefinition("LPressureOut",        "中冷泵出口压力 kPa"),
            new ColumnDefinition("HPressureOut",        "高温泵出口压力 kPa"),
            new ColumnDefinition("EOPressure1",         "机油泵出口压力 kPa"),
            new ColumnDefinition("POilInlet",           "机油进口压力 kPa"),
            new ColumnDefinition("EOPressure2",         "机油总管末端压力 kPa"),
            new ColumnDefinition("PTurboOilFront",      "前增压器油压 kPa"),
            new ColumnDefinition("PTurboOilAfter",      "后增压器油压 kPa"),
            new ColumnDefinition("HeatExchangerTempIn",  "机油进口温度 ℃"),
            new ColumnDefinition("HeatExchangerTempOut", "机油出口温度 ℃"),
            new ColumnDefinition("HWaterTempIn",   "高温水进口温度 ℃"),
            new ColumnDefinition("HWaterTempOut",  "高温水出口温度 ℃"),
            new ColumnDefinition("LWaterTempIn",   "中冷水进口温度 ℃"),
            new ColumnDefinition("LWaterTempOut",  "中冷水出口温度 ℃"),
            new ColumnDefinition("FrontTurbochargerRPM", "前增压器转速 rpm"),
            new ColumnDefinition("AfterTurbochargerRPM", "后增压器转速 rpm"),
            new ColumnDefinition("Remark",         "备注"),
        };

        private List<ColumnDefinition> CurrentColumnDefinitions =>
            rdoManual.Checked ? _manualColumnDefinitions : _columnDefinitions;


        // 单位转换（要求①）
        // 模板压力单位：表1(列11-18) = ×0.1MPa；表2 曲轴箱(列32) = ×10Pa；
        // 表2 中冷器前/后(列33-36) = ×100Pa；表2 压气机前/涡轮后/涡轮前 = Pa。
        // 例如 AI2Grp 原始返回值单位为 kPa（与项目里大多数压力字段一致）。
        private const double FACTOR_0P1MPA = 100.0;   // 原始kPa ÷ 100  → ×0.1MPa单位下数值
        private const double FACTOR_10PA = 0.01;    // 原始kPa ÷ 0.01 → ×10Pa 单位下数值
        private const double FACTOR_100PA = 0.1;     // 原始kPa ÷ 0.1  → ×100Pa单位下数值

        private static double ToUnit_0p1MPa(double rawKPa) => rawKPa / FACTOR_0P1MPA;
        private static double ToUnit_10Pa(double rawKPa) => rawKPa / FACTOR_10PA;
        private static double ToUnit_100Pa(double rawKPa) => rawKPa / FACTOR_100PA;

        // 手动模式导出：按模板填充 .xls
        private void ExportManualToExcel()
        {
            string templatePath = System.IO.Path.Combine(
                Application.StartupPath, "Templates", "ManualReport.xls");

            if (!System.IO.File.Exists(templatePath))
            {
                Var.MsgBoxWarn(this, $"模板文件不存在：{templatePath}\n请将出厂记录模板放至 Templates 目录。");
                return;
            }

            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "Excel 97-2003 (*.xls)|*.xls";
                dlg.FileName = $"出厂试验记录_{cboModel.Text}_{txtNumber.Text}_{DateTime.Now:yyyyMMdd_HHmmss}.xls";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                try
                {
                    System.IO.File.Copy(templatePath, dlg.FileName, overwrite: true);
                    FillManualExcelTemplate(dlg.FileName);
                    Var.MsgBoxInfo(this, $"导出成功：{dlg.FileName}");

                    if (Var.MsgBoxYesNo(this, "是否立即打开文件？"))
                        System.Diagnostics.Process.Start(dlg.FileName);
                }
                catch (Exception ex)
                {
                    Var.MsgBoxWarn(this, "导出失败：" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 使用 NPOI 按固定坐标将 ManualRecordPara 列表填入 .xls 模板。
        /// 压力字段按模板标注单位换算后写入。
        /// 各缸爆发压力：不写值，保留空白，由人工打印后手动填写。
        /// </summary>
        private void FillManualExcelTemplate(string filePath)
        {
            var records = _allData.Cast<ManualRecordPara>().ToList();

            using (var fs = new System.IO.FileStream(filePath,
                System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite))
            {
                var wb = new NPOI.HSSF.UserModel.HSSFWorkbook(fs);
                var sh = wb.GetSheetAt(0);

                // ── 表头区 ──────────────────────────────────────────────────
                SetCell(sh, 2, 2, DateTime.Now.ToString("yyyy-MM-dd"));   // 试验日期
                SetCell(sh, 2, 9, txtNumber.Text);                         // 柴油机出厂编号

                // ── 表1 主数据区（模板第12行起，0-based行索引11）─────────────
                int dataStartRow1 = 11;
                for (int i = 0; i < records.Count; i++)
                {
                    var rec = records[i];
                    int rowIdx = dataStartRow1 + i;
                    if (i >= 14) sh.ShiftRows(rowIdx, sh.LastRowNum, 1);

                    var row = sh.GetRow(rowIdx) ?? sh.CreateRow(rowIdx);

                    SetCell(row, 0, (int)rec.Index + 1);
                    SetCell(row, 1, rec.TestHour);
                    SetCell(row, 3, rec.TestMinute);
                    SetCell(row, 4, rec.NominalRPM);
                    SetCell(row, 5, rec.RPM);
                    SetCell(row, 6, rec.NominalPower);
                    SetCell(row, 7, rec.Power);
                    SetCell(row, 8, rec.ECOQuantity);
                    SetCell(row, 10, rec.ECORate);

                    // 表1压力列统一 ×0.1MPa
                    SetCell(row, 11, ToUnit_0p1MPa(rec.PFuelInlet));
                    SetCell(row, 12, ToUnit_0p1MPa(rec.LPressureOut));
                    SetCell(row, 13, ToUnit_0p1MPa(rec.HPressureOut));
                    SetCell(row, 14, ToUnit_0p1MPa(rec.EOPressure1));
                    SetCell(row, 15, ToUnit_0p1MPa(rec.POilInlet));
                    SetCell(row, 16, ToUnit_0p1MPa(rec.EOPressure2));
                    SetCell(row, 17, ToUnit_0p1MPa(rec.PTurboOilFront));
                    SetCell(row, 18, ToUnit_0p1MPa(rec.PTurboOilAfter));

                    SetCell(row, 19, rec.HeatExchangerTempIn);
                    SetCell(row, 20, rec.HeatExchangerTempOut);
                    SetCell(row, 21, rec.HWaterTempIn);
                    SetCell(row, 22, rec.HWaterTempOut);
                    SetCell(row, 23, rec.LWaterTempIn);
                    SetCell(row, 24, rec.LWaterTempOut);
                    SetCell(row, 25, rec.FrontTurbochargerRPM);
                    SetCell(row, 26, rec.AfterTurbochargerRPM);
                    SetCell(row, 27, rec.Remark ?? "");
                }

                // ── 表2 增压器子表（模板第32行起）────────────────────────────
                int table1Extra = Math.Max(0, records.Count - 14);
                int dataStartRow2 = 31 + table1Extra;

                for (int i = 0; i < records.Count; i++)
                {
                    var rec = records[i];
                    int rowIdx = dataStartRow2 + i;
                    if (i >= 14) sh.ShiftRows(rowIdx, sh.LastRowNum, 1);

                    var row = sh.GetRow(rowIdx) ?? sh.CreateRow(rowIdx);

                    SetCell(row, 0, (int)rec.Index + 1);
                    SetCell(row, 1, rec.PCompressorFront);                  // Pa，原值直接写
                    SetCell(row, 2, rec.PCompressorAfter);                  // Pa
                    SetCell(row, 3, ToUnit_10Pa(rec.PCrankcase));            // ×10Pa
                    SetCell(row, 4, ToUnit_100Pa(rec.PInterCoolerFrontFront)); // ×100Pa
                    SetCell(row, 5, ToUnit_100Pa(rec.PInterCoolerFrontAfter)); // ×100Pa
                    SetCell(row, 6, rec.FrontTurbochargerPressureIn2);        // Pa（涡轮前）
                    SetCell(row, 7, rec.TInterCoolerFrontFront);
                    SetCell(row, 8, rec.TInterCoolerFrontAfter);

                    SetCell(row, 9, rec.EGTempA1);
                    SetCell(row, 10, rec.EGTempA2);
                    SetCell(row, 11, rec.EGTempA3);
                    SetCell(row, 12, rec.EGTempA4);
                    SetCell(row, 13, rec.EGTempA5);
                    SetCell(row, 14, rec.EGTempA6);

                    SetCell(row, 17, rec.FrontTurbochargerTempIn);
                    SetCell(row, 18, rec.AfterTurbochargerTempIn);

                    // ── 要求③：各缸爆发压力不写值，保留空白，留给人工打印后手填 ──
                    // 故意不调用 SetCell，保持模板原状空白。
                }

                fs.Seek(0, System.IO.SeekOrigin.Begin);
                wb.Write(fs);
            }
        }

        // ── NPOI 辅助方法 ──────────────────────────────────────────────────────
        private static void SetCell(NPOI.SS.UserModel.ISheet sh, int row, int col, object value)
        {
            var r = sh.GetRow(row) ?? sh.CreateRow(row);
            SetCell(r, col, value);
        }

        private static void SetCell(NPOI.SS.UserModel.IRow row, int col, object value)
        {
            var cell = row.GetCell(col) ?? row.CreateCell(col);
            if (value is double d) cell.SetCellValue(d);
            else if (value is int i) cell.SetCellValue(i);
            else cell.SetCellValue(value?.ToString() ?? "");
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainUI.BLL;
using MainUI.FSql;

namespace MainUI.Fault
{
    public partial class frmFaultManager : Form
    {
        private readonly FaultEventService faultEventService = new FaultEventService();

        public frmFaultManager()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        private void frmDataManager_Load(object sender, EventArgs e)
        {
            Init();
            LoadData();

            // 订阅CellFormatting事件
            uiDataGridView1.CellFormatting += UiDataGridView1_CellFormatting;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            try
            {
                cboType.Text = "全部";
                // 设置默认时间范围：近7天
                dtpStartEnd.Value = DateTime.Now;
                dtpStartBig.Value = DateTime.Now.AddDays(-7);

            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, ex.Message);
            }
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        private void LoadData()
        {
            try
            {
                // 获取查询条件
                string type = cboType.Text.Trim();
                string description = txtFaultDesc.Text.Trim();
                DateTime startTime = dtpStartBig.Value;
                DateTime endTime = dtpStartEnd.Value;

                // 确保结束时间不小于开始时间
                if (startTime > endTime)
                {
                    Var.MsgBoxWarn(this, "开始时间不能大于结束时间！");
                    return;
                }

                // 获取故障数据
                DataTable dt = faultEventService.GetFaultEventRecordByCondition(
                    startTime,
                    endTime,
                    type,
                    description);

                // 绑定数据到表格
                BindDataToGridView(dt);
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, $"加载数据失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 将数据绑定到DataGridView
        /// </summary>
        private void BindDataToGridView(DataTable dt)
        {
            try
            {
                // 设置数据源
                uiDataGridView1.DataSource = dt;

                // 显示记录数
                UpdateRecordCount(dt.Rows.Count);
            }
            catch (Exception ex)
            {
                throw new Exception($"绑定数据到表格失败：{ex.Message}");
            }
        }

        private void UiDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 确保是单元格而不是表头
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewColumn column = uiDataGridView1.Columns[e.ColumnIndex];
                if (column.Name == "colStatus")
                {
                    if (e.Value != null)
                    {
                        // 将状态值转换为显示文本
                        if (int.TryParse(e.Value.ToString(), out int status))
                        {
                            e.Value = status == 0 ? "未复位" : "已复位";
                            e.FormattingApplied = true; // 表示已经处理过格式化
                        }
                    }
                }
                else if (column.Name == "colFaultType")
                {
                    if (e.Value != null)
                    {
                        string faultTypeValue = e.Value.ToString();
                        e.Value = GetFaultTypeDisplay(faultTypeValue);
                        e.FormattingApplied = true;
                    }
                }
                else if (column.Name == "created_at")
                {
                    if (e.Value != null)
                    {
                        string faultTypeValue = e.Value.ToString();
                        e.Value = GetFaultTypeDisplay(faultTypeValue);
                        e.FormattingApplied = true;
                    }
                }
                else if (column.Name == "colSeverity")
                {
                    if (e.Value != null)
                    {
                        string faultTypeValue = e.Value.ToString();
                        e.Value = GetFaultSeverityDisplay(faultTypeValue);
                        e.FormattingApplied = true;
                    }
                }
                else if (column.Name == "colOccurTime")
                {
                    if (e.Value != null && e.Value is DateTime)
                    {
                        DateTime occurTime = (DateTime)e.Value;
                        e.Value = occurTime.ToString("yyyy-MM-dd HH:mm:ss");
                        e.FormattingApplied = true;
                    }
                }
                else if (column.Name == "colResetTime")
                {
                    if (e.Value != null && e.Value != DBNull.Value)
                    {
                        if (DateTime.TryParse(e.Value.ToString(), out DateTime resetTime))
                        {
                            e.Value = resetTime.ToString("yyyy-MM-dd HH:mm:ss");
                            e.FormattingApplied = true;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 根据故障类型值获取显示文本
        /// </summary>
        private string GetFaultTypeDisplay(string faultTypeValue)
        {
            if (int.TryParse(faultTypeValue, out int type))
            {
                switch (type)
                {
                    case 0: return "通讯";
                    case 1: return "OPC检测";
                    case 2: return "逻辑判断";
                    case 3: return "发动机控制器";
                    default: return "未知类型";
                }
            }
            return faultTypeValue;
        }

        /// <summary>
        /// 根据故障类型值获取显示文本
        /// </summary>
        private string GetFaultSeverityDisplay(string faultTypeValue)
        {
            if (int.TryParse(faultTypeValue, out int type))
            {
                switch (type)
                {
                    case 1: return "报警故障";
                    case 2: return "降载故障";
                    case 3: return "停机故障";
                    case 4: return "报警故障(提示)";
                    default: return "未知类型";
                }
            }
            return faultTypeValue;
        }

        /// <summary>
        /// 更新记录数显示
        /// </summary>
        private void UpdateRecordCount(int count)
        {
            // 这里可以添加状态栏或标签显示记录数
            lblRecordCount.Text = $"故障记录： 共 {count} 条记录";
        }


        /// <summary>
        /// 搜索
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 退出
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            // 释放资源
            if (uiDataGridView1.DataSource is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
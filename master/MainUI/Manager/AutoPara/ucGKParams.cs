using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainUI.BLL;
using RW.UI.Manager;
using MainUI.Config;
using Sunny.UI;
using MainUI.Global;
using static MainUI.Config.PubConfig;
using RW.UI.Controls;

namespace MainUI.Procedure
{
    public partial class ucGKParams : ucBaseManagerUI
    {
        // 全局常量定义
        private const double MAX_TORQUE = 60000; // 最大扭矩
        private const double MAX_RPM = 1100; // 最大转速

        // 工况配置参数
        GKConfig gkConfig { get; set; }

        // 存储复制的数据
        private List<GKData> copiedDataList = new List<GKData>();

        private string _key = "360h";
        /// <summary>
        /// 配置文件标识
        /// </summary>
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }


        public ucGKParams()
        {
            InitializeComponent();
            dgvMH.AutoGenerateColumns = false;
        }

        private void ucModelManage_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;

            try
            {
                LoadGridView();
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, $"工况表界面初始化失败；原因：{ex.Message}");
            }
        }

        /// <summary>
        /// 加载表格参数
        /// </summary>
        private void LoadGridView()
        {
            try
            {
                // 加载工况配置参数
                gkConfig = new GKConfig(Key);

                this.lblTitle.Text = Key + " 工况表";

                this.dgvMH.Rows.Clear();
                foreach (var item in gkConfig.DurabilityDatas)
                {
                    int rowIndex = this.dgvMH.Rows.Add(
                        item.Index,
                        item.GKNo,
                        item.Speed,
                        item.Torque,
                        item.ExcitationCurrent,
                        item.Power);
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
            if (gkConfig.DurabilityDatas.Count == 0)
                return 1;

            return gkConfig.DurabilityDatas.Max(d => d.Index) + 1;
        }

        /// <summary>
        /// 保存工况数据
        /// </summary>
        private void SaveGKData()
        {
            try
            {
                gkConfig.DurabilityDatas.Clear();

                for (int i = 0; i < dgvMH.Rows.Count; i++)
                {
                    DataGridViewRow row = dgvMH.Rows[i];

                    // 跳过空行（新行）
                    if (row.IsNewRow) continue;

                    // 检查必填字段
                    string gkCode = row.Cells["GK"].Value?.ToString()?.Trim();
                    string speed = row.Cells["RPM"].Value?.ToString()?.Trim();
                    string torque = row.Cells["Torque"].Value?.ToString()?.Trim();
                    string excitationCurrent = row.Cells["ExcitationCurrent"].Value?.ToString()?.Trim();
                    string power = row.Cells["Power"].Value?.ToString()?.Trim();

                    // 验证必填字段
                    if (string.IsNullOrEmpty(gkCode))
                    {
                        Var.MsgBoxWarn(this, $"第{i + 1}行：工况编号不能为空");
                        gkCode = "0";
                    }

                    // 验证数值字段
                    if (!IsValidNumber(speed))
                    {
                        Var.MsgBoxWarn(this, $"第{i + 1}行：转速值必须为有效数字");
                        speed = "0";
                    }
                    if (!IsValidNumber(torque))
                    {
                        Var.MsgBoxWarn(this, $"第{i + 1}行：扭矩值必须为有效数字");
                        torque = "0";
                    }
                    if (!IsValidNumber(excitationCurrent))
                    {
                        Var.MsgBoxWarn(this, $"第{i + 1}行：励磁电流必须为有效数字");
                        excitationCurrent = "0";
                    }
                    if (!IsValidNumber(power))
                    {
                        Var.MsgBoxWarn(this, $"第{i + 1}行：功率必须为有效数字");
                        power = "0";
                    }

                    // 验证数值范围
                    double speedValue = double.Parse(speed);
                    double torqueValue = double.Parse(torque);

                    if (speedValue < 0 || speedValue > MAX_RPM)
                    {
                        Var.MsgBoxWarn(this, $"第{i + 1}行：转速值必须在0-{MAX_RPM}之间");
                        speed = "0";
                    }
                    if (torqueValue < 0 || torqueValue > MAX_TORQUE)
                    {
                        Var.MsgBoxWarn(this, $"第{i + 1}行：扭矩值必须在0-{MAX_TORQUE}之间");
                        torque = "0";
                    }

                    GKData data = new GKData
                    {
                        Index = i + 1, // 重新排序
                        GKNo = gkCode,
                        Speed = speedValue,
                        Torque = torqueValue,
                        ExcitationCurrent = double.Parse(excitationCurrent),
                        Power = double.Parse(power)
                    };

                    gkConfig.DurabilityDatas.Add(data);
                }

                gkConfig.Save();
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "保存失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 表格单元格开始编辑
        /// </summary>
        private void dgvMH_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // 不允许编辑序号列
            if (e.ColumnIndex == dgvMH.Columns["StepIndex100"].Index)
            {
                e.Cancel = true;
                return;
            }
        }

        /// <summary>
        /// 表格单元格内容点击事件
        /// </summary>
        private void dgvMH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 如果点击的是删除按钮列（第6列，索引为6）
            if (e.RowIndex >= 0 && e.ColumnIndex == 6)
            {
                // 防止事件冒泡
                dgvMH.EndEdit();

                // 调用删除方法
                DeleteGKDataByRowIndex(e.RowIndex);
            }
        }

        /// <summary>
        /// 根据行索引删除数据
        /// </summary>
        private void DeleteGKDataByRowIndex(int rowIndex)
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
                SaveGKData();

                // 如果删除后还有行，自动选中上一行或第一行
                if (dgvMH.Rows.Count > 0)
                {
                    int selectRowIndex = rowIndex - 1;
                    if (selectRowIndex < 0) selectRowIndex = 0;

                    if (selectRowIndex < dgvMH.Rows.Count)
                    {
                        dgvMH.CurrentCell = dgvMH.Rows[selectRowIndex].Cells[1]; // 选中工况编号列
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
                    SaveGKData();
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
        private void dgvMH_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // 可以在这里添加额外的处理逻辑
        }

        /// <summary>
        /// 表格单元格验证事件
        /// </summary>
        private void dgvMH_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // 可以在这里添加单元格验证逻辑
        }

        /// <summary>
        /// 表格数据错误事件
        /// </summary>
        private void dgvMH_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
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
                if (c >= 0x4E00 && c <= 0x9FA5)
                {
                    return false;
                }
            }

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

            // 允许小数点
            if (e.KeyChar == '.')
            {
                TextBox textBox = sender as TextBox;
                if (textBox != null && textBox.Text.Contains("."))
                {
                    e.Handled = true;
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
        /// 新增工况步骤
        /// </summary>
        private void btnAdd100hStep_Click(object sender, EventArgs e)
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
                    GKData newStep = new GKData
                    {
                        Index = currentMaxIndex + i,
                        GKNo = "0",
                        Speed = 0,
                        Torque = 0,
                        ExcitationCurrent = 0,
                        Power = 0
                    };

                    gkConfig.DurabilityDatas.Add(newStep);
                    gkConfig.Save();
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
        /// 从工况表进行快速导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelIn_Click(object sender, EventArgs e)
        {

        }

    }
}
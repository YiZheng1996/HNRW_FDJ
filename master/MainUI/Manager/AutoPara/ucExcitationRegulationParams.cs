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
    public partial class ucExcitationRegulationParams : ucBaseManagerUI
    {

        ExcitationRegulationConfig excitationRegulation = null;

        public string Model { get; set; }

        public ucExcitationRegulationParams()
        {
            InitializeComponent();
        }

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
            try
            {
                if (string.IsNullOrEmpty(Model))
                {
                    return;
                }

                excitationRegulation = new ExcitationRegulationConfig(Model);

                // 加功率
                // 一阶段
                this.nudCurrentOne.Value = (decimal)excitationRegulation.Phase1Current;
                this.nudPhase1Rise1.Value = (decimal)excitationRegulation.Phase1Rise1;
                this.nudPhase1Rise2.Value = (decimal)excitationRegulation.Phase1Rise2;
                this.nudPhase1Rise3.Value = (decimal)excitationRegulation.Phase1Rise3;
                this.nudPhase1Rise4.Value = (decimal)excitationRegulation.Phase1Rise4;
                this.nudPhase1Rise5.Value = (decimal)excitationRegulation.Phase1Rise5;

                // 二阶段
                this.nudCurrentTwo.Value = (decimal)excitationRegulation.Phase2Current;
                this.nudPhase2Rise1.Value = (decimal)excitationRegulation.Phase2Rise1;
                this.nudPhase2Rise2.Value = (decimal)excitationRegulation.Phase2Rise2;
                this.nudPhase2Rise3.Value = (decimal)excitationRegulation.Phase2Rise3;
                this.nudPhase2Rise4.Value = (decimal)excitationRegulation.Phase2Rise4;
                this.nudPhase2Rise5.Value = (decimal)excitationRegulation.Phase2Rise5;
                this.nudPhase2Rise6.Value = (decimal)excitationRegulation.Phase2Rise6;

                // 降功率
                this.nudPhase3Rise1.Value = (decimal)excitationRegulation.Phase3Rise1;
                this.nudPhase3Rise2.Value = (decimal)excitationRegulation.Phase3Rise2;
                this.nudPhase3Rise3.Value = (decimal)excitationRegulation.Phase3Rise3;
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "加载数据失败：" + ex.Message);
            }
        }

        private void ucTestParams_Load(object sender, EventArgs e)
        {

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


        private void dgvMH_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // 处理数据验证错误
            if (e.Exception != null)
            {
                Var.MsgBoxWarn(this, "数据输入错误：" + e.Exception.Message);
                e.ThrowException = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            excitationRegulation.Phase1Current = this.nudCurrentOne.Value.ToDouble();
            excitationRegulation.Phase1Rise1 = this.nudPhase1Rise1.Value.ToDouble();
            excitationRegulation.Phase1Rise2 = this.nudPhase1Rise2.Value.ToDouble();
            excitationRegulation.Phase1Rise3 = this.nudPhase1Rise3.Value.ToDouble();
            excitationRegulation.Phase1Rise4 = this.nudPhase1Rise4.Value.ToDouble();
            excitationRegulation.Phase1Rise5 = this.nudPhase1Rise5.Value.ToDouble();

            // 二阶段
            excitationRegulation.Phase2Current = this.nudCurrentTwo.Value.ToDouble();
            excitationRegulation.Phase2Rise1 = this.nudPhase2Rise1.Value.ToDouble();
            excitationRegulation.Phase2Rise2 = this.nudPhase2Rise2.Value.ToDouble();
            excitationRegulation.Phase2Rise3 = this.nudPhase2Rise3.Value.ToDouble();
            excitationRegulation.Phase2Rise4 = this.nudPhase2Rise4.Value.ToDouble();
            excitationRegulation.Phase2Rise5 = this.nudPhase2Rise5.Value.ToDouble();
            excitationRegulation.Phase2Rise6 = this.nudPhase2Rise6.Value.ToDouble();

            // 降功率
            excitationRegulation.Phase3Rise1 = this.nudPhase3Rise1.Value.ToDouble();
            excitationRegulation.Phase3Rise2 = this.nudPhase3Rise2.Value.ToDouble();
            excitationRegulation.Phase3Rise3 = this.nudPhase3Rise3.Value.ToDouble();
            excitationRegulation.Save();
        }

        /// <summary>
        /// 设置为默认速率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDefault_Click(object sender, EventArgs e)
        {
            var msg = Var.MsgBoxYesNo(this, "确认要设置为默认数值吗？");
            if (!msg) return;

            excitationRegulation.Phase1Current = 150;
            excitationRegulation.Phase1Rise1 = 35;
            excitationRegulation.Phase1Rise2 = 35;
            excitationRegulation.Phase1Rise3 = 30;
            excitationRegulation.Phase1Rise4 = 30;
            excitationRegulation.Phase1Rise5 = 35;

            // 二阶段
            excitationRegulation.Phase2Current = 35;
            excitationRegulation.Phase2Rise1 = 40;
            excitationRegulation.Phase2Rise2 = 35;
            excitationRegulation.Phase2Rise3 = 35;
            excitationRegulation.Phase2Rise4 = 35;
            excitationRegulation.Phase2Rise5 = 35;
            excitationRegulation.Phase2Rise6 = 30;

            // 降功率
            excitationRegulation.Phase3Rise1 = 20;
            excitationRegulation.Phase3Rise2 = 30;
            excitationRegulation.Phase3Rise3 = 30;
        }
    }
}


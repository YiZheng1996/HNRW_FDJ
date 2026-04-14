using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MainUI.Widgets;
using MainUI.Config;

namespace MainUI.Manager
{
    public partial class ucDashboardPara : UserControl
    {
        DashboardConfig dashboardConfig = null;

        public ucDashboardPara()
        {
            InitializeComponent();
        }


        private void ucFaultPara_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 刷新表格数据
        /// </summary>
        public void LoadConfig()
        {
            try
            {
                this.dgvMH.Rows.Clear();

                // 加载型号
                dashboardConfig = new DashboardConfig(txtModel.Text);
                dashboardConfig.Load();

                foreach (var item in dashboardConfig.dashboardDatas)
                {
                    var scarmTD = item.ScarmTodo == 0 ? "默认报警" : "停机";
                    this.dgvMH.Rows.Add(item.Point, item.Name, item.Unit, item.MinVal, item.MaxVal, item.ScarmVal, scarmTD);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string key = null;
                frmDashBoard edit = new frmDashBoard(dashboardConfig,txtModel.Text, key);
                DialogResult dr = edit.ShowDialog();
                if (dr == DialogResult.Cancel)
                    return;

                Var.MsgBoxInfo(this, "新增仪表盘参数编辑成功。");
                LoadConfig();
            }
            catch (Exception ex)
            {
                string err = "新增仪表盘参数出现错误；原因：" + ex.Message;
                Var.MsgBoxWarn(this, err);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var dr = Var.MsgBoxYesNo(this ,"确定删除此参数吗？");
            if (!dr)
                return;
            if (this.dgvMH.CurrentRow == null)
            {
                Var.MsgBoxInfo(this, "请选中要删除的数据行！");
                return;
            }

            int removeIndex = dashboardConfig.dashboardDatas.Count;
            for (int i = 0; i < dashboardConfig.dashboardDatas.Count; i++)
            {
                // 如果名称一致就可以删除
                if (dashboardConfig.dashboardDatas[i].Point == this.dgvMH.CurrentRow.Cells[0].FormattedValue)
                {
                    removeIndex = i;
                    continue;
                }
            }

            // 没找到对应数据
            if (removeIndex == dashboardConfig.dashboardDatas.Count)
            {
                Var.MsgBoxSuccess(this, "未找到要删除的数据!");
                return;
            }

            dashboardConfig.dashboardDatas.Remove(dashboardConfig.dashboardDatas[removeIndex]);
            dashboardConfig.Save();
            Var.MsgBoxSuccess(this, "删除成功!");

            LoadConfig();
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string key = null;
                if (this.dgvMH.SelectedRows.Count > 0)
                {
                    key = this.dgvMH.SelectedRows[0].Cells[0].Value.ToString();
                }
                if (string.IsNullOrEmpty(key))
                {
                    Var.MsgBoxWarn(this, "请选择一行数据进行编辑！");
                    return;
                }
                frmDashBoard edit = new frmDashBoard(dashboardConfig, txtModel.Text, key);
                DialogResult dr = edit.ShowDialog();
                if (dr == DialogResult.Cancel)
                    return;

                Var.MsgBoxInfo(this, "编辑仪表盘参数编辑成功。");
                LoadConfig();
            }
            catch (Exception ex)
            {
                string err = "编辑仪表盘参数出现错误；原因：" + ex.Message;
                Var.MsgBoxWarn(this, err);
            }
        }


        /// <summary>
        /// 型号选择后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGet_Click(object sender, EventArgs e)
        {
            frmSpec fs = new frmSpec();
            fs.ShowDialog();
            txtType.Text = Common.mTestViewModel.ModelType;
            txtModel.Text = Common.mTestViewModel.ModelName;
            LoadConfig();
        }
    }
}

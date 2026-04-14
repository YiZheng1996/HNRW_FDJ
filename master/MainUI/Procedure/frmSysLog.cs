
using MainUI.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using RW.UI.User;

namespace MainUI
{
    public partial class frmSysLog : Form
    {
        public frmSysLog()
        {
            InitializeComponent();
        }


        private void frmSysLog_Load(object sender, EventArgs e)
        {
            try
            {
                BindTester();
                InitDate();
                EnableDgv();

                btnSearch_Click(null, null);
                //if (Var.UserPrivate != 0)
                //    btnDelete.Visible = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show("系统日志界面初始化有误。" + ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void InitDate()
        {
            dtpTo.Value = System.DateTime.Now;
            dtpFrom.Value = Convert.ToDateTime( System.DateTime.Now.ToString("yyyy-MM-dd 00:00"));
        }

        private void EnableDgv()
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].ReadOnly = true;    //内容为只读
               
            }

            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;  // 列标题居中
            dataGridView1.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;  // 每行内容居中
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;   // 列标题高度固定
            dataGridView1.ColumnHeadersHeight = 40;   // 高度固定为30 

            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption; // 奇数行设置背景色
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            
        }
        //

        private void BindTester()

        {
            UserBLL userBll = new UserBLL();
            DataSet dtUser = userBll.GetSortedList();
            //DataSet dtUser = userBll.GetSortedList();

            if (dtUser != null)
            {
                DataRow row = dtUser.Tables[0].NewRow();
                row["ID"] = 0;
                row["Username"] = "全部";
                dtUser.Tables[0].Rows.InsertAt(row, 0);

                this.cboTester.DataSource = dtUser.Tables[0];
                this.cboTester.DisplayMember = "Username";
                this.cboTester.ValueMember = "ID";
            }
        }

        SysLogBLL syslog = new SysLogBLL();
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string con = "";
            try
            {
                if (cboTester.Text != "全部")
                   con += " and a.UName='" + cboTester.Text+"'";
                // where += " and testTime>=#" + from.Date + "# and testTime<=#" + to.Date.AddDays(1) + "#";
                con += " and (a.LogTime >='" + dtpFrom.Value.ToString("yyyy-MM-dd HH:mm:00") + "' and a.LogTime<='" + dtpTo.Value.ToString("yyyy-MM-dd HH:mm:59") + "')";
                DataTable dt = syslog.GetByCondition(con);
                dataGridView1.DataSource = dt;

                int cnt = 0;
                if (dt != null)
                    cnt = dt.Rows.Count;
                grpRecord.Text = "记录列表（共" + cnt + "条）";

                ColumnsSize();
                EnableDgv();
            }
            catch (Exception ex)
            {
                string err = "搜索系统日志条件有误；具体原因：" + ex.Message;
                MessageBox.Show(err, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ColumnsSize()
        {
            dataGridView1.Columns[0].Width = 10;
            dataGridView1.Columns[0].Visible =false;

            dataGridView1.Columns[1].Width = 30;
            dataGridView1.Columns[2].Width = 50;
            

            dataGridView1.Columns[3].Width = 500;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1) return;
            if (dataGridView1.SelectedRows.Count < 1) return;

            int cnt = dataGridView1.SelectedRows.Count;
            string tip = "确定要删除选择的"+cnt+"条记录吗？";
            if (MessageBox.Show(tip, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;

            try
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[i].Cells["序号"].Value);

                    syslog.Delete(id);
                }
                btnSearch_Click(null, null);
            }
            catch (Exception ex)
            {
                string err ="删除失败。原因：" + ex.Message;
                MessageBox.Show(err);
            }
        }
    }
}

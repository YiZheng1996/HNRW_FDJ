using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using RW.UI.Model;
using RW.Data;
//using RW.UI.BLL; //user.db
using MainUI.BLL;  // //ZLCARD.db

namespace MainUI.Procedure.User
{
    public partial class ucUserManager : UserControl
    {
        public ucUserManager()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        private void ucUserManager_Load(object sender, EventArgs e)
        {
            this.DataBind();
        }

        UserBLL userBll = new UserBLL();


        //数据绑定
        private void DataBind()
        {
            dataGridView1.DataSource = userBll.GetSortedList().Tables[0];
        }

        //添加用户按钮
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ShowEdit(null);
        }

        //点击编辑用户按钮
        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.CurrentRow;
            if (row != null)
            {
                ShowEdit(row);
            }

        }

        //双击编辑用户
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowEdit(this.dataGridView1.Rows[e.RowIndex]);
        }

        //删除用户按钮
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[dataGridView1.CurrentRow.Index];
            Delete(row);
            //foreach (DataGridViewRow row in rows)
            //{
            //    Delete(row);
            //}
            this.DataBind();
        }

        //上移按钮
        private void btnUp_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = this.dataGridView1.SelectedRows;
            if (rows.Count == 0)
            {
                return;
            }
            DataGridViewRow row = rows[0];
            int rowIndex = row.Index;
            if (row == null)
            {
                return;
            }
            if (row.Index == 0)
            {
                MessageBox.Show("已经是第一条记录了，无法上移。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            DataGridViewRow rowUp = this.dataGridView1.Rows[row.Index - 1];

            userBll.Move(rowUp.Cells["colID"].Value, rowUp.Cells["colSort"].Value, row.Cells["colID"].Value, row.Cells["colSort"].Value);

            this.DataBind();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Rows[rowIndex - 1].Selected = true;
        }

        //下移按钮
        private void btnBelow_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = this.dataGridView1.SelectedRows;
            if (rows.Count == 0)
            {
                return;
            }

            DataGridViewRow row = rows[0];
            int rowIndex = row.Index;
            if (row == null)
            {
                return;
            }
            if (row.Index == this.dataGridView1.Rows.Count - 1)
            {
                MessageBox.Show("已经是最后一条记录了，无法下移。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            DataGridViewRow rowDown = this.dataGridView1.Rows[row.Index + 1];

            userBll.Move(row.Cells["colID"].Value, row.Cells["colSort"].Value, rowDown.Cells["colID"].Value, rowDown.Cells["colSort"].Value);
            this.DataBind();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Rows[rowIndex + 1].Selected = true;
        }

        //显示编辑用户的窗口
        private void ShowEdit(DataGridViewRow row)
        {
            UserInfo user = new UserInfo();
            if (row != null)
            {
                user.ID = Convert.ToInt32(row.Cells["colID"].Value);
                user.Password = row.Cells["colPassword"].Value.ToString();
                user.Username = row.Cells["colUsername"].Value.ToString();
                user.Permission = row.Cells["colPermission"].Value.ToString();
                user.Sort = Convert.ToInt32(row.Cells["colSort"].Value);
                if (user.Username == "admin")
                {
                    MessageBox.Show("超级管理员无法编辑！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            frmUserEdit editUser = new frmUserEdit(user);
            if (editUser.ShowDialog() == DialogResult.OK)
            {
                this.DataBind();
            }
        }

        private void Delete(DataGridViewRow row)
        {
            string username = row.Cells["colUsername"].Value.ToString();
            if (username == "admin")
            {
                MessageBox.Show("超级管理员无法删除！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (username == RW.UI.RWUser.User.Username)
            {
                MessageBox.Show("当前用户无法删除", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("确定要删除吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int count = userBll.RemoveByUsername(username);
                if (count > 0)
                {
                    MessageBox.Show("删除成功！", "系统提示", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("删除失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}

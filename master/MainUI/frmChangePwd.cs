using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RW.Driver;
using RW.UI.Manager.User;
//using RW.UI.BLL;
using MainUI.BLL;

namespace MainUI
{
    public partial class frmChangePwd : Form
    {
        public frmChangePwd()
        {
            InitializeComponent();
        }


        private void frmChangePwd_Load(object sender, EventArgs e)
        {
            this.txtUsername.Text = RW.UI.RWUser.User.Username;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string password = this.txtPassword.Text;
            string newPwd1 = this.txtNewPwd1.Text;
            string newPwd2 = this.txtNewPwd2.Text;


            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("密码不能为空，请重新输入！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtPassword.Focus();
                return;
            }
            if (string.IsNullOrEmpty(newPwd1))
            {
                MessageBox.Show("新密码不能为空，请重新输入", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtNewPwd1.Focus();
                return;
            }
            if (string.IsNullOrEmpty(newPwd2))
            {
                MessageBox.Show("确认密码不能为空，请重新输入！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtNewPwd2.Focus();
                return;
            }
            if (newPwd1 != newPwd2)
            {
                MessageBox.Show("两次输入的密码不正确，请重新输入！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtNewPwd1.Focus();
                return;
            }
            if (password != RW.UI.RWUser.User.Password)
            {
                MessageBox.Show("原始密码不正确，请重新输入！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtPassword.Focus();
                return;
            }

            UserBLL bllUser = new UserBLL();
            if (bllUser.ChangePwd(RW.UI.RWUser.User.Username, newPwd1))
            {
                MessageBox.Show("密码修改成功，重新登录后即可生效！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RW.UI.RWUser.User.Password = newPwd1;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("修改失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
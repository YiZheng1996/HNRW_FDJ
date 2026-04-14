using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RW.UI.Model;
//using RW.UI.BLL;  //user.db
using MainUI.BLL;  //ZLCARD.db

namespace MainUI.Procedure.User
{
    public partial class frmUserEdit : Form
    {
        public frmUserEdit()
        {
            InitializeComponent();
            InitRadioButton();
        }

        private UserInfo User;
        public frmUserEdit(UserInfo user)
        {
            InitializeComponent();
            InitRadioButton();
            this.User = user;
            this.txtUserName.Text = user.Username;
            this.txtPassword.Text = user.Password;
            this.comboBox1.Text = user.Permission;
            string level = user.Permission;
            if (level != null)
                this.comboBox1.SelectedValue = level;
        }

        void InitRadioButton()
        {
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.ValueMember = "ID";
            this.comboBox1.DataSource = RW.UI.RWUser.Permissions;

        }
        UserBLL userBll = new UserBLL();
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (User == null)
                {
                    User = new UserInfo();
                }
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("请选择权限", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                User.Username = this.txtUserName.Text;
                User.Password = this.txtPassword.Text;
                User.Permission = this.comboBox1.Text;

                int count = userBll.Save(User);

                if (count > 0)
                {
                    MessageBox.Show("保存成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("保存失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("检测到非法字符，请正确录入信息", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RW.UI.Manager.User;
using RW.Driver;
//using RW.UI.BLL;  //acces
using RW.UI;
using RW.EventLog;
using System.Globalization;
using MainUI.Config;
using RW;
using MainUI.BLL;  //sqlite

namespace MainUI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        UserBLL bllUser = null;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        private void frmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
              //  Logo.Size = new Size(230, 106);
                lblSoftName.Text = Var.SoftName;
                bllUser = new UserBLL();
              
                txtPassword.Focus();

            }
            catch (Exception ex)
            {
                lblMessage.Text = "数据库连接失败" + ex.Message;
                lblMessage.Visible = true;
            }

            try
            {
                DataSet ds = bllUser.GetSortedList();
                this.cboUsername.DataSource = ds.Tables[0];
                this.cboUsername.DisplayMember = "Username";
                this.cboUsername.ValueMember = "ID";

                if (Var.SysConfig.LastLoginIndex != -1)
                    cboUsername.SelectedIndex = Var.SysConfig.LastLoginIndex;
            }
            catch (Exception)
            {
                lblMessage.Text = "获取用户信息失败";
                lblMessage.Visible = true;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string username = this.cboUsername.Text;
            string password = this.txtPassword.Text;
            if (string.IsNullOrEmpty(username))
            {
                lblMessage.Text = "请选择要登录的用户!";
                lblMessage.Visible = true;
                this.cboUsername.Focus();
                return;
            }
            if (string.IsNullOrEmpty(password))
            {

                lblMessage.Text = "密码不能为空，请重新输入!";
                lblMessage.Visible = true;
                this.txtPassword.Focus();
                return;
            }

            int id = Convert.ToInt32(this.cboUsername.SelectedValue);
            DataTable dsUser = bllUser.Get(id);
            if (dsUser.Rows.Count > 0)
            {
                if (dsUser.Rows[0]["password"].ToString() != password)
                {
                    lblMessage.Text = "密码错误，请重新输入!";
                    lblMessage.Visible = true;
                    this.txtPassword.Focus();
                    return;
                }
                else
                {
                    RWUser.User.InitUser(dsUser.Rows[0]);
                    Var.LogInfo("用户" + RWUser.User.Username + "登录。");
                    Var.SysConfig.LastLoginIndex = cboUsername.SelectedIndex;
                    Var.SysConfig.Save();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                lblMessage.Text = "未找到该用户!";
                lblMessage.Visible = true;
                return;
            }
        }

      
    }
}
using MainUI.BLL;
using MainUI.Global;
using RW.UI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MainUI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private UserBLL bllUser;
        private ModelBLL bllModel;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private void FrmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            lblSoftName.Text = Var.SoftName;

            // 用户列表
            try
            {
                bllUser = new UserBLL();
                DataSet ds = bllUser.GetSortedList();
                cboUsername.DataSource = ds.Tables[0];
                cboUsername.DisplayMember = "Username";
                cboUsername.ValueMember = "ID";

                if (Var.SysConfig.LastLoginIndex != -1)
                    cboUsername.SelectedIndex = Var.SysConfig.LastLoginIndex;
            }
            catch (Exception ex)
            {
                ShowError("获取用户信息失败：" + ex.Message);
            }

            // 型号列表
            try
            {
                bllModel = new ModelBLL();
                DataTable dt = bllModel.GetAllKindByCon("");
                cboModel.DataSource = dt;
                cboModel.DisplayMember = "Name";
                cboModel.ValueMember = "ID";
                cboModel.SelectedIndex = -1;

                if (!string.IsNullOrEmpty(Var.SysConfig.LastModel))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Name"].ToString() == Var.SysConfig.LastModel)
                        {
                            cboModel.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("获取型号信息失败：" + ex.Message);
            }

            // 试验类型
            cboTrialType.Items.Clear();
            cboTrialType.Items.Add("-- 请选择试验类型 --");
            cboTrialType.Items.Add(TrialTypeEnum.RoutineTest.DisplayName()); // index 1
            cboTrialType.Items.Add(TrialTypeEnum.TypeTest.DisplayName());    // index 2

            // 记录试验类型
            //int lastTrialIdx = Var.SysConfig.LastTrialType + 1;
            //cboTrialType.SelectedIndex = (lastTrialIdx >= 1 && lastTrialIdx <= 2)
            //    ? lastTrialIdx : 0;

            cboTrialType.SelectedIndex = 0;

            // 试验编号
            txtTestNo.Text = Var.SysConfig.TestNo ?? "";

            UpdateConfirmButton();
        }

        // 试验类型自绘：型式试验（index 2）红色加粗
        private void cboTrialType_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            e.DrawBackground();

            string text = cboTrialType.Items[e.Index].ToString();
            bool isTypeTest = (e.Index == 2);

            Color fore = isTypeTest
                ? Color.FromArgb(192, 57, 43)
                : ((e.State & DrawItemState.Selected) != 0
                    ? SystemColors.HighlightText
                    : SystemColors.WindowText);

            Font drawFont = isTypeTest
                ? new Font(e.Font, FontStyle.Bold)
                : e.Font;

            using (var brush = new SolidBrush(fore))
                e.Graphics.DrawString(text, drawFont, brush, e.Bounds.X + 4, e.Bounds.Y + 3);

            if (isTypeTest) drawFont.Dispose();
            e.DrawFocusRectangle();
        }

        // 任意字段变化 → 刷新确认按钮
        private void AnyField_Changed(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            UpdateConfirmButton();
        }

        private void UpdateConfirmButton()
        {
            bool ok = cboUsername.SelectedIndex >= 0
                   && !string.IsNullOrEmpty(txtPassword.Text)
                   && cboModel.SelectedIndex >= 0
                   && cboTrialType.SelectedIndex > 0
                   && !string.IsNullOrEmpty(txtTestNo.Text.Trim());
            btnSignIn.Enabled = ok;
        }

        // 确认登录
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboUsername.Text))
            { ShowError("请选择要登录的用户！"); cboUsername.Focus(); return; }

            if (string.IsNullOrEmpty(txtPassword.Text))
            { ShowError("密码不能为空，请重新输入！"); txtPassword.Focus(); return; }

            if (cboModel.SelectedIndex < 0)
            { ShowError("请选择发动机型号！"); cboModel.Focus(); return; }

            if (cboTrialType.SelectedIndex <= 0)
            { ShowError("请选择试验类型！"); cboTrialType.Focus(); return; }

            if (string.IsNullOrEmpty(txtTestNo.Text.Trim()))
            { ShowError("请输入发动机编号！"); txtTestNo.Focus(); return; }

            // 密码校验
            int uid = Convert.ToInt32(cboUsername.SelectedValue);
            var dtUser = bllUser.GetById(uid);
            if (dtUser.Rows.Count == 0)
            { ShowError("未找到该用户！"); return; }

            if (dtUser.Rows[0]["password"].ToString() != txtPassword.Text)
            {
                ShowError("密码错误，请重新输入！");
                txtPassword.SelectAll();
                txtPassword.Focus();
                return;
            }

            // 写入全局状态
            RWUser.User.InitUser(dtUser.Rows[0]);
            Var.LogInfo("用户 " + RWUser.User.Username + " 登录。");

            Var.SysConfig.LastLoginIndex = cboUsername.SelectedIndex;

            if (cboModel.SelectedItem is DataRowView drv)
            {
                Var.SysConfig.LastModel = drv["Name"] != null
                    ? drv["Name"].ToString() : "";
                Var.SysConfig.LastModelType =
                    (drv.DataView.Table.Columns.Contains("ModelType") && drv["ModelType"] != null)
                    ? drv["ModelType"].ToString() : "";
            }
            else
            {
                Var.SysConfig.LastModel = cboModel.Text;
                Var.SysConfig.LastModelType = "";
            }

            // 密码校验通过之后，写入 Var.SysConfig.TestNo 之前
            string newNo = txtTestNo.Text.Trim();
            bool hasOpenManualBatch = !string.IsNullOrEmpty(Var.SysConfig.ManualRecordMGid);
            bool numberChanged = hasOpenManualBatch
                               && !string.IsNullOrEmpty(Var.SysConfig.TestNo)
                               && Var.SysConfig.TestNo != newNo;

            if (numberChanged)
            {
                bool keepOld = Var.MsgBoxYesNo(this,
                    $"存在尚未开启新批次的手动记录（编号：{Var.SysConfig.TestNo}）。\n" +
                    $"本次登录填写的编号（{newNo}）与之不同。\n\n" +
                    "是否仍沿用旧编号继续该批次记录？\n" +
                    "选择\"否\"将按新编号开始新批次（旧批次数据不受影响，可在报表界面查看）。");

                if (keepOld)
                {
                    txtTestNo.Text = Var.SysConfig.TestNo; // 沿用旧编号，忽略这次的修改
                }
                else
                {
                    new MainUI.FSql.ManualRecordService().StartNewBatch(); // 清 MGid/Index，允许用新编号
                }
            }

            Var.SysConfig.LastTrialTypeEnum = (TrialTypeEnum)(cboTrialType.SelectedIndex - 1);
            Var.SysConfig.TestNo = txtTestNo.Text.Trim();
            Var.SysConfig.Save();

            Var.LogInfo(string.Format(
                "试验信息 — 型号: {0}，试验类型: {1}，试验编号: {2}",
                Var.SysConfig.LastModel,
                Var.SysConfig.LastTrialTypeEnum.DisplayName(),
                Var.SysConfig.TestNo));

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowError(string msg)
        {
            lblMessage.Text = msg;
            lblMessage.Visible = true;
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }
    }
}
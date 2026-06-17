using System;
using System.Data;
using System.Windows.Forms;
using MainUI.BLL;
using MainUI.Config;
using MainUI.Global;

namespace MainUI
{
    public partial class FrmStartupConfirm : Form
    {
        // 确认后的结果，供调用方读取
        public string SelectedModel { get; private set; }
        public string SelectedModelType { get; private set; }
        public TrialTypeEnum SelectedTrialType { get; private set; }
        public string TestNo { get; private set; }

        private static FrmStartupConfirm _instance;

        public static FrmStartupConfirm GetInstance()
        {
            if (_instance == null || _instance.IsDisposed)
                _instance = new FrmStartupConfirm();
            return _instance;
        }

        private ModelBLL _modelBll = new ModelBLL();

        public FrmStartupConfirm()
        {
            InitializeComponent();
        }

        private void FrmStartupConfirm_Load(object sender, EventArgs e)
        {
            // 型号下拉框：第一项加"请选择"
            DataTable dt = _modelBll.GetAllKind();
            DataRow placeholderRow = dt.NewRow();
            placeholderRow["Name"] = "-- 请选择型号 --";
            dt.Rows.InsertAt(placeholderRow, 0);
            cboModel.DisplayMember = "Name";
            cboModel.ValueMember = "Name";
            cboModel.DataSource = dt;
            cboModel.SelectedIndex = 0; // 默认显示"请选择"

            // 试验类型下拉框：第一项加"请选择"
            cboTrialType.Items.Clear();
            cboTrialType.Items.Add("-- 请选择试验类型 --"); // index 0 占位
            cboTrialType.Items.Add(TrialTypeEnum.RoutineTest.DisplayName()); // index 1
            cboTrialType.Items.Add(TrialTypeEnum.TypeTest.DisplayName());    // index 2
            cboTrialType.SelectedIndex = 0;

            txtTestNo.Text = string.Empty;
            btnConfirm.Enabled = false;
        }

        // 任意一项变化时校验是否可以确认
        private void ValidateConfirm()
        {
            // 型号选了真实项（index > 0）、试验类型选了真实项（index > 0）、编号非空
            btnConfirm.Enabled =
                cboModel.SelectedIndex > 0 &&
                cboTrialType.SelectedIndex > 0 &&
                !string.IsNullOrWhiteSpace(txtTestNo.Text);
        }

        private void CboModel_SelectedIndexChanged(object sender, EventArgs e)
            => ValidateConfirm();

        private void CboTrialType_SelectedIndexChanged(object sender, EventArgs e)
            => ValidateConfirm();

        private void TxtTestNo_TextChanged(object sender, EventArgs e)
            => ValidateConfirm();

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // 二次确认，防止误操作——尤其是试验类型
            string msg = $"请确认以下信息：\n\n" +
                         $"  产品型号：{cboModel.Text}\n\n" +
                         $"  试验类型：{cboTrialType.Text}\n\n" +
                         $"  试验编号：{txtTestNo.Text.Trim()}\n\n" +
                         $"确认后将下发对应参数，是否继续？";

            if (!Var.MsgBoxYesNo(this, msg))
                return;

            SelectedModel = cboModel.Text;
            SelectedModelType = (cboModel.SelectedValue as DataRowView)?["ModelType"]?.ToString() ?? "";
            SelectedTrialType = (TrialTypeEnum)(cboTrialType.SelectedIndex - 1);
            TestNo = txtTestNo.Text.Trim();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
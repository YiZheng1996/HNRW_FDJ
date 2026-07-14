using MainUI.Report.Entity;
using System;
using System.Windows.Forms;

namespace MainUI.Report
{
    public partial class frmNominalValueInput : Form
    {
        public NominalValueInfo Result { get; private set; }

        public frmNominalValueInput()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtNominalRPM.Text, out double rpm) || rpm <= 0)
            {
                MessageBox.Show("请输入有效的名义转速（大于0）。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }
            if (!double.TryParse(txtNominalPower.Text, out double power) || power < 0)
            {
                MessageBox.Show("请输入有效的名义功率（大于等于0）。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            Result = new NominalValueInfo { NominalRPM = rpm, NominalPower = power };
        }
    }
}
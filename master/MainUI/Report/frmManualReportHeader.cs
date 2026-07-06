using MainUI.Report.Entity;
using System;
using System.Windows.Forms;

namespace MainUI.Report
{
    public partial class frmManualReportHeader : Form
    {
        public ManualReportHeaderInfo Result { get; private set; }

        public frmManualReportHeader(ManualReportHeaderInfo lastValues = null)
        {
            InitializeComponent();

            if (lastValues != null)
            {
                txtTestProject.Text = lastValues.TestProject;
                txtSuperchargerModel.Text = lastValues.SuperchargerModel;
                txtSuperchargerSN.Text = lastValues.SuperchargerSN;
                txtTestBenchNo.Text = lastValues.TestBenchNo;
                txtMainGeneratorNo.Text = lastValues.MainGeneratorNo;
                txtAvgOutsideTemp.Text = lastValues.AvgOutsideTemp.ToString();
                txtAvgAtmPressure.Text = lastValues.AvgAtmPressure.ToString();
                txtHumidity.Text = lastValues.Humidity.ToString();
                txtOilGrade.Text = lastValues.OilGrade;
                txtFuelGrade.Text = lastValues.FuelGrade;
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTestProject.Text) ||
                string.IsNullOrWhiteSpace(txtSuperchargerModel.Text) ||
                string.IsNullOrWhiteSpace(txtSuperchargerSN.Text) ||
                string.IsNullOrWhiteSpace(txtTestBenchNo.Text) ||
                string.IsNullOrWhiteSpace(txtMainGeneratorNo.Text) ||
                string.IsNullOrWhiteSpace(txtOilGrade.Text) ||
                string.IsNullOrWhiteSpace(txtFuelGrade.Text))
            {
                MessageBox.Show("请把所有字段填写完整。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            if (!double.TryParse(txtAvgOutsideTemp.Text, out double outTemp))
            {
                MessageBox.Show("平均外温必须是数字。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None; return;
            }
            if (!double.TryParse(txtAvgAtmPressure.Text, out double atmP))
            {
                MessageBox.Show("平均大气压力必须是数字。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None; return;
            }
            if (!double.TryParse(txtHumidity.Text, out double humidity) || humidity < 0 || humidity > 100)
            {
                MessageBox.Show("相对湿度必须是 0~100 之间的数字。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None; return;
            }

            Result = new ManualReportHeaderInfo
            {
                TestProject = txtTestProject.Text.Trim(),
                SuperchargerModel = txtSuperchargerModel.Text.Trim(),
                SuperchargerSN = txtSuperchargerSN.Text.Trim(),
                TestBenchNo = txtTestBenchNo.Text.Trim(),
                MainGeneratorNo = txtMainGeneratorNo.Text.Trim(),
                AvgOutsideTemp = outTemp,
                AvgAtmPressure = atmP,
                Humidity = humidity,
                OilGrade = txtOilGrade.Text.Trim(),
                FuelGrade = txtFuelGrade.Text.Trim(),
            };
            this.DialogResult = DialogResult.OK;
        }
    }
}
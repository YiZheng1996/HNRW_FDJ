using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RW.UI.Manager;
using MainUI.Model;
using MainUI.BLL;
using MainUI.Config;
using MainUI.Config.Test;
using MainUI.Properties;
using RW.Components;

namespace MainUI.Procedure.Test
{
    public partial class ucTestManage : ucBaseManagerUI
    {
        ParaConfig paraconfig = new ParaConfig();
        public ucTestManage()
        {
            InitializeComponent();
        }
        private void ucTestParams_Load(object sender, EventArgs e)
        {

            LoadSysConfig();
        }

        private void LoadSysConfig()
        {

        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        private void LoadConfig()
        {
            try
            {
                paraconfig = new ParaConfig();
                paraconfig.SetSectionName(txtModel.Text);
                paraconfig.Load(paraconfig.Filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtModel.Text))
                {
                    MessageBox.Show("试验参数请选择产品后保存", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                paraconfig.SetSectionName(txtModel.Text);
                paraconfig.Save();
                MessageBox.Show("保存成功。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败。" + ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 重置
        /// </summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtModel.Text))
                LoadConfig();
        }


        //产品选择
        private void btnProductSelection_Click(object sender, EventArgs e)
        {
            frmSpec fs = new frmSpec();
            fs.ShowDialog();
            txtType.Text = Common.mTestViewModel.ModelType;
            txtModel.Text = Common.mTestViewModel.ModelName;
            LoadConfig();
        }


        /// <summary>
        ///  添加步骤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddStep_Click(object sender, EventArgs e)
        {
            string model = this.txtModel.Text;
            string TestName = listTestStep.SelectedItem.ToString();
            frmTestEdit cde = new frmTestEdit(model,TestName, new TestBasePara());
            DialogResult dialogResult = cde.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                TestConfig testConfig = new TestConfig(model, TestName);
                TestBasePara para1 = new TestBasePara();
                int MaxIndex = 0;
                for (int i = 0; i < testConfig.testBasePara.Count; i++)
                {
                    if(testConfig.testBasePara[i].Index>MaxIndex)
                    {
                        MaxIndex = testConfig.testBasePara[i].Index;
                    }
                }
                para1.Index = MaxIndex + 1;
                para1.TestName = cde.;
                para1.CycleName = dicTestInfo["CycleName"];
                para1.StepName = dicTestInfo["StepName"];
                para1.RunTime = dicTest["Time"];
                para1.Torque = dicTest["Torque"];
                para1.RPM = dicTest["RPM"];
                testConfig.testBasePara.Add(para1);
                testConfig.TorqueChangeTime = Convert.ToInt32(this.TorqueChangeTimeValue.Value);
                testConfig.TorqueChangeMultiple = Convert.ToInt32(this.TorqueChangeMultValue.Value);
                testConfig.RPMChangeTime = Convert.ToInt32(this.RPMChangeTimeValue.Value);
                testConfig.RPMChangeMultiple = Convert.ToInt32(this.RPMChangeMultValue.Value);
                testConfig.Save();
                
                LoadGridView();
            }

        }

        private void listTestStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridView();

        }

        /// <summary>
        ///  编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStepEdit_Click(object sender, EventArgs e)
        {
            if (this.dgvMH.SelectedRows == null)
            {

            }
            else
            {
                dicTest.Clear();
                string model = this.txtModel.Text;
                string TestName = listTestStep.SelectedItem.ToString();
                dicTest.Add("Time", dgvMH.CurrentRow.Cells["Time"].Value.ToDouble());
                dicTest.Add("Torque", dgvMH.CurrentRow.Cells["Torque"].Value.ToDouble());
                dicTest.Add("RPM", dgvMH.CurrentRow.Cells["RPM"].Value.ToDouble());
                frmTestEdit edit = new frmTestEdit(model, TestName);
                DialogResult result = edit.ShowDialog();
                if (result == DialogResult.OK)
                {
                    TestBasePara para1 = null;
                    dicTest = edit._dicTest;
                    TestConfig testConfig = new TestConfig(model, TestName);
                    
                    var duData = testConfig.testBasePara.Where(d => d.Index == Convert.ToInt32(this.dgvMH.CurrentRow.Cells["Index"].Value)).FirstOrDefault();
                    duData.RunTime = dicTest["Time"];
                    duData.Torque = dicTest["Torque"];
                    duData.RPM = dicTest["RPM"];

                    testConfig.Save();

                    LoadGridView();


                }
            }
        }
        /// <summary>
        /// 刷新表格数据
        /// </summary>
        private void LoadGridView()
        {
            
            TestConfig testConfig = new TestConfig(this.txtModel.Text.ToString(), listTestStep.SelectedItem.ToString());
            testConfig.Load();
            this.dgvMH.Rows.Clear();
            this.TorqueChangeTimeValue.Value = testConfig.TorqueChangeTime;
            this.TorqueChangeMultValue.Value = testConfig.TorqueChangeMultiple;
            this.RPMChangeTimeValue.Value = testConfig.RPMChangeTime;
            this.RPMChangeMultValue.Value = testConfig.RPMChangeMultiple;
            var para = testConfig.testBasePara;
            for (int i = 0; i < para.Count; i++)
            {
                this.dgvMH.Rows.Add(para[i].Index, para[i].TestName,para[i].CycleName,para[i].StepName, para[i].RunTime, para[i].Torque, para[i].RPM);
                dgvMH.Columns["Index"].Visible = false;
            }

        }

        private void btnStepDelete_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("确定删除此运行时间的操作吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
                return;
            if (this.dgvMH.CurrentRow == null)
            {
                MessageBox.Show("请选中要删除的数据行！", "提示");
            }
            TestConfig testConfig = new TestConfig(this.txtModel.Text.ToString(), listTestStep.SelectedItem.ToString());
            testConfig.Load();

            for (int i = 0; i < testConfig.testBasePara.Count; i++)
            {
                if (testConfig.testBasePara[i].Index == this.dgvMH.CurrentRow.Cells[0].FormattedValue.ToDouble())
                {
                    testConfig.testBasePara.Remove(testConfig.testBasePara[i]);
                }
            }
            //urStep.testBasePara.Remove();
            testConfig.Save();
            MessageBox.Show("删除成功!", "提示");
            LoadGridView();
        }

     

        private void btnChangeParaWrite_Click(object sender, EventArgs e)
        {
            dicTest.Clear();
            dicTest.Add("TorqueChangeTime", this.TorqueChangeTimeValue.Value.ToDouble());
            dicTest.Add("TorqueChangeMult", this.TorqueChangeMultValue.Value.ToDouble());
            dicTest.Add("RPMChangeTime", this.RPMChangeTimeValue.Value.ToDouble());
            dicTest.Add("RPMChangeMult", this.RPMChangeMultValue.Value.ToDouble());
            frmTestChangeParaEdit frmTestChangeParaEdit = new frmTestChangeParaEdit(this.txtModel.Text,listTestStep.SelectedItem.ToString(),dicTest);
            DialogResult result = frmTestChangeParaEdit.ShowDialog();
            
            //DialogResult result = MessageBox.Show("确认修改吗？（操作将会修改此节点所有分段数据！）", "提示", MessageBoxButtons.OKCancel);
            //if(result == DialogResult.OK)
            //{
            //    try
            //    {
            //        DurStepConfig durStep = new DurStepConfig(this.txtModel.Text.ToString(), listTestStep.SelectedItem.ToString());
            //        durStep.Load();
            //        durStep.TorqueChangeTime = Convert.ToInt32(this.TorqueChangeTimeValue.Value);
            //        durStep.TorqueChangeMultiple = Convert.ToInt32(this.TorqueChangeMultValue.Value);
            //        durStep.RPMChangeTime = Convert.ToInt32(this.RPMChangeTimeValue.Value);
            //        durStep.RPMChangeMultiple = Convert.ToInt32(this.RPMChangeMultValue.Value);
            //        durStep.Save();
            //        MessageBox.Show("修改分段数据成功！");
            //    }
            //    catch (Exception ex)
            //    {

            //        throw new Exception("修改分段数据失败！原因： "+ex.Message);
            //    }
            //}
           
            LoadGridView();
        }
    }
}

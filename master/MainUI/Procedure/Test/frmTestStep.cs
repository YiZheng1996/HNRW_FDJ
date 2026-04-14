
using RW.Components;
using RW.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MainUI.Config.Test;


namespace MainUI.Procedure.Test
{
    public partial class frmTestStep : Form
    {
        string _model = null;
        string _testName = null;

        /// <summary>
        /// 新增/修改 数据集合
        /// </summary>
        public TestStepList _testStep { get; set; }

        public frmTestStep(string model, string testName, TestStepList testStep)
        {
            _model = model;
            _testName = testName;
            _testStep = testStep;
            InitializeComponent();

            //if (testName == "性能试验")
            //{
            //    BuildPerfTest();
            //}
            //else
            //{
            //    BuildDurTest();
            //}

            if (_testStep.Index == 0)
            {
                // 新增
                this.lblAdd.Visible = true;
            }
            else
            {
                // 编辑
                this.cbTestName.Text = _testStep.TestName;

                // 加载表格
                LoadView();
            }
        }


        /// <summary>
        /// 加载表格
        /// </summary>
        public void LoadView()
        {
            this.dgvMH.Rows.Clear();
            foreach (var item in _testStep.testBasePara)
            {
                this.dgvMH.Rows.Add(item.Index, item.TestName, item.CycleName, item.StepName, item.RPM, item.Torque, item.RunTime, item.CollectIntervalTime);
            }

        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStepSave_Click(object sender, EventArgs e)
        {
            string TestName = this.cbTestName.Text;

            _testStep.TestName = TestName;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCencel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 添加具体步骤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddStep_Click(object sender, EventArgs e)
        {
            // 添加 
            // 创建新的测试参数
            TestBasePara newPara = new TestBasePara()
            {
                TestName = this.cbTestName.Text,
                CycleName = "NULL",
                StepName = "NULL",
            };

            // 设置默认值
            newPara.Torque = (double)this.TorqueValue.Value;
            newPara.RPM = (double)this.RPMValue.Value;
            newPara.RunTime = (double)this.TimeValue.Value;

            // 计算最大索引
            int maxIndex = 0;
            foreach (var item in _testStep.testBasePara)
            {
                if (item.Index > maxIndex)
                {
                    maxIndex = item.Index;
                }
            }
            newPara.Index = maxIndex + 1;

            // 添加到列表
            _testStep.testBasePara.Add(newPara);

            // 刷新表格
            LoadView();

            //frmTestEdit frmTestEdit = new frmTestEdit(_model, _testName, new TestBasePara() { TestName = this.cbTestName.Text});
            //DialogResult dialogResult = frmTestEdit.ShowDialog();
            //if (dialogResult == DialogResult.OK)
            //{
            //    TestBasePara para1 = new TestBasePara();
            //    int MaxIndex = 0;
            //    for (int i = 0; i < _testStep.testBasePara.Count; i++)
            //    {
            //        if (_testStep.testBasePara[i].Index > MaxIndex)
            //        {
            //            MaxIndex = _testStep.testBasePara[i].Index;
            //        }
            //    }
            //    para1.Index = MaxIndex + 1;
            //    para1.TestName = frmTestEdit._testBasePara.TestName;
            //    para1.RunTime = frmTestEdit._testBasePara.RunTime;
            //    para1.Torque = frmTestEdit._testBasePara.Torque;
            //    para1.RPM = frmTestEdit._testBasePara.RPM;

            //    _testStep.testBasePara.Add(para1);

            //    LoadView();
            //}

        }

        /// <summary>
        /// 编辑步骤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStepEdit_Click(object sender, EventArgs e)
        {
            if (this.dgvMH.SelectedRows == null)
            {
                Var.MsgBoxWarn(this, "请先选择一条数据进行编辑");
            }
            else
            {
                // 编辑
                if (this.dgvMH.SelectedRows == null || this.dgvMH.SelectedRows.Count == 0)
                {
                    MessageBox.Show("请先选择一条数据进行编辑", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 获取选中的行
                DataGridViewRow selectedRow = this.dgvMH.CurrentRow;
                int selectedIndex = Convert.ToInt32(selectedRow.Cells["Index"].Value);

                // 查找对应的数据
                TestBasePara editPara = _testStep.testBasePara.Find(d => d.Index == selectedIndex);
                if (editPara != null)
                {
                    // 更新参数
                    editPara.Torque = (double)this.TorqueValue.Value;
                    editPara.RPM = (double)this.RPMValue.Value;
                    editPara.RunTime = (double)this.TimeValue.Value;

                    // 刷新表格
                    LoadView();

                    Var.MsgBoxSuccess(this, "编辑成功");
                }

                //TestBasePara testBasePara = new TestBasePara();
                //testBasePara.Index = dgvMH.CurrentRow.Cells["Index"].Value.ToInt();
                //testBasePara.TestName = dgvMH.CurrentRow.Cells["StepName"].Value.ToString();
                //testBasePara.RunTime = dgvMH.CurrentRow.Cells["RunTime"].Value.ToDouble();
                //testBasePara.Torque = dgvMH.CurrentRow.Cells["Torque"].Value.ToDouble();
                //testBasePara.RPM = dgvMH.CurrentRow.Cells["RPM"].Value.ToDouble();

                //frmTestEdit edit = new frmTestEdit(_model, _testName, testBasePara);
                //DialogResult result = edit.ShowDialog();
                //if (result == DialogResult.OK)
                //{
                //    var duData = _testStep.testBasePara.Find(d => d.Index == this.dgvMH.CurrentRow.Cells["Index"].Value.ToInt()); //(d => d.Index == Convert.ToInt32(this.dgvMH.CurrentRow.Cells["Index"].Value)).FirstOrDefault();
                //    duData.RunTime = edit._testBasePara.RunTime;
                //    duData.RPM = edit._testBasePara.RPM;
                //    duData.Torque = edit._testBasePara.Torque;
                //    LoadView();
                //}
            }
        }

        /// <summary>
        /// 删除步骤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStepDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvMH.SelectedRows == null || this.dgvMH.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择一条数据进行删除", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 确认删除
            DialogResult result = MessageBox.Show("确定要删除选中的步骤吗？", "确认删除",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // 获取选中的行
                DataGridViewRow selectedRow = this.dgvMH.CurrentRow;
                int selectedIndex = Convert.ToInt32(selectedRow.Cells["Index"].Value);

                // 从列表中删除
                TestBasePara deletePara = _testStep.testBasePara.Find(d => d.Index == selectedIndex);
                if (deletePara != null)
                {
                    _testStep.testBasePara.Remove(deletePara);
                    LoadView();
                }
            }
        }


        ////耐久试验初始化
        //private void BuildDurTest()
        //{
        //    string[] listStep = new string[]
        //    {
        //        "A",
        //        "A'",
        //        "B",
        //        "C",
        //        "D",
        //        "E",
        //        "F",
        //        "G",
        //        "H",
        //        "I",
        //        "L",
        //        "M",
        //        "N",
        //        "O",
        //        "P",
        //        "Q",
        //        "R",
        //    };
        //    //this.cbTestName.Items.Add("耐久试验I");
        //    //this.cbTestName.Items.Add("耐久试验II");
        //    //this.cbTestName.Items.Add("耐久试验III");
        //    //this.cbTestName.Items.Add("耐久试验IV");
        //    //this.cbTestName.Items.Add("耐久试验V");

        //    //this.cbTestName.SelectedIndex = 0;
        //}

        /// <summary>
        /// 单元格点击事件 - 将选中行的数据填充到编辑区域
        /// </summary>
        private void dgvMH_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvMH.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvMH.Rows[e.RowIndex];

                // 将选中行的数据填充到编辑区域
                this.TorqueValue.Value = Convert.ToDecimal(selectedRow.Cells["Torque"].Value);
                this.RPMValue.Value = Convert.ToDecimal(selectedRow.Cells["RPM"].Value);
                this.TimeValue.Value = Convert.ToDecimal(selectedRow.Cells["RunTime"].Value);
            }
        }
    }
}
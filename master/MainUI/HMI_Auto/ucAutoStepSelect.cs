using MainUI.Config;
using MainUI.Config.Test;
using MainUI.Global;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.HMI_Auto
{
    public partial class ucAutoStepSelect : Form
    {
        CurrentStatusConfig currentStatusConfig = new CurrentStatusConfig();

        /// <summary>
        /// 100h 试验参数
        /// </summary>
        Test100hConfig testConfig100h { get; set; }

        /// <summary>
        /// 循环代码对应时间
        /// </summary>
        public ConcurrentDictionary<string, int> NodeDataTime = new ConcurrentDictionary<string, int>() { };

        /// <summary>
        /// 当前选中的主表行索引
        /// </summary>
        private int selectedRowIndex = 0;

        /// <summary>
        /// 当前选中的详细表行索引
        /// </summary>
        private int selectedDetailRowIndex = 0;

        /// <summary>
        /// 当前选中的主表行索引
        /// </summary>
        private string selectedDetailKey = "";

        /// <summary>
        /// 试验名称
        /// </summary>
        private string _TestName { get; set; }

        /// <summary>
        /// 工况配置参数
        /// </summary>
        GKConfig gkConfig { get; set; }

        public ucAutoStepSelect(string TestName, string No)
        {
            InitializeComponent();

            if (this.DesignMode) return;
            _TestName = TestName;
            this.lblNo.Text = No;

            // 注册GridViewStepAll的点击事件
            this.GridViewStepAll.CellClick += GridViewStepAll_CellClick;
            this.dataGridLoopCode.CellClick += DataGridLoopCode_CellClick;

            // 实时刷新转速/扭矩...
            this.timer1.Enabled = true;
            this.timer1.Start();
        }

        private void ucAutoStepSelect_Load(object sender, EventArgs e)
        {
            FreshStepView();
        }

        /// <summary>
        /// 子表（循环代码表）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridLoopCode_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // 点击标题行时忽略

            // 更新选中的详细行索引
            selectedDetailRowIndex = e.RowIndex;

            this.dataGridLoopCode.Rows[selectedDetailRowIndex].Selected = true;

            if (dataGridLoopCode.Rows[selectedDetailRowIndex].Cells[4].Value != null)
            {
                // 设置 ucStepTrackBar2 的最大值为选中的运行时间
                if (double.TryParse(dataGridLoopCode.Rows[selectedDetailRowIndex].Cells[4].Value.ToString(), out double runTime))
                {
                    this.ucStepTrackBar2.MaxValue = runTime - 1 < 0 ? 0 : (int)runTime - 1;
                    this.ucStepTrackBar2.CurrentValue = 0; // 重置为0
                    this.ucStepTrackBar2.Invalidate(); // 刷新显示
                }

                var gkNo = this.dataGridLoopCode.Rows[selectedDetailRowIndex].Cells[1].Value.ToString();
                this.lblGKBH.Text = gkNo;

                // 通过工况编号查询数据
                ViewDataToGK(gkNo);
            }

            this.lblGKSore.Text = (selectedDetailRowIndex + 1).ToString();
            this.lblGKTime.Text = "0";
        }

        /// <summary>
        /// 主表（主流程表）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewStepAll_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // 点击标题行时忽略

            // 更新选中的行索引
            selectedRowIndex = e.RowIndex;

            this.GridViewStepAll.Rows[selectedRowIndex].Selected = true;
            this.lblSelectSore.Text = this.GridViewStepAll.SelectedCells[0].Value.ToString();
            this.lblNodeName.Text = this.lblTitle.Text = this.GridViewStepAll.SelectedCells[3].Value.ToString();
            selectedDetailKey = this.lblNodeName.Text;

            // 刷新详细表格
            UpdateLoopCodeView();
        }

        /// <summary>
        /// 刷新步骤表
        /// </summary>
        public void FreshStepView()
        {
            // 清空表
            this.GridViewStepAll.Rows.Clear();
            this.dataGridLoopCode.Rows.Clear();

            this.lblTestName.Text = _TestName == "耐久试验" ? "360h" : "100h";
            this.lblTestName2.Text = $"{this.lblTestName.Text} 执行步骤";
            gkConfig = new GKConfig(this.lblTestName.Text);

            currentStatusConfig = new CurrentStatusConfig(Common.mTestViewModel.ModelName, _TestName);

            // 设置上次试验的时间
            ucStepTrackBar1.CurrentValue = currentStatusConfig.StepTime;
            ucStepTrackBar1.Invalidate();

            if (_TestName == "性能试验")
            {
                // 100小时 主试验流程
                testConfig100h = new Test100hConfig(Common.mTestViewModel.ModelName, _TestName);
                foreach (var item in testConfig100h.testStepLists)
                {
                    GridViewStepAll.Rows.Add(item.Index, item.Index, "-", item.TestName, item.DayNum);
                }

                if (GridViewStepAll.Rows.Count > 0)
                {
                    // 如果存在上一次未执行完的步骤则选中，否则默认选中第一条
                    selectedRowIndex = 0;
                    selectedDetailRowIndex = 0;

                    if (!currentStatusConfig.TestStatus && currentStatusConfig.Sore > 0)
                    {
                        // 查找与当前步骤数匹配的行
                        for (int i = 0; i < GridViewStepAll.Rows.Count; i++)
                        {
                            if (GridViewStepAll.Rows[i].Cells[0].Value != null &&
                                GridViewStepAll.Rows[i].Cells[0].Value.ToString() == currentStatusConfig.Sore.ToString())
                            {
                                selectedRowIndex = i;
                                break;
                            }
                        }
                    }

                    // 选中并高亮显示
                    if (GridViewStepAll.Rows.Count >= selectedRowIndex)
                    {
                        // 在设置选中行之前，先清除所有选择
                        GridViewStepAll.ClearSelection();
                        GridViewStepAll.Rows[selectedRowIndex].Selected = true;
                        GridViewStepAll.Rows[selectedRowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                        GridViewStepAll.FirstDisplayedScrollingRowIndex = selectedRowIndex;
                    }

                    // 加载循环代码流程表
                    UpdateLoopCodeView();
                }
            }
            else
            {
                // 360小时 主试验流程
                var DurabilityTestConfig360 = new Test360hConfig(Common.mTestViewModel.ModelName);
                foreach (var item in DurabilityTestConfig360.DurabilityDatas)
                {
                    GridViewStepAll.Rows.Add(item.Index, item.PhaseName, item.CycleName, item.NodeName, item.DayNum, "未开始");
                }

                if (GridViewStepAll.Rows.Count > 0)
                {
                    // 如果存在上一次未执行完的步骤，默认（黄色高亮），否则默认选中第一条
                    selectedDetailKey = "";
                    selectedRowIndex = 0;
                    selectedDetailRowIndex = 0;

                    if (!currentStatusConfig.TestStatus && currentStatusConfig.Sore > 0)
                    {
                        // 查找与当前步骤数匹配的行
                        for (int i = 0; i < GridViewStepAll.Rows.Count; i++)
                        {
                            if (GridViewStepAll.Rows[i].Cells[0].Value != null &&
                                GridViewStepAll.Rows[i].Cells[0].Value.ToString() == currentStatusConfig.Sore.ToString())
                            {
                                selectedDetailKey = GridViewStepAll.Rows[i].Cells[3].Value.ToString();
                                selectedRowIndex = i;
                                break;
                            }
                        }
                    }

                    // 选中并高亮主表格行
                    if (GridViewStepAll.Rows.Count >= selectedRowIndex)
                    {
                        // 在设置选中行之前，先清除所有选择
                        GridViewStepAll.ClearSelection();
                        GridViewStepAll.Rows[selectedRowIndex].Selected = true;
                        GridViewStepAll.Rows[selectedRowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                        GridViewStepAll.FirstDisplayedScrollingRowIndex = selectedRowIndex;
                    }

                    // 加载循环代码流程表
                    UpdateLoopCodeView();
                }

            }

            // 更新界面显示的上一次试验数据
            UpdateLastTestDataDisplay();
        }

        /// <summary>
        /// 更新上一次试验数据显示
        /// </summary>
        private void UpdateLastTestDataDisplay()
        {
            // 更新试验信息区域的显示
            this.lblSelectSore.Text = this.lblLSSore.Text = currentStatusConfig.Sore.ToString();
            this.lblLSGKSore.Text = currentStatusConfig.PhaseIndex.ToString();
            this.lblLSGKName.Text = currentStatusConfig.NodeName;
            this.lblLSGKTime.Text = currentStatusConfig.StepTime.ToString();
            this.lblNodeName.Text = this.lblTitle.Text = currentStatusConfig.NodeName;
            this.lblLSGKBH.Text = currentStatusConfig.GKBH;
            this.lblLSSpeed.Text = currentStatusConfig.Speed.ToString();
            this.lblLSLC.Text = currentStatusConfig.ExcitationCurrent.ToString();
            this.lblLSPower.Text = currentStatusConfig.TargetPower.ToString();
        }

        /// <summary>
        /// 更新循环步骤表
        /// </summary>
        public void UpdateLoopCodeView()
        {
            selectedDetailRowIndex = 0;

            if (_TestName == "耐久试验")
            {
                // 360h
                this.dataGridLoopCode.Rows.Clear();
                var durStepConfig = new DurStepConfig(Common.mTestViewModel.ModelName, selectedDetailKey);
                foreach (var item in durStepConfig.testBasePara)
                {
                    this.dataGridLoopCode.Rows.Add(item.Index, item.GKNo, item.Torque, item.RPM, item.RunTime);
                }

                // 判断主流程表选中行是否为最后一次试验序号
                if (Convert.ToInt32(GridViewStepAll.SelectedCells[0].Value) == currentStatusConfig.Sore)
                {
                    // 未做完试验 && 最后一次进行的子工况 && 表格的行数是否正确
                    if (!currentStatusConfig.TestStatus && currentStatusConfig.PhaseIndex > 0 && this.dataGridLoopCode.Rows.Count >= currentStatusConfig.PhaseIndex)
                    {
                        selectedDetailRowIndex = currentStatusConfig.PhaseIndex - 1; // 索引从0开始
                        if (selectedDetailRowIndex < this.dataGridLoopCode.Rows.Count)
                        {
                            // 高亮选中行
                            this.dataGridLoopCode.Rows[selectedDetailRowIndex].Selected = true;
                            this.dataGridLoopCode.Rows[selectedDetailRowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                            this.dataGridLoopCode.FirstDisplayedScrollingRowIndex = selectedDetailRowIndex;

                            // 设置步骤条的最大值
                            if (this.dataGridLoopCode.Rows[selectedDetailRowIndex].Cells[4].Value != null &&
                                double.TryParse(this.dataGridLoopCode.Rows[selectedDetailRowIndex].Cells[4].Value.ToString(), out double runTime))
                            {
                                this.ucStepTrackBar2.MaxValue = runTime - 1 < 0 ? 0 : (int)runTime - 1;
                                this.ucStepTrackBar2.CurrentValue = currentStatusConfig.StepTime > this.ucStepTrackBar2.MaxValue ? this.ucStepTrackBar2.MaxValue : currentStatusConfig.StepTime;
                                this.ucStepTrackBar2.Invalidate();

                                this.lblGKSore.Text = (selectedDetailRowIndex + 1).ToString();
                                this.lblGKTime.Text = runTime.ToString();

                                var gkNo = this.dataGridLoopCode.Rows[selectedDetailRowIndex].Cells[1].Value.ToString();
                                this.lblGKBH.Text = gkNo;

                                // 通过工况编号查询数据
                                ViewDataToGK(currentStatusConfig.GKBH);
                            }
                        }
                    }
                    else
                    {
                        // 默认重置步骤条
                        this.ucStepTrackBar2.CurrentValue = 0;
                        this.ucStepTrackBar2.Invalidate();
                        this.lblGKBH.Text = "0";
                        this.lblSpeed.Text = "0";
                        this.lblLC.Text = "0";
                        this.lblPower.Text = "0";
                    }
                }
                else
                {
                    if (this.dataGridLoopCode.Rows.Count > 0)
                    {
                        // 设置步骤条的最大值
                        if (this.dataGridLoopCode.Rows[selectedDetailRowIndex].Cells[4].Value != null &&
                        double.TryParse(this.dataGridLoopCode.Rows[selectedDetailRowIndex].Cells[4].Value.ToString(), out double runTime))
                        {
                            this.ucStepTrackBar2.MaxValue = runTime - 1 < 0 ? 0 : (int)runTime - 1;
                            this.ucStepTrackBar2.CurrentValue = 0;
                            this.ucStepTrackBar2.Invalidate();

                            this.lblGKSore.Text = (selectedDetailRowIndex + 1).ToString();
                            this.lblGKTime.Text = "0";

                            var gkNo = this.dataGridLoopCode.Rows[selectedDetailRowIndex].Cells[1].Value.ToString();
                            this.lblGKBH.Text = gkNo;

                            // 通过工况编号查询数据
                            ViewDataToGK(gkNo);
                        }
                    }
                }
            }
            else
            {
                // 100h
                this.dataGridLoopCode.Rows.Clear();
                var durStepConfig = testConfig100h.testStepLists.Where(d => d.Index == (selectedRowIndex + 1)).FirstOrDefault();
                foreach (var item in durStepConfig.testBasePara)
                {
                    this.dataGridLoopCode.Rows.Add(item.Index, item.GKNo, item.Torque, item.RPM, item.RunTime);
                }

                // 判断主流程表选中行是否为最后一次试验序号
                if (Convert.ToInt32(GridViewStepAll.SelectedCells[0].Value) == currentStatusConfig.Sore)
                {
                    // 未做完试验 && 最后一次进行的子工况 && 表格的行数是否正确
                    if (!currentStatusConfig.TestStatus && currentStatusConfig.PhaseIndex > 0 && this.dataGridLoopCode.Rows.Count >= currentStatusConfig.PhaseIndex)
                    {
                        selectedDetailRowIndex = currentStatusConfig.PhaseIndex - 1; // 索引从0开始
                        if (selectedDetailRowIndex < this.dataGridLoopCode.Rows.Count)
                        {
                            // 高亮选中行
                            this.dataGridLoopCode.Rows[selectedDetailRowIndex].Selected = true;
                            this.dataGridLoopCode.Rows[selectedDetailRowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                            this.dataGridLoopCode.FirstDisplayedScrollingRowIndex = selectedDetailRowIndex;

                            // 设置步骤条的最大值
                            if (this.dataGridLoopCode.Rows[selectedDetailRowIndex].Cells[4].Value != null &&
                                double.TryParse(this.dataGridLoopCode.Rows[selectedDetailRowIndex].Cells[4].Value.ToString(), out double runTime))
                            {
                                this.ucStepTrackBar2.MaxValue = runTime - 1 < 0 ? 0 : (int)runTime - 1;
                                this.ucStepTrackBar2.CurrentValue = currentStatusConfig.StepTime > this.ucStepTrackBar2.MaxValue ? this.ucStepTrackBar2.MaxValue : currentStatusConfig.StepTime; ;
                                this.ucStepTrackBar2.Invalidate();

                                this.lblGKSore.Text = (selectedDetailRowIndex + 1).ToString();
                                this.lblGKTime.Text = currentStatusConfig.StepTime.ToString();

                                var gkNo = this.dataGridLoopCode.Rows[selectedDetailRowIndex].Cells[1].Value.ToString();
                                this.lblGKBH.Text = gkNo;

                                // 通过工况编号查询数据
                                ViewDataToGK(gkNo);
                            }
                        }
                    }
                    else
                    {
                        // 默认重置步骤条
                        this.ucStepTrackBar2.CurrentValue = 0;
                        this.ucStepTrackBar2.Invalidate();

                        this.lblGKBH.Text = "0";
                        this.lblSpeed.Text = "0";
                        this.lblLC.Text = "0";
                        this.lblPower.Text = "0";
                    }

                }
                else
                {
                    if (this.dataGridLoopCode.Rows.Count > 0)
                    {
                        // 显示默认第一步的序号
                        if (this.dataGridLoopCode.Rows[selectedDetailRowIndex].Cells[4].Value != null &&
                            double.TryParse(this.dataGridLoopCode.Rows[selectedDetailRowIndex].Cells[4].Value.ToString(), out double runTime))
                        {
                            this.ucStepTrackBar2.MaxValue = runTime - 1 < 0 ? 0 : (int)runTime - 1;
                            this.ucStepTrackBar2.CurrentValue = 0;
                            this.ucStepTrackBar2.Invalidate();

                            this.lblGKSore.Text = (selectedDetailRowIndex + 1).ToString();
                            this.lblGKTime.Text = "0";

                            var gkNo = this.dataGridLoopCode.Rows[selectedDetailRowIndex].Cells[1].Value.ToString();
                            this.lblGKBH.Text = gkNo;

                            // 通过工况编号查询数据
                            ViewDataToGK(gkNo);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 通过工况编号获取实际数据
        /// </summary>
        public void ViewDataToGK(string gkNo)
        {
            // 通过工况编号查询数据
            var data = gkConfig.DurabilityDatas.FirstOrDefault(d => d.GKNo == gkNo);
            if (data != null)
            {
                this.lblSpeed.Text = data.Speed.ToString();
                this.lblLC.Text = data.ExcitationCurrent.ToString();
                this.lblPower.Text = data.Power.ToString();
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        /// <summary>
        /// 拖动滚动条更新时间文本
        /// </summary>
        /// <param name="currentValue"></param>
        private void ucStepTrackBar2_OnValueChanged(double currentValue)
        {
            this.lblGKTime.Text = currentValue.ToString();
        }

        /// <summary>
        /// 开始试验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            // todo 判断当前实时数据，当前转速，功率 与选中的参数基本一致才能进行试验
            if (Math.Abs(this.lblSpeed.Text.ToDouble() - lblCurrentSpeed.Text.ToDouble()) > 100)
            {
                Var.MsgBoxWarn(this, "转速相差过大，禁止开始试验。");
                return;
            }

            if (Math.Abs(this.lblPower.Text.ToDouble() - lblCurrentPower.Text.ToDouble()) > 500)
            {
                Var.MsgBoxWarn(this, "功率相差过大，禁止开始试验。");
                return;
            }

            // TODO:预留功能，实时功率和目标功率差值的百分比，目前不知道差值多少
            //if (MiddleData.instnce.EnginePower <= MiddleData.instnce.EnginePower * 0.2)
            //{
            //    Var.MsgBoxWarn(this, "功率相差过大，禁止开始试验。");
            //    return;
            //}

            // 计算StepTimeTotal
            currentStatusConfig.StepTimeTotal = CalculateStepTimeTotal();

            // 保存选中的参数
            currentStatusConfig.Sore = this.lblSelectSore.Text.ToInt(); // 步骤数
            currentStatusConfig.AllSore = this.GridViewStepAll.Rows.Count; // 步骤总数
            currentStatusConfig.PhaseIndex = this.lblGKSore.Text.ToInt(); // 试验序号
            currentStatusConfig.AllPhaseIndex = this.dataGridLoopCode.Rows.Count; // 总序号 
            currentStatusConfig.GKBH = this.lblGKBH.Text; // 工况编号
            currentStatusConfig.NodeName = this.lblNodeName.Text; // 循环代码号
            currentStatusConfig.PhaseName = this.GridViewStepAll.SelectedCells[1].Value.ToString(); // 阶段号
            currentStatusConfig.CycleName = this.GridViewStepAll.SelectedCells[2].Value.ToString(); // 周期号
            currentStatusConfig.Speed = this.lblSpeed.Text.ToDouble(); // 目标实际转速
            currentStatusConfig.TargetPower = this.lblPower.Text.ToDouble(); // 目标实际功率
            currentStatusConfig.ExcitationCurrent = this.lblLC.Text.ToDouble(); // 目标实际励磁电流
            currentStatusConfig.StepTime = this.ucStepTrackBar2.CurrentValue; // 从第几分钟开始
            currentStatusConfig.TargetOperationTime = this.ucStepTrackBar2.MaxValue + 1; // 当前选择的工况总时间
            currentStatusConfig.TestStatus = false;
            currentStatusConfig.Save();

            this.DialogResult = DialogResult.Yes;
        }

        /// <summary>
        /// 计算从开始到当前工况的总时间
        /// </summary>
        private double CalculateStepTimeTotal()
        {
            double totalTime = 0; // 当前工况已运行时间

            if (_TestName == "耐久试验")
            {
                // 360h试验计算逻辑
                // 计算当前工况之前所有循环代码的时间总和

                // 获取当前选中的主步骤索引
                int currentMainStepIndex = selectedRowIndex;

                // 遍历当前主步骤前的所有步骤
                for (int i = 0; i < currentMainStepIndex; i++)
                {
                    // 获取每个主步骤对应的循环代码列表
                    var nodeName = GridViewStepAll.Rows[i].Cells[3].Value?.ToString();
                    if (!string.IsNullOrEmpty(nodeName))
                    {
                        try
                        {
                            // 累加该节点下所有循环代码的运行时间
                            if (NodeDataTime.ContainsKey(nodeName))
                            {
                                NodeDataTime.TryGetValue(nodeName, out var value);

                                totalTime += value;
                            }
                            else
                            {
                                var durStepConfig = new DurStepConfig(Common.mTestViewModel.ModelName, nodeName);
                                var time = durStepConfig.testBasePara.Sum(item => item.RunTime);
                                totalTime += time;

                                NodeDataTime.AddOrUpdate(nodeName, (int)time, (k, oldValue) => (int)time);
                            }
                        }
                        catch (Exception ex)
                        {
                            // 处理配置文件读取异常
                            Console.WriteLine($"读取节点 {nodeName} 配置失败: {ex.Message}");
                        }
                    }
                }

                // 当前主步骤中，当前工况之前的循环代码时间
                var currentNodeName = GridViewStepAll.Rows[currentMainStepIndex].Cells[3].Value?.ToString();
                if (!string.IsNullOrEmpty(currentNodeName))
                {
                    try
                    {
                        var currentDurStepConfig = new DurStepConfig(Common.mTestViewModel.ModelName, currentNodeName);
                        // 累加当前节点中当前工况之前的循环代码时间
                        for (int i = 0; i < selectedDetailRowIndex; i++)
                        {
                            if (i < currentDurStepConfig.testBasePara.Count)
                            {
                                totalTime += currentDurStepConfig.testBasePara[i].RunTime;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"读取当前节点配置失败: {ex.Message}");
                    }
                }
            }
            else
            {
                // 100h试验计算逻辑
                // 计算当前步骤之前所有步骤的时间总和

                // 获取当前选中的主步骤索引
                int currentMainStepIndex = selectedRowIndex;

                // 遍历当前步骤之前的所有步骤
                for (int i = 0; i < currentMainStepIndex; i++)
                {
                    var stepConfig = testConfig100h.testStepLists.Where(d => d.Index == (i + 1)).FirstOrDefault();
                    if (stepConfig != null)
                    {
                        // 累加该步骤下所有循环代码的运行时间
                        totalTime += stepConfig.testBasePara.Sum(item => item.RunTime);
                    }
                }
            }

            // 加上进度条的值
            totalTime += ucStepTrackBar2.CurrentValue;

            return totalTime;
        }

        /// <summary>
        /// 实时刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblCurrentSpeed.Text = MiddleData.instnce.EngineSpeed.ToString();
            this.lblCurrentTorque.Text = MiddleData.instnce.EngineTorque.ToString();
            this.lblCurrentPower.Text = MiddleData.instnce.EnginePower.ToString();
        }


    }
}

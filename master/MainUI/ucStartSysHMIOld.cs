using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MainUI.Model;
using MainUI.Modules;
using MainUI.Config;
using RW.UI.Controls;
using MainUI.Procedure;
using System.Threading;
using System.Diagnostics;
using RW.Fonts;
using Sunny.UI;
using RW.UI;
using RW;
using MainUI.Equip;
using MainUI.Procedure.Test.Performance;
using System.Linq;
using MainUI.BLL;
using System.Text;
using System.Data;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Management;
using MainUI.Widget;
using System.Linq;
using MainUI.Procedure.Test.Durability;
using MainUI.Config.Test;
using MainUI.Global;
using System.Threading.Tasks;

namespace MainUI
{
    public partial class ucStartSysHMIOld : UserControl
    {
        //显示管路系统的窗体
        FrmPipeControl frmPipeControlHMI;

        // 试验列表
        Dictionary<string, BaseTest> dicBase = new Dictionary<string, BaseTest>();

        // 实时测试状态
        public TestData testDataView = new TestData();

        // 型号选择选中的型号
        public string Model { get; set; }

        // 模拟量控件的集合
        public Dictionary<string, AINumericalDisplay> DoubleDicValve = new Dictionary<string, AINumericalDisplay>();

        // 所有状态的集合
        private Dictionary<string, UIButton> btnDic = new Dictionary<string, UIButton>();

        // 所有状态的集合
        private Dictionary<string, UILight> btnLight = new Dictionary<string, UILight>();

        // 测试基础类
        public TestConfig CurrentTestConfig { get; set; }

        // 实时状态（用于测试中断后的继续试验）
        public CurrentStatusConfig CurrentStatusData { get; set; }

        /// <summary>
        /// 测试列表
        /// </summary>
        public List<AutoTestStep> AutoTestStepList { get; set; } = new List<AutoTestStep>() { };

        // 测试项点字典
        public Dictionary<string, UIRadioButton> listChk = new Dictionary<string, UIRadioButton>();
        // 是否测试完成
        public bool isOK = false;
        // 试验状态更新
        public delegate void RunStatusHandler(bool obj);
        // 拍下急停后的委托
        public event RunStatusHandler EmergencyStatusChanged;

        // 试验总时间
        public System.Timers.Timer timerALL = new System.Timers.Timer();
        // 精确计时
        Stopwatch watchAll = new Stopwatch();

        // 子阶段计时
        public System.Timers.Timer timerStep = new System.Timers.Timer();
        // 精确计时
        Stopwatch watchStep = new Stopwatch();

        //public AIGrp AIgrp = null;
        //public AOGrp AOgrp = null;
        //public DIGrp DIgrp = null;
        //public DOGrp DOgrp = null;
        //public EngineOilGrp engineOilGrp = null;
        //public FuelGrp fuelGrp = null;
        //public ThreePhaseElectric threePhaseElectric = null;
        //public WaterGrp waterGrp = null;
        //public PLC2AIGrp engineParaGrp = null;
        //public GD350_1 gD350_1 = null;



        ParaConfig paraconfig = null;
        Dictionary<string, BaseTest> dicDur = new Dictionary<string, BaseTest>();

        Label[] listPefTest = null;
        Label[] listDur = null;

        string rn = Environment.NewLine;
        private delegate void Del();

        private delegate void Del2(bool b);



        public ucStartSysHMIOld()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        static extern int SystemParametersInfo(uint uiAction, uint uiParam, ref int pvParam, uint fWinIni);
        const uint SPI_SETSCROLLBARS = 98;
        const uint SPIF_UPDATEINIFILE = 0x01;


        public Dictionary<int, Sunny.UI.UILight> dicDI = new Dictionary<int, UILight>();

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            try
            {
                // 加载报警控件
                ucWarnList1.init();

                int scrollBarWidth = 20; // 设置新的滚动条宽度为20像素
                SystemParametersInfo(SPI_SETSCROLLBARS, 0, ref scrollBarWidth, SPIF_UPDATEINIFILE);

                this.dataGridViewStep.EditMode = DataGridViewEditMode.EditProgrammatically;
                this.dataGridViewStep.AllowUserToAddRows = false;
                this.dataGridViewData.EditMode = DataGridViewEditMode.EditProgrammatically;
                this.dataGridViewData.AllowUserToAddRows = false;

                // 把控件添加到字典（数据刷新）
                EachControl(this);
                // 加载测试项点
                LoadChk();

                // 注册事件
                //Common.DIgrp.DIGroupChanged += DIgrp_DIGroupChanged;
                Common.DIgrp.KeyValueChange += DIgrp_KeyValueChange;
                Common.DOgrp.KeyValueChange += DOgrp_KeyValueChange;
                Common.AI2Grp.KeyValueChange += EngineParaGrp_KeyValueChange;

                BaseTest.TipsChanged += UcBase_TipsChanged;
                TestState.StaticPropertyChanged += TestState_StaticPropertyChanged;
                TestLog.LogChanged += TestLog_LogChanged;
                BaseTest.WaitStepTick += BaseTest_WaitStepTick;

                //试验总时间计时器
                timerALL.Interval = 1000;
                timerALL.AutoReset = true;
                timerALL.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
                //试验阶段时间计时器
                timerStep.Interval = 1000;
                timerStep.AutoReset = true;
                timerStep.Elapsed += new System.Timers.ElapsedEventHandler(timer2_Elapsed);

                // init 故障点位
                ucWarning1.InitFault();
                ucWarning1.FaultReset += UcWarning1_FaultReset;

                // 强制刷新一次DI点，更新界面元素
                Common.AI2Grp.Fresh();
                Common.DIgrp.Fresh();
                Common.DOgrp.Fresh();

                // 打开数据刷新计时器
                AutoTestTimer.Enabled = true;
                // 发动手动交互数据
                SendInteractionTimer.Enabled = true;
                SendInteractionTimer.Start();

                // 初始化默认选中性能试验
                this.RadXN.Checked = true;

                int moveIndex = 0;
                // 打开另一个窗体
                if (Screen.AllScreens.Count() > 1)
                {
                    moveIndex = 1;
                }

                // 如果窗体不存在则创建，否则激活
                if (frmPipeControlHMI == null || frmPipeControlHMI.IsDisposed)
                {
                    frmPipeControlHMI = new FrmPipeControl();
                    frmPipeControlHMI.Init();
                    frmPipeControlHMI.Show();
                }
                else
                {
                    frmPipeControlHMI.Activate();
                }
                // 如果存在另一个窗体，弹出到另一个页面
                MoveFormToMonitor(frmPipeControlHMI, moveIndex);

            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, ex.Message);
            }
        }
        // 将窗体移动到指定的显示器
        private void MoveFormToMonitor(Form form, int monitorIndex)
        {
            if (monitorIndex >= 0 && monitorIndex < Screen.AllScreens.Length)
            {
                Screen targetScreen = Screen.AllScreens[monitorIndex];

                // 设置窗体位置到目标显示器的中心
                form.Location = new Point(
                    targetScreen.Bounds.Left + (targetScreen.Bounds.Width - form.Width) / 2,
                    targetScreen.Bounds.Top + (targetScreen.Bounds.Height - form.Height) / 2
                );
            }
        }
        private void DIgrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (btnLight.ContainsKey(e.Key))
            {
                if (e.Value)
                {
                    btnLight[e.Key].State = UILightState.On;
                }
                else
                {
                    btnLight[e.Key].State = UILightState.Off;
                }
            }
        }

        /// <summary>
        /// 水极板上升点动按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterPlateUp_MouseDown(object sender, MouseEventArgs e)
        {
            Common.DOgrp["水阻下降控制"] = false;
            Thread.Sleep(10);
            Common.DOgrp["水阻上升控制"] = true;
        }

        /// <summary>
        /// 水极板上升点动松开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterPlateUp_MouseUp(object sender, MouseEventArgs e)
        {
            Common.DOgrp["水阻上升控制"] = false;
            Thread.Sleep(10);
            Common.DOgrp["水阻下降控制"] = false;
        }

        /// <summary>
        /// 水极板下降点动按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterPlateDown_MouseDown(object sender, MouseEventArgs e)
        {
            Common.DOgrp["水阻上升控制"] = false;
            Thread.Sleep(10);
            Common.DOgrp["水阻下降控制"] = true;
        }

        /// <summary>
        /// 水极板下降点动松开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterPlateDown_MouseUp(object sender, MouseEventArgs e)
        {
            Common.DOgrp["水阻下降控制"] = false;
            Thread.Sleep(10);
            Common.DOgrp["水阻上升控制"] = false;
        }


        /// <summary>
        /// 加载试验项点复选框
        /// </summary>
        private void LoadChk()
        {
            listChk.Add("性能试验", RadXN);
            listChk.Add("循环试验", RadNJ);
        }

        private void TestLog_LogChanged(object sender, LogChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => TestLog_LogChanged(sender, e)));
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(e.NewValue.ToString());
            // ... 追加更多行
            tbDurTestLog.AppendText(sb.ToString());
        }


        /// <summary>
        /// 试验状态改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestState_StaticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => TestState_StaticPropertyChanged(sender, e)));
                return;
            }

            // 更新对应的Label
            switch (e.PropertyName)
            {
                //改变的值为试验阶段
                case "TestName": lblTest.Text = $"{e.NewValue}"; break;
                //改变的值为试验周期
                case "TestCycle":
                    lblCycle.Text = $"{e.NewValue}"; lblCycle2.Text = $"{e.NewValue}";
                    {
                        //读取此周期要循环的节点 加到耐久试验界面右边的试验周期中
                        this.flowLayoutPanel1.Controls.Clear();
                        DurCycleConfig durCycleConfig = new DurCycleConfig(Model, this.lblTest.Text);
                        durCycleConfig.Load();
                        //取到当前试验周期的循环节点队列
                        var v1 = durCycleConfig.DurCyclePara[Convert.ToInt32(this.lblCycle2.Text) - 1].DurCycleQueue;
                        for (int j = 0; j < v1.Count; j++)
                        {
                            //添加循环节点到界面显示中的FlowLayout控件内
                            Label lbl = new Label();
                            lbl.AutoSize = true;
                            lbl.Text = v1[j];
                            lbl.Font = new Font("宋体", 16);
                            lbl.BackColor = Color.FromArgb(243, 249, 255);
                            this.flowLayoutPanel1.Controls.Add(lbl);
                        }
                    }; break;
                //改变的值为试验循环节点
                case "TestStep":
                    lblStep.Text = $"{e.NewValue}";
                    {
                        int index; //当前正在试验的节点下标
                        for (int i = 0; i < this.flowLayoutPanel1.Controls.Count; i++)
                        {
                            //循环显示循环节点的FlowLayout控件
                            if (this.flowLayoutPanel1.Controls[i] is Label)
                            {
                                //判断是否是表示循环节点的Label控件
                                Label lblNow = this.flowLayoutPanel1.Controls[i] as Label;
                                if (lblNow.Text == e.NewValue.ToString() && lblNow.BackColor == Color.FromArgb(255, 243, 249, 255))
                                {
                                    //取到当前循环节点Label 变黄
                                    lblNow.BackColor = Color.Yellow;
                                    if (i != 0)
                                    {
                                        //只要不为第一个 将上一个变为绿色
                                        Label lblLast = this.flowLayoutPanel1.Controls[i - 1] as Label;
                                        if (lblLast.BackColor == Color.Yellow)
                                        {
                                            lblLast.BackColor = Color.Green;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        //foreach (var item in this.flowLayoutPanel1.Controls)
                        //{
                        //    if (item is Label)
                        //    {
                        //        Label lbl = item as Label;
                        //        if (lbl.Text == e.NewValue.ToString() && lbl.BackColor == Color.FromArgb(255, 243, 249, 255))
                        //        {
                        //            lbl.BackColor = Color.Green;
                        //            break;
                        //        }
                        //    }
                        //}
                    }
                    /*lblStep2.Text = $"{e.NewValue}";*/
                    break;
            }
        }

        /// <summary>
        /// DO 点状态改变(灯和部分点击按钮)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DOgrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (btnDic.ContainsKey(e.Key))
            {
                if (e.Value)
                {
                    btnDic[e.Key].FillColor = Color.Lime;
                }
                else
                {
                    btnDic[e.Key].FillColor = Color.FromArgb(243, 249, 255);
                }
            }
            else if (btnLight.ContainsKey(e.Key))
            {
                if (e.Value)
                {
                    btnLight[e.Key].State = UILightState.On;
                }
                else
                {
                    btnLight[e.Key].State = UILightState.Off;
                }
            }
        }

        /// <summary>
        /// PLC2 AI 点位更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EngineParaGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (DoubleDicValve.ContainsKey(e.Key))
            {
                DoubleDicValve[e.Key].Value = e.Value;
            }
        }

        /// <summary>
        /// DI 更新后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        private void DIgrp_DIGroupChanged(object sender, int index, bool value)
        {
            try
            {
                if (index == 1 || index == 2)
                {
                    bool emer = Common.DIgrp["紧急停止"]; //操作台急停 且 机柜急停

                    EmergencyStatusChanged?.Invoke(emer);
                }
            }
            catch (Exception ex)
            {
                //log
            }
        }

        /// <summary>
        /// 遍历控件添加到字典中，用于数据值刷新
        /// </summary>
        /// <param name="con"></param>
        private void EachControl(Control con)
        {
            try
            {
                foreach (Control item in con.Controls)
                {
                    EachControl(item);
                }

                if (con is AINumericalDisplay)
                {
                    var ival = con as AINumericalDisplay;
                    if (ival.Tag != null)
                    {
                        if (string.IsNullOrEmpty(ival.Tag.ToString()))
                        {
                            return;
                        }
                        DoubleDicValve.Add(ival.Tag.ToString(), ival);
                    }
                }
                else if (con is UIButton)
                {
                    UIButton uiBtn = con as UIButton;
                    if (uiBtn.Tag != null)
                    {
                        if (string.IsNullOrEmpty(uiBtn.Tag.ToString()))
                        {
                            return;
                        }
                        btnDic.Add(uiBtn.Tag.ToString(), uiBtn);
                    }

                }
                else if (con is UILight)
                {
                    UILight light = con as UILight;
                    if (light.Tag != null)
                    {
                        if (string.IsNullOrEmpty(light.Tag.ToString()))
                        {
                            return;
                        }
                        btnLight.Add(light.Tag.ToString(), light);
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// 故障复位点击事件
        /// </summary>
        private void UcWarning1_FaultReset()
        {
            Common.startPLCGrp.FaultReset = true;
        }


        public Dictionary<int, SwitchPictureBox> dicYougang = new Dictionary<int, SwitchPictureBox>();
        public Dictionary<int, SwitchPictureBox> dicMada = new Dictionary<int, SwitchPictureBox>();
        public Dictionary<int, SwitchPictureBox> dicYoubeng = new Dictionary<int, SwitchPictureBox>();



        string RptFilePath = "";
        /// <summary>
        /// 读取配置文件，配置参数
        /// </summary>
        private void InitParaConfig()
        {
            try
            {
                if (Common.mTestViewModel == null)
                    return;
                paraconfig = new ParaConfig();
                paraconfig.SetSectionName(Common.mTestViewModel.ModelName);
                paraconfig.Load();
                BaseTest.para = paraconfig;

                // 刷新DI点位
                Common.DIgrp.Fresh();
            }
            catch (Exception ex)
            {
                string err = "初始化试验参数有误；具体原因：" + ex.Message;
                MessageBox.Show(err, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        frmReport frmRpt = null;
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(RptFilePath))
            {
                Var.MsgBoxInfo(this, "请先选择产品型号！");
                return;
            }

            SystemHelper.KillProcess("EXCEL"); //正确参数
            //SystemHelper.KillProcess("EXCEL.EXE"); //错误参数
            Thread.Sleep(200);


            string filePath = Application.StartupPath + "\\reports\\报表模板.xls";
            string destFile = Application.StartupPath + "\\reports\\tempFile.xls";
            System.IO.File.Copy(filePath, destFile, true);

            //RptFilePath = filePath; //直接用原始模板，保存后，会把数据保存在原始模板上。
            RptFilePath = destFile; //用copy出来的临时文件


            frmRpt = new frmReport();
            Common.mTestViewModel.TestNO = txtChuchanghao.Text;
            frmRpt.viewMole = Common.mTestViewModel;


            frmRpt.Filename = RptFilePath;
            frmRpt.Opened += FrmRpt_Opened;
            frmRpt.ShowDialog();
        }

        private void BaseTest_WaitStepTick(int tick, string stepName)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, tick);
            string str = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
            //if (string.IsNullOrEmpty(stepName) == false)
            //    lblTips.Text = stepName + "\r\n" + str;
            //else
            //    lblTips.Text = str;
        }


        //写试验信息提示
        private void UcBase_TipsChanged(object sender, object info)
        {
            //this.Invoke(new Del(delegate { lblTips.AppendText(info.ToString() + "\r\n"); lblTips.ScrollToCaret(); }));
        }

        public void Close()
        {

        }


        private void btnSelectModel_Click(object sender, EventArgs e)
        {
            frmSpec fs = new frmSpec();
            fs.ShowDialog();
            txtType.Text = Common.mTestViewModel.ModelType;
            txtModel.Text = Common.mTestViewModel.ModelName;

            InitParaConfig();
        }


        private void FrmRpt_Opened(object sender, RW.UI.Controls.Report.OpenedReportArgs e)
        {
            try
            {
                frmRpt.Write("xinghao", Common.mTestViewModel.ModelName);
                frmRpt.Write("proNo", txtChuchanghao.Text);
                //frmRpt.Write("factory", lblTips.Text);


                frmRpt.Write("tester", RWUser.User.Username);
                frmRpt.Write("testDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                frmRpt.Write("yqxh", Var.SysConfig.DeviceModel);
                frmRpt.Write("yqbh", Var.SysConfig.DeviceNo);
                frmRpt.Write("yqyxq", Var.SysConfig.DeviceLimit);

                int rows = 6;
                if (Var.SysConfig.RowStart != 0)
                    rows = Var.SysConfig.RowStart;

            }
            catch (Exception ex)
            {
                string err = "报表写值有误；原因：" + ex.Message;
                Var.LogInfo(err);
                MessageBox.Show(err);
            }
        }


        private void btnExciter_Click(object sender, EventArgs e)
        {
            try
            {
                //bool result = Common.DIgrp["主发励磁柜电源"]
            }
            catch (Exception ex)
            {

                throw new Exception("励磁电源启动失败！" + ex.Message);
            }
        }

        private void btnWaterPlateDown_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw new Exception("水阻极板下降失败！" + ex.Message);
            }
        }

        private void btnWaterPlateUp_Click(object sender, EventArgs e)
        {
            Common.DOgrp["水阻上升控制"] = true;
            Common.DOgrp["水阻下降控制"] = false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void RefreshButtonStatus()
        {
            foreach (var item in btnDic)
            {

            }
        }

        public Thread th;
        private void btnTestStart_Click(object sender, EventArgs e)
        {
            if (th != null)
            {
                if (th.IsAlive)
                {
                    MessageBox.Show("已有试验在进行中！");
                    return;
                }

            }
            btnTestStart.Enabled = false;
            btnTestDown.Enabled = true;

            th = new Thread(new ThreadStart(TestStart));
            th.Start();

        }


        public void TestStart()
        {

            this.lblPefTime.Visible = true;
            this.lblDurTime.Visible = false;
            for (int i = 1; i < 6; i++)
            {
                //五个性能试验阶段
                dicBase[i.ToString()].IsTesting = true;
                listPefTest[i - 1].BackColor = Color.Yellow;
                this.lblTest.Text = listPefTest[i - 1].Text;
                this.lblCycle.Text = "NULL";
                this.lblStep.Text = "NULL";

                BeginWatch();
                //isOK = dicBase[i.ToString()].Execute();
                //dicBase[i.ToString()].IsTesting = false;
                //listPefTest[i - 1].BackColor = Color.Green;
                //if (!isOK)
                //{
                //    listPefTest[i - 1].BackColor = Color.Red;
                //    break;
                //}
            }
            StopWatch();
            if (isOK)
            {
                MessageBox.Show("试验合格");
            }
            BaseTest bt = new BaseTest();
            bt.TestStatus(false);
            btnTestStart.Enabled = true;
            btnTestDown.Enabled = false;
        }



        private void ucStartSysHMI_Load(object sender, EventArgs e)
        {
            listPefTest = new Label[]
            {
                lblSpecified,lblOverLoad,lblPartLoad,lblAlternatringLoad,lblSpecified2
            };
            listDur = new Label[]
            {
                lblDur1,lblDur2,lblDur3,lblDur4,lblDur5
            };

            dicBase.Clear();
            dicBase.Add("自动试验", new TestStartProc());

            //dicBase.Add(lblSpecified.Tag.ToString(), new Specified());
            //dicBase.Add(lblOverLoad.Tag.ToString(), new OverLoad());
            //dicBase.Add(lblPartLoad.Tag.ToString(), new OverLoad());
            //dicBase.Add(lblAlternatringLoad.Tag.ToString(), new AlternatingLoad());
            //dicBase.Add(lblSpecified2.Tag.ToString(), new Specified2());
            //dicDur.Add(lblDur1.Text.ToString(), new DurTest());
            //dicDur.Add(lblDur2.Text.ToString(), new DurTest());
            //dicDur.Add(lblDur3.Text.ToString(), new DurTest());
            //dicDur.Add(lblDur4.Text.ToString(), new DurTest());
            //dicDur.Add(lblDur5.Text.ToString(), new DurTest());
        }

        private void btnTestDown_Click(object sender, EventArgs e)
        {
            if (th != null)
            {
                if (th.IsAlive)
                {
                    th.Abort();
                }
            }
            StopWatch();
            btnTestStart.Enabled = true;
            btnTestDown.Enabled = false;
            foreach (var item in this.panel3.Controls)
            {
                if (item is Label)
                {
                    Label lbl = item as Label;
                    if (listPefTest.Contains(lbl))
                    {
                        //是试验阶段Label
                        lbl.BackColor = Color.DarkGray;
                    }

                }
            }
            this.lblTest.Text = "未开始";
            this.lblCycle.Text = "NULL";
            this.lblStep.Text = "NULL";
            this.lblTestTime.Text = "NULL";
            BaseTest bt = new BaseTest();
            bt.TestStatus(false);
        }



        private void btnDurStart_Click(object sender, EventArgs e)
        {
            if (th != null)
            {
                if (th.IsAlive)
                {
                    MessageBox.Show("已有试验在进行中！");
                    return;
                }

            }

            btnDurStart.Enabled = false;
            btnDurDown.Enabled = true;
            foreach (var item in this.panel5.Controls)
            {
                if (item is Label)
                {
                    Label lbl = item as Label;
                    if (listDur.Contains(lbl))
                    {
                        //是试验阶段Label
                        lbl.BackColor = Color.DarkGray;
                    }

                }

            }
            th = new Thread(new ThreadStart(DurStart));
            th.Start();

        }

        public void DurStart()
        {
            this.lblPefTime.Visible = false;
            this.lblDurTime.Visible = true;
            BeginWatch();

            for (int i = 0; i < 5; i++)
            {
                BeginWatchStep();
                this.listDur[i].BackColor = Color.Yellow;
                dicDur[listDur[i].Text.ToString()].IsTesting = true;

                bool isOK = dicDur[listDur[i].Text.ToString()].Execute(Model, listDur[i].Text);

                dicDur[listDur[i].Text.ToString()].IsTesting = false;
                StopWatchStep();
                listDur[i].BackColor = Color.Green;
                if (!isOK)
                {
                    listDur[i - 1].BackColor = Color.Red;
                    break;
                }

            }
            if (isOK)
            {
                MessageBox.Show("试验合格");
            }
            StopWatch();
            BaseTest bt = new BaseTest();
            bt.TestStatus(false);

        }


        private void btnDurDown_Click(object sender, EventArgs e)
        {
            if (th != null)
            {
                if (th.IsAlive)
                {
                    th.Abort();
                }
            }
            StopWatch();
            StopWatchStep();
            btnDurStart.Enabled = true;
            btnDurDown.Enabled = false;
            foreach (var item in this.panel5.Controls)
            {
                if (item is Label)
                {
                    Label lbl = item as Label;
                    if (listDur.Contains(lbl))
                    {
                        //是试验阶段Label
                        lbl.BackColor = Color.DarkGray;
                    }
                }
            }
            BaseTest bt = new BaseTest();
            bt.TestStatus(false);
        }

        /// <summary>
        /// 型号选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectModel_Click_1(object sender, EventArgs e)
        {
            frmSpec fs = new frmSpec();
            fs.ShowDialog();
            Model = Common.mTestViewModel.ModelName;
            this.txtType.Text = this.txtType2.Text = Common.mTestViewModel.ModelType;
            this.txtModel.Text = this.txtModel2.Text = Common.mTestViewModel.ModelName;
            EventTriggerModel.RaiseOnModelNameChanged(Model);

            // 刷新表格
            InitializeDataGridView();
        }

        /// <summary>
        /// 初始化表格
        /// </summary>
        private void InitializeDataGridView()
        {
            // 根据选择的项点来刷新
            CurrentTestConfig = new TestConfig(Model, testDataView.TestName);
            CurrentStatusData = new CurrentStatusConfig(Model, testDataView.TestName);

            // 更新 自动试验的 dataGridViewStep
            this.dataGridViewStep.Rows.Clear();
            this.dataGridViewData.Rows.Clear();
            // 清空测试步骤
            AutoTestStepList.Clear();
            // 清空采集部分
            testDataView.NextCollectTime = DateTime.MinValue; // 重置为最小值，在第一次采集时初始化
            testDataView.TotalCollectCount = 0;
            testDataView.IsCollecting = false;
            testDataView.StepNameCollectIndex = 0;

            int globalRowIndex = 1; // 全局序号，从1开始
            int currentStepIndex = 1; // 步骤号计数器
            foreach (var item in CurrentTestConfig.testStepLists)
            {
                currentStepIndex = 1;
                // 循环指定次数 (默认是1次， 主要用于交变负荷 4min，6min 重复8 小时)
                for (int loop = 0; loop < item.ForeachNum; loop++)
                {
                    foreach (var subItem in item.testBasePara)
                    {
                        // 添加一行数据
                        dataGridViewStep.Rows.Add(
                            globalRowIndex,                     // 序号
                            item.TestName,                      // 阶段号（阶段名称）
                            "-",                                // 默认状态
                            "-",                                // 开始时间
                            "-",                                // 结束时间
                            currentStepIndex.ToString(),        // (显示)步骤号，使用当前的步骤号
                            subItem.Index,                      // （真实）步骤号，config中存储的
                            subItem.RunTime.ToString("F1"),     // 运行时间，保留两位小数
                            subItem.Torque.ToString("F1"),      // 扭矩
                            subItem.RPM.ToString("F1"),          // 转速
                            item.CollectIntervalTime           // 采集间隔时间
                        );
                        AutoTestStepList.Add(new AutoTestStep { Sore = globalRowIndex, Index = subItem.Index, StepIndex = currentStepIndex, TestName = item.TestName, CollectIntervalTime = item.CollectIntervalTime, TestStatus = TestStatusEnum.NotStarted, Torque = subItem.Torque, RPM = subItem.RPM, RunTime = subItem.RunTime, CycleName = "-", StepName = "-" });
                        globalRowIndex++; // 序号递增
                        currentStepIndex++; // 步骤号递增
                    }
                }
            }
        }

        /// <summary>
        /// 试验开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnStart_Click(object sender, EventArgs e)
        {
            // todo 前提条件（后续添加）

            // todo 重做

            // 更改页面状态
            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;
            this.btnReport.Enabled = false;
            this.lblStatus.Text = "试验中";

            foreach (var item in listChk)
            {
                item.Value.Enabled = false;
            }

            // 刷新界面，刷新参数
            InitializeDataGridView();

            // 线程执行
            th = new Thread(new ThreadStart(AutoTestStart));
            th.Name = testDataView.TestName + "线程";
            th.Start();

            // 打开采集计时器
            AutoCollectTestTimer.Start();
            //// 测试开始（异步方法）
            //await AutoTestStart(testDataView.TestName);
        }

        /// <summary>
        /// 试验项点选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadXN_CheckedChanged(object sender, EventArgs e)
        {
            var rad = sender as UIRadioButton;
            testDataView.TestName = rad.Tag.ToString();
        }

        /// <summary>
        /// 测试开始的异步逻辑
        /// </summary>
        /// <returns></returns>
        private void AutoTestStart()
        {
            try
            {
                var testName = testDataView.TestName;

                // 检查是否需要从断点恢复
                bool needResume = CurrentStatusData != null &&
                                 !CurrentStatusData.TestStatus &&
                                 CurrentStatusData.Sore > 0;

                // 存在没做完的试验
                if (needResume)
                {
                    var dr = Var.MsgBoxYesNo(this, $"检测到未完成的测试，步骤{CurrentStatusData.Sore}({CurrentStatusData.PhaseName})，阶段内采集{CurrentStatusData.PhaseCollectNum}次。是否需要继续试验?");
                    if (dr)
                    {
                        // 如果是恢复模式，不重置状态数据
                        TestLog.UpdateTestPara($"{DateTime.Now}：检测到未完成的测试，从步骤{CurrentStatusData.Sore}恢复");
                    }
                    else
                    {
                        // 重新开始试验
                        CurrentStatusData.TestStatus = false;
                        CurrentStatusData.Sore = 0;
                        CurrentStatusData.PhaseIndex = 0;
                        CurrentStatusData.PhaseName = "";
                        CurrentStatusData.CycleName = "";
                        CurrentStatusData.NodeName = "";
                        CurrentStatusData.PhaseCollectNum = 0;
                        CurrentStatusData.PhaseCollectTime = 0;
                        CurrentStatusData.Save();
                    };
                }
                else
                {
                    // 型号一致和测试完成时，测试状态初始化
                    if (CurrentStatusData.TestStatus)
                    {
                        CurrentStatusData.TestStatus = false;
                        CurrentStatusData.Sore = 0;
                        CurrentStatusData.PhaseIndex = 0;
                        CurrentStatusData.PhaseName = "";
                        CurrentStatusData.CycleName = "";
                        CurrentStatusData.NodeName = "";
                        CurrentStatusData.PhaseCollectNum = 0;
                        CurrentStatusData.PhaseCollectTime = 0;
                        CurrentStatusData.Save();
                    }
                }

                // 设置为开始
                testDataView.IsTest = true;

                // 更改状态色(试验为黄色) 默认只能单个试验运行
                var radItem = listChk.GetValue(testName);
                this.Invoke(new Action(() =>
                {
                    radItem.Enabled = false;
                    radItem.BackColor = Color.Yellow;

                    // 打开试验总计时
                    BeginWatch();
                }));

                //dicBase["自动试验"].hmi = this;
                dicBase["自动试验"].CurrentTestConfig = CurrentTestConfig;
                var isOk = dicBase["自动试验"].Execute();
                if (isOk == TestStatusEnum.Success)
                {
                    this.Invoke(new Action(() =>
                    {
                        radItem.BackColor = Color.FromArgb(110, 190, 40);
                    }));
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        // 故障或者普通停止  todo 后续再做区分
                        radItem.BackColor = Color.FromArgb(230, 80, 80);
                    }));
                }

                this.Invoke(new Action(() =>
                {
                    // 设置控件状态
                    StopWatch();  // 停止计时
                    this.btnReport.Enabled = true;
                    this.btnStart.Enabled = true;
                    this.btnStop.Enabled = false;
                    foreach (var item in listChk)
                    {
                        item.Value.Enabled = true;
                    }
                    testDataView.IsTest = false;
                }));

                // 暂时注释

                //// 重置当前步骤索引
                //int currentStepIndex = 0;

                //// 启动自动测试循环
                //while (currentStepIndex < AutoTestStepList.Count && testDataView.IsTest)
                //{
                //    var currentStep = AutoTestStepList[currentStepIndex];
                //    testDataView.Sore = currentStep.Sore;
                //    testDataView.StepName = currentStep.StepName;

                //    // 开始当前步骤
                //    currentStep.TestStatus = TestStatusEnum.IsTest;
                //    currentStep.StarDateTime = DateTime.Now;

                //    // 更新UI显示
                //    UpdateDataGridView(currentStep);

                //    // 重启动步骤计时器
                //    StopWatchStep();
                //    BeginWatchStep();

                //    // 异步下发转速扭矩（受 testData.IsTest 影响）


                //    // 等待当前步骤完成
                //    bool stepCompleted = await WaitForStepCompletion(currentStep.RunTime);

                //    if (!stepCompleted)
                //    {
                //        // 测试被中止
                //        currentStep.TestStatus = TestStatusEnum.Stop;
                //        currentStep.EndDateTime = DateTime.Now;

                //        // 步骤异常
                //        // 结束时更新数据表格显示
                //        UpdateDataGridView(currentStep);
                //        break;
                //    }

                //    // 步骤完成
                //    currentStep.TestStatus = TestStatusEnum.Success;
                //    currentStep.EndDateTime = DateTime.Now;

                //    // 停止步骤计时器
                //    StopWatchStep();

                //    // 结束时更新数据表格显示
                //    UpdateDataGridView(currentStep);

                //    // 移动到下一步
                //    currentStepIndex++;
                //}

                //// 所有测试步骤完成才算合格
                //if (currentStepIndex >= AutoTestStepList.Count)
                //{
                //    radItem.BackColor = Color.FromArgb(110, 190, 40);
                //    this.lblStatus.Text = "测试完成";
                //    // 所有步骤完成
                //    Var.MsgBoxSuccess(this, "测试完成！");
                //}
                //else
                //{
                //    radItem.BackColor = Color.FromArgb(230, 80, 80);
                //}

                //// 停止计时
                //StopWatch();

                //testDataView.IsTest = false;

                //// 恢复按钮状态
                //this.Invoke(new Action(() =>
                //{
                //    this.btnStart.Enabled = true;
                //    this.btnStop.Enabled = false;
                //    this.btnReport.Enabled = true;

                //    foreach (var item in listChk)
                //    {
                //        item.Value.Enabled = true;
                //    }
                //}));
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "出现异常：" + ex.ToString());
            }

        }

        /// <summary>
        /// 等待步骤完成的异步方法
        /// </summary>
        public async Task<bool> WaitForStepCompletion(double runTimeMinutes)
        {
            double elapsedSeconds = 0;
            double targetSeconds = 2;//runTimeMinutes * 60;

            while (elapsedSeconds < targetSeconds && testDataView.IsTest)
            {
                // 等待1秒
                await Task.Delay(1000);

                // 检查是否被中止
                // todo 后续再补全，故障时也要退出提示
                if (!testDataView.IsTest)
                    return false;

                elapsedSeconds++;
            }

            // 正常做完
            return elapsedSeconds >= targetSeconds;
        }

        /// <summary>
        /// 结束时更新数据表格显示
        /// </summary>
        public void UpdateDataGridView(AutoTestStep step)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<AutoTestStep>(UpdateDataGridView), step);
                return;
            }

            switch (step.TestStatus)
            {
                // 阶段开始
                case TestStatusEnum.IsTest:

                    // 更新主界面显示
                    this.lblSore.Text = step.Sore.ToString();
                    this.lblStepName.Text = step.TestName;

                    // 获取表格的试验行
                    var row = dataGridViewStep.Rows[step.Sore - 1];
                    row.DefaultCellStyle.BackColor = Color.Yellow;

                    // 更新开始时间
                    row.Cells["StarDateTime"].Value = step.StarDateTime.ToString("MM-dd HH:mm:ss");
                    // 更新状态
                    row.Cells["TestStatus"].Value = GetStatusText(step.TestStatus);

                    //自动滚动到当前行（确保在视野中）
                    row.DataGridView.FirstDisplayedScrollingRowIndex = step.Sore - 1;
                    row.DataGridView.CurrentCell = row.Cells[0];

                    // 保存到ini里
                    CurrentStatusData.Sore = step.Sore;
                    CurrentStatusData.PhaseIndex = step.StepIndex;
                    CurrentStatusData.PhaseName = step.TestName;
                    CurrentStatusData.CycleName = step.CycleName;
                    CurrentStatusData.NodeName = step.StepName;
                    CurrentStatusData.PhaseCollectTime = step.CollectIntervalTime;
                    CurrentStatusData.Save();

                    break;

                // 正常结束
                case TestStatusEnum.Success:
                    var successRow = dataGridViewStep.Rows[step.Sore - 1];
                    successRow.DefaultCellStyle.BackColor = Color.LightGreen;

                    // 更新状态
                    successRow.Cells["EndDateTime"].Value = DateTime.Now.ToString("MM-dd HH:mm:ss");
                    // 更新状态
                    successRow.Cells["TestStatus"].Value = GetStatusText(step.TestStatus);

                    break;

                // 异常结束
                case TestStatusEnum.Stop:
                    var stopRow = dataGridViewStep.Rows[step.Sore - 1];
                    stopRow.DefaultCellStyle.BackColor = Color.Red;

                    // 更新状态
                    stopRow.Cells["TestStatus"].Value = GetStatusText(step.TestStatus);
                    stopRow.Cells["EndDateTime"].Value = DateTime.Now.ToString("MM-dd HH:mm:ss");
                    break;
            }

            dataGridViewStep.Refresh();
        }

        /// <summary>
        /// 获取状态文本
        /// </summary>
        public string GetStatusText(TestStatusEnum status)
        {
            switch (status)
            {
                case TestStatusEnum.NotStarted: return "未开始";
                case TestStatusEnum.IsTest: return "测试中";
                case TestStatusEnum.Stop: return "已中止";
                case TestStatusEnum.Success: return "已完成";
                default: return "-";
            }
        }

        /// <summary>
        /// 停止测试
        /// </summary>
        public void btnStop_Click(object sender, EventArgs e)
        {
            // 将测试状态设置为false
            testDataView.IsTest = false;
            dicBase["自动试验"].IsTesting = false;
            this.lblStatus.Text = "试验终止";

            // 记录最终采集统计
            TestLog.UpdateTestPara($"{DateTime.Now}：试验终止，总共采集{testDataView.TotalCollectCount}次数据");
        }

        /// <summary>
        /// 自动测试逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoTestTimer_Tick(object sender, EventArgs e)
        {

            //while (true)
            //{
            //    // 自动试验逻辑,怎么按照流程进行，
            //}
        }

        /// <summary>
        /// 自动采集逻辑线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoCollectTestTimer_Tick(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 采集到表格
        /// </summary>
        private void CollectDataToDataGridView()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(CollectDataToDataGridView));
                return;
            }

            // 如果没有设置采集间隔或者不在测试中，直接返回
            if (testDataView.CollectTime <= 0 || !testDataView.IsTest)
                return;

            // 检查是否需要初始化下一次采集时间
            if (testDataView.NextCollectTime == DateTime.MinValue)
            {
                // 设置第一次采集时间为当前时间
                var now = DateTime.Now;
                testDataView.NextCollectTime = now.AddMinutes(testDataView.CollectTime).AddSeconds(-1);
                return;
            }

            // 如果达到采集间隔时间，则采集数据
            if (DateTime.Now >= testDataView.NextCollectTime)
            {
                testDataView.IsCollecting = true;

                try
                {
                    // 获取当前试验状态
                    var currentStep = AutoTestStepList.FirstOrDefault(s => s.Sore == testDataView.Sore);

                    // 创建新行数据
                    object[] rowData = CreateDataRow(currentStep);

                    // 添加新行到dataGridViewData
                    dataGridViewData.Rows.Add(rowData);

                    // 自动滚动到最后一行
                    if (dataGridViewData.Rows.Count > 0)
                    {
                        dataGridViewData.FirstDisplayedScrollingRowIndex = dataGridViewData.Rows.Count - 1;
                    }

                    // 更新最后采集时间和计数器
                    testDataView.TotalCollectCount++;
                    testDataView.StepNameCollectIndex++;

                    // 记录采集日志
                    string stepInfo = currentStep != null ?
                        $"步骤{currentStep.Sore}({currentStep.TestName})" : "未知步骤";

                    TestLog.UpdateTestPara($"{testDataView.NextCollectTime}：第{testDataView.TotalCollectCount}次采集 - {stepInfo}，采集间隔{testDataView.CollectTime}分钟");

                    // 采集完更新下次采集时间
                    testDataView.NextCollectTime = testDataView.NextCollectTime.AddMinutes(testDataView.CollectTime);

                    // 保存当前状态 阶段采集次数
                    CurrentStatusData.PhaseCollectNum = testDataView.StepNameCollectIndex;
                    CurrentStatusData.Save();
                }
                finally
                {
                    testDataView.IsCollecting = false;
                }
            }
        }

        /// <summary>
        /// 当采集间隔改变时重新计算下一次采集时间,切换大阶段时需要调用
        /// </summary>
        public void OnCollectIntervalChanged()
        {
            if (testDataView.IsTest && testDataView.CollectTime > 0)
            {
                // 重新计算下一次采集时间，基于当前时间
                var now = DateTime.Now;
                testDataView.NextCollectTime = now.AddMinutes(testDataView.CollectTime).AddSeconds(-1);

                TestLog.UpdateTestPara($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}：采集间隔已更改为{testDataView.CollectTime}分钟");
            }
        }

        /// <summary>
        /// 创建数据行 - 基于当前实时状态
        /// </summary>
        private object[] CreateDataRow(AutoTestStep currentStep)
        {
            // 获取实时数据
            double actualSpeed = 1; // todo 实时转速
            double actualTorque = 1; // todo 实时扭矩
            double power = (actualSpeed * actualTorque) / 9550;

            // todo 还需要很多参数需要补充

            // 获取设定值（如果有当前步骤）
            double setSpeed = currentStep?.RPM ?? 0;
            double setTorque = currentStep?.Torque ?? 0;
            string stepName = currentStep?.StepName ?? "未知";
            string testName = currentStep?.TestName ?? testDataView.TestName;
            int sore = currentStep?.Sore ?? testDataView.Sore;

            return new object[]
            {
                 sore,                                          // 当前步骤序号
                 testDataView.NextCollectTime.ToString("yyyy-MM-dd"),  // 采集时间(日期)
                 testDataView.NextCollectTime.ToString("HH:mm:ss"),  // 采集时间（时间）
                 testDataView.StepNameCollectIndex,             // 阶段采集次数
                 testName,                                      // 试验阶段名称
                 stepName,                                      // 步骤名称
                 setSpeed.ToString("F1"),                       // 设定转速
                 setTorque.ToString("F1"),                      // 设定扭矩
                 actualSpeed.ToString("F1"),                    // 实际转速
                 actualTorque.ToString("F1"),                   // 实际扭矩
                 power.ToString("F1"),                          // 计算功率
                 testDataView.CollectTime.ToString("F1"),       // 采集间隔
                 "正常"                                         // 状态
            };
        }


        /// <summary>
        /// 停止总计时器（计时器重置）
        /// </summary>
        public void StopWatch()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(StopWatch));
                return;
            }

            watchAll.Reset();
            this.timerALL.Stop();
            watchStep.Reset();
            this.timerStep.Stop();
        }

        /// <summary>
        /// 停止步骤计时
        /// </summary>
        public void StopWatchStep()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(StopWatchStep));
                return;
            }
            watchStep.Reset();
            this.timerStep.Stop();
        }

        /// <summary>
        /// 恢复计时器
        /// </summary>
        public void BeginWatch()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(BeginWatch));
                return;
            }

            watchAll.Start();
            this.timerALL.Start();
        }

        /// <summary>
        /// 恢复步骤计时
        /// </summary>
        public void BeginWatchStep()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(BeginWatchStep));
                return;
            }
            watchStep.Start();
            this.timerStep.Start();
        }

        /// <summary>
        /// 试验总时间精确计时（1秒执行一次）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!this.IsHandleCreated)
                return;
            try
            {
                // 获取总的运行时间间隔
                TimeSpan totalTime = watchAll.Elapsed;

                // 手动提取 拼接
                int totalHours = (int)totalTime.TotalHours;  // 总小时数
                int minutes = totalTime.Minutes;             // 分钟 [0~59]
                int seconds = totalTime.Seconds;             // 秒 [0~59]

                // 格式化为：HH:mm:ss 
                string mainTime = $"{totalHours:D2}:{minutes:D2}:{seconds:D2}";

                RW.Components.ControlHelper.Invoke(this, delegate
                {
                    //this.lblTestTime.Text = mainTime; 
                    //this.lblPefTime.Text = mainTime;
                    //this.lblDurTime.Text = mainTime;
                    this.lblAllTime.Text = mainTime;
                });


                // 获取正计时
                TimeSpan totalTimeStep = watchStep.Elapsed;
                // ========== 2. 当前步骤剩余时间==========
                int stepAllSeconds = testDataView.StepTotalSeconds;
                // 计时器的总时间
                int watchStepSeconds = (int)totalTimeStep.TotalSeconds;
                // 剩余时间与计时器时间差值
                int difference = stepAllSeconds - watchStepSeconds;
                testDataView.StepRemainingSeconds = difference;
                if (difference < 0) difference = 0;
                // 分段计算 因为直接转换 小时部分不能超过24小时
                int diffHours = difference / 3600;
                int diffMinutes = (difference % 3600) / 60;
                int diffSeconds = difference % 60;

                // 格式化为：HH:mm:ss 
                string mainTimeStep = $"{diffHours:D2}:{diffMinutes:D2}:{diffSeconds:D2}";

                RW.Components.ControlHelper.Invoke(this, delegate
                {
                    //this.lblDurNowTime.Text = mainTime;
                    this.lblRemainTime.Text = mainTimeStep;
                });

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 获取阶段计时器的时间(秒)
        /// </summary>
        /// <returns></returns>
        public int GetTotalSecondsStepWatch()
        {
            if (InvokeRequired)
            {
                return (int)Invoke(new Func<int>(GetTotalSecondsStepWatch));
            }
            TimeSpan totalTimeStep = watchStep.Elapsed;
            int secondsStep = (int)totalTimeStep.TotalSeconds;
            return secondsStep;
        }

        public void timer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!this.IsHandleCreated)
                return;
            try
            {
                // 在做试验时，每隔X分钟采集一次数据进
                if (testDataView.IsTest && !testDataView.IsCollecting)
                {
                    // 根据当前的采集间隔，将数据添加到 dataGridViewData 中，需要有采集时间的计时器
                    CollectDataToDataGridView();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 值更新刷新文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtChuchanghao_TextChanged(object sender, EventArgs e)
        {
            this.txtChuchanghao2.Text = this.txtChuchanghao.Text;
        }

        /// <summary>
        /// 交互
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendInteractionTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Common.ExChangeGrp.SetDouble("待处理机油箱温度", Common.engineOilGrp["待处理机油箱温度检测-T24"]);
                Common.ExChangeGrp.SetDouble("待处理机油箱液位", Common.engineOilGrp["待处理机油箱液位检测-L19"]);
                Common.ExChangeGrp.SetDouble("高温水出机温度", Common.AI2Grp["T1高温水出机温度"]);
                Common.ExChangeGrp.SetDouble("高温水进机温度", Common.AI2Grp["T2高温水进机温度"]);

                Common.ExChangeGrp.SetDouble("机油出机压力", Common.AI2Grp["P20机油泵出口压力"]);
                Common.ExChangeGrp.SetDouble("机油进机压力", Common.AI2Grp["P21主油道进口油压"]);
                Common.ExChangeGrp.SetDouble("机油箱温度", Common.engineOilGrp["机油箱温度检测-T23"]);
                Common.ExChangeGrp.SetDouble("机油箱液位", Common.engineOilGrp["机油箱液位检测-L18"]);

                //todo 暂时不知道哪个点位
                //Common.ExChangeGrp.SetDouble("内循环水箱液位", Common.engineOilGrp[""]);
                Common.ExChangeGrp.SetDouble("燃油进机温度", Common.AI2Grp["T31燃油泵进口油温"]);
                Common.ExChangeGrp.SetDouble("燃油进机压力", Common.AI2Grp["P38燃油供油压力"]);
                Common.ExChangeGrp.SetDouble("燃油箱液位", Common.fuelGrp["柴油箱液位检测-L29"]);

                Common.ExChangeGrp.SetDouble("预热水箱温度", Common.waterGrp["预热水箱温度检测-T12"]);
                Common.ExChangeGrp.SetDouble("预热水箱液位", Common.waterGrp["预热水箱液位检测"]);
            }
            catch (Exception)
            {
                Var.LogInfo("实时发送数据给下位机失败。");
            }
            
        }
    }
}

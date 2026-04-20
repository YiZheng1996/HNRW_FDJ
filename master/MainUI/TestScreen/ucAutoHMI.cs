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
using MainUI.Config.Test;
using MainUI.Global;
using System.Threading.Tasks;
using MainUI.FSql;
using System.Collections.Concurrent;
using static MainUI.Modules.EventArgsModel;
using MainUI.HMI_Auto;
using MainUI.Helper;
using MainUI.Wave;
using System.ComponentModel;

namespace MainUI
{
    public partial class ucAutoHMI : UserControl
    {
        /// <summary>
        /// 启机弹窗描述
        /// </summary>
        frmMessageWarning frmStartupMessage { get; set; } = new frmMessageWarning();

        private FrmGKChangeInfo _frmChangeInfo;
        /// <summary>
        /// 工况切换前弹窗提示
        /// </summary>
        private FrmGKChangeInfo frmChangeInfo
        {
            get
            {
                if (_frmChangeInfo == null || _frmChangeInfo.IsDisposed)
                {
                    // 确保在UI线程中创建窗体
                    if (this.InvokeRequired)
                    {
                        return (FrmGKChangeInfo)this.Invoke(new Func<FrmGKChangeInfo>(() =>
                            new FrmGKChangeInfo()));
                    }
                    else
                    {
                        _frmChangeInfo = new FrmGKChangeInfo();
                    }
                }
                return _frmChangeInfo;
            }
            set { _frmChangeInfo = value; }
        }

        /// <summary>
        /// 试验列表
        /// </summary>
        Dictionary<string, BaseTest> dicBase { get; set; } = new Dictionary<string, BaseTest>();

        #region 360 小时试验相关

        // 360小时循环代码的步骤
        public List<DurStepConfig> DurStepConfigDic { get; set; } = new List<DurStepConfig>() { };

        // 360小时流程配置参数
        public Test360hConfig TestConfig360 { get; set; } = new Test360hConfig();
        #endregion

        #region 100 小时试验相关
        // 100小时 测试基础类
        public Test100hConfig TestConfig100 { get; set; }

        #endregion

        /// <summary>
        /// 模拟量控件的集合
        /// </summary>
        public Dictionary<string, AINumericalDisplay> DoubleDicValve = new Dictionary<string, AINumericalDisplay>();

        /// <summary>
        /// 所有状态的集合
        /// </summary> 
        private Dictionary<string, UIButton> btnDic = new Dictionary<string, UIButton>();

        /// <summary>
        /// 所有状态的集合
        /// </summary>  
        private Dictionary<string, UILight> btnLight = new Dictionary<string, UILight>();

        /// <summary>
        /// 工况列表（默认加载100h）
        /// </summary>
        public GKConfig gkConfig { get; set; }

        /// <summary>
        /// 测试列表
        /// </summary>
        public List<AutoTestStep> AutoTestStepList { get; set; } = null;

        /// <summary>
        /// 试验时间
        /// </summary> 
        public System.Timers.Timer timerALL = new System.Timers.Timer();
        /// <summary>
        /// 计时器精确计时
        /// </summary>  
        Stopwatch watchAll = new Stopwatch();

        /// <summary>
        /// 暂停时计时
        /// </summary>
        public System.Timers.Timer timerPause = new System.Timers.Timer();
        /// <summary>
        /// 暂停时精确计时
        /// </summary>
        Stopwatch watchPause = new Stopwatch();

        /// <summary>
        /// 子阶段计时（工况）
        /// </summary>
        public System.Timers.Timer timerStep = new System.Timers.Timer();
        /// <summary>
        /// 子阶段精确计时（工况）
        /// </summary>
        Stopwatch watchStep = new Stopwatch();

        /// <summary>
        /// 手动记录Service类
        /// </summary> 
        ManualRecordService manualRecordService = new ManualRecordService();

        /// <summary>
        /// 自动试验记录Service类
        /// </summary> 
        AutoRecordService autoRecordService = new AutoRecordService();

        /// <summary>
        /// 试验工况选中后的行
        /// </summary>
        int PreviousSelectRow { get; set; }

        /// <summary>
        /// 曲线记录字典
        /// </summary> 
        public ConcurrentDictionary<string, WaveReocrd> waveReocrd = new ConcurrentDictionary<string, WaveReocrd>() { };

        string rn = Environment.NewLine;
        private delegate void Del();

        public ucAutoHMI()
        {
            InitializeComponent();
        }


        [DllImport("user32.dll")]
        static extern int SystemParametersInfo(uint uiAction, uint uiParam, ref int pvParam, uint fWinIni);
        const uint SPI_SETSCROLLBARS = 98;
        const uint SPIF_UPDATEINIFILE = 0x01;


        public Dictionary<int, Sunny.UI.UILight> dicDI = new Dictionary<int, UILight>();

        /// <summary>
        /// Init初始化
        /// </summary>
        public void Init()
        {
            try
            {
                // 添加设计时检查
                if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                {
                    return;
                }

                tabControl2.TabPages.RemoveAt(2);

                int scrollBarWidth = 20; // 设置新的滚动条宽度为20像素
                SystemParametersInfo(SPI_SETSCROLLBARS, 0, ref scrollBarWidth, SPIF_UPDATEINIFILE);

                this.dataGridLoopCode.EditMode = DataGridViewEditMode.EditProgrammatically;
                this.dataGridLoopCode.AllowUserToAddRows = false;
                this.dataGridViewData.EditMode = DataGridViewEditMode.EditProgrammatically;
                this.dataGridViewData.AllowUserToAddRows = false;
                // 把控件添加到字典（数据刷新）
                EachControl(this);
                // 初始化默认选中性能试验
                this.btnXN.Switch = true;
                this.btnStop.Enabled = false;
                this.btnPause.Enabled = false;
                gkConfig = new GKConfig("100h");

                MiddleData.instnce.testDataView.TestName = "性能试验";
                this.lblAutoName.Text = "100h 步骤";
                // 柴油机运行时间
                var timer = Math.Round(Var.SysConfig.RunTime / 60, 1);
                this.lblRunTime.Text = timer.ToString();

                // 默认加载曲线
                InitWave();

                // 在手动界面型号选择后
                EventTriggerModel.OnModelNameChanged += EventTriggerModel_OnModelNameChanged;

                // 更改编号时
                EventTriggerModel.OnModelNumberChanged += EventTriggerModel_OnModelNumberChanged;

                // 触发计时更新
                EventTriggerModel.OnTimngChanged += EventTriggerModel_OnTimngChanged;

                // 注册模块事件
                Common.DIgrp.KeyValueChange += DIgrp_KeyValueChange;
                Common.DOgrp.KeyValueChange += DOgrp_KeyValueChange;
                Common.AI2Grp.KeyValueChange += EngineParaGrp_KeyValueChange;
                Common.AOgrp.KeyValueChange += AOgrp_KeyValueChange;

                BaseTest.TipsChanged += UcBase_TipsChanged;
                TestLog.LogChanged += TestLog_LogChanged;
                BaseTest.WaitStepTick += BaseTest_WaitStepTick;

                // 试验总时间计时器
                timerALL.Interval = 980;
                timerALL.AutoReset = true;
                timerALL.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
                //// 试验阶段时间计时器
                //timerStep.Interval = 980;
                //timerStep.AutoReset = true;
                //timerStep.Elapsed += new System.Timers.ElapsedEventHandler(timer2_Elapsed);

                timerPause.Interval = 980;
                timerPause.AutoReset = true;
                timerPause.Elapsed += new System.Timers.ElapsedEventHandler(timerPause_Elapsed);

                // 打开数据刷新计时器
                this.AutoTestTimer.Enabled = true;
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, ex.Message);
            }
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucStartSysHMI_Load(object sender, EventArgs e)
        {
            dicBase.Clear();
            dicBase.Add("自动试验", new Test100hProc());
        }

        /// <summary>
        ///  型号选择
        /// </summary>
        /// <param name="model"></param>
        private void EventTriggerModel_OnModelNameChanged(string model)
        {
            this.txtType.Text = Common.mTestViewModel.ModelType;
            this.txtModel.Text = Common.mTestViewModel.ModelName;
            this.txtTorque.Text = MiddleData.instnce.SelectModelConfig.RatedTorque.ToString();
            this.txtSpeed.Text = MiddleData.instnce.SelectModelConfig.RatedSpeed.ToString();
            this.txtMinSpeed.Text = MiddleData.instnce.SelectModelConfig.MinSpeed.ToString();

            // 刷新基础参数
            BaseTest.para = MiddleData.instnce.SelectModelConfig;

            // 刷新表格
            // InitializeDataGridView();

            // 根据当前选择的类型刷新
            FreshStepView();
        }

        /// <summary>
        /// 编号切换时
        /// </summary>
        /// <param name="obj"></param>
        private void EventTriggerModel_OnModelNumberChanged(string number)
        {
            this.txtChuchanghao.Text = number;
        }

        /// <summary>
        /// 后台记录发动机运行时间更新
        /// </summary>
        /// <param name="obj"></param>
        private void EventTriggerModel_OnTimngChanged(double timer)
        {
            this.lblRunTime.Text = timer.ToString();
        }

        /// <summary>
        /// AO 值改变事件
        /// 用于刷新励磁与转速设定值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AOgrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (e.Key == "励磁调节")
            {
                this.ucNudLC.Value = e.Value;
            }
            else if (e.Key == "发动机油门调节")
            {
                this.ucNudSpeed.Value = e.Value;
            }
        }

        /// <summary>
        /// DI值改变检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        string RptFilePath = "";

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
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, object>(UcBase_TipsChanged), sender, info);
                return;
            }

            this.lblStatus.Text = info.ToString();
        }


        private void FrmRpt_Opened(object sender, RW.UI.Controls.Report.OpenedReportArgs e)
        {
            try
            {
                //var record = DB.mysql.Select<ManualRecordPara>().Where(d => d.gid == gid).ToList();
                //rmRpt.Write("RecordName", record[0].RecordName);

                //frmRpt.sa
                //frmRpt.Write("RecordDataTime", record[0].RecordDataTime);
                //frmRpt.Write("AT", record[0].AT);
                //frmRpt.Write("AP", record[0].AP);
                //frmRpt.Write("AH", record[0].AH);
                //frmRpt.Write("RPM", record[0].RPM);
                //frmRpt.Write("Torque", record[0].Torque);
                //frmRpt.Write("Power", record[0].Power);
                //frmRpt.Write("LWaterTempIn", record[0].LWaterTempIn);
                //frmRpt.Write("HWaterTempIn", record[0].HWaterTempIn);
                //frmRpt.Write("LWaterTempOut", record[0].LWaterTempOut);
                //frmRpt.Write("HWaterTempOut", record[0].HWaterTempOut);
                //frmRpt.Write("EngineWaterTempIn", record[0].EngineWaterTempIn);
                //frmRpt.Write("EngineWaterTempOut", record[0].EngineWaterTempOut);
                //frmRpt.Write("LPressureIn", record[0].LPressureIn);
                //frmRpt.Write("HPressureIn", record[0].HPressureIn);
                //frmRpt.Write("LPressureOut", record[0].LPressureOut);
                //frmRpt.Write("HPressureOut", record[0].HPressureOut);
                //frmRpt.Write("LWaterPressureOut", record[0].LWaterPressureOut);
                //frmRpt.Write("HWaterPressureOut", record[0].HWaterPressureOut);
                //frmRpt.Write("HeatExchangerTempIn", record[0].HeatExchangerTempIn);
                //frmRpt.Write("HeatExchangerTempOut", record[0].HeatExchangerTempOut);
                //frmRpt.Write("EOPressure2", record[0].EOPressure2);
                //frmRpt.Write("EOPressure1", record[0].EOPressure1);
                //frmRpt.Write("EngineOilOutletTemp", record[0].EngineOilOutletTemp);
                //frmRpt.Write("EOAnalysis", record[0].EOAnalysis);
                //frmRpt.Write("EOConsumption", record[0].EOConsumption);
                //frmRpt.Write("FrontAirTempIn", record[0].FrontAirTempIn);
                //frmRpt.Write("AfterAirTempIn", record[0].AfterAirTempIn);
                //frmRpt.Write("FrontAirTempOut", record[0].FrontAirTempOut);
                //frmRpt.Write("AfterAirTempOut", record[0].AfterAirTempOut);
                //frmRpt.Write("FrontAirPressureIn", record[0].FrontAirPressureIn);
                //frmRpt.Write("AfterAirPressureIn", record[0].AfterAirPressureIn);
                //frmRpt.Write("FrontAirPressureOut", record[0].FrontAirPressureOut);
                //frmRpt.Write("AfterAirPressureOut", record[0].AfterAirPressureOut);
                //frmRpt.Write("FrontTurbochargerRPM", record[0].FrontTurbochargerRPM);
                //frmRpt.Write("AfterTurbochargerRPM", record[0].AfterTurbochargerRPM);
                //frmRpt.Write("FrontTurbochargerPressureIn", record[0].FrontTurbochargerPressureIn);
                //frmRpt.Write("AfterTurbochargerPressureIn", record[0].AfterTurbochargerPressureIn);
                //frmRpt.Write("FrontTurbochargerPressureOut", record[0].FrontTurbochargerPressureOut);
                //frmRpt.Write("AfterTurbochargerPressureOut", record[0].AfterTurbochargerPressureOut);
                //frmRpt.Write("EGTempA1", record[0].EGTempA1);
                //frmRpt.Write("EGTempA2", record[0].EGTempA2);
                //frmRpt.Write("EGTempA3", record[0].EGTempA3);
                //frmRpt.Write("EGTempA4", record[0].EGTempA4);
                //frmRpt.Write("EGTempA5", record[0].EGTempA5);
                //frmRpt.Write("EGTempA6", record[0].EGTempA6);
                //frmRpt.Write("EGTempA7", record[0].EGTempA7);
                //frmRpt.Write("EGTempA8", record[0].EGTempA8);
                //frmRpt.Write("EGTempB1", record[0].EGTempB1);
                //frmRpt.Write("EGTempB2", record[0].EGTempB2);
                //frmRpt.Write("EGTempB3", record[0].EGTempB3);
                //frmRpt.Write("EGTempB4", record[0].EGTempB4);
                //frmRpt.Write("EGTempB5", record[0].EGTempB5);
                //frmRpt.Write("EGTempB6", record[0].EGTempB6);
                //frmRpt.Write("EGTempB7", record[0].EGTempB7);
                //frmRpt.Write("EGTempB8", record[0].EGTempB8);
                //frmRpt.Write("FrontTurbochargerTempIn", record[0].FrontTurbochargerTempIn);
                //frmRpt.Write("AfterTurbochargerTempIn", record[0].AfterTurbochargerTempIn);
                //frmRpt.Write("FrontTurbochargerTempOut", record[0].FrontTurbochargerTempOut);
                //frmRpt.Write("AfterTurbochargerTempOut", record[0].AfterTurbochargerTempOut);
                //frmRpt.Write("FrontTurbochargerPressureIn2", record[0].FrontTurbochargerPressureIn2);
                //frmRpt.Write("AfterTurbochargerPressureIn2", record[0].AfterTurbochargerPressureIn2);
                //frmRpt.Write("Smoke", record[0].Smoke);
                //frmRpt.Write("ECOTime", record[0].ECOTime);
                //frmRpt.Write("ECOQuantity", record[0].ECOQuantity);
                //frmRpt.Write("ECORate", record[0].ECORate);
                //frmRpt.Write("OilTempIn", record[0].OilTempIn);
                //frmRpt.Write("InjectionParameter", record[0].InjectionParameter);


                //frmRpt.Write("",record.)


                //frmRpt.Write("xinghao", Common.mTestViewModel.ModelName);
                //frmRpt.Write("proNo", txtChuchanghao.Text);
                ////frmRpt.Write("factory", lblTips.Text);


                //frmRpt.Write("tester", RWUser.User.Username);
                //frmRpt.Write("testDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                //frmRpt.Write("yqxh", Var.SysConfig.DeviceModel);
                //frmRpt.Write("yqbh", Var.SysConfig.DeviceNo);
                //frmRpt.Write("yqyxq", Var.SysConfig.DeviceLimit);

                //int rows = 6;
                //if (Var.SysConfig.RowStart != 0)
                //    rows = Var.SysConfig.RowStart;

            }
            catch (Exception ex)
            {
                string err = "报表写值有误；原因：" + ex.Message;
                Var.LogInfo(err);
                MessageBox.Show(err);
            }
        }

        /// <summary>
        /// 刷新步骤表
        /// </summary>
        public void FreshStepView()
        {
            // 清空表
            this.dgvAuto.Rows.Clear();
            DurStepConfigDic.Clear();

            MiddleData.instnce.CurrentStatusData = new CurrentStatusConfig(Common.mTestViewModel.ModelName, MiddleData.instnce.testDataView.TestName);

            if (this.btnXN.Switch)
            {
                // 100小时
                TestConfig100 = new Test100hConfig(Common.mTestViewModel.ModelName, MiddleData.instnce.testDataView.TestName);
                var maxVal = Math.Max(gkConfig.DurabilityDatas.Count, TestConfig100.testStepLists.Count);

                for (int i = 0; i < maxVal; i++)
                {
                    var stepData = TestConfig100.testStepLists.Count - 1 >= i ? TestConfig100.testStepLists[i] : null;
                    var gkData = gkConfig.DurabilityDatas.Count - 1 >= i ? gkConfig.DurabilityDatas[i] : null;
                    this.dgvAuto.Rows.Add(stepData?.Index, stepData?.Index, stepData == null ? "-" : "", stepData?.TestName, stepData?.DayNum, gkData?.GKNo, gkData?.Speed, gkData?.ExcitationCurrent, gkData?.Torque, gkData?.Power);
                }

                // 100小时 循环代码 数据
                ucStepStatus1.Load100hItem(TestConfig100);
            }
            else
            {
                // 360小时的主流程 数据
                TestConfig360 = new Test360hConfig(Common.mTestViewModel.ModelName);
                var maxVal = Math.Max(gkConfig.DurabilityDatas.Count, TestConfig360.DurabilityDatas.Count);

                for (int i = 0; i < maxVal; i++)
                {
                    var stepData = TestConfig360.DurabilityDatas.Count - 1 >= i ? TestConfig360.DurabilityDatas[i] : null;
                    var gkData = gkConfig.DurabilityDatas.Count - 1 >= i ? gkConfig.DurabilityDatas[i] : null;
                    this.dgvAuto.Rows.Add(stepData?.Index, stepData?.PhaseName, stepData?.CycleName, stepData?.NodeName, stepData?.DayNum, gkData?.GKNo, gkData?.Speed, gkData?.ExcitationCurrent, gkData?.Torque, gkData?.Power);
                }

                // 360小时 循环代码 数据
                for (int i = 0; i < Var.SysConfig.TestStep360.Count(); i++)
                {
                    var durStepConfig = new DurStepConfig(Common.mTestViewModel.ModelName, Var.SysConfig.TestStep360[i]);
                    DurStepConfigDic.Add(durStepConfig);
                }
                ucStepStatus1.LoadItem(DurStepConfigDic);
            }

            // 刷新步骤
            UpdateLastStepData();
        }

        /// <summary>
        /// 刷新最后一次试验的状态
        /// </summary>
        public void UpdateLastStepData()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(UpdateLastStepData));
                return;
            }

            // 为界面更新数据
            this.lblStepReal.Text = MiddleData.instnce.CurrentStatusData.Sore.ToString(); // 步骤号
            this.lblStepAll.Text = MiddleData.instnce.CurrentStatusData.AllSore.ToString(); // 总步骤数
            this.lblJDReal.Text = MiddleData.instnce.CurrentStatusData.PhaseName.ToString(); // 阶段
            this.lblZQReal.Text = MiddleData.instnce.CurrentStatusData.CycleName == null ? "-" : MiddleData.instnce.CurrentStatusData.CycleName.ToString(); // 周期
            this.lblXHDMReal.Text = MiddleData.instnce.CurrentStatusData.NodeName; // 循环代码
            this.lblSoreReal.Text = MiddleData.instnce.CurrentStatusData.PhaseIndex.ToString(); // 序号
            this.lblSoreaAll.Text = MiddleData.instnce.CurrentStatusData.AllPhaseIndex.ToString(); // 总序号
            this.lblGKNo.Text = MiddleData.instnce.CurrentStatusData.GKBH.ToString(); // 工况编号
            this.lblSetRpm.Text = MiddleData.instnce.CurrentStatusData.Speed.ToString(); // 转速
            this.lblSetCurrent.Text = MiddleData.instnce.CurrentStatusData.ExcitationCurrent.ToString(); // 励磁电流
            this.lblSetTime.Text = MiddleData.instnce.CurrentStatusData.TargetOperationTime.ToString(); // 工况时间
            this.lblSetPower.Text = MiddleData.instnce.CurrentStatusData.TargetPower.ToString(); // 目标功率
            if (MiddleData.instnce.CurrentStatusData.Sore == 1 && MiddleData.instnce.CurrentStatusData.PhaseIndex == 1 && MiddleData.instnce.CurrentStatusData.StepTime == 0)
            {
                MiddleData.instnce.CurrentStatusData.StepTimeTotal = 0;
                MiddleData.instnce.CurrentStatusData.Save();
            }

            // 更新时间显示
            UpdateTimeView();

            // 取消之前的高亮（恢复默认颜色）
            if (PreviousSelectRow - 1 >= 0 && PreviousSelectRow - 1 < this.dgvAuto.Rows.Count)
            {
                this.dgvAuto.Rows[PreviousSelectRow - 1].DefaultCellStyle.BackColor = Color.White;
            }

            // 设置新的高亮行，但不设置当前单元格（避免影响编辑）
            if (MiddleData.instnce.CurrentStatusData.Sore - 1 >= 0 && MiddleData.instnce.CurrentStatusData.Sore - 1 < this.dgvAuto.Rows.Count)
            {
                this.dgvAuto.Rows[MiddleData.instnce.CurrentStatusData.Sore - 1].DefaultCellStyle.BackColor = Color.Yellow;

                // 只滚动到可见区域，不设置当前单元格
                if (this.dgvAuto.FirstDisplayedScrollingRowIndex < 0 ||
                    MiddleData.instnce.CurrentStatusData.Sore - 1 < this.dgvAuto.FirstDisplayedScrollingRowIndex ||
                    MiddleData.instnce.CurrentStatusData.Sore - 1 >= this.dgvAuto.FirstDisplayedScrollingRowIndex + this.dgvAuto.DisplayedRowCount(false))
                {
                    this.dgvAuto.FirstDisplayedScrollingRowIndex =
                        Math.Max(0, MiddleData.instnce.CurrentStatusData.Sore - 1 - this.dgvAuto.DisplayedRowCount(false) / 2);
                }
            }

            PreviousSelectRow = MiddleData.instnce.CurrentStatusData.Sore;

            //// 高亮主流程表格
            //if (PreviousSelectRow - 1 >= 0)
            //{
            //    this.GridViewStepAll.Rows[PreviousSelectRow - 1].DefaultCellStyle.BackColor = Color.White;
            //    this.GridViewStepAll.Rows[PreviousSelectRow - 1].DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            //}

            //this.GridViewStepAll.ClearSelection();
            //this.GridViewStepAll.Rows[MiddleData.instnce.CurrentStatusData.Sore - 1].Selected = true;
            //this.GridViewStepAll.Rows[MiddleData.instnce.CurrentStatusData.Sore - 1].DefaultCellStyle.BackColor = Color.Yellow;
            //this.GridViewStepAll.FirstDisplayedScrollingRowIndex = MiddleData.instnce.CurrentStatusData.Sore - 1;

            // 设置工况表高亮
            this.ucStepStatus1.ClearAllHighlights();
            this.ucStepStatus1.HighlightStepRow(MiddleData.instnce.CurrentStatusData.Sore - 1, MiddleData.instnce.CurrentStatusData.PhaseIndex - 1);

            PreviousSelectRow = MiddleData.instnce.CurrentStatusData.Sore;
        }

        // 自动试验线程
        Thread th;
        /// <summary>
        /// 试验开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnStart_Click(object sender, EventArgs e)
        {
            // todo 前提条件（后续添加）
            int msgIndex = 0;
            StringBuilder errorMessages = new StringBuilder();

            // 检查水阻升降上极限检测
            if (Common.mTestViewModel.ModelName == "")
            {
                errorMessages.AppendLine($"{++msgIndex}. 请选择型号。");
            }
            if (string.IsNullOrWhiteSpace(Common.mTestViewModel.ModelNo))
            {
                errorMessages.AppendLine($"{++msgIndex}. 请输入编号。");
            }
            if (!Common.DIgrp["紧急停止"])
            {
                errorMessages.AppendLine($"{++msgIndex}. 急停状态下禁止试验。");
            }
            // 如果有错误信息，统一显示
            if (errorMessages.Length > 0)
            {
                string message = $"不满足开始试验条件：\n\n" + errorMessages;

                if (frmStartupMessage != null && !frmStartupMessage.IsDisposed && frmStartupMessage.Visible)
                {
                    // 窗体已显示，只更新消息
                    frmStartupMessage.Msg = message;
                }
                else
                {
                    // 创建新窗体或重用
                    if (frmStartupMessage != null && !frmStartupMessage.IsDisposed)
                    {
                        frmStartupMessage.Close();
                        frmStartupMessage.Dispose();
                    }

                    frmStartupMessage = new frmMessageWarning
                    {
                        Msg = message
                    };
                    frmStartupMessage.ShowDialog();
                }
                return;
            }

            bool Is360 = true;
            if (this.btnXN.Switch)
            {
                Is360 = false;
            }

            ucAutoStepSelect ucAutoStepSelect = new ucAutoStepSelect(Is360 ? "耐久试验" : "性能试验", this.txtChuchanghao.Text);
            ucAutoStepSelect.ShowDialog();
            if (ucAutoStepSelect.DialogResult == DialogResult.No) return;

            // 刷新一次参数
            MiddleData.instnce.CurrentStatusData = new CurrentStatusConfig(Common.mTestViewModel.ModelName, MiddleData.instnce.testDataView.TestName);

            // 更改页面状态
            UpdateLastStepData();

            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;
            this.btnPause.Enabled = true;
            this.lblStatus.Text = "试验中";

            // 刷新界面，刷新参数
            //InitializeDataGridView();

            // 启动实时曲线记录
            //this.ucWaveTorqueNew.StartRealtime();


            // 线程执行
            th = new Thread(new ThreadStart(AutoTestStart));
            th.Name = MiddleData.instnce.testDataView.TestName + "线程";
            th.Start();

            // 打开曲线采集计时器
            this.AutoCollectTestTimer.Enabled = true;
            this.AutoCollectTestTimer.Start();
        }

        /// <summary>
        /// 测试开始逻辑
        /// </summary>
        /// <returns></returns>
        private void AutoTestStart()
        {
            try
            {
                var testName = MiddleData.instnce.testDataView.TestName;
                // 设置为开始
                MiddleData.instnce.testDataView.IsTest = true;

                // 打开试验总计时
                BeginWatch();

                dicBase["自动试验"].hmi = this;
                var isOk = dicBase["自动试验"].Execute();

                this.Invoke(new Action(() =>
                {
                    MiddleData.instnce.testDataView.IsTest = false;

                    this.btnStart.Enabled = true;
                    this.btnStop.Enabled = false;
                    this.btnPause.Enabled = false;
                    this.grpTestProc.Enabled = true;

                    // 设置控件状态
                    StopWatch();
                    StopPauseWatch();

                    // 关闭曲线记录
                    this.AutoCollectTestTimer.Stop();
                    // 停止实时曲线
                    //this.ucWaveTorqueNew.StopRealtime();
                }));

                // 暂时注释

                //// 重置当前步骤索引
                //int currentStepIndex = 0;

                //// 启动自动测试循环
                //while (currentStepIndex < AutoTestStepList.Count && MiddleData.instnce.testDataView.IsTest)
                //{
                //    var currentStep = AutoTestStepList[currentStepIndex];
                //    MiddleData.instnce.testDataView.Sore = currentStep.Sore;
                //    MiddleData.instnce.testDataView.StepName = currentStep.StepName;

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

                //MiddleData.instnce.testDataView.IsTest = false;

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

            while (elapsedSeconds < targetSeconds && MiddleData.instnce.testDataView.IsTest)
            {
                // 等待1秒
                await Task.Delay(1000);

                // 检查是否被中止
                // todo 后续再补全，故障时也要退出提示
                if (!MiddleData.instnce.testDataView.IsTest)
                    return false;

                elapsedSeconds++;
            }

            // 正常做完
            return elapsedSeconds >= targetSeconds;
        }

        /// <summary>
        /// 结束时更新数据表格显示（弃用）
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
                    this.lblSoreReal.Text = step.Sore.ToString();
                    this.lblXHDMReal.Text = step.TestName;

                    // 获取表格的试验行
                    var row = dataGridLoopCode.Rows[step.Sore - 1];
                    row.DefaultCellStyle.BackColor = Color.Yellow;

                    // 更新开始时间
                    row.Cells["StarDateTime"].Value = step.StarDateTime.ToString("MM-dd HH:mm:ss");
                    // 更新状态
                    row.Cells["TestStatus"].Value = GetStatusText(step.TestStatus);

                    //自动滚动到当前行（确保在视野中）
                    row.DataGridView.FirstDisplayedScrollingRowIndex = step.Sore - 1;
                    row.DataGridView.CurrentCell = row.Cells[0];

                    // 保存到ini里
                    MiddleData.instnce.CurrentStatusData.Sore = step.Sore;
                    MiddleData.instnce.CurrentStatusData.PhaseIndex = step.StepIndex;
                    MiddleData.instnce.CurrentStatusData.PhaseName = step.TestName;
                    MiddleData.instnce.CurrentStatusData.CycleName = step.CycleName;
                    MiddleData.instnce.CurrentStatusData.NodeName = step.StepName;
                    MiddleData.instnce.CurrentStatusData.PhaseCollectTime = step.CollectIntervalTime;
                    MiddleData.instnce.CurrentStatusData.Save();

                    break;

                // 正常结束
                case TestStatusEnum.Success:
                    var successRow = dataGridLoopCode.Rows[step.Sore - 1];
                    successRow.DefaultCellStyle.BackColor = Color.LightGreen;

                    // 更新状态
                    successRow.Cells["EndDateTime"].Value = DateTime.Now.ToString("MM-dd HH:mm:ss");
                    // 更新状态
                    successRow.Cells["TestStatus"].Value = GetStatusText(step.TestStatus);

                    break;

                // 异常结束
                case TestStatusEnum.Stop:
                    var stopRow = dataGridLoopCode.Rows[step.Sore - 1];
                    stopRow.DefaultCellStyle.BackColor = Color.Red;

                    // 更新状态
                    stopRow.Cells["TestStatus"].Value = GetStatusText(step.TestStatus);
                    stopRow.Cells["EndDateTime"].Value = DateTime.Now.ToString("MM-dd HH:mm:ss");
                    break;
            }

            dataGridLoopCode.Refresh();
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
                case TestStatusEnum.Pause: return "暂停中";
                case TestStatusEnum.Stop: return "已停止";
                case TestStatusEnum.Success: return "已完成";
                default: return "-";
            }
        }

        /// <summary>
        /// 停止测试
        /// </summary>
        public void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                var result = Var.MsgBoxYesNo(this, "确定要结束试验流程吗？");
                if (!result) return;

                // 将测试状态设置为false
                MiddleData.instnce.testDataView.IsTest = false;
                dicBase["自动试验"].IsTesting = false;
                this.lblStatus.Text = "试验终止";
                this.btnPause.Text = "暂停";

                // 记录最终采集统计
                TestLog.UpdateTestPara($"{DateTime.Now}：试验终止，总共采集{MiddleData.instnce.testDataView.TotalCollectCount}次数据");
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, $"停止出现异常：{ex.ToString()}");
            }
        }

        /// <summary>
        /// 数据刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoTestTimer_Tick(object sender, EventArgs e)
        {
            this.lblPowerReal.Text = MiddleData.instnce.EnginePower.ToString();
            this.lblRPMReal.Text = MiddleData.instnce.EngineSpeed.ToString();
            this.lblTSecond.Text = dicBase["自动试验"].TVar.TSecond.ToString();
            this.lblSetStepLC.Text = dicBase["自动试验"].TVar.Variable.ToString();

            //while (true)
            //{
            //    // 自动试验逻辑,怎么按照流程进行，
            //}
        }

        /// <summary>
        /// 曲线采集逻辑线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoCollectTestTimer_Tick(object sender, EventArgs e)
        {
            // 扭矩
            double[] values = new double[2];
            values[0] = MiddleData.instnce.testDataView.TestTorquePercent;
            values[1] = MiddleData.instnce.EngineTorquePercent;
            //this.ucWaveTorqueNew.AddRealtimeData(values); // 添加数据

            //this.ucWaveTorque.Waves[0].List
        }



        /// <summary>
        /// 停止总计时器（计时器重置）
        /// </summary>
        public void StopWatch(bool IsReset = true)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<bool>(StopWatch), IsReset);
                return;
            }

            if (IsReset)
            {
                watchAll.Reset();
            }

            watchAll.Stop();
            this.timerALL.Stop();
        }

        /// <summary>
        /// 重置计时器
        /// </summary>
        public void ResetWatch()
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action(ResetWatch));
                return;
            }

            watchAll.Reset();
            watchAll.Start();
            this.timerALL.Start();
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
        /// 开始暂停计时器
        /// </summary>
        public void StartPauseWatch()
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action(StartPauseWatch));
                return;
            }

            watchPause.Start();
            this.timerPause.Start();
        }

        /// <summary>
        /// 停止暂停计时器
        /// </summary>
        public void StopPauseWatch(bool IsReset = true)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<bool>(StopPauseWatch), IsReset);
                return;
            }

            if (IsReset)
            {
                watchPause.Reset();
            }

            watchPause.Stop();
            this.timerPause.Stop();
        }

        int LastRecordSecond = 0;
        public object locked = new object();
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
                // 分钟计时器
                TimeSpan watchTime = watchAll.Elapsed;
                MiddleData.instnce.testDataView.Second = (int)watchTime.TotalSeconds; // 秒 [0~59]
                if (MiddleData.instnce.testDataView.Second >= 60)
                {
                    // 大于60秒进1
                    lock (locked)
                    {
                        MiddleData.instnce.CurrentStatusData.StepTimeTotal++;
                        MiddleData.instnce.CurrentStatusData.StepTime++;
                        MiddleData.instnce.CurrentStatusData.Save();
                    }

                    // 记录曲线一个点
                    var nowDate = DateTime.Now;
                    AddTorqueData(nowDate, MiddleData.instnce.EngineTorquePercent, MiddleData.instnce.CurrentStatusData.TargetTorquePercent);
                    AddSpeedData(nowDate, MiddleData.instnce.EngineSpeedPercent, MiddleData.instnce.CurrentStatusData.TargetSpeedPercent);

                    // 重新开始计时器（从0开始）
                    ResetWatch();
                    MiddleData.instnce.testDataView.Second = 0;
                }

                // 异步存储自动试验数据
                if (LastRecordSecond != MiddleData.instnce.testDataView.Second)
                {
                    LastRecordSecond = MiddleData.instnce.testDataView.Second;
                    autoRecordService.StartRecord();
                }

                // 检查是否到达15秒且需要弹窗
                if (MiddleData.instnce.CurrentStatusData.StepTime >= MiddleData.instnce.CurrentStatusData.TargetOperationTime - 1 && MiddleData.instnce.testDataView.Second == 43 && !frmChangeInfo.IsOpen)
                {
                    // 使用Invoke确保在UI线程中执行
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            if (!frmChangeInfo.IsOpen)
                                frmChangeInfo.ShowInfo(MiddleData.instnce.testDataView.TestSpeedPercent, MiddleData.instnce.testDataView.TestTorquePercent, MiddleData.instnce.testDataView.TestNextSpeedPercent, MiddleData.instnce.testDataView.TestNextTorquePercent, MiddleData.instnce.testDataView.TestNextTime, MiddleData.instnce.testDataView.TestNextGKNo);
                        }));
                    }
                    else
                    {
                        if (!frmChangeInfo.IsOpen)
                            frmChangeInfo.ShowInfo(MiddleData.instnce.testDataView.TestSpeedPercent, MiddleData.instnce.testDataView.TestTorquePercent, MiddleData.instnce.testDataView.TestNextSpeedPercent, MiddleData.instnce.testDataView.TestNextTorquePercent, MiddleData.instnce.testDataView.TestNextTime, MiddleData.instnce.testDataView.TestNextGKNo);
                    }
                }

                UpdateTimeView(MiddleData.instnce.testDataView.Second);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 暂停工况时 计时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void timerPause_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!this.IsHandleCreated)
                return;

            try
            {
                // 分钟计时器
                TimeSpan watchTime = watchPause.Elapsed;
                MiddleData.instnce.testDataView.StopSecond = (int)watchTime.TotalSeconds; // 秒 [0~59]
                if (MiddleData.instnce.testDataView.StopSecond >= 60)
                {
                    // 大于60秒进1
                    MiddleData.instnce.testDataView.StopSecond++;
                    MiddleData.instnce.testDataView.StopMiunte++;

                    // 重新开始计时器（从0开始）
                    StopPauseWatch();
                    MiddleData.instnce.testDataView.StopSecond = 0;
                }

                // 获取停止步骤正计时
                TimeSpan stepTime = TimeSpan.FromMinutes(MiddleData.instnce.testDataView.StopMiunte);
                // 手动提取 拼接
                int stepTotalHours = (int)stepTime.TotalHours;  // 总小时数
                int stepMinutes = stepTime.Minutes;             // 分钟 [0~59]
                int stepSeconds = MiddleData.instnce.testDataView.StopSecond;                      // 秒 [0~59]

                // 格式化为：HH:mm:ss 
                string stepMainTime = $"{stepTotalHours:D2}:{stepMinutes:D2}:{stepSeconds:D2}";
                RW.Components.ControlHelper.Invoke(this, delegate
                {
                    this.lblStopTime.Text = stepMainTime;
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 更新时间显示
        /// </summary>
        public void UpdateTimeView(int seconds = 0)
        {
            TimeSpan totalTime = TimeSpan.FromMinutes(MiddleData.instnce.CurrentStatusData.StepTimeTotal);
            // 手动提取 拼接
            int totalHours = (int)totalTime.TotalHours;  // 总小时数
            int minutes = totalTime.Minutes;             // 分钟 [0~59]

            // 格式化为：HH:mm:ss 
            string mainTime = $"{totalHours:D2}:{minutes:D2}:{seconds:D2}";
            RW.Components.ControlHelper.Invoke(this, delegate
            {
                this.lblAllTime.Text = mainTime;
            });

            // 获取步骤正计时
            TimeSpan stepTime = TimeSpan.FromMinutes(MiddleData.instnce.CurrentStatusData.StepTime);
            // 手动提取 拼接
            int stepTotalHours = (int)stepTime.TotalHours;  // 总小时数
            int stepMinutes = stepTime.Minutes;             // 分钟 [0~59]
            int stepSeconds = seconds;                      // 秒 [0~59]

            // 格式化为：HH:mm:ss 
            string stepMainTime = $"{stepTotalHours:D2}:{stepMinutes:D2}:{stepSeconds:D2}";
            RW.Components.ControlHelper.Invoke(this, delegate
            {
                this.lblRemainTime.Text = stepMainTime;
            });
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

        /// <summary>
        /// 当采集间隔改变时重新计算下一次采集时间,切换大阶段时需要调用
        /// </summary>
        public void OnCollectIntervalChanged()
        {
            if (MiddleData.instnce.testDataView.IsTest && MiddleData.instnce.testDataView.CollectTime > 0)
            {
                // 重新计算下一次采集时间，基于当前时间
                var now = DateTime.Now;
                MiddleData.instnce.testDataView.NextCollectTime = now.AddMinutes(MiddleData.instnce.testDataView.CollectTime).AddSeconds(-1);

                TestLog.UpdateTestPara($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}：采集间隔已更改为{MiddleData.instnce.testDataView.CollectTime}分钟");
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
            string testName = currentStep?.TestName ?? MiddleData.instnce.testDataView.TestName;
            int sore = currentStep?.Sore ?? MiddleData.instnce.CurrentStatusData.Sore;

            return new object[]
            {
                 sore,                                          // 当前步骤序号
                 MiddleData.instnce.testDataView.NextCollectTime.ToString("yyyy-MM-dd"),  // 采集时间(日期)
                 MiddleData.instnce.testDataView.NextCollectTime.ToString("HH:mm:ss"),  // 采集时间（时间）
                 MiddleData.instnce.testDataView.StepNameCollectIndex,             // 阶段采集次数
                 testName,                                      // 试验阶段名称
                 stepName,                                      // 步骤名称
                 setSpeed.ToString("F1"),                       // 设定转速
                 setTorque.ToString("F1"),                      // 设定扭矩
                 actualSpeed.ToString("F1"),                    // 实际转速
                 actualTorque.ToString("F1"),                   // 实际扭矩
                 power.ToString("F1"),                          // 计算功率
                 MiddleData.instnce.testDataView.CollectTime.ToString("F1"),       // 采集间隔
                 "正常"                                         // 状态
            };
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
            if (MiddleData.instnce.testDataView.CollectTime <= 0 || !MiddleData.instnce.testDataView.IsTest)
                return;

            // 检查是否需要初始化下一次采集时间
            if (MiddleData.instnce.testDataView.NextCollectTime == DateTime.MinValue)
            {
                // 设置第一次采集时间为当前时间
                var now = DateTime.Now;
                MiddleData.instnce.testDataView.NextCollectTime = now.AddMinutes(MiddleData.instnce.testDataView.CollectTime).AddSeconds(-1);
                return;
            }

            // 如果达到采集间隔时间，则采集数据
            if (DateTime.Now >= MiddleData.instnce.testDataView.NextCollectTime)
            {
                MiddleData.instnce.testDataView.IsCollecting = true;

                try
                {
                    // 获取当前试验状态
                    var currentStep = AutoTestStepList.FirstOrDefault(s => s.Sore == MiddleData.instnce.CurrentStatusData.Sore);

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
                    MiddleData.instnce.testDataView.TotalCollectCount++;
                    MiddleData.instnce.testDataView.StepNameCollectIndex++;

                    // 记录采集日志
                    string stepInfo = currentStep != null ?
                        $"步骤{currentStep.Sore}({currentStep.TestName})" : "未知步骤";

                    TestLog.UpdateTestPara($"{MiddleData.instnce.testDataView.NextCollectTime}：第{MiddleData.instnce.testDataView.TotalCollectCount}次采集 - {stepInfo}，采集间隔{MiddleData.instnce.testDataView.CollectTime}分钟");

                    // 采集完更新下次采集时间
                    MiddleData.instnce.testDataView.NextCollectTime = MiddleData.instnce.testDataView.NextCollectTime.AddMinutes(MiddleData.instnce.testDataView.CollectTime);

                    // 保存当前状态 阶段采集次数
                    MiddleData.instnce.CurrentStatusData.PhaseCollectNum = MiddleData.instnce.testDataView.StepNameCollectIndex;
                    MiddleData.instnce.CurrentStatusData.Save();
                }
                finally
                {
                    MiddleData.instnce.testDataView.IsCollecting = false;
                }
            }
        }

        public void timer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!this.IsHandleCreated)
                return;
            try
            {
                // 在做试验时，每隔X分钟采集一次数据进
                if (MiddleData.instnce.testDataView.IsTest && !MiddleData.instnce.testDataView.IsCollecting)
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


        bool isRecording = false;   //记录状态
        /// <summary>
        /// 手动记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecord_Click(object sender, EventArgs e)
        {
            isRecording = !isRecording;
            if (isRecording)
            {
                //开始记录
                //改变按钮样式
                this.btnRecord.Switch = true;
                this.btnRecord.Text = "停止记录";
                this.nudRecordFrequency.Enabled = false;

                //传递柴油机试验数据
                Dictionary<string, string> saveInfo = new Dictionary<string, string>
                 {
                    { "Model", Common.mTestViewModel.ModelName},       //柴油机型号
                    { "No",this.txtChuchanghao.Text},    //柴油机编号
                    { "MaxIndex",(this.dgvManualRecord.Rows.Count + 1).ToString()},  //下一个序号
                    { "recordFrequency",this.nudRecordFrequency.Value.ToString() },     //记录频率
                    { "TestName", this.btnXN.Switch ? "100h" : "360h" },     //试验项点 
                 };

                //BuildDgvManualRecord();

                // 订阅数据保存事件
                manualRecordService.DataSaved += ManualRecordService_DataSaved;
                var result = manualRecordService.StartRecord(saveInfo);
                if (!result)
                {
                    Var.MsgBoxWarn(this, "记录数据出现异常！！");
                }
            }
            else
            {
                //停止记录时取消订阅
                manualRecordService.DataSaved -= ManualRecordService_DataSaved;
                //停止记录
                manualRecordService.StopRecord();
                //改变按钮样式
                this.btnRecord.Text = "记录";
                this.btnRecord.Switch = false;
                this.nudRecordFrequency.Enabled = true;
                //更新表格
                //BuildDgvManualRecord();
            }
        }

        /// <summary>
        /// 单条数据添加后触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManualRecordService_DataSaved(object sender, ManualRecordService.DataSavedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, ManualRecordService.DataSavedEventArgs>(ManualRecordService_DataSaved), sender, e);
                return;
            }

            RefreshAddedRecord(e.manualRecord);
        }

        /// <summary>
        /// 更新新添加的单条记录到手动记录表格中
        /// </summary>
        private void RefreshAddedRecord(ManualRecordPara addedRecord)
        {
            //添加到手动记录表格中 倒叙
            this.dgvManualRecord.Rows.Insert(0,
                addedRecord.Index,
                addedRecord.RecordName,
                addedRecord.TestName,
                addedRecord.TestStage,
                addedRecord.TestCycle,
                addedRecord.TestStep,
                addedRecord.DataTime,
                addedRecord.Time,
                addedRecord.HourNum,
                addedRecord.RecordDataTime,
                addedRecord.AT,     // 1 环境温度
                addedRecord.AP,     // 2 大气压力
                addedRecord.AH,     // 3 空气湿度
                addedRecord.RPM,    // 4 柴油机转速
                addedRecord.Torque, // 5 柴油机有效扭矩
                addedRecord.Power,  // 6 柴油机有效功率

                addedRecord.LWaterTempIn,      // 7 中冷水进机温度
                addedRecord.HWaterTempIn,      // 8 高温水进机温度
                addedRecord.LWaterTempOut,     // 9 中冷水出机温度
                addedRecord.HWaterTempOut,     // 10 高温水出机温度
                addedRecord.EngineWaterTempIn, // 11 机油热交换器进口水温
                addedRecord.EngineWaterTempOut,// 12 机油热交换器出口水温

                addedRecord.LPressureIn,  // 13 中冷水泵进口压力
                addedRecord.HPressureIn,  // 14 高温水泵进口压力
                addedRecord.LPressureOut, // 15 中冷水泵出口压力
                addedRecord.HPressureOut, // 16 高温水泵出口压力

                addedRecord.LWaterPressureOut, // 17 中冷水出机压力
                addedRecord.HWaterPressureOut, // 18 高温水出机压力

                addedRecord.HeatExchangerTempIn,  // 19 机油热交换器进口油温
                addedRecord.HeatExchangerTempOut, // 20 机油热交换器出口油温

                addedRecord.EOPressure2,  // 21 主油道末端油压
                addedRecord.EOPressure1,  // 22 机油泵出口油压
                addedRecord.EngineOilOutletTemp, // 23 机油泵出口油温
                addedRecord.EOAnalysis,   // 24 机油分析
                addedRecord.EOConsumption,// 25 机油消耗

                addedRecord.FrontAirTempIn,   // 26 前中冷前空气温度
                addedRecord.AfterAirTempIn,   // 27 后中冷前空气温度
                addedRecord.FrontAirTempOut,  // 28 前中冷后空气温度
                addedRecord.AfterAirTempOut,  // 29 后中冷后空气温度

                addedRecord.FrontAirPressureIn,   // 30 前中冷前空气压力
                addedRecord.AfterAirPressureIn,   // 31 后中冷前空气压力
                addedRecord.FrontAirPressureOut,  // 32 前中冷后空气压力
                addedRecord.AfterAirPressureOut,  // 33 后中冷后空气压力

                addedRecord.FrontTurbochargerRPM,    // 34 前增压器转速
                addedRecord.AfterTurbochargerRPM,    // 35 后增压器转速
                addedRecord.FrontTurbochargerPressureIn,  // 36 前增压器进气真空度
                addedRecord.AfterTurbochargerPressureIn,  // 37 后增压器进气真空度
                addedRecord.FrontTurbochargerPressureOut, // 38 前增压器排气背压
                addedRecord.AfterTurbochargerPressureOut, // 39 后增压器排气背压

                addedRecord.EGTempA1, // 40 A1缸排气温度
                addedRecord.EGTempA2, // 41 A2缸排气温度
                addedRecord.EGTempA3, // 42 A3缸排气温度
                addedRecord.EGTempA4, // 43 A4缸排气温度
                addedRecord.EGTempA5, // 44 A5缸排气温度
                addedRecord.EGTempA6, // 45 A6缸排气温度
                addedRecord.EGTempA7, // 46 A7缸排气温度
                addedRecord.EGTempA8, // 47 A8缸排气温度

                addedRecord.EGTempB1, // 48 B1缸排气温度
                addedRecord.EGTempB2, // 49 B2缸排气温度
                addedRecord.EGTempB3, // 50 B3缸排气温度
                addedRecord.EGTempB4, // 51 B4缸排气温度
                addedRecord.EGTempB5, // 52 B5缸排气温度
                addedRecord.EGTempB6, // 53 B6缸排气温度
                addedRecord.EGTempB7, // 54 B7缸排气温度
                addedRecord.EGTempB8, // 55 B8缸排气温度

                addedRecord.FrontTurbochargerTempIn,   // 56 前涡轮进口废气温度
                addedRecord.AfterTurbochargerTempIn,   // 57 后涡轮进口废气温度
                addedRecord.FrontTurbochargerTempOut,  // 58 前涡轮出口废气温度
                addedRecord.AfterTurbochargerTempOut,  // 59 后涡轮出口废气温度

                addedRecord.FrontTurbochargerPressureIn2,  // 60 前涡轮进口废气压力
                addedRecord.AfterTurbochargerPressureIn2,  // 61 后涡轮进口废气压力

                addedRecord.Smoke,           // 62 烟度
                addedRecord.ECOTime,         // 63 燃油消耗时间
                addedRecord.ECOQuantity,     // 64 燃油消耗量
                addedRecord.ECORate,         // 65 燃油消耗率
                addedRecord.OilTempIn,       // 66 燃油泵进口油温
                addedRecord.InjectionParameter // 67 喷射参数
            );
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearDataGridView_Click(object sender, EventArgs e)
        {
            var result = Var.MsgBoxYesNo(this, "确定要清空数据吗？");
            if (!result) return;

            if (this.btnRecord.Switch)
            {
                btnRecord_Click(null, null);
            }

            this.dgvManualRecord.Rows.Clear();
            manualRecordService.MGid = null;
        }

        /// <summary>
        /// 间隔时间更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        private void nudRecordFrequency_ValueChanged(object sender, double value)
        {
            manualRecordService.Second = value.ToInt();
        }

        /// <summary>
        /// 100h 360h 模式切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoChange_Click(object sender, EventArgs e)
        {
            try
            {
                var button = sender as RButton;
                var result = Var.MsgBoxYesNo(this, "确定要切换试验模式吗？\r\n请确保试验报表已保存。");
                if (!result) return;

                if (dicBase["自动试验"].IsTesting)
                {
                    Var.MsgBoxWarn(this, "试验中禁止切换试验模式。");
                    return;
                }

                // 切换
                string tagValue = button.Tag?.ToString();
                bool Is100h = tagValue == "100h";
                string nameView = Is100h ? "性能试验" : "耐久试验";

                // 更新工况表
                gkConfig = new GKConfig(tagValue);

                this.btnXN.Switch = Is100h;
                this.btnNJ.Switch = !Is100h;
                this.lblAutoName.Text = $"{tagValue} 步骤";
                MiddleData.instnce.testDataView.TestName = nameView;

                // 根据当前选择的类型刷新
                FreshStepView();
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, $"100h 360h 模式切换出现异常： {ex.ToString()}");
            }
        }

        private void btnSetSpeed_Click(object sender, EventArgs e)
        {
            this.btnSetSpeed.Focus();
            Common.AOgrp["发动机油门调节"] = ucNudSpeed.Value;
        }

        private void btnSetSpeedReduce_Click(object sender, EventArgs e)
        {
            this.btnSetSpeedReduce.Focus();
            var button = sender as RButton;
            var tag = button.Tag.ToInt();

            var val = Common.AOgrp["发动机油门调节"] - tag;
            if (val <= 0)
            {
                val = 0;
            }
            Common.AOgrp["发动机油门调节"] = val;
            this.ucNudSpeed.Value = val;
        }

        private void btnSetSpeedAdd_Click(object sender, EventArgs e)
        {
            this.btnSetSpeedAdd.Focus();
            var button = sender as RButton;
            var tag = button.Tag.ToInt();

            var val = Common.AOgrp["发动机油门调节"] + tag;
            if (val >= 1100)
            {
                val = 1100;
            }

            Common.AOgrp["发动机油门调节"] = val;
            this.ucNudSpeed.Value = val;
        }

        /// <summary>
        /// 手动励磁调节
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetLC_Click(object sender, EventArgs e)
        {
            this.btnSetLC.Focus();
            Common.AOgrp["励磁调节"] = ucNudLC.Value;
        }

        /// <summary>
        /// 励磁归0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetLCZero_Click(object sender, EventArgs e)
        {
            this.btnSetLCZero.Focus();
            Common.AOgrp["励磁调节"] = 0;
            this.ucNudLC.Value = 0;
        }

        private void btnSetLCReduce_Click(object sender, EventArgs e)
        {
            this.btnSetLCReduce.Focus();

            var val = Common.AOgrp["励磁调节"] - 1;
            if (val <= 0)
            {
                val = 0;
            }
            Common.AOgrp["励磁调节"] = val;
            this.ucNudLC.Value = val;
        }

        private void btnSetLCAdd_Click(object sender, EventArgs e)
        {
            this.btnSetLCAdd.Focus();

            var val = Common.AOgrp["励磁调节"] + 1;
            if (val >= 500)
            {
                val = 500;
            }
            Common.AOgrp["励磁调节"] = val;
            this.ucNudLC.Value = val;
        }

        /// <summary>
        /// 暂停测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPause_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button.Text == "暂停")
            {
                button.Text = "继续";
                dicBase["自动试验"].TestingStatus = TestStatusEnum.Pause;
                // 停止正常计时
                StopWatch(false);
                // 开始暂停计时器
                StartPauseWatch();
            }
            else
            {
                button.Text = "暂停";

                // 点击继续后，把励磁调节稳定时间设置为0
                dicBase["自动试验"].TVar.TSecond = 0;

                dicBase["自动试验"].TestingStatus = TestStatusEnum.IsTest;
                // 开始正常计时
                BeginWatch();
                // 停止暂停计时器
                StopPauseWatch(false);
            }

        }

        /// <summary>
        /// 重新开始本工况
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestartGK_Click(object sender, EventArgs e)
        {
            if (!dicBase["自动试验"].IsTesting)
            {
                Var.MsgBoxWarn(this, "试验未开始！");
                return;
            }

            MiddleData.instnce.CurrentStatusData.StepTimeTotal = MiddleData.instnce.CurrentStatusData.StepTimeTotal - MiddleData.instnce.CurrentStatusData.StepTime;
            MiddleData.instnce.CurrentStatusData.StepTime = 0;
            MiddleData.instnce.CurrentStatusData.Save();

            StopPauseWatch();
            ResetWatch();
            dicBase["自动试验"].TVar.TSecond = 0;
            dicBase["自动试验"].TVar.Second = 0;
            dicBase["自动试验"].TVar.Minute = 0;
        }

        #region 处理曲线问题
        /// <summary>
        /// 初始化曲线控件
        /// </summary>
        public void InitWave()
        {
            DateTime dt = DateTime.Now;

            #region 扭矩曲线
            UILineOption option = new UILineOption();

            List<WaveData> waveDatasTorque = new List<WaveData>()
            {
                new WaveData(){ CurveName= "扭矩"},
                new WaveData(){ CurveName= "设定扭矩"},
            };
            var torqueData = new WaveReocrd
            {
                ReocrdName = "扭矩曲线",
                CurrentType = true,
                WaveDataPoints = waveDatasTorque
            };
            waveReocrd.AddOrUpdate("扭矩曲线", torqueData, (k, oldValue) => torqueData);

            //设置图表边框空白部分尺寸
            option.Grid.Left = 50;
            option.Grid.Right = 30;
            option.Grid.Top = 48;
            option.Grid.Bottom = 32;

            option.ToolTip.Visible = false;
            option.Title = new UITitle();
            option.Title.Text = "扭矩曲线";
            //option.Title.SubText = "LineChart";

            // 设置图例显示
            option.Legend = new UILegend();
            option.Legend.AddData("扭矩", Color.Red);  // 启用图例
            option.Legend.AddData("设定扭矩", Color.Blue);  // 启用图例
            option.Legend.Top = 0;           // 顶部对齐
            option.Legend.Left = UILeftAlignment.Left;  // 左对齐

            option.XAxisType = UIAxisType.DateTime;

            var series = option.AddSeries(new UILineSeries("设定扭矩", Color.Blue));
            series.Symbol = UILinePointSymbol.None;
            series.SymbolSize = 4;
            series.SymbolLineWidth = 2;
            series.ShowLine = true;

            series = option.AddSeries(new UILineSeries("扭矩", Color.Red));
            series.Symbol = UILinePointSymbol.None;
            series.SymbolSize = 4;
            series.SymbolLineWidth = 2;
            series.ShowLine = true;

            option.YAxis.Name = "扭矩(%)";
            //X轴坐标轴显示格式化
            option.XAxis.AxisLabel.DateTimeFormat = "yyyy-MM-dd HH:mm";
            // 设置X轴间隔为10分钟
            //option.XAxis.Interval = 10 * 60 * 1000;  // 10分钟，单位是毫秒

            //Y轴坐标轴显示小数位数
            option.YAxis.AxisLabel.DecimalPlaces = 0;
            // 设置Y轴最大值（关键代码）
            option.YAxis.SetRange(0, 110);  // Y轴范围0-100，最大值设为100
            //设置X轴显示范围
            option.XAxis.SetRange(dt, dt.AddMinutes(121));
            LineChartTorque.SetOption(option);
            #endregion

            #region 转速曲线
            List<WaveData> waveDatasSpeed = new List<WaveData>()
            {
                new WaveData(){ CurveName= "转速"},
                new WaveData(){ CurveName= "设定转速"},
            };
            var speedData = new WaveReocrd
            {
                ReocrdName = "转速曲线",
                CurrentType = true,
                WaveDataPoints = waveDatasSpeed
            };
            waveReocrd.AddOrUpdate("转速曲线", speedData, (k, oldValue) => speedData);

            UILineOption speedOption = new UILineOption();
            //设置图表边框空白部分尺寸
            speedOption.Grid.Left = 50;
            speedOption.Grid.Right = 30;
            speedOption.Grid.Top = 48;
            speedOption.Grid.Bottom = 32;

            speedOption.ToolTip.Visible = false;
            speedOption.Title = new UITitle();
            speedOption.Title.Text = "转速曲线";

            // 设置图例显示
            speedOption.Legend = new UILegend();
            speedOption.Legend.AddData("转速", Color.Red);
            speedOption.Legend.AddData("设定转速", Color.Blue);
            speedOption.Legend.Top = 0;
            speedOption.Legend.Left = UILeftAlignment.Left;

            speedOption.XAxisType = UIAxisType.DateTime;

            // 添加设定转速曲线
            var speedSeries = speedOption.AddSeries(new UILineSeries("设定转速", Color.Blue));
            speedSeries.Symbol = UILinePointSymbol.None;
            speedSeries.SymbolSize = 4;
            speedSeries.SymbolLineWidth = 2;
            speedSeries.ShowLine = true;

            // 添加实际转速曲线
            speedSeries = speedOption.AddSeries(new UILineSeries("转速", Color.Red));
            speedSeries.Symbol = UILinePointSymbol.None;
            speedSeries.SymbolSize = 4;
            speedSeries.SymbolLineWidth = 2;
            speedSeries.ShowLine = true;

            speedOption.YAxis.Name = "转速(%)";
            // X轴坐标轴显示格式化
            speedOption.XAxis.AxisLabel.DateTimeFormat = "yyyy-MM-dd HH:mm";
            // Y轴坐标轴显示小数位数
            speedOption.YAxis.AxisLabel.DecimalPlaces = 0;
            // 设置Y轴最大值
            speedOption.YAxis.SetRange(0, 110);
            // 设置X轴显示范围（2小时范围）
            speedOption.XAxis.SetRange(dt, dt.AddMinutes(121));

            LineChartSpeed.SetOption(speedOption);
            #endregion
        }

        /// <summary>
        /// 添加实时数据到曲线（通用方法）
        /// </summary>
        /// <param name="chart">曲线控件</param>
        /// <param name="waveName">所属曲线类名称</param>
        /// <param name="seriesName">曲线名称</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="value">数值</param>
        public void AddRealTimeData(UILineChart chart, string waveName, string seriesName, DateTime timestamp, double value)
        {
            // 添加曲线数据
            chart.Option.AddData(seriesName, timestamp, value);

            // 从字典中获取对象后再新增数据
            waveReocrd.TryGetValue(waveName, out var WRecord);
            var WData = WRecord.WaveDataPoints.FirstOrDefault(d => d.CurveName == seriesName);
            WData.DataPoints.Add(new DataPoint() { Timestamp = timestamp, Value = value });

            // 获取指定系列的数据长度
            if (chart.Option.Series.TryGetValue(seriesName, out var series))
            {
                int dataCount = series.DataCount;

                // 最长实时数据只显示2小时
                if (dataCount > 120 && WRecord.CurrentType)
                {
                    // 调整X轴范围显示最新2小时数据
                    chart.Option.XAxis.SetRange(DateTime.Now.AddMinutes(-120), DateTime.Now.AddMinutes(1));
                }
            }

            chart.Refresh();
        }

        /// <summary>
        /// 添加扭矩实时数据
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="actualTorque">实时扭矩</param>
        /// <param name="setTorque">设定扭矩</param>
        public void AddTorqueData(DateTime timestamp, double actualTorque, double setTorque)
        {
            AddRealTimeData(LineChartTorque, "扭矩曲线", "扭矩", timestamp, actualTorque);
            AddRealTimeData(LineChartTorque, "扭矩曲线", "设定扭矩", timestamp, setTorque);
        }

        /// <summary>
        /// 添加转速实时数据
        /// </summary>
        public void AddSpeedData(DateTime timestamp, double actualSpeed, double setSpeed)
        {
            AddRealTimeData(LineChartSpeed, "转速曲线", "转速", timestamp, actualSpeed);
            AddRealTimeData(LineChartSpeed, "转速曲线", "设定转速", timestamp, setSpeed);
        }

        /// <summary>
        /// 更改曲线类型(实时曲线)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCurrentWave_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            waveReocrd.TryGetValue(button.Tag.ToString(), out var WRecord);
            WRecord.CurrentType = true;

            var chart = GetChartByWaveName(button.Tag.ToString());
            if (WRecord.WaveDataPoints[0].DataPoints.Count > 0)
            {
                // 调整X轴范围显示最新2小时数据
                chart.Option.XAxis.SetRange(DateTime.Now.AddMinutes(-120), WRecord.WaveDataPoints[0].DataPoints[WRecord.WaveDataPoints[0].DataPoints.Count - 1].Timestamp.AddMinutes(1));
                chart.Refresh();
            }
        }

        /// <summary>
        /// 更改曲线类型(历史曲线)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHistoryWave_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            waveReocrd.TryGetValue(button.Tag.ToString(), out var WRecord);
            WRecord.CurrentType = false;

            var chart = GetChartByWaveName(button.Tag.ToString());

            if (WRecord.WaveDataPoints[0].DataPoints.Count > 0)
            {
                // 历史曲线查询所有数据
                chart.Option.XAxis.SetRange(WRecord.WaveDataPoints[0].DataPoints[0].Timestamp, WRecord.WaveDataPoints[0].DataPoints[WRecord.WaveDataPoints[0].DataPoints.Count - 1].Timestamp.AddMinutes(1));
                chart.Refresh();
            }
        }

        /// <summary>
        /// 通过时间查询曲线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTimeWave_Click(object sender, EventArgs e)
        {
            try
            {
                var button = sender as Button;
                string waveName = button.Tag.ToString();

                // 从字典中获取曲线记录
                if (waveReocrd.TryGetValue(waveName, out var WRecord))
                {
                    // 弹出时间选择对话框
                    using (var timeForm = new FrmTimePick())
                    {
                        if (timeForm.ShowDialog() == DialogResult.OK)
                        {
                            DateTime startTime = timeForm.StartTime;
                            DateTime endTime = timeForm.EndTime;

                            // 验证时间范围
                            if (startTime >= endTime)
                            {
                                UIMessageBox.ShowError("开始时间必须小于结束时间！");
                                return;
                            }

                            // 根据曲线名称确定对应的图表控件
                            UILineChart chart = GetChartByWaveName(waveName);
                            if (chart == null)
                            {
                                UIMessageBox.ShowError($"未找到对应的图表控件：{waveName}");
                                return;
                            }

                            // 更新曲线类型为历史曲线
                            WRecord.CurrentType = false;

                            // 设置X轴范围为查询的时间范围
                            chart.Option.XAxis.SetRange(startTime, endTime);
                            chart.Refresh();
                        }
                    }
                }
                else
                {
                    UIMessageBox.ShowError($"未找到曲线记录：{waveName}");
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"查询曲线数据时出错：{ex.Message}");
            }
        }

        /// <summary>
        /// 根据曲线名称获取对应的图表控件
        /// </summary>
        private UILineChart GetChartByWaveName(string waveName)
        {
            if (waveName == "扭矩曲线")
            {
                return LineChartTorque;
            }
            else if (waveName == "转速曲线")
            {
                return LineChartSpeed;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据曲线名称获取对应的颜色
        /// </summary>
        private Color GetSeriesColor(string curveName)
        {
            if (curveName == "扭矩" || curveName == "转速")
            {
                return Color.Red;
            }
            else if (curveName == "设定扭矩" || curveName == "设定转速")
            {
                return Color.Blue;
            }
            else
            {
                return Color.Green;
            }
        }

        #endregion

        #region 表格更改事件

        /// <summary>
        /// 表格编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAuto_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // 获取列名来更安全地判断
            string columnName = this.dgvAuto.Columns[e.ColumnIndex].Name;

            // 只允许编辑励磁电流列
            if (columnName != "colLC")
            {
                e.Cancel = true;
                return;
            }

            // 允许编辑，清空输入法状态（防止中文输入）
            this.ImeMode = ImeMode.Disable;
        }

        /// <summary>
        /// 停止编辑时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAuto_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < this.dgvAuto.Rows.Count)
            {
                object originalValue = null;

                try
                {
                    // 恢复输入法状态
                    this.ImeMode = ImeMode.NoControl;

                    // 获取单元格
                    DataGridViewRow detailRow = this.dgvAuto.Rows[e.RowIndex];
                    DataGridViewCell cell = detailRow.Cells[e.ColumnIndex];
                    string columnName = this.dgvAuto.Columns[e.ColumnIndex].Name;

                    // 保存原始值用于错误恢复
                    originalValue = cell.Value;
                    string inputValue = cell.Value?.ToString() ?? "";

                    if (columnName == "colLC")
                    {
                        // 验证是否为数字（防止中文输入）
                        if (!ObjectCopier.IsValidNumber(inputValue))
                        {
                            Var.MsgBoxWarn(this, $"请输入有效的数字！当前输入：{inputValue}");
                            cell.Value = originalValue; // 恢复原始值
                            return;
                        }

                        double value = Convert.ToDouble(inputValue);

                        // 验证范围
                        if (value < 0 || value > 500)
                        {
                            Var.MsgBoxWarn(this, $"励磁电流范围：0-500！当前输入：{value}");
                            cell.Value = originalValue; // 恢复原始值
                            return;
                        }

                    }

                    double excitationValue = Convert.ToDouble(inputValue);
                    // 获取修改后的值
                    string gkNo = detailRow.Cells["GK"].Value.ToString();

                    // 根据当前试验模式更新对应的配置
                    var data = gkConfig.DurabilityDatas.FirstOrDefault(d => d.GKNo == gkNo);
                    if (data != null)
                    {
                        data.ExcitationCurrent = excitationValue;
                        gkConfig.Save();
                    }
                }
                catch (FormatException)
                {
                    Var.MsgBoxWarn(this, "输入格式错误，请输入有效的数字！");
                    // 恢复原始值
                    this.dgvAuto.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = originalValue;
                }
                catch (Exception ex)
                {
                    Var.MsgBoxWarn(this, "保存失败：" + ex.Message);
                    // 恢复原始值
                    this.dgvAuto.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = originalValue;
                }
            }
        }

        /// <summary>
        /// 单元格编辑时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAuto_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // 获取当前编辑的单元格
            if (this.dgvAuto.CurrentCell == null) return;

            int columnIndex = this.dgvAuto.CurrentCell.ColumnIndex;
            string columnName = this.dgvAuto.Columns[columnIndex].Name;

            // 只对RPM、Torque、RunTime列进行输入限制
            if (columnName == "ColLC")
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox != null)
                {
                    // 移除之前的事件处理器（防止重复添加）
                    textBox.KeyPress -= new KeyPressEventHandler(TextBox_KeyPress);
                    // 添加新的事件处理器
                    textBox.KeyPress += new KeyPressEventHandler(TextBox_KeyPress);

                    // 设置输入法为关闭状态
                    textBox.ImeMode = ImeMode.Disable;
                }
            }
        }

        private void dgvAuto_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvAuto.Rows.Count)
            {
                string columnName = dgvAuto.Columns[e.ColumnIndex].Name;

                // 只验证RPM、Torque、RunTime列
                if (columnName == "colLC")
                {
                    string value = e.FormattedValue?.ToString() ?? "";

                    if (string.IsNullOrWhiteSpace(value))
                    {
                        dgvAuto.Rows[e.RowIndex].ErrorText = "不能为空！";
                        e.Cancel = true;
                        return;
                    }

                    // 验证是否为数字
                    if (!ObjectCopier.IsValidNumber(value))
                    {
                        dgvAuto.Rows[e.RowIndex].ErrorText = "请输入有效的数字！";
                        e.Cancel = true;
                        return;
                    }

                    double doubleValue;
                    if (!double.TryParse(value, out doubleValue))
                    {
                        dgvAuto.Rows[e.RowIndex].ErrorText = "请输入有效的数字！";
                        e.Cancel = true;
                        return;
                    }

                    // 验证范围

                    if (doubleValue < 0 || doubleValue > 500)
                    {
                        dgvAuto.Rows[e.RowIndex].ErrorText = $"励磁电流范围：0-{500} A";
                        Var.MsgBoxWarn(this, dgvAuto.Rows[e.RowIndex].ErrorText);
                        e.Cancel = true;
                    }

                    // 清除错误提示
                    dgvAuto.Rows[e.RowIndex].ErrorText = "";
                }
            }
        }

        /// <summary>
        /// 文本框按键事件处理
        /// </summary>
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 允许退格键
            if (e.KeyChar == (char)8)
                return;

            // 允许小数点（只能有一个）
            if (e.KeyChar == '.')
            {
                TextBox textBox = sender as TextBox;
                if (textBox != null && textBox.Text.Contains("."))
                {
                    e.Handled = true; // 阻止输入多个小数点
                }
                return;
            }

            // 允许数字
            if (char.IsDigit(e.KeyChar))
                return;

            // 阻止其他所有字符
            e.Handled = true;
        }







        #endregion

    }

    /// <summary>
    /// 自动试验测试步骤类
    /// </summary>
    [Serializable]
    public class AutoTestStep : TestBasePara
    {
        /// <summary>
        /// 测试步骤序号
        /// </summary>
        public int Sore { get; set; }

        /// <summary>
        /// 测试状态
        /// </summary>
        public TestStatusEnum TestStatus { get; set; }

        /// <summary>
        /// 当前阶段 测试子步骤
        /// </summary>
        public int StepIndex { get; set; }

        /// <summary>
        /// 测试开始时间
        /// </summary>
        public DateTime StarDateTime { get; set; }

        /// <summary>
        /// 测试结束时间
        /// </summary>
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// 采集间隔时间
        /// </summary>

        public double CollectIntervalTime { get; set; }

        /// <summary>
        /// 步骤总时间（秒）
        /// </summary>
        public int TotalSeconds => (int)(RunTime * 60);
    }

    /// <summary>
    ///  测试状态枚举
    /// </summary>
    public enum TestStatusEnum
    {
        /// <summary>
        /// 未开始
        /// </summary>
        NotStarted,
        /// <summary>
        /// 试验中
        /// </summary>
        IsTest,
        /// <summary>
        /// 试验暂停
        /// </summary>
        Pause,
        /// <summary>
        /// 试验继续（作为暂停后的中转状态使用，用于复位变量）
        /// </summary>
        Continue,
        /// <summary>
        /// 故障（内部故障）
        /// </summary>
        Error,
        /// <summary>
        /// 正常结束-成功
        /// </summary>
        Success,
        /// <summary>
        /// 试验结束(人工点击结束)
        /// </summary>
        Stop,
    }
}

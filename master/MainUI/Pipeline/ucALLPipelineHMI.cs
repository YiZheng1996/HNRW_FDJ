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

using System.Linq;
using MainUI.BLL;
using System.Text;
using System.Data;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Management;

using System.Linq;
using MainUI.Properties;
using static MainUI.Modules.EventArgsModel;
using MainUI.Helper;

namespace MainUI
{
    public partial class ucALLPipelineHMI : UserControl
    {
        // 所有数据界面
        frmAllData frmAllDataHMI;

        ucFeulHMI ucFeulHMI = new ucFeulHMI();
        ucEOHMI2 ucEOHMI = new ucEOHMI2();
        ucWaterHMI2 ucWaterHMI = new ucWaterHMI2();
        ucControlHMI ucControlHMI = new ucControlHMI(); //单阀控制
        ucControlHMICycle ucControlHMICycle = new ucControlHMICycle(); //一键循环控制
        ucEngineControlHMI ucEngineControlHMI = new ucEngineControlHMI();

        // 按钮点击事件
        Dictionary<string, UIButton> ButtonDic = new Dictionary<string, UIButton>();
        Dictionary<string, UserControl> PilpDic = new Dictionary<string, UserControl>();

        public delegate void RunStatusHandler(bool obj);
        public event RunStatusHandler EmergencyStatusChanged;
        public AIGrp AIgrp = null;
        public AOGrp AOgrp = null;
        public DIGrp DIgrp = null;
        public DOGrp DOgrp = null;

        // 状态栏处理类
        private DeviceStatusProcessor _statusProcessor;
        // 型号参数
        ParaConfig paraconfig = null;

        Dictionary<string, BaseTest> dicBase = new Dictionary<string, BaseTest>();

        string rn = Environment.NewLine;
        private delegate void Del();

        private delegate void Del2(bool b);

        public ucALLPipelineHMI()
        {
            InitializeComponent();
        }


        [DllImport("user32.dll")]
        static extern int SystemParametersInfo(uint uiAction, uint uiParam, ref int pvParam, uint fWinIni);
        const uint SPI_SETSCROLLBARS = 98;
        const uint SPIF_UPDATEINIFILE = 0x01;


        public Dictionary<int, Sunny.UI.UILight> dicDI = new Dictionary<int, UILight>();

        //初始化
        public void Init()
        {
            try
            {
                // 初始化设备状态处理器
                _statusProcessor = new DeviceStatusProcessor(false);

                tslblUser.Text = "登录用户：" + RW.UI.RWUser.User.Username;
                tslblVersion.Text = Var.Version;
                statusStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
                tslblVersion.Alignment = ToolStripItemAlignment.Right;

                // 初始化字典
                ButtonDic.Add("高温水/低温水系统", this.btnWater);
                ButtonDic.Add("机油系统", this.btnJY);
                ButtonDic.Add("燃油系统", this.btnRY);
                ButtonDic.Add("管路控制", this.btnContronl);
                ButtonDic.Add("一键循环", this.btnCycle);
                ButtonDic.Add("发动机相关控制", this.btnMainContronl);

                ucWaterHMI.Font = new Font("宋体", 9f);
                ucEOHMI.Font = new Font("宋体", 9f);
                ucFeulHMI.Font = new Font("宋体", 9f);
                ucControlHMI.Font = new Font("宋体", 9f);
                ucControlHMICycle.Font = new Font("宋体", 9f);
                PilpDic.Add("高温水/低温水系统", ucWaterHMI);
                PilpDic.Add("机油系统", ucEOHMI);
                PilpDic.Add("燃油系统", ucFeulHMI);
                PilpDic.Add("管路控制", ucControlHMI);
                PilpDic.Add("一键循环", ucControlHMICycle);
                PilpDic.Add("发动机相关控制", ucEngineControlHMI);


                int scrollBarWidth = 20; // 设置新的滚动条宽度为20像素
                SystemParametersInfo(SPI_SETSCROLLBARS, 0, ref scrollBarWidth, SPIF_UPDATEINIFILE);

                this.panelSystem.Controls.Add(ucFeulHMI);
                this.panelSystem.Controls.Add(ucEOHMI);
                this.panelSystem.Controls.Add(ucWaterHMI);
                this.panelSystem.Controls.Add(ucControlHMI);
                this.panelSystem.Controls.Add(ucControlHMICycle);
                this.panelSystem.Controls.Add(ucEngineControlHMI);

                ucFeulHMI.Init();
                ucEOHMI.Init();
                ucWaterHMI.Init();
                ucControlHMI.Init();
                ucControlHMICycle.Init();
                ucEngineControlHMI.Init();

                Common.DIgrp.KeyValueChange += DIgrp_KeyValueChange;
                //Common.DIgrp.Fresh();

                // 默认显示水系统
                var key = "高温水/低温水系统";
                foreach (var item in ButtonDic)
                {
                    if (item.Key == key)
                    {
                        ButtonDic[item.Key].FillColor = Color.FromArgb(80, 160, 255);
                        PilpDic[item.Key].Visible = true;
                    }
                    else
                    {
                        PilpDic[item.Key].Visible = false;
                        ButtonDic[item.Key].FillColor = Color.FromArgb(173, 178, 181);
                    }
                }

                timer1.Enabled = true;
                timer1.Start();

                timerPLC.Enabled = true;
                timerPLC.Start();
                //BaseTest.hmiPipeline = this;

                //int moveIndex = 0;
                //// 打开另一个窗体
                //if (Screen.AllScreens.Count() > 1)
                //{
                //    moveIndex = 1;
                //}

                //// 如果窗体不存在则创建，否则激活
                //if (frmAllDataHMI == null || frmAllDataHMI.IsDisposed)
                //{
                //    frmAllDataHMI = new frmAllData();
                //    frmAllDataHMI.Show();
                //}
                //else
                //{
                //    frmAllDataHMI.Activate();
                //}
                //// 如果存在另一个窗体，弹出到另一个页面
                //MoveFormToMonitor(frmAllDataHMI, moveIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 数据监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DIgrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (e.Key == "紧急停止")
            {
                if (e.Value)
                {
                    this.picRunStatus.Image = Resources.normal;
                }
                else
                {
                    this.picRunStatus.Image = Resources.scram;
                }
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

                // 暂时注释
                // Common.DIgrp.Fresh();

            }
            catch (Exception ex)
            {
                string err = "初始化试验参数有误；具体原因：" + ex.Message;
                MessageBox.Show(err, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// 切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWater_Click(object sender, EventArgs e)
        {
            var button = sender as UIButton;
            var key = button.Tag.ToString();
            foreach (var item in ButtonDic)
            {
                if (item.Key == key)
                {
                    ButtonDic[item.Key].FillColor = Color.FromArgb(80, 160, 255);
                    PilpDic[item.Key].Visible = true;
                }
                else
                {
                    PilpDic[item.Key].Visible = false;
                    ButtonDic[item.Key].FillColor = Color.FromArgb(173, 178, 181);
                }
            }
        }

        /// <summary>
        /// 时间刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void timerPLC_Tick(object sender, EventArgs e)
        {
            try
            {
                // 1. 台位控制OPC
                bool IsPlcNg = _statusProcessor.FreshCommStatus(tslblPLC, "台位控制", Common.opcStatus.Simulated, Common.opcStatus.NoError);

                // 2. PLC测量OPC
                bool IsCkPlcNg = _statusProcessor.FreshCommStatus(tslblCKPLC, "测量柜", Common.AI2Grp.Simulated, Common.AI2Grp.NoError);

                // 3. 启动柜OPC
                bool IsStartNg = _statusProcessor.FreshCommStatus(tslblStart, "启动柜", Common.startPLCGrp.Simulated, Common.startPLCGrp.NoError);

                // 4. 发动机电参数
                bool IsElectricalNg = _statusProcessor.FreshCommStatus(tslblElectrical, "发动机电参数", Common.threePhaseElectric.Simulated, Common.threePhaseElectric.NoError);

                // 5-7. 机油箱系统 - 统一处理
                ProcessEngineOilSystem();

                // 燃油
                ProcessFeulSystem();

                // 水系统
                ProcessWaterSystem();

                // 11. 柴油机控制器 (注意：此处第二个参数固定为false，表示非仿真模式)
                bool IsTrdpNg = _statusProcessor.FreshCommStatus(tslblTRDP, "柴油机控制器", false, Var.TRDP.IsConnected);

                var IszcNg = false;
                var lsRYNg = false;
                var lsWeightNg = false;
                var IsBC = false;
                if (Var.SysConfig.ExeType == 1)
                {
                    // 如果是控制端
                    IszcNg = _statusProcessor.FreshCommStatus(tslblCommunication, "台位主从通讯", false, true);
                    lsRYNg = _statusProcessor.FreshCommStatus(tslblRYHY, "燃油耗仪", false, ET4500.Instance.IsConnected);
                    lsWeightNg = _statusProcessor.FreshCommStatus(tslblWeight, "称重仪", false, ZMPT650F.Instance.IsConnected);
                    IsBC = _statusProcessor.FreshCommStatus(tslblJYBC, "机油耗磅秤", false, YHA27.Instance.IsConnected);
                }
                else
                {
                    // 检测
                    IszcNg = _statusProcessor.FreshCommStatus(tslblCommunication, "台位主从通讯", Common.opcExChangeReceiveGrp.Simulated, Common.opcExChangeReceiveGrp.NoError);
                    lsRYNg = _statusProcessor.FreshCommStatus(tslblRYHY, "燃油耗仪", false, Common.opcExChangeReceiveGrp.GetDouble("油耗仪_NoError") == 1);
                    lsWeightNg = _statusProcessor.FreshCommStatus(tslblWeight, "称重仪", false, Common.opcExChangeReceiveGrp.GetDouble("称重仪_NoError") == 1);
                    IsBC = _statusProcessor.FreshCommStatus(tslblJYBC, "机油耗磅秤", false, Common.opcExChangeReceiveGrp.GetDouble("磅秤_NoError") == 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 处理机油箱系统
        /// </summary>
        private void ProcessEngineOilSystem()
        {
            var status = _statusProcessor.AnalyzeDeviceStatus(
                Common.engineOilGrp.Simulated,
                Common.engineOilGrp.NoError,
                "机油系统"
            );

            _statusProcessor.UpdateStatusLabel(
                status,
                tslblJYOK,
                tslblJYNG,
                tslblJYSim,
                "机油系统",
                () => Common.engineOilGrp.IsSimulated,
                () => Common.engineOilGrp.IsNoError
            );
        }

        /// <summary>
        /// 处理燃油箱系统
        /// </summary>
        private void ProcessFeulSystem()
        {
            var status = _statusProcessor.AnalyzeDeviceStatus(
                Common.fuelGrp.Simulated,
                Common.fuelGrp.NoError,
                "燃油系统"
            );

            _statusProcessor.UpdateStatusLabel(
                status,
                tslblRYOK,
                tslblRYNG,
                tslblRYSim,
                "燃油系统",
                () => Common.fuelGrp.IsSimulated,
                () => Common.fuelGrp.IsNoError
            );
        }

        /// <summary>
        /// 处理水箱系统
        /// </summary>
        private void ProcessWaterSystem()
        {
            var status = _statusProcessor.AnalyzeDeviceStatus(
                Common.waterGrp.Simulated,
                Common.waterGrp.NoError,
                "水系统"
            );

            _statusProcessor.UpdateStatusLabel(
                status,
                tslblWaterOK,
                tslblWaterNG,
                tslblWaterSim,
                "水系统",
                () => Common.waterGrp.IsSimulated,
                () => Common.waterGrp.IsNoError
            );
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            bool ok = Var.MsgBoxYesNo(this, "确定要退出软件吗？");
            if (ok)
            {
                Var.LogInfo("用户" + RWUser.User.Username + "退出登录。");

                try
                {
                    Var.Close();
                    // 关trdp连接
                    Var.TRDP.Timestop();
                    // 关闭燃油耗仪通讯
                    ET4500.Instance.Close();
                }
                catch (Exception ex)
                {
                    Var.LogInfo($"程序退出 OPC关闭失败 {ex.ToString()}");
                }
                Application.Exit();
            }
        }
    }
}

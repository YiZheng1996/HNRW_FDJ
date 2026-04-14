using System;
using System.Windows.Forms;
using MainUI.Properties;
using MainUI.Procedure;
using MainUI.Modules;
using RW.EventLog;
using System.Drawing;
using MainUI.Procedure.User;
using Sunny.UI;
using MainUI.Equip;
using System.Threading;
using RW.UI;
using MainUI.Manager;
using MainUI.Procedure.Test;
using MainUI.Graph;
using System.IO;
using System.Text;
using MainUI.TestScreen;
using System.Diagnostics;
using MainUI.Global;
using System.Collections.Generic;
using MainUI.FSql;
using System.Linq;
using MainUI.Helper;
using MainUI.Fault.Model;
using MainUI.Fault;
using MainUI.Report;

namespace MainUI
{
    public partial class frmMainMenu : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        // 状态栏处理类
        private DeviceStatusProcessor _statusProcessor;

        /// <summary>
        /// 手/自动页面
        /// </summary>
        ucHMI ucFromHMI = new ucHMI();

        /// <summary>
        /// 1 屏 发动机主数据
        /// </summary>
        ucForm1 Screen1HMI = new ucForm1();

        /// <summary>
        /// 自动试验报表导出
        /// </summary>
        ucAutoRecord ucAutoRecord = new ucAutoRecord();

        /// <summary>
        /// 参数管理界面
        /// </summary>
        ucParaManger ucParaMangerHMI = new ucParaManger();

        /// <summary>
        /// 进气加热装置
        /// </summary>
        ucDuctHeating ucDuctHeatingHMI = new ucDuctHeating();

        public frmMainMenu()
        {
            InitializeComponent();
            this.lblTitle.Text = Var.SysConfig.DeviceModel;// Var.SoftName;
            this.Text = Var.SoftName;
            // 点位初始化
            //Common.Init();
        }
        /// <summary>
        /// C#winform实现界面拖动
        /// </summary>
        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = true;

                // 试验状态刷新后的事件
                BaseTest.TestStateChanged += BaseTest_TestStateChanged;

                // 初始化设备状态处理器
                _statusProcessor = new DeviceStatusProcessor(true);

                // 工艺界面显示管路或者控制工艺界面
                var ExeType = Var.SysConfig.ExeType;

                //为参数管理添加点位
                LoadParaManagerNodes(ExeType);

                // 如果类型是1，则跳转到控制界面
                if (ExeType == 1)
                {
                    // 控制界面（带管路界面）
                    this.splitContainer1.Panel2.Controls.Clear();
                    this.splitContainer1.Panel2.Controls.Add(ucFromHMI);
                    ucFromHMI.Dock = DockStyle.Fill;
                    ucFromHMI.Init();

                    // 默认加载手动界面
                    uiImageButtonMain_Click(null, null);
                }
                else
                {
                    this.splitContainer1.Panel2.Controls.Clear();
                    this.splitContainer1.Panel2.Controls.Add(Screen1HMI);
                    Screen1HMI.Dock = DockStyle.Fill;
                    Screen1HMI.Init();
                }

                this.splitContainer1.Panel2.Controls.Add(ucDuctHeatingHMI);
                this.ucDuctHeatingHMI.Dock = DockStyle.Fill;
                this.splitContainer1.Panel2.Controls.Add(ucParaMangerHMI);
                this.ucParaMangerHMI.Dock = DockStyle.Fill;
                this.splitContainer1.Panel2.Controls.Add(ucAutoRecord);
                this.ucAutoRecord.Dock = DockStyle.Fill;

                // 监听拍下急停时
                EventTriggerModel.OnScramChanged += EventTriggerModel_OnScramChanged;

                // 用户校验
                RW.Components.User.BLL.UserBLL userBll = new RW.Components.User.BLL.UserBLL();
                int level = userBll.GetPermissionLevel(RW.UI.RWUser.User.Permission);
                switch (RW.UI.RWUser.User.Permission)
                {
                    case "1"://系统管理员
                    case "管理员":
                        Var.UserPrivate = 1;
                        break;
                    case "2"://工程师
                    case "工艺员":
                        this.btnMainData.Visible = false;
                        Var.UserPrivate = 2;
                        break;
                    case "3"://操作员
                    case "操作员":
                        this.btnMainData.Visible = false;
                        this.btnHardwareTest.Visible = false;
                        //this.btnSysLog.Visible = false;
                        Var.UserPrivate = 3;
                        break;
                    default:
                        Var.UserPrivate = 3;
                        this.btnMainData.Visible = false;
                        this.btnHardwareTest.Visible = false;
                        break;
                }

                this.tslblUser.Text = "登录用户：" + RW.UI.RWUser.User.Username;
                this.tslblVersion.Text = Var.Version;
                this.statusStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
                this.tslblVersion.Alignment = ToolStripItemAlignment.Right;

                // 刷新plc状态计时器
                timerPLC.Enabled = true;

                // 统一init 所有模块
                Common.InitModule();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 拍急停
        /// </summary>
        /// <param name="obj"></param>
        private void EventTriggerModel_OnScramChanged(bool obj)
        {
            if (obj)
            {
                this.picRunStatus.Image = Resources.normal;
            }
            else
            {
                Var.LogInfo("急停被按下。");
                this.picRunStatus.Image = Resources.scram;
            }
        }

        /// <summary>
        /// 关闭现有窗体
        /// </summary>
        private void CloseRunForm()
        {
            foreach (Control item in this.splitContainer1.Panel2.Controls)
            {
                if (item is Form)
                {
                    Form form = (Form)item;
                    form.Close();

                }
            }
        }

        /// <summary>
        /// 在panel2添加新窗体
        /// </summary>
        /// <param name="form"></param>
        private void AddNewForm(UIForm form)
        {

            // 将子窗体设置成非顶级窗体
            form.TopLevel = false;
            // 让子窗体最大化显示
            form.WindowState = FormWindowState.Maximized;
            // 设置窗体的边框为：None（去掉窗体的边框）
            form.FormBorderStyle = FormBorderStyle.None;
            // 设置窗体的父容器（指定子窗体显示的容器）
            form.Parent = this.splitContainer1.Panel2;
            form.Init();
            form.Show();
            //form.Close();
        }

        private void BaseTest_TestStateChanged(bool isTesting)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<bool>(BaseTest_TestStateChanged), isTesting);
                return;
            }

            btnHardwareTest.Enabled = !isTesting;
            btnMainData.Enabled = !isTesting;
            btnReports.Enabled = !isTesting;
            btnChangePwd.Enabled = !isTesting;
            btnExit.Enabled = !isTesting;
        }

        /// <summary>
        /// 显示界面时间
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 硬件校准
        /// </summary>
        private void btnHardwareTest_Click(object sender, EventArgs e)
        {
            Var.hardWare.ShowDialog();
        }

        /// <summary>
        /// 给参数管理添加节点
        /// </summary>
        /// <param name="ExeType">1:主控 0：检测</param>
        private void LoadParaManagerNodes(int ExeType)
        {
            this.ucParaMangerHMI.AddNodes("用户管理", new ucUserManager());
            this.ucParaMangerHMI.AddNodes("类别管理", new ucModelType());
            this.ucParaMangerHMI.AddNodes("型号管理", new ucModelManage());
            if (ExeType == 1)
            {
                this.ucParaMangerHMI.AddNodes("试验基础参数管理", new ucTestParams());
                this.btnReports.Visible = true;
                this.uiImageButtonAutoMain.Visible = true;
                this.btnDuctHeating.Visible = true;
            }
            else
            {
                this.btnReports.Visible = false;
                this.uiImageButtonAutoMain.Visible = false;
                this.btnDuctHeating.Visible = false;
            }
            //this.ucParaMangerHMI.AddNodes("工况表管理", new ucGKParams());
            //main.AddNodes("试验参数管理", new ucTestManage());
            //main.AddNodes("仪表盘显示管理", new ucDashboardPara()); 
            this.ucParaMangerHMI.AddNodes("图表显示管理", new ucGraphPara());
        }

        /// <summary>
        /// 数据管理
        /// </summary>
        private void btnMainData_Click(object sender, EventArgs e)
        {
            this.uiImageButtonMain.BackColor = this.BackColor;
            this.uiImageButtonAutoMain.BackColor = this.BackColor;
            this.btnReports.BackColor = this.BackColor;
            this.btnDuctHeating.BackColor = this.BackColor;
            this.btnMainData.BackColor = SystemColors.GradientActiveCaption;

            // 添加参数管理界面
            this.ucAutoRecord.Visible = false;
            this.ucFromHMI.Visible = false;
            this.Screen1HMI.Visible = false;
            this.ucAutoRecord.Visible = false;
            this.ucDuctHeatingHMI.Visible = false;
            this.ucParaMangerHMI.Visible = true;
        }

        /// <summary>
        /// 历史报表
        /// </summary>
        private void btnReports_Click(object sender, EventArgs e)
        {
            this.uiImageButtonAutoMain.BackColor = this.BackColor;
            this.uiImageButtonMain.BackColor = this.BackColor;
            this.btnMainData.BackColor = this.BackColor;
            this.btnReports.BackColor = SystemColors.GradientActiveCaption;

            this.ucFromHMI.Visible = false;
            this.ucAutoRecord.Visible = false;
            this.ucParaMangerHMI.Visible = false;
            this.ucAutoRecord.Visible = true;

            //frmDataManager fdm = new frmDataManager();
            //fdm.ShowDialog();
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        private void btnChangePwd_Click(object sender, EventArgs e)
        {
            frmChangePwd changePwd = new frmChangePwd();
            changePwd.ShowDialog();
        }

        /// <summary>
        /// 注销
        /// </summary>
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        /// <summary>
        /// 退出
        /// </summary>
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

        private void timerPLC_Tick(object sender, EventArgs e)
        {
            try
            {
                // 1. 台位控制OPC
                bool IsPlcNg = _statusProcessor.FreshCommStatus(tslblPLC, "台位控制", Common.opcStatus.Simulated, Common.opcStatus.NoError);
                Var.FaultService.FaultStatusChange(FaultTypeEnum.communication, IsPlcNg ? WarnTypeEnum.Alarm : WarnTypeEnum.None, "台位控制");

                // 2. PLC测量OPC
                bool IsCkPlcNg = _statusProcessor.FreshCommStatus(tslblCKPLC, "测量柜", Common.AI2Grp.Simulated, Common.AI2Grp.NoError);
                Var.FaultService.FaultStatusChange(FaultTypeEnum.communication, IsCkPlcNg ? WarnTypeEnum.Alarm : WarnTypeEnum.None, "测量柜");

                // 3. 启动柜OPC
                bool IsStartNg = _statusProcessor.FreshCommStatus(tslblStart, "启动柜", Common.startPLCGrp.Simulated, Common.startPLCGrp.NoError);
                //Var.FaultService.FaultStatusChange(FaultTypeEnum.communication, IsStartNg ? WarnTypeEnum.Alarm : WarnTypeEnum.None, "启动柜");

                // 4. 发动机电参数
                bool IsElectricalNg = _statusProcessor.FreshCommStatus(tslblElectrical, "发动机电参数", Common.threePhaseElectric.Simulated, Common.threePhaseElectric.NoError);
                Var.FaultService.FaultStatusChange(FaultTypeEnum.communication, IsElectricalNg ? WarnTypeEnum.Alarm : WarnTypeEnum.None, "发动机电参数");

                // 5-7. 机油箱系统 - 统一处理
                ProcessEngineOilSystem();

                // 燃油
                ProcessFeulSystem();

                // 水系统
                ProcessWaterSystem();

                // 11. 柴油机控制器 (注意：此处第二个参数固定为false，表示非仿真模式)
                bool IsTrdpNg = _statusProcessor.FreshCommStatus(tslblTRDP, "柴油机控制器", false, Var.TRDP.IsConnected);
                //Var.testBedFaultService.CheckAndLogStatusChange(TestBedFaultEnum.communication, WarnType.Alarm, "柴油机控制器", IsTrdpNg);

                var IsZCNg = false;
                var lsRYNg = false;
                var lsWeightNg = false;
                var IsBC = false;
                if (Var.SysConfig.ExeType == 1)
                {
                    // 如果是控制端
                    IsZCNg = _statusProcessor.FreshCommStatus(tslblCommunication, "台位主从通讯", false, true);
                    lsRYNg = _statusProcessor.FreshCommStatus(tslblRYHY, "燃油耗仪", false, ET4500.Instance.IsConnected);
                    lsWeightNg = _statusProcessor.FreshCommStatus(tslblWeight, "称重仪", false, ZMPT650F.Instance.IsConnected);
                    IsBC = _statusProcessor.FreshCommStatus(tslblJYBC, "机油耗磅秤", false, YHA27.Instance.IsConnected);
                }
                else
                {
                    // 检测
                    IsZCNg = _statusProcessor.FreshCommStatus(tslblCommunication, "台位主从通讯", Common.opcExChangeReceiveGrp.Simulated, Common.opcExChangeReceiveGrp.NoError);
                    lsRYNg = _statusProcessor.FreshCommStatus(tslblRYHY, "燃油耗仪", false, Common.opcExChangeReceiveGrp.GetDouble("油耗仪_NoError") == 1);
                    lsWeightNg = _statusProcessor.FreshCommStatus(tslblWeight, "称重仪", false, Common.opcExChangeReceiveGrp.GetDouble("称重仪_NoError") == 1);
                    IsBC = _statusProcessor.FreshCommStatus(tslblJYBC, "机油耗磅秤", false, Common.opcExChangeReceiveGrp.GetDouble("磅秤_NoError") == 1);
                }
                Var.FaultService.FaultStatusChange(FaultTypeEnum.communication, IsZCNg ? WarnTypeEnum.Alarm : WarnTypeEnum.None, "台位主从通讯");
                Var.FaultService.FaultStatusChange(FaultTypeEnum.communication, lsRYNg ? WarnTypeEnum.Tip : WarnTypeEnum.None, "燃油耗仪");
                Var.FaultService.FaultStatusChange(FaultTypeEnum.communication, IsBC ? WarnTypeEnum.Tip : WarnTypeEnum.None, "机油耗磅秤");
                Var.FaultService.FaultStatusChange(FaultTypeEnum.communication, lsWeightNg ? WarnTypeEnum.Stop : WarnTypeEnum.None, "称重仪");
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
        /// 点击手动界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiImageButtonMain_Click(object sender, EventArgs e)
        {
            this.uiImageButtonMain.BackColor = SystemColors.GradientActiveCaption;
            this.uiImageButtonAutoMain.BackColor = this.BackColor;
            this.btnMainData.BackColor = this.BackColor;
            this.btnReports.BackColor = this.BackColor;
            this.btnDuctHeating.BackColor = this.BackColor;

            this.ucAutoRecord.Visible = false;
            this.ucParaMangerHMI.Visible = false;
            this.ucAutoRecord.Visible = false;
            this.ucDuctHeatingHMI.Visible = false;

            if (Var.SysConfig.ExeType == 1)
            {
                // 添加手动界面
                this.ucFromHMI.SelectPage("Manual");
                this.ucFromHMI.Visible = true;
            }
            else
            {
                this.Screen1HMI.Visible = true;
            }
        }

        /// <summary>
        /// 点击自动界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiImageButtonAutoMain_Click(object sender, EventArgs e)
        {
            this.uiImageButtonAutoMain.BackColor = SystemColors.GradientActiveCaption;
            this.uiImageButtonMain.BackColor = this.BackColor;
            this.btnMainData.BackColor = this.BackColor;
            this.btnReports.BackColor = this.BackColor;
            this.btnDuctHeating.BackColor = this.BackColor;

            this.ucDuctHeatingHMI.Visible = false;
            this.ucAutoRecord.Visible = false;
            this.ucParaMangerHMI.Visible = false;
            this.ucAutoRecord.Visible = false;

            // 添加自动界面
            this.ucFromHMI.SelectPage("Auto");
            this.ucFromHMI.Visible = true;
        }

        /// <summary>
        /// 风道加热控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDuctHeating_Click(object sender, EventArgs e)
        {
            this.uiImageButtonMain.BackColor = this.BackColor;
            this.uiImageButtonAutoMain.BackColor = this.BackColor;
            this.btnReports.BackColor = this.BackColor;
            this.btnMainData.BackColor = this.BackColor;
            this.btnDuctHeating.BackColor = SystemColors.GradientActiveCaption;

            // 添加参数管理界面
            this.ucAutoRecord.Visible = false;
            this.ucFromHMI.Visible = false;
            this.Screen1HMI.Visible = false;
            this.ucAutoRecord.Visible = false;
            this.ucParaMangerHMI.Visible = false;
            this.ucDuctHeatingHMI.Visible = true;
        }

        /// <summary>
        /// 快捷键截图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 && e.Alt) // 检测Alt+F2
            {
                //阈值设置界面
                e.Handled = true; // 标记事件已处理，避免传递到其他控件

                SystemConstantForm configPage = new SystemConstantForm();
                var dr = configPage.ShowDialog();
            }
        }

       
    }
}

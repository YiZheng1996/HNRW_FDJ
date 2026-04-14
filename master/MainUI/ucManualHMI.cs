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

namespace MainUI
{
    public partial class ucManualHMI : UserControl
    {
        // 仪表盘界面
        frmkeyParameter frmkeyParameterHMI;

        public delegate void RunStatusHandler(bool obj);
        public event RunStatusHandler EmergencyStatusChanged;
        public AIGrp AIgrp = null;
        public AOGrp AOgrp = null;
        public DIGrp DIgrp = null;
        public DOGrp DOgrp = null;
        public EngineOilGrp engineOilGrp = null;
        public FuelGrp fuelGrp = null;
        public ThreePhaseElectric threePhaseElectric = null;
        public WaterGrp waterGrp = null;
        public PLC2AIGrp engineParaGrp = null;
        public GD350_1 gD350_1 = null;

        ParaConfig paraconfig = null;

        Dictionary<string, BaseTest> dicBase = new Dictionary<string, BaseTest>();

        string rn = Environment.NewLine;
        private delegate void Del();

        private delegate void Del2(bool b);

        public ucManualHMI()
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
                int scrollBarWidth = 20; // 设置新的滚动条宽度为20像素
                SystemParametersInfo(SPI_SETSCROLLBARS, 0, ref scrollBarWidth, SPIF_UPDATEINIFILE);

                AIgrp = Common.AIgrp;
                AOgrp = Common.AOgrp;
                DIgrp = Common.DIgrp;
                DOgrp = Common.DOgrp;
                engineOilGrp = Common.engineOilGrp;
                fuelGrp = Common.fuelGrp;
                threePhaseElectric = Common.threePhaseElectric;
                waterGrp = Common.waterGrp;
                engineOilGrp = Common.engineOilGrp;
                gD350_1 = Common.gd350_1;


                BaseTest.TipsChanged += UcBase_TipsChanged;

                BaseTest.WaitStepTick += BaseTest_WaitStepTick;

                //BaseTest.hmi = this;

                // init 故障点位
                ucWarning1.InitFault();
                ucWarning1.FaultReset += UcWarning1_FaultReset;

                // 打开数据刷新计时器
                timerAI.Enabled = true;

                // 强制刷新一次DI点，更新界面元素
                Common.DIgrp.Fresh();

                int moveIndex = 0;
                // 打开另一个窗体
                if (Screen.AllScreens.Count() > 1)
                {
                    moveIndex = 1;
                }

                // 如果窗体不存在则创建，否则激活
                if (frmkeyParameterHMI == null || frmkeyParameterHMI.IsDisposed)
                {
                    frmkeyParameterHMI = new frmkeyParameter();
                    frmkeyParameterHMI.Show();
                }
                else
                {
                    frmkeyParameterHMI.Activate();
                }
                // 如果存在另一个窗体，弹出到另一个页面
                MoveFormToMonitor(frmkeyParameterHMI, moveIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        Dictionary<string, PLC2AIGrp> DoubleDicValve = new Dictionary<string, PLC2AIGrp>();

        /// <summary>
        /// 故障复位点击事件
        /// </summary>
        private void UcWarning1_FaultReset()
        {
            Common.startPLCGrp.FaultReset = true;
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

        private void DIgrp_DIGroupChanged(object sender, int index, bool value)
        {
            try
            {
                //if(dicDI.ContainsKey(index))
                //{
                //    dicDI[index].State = value ? UILightState.On : UILightState.Off; 
                //}

                if (index == 1 || index == 2)
                {
                    bool emer = Common.DIgrp[1] && Common.DIgrp[2]; //操作台急停 且 机柜急停

                    EmergencyStatusChanged?.Invoke(emer);
                }


            }
            catch (Exception ex)
            {

            }
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

        private UILightState GetState(bool home)
        {
            if (home)
            {
                return UILightState.On;
            }
            else
            {

                return UILightState.Off;
            }
        }

        private void timerAI_Tick(object sender, EventArgs e)
        {
            //try
            //{
            //    lblPress01.Text = Common.AIgrp.Press1.ToString();

            //    lt01.State = GetState(Common.DIgrp[1]);
            //    lt09.State = GetState(Common.DIgrp[9]);

            //    for (int i = 0; i < Common.DOgrp.DOlist.Length; i++)
            //    {
            //        if(dicYougang.ContainsKey(i))
            //        {
            //            dicYougang[i].Switch = Common.DOgrp.DOlist[i];
            //        }

            //        if (dicMada.ContainsKey(i))
            //        {
            //            dicMada[i].Switch = Common.DOgrp.DOlist[i];
            //        }

            //        if (dicYoubeng.ContainsKey(i))
            //        {
            //            dicYoubeng[i].Switch = Common.DOgrp.DOlist[i];
            //        }

            //    }

            //}
            //catch (Exception ex)
            //{

            //}
        }

        private void switchPictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                SwitchPictureBox uc = sender as SwitchPictureBox;
                int idx = uc.Index;
                bool setV = !Common.DOgrp[idx];

                #region//互锁==============
                if (idx == 2 && setV)
                {
                    //Common.DOgrp[3] = false;
                }

                #endregion

                // Common.DOgrp[idx] = setV;
            }
            catch (Exception ex)
            {
                string err = "阀控制有误。原因：" + ex.Message;
                MessageBox.Show(err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 故障日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFaultLog_Click(object sender, EventArgs e)
        {
            frmEventLogs frmlog = new frmEventLogs();
            frmlog.ShowDialog();
        }

        private void btnFaultReset_Click(object sender, EventArgs e)
        {
            try
            {
                Common.startPLCGrp.FaultReset = true;
            }
            catch (Exception ex)
            {

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

        private void btnWaterPower_Click(object sender, EventArgs e)
        {

        }

        private void btnWaterPlateUp_Click(object sender, EventArgs e)
        {
            try
            {
                Common.DOgrp["水阻上升控制"] = true;
                Common.DOgrp["水阻下降控制"] = false;
                this.btnWaterPlateUp.BackColor = Color.Lime;
                this.btnWaterPlateDown.BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {

                throw new Exception("水阻极板上升失败！" + ex.Message);
            }
        }

        private void btnWaterPlateDown_Click(object sender, EventArgs e)
        {
            try
            {
                Common.DOgrp["水阻上升控制"] = false;
                Common.DOgrp["水阻下降控制"] = true;
                this.btnWaterPlateUp.BackColor = Color.Transparent;
                this.btnWaterPlateDown.BackColor = Color.Lime;
            }
            catch (Exception ex)
            {

                throw new Exception("水阻极板下降失败！" + ex.Message);
            }
        }
    }
}

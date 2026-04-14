using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MainUI.Model;
using RW.UI.Controls;
using RW.EventLog;
using RW.UI;
using MainUI.Services;
using MainUI.Fault;
using System.Threading;
using MainUI.Fault.Model;
using MainUI.Global;

namespace MainUI.Widget
{
    public partial class ucWarnList : UserControl
    {
        /// <summary>
        /// 是否常量警示灯
        /// </summary>
        public bool IsLight { get; set; } = true;

        /// <summary>
        /// 报警检测类型 （true：发动机控制器   false：台位）
        /// </summary>
        public bool FaultType { get; set; } = true;

        /// <summary>
        /// 实时报警
        /// </summary>
        public frmCurrentWarn frmCurrent;

        /// <summary>
        /// ECM所有故障的控件
        /// </summary>
        private Dictionary<string, ucWarn> _faultWarnMap = new Dictionary<string, ucWarn>();

        public ucWarnList()
        {
            InitializeComponent();
        }


        public void Init()
        {
            if (this.DesignMode) return;

            frmCurrent = new frmCurrentWarn();

            // 初始化加载 故障控件
            InitializeFaultDetection();
            LoadTestBedFault();

            // 监控台位本身故障
            //MonitFault();

            // 数据实时更新方法
            StartCurrentData();

            // 重新刷新一次故障（用于程序初始化）
            Var.FaultService.FaultCheckResend();

            // 打开刷新故障状态计时器
            this.timerCheckECM.Enabled = true;
            this.timerCheckECM.Start();

            // 刷新试验台检测故障
            this.timerCheckTestBed.Enabled = true;
            this.timerCheckTestBed.Start();

            // 默认打开报警音
            btnFaultBlock_Click(null, null);

            // 订阅故障检测事件
            Var.FaultService.FaultDetected += OnFaultDetected;
        }

        private void btnFaultLog_Click(object sender, EventArgs e)
        {
            frmFaultManager frmlog = new frmFaultManager();
            frmlog.ShowDialog();
        }

        /// <summary>
        /// 监控台位故障
        /// </summary>
        public void MonitFault()
        {
            Thread t = new Thread(xxx =>
            {
                while (true)
                {
                    try
                    {

                        if (Var.FaultService.HasActiveFaults)
                        {
                            // 确保在UI线程上更新
                            if (this.InvokeRequired)
                            {
                                this.Invoke(new Action(() =>
                                {
                                    this.lblFaultDesECM.Visible = false;
                                }));
                            }
                        }
                        else
                        {
                            this.lblFaultDesECM.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"故障检测异常: {ex.Message}");
                    }
                    Thread.Sleep(1000);
                }
            });
            t.IsBackground = true;
            t.Name = "实时检测故障线程";
            t.Start();
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        private void InitializeFaultDetection()
        {
            _faultWarnMap.Clear();

            this.flowLayoutPanel1.SuspendLayout();
            // 建立故障ID到ucWarn控件的映射
            foreach (var item in this.flowLayoutPanel1.Controls)
            {
                if (item is ucWarn)
                {
                    ucWarn warn = item as ucWarn;
                    warn.Visible = false;
                    _faultWarnMap.Add(warn.Key, warn);
                }
            }
            this.flowLayoutPanel1.ResumeLayout();

        }

        /// <summary>
        /// 加载故障信息，需要先实例化FaultData对象
        /// </summary>
        public void LoadTestBedFault()
        {
            this.flowLayoutPanel2.SuspendLayout();

            var faultStates = Var.FaultService.GetAllFault();
            foreach (var item in faultStates)
            {
                if (item.Key == FaultTypeEnum.ecm) continue;
                //todo 只加载本地故障
                foreach (var item2 in item.Value)
                {
                    Label lbl = new Label();
                    lbl.Tag = item2.Name;
                    lbl.Text = item2.Desc;
                    lbl.BackColor = Color.Red;
                    lbl.Font = new Font("微软雅黑", 15);
                    lbl.BorderStyle = BorderStyle.FixedSingle;
                    lbl.AutoSize = true;
                    lbl.Margin = new Padding(0, 0, 10, 0);
                    if (string.IsNullOrEmpty(item2.Name))
                        lbl.Visible = false;
                    item2.BindControl = lbl;
                    this.flowLayoutPanel2.Controls.Add(lbl);
                }
            }

            this.flowLayoutPanel2.ResumeLayout();
        }

        /// <summary>
        /// 计时器开始记录实时数据
        /// </summary>
        private void StartCurrentData()
        {
            var timer = new System.Timers.Timer(500);
            timer.Elapsed += (s, e) =>
            {
                try
                {
                    // 从trdp字典中更新数据
                    Var.FaultService.UpdateSensorData(data =>
                    {
                        if (FaultType)
                        {
                            // 控制器
                            data.发动机转速 = Var.TRDP.GetDicValue("柴油机转速");
                            data.发动机功率 = MiddleData.instnce.EnginePower;
                            data.高温水出水温度 = Var.TRDP.GetDicValue("高温水出水温度");
                            data.中冷水进水温度 = Var.TRDP.GetDicValue("中冷水进水温度");
                            data.中冷水出水温度 = Var.TRDP.GetDicValue("中冷水出水温度");
                            data.后中冷后空气温度 = Var.TRDP.GetDicValue("后中冷器后空气温度");
                            data.主油道进口油温 = Var.TRDP.GetDicValue("主油道进口油温");
                            data.前压气机出口空气温度 = Var.TRDP.GetDicValue("前压气机出口空气温度");
                            data.后压气机出口空气温度 = Var.TRDP.GetDicValue("后压气机出口空气温度");
                            data.主油道进口油压 = Var.TRDP.GetDicValue("主油道进口油压");
                            data.燃油精滤器前油压 = Var.TRDP.GetDicValue("燃油精滤器前油压");
                            data.燃油精滤器后油压 = Var.TRDP.GetDicValue("燃油精滤器后油压");
                            data.机油泵出口油温 = Var.TRDP.GetDicValue("机油泵出口油温");
                            data.主油道末端油压 = Var.TRDP.GetDicValue("主油道末端油压");
                            data.后增压器进口油压 = Var.TRDP.GetDicValue("后增压器进口油压");
                            data.前增压器转速 = Var.TRDP.GetDicValue("前增压器转速");
                            data.后增压器转速 = Var.TRDP.GetDicValue("后增压器转速");

                            // A1-A6缸排气温度
                            if (data.A1A6缸排气温度 == null || data.A1A6缸排气温度.Length != 6)
                                data.A1A6缸排气温度 = new double[6];

                            data.A1A6缸排气温度[0] = Convert.ToDouble(Var.TRDP.GetDicValue("A1缸排气温度"));
                            data.A1A6缸排气温度[1] = Convert.ToDouble(Var.TRDP.GetDicValue("A2缸排气温度"));
                            data.A1A6缸排气温度[2] = Convert.ToDouble(Var.TRDP.GetDicValue("A3缸排气温度"));
                            data.A1A6缸排气温度[3] = Convert.ToDouble(Var.TRDP.GetDicValue("A4缸排气温度"));
                            data.A1A6缸排气温度[4] = Convert.ToDouble(Var.TRDP.GetDicValue("A5缸排气温度"));
                            data.A1A6缸排气温度[5] = Convert.ToDouble(Var.TRDP.GetDicValue("A6缸排气温度"));
                            data.A涡前排气温度 = Var.TRDP.GetDicValue("A涡前排气温度");

                            // B1-B6缸排气温度
                            if (data.B1B6缸排气温度 == null || data.B1B6缸排气温度.Length != 6)
                                data.B1B6缸排气温度 = new double[6];

                            data.B1B6缸排气温度[0] = Convert.ToDouble(Var.TRDP.GetDicValue("B1缸排气温度"));
                            data.B1B6缸排气温度[1] = Convert.ToDouble(Var.TRDP.GetDicValue("B2缸排气温度"));
                            data.B1B6缸排气温度[2] = Convert.ToDouble(Var.TRDP.GetDicValue("B3缸排气温度"));
                            data.B1B6缸排气温度[3] = Convert.ToDouble(Var.TRDP.GetDicValue("B4缸排气温度"));
                            data.B1B6缸排气温度[4] = Convert.ToDouble(Var.TRDP.GetDicValue("B5缸排气温度"));
                            data.B1B6缸排气温度[5] = Convert.ToDouble(Var.TRDP.GetDicValue("B6缸排气温度"));
                            data.B涡前排气温度 = Var.TRDP.GetDicValue("B涡前排气温度");

                            // 1-7缸排气温度
                            if (data._1_7档轴温 == null || data._1_7档轴温.Length != 7)
                                data._1_7档轴温 = new double[7];
                            data._1_7档轴温[0] = Convert.ToDouble(Var.TRDP.GetDicValue("一档轴温"));
                            data._1_7档轴温[1] = Convert.ToDouble(Var.TRDP.GetDicValue("二档轴温"));
                            data._1_7档轴温[2] = Convert.ToDouble(Var.TRDP.GetDicValue("三档轴温"));
                            data._1_7档轴温[3] = Convert.ToDouble(Var.TRDP.GetDicValue("四档轴温"));
                            data._1_7档轴温[4] = Convert.ToDouble(Var.TRDP.GetDicValue("五档轴温"));
                            data._1_7档轴温[5] = Convert.ToDouble(Var.TRDP.GetDicValue("六档轴温"));
                            data._1_7档轴温[6] = Convert.ToDouble(Var.TRDP.GetDicValue("七档轴温"));

                            data.轴温监控装置通讯故障 = Var.TRDP.GetDicValue("从站6串口故障");
                            data.电喷转速1 = Var.TRDP.GetDicValue("转速传感器1#");
                            data.电喷转速2 = Var.TRDP.GetDicValue("转速传感器2#");
                            data.电喷状态 = Var.TRDP.GetDicValue("从站1串口故障");
                        }
                        else
                        {
                            // 台位
                            data.发动机转速 = Common.speedGrp["转速1"];
                            data.发动机功率 = MiddleData.instnce.EnginePower;
                            data.高温水出水温度 = Common.AI2Grp["T1高温水出机温度"];
                            data.中冷水进水温度 = Common.AI2Grp["T3中冷水进机温度"];
                            data.中冷水出水温度 = Common.AI2Grp["T5中冷水出机温度"];
                            data.后中冷后空气温度 = Common.AI2Grp["后中冷后空气温度"];
                            data.主油道进口油温 = Common.AI2Grp["T21主油道进口油温"];
                            data.前压气机出口空气温度 = Var.TRDP.GetDicValue("后压气机出口空气温度");
                            data.后压气机出口空气温度 = Var.TRDP.GetDicValue("后压气机出口空气温度");
                            data.主油道进口油压 = Common.AI2Grp["P21主油道进口油压"];
                            //data.燃油精滤器前油压 = Var.TRDP.GetDicValue("燃油精滤器前油压");
                            //data.燃油精滤器后油压 = Var.TRDP.GetDicValue("燃油精滤器后油压");
                            data.机油泵出口油温 = Common.AI2Grp["T20机油泵出口油温"];
                            data.主油道末端油压 = Common.AI2Grp["主油道末端油压"];
                            data.后增压器进口油压 = Common.AI2Grp["后增压器机油进口压力"];
                            //data.前增压器转速 = Common.speedGrp["转速2"];
                            //data.后增压器转速 = Common.speedGrp["转速3"];

                            // A1-A6缸排气温度
                            if (data.A1A6缸排气温度 == null || data.A1A6缸排气温度.Length != 8)
                                data.A1A6缸排气温度 = new double[8];

                            data.A1A6缸排气温度[0] = Common.AI2Grp["A1缸排气温度"];
                            data.A1A6缸排气温度[1] = Common.AI2Grp["A2缸排气温度"];
                            data.A1A6缸排气温度[2] = Common.AI2Grp["A3缸排气温度"];
                            data.A1A6缸排气温度[3] = Common.AI2Grp["A4缸排气温度"];
                            data.A1A6缸排气温度[4] = Common.AI2Grp["A5缸排气温度"];
                            data.A1A6缸排气温度[5] = Common.AI2Grp["A6缸排气温度"];
                            data.A1A6缸排气温度[6] = Common.AI2Grp["A7缸排气温度"];
                            data.A1A6缸排气温度[7] = Common.AI2Grp["A8缸排气温度"];
                            data.A涡前排气温度 = Common.AI2Grp["前涡轮进口废气温度"];

                            // B1-B6缸排气温度
                            if (data.B1B6缸排气温度 == null || data.B1B6缸排气温度.Length != 8)
                                data.B1B6缸排气温度 = new double[8];

                            data.B1B6缸排气温度[0] = Common.AI2Grp["B1缸排气温度"];
                            data.B1B6缸排气温度[1] = Common.AI2Grp["B2缸排气温度"];
                            data.B1B6缸排气温度[2] = Common.AI2Grp["B3缸排气温度"];
                            data.B1B6缸排气温度[3] = Common.AI2Grp["B4缸排气温度"];
                            data.B1B6缸排气温度[4] = Common.AI2Grp["B5缸排气温度"];
                            data.B1B6缸排气温度[5] = Common.AI2Grp["B6缸排气温度"];
                            data.B1B6缸排气温度[6] = Common.AI2Grp["B7缸排气温度"];
                            data.B1B6缸排气温度[7] = Common.AI2Grp["B8缸排气温度"];
                            data.B涡前排气温度 = Common.AI2Grp["后涡轮进口废气温度"];
                        }

                        data.飞轮发动机转速1 = Common.speedGrp["转速2"];
                        data.飞轮发动机转速2 = Common.speedGrp["转速3"];
                        data.后增进油压卸载开关 = Common.DIgrp["柴油机卸载"] ? 1 : 0;
                        data.后增进油压停机开关 = Common.DIgrp["柴油机停机"] ? 1 : 0;
                        data.曲轴箱差压开关 = Common.DIgrp["曲轴箱压力开关"] ? 1 : 0;
                    });
                }
                catch (Exception ex)
                {
                    //Var.MsgBoxWarn(this, $"故障模块，实时发动数据出现异常：{ex.ToString()}");
                }

            };
            timer.Start();
        }

        /// <summary>
        /// 故障触发
        /// </summary>
        /// <param name="faultId"></param>
        /// <param name="warnType"></param>
        private void OnFaultDetected(string faultId, FaultState faultState, WarnTypeEnum warnType)
        {
            // 确保在UI线程上更新
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string, FaultState, WarnTypeEnum>(OnFaultDetected), faultId, faultState, warnType);
                return;
            }

            if (faultState.FaultType == FaultTypeEnum.ecm && _faultWarnMap.TryGetValue(faultId, out var warnControl))
            {
                if (!warnControl.Visible) 
                {
                    this.flowLayoutPanel1.SuspendLayout();

                    warnControl.CurrentFault = warnType;

                    // 根据故障类型直接控制可见性
                    warnControl.Visible = (warnType != WarnTypeEnum.None);

                    this.flowLayoutPanel1.ResumeLayout();
                }
            }

            //if (faultState.FaultType == FaultTypeEnum.ecm)
            //{
            //    //ECM故障
            //    if (_faultWarnMap.TryGetValue(faultId, out var warnControl))
            //    {
            //        warnControl.CurrentFault = warnType;
            //        if (warnType == WarnTypeEnum.None)
            //        {
            //            warnControl.Visible = false;
            //            return;
            //        }

            //        warnControl.Visible = true;
            //        if (warnControl.CurrentFault == WarnTypeEnum.Alarm || warnControl.CurrentFault == WarnTypeEnum.Shedding)
            //        {
            //            // todo 暂时屏蔽 只报警
            //            //Common.DOgrp["声光报警"] = true;
            //        }
            //        else
            //        {
            //            //Common.DOgrp["发动机启停预启动"] = false;
            //        }

            //    }
            //}
            //else
            //{
            //    // 试验台的检测故障
            //}

        }


        /// <summary>
        /// 计时器开始记录实时数据
        /// </summary>
        private void FaultResetToSpeed()
        {
            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += (s, e) =>
            {
                // 从trdp字典中更新数据
                Var.FaultService.UpdateSensorData(data =>
                {
                    // 如果不存在故障，则复位，否则不复位   检测发动机转速 低于10转 给
                    if (Var.TRDP.GetDicValue("柴油机转速") < 300)
                    {
                        if (!Common.DOgrp["发动机启停预启动"])
                            Common.DOgrp["发动机启停预启动"] = true;
                    }

                });
            };
            timer.Start();
        }


        private void ucFaultList_Load(object sender, EventArgs e)
        {
            // 可以在这里开始模拟数据变化来测试

        }

        private void btnFaultMore_Click(object sender, EventArgs e)
        {
            frmCurrent.ShowDialog();
        }

        /// <summary>
        /// 屏蔽故障
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFaultBlock_Click(object sender, EventArgs e)
        {
            if (this.btnFaultBlock.BackColor == SystemColors.ControlLight)
            {
                // 打开声音
                this.btnFaultBlock.Text = "报警音已打开";
                this.btnFaultBlock.BackColor = Color.Lime;
                IsLight = true;
            }
            else
            {
                // 关闭声音
                this.btnFaultBlock.Text = "报警音已关闭";
                this.btnFaultBlock.BackColor = SystemColors.ControlLight;
                IsLight = false;
            }

        }

        /// <summary>
        /// 报警参数设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPara_Click(object sender, EventArgs e)
        {
            // 根据型号加载 frmFalutEdit 
            if (RWUser.Current.Permission == "工艺员" || RWUser.Current.Permission == "管理员")
            {
                frmWarnParaConfig frmWarn = new frmWarnParaConfig();
                frmWarn.ShowDialog();
            }
            else
            {
                Var.MsgBoxInfo(this, "当前用户权限不足。");
            }

        }

        /// <summary>
        /// 切换检测模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnType_Click(object sender, EventArgs e)
        {
            if (this.btnType.Text == "切换为台位")
            {
                var result = Var.MsgBoxYesNo(this, "确定要切换为台位检测吗？");
                if (!result) return;

                FaultType = false;
                //this.lblType.Text = "台位";
                //this.lblFaultTtitle.Text = "台位检测报警列表";
                //this.btnType.Text = "切换为控制器";
            }
            else
            {
                var result = Var.MsgBoxYesNo(this, "确定要切换为发动机控制器吗？");
                if (!result) return;

                FaultType = true;
                //this.lblType.Text = "发动机控制器";
                //this.lblFaultTtitle.Text = "控制器检测报警列表";
                //this.btnType.Text = "切换为台位";
            }

        }

        /// <summary>
        /// 显示隐藏 暂无故障
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool IsScram = Common.DIgrp["紧急停止"] == false;

            // 检查ECM是否存在故障
            bool IsEcmFault = false;
            foreach (var warnControl in _faultWarnMap.Values)
            {
                if (warnControl.Visible)
                {
                    IsEcmFault = true;
                    break; // 找到一个即可退出循环
                }
            }
            this.lblFaultDesECM.Visible = !IsEcmFault;

            bool IsTestBedFault = false;
            var StopData = Var.FaultService.GetActiveFaultsByType(WarnTypeEnum.Stop); // 存在停机故障的
            var TestBedFaultTipsData = Var.FaultService.GetActiveFaultsByType(WarnTypeEnum.Alarm); // 存在报警故障的
            if (StopData.Count > 0 || TestBedFaultTipsData.Count > 0) 
            {
                IsTestBedFault = true;
            } 

            // ECM值的故障报警或者试验台部分检测到
            var status = (IsEcmFault || IsScram || IsTestBedFault);
            if (!IsLight)
            {
                if (Common.DOgrp["蜂鸣器控制"] != false)
                {
                    Common.DOgrp["蜂鸣器控制"] = false;
                }
            }
            else {
                // 有故障
                if (status)
                {
                    if (Common.DOgrp["蜂鸣器控制"] != true)
                    {
                        Common.DOgrp["蜂鸣器控制"] = true;
                    }
                }
                else {
                    if (Common.DOgrp["蜂鸣器控制"] != false)
                    {
                        Common.DOgrp["蜂鸣器控制"] = false;
                    }
                }
            }
        }

        /// <summary>
        /// 试验台部分检测故障
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerCheckTestBed_Tick(object sender, EventArgs e)
        {
            if (this.DesignMode) return;

            this.uiLightStop.State = Common.ExChangeGrp.GetBool("上位机停机控制") ? Sunny.UI.UILightState.On : Sunny.UI.UILightState.Off;
            this.lblAutoStop1.ForeColor = this.lblAutoStop2.ForeColor = Common.ExChangeGrp.GetBool("上位机停机控制") ? Color.Red : Color.Black;

            // 试验台内部检测故障
            bool IsTestBedFault = Var.FaultService.HasActiveFaultsTestBed();
            this.lblFaultDes.Visible = !IsTestBedFault;

            var items = Var.FaultService.GetAllFault();
            foreach (var item1 in items)
            {
                if (item1.Key == FaultTypeEnum.ecm) continue;

                foreach (var item in item1.Value)
                {
                    if (item != null && item.BindControl != null)
                    {
                        (item.BindControl as Control).Visible = item.CurrentActiveFault != WarnTypeEnum.None;
                    }
                }
            }
        }

        /// <summary>
        /// 按下故障复位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFaultReset_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                // plc在线时发送
                if (Common.opcStatus.NoError)
                {
                    // 手动复位停机故障
                    Common.ExChangeGrp.SetBool("上位机停机控制", false);
                    Var.FaultService.IsStopDoing = false;

                    // 故障复位
                    Common.DOgrp["故障复位"] = true;

                    // 故障复位隐藏ECM控件
                    foreach (var warnControl in _faultWarnMap.Values)
                    {
                        warnControl.RestartSwitch();
                        warnControl.Visible = false;
                    }

                    // 把ECM模块复位
                    Var.FaultService.FaultReset();
                    //Common.gd350_1.SetRunStatus = 7;
                    //Var.MsgBoxSuccess(this, "故障复位成功。");
                }
                else
                {
                    Var.MsgBoxWarn(this, $"PLC状态异常，当前处于离线状态");
                }
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, $"按下故障复位出现异常： {ex.ToString()}");
            }
        }

        /// <summary>
        /// 松开故障复位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFaultReset_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                Common.DOgrp["故障复位"] = false;
            }
            catch (Exception ex)
            {
                Var.LogInfo($"松开故障复位出现异常:  {ex.ToString()}");
            }
       
        }
    }
}
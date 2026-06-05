using MainUI.Fault;
using MainUI.Fault.Engine;
using MainUI.Fault.Model;
using MainUI.Global;
using RW.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MainUI.Widget
{
    /// <summary>
    /// 动态报警墙扩展。
    /// 目的：当前型号若由数据驱动引擎接管(存在 {型号}.faults.json)，
    /// 按 JSON 的规则名补建设计期不存在的 ucWarn 控件，使 280 等新型号的
    /// 故障也能在报警列表里点亮，且不改动任何现有(240)控件与逻辑。
    /// 240 无 JSON 时该方法直接返回，零影响。
    /// </summary>
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

            // 初始化加油状态专用显示标签
            InitFuelingLevelLabel();
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

            // 此刻 map 里只有设计期 ucWarn，拍一份快照
            _fixedWarns.Clear();
            _fixedWarns.AddRange(_faultWarnMap.Values);

            // 记录固定控件设计期按钮显隐，供型号切换时还原
            _fixedOrigBtn.Clear();
            foreach (var w in _fixedWarns)
                if (w != null)
                    _fixedOrigBtn[w] = new BtnVis { Alarm = w.ShowAlarmButton, Shed = w.ShowSheddingButton, Stop = w.ShowStopButton };

            // 首次构建动态报警灯，并在此安装型号切换钩子（缺这一句切型号不会刷新）
            BuildDynamicEcmWarns();
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
            string model = Var.SysConfig != null ? Var.SysConfig.LastModel : null;
            if (EcmProfileStore.Exists(model))
                new frmCurrentWarnDynamic(model).ShowDialog();   // 有 JSON(280…) → 生成式
            else
                frmCurrent.ShowDialog();                         // 240 → 原弹窗，原样不动
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
            if (RWUser.Current.Permission == "工艺员" || RWUser.Current.Permission == "管理员")
            {
                string model = Var.SysConfig.LastModel;
                string jsonPath = EcmProfileStore.PathOf(model);
                if (EcmProfileStore.Exists(model))
                {
                    var profile = EcmProfileStore.Load(model);   // 强类型，不是 JObject
                    new frmWarnParaDynamic(jsonPath).ShowDialog(); // 保存在窗体内部走 EcmProfileStore.Save(profile)
                }
                else
                {
                    new frmWarnParaConfig().ShowDialog();          // 240：原窗体不动
                }
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
            else
            {
                // 有故障
                if (status)
                {
                    if (Common.DOgrp["蜂鸣器控制"] != true)
                    {
                        Common.DOgrp["蜂鸣器控制"] = true;
                    }
                }
                else
                {
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

            CheckFuelLevelDuringRefueling();
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
                    using (MainUI.Fault.OperationContext.Begin(this, sender, "故障复位-按下"))
                    {
                        // 手动复位停机故障
                        Common.ExChangeGrp.SetBool("上位机停机控制", false);

                        // 故障复位
                        Common.DOgrp["故障复位"] = true;
                    }
                    Var.FaultService.IsStopDoing = false;

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
                using (MainUI.Fault.OperationContext.Begin(this, sender, "故障复位-松开"))
                {
                    Common.DOgrp["故障复位"] = false;
                }
            }
            catch (Exception ex)
            {
                Var.LogInfo($"松开故障复位出现异常:  {ex.ToString()}");
            }

        }

        #region 燃油自动加油，液位检测功能

        // ===== 燃油液位检测 =====
        private Label _lblFuelingStatus;
        private bool _fuelingActive = false;
        private double _lastFuelLevel = double.NaN;
        private DateTime _lastLevelChangeTime = DateTime.MinValue;
        private bool _fuelingNoChangeAlarm = false;

        /// <summary>液位变化判定阈值 mm（小于此值视为未变化）</summary>
        private const double FUEL_LEVEL_THRESHOLD = 5.0;
        /// <summary>无液位变化持续多少秒后触发报警</summary>
        private const int FUEL_NO_CHANGE_WARN_SEC = 60;

        /// <summary>
        /// 初始化加油状态专用显示标签
        /// </summary>
        private void InitFuelingLevelLabel()
        {
            _lblFuelingStatus = new Label
            {
                Text = "",
                Font = new Font("宋体", 15f, FontStyle.Bold),
                BorderStyle = BorderStyle.FixedSingle,
                AutoSize = true,
                Margin = new Padding(0, 0, 10, 0),
                Visible = false
            };
            this.flowLayoutPanel2.Controls.Add(_lblFuelingStatus);
        }

        /// <summary>
        /// 加油期间液位监控
        /// </summary>
        private void CheckFuelLevelDuringRefueling()
        {
            // 读 DI"燃油加油开始"
            Common.DIgrp.DataValue.TryGetValue("燃油加油开始", out bool fuelingDI);

            if (fuelingDI && !_fuelingActive)
            {
                // 加油刚开始
                _fuelingActive = true;
                _fuelingNoChangeAlarm = false;
                _lastFuelLevel = Common.fuelGrp["柴油箱液位检测-L29"];
                _lastLevelChangeTime = DateTime.Now;
                SetFuelingLabel("检测到燃油箱液位 ≤ 300mm，自动加油中...", Color.DodgerBlue);
            }
            else if (!fuelingDI && _fuelingActive)
            {
                // 加油结束，停止一切检测
                _fuelingActive = false;
                _fuelingNoChangeAlarm = false;
                SetFuelingLabel("", Color.Transparent, visible: false);
                return;
            }

            if (!_fuelingActive) return;

            // 实时液位变化检测
            double currentLevel = Common.fuelGrp["柴油箱液位检测-L29"];
            if (!double.IsNaN(_lastFuelLevel)
                && Math.Abs(currentLevel - _lastFuelLevel) >= FUEL_LEVEL_THRESHOLD)
            {
                _lastFuelLevel = currentLevel;
                _lastLevelChangeTime = DateTime.Now;
                if (_fuelingNoChangeAlarm)
                {
                    // 液位恢复变化，撤销报警
                    _fuelingNoChangeAlarm = false;
                    SetFuelingLabel("检测到燃油箱液位 ≤ 300mm，自动加油中...", Color.DodgerBlue);
                }
            }

            // 超时无变化 → 报警
            if (!_fuelingNoChangeAlarm
                && (DateTime.Now - _lastLevelChangeTime).TotalSeconds >= FUEL_NO_CHANGE_WARN_SEC)
            {
                _fuelingNoChangeAlarm = true;
                SetFuelingLabel(
                    "加油中燃油箱液位无变化！请检查手阀是否开启、Y164阀是否损坏",
                    Color.OrangeRed);
            }
        }

        private void SetFuelingLabel(string text, Color backColor, bool visible = true)
        {
            if (_lblFuelingStatus == null) return;
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => SetFuelingLabel(text, backColor, visible)));
                return;
            }
            _lblFuelingStatus.Text = text;
            _lblFuelingStatus.BackColor = backColor;
            _lblFuelingStatus.Visible = visible;
        }

        #endregion

        #region 动态报警墙扩展（型号驱动）
        private struct BtnVis { public bool Alarm, Shed, Stop; }
        /// <summary>固定控件设计期按钮显隐原值快照（用于 280→240 切回还原）。</summary>
        private readonly Dictionary<ucWarn, BtnVis> _fixedOrigBtn = new Dictionary<ucWarn, BtnVis>();

        /// <summary>设计期固定控件快照（init 时拍一次）。型号切换时按 Key 只隐不显，绝不 Dispose。</summary>
        private readonly List<ucWarn> _fixedWarns = new List<ucWarn>();

        /// <summary>本类补建的动态控件（型号切换时定点清理），与固定的 _faultWarnMap 并存。</summary>
        private readonly List<ucWarn> _dynamicEcmWarns = new List<ucWarn>();

        /// <summary>型号切换事件钩子是否已安装（惰性安装，确保只订阅一次）。</summary>
        private bool _ecmModelHookInstalled;

        /// <summary>
        /// 按当前型号的引擎判据补建缺失的 ucWarn。
        /// 仅补“现有 _faultWarnMap 里没有的 Key”，已有的固定控件原样复用。
        /// </summary>
        public void BuildDynamicEcmWarns()
        {
            // 钩子必须无条件先装好：即使当前是 240(无JSON)，
            // 也要能在切到 280 时收到通知并刷新。
            EnsureEcmModelHook();

            try
            {
                if (this.DesignMode) return;

                ApplyFixedWarnVisibility(); // 每次构建都先校正固定控件显隐

                string model = Var.SysConfig?.LastModel;
                if (!EcmProfileStore.Exists(model)) return;   // 无 JSON(如240) → 不动

                var profile = EcmProfileStore.Load(model);
                if (profile == null || profile.Rules == null) return;

                this.flowLayoutPanel1.SuspendLayout();
                try
                {
                    foreach (var rule in profile.Rules)
                    {
                        string key = rule.Name;
                        if (string.IsNullOrEmpty(key)) continue;
                        if (_faultWarnMap.ContainsKey(key)) continue;   // 已有固定控件 → 复用

                        var w = NewEcmWarn(rule);
                        this.flowLayoutPanel1.Controls.Add(w);
                        _faultWarnMap.Add(key, w);
                        _dynamicEcmWarns.Add(w);
                    }
                }
                finally
                {
                    this.flowLayoutPanel1.ResumeLayout();
                }
            }
            catch (Exception ex)
            {
                try { Var.LogInfo("BuildDynamicEcmWarns 失败: " + ex.Message); } catch { }
            }
        }

        /// <summary>
        /// 型号切换时调用：先清掉上一型号补建的控件，再按新型号重建。
        /// 设计期固定控件(_faultWarnMap 里那批)不动。
        /// </summary>
        public void RebuildDynamicEcmWarns()
        {
            try
            {
                if (this.DesignMode) return;

                this.flowLayoutPanel1.SuspendLayout();
                try
                {
                    foreach (var w in _dynamicEcmWarns)
                    {
                        if (w == null) continue;
                        string k = w.Key;
                        if (!string.IsNullOrEmpty(k) && _faultWarnMap.ContainsKey(k))
                            _faultWarnMap.Remove(k);
                        this.flowLayoutPanel1.Controls.Remove(w);
                        w.Dispose();
                    }
                    _dynamicEcmWarns.Clear();
                }
                finally
                {
                    this.flowLayoutPanel1.ResumeLayout();
                }

                BuildDynamicEcmWarns();
            }
            catch (Exception ex)
            {
                try { Var.LogInfo("RebuildDynamicEcmWarns 失败: " + ex.Message); } catch { }
            }
        }


        /// <summary>按现有固定 ucWarn 的样式新建一个，按规则涉及的级别决定三个灯显隐（与 frmCurrentWarnDynamic 一致）。</summary>
        private ucWarn NewEcmWarn(RuleDef rule)
        {
            string key = rule?.Name ?? "";

            bool hasAlarm = false, hasShed = false, hasStop = false;

            if (rule?.Checks != null)
                foreach (var c in rule.Checks)
                {
                    if (c == null || string.IsNullOrEmpty(c.Level)) continue;
                    if (string.Equals(c.Level, "Alarm", StringComparison.OrdinalIgnoreCase)) hasAlarm = true;
                    else if (string.Equals(c.Level, "Shedding", StringComparison.OrdinalIgnoreCase)) hasShed = true;
                    else if (string.Equals(c.Level, "Stop", StringComparison.OrdinalIgnoreCase)) hasStop = true;
                    // Record/Tip 不开灯
                }

            // 表决规则（规范 11.1/11.2：增压器进油压、曲轴箱压力）按 Vote.Level 决定灯
            if (rule?.Vote != null && !string.IsNullOrEmpty(rule.Vote.Level))
            {
                if (string.Equals(rule.Vote.Level, "Alarm", StringComparison.OrdinalIgnoreCase)) hasAlarm = true;
                else if (string.Equals(rule.Vote.Level, "Shedding", StringComparison.OrdinalIgnoreCase)) hasShed = true;
                else if (string.Equals(rule.Vote.Level, "Stop", StringComparison.OrdinalIgnoreCase)) hasStop = true;
            }

            return new ucWarn
            {
                Key = key,
                Title = key,
                Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134))),
                Margin = new Padding(5),
                Size = new Size(561, 37),
                ShowAlarmButton = hasAlarm,
                ShowSheddingButton = hasShed,
                ShowStopButton = hasStop,
                Visible = false
            };
        }

        // 型号切换即时刷新
        /// <summary>
        /// 惰性安装型号切换钩子（只装一次）。订阅 EventTriggerModel.OnModelNameChanged，
        /// 并在控件销毁时反订阅，避免静态事件持有已释放控件造成泄漏/跨线程异常。
        /// </summary>
        private void EnsureEcmModelHook()
        {
            if (_ecmModelHookInstalled) return;
            _ecmModelHookInstalled = true;

            EventTriggerModel.OnModelNameChanged += OnEcmModelChanged;
            this.Disposed += (s, e) =>
            {
                EventTriggerModel.OnModelNameChanged -= OnEcmModelChanged;
            };
        }

        /// <summary>
        /// 型号切换回调。可能在后台线程触发，统一切到 UI 线程后再重建控件。
        /// </summary>
        private void OnEcmModelChanged(string modelName)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;
            try
            {
                if (this.InvokeRequired)
                    this.BeginInvoke(new Action(RefreshAfterModelChanged));
                else
                    RefreshAfterModelChanged();
            }
            catch { /* 控件已销毁，忽略 */ }
        }

        /// <summary>UI 线程：重建动态控件，并立即按新型号当前故障状态重绘报警墙。</summary>
        private void RefreshAfterModelChanged()
        {
            RebuildDynamicEcmWarns();

            // 让报警墙立刻反映新型号的当前故障态（新型号刚初始化通常全为正常）。
            // FaultCheckResend 只对当前活跃故障重发 FaultDetected，幂等、无副作用。
            try { Var.FaultService?.FaultCheckResend(); } catch { }
        }

        /// <summary>
        /// 切到带 JSON 的型号时，熄灭当前型号未引用到的固定灯（型号专属/不适用的灯）。
        /// 命中 keep 的固定灯不碰，由 OnFaultDetected/FaultCheckResend 按故障态自行决定显隐。
        /// 无 JSON(240) → keep 为 null → 本方法不做任何事，固定灯显隐保持原故障驱动逻辑。
        /// </summary>
        private void ApplyFixedWarnVisibility()
        {
            string model = Var.SysConfig?.LastModel;
            bool hasJson = EcmProfileStore.Exists(model);

            // 先把所有固定控件按钮显隐还原到设计期原值（保证 280→240 切回不残留）
            foreach (var w in _fixedWarns)
            {
                if (w == null || w.IsDisposed) continue;
                if (_fixedOrigBtn.TryGetValue(w, out var o))
                {
                    w.ShowAlarmButton = o.Alarm;
                    w.ShowSheddingButton = o.Shed;
                    w.ShowStopButton = o.Stop;
                }
            }

            if (!hasJson) return;   // 240：还原后不做型号联动，原逻辑零影响

            var profile = EcmProfileStore.Load(model);
            var ruleByName = new Dictionary<string, RuleDef>(StringComparer.Ordinal);
            if (profile?.Rules != null)
                foreach (var r in profile.Rules)
                    if (!string.IsNullOrEmpty(r.Name)) ruleByName[r.Name] = r;

            this.flowLayoutPanel1.SuspendLayout();
            try
            {
                foreach (var w in _fixedWarns)
                {
                    if (w == null || w.IsDisposed) continue;

                    if (ruleByName.TryGetValue(w.Key, out var rule))
                    {
                        // 当前型号用得到 → 按规则级别校正三个灯（如 280 的 主油道进口油压 需要降载灯）
                        LevelsOf(rule, out bool a, out bool s, out bool st);
                        w.ShowAlarmButton = a;
                        w.ShowSheddingButton = s;
                        w.ShowStopButton = st;
                        // Visible 不动，交回 OnFaultDetected/FaultCheckResend 按故障态决定
                    }
                    else
                    {
                        w.Visible = false;   // 当前型号用不到 → 熄灭
                    }
                }
            }
            finally
            {
                this.flowLayoutPanel1.ResumeLayout();
            }
        }

        /// <summary>按规则涉及的级别算三个灯（Checks 各级 + Vote.Level），与 frmCurrentWarnDynamic/NewEcmWarn 一致。</summary>
        private static void LevelsOf(RuleDef rule, out bool hasAlarm, out bool hasShed, out bool hasStop)
        {
            hasAlarm = hasShed = hasStop = false;
            if (rule == null) return;

            if (rule.Checks != null)
                foreach (var c in rule.Checks)
                {
                    if (c == null || string.IsNullOrEmpty(c.Level)) continue;
                    if (string.Equals(c.Level, "Alarm", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(c.Level, "Record", StringComparison.OrdinalIgnoreCase)) hasAlarm = true;
                    else if (string.Equals(c.Level, "Shedding", StringComparison.OrdinalIgnoreCase)) hasShed = true;
                    else if (string.Equals(c.Level, "Stop", StringComparison.OrdinalIgnoreCase)) hasStop = true;
                }

            if (rule.Vote != null && !string.IsNullOrEmpty(rule.Vote.Level))
            {
                if (string.Equals(rule.Vote.Level, "Alarm", StringComparison.OrdinalIgnoreCase)) hasAlarm = true;
                else if (string.Equals(rule.Vote.Level, "Shedding", StringComparison.OrdinalIgnoreCase)) hasShed = true;
                else if (string.Equals(rule.Vote.Level, "Stop", StringComparison.OrdinalIgnoreCase)) hasStop = true;
            }
        }

        #endregion
    }
}
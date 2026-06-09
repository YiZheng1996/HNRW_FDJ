using MainUI.Config;
using MainUI.FSql;
using MainUI.Global;
using MainUI.Widget;
using RW;
using RW.UI.Controls;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static MainUI.Modules.EventArgsModel;

namespace MainUI.TestScreen
{
    public partial class ucFormMainControl : UserControl
    {

        // 显示管路系统的窗体
        FrmPipeControl frmPipeControlHMI { get; set; } = null;

        // 记录数据View状态类
        ManaulData manaulData = new ManaulData();

        private FrmScarmOpen _scarmForm;
        /// <summary>
        /// 工况切换前弹窗提示
        /// </summary>
        private FrmScarmOpen scarmForm
        {
            get
            {
                if (_scarmForm == null || _scarmForm.IsDisposed)
                {
                    // 确保在UI线程中创建窗体
                    if (this.InvokeRequired)
                    {
                        return (FrmScarmOpen)this.Invoke(new Func<FrmScarmOpen>(() =>
                            new FrmScarmOpen()));
                    }
                    else
                    {
                        _scarmForm = new FrmScarmOpen();
                    }
                }
                return _scarmForm;
            }
            set { _scarmForm = value; }
        }

        /// <summary>
        /// 开/关到位，泵控制 字典
        /// </summary>
        Dictionary<string, List<RButton>> dicBtn = new Dictionary<string, List<RButton>>();
        /// <summary>
        /// 故障灯状态
        /// </summary>
        Dictionary<string, List<UILedBulb>> dicLight = new Dictionary<string, List<UILedBulb>>();
        /// <summary>
        /// 数值状态
        /// </summary>
        Dictionary<string, Label> dicLabel = new Dictionary<string, Label>();
        /// <summary>
        /// 数值状态
        /// </summary>
        Dictionary<string, ucValueLabel> dicValueLabel = new Dictionary<string, ucValueLabel>();

        // 配方
        PubConfig pub { get; set; } = new PubConfig();

        public ucFormMainControl()
        {
            InitializeComponent();

            if (this.DesignMode) return;

            // 监听按下启机
            EventTriggerModel.OnStartupChanged += (bool isrecord) =>
            {
                if (isrecord)
                {
                    StartupRecord();
                }
                else
                {

                }
            };

            EventTriggerModel.OnParaNameChanged += (string model) =>
            {
                if (!MiddleData.instnce.testDataView.IsTest)
                {
                    LoadParaConfig(model);
                }
            };
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            // 加载控件

            ucStartup1.Init(); // 启机

            timerFast.Enabled = true;
            timerFast.Start();
            Screen3Timer.Enabled = true;
            Screen3Timer.Start();
            timeTiming.Enabled = true;
            timeTiming.Start();
            // 发动手动交互数据
            //SendInteractionTimer.Enabled = true;
            //SendInteractionTimer.Start();

            // 仪表盘量程
            ucParamSpeed.SetRand(0, 1100, 1120);
            ucParamPower.SetRand(0, 6000, 5500);
            ucParamTorque.SetRand(0, 47750, 47000);

            ucParamFuelInletP.SetRand(0, 1100, 1000); //燃油进口压力
            ucParamEngineInP.SetRand(0, 1100, 1000); //机油进口压力
            ucParamEngineOutP.SetRand(0, 1100, 1000); //机油进口压力

            // 添加进字典
            EachControl(grpFJKZ); // 主发通风机
            EachControl(grpFJKZ); // 抽油泵

            Common.AIgrp.KeyValueChange += AIgrp_KeyValueChange;
            Common.AOgrp.KeyValueChange += AOgrp_KeyValueChange;
            Common.DIgrp.KeyValueChange += DIgrp_KeyValueChange;
            Common.DOgrp.KeyValueChange += DOgrp_KeyValueChange;
            Common.excitationGrp.KeyValueChange += ExcitationGrp_KeyValueChange;

            // 更新UI
            this.ucNudLC.Value = Common.AOgrp["励磁调节"];
            this.ucNudSpeed.Value = Common.AOgrp["发动机油门调节"];

            // 柴油机运行时间
            var timer = Math.Round(Var.SysConfig.RunTime / 60, 1);
            this.lblRunTime.Text = timer.ToString();

            int moveIndex = 0;
            // 打开另一个窗体
            if (Screen.AllScreens.Count() > 1)
            {
                // 现场屏幕是 2 1 3 所以把另一个屏，强制放在1屏
                moveIndex = 0;
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

            //TestParaService.instnce.StartRecord();
            // 检查一下最后一次是否选择型号，如果选择好，则默认加载
            if (Var.SysConfig.LastModel != null)
            {
                Common.mTestViewModel.ModelName = Var.SysConfig.LastModel;
                Common.mTestViewModel.ModelType = Var.SysConfig.LastModelType;

                // 加载型号基础参数
                LoadParaConfig(Var.SysConfig.LastModel);
                // 加载配方
                LoadPubConfig();
                EventTriggerModel.RaiseOnModelNameChanged(Var.SysConfig.LastModel);
            }

            // 后台自动采集数据
            var result = TestParaService.instnce.StartRecordAll();
            if (!result)
            {
                Var.MsgBoxWarn(this, "后台数据备份异常！");
            }

            BindWaterLongPressButtons();

            // 通风机开度需要开机默认全部打开100%
            Common.AOgrp["进气风道右调节阀控制"] = 100;
            Common.AOgrp["排气风道右调节阀控制"] = 100;
            Common.AOgrp["进气风道左调节阀控制"] = 100;
            Common.AOgrp["排气风道左调节阀控制"] = 100;
        }

        /// <summary>
        /// 切换型号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectModel_Click(object sender, EventArgs e)
        {
            frmSpec fs = new frmSpec(true);
            fs.ShowDialog();
            if (fs.DialogResult != DialogResult.Yes) return;

            // 保存型号
            Var.SysConfig.LastModel = Common.mTestViewModel.ModelName;
            Var.SysConfig.LastModelType = Common.mTestViewModel.ModelType;
            Var.SysConfig.Save();

            // 加载型号基础参数
            LoadParaConfig(Common.mTestViewModel.ModelName);

            // 加载配方
            LoadPubConfig();

            // 加载TRDP
            LoadTRDPConfig();

            EventTriggerModel.RaiseOnModelNameChanged(Common.mTestViewModel.ModelName);
        }

        /// <summary>
        /// 编号改变时，同步到自动试验页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtChuchanghao_TextChanged(object sender, EventArgs e)
        {
            Common.mTestViewModel.ModelNo = this.txtChuchanghao.Text;
            EventTriggerModel.ModelNumberChanged(this.txtChuchanghao.Text);
        }

        /// <summary>
        /// 设置型号参数
        /// </summary>
        /// <param name="model"></param>
        private void LoadParaConfig(string model)
        {
            // 发给1#工控机
            Common.opcExChangeSendGrp.SetModel = Var.SysConfig.LastModel;

            MiddleData.instnce.SelectModelConfig = new ParaConfig(model);

            MiddleData.instnce.PubsConfig = new PubConfig(model);

            this.txtModel.Text = model;
            this.txtTorque.Text = MiddleData.instnce.SelectModelConfig.RatedTorque.ToString();
            this.txtSpeed.Text = MiddleData.instnce.SelectModelConfig.RatedSpeed.ToString();
            this.txtMinSpeed.Text = MiddleData.instnce.SelectModelConfig.MinSpeed.ToString();

            try
            {
                using (MainUI.Fault.OperationContext.Begin(this, null, string.Format("切换型号[{0}]-下发最低转速", model)))
                {
                    Common.AOgrp["设置发动机最低转速"] = MiddleData.instnce.SelectModelConfig.MinSpeed;
                }
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "台位控制PLC通讯异常，下发最低工作转速失败!" + ex.Message);
            }

            // 下发齿数到转速模块
            try
            {
                using (MainUI.Fault.OperationContext.Begin(this, null,
                    string.Format("切换型号[{0}]-下发齿数", model)))
                {
                    //Common.speedGrp.SetTooth1(); //TODO:齿数固定暂时无法得知，预留
                    Common.speedGrp.SetTooth2(MiddleData.instnce.SelectModelConfig.NumberofTeeth1);
                    Common.speedGrp.SetTooth3(MiddleData.instnce.SelectModelConfig.NumberofTeeth2);
                }
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "转速模块通讯异常，下发齿数失败！" + ex.Message);
            }

        }

        /// <summary>
        /// 加载对应型号的TRDPConfig
        /// </summary>
        public void LoadTRDPConfig()
        {
            string adress = $"{Application.StartupPath}\\TRDPConfig\\{Var.SysConfig.LastModel}.xlsx";
            if (!File.Exists(adress))
            {
                // 配置文件存在
                Var.MsgBoxWarn(this, "该型号的发动机控制器（ECM模块）配置文件不存在！");
                return;
            }

            var msg = Var.TRDP.InitExcel(adress);
            if (msg != "初始化成功")
            {
                Var.MsgBoxWarn(this, msg);
            }
        }

        /// <summary>
        /// 更新配方监听型号更新
        /// </summary>
        /// <param name="obj"></param>
        private void LoadPubConfig()
        {
            //读取配方文件 初始化控件
            pub = new PubConfig(Common.mTestViewModel.ModelName);
            if (pub.PubParaList.Count == 0)
                return;
            var pubInfo = pub.PubParaList[0];
            //励磁电流设置
            //this.ucNudLC.Value = pubInfo.DefaultExcitationCurrent;
            this.ucNudLC.Minimum = pubInfo.MinExcitationCurrent;
            this.ucNudLC.Maximum = pubInfo.MaxExcitationCurrent;
            //转速设置
            //this.ucNudSpeed.Value = pubInfo.DefaultRotationSpeed;
            this.ucNudSpeed.Minimum = pubInfo.MinRotationSpeed;
            this.ucNudSpeed.Maximum = pubInfo.MaxRotationSpeed;
            //流量设置及写入
            if (!Common.opcStatus.NoError)
            {
                Var.MsgBoxWarn(this, "控制柜 PLC 通讯异常。");
                return;
            }

            //给启机/甩车控制面板里的转速和电流初始化
            //转速
            Control control = FindControl(ucStartup1, "nudBeginInvertSpeed");
            if (control is UIDoubleUpDown)
            {
                UIDoubleUpDown dud = control as UIDoubleUpDown;
                //dud.Value = pubInfo.DefaultRotationSpeed;
                dud.Maximum = pubInfo.MaxRotationSpeed;
                dud.Minimum = pubInfo.MinRotationSpeed;
            }
            //电流
            Control control2 = FindControl(ucStartup1, "nudBeginCurrent");
            if (control2 is UIDoubleUpDown)
            {
                UIDoubleUpDown dud = control2 as UIDoubleUpDown;
                //dud.Value = pubInfo.DefaultExcitationCurrent;
                dud.Maximum = pubInfo.MaxExcitationCurrent;
                dud.Minimum = pubInfo.MinExcitationCurrent;
            }

            // 设置甩机启机运行超时时间
            Common.gd350_1.TimeOutPeriod = Var.SysConfig.StartupHoldTimeoutMs;
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

        /// <summary>
        /// 在父窗体里找到子用户控件里的某个控件
        /// </summary>
        /// <param name="container">父窗体</param>
        /// <param name="controlName">寻找的控件的Name属性</param>
        /// <returns></returns>
        public Control FindControl(Control container, string controlName)
        {
            // 1. 先检查当前容器本身是不是要找的控件
            if (container.Name == controlName)
                return container;

            // 2. 递归遍历其所有子控件
            foreach (Control childCtrl in container.Controls)
            {
                // 在子树中查找
                Control foundCtrl = FindControl(childCtrl, controlName);
                if (foundCtrl != null)
                    return foundCtrl;
            }

            // 3. 如果当前子树没找到，返回null
            return null;
        }


        /// <summary>
        /// 开度实时值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AOgrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DoubleValueChangedEventArgs>(AOgrp_KeyValueChange), sender, e);
                return;
            }

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
        /// 开度实时值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AIgrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DoubleValueChangedEventArgs>(AIgrp_KeyValueChange), sender, e);
                return;
            }

            if (e.Key == "水阻箱进水调节阀开度")
            {
                this.lblSZSRealValue.Text = e.Value.ToString();
            }
        }

        /// <summary>
        /// 励磁刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcitationGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DoubleValueChangedEventArgs>(ExcitationGrp_KeyValueChange), sender, e);
                return;
            }

            if (e.Key == "励磁电压检测")
            {
                this.LCVoltageValue.Text = e.Value.ToString();
            }
            else if (e.Key == "励磁电流检测")
            {
                this.LCCurrentValue.Text = e.Value.ToString();
            }

        }


        /// <summary>
        /// 添加控件到字典
        /// </summary>
        /// <param name="con"></param>
        private void EachControl(Control con)
        {
            foreach (Control item in con.Controls)
            {
                EachControl(item);
            }

            if (con is ucValueLabel)
            {
                //添加数值显示条
                ucValueLabel valueLabel = con as ucValueLabel;
                if (valueLabel.Tag != null && valueLabel.Tag.ToString() != string.Empty)
                {
                    dicValueLabel.Add(valueLabel.Tag.ToString(), valueLabel);
                }
            }
            else if (con is UILedBulb)
            {
                //添加灯
                UILedBulb light = con as UILedBulb;
                if (light.Tag != null && light.Tag.ToString() != string.Empty)
                {
                    List<UILedBulb> UILightList;
                    dicLight.TryGetValue(light.Tag.ToString(), out UILightList);
                    if (UILightList == null)
                    {
                        UILightList = new List<UILedBulb>() { };
                        UILightList.Add(light);
                        dicLight.Add(light.Tag.ToString(), UILightList);
                    }
                    else
                    {
                        dicLight[light.Tag.ToString()].Add(light);
                    }
                }
            }
            else if (con is RButton)
            {
                //添加按钮
                RButton btn = con as RButton;
                if (btn.InputTagName != null && btn.InputTagName.ToString() != string.Empty)
                {
                    List<RButton> ButtonList;
                    dicBtn.TryGetValue(btn.InputTagName.ToString(), out ButtonList);
                    if (ButtonList == null)
                    {
                        ButtonList = new List<RButton>() { };
                        ButtonList.Add(btn);
                        dicBtn.Add(btn.InputTagName.ToString(), ButtonList);
                    }
                    else
                    {
                        dicBtn[btn.InputTagName.ToString()].Add(btn);
                    }

                }
            }
            else if (con is Label)
            {
                //添加机组测量值数值显示标签
                Label lbl = con as Label;
                if (lbl.Tag != null && lbl.Tag.ToString() != string.Empty)
                {
                    dicLabel.Add(lbl.Tag.ToString(), lbl);
                }
            }
        }


        /// <summary>
        /// 流量调节
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetFlow_Click(object sender, EventArgs e)
        {
            if (!Common.opcStatus.NoError)
            {
                Var.MsgBoxWarn(this, "控制柜 PLC 通讯异常。");
                return;
            }
            var pipePara = sender as RButton;
            RW.Components.Widgets.frmSetValue set = new RW.Components.Widgets.frmSetValue();
            set.DecimalNumber = 1;
            //按照不同点位给setValue赋值
            if (pub.PubParaList.Count != 0)
            {
                if (pipePara.Tag.ToString() == "进气风道右调节阀控制")
                {
                    set.MaxValue = pub.PubParaList[0].MaxIntakeDuctRightFlow;
                    set.MinValue = pub.PubParaList[0].MinIntakeDuctRightFlow;
                }
                if (pipePara.Tag.ToString() == "进气风道左调节阀控制")
                {
                    set.MaxValue = pub.PubParaList[0].MaxIntakeDuctLeftFlow;
                    set.MinValue = pub.PubParaList[0].MinIntakeDuctLeftFlow;
                }
                if (pipePara.Tag.ToString() == "排气风道右调节阀控制")
                {
                    set.MaxValue = pub.PubParaList[0].MaxExhaustDuctRight;
                    set.MinValue = pub.PubParaList[0].MinExhaustDuctRight;
                }
                if (pipePara.Tag.ToString() == "排气风道左调节阀控制")
                {
                    set.MaxValue = pub.PubParaList[0].MaxExhaustDuctLeft;
                    set.MinValue = pub.PubParaList[0].MinExhaustDuctLeft;
                }
            }
            else
            {
                set.MaxValue = 100;
            }

            set.Unit = UnitEnum.percent;
            set.Text = "流量设置";
            set.Value = Common.AOgrp[pipePara.Tag.ToString()];
            var dr = set.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                using (MainUI.Fault.OperationContext.Begin(this, sender, "流量设置-" + pipePara.Tag.ToString()))
                {
                    Common.AOgrp[pipePara.Tag.ToString()] = set.Value;
                }
            }
        }

        /// <summary>
        /// 水极板的检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DOgrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DIValueChangedEventArgs>(DOgrp_KeyValueChange), sender, e);
                return;
            }

            if (e.Key == "水阻上升控制")
            {
                this.btnWaterPlateUp.Switch = e.Value;
            }
            else if (e.Key == "水阻下降控制")
            {
                this.btnWaterPlateDown.Switch = e.Value;
            }
            else if (e.Key == "水阻箱调节阀开")
            {
                this.btnWaterOpen.Switch = e.Value;
            }
            else if (e.Key == "水阻箱调节阀关")
            {
                this.btnWaterClose.Switch = e.Value;
            }

        }

        /// <summary>
        /// DI值改变事件，用于切换泵的状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DIgrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DIValueChangedEventArgs>(DIgrp_KeyValueChange), sender, e);
                return;
            }

            if (e.Key == "紧急停止")
            {
                EventTriggerModel.ScramChanged(e.Value);

                if (e.Value)
                {

                }
                else
                {
                    // 使用Invoke确保在UI线程中执行
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            if (!scarmForm.IsOpen)
                                scarmForm.ShowInfo();
                        }));
                    }
                    else
                    {
                        if (!scarmForm.IsOpen)
                            scarmForm.ShowInfo();
                    }

                }
            }
            //泵类信号灯特殊处理
            else if (e.Key == "主发通风机1主接检测")
            {
                if (e.Value)
                {
                    this.rButton50.Switch = true;
                    this.rButton95.Switch = false;
                }
                else
                {
                    this.rButton50.Switch = false;
                    this.rButton95.Switch = true;
                }
            }
            else if (e.Key == "主发通风机2主接检测")
            {
                if (e.Value)
                {
                    this.rButton1.Switch = true;
                    this.rButton3.Switch = false;
                }
                else
                {
                    this.rButton1.Switch = false;
                    this.rButton3.Switch = true;
                }
            }
            else if (e.Key == "抽油泵合闸检测")
            {
                if (e.Value)
                {
                    this.rButton2.Switch = true;
                    this.rButton5.Switch = false;
                }
                else
                {
                    this.rButton2.Switch = false;
                    this.rButton5.Switch = true;
                }
            }
            else if (dicBtn.ContainsKey(e.Key))
            {
                foreach (var item in dicBtn[e.Key])
                {
                    item.Switch = e.Value;
                }
            }
        }

        /// <summary>
        /// 计时器检测状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Screen3Timer_Tick(object sender, EventArgs e)
        {
            // 上限/下限位
            this.uiLightUP.State = Common.DIgrp["水阻升降上极限检测"] ? UILightState.On : UILightState.Off;
            this.uiLightDown.State = Common.DIgrp["水阻升降下极限检测"] ? UILightState.On : UILightState.Off;

            // 转速
            this.uiLight1.State = Common.DIgrp["曲轴箱压力开关"] ? UILightState.On : UILightState.Off;
            this.uiLight2.State = Common.DIgrp["盘车连锁开关"] ? UILightState.On : UILightState.Off;
            this.uiLight3.State = Common.DIgrp["柴油机卸载"] ? UILightState.On : UILightState.Off;
            this.uiLight4.State = Common.DIgrp["柴油机停机"] ? UILightState.On : UILightState.Off;

            // 水阻箱温度
            this.lblSZTemp.Text = Common.fuelGrp["水阻箱温度检测"].ToString();

            // 实时重量
            this.WeightValue.Text = MiddleData.instnce.PTFWeight.ToString();

            // 过滤柴油机转速点位写入日志
            using (MainUI.Fault.OperationContext.SuppressLog())
            {
                // 实时下发给下位机
                Common.ExChangeGrp.SetDouble("柴油机转速", MiddleData.instnce.EngineSpeed);
            }

            // 以下点位为kepserver转发
            //Common.ExChangeGrp.SetDouble("高温水出机温度", Common.AI2Grp["T1高温水出机温度"]);
            //Common.ExChangeGrp.SetDouble("高温水进机温度", Common.AI2Grp["T2高温水进机温度"]);
            //Common.ExChangeGrp.SetDouble("预热水箱温度", Common.waterGrp["预热水箱温度检测-T12"]);
            //Common.ExChangeGrp.SetDouble("预热水箱液位", Common.waterGrp["预热水箱液位检测"]);

            //Common.ExChangeGrp.SetDouble("待处理机油箱温度", Common.engineOilGrp["待处理机油箱温度检测-T24"]);
            //Common.ExChangeGrp.SetDouble("待处理机油箱液位", Common.engineOilGrp["待处理机油箱液位检测-L19"]);
            //Common.ExChangeGrp.SetDouble("机油箱温度", Common.engineOilGrp["机油箱温度检测-T23"]);
            //Common.ExChangeGrp.SetDouble("机油箱液位", Common.engineOilGrp["机油箱液位检测-L18"]);
            //Common.ExChangeGrp.SetDouble("机油出机压力", Common.AI2Grp["P20机油泵出口压力"]);
            //Common.ExChangeGrp.SetDouble("机油进机压力", Common.AI2Grp["P21主油道进口油压"]);

            ////todo 暂时不知道哪个点位
            ////Common.ExChangeGrp.SetDouble("内循环水箱液位", Common.engineOilGrp[""]);
            //Common.ExChangeGrp.SetDouble("燃油进机温度", Common.AI2Grp["T31燃油泵进口油温"]);
            //Common.ExChangeGrp.SetDouble("燃油进机压力", Common.AI2Grp["P38燃油供油压力"]);
            //Common.ExChangeGrp.SetDouble("燃油箱液位", Common.fuelGrp["柴油箱液位检测-L29"]);
        }

        /// <summary>
        /// 水极板上升点动按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterPlateUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (uiLightUP.State == UILightState.On)
            {
                Var.MsgBoxWarn(this, "水极板已经在上限位了");
                return;
            }
            _isWPUpPressing = true;
            using (MainUI.Fault.OperationContext.Begin(this, sender, "水极板上升-按下"))
            {
                Common.DOgrp["水阻下降控制"] = false;
                Common.DOgrp["水阻上升控制"] = true;
            }
            try { this.btnWaterPlateUp.Capture = false; } catch { }
        }

        /// <summary>
        /// 水极板上升点动松开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterPlateUp_MouseUp(object sender, MouseEventArgs e)
        {
            _isWPUpPressing = false;
            using (MainUI.Fault.OperationContext.Begin(this, sender, "水极板上升-松开"))
            {
                Common.DOgrp["水阻上升控制"] = false;
                Common.DOgrp["水阻下降控制"] = false;
            }
            try { this.btnWaterPlateUp.Capture = true; } catch { }
        }

        /// <summary>
        /// 水极板下降点动按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterPlateDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (uiLightDown.State == UILightState.On)
            {
                Var.MsgBoxWarn(this, "水极板已经在下限位了");
                return;
            }
            _isWPDownPressing = true;
            using (MainUI.Fault.OperationContext.Begin(this, sender, "水极板下降-按下"))
            {
                Common.DOgrp["水阻上升控制"] = false;
                Common.DOgrp["水阻下降控制"] = true;
            }
            try { this.btnWaterPlateDown.Capture = false; } catch { }
        }
        /// <summary>
        /// 水极板下降点动松开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterPlateDown_MouseUp(object sender, MouseEventArgs e)
        {
            _isWPDownPressing = false;
            try { this.btnWaterPlateDown.Capture = false; } catch { }
            using (MainUI.Fault.OperationContext.Begin(this, sender, "水极板下降-松开"))
            {
                Common.DOgrp["水阻下降控制"] = false;
                Common.DOgrp["水阻上升控制"] = false;
            }
        }

        /// <summary>
        /// 通用点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sw_Valve_Click(object sender, EventArgs e)
        {
            RButton sw = sender as RButton;
            if (sw.Tag == null)
            {
                Var.MsgBoxWarn(this, "程序异常，未绑定Tag值。");
                return;
            }
            if (string.IsNullOrEmpty(sw.Tag.ToString()))
            {
                Var.MsgBoxWarn(this, "程序异常，未绑定Tag值。");
                return;
            }
            if (string.IsNullOrEmpty(sw.OutputTagName.ToString()))
            {
                Var.MsgBoxWarn(this, "程序异常，未绑定OutputTagName值。");
                return;
            }
            // 如果已经打开/关闭 则过滤
            if (sw.Switch == true)
            {
                return;
            }

            string str = sw.Text.Contains("开") ? "打开" : "关闭";
            string th = sw.OutputTagName.ToString().Replace("控制", "").Replace("合闸", "");
            string strMessage = $"是否要{str}{th}?";
            bool result = Var.MsgBoxYesNo(this, strMessage);
            if (result == false)
            {
                return;
            }
            else
            {
                try
                {
                    using (MainUI.Fault.OperationContext.Begin(this, sender, str + th))
                    {
                        Common.DOgrp[sw.OutputTagName.ToString()] = Convert.ToBoolean(sw.Tag.ToInt());//!Common.DOgrp[sw.Tag.ToString()];
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception($"{th}+{str}失败！原因：" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 泵的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sw_Valve_PlusClick(object sender, EventArgs e)
        {
            RButton sw = sender as RButton;
            if (sw.Tag == null)
            {
                Var.MsgBoxWarn(this, "程序异常，未绑定Tag值。");
                return;
            }
            if (string.IsNullOrEmpty(sw.Tag.ToString()))
            {
                Var.MsgBoxWarn(this, "程序异常，未绑定Tag值。");
                return;
            }
            if (string.IsNullOrEmpty(sw.OutputTagName.ToString()))
            {
                Var.MsgBoxWarn(this, "程序异常，未绑定OutputTagName值。");
                return;
            }
            // 如果已经打开/关闭 则过滤
            if (sw.Switch == true)
            {
                return;
            }

            string msg = sw.Text.Contains("开") ? "打开" : "关闭";
            string th = sw.OutputTagName.ToString().Replace("控制", "").Replace("合闸", "");
            var dr = Var.MsgBoxYesNo(this, $"确定要{msg}{th}吗？");
            if (!dr) return;

            // true和false都是脉冲了
            using (MainUI.Fault.OperationContext.Begin(this, sender, msg + th + "(脉冲)"))
            {
                Common.DOgrp[sw.OutputTagName.ToString()] = true;
            }
        }

        /// <summary>
        /// 励磁设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetLC_Click(object sender, EventArgs e)
        {
            this.btnSetLC.Focus();

            // 检查启动柜运行状态
            if (Common.gd350_1.RunningStatus)
            {
                Var.MsgBoxWarn(this, "启动柜运行中，禁止加载。");
                return;
            }

            if (MiddleData.instnce.EngineSpeed >= 350)
            {
                if (!this.rButton50.Switch && !this.rButton1.Switch)
                {
                    Var.MsgBoxWarn(this, "请先打开主发通风机后再进行励磁电流设置。");
                    return;
                }
            }
            using (MainUI.Fault.OperationContext.Begin(this, sender, "手动设定励磁电流"))
            {
                Common.AOgrp["励磁调节"] = ucNudLC.Value;
            }
        }

        /// <summary>
        /// 励磁设置为0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetLCZero_Click(object sender, EventArgs e)
        {
            bool result = Var.MsgBoxYesNo(this, "确定将励磁归0吗？");
            if (result == false)
            {
                return;
            }
            this.btnSetLCZero.Focus();
            using (MainUI.Fault.OperationContext.Begin(this, sender, "励磁紧急归零"))
            {
                Common.AOgrp["励磁调节"] = 0;
            }
            this.ucNudLC.Value = 0;
        }

        /// <summary>
        /// 励磁减1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetLCReduce_Click(object sender, EventArgs e)
        {
            this.btnSetLCReduce.Focus();
            var val = Common.AOgrp["励磁调节"] - 1;
            if (val <= 0) val = 0;
            using (MainUI.Fault.OperationContext.Begin(this, sender, "励磁电流-1"))
            {
                Common.AOgrp["励磁调节"] = val;
            }
            this.ucNudLC.Value = val;
        }

        /// <summary>
        /// 励磁加1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetLCAdd_Click(object sender, EventArgs e)
        {
            this.btnSetLCAdd.Focus();
            // 检查启动柜运行状态
            if (Common.gd350_1.RunningStatus)
            {
                Var.MsgBoxWarn(this, "启动柜运行中，禁止加载。");
                return;
            }

            if (!this.rButton50.Switch && !this.rButton1.Switch)
            {
                Var.MsgBoxWarn(this, "请先打开主发通风机后再进行励磁电流设置。");
                return;
            }

            var val = Common.AOgrp["励磁调节"] + 1;
            if (val >= 500)
            {
                val = 500;
            }
            using (MainUI.Fault.OperationContext.Begin(this, sender, "励磁电流+1"))
            {
                Common.AOgrp["励磁调节"] = val;
            }
            this.ucNudLC.Value = val;
        }

        /// <summary>
        /// 转速设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetSpeed_Click(object sender, EventArgs e)
        {
            this.btnSetSpeed.Focus();
            // 检查启动柜运行状态
            if (Common.gd350_1.RunningStatus)
            {
                Var.MsgBoxWarn(this, "启动柜运行中，禁止加载。");
                return;
            }
            using (MainUI.Fault.OperationContext.Begin(this, sender, "手动设定发动机转速"))
            {
                Common.AOgrp["发动机油门调节"] = ucNudSpeed.Value;
            }
        }

        /// <summary>
        /// 转速减少10
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetSpeedReduce_Click(object sender, EventArgs e)
        {
            this.btnSetSpeedReduce.Focus();
            var button = sender as RButton;
            var tag = button.Tag.ToInt();
            var val = Common.AOgrp["发动机油门调节"] - tag;
            if (val <= 0) val = 0;
            using (MainUI.Fault.OperationContext.Begin(this, sender, string.Format("发动机转速-{0}", tag)))
            {
                Common.AOgrp["发动机油门调节"] = val;
            }
            this.ucNudSpeed.Value = val;
        }

        /// <summary>
        /// 加转速
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetSpeedAdd_Click(object sender, EventArgs e)
        {
            this.btnSetSpeedAdd.Focus();

            // 检查启动柜运行状态
            if (Common.gd350_1.RunningStatus)
            {
                Var.MsgBoxWarn(this, "启动柜运行中，禁止加载。");
                return;
            }

            var button = sender as RButton;
            var tag = button.Tag.ToInt();
            var val = Common.AOgrp["发动机油门调节"] + tag;
            if (val >= 1100) val = 1100;
            using (MainUI.Fault.OperationContext.Begin(this, sender, string.Format("发动机转速+{0}", tag)))
            {
                Common.AOgrp["发动机油门调节"] = val;
            }
            this.ucNudSpeed.Value = val;
        }

        /// <summary>
        /// 快速刷新（仪表盘）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerFast_Tick(object sender, EventArgs e)
        {
            this.ucParamSpeed.GaugeValue = MiddleData.instnce.EngineSpeed;

            // 优先使用扭矩/转速计算功率；无效时回退到机组测量值有功功率
            double enginePower = MiddleData.instnce.EnginePower;
            if (enginePower > 0)
            {
                this.ucParamPower.GaugeValue = enginePower;
            }
            else
            {
                // 有功功率存在 DataValue["有功功率"]，Power 属性未赋值，不能用
                double electricPower = 0;
                Common.threePhaseElectric.DataValue.TryGetValue("有功功率", out electricPower);
                this.ucParamPower.GaugeValue = electricPower;
            }

            this.ucParamTorque.GaugeValue = MiddleData.instnce.EngineTorque;
            this.ucParamFuelInletP.GaugeValue = Common.AI2Grp["P38燃油供油压力"];
            this.ucParamEngineInP.GaugeValue = Common.AI2Grp["P21主油道进口油压"];
            this.ucParamEngineOutP.GaugeValue = Common.AI2Grp["P20机油泵出口压力"];
        }

        /// <summary>
        /// 开 按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterOpen_MouseDown(object sender, MouseEventArgs e)
        {
            _isWVOpenPressing = true;
            using (MainUI.Fault.OperationContext.Begin(this, sender, "水阻箱调节阀开-按下"))
            {
                Common.DOgrp["水阻箱调节阀关"] = false;
                Thread.Sleep(10);
                Common.DOgrp["水阻箱调节阀开"] = true;
            }
            try { this.btnWaterOpen.Capture = false; } catch { }
        }

        /// <summary>
        /// 开 松开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterOpen_MouseUp(object sender, MouseEventArgs e)
        {
            _isWVOpenPressing = false;
            try { this.btnWaterOpen.Capture = false; } catch { }
            using (MainUI.Fault.OperationContext.Begin(this, sender, "水阻箱调节阀开-松开"))
            {
                Common.DOgrp["水阻箱调节阀开"] = false;
                Thread.Sleep(10);
                Common.DOgrp["水阻箱调节阀关"] = false;
            }
        }

        /// <summary>
        /// 关 按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterClose_MouseDown(object sender, MouseEventArgs e)
        {
            _isWVClosePressing = true;
            using (MainUI.Fault.OperationContext.Begin(this, sender, "水阻箱调节阀关-按下"))
            {
                Common.DOgrp["水阻箱调节阀开"] = false;
                Thread.Sleep(10);
                Common.DOgrp["水阻箱调节阀关"] = true;
            }
            try { this.btnWaterClose.Capture = false; } catch { }
        }

        /// <summary>
        /// 关 松开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterClose_MouseUp(object sender, MouseEventArgs e)
        {
            _isWVClosePressing = false;
            try { this.btnWaterClose.Capture = false; } catch { }
            using (MainUI.Fault.OperationContext.Begin(this, sender, "水阻箱调节阀关-松开"))
            {
                Common.DOgrp["水阻箱调节阀关"] = false;
                Thread.Sleep(10);
                Common.DOgrp["水阻箱调节阀开"] = false;
            }
        }

        /// <summary>
        /// 停止水阻进水
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetWaterStop_Click(object sender, EventArgs e)
        {
            manaulData.IsSetWaterResistance = false;
        }

        /// <summary>
        /// 与PLC交互
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendInteractionTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // todo 暂时注释，使用opc发送
                return;
                Common.ExChangeGrp.SetDouble("柴油机转速", MiddleData.instnce.EngineSpeed);

                Common.ExChangeGrp.SetDouble("高温水出机温度", Common.AI2Grp["T1高温水出机温度"]);
                Common.ExChangeGrp.SetDouble("高温水进机温度", Common.AI2Grp["T2高温水进机温度"]);
                Common.ExChangeGrp.SetDouble("预热水箱温度", Common.waterGrp["预热水箱温度检测-T12"]);
                Common.ExChangeGrp.SetDouble("预热水箱液位", Common.waterGrp["预热水箱液位检测"]);

                Common.ExChangeGrp.SetDouble("待处理机油箱温度", Common.engineOilGrp["待处理机油箱温度检测-T24"]);
                Common.ExChangeGrp.SetDouble("待处理机油箱液位", Common.engineOilGrp["待处理机油箱液位检测-L19"]);
                Common.ExChangeGrp.SetDouble("机油箱温度", Common.engineOilGrp["机油箱温度检测-T23"]);
                Common.ExChangeGrp.SetDouble("机油箱液位", Common.engineOilGrp["机油箱液位检测-L18"]);
                Common.ExChangeGrp.SetDouble("机油出机压力", Common.AI2Grp["P20机油泵出口压力"]);
                Common.ExChangeGrp.SetDouble("机油进机压力", Common.AI2Grp["P21主油道进口油压"]);

                //todo 暂时不知道哪个点位
                //Common.ExChangeGrp.SetDouble("内循环水箱液位", Common.engineOilGrp[""]);
                Common.ExChangeGrp.SetDouble("燃油进机温度", Common.AI2Grp["T31燃油泵进口油温"]);
                Common.ExChangeGrp.SetDouble("燃油进机压力", Common.AI2Grp["P38燃油供油压力"]);
                Common.ExChangeGrp.SetDouble("燃油箱液位", Common.fuelGrp["柴油箱液位检测-L29"]);
            }
            catch (Exception ex)
            {
                Var.LogInfo($"实时发送数据给下位机失败。{ex.ToString()}");
            }
        }

        /// <summary>
        /// 开始记录所有模块数据
        /// </summary>
        public void StartupRecord()
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                while (true)
                {
                    try
                    {
                        long startTime = stopwatch.ElapsedMilliseconds;

                        // 检查是否处于记录状态，如果是，则插入数据
                        if (MiddleData.instnce.isStartupRecording)
                        {
                            InsertStartupData();
                        }
                        // 如果已经松手（isRecording为false），并且记录了松手时间
                        else if (MiddleData.instnce.StartupReleaseTime.HasValue)
                        {
                            // 判断从松手到现在是否已超过2秒
                            if ((DateTime.Now - MiddleData.instnce.StartupReleaseTime.Value).TotalSeconds >= 1)
                            {
                                MiddleData.instnce.StartupReleaseTime = null; // 可选的清理操作
                                break;
                            }
                            else
                            {
                                InsertStartupData();
                            }
                        }

                        long endTime = stopwatch.ElapsedMilliseconds;
                        long elapsed = endTime - startTime;
                        long sleepTime = 1000 - elapsed;

                        if (sleepTime > 0)
                        {
                            Thread.Sleep((int)sleepTime);
                        }
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(1000);
                        Debug.WriteLine($"记录启机数据的线程 异常：{ex.Message}");
                    }
                }

                stopwatch.Stop();
            }));
            thread.IsBackground = true;
            thread.Name = "记录启机数据的线程";
            thread.Start();
        }

        /// <summary>
        /// 插入启机的数据
        /// </summary>
        private void InsertStartupData()
        {
            try
            {
                int index = this.dataGridStartup.Rows.Count + 1;
                DateTime dateTime = DateTime.Now;
                string dateTimeStr = dateTime.ToString("yyyy-MM-dd HH:MM:ss");
                string type = MiddleData.instnce.StartupName;
                double speed = MiddleData.instnce.EngineSpeed;
                double torque = MiddleData.instnce.EngineTorque;
                double power = MiddleData.instnce.EnginePower;
                double excitationVoltage = Common.excitationGrp["励磁电压检测"];
                double excitationCurrent = Common.excitationGrp["励磁电流检测"];
                double invertVoltage = Common.gd350_1.OutputVoltage;
                double invertCurrent = Common.gd350_1.OutputCurrent;
                double invertSpeed = Common.gd350_1.OutputSpeed;
                double invertPower = Common.gd350_1.OutputPower;
                int invertFaultCode = Common.gd350_1.FaultCode;

                StartupTestPara startupTestPara = new StartupTestPara();
                startupTestPara.gid = Guid.NewGuid().ToString("N"); // 生成正整数ID
                startupTestPara.Index = index;
                startupTestPara.TestName = type;
                startupTestPara.RecordDataTime = dateTime;
                startupTestPara.RPM = speed;
                startupTestPara.Torque = torque;
                startupTestPara.Power = power;
                startupTestPara.ExcitationVoltage = excitationVoltage;
                startupTestPara.ExcitationCurrent = excitationCurrent;
                startupTestPara.ExcitationCurrentSet = Common.AOgrp["励磁调节"];
                startupTestPara.InvertPower = invertPower;
                startupTestPara.InvertRPM = invertSpeed;
                startupTestPara.InvertVoltage = invertVoltage;
                startupTestPara.InvertCurrent = invertCurrent;
                startupTestPara.InvertPower = invertPower;
                startupTestPara.InvertFaultCode = invertFaultCode;
                this.dataGridStartup.Rows.Insert(0, index, dateTimeStr, type, speed, torque, power, excitationVoltage, excitationCurrent, invertVoltage, invertCurrent, invertSpeed, invertPower, invertFaultCode);

                TestParaService.instnce.SaveRecordStartup(startupTestPara);
            }
            catch (Exception ex)
            {

                Var.LogInfo($"记录 启机、甩车出现异常:{ex.ToString()}");
            }
        }

        /// <summary>
        /// 清空启机表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearDataGridView_Click(object sender, EventArgs e)
        {
            this.dataGridStartup.Rows.Clear();
        }

        /// <summary>
        /// 记录发动机累计时间（小时，带一位小数）：
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timeTiming_Tick(object sender, EventArgs e)
        {
            if (MiddleData.instnce.GetEngineSpeed() > 300)
            {
                Var.SysConfig.RunTime = Var.SysConfig.RunTime + 1;
                Var.SysConfig.Save();

                var timer = Math.Round(Var.SysConfig.RunTime / 60, 1);
                this.lblRunTime.Text = timer.ToString();

                EventTriggerModel.TimngChanged(timer);
            }
        }

        private void btnSetLCReduce2_Click(object sender, EventArgs e)
        {
            this.btnSetLCReduce.Focus();
            var val = Common.AOgrp["励磁调节"] - 2;
            if (val <= 0) val = 0;
            using (MainUI.Fault.OperationContext.Begin(this, sender, "励磁电流-2"))
            {
                Common.AOgrp["励磁调节"] = val;
            }
            this.ucNudLC.Value = val;
        }

        private void btnSetLCReduce5_Click(object sender, EventArgs e)
        {
            this.btnSetLCReduce.Focus();
            var val = Common.AOgrp["励磁调节"] - 5;
            if (val <= 0) val = 0;
            using (MainUI.Fault.OperationContext.Begin(this, sender, "励磁电流-5"))
            {
                Common.AOgrp["励磁调节"] = val;
            }
            this.ucNudLC.Value = val;
        }

        private void btnSetLCAdd2_Click(object sender, EventArgs e)
        {
            this.btnSetLCAdd.Focus();

            if (!this.rButton50.Switch && !this.rButton1.Switch)
            {
                Var.MsgBoxWarn(this, "请先打开主发通风机后再进行励磁电流设置。");
                return;
            }

            var val = Common.AOgrp["励磁调节"] + 2;
            if (val >= 500)
            {
                val = 500;
            }
            using (MainUI.Fault.OperationContext.Begin(this, sender, "励磁电流+2"))
            {
                Common.AOgrp["励磁调节"] = val;
            }
            this.ucNudLC.Value = val;
        }

        private void btnSetLCAdd5_Click(object sender, EventArgs e)
        {
            this.btnSetLCAdd.Focus();

            if (!this.rButton50.Switch && !this.rButton1.Switch)
            {
                Var.MsgBoxWarn(this, "请先打开主发通风机后再进行励磁电流设置。");
                return;
            }

            var val = Common.AOgrp["励磁调节"] + 5;
            if (val >= 500)
            {
                val = 500;
            }
            using (MainUI.Fault.OperationContext.Begin(this, sender, "励磁电流+5"))
            {
                Common.AOgrp["励磁调节"] = val;
            }
            this.ucNudLC.Value = val;
        }

        private void btnSetLCReduce10_Click(object sender, EventArgs e)
        {
            this.btnSetLCReduce.Focus();
            var val = Common.AOgrp["励磁调节"] - 10;
            if (val <= 0) val = 0;
            using (MainUI.Fault.OperationContext.Begin(this, sender, "励磁电流-10"))
            {
                Common.AOgrp["励磁调节"] = val;
            }
            this.ucNudLC.Value = val;
        }

        private void btnSetLCAdd10_Click(object sender, EventArgs e)
        {
            this.btnSetLCAdd.Focus();

            if (!this.rButton50.Switch && !this.rButton1.Switch)
            {
                Var.MsgBoxWarn(this, "请先打开主发通风机后再进行励磁电流设置。");
                return;
            }

            var val = Common.AOgrp["励磁调节"] + 10;
            if (val >= 500)
            {
                val = 500;
            }
            using (MainUI.Fault.OperationContext.Begin(this, sender, "励磁电流+10"))
            {
                Common.AOgrp["励磁调节"] = val;
            }
            this.ucNudLC.Value = val;
        }

        #region 长按按钮安全机制

        #region ====== 水极板/进水阀 长按安全机制 - 字段 ======

        // 释放互斥锁（每个按钮独立，幂等保护）
        private readonly object _wpUpReleaseLock = new object();
        private readonly object _wpDownReleaseLock = new object();
        private readonly object _wvOpenReleaseLock = new object();
        private readonly object _wvCloseReleaseLock = new object();

        // 按下状态标志
        private volatile bool _isWPUpPressing = false;
        private volatile bool _isWPDownPressing = false;
        private volatile bool _isWVOpenPressing = false;
        private volatile bool _isWVClosePressing = false;

        #endregion
        /// <summary>
        /// 绑定水极板/进水阀 4个按钮的安全释放机制。
        /// </summary>
        private void BindWaterLongPressButtons()
        {
            // 水极板上升
            this.btnWaterPlateUp.MouseLeave += (s, e) => ReleaseWaterPlateUp("MouseLeave");
            this.btnWaterPlateUp.LostFocus += (s, e) => ReleaseWaterPlateUp("LostFocus");

            // 水极板下降
            this.btnWaterPlateDown.MouseLeave += (s, e) => ReleaseWaterPlateDown("MouseLeave");
            this.btnWaterPlateDown.LostFocus += (s, e) => ReleaseWaterPlateDown("LostFocus");

            // 进水阀开
            this.btnWaterOpen.MouseLeave += (s, e) => ReleaseWaterValveOpen("MouseLeave");
            this.btnWaterOpen.LostFocus += (s, e) => ReleaseWaterValveOpen("LostFocus");

            // 进水阀关
            this.btnWaterClose.MouseLeave += (s, e) => ReleaseWaterValveClose("MouseLeave");
            this.btnWaterClose.LostFocus += (s, e) => ReleaseWaterValveClose("LostFocus");
        }

        #endregion

        #region ====== 水极板上升 ======

        private void ReleaseWaterPlateUp(string reason)
        {
            lock (_wpUpReleaseLock)
            {
                if (!_isWPUpPressing) return;
                _isWPUpPressing = false;

                // 写两次保险，防止 OPC 偶发丢包
                for (int i = 0; i < 2; i++)
                {
                    try { Common.DOgrp["水阻上升控制"] = false; } catch { }
                }
                try { Common.DOgrp["水阻下降控制"] = false; } catch { }

                try { this.btnWaterPlateUp.Capture = false; } catch { }

                try { Var.LogInfo("水极板上升-释放，原因=" + reason); } catch { }
            }
        }

        #endregion


        #region ====== 水极板下降 ======

        private void ReleaseWaterPlateDown(string reason)
        {
            lock (_wpDownReleaseLock)
            {
                if (!_isWPDownPressing) return;
                _isWPDownPressing = false;

                for (int i = 0; i < 2; i++)
                {
                    try { Common.DOgrp["水阻下降控制"] = false; } catch { }
                }
                try { Common.DOgrp["水阻上升控制"] = false; } catch { }

                try { this.btnWaterPlateDown.Capture = false; } catch { }

                try { Var.LogInfo("水极板下降-释放，原因=" + reason); } catch { }
            }
        }

        #endregion


        #region ====== 进水阀开 ======

        private void ReleaseWaterValveOpen(string reason)
        {
            lock (_wvOpenReleaseLock)
            {
                if (!_isWVOpenPressing) return;
                _isWVOpenPressing = false;

                for (int i = 0; i < 2; i++)
                {
                    try { Common.DOgrp["水阻箱调节阀开"] = false; } catch { }
                }
                try { Common.DOgrp["水阻箱调节阀关"] = false; } catch { }

                try { this.btnWaterOpen.Capture = false; } catch { }

                try { Var.LogInfo("进水阀开-释放，原因=" + reason); } catch { }
            }
        }

        #endregion


        #region ====== 进水阀关 ======

        private void ReleaseWaterValveClose(string reason)
        {
            lock (_wvCloseReleaseLock)
            {
                if (!_isWVClosePressing) return;
                _isWVClosePressing = false;

                for (int i = 0; i < 2; i++)
                {
                    try { Common.DOgrp["水阻箱调节阀关"] = false; } catch { }
                }
                try { Common.DOgrp["水阻箱调节阀开"] = false; } catch { }

                try { this.btnWaterClose.Capture = false; } catch { }

                try { Var.LogInfo("进水阀关-释放，原因=" + reason); } catch { }
            }
        }

        #endregion

        // 控件禁用
        public void SetAdjustButtonsEnabled(bool enabled)
        {
            try
            {
                // 励磁调节组（不含归零）
                SafeSetEnabled(btnSetLC, enabled);
                SafeSetEnabled(btnSetLCAdd, enabled);
                SafeSetEnabled(btnSetLCAdd2, enabled);
                SafeSetEnabled(btnSetLCAdd5, enabled);
                SafeSetEnabled(btnSetLCAdd10, enabled);
                SafeSetEnabled(btnSetLCReduce, enabled);
                SafeSetEnabled(btnSetLCReduce2, enabled);
                SafeSetEnabled(btnSetLCReduce5, enabled);
                SafeSetEnabled(btnSetLCReduce10, enabled);

                // 转速调节组
                SafeSetEnabled(btnSetSpeed, enabled);
                SafeSetEnabled(btnSetSpeedAdd, enabled);
                SafeSetEnabled(btnSetSpeedReduce, enabled);
                SafeSetEnabled(btnSetSpeedReduce20, enabled);
                SafeSetEnabled(btnSetSpeedAdd20, enabled);

                // 水极板/进水阀（启机/甩车期间同样禁用）
                SafeSetEnabled(btnWaterPlateUp, enabled);
                SafeSetEnabled(btnWaterPlateDown, enabled);
                SafeSetEnabled(btnWaterOpen, enabled);
                SafeSetEnabled(btnWaterClose, enabled);

                // btnSetLCZero（励磁紧急归零）始终可用，不加入此列表
            }
            catch (Exception ex)
            {
                try { Var.LogInfo("SetAdjustButtonsEnabled 异常: " + ex.Message); } catch { }
            }
        }


        private void SafeSetEnabled(Control ctrl, bool enabled)
        {
            if (ctrl == null) return;
            try
            {
                if (ctrl.IsDisposed) return;
                if (ctrl.InvokeRequired)
                    ctrl.BeginInvoke((Action)(delegate { try { ctrl.Enabled = enabled; } catch { } }));
                else
                    ctrl.Enabled = enabled;
            }
            catch { }
        }

    }


    /// <summary>
    /// 手动测试的实时状态存储
    /// </summary>
    public class ManaulData
    {
        /// <summary>
        /// 是否正在进行水阻进水值
        /// </summary>
        public bool IsSetWaterResistance { get; set; }

        /// <summary>
        /// 设置水阻进水值
        /// </summary>
        public double SetWaterResistanceValue { get; set; }

        /// <summary>
        /// 发动机设置转速设置
        /// </summary>
        public double BeginInvertSpeed { get; set; }

        /// <summary>
        /// 发动机启机电流设置
        /// </summary>
        public double BeginCurrent { get; set; }

        /// <summary>
        /// 发动机启机是否在运行中
        /// </summary>
        public bool IsStartRun { get; set; }

        /// <summary>
        /// 发动机启机的时间
        /// </summary>
        public DateTime StartRunBeginTime { get; set; }

    }

}
using MainUI.FSql;
using MainUI.Global;
using MainUI.Modules;
using MainUI.Widget;
using RW;
using RW.UI.Controls;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainUI.Config;
using static MainUI.Config.PubConfig;
using System.Diagnostics;
using static MainUI.Modules.EventArgsModel;
using System.IO;

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

            // 暂时隐藏停车停车页面
            tabControl1.TabPages.Remove(tabPage2);

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
            ucShutDown1.Init(); // 停车

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
            ucParamSpeed.SetRand(0, 1100, 1000);
            ucParamPower.SetRand(0, 6000, 5500);
            ucParamTorque.SetRand(0, 47750, 47000);

            ucParamFuelInletP.SetRand(0, 1100, 1000); //燃油进口压力
            ucParamEngineInP.SetRand(0, 1100, 1000); //机油进口压力
            ucParamEngineOutP.SetRand(0, 1100, 1000); //机油进口压力

            // 添加进字典
            EachControl(grpZF1); // 主发通风机
            EachControl(grpCYB); // 抽油泵

            EachControl(groupBox11); // 暂时隐藏

            Common.AIgrp.KeyValueChange += AIgrp_KeyValueChange;
            Common.AOgrp.KeyValueChange += AOgrp_KeyValueChange;
            Common.DIgrp.KeyValueChange += DIgrp_KeyValueChange;
            Common.DOgrp.KeyValueChange += DOgrp_KeyValueChange;
            Common.excitationGrp.KeyValueChange += ExcitationGrp_KeyValueChange;

            // 赋默认值
            manaulData.SetWaterResistanceValue = this.lblSZSSetValue.Text.ToDouble();

            // 更新UI
            this.lblSZSSetValue.Text = Common.AIgrp["水阻箱进水调节阀开度"].ToString();
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
                Common.AOgrp["设置发动机最低转速"] = MiddleData.instnce.SelectModelConfig.MinSpeed;
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "台位控制PLC通讯异常，下发最低工作转速失败!");
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
            //水阻箱进水设置值
            this.lblSZSSetValue.Text = pubInfo.DefaultWaterResistanceTankInlet.ToString();
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
            //正常停车控制
            //转速
            Control control3 = FindControl(ucShutDown1, "nudBeginInvertSpeed");
            if (control3 is UIDoubleUpDown)
            {
                UIDoubleUpDown dud = control3 as UIDoubleUpDown;
                //dud.Value = pubInfo.DefaultRotationSpeed;
                dud.Maximum = pubInfo.MaxRotationSpeed;
                dud.Minimum = pubInfo.MinRotationSpeed;
            }
            //电流
            Control control4 = FindControl(ucShutDown1, "nudBeginCurrent");
            if (control4 is UIDoubleUpDown)
            {
                UIDoubleUpDown dud = control4 as UIDoubleUpDown;
                //dud.Value = pubInfo.DefaultExcitationCurrent;
                dud.Maximum = pubInfo.MaxExcitationCurrent;
                dud.Minimum = pubInfo.MinExcitationCurrent;
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
                Common.AOgrp[pipePara.Tag.ToString()] = set.Value;
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
                this.btnSJBS.Switch = e.Value;
            }
            else if (e.Key == "水阻下降控制")
            {
                this.btnSJBJ.Switch = e.Value;
            }
            else if (e.Key == "水阻箱调节阀开")
            {
                this.rButton9.Switch = e.Value;
            }
            else if (e.Key == "水阻箱调节阀关")
            {
                this.rButton4.Switch = e.Value;
            }
            else if (e.Key == "发动机DC24V供电")
            {
                if (e.Value)
                {
                    this.rButton10.Switch = true;
                    this.rButton11.Switch = false;
                }
                else
                {
                    this.rButton10.Switch = false;
                    this.rButton11.Switch = true;
                }
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

            // 实时下发给下位机
            Common.ExChangeGrp.SetDouble("柴油机转速", MiddleData.instnce.EngineSpeed);

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
            Common.DOgrp["水阻下降控制"] = false;
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
            Common.DOgrp["水阻下降控制"] = false;
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
            Common.DOgrp["水阻上升控制"] = false;
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
            Common.DOgrp["水阻上升控制"] = false;
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
                    Common.DOgrp[sw.OutputTagName.ToString()] = Convert.ToBoolean(sw.Tag.ToInt()); //!Common.DOgrp[sw.Tag.ToString()];
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
            Common.DOgrp[sw.OutputTagName.ToString()] = true;
        }

        /// <summary>
        /// 励磁设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetLC_Click(object sender, EventArgs e)
        {
            this.btnSetLC.Focus();

            if (!this.rButton50.Switch && !this.rButton1.Switch)
            {
                Var.MsgBoxWarn(this,"请先打开主发通风机后再进行励磁电流设置。");
                return;
            }
            Common.AOgrp["励磁调节"] = ucNudLC.Value;
        }

        /// <summary>
        /// 励磁设置为0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetLCZero_Click(object sender, EventArgs e)
        {
            this.btnSetLCZero.Focus();
            Common.AOgrp["励磁调节"] = 0;
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
            if (val <= 0)
            {
                val = 0;
            }
            Common.AOgrp["励磁调节"] = val;
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
            Common.AOgrp["励磁调节"] = val;
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
            Common.AOgrp["发动机油门调节"] = ucNudSpeed.Value;
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
            if (val <= 0)
            {
                val = 0;
            }
            Common.AOgrp["发动机油门调节"] = val;
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
        /// 快速刷新（仪表盘）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerFast_Tick(object sender, EventArgs e)
        {
            this.ucParamSpeed.GaugeValue = MiddleData.instnce.EngineSpeed;
            this.ucParamPower.GaugeValue = MiddleData.instnce.EnginePower;
            this.ucParamTorque.GaugeValue = MiddleData.instnce.EngineTorque;
            this.ucParamFuelInletP.GaugeValue = Common.AI2Grp["P38燃油供油压力"];
            this.ucParamEngineInP.GaugeValue = Common.AI2Grp["P21主油道进口油压"];
            this.ucParamEngineOutP.GaugeValue = Common.AI2Grp["P20机油泵出口压力"];
        }

        /// <summary>
        /// 水阻箱进水设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetWaterValueIn_Click(object sender, EventArgs e)
        {
            if (!Common.opcStatus.NoError)
            {
                Var.MsgBoxWarn(this, "控制柜 PLC 通讯异常。");
                return;
            }

            // 先检查是否正在执行
            if (manaulData.IsSetWaterResistance)
            {
                Var.MsgBoxWarn(this, "正在进行水阻值调节，请先停止当前操作。");
                return;
            }

            var pipePara = sender as RButton;
            RW.Components.Widgets.frmSetValue set = new RW.Components.Widgets.frmSetValue();
            set.DecimalNumber = 1;
            set.MaxValue = 100;
            set.Unit = UnitEnum.percent;
            set.Text = "流量设置";
            set.Value = manaulData.SetWaterResistanceValue;

            var dr = set.ShowDialog(this);
            if (dr != DialogResult.OK)
                return;  // 用户取消

            // 更新UI
            this.rButton9.Enabled = false;
            this.rButton4.Enabled = false;

            manaulData.SetWaterResistanceValue = set.Value;
            this.lblSZSSetValue.Text = set.Value.ToString();

            // 设置按钮状态
            this.btnSetWaterValueIn.Switch = true;

            // 启动调节线程
            Thread thread = new Thread(() =>
            {
                try
                {
                    // 设置执行标志
                    manaulData.IsSetWaterResistance = true;

                    // 计算阀门操作方向
                    bool needOpen;  // true:需要开阀 false:需要关阀
                    if (manaulData.SetWaterResistanceValue > Common.AIgrp["水阻箱进水调节阀开度"])
                    {
                        Common.DOgrp["水阻箱调节阀关"] = false;
                        Common.DOgrp["水阻箱调节阀开"] = true;
                        needOpen = true;
                    }
                    else
                    {
                        Common.DOgrp["水阻箱调节阀开"] = false;
                        Common.DOgrp["水阻箱调节阀关"] = true;
                        needOpen = false;
                    }

                    // 等待达到目标值或被中断
                    while (manaulData.IsSetWaterResistance)
                    {
                        double currentValue = Common.AIgrp["水阻箱进水调节阀开度"];

                        // 检查是否达到目标
                        if (needOpen)
                        {
                            if (currentValue >= manaulData.SetWaterResistanceValue - 1)
                                break;
                        }
                        else
                        {
                            if (currentValue + 1 <= manaulData.SetWaterResistanceValue)
                                break;
                        }

                        Thread.Sleep(100);
                    }
                }
                catch (Exception ex)
                {
                    // 记录日志
                    Var.LogInfo($"水阻箱调节异常:{ex}");
                }
                finally
                {
                    // 确保阀门停止
                    Common.DOgrp["水阻箱调节阀开"] = false;
                    Common.DOgrp["水阻箱调节阀关"] = false;

                    // 重置状态标志
                    manaulData.IsSetWaterResistance = false;

                    // 更新UI
                    this.Invoke(new Action(() =>
                    {
                        this.rButton9.Enabled = true;
                        this.rButton4.Enabled = true;
                        this.btnSetWaterValueIn.Switch = false;
                    }));
                }
            });

            thread.Name = "水阻箱进水动态调节";
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// 开 按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterOpen_MouseDown(object sender, MouseEventArgs e)
        {
            Common.DOgrp["水阻箱调节阀关"] = false;
            Thread.Sleep(10);
            Common.DOgrp["水阻箱调节阀开"] = true;
        }

        /// <summary>
        /// 开 松开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterOpen_MouseUp(object sender, MouseEventArgs e)
        {
            Common.DOgrp["水阻箱调节阀开"] = false;
            Thread.Sleep(10);
            Common.DOgrp["水阻箱调节阀关"] = false;
        }

        /// <summary>
        /// 关 按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterClose_MouseDown(object sender, MouseEventArgs e)
        {
            Common.DOgrp["水阻箱调节阀开"] = false;
            Thread.Sleep(10);
            Common.DOgrp["水阻箱调节阀关"] = true;
        }

        /// <summary>
        /// 关 松开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterClose_MouseUp(object sender, MouseEventArgs e)
        {
            Common.DOgrp["水阻箱调节阀关"] = false;
            Thread.Sleep(10);
            Common.DOgrp["水阻箱调节阀开"] = false;
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
                Var.LogInfo($"实时发送数据给下位机失败。{ ex.ToString() }");
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

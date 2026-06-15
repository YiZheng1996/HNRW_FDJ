using MainUI.Global;
using MainUI.TestScreen;
using RW.UI.Controls;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using static MainUI.Modules.EventArgsModel;

namespace MainUI.Widget
{
    /// <summary>
    /// 启机 甩车 页面
    /// </summary>
    public partial class ucStartup : UserControl
    {
        // 超时计时器（启机/甩车共用一个，因为两者互斥）
        private System.Windows.Forms.Timer _startupTimeoutTimer;
        private System.Windows.Forms.Timer _shakeTimeoutTimer;

        // 启机弹窗描述
        frmMessageWarning frmStartupMessage = new frmMessageWarning();

        private Dictionary<string, ucValueLabel> _electricValueLabels = new Dictionary<string, ucValueLabel>();

        ManaulData manaulData = new ManaulData();

        public ucStartup()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            this.timerSlow.Enabled = true;
            this.timerSlow.Start();

            this.nudBeginCurrent.Value = Common.AOgrp["励磁调节"];
            manaulData.BeginInvertSpeed = nudBeginInvertSpeed.Value;
            manaulData.BeginCurrent = nudBeginCurrent.Value;

            Common.gd350_1.KeyValueChange += Gd350_1_KeyValueChange;
            Common.DIgrp.KeyValueChange += DIgrp_KeyValueChange;
            Common.DOgrp.KeyValueChange += DOgrp_KeyValueChange;
            Common.AOgrp.KeyValueChange += AOgrp_KeyValueChange;

            // 扫描拖入的 ucValueLabel，按 Tag 注册到字典
            EachElectricControl(grpElectric);

            // 订阅三相电事件
            Common.threePhaseElectric.KeyValueChange += ThreePhaseElectric_KeyValueChange;

            // 立刻刷新一次当前值
            Common.threePhaseElectric.Fresh();

            // 绑定长按安全机制
            BindLongPressButtons();
        }

        /// <summary>
        /// 绑定长按安全机制（Capture、超时、联动锁定）已直接注入原有方法内部。
        /// </summary>
        private void BindLongPressButtons()
        {
            // 超时计时器 —— Tick 时调用原有松开方法
            _startupTimeoutTimer = new System.Windows.Forms.Timer
            {
                Interval = Var.SysConfig.StartupHoldTimeoutMs
            };
            _startupTimeoutTimer.Tick += (s, e) =>
            {
                if (manaulData.IsStartRun)
                {
                    Var.LogInfo("启机/甩车超时自动释放");
                    btnManualRun_MouseUp(null, null);
                }
            };

            _shakeTimeoutTimer = _startupTimeoutTimer; // 启机/甩车互斥，共用同一个计时器

            // 启机按钮：MouseLeave / LostFocus → 有按下状态时触发松开
            this.btnManualStart.MouseLeave += (s, e) =>
            {
                if (manaulData.IsStartRun) btnManualRun_MouseUp(null, null);
            };
            this.btnManualStart.LostFocus += (s, e) =>
            {
                if (manaulData.IsStartRun) btnManualRun_MouseUp(null, null);
            };

            // 甩车按钮：MouseLeave / LostFocus → 有按下状态时触发松开
            this.btnManualShake.MouseLeave += (s, e) =>
            {
                if (manaulData.IsStartRun) btnManualRun_MouseUp(null, null);
            };
            this.btnManualShake.LostFocus += (s, e) =>
            {
                if (manaulData.IsStartRun) btnManualRun_MouseUp(null, null);
            };
        }

        /// <summary>
        /// 递归扫描容器，将 Tag 不为空的 ucValueLabel 加入字典
        /// </summary>
        private void EachElectricControl(Control container)
        {
            foreach (Control ctrl in container.Controls)
            {
                EachElectricControl(ctrl);
            }
            if (container is ucValueLabel lbl
                && lbl.Tag != null
                && !string.IsNullOrEmpty(lbl.Tag.ToString()))
            {
                string key = lbl.Tag.ToString();
                if (!_electricValueLabels.ContainsKey(key))
                    _electricValueLabels.Add(key, lbl);
            }
        }

        /// <summary>
        /// 三相电参数刷新（机组测量值）
        /// </summary>
        private void ThreePhaseElectric_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DoubleValueChangedEventArgs>(
                    ThreePhaseElectric_KeyValueChange), sender, e);
                return;
            }
            if (_electricValueLabels.ContainsKey(e.Key))
            {
                _electricValueLabels[e.Key].Value = e.Value;
            }
        }

        /// <summary>
        /// DO 检测
        /// </summary>
        private void DOgrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DIValueChangedEventArgs>(DOgrp_KeyValueChange), sender, e);
                return;
            }
        }

        /// <summary>
        /// 励磁电流同步刷新
        /// 当 ucFormMainControl 里的加减按钮修改了 OPC 值后，
        /// OPC 回调触发此方法，保持 nudBeginCurrent 与 ucNudLC 同步。
        /// </summary>
        private void AOgrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DoubleValueChangedEventArgs>(
                    AOgrp_KeyValueChange), sender, e);
                return;
            }

            if (e.Key == "励磁调节")
            {
                // 启机过程中不要覆盖 nudBeginCurrent，避免干扰正在执行的启机
                if (manaulData.IsStartRun) return;

                this.nudBeginCurrent.Value = e.Value;
                manaulData.BeginCurrent = e.Value;   // 同步到内存，下次启机直接使用最新值
            }
        }

        bool FirstReturn = false;
        /// <summary>
        /// DI值改变事件，用于切换泵的状态
        /// </summary>
        private void DIgrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DIValueChangedEventArgs>(DIgrp_KeyValueChange), sender, e);
                return;
            }

            if (e.Key == "柴油机启动")
            {
                // 避免第一次触发
                if (!FirstReturn)
                {
                    FirstReturn = true;
                    return;
                }

                if (e.Value)
                {
                    string type = "启机";
                    if (this.uiLightShake.State == UILightState.On)
                        type = "甩车";
                    btnManualRunDown(type);  // 硬件按钮路径：复用原有完整逻辑（含新功能）
                }
                else
                {
                    btnManualRun_MouseUp(null, null);  // 硬件松开：复用原有释放逻辑（含新功能）
                }
            }
        }

        /// <summary>
        /// 变频器的值改变事件
        /// </summary>
        private void Gd350_1_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (e.Key == "就绪")
            {
                this.LightInvertReady.State = e.Value == 1 ? UILightState.On : UILightState.Off;
            }
            else if (e.Key == "故障代码")
            {
                var faultResult = e.Value > 1;
                this.LightInvertFault.State = faultResult ? UILightState.On : UILightState.Off;
                this.lblFaultCode.Text = $"故障代码: {e.Value.ToString()}";
                this.lblFaultCode.Visible = faultResult ? true : false;
            }
        }

        /// <summary>
        /// 指示灯的状态（慢速）
        /// </summary>
        private void timerSlow_Tick(object sender, EventArgs e)
        {
            var speed = Common.speedGrp["转速1"] < 100;
            var status = Common.DIgrp["盘车连锁开关"];
            var status2 = Common.DOgrp["发动机启停预启动"] == true;
            var status3 = Common.DIgrp["水阻升降上极限检测"] == true;
            var status4 = Common.gd350_1.Ready == 1;
            var status5 = MiddleData.instnce.IsOpenFuelCycle() == true;
            var lcStatus = Common.AOgrp["励磁调节"] > 0;
            var speedStatus = manaulData.BeginInvertSpeed > 0;
            var status8 = MiddleData.instnce.IsOpenEngineCycle() == true;
            var status9 = Common.DOgrp["发动机DC24V供电"] == true;
            var status10 = Common.gd350_1.RunningStatus == false;
            var statusStop = Common.DIgrp["柴油机停止"] == false;

            this.uiLightStart.State = (speed && status && status2 && status3 && status4 && status5 && status8 && status9 && lcStatus && speedStatus && status10 && statusStop) ? UILightState.On : UILightState.Off;
            this.uiLightShake.State = (speed && status && status2 && status3 && status4 && !status5 && status8 && !status9 && lcStatus && speedStatus) ? UILightState.On : UILightState.Off;

            this.uiLightWaterUP.State = status3 ? UILightState.On : UILightState.Off;

            this.uiLightFuelCycle.State = status5 ? UILightState.On : UILightState.Off;
            this.uiLightFuelCycleClose.State = !status5 ? UILightState.On : UILightState.Off;
            this.btnFuelCycleOpen.Switch = status5;

            this.uiLightEO.State = status8 ? UILightState.On : UILightState.Off;
            this.btnEoOpen.Switch = status8;

            this.uiLightPC.State = status ? UILightState.On : UILightState.Off;
            this.LightInvertReady.State = status4 ? UILightState.On : UILightState.Off;

            this.uiLightDV24Open.State = status9 ? UILightState.On : UILightState.Off;
            this.uiLightDV24Close.State = !status9 ? UILightState.On : UILightState.Off;

            this.LightInvertRunning.State = (Common.gd350_1.RunStatusAI == 1 || Common.gd350_1.RunStatusAI == 2) ? UILightState.On : UILightState.Off;
            this.lblInverterVoltage.Text = Common.gd350_1.OutputVoltage.ToString();
            this.lblInverterCurrent.Text = Common.gd350_1.OutputCurrent.ToString();
            this.lblInverterPower.Text = Common.gd350_1.OutputPower.ToString();

            uiLightStop.State = statusStop ? UILightState.On : UILightState.Off;

            if (Common.DOgrp["发动机DC24V供电"])
            {
                this.btnDC24VOpen.Switch = true;
                this.btnDC24VClose.Switch = false;
            }
            else
            {
                this.btnDC24VOpen.Switch = false;
                this.btnDC24VClose.Switch = true;
            }

            UpdateStartupParaLock();
        }

        /// <summary>
        /// 启机参数锁定：发动机转速 > 300 视为启机完成，
        /// 锁定"变频器转速"和"励磁电流"的设置输入框及设置按钮，
        /// 转速回落到 ≤300（即停机）后自动解锁。
        /// </summary>
        private void UpdateStartupParaLock()
        {
            try
            {
                bool enabled = MiddleData.instnce.GetEngineSpeed() <= 300;

                this.nudBeginInvertSpeed.Enabled = enabled;
                this.nudBeginCurrent.Enabled = enabled;
                this.btnSetBeginSpeed.Enabled = enabled;
                this.btnSetBeginLC.Enabled = enabled;
            }
            catch (Exception ex)
            {
                try { Var.LogInfo("UpdateStartupParaLock 异常: " + ex.Message); } catch { }
            }
        }

        /// <summary>
        /// 关闭开启按钮通用点击事件
        /// </summary>
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
            if (sw.Switch == true)
            {
                return;
            }

            string str = sw.Text.Contains("开") ? "打开" : "关闭";
            string th = sw.OutputTagName.ToString().Replace("控制", "").Replace("合闸", "");

            if (sw.Text.Contains("开"))
            {
                // 先做气压检测（仅燃油循环和预供机油循环）
                if (sw.OutputTagName == "燃油循环" || sw.OutputTagName == "预供机油循环")
                {
                    var Pressure1 = Common.AIgrp["厂房进气压力检测1"];
                    var Pressure2 = Common.AIgrp["厂房进气压力检测2"];
                    if (Pressure1 < 200 && Pressure2 < 200)
                    {
                        Var.MsgBoxWarn(this, "厂房压力低，不能进行一键操作。");
                        return;
                    }
                }

                string strMessage = $"是否要{str}{th}?";
                bool result = Var.MsgBoxYesNo(this, strMessage);
                if (result == false)
                {
                    return;
                }
            }

            try
            {
                using (MainUI.Fault.OperationContext.Begin(this, sender, str + th))
                {
                    if (sw.OutputTagName == "发动机DC24V供电")
                    {
                        Common.DOgrp["发动机DC24V供电"] = Convert.ToBoolean(sw.Tag.ToInt());
                    }

                    if (sw.OutputTagName == "燃油循环" || sw.OutputTagName == "预供机油循环")
                    {
                        Common.ExChangeGrp.SetBool(sw.OutputTagName, Convert.ToBoolean(sw.Tag.ToInt()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{th}+{str}失败！原因：" + ex.Message);
            }
        }


        /// <summary>
        /// 设置转速更改
        /// </summary>
        private void nudBeginInvertSpeed_ValueChanged(object sender, double value)
        {
            manaulData.BeginInvertSpeed = value;
        }

        /// <summary>
        /// 励磁电流值改变
        /// </summary>
        private void nudBeginCurrent_ValueChanged(object sender, double value)
        {
            manaulData.BeginCurrent = value;
        }

        /// <summary>
        /// 启机转速设置
        /// </summary>
        private void btnSetBeginSpeed_Click(object sender, EventArgs e)
        {
            if (MiddleData.instnce.GetEngineSpeed() > 300)
            {
                Var.MsgBoxWarn(this, "发动机运行中，启机参数不可调整，请等待停机后再设置。");
                return;
            }

            this.btnSetBeginSpeed.Focus();

            var value = Math.Round((manaulData.BeginInvertSpeed * 7) / 60, 2);
            using (MainUI.Fault.OperationContext.Begin(this, sender, "启机-设置变频器频率"))
            {
                Common.gd350_1.SetFrequency = value;
            }

        }

        /// <summary>
        /// 启机励磁设置
        /// </summary>
        private void btnSetBeginLC_Click(object sender, EventArgs e)
        {
            if (MiddleData.instnce.GetEngineSpeed() > 300)
            {
                Var.MsgBoxWarn(this, "发动机运行中，启机参数不可调整，请等待停机后再设置。");
                return;
            }

            this.btnSetBeginLC.Focus();

            if (MiddleData.instnce.EngineSpeed >= 350)
            {
                if (!Common.DIgrp["主发通风机1主接检测"] && !Common.DIgrp["主发通风机2主接检测"])
                {
                    Var.MsgBoxWarn(this, "请先打开主发通风机后再进行励磁电流设置。");
                    return;
                }
            }

            using (MainUI.Fault.OperationContext.Begin(this, sender, "启机-设置励磁电流"))
            {
                double current = manaulData.BeginCurrent;

                // 按当前型号配方参数中的励磁电流最大值判断：超过则输出0
                try
                {
                    var pubsConfig = MiddleData.instnce.PubsConfig;
                    if (pubsConfig != null && pubsConfig.PubParaList.Count > 0)
                    {
                        double maxExcitation = pubsConfig.PubParaList[0].MaxExcitationCurrent;
                        if (current > maxExcitation)
                        {
                            current = 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Var.LogInfo($"启机励磁设置-读取配方参数异常:{ex.ToString()}");
                }

                Common.AOgrp["励磁调节"] = current;
                nudBeginCurrent.Value = current;
            }
        }

        /// <summary>
        /// 启机按下
        /// </summary>
        private void btnManualRun_MouseDown(object sender, MouseEventArgs e)
        {
            var button = sender as RButton;
            var tag = button.Tag.ToString();
            btnManualRunDown(tag);
        }

        /// <summary>
        /// 按钮按下
        /// </summary>
        private void btnManualRunDown(string tag)
        {
            try
            {
                int msgIndex = 0;
                StringBuilder errorMessages = new StringBuilder();

                // 当前是启机还是甩车
                MiddleData.instnce.StartupName = tag;

                // 检查水阻升降上极限检测
                if (!Common.DIgrp["水阻升降上极限检测"])
                {
                    errorMessages.AppendLine($"{++msgIndex}. 请先把水极板上升至上限位。");
                }

                // 检查转速
                if (Common.speedGrp["转速1"] > 100)
                {
                    errorMessages.AppendLine($"{++msgIndex}. 请先等待发动机转速降低至100rpm后再启机。");
                }

                // 盘车联锁信号
                if (Common.DIgrp["盘车连锁开关"] == false)
                {
                    errorMessages.AppendLine($"{++msgIndex}. 盘车连锁开关未处于闭合状态。");
                }

                // 桌面按钮，柴油机停止新年好检测
                if (Common.DIgrp["柴油机停止"] == true)
                {
                    errorMessages.AppendLine($"{++msgIndex}. 桌面柴油机停止处于按下状态，请取消。");
                }

                // 检查发动机启停预启动
                if (!Common.DOgrp["发动机启停预启动"])
                {
                    errorMessages.AppendLine($"{++msgIndex}. 发动机启停预启动还未复位，请先等待发动机启停预启动信号复位。");
                }

                // 检查发电机就绪状态
                if (Common.gd350_1.Ready == 0)
                {
                    errorMessages.AppendLine($"{++msgIndex}. 发电机未就绪。");
                }

                // 检查发电机故障状态
                if (Common.gd350_1.FaultCode > 0)
                {
                    errorMessages.AppendLine($"{++msgIndex}. 发电机正处于故障中,请先故障复位。");
                }

                // 启动柜运行中，无法再次启动
                if (Common.gd350_1.RunningStatus)
                {
                    errorMessages.AppendLine($"{++msgIndex}. 启动柜运行中,无法再次启动。");
                }

                // 预供机油循环
                if (MiddleData.instnce.IsOpenEngineCycle() == false)
                {
                    errorMessages.AppendLine($"{++msgIndex}. 请先开始预供机油循环。");
                }

                if (Common.AOgrp["励磁调节"] == 0)
                {
                    errorMessages.AppendLine($"{++msgIndex}. 励磁电流不能为 0 A。");
                }

                if (manaulData.BeginInvertSpeed == 0)
                {
                    errorMessages.AppendLine($"{++msgIndex}. 启机转速不能为 0 rpm。");
                }

                if (tag == "启机")
                {
                    if (MiddleData.instnce.IsOpenFuelCycle() == false)
                    {
                        errorMessages.AppendLine($"{++msgIndex}. 请先开始燃油循环。");
                    }
                    if (uiLightDV24Open.State == UILightState.Off)
                    {
                        errorMessages.AppendLine($"{++msgIndex}. 请先打开发动机控制盒电源 DC24V。");
                    }
                }
                else
                {
                    if (MiddleData.instnce.IsOpenFuelCycle() == true)
                    {
                        errorMessages.AppendLine($"{++msgIndex}. 请先关闭燃油循环。");
                    }
                    if (uiLightDV24Open.State == UILightState.On)
                    {
                        errorMessages.AppendLine($"{++msgIndex}. 请先关闭发动机控制盒电源 DC24V。");
                    }
                }

                // 如果有错误信息，统一显示
                if (errorMessages.Length > 0)
                {
                    string message = $"不满足{tag}条件：\n\n" + errorMessages;

                    if (frmStartupMessage != null && !frmStartupMessage.IsDisposed && frmStartupMessage.Visible)
                    {
                        frmStartupMessage.Msg = message;
                    }
                    else
                    {
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

                if (tag == "启机")
                {
                    this.btnManualStart.Switch = true;
                }
                else
                {
                    this.btnManualShake.Switch = true;
                }

                manaulData.IsStartRun = true;
                manaulData.StartRunBeginTime = DateTime.Now;

                using (MainUI.Fault.OperationContext.Begin(this, null, tag + "流程-按下"))
                {
                    Common.AOgrp["励磁调节"] = manaulData.BeginCurrent;

                    var value = Math.Round((manaulData.BeginInvertSpeed * 7) / 60, 2);
                    Common.gd350_1.SetFrequency = value;
                    Common.gd350_1.SetRun = true;
                }

                MiddleData.instnce.isStartupRecording = true;
                MiddleData.instnce.StartupReleaseTime = null;
                EventTriggerModel.StartupChanged(true);

                // 主动释放系统自动设置的鼠标捕获，让 MouseLeave 能正常触发
                try
                {
                    if (tag == "启机")
                        this.btnManualStart.Capture = false;
                    else
                        this.btnManualShake.Capture = false;
                }
                catch { }

                // 启动超时计时器（每次按下重新读取配置，支持热更新）
                try
                {
                    _startupTimeoutTimer.Stop();
                    _startupTimeoutTimer.Interval = Var.SysConfig.StartupHoldTimeoutMs;
                    _startupTimeoutTimer.Start();
                }
                catch { }

                // 联动锁定（禁用 ucFormMainControl 里的励磁/转速调节按钮）
                try { SetStartupAdjustLock(false); } catch { }
            }
            catch (Exception ex)
            {
                MiddleData.instnce.isStartupRecording = false;
                MiddleData.instnce.StartupReleaseTime = DateTime.Now;

                using (MainUI.Fault.OperationContext.Begin(this, null, tag + "流程-异常退出"))
                {
                    manaulData.IsStartRun = false;
                    Common.gd350_1.SetRun = false;
                }

                Var.MsgBoxWarn(this, $"启机出现异常:{ex.Message.ToString()}");
                Var.LogInfo($"点动-启机出现异常:{ex.ToString()}");
            }
        }

        /// <summary>
        /// 启机松开
        /// </summary>
        private void btnManualRun_MouseUp(object sender, MouseEventArgs e)
        {
            using (MainUI.Fault.OperationContext.Begin(this, sender, "启机流程-松开"))
            {
                // 松开励磁写0
                Common.AOgrp["励磁调节"] = 0;
                Common.gd350_1.SetRun = false;
            }

            this.nudBeginCurrent.Value = 0;

            MiddleData.instnce.isStartupRecording = false;
            MiddleData.instnce.StartupReleaseTime = DateTime.Now;

            this.btnManualStart.Switch = false;
            this.btnManualShake.Switch = false;

            manaulData.IsStartRun = false;
            manaulData.StartRunBeginTime = DateTime.Now;

            // 停止超时计时器
            try { _startupTimeoutTimer.Stop(); } catch { }

            // 释放 Capture
            try { this.btnManualStart.Capture = false; } catch { }
            try { this.btnManualShake.Capture = false; } catch { }

            // 解除联动锁定（恢复励磁/转速调节按钮）
            try { SetStartupAdjustLock(true); } catch { }
        }

        /// <summary>
        /// 手动测试的实时状态存储
        /// </summary>
        public class ManaulData
        {
            public double BeginInvertSpeed { get; set; }
            public double BeginCurrent { get; set; }
            public bool IsStartRun { get; set; }
            public DateTime StartRunBeginTime { get; set; }
        }


        #region ====== 联动锁定（通知 ucFormMainControl）======

        /// <summary>
        /// 启机/甩车长按期间，锁定 ucFormMainControl 内的励磁/转速调节按钮。
        /// enabled=false → 禁用；enabled=true → 恢复。
        /// </summary>
        private void SetStartupAdjustLock(bool enabled)
        {
            try
            {
                var mainCtrl = FindControlOfType<ucFormMainControl>(this.FindForm());
                mainCtrl?.SetAdjustButtonsEnabled(enabled);
            }
            catch (Exception ex)
            {
                try { Var.LogInfo("SetStartupAdjustLock 异常: " + ex.Message); } catch { }
            }
        }

        /// <summary>
        /// 递归查找指定类型控件
        /// </summary>
        private T FindControlOfType<T>(Control parent) where T : Control
        {
            if (parent == null) return null;
            foreach (Control ctrl in parent.Controls)
            {
                T match = ctrl as T;
                if (match != null) return match;
                T child = FindControlOfType<T>(ctrl);
                if (child != null) return child;
            }
            return null;
        }

        #endregion

    }
}

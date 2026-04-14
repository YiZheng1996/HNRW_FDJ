using MainUI.Global;
using MainUI.Modules;
using MainUI.TestScreen;
using RW.UI.Controls;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MainUI.Modules.EventArgsModel;

namespace MainUI.Widget
{
    /// <summary>
    /// 启机 甩车 页面
    /// </summary>
    public partial class ucStartup : UserControl
    {
        // 启机弹窗描述
        frmMessageWarning frmStartupMessage = new frmMessageWarning();

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
            this.timerFast.Enabled = true;
            this.timerFast.Start();
            this.timerSlow.Enabled = true;
            this.timerSlow.Start();

            this.nudBeginCurrent.Value = Common.AOgrp["励磁调节"];
            manaulData.BeginInvertSpeed = nudBeginInvertSpeed.Value;
            manaulData.BeginCurrent = nudBeginCurrent.Value;

            Common.gd350_1.KeyValueChange += Gd350_1_KeyValueChange;
            Common.DIgrp.KeyValueChange += DIgrp_KeyValueChange;
            Common.DOgrp.KeyValueChange += DOgrp_KeyValueChange;
        }

        /// <summary>
        /// DO 检测
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

            //if (e.Key == "水阻上升控制")
            //{
            //    this.btnWaterUP.Switch = e.Value;
            //}
            //else if (e.Key == "水阻下降控制")
            //{
            //    this.btnWaterDown.Switch = e.Value;
            //}
        }

        bool FirstReturn = false;
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
                    btnManualRunDown(type);
                }
                else
                {
                    btnManualRun_MouseUp(null, null);
                }
            }
        }

        /// <summary>
        /// 变频器的值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Gd350_1_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (e.Key == "就绪")
            {
                // 就绪
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerSlow_Tick(object sender, EventArgs e)
        {
            // 转速小于10 && 盘车连锁开关闭合信号 && 闭合电喷控制器启机回路 && 水极板在上限位
            var speed = Common.speedGrp["转速1"] < 100;
            var status = Common.DIgrp["盘车连锁开关"];
            var status2 = Common.DOgrp["发动机启停预启动"] == true;
            var status3 = Common.DIgrp["水阻升降上极限检测"] == true;
            var status4 = Common.gd350_1.Ready == 1; // 发电机就绪
            var status5 = MiddleData.instnce.IsOpenFuelCycle() == true; // Common.ExChangeGrp.GetBool("燃油循环") == true;
            var lcStatus = Common.AOgrp["励磁调节"] > 0;// && Common.AIgrp2["励磁电流检测"] > 0;
            var speedStatus = manaulData.BeginInvertSpeed > 0;

            //var status6 = Common.ExChangeGrp.GetBool("高温水预热循环") == true;
            //var status7 = Common.ExChangeGrp.GetBool("高温水膨胀水箱加水") == true;
            var status8 = MiddleData.instnce.IsOpenEngineCycle() == true; //Common.ExChangeGrp.GetBool("预供机油循环") == true;
            var status9 = Common.DOgrp["发动机DC24V供电"] == true;
            this.uiLightStart.State = (speed && status && status2 && status3 && status4 && status5 && status8 && status9 && lcStatus && speedStatus) ? UILightState.On : UILightState.Off;
            this.uiLightShake.State = (speed && status && status2 && status3 && status4 && !status5 && status8 && !status9 && lcStatus && speedStatus) ? UILightState.On : UILightState.Off;

            //暂定 刷新状态灯
            this.uiLightWaterUP.State = status3 ? UILightState.On : UILightState.Off;
            //this.uiLightUP.State = Common.DIgrp["水阻升降上极限检测"] == true ? UILightState.On : UILightState.Off;
            //this.uiLightDown.State = Common.DIgrp["水阻升降下极限检测"] == true ? UILightState.On : UILightState.Off;

            // 燃油循环
            this.uiLightFuelCycle.State = status5 ? UILightState.On : UILightState.Off;
            this.uiLightFuelCycleClose.State = !status5 ? UILightState.On : UILightState.Off;
            this.btnFuelCycleOpen.Switch = status5;

            this.uiLightEO.State = status8 ? UILightState.On : UILightState.Off;
            this.btnEoOpen.Switch = status8;

            this.uiLightPC.State = status ? UILightState.On : UILightState.Off;
            this.LightInvertReady.State = status4 ? UILightState.On : UILightState.Off;

            // DC24V
            this.uiLightDV24Open.State = status9 ? UILightState.On : UILightState.Off;
            this.uiLightDV24Close.State = !status9 ? UILightState.On : UILightState.Off;

            // 发电机运行中
            this.LightInvertRunning.State = (Common.gd350_1.RunStatusAI == 1 || Common.gd350_1.RunStatusAI == 2) ? UILightState.On : UILightState.Off;
            this.lblInverterVoltage.Text = Common.gd350_1.OutputVoltage.ToString();
            this.lblInverterCurrent.Text = Common.gd350_1.OutputCurrent.ToString();
            this.lblInverterPower.Text = Common.gd350_1.OutputPower.ToString();

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

        }


        /// <summary>
        /// 仪表盘较快数据的刷新（快速）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerFast_Tick(object sender, EventArgs e)
        {
            this.LCVoltageValue.Text = Common.excitationGrp["励磁电压检测"].ToString();
            this.LCCurrentValue.Text = Common.excitationGrp["励磁电流检测"].ToString();

            // 转速
            this.lblSpeed.Text = MiddleData.instnce.EngineSpeed.ToString();
            this.lblPower.Text = MiddleData.instnce.EnginePower.ToString();
            this.lblTorque.Text = MiddleData.instnce.EngineTorque.ToString();
        }

        /// <summary>
        /// 关闭开启按钮通用点击事件
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
                    if (sw.OutputTagName == "发动机DC24V供电")
                    {
                        // 关闭电喷供电
                        Common.DOgrp["发动机DC24V供电"] = Convert.ToBoolean(sw.Tag.ToInt());
                    }

                    if (sw.OutputTagName == "燃油循环" || sw.OutputTagName == "预供机油循环")
                    {
                        // 管路相关操作
                        Common.ExChangeGrp.SetBool(sw.OutputTagName, Convert.ToBoolean(sw.Tag.ToInt()));
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception($"{th}+{str}失败！原因：" + ex.Message);
                }
            }
        }


        /// <summary>
        /// 设置转速更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        private void nudBeginInvertSpeed_ValueChanged(object sender, double value)
        {
            manaulData.BeginInvertSpeed = value;
        }

        /// <summary>
        /// 励磁电流值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        private void nudBeginCurrent_ValueChanged(object sender, double value)
        {
            manaulData.BeginCurrent = value;
        }

        /// <summary>
        /// 启机转速设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetBeginSpeed_Click(object sender, EventArgs e)
        {
            this.btnSetBeginSpeed.Focus();

            var value = Math.Round((manaulData.BeginInvertSpeed * 7) / 60, 2);
            Common.gd350_1.SetFrequency = value;
        }

        /// <summary>
        /// 启机励磁设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetBeginLC_Click(object sender, EventArgs e)
        {
            this.btnSetBeginLC.Focus();
            Common.AOgrp["励磁调节"] = manaulData.BeginCurrent;
        }

        /// <summary>
        /// 启机按下(流程)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnManualRun_MouseDown(object sender, MouseEventArgs e)
        {
            var button = sender as RButton;
            var tag = button.Tag.ToString();
            btnManualRunDown(tag);
        }

        /// <summary>
        /// 按钮按下（包括硬件按钮）
        /// </summary>
        /// <param name="key"></param>
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

                // 预供机油循环
                if (MiddleData.instnce.IsOpenEngineCycle() == false)
                {
                    errorMessages.AppendLine($"{++msgIndex}. 请先开始预供机油循环。");
                }

                if (Common.AOgrp["励磁调节"] == 0) //|| Common.AIgrp2["励磁电流检测"] == 0
                {
                    errorMessages.AppendLine($"{++msgIndex}. 励磁电流不能为 0 A。");
                }

                if (manaulData.BeginInvertSpeed == 0)
                {
                    errorMessages.AppendLine($"{++msgIndex}. 启机转速不能为 0 rpm。");
                }

                if (tag == "启机")
                {
                    // 燃油循环
                    if (MiddleData.instnce.IsOpenFuelCycle() == false)
                    {
                        errorMessages.AppendLine($"{++msgIndex}. 请先开始燃油循环。");
                    }
                    // DC24V
                    if (uiLightDV24Open.State == UILightState.Off)
                    {
                        errorMessages.AppendLine($"{++msgIndex}. 请先打开发动机控制盒电源 DC24V。");
                    }
                }
                else
                {
                    // 燃油循环
                    if (MiddleData.instnce.IsOpenFuelCycle() == true)
                    {
                        errorMessages.AppendLine($"{++msgIndex}. 请先关闭燃油循环。");
                    }
                    // DV24V
                    if (uiLightDV24Open.State == UILightState.On)
                    {
                        errorMessages.AppendLine($"{++msgIndex}. 请先关闭发动机控制盒电源 DC24V。");
                    }
                }


                //// 高温水预热循环
                //if (Common.ExChangeGrp.GetBool("高温水预热循环") == false)
                //{
                //    errorMessages.AppendLine($"{++msgIndex}. 请先开始高温水预热循环。");
                //}

                //// 高温水膨胀水箱加水
                //if (Common.ExChangeGrp.GetBool("高温水膨胀水箱加水") == false)
                //{
                //    errorMessages.AppendLine($"{++msgIndex}. 请先开始高温水膨胀水箱加水。");
                //}



                //// 检查启机间隔时间
                //TimeSpan timeDiff = DateTime.Now - manaulData.StartRunBeginTime;
                //if (timeDiff.TotalSeconds < 5)
                //{
                //    errorMessages.AppendLine($"{++msgIndex}. 请不要频繁启机，请等待{5 - (int)timeDiff.TotalSeconds}秒后再试。");
                //}

                // 如果有错误信息，统一显示
                if (errorMessages.Length > 0)
                {
                    string message = $"不满足{tag}条件：\n\n" + errorMessages;

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
                    //Var.MsgBoxWarn(this, "不满足启机条件：\n\n" + errorMessages.ToString());
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
                Common.AOgrp["励磁调节"] = manaulData.BeginCurrent;

                var value = Math.Round((manaulData.BeginInvertSpeed * 7) / 60, 2);
                Common.gd350_1.SetFrequency = value;
                Common.gd350_1.SetRun = true;

                MiddleData.instnce.isStartupRecording = true;
                MiddleData.instnce.StartupReleaseTime = null;
                EventTriggerModel.StartupChanged(true);
                //Thread t = new Thread(xxx =>
                //{
                //    try
                //    {
                //        manaulData.IsStartRun = true;
                //        manaulData.StartRunBeginTime = DateTime.Now;

                //        Common.AOgrp["励磁调节"] = manaulData.BeginCurrent;
                //        Thread.Sleep(50);
                //        Common.gd350_1.SetFrequency = Math.Round((manaulData.BeginInvertSpeed * 7) / 60, 1);
                //        Thread.Sleep(50);
                //        Common.gd350_1.SetRun = true;
                //    }
                //    catch (Exception ex)
                //    {
                //        System.Diagnostics.Debug.WriteLine($"故障检测异常: {ex.Message}");
                //    }
                //    Thread.Sleep(300);

                //});
                //t.IsBackground = true;
                //t.Name = "实时检测故障列表线程";
                //t.Start();
            }
            catch (Exception ex)
            {
                MiddleData.instnce.isStartupRecording = false;
                MiddleData.instnce.StartupReleaseTime = DateTime.Now;

                manaulData.IsStartRun = false;
                Common.gd350_1.SetRun = false;

                Var.MsgBoxWarn(this, $"启机出现异常:{ex.Message.ToString()}");
                Var.LogInfo($"点动-启机出现异常:{ex.ToString()}");
            }
        }

        /// <summary>
        /// 启机松开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnManualRun_MouseUp(object sender, MouseEventArgs e)
        {
            // 松开励磁写0
            Common.AOgrp["励磁调节"] = 0;
            this.nudBeginCurrent.Value = 0;

            MiddleData.instnce.isStartupRecording = false;
            MiddleData.instnce.StartupReleaseTime = DateTime.Now;

            this.btnManualStart.Switch = false;
            this.btnManualShake.Switch = false;

            manaulData.IsStartRun = false;
            manaulData.StartRunBeginTime = DateTime.Now;
            Common.gd350_1.SetRun = false;
        }

        /// <summary>
        /// 手动测试的实时状态存储
        /// </summary>
        public class ManaulData
        {
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
}

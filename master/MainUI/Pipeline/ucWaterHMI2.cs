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
using System.Threading.Tasks;
using System.Windows.Forms;
using static MainUI.Modules.EventArgsModel;

namespace MainUI
{
    public partial class ucWaterHMI2 : UserControl
    {
        public ucWaterHMI2()
        {
            InitializeComponent();
        }

        // 数字量的集合
        Dictionary<string, SwitchPictureBox> dicValve = new Dictionary<string, SwitchPictureBox>();
        // 模拟量的集合
        Dictionary<string, ucPipePara> DoubleDicValve = new Dictionary<string, ucPipePara>();

        public void Init()
        {
            LoadAllValve();

            Common.DOgrp.KeyValueChange += DOgrp_KeyValueChange;
            Common.DIgrp.KeyValueChange += DIgrp_KeyValueChange;
            Common.AOgrp.KeyValueChange += AOgrp_KeyValueChange;
            Common.AIgrp.KeyValueChange += AIgrp_KeyValueChange;
            Common.waterGrp.KeyValueChange += WaterGrp_KeyValueChange;
            Common.AI2Grp.KeyValueChange += EngineParaGrp_KeyValueChange;

            timerTube.Enabled = true;
            timerTube.Start();
            timerSwitch.Enabled = true;
            timerSwitch.Start();
        }

        private void DIgrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (dicValve.ContainsKey(e.Key))
            {
                dicValve[e.Key].Switch = e.Value;
            }
        }

        /// <summary>
        /// 新增压力/温度（橙）数据
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
        /// 计油数据更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WaterGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (DoubleDicValve.ContainsKey(e.Key))
            {
                DoubleDicValve[e.Key].Value = e.Value;
            }
        }

        /// <summary>
        /// AI数据更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AIgrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (DoubleDicValve.ContainsKey(e.Key))
            {
                DoubleDicValve[e.Key].Value = e.Value;
            }
        }

        /// <summary>
        /// AO数据更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AOgrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (DoubleDicValve.ContainsKey(e.Key))
            {
                DoubleDicValve[e.Key].Value = e.Value;
            }
        }

        /// <summary>
        /// 泵/阀状态变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DOgrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (dicValve.ContainsKey(e.Key))
            {
                if (e.Key == "Y03阀控制" || e.Key == "Y41阀控制")
                {
                    dicValve[e.Key].Switch = !e.Value;
                }
                else
                {
                    dicValve[e.Key].Switch = e.Value;
                }
            }
        }

        /// <summary>
        /// 把所有点位添加到字典
        /// </summary>
        private void LoadAllValve()
        {
            try
            {
                foreach (var item in this.Controls)
                {
                    // 如果为阀/泵
                    if (item is SwitchPictureBox)
                    {
                        SwitchPictureBox sw = item as SwitchPictureBox;
                        if (sw.Tag != null && string.IsNullOrEmpty(sw.Tag.ToString()) == false)
                        {
                            if (!dicValve.ContainsKey(sw.Tag.ToString()))
                            {
                                dicValve.Add(sw.Tag.ToString(), sw);
                            }
                        }
                    }

                    // 如果为模拟量
                    if (item is ucPipePara)
                    {
                        ucPipePara upp = item as ucPipePara;
                        if (upp.Tag != null && string.IsNullOrEmpty(upp.Tag.ToString()) == false)
                        {
                            if (!DoubleDicValve.ContainsKey(upp.Tag.ToString()))
                            {
                                DoubleDicValve.Add(upp.Tag.ToString(), upp);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        /// <summary>
        /// 通用点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sw_Valve_Click(object sender, EventArgs e)
        {
            SwitchPictureBox sw = sender as SwitchPictureBox;
            if (sw.Tag == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(sw.Tag.ToString()))
            {
                return;
            }
            if ((Common.AIgrp["厂房进气压力检测1"] < 200 || Common.AIgrp["厂房进气压力检测2"] < 200) && !Var.SysConfig.RepairModel)
            {
                Var.MsgBoxWarn(this, $"厂房气压小于200kPa，禁止操作。");
                return;
            }

            if (Common.ExChangeGrp.GetBool("预热水箱加水"))
            {
                HashSet<string> ForbiddenValves = new HashSet<string> { "Y28阀控制", "Y26阀控制" };
                if (ForbiddenValves.Contains(sw.Tag?.ToString() ?? ""))
                {
                    Var.MsgBoxWarn(this, "正在进行【一键控制】预热水箱加水，不能操作相关的阀。");
                    return;
                }
            }
            if (Common.ExChangeGrp.GetBool("预热水箱加热"))
            {
                HashSet<string> ForbiddenValves = new HashSet<string> { "Y28阀控制", "Y26阀控制" };
                if (ForbiddenValves.Contains(sw.Tag?.ToString() ?? ""))
                {
                    Var.MsgBoxWarn(this, "正在进行【一键控制】预热水箱加水，不能操作相关的阀。");
                    return;
                }
            }
            if (Common.ExChangeGrp.GetBool("高温水预热循环"))
            {
                HashSet<string> ForbiddenValves = new HashSet<string> { "Y3阀控制", "Y15阀控制", "Y16阀控制", "Y17阀控制", "Y20阀控制", "Y21阀控制", "Y22阀控制" };
                if (ForbiddenValves.Contains(sw.Tag?.ToString() ?? ""))
                {
                    Var.MsgBoxWarn(this, "正在进行【一键控制】高温水预热循环，不能操作相关的阀。");
                    return;
                }
            }
            if (Common.ExChangeGrp.GetBool("高温水中冷水回抽"))
            {
                HashSet<string> ForbiddenValves = new HashSet<string> { "Y16阀控制", "Y17阀控制", "Y21阀控制", "Y23阀控制", "Y24阀控制", "Y31阀控制" };
                if (ForbiddenValves.Contains(sw.Tag?.ToString() ?? ""))
                {
                    Var.MsgBoxWarn(this, "正在进行【一键控制】高温水中冷水回抽，不能操作相关的阀。");
                    return;
                }
            }

            // 保护 
            //if (sw.Tag.ToString() == "预热水泵合闸控制")
            //{
            //    if (Common.ExChangeGrp.GetBool("高温水预热循环"))
            //    {
            //        // 不能开始
            //        Var.MsgBoxWarn(this, "正在进行高温水预热循环，不能进行操作");
            //        return;
            //    }
            //}
            //else if (sw.Tag.ToString() == "预热水箱加热检测")
            //{
            //    if (Common.ExChangeGrp.GetBool("预热水箱加热"))
            //    {
            //        // 不能开始
            //        Var.MsgBoxWarn(this, "正在进行预热水箱加热，不能进行操作");
            //        return;
            //    }
            //}

            string str = sw.Switch == false ? "开启" : "关闭";
            string th = sw.Tag.ToString().Replace("控制", "");
            string strMessage = $"是否要{str}{th}?";

            // 特殊处理：对于泵来说，关闭操作不弹窗
            HashSet<string> noConfirmCloseSwitches = new HashSet<string>
            {
                "预热水泵合闸控制",
            };
            if (sw.Switch == true && noConfirmCloseSwitches.Contains(sw.Tag.ToString()))
            {
                try
                {
                    Common.DOgrp[sw.Tag.ToString()] = !Common.DOgrp[sw.Tag.ToString()];
                }
                catch (Exception ex)
                {
                    throw new Exception($"{th}关闭失败！原因：" + ex.Message);
                }
                return;
            }

            bool result = Var.MsgBoxYesNo(this, strMessage);
            if (result == false)
            {
                return;
            }
            else
            {
                try
                {
                    Common.DOgrp[sw.Tag.ToString()] = !Common.DOgrp[sw.Tag.ToString()];
                }
                catch (Exception ex)
                {

                    throw new Exception($"{th}+{str}失败！原因：" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 流量调节
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLL194_Click(object sender, EventArgs e)
        {
            var pipePara = sender as UIButton;
            RW.Components.Widgets.frmSetValue set = new RW.Components.Widgets.frmSetValue();
            set.DecimalNumber = 1;
            set.MaxValue = 100;
            set.Unit = UnitEnum.percent;
            set.Text = "流量设置";
            set.Value = Common.AOgrp[pipePara.Tag.ToString()];
            var dr = set.ShowDialog(this);
            if (dr == DialogResult.OK)
                Common.AOgrp[pipePara.Tag.ToString()] = set.Value;
        }

        /// <summary>
        /// 预热水箱加热目标温度设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYRTemp_Click(object sender, EventArgs e)
        {
            var pipePara = sender as UIButton;
            RW.Components.Widgets.frmSetValue set = new RW.Components.Widgets.frmSetValue();
            set.DecimalNumber = 1;
            set.MaxValue = 150;
            set.Unit = UnitEnum.celsius;
            set.Text = "温度设置";
            set.Value = Common.ExChangeGrp.GetDouble(pipePara.Tag.ToString());
            var dr = set.ShowDialog(this);
            if (dr == DialogResult.OK)
                Common.ExChangeGrp.SetDouble(pipePara.Tag.ToString(), set.Value);
        }

        /// <summary>
        /// 调节阀-87（高温水设置）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButton1_Click(object sender, EventArgs e)
        {
            var pipePara = sender as UIButton;
            RW.Components.Widgets.frmSetValue set = new RW.Components.Widgets.frmSetValue();
            set.DecimalNumber = 1;
            set.MaxValue = 150;
            set.Unit = UnitEnum.celsius;
            set.Text = "温度设置";
            set.Value = Common.waterGrp[pipePara.Tag.ToString()];
            var dr = set.ShowDialog(this);
            if (dr == DialogResult.OK)
                Common.waterGrp.HeightTempWaterPID = set.Value;
        }

        /// <summary>
        /// 调节阀-88（中冷水设置）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButton2_Click(object sender, EventArgs e)
        {
            var pipePara = sender as UIButton;
            RW.Components.Widgets.frmSetValue set = new RW.Components.Widgets.frmSetValue();
            set.DecimalNumber = 1;
            set.MaxValue = 150;
            set.Unit = UnitEnum.celsius;
            set.Text = "温度设置";
            set.Value = Common.waterGrp[pipePara.Tag.ToString()];
            var dr = set.ShowDialog(this);
            if (dr == DialogResult.OK)
                Common.waterGrp.MediumColdPID = set.Value;
        }

        private void timerTube_Tick(object sender, EventArgs e)
        {
            foreach (var pipe in this.GetControls<UIPipe>())
            {
                pipe.Invalidate();

            }

            // HT水换热器 - 出机
            this.uiPHTRH1.Active = this.uiPHTRH2.Active = this.ucPipePara8.Value > 0;
            this.uiPHTlh1.Active = this.uiPHTlh2.Active = this.uiPHTlh3.Active = this.ucPipePara9.Value > 0;
            this.uiPHTOut1.Active = this.uiPHTOut2.Active = this.ucPipePara7.Value > 0;
            this.uiP03right.Active = (this.swp3.Switch && this.uiPHTlh3.Active);
            this.uiPHTIn.Active = this.uiPHT1.Active = this.uiPHT2.Active = this.ucPipePara11.Value > 0;

            // LT水换热器
            this.uiPLTlq1.Active = this.ucPipePara18.Value > 0;
            this.uiP31left.Active = this.uiP31left2.Active = this.uiP31left3.Active = this.uiPLTlh1.Active = this.uiPLTlh2.Active = this.uiPLTlh3.Active = this.ucPipePara17.Value > 0;

            // 交汇处
            this.uiP21down.Active = (this.swp21.Switch && this.uiPHTlh2.Active);
            this.uiP31right.Active = (this.swp31.Switch && this.uiP31left.Active);
            this.uiP23left.Active = this.uiP22up.Active = (this.uiP21down.Active || this.uiP31right.Active);
            this.uiP23right.Active = this.swp23.Switch;
            this.uiP22down1.Active = this.uiP22down2.Active = (this.swp22.Switch && this.uiP22up.Active);

            // 预热水箱
            this.uiP15left1.Active = this.uiP15left2.Active = this.uiP15right.Active = (this.swp15.Switch && this.ucPipePara13.Value > 0);
            this.uiP16left.Active = (this.uiP15left2.Active || this.uiP23right.Active);
            this.uiP24left.Active = (this.swp24.Switch && this.uiP24right.Active);
            this.uiP26left.Active = (this.swp26.Switch && this.uiP26right.Active);

            //预热水泵
            this.uiP16right.Active = this.uiP17left.Active = (this.swpYRSB.Switch && this.swp16.Switch && this.uiP16left.Active);
            this.uiP17right.Active = this.uiP20down.Active = this.uiP24right.Active = this.uiP24right2.Active = this.uiP27up2.Active = this.uiP27up.Active = this.ucPipePara2.Value > 0;
            this.uiP20up.Active = (this.swp20.Switch && this.uiP20down.Active);
            this.uiP20up2.Active = (this.uiP03right.Active || this.uiP20up.Active);

            this.uiP26right.Active = this.uiP27down.Active = this.uiP2728.Active = ((this.swp27.Switch && this.uiP27up.Active) || this.swp28.Switch);

            this.uiP28down1.Active = this.uiP28down2.Active = this.uiP28down3.Active = this.uiP28down4.Active = this.swp28.Switch;

            // 膨胀水箱
            this.uiP29right.Active = this.uiP29left.Active = this.swp29.Switch;
            this.uiP30left.Active = this.uiP30left2.Active = this.uiP30right.Active = this.swp30.Switch;

            // LT 水
            this.uiPLTOut.Active = this.ucPipePara21.Value > 0;
            this.uiPPZ1.Active = this.uiPPZ2.Active = this.uiPLTIn.Active = this.ucPipePara15.Value > 0;
            this.uiP41right.Active = (this.swp41.Switch && this.uiPLTlh3.Active);
        }

        private void switchPictureBox29_SwitchChanged(object sender, bool value)
        {
            uiP17left.Active = swpYRSB.Switch;
        }

        private void switchPictureBox10_SwitchChanged(object sender, bool value)
        {
            uiP17right.Active = swp17.Switch;
        }

        /// <summary>
        /// 更新泵的显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerSwitch_Tick(object sender, EventArgs e)
        {
            // 更新一键控制
            this.rButton42.Switch = Common.ExChangeGrp.GetBool("预热水箱加水");
            this.swYR.Switch = Common.ExChangeGrp.GetBool("预热水箱加热");
            this.rButton78.Switch = Common.ExChangeGrp.GetBool("高温水预热循环");
            this.rButton76.Switch = Common.ExChangeGrp.GetBool("高温水中冷水回抽");

            //var switchT = Common.DIgrp["预热水箱加热器控制1检测"]
            //           || Common.DIgrp["预热水箱加热器控制2检测"]
            //           || Common.DIgrp["预热水箱加热器控制3检测"]
            //           || Common.DIgrp["预热水箱加热器控制4检测"]
            //           || Common.DIgrp["预热水箱加热器控制5检测"]
            //           || Common.DIgrp["预热水箱加热器控制6检测"];

            //const string key = "预热水箱加热检测";
            //// 安全取值 + 线程安全刷新
            //if (dicValve.TryGetValue(key, out var spb))
            //{
            //    // 如果 Switch 是 bool 类型
            //    spb.Switch = switchT;
            //}

            // 目标温度
            this.ucPipeParaYLTemp.Value = Common.ExChangeGrp.GetDouble("预热水箱加热温度设定");

            this.LightHTLow.State = Common.DIgrp["高温水膨胀水箱低液位"] ? UILightState.On : UILightState.Off;
            this.LightHTHight.State = Common.DIgrp["高温水膨胀水箱高液位"] ? UILightState.Off : UILightState.On;

            this.LightLTLow.State = Common.DIgrp["中冷水膨胀水箱低液位"] ? UILightState.On : UILightState.Off;
            this.LightLTHight.State = Common.DIgrp["中冷水膨胀水箱高液位"] ? UILightState.Off : UILightState.On;
        }

        /// <summary>
        /// 预热水箱加热控制  调整为一键控制逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void swYR_Click(object sender, EventArgs e)
        {
            SwitchPictureBox sw = sender as SwitchPictureBox;
            if (sw.Tag == null)
            {
                return;
            }
            if (sw.OutputTagName == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(sw.Tag.ToString()))
            {
                return;
            }
            if (string.IsNullOrEmpty(sw.OutputTagName.ToString()))
            {
                return;
            }

            string msg = "开始";
            var switchT = swYR.Switch;
            //var switchT = Common.DIgrp["预热水箱加热器控制1检测"]
            //           || Common.DIgrp["预热水箱加热器控制2检测"]
            //           || Common.DIgrp["预热水箱加热器控制3检测"]
            //           || Common.DIgrp["预热水箱加热器控制4检测"]
            //           || Common.DIgrp["预热水箱加热器控制5检测"]
            //           || Common.DIgrp["预热水箱加热器控制6检测"];

            if (switchT)
            {
                msg = "停止";
            }

            if (Convert.ToDouble(this.ucPipePara13.Value) <= 600)
            {
                Var.MsgBoxWarn(this, "预热水箱加热开启条件：预热水箱液位>600mm。");
                return;
            }

            var dr = Var.MsgBoxYesNo(this, $"确定要{msg}预热水箱加热吗？");
            if (!dr) return;

            Common.ExChangeGrp.SetBool("预热水箱加热", !swYR.Switch);

            // 改成1.0
            //Common.DOgrp[sw.OutputTagName.ToString()] = !switchT;
        }

        /// <summary>
        /// 通用的开始试验逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoStart_Click(object sender, EventArgs e)
        {
            try
            {
                RButton btn = sender as RButton;
                if (btn.Tag == null)
                {
                    Var.MsgBoxWarn(this, "程序异常，未绑定Tag值。");
                    return;
                }
                if (string.IsNullOrEmpty(btn.Tag.ToString()))
                {
                    Var.MsgBoxWarn(this, "程序异常，未绑定Tag值。");
                    return;
                }
                if (string.IsNullOrEmpty(btn.OutputTagName.ToString()))
                {
                    Var.MsgBoxWarn(this, "程序异常，未绑定OutputTagName值。");
                    return;
                }
                var Pressure1 = Common.AIgrp["厂房进气压力检测1"];
                var Pressure2 = Common.AIgrp["厂房进气压力检测2"];
                if (Pressure1 < 200 && Pressure2 < 200)
                {
                    Var.MsgBoxWarn(this, "厂房压力低，不能进行一键操作。");
                    return;
                }

                // 如果已经打开/关闭 则过滤
                if (btn.Switch == true)
                {
                    return;
                }

                var setResult = btn.Tag.ToString() == "1";

                // 水系统判定
                if (setResult && btn.OutputTagName == "预热水箱加水")
                {
                    if (Convert.ToDouble(this.ucPipePara13.Value) >= 1500)
                    {
                        Var.MsgBoxWarn(this, "预热水箱加水开启条件：预热水箱液位<1500mm。");
                        return;
                    }
                }
                else if (setResult && btn.OutputTagName == "高温水预热循环")
                {
                    StringBuilder errorMessages = new StringBuilder();
                    int msgIndex = 0;

                    if (Convert.ToDouble(this.ucPipePara1.Value) <= 60)
                    {
                        errorMessages.AppendLine($"不满足开启条件{++msgIndex}：预热水箱温度>60℃。");
                    }
                    if (Convert.ToDouble(this.ucPipePara13.Value) <= 600)
                    {
                        errorMessages.AppendLine($"不满足开启条件{++msgIndex}.预热水箱液位>600mm。");
                    }
                    if (msgIndex > 0)
                    {
                        Var.MsgBoxWarn(this, errorMessages.ToString());
                        return;
                    }
                }
                else if (setResult && btn.OutputTagName == "高温水中冷水回抽")
                {
                    if (Convert.ToDouble(this.ucPipePara13.Value) >= 1500)
                    {
                        Var.MsgBoxWarn(this, "高温水/中冷水回抽开启条件：预热水箱液位<1500mm。");
                        return;
                    }
                }


                string th = btn.OutputTagName.ToString().Replace("控制", "").Replace("合闸", "");
                string strMessage = $"是否要{ btn.Text }{th}?";
                bool mesResult = Var.MsgBoxYesNo(this, strMessage);
                if (mesResult == false)
                {
                    return;
                }
                else
                {
                    try
                    {
                        Common.ExChangeGrp.SetBool(btn.OutputTagName, setResult);
                    }
                    catch (Exception ex)
                    {
                        Var.MsgBoxWarn(this, $"{btn.Text}+{th}失败！原因：" + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "水系统一键控制异常" + ex.Message);
            }
        }
    }
}

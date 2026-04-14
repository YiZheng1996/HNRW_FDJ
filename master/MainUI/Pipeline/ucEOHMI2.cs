using MainUI.Equip;
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
using System.Threading.Tasks;
using System.Windows.Forms;
using static MainUI.Modules.EventArgsModel;

namespace MainUI
{
    public partial class ucEOHMI2 : UserControl
    {
        public ucEOHMI2()
        {
            InitializeComponent();
        }


        // 数字量的集合
        Dictionary<string, SwitchPictureBox> dicValve = new Dictionary<string, SwitchPictureBox>();
        // 模拟量的集合
        Dictionary<string, ucPipePara> DoubleDicValve = new Dictionary<string, ucPipePara>();

        /// <summary>
        /// 加载
        /// </summary>
        public void Init()
        {
            this.timerTube.Enabled = true;
            this.timerTube.Start();
            this.timerEngineOilConsumption.Enabled = true;
            this.timerEngineOilConsumption.Start();

            LoadAllValve(this);

            Common.DOgrp.KeyValueChange += DOgrp_KeyValueChange;
            Common.AOgrp.KeyValueChange += AOgrp_KeyValueChange;
            Common.AIgrp.KeyValueChange += AIgrp_KeyValueChange;
            Common.DIgrp.KeyValueChange += DIgrp_KeyValueChange;
            Common.engineOilGrp.KeyValueChange += EngineOilGrp_KeyValueChange;
            Common.AI2Grp.KeyValueChange += EngineParaGrp_KeyValueChange;
        }

        private void DIgrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (dicValve.ContainsKey(e.Key))
            {
                dicValve[e.Key].Switch = e.Value;
            }
        }

        /// <summary>
        /// PLC2AI点位中的压力/温度数据
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

        private void EngineOilGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
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

        ///// <summary>
        ///// 把所有点位添加到字典
        ///// </summary>
        //private void LoadAllValve()
        //{
        //    try
        //    {
        //        foreach (var item in this.Controls)
        //        {
        //            // 如果为阀/泵
        //            if (item is SwitchPictureBox)
        //            {
        //                SwitchPictureBox sw = item as SwitchPictureBox;
        //                if (sw.Tag != null && string.IsNullOrEmpty(sw.Tag.ToString()) == false)
        //                {
        //                    dicValve.Add(sw.Tag.ToString(), sw);
        //                }

        //            }

        //            // 如果为模拟量
        //            if (item is ucPipePara)
        //            {
        //                ucPipePara upp = item as ucPipePara;
        //                if (upp.Tag != null && string.IsNullOrEmpty(upp.Tag.ToString()) == false)
        //                {
        //                    DoubleDicValve.Add(upp.Tag.ToString(), upp);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}

        /// <summary>
        /// 添加控件到字典
        /// </summary>
        /// <param name="con"></param>
        private void LoadAllValve(Control con)
        {
            foreach (Control item in con.Controls)
            {
                LoadAllValve(item);
            }

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
                Var.MsgBoxWarn(this, $"机油系统增加点位异常:{ex.ToString()}");
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
            if ((Common.AIgrp["厂房进气压力检测1"] < 400 || Common.AIgrp["厂房进气压力检测2"] < 400) && !Var.SysConfig.RepairModel)
            {
                Var.MsgBoxWarn(this, $"厂房气压小于400kPa，禁止操作。");
                return;
            }

            // 保护 
            if (Common.ExChangeGrp.GetBool("机油箱加油"))
            {
                if (sw.Tag.ToString() == "Y137阀控制")
                {
                    Var.MsgBoxWarn(this, "正在进行【一键控制】机油箱加油，不能操作相关的阀。");
                    return;
                }
            }
            if (Common.ExChangeGrp.GetBool("预供机油循环"))
            {
                HashSet<string> ForbiddenValves = new HashSet<string> { "Y92阀控制", "Y95阀控制", "Y96阀控制", "Y97阀控制", "Y100阀控制" };
                if (ForbiddenValves.Contains(sw.Tag?.ToString() ?? ""))
                {
                    Var.MsgBoxWarn(this, "正在进行【一键控制】预供机油循环，不能操作相关的阀。");
                    return;
                }
            }
            if (Common.ExChangeGrp.GetBool("机油加热处理循环"))
            {
                HashSet<string> ForbiddenValves = new HashSet<string> { "Y133阀控制", "Y135阀控制", "Y136阀控制", "Y137阀控制" };
                if (ForbiddenValves.Contains(sw.Tag?.ToString() ?? ""))
                {
                    Var.MsgBoxWarn(this, "正在进行【一键控制】机油加热循环，不能操作相关的阀。");
                    return;
                }
            }
            if (Common.ExChangeGrp.GetBool("油底壳加油"))
            {
                HashSet<string> ForbiddenValves = new HashSet<string> { "Y111阀控制", "Y96阀控制", "Y97阀控制", "Y100阀控制" };
                if (ForbiddenValves.Contains(sw.Tag?.ToString() ?? ""))
                {
                    Var.MsgBoxWarn(this, "正在进行【一键控制】油底壳加油，不能操作相关的阀。");
                    return;
                }
            }
            if (Common.ExChangeGrp.GetBool("油底壳抽油"))
            {
                HashSet<string> ForbiddenValves = new HashSet<string> { "Y93阀控制", "Y95阀控制", "Y96阀控制", "Y97阀控制", "Y115阀控制" };
                if (ForbiddenValves.Contains(sw.Tag?.ToString() ?? ""))
                {
                    Var.MsgBoxWarn(this, "正在进行【一键控制】油底壳抽油，不能操作相关的阀。");
                    return;
                }
            }
            if (Common.ExChangeGrp.GetBool("机油回抽"))
            {
                HashSet<string> ForbiddenValves = new HashSet<string> { "Y137阀控制" };
                if (ForbiddenValves.Contains(sw.Tag?.ToString() ?? ""))
                {
                    Var.MsgBoxWarn(this, "正在进行【一键控制】机油回抽，不能操作相关的阀。");
                    return;
                }
            }

            string str = sw.Switch == false ? "开启" : "关闭";
            string th = sw.Tag.ToString().Replace("控制", "");
            string strMessage = $"是否要{str}{th}?";

            // 特殊处理：对于泵来说，关闭操作不弹窗
            HashSet<string> noConfirmCloseSwitches = new HashSet<string>
            {
                "预供机油泵合闸控制",
                "污油排出泵合闸控制"
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
            // 暂时屏蔽
            if (sw.Tag.ToString() == "污油排出泵合闸控制")
            {
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


        private void btnLL194_Click(object sender, EventArgs e)
        {
            var pipePara = sender as UIButton;
            RW.Components.Widgets.frmSetValue set = new RW.Components.Widgets.frmSetValue();
            set.DecimalNumber = 1;
            set.MaxValue = 150;
            set.Unit = UnitEnum.celsius;
            set.Text = "温度设置";
            set.Value = Common.engineOilGrp[pipePara.Tag.ToString()];
            var dr = set.ShowDialog(this);
            if (dr == DialogResult.OK)
                Common.engineOilGrp.EngineOilPID = set.Value;
        }

        private void timerTube_Tick(object sender, EventArgs e)
        {
            foreach (var pipe in this.GetControls<UIPipe>())
            {
                pipe.Invalidate();
            }

            // 机油进口 流量计前后
            this.uiPInEo.Active = ucPipePara1.Value >= 0.2;  // 进机流量
            this.uiPlqqOut2.Active = ucPipePara24.Value >= 0.2; //流量计前压力

            //油底壳附近
            this.uiP90right4.Active = this.uiP90right3.Active = this.uiP90right2.Active = this.uiP90right.Active = this.swp90.Switch;
            this.uiP91right.Active = this.uiP91right2.Active = this.uiP91right3.Active = this.swp91.Switch;
            this.uiP92right.Active = this.swp92.Switch;
            this.uiP93right.Active = this.swp93.Switch;
            this.uiP122up.Active = this.uiP122down.Active = this.swp122.Switch;

            this.uiP90left.Active = this.swp90.Switch;
            this.uiP91left.Active = this.swp91.Switch;
            this.uiP92left.Active = this.swp92.Switch;
            this.uiP93left.Active = this.swp93.Switch;

            this.uiP90down1.Active = this.swp90.Switch;
            this.uiP91down1.Active = (this.swp90.Switch || this.swp91.Switch);
            this.uiP92down1.Active = (this.swp90.Switch || this.swp91.Switch || this.swp92.Switch);
            this.uiP95up.Active = uiP90left.Active || uiP91left.Active || uiP92left.Active || uiP93left.Active;

            // 机油预供泵
            this.uiP96right.Active = this.uiP96left.Active;
            this.uiP111up.Active = this.swp111.Switch && this.uiP111down.Active;
            this.uiP96left.Active = this.uiP95down.Active || this.uiP111up.Active;
            this.uiP97right1.Active = this.uiP97right2.Active = this.uiP100left.Active = this.uiP97right3.Active = this.uiP97right4.Active = this.uiP115up.Active = this.uiP116up.Active = (this.ucPipePara22.Value > 0 && this.swp97.Switch);

            // 进/出油 右侧通道
            this.uiPInEo.Active = ucPipePara6.Value > 0;
            this.uiPOutEo.Active = ucPipePara11.Value > 0;
            this.uiPlqqOut.Active = this.uiPlqqOut2.Active = this.ucPipePara24.Value > 0;

            // 机油进油管
            this.uiPYK1.Active = this.ucPipePara17.Value > 0;
            this.uiPYK2.Active = this.uiP136up.Active || this.uiPYK1.Active;
            this.uiPYK3.Active = this.uiP137up.Active = this.uiP139up.Active = this.uiPYK2.Active;

            // 机滤器前
            this.uiPjvq.Active = this.uiP100right.Active || this.uiPOutEo.Active;
            this.uiPjv2down1.Active = this.uiPjv2down2.Active = (this.ucPipePara19.Value > 0 && this.uiPjvq.Active);
            this.uiPjv1down1.Active = this.uiPjv1down2.Active = (this.ucPipePara18.Value > 0 && this.uiPjvq.Active);
            // 机滤器后
            this.uiPjv2up1.Active = this.uiPjv2up2.Active = (this.ucPipePara8.Value > 0 && this.uiPjv2down1.Active);
            this.uiPjv1up1.Active = this.uiPjv1up2.Active = (this.ucPipePara9.Value > 0 && this.uiPjv1down1.Active);
            this.uiPlqIn.Active = this.uiPjv2up2.Active || this.uiPjv1up2.Active;

            // 机油箱 - 过滤器
            this.uiP133up.Active = this.uip133down.Active = uip133down2.Active = (this.swp133.Switch && this.ucPipePara3.Value > 0);
            this.uiPJYXleft.Active = this.uiPJYXleft2.Active = (this.swp111.Switch && this.ucPipePara14.Value > 0);
            this.uiP111down.Active = (this.swp111.Switch && this.ucPipePara13.Value > 0);

            // 污油排出泵
            this.uiPyxdown1.Active = this.uiPyxdown2.Active = this.uiPdyxdown1.Active = this.uiPdyxdown2.Active = this.swpwypc.Switch;

            // 待处理机油箱
            this.uip134up.Active = this.uip134down.Active = this.swp134.Switch && this.ucPipePara21.Value > 0;
            this.uip134down2.Active = this.uip134down3.Active = (this.uip133down2.Active || this.uip134down.Active);

            // 机油耗测量
        }


        private void switchPictureBox3_SwitchChanged(object sender, bool value)
        {
            uiP92left.Active = swp92.Switch;
        }
        private void switchPictureBox4_SwitchChanged(object sender, bool value)
        {
            uiP93left.Active = swp93.Switch;
        }


        private void switchPictureBox29_SwitchChanged(object sender, bool value)
        {
            uiP97left.Active = swpJYYGB.Switch;
        }

        private void swp95_SwitchChanged(object sender, bool value)
        {
            this.uiP95down.Active = value;
        }

        private void swp100_SwitchChanged(object sender, bool value)
        {
            uiP100right.Active = swp100.Switch;
        }


        private void swp115_SwitchChanged(object sender, bool value)
        {
            uiP115down.Active = swp115.Switch;
        }

        private void swp137_SwitchChanged(object sender, bool value)
        {
            uiP137down.Active = swp137.Switch;
        }

        private void swp116_SwitchChanged(object sender, bool value)
        {
            uiP116down.Active = swp116.Switch;
        }

        private void swp139_SwitchChanged(object sender, bool value)
        {
            uiP139down.Active = swp139.Switch;
        }

        private void swp135_SwitchChanged(object sender, bool value)
        {
            uiP135up.Active = swp135.Switch;
        }

        private void swp136_SwitchChanged(object sender, bool value)
        {
            uiP136up.Active = uiP136down.Active = swp136.Switch;
        }

        /// <summary>
        /// 机油耗数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerEngineOilConsumption_Tick(object sender, EventArgs e)
        {
            try
            {
                // 一键控制
                this.rButton52.Switch = Common.ExChangeGrp.GetBool("预供机油循环");
                this.rButton53.Switch = Common.ExChangeGrp.GetBool("机油加热处理循环");
                this.rButton24.Switch = Common.ExChangeGrp.GetBool("油底壳加油");
                this.rButton56.Switch = Common.ExChangeGrp.GetBool("油底壳抽油");
                this.rButton80.Switch = Common.ExChangeGrp.GetBool("机油回抽");

                //// 初始值
                //var beginOilPValue = this.lblOilBeginP.Text.ToDouble();
                //var beginOilHValue = this.lblOilBeginP.Text.ToDouble();
                //var beginWeightValue = this.lblCurrentWeight.Text.ToDouble();
                //var beginoilReplenishment = this.lblBeforeReplenishment.Text.ToDouble();

                //// 实时
                //var currentP = Common.AI2Grp["机油耗测量压力"];
                //var currentH = Common.AI2Grp["机油耗测量液位"];

                //this.lblCurrentPressure.Text = currentP.ToString();
                //this.lblCurrentLiquidLevel.Text = currentH.ToString();
                //this.lblCurrentWeight.Text = YHA27.Instance.Weight.ToString();

                //if (beginOilPValue > 0 && !this.btnStartReplenishment.Switch)
                //{
                //    // 油耗测量1
                //    // (压差 / 机油密度 * 9.8（重力加速度）) * 油底壳面积
                //    var oilPressDiff = (beginOilPValue - currentP) / 900 * 9.8;
                //    double Area = (MiddleData.instnce.SelectModelConfig.OilPanLong * MiddleData.instnce.SelectModelConfig.OilPanWide).ToDouble();
                //    double OilPanArea = Math.Round(Area / 1000 / 1000, 4);
                //    this.lblMeasureFuelConsumption.Text = Math.Round(oilPressDiff * OilPanArea, 4).ToString();

                //    // 油耗测量3
                //    // 磅秤差/机油密度
                //    this.lblMeasureBC.Text = Math.Round((beginWeightValue - YHA27.Instance.Weight) / 900, 4).ToString();
                //}

                //if (this.btnStartReplenishment.Switch)
                //{
                //    // 油耗测量2
                //    var oilPressDiff = (currentP - beginoilReplenishment) / 900 * 9.8;
                //    this.lblMeasureReplenishment.Text = Math.Round(0.16 * oilPressDiff, 1).ToString();
                //}
            }
            catch (Exception ex)
            {

            }

        }



        /// <summary>
        /// 通用的开始试验逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
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

            // 机油系统
            if (setResult && btn.OutputTagName == "机油箱加油")
            {
                if (Convert.ToDouble(this.ucPipePara3.Value) >= 1500)
                {
                    Var.MsgBoxWarn(this, "机油箱加油开启条件：机油箱液位＜1500mm。");
                    return;
                }
            }
            else if (setResult && btn.OutputTagName == "机油加热处理循环")
            {
                StringBuilder errorMessages = new StringBuilder();
                int msgIndex = 0;

                if (Convert.ToDouble(this.ucPipePara3.Value) <= 600)
                {
                    errorMessages.AppendLine($"不满足开启条件{++msgIndex}：机油箱液位>600mm。");
                }
                if (Convert.ToDouble(this.ucPipePara15.Value) >= 30)
                {
                    errorMessages.AppendLine($"不满足开启条件{++msgIndex}：机油箱温度<30℃。");
                }
                if (msgIndex > 0)
                {
                    Var.MsgBoxWarn(this, errorMessages.ToString());
                    return;
                }
            }
            else if (setResult && btn.OutputTagName == "油底壳加油")
            {
                if (Convert.ToDouble(this.ucPipePara3.Value) <= 600)
                {
                    Var.MsgBoxWarn(this, "油底壳加油开启条件：机油箱液位>600mm。");
                    return;
                }
            }
            else if (setResult && btn.OutputTagName == "油底壳抽油")
            {
                if (Convert.ToDouble(this.ucPipePara3.Value) >= 1500)
                {
                    Var.MsgBoxWarn(this, "油底壳抽油开启条件：机油箱液位<1500mm。");
                    return;
                }
            }
            else if (setResult && btn.OutputTagName == "机油回抽")
            {
                if (Convert.ToDouble(this.ucPipePara3.Value) >= 1500)
                {
                    Var.MsgBoxWarn(this, "机油回抽开启条件：机油箱液位<1500mm。");
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

        /// <summary>
        /// 模式切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRYXHSelect_Click(object sender, EventArgs e)
        {
            RButton btn = sender as RButton;
            if (btn.Switch) return;

            // 油底壳抽油
            if (rButton56.Switch)
            {
                Var.MsgBoxWarn(this, "正在进行油底壳抽油，不能进行切换。");
                return;
            }
            Common.ExChangeGrp.SetBool("油底壳抽油选择油箱", (btn.Tag.ToString() == "1"));

        }
    }
}

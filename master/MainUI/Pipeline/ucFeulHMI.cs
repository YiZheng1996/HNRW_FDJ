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
    public partial class ucFeulHMI : UserControl
    {
        public ucFeulHMI()
        {
            InitializeComponent();
        }

        // 最低流动液位
        public const int OilLevel = 100;

        // 柴油箱是否存在液体
        public bool flowResult { get; set; } = false;

        // 数字量的集合
        Dictionary<string, SwitchPictureBox> dicValve = new Dictionary<string, SwitchPictureBox>();
        // 模拟量的集合
        Dictionary<string, ucPipePara> DoubleDicValve = new Dictionary<string, ucPipePara>();

        public void Init()
        {
            uipry33up1.Link(uiPipe2);
            uip164up2.Link(uiPipe5);
            uip190right1.Link(uip190right3);
            uip190right3.Link(uip190right2);
            uip164up1.Link(uip164up2);

            timerTube.Enabled = true;
            timerTube.Start();
            timer1.Enabled = true;
            timer1.Start();

            LoadAllValve(this);

            Common.DOgrp.KeyValueChange += DOgrp_KeyValueChange;
            Common.DIgrp.KeyValueChange += DIgrp_KeyValueChange;
            Common.AOgrp.KeyValueChange += AOgrp_KeyValueChange;
            Common.AIgrp.KeyValueChange += AIgrp_KeyValueChange;
            Common.fuelGrp.KeyValueChange += FuelGrp_KeyValueChange;
            Common.AI2Grp.KeyValueChange += EngineParaGrp_KeyValueChange;
        }



        /// <summary>
        /// PLC2中的AI压力/温度点位补齐
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
        /// 燃油数据更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FuelGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
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
        /// 泵状态变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DIgrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (dicValve.ContainsKey(e.Key))
            {
                dicValve[e.Key].Switch = e.Value;
            }
        }

        /// <summary>
        /// 阀状态变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DOgrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (dicValve.ContainsKey(e.Key))
            {
                dicValve[e.Key].Switch = e.Value;
            }
        }

        /// <summary>
        /// 把所有点位添加到字典
        /// </summary>
        private void LoadAllValve(Control con)
        {
            foreach (Control item in con.Controls)
            {
                LoadAllValve(item);
                return;
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
                Var.MsgBoxWarn(this, $"燃油原理图 init 出现异常： {ex.Message.ToString()}");
            }

        }

        /// <summary>
        /// 通用点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sw_Valve_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, EventArgs>(sw_Valve_Click), sender, e);
                return;
            }
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

            if (Common.ExChangeGrp.GetBool("燃油循环"))
            {
                if (sw.Tag.ToString() == "Y179阀控制" || sw.Tag.ToString() == "Y183阀控制" || sw.Tag.ToString() == "Y190阀控制")
                {
                    Var.MsgBoxWarn(this, "正在进行【一键控制】燃油循环，不能操作相关的阀。");
                    return;
                }
            }
            if (Common.ExChangeGrp.GetBool("燃油箱回油冷却"))
            {
                if (sw.Tag.ToString() == "Y61阀控制")
                {
                    Var.MsgBoxWarn(this, "正在进行【一键控制】燃油箱回油冷却，不能操作相关的阀。");
                    return;
                }
            }

            // 保护 
            //if (sw.Tag.ToString() == "燃油泵1合闸控制" || sw.Tag.ToString() == "燃油泵2合闸控制")
            //{
            //    if (Common.ExChangeGrp.GetBool("燃油循环"))
            //    {
            //        // 不能开始
            //        Var.MsgBoxWarn(this, "正在进行 燃油循环，不能进行操作。");
            //        return;
            //    }
            //}

            string str = sw.Switch == false ? "开启" : "关闭";
            string th = sw.Tag.ToString().Replace("控制", "");
            string strMessage = $"是否要{str}{th}?";

            // 特殊处理：对于泵来说，关闭操作不弹窗
            HashSet<string> noConfirmCloseSwitches = new HashSet<string>
            {
                "燃油泵1合闸控制",
                "燃油泵2合闸控制"
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
        /// 两位三通点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void swp_Valve_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, EventArgs>(swp_Valve_Click), sender, e);
                return;
            }
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

            string str = "切换";
            string th = sw.Tag.ToString().Replace("控制", "");
            string strMessage = $"是否要{str} {th} 的流向吗?";
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

                    Var.MsgBoxWarn(this, $"{th}+{str}失败！原因：{ex.Message}");
                }
            }

        }

        private void ucPipePara12_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 通用流量调节阀给定
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
            set.Text = "流量给定";
            set.Value = Common.AOgrp[pipePara.Tag.ToString()];
            var dr = set.ShowDialog(this);
            if (dr == DialogResult.OK)
                Common.AOgrp[pipePara.Tag.ToString()] = set.Value;
        }

        private void timerTube_Tick(object sender, EventArgs e)
        {
            foreach (var pipe in this.GetControls<UIPipe>())
            {
                pipe.Invalidate();
            }

            // 进油
            this.uiPipe2.Active = ucPipePara15.Value > 0;
            this.uip164up1.Active = this.uip164up2.Active = (this.uiPipe2.Active && this.swp164.Switch);

            // 粗滤器
            this.uiPcl2q.Active = this.ucPipePara1.Value > 0;
            this.uiPcl2h.Active = this.ucPipePara2.Value > 0;
            this.uiPcl1q.Active = this.ucPipePara8.Value > 0;
            this.uiPcl1h.Active = this.uiPcl1h2.Active = this.ucPipePara17.Value > 0;
            this.uiPipe4.Active = this.uiPcl2h.Active || this.uiPcl1h2.Active;

            // 燃油耗仪
            this.uip190left.Active = this.uip190right1.Active = this.uip190right2.Active = this.uip190right3.Active = this.swp190.Switch && this.uiPipe4.Active;

            // 是否存在液位(先定 100 mm = 10 cm) 时
            flowResult = this.ucPipePara4.Value > 0 && (this.ucPipePara2.Value > 0 || this.ucPipePara17.Value > 0);
            //183 部分比价复杂 检测柴油箱液位是否大于
            if (flowResult && !swp183.Switch)
            {
                //为0时 正常流向
                uip183down1.Active = uip183down2.Active = true;
            }
            else
            {
                //为1 时，走燃油耗仪
                uip183down1.Active = uip183down2.Active = false;
            }

            //184 为燃油回流的时候流动
            this.uip184up.Active = !swp182.Switch && swp184.Switch && this.ucPipePara9.Value > 0;

            // 中间部分 如果粗滤器存在滤后压力
            if (uip190right3.Active || uip183down2.Active || uip184up.Active)
            {
                uipry2733.Active = uipry33.Active = uipry27.Active = uipry27down1.Active = uipry27down2.Active = uipry33up1.Active = uipry33up2.Active = true;
            }
            else
            {
                uipry2733.Active = uipry33.Active = uipry27.Active = uipry27down1.Active = uipry27down2.Active = uipry33up1.Active = uipry33up2.Active = false;
            }

            //// 燃油泵流量计
            this.uipry33up3.Active = this.ucPipePara16.Value > 0 && this.uipry33up2.Active;
            this.uipry27down3.Active = this.ucPipePara18.Value > 0 && this.uipry27down2.Active;

            // 精滤器
            this.uiPJV2q.Active = (this.ucPipePara12.Value > 0 && (uipry33up3.Active || uipry33right1.Active));
            this.uiPJV2h1.Active = this.uiPJV2h2.Active = (this.ucPipePara11.Value > 0 && this.uiPJV2q.Active);

            this.uiPJV1q.Active = (this.ucPipePara3.Value > 0 && (uipry27down3.Active || uipry27right1.Active));
            this.uiPJV1h.Active = (this.ucPipePara10.Value > 0 && this.uiPJV1q.Active);

            //燃油进油管
            this.uipry27right2.Active = this.ucPipePara14.Value > 0;
            this.uip181right.Active = this.swp181.Switch && this.uipry27right2.Active;

            // 回油
            this.uip182left.Active = this.uip182.Active = this.ucPipePara9.Value > 0;
            // 184 左边
            this.uip184left.Active = !this.swp184.Switch && this.uip182left.Active;
            this.uip179left1.Active = this.uip179left2.Active = this.swp179.Switch && this.uip184left.Active;

            // 油耗仪
            this.ucPipePara13.Value = ET4500.Instance.fuelConsumption;
            this.ucPipePara19.Value = ET4500.Instance.fuelPercentage;
            this.ucPipePara21.Value = ET4500.Instance.remainingFuel;
            this.lblYHStatus.Text = ET4500.Instance.fuelStatus;

            // 计算差值
            var MassFlowCC = this.ucPipePara14.Value - this.ucPipePara9.Value;

            this.flowDiff.Text = Math.Round(MassFlowCC, 1).ToString();
            this.lblOilCoast.Text = Math.Round(MassFlowCC / MiddleData.instnce.EnginePower, 1).ToString();
        }

        private void switchPictureBox13_SwitchChanged(object sender, bool value)
        {
            this.uiPRY1left.Active = uipry27right1.Active = swp27.Switch;
        }


        private void switchPictureBox6_SwitchChanged(object sender, bool value)
        {
            uip61.Active = swp61.Switch;
        }


        private void swp183_SwitchChanged(object sender, bool value)
        {
            uip190left.Active = swp183.Switch;
            //uiPipe9.Active = uiPipe10.Active = uiPipe15.Active = swp183.Switch;
        }


        private void swp33_SwitchChanged(object sender, bool value)
        {
            this.uiPRY2left.Active = this.uipry33right1.Active = this.swp33.Switch;
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

            // 燃油系统
             if (setResult && btn.OutputTagName == "燃油循环")
            {
                if (Convert.ToDouble(this.ucPipePara4.Value) <= 300)
                {
                    Var.MsgBoxWarn(this, "燃油循环开启条件：燃油箱液位＞300mm。");
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

            // 如果燃油循环正打开
            if (btn.OutputTagName == "燃油循环油泵选择")
            {
                if (rButton58.Switch)
                {
                    var result = Var.MsgBoxYesNo(this, "确定要切换燃油输送泵吗？");
                    if (!result) return;
                }
                Common.ExChangeGrp.SetBool("燃油循环油泵选择", (btn.Tag.ToString() == "1"));
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.rButton58.Switch = Common.ExChangeGrp.GetBool("燃油循环");
            //this.rButton37.Switch = Common.ExChangeGrp.GetBool("燃油耗测量");
            this.rButton64.Switch = Common.ExChangeGrp.GetBool("燃油箱回油冷却");

            var ry1 = Common.ExChangeGrp.GetBool("燃油循环油泵选择");
            this.rButton28.Switch = !ry1; // 
            this.rButton40.Switch = ry1;

            //var ry2 = Common.ExChangeGrp.GetBool("燃油耗测量油泵选择");
            //this.rButton66.Switch = !ry2; // 燃油泵1
            //this.rButton65.Switch = ry2;
        }
    }
}

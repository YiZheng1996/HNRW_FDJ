using MainUI.Global;
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

namespace MainUI.TestScreen
{
    public partial class ucDuctHeating : UserControl
    {
        // 仪表盘
        Dictionary<string, ucParamKeyUI> ucParamKeyList = new Dictionary<string, ucParamKeyUI>();

        // 指示灯
        Dictionary<string, UILight> ucUILightList = new Dictionary<string, UILight>();

        public ucDuctHeating()
        {
            InitializeComponent();

            Common.AirDuctGrp.FaultKeyValueChange += AirDuctGrp_FaultKeyValueChange;
            Common.AirDuctGrp.DoubleChange += AirDuctGrp_DoubleChange;
            Common.AirDuctGrp.KeyValueChange += AirDuctGrp_KeyValueChange;

            EachControl(this);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucDuctHeating_Load(object sender, EventArgs e)
        {
            ucParamTemp1.SetRand(0, 190, 200); // 出口温度
            ucParamTemp2.SetRand(0, 190, 200); //内腔温度1
            ucParamTemp3.SetRand(0, 190, 200); //内腔温度2
            ucParamFrequency1.SetRand(0, 190, 200); //风机频率1

            ucParamTemp4.SetRand(0, 190, 200); // 出口温度2
            ucParamTemp5.SetRand(0, 190, 200); //内腔温度1
            ucParamTemp6.SetRand(0, 190, 200); //内腔温度2
            ucParamFrequency2.SetRand(0, 190, 200); //风机频率2

            timer1.Enabled = true;
            timer1.Start();
        }

        /// <summary>
        /// 故障值更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AirDuctGrp_FaultKeyValueChange(object sender, Modules.EventArgsModel.DIValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, Modules.EventArgsModel.DIValueChangedEventArgs>(AirDuctGrp_FaultKeyValueChange), sender, e);
                return;
            }

            if (ucUILightList.ContainsKey(e.Key))
            {
                ucUILightList[e.Key].State = e.Value ? UILightState.On : UILightState.Off;
            }
            else
            {
                if (e.Key == "一号一键启动")
                {
                    this.btnStart1.Switch = e.Value;
                }
                else if (e.Key == "一号一键停止")
                {
                    this.btnStop1.Switch = e.Value;
                }
                else if (e.Key == "二号一键启动")
                {
                    this.btnStart2.Switch = e.Value;
                }
                else if (e.Key == "二号一键停止")
                {
                    this.btnStop2.Switch = e.Value;
                }
            }
        }

        /// <summary>
        /// 数值更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AirDuctGrp_DoubleChange(object sender, Modules.EventArgsModel.DoubleValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, Modules.EventArgsModel.DoubleValueChangedEventArgs>(AirDuctGrp_DoubleChange), sender, e);
                return;
            }

            if (ucParamKeyList.ContainsKey(e.Key))
            {
                ucParamKeyList[e.Key].GaugeValue = e.Value;
            }
            else 
            {
                if (e.Key == "一号设置出口温度")
                {
                    this.nudTempExport1.Value = e.Value;
                }
                else if (e.Key == "二号设置出口温度")
                {
                    this.nudTempExport2.Value = e.Value;
                }
                else if (e.Key == "一号设置内腔温度")
                {
                    this.nudTempLumen1.Value = e.Value;
                }
                else if (e.Key == "二号设置内腔温度")
                {
                    this.nudTempLumen2.Value = e.Value;
                }
            }
        }

        /// <summary>
        /// bool类型值更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AirDuctGrp_KeyValueChange(object sender, Modules.EventArgsModel.DIValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, Modules.EventArgsModel.DIValueChangedEventArgs>(AirDuctGrp_KeyValueChange), sender, e);
                return;
            }

            if (ucUILightList.ContainsKey(e.Key))
            {
                ucUILightList[e.Key].State = e.Value ? UILightState.On : UILightState.Off;
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

            if (con is ucParamKeyUI)
            {
                //添加仪表盘
                ucParamKeyUI paramKey = con as ucParamKeyUI;
                if (paramKey.Key != null && paramKey.Key.ToString() != string.Empty)
                {
                    ucParamKeyList.Add(paramKey.Key.ToString(), paramKey);
                }
            }
            else if (con is UILight)
            {
                //添加机组测量值数值显示标签
                UILight light = con as UILight;
                if (light.Tag != null && light.Tag.ToString() != string.Empty)
                {
                    ucUILightList.Add(light.Tag.ToString(), light);
                }
            }
        }

        /// <summary>
        /// 出口温度设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTempExport1_Click(object sender, EventArgs e)
        {
            try
            {
                var button = sender as RButton;
                var tag = button.Tag.ToString();
                if (tag == "1")
                {
                    Common.AirDuctGrp.SetDouble("一号DO.一号设置出口温度", this.nudTempExport1.Value);
                }
                else
                {
                    Common.AirDuctGrp.SetDouble("二号DO.二号设置出口温度", this.nudTempExport2.Value);
                }
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "设置出口温度失败。");
            }
        }

        /// <summary>
        /// 内腔温度设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTempLumen1_Click(object sender, EventArgs e)
        {
            try
            {
                var button = sender as RButton;
                var tag = button.Tag.ToString();
                if (tag == "1")
                {
                    Common.AirDuctGrp.SetDouble("一号DO.一号设置内腔温度", this.nudTempLumen1.Value);
                }
                else
                {
                    Common.AirDuctGrp.SetDouble("二号DO.二号设置内腔温度", this.nudTempLumen2.Value);
                }
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "设置内腔温度失败。");
            }
        }

        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart1_Click(object sender, EventArgs e)
        {
            try
            {
                var button = sender as RButton;
                var key = button.OutputTagName.ToString();
                if (key.Contains("一号"))
                {
                    key = $"一号DO.{key}";
                    Common.AirDuctGrp.SetBool("一号DO.一号一键停止", false);
                }
                else
                {
                    key = $"二号DO.{key}";
                    Common.AirDuctGrp.SetBool("二号DO.二号一键停止", false);
                }
                Thread.Sleep(200);
                Common.AirDuctGrp.SetBool(key, true);
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "下发开始风道加热指令失败。");
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop1_Click(object sender, EventArgs e)
        {
            try
            {
                var button = sender as RButton;
                var key = button.OutputTagName.ToString();
                if (key.Contains("一号"))
                {
                    key = $"一号DO.{key}";
                    Common.AirDuctGrp.SetBool("一号DO.一号一键启动", false);
                }
                else
                {
                    key = $"二号DO.{key}";
                    Common.AirDuctGrp.SetBool("二号DO.二号一键启动", false);
                }
                Thread.Sleep(200);
                Common.AirDuctGrp.SetBool(key, true);
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "下发停止风道加热指令失败。");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblJinQiZ.Text = Common.AIgrp["进气流量测量左"].ToString();
            this.lblJinQiY.Text = Common.AIgrp["进气流量测量右"].ToString();

            this.btnStart1.Switch = Common.AirDuctGrp.GetBool("一号一键启动");
            this.btnStart2.Switch = Common.AirDuctGrp.GetBool("二号一键启动");
            this.btnStop1.Switch = Common.AirDuctGrp.GetBool("一号一键停止");
            this.btnStop2.Switch = Common.AirDuctGrp.GetBool("二号一键停止");

            this.lblJQZ.Text = Common.AOgrp["进气风道左调节阀控制"].ToString();
            this.lblJQY.Text = Common.AOgrp["进气风道右调节阀控制"].ToString();
            this.lblPQZ.Text = Common.AOgrp["排气风道左调节阀控制"].ToString();
            this.lblPQY.Text = Common.AOgrp["排气风道右调节阀控制"].ToString();
        }

        /// <summary>
        /// 流量调节
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetFlow_Click(object sender, EventArgs e)
        {
            var pipePara = sender as RButton;
            RW.Components.Widgets.frmSetValue set = new RW.Components.Widgets.frmSetValue();
            set.DecimalNumber = 0;

            var pub = MiddleData.instnce.PubsConfig;
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
    }
}

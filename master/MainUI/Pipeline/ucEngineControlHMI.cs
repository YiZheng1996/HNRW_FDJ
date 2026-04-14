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
using MainUI.Config.Modules;
using System.IO;
using System.Threading;
using MainUI.Config;
using static MainUI.Modules.EventArgsModel;

namespace MainUI
{
    public partial class ucEngineControlHMI : UserControl
    {
        SysConstantParas SysConstantParasConfig = new SysConstantParas();

        /// <summary>
        /// 手动控制点位
        /// </summary>
        Dictionary<string, List<RButton>> dicBtn = new Dictionary<string, List<RButton>>();
        /// <summary>
        /// 手动控制灯状态
        /// </summary>
        Dictionary<string, List<UILight>> dicLight = new Dictionary<string, List<UILight>>();

        /// <summary>
        /// 数值状态
        /// </summary>
        Dictionary<string, Label> dicLabel = new Dictionary<string, Label>();

        /// <summary>
        /// 通用测试列表
        /// </summary>
        public Dictionary<string, List<ContronlPoint>> _contronlMap = new Dictionary<string, List<ContronlPoint>>();


        public ucEngineControlHMI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            // 加载手动控制点位
            EachControl(panelManual);
            // 绑定数值
            EachValueControl();

            timer1.Enabled = true;
            timer1.Start();

            Common.AOgrp.KeyValueChange += AOgrp_KeyValueChange;
            Common.DIgrp.KeyValueChange += DIgrp_KeyValueChange;
            Common.DOgrp.KeyValueChange += DOgrp_KeyValueChange;
            //Common.DIgrp.Fresh();
            //Common.DOgrp.Fresh();
        }

        /// <summary>
        /// 水极板上升点动按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterPlateUp_MouseDown(object sender, MouseEventArgs e)
        {
            Common.DOgrp["水阻下降控制"] = false;
            Thread.Sleep(10);
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
            Thread.Sleep(10);
            Common.DOgrp["水阻下降控制"] = false;
        }

        /// <summary>
        /// 水极板下降点动按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterPlateDown_MouseDown(object sender, MouseEventArgs e)
        {
            Common.DOgrp["水阻上升控制"] = false;
            Thread.Sleep(10);
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
            Thread.Sleep(10);
            Common.DOgrp["水阻上升控制"] = false;
        }

        /// <summary>
        /// 值改变事件
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

            if (e.Key == "主发通风机1合闸控制"
                || e.Key == "主发通风机2合闸控制"
                || e.Key == "抽油泵合闸控制")
            {
                //if (e.Value)
                //{
                //    this.uiLight48.State = UILightState.On;
                //    this.uiLight47.State = UILightState.Off;
                //}
                //else
                //{
                //    this.uiLight48.State = UILightState.Off;
                //    this.uiLight47.State = UILightState.On;
                //}
            }
            else
            {
                if (dicBtn.ContainsKey(e.Key))
                {
                    foreach (var item in dicBtn[e.Key])
                    {
                        item.Switch = e.Value;
                    }
                }
            }

        }

        /// <summary>
        /// 值改变事件
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

            ////泵类信号灯特殊处理
            //if (e.Key == "主发通风机1主接检测")
            //{
            //    if (e.Value)
            //    {
            //        this.uiLight48.State = UILightState.On;
            //        this.uiLight47.State = UILightState.Off;
            //        this.rButton1.Switch = true;
            //    }
            //    else
            //    {
            //        this.uiLight48.State = UILightState.Off;
            //        this.uiLight47.State = UILightState.On;
            //        this.rButton1.Switch = false;
            //    }
            //}
            //else if(e.Key == "主发通风机2主接检测")
            //{
            //    if (e.Value)
            //    {
            //        this.uiLight3.State = UILightState.On;
            //        this.uiLight2.State = UILightState.Off;
            //        this.rButton2.Switch = true;
            //    }
            //    else
            //    {
            //        this.uiLight3.State = UILightState.Off;
            //        this.uiLight2.State = UILightState.On;
            //        this.rButton2.Switch = false;
            //    }
            //}
            //else if (e.Key == "抽油泵合闸检测")
            //{
            //    if (e.Value)
            //    {
            //        this.uiLight9.State = UILightState.On;
            //        this.uiLight8.State = UILightState.Off;
            //        this.rButton4.Switch = true;
            //    }
            //    else
            //    {
            //        this.uiLight9.State = UILightState.Off;
            //        this.uiLight8.State = UILightState.On;
            //        this.rButton4.Switch = false;
            //    }
            //}
            else if (dicLight.ContainsKey(e.Key))
            {
                foreach (var item in dicLight[e.Key])
                {
                    item.State = e.Value == false ? UILightState.Off : UILightState.On;
                }
            }
        }

        /// <summary>
        /// AO值改变
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

            if (dicLabel.ContainsKey(e.Key))
            {
                dicLabel[e.Key].Text = e.Value.ToString();
            }
        }

        /// <summary>
        /// 添加界面所有灯和按钮
        /// </summary>
        /// <param name="con"></param>
        private void EachControl(Control con)
        {
            foreach (Control item in con.Controls)
            {
                EachControl(item);
            }
            if (con is UILight)
            {
                //添加灯
                UILight light = con as UILight;
                if (light.Tag != null && light.Tag.ToString() != string.Empty)
                {
                    List<UILight> UILightList;
                    dicLight.TryGetValue(light.Tag.ToString(), out UILightList);
                    if (UILightList == null)
                    {
                        UILightList = new List<UILight>() { };
                        UILightList.Add(light);
                        dicLight.Add(light.Tag.ToString(), UILightList);
                    }
                    else
                    {
                        dicLight[light.Tag.ToString()].Add(light);
                    }
                }
            }
            if (con is RButton)
            {
                //添加按钮
                RButton btn = con as RButton;
                if (btn.Tag != null && btn.Tag.ToString() != string.Empty)
                {
                    List<RButton> ButtonList;
                    dicBtn.TryGetValue(btn.Tag.ToString(), out ButtonList);
                    if (ButtonList == null)
                    {
                        ButtonList = new List<RButton>() { };
                        ButtonList.Add(btn);
                        dicBtn.Add(btn.Tag.ToString(), ButtonList);
                    }
                    else
                    {
                        dicBtn[btn.Tag.ToString()].Add(btn);
                    }

                }
            }
        }

        /// <summary>
        /// 添加数值绑定
        /// </summary>
        /// <param name="con"></param>
        private void EachValueControl()
        {
            dicLabel.Add("进气风道右调节阀控制", lblJQY);
            dicLabel.Add("进气风道左调节阀控制", lblJQZ);
            dicLabel.Add("排气风道右调节阀控制", lblPQY);
            dicLabel.Add("排气风道左调节阀控制", lblPQZ);
            dicLabel.Add("水阻箱进水电动调节阀", lblSZ);
        }

        /// <summary>
        /// 通用点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sw_Valve_Click(object sender, EventArgs e)
        {

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
                return;
            }

            if (string.IsNullOrEmpty(sw.Tag.ToString()))
            {
                return;
            }
            string msg = "打开";
            string th = sw.Tag.ToString().Replace("控制", "");
            var dr = Var.MsgBoxYesNo(this, $"确定要{msg}{th}吗？");
            if (!dr) return;

            // true和false都是脉冲了
            Common.DOgrp[sw.Tag.ToString()] = true;
        }

        /// <summary>
        /// 流量调节
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetFlow_Click(object sender, EventArgs e)
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
        /// 实时检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {

        }

    }

}

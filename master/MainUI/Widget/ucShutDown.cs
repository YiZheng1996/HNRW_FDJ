using MainUI.Global;
using RW.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Widget
{
    /// <summary>
    /// 正常停机页面控件
    /// </summary>
    public partial class ucShutDown : UserControl
    {
        ManaulData manaulData = new ManaulData();

        public ucShutDown()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            timerState.Enabled = true;
            timerState.Start();
            timerFast.Enabled = true;
            timerFast.Start();

            manaulData.BeginInvertSpeed = nudBeginInvertSpeed.Value;
            manaulData.BeginCurrent = nudBeginCurrent.Value;
        }

        /// <summary>
        /// 慢速刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerState_Tick(object sender, EventArgs e)
        {
            //刷新三个标签数据
            this.lblWaterTemp.Text = Common.AI2Grp["T1高温水出机温度"].ToString();
            this.lblEoTemp.Text = Common.AI2Grp["T21主油道进口油温"].ToString();
            this.lblEoTempOut.Text = Common.AI2Grp["T20机油泵出口油温"].ToString();

            this.lblEoPressure.Text = Common.AI2Grp["P21主油道进口油压"].ToString();
            this.lblEoPressureOut.Text = Common.AI2Grp["P20机油泵出口压力"].ToString();

            //按钮状态
            if (Common.ExChangeGrp.GetBool("燃油循环"))
            {
                this.btnFuelCycleOpen.Switch = true;
                //this.btnFuelCycleClose.Switch = false;
            }
            else
            {
                this.btnFuelCycleOpen.Switch = false;
                //this.btnFuelCycleClose.Switch = true;
            }
            if (Common.ExChangeGrp.GetBool("预供机油循环"))
            {
                this.btnEoOpen.Switch = true;
                //this.btnEoClose.Switch = false;
            }
            else
            {
                this.btnEoOpen.Switch = false;
                //this.btnEoClose.Switch = true;
            }
            if (Common.DOgrp["发动机DC24V供电"])
            {
                this.btn24Open.Switch = true;
                this.btn24Close.Switch = false;
            }
            else
            {
                this.btn24Open.Switch = false;
                this.btn24Close.Switch = true;
            }

        }

        /// <summary>
        /// 快速刷新
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
        /// 设置转速更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        private void nudBeginInvertSpeed_ValueChanged(object sender, double value)
        {
            manaulData.BeginInvertSpeed = value;
        }

        private void nudBeginCurrent_ValueChanged(object sender, double value)
        {
            manaulData.BeginCurrent = value;
        }

        /// <summary>
        /// 发动机转速设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetBeginSpeed_Click(object sender, EventArgs e)
        {
            this.btnSetBeginSpeed.Focus();

            Common.AOgrp["发动机油门调节"] = manaulData.BeginInvertSpeed;
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
                        //DC24V操作
                        Common.DOgrp["发动机DC24V供电"] = Convert.ToBoolean(sw.Tag.ToInt()); //!Common.DOgrp[sw.Tag.ToString()];
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

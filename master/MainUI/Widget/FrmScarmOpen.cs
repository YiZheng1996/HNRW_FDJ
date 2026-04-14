using MainUI.Global;
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
using static MainUI.Widget.FrmGKChangeInfo;

namespace MainUI.Widget
{
    /// <summary>
    /// 急停弹窗页面
    /// </summary>
    public partial class FrmScarmOpen : Form
    {
        public FrmScarmOpen()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// 是否正在弹窗
        /// </summary>
        private bool _isOpen;
        public bool IsOpen
        {
            get
            {
                return _isOpen;
            }
            set
            {
                _isOpen = value;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            this.timerSlow.Start();
            Common.engineOilGrp.KeyValueChange += EngineOilGrp_KeyValueChange;
        }

        private void EngineOilGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DoubleValueChangedEventArgs>(EngineOilGrp_KeyValueChange), sender, e);
                return;
            }

            if (e.Key == "机油温度设置PID")
            {
                this.nudEoTemp.Value = e.Value;
            }
        }

        /// <summary>
        /// 指示灯的状态（慢速）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerSlow_Tick(object sender, EventArgs e)
        {
            //暂定 刷新数值显示和按钮状态

            this.lblSpeed.Text = MiddleData.instnce.EngineSpeed.ToString();
            this.LCVoltageValue.Text = Common.excitationGrp["励磁电压检测"].ToString();
            this.LCCurrentValue.Text = Common.excitationGrp["励磁电流检测"].ToString();

            this.lblEoSetTemp.Text = Common.engineOilGrp["机油温度设置PID"].ToString();
            this.lblEoTemp.Text = Common.AI2Grp["T21主油道进口油温"].ToString();

            //按钮
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
            this.btnY181Open.Switch = Common.DIgrp["紧急停车1开到位-181"];
            this.btnY181Close.Switch = Common.DIgrp["紧急停车1关到位-181"];
            this.btnY182Open.Switch = Common.DIgrp["紧急停车2开到位-182"];
            this.btnY182Close.Switch = Common.DIgrp["紧急停车2关到位-182"];
        }


        /// <summary>
        /// 仪表盘较快数据的刷新（快速）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerFast_Tick(object sender, EventArgs e)
        {

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
                    if (sw.OutputTagName == "Y181阀控制" || sw.OutputTagName == "Y182阀控制")
                    {
                        Common.DOgrp[sw.OutputTagName] = Convert.ToBoolean(sw.Tag.ToInt());
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception($"{th}+{str}失败！原因：" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 设置机油温度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rButton9_Click(object sender, EventArgs e)
        {
            if (!Common.opcStatus.NoError)
            {
                Var.MsgBoxWarn(this, "控制柜 PLC 通讯异常。");
                return;
            }

            var pipePara = sender as RButton;
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


        /// <summary>
        /// 设置机油温度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rButton3_Click(object sender, EventArgs e)
        {
            Common.engineOilGrp.EngineOilPID = this.nudEoTemp.Value;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.IsOpen = false;
            this.Close();
        }


        private int x, y;
        private AlertFormAction action;
        public void ShowInfo()
        {
            // 设置窗口启始位置为屏幕中央
            this.IsOpen = true;

            this.StartPosition = FormStartPosition.CenterScreen;
            //this.x = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            //this.y = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            //this.Location = new Point(x, y);

            //this.Opacity = 0.0;

            base.Show();

            action = AlertFormAction.Start;
        }
    }
}

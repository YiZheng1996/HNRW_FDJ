using MainUI.Equip;
using MainUI.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.HMI_Auto
{
    /// <summary>
    /// 机油/燃油耗测量
    /// </summary>
    public partial class ucFuelConsumptionMeasurement : UserControl
    {
        public ucFuelConsumptionMeasurement()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 是否开始测量
        /// </summary>
        public bool IsStartOilEngineMeasurement = false;

        /// <summary>
        /// 是否停止测量 - 打开泵动作
        /// </summary>
        public bool IsStopOilEngineMeasurement = false;

        /// <summary>
        /// 机油耗测量的时间(小时)
        /// </summary>
        public double timesDiff = 0;

        /// <summary>
        /// 按下停止时的发动机功率
        /// </summary>
        public double StopEnginePower = 0;

        /// <summary>
        /// 机油耗测量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartOil_Click(object sender, EventArgs e)
        {
            // 记录初始值
            this.lblOilBeginP.Text = this.lblCurrentPressure.Text;
            this.lblOilBeginH.Text = this.lblCurrentLiquidLevel.Text;
            this.lblBCWeight.Text = YHA27.Instance.Weight.ToString();

            this.lblStartOilTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.lblStopOilTime.Text = "-";
            this.lblOilTime.Text = "-";

            IsStartOilEngineMeasurement = true;
            IsStopOilEngineMeasurement = false;
            timesDiff = 0;
            StopEnginePower = 0;
        }

        /// <summary>
        /// 停止机油耗测量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopOil_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsStartOilEngineMeasurement)
                {
                    Var.MsgBoxInfo(this, "请先开始机油耗测量。");
                    return;
                }

                this.lblStopOilTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // 解析开始时间和结束时间计算时间差
                DateTime startTime = DateTime.Parse(this.lblStartOilTime.Text);
                DateTime endTime = DateTime.Parse(this.lblStopOilTime.Text);
                TimeSpan timeDiff = endTime - startTime;

                // 转换为小时
                timesDiff = Math.Round(timeDiff.TotalHours, 1);

                // 显示测量时间（小时）
                this.lblOilTime.Text = timesDiff + " h";

                StopEnginePower = MiddleData.instnce.EnginePower;
                IsStopOilEngineMeasurement = true;

                // todo 打开泵动作

            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 强制停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnForceStop_Click(object sender, EventArgs e)
        {
            try
            {
                IsStartOilEngineMeasurement = false;
                IsStopOilEngineMeasurement = false;
                // todo 关泵动作

            }
            catch (Exception ex)
            {

            }

        }

        private void timerEngineOilConsumption_Tick(object sender, EventArgs e)
        {
            try
            {
                // 实时
                var currentP = Common.AI2Grp["机油耗测量压力"];
                var currentH = Common.AI2Grp["机油耗测量液位"];

                this.lblCurrentPressure.Text = currentP.ToString();
                this.lblCurrentLiquidLevel.Text = currentH.ToString();
                this.lblCurrentWeight.Text = YHA27.Instance.Weight.ToString();

                // 计算差值 燃油质量流量计
                var massFlowCC = Common.AIgrp["燃油进油流量测量-L30"] - Common.AIgrp["燃油回油流量测量-L31"];
                if (massFlowCC == 0)
                {
                    this.lblMeasureReplenishment.Text = "0";
                }
                else
                {
                    this.lblMeasureReplenishment.Text = Math.Round(massFlowCC / MiddleData.instnce.EnginePower, 2).ToString();
                }

                // 燃油耗仪
                if (ET4500.Instance.fuelConsumption == 0)
                {
                    this.lblfuelConsumptionMeter.Text = "0";
                }
                else
                {
                    this.lblfuelConsumptionMeter.Text = Math.Round(ET4500.Instance.fuelConsumption / MiddleData.instnce.EnginePower, 2).ToString();
                }


                if (timesDiff > 0 && IsStartOilEngineMeasurement && IsStopOilEngineMeasurement)
                {
                    double BeginP = this.lblOilBeginP.Text.ToDouble();
                    if (currentP >= BeginP)
                    {
                        // todo 关泵动作

                        IsStopOilEngineMeasurement = false;
                        IsStartOilEngineMeasurement = false;

                        // 记录磅秤的差值
                        var beginWeight = this.lblBCWeight.Text.ToDouble();
                        var weightDiff_g = (beginWeight - YHA27.Instance.Weight) * 1000;

                        // 机油耗值
                        if (timesDiff > 0 && StopEnginePower > 0)
                        {
                            // 公式：机油耗(g/kW·h) = 重量差(g) / (功率(kW) × 时间(h))
                            var engineOilVal = Math.Round(weightDiff_g / (timesDiff * StopEnginePower), 3);
                            this.lblMeasureFuelConsumption.Text = engineOilVal.ToString();
                        }
                        else
                        {
                            this.lblMeasureFuelConsumption.Text = "0.000";
                        }


                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ucFuelConsumptionMeasurement_Load(object sender, EventArgs e)
        {
            // 添加设计时检查
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            this.timerEngineOilConsumption.Enabled = true;
            this.timerEngineOilConsumption.Start();
        }
    }
}

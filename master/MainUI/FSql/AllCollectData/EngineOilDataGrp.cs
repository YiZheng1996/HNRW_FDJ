using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class EngineOilDataGrp
    {

        //EngineOilGrp
        /// <summary>
        /// 流量计口后压力检测-P29
        /// </summary>
        public double FlowMeterRearPressureDetectP29 { get; set; }

        /// <summary>
        /// 机油箱出口后压力检测-P23
        /// </summary>
        public double OilTankOutletRearPressureDetectP23 { get; set; }

        /// <summary>
        /// 机油箱出口前压力检测-P22
        /// </summary>
        public double OilTankOutletFrontPressureDetectP22 { get; set; }

        /// <summary>
        /// 机油温度密码
        /// </summary>
        public double OilTemperaturePassword { get; set; }

        /// <summary>
        /// 机油温度实时PID
        /// </summary>
        public double OilTemperatureRealTimePID { get; set; }

        /// <summary>
        /// 待处理机油箱液位检测-L19
        /// </summary>
        public double PendingOilTankLevelDetectL19 { get; set; }

        /// <summary>
        /// 冷却器进口油温-T25
        /// </summary>
        public double CoolerInletOilTemperatureT25 { get; set; }

        /// <summary>
        /// 预供机油压力检测-P19
        /// </summary>
        public double PreSupplyOilPressureDetectP19 { get; set; }

        /// <summary>
        /// 前压力检测-P24
        /// </summary>
        public double FrontPressureDetectP24 { get; set; }

        /// <summary>
        /// 机油温度设置PID
        /// </summary>
        public double OilTemperatureSetPID { get; set; }

        /// <summary>
        /// 后压力检测-P25
        /// </summary>
        public double RearPressureDetectP25 { get; set; }

        /// <summary>
        /// 机油箱液位检测-L18
        /// </summary>
        public double OilTankLevelDetectL18 { get; set; }

        /// <summary>
        /// 机油箱温度检测-T23
        /// </summary>
        public double OilTankTemperatureDetectT23 { get; set; }

        /// <summary>
        /// 流量计口前压力检测-P28
        /// </summary>
        public double FlowMeterFrontPressureDetectP28 { get; set; }

        /// <summary>
        /// 前1压力检测-P26
        /// </summary>
        public double Front1PressureDetectP26 { get; set; }

        /// <summary>
        /// 后1压力检测-P27
        /// </summary>
        public double Rear1PressureDetectP27 { get; set; }

        /// <summary>
        /// 待处理机油箱温度检测-T24
        /// </summary>
        public double PendingOilTankTemperatureDetectT24 { get; set; }

        /// <summary>
        /// 机油温度PID上限值
        /// </summary>
        public double OilTemperaturePIDUpperLimit { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class WaterDataGrp
    {

        //WaterGrp
        /// <summary>
        /// 中冷水温度设置PID
        /// </summary>
        public double CoolWaterTemperatureSetPID { get; set; }

        /// <summary>
        /// 高温水过滤器前压力检测-P6
        /// </summary>
        public double HighTempWaterFilterFrontPressureDetectP6 { get; set; }

        /// <summary>
        /// 预热水箱液位检测
        /// </summary>
        public double PreHeatTankLevelDetect { get; set; }

        /// <summary>
        /// 中冷水过滤器前压力检测-P9
        /// </summary>
        public double CoolWaterFilterFrontPressureDetectP9 { get; set; }

        /// <summary>
        /// 中冷水过滤器后压力检测-P10
        /// </summary>
        public double CoolWaterFilterRearPressureDetectP10 { get; set; }

        /// <summary>
        /// 高温水温度密码
        /// </summary>
        public double HighTempWaterTemperaturePassword { get; set; }

        /// <summary>
        /// 高温水过滤器后压力检测-P7
        /// </summary>
        public double HighTempWaterFilterRearPressureDetectP7 { get; set; }

        /// <summary>
        /// 高温水温度设置PID
        /// </summary>
        public double HighTempWaterTemperatureSetPID { get; set; }

        /// <summary>
        /// 中冷水冷却器进口温度检测-T14
        /// </summary>
        public double CoolWaterCoolerInletTemperatureDetectT14 { get; set; }

        /// <summary>
        /// 高温水温度实时PID
        /// </summary>
        public double HighTempWaterTemperatureRealTimePID { get; set; }

        /// <summary>
        /// 预热水箱温度检测-T12
        /// </summary>
        public double PreHeatTankTemperatureDetectT12 { get; set; }

        /// <summary>
        /// 中冷水温度密码
        /// </summary>
        public double CoolWaterTemperaturePassword { get; set; }

        /// <summary>
        /// 中冷水温度实时PID
        /// </summary>
        public double CoolWaterTemperatureRealTimePID { get; set; }

        /// <summary>
        /// 高温水冷却器进口温度检测-T13
        /// </summary>
        public double HighTempWaterCoolerInletTemperatureDetectT13 { get; set; }

        /// <summary>
        /// 高温水温度PID上限值
        /// </summary>
        public double HighTempWaterTemperaturePIDUpperLimit { get; set; }

        /// <summary>
        /// 中冷水温度PID上限值
        /// </summary>
        public double CoolWaterTemperaturePIDUpperLimit { get; set; }

    }
}

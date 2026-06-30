using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class WaterDataGrp
    {
        [JsonProperty("Water_中冷水温度设置PID")]
        public double CoolWaterTemperatureSetPID { get; set; }

        [JsonProperty("Water_高温水过滤器前压力检测-P6")]
        public double HighTempWaterFilterFrontPressureDetectP6 { get; set; }

        [JsonProperty("Water_预热水箱液位检测")]
        public double PreHeatTankLevelDetect { get; set; }

        [JsonProperty("Water_中冷水过滤器前压力检测-P9")]
        public double CoolWaterFilterFrontPressureDetectP9 { get; set; }

        [JsonProperty("Water_中冷水过滤器后压力检测-P10")]
        public double CoolWaterFilterRearPressureDetectP10 { get; set; }

        [JsonProperty("Water_高温水温度密码")]
        public double HighTempWaterTemperaturePassword { get; set; }

        [JsonProperty("Water_高温水过滤器后压力检测-P7")]
        public double HighTempWaterFilterRearPressureDetectP7 { get; set; }

        [JsonProperty("Water_高温水温度设置PID")]
        public double HighTempWaterTemperatureSetPID { get; set; }

        [JsonProperty("Water_中冷水冷却器进口温度检测-T14")]
        public double CoolWaterCoolerInletTemperatureDetectT14 { get; set; }

        [JsonProperty("Water_高温水温度实时PID")]
        public double HighTempWaterTemperatureRealTimePID { get; set; }

        [JsonProperty("Water_预热水箱温度检测-T12")]
        public double PreHeatTankTemperatureDetectT12 { get; set; }

        [JsonProperty("Water_中冷水温度密码")]
        public double CoolWaterTemperaturePassword { get; set; }

        [JsonProperty("Water_中冷水温度实时PID")]
        public double CoolWaterTemperatureRealTimePID { get; set; }

        [JsonProperty("Water_高温水冷却器进口温度检测-T13")]
        public double HighTempWaterCoolerInletTemperatureDetectT13 { get; set; }
    }
}

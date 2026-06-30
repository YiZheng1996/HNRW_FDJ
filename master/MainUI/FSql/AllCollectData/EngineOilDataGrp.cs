using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class EngineOilDataGrp
    {
        [JsonProperty("Oil_流量计口后压力检测-P29")]
        public double FlowMeterRearPressureDetectP29 { get; set; }

        [JsonProperty("Oil_机油箱出口后压力检测-P23")]
        public double OilTankOutletRearPressureDetectP23 { get; set; }

        [JsonProperty("Oil_机油箱出口前压力检测-P22")]
        public double OilTankOutletFrontPressureDetectP22 { get; set; }

        [JsonProperty("Oil_机油温度密码")]
        public double OilTemperaturePassword { get; set; }

        [JsonProperty("Oil_机油温度实时PID")]
        public double OilTemperatureRealTimePID { get; set; }

        [JsonProperty("Oil_待处理机油箱液位检测-L19")]
        public double PendingOilTankLevelDetectL19 { get; set; }

        [JsonProperty("Oil_冷却器进口油温-T25")]
        public double CoolerInletOilTemperatureT25 { get; set; }

        [JsonProperty("Oil_预供机油压力检测-P19")]
        public double PreSupplyOilPressureDetectP19 { get; set; }

        [JsonProperty("Oil_前压力检测-P24")]
        public double FrontPressureDetectP24 { get; set; }

        [JsonProperty("Oil_机油温度设置PID")]
        public double OilTemperatureSetPID { get; set; }

        [JsonProperty("Oil_后压力检测-P25")]
        public double RearPressureDetectP25 { get; set; }

        [JsonProperty("Oil_机油箱液位检测-L18")]
        public double OilTankLevelDetectL18 { get; set; }

        [JsonProperty("Oil_机油箱温度检测-T23")]
        public double OilTankTemperatureDetectT23 { get; set; }

        [JsonProperty("Oil_流量计口前压力检测-P28")]
        public double FlowMeterFrontPressureDetectP28 { get; set; }

        [JsonProperty("Oil_前1压力检测-P26")]
        public double Front1PressureDetectP26 { get; set; }

        [JsonProperty("Oil_后1压力检测-P27")]
        public double Rear1PressureDetectP27 { get; set; }

        [JsonProperty("Oil_待处理机油箱温度检测-T24")]
        public double PendingOilTankTemperatureDetectT24 { get; set; }
    }
}

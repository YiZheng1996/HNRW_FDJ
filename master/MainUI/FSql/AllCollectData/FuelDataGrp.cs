using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class FuelDataGrp
    {
        [JsonProperty("Fuel_水阻箱温度检测")]
        public double WaterResistBoxTemperatureDetect { get; set; }

        [JsonProperty("Fuel_精滤器2前压力检测-P36")]
        public double FineFilter2FrontPressureDetectP36 { get; set; }

        [JsonProperty("Fuel_水阻箱极板位移检测")]
        public double WaterResistBoxPolarPlateDisplacementDetect { get; set; }

        [JsonProperty("Fuel_粗滤器2后压力检测-P33")]
        public double CoarseFilter2RearPressureDetectP33 { get; set; }

        [JsonProperty("Fuel_精滤器1前压力检测-P34")]
        public double FineFilter1FrontPressureDetectP34 { get; set; }

        [JsonProperty("Fuel_粗滤器1后压力检测-P31")]
        public double CoarseFilter1RearPressureDetectP31 { get; set; }

        [JsonProperty("Fuel_精滤器2后压力检测-P37")]
        public double FineFilter2RearPressureDetectP37 { get; set; }

        [JsonProperty("Fuel_粗滤器2前压力检测-P32")]
        public double CoarseFilter2FrontPressureDetectP32 { get; set; }

        [JsonProperty("Fuel_柴油箱液位检测-L29")]
        public double DieselTankLevelDetectL29 { get; set; }

        [JsonProperty("Fuel_精滤器1后压力检测-P35")]
        public double FineFilter1RearPressureDetectP35 { get; set; }

        [JsonProperty("Fuel_粗滤器1前压力检测-P30")]
        public double CoarseFilter1FrontPressureDetectP30 { get; set; }
    }
}

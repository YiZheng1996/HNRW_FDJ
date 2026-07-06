using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class AIDataGrp
    {
        [JsonProperty("AI_大气温度")]
        public double AT { get; set; }

        [JsonProperty("AI_大气压力")]
        public double AP { get; set; }

        [JsonProperty("AI_大气湿度")]
        public double AH { get; set; }

        [JsonProperty("AI_高温水流量测量-L3")]
        public double HWaterFlow { get; set; }

        [JsonProperty("AI_进气流量测量左")]
        public double InAirFlowLeft { get; set; }

        [JsonProperty("AI_中冷水流量测量-L8")]
        public double LWaterFlow { get; set; }

        [JsonProperty("AI_中冷水膨胀水箱液位检测")]
        public double LWaterBoxLevel { get; set; }

        [JsonProperty("AI_清洁油罐来油流量")]
        public double cleanOilFlow { get; set; }

        [JsonProperty("AI_机油流量")]
        public double EngineOilFlow { get; set; }

        [JsonProperty("AI_燃油回油流量测量-L31")]
        public double OilFlow_L31 { get; set; }

        [JsonProperty("AI_进气流量测量右")]
        public double InAirFlowRight { get; set; }

        [JsonProperty("AI_前增压器进气流量计前温度")]
        public double BeforeInAirTemp { get; set; }

        [JsonProperty("AI_后增压器进气流量计前温度")]
        public double AfterInAirTemp { get; set; }

        [JsonProperty("AI_厂房进气压力检测1")]
        public double FactoryAirPressureIn1 { get; set; }

        [JsonProperty("AI_厂房进气压力检测2")]
        public double FactoryAirPressureIn2 { get; set; }

        [JsonProperty("AI_L32")]
        public double L32 { get; set; }

        [JsonProperty("AI_燃油进油流量测量-L30")]
        public double OilFlowIn_L30 { get; set; }

        [JsonProperty("AI_高温水膨胀水箱液位检测")]
        public double HWaterBoxLevel { get; set; }
    }
}

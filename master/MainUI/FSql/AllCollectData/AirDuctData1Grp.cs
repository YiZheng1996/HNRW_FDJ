using Newtonsoft.Json;

namespace MainUI.FSql.AllCollectData
{
    /// <summary>
    /// 一号风道加热数据（模拟量 + 开关量）
    /// </summary>
    public class AirDuctData1Grp
    {
        [JsonProperty("AirDuct_一号显示出口温度")]
        public double Unit1OutletTempDisplay { get; set; }

        [JsonProperty("AirDuct_一号显示内腔温度1")]
        public double Unit1CavityTemp1Display { get; set; }

        [JsonProperty("AirDuct_一号显示内腔温度2")]
        public double Unit1CavityTemp2Display { get; set; }

        [JsonProperty("AirDuct_一号显示风机频率")]
        public double Unit1FanFrequencyDisplay { get; set; }

        [JsonProperty("AirDuct_一号设置出口温度")]
        public double Unit1OutletTempSet { get; set; }

        [JsonProperty("AirDuct_一号设置内腔温度")]
        public double Unit1CavityTempSet { get; set; }

        [JsonProperty("AirDuct_一号一键启动")]
        public bool Unit1OneKeyStart { get; set; }

        [JsonProperty("AirDuct_一号一键停止")]
        public bool Unit1OneKeyStop { get; set; }

        [JsonProperty("AirDuct_一号风机运行")]
        public bool Unit1FanRunning { get; set; }

        [JsonProperty("AirDuct_一号加热运行1")]
        public bool Unit1HeatRunning1 { get; set; }

        [JsonProperty("AirDuct_一号加热运行2")]
        public bool Unit1HeatRunning2 { get; set; }

        [JsonProperty("AirDuct_一号加热运行3")]
        public bool Unit1HeatRunning3 { get; set; }

        [JsonProperty("AirDuct_一号加热运行4")]
        public bool Unit1HeatRunning4 { get; set; }

        [JsonProperty("AirDuct_一号加热运行5")]
        public bool Unit1HeatRunning5 { get; set; }

        [JsonProperty("AirDuct_一号加热运行6")]
        public bool Unit1HeatRunning6 { get; set; }

        [JsonProperty("AirDuct_一号停止按下中间")]
        public bool Unit1StopPressedMiddle { get; set; }

        [JsonProperty("AirDuct_一号风机变频器故障")]
        public bool Unit1FanInverterFault { get; set; }

        [JsonProperty("AirDuct_一号风机冷却风扇过载")]
        public bool Unit1FanCoolingFanOverload { get; set; }

        [JsonProperty("AirDuct_一号出口超温")]
        public bool Unit1OutletOverTemp { get; set; }

        [JsonProperty("AirDuct_一号内腔保护1超温")]
        public bool Unit1CavityProtect1OverTemp { get; set; }

        [JsonProperty("AirDuct_一号内腔保护2超温")]
        public bool Unit1CavityProtect2OverTemp { get; set; }

        [JsonProperty("AirDuct_一号温控器通讯错误")]
        public bool Unit1TempControllerCommError { get; set; }

        [JsonProperty("AirDuct_一号变频器通讯错误")]
        public bool Unit1InverterCommError { get; set; }

        [JsonProperty("AirDuct_一号相序保护故障")]
        public bool Unit1PhaseSequenceFault { get; set; }
    }
}
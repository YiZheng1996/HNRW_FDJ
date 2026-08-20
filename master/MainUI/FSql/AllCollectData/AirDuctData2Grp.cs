using Newtonsoft.Json;

namespace MainUI.FSql.AllCollectData
{
    /// <summary>
    /// 二号风道加热数据（模拟量 + 开关量）
    /// </summary>
    public class AirDuctData2Grp
    {
        [JsonProperty("AirDuct_二号显示出口温度")]
        public double Unit2OutletTempDisplay { get; set; }

        [JsonProperty("AirDuct_二号显示内腔温度1")]
        public double Unit2CavityTemp1Display { get; set; }

        [JsonProperty("AirDuct_二号显示内腔温度2")]
        public double Unit2CavityTemp2Display { get; set; }

        [JsonProperty("AirDuct_二号显示风机频率")]
        public double Unit2FanFrequencyDisplay { get; set; }

        [JsonProperty("AirDuct_二号设置出口温度")]
        public double Unit2OutletTempSet { get; set; }

        [JsonProperty("AirDuct_二号设置内腔温度")]
        public double Unit2CavityTempSet { get; set; }

        [JsonProperty("AirDuct_二号一键启动")]
        public bool Unit2OneKeyStart { get; set; }

        [JsonProperty("AirDuct_二号一键停止")]
        public bool Unit2OneKeyStop { get; set; }

        [JsonProperty("AirDuct_二号风机运行")]
        public bool Unit2FanRunning { get; set; }

        [JsonProperty("AirDuct_二号加热运行1")]
        public bool Unit2HeatRunning1 { get; set; }

        [JsonProperty("AirDuct_二号加热运行2")]
        public bool Unit2HeatRunning2 { get; set; }

        [JsonProperty("AirDuct_二号加热运行3")]
        public bool Unit2HeatRunning3 { get; set; }

        [JsonProperty("AirDuct_二号加热运行4")]
        public bool Unit2HeatRunning4 { get; set; }

        [JsonProperty("AirDuct_二号加热运行5")]
        public bool Unit2HeatRunning5 { get; set; }

        [JsonProperty("AirDuct_二号加热运行6")]
        public bool Unit2HeatRunning6 { get; set; }

        [JsonProperty("AirDuct_二号停止按下中间")]
        public bool Unit2StopPressedMiddle { get; set; }

        [JsonProperty("AirDuct_二号风机变频器故障")]
        public bool Unit2FanInverterFault { get; set; }

        [JsonProperty("AirDuct_二号风机冷却风扇过载")]
        public bool Unit2FanCoolingFanOverload { get; set; }

        [JsonProperty("AirDuct_二号出口超温")]
        public bool Unit2OutletOverTemp { get; set; }

        [JsonProperty("AirDuct_二号内腔保护1超温")]
        public bool Unit2CavityProtect1OverTemp { get; set; }

        [JsonProperty("AirDuct_二号内腔保护2超温")]
        public bool Unit2CavityProtect2OverTemp { get; set; }

        [JsonProperty("AirDuct_二号温控器通讯错误")]
        public bool Unit2TempControllerCommError { get; set; }

        [JsonProperty("AirDuct_二号变频器通讯错误")]
        public bool Unit2InverterCommError { get; set; }

        [JsonProperty("AirDuct_二号相序保护故障")]
        public bool Unit2PhaseSequenceFault { get; set; }
    }
}
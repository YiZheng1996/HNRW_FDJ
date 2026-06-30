using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class GD350_1Data
    {
        [JsonProperty("Inverter_输出功率检测")]
        public double OutputPowerDetect { get; set; }

        [JsonProperty("Inverter_启动_停止")]
        public double StartStop { get; set; }

        [JsonProperty("Inverter_运行超时时间")]
        public double RunTimeout { get; set; }

        [JsonProperty("Inverter_输出电流检测")]
        public double OutputCurrentDetect { get; set; }

        [JsonProperty("Inverter_启动柜运行状态")]
        public double CabinetRunningStatus { get; set; }

        [JsonProperty("Inverter_就绪")]
        public double Ready { get; set; }

        [JsonProperty("Inverter_故障代码")]
        public double FaultCode { get; set; }

        [JsonProperty("Inverter_母线电压检测")]
        public double BusVoltageDetect { get; set; }

        [JsonProperty("Inverter_启动柜频率设定")]
        public double CabinetFrequencySet { get; set; }

        [JsonProperty("Inverter_输出电压检测")]
        public double OutputVoltageDetect { get; set; }

        [JsonProperty("Inverter_启动柜启动")]
        public double CabinetStart { get; set; }

        [JsonProperty("Inverter_运行状态")]
        public double RunningStatus { get; set; }

        [JsonProperty("Inverter_运行频率")]
        public double RunningFrequency { get; set; }
    }
}

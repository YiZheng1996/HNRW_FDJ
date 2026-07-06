using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class ThreePhaseElectricData
    {
        [JsonProperty("Electric_有功功率")]
        public double ActivePower { get; set; }

        [JsonProperty("Electric_Ic")]
        public double CurrentIc { get; set; }

        [JsonProperty("Electric_电流")]
        public double TotalCurrent { get; set; }

        [JsonProperty("Electric_Ib")]
        public double CurrentIb { get; set; }

        [JsonProperty("Electric_Uvw")]
        public double VoltageUvw { get; set; }

        [JsonProperty("Electric_电压")]
        public double TotalVoltage { get; set; }

        [JsonProperty("Electric_Ia")]
        public double CurrentIa { get; set; }

        [JsonProperty("Electric_Uuv")]
        public double VoltageUuv { get; set; }

        [JsonProperty("Electric_Uwu")]
        public double VoltageUwu { get; set; }
    }
}

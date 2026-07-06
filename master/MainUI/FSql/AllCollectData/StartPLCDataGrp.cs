using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class StartPLCDataGrp
    {
        [JsonProperty("StartPLC_FaultReset")]
        public bool FaultReset { get; set; }

        [JsonProperty("StartPLC_变频器运行中")]
        public bool InverterRunning { get; set; }

        [JsonProperty("StartPLC_变频器输出检测")]
        public bool InverterOutputDetect { get; set; }

        [JsonProperty("StartPLC_变频器故障")]
        public bool InverterFault { get; set; }

        [JsonProperty("StartPLC_Auto")]
        public bool Auto { get; set; }

        [JsonProperty("StartPLC_变频器输出合分闸")]
        public bool InverterOutputSwitch { get; set; }

        [JsonProperty("StartPLC_Scram")]
        public bool Scram { get; set; }

        [JsonProperty("StartPLC_前门检测")]
        public bool FrontDoorDetect { get; set; }

        [JsonProperty("StartPLC_后门检测")]
        public bool RearDoorDetect { get; set; }
    }
}

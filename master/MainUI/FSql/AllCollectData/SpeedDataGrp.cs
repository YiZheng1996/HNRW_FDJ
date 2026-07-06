using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class SpeedDataGrp
    {
        [JsonProperty("Speed_转速3")]
        public double Speed3 { get; set; }

        [JsonProperty("Speed_每转感应点数")]
        public double PulsesPerRevolution { get; set; }

        [JsonProperty("Speed_转速1")]
        public double Speed1 { get; set; }

        [JsonProperty("Speed_转速2")]
        public double Speed2 { get; set; }
    }
}

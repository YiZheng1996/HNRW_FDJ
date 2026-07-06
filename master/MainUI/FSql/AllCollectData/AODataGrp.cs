using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class AODataGrp
    {
        [JsonProperty("AO_排气风道右调节阀控制")]
        public double AirOutChannelValve { get; set; }

        [JsonProperty("AO_设置发动机最低转速")]
        public double EngineLowSpeed { get; set; }

        [JsonProperty("AO_水泵出口电动调节阀控制-18")]
        public double WaterOutValve_18 { get; set; }

        [JsonProperty("AO_发动机油门调节")]
        public double EngineOilValve { get; set; }

        [JsonProperty("AO_排气风道左调节阀控制")]
        public double AirOutFlowValveLeft { get; set; }

        [JsonProperty("AO_励磁调节")]
        public double Excitation { get; set; }

        [JsonProperty("AO_燃油泵旁路1电动调节阀控制-194")]
        public double AO_OilBy_Pass1Valve_194 { get; set; }

        [JsonProperty("AO_进气风道左调节阀控制")]
        public double AirInFlowleftValve { get; set; }

        [JsonProperty("AO_燃油泵1电动调节阀控制-170")]
        public double Oil1Vlave_170 { get; set; }

        [JsonProperty("AO_进气风道右调节阀控制")]
        public double AirFlowRightValve { get; set; }

        [JsonProperty("AO_水阻箱进水电动调节阀")]
        public double WaterResistanceBoxValve { get; set; }
    }
}

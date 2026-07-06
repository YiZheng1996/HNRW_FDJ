using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.Model
{
    public class BaseDataGrp
    {
        /// <summary>
        /// 4 柴油机转速
        /// </summary>
        [JsonProperty("Base_EngineSpeed")]
        public double RPM { get; set; }

        /// <summary>
        /// 5 柴油机有效扭矩
        /// </summary>
        [JsonProperty("Base_EngineTorque")]
        public double Torque { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        [JsonProperty("Base_Weight")]
        public double Weight { get; set; }

        /// <summary>
        /// 6 柴油机有效功率
        /// </summary>
        [JsonProperty("Base_EnginePower")]
        public double Power { get; set; }
    }
}

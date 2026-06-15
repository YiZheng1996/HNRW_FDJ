using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class StartPLCDataGrp
    {
        /// <summary>
        /// 故障复位
        /// </summary>
        public bool FaultReset { get; set; }

        /// <summary>
        /// 变频器运行中
        /// </summary>
        public bool InverterRunning { get; set; }

        /// <summary>
        /// 变频器输出检测
        /// </summary>
        public bool InverterOutputDetect { get; set; }

        /// <summary>
        /// 变频器故障
        /// </summary>
        public bool InverterFault { get; set; }

        /// <summary>
        /// 自动模式
        /// </summary>
        public bool Auto { get; set; }

        /// <summary>
        /// 变频器输出合分闸
        /// </summary>
        public bool InverterOutputSwitch { get; set; }

        /// <summary>
        /// 急停
        /// </summary>
        public bool Scram { get; set; }

        /// <summary>
        /// 前门检测
        /// </summary>
        public bool FrontDoorDetect { get; set; }

        /// <summary>
        /// 后门检测
        /// </summary>
        public bool RearDoorDetect { get; set; }
    }
}

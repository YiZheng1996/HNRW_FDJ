using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Fault.Model
{
    public enum FaultTypeEnum
    {
        /// <summary>
        /// 通讯
        /// </summary>
        communication,
        /// <summary>
        /// OPC检测故障
        /// </summary>
        opcDetection,
        /// <summary>
        /// 程序监控故障（逻辑）
        /// </summary>
        calculate,
        /// <summary>
        /// ECM故障
        /// </summary>
        ecm,
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Fault.Model
{
    /// <summary>
    /// 故障类型
    /// </summary>
    public enum WarnTypeEnum
    {
        /// <summary>
        /// 无故障
        /// </summary>
        None,
        /// <summary>
        /// 报警
        /// </summary>
        Alarm,
        /// <summary>
        /// 降载
        /// </summary>
        Shedding,
        /// <summary>
        /// 发动机停机
        /// </summary>
        Stop,
        /// <summary>
        /// 只显示
        /// </summary>
        Tip,
    }
}

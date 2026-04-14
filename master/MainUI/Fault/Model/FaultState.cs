using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Fault.Model
{
    /// <summary>
    /// 故障状态类
    /// </summary>
    public class FaultState
    {
        /// <summary>
        /// 故障名称（现于点位名称一致）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 故障描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 提示报警故障（不蜂鸣）
        /// </summary>
        public bool TipConditionMet { get; set; }
        /// <summary>
        /// 报警故障
        /// </summary>
        public bool AlarmConditionMet { get; set; }
        /// <summary>
        /// 降载故障
        /// </summary>
        public bool SheddingConditionMet { get; set; }
        /// <summary>
        /// 停机故障
        /// </summary>
        public bool StopConditionMet { get; set; }
        /// <summary>
        /// 报警故障开始时间
        /// </summary>
        public DateTime AlarmStartTime { get; set; }
        /// <summary>
        /// 提示故障开始时间
        /// </summary>
        public DateTime TipStartTime { get; set; }
        /// <summary>
        /// 降载故障开始时间
        /// </summary>
        public DateTime SheddingStartTime { get; set; }
        /// <summary>
        /// 停机故障开始时间
        /// </summary>
        public DateTime StopStartTime { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public WarnTypeEnum CurrentActiveFault { get; set; } = WarnTypeEnum.None;

        /// <summary>
        /// 故障类型
        /// </summary>
        public FaultTypeEnum FaultType { get; set; } = FaultTypeEnum.communication;

        /// <summary>
        /// 绑定控件
        /// </summary>
        public Control BindControl { get; set; }
    }
}

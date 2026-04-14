using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Fault.Model
{
    /// <summary>
    /// 方法委托
    /// </summary>
    public class FaultCondition
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 报警委托
        /// </summary>
        public Func<SensorData, bool> CheckAlarm { get; set; }
        public double AlarmDuration { get; set; } = 0; // 默认立即触发

        /// <summary>
        /// 降载委托
        /// </summary>
        public Func<SensorData, bool> CheckShedding { get; set; }
        public double SheddingDuration { get; set; } = 0;

        /// <summary>
        /// 停机委托
        /// </summary>
        public Func<SensorData, bool> CheckStop { get; set; }
        public double StopDuration { get; set; } = 0;
    }

}

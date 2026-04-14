using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using FreeSql.DataAnnotations;

namespace MainUI.FSql
{
    [Table(Name = "fault_events")]
    public class FaultEvent
    {
        /// <summary>
        /// 故障记录唯一标识（主键）
        /// </summary>
        [Column(Name = "id", IsPrimary = true)]
        public string Id { get; set; }

        /// <summary>
        /// 故障代码/类型
        /// </summary>
        [Column(Name = "fault_code")]
        public string FaultCode { get; set; }

        /// <summary>
        /// 故障发生时间
        /// </summary>
        [Column(Name = "occur_time")]
        public DateTime OccurTime { get; set; }

        /// <summary>
        /// 故障复位/恢复时间（为空表示未恢复）
        /// </summary>
        [Column(Name = "reset_time")]
        public DateTime? ResetTime { get; set; }

        /// <summary>
        /// 故障状态：0-未处理，1-已处理
        /// </summary>
        [Column(Name = "status")]
        public int Status { get; set; }

        /// <summary>
        /// 故障详细描述
        /// </summary>
        [Column(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// 故障严重程度：1-报警故障，2-降载故障，3-停机故障
        /// </summary>
        [Column(Name = "severity")]
        public int Severity { get; set; }

        /// <summary>
        /// 故障类型（0：communication  1.opcDetection  2.calculate  3.ecm）
        /// </summary>
        [Column(Name = "fault_type")]
        public int FaultType { get; set; }
    }
}

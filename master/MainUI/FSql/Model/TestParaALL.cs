using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;

namespace MainUI.FSql
{
    /// <summary>
    /// 试验数据记录(所有)
    /// </summary>
    [Table (Name = "TestParaALL")]
    public class TestParaALL
    {
        /// <summary>
        /// gid
        /// </summary>
        [Column(Name = "gid", IsPrimary = true)]
        public string gid { get; set; }

        /// <summary>
        /// 主表id
        /// </summary>
        public string mgid { get; set; }

        /// <summary>
        /// 记录号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 记录名称
        /// </summary>
        public string RecordName { get; set; }
        /// <summary>
        /// 试验类型
        /// </summary>
        public string TestName { get; set; }
        /// <summary>
        /// 试验阶段
        /// </summary>
        public string TestStage { get; set; }
        /// <summary>
        /// 试验周期
        /// </summary>
        public string TestCycle { get; set; }
        /// <summary>
        /// 试验循环节点
        /// </summary>
        public string TestStep { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public string DataTime { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// 小时数
        /// </summary>
        public double HourNum { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecordDataTime { get; set; }

        /// <summary>
        /// 所有测试数据
        /// </summary>
        [Column(DbType = "TEXT")]
        public string MonitorData { get; set; }

    }
}

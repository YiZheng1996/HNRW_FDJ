using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.Model
{
    /// <summary>
    /// pym
    /// </summary>
    public class TestParaAllData
    {
        /// <summary>
        /// gid
        /// </summary>
        [Column(Name = "gid", IsPrimary = true)]
        public string gid { get; set; }

        /// <summary>
        /// 记录号
        /// </summary>
        public double Index { get; set; }

        /// <summary>
        /// 柴油机型号
        /// </summary>
        public string DieselEngineModel { get; set; }

        /// <summary>
        /// 柴油机编号
        /// </summary>
        public string DieselEngineNo { get; set; }

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
        /// 操作人员
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 所有测试数据
        /// </summary>
        [Column(DbType = "TEXT")]
        public string MonitorData { get; set; }
    }
}

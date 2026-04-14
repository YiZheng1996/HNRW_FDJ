using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.Model
{
    /// <summary>
    /// 全程记录数据主表
    /// </summary>
    [Table(Name = "TestParaALLMain")]
    public class TestParaALLMain
    {
        /// <summary>
        /// gid
        /// </summary>
        [Column(Name = "gid", IsPrimary = true)]
        public string gid { get; set; }

        /// <summary>
        /// 柴油机型号
        /// </summary>
        public string DieselEngineModel { get; set; }

        /// <summary>
        /// 柴油机编号
        /// </summary>
        public string DieselEngineNo { get; set; }

        /// <summary>
        /// 试验类型
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// 操作人员
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 开始记录时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束记录时间
        /// </summary>
        public DateTime EndTime { get; set; }

    }
}

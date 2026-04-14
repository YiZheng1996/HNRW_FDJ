using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;

namespace MainUI.FSql
{
    /// <summary>
    /// 试验启机 数据记录
    /// </summary>
    [Table (Name = "StartupTestPara")]
    public class StartupTestPara
    {
        /// <summary>
        /// gid
        /// </summary>
        [Column(Name = "gid", IsPrimary = true)]
        public string gid { get; set; }

        /// <summary>
        /// 记录号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 试验类型
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecordDataTime { get; set; }

        /// <summary>
        /// 柴油机转速
        /// </summary>
        public double RPM { get; set; }

        /// <summary>
        /// 柴油机有效扭矩
        /// </summary>
        public double Torque { get; set; }

        /// <summary>
        /// 柴油机有效功率
        /// </summary>
        public double Power { get; set; }

        /// <summary>
        /// 励磁电压
        /// </summary>
        public double ExcitationVoltage { get; set; }

        /// <summary>
        /// 励磁电流
        /// </summary>
        public double ExcitationCurrent { get; set; }

        /// <summary>
        /// 励磁电流设置
        /// </summary>
        public double ExcitationCurrentSet { get; set; }

        /// <summary>
        /// 变频器转速
        /// </summary>
        public double InvertRPM { get; set; }

        /// <summary>
        /// 变频器电压
        /// </summary>
        public double InvertVoltage { get; set; }

        /// <summary>
        /// 变频器电流
        /// </summary>
        public double InvertCurrent { get; set; }

        /// <summary>
        /// 变频器功率
        /// </summary>
        public double InvertPower { get; set; }

        /// <summary>
        /// 变频器故障代码
        /// </summary>
        public int InvertFaultCode { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.Configuration;
using System.Windows.Forms;
using System.ComponentModel;

namespace MainUI.Config
{
    public class ParaConfig : IniConfig
    {
        public ParaConfig()
          : base(Application.StartupPath + "\\config\\Para.ini")
        {
        }
        public ParaConfig(string sectionName)
            : base(Application.StartupPath + "\\config\\Para.ini")
        {
            this.SetSectionName(sectionName);
            Load();
        }
        /// <summary>
        /// 报表名称
        /// </summary>
        [IniKeyName("报表名称")]
        public string RptFile { get; set; }

        /// <summary>
        /// 标定功率
        /// </summary>
        [IniKeyName("标定功率")]
        public int RatedPower { get; set; }

        /// <summary>
        /// 标定转速
        /// </summary>
        [IniKeyName("标定转速")]
        public int RatedSpeed { get; set; }

        /// <summary>
        /// 标定扭矩
        /// </summary>
        [IniKeyName("标定扭矩")]
        public int RatedTorque { get; set; }

        /// <summary>
        /// 飞轮盘2齿数
        /// </summary>
        [IniKeyName("飞轮盘1齿数")]
        public int NumberofTeeth1 { get; set; } = 0;

        /// <summary>
        /// 飞轮盘2齿数
        /// </summary>
        [IniKeyName("飞轮盘2齿数")]
        public int NumberofTeeth2 { get; set; } = 0;

        /// <summary>
        /// 此节点扭矩分段(增加/减少)时间
        /// </summary>
        public int IntervalTime { get; set; }

        /// <summary>
        /// 此节点扭矩分段(增加/减少)比例
        /// </summary>
        public int TorqueChangeMultiple { get; set; }

        /// <summary>
        /// 此节点转速分段(增加/减少)比例
        /// </summary>
        public int RPMChangeMultiple { get; set; }

    }
}

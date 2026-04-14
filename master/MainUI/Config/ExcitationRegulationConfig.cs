using System;
using System.Collections.Generic;
using System.Text;
using RW.Configuration;
using System.Windows.Forms;

namespace MainUI.Config
{
    /// <summary>
    /// 故障参数
    /// </summary>
    public class ExcitationRegulationConfig : IniConfig
    {
        public ExcitationRegulationConfig(string model)
          : base(Application.StartupPath + $"\\config\\{model}ExcitationConfig.ini")
        {
            this.SetSectionName("ExcitationConfig");
            this.Load();
        }

        /// <summary>
        /// 一阶段电流目标励磁差值
        /// </summary>
        [IniKeyName("一阶段电流目标励磁差值")]
        public double Phase1Current { get; set; }

        /// <summary>
        /// 一阶段调节百分比1
        /// </summary>
        [IniKeyName("一阶段调节百分比1")]
        public double Phase1Rise1 { get; set; }

        /// <summary>
        /// 一阶段调节百分比2
        /// </summary>
        [IniKeyName("一阶段调节百分比2")]
        public double Phase1Rise2 { get; set; }

        /// <summary>
        /// 一阶段调节百分比3
        /// </summary>
        [IniKeyName("一阶段调节百分比3")]
        public double Phase1Rise3 { get; set; }

        /// <summary>
        /// 一阶段调节百分比4
        /// </summary>
        [IniKeyName("一阶段调节百分比4")]
        public double Phase1Rise4 { get; set; }

        /// <summary>
        /// 一阶段调节百分比5
        /// </summary>
        [IniKeyName("一阶段调节百分比5")]
        public double Phase1Rise5 { get; set; }


        /// <summary>
        /// 二阶段电流目标励磁差值
        /// </summary>
        [IniKeyName("二阶段电流目标励磁差值")]
        public double Phase2Current { get; set; }

        /// <summary>
        /// 二阶段调节百分比1
        /// </summary>
        [IniKeyName("二阶段调节百分比1")]
        public double Phase2Rise1 { get; set; }

        /// <summary>
        /// 二阶段调节百分比2
        /// </summary>
        [IniKeyName("二阶段调节百分比2")]
        public double Phase2Rise2 { get; set; }

        /// <summary>
        /// 二阶段调节百分比3
        /// </summary>
        [IniKeyName("二阶段调节百分比3")]
        public double Phase2Rise3 { get; set; }

        /// <summary>
        /// 二阶段调节百分比4
        /// </summary>
        [IniKeyName("二阶段调节百分比4")]
        public double Phase2Rise4 { get; set; }

        /// <summary>
        /// 二阶段调节百分比5
        /// </summary>
        [IniKeyName("二阶段调节百分比5")]
        public double Phase2Rise5 { get; set; }

        /// <summary>
        /// 二阶段调节百分比6
        /// </summary>
        [IniKeyName("二阶段调节百分比6")]
        public double Phase2Rise6 { get; set; }

        /// <summary>
        /// 三阶段调节百分比1
        /// </summary>
        [IniKeyName("三阶段调节百分比1")]
        public double Phase3Rise1 { get; set; }

        /// <summary>
        /// 三阶段调节百分比2
        /// </summary>
        [IniKeyName("三阶段调节百分比2")]
        public double Phase3Rise2 { get; set; }

        /// <summary>
        /// 三阶段调节百分比3
        /// </summary>
        [IniKeyName("三阶段调节百分比3")]
        public double Phase3Rise3 { get; set; }
    }

}

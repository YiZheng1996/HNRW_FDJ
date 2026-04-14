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
    /// <summary>
    /// 耐久试验流程参数配置
    /// </summary>
    public class Test360hConfig : IniConfig
    {
        public Test360hConfig()
          : base(Application.StartupPath + "\\config\\DurabilityTestConfig.ini")
        {

        }

        public Test360hConfig(string model)
            : base(Application.StartupPath + $"\\config\\{model}.ini")
        {
            this.SetSectionName("耐久试验");
            Load();
        }

        /// <summary>
        /// 360小时流程参数
        /// </summary>
        public List<DurabilityData> DurabilityDatas { get; set; } = new List<DurabilityData>();

    }

    public class DurabilityData
    {
        /// <summary>
        /// 序号
        /// </summary>
        [IniKeyName("序号")]
        public int Index { get; set; }

        /// <summary>
        /// 阶段名称
        /// </summary>
        [IniKeyName("阶段名称")]
        public string PhaseName { get; set; }

        /// <summary>
        /// 周期号
        /// </summary>
        [DisplayName("所属周期号")]
        public string CycleName { get; set; }

        /// <summary>
        /// 循环代码名 节点名（标定功率/超负荷）/ (A/B/C)
        /// </summary>
        [DisplayName("循环代码名")]
        public string NodeName { get; set; }

        /// <summary>
        /// 天数
        /// </summary>
        [IniKeyName("天数")]
        public string DayNum { get; set; }
    }

}

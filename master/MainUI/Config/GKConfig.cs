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
    /// 工况表程参数配置
    /// </summary>
    public class GKConfig : IniConfig
    {
        public GKConfig(string tag)
          : base(Application.StartupPath + "\\config\\GKConfig.ini")
        {
            this.SetSectionName(tag + "_GKConfig");
            Load();
        }

        /// <summary>
        /// 工况列表
        /// </summary>
        public List<GKData> DurabilityDatas { get; set; } = new List<GKData>();

    }

    public class GKData
    {
        /// <summary>
        /// 序号
        /// </summary>
        [IniKeyName("序号")]
        public int Index { get; set; }

        /// <summary>
        /// 工况编号
        /// </summary>
        [IniKeyName("工况编号")]
        public string GKNo { get; set; }

        /// <summary>
        /// 转速值
        /// </summary>
        [IniKeyName("转速值")]
        public double Speed { get; set; }

        /// <summary>
        /// 励磁电流
        /// </summary>
        [IniKeyName("励磁电流")]
        public double ExcitationCurrent { get; set; }

        /// <summary>
        /// 扭矩值 N.m
        /// </summary>
        [IniKeyName("扭矩值")]
        public double Torque { get; set; }

        /// <summary>
        /// 功率值 kW
        /// </summary>
        [IniKeyName("功率值")]
        public double Power { get; set; }
    }

}

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
    /// 配方参数
    /// </summary>
    public class PubConfig : IniConfig
    {
        public PubConfig()
          : base(Application.StartupPath + "\\config\\Pub.ini")
        {
        }
        public PubConfig(string sectionName)
            : base(Application.StartupPath + "\\config\\Pub.ini")
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
        /// 图表参数列表
        /// </summary>
        [IniKeyName("图表参数")]
        public List<PubPara> PubParaList { get; set; } = new List<PubPara>();


        public class PubPara
        {
            /// <summary>
            /// 励磁电流默认值
            /// </summary>
            public int DefaultExcitationCurrent { get; set; }
            /// <summary>
            /// 励磁电流最小值
            /// </summary>
            public int MinExcitationCurrent { get; set; }
            /// <summary>
            /// 励磁电流最大值
            /// </summary>
            public int MaxExcitationCurrent { get; set; }

            /// <summary>
            /// 转速设置默认值
            /// </summary>
            public int DefaultRotationSpeed { get; set; } = 400;
            /// <summary>
            /// 转速设置最小值
            /// </summary>
            public int MinRotationSpeed { get; set; } = 400;
            /// <summary>
            /// 转速设置最大值
            /// </summary>
            public int MaxRotationSpeed { get; set; } = 1100;

            /// <summary>
            /// 机油温度默认值
            /// </summary>
            public int DefaultOilTemperature { get; set; }
            /// <summary>
            /// 机油温度最小值
            /// </summary>
            public int MinOilTemperature { get; set; }
            /// <summary>
            /// 机油温度最大值
            /// </summary>
            public int MaxOilTemperature { get; set; }

            /// <summary>
            /// 高温水温度默认值
            /// </summary>
            public int DefaultHighTemperatureWater { get; set; }
            /// <summary>
            /// 高温水温度最小值
            /// </summary>
            public int MinHighTemperatureWater { get; set; }
            /// <summary>
            /// 高温水温度最大值
            /// </summary>
            public int MaxHighTemperatureWater { get; set; }

            /// <summary>
            /// 中冷水温度默认值
            /// </summary>
            public int DefaultMediumColdWaterTemperature { get; set; }
            /// <summary>
            /// 中冷水温度最小值
            /// </summary>
            public int MinMediumColdWaterTemperature { get; set; }
            /// <summary>
            /// 中冷水温度最大值
            /// </summary>
            public int MaxMediumColdWaterTemperature { get; set; }

            /// <summary>
            /// 水泵出口流量默认值
            /// </summary>
            public int DefaultWaterPumpOutletFlow { get; set; }
            /// <summary>
            /// 水泵出口流量最小值
            /// </summary>
            public int MinWaterPumpOutletFlow { get; set; }
            /// <summary>
            /// 水泵出口流量最大值
            /// </summary>
            public int MaxWaterPumpOutletFlow { get; set; }

            /// <summary>
            /// 燃油泵1流量默认值
            /// </summary>
            public int DefaultFuelPump1Flow { get; set; }
            /// <summary>
            /// 燃油泵1流量最小值
            /// </summary>
            public int MinFuelPump1Flow { get; set; }
            /// <summary>
            /// 燃油泵1流量最大值
            /// </summary>
            public int MaxFuelPump1Flow { get; set; }

            /// <summary>
            /// 燃油泵2流量默认值
            /// </summary>
            public int DefaultFuelPump2Flow { get; set; }
            /// <summary>
            /// 燃油泵2流量最小值
            /// </summary>
            public int MinFuelPump2Flow { get; set; }
            /// <summary>
            /// 燃油泵2流量最大值
            /// </summary>
            public int MaxFuelPump2Flow { get; set; }

            /// <summary>
            /// 进气风道右流量默认值
            /// </summary>
            public int DefaultIntakeDuctRightFlow { get; set; }
            /// <summary>
            /// 进气风道右流量最小值
            /// </summary>
            public int MinIntakeDuctRightFlow { get; set; }
            /// <summary>
            /// 进气风道右流量最大值
            /// </summary>
            public int MaxIntakeDuctRightFlow { get; set; }

            /// <summary>
            /// 进气风道左流量默认值
            /// </summary>
            public int DefaultIntakeDuctLeftFlow { get; set; }
            /// <summary>
            /// 进气风道左流量最小值
            /// </summary>
            public int MinIntakeDuctLeftFlow { get; set; }
            /// <summary>
            /// 进气风道左流量最大值
            /// </summary>
            public int MaxIntakeDuctLeftFlow { get; set; }

            /// <summary>
            /// 排气风道右默认值
            /// </summary>
            public int DefaultExhaustDuctRight { get; set; }
            /// <summary>
            /// 排气风道右最小值
            /// </summary>
            public int MinExhaustDuctRight { get; set; }
            /// <summary>
            /// 排气风道右最大值
            /// </summary>
            public int MaxExhaustDuctRight { get; set; }

            /// <summary>
            /// 排气风道左默认值
            /// </summary>
            public int DefaultExhaustDuctLeft { get; set; }
            /// <summary>
            /// 排气风道左最小值
            /// </summary>
            public int MinExhaustDuctLeft { get; set; }
            /// <summary>
            /// 排气风道左最大值
            /// </summary>
            public int MaxExhaustDuctLeft { get; set; }

            /// <summary>
            /// 水阻箱进水默认值
            /// </summary>
            public int DefaultWaterResistanceTankInlet { get; set; }
            /// <summary>
            /// 水阻箱进水最小值
            /// </summary>
            public int MinWaterResistanceTankInlet { get; set; }
            /// <summary>
            /// 水阻箱进水最大值
            /// </summary>
            public int MaxWaterResistanceTankInlet { get; set; }

        }
    }
        
}

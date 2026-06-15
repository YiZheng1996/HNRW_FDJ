using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class ThreePhaseElectricData
    {

        //ThreePhaseElectric
        /// <summary>
        /// 有功功率
        /// </summary>
        public double ActivePower { get; set; }

        /// <summary>
        /// Ic
        /// </summary>
        public double CurrentIc { get; set; }

        /// <summary>
        /// 电流
        /// </summary>
        public double TotalCurrent { get; set; }

        /// <summary>
        /// Ib
        /// </summary>
        public double CurrentIb { get; set; }

        /// <summary>
        /// Uvw
        /// </summary>
        public double VoltageUvw { get; set; }

        /// <summary>
        /// 电压
        /// </summary>
        public double TotalVoltage { get; set; }

        /// <summary>
        /// Ia
        /// </summary>
        public double CurrentIa { get; set; }

        /// <summary>
        /// Uuv
        /// </summary>
        public double VoltageUuv { get; set; }

        /// <summary>
        /// Uwu
        /// </summary>
        public double VoltageUwu { get; set; }

        /// <summary>
        /// 无功功率
        /// </summary>
        public double ReactivePower { get; set; }

        /// <summary>
        /// 视在功率
        /// </summary>
        public double ApparentPower { get; set; }

        /// <summary>
        /// 频率
        /// </summary>
        public double Frequency { get; set; }

    }
}

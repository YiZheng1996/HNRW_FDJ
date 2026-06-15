using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.Model
{
    public class BaseDataGrp
    {
        /// <summary>
        /// 4 柴油机转速
        /// </summary>
        public double RPM { get; set; }

        /// <summary>
        /// 5 柴油机有效扭矩
        /// </summary>
        public double Torque { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// 6 柴油机有效功率
        /// </summary>
        public double Power { get; set; }
    }
}

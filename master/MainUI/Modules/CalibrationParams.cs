using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Modules
{
    // 校准参数类
    public class CalibrationParams
    {
        public double Zero { get; set; }
        public double Gain { get; set; }

        public CalibrationParams()
        {
            Zero = 0;
            Gain = 1;
        }

        public CalibrationParams(double zero, double gain)
        {
            Zero = zero;
            Gain = gain;
        }
    }
}

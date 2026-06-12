using MainUI.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Helper
{
    /// <summary>
    /// 转速/扭矩百分比结果
    /// </summary>
    public struct GKPercent
    {
        public double SpeedPct;
        public double TorquePct;

        public GKPercent(double speedPct, double torquePct)
        {
            SpeedPct = speedPct;
            TorquePct = torquePct;
        }
    }

    /// <summary>
    /// GKData 百分比换算扩展方法
    /// 工况表(ucGKParams)和360h循环代码(ucTestParams)共用
    /// </summary>
    public static class GKDataExtensions
    {
        /// <summary>
        /// 根据型号标定值(ParaConfig.RatedSpeed/RatedTorque)，
        /// 计算该工况的转速%/扭矩%；标定值为0时对应项返回0
        /// </summary>
        public static GKPercent ToPercent(this GKData data, ParaConfig paraConfig)
        {
            double speedPct = paraConfig.RatedSpeed > 0
                ? data.Speed / paraConfig.RatedSpeed * 100
                : 0;

            double torquePct = paraConfig.RatedTorque > 0
                ? data.Torque / paraConfig.RatedTorque * 100
                : 0;

            return new GKPercent(Math.Round(speedPct), Math.Round(torquePct));
        }
    }

}

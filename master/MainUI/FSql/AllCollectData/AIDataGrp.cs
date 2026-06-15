using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class AIDataGrp
    {
        /// <summary>
        /// 1 环境温度
        /// </summary>
        public double AT { get; set; }

        /// <summary>
        /// 2 大气压力
        /// </summary>
        public double AP { get; set; }

        /// <summary>
        /// 3 空气湿度
        /// </summary>
        public double AH { get; set; }

        /// <summary>
        /// 高温水流量测量-L3
        /// </summary>
        public double HWaterFlow { get; set; }



        /// <summary>
        /// 进气流量测量左
        /// </summary>
        public double InAirFlowLeft { get; set; }

        /// <summary>
        /// 中冷水流量测量-L8
        /// </summary>
        public double LWaterFlow { get; set; }


        /// <summary>
        /// 中冷水膨胀水箱液位检测
        /// </summary>
        public double LWaterBoxLevel { get; set; }

        /// <summary>
        /// 清洁油罐来油流量
        /// </summary>
        public double cleanOilFlow { get; set; }

        /// <summary>
        /// 机油流量
        /// </summary>
        public double EngineOilFlow { get; set; }

        /// <summary>
        /// 燃油回油流量测量-L31
        /// </summary>
        public double OilFlow_L31 { get; set; }

        /// <summary>
        /// 进气流量测量右
        /// </summary>
        public double InAirFlowRight { get; set; }

        /// <summary>
        /// 前增压器进气流量计前温度
        /// </summary>
        public double BeforeInAirTemp { get; set; }

        /// <summary>
        /// 后增压器进气流量计前温度
        /// </summary>
        public double AfterInAirTemp { get; set; }

        /// <summary>
        /// 厂房进气压力检测1
        /// </summary>
        public double FactoryAirPressureIn1 { get; set; }

        /// <summary>
        /// 厂房进气压力检测2
        /// </summary>
        public double FactoryAirPressureIn2 { get; set; }

        /// <summary>
        /// L32
        /// </summary>
        public double L32 { get; set; }

        /// <summary>
        /// 燃油进油流量测量-L30
        /// </summary>
        public double OilFlowIn_L30 { get; set; }

        /// <summary>
        /// 高温水膨胀水箱液位检测
        /// </summary>
        public double HWaterBoxLevel { get; set; }

        /// <summary>
        /// 励磁电压
        /// </summary>
        public double ExcitationV { get; set; }

        /// <summary>
        /// 励磁电流
        /// </summary>
        public double ExcitationI { get; set; }

    }
}

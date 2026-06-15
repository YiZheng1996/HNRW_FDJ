using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class FuelDataGrp
    {

        //FuelGrp
        /// <summary>
        /// 水阻箱温度检测
        /// </summary>
        public double WaterResistBoxTemperatureDetect { get; set; }

        /// <summary>
        /// 精滤器2前压力检测-P36
        /// </summary>
        public double FineFilter2FrontPressureDetectP36 { get; set; }

        /// <summary>
        /// 水阻箱极板位移检测
        /// </summary>
        public double WaterResistBoxPolarPlateDisplacementDetect { get; set; }

        /// <summary>
        /// 粗滤器2后压力检测-P33
        /// </summary>
        public double CoarseFilter2RearPressureDetectP33 { get; set; }

        /// <summary>
        /// 精滤器1前压力检测-P34
        /// </summary>
        public double FineFilter1FrontPressureDetectP34 { get; set; }

        /// <summary>
        /// 粗滤器1后压力检测-P31
        /// </summary>
        public double CoarseFilter1RearPressureDetectP31 { get; set; }

        /// <summary>
        /// 精滤器2后压力检测-P37
        /// </summary>
        public double FineFilter2RearPressureDetectP37 { get; set; }

        /// <summary>
        /// 粗滤器2前压力检测-P32
        /// </summary>
        public double CoarseFilter2FrontPressureDetectP32 { get; set; }

        /// <summary>
        /// 柴油箱液位检测-L29
        /// </summary>
        public double DieselTankLevelDetectL29 { get; set; }

        /// <summary>
        /// 精滤器1后压力检测-P35
        /// </summary>
        public double FineFilter1RearPressureDetectP35 { get; set; }

        /// <summary>
        /// 粗滤器1前压力检测-P30
        /// </summary>
        public double CoarseFilter1FrontPressureDetectP30 { get; set; }

    }
}

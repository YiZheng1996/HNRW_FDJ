using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Fault.Model
{
    /// <summary>
    /// ECM故障需检测的实时值类
    /// </summary>
    public class SensorData
    {
        public double 发动机转速 { get; set; }

        public double 发动机功率 { get; set; }

        public double 电喷转速1 { get; set; }
        public double 电喷转速2 { get; set; }
        public double 电喷状态 { get; set; }


        public double 高温水出水温度 { get; set; }
        public double 中冷水进水温度 { get; set; }
        public double 中冷水出水温度 { get; set; }
        public double 后中冷后空气温度 { get; set; }
        public double 主油道进口油温 { get; set; }
        public double 前压气机出口空气温度 { get; set; }
        public double 后压气机出口空气温度 { get; set; }
        public double 主油道进口油压 { get; set; }
        public double 燃油精滤器前油压 { get; set; }
        public double 燃油精滤器后油压 { get; set; }
        public double 机油泵出口油温 { get; set; }
        public double 主油道末端油压 { get; set; }
        public double 后增压器进口油压 { get; set; }
        public double 前增压器转速 { get; set; }
        public double 后增压器转速 { get; set; }

        public double[] A1A6缸排气温度 { get; set; }
        public double[] B1B6缸排气温度 { get; set; }
        public double A涡前排气温度 { get; set; }
        public double B涡前排气温度 { get; set; }
        public double[] _1_7档轴温 { get; set; }

        public double 轴温监控装置通讯故障 { get; set; }

        /// <summary>
        /// 以下为硬线信号
        /// </summary>
        public double 飞轮发动机转速1 { get; set; }
        public double 飞轮发动机转速2 { get; set; }
        public double 后增进油压卸载开关 { get; set; }
        public double 后增进油压停机开关 { get; set; }
        public double 曲轴箱差压开关 { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using RW.Configuration;
using System.Windows.Forms;

namespace MainUI.Config
{
    // public class SystemConfig : RW.Configuration.SystemConfig

    /// <summary>
    /// 故障参数
    /// </summary>
    public class FaultConfig : IniConfig
    {
        public FaultConfig(string model)
          : base(Application.StartupPath + $"\\config\\{model}.ini")
        {
            this.SetSectionName("FaultConfig");
            this.Load();
        }

        public FaultConfig()
     : base(Application.StartupPath + $"\\config\\FaultConfig.ini")
        {
            this.SetSectionName("FaultConfig");
            this.Load();
        }

        /// <summary>
        /// 执行阶段列表
        /// </summary>
        [IniKeyName("执行阶段列表")]
        public List<FaultData> FaultDataLists { get; set; } = new List<FaultData>();

    }


    public class FaultData 
    {
        /// <summary>
        /// 故障1: 高温水出水温度.警报.高温水出水温度 >=
        /// </summary>
        [IniKeyName("故障1: 高温水出水温度.警报.高温水出水温度 >=")]
        public int F1V1 { get; set; }

        /// <summary>
        /// 故障1: 高温水出水温度.降载.高温水出水温度 >=
        /// </summary>
        [IniKeyName("故障1: 高温水出水温度.降载.高温水出水温度 >=")]
        public int F1V2 { get; set; }


        /// <summary>
        /// 故障2: 中冷水进水温度.警报.中冷水进水温度 >=
        /// </summary>
        [IniKeyName("故障2: 中冷水进水温度.警报.中冷水进水温度 >=")]
        public int F2V1 { get; set; }


        /// <summary>
        /// 故障3: 中冷水出水温度.警报.中冷水出水温度 >=
        /// </summary>
        [IniKeyName("故障3: 中冷水出水温度.警报.中冷水出水温度 >=")]
        public int F3V1 { get; set; }


        /// <summary>
        /// 故障6: 后中冷后空气温度.警报.后中冷后空气温度 >=
        /// </summary>
        [IniKeyName("故障6: 后中冷后空气温度.警报.后中冷后空气温度 >=")]
        public int F6V1 { get; set; }


        /// <summary>
        /// 故障7: 主油道进口油温.警报.主油道进口油温 >=
        /// </summary>
        [IniKeyName("故障7: 主油道进口油温.警报.主油道进口油温 >=")]
        public int F7V1 { get; set; }


        /// <summary>
        /// 故障8: 前压气机出口空气温度.警报.前压气机出口空气温度 >=
        /// </summary>
        [IniKeyName("故障8: 前压气机出口空气温度.警报.前压气机出口空气温度 >=")]
        public int F8V1 { get; set; }


        /// <summary>
        /// 故障9: 后压气机出口空气温度.警报.后压气机出口空气温度 >=
        /// </summary>
        [IniKeyName("故障9: 后压气机出口空气温度.警报.后压气机出口空气温度 >=")]
        public int F9V1 { get; set; }


        /// <summary>
        /// 故障11: 主油道进口油压.警报.主油道进口油压 <
        /// </summary>
        [IniKeyName("故障11: 主油道进口油压.警报.主油道进口油压 <")]
        public int F11V1 { get; set; }


        /// <summary>
        /// 故障11: 主油道进口油压.警报.发动机转速 >
        /// </summary>
        [IniKeyName("故障11: 主油道进口油压.警报.发动机转速 >")]
        public int F11V2 { get; set; }


        /// <summary>
        /// 故障14.1: 燃油精滤器后油压.警报.燃油精滤器后油压 >
        /// </summary>
        [IniKeyName("故障14.1: 燃油精滤器后油压.警报.燃油精滤器后油压 >")]
        public int F141V1 { get; set; }


        /// <summary>
        /// 故障14.1: 燃油精滤器后油压.警报.燃油精滤器后油压 <
        /// </summary>
        [IniKeyName("故障14.1: 燃油精滤器后油压.警报.燃油精滤器后油压 <")]
        public int F141V2 { get; set; }



        /// <summary>
        /// 故障14.1: 燃油精滤器后油压.警报.发动机转速 >
        /// </summary>
        [IniKeyName("故障14.1: 燃油精滤器后油压.警报.发动机转速 >")]
        public int F141V3 { get; set; }


        /// <summary>
        /// 故障14.1: 燃油精滤器后油压.警报时长
        /// </summary>
        [IniKeyName("故障14.1: 燃油精滤器后油压.警报时长")]
        public int F141V4 { get; set; }


        /// <summary>
        /// 故障14.2: 燃油精滤器后前后压差大于100 kPa.警报.diff(|燃油精滤器后油压 -燃油精滤器前油压|)
        /// </summary>
        [IniKeyName("故障14.2: 燃油精滤器后前后压差大于100 kPa.警报.diff(|燃油精滤器后油压 -燃油精滤器前油压|)")]
        public int F142V1 { get; set; }


        /// <summary>
        /// 故障14.2: 燃油精滤器后前后压差大于100 kPa.警报.警报时长
        /// </summary>
        [IniKeyName("故障14.2: 燃油精滤器后前后压差大于100 kPa.警报.警报时长")]
        public int F142V2 { get; set; }


        /// <summary>
        /// 故障17: 机油泵出口油温.警报.机油泵出口油温 >=
        /// </summary>
        [IniKeyName("故障17: 机油泵出口油温.警报.机油泵出口油温 >=")]
        public int F17V1 { get; set; }


        /// <summary>
        /// 故障17: 机油泵出口油温.降载.机油泵出口油温 >=
        /// </summary>
        [IniKeyName("故障17: 机油泵出口油温.降载.机油泵出口油温 >=")]
        public int F17V2 { get; set; }


        /// <summary>
        /// 故障18: 主油道末端油压.警报.warn1.主油道末端油压 >
        /// </summary>
        [IniKeyName("故障18: 主油道末端油压.警报.warn1.主油道末端油压 >")]
        public int F18V1 { get; set; }


        /// <summary>
        /// 故障18: 主油道末端油压.警报.warn1.主油道末端油压 <
        /// </summary>
        [IniKeyName("故障18: 主油道末端油压.警报.warn1.主油道末端油压 <")]
        public int F18V2 { get; set; }


        /// <summary>
        /// 故障18: 主油道末端油压.警报.warn1.发动机转速 > 
        /// </summary>
        [IniKeyName("故障18: 主油道末端油压.警报.warn1.发动机转速 > ")]
        public int F18V3 { get; set; }


        /// <summary>
        /// 故障18: 主油道末端油压.警报.warn2.主油道末端油压 >
        /// </summary>
        [IniKeyName("故障18: 主油道末端油压.警报.warn2.主油道末端油压 >")]
        public int F18V4 { get; set; }


        /// <summary>
        /// 故障18: 主油道末端油压.警报.warn2.主油道末端油压 <=
        /// </summary>
        [IniKeyName("故障18: 主油道末端油压.警报.warn2.主油道末端油压 <=")]
        public int F18V5 { get; set; }


        /// <summary>
        /// 故障18: 主油道末端油压.警报.warn2.发动机转速 >= 
        /// </summary>
        [IniKeyName("故障18: 主油道末端油压.警报.warn2.发动机转速 >= ")]
        public int F18V6 { get; set; }


        /// <summary>
        /// 故障18: 主油道末端油压.警报.warn2.发动机转速 <=
        /// </summary>
        [IniKeyName("故障18: 主油道末端油压.警报.warn2.发动机转速 <= ")]
        public int F18V7 { get; set; }


        /// <summary>
        /// 故障18: 主油道末端油压.降载.主油道末端油压 >
        /// </summary>
        [IniKeyName("故障18: 主油道末端油压.降载.主油道末端油压 >")]
        public int F18V8 { get; set; }


        /// <summary>
        /// 故障18: 主油道末端油压.降载.主油道末端油压 <
        /// </summary>
        [IniKeyName("故障18: 主油道末端油压.降载.主油道末端油压 <")]
        public int F18V9 { get; set; }


        /// <summary>
        /// 故障18: 主油道末端油压.降载.发动机转速 >
        /// </summary>
        [IniKeyName("故障18: 主油道末端油压.降载.发动机转速 >")]
        public int F18V10 { get; set; }


        /// <summary>
        /// 故障18: 主油道末端油压.停机.主油道末端油压 >
        /// </summary>
        [IniKeyName("故障18: 主油道末端油压.停机.主油道末端油压 >")]
        public int F18V11 { get; set; }


        /// <summary>
        /// 故障18: 主油道末端油压.停机.主油道末端油压 <
        /// </summary>
        [IniKeyName("故障18: 主油道末端油压.停机.主油道末端油压 <")]
        public int F18V12 { get; set; }


        /// <summary>
        /// 故障18: 主油道末端油压.停机.发动机转速 >
        /// </summary>
        [IniKeyName("故障18: 主油道末端油压.停机.发动机转速 >=")]
        public int F18V13 { get; set; }


        /// <summary>
        /// 故障20: 后增压器进油压.警报.后增压器进口油压 <=
        /// </summary>
        [IniKeyName("故障20: 后增压器进油压.警报.后增压器进口油压 <=")]
        public int F20V1 { get; set; }


        /// <summary>
        /// 故障20: 后增压器进油压.降载.后增压器进口油压 <=
        /// </summary>
        [IniKeyName("故障20: 后增压器进油压.降载.后增压器进口油压 <=")]
        public int F20V2 { get; set; }


        /// <summary>
        /// 故障22: 前增压器转速.警报.前增压器转速 >=
        /// </summary>
        [IniKeyName("故障22: 前增压器转速.警报.前增压器转速 >=")]
        public int F22V1 { get; set; }


        /// <summary>
        /// 故障22: 前增压器转速.警报.警报时间
        /// </summary>
        [IniKeyName("故障22: 前增压器转速.警报.警报时间")]
        public int F22V2 { get; set; }


        /// <summary>
        /// 故障23: 后增压器转速.警报.后增压器转速 >=
        /// </summary>
        [IniKeyName("故障23: 后增压器转速.警报.后增压器转速 >=")]
        public int F23V1 { get; set; }


        /// <summary>
        /// 故障23: 后增压器转速.警报.警报时间
        /// </summary>
        [IniKeyName("故障23: 后增压器转速.警报.警报时间")]
        public int F23V2 { get; set; }


        /// <summary>
        /// 故障24: AI-A6缸排气温度.警报.A1A6缸排气温度.Max
        /// </summary>
        [IniKeyName("故障24: AI-A6缸排气温度.警报.A1A6缸排气温度.Max")]
        public int F24V1 { get; set; }


        /// <summary>
        /// 故障24: AI-A6缸排气温度.降载.发动机功率 >=
        /// </summary>
        [IniKeyName("故障24: AI-A6缸排气温度.降载.发动机功率 >=")]
        public int F24V2 { get; set; }

        /// <summary>
        /// 故障24: A1-A6缸排气温度.降载.（A1A6缸排气温度.Max()-A1A6缸排气温度.Min())>
        /// </summary>
        [IniKeyName("故障24: AI-A6缸排气温度.降载.（A1A6缸排气温度.Max()-A1A6缸排气温度.Min())>")]
        public int F24V3 { get; set; }

        /// <summary>
        /// 故障24: 降载检测时间
        /// </summary>
        [IniKeyName("故障24:降载检测时间")]
        public int F24V4 { get; set; }

        /// <summary>
        /// 故障25: A1-A6缸排气温度.警报.A1A6缸排气温度.Max
        /// </summary>
        [IniKeyName("故障25: B1-B6缸排气温度.警报.B1B6缸排气温度.Max")]
        public int F25V1 { get; set; }


        /// <summary>
        /// 故障25: B1-B6缸排气温度.降载.发动机功率 >=
        /// </summary>
        [IniKeyName("故障25: BI-B6缸排气温度.降载.发动机功率 >=")]
        public int F25V2 { get; set; }


        /// <summary>
        /// 故障25: B1-B6缸排气温度.降载.（A1A6缸排气温度.Max()-A1A6缸排气温度.Min())>
        /// </summary>
        [IniKeyName("故障24: B1-B6缸排气温度.降载.（B1B6缸排气温度.Max()-B1B6缸排气温度.Min())>")]
        public int F25V3 { get; set; }

        /// <summary>
        /// 故障25: 降载检测时间
        /// </summary>
        [IniKeyName("故障25:降载检测时间")]
        public int F25V4 { get; set; }

        /// <summary>
        /// 故障26: A涡前排气温度.警报.A涡前排气温度 >=
        /// </summary>
        [IniKeyName("故障26: A涡前排气温度.警报.A涡前排气温度 >=")]
        public int F26V1 { get; set; }

        /// <summary>
        /// 故障27: B涡前排气温度.警报.B涡前排气温度 >=
        /// </summary>
        [IniKeyName("故障27: B涡前排气温度.警报.B涡前排气温度 >=")]
        public int F27V1 { get; set; }


        /// <summary>
        /// 故障28: 1-7档轴温.降载._1_7档轴温.Max() >=
        /// </summary>
        [IniKeyName("故障28: 1-7档轴温.降载._1_7档轴温.Max() >=")]
        public int F28V1 { get; set; }


        /// <summary>
        /// 故障28: I-7档轴温.停机._1_7档轴温.Max() >=
        /// </summary>
        [IniKeyName("故障28: I-7档轴温.停机._1_7档轴温.Max() >=")]
        public int F28V2 { get; set; }


        /// <summary>
        /// 故障28: I-7档轴温.停机._1_7档轴温.Max() <
        /// </summary>
        [IniKeyName("故障28: I-7档轴温.停机._1_7档轴温.Max() <")]
        public int F28V3 { get; set; }

        /// <summary>
        /// 故障29: 轴温监控装置通讯故障
        /// </summary>
        [IniKeyName("故障29: 轴温监控装置通讯故障")]
        public int F29V1 { get; set; } = 1;

        /// <summary>
        /// 故障30: 电喷转速1.停机.电喷转速1 >=
        /// </summary>
        [IniKeyName("故障30: 电喷转速1.停机.电喷转速1 >=")]
        public int F30V1 { get; set; }


        /// <summary>
        /// 故障31: 电喷转速2.停机.电喷转速2 >=
        /// </summary>
        [IniKeyName("故障31: 电喷转速2.停机.电喷转速2 >=")]
        public int F31V1 { get; set; }

        /// <summary>
        /// 故障32: 电喷故障
        /// </summary>
        [IniKeyName("故障32: 电喷故障")]
        public int F32V1 { get; set; } = 1;

        /// <summary>
        /// 故障33: 发动机转速1 飞轮.停机.飞轮发动机转速1 >=
        /// </summary>
        [IniKeyName("故障33: 发动机转速1 飞轮.停机.飞轮发动机转速1 >=")]
        public int F33V1 { get; set; }


        /// <summary>
        /// 故障34: 发动机转速2 飞轮.停机.飞轮发动机转速2 >=
        /// </summary>
        [IniKeyName("故障34: 发动机转速2 飞轮.停机.飞轮发动机转速2 >=")]
        public int F34V1 { get; set; }

        /// <summary>
        /// 故障35: 后增进油压卸载开关.降载.后增进油压卸载开关(==0降载)
        /// </summary>
        [IniKeyName("故障35: 后增进油压卸载开关.降载.后增进油压卸载开关(==0降载)")]
        public int F35V1 { get; set; } = 0;


        /// <summary>
        /// 故障36: 后增进油压停机开关.停机.后增进油压停机开关（==0停机）
        /// </summary>
        [IniKeyName("故障36: 后增进油压停机开关.停机.后增进油压停机开关（==0停机）")]
        public int F36V1 { get; set; } = 0;


        /// <summary>
        /// 故障37: 曲轴箱差压开关.停机.曲轴箱差压开关（==1停机）
        /// </summary>
        [IniKeyName("故障37: 曲轴箱差压开关.停机.曲轴箱差压开关（==1停机）")]
        public int F37V1 { get; set; } = 1;
    }
}

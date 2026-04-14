using System;
using FreeSql.DataAnnotations;
using MainUI.FSql.Model;

namespace MainUI.FSql
{
    /// <summary>
    /// 试验数据记录（按序号排列）
    /// </summary>
    [Table(Name = "ManualRecordPara")]
    public class ManualRecordPara : IRecordData
    {
        /// <summary>
        /// gid
        /// </summary>
        [Column(Name = "gid", IsPrimary = true)]
        public string gid { get; set; }

        /// <summary>
        /// 主表gid
        /// </summary>
        [Column(Name = "mgid")]
        public string mgid { get; set; }

        ///// <summary>
        ///// 记录号
        ///// </summary>
        //public int Index { get; set; }

        ///// <summary>
        ///// 柴油机型号
        ///// </summary>
        //public string DieselEngineModel { get; set; }

        ///// <summary>
        ///// 柴油机编号
        ///// </summary>
        //public string DieselEngineNo { get; set; }

        ///// <summary>
        ///// 记录名称
        ///// </summary>
        //public string RecordName { get; set; }

        ///// <summary>
        ///// 试验类型
        ///// </summary>
        //public string TestName { get; set; }

        ///// <summary>
        ///// 试验阶段
        ///// </summary>
        //public string TestStage { get; set; }

        ///// <summary>
        ///// 试验周期
        ///// </summary>
        //public string TestCycle { get; set; }

        ///// <summary>
        ///// 试验循环节点
        ///// </summary>
        //public string TestStep { get; set; }

        ///// <summary>
        ///// 日期
        ///// </summary>
        //public string DataTime { get; set; }

        ///// <summary>
        ///// 时间
        ///// </summary>
        //public string Time { get; set; }

        ///// <summary>
        ///// 小时数
        ///// </summary>
        //public double HourNum { get; set; }

        ///// <summary>
        ///// 记录时间
        ///// </summary>
        //public DateTime RecordDataTime { get; set; }

        //// ========== 以下是按照序号排列的测试参数 ==========

        ///// <summary>
        ///// 1 环境温度
        ///// </summary>
        //public double AT { get; set; }

        ///// <summary>
        ///// 2 大气压力
        ///// </summary>
        //public double AP { get; set; }

        ///// <summary>
        ///// 3 空气湿度
        ///// </summary>
        //public double AH { get; set; }

        ///// <summary>
        ///// 4 柴油机转速
        ///// </summary>
        //public double RPM { get; set; }

        ///// <summary>
        ///// 5 柴油机有效扭矩
        ///// </summary>
        //public double Torque { get; set; }

        ///// <summary>
        ///// 6 柴油机有效功率
        ///// </summary>
        //public double Power { get; set; }

        ///// <summary>
        ///// 7 中冷水进机温度
        ///// </summary>
        //public double LWaterTempIn { get; set; }

        ///// <summary>
        ///// 8 高温水进机温度
        ///// </summary>
        //public double HWaterTempIn { get; set; }

        ///// <summary>
        ///// 9 中冷水出机温度
        ///// </summary>
        //public double LWaterTempOut { get; set; }

        ///// <summary>
        ///// 10 高温水出机温度
        ///// </summary>
        //public double HWaterTempOut { get; set; }

        ///// <summary>
        ///// 11 机油热交换器进口水温
        ///// </summary>
        //public double EngineWaterTempIn { get; set; }

        ///// <summary>
        ///// 12 机油热交换器出口水温
        ///// </summary>
        //public double EngineWaterTempOut { get; set; }

        ///// <summary>
        ///// 13 中冷水泵进口压力
        ///// </summary>
        //public double LPressureIn { get; set; }

        ///// <summary>
        ///// 14 高温水泵进口压力
        ///// </summary>
        //public double HPressureIn { get; set; }

        ///// <summary>
        ///// 15 中冷水泵出口压力
        ///// </summary>
        //public double LPressureOut { get; set; }

        ///// <summary>
        ///// 16 高温水泵出口压力
        ///// </summary>
        //public double HPressureOut { get; set; }

        ///// <summary>
        ///// 17 中冷水出机压力
        ///// </summary>
        //public double LWaterPressureOut { get; set; }

        ///// <summary>
        ///// 18 高温水出机压力
        ///// </summary>
        //public double HWaterPressureOut { get; set; }

        ///// <summary>
        ///// 19 机油热交换器进口油温
        ///// </summary>
        //public double HeatExchangerTempIn { get; set; }

        ///// <summary>
        ///// 20 机油热交换器出口油温
        ///// </summary>
        //public double HeatExchangerTempOut { get; set; }

        ///// <summary>
        ///// 21 主油道末端油压
        ///// </summary>
        //public double EOPressure2 { get; set; }

        ///// <summary>
        ///// 22 机油泵出口油压
        ///// </summary>
        //public double EOPressure1 { get; set; }

        ///// <summary>
        ///// 23 机油泵出口油温
        ///// </summary>
        //public double EngineOilOutletTemp { get; set; }

        ///// <summary>
        ///// 24 机油分析（实体类中注释掉了，需要添加字段）
        ///// </summary>
        //public double EOAnalysis { get; set; }

        ///// <summary>
        ///// 25 机油消耗（实体类中注释掉了，需要添加字段）
        ///// </summary>
        //public double EOConsumption { get; set; }

        ///// <summary>
        ///// 26 前中冷前空气温度
        ///// </summary>
        //public double FrontAirTempIn { get; set; }

        ///// <summary>
        ///// 27 后中冷前空气温度
        ///// </summary>
        //public double AfterAirTempIn { get; set; }

        ///// <summary>
        ///// 28 前中冷后空气温度
        ///// </summary>
        //public double FrontAirTempOut { get; set; }

        ///// <summary>
        ///// 29 后中冷后空气温度
        ///// </summary>
        //public double AfterAirTempOut { get; set; }

        ///// <summary>
        ///// 30 前中冷前空气压力
        ///// </summary>
        //public double FrontAirPressureIn { get; set; }

        ///// <summary>
        ///// 31 后中冷前空气压力
        ///// </summary>
        //public double AfterAirPressureIn { get; set; }

        ///// <summary>
        ///// 32 前中冷后空气压力
        ///// </summary>
        //public double FrontAirPressureOut { get; set; }

        ///// <summary>
        ///// 33 后中冷后空气压力
        ///// </summary>
        //public double AfterAirPressureOut { get; set; }

        ///// <summary>
        ///// 34 前增压器转速
        ///// </summary>
        //public double FrontTurbochargerRPM { get; set; }

        ///// <summary>
        ///// 35 后增压器转速
        ///// </summary>
        //public double AfterTurbochargerRPM { get; set; }

        ///// <summary>
        ///// 36 前增压器进气真空度
        ///// </summary>
        //public double FrontTurbochargerPressureIn { get; set; }

        ///// <summary>
        ///// 37 后增压器进气真空度
        ///// </summary>
        //public double AfterTurbochargerPressureIn { get; set; }

        ///// <summary>
        ///// 38 前增压器排气背压
        ///// </summary>
        //public double FrontTurbochargerPressureOut { get; set; }

        ///// <summary>
        ///// 39 后增压器排气背压
        ///// </summary>
        //public double AfterTurbochargerPressureOut { get; set; }

        ///// <summary>
        ///// 40 A1缸排气温度
        ///// </summary>
        //public double EGTempA1 { get; set; }

        ///// <summary>
        ///// 41 A2缸排气温度
        ///// </summary>
        //public double EGTempA2 { get; set; }

        ///// <summary>
        ///// 42 A3缸排气温度
        ///// </summary>
        //public double EGTempA3 { get; set; }

        ///// <summary>
        ///// 43 A4缸排气温度
        ///// </summary>
        //public double EGTempA4 { get; set; }

        ///// <summary>
        ///// 44 A5缸排气温度
        ///// </summary>
        //public double EGTempA5 { get; set; }

        ///// <summary>
        ///// 45 A6缸排气温度
        ///// </summary>
        //public double EGTempA6 { get; set; }

        ///// <summary>
        ///// 46 A7缸排气温度
        ///// </summary>
        //public double EGTempA7 { get; set; }

        ///// <summary>
        ///// 47 A8缸排气温度
        ///// </summary>
        //public double EGTempA8 { get; set; }

        ///// <summary>
        ///// 48 B1缸排气温度
        ///// </summary>
        //public double EGTempB1 { get; set; }

        ///// <summary>
        ///// 49 B2缸排气温度
        ///// </summary>
        //public double EGTempB2 { get; set; }

        ///// <summary>
        ///// 50 B3缸排气温度
        ///// </summary>
        //public double EGTempB3 { get; set; }

        ///// <summary>
        ///// 51 B4缸排气温度
        ///// </summary>
        //public double EGTempB4 { get; set; }

        ///// <summary>
        ///// 52 B5缸排气温度
        ///// </summary>
        //public double EGTempB5 { get; set; }

        ///// <summary>
        ///// 53 B6缸排气温度
        ///// </summary>
        //public double EGTempB6 { get; set; }

        ///// <summary>
        ///// 54 B7缸排气温度
        ///// </summary>
        //public double EGTempB7 { get; set; }

        ///// <summary>
        ///// 55 B8缸排气温度
        ///// </summary>
        //public double EGTempB8 { get; set; }

        ///// <summary>
        ///// 56 前涡轮进口废气温度
        ///// </summary>
        //public double FrontTurbochargerTempIn { get; set; }

        ///// <summary>
        ///// 57 后涡轮进口废气温度
        ///// </summary>
        //public double AfterTurbochargerTempIn { get; set; }

        ///// <summary>
        ///// 58 前涡轮出口废气温度
        ///// </summary>
        //public double FrontTurbochargerTempOut { get; set; }

        ///// <summary>
        ///// 59 后涡轮出口废气温度
        ///// </summary>
        //public double AfterTurbochargerTempOut { get; set; }

        ///// <summary>
        ///// 60 前涡轮进口废气压力（注意：实体类中是前涡轮前机油进口压力，可能需要确认）
        ///// </summary>
        //public double FrontTurbochargerPressureIn2 { get; set; }

        ///// <summary>
        ///// 61 后涡轮进口废气压力（注意：实体类中是后涡轮前机油进口压力，可能需要确认）
        ///// </summary>
        //public double AfterTurbochargerPressureIn2 { get; set; }

        ///// <summary>
        ///// 62 烟度（缺失，需要添加字段）
        ///// </summary>
        //public double Smoke { get; set; }

        ///// <summary>
        ///// 63 燃油消耗时间（缺失，需要添加字段）
        ///// </summary>
        //public double ECOTime { get; set; }

        ///// <summary>
        ///// 64 燃油消耗量（缺失，需要添加字段）
        ///// </summary>
        //public double ECOQuantity { get; set; }

        ///// <summary>
        ///// 65 燃油消耗率（缺失，需要添加字段）
        ///// </summary>
        //public double ECORate { get; set; }

        ///// <summary>
        ///// 66 燃油泵进口油温
        ///// </summary>
        //public double OilTempIn { get; set; }

        ///// <summary>
        ///// 67 喷射参数（缺失，需要添加字段）
        ///// </summary>
        //public double InjectionParameter { get; set; }

        
    }
}
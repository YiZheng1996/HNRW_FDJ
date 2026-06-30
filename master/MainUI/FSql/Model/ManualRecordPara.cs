using FreeSql.DataAnnotations;
using MainUI.FSql.Model;

namespace MainUI.FSql
{
    /// <summary>
    /// 手动出厂试验数据记录（对应出厂记录表
    /// 注意：不要在子类重新声明基类已有的同名属性（如 Index、RPM、Power、
    /// EGTempA1 等），否则会发生属性隐藏，FreeSql 列映射会出问题。
    /// </summary>
    [Table(Name = "ManualRecordPara")]
    public class ManualRecordPara : IRecordData
    {
        // ── 主键 / 外键 ────────────────────────────────────────────────────

        [Column(Name = "gid", IsPrimary = true)]
        public string gid { get; set; }

        /// <summary>关联 ManualRecordMain 的主键</summary>
        [Column(Name = "mgid")]
        public string mgid { get; set; }

        // 出厂表特有的元信息

        /// <summary>试验时间 — 时（出厂表列2）</summary>
        public int TestHour { get; set; }

        /// <summary>试验时间 — 分（出厂表列3）</summary>
        public int TestMinute { get; set; }

        /// <summary>备注（出厂表列28）</summary>
        public string Remark { get; set; }

        // 名义值基类只有实测值 RPM/Power

        /// <summary>柴油机名义转速 rpm（出厂表列4，区别于基类 RPM=实测转速）</summary>
        public double NominalRPM { get; set; }

        /// <summary>柴油机名义功率 kW（出厂表列6，区别于基类 Power=实测功率）</summary>
        public double NominalPower { get; set; }

        // 出厂表特有的压力点（基类没有对应字段）

        /// <summary>燃油进口压力（出厂表列11）</summary>
        public double PFuelInlet { get; set; }

        /// <summary>机油进口压力（出厂表列15，区别于基类 EOPressure1=机油泵出口油压）</summary>
        public double POilInlet { get; set; }

        /// <summary>前增压器油压（出厂表列17）</summary>
        public double PTurboOilFront { get; set; }

        /// <summary>后增压器油压（出厂表列18）</summary>
        public double PTurboOilAfter { get; set; }

        // 增压器子表特有字段（基类没有覆盖）

        /// <summary>压气机前压力 前（出厂表列28）</summary>
        public double PCompressorFront { get; set; }

        /// <summary>压气机前压力 后（出厂表列29）</summary>
        public double PCompressorAfter { get; set; }

        /// <summary>涡轮后压力 前（出厂表列30）</summary>
        public double PTurboOutPressureFront { get; set; }

        /// <summary>涡轮后压力 后（出厂表列31）</summary>
        public double PTurboOutPressureAfter { get; set; }

        /// <summary>曲轴箱压力 ×10Pa（出厂表列32）</summary>
        public double PCrankcase { get; set; }

        /// <summary>中冷器前压力 前（×100Pa，出厂表列33）</summary>
        public double PInterCoolerFrontFront { get; set; }

        /// <summary>中冷器前压力 后（出厂表列34）</summary>
        public double PInterCoolerFrontAfter { get; set; }

        /// <summary>中冷器后压力 前（出厂表列35）</summary>
        public double PInterCoolerAfterFront { get; set; }

        /// <summary>中冷器后压力 后（出厂表列36）</summary>
        public double PInterCoolerAfterAfter { get; set; }

        /// <summary>中冷器前温度 前（出厂表列39）</summary>
        public double TInterCoolerFrontFront { get; set; }

        /// <summary>中冷器前温度 后（出厂表列40）</summary>
        public double TInterCoolerFrontAfter { get; set; }

        /// <summary>中冷器后温度 前（出厂表列41）</summary>
        public double TInterCoolerAfterFront { get; set; }

        /// <summary>中冷器后温度 后（出厂表列42）</summary>
        public double TInterCoolerAfterAfter { get; set; }

        // 各缸爆发压力 ×0.1MPa（基类没有，出厂表列63-78）

        public double BurstPA1 { get; set; }
        public double BurstPA2 { get; set; }
        public double BurstPA3 { get; set; }
        public double BurstPA4 { get; set; }
        public double BurstPA5 { get; set; }
        public double BurstPA6 { get; set; }
        public double BurstPB1 { get; set; }
        public double BurstPB2 { get; set; }
        public double BurstPB3 { get; set; }
        public double BurstPB4 { get; set; }
        public double BurstPB5 { get; set; }
        public double BurstPB6 { get; set; }

        // ════════════════════════════════════════════════════════════════
        // 以下出厂表字段直接复用基类（IRecordData）已有的同名/同义属性，
        // 不在此重新声明，CollectSnapshot() 采集时直接赋值基类属性即可：
        //
        //   出厂表字段            →  基类属性（IRecordData）
        //   ─────────────────────────────────────────────────
        //   柴油机实测转速        →  RPM
        //   柴油机实测功率        →  Power
        //   测油耗量              →  ECOQuantity
        //   燃油消耗率            →  ECORate
        //   中冷泵出口压力        →  LPressureOut
        //   高温泵出口压力        →  HPressureOut
        //   机油泵出口压力        →  EOPressure1
        //   机油总管末端压力      →  EOPressure2
        //   机油进口温度          →  HeatExchangerTempIn
        //   机油出口温度          →  HeatExchangerTempOut
        //   高温水进口温度        →  HWaterTempIn
        //   高温水出口温度        →  HWaterTempOut
        //   中冷水进口温度        →  LWaterTempIn
        //   中冷水出口温度        →  LWaterTempOut
        //   前增压器转速          →  FrontTurbochargerRPM
        //   后增压器转速          →  AfterTurbochargerRPM
        //   A1~A6缸排温           →  EGTempA1 ~ EGTempA6
        //   B1~B6缸排温           →  EGTempB1 ~ EGTempB6
        //   涡轮前废气温度(前/后) →  FrontTurbochargerTempIn / AfterTurbochargerTempIn
        //   涡轮后废气温度(前/后) →  FrontTurbochargerTempOut / AfterTurbochargerTempOut
        //   涡轮前废气压力(前/后) →  FrontTurbochargerPressureIn2 / AfterTurbochargerPressureIn2
        // ════════════════════════════════════════════════════════════════
    }
}
using System;
using System.Collections.Generic;
using MainUI.Equip;
using MainUI.Global;
using MainUI.Simulate;

namespace MainUI
{
    /// <summary>
    /// 模拟测试工况预设服务
    ///
    /// 一次性向 AutoRecordService 的所有数据源注入合理值：
    ///   - Common.AIgrp / AI2Grp（set索引器）
    ///   - Common.waterGrp.NewDataValue（直接写字典）
    ///   - Common.engineOilGrp.NewDataValue（直接写字典）
    ///   - MiddleData.instnce.PTFWeight（→ 自动计算 Torque / Power）
    ///   - TRDPSimulatorService（→ EngineSpeed / 增压器转速 / 轴温等）
    ///   - ET4500 / ZMPT650F
    /// </summary>
    public class SimulatePresetService
    {
        public static readonly SimulatePresetService Instance
            = new SimulatePresetService();

        private SimulatePresetService() { }

        // ════════════════════════════════════════════════════════════════════
        // 预设定义（TB/T 2745-2017 标准工况）
        // ════════════════════════════════════════════════════════════════════

        /// <summary>100h性能试验 - 标定功率工况（100% load, 1000rpm）</summary>
        public void ApplyRatedPower()
        {
            Apply(new PresetValues
            {
                Name = "标定功率工况",
                RPM = 1000,
                PTFWeight = 306.1,   // 3000 N.m ÷ 9.8 ≈ 306.1 kg
                FuelKgH = 185.0,
                CylExhaust = 420,
                TurboRPM = 38000,
                WaterHiOut = 88,
                WaterHiIn = 72,
                WaterLoOut = 52,
                WaterLoIn = 38,
                OilTemp = 75,
                OilPress = 480,
                AirTempFront = 42,
                AirTempAfter = 42,
            });
        }

        /// <summary>100h性能试验 - 超负荷工况（110% load, 1000rpm）</summary>
        public void ApplyOverload()
        {
            Apply(new PresetValues
            {
                Name = "超负荷工况",
                RPM = 1000,
                PTFWeight = 336.7,   // 3300 N.m ÷ 9.8
                FuelKgH = 205.0,
                CylExhaust = 455,
                TurboRPM = 41000,
                WaterHiOut = 92,
                WaterHiIn = 75,
                WaterLoOut = 55,
                WaterLoIn = 40,
                OilTemp = 78,
                OilPress = 460,
                AirTempFront = 46,
                AirTempAfter = 46,
            });
        }

        /// <summary>100h性能试验 - 部分负荷工况（75% load, 1000rpm）</summary>
        public void ApplyPartialLoad()
        {
            Apply(new PresetValues
            {
                Name = "部分负荷工况",
                RPM = 1000,
                PTFWeight = 229.6,   // 2250 N.m ÷ 9.8
                FuelKgH = 142.0,
                CylExhaust = 370,
                TurboRPM = 32000,
                WaterHiOut = 82,
                WaterHiIn = 68,
                WaterLoOut = 47,
                WaterLoIn = 34,
                OilTemp = 70,
                OilPress = 500,
                AirTempFront = 36,
                AirTempAfter = 36,
            });
        }

        /// <summary>100h性能试验 - 交替突变负荷工况（在标定与空载间切换）</summary>
        public void ApplyAlternatingLoad(bool highLoad)
        {
            if (highLoad)
            {
                Apply(new PresetValues
                {
                    Name = "交变负荷-高载",
                    RPM = 1000,
                    PTFWeight = 306.1,
                    FuelKgH = 185.0,
                    CylExhaust = 420,
                    TurboRPM = 38000,
                    WaterHiOut = 88,
                    WaterHiIn = 72,
                    WaterLoOut = 52,
                    WaterLoIn = 38,
                    OilTemp = 75,
                    OilPress = 480,
                    AirTempFront = 42,
                    AirTempAfter = 42,
                });
            }
            else
            {
                Apply(new PresetValues
                {
                    Name = "交变负荷-空载",
                    RPM = 700,
                    PTFWeight = 0,
                    FuelKgH = 28.0,
                    CylExhaust = 180,
                    TurboRPM = 12000,
                    WaterHiOut = 72,
                    WaterHiIn = 60,
                    WaterLoOut = 40,
                    WaterLoIn = 30,
                    OilTemp = 62,
                    OilPress = 520,
                    AirTempFront = 28,
                    AirTempAfter = 28,
                });
            }
        }

        /// <summary>360h耐久性试验 - 第Ⅰ阶段典型循环节点（1A: 50%转矩）</summary>
        public void ApplyEndurance1()
        {
            Apply(new PresetValues
            {
                Name = "耐久Ⅰ阶段-循环A",
                RPM = 630,     // 47% 额定转速
                PTFWeight = 122.4,   // 50% 扭矩
                FuelKgH = 55.0,
                CylExhaust = 250,
                TurboRPM = 18000,
                WaterHiOut = 76,
                WaterHiIn = 62,
                WaterLoOut = 44,
                WaterLoIn = 32,
                OilTemp = 65,
                OilPress = 530,
                AirTempFront = 30,
                AirTempAfter = 30,
            });
        }

        /// <summary>360h耐久性试验 - 第Ⅱ阶段典型循环节点（1D: 标定）</summary>
        public void ApplyEndurance2()
        {
            Apply(new PresetValues
            {
                Name = "耐久Ⅱ阶段-循环D",
                RPM = 520,     // 52%转速
                PTFWeight = 72.0,    // 18%扭矩
                FuelKgH = 38.0,
                CylExhaust = 210,
                TurboRPM = 14000,
                WaterHiOut = 74,
                WaterHiIn = 60,
                WaterLoOut = 42,
                WaterLoIn = 30,
                OilTemp = 63,
                OilPress = 535,
                AirTempFront = 28,
                AirTempAfter = 28,
            });
        }

        /// <summary>360h耐久性试验 - 第Ⅲ阶段典型循环节点（标定工况长时运行）</summary>
        public void ApplyEndurance3()
        {
            Apply(new PresetValues
            {
                Name = "耐久Ⅲ阶段-长时标定",
                RPM = 1000,
                PTFWeight = 306.1,
                FuelKgH = 185.0,
                CylExhaust = 420,
                TurboRPM = 38000,
                WaterHiOut = 89,
                WaterHiIn = 73,
                WaterLoOut = 53,
                WaterLoIn = 39,
                OilTemp = 76,
                OilPress = 475,
                AirTempFront = 43,
                AirTempAfter = 43,
            });
        }

        /// <summary>怠速工况（启机成功后稳定状态）</summary>
        public void ApplyIdle()
        {
            Apply(new PresetValues
            {
                Name = "怠速工况",
                RPM = 450,
                PTFWeight = 0,
                FuelKgH = 18.0,
                CylExhaust = 140,
                TurboRPM = 8000,
                WaterHiOut = 65,
                WaterHiIn = 52,
                WaterLoOut = 36,
                WaterLoIn = 28,
                OilTemp = 55,
                OilPress = 550,
                AirTempFront = 24,
                AirTempAfter = 24,
            });
        }

        // ════════════════════════════════════════════════════════════════════
        // 核心注入（对应 AutoRecordService.StartRecord() 的所有取值点）
        // ════════════════════════════════════════════════════════════════════

        private void Apply(PresetValues p)
        {
            try
            {
                // ── 1. TRDP → EngineSpeed（MiddleData.EngineSpeed 从这里读）
                TRDPSimulatorService.Instance.InjectValue("柴油机转速", (decimal)p.RPM);
                TRDPSimulatorService.Instance.InjectValue("前增压器转速", (decimal)p.TurboRPM);
                TRDPSimulatorService.Instance.InjectValue("后增压器转速", (decimal)p.TurboRPM);

                // TRDP 轴温（正常值，不触发报警）
                for (int i = 0; i < 7; i++)
                    TRDPSimulatorService.Instance.InjectValue(
                        TRDPSimulatorService.GearName(i), 72m + i * 1.5m);

                // TRDP 故障位全清
                TRDPSimulatorService.Instance.ClearAllFaults();
                TRDPSimulatorService.Instance.InjectValue("电喷转速1#", (decimal)p.RPM);
                TRDPSimulatorService.Instance.InjectValue("电喷转速2#", (decimal)p.RPM);
                TRDPSimulatorService.Instance.InjectValue("转速传感器1#", (decimal)p.RPM);
                TRDPSimulatorService.Instance.InjectValue("转速传感器2#", (decimal)p.RPM);

                // ── 2. PTFWeight → Torque / Power（自动计算）
                MiddleData.instnce.PTFWeight = p.PTFWeight;

                // ── 3. AIgrp（PLC1）
                Common.AIgrp["大气温度"] = 25.0;
                Common.AIgrp["大气压力"] = 101.3;
                Common.AIgrp["大气湿度"] = 55.0;
                Common.AIgrp["机油流量"] = 85.0;
                Common.AIgrp["高温水流量测量-L3"] = 120.0;
                Common.AIgrp["中冷水流量测量-L8"] = 95.0;
                Common.AIgrp["燃油进油流量测量-L30"] = p.FuelKgH * 1.2;
                Common.AIgrp["燃油回油流量测量-L31"] = p.FuelKgH * 0.2;

                // ── 4. AI2Grp（PLC2）缸温、水压、机油
                double cyl = p.CylExhaust;
                Common.AI2Grp["A1缸排气温度"] = cyl + 5;
                Common.AI2Grp["A2缸排气温度"] = cyl + 8;
                Common.AI2Grp["A3缸排气温度"] = cyl + 3;
                Common.AI2Grp["A4缸排气温度"] = cyl + 6;
                Common.AI2Grp["A5缸排气温度"] = cyl + 4;
                Common.AI2Grp["A6缸排气温度"] = cyl + 7;
                Common.AI2Grp["A7缸排气温度"] = cyl + 5;
                Common.AI2Grp["A8缸排气温度"] = cyl + 3;
                Common.AI2Grp["B1缸排气温度"] = cyl + 4;
                Common.AI2Grp["B2缸排气温度"] = cyl + 7;
                Common.AI2Grp["B3缸排气温度"] = cyl + 5;
                Common.AI2Grp["B4缸排气温度"] = cyl + 3;
                Common.AI2Grp["B5缸排气温度"] = cyl + 6;
                Common.AI2Grp["B6缸排气温度"] = cyl + 8;
                Common.AI2Grp["B7缸排气温度"] = cyl + 4;
                Common.AI2Grp["B8缸排气温度"] = cyl + 2;

                Common.AI2Grp["T1高温水出机温度"] = p.WaterHiOut;
                Common.AI2Grp["T2高温水进机温度"] = p.WaterHiIn;
                Common.AI2Grp["T3中冷水进机温度"] = p.WaterLoIn;
                Common.AI2Grp["T5中冷水出机温度"] = p.WaterLoOut;
                Common.AI2Grp["T20机油泵出口油温"] = p.OilTemp;
                Common.AI2Grp["T21主油道进口油温"] = p.OilTemp - 3;
                Common.AI2Grp["P1高温水出机压力"] = p.OilPress * 0.35;
                Common.AI2Grp["P2高温水泵进口压力"] = p.OilPress * 0.30;
                Common.AI2Grp["P3中冷水泵进口压力"] = p.OilPress * 0.28;
                Common.AI2Grp["P5中冷水出机压力"] = p.OilPress * 0.32;
                Common.AI2Grp["P20机油泵出口压力"] = p.OilPress;
                Common.AI2Grp["主油道末端油压"] = p.OilPress * 0.85;
                Common.AI2Grp["前中冷前空气温度"] = p.AirTempFront;
                Common.AI2Grp["前中冷后空气温度"] = p.AirTempFront - 8;
                Common.AI2Grp["后中冷前空气温度"] = p.AirTempAfter;
                Common.AI2Grp["后中冷后空气温度"] = p.AirTempAfter - 8;
                Common.AI2Grp["前中冷前空气压力"] = 102.0;
                Common.AI2Grp["前中冷后空气压力"] = 100.5;
                Common.AI2Grp["后中冷前空气压力"] = 102.0;
                Common.AI2Grp["后中冷后空气压力"] = 100.5;
                Common.AI2Grp["前增压器进气真空度"] = -2.5;
                Common.AI2Grp["后增压器进气真空度"] = -2.5;
                Common.AI2Grp["前增压器排气背压"] = 3.2;
                Common.AI2Grp["后增压器排气背压"] = 3.2;
                Common.AI2Grp["高温水泵出口压力"] = p.OilPress * 0.38;

                // ── 5. waterGrp.NewDataValue（直接写字典）
                Common.waterGrp.NewDataValue["高温水冷却器进口温度检测-T13"] = p.WaterHiIn;
                Common.waterGrp.NewDataValue["中冷水冷却器进口温度检测-T14"] = p.WaterLoIn;
                Common.waterGrp.NewDataValue["高温水温度实时PID"] = p.WaterHiOut;
                Common.waterGrp.NewDataValue["中冷水温度实时PID"] = p.WaterLoOut;

                // ── 6. engineOilGrp.NewDataValue（直接写字典）
                Common.engineOilGrp.NewDataValue["冷却器进口油温-T25"] = p.OilTemp - 5;
                Common.engineOilGrp.NewDataValue["机油温度实时PID"] = p.OilTemp;

                // ── 7. ET4500（油耗仪）
                ET4500.Instance.fuelConsumption = p.FuelKgH;
                ET4500.Instance.remainingFuel = 350.0;
                ET4500.Instance.fuelPercentage = 70.0;
                Common.opcExChangeSendGrp.SetDouble("油耗仪_NoError", 1);

                // ── 8. ZMPT650F（称重仪，对应扭矩）
                ZMPT650F.Instance.Weight = p.PTFWeight;
                MiddleData.instnce.PTFWeight = p.PTFWeight; // 再赋一次确保同步
                Common.opcExChangeSendGrp.SetDouble("重量", p.PTFWeight);
                Common.opcExChangeSendGrp.SetDouble("称重仪_NoError", 1);

                Var.LogInfo(string.Format("[SimulatePreset] 已注入工况：{0}  RPM={1}  PTFWeight={2}kg  油耗={3}kg/h",
                    p.Name, p.RPM, p.PTFWeight, p.FuelKgH));
            }
            catch (Exception ex)
            {
                Var.LogInfo(string.Format("[SimulatePreset] 注入异常：{0}", ex.Message));
            }
        }

        // ════════════════════════════════════════════════════════════════════
        // 内部数据结构
        // ════════════════════════════════════════════════════════════════════

        private class PresetValues
        {
            public string Name { get; set; }
            public double RPM { get; set; }
            /// <summary>磅秤重量 kg，EngineTorque = PTFWeight × 9.8 N.m</summary>
            public double PTFWeight { get; set; }
            public double FuelKgH { get; set; }
            /// <summary>缸排气温度基准值 ℃</summary>
            public double CylExhaust { get; set; }
            public double TurboRPM { get; set; }
            public double WaterHiOut { get; set; }
            public double WaterHiIn { get; set; }
            public double WaterLoOut { get; set; }
            public double WaterLoIn { get; set; }
            public double OilTemp { get; set; }
            public double OilPress { get; set; }
            public double AirTempFront { get; set; }
            public double AirTempAfter { get; set; }
        }
    }
}
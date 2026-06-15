using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class PLC2AIDataGrp
    {

        //PLC2AIGrp
        /// <summary>
        /// 机油耗测量压力
        /// </summary>
        public double OilConsumptionMeasurePressure { get; set; }

        /// <summary>
        /// T21主油道进口油温
        /// </summary>
        public double T21MainOilChannelInletOilTemperature { get; set; }

        /// <summary>
        /// T5中冷水出机温度
        /// </summary>
        public double T5CoolWaterOutletTemperature { get; set; }

        /// <summary>
        /// B8缸排气温度
        /// </summary>
        public double B8CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// 测功机U相温度
        /// </summary>
        public double DynamometerUPhaseTemperature { get; set; }

        /// <summary>
        /// 高温水泵出口压力
        /// </summary>
        public double HighTempWaterPumpOutletPressure { get; set; }

        /// <summary>
        /// 后增压器进气真空度
        /// </summary>
        public double RearSuperchargerIntakeVacuumDegree { get; set; }

        /// <summary>
        /// B4缸排气温度
        /// </summary>
        public double B4CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// 中冷器进口水温
        /// </summary>
        public double InterCoolerInletWaterTemperature { get; set; }

        /// <summary>
        /// 测功机D相温度
        /// </summary>
        public double DynamometerDPhaseTemperature { get; set; }

        /// <summary>
        /// A8缸排气温度
        /// </summary>
        public double A8CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// 励磁电压检测
        /// </summary>
        public double ExcitationVoltageDetect { get; set; }

        /// <summary>
        /// 前增压器进气真空度
        /// </summary>
        public double FrontSuperchargerIntakeVacuumDegree { get; set; }

        /// <summary>
        /// P20机油泵出口压力
        /// </summary>
        public double P20OilPumpOutletPressure { get; set; }

        /// <summary>
        /// B7缸排气温度
        /// </summary>
        public double B7CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// 前中冷后空气压力
        /// </summary>
        public double FrontInterCoolerRearAirPressure { get; set; }

        /// <summary>
        /// B2缸排气温度
        /// </summary>
        public double B2CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// 主油道末端油压
        /// </summary>
        public double MainOilChannelEndOilPressure { get; set; }

        /// <summary>
        /// A4缸排气温度
        /// </summary>
        public double A4CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// 测功机W相温度
        /// </summary>
        public double DynamometerWPhaseTemperature { get; set; }

        /// <summary>
        /// T31燃油泵进口油温
        /// </summary>
        public double T31FuelPumpInletOilTemperature { get; set; }

        /// <summary>
        /// P21主油道进口油压
        /// </summary>
        public double P21MainOilChannelInletOilPressure { get; set; }

        /// <summary>
        /// 测功机N相温度
        /// </summary>
        public double DynamometerNPhaseTemperature { get; set; }

        /// <summary>
        /// 前中冷前空气温度
        /// </summary>
        public double FrontInterCoolerFrontAirTemperature { get; set; }

        /// <summary>
        /// A7缸排气温度
        /// </summary>
        public double A7CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// 前增压器机油进口压力
        /// </summary>
        public double FrontSuperchargerOilInletPressure { get; set; }

        /// <summary>
        /// A2缸排气温度
        /// </summary>
        public double A2CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// 中冷器出口水温
        /// </summary>
        public double InterCoolerOutletWaterTemperature { get; set; }

        /// <summary>
        /// B6缸排气温度
        /// </summary>
        public double B6CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// 前增压器机油进口温度
        /// </summary>
        public double FrontSuperchargerOilInletTemperature { get; set; }

        /// <summary>
        /// 后增压器机油进口温度
        /// </summary>
        public double RearSuperchargerOilInletTemperature { get; set; }

        /// <summary>
        /// 后增压器排气背压
        /// </summary>
        public double RearSuperchargerExhaustBackPressure { get; set; }

        /// <summary>
        /// 后涡轮出口废气温度
        /// </summary>
        public double RearTurboOutletExhaustGasTemperature { get; set; }

        /// <summary>
        /// P38燃油供油压力
        /// </summary>
        public double P38FuelSupplyPressure { get; set; }

        /// <summary>
        /// 前涡轮进口废气压力
        /// </summary>
        public double FrontTurboInletExhaustGasPressure { get; set; }

        /// <summary>
        /// 后增压器机油进口压力
        /// </summary>
        public double RearSuperchargerOilInletPressure { get; set; }

        /// <summary>
        /// P2高温水泵进口压力
        /// </summary>
        public double P2HighTempWaterPumpInletPressure { get; set; }

        /// <summary>
        /// 后中冷后空气压力
        /// </summary>
        public double RearInterCoolerRearAirPressure { get; set; }

        /// <summary>
        /// 后增压器进气温度
        /// </summary>
        public double RearSuperchargerIntakeTemperature { get; set; }

        /// <summary>
        /// 前中冷后空气温度
        /// </summary>
        public double FrontInterCoolerRearAirTemperature { get; set; }

        /// <summary>
        /// A6缸排气温度
        /// </summary>
        public double A6CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// 前增压器排气背压
        /// </summary>
        public double FrontSuperchargerExhaustBackPressure { get; set; }

        /// <summary>
        /// T20机油泵出口油温
        /// </summary>
        public double T20OilPumpOutletOilTemperature { get; set; }

        /// <summary>
        /// T1高温水出机温度
        /// </summary>
        public double T1HighTempWaterOutletTemperature { get; set; }

        /// <summary>
        /// 后涡轮进口废气温度
        /// </summary>
        public double RearTurboInletExhaustGasTemperature { get; set; }

        /// <summary>
        /// 后中冷前空气温度
        /// </summary>
        public double RearInterCoolerFrontAirTemperature { get; set; }

        /// <summary>
        /// 励磁电流检测
        /// </summary>
        public double ExcitationCurrentDetect { get; set; }

        /// <summary>
        /// T2高温水进机温度
        /// </summary>
        public double T2HighTempWaterInletTemperature { get; set; }

        /// <summary>
        /// T3中冷水进机温度
        /// </summary>
        public double T3CoolWaterInletTemperature { get; set; }

        /// <summary>
        /// 机油耗测量液位
        /// </summary>
        public double OilConsumptionMeasureLevel { get; set; }

        /// <summary>
        /// T30燃油回油温度
        /// </summary>
        public double T30FuelReturnOilTemperature { get; set; }

        /// <summary>
        /// B3缸排气温度
        /// </summary>
        public double B3CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// 后涡轮进口废气压力
        /// </summary>
        public double RearTurboInletExhaustGasPressure { get; set; }

        /// <summary>
        /// 后中冷后空气温度
        /// </summary>
        public double RearInterCoolerRearAirTemperature { get; set; }

        /// <summary>
        /// 中冷水泵出口压力
        /// </summary>
        public double CoolWaterPumpOutletPressure { get; set; }

        /// <summary>
        /// 前中冷前空气压力
        /// </summary>
        public double FrontInterCoolerFrontAirPressure { get; set; }

        /// <summary>
        /// 测功机V相温度
        /// </summary>
        public double DynamometerVPhaseTemperature { get; set; }

        /// <summary>
        /// B1缸排气温度
        /// </summary>
        public double B1CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// 前增压器机油出口温度
        /// </summary>
        public double FrontSuperchargerOilOutletTemperature { get; set; }

        /// <summary>
        /// 后增压器机油出口温度
        /// </summary>
        public double RearSuperchargerOilOutletTemperature { get; set; }

        /// <summary>
        /// A3缸排气温度
        /// </summary>
        public double A3CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// P3中冷水泵进口压力
        /// </summary>
        public double P3CoolWaterPumpInletPressure { get; set; }

        /// <summary>
        /// 前涡轮出口废气温度
        /// </summary>
        public double FrontTurboOutletExhaustGasTemperature { get; set; }

        /// <summary>
        /// A1缸排气温度
        /// </summary>
        public double A1CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// 前增压器进气温度
        /// </summary>
        public double FrontSuperchargerIntakeTemperature { get; set; }

        /// <summary>
        /// P1高温水出机压力
        /// </summary>
        public double P1HighTempWaterOutletPressure { get; set; }

        /// <summary>
        /// B5缸排气温度
        /// </summary>
        public double B5CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// P5中冷水出机压力
        /// </summary>
        public double P5CoolWaterOutletPressure { get; set; }

        /// <summary>
        /// 前涡轮进口废气温度
        /// </summary>
        public double FrontTurboInletExhaustGasTemperature { get; set; }

        /// <summary>
        /// 后中冷前空气压力
        /// </summary>
        public double RearInterCoolerFrontAirPressure { get; set; }

        /// <summary>
        /// A5缸排气温度
        /// </summary>
        public double A5CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// 后冷前空气温度
        /// </summary>
        public double RearCoolerFrontAirTemperature { get; set; }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class PLC2AIDataGrp
    {
        [JsonProperty("AI2_机油耗测量压力")]
        public double OilConsumptionMeasurePressure { get; set; }

        [JsonProperty("AI2_T21主油道进口油温")]
        public double T21MainOilChannelInletOilTemperature { get; set; }

        [JsonProperty("AI2_T5中冷水出机温度")]
        public double T5CoolWaterOutletTemperature { get; set; }

        [JsonProperty("AI2_B8缸排气温度")]
        public double B8CylinderExhaustTemperature { get; set; }

        [JsonProperty("AI2_测功机U相温度")]
        public double DynamometerUPhaseTemperature { get; set; }

        [JsonProperty("AI2_高温水泵出口压力")]
        public double HighTempWaterPumpOutletPressure { get; set; }

        [JsonProperty("AI2_后增压器进气真空度")]
        public double RearSuperchargerIntakeVacuumDegree { get; set; }

        [JsonProperty("AI2_B4缸排气温度")]
        public double B4CylinderExhaustTemperature { get; set; }

        [JsonProperty("AI2_中冷器进口水温")]
        public double InterCoolerInletWaterTemperature { get; set; }

        [JsonProperty("AI2_测功机D相温度")]
        public double DynamometerDPhaseTemperature { get; set; }

        [JsonProperty("AI2_A8缸排气温度")]
        public double A8CylinderExhaustTemperature { get; set; }

        [JsonProperty("AI2_励磁电压检测")]
        public double ExcitationVoltageDetect { get; set; }

        [JsonProperty("AI2_前增压器进气真空度")]
        public double FrontSuperchargerIntakeVacuumDegree { get; set; }

        [JsonProperty("AI2_P20机油泵出口压力")]
        public double P20OilPumpOutletPressure { get; set; }

        [JsonProperty("AI2_B7缸排气温度")]
        public double B7CylinderExhaustTemperature { get; set; }

        [JsonProperty("AI2_前中冷后空气压力")]
        public double FrontInterCoolerRearAirPressure { get; set; }

        [JsonProperty("AI2_B2缸排气温度")]
        public double B2CylinderExhaustTemperature { get; set; }

        [JsonProperty("AI2_主油道末端油压")]
        public double MainOilChannelEndOilPressure { get; set; }

        [JsonProperty("AI2_A4缸排气温度")]
        public double A4CylinderExhaustTemperature { get; set; }

        [JsonProperty("AI2_测功机W相温度")]
        public double DynamometerWPhaseTemperature { get; set; }

        [JsonProperty("AI2_T31燃油泵进口油温")]
        public double T31FuelPumpInletOilTemperature { get; set; }

        [JsonProperty("AI2_P21主油道进口油压")]
        public double P21MainOilChannelInletOilPressure { get; set; }

        [JsonProperty("AI2_测功机N相温度")]
        public double DynamometerNPhaseTemperature { get; set; }

        [JsonProperty("AI2_前中冷前空气温度")]
        public double FrontInterCoolerFrontAirTemperature { get; set; }

        [JsonProperty("AI2_A7缸排气温度")]
        public double A7CylinderExhaustTemperature { get; set; }

        [JsonProperty("AI2_前增压器机油进口压力")]
        public double FrontSuperchargerOilInletPressure { get; set; }

        [JsonProperty("AI2_A2缸排气温度")]
        public double A2CylinderExhaustTemperature { get; set; }

        [JsonProperty("AI2_中冷器出口水温")]
        public double InterCoolerOutletWaterTemperature { get; set; }

        [JsonProperty("AI2_B6缸排气温度")]
        public double B6CylinderExhaustTemperature { get; set; }

        [JsonProperty("AI2_前增压器机油进口温度")]
        public double FrontSuperchargerOilInletTemperature { get; set; }

        [JsonProperty("AI2_后增压器机油进口温度")]
        public double RearSuperchargerOilInletTemperature { get; set; }

        [JsonProperty("AI2_后增压器排气背压")]
        public double RearSuperchargerExhaustBackPressure { get; set; }

        [JsonProperty("AI2_后涡轮出口废气温度")]
        public double RearTurboOutletExhaustGasTemperature { get; set; }

        [JsonProperty("AI2_P38燃油供油压力")]
        public double P38FuelSupplyPressure { get; set; }

        [JsonProperty("AI2_前涡轮进口废气压力")]
        public double FrontTurboInletExhaustGasPressure { get; set; }

        [JsonProperty("AI2_后增压器机油进口压力")]
        public double RearSuperchargerOilInletPressure { get; set; }

        [JsonProperty("AI2_P2高温水泵进口压力")]
        public double P2HighTempWaterPumpInletPressure { get; set; }

        [JsonProperty("AI2_后中冷后空气压力")]
        public double RearInterCoolerRearAirPressure { get; set; }

        [JsonProperty("AI2_后增压器进气温度")]
        public double RearSuperchargerIntakeTemperature { get; set; }

        [JsonProperty("AI2_前中冷后空气温度")]
        public double FrontInterCoolerRearAirTemperature { get; set; }

        [JsonProperty("AI2_A6缸排气温度")]
        public double A6CylinderExhaustTemperature { get; set; }

        [JsonProperty("AI2_前增压器排气背压")]
        public double FrontSuperchargerExhaustBackPressure { get; set; }

        [JsonProperty("AI2_T20机油泵出口油温")]
        public double T20OilPumpOutletOilTemperature { get; set; }

        [JsonProperty("AI2_T1高温水出机温度")]
        public double T1HighTempWaterOutletTemperature { get; set; }

        [JsonProperty("AI2_后涡轮进口废气温度")]
        public double RearTurboInletExhaustGasTemperature { get; set; }

        [JsonProperty("AI2_后中冷前空气温度")]
        public double RearInterCoolerFrontAirTemperature { get; set; }

        [JsonProperty("AI2_励磁电流检测")]
        public double ExcitationCurrentDetect { get; set; }

        [JsonProperty("AI2_T2高温水进机温度")]
        public double T2HighTempWaterInletTemperature { get; set; }

        [JsonProperty("AI2_T3中冷水进机温度")]
        public double T3CoolWaterInletTemperature { get; set; }

        [JsonProperty("AI2_机油耗测量液位")]
        public double OilConsumptionMeasureLevel { get; set; }

        [JsonProperty("AI2_T30燃油回油温度")]
        public double T30FuelReturnOilTemperature { get; set; }

        [JsonProperty("AI2_B3缸排气温度")]
        public double B3CylinderExhaustTemperature { get; set; }

        [JsonProperty("AI2_后涡轮进口废气压力")]
        public double RearTurboInletExhaustGasPressure { get; set; }

        [JsonProperty("AI2_后中冷后空气温度")]
        public double RearInterCoolerRearAirTemperature { get; set; }

        [JsonProperty("AI2_中冷水泵出口压力")]
        public double CoolWaterPumpOutletPressure { get; set; }

        [JsonProperty("AI2_前中冷前空气压力")]
        public double FrontInterCoolerFrontAirPressure { get; set; }

        [JsonProperty("AI2_测功机V相温度")]
        public double DynamometerVPhaseTemperature { get; set; }

        [JsonProperty("AI2_B1缸排气温度")]
        public double B1CylinderExhaustTemperature { get; set; }

        [JsonProperty("AI2_前增压器机油出口温度")]
        public double FrontSuperchargerOilOutletTemperature { get; set; }

        [JsonProperty("AI2_后增压器机油出口温度")]
        public double RearSuperchargerOilOutletTemperature { get; set; }

        [JsonProperty("AI2_A3缸排气温度")]
        public double A3CylinderExhaustTemperature { get; set; }

        [JsonProperty("AI2_P3中冷水泵进口压力")]
        public double P3CoolWaterPumpInletPressure { get; set; }

        [JsonProperty("AI2_前涡轮出口废气温度")]
        public double FrontTurboOutletExhaustGasTemperature { get; set; }

        [JsonProperty("AI2_A1缸排气温度")]
        public double A1CylinderExhaustTemperature { get; set; }

        [JsonProperty("AI2_前增压器进气温度")]
        public double FrontSuperchargerIntakeTemperature { get; set; }

        [JsonProperty("AI2_P1高温水出机压力")]
        public double P1HighTempWaterOutletPressure { get; set; }

        [JsonProperty("AI2_B5缸排气温度")]
        public double B5CylinderExhaustTemperature { get; set; }

        [JsonProperty("AI2_P5中冷水出机压力")]
        public double P5CoolWaterOutletPressure { get; set; }

        [JsonProperty("AI2_前涡轮进口废气温度")]
        public double FrontTurboInletExhaustGasTemperature { get; set; }

        [JsonProperty("AI2_后中冷前空气压力")]
        public double RearInterCoolerFrontAirPressure { get; set; }

        [JsonProperty("AI2_A5缸排气温度")]
        public double A5CylinderExhaustTemperature { get; set; }
    }
}

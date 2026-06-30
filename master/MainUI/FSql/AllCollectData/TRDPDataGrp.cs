using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class TRDPDataGrp
    {
        [JsonProperty("TRDP_前增压器转速")]
        public double FrontTurbochargerRPM { get; set; }

        [JsonProperty("TRDP_后增压器转速")]
        public double AfterTurbochargerRPM { get; set; }

        [JsonProperty("TRDP_转速传感器1#")]
        public double RotateSpeedSensor1 { get; set; }

        [JsonProperty("TRDP_转速传感器2#")]
        public double RotateSpeedSensor2 { get; set; }

        [JsonProperty("TRDP_相位传感器")]
        public double PhaseSensor { get; set; }

        [JsonProperty("TRDP_转速设定")]
        public double RotateSpeedSet { get; set; }

        [JsonProperty("TRDP_燃油量")]
        public double FuelQuantity { get; set; }

        [JsonProperty("TRDP_电源A")]
        public double PowerA { get; set; }

        [JsonProperty("TRDP_电源B")]
        public double PowerB { get; set; }

        [JsonProperty("TRDP_紧急报警")]
        public double EmergencyAlarm { get; set; }

        [JsonProperty("TRDP_公共报警")]
        public double PublicAlarm { get; set; }

        [JsonProperty("TRDP_持续期")]
        public double Duration { get; set; }

        [JsonProperty("TRDP_提前角")]
        public double AdvanceAngle { get; set; }

        [JsonProperty("TRDP_同步状态")]
        public double SyncStatus { get; set; }

        [JsonProperty("TRDP_电源放大器A滤值")]
        public double PowerAmplifierAFilteredValue { get; set; }

        [JsonProperty("TRDP_电源放大器B滤值")]
        public double PowerAmplifierBFilteredValue { get; set; }

        [JsonProperty("TRDP_电源放大器C滤值")]
        public double PowerAmplifierCFilteredValue { get; set; }

        [JsonProperty("TRDP_电源放大器A实际值")]
        public double PowerAmplifierAActualValue { get; set; }

        [JsonProperty("TRDP_电源放大器B实际值")]
        public double PowerAmplifierBActualValue { get; set; }

        [JsonProperty("TRDP_电源放大器C实际值")]
        public double PowerAmplifierCActualValue { get; set; }

        [JsonProperty("TRDP_ECU运行时间")]
        public double ECURunTime { get; set; }

        [JsonProperty("TRDP_电磁阀故障1#")]
        public double SolenoidValveFault1 { get; set; }

        [JsonProperty("TRDP_电磁阀故障2#")]
        public double SolenoidValveFault2 { get; set; }

        [JsonProperty("TRDP_电磁阀故障3#")]
        public double SolenoidValveFault3 { get; set; }

        [JsonProperty("TRDP_电磁阀故障4#")]
        public double SolenoidValveFault4 { get; set; }

        [JsonProperty("TRDP_电磁阀故障5#")]
        public double SolenoidValveFault5 { get; set; }

        [JsonProperty("TRDP_电磁阀故障6#")]
        public double SolenoidValveFault6 { get; set; }

        [JsonProperty("TRDP_电磁阀故障7#")]
        public double SolenoidValveFault7 { get; set; }

        [JsonProperty("TRDP_电磁阀故障8#")]
        public double SolenoidValveFault8 { get; set; }

        [JsonProperty("TRDP_电磁阀故障9#")]
        public double SolenoidValveFault9 { get; set; }

        [JsonProperty("TRDP_电磁阀故障10#")]
        public double SolenoidValveFault10 { get; set; }

        [JsonProperty("TRDP_电磁阀故障11#")]
        public double SolenoidValveFault11 { get; set; }

        [JsonProperty("TRDP_电磁阀故障12#")]
        public double SolenoidValveFault12 { get; set; }

        [JsonProperty("TRDP_供电电源故障")]
        public double PowerSupplyFault { get; set; }

        [JsonProperty("TRDP_转速传感器故障1#")]
        public double RotateSpeedSensorFault1 { get; set; }

        [JsonProperty("TRDP_转速传感器故障2#")]
        public double RotateSpeedSensorFault2 { get; set; }

        [JsonProperty("TRDP_相位传感器故障")]
        public double PhaseSensorFault { get; set; }

        [JsonProperty("TRDP_超速故障")]
        public double OverSpeedFault { get; set; }

        [JsonProperty("TRDP_同步输入故障")]
        public double SyncInputFault { get; set; }

        [JsonProperty("TRDP_硬件故障")]
        public double HardwareFault { get; set; }

        [JsonProperty("TRDP_同步故障")]
        public double SyncFault { get; set; }

        [JsonProperty("TRDP_电压放大器故障A")]
        public double VoltageAmplifierFaultA { get; set; }

        [JsonProperty("TRDP_电压放大器故障B")]
        public double VoltageAmplifierFaultB { get; set; }

        [JsonProperty("TRDP_电压放大器故障C")]
        public double VoltageAmplifierFaultC { get; set; }

        [JsonProperty("TRDP_机油泵出口油压")]
        public double OilPumpOutletOilPressure { get; set; }

        [JsonProperty("TRDP_燃油精滤器前油压")]
        public double FuelFineFilterFrontOilPressure { get; set; }

        [JsonProperty("TRDP_燃油精滤器后油压")]
        public double FuelFineFilterRearOilPressure { get; set; }

        [JsonProperty("TRDP_前增压器进口油压")]
        public double FrontSuperchargerInletOilPressure { get; set; }

        [JsonProperty("TRDP_后增压器进口油压")]
        public double RearSuperchargerInletOilPressure { get; set; }

        [JsonProperty("TRDP_高温水泵出口压力")]
        public double HighTempWaterPumpOutletPressure { get; set; }

        [JsonProperty("TRDP_主油道末端油压")]
        public double MainOilChannelEndOilPressure { get; set; }

        [JsonProperty("TRDP_主油道进口油压")]
        public double MainOilChannelInletOilPressure { get; set; }

        [JsonProperty("TRDP_高温水出水温度")]
        public double HighTempWaterOutletTemperature { get; set; }

        [JsonProperty("TRDP_中冷水进水温度")]
        public double CoolWaterInletTemperature { get; set; }

        [JsonProperty("TRDP_中冷水出水温度")]
        public double CoolWaterOutletTemperature { get; set; }

        [JsonProperty("TRDP_主油道进口油温")]
        public double MainOilChannelInletOilTemperature { get; set; }

        [JsonProperty("TRDP_机油泵出口油温")]
        public double OilPumpOutletOilTemperature { get; set; }

        [JsonProperty("TRDP_中冷水泵出口压力")]
        public double CoolWaterPumpOutletPressure { get; set; }

        [JsonProperty("TRDP_前压气机出口空气温度")]
        public double FrontCompressorOutletAirTemperature { get; set; }

        [JsonProperty("TRDP_后压气机出口空气温度")]
        public double RearCompressorOutletAirTemperature { get; set; }

        [JsonProperty("TRDP_前增压器回油温度")]
        public double FrontSuperchargerReturnOilTemperature { get; set; }

        [JsonProperty("TRDP_后中冷器后空气温度")]
        public double RearInterCoolerRearAirTemperature { get; set; }

        [JsonProperty("TRDP_后增压器回油温度")]
        public double RearSuperchargerReturnOilTemperature { get; set; }

        [JsonProperty("TRDP_后中冷后空气压力")]
        public double RearInterCoolerRearAirPressure { get; set; }

        [JsonProperty("TRDP_A1缸排气温度")]
        public double A1CylinderExhaustTemperature { get; set; }

        [JsonProperty("TRDP_A2缸排气温度")]
        public double A2CylinderExhaustTemperature { get; set; }

        [JsonProperty("TRDP_A3缸排气温度")]
        public double A3CylinderExhaustTemperature { get; set; }

        [JsonProperty("TRDP_A4缸排气温度")]
        public double A4CylinderExhaustTemperature { get; set; }

        [JsonProperty("TRDP_A5缸排气温度")]
        public double A5CylinderExhaustTemperature { get; set; }

        [JsonProperty("TRDP_A6缸排气温度")]
        public double A6CylinderExhaustTemperature { get; set; }

        [JsonProperty("TRDP_A涡前排气温度")]
        public double AVortexFrontExhaustTemperature { get; set; }

        [JsonProperty("TRDP_B1缸排气温度")]
        public double B1CylinderExhaustTemperature { get; set; }

        [JsonProperty("TRDP_B2缸排气温度")]
        public double B2CylinderExhaustTemperature { get; set; }

        [JsonProperty("TRDP_B3缸排气温度")]
        public double B3CylinderExhaustTemperature { get; set; }

        [JsonProperty("TRDP_B4缸排气温度")]
        public double B4CylinderExhaustTemperature { get; set; }

        [JsonProperty("TRDP_B5缸排气温度")]
        public double B5CylinderExhaustTemperature { get; set; }

        [JsonProperty("TRDP_B6缸排气温度")]
        public double B6CylinderExhaustTemperature { get; set; }

        [JsonProperty("TRDP_B涡前排气温度")]
        public double BVortexFrontExhaustTemperature { get; set; }

        [JsonProperty("TRDP_一档轴温")]
        public double FirstGearShaftTemperature { get; set; }

        [JsonProperty("TRDP_二档轴温")]
        public double SecondGearShaftTemperature { get; set; }

        [JsonProperty("TRDP_三档轴温")]
        public double ThirdGearShaftTemperature { get; set; }

        [JsonProperty("TRDP_四档轴温")]
        public double FourthGearShaftTemperature { get; set; }

        [JsonProperty("TRDP_五档轴温")]
        public double FifthGearShaftTemperature { get; set; }

        [JsonProperty("TRDP_六档轴温")]
        public double SixthGearShaftTemperature { get; set; }

        [JsonProperty("TRDP_七档轴温")]
        public double SeventhGearShaftTemperature { get; set; }

        [JsonProperty("TRDP_设备生命信号")]
        public double DeviceLifeSignal { get; set; }

        [JsonProperty("TRDP_网口0故障")]
        public double NetworkPort0Fault { get; set; }

        [JsonProperty("TRDP_从站1串口故障")]
        public double Slave1SerialPortFault { get; set; }

        [JsonProperty("TRDP_从站2串口故障")]
        public double Slave2SerialPortFault { get; set; }

        [JsonProperty("TRDP_从站3串口故障")]
        public double Slave3SerialPortFault { get; set; }

        [JsonProperty("TRDP_从站4串口故障")]
        public double Slave4SerialPortFault { get; set; }

        [JsonProperty("TRDP_从站5串口故障")]
        public double Slave5SerialPortFault { get; set; }

        [JsonProperty("TRDP_从站6串口故障")]
        public double Slave6SerialPortFault { get; set; }

        [JsonProperty("TRDP_从站7串口故障")]
        public double Slave7SerialPortFault { get; set; }
    }
}

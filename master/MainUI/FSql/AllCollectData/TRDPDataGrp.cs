using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class TRDPDataGrp
    {
        /// <summary>
        ///前增压器转速
        /// </summary>
        public double FrontTurbochargerRPM { get; set; }


        /// <summary>
        ///后增压器转速
        /// </summary>
        public double AfterTurbochargerRPM { get; set; }

        /// <summary>
        /// 转速传感器1#
        /// </summary>
        public double RotateSpeedSensor1 { get; set; }

        /// <summary>
        /// 转速传感器2#
        /// </summary>
        public double RotateSpeedSensor2 { get; set; }

        /// <summary>
        /// 相位传感器
        /// </summary>
        public double PhaseSensor { get; set; }

        /// <summary>
        /// 转速设定
        /// </summary>
        public double RotateSpeedSet { get; set; }


        /// <summary>
        /// 电源放大器A滤值
        /// </summary>
        public double PowerAmplifierAFilteredValue { get; set; }

        /// <summary>
        /// 电源放大器B滤值
        /// </summary>
        public double PowerAmplifierBFilteredValue { get; set; }

        /// <summary>
        /// 电源放大器C滤值
        /// </summary>
        public double PowerAmplifierCFilteredValue { get; set; }

        /// <summary>
        /// 电源放大器A实际值
        /// </summary>
        public double PowerAmplifierAActualValue { get; set; }

        /// <summary>
        /// 电源放大器B实际值
        /// </summary>
        public double PowerAmplifierBActualValue { get; set; }

        /// <summary>
        /// 电源放大器C实际值
        /// </summary>
        public double PowerAmplifierCActualValue { get; set; }

        /// <summary>
        /// 燃油量
        /// </summary>
        public double FuelQuantity { get; set; }

        /// <summary>
        /// 电源A
        /// </summary>
        public double PowerA { get; set; }

        /// <summary>
        /// 电源B
        /// </summary>
        public double PowerB { get; set; }

        /// <summary>
        /// 紧急报警
        /// </summary>
        public double EmergencyAlarm { get; set; }

        /// <summary>
        /// 公共报警
        /// </summary>
        public double PublicAlarm { get; set; }

        /// <summary>
        /// 持续期
        /// </summary>
        public double Duration { get; set; }

        /// <summary>
        /// 提前角
        /// </summary>
        public double AdvanceAngle { get; set; }

        /// <summary>
        /// 同步状态
        /// </summary>
        public double SyncStatus { get; set; }

        /// <summary>
        /// ECU运行时间
        /// </summary>
        public double ECURunTime { get; set; }

        /// <summary>
        /// 电磁阀故障1#
        /// </summary>
        public double SolenoidValveFault1 { get; set; }

        /// <summary>
        /// 电磁阀故障2#
        /// </summary>
        public double SolenoidValveFault2 { get; set; }

        /// <summary>
        /// 电磁阀故障3#
        /// </summary>
        public double SolenoidValveFault3 { get; set; }

        /// <summary>
        /// 电磁阀故障4#
        /// </summary>
        public double SolenoidValveFault4 { get; set; }

        /// <summary>
        /// 电磁阀故障5#
        /// </summary>
        public double SolenoidValveFault5 { get; set; }

        /// <summary>
        /// 电磁阀故障6#
        /// </summary>
        public double SolenoidValveFault6 { get; set; }

        /// <summary>
        /// 电磁阀故障7#
        /// </summary>
        public double SolenoidValveFault7 { get; set; }

        /// <summary>
        /// 电磁阀故障8#
        /// </summary>
        public double SolenoidValveFault8 { get; set; }

        /// <summary>
        /// 电磁阀故障9#
        /// </summary>
        public double SolenoidValveFault9 { get; set; }

        /// <summary>
        /// 电磁阀故障10#
        /// </summary>
        public double SolenoidValveFault10 { get; set; }

        /// <summary>
        /// 电磁阀故障11#
        /// </summary>
        public double SolenoidValveFault11 { get; set; }

        /// <summary>
        /// 电磁阀故障12#
        /// </summary>
        public double SolenoidValveFault12 { get; set; }

        /// <summary>
        /// 供电电源故障
        /// </summary>
        public double PowerSupplyFault { get; set; }

        /// <summary>
        /// 转速传感器故障1#
        /// </summary>
        public double RotateSpeedSensorFault1 { get; set; }

        /// <summary>
        /// 转速传感器故障2#
        /// </summary>
        public double RotateSpeedSensorFault2 { get; set; }

        /// <summary>
        /// 相位传感器故障
        /// </summary>
        public double PhaseSensorFault { get; set; }

        /// <summary>
        /// 超速故障
        /// </summary>
        public double OverSpeedFault { get; set; }

        /// <summary>
        /// 同步输入故障
        /// </summary>
        public double SyncInputFault { get; set; }

        /// <summary>
        /// 硬件故障
        /// </summary>
        public double HardwareFault { get; set; }

        /// <summary>
        /// 同步故障
        /// </summary>
        public double SyncFault { get; set; }

        /// <summary>
        /// 电压放大器故障A
        /// </summary>
        public double VoltageAmplifierFaultA { get; set; }

        /// <summary>
        /// 电压放大器故障B
        /// </summary>
        public double VoltageAmplifierFaultB { get; set; }

        /// <summary>
        /// 电压放大器故障C
        /// </summary>
        public double VoltageAmplifierFaultC { get; set; }

        /// <summary>
        /// 机油泵出口油压
        /// </summary>
        public double OilPumpOutletOilPressure { get; set; }

        /// <summary>
        /// 燃油精滤器前油压
        /// </summary>
        public double FuelFineFilterFrontOilPressure { get; set; }

        /// <summary>
        /// 燃油精滤器后油压
        /// </summary>
        public double FuelFineFilterRearOilPressure { get; set; }

        /// <summary>
        /// 前增压器进口油压
        /// </summary>
        public double FrontSuperchargerInletOilPressure { get; set; }

        /// <summary>
        /// 后增压器进口油压
        /// </summary>
        public double RearSuperchargerInletOilPressure { get; set; }

        /// <summary>
        /// 高温水泵出口压力
        /// </summary>
        public double HighTempWaterPumpOutletPressure { get; set; }

        /// <summary>
        /// 主油道末端油压
        /// </summary>
        public double MainOilChannelEndOilPressure { get; set; }

        /// <summary>
        /// 主油道进口油压
        /// </summary>
        public double MainOilChannelInletOilPressure { get; set; }

        /// <summary>
        /// 高温水出水温度
        /// </summary>
        public double HighTempWaterOutletTemperature { get; set; }

        /// <summary>
        /// 中冷水进水温度
        /// </summary>
        public double CoolWaterInletTemperature { get; set; }

        /// <summary>
        /// 中冷水出水温度
        /// </summary>
        public double CoolWaterOutletTemperature { get; set; }

        /// <summary>
        /// 主油道进口油温
        /// </summary>
        public double MainOilChannelInletOilTemperature { get; set; }

        /// <summary>
        /// 机油泵出口油温
        /// </summary>
        public double OilPumpOutletOilTemperature { get; set; }

        /// <summary>
        /// 中冷水泵出口压力
        /// </summary>
        public double CoolWaterPumpOutletPressure { get; set; }

        /// <summary>
        /// 前压气机出口空气温度
        /// </summary>
        public double FrontCompressorOutletAirTemperature { get; set; }

        /// <summary>
        /// 后压气机出口空气温度
        /// </summary>
        public double RearCompressorOutletAirTemperature { get; set; }

        /// <summary>
        /// 前增压器回油温度
        /// </summary>
        public double FrontSuperchargerReturnOilTemperature { get; set; }

        /// <summary>
        /// 后中冷器后空气温度
        /// </summary>
        public double RearInterCoolerRearAirTemperature { get; set; }

        /// <summary>
        /// 后增压器回油温度
        /// </summary>
        public double RearSuperchargerReturnOilTemperature { get; set; }

        /// <summary>
        /// 后中冷后空气压力
        /// </summary>
        public double RearInterCoolerRearAirPressure { get; set; }

        /// <summary>
        /// A1缸排气温度
        /// </summary>
        public double A1CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// A2缸排气温度
        /// </summary>
        public double A2CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// A3缸排气温度
        /// </summary>
        public double A3CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// A4缸排气温度
        /// </summary>
        public double A4CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// A5缸排气温度
        /// </summary>
        public double A5CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// A6缸排气温度
        /// </summary>
        public double A6CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// A涡前排气温度
        /// </summary>
        public double AVortexFrontExhaustTemperature { get; set; }

        /// <summary>
        /// B1缸排气温度
        /// </summary>
        public double B1CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// B2缸排气温度
        /// </summary>
        public double B2CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// B3缸排气温度
        /// </summary>
        public double B3CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// B4缸排气温度
        /// </summary>
        public double B4CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// B5缸排气温度
        /// </summary>
        public double B5CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// B6缸排气温度
        /// </summary>
        public double B6CylinderExhaustTemperature { get; set; }

        /// <summary>
        /// B涡前排气温度
        /// </summary>
        public double BVortexFrontExhaustTemperature { get; set; }

        /// <summary>
        /// 一档轴温
        /// </summary>
        public double FirstGearShaftTemperature { get; set; }

        /// <summary>
        /// 二档轴温
        /// </summary>
        public double SecondGearShaftTemperature { get; set; }

        /// <summary>
        /// 三档轴温
        /// </summary>
        public double ThirdGearShaftTemperature { get; set; }

        /// <summary>
        /// 四档轴温
        /// </summary>
        public double FourthGearShaftTemperature { get; set; }

        /// <summary>
        /// 五档轴温
        /// </summary>
        public double FifthGearShaftTemperature { get; set; }

        /// <summary>
        /// 六档轴温
        /// </summary>
        public double SixthGearShaftTemperature { get; set; }

        /// <summary>
        /// 七档轴温
        /// </summary>
        public double SeventhGearShaftTemperature { get; set; }

        /// <summary>
        /// 设备生命信号
        /// </summary>
        public double DeviceLifeSignal { get; set; }

        /// <summary>
        /// 网口0故障
        /// </summary>
        public double NetworkPort0Fault { get; set; }

        /// <summary>
        /// 从站1串口故障
        /// </summary>
        public double Slave1SerialPortFault { get; set; }

        /// <summary>
        /// 从站2串口故障
        /// </summary>
        public double Slave2SerialPortFault { get; set; }

        /// <summary>
        /// 从站3串口故障
        /// </summary>
        public double Slave3SerialPortFault { get; set; }

        /// <summary>
        /// 从站4串口故障
        /// </summary>
        public double Slave4SerialPortFault { get; set; }

        /// <summary>
        /// 从站5串口故障
        /// </summary>
        public double Slave5SerialPortFault { get; set; }

        /// <summary>
        /// 从站6串口故障
        /// </summary>
        public double Slave6SerialPortFault { get; set; }

        /// <summary>
        /// 从站7串口故障
        /// </summary>
        public double Slave7SerialPortFault { get; set; }
    }
}

using MainUI.FSql;
using MainUI.FSql.AllCollectData;
using MainUI.FSql.Model;
using MiniExcelLibs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Report
{
    /// <summary>
    /// 增加表格字段
    /// 
    /// </summary>
    public class ucAllDataRecord_Method
    {
        /// <summary>
        /// pym
        /// 1、添加表格字段
        /// 2、根据json里的对应数据，加入到对应的类中
        /// </summary>
        /// <param name="keyNameList"></param>
        public  List<ColumnDefinition> AddtcolumnDefinitions(List<string> keyNameList, List<ColumnDefinition> _columnDefinitions)
        {
            if (keyNameList.Count != 0 && keyNameList != null)
            {

                if (keyNameList.Any(key => key == "BaseDataGrp"))
                {
                    _columnDefinitions.Add(new ColumnDefinition("Weight", "重量"));
                    _columnDefinitions.Add(new ColumnDefinition("RPM", "发动机转速 rpm"));
                    _columnDefinitions.Add(new ColumnDefinition("Torque", "扭矩 N.m"));
                    _columnDefinitions.Add(new ColumnDefinition("Power", "功率 kW"));
                }
                if (keyNameList.Any(key => key == "TRDPDataGrp"))
                {
                    _columnDefinitions.Add(new ColumnDefinition("FrontTurbochargerRPM", "前增压器转速 rpm"));
                    _columnDefinitions.Add(new ColumnDefinition("AfterTurbochargerRPM", "后增压器转速 rpm"));
                    _columnDefinitions.Add(new ColumnDefinition("RotateSpeedSensor1", "转速传感器1#"));
                    _columnDefinitions.Add(new ColumnDefinition("RotateSpeedSensor2", "转速传感器2#"));
                    _columnDefinitions.Add(new ColumnDefinition("PhaseSensor", "相位传感器"));
                    _columnDefinitions.Add(new ColumnDefinition("RotateSpeedSet", "转速设定"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelQuantity", "燃油量"));
                    _columnDefinitions.Add(new ColumnDefinition("PowerA", "电源A"));
                    _columnDefinitions.Add(new ColumnDefinition("PowerB", "电源B"));
                    _columnDefinitions.Add(new ColumnDefinition("EmergencyAlarm", "紧急报警"));
                    _columnDefinitions.Add(new ColumnDefinition("PublicAlarm", "公共报警"));
                    _columnDefinitions.Add(new ColumnDefinition("Duration", "持续期"));
                    _columnDefinitions.Add(new ColumnDefinition("AdvanceAngle", "提前角"));
                    _columnDefinitions.Add(new ColumnDefinition("SyncStatus", "同步状态"));

                    _columnDefinitions.Add(new ColumnDefinition("PowerAmplifierAFilteredValue", "电源放大器A滤值"));
                    _columnDefinitions.Add(new ColumnDefinition("PowerAmplifierBFilteredValue", "电源放大器B滤值"));
                    _columnDefinitions.Add(new ColumnDefinition("PowerAmplifierCFilteredValue", "电源放大器C滤值"));
                    _columnDefinitions.Add(new ColumnDefinition("PowerAmplifierAActualValue", "电源放大器A实际值"));
                    _columnDefinitions.Add(new ColumnDefinition("PowerAmplifierBActualValue", "电源放大器B实际值"));
                    _columnDefinitions.Add(new ColumnDefinition("PowerAmplifierCActualValue", "电源放大器C实际值"));

                    _columnDefinitions.Add(new ColumnDefinition("ECURunTime", "ECU运行时间"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault1", "电磁阀故障1#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault2", "电磁阀故障2#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault3", "电磁阀故障3#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault4", "电磁阀故障4#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault5", "电磁阀故障5#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault6", "电磁阀故障6#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault7", "电磁阀故障7#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault8", "电磁阀故障8#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault9", "电磁阀故障9#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault10", "电磁阀故障10#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault11", "电磁阀故障11#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault12", "电磁阀故障12#"));
                    _columnDefinitions.Add(new ColumnDefinition("PowerSupplyFault", "供电电源故障"));
                    _columnDefinitions.Add(new ColumnDefinition("RotateSpeedSensorFault1", "转速传感器故障1#"));
                    _columnDefinitions.Add(new ColumnDefinition("RotateSpeedSensorFault2", "转速传感器故障2#"));
                    _columnDefinitions.Add(new ColumnDefinition("PhaseSensorFault", "相位传感器故障"));
                    _columnDefinitions.Add(new ColumnDefinition("OverSpeedFault", "超速故障"));
                    _columnDefinitions.Add(new ColumnDefinition("SyncInputFault", "同步输入故障"));
                    _columnDefinitions.Add(new ColumnDefinition("HardwareFault", "硬件故障"));
                    _columnDefinitions.Add(new ColumnDefinition("SyncFault", "同步故障"));
                    _columnDefinitions.Add(new ColumnDefinition("VoltageAmplifierFaultA", "电压放大器故障A"));
                    _columnDefinitions.Add(new ColumnDefinition("VoltageAmplifierFaultB", "电压放大器故障B"));
                    _columnDefinitions.Add(new ColumnDefinition("VoltageAmplifierFaultC", "电压放大器故障C"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPumpOutletOilPressure", "机油泵出口油压"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelFineFilterFrontOilPressure", "燃油精滤器前油压"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelFineFilterRearOilPressure", "燃油精滤器后油压"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontSuperchargerInletOilPressure", "前增压器进口油压"));
                    _columnDefinitions.Add(new ColumnDefinition("RearSuperchargerInletOilPressure", "后增压器进口油压"));
                    _columnDefinitions.Add(new ColumnDefinition("HighTempWaterPumpOutletPressure", "高温水泵出口压力"));
                    _columnDefinitions.Add(new ColumnDefinition("MainOilChannelEndOilPressure", "主油道末端油压"));
                    _columnDefinitions.Add(new ColumnDefinition("MainOilChannelInletOilPressure", "主油道进口油压"));
                    _columnDefinitions.Add(new ColumnDefinition("HighTempWaterOutletTemperature", "高温水出水温度"));
                    _columnDefinitions.Add(new ColumnDefinition("CoolWaterInletTemperature", "中冷水进水温度"));
                    _columnDefinitions.Add(new ColumnDefinition("CoolWaterOutletTemperature", "中冷水出水温度"));
                    _columnDefinitions.Add(new ColumnDefinition("MainOilChannelInletOilTemperature", "主油道进口油温"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPumpOutletOilTemperature", "机油泵出口油温"));
                    _columnDefinitions.Add(new ColumnDefinition("CoolWaterPumpOutletPressure", "中冷水泵出口压力"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontCompressorOutletAirTemperature", "前压气机出口空气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("RearCompressorOutletAirTemperature", "后压气机出口空气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontSuperchargerReturnOilTemperature", "前增压器回油温度"));
                    _columnDefinitions.Add(new ColumnDefinition("RearInterCoolerRearAirTemperature", "后中冷器后空气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("RearSuperchargerReturnOilTemperature", "后增压器回油温度"));
                    _columnDefinitions.Add(new ColumnDefinition("RearInterCoolerRearAirPressure", "后中冷后空气压力"));
                    _columnDefinitions.Add(new ColumnDefinition("A1CylinderExhaustTemperature", "A1缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("A2CylinderExhaustTemperature", "A2缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("A3CylinderExhaustTemperature", "A3缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("A4CylinderExhaustTemperature", "A4缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("A5CylinderExhaustTemperature", "A5缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("A6CylinderExhaustTemperature", "A6缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("AVortexFrontExhaustTemperature", "A涡前排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("B1CylinderExhaustTemperature", "B1缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("B2CylinderExhaustTemperature", "B2缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("B3CylinderExhaustTemperature", "B3缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("B4CylinderExhaustTemperature", "B4缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("B5CylinderExhaustTemperature", "B5缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("B6CylinderExhaustTemperature", "B6缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("BVortexFrontExhaustTemperature", "B涡前排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("FirstGearShaftTemperature", "一档轴温"));
                    _columnDefinitions.Add(new ColumnDefinition("SecondGearShaftTemperature", "二档轴温"));
                    _columnDefinitions.Add(new ColumnDefinition("ThirdGearShaftTemperature", "三档轴温"));
                    _columnDefinitions.Add(new ColumnDefinition("FourthGearShaftTemperature", "四档轴温"));
                    _columnDefinitions.Add(new ColumnDefinition("FifthGearShaftTemperature", "五档轴温"));
                    _columnDefinitions.Add(new ColumnDefinition("SixthGearShaftTemperature", "六档轴温"));
                    _columnDefinitions.Add(new ColumnDefinition("SeventhGearShaftTemperature", "七档轴温"));
                    _columnDefinitions.Add(new ColumnDefinition("DeviceLifeSignal", "设备生命信号"));
                    _columnDefinitions.Add(new ColumnDefinition("NetworkPort0Fault", "网口0故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Slave1SerialPortFault", "从站1串口故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Slave2SerialPortFault", "从站2串口故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Slave3SerialPortFault", "从站3串口故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Slave4SerialPortFault", "从站4串口故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Slave5SerialPortFault", "从站5串口故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Slave6SerialPortFault", "从站6串口故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Slave7SerialPortFault", "从站7串口故障"));

                }
                if (keyNameList.Any(key => key == "TRDPData1Grp"))
                {
                    _columnDefinitions.Add(new ColumnDefinition("DieselEngineRPM", "柴油机转速 rpm"));
                    _columnDefinitions.Add(new ColumnDefinition("RotateSpeedSensor1", "转速传感器1#"));
                    _columnDefinitions.Add(new ColumnDefinition("RotateSpeedSensor2", "转速传感器2#"));
                    _columnDefinitions.Add(new ColumnDefinition("PhaseSensor", "相位传感器"));
                    _columnDefinitions.Add(new ColumnDefinition("RotateSpeedSet", "转速设定"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelQuantity", "燃油量"));
                    _columnDefinitions.Add(new ColumnDefinition("PowerA", "电源A"));
                    _columnDefinitions.Add(new ColumnDefinition("PowerB", "电源B"));
                    _columnDefinitions.Add(new ColumnDefinition("EmergencyAlarm", "紧急报警"));
                    _columnDefinitions.Add(new ColumnDefinition("PublicAlarm", "公共报警"));
                    _columnDefinitions.Add(new ColumnDefinition("Duration", "持续期"));
                    _columnDefinitions.Add(new ColumnDefinition("AdvanceAngle", "提前角"));
                    _columnDefinitions.Add(new ColumnDefinition("SyncStatus", "同步状态"));

                    _columnDefinitions.Add(new ColumnDefinition("PowerAmplifierAFilteredValue", "电源放大器A滤值"));
                    _columnDefinitions.Add(new ColumnDefinition("PowerAmplifierBFilteredValue", "电源放大器B滤值"));
                    _columnDefinitions.Add(new ColumnDefinition("PowerAmplifierCFilteredValue", "电源放大器C滤值"));
                    _columnDefinitions.Add(new ColumnDefinition("PowerAmplifierAActualValue", "电源放大器A实际值"));
                    _columnDefinitions.Add(new ColumnDefinition("PowerAmplifierBActualValue", "电源放大器B实际值"));
                    _columnDefinitions.Add(new ColumnDefinition("PowerAmplifierCActualValue", "电源放大器C实际值"));

                    _columnDefinitions.Add(new ColumnDefinition("ECURunTime", "ECU运行时间"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault1", "电磁阀故障1#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault2", "电磁阀故障2#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault3", "电磁阀故障3#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault4", "电磁阀故障4#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault5", "电磁阀故障5#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault6", "电磁阀故障6#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault7", "电磁阀故障7#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault8", "电磁阀故障8#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault9", "电磁阀故障9#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault10", "电磁阀故障10#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault11", "电磁阀故障11#"));
                    _columnDefinitions.Add(new ColumnDefinition("SolenoidValveFault12", "电磁阀故障12#"));
                    _columnDefinitions.Add(new ColumnDefinition("PowerSupplyFault", "供电电源故障"));
                    _columnDefinitions.Add(new ColumnDefinition("RotateSpeedSensorFault1", "转速传感器故障1#"));
                    _columnDefinitions.Add(new ColumnDefinition("RotateSpeedSensorFault2", "转速传感器故障2#"));
                    _columnDefinitions.Add(new ColumnDefinition("PhaseSensorFault", "相位传感器故障"));
                    _columnDefinitions.Add(new ColumnDefinition("OverSpeedFault", "超速故障"));
                    _columnDefinitions.Add(new ColumnDefinition("SyncInputFault", "同步输入故障"));
                    _columnDefinitions.Add(new ColumnDefinition("HardwareFault", "硬件故障"));
                    _columnDefinitions.Add(new ColumnDefinition("SyncFault", "同步故障"));
                    _columnDefinitions.Add(new ColumnDefinition("VoltageAmplifierFaultA", "电压放大器故障A"));
                    _columnDefinitions.Add(new ColumnDefinition("VoltageAmplifierFaultB", "电压放大器故障B"));
                    _columnDefinitions.Add(new ColumnDefinition("VoltageAmplifierFaultC", "电压放大器故障C"));

                    _columnDefinitions.Add(new ColumnDefinition("FrontTurbochargerInletOilPressure1", "前增压器进油压1"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontTurbochargerInletOilPressure2", "前增压器进油压2"));
                    _columnDefinitions.Add(new ColumnDefinition("RearTurbochargerInletOilPressure1", "后增压器进油压1"));
                    _columnDefinitions.Add(new ColumnDefinition("RearTurbochargerInletOilPressure2", "后增压器进油压2"));
                    _columnDefinitions.Add(new ColumnDefinition("MainOilChannelInletOilPressure", "主油道进口油压"));
                    _columnDefinitions.Add(new ColumnDefinition("MainOilChannelEndOilPressure", "主油道末端油压"));
                    _columnDefinitions.Add(new ColumnDefinition("CoolWaterPumpOutletPressure", "中冷水泵出口压力"));
                    _columnDefinitions.Add(new ColumnDefinition("HighTempWaterPumpOutletPressure", "高温水泵出口压力"));
                    _columnDefinitions.Add(new ColumnDefinition("IntercoolerRearAirPressure", "中冷后空气压力"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelFineFilterRearOilPressure", "燃油精滤器后油压"));
                    _columnDefinitions.Add(new ColumnDefinition("CrankcasePressure1", "曲轴箱压力1"));
                    _columnDefinitions.Add(new ColumnDefinition("CrankcasePressure2", "曲轴箱压力2"));
                    _columnDefinitions.Add(new ColumnDefinition("CylinderHeadOutletWaterTemperature", "气缸盖出水温度"));
                    _columnDefinitions.Add(new ColumnDefinition("CoolWaterInletTemperature", "中冷水进水温度"));
                    _columnDefinitions.Add(new ColumnDefinition("MainOilChannelInletOilTemperature", "主油道进口油温"));
                    _columnDefinitions.Add(new ColumnDefinition("IntercoolerRearAirTemperature", "中冷后空气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontTurbochargerRPM", "前增压器转速 rpm"));
                    _columnDefinitions.Add(new ColumnDefinition("AfterTurbochargerRPM", "后增压器转速 rpm"));

                    _columnDefinitions.Add(new ColumnDefinition("A1CylinderExhaustTemperature", "A1缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("A2CylinderExhaustTemperature", "A2缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("A3CylinderExhaustTemperature", "A3缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("A4CylinderExhaustTemperature", "A4缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("A5CylinderExhaustTemperature", "A5缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("A6CylinderExhaustTemperature", "A6缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("A7CylinderExhaustTemperature", "A7缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("A8CylinderExhaustTemperature", "A8缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("AVortexFrontExhaustTemperature", "A涡前排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("B1CylinderExhaustTemperature", "B1缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("B2CylinderExhaustTemperature", "B2缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("B3CylinderExhaustTemperature", "B3缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("B4CylinderExhaustTemperature", "B4缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("B5CylinderExhaustTemperature", "B5缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("B6CylinderExhaustTemperature", "B6缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("B7CylinderExhaustTemperature", "B7缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("B8CylinderExhaustTemperature", "B8缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("BVortexFrontExhaustTemperature", "B涡前排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("FirstGearShaftTemperature", "一档轴温"));
                    _columnDefinitions.Add(new ColumnDefinition("SecondGearShaftTemperature", "二档轴温"));
                    _columnDefinitions.Add(new ColumnDefinition("ThirdGearShaftTemperature", "三档轴温"));
                    _columnDefinitions.Add(new ColumnDefinition("FourthGearShaftTemperature", "四档轴温"));
                    _columnDefinitions.Add(new ColumnDefinition("FifthGearShaftTemperature", "五档轴温"));
                    _columnDefinitions.Add(new ColumnDefinition("SixthGearShaftTemperature", "六档轴温"));
                    _columnDefinitions.Add(new ColumnDefinition("SeventhGearShaftTemperature", "七档轴温"));
                    _columnDefinitions.Add(new ColumnDefinition("DeviceLifeSignal", "设备生命信号"));
                    _columnDefinitions.Add(new ColumnDefinition("NetworkPort0Fault", "网口0故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Slave1SerialPortFault", "从站1串口故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Slave2SerialPortFault", "从站2串口故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Slave3SerialPortFault", "从站3串口故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Slave4SerialPortFault", "从站4串口故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Slave5SerialPortFault", "从站5串口故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Slave6SerialPortFault", "从站6串口故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Slave7SerialPortFault", "从站7串口故障"));
                }
                if (keyNameList.Any(key => key == "AIDataGrp"))
                {
                    _columnDefinitions.Add(new ColumnDefinition("AT", "环境温度 ℃"));
                    _columnDefinitions.Add(new ColumnDefinition("AP", "大气压力 kPa"));
                    _columnDefinitions.Add(new ColumnDefinition("AH", "空气湿度 %RH"));
                    _columnDefinitions.Add(new ColumnDefinition("HWaterFlow", "高温水流量测量-L3"));
                    _columnDefinitions.Add(new ColumnDefinition("InAirFlowLeft", "进气流量测量左"));
                    _columnDefinitions.Add(new ColumnDefinition("LWaterFlow", "中冷水流量测量"));
                    _columnDefinitions.Add(new ColumnDefinition("LWaterBoxLevel", "中冷水膨胀水箱液位检测"));
                    _columnDefinitions.Add(new ColumnDefinition("cleanOilFlow", "清洁油罐来油流量"));
                    _columnDefinitions.Add(new ColumnDefinition("EngineOilFlow", "机油流量"));
                    _columnDefinitions.Add(new ColumnDefinition("OilFlow_L31", "燃油回油流量测量-L31"));
                    _columnDefinitions.Add(new ColumnDefinition("InAirFlowRight", "进气流量测量右"));
                    _columnDefinitions.Add(new ColumnDefinition("BeforeInAirTemp", "前增压器进气流量计前温度"));
                    _columnDefinitions.Add(new ColumnDefinition("AfterInAirTemp", "后增压器进气流量计前温度"));
                    _columnDefinitions.Add(new ColumnDefinition("FactoryAirPressureIn1", "厂房进气压力检测1"));
                    _columnDefinitions.Add(new ColumnDefinition("FactoryAirPressureIn2", "厂房进气压力检测2"));
                    _columnDefinitions.Add(new ColumnDefinition("L32", "L32"));
                    _columnDefinitions.Add(new ColumnDefinition("OilFlowIn_L30", "燃油进油流量测量-L30"));
                    _columnDefinitions.Add(new ColumnDefinition("HWaterBoxLevel", "高温水膨胀水箱液位检测"));
                }
                if (keyNameList.Any(key => key == "AODataGrp"))
                {
                    _columnDefinitions.Add(new ColumnDefinition("AirOutChannelValve", "排气风道右调节阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("EngineLowSpeed", "设置发动机最低转速"));
                    _columnDefinitions.Add(new ColumnDefinition("WaterOutValve_18", "水泵出口电动调节阀控制-18"));
                    _columnDefinitions.Add(new ColumnDefinition("EngineOilValve", "发动机油门调节"));
                    _columnDefinitions.Add(new ColumnDefinition("AirOutFlowValveLeft", "排气风道左调节阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Excitation", "_励磁调节"));
                    _columnDefinitions.Add(new ColumnDefinition("AO_OilBy_Pass1Valve_194", "燃油泵旁路1电动调节阀控制-194"));
                    _columnDefinitions.Add(new ColumnDefinition("AirInFlowleftValve", "进气风道左调节阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Oil1Vlave_170", "燃油泵1电动调节阀控制-170"));
                    _columnDefinitions.Add(new ColumnDefinition("AirFlowRightValve", "进气风道右调节阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("WaterResistanceBoxValve", "水阻箱进水电动调节阀"));
                }
                if (keyNameList.Any(key => key == "DIDataGrp"))
                {
                    _columnDefinitions.Add(new ColumnDefinition("OilByPass1ValveLocal_194", "燃油泵旁路1电动调节阀就地-194"));
                    _columnDefinitions.Add(new ColumnDefinition("DrawMidCoolWaterOpenLimit_31", "抽中冷水开到位-31"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankHeaterCtrl5Detect", "预热水箱加热器控制5检测"));
                    _columnDefinitions.Add(new ColumnDefinition("OilProcessTankOutOpenLimit_136", "机油处理箱出开到位-136"));
                    _columnDefinitions.Add(new ColumnDefinition("DieselTankOilInOpenLimit_164", "柴油箱进油开到位-164"));
                    _columnDefinitions.Add(new ColumnDefinition("PumpAndTankWaterOutOpenLimit_20", "泵和水箱出水开到位-20"));
                    _columnDefinitions.Add(new ColumnDefinition("CoolerOilReturnCloseLimit_90", "冷却器回油关到位-90"));
                    _columnDefinitions.Add(new ColumnDefinition("PendingOilTankSupplementOpenLimit_139", "待处理机油箱补油开到位-139"));
                    _columnDefinitions.Add(new ColumnDefinition("DieselEngineStart", "柴油机启动"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPumpOilInOpenLimit_96", "油泵进油开到位-96"));
                    _columnDefinitions.Add(new ColumnDefinition("OilCoolerWaterInValveFault_89", "机油冷却器进水电动调节阀故障-89"));
                    _columnDefinitions.Add(new ColumnDefinition("ExhaustDuctRightValveClose", "排气风道右调节阀关"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankWaterOutOpenLimit_15", "预热水箱出水开到位-15"));
                    _columnDefinitions.Add(new ColumnDefinition("HighTempWaterExpTankOutCloseLimit_30", "高温水膨胀水箱出关到位-30"));
                    _columnDefinitions.Add(new ColumnDefinition("PendingOilTankConnectProcessorOpenLimit_134", "待处理机油箱通处理机开到位-134"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPumpPreSupplyCloseLimit_93", "机油泵预供油关到位-93"));
                    _columnDefinitions.Add(new ColumnDefinition("CoolWaterInCloseLimit_61", "冷却水进水关到位-61"));
                    _columnDefinitions.Add(new ColumnDefinition("EmergencyStop2OpenLimit_182", "紧急停车2开到位-182"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPumpOilOutCloseLimit_97", "油泵出油关到位-97"));
                    _columnDefinitions.Add(new ColumnDefinition("DrawOilPumpThermalRelayDetect", "抽油泵热继检测"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTankOilOutOpenLimit_111", "机油箱出油开到位-111"));
                    _columnDefinitions.Add(new ColumnDefinition("ControlRoom2CabFrontDoorDetect", "控制间2柜前门检测"));
                    _columnDefinitions.Add(new ColumnDefinition("EmergencyStop1OpenLimit_181", "紧急停车1开到位-181"));
                    _columnDefinitions.Add(new ColumnDefinition("MainFanPower", "主发通风机电源"));
                    _columnDefinitions.Add(new ColumnDefinition("HighTempWaterExpTankOutOpenLimit_30", "高温水膨胀水箱出开到位-30"));
                    _columnDefinitions.Add(new ColumnDefinition("HighTempWaterCloseLimit_3", "高温水关到位-3"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPumpPreSupplyOpenLimit_93", "机油泵预供油开到位-93"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPumpPipeOilInCloseLimit_95", "油泵管路进油关到位-95"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatPumpYDetect", "预热水泵Y检测"));
                    _columnDefinitions.Add(new ColumnDefinition("DieselTankOilInCloseLimit_164", "柴油箱进油关到位-164"));
                    _columnDefinitions.Add(new ColumnDefinition("MainFan1YDetect", "主发通风机1Y检测"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTankPumpOilInOpenLimit_115", "机油箱油泵来油开到位-115"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankWaterReturnCloseLimit_24", "预热水箱回水关到位-24"));
                    _columnDefinitions.Add(new ColumnDefinition("OilByPass1ValveFullyClose_194", "燃油泵旁路1电动调节阀全关-194"));
                    _columnDefinitions.Add(new ColumnDefinition("OilByPass1ValveFullyOpen_194", "燃油泵旁路1电动调节阀全开-194"));
                    _columnDefinitions.Add(new ColumnDefinition("MainFan2Detect", "主发通风机2检测"));
                    _columnDefinitions.Add(new ColumnDefinition("StartCabinetPowerDetect", "启动柜电源检测"));
                    _columnDefinitions.Add(new ColumnDefinition("MainFan1Detect", "主发通风机1检测"));
                    _columnDefinitions.Add(new ColumnDefinition("PendingOilTankOilInOpenLimit_116", "待处理机油箱进油开到位-116"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPumpOilInCloseLimit_96", "油泵进油关到位-96"));
                    _columnDefinitions.Add(new ColumnDefinition("DieselEngineUnload", "柴油机卸载"));
                    _columnDefinitions.Add(new ColumnDefinition("MidCoolWaterExpTankHighLevel", "中冷水膨胀水箱高液位"));
                    _columnDefinitions.Add(new ColumnDefinition("OilSumpFillCloseLimit_122", "油底壳加油关到位-122"));
                    _columnDefinitions.Add(new ColumnDefinition("WaterResistLiftLowerLimitDetect", "水阻升降下极限检测"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPumpPreHeatPumpPowerDetect", "燃油泵/预热水泵电源检测"));
                    _columnDefinitions.Add(new ColumnDefinition("OilByPass1ValveFault_194", "燃油泵旁路1电动调节阀故障-194"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontOilFilter1CloseLimit_91", "机滤器前1关到位-91"));
                    _columnDefinitions.Add(new ColumnDefinition("EmergencyStop", "紧急停止"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPump1ValveLocal_170", "燃油泵1电动调节阀就地-170"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelMeterCommuteInCloseLimit_183", "油耗仪换向进关到位-183"));
                    _columnDefinitions.Add(new ColumnDefinition("EquipmentRoomPowerDetect", "设备间电源检测"));
                    _columnDefinitions.Add(new ColumnDefinition("MidCoolWaterInValveFault_88", "中冷水冷却水进口电动调节阀故障-88"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPumpOilOutOpenLimit_97", "油泵出油开到位-97"));
                    _columnDefinitions.Add(new ColumnDefinition("ControlPowerDetect", "控制电源检测"));
                    _columnDefinitions.Add(new ColumnDefinition("MidCoolWaterCircOpenLimit_41", "中冷水循环开到位-41"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTankCleanOilInCloseLimit_137", "机油箱清洁来油关到位-137"));
                    _columnDefinitions.Add(new ColumnDefinition("ExhaustDuctLeftValveOpen", "排气风道左调节阀开"));
                    _columnDefinitions.Add(new ColumnDefinition("MainExcitationCabPower", "主发励磁柜电源"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankWaterFillOpenLimit_27", "预热水箱加水开到位-27"));
                    _columnDefinitions.Add(new ColumnDefinition("OilProcessTankInOpenLimit_135", "机油处理箱进开到位-135"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPreSupplyPumpStop", "机油预供泵停止"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankHeaterCtrl2Detect", "预热水箱加热器控制2检测"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankHeaterCtrl1Detect", "预热水箱加热器控制1检测"));
                    _columnDefinitions.Add(new ColumnDefinition("WaterPumpOutValveFullyOpen_18", "水泵出口电动调节阀全开-18"));
                    _columnDefinitions.Add(new ColumnDefinition("WasteOilDrainPumpYDetect", "污油排出泵Y检测"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankHeaterCtrl4Detect", "预热水箱加热器控制4检测"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPump1ValveFault_170", "燃油泵1电动调节阀故障-170"));
                    _columnDefinitions.Add(new ColumnDefinition("OilReturnOpenLimit_179", "回油开到位-179"));
                    _columnDefinitions.Add(new ColumnDefinition("InnerCircWaterInOpenLimit_28", "内循环水来水开到位-28"));
                    _columnDefinitions.Add(new ColumnDefinition("WaterResistLiftUpperLimitDetect", "水阻升降上极限检测"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelMeterCommuteReturnCloseLimit_184", "油耗仪换向回关到位-184"));
                    _columnDefinitions.Add(new ColumnDefinition("IntakeDuctRightValveOpen", "进气风道右调节阀开"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelFillStart", "燃油加油开始"));
                    _columnDefinitions.Add(new ColumnDefinition("ExhaustDuctRightValveFault", "排气风道右调节阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("EquipmentRoomCabFrontDoorDetect", "设备间控制柜体前门检测"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankPowerDetect", "预热水箱电源检测"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontOilFilter2CloseLimit_100", "机滤器前2关到位-100"));
                    _columnDefinitions.Add(new ColumnDefinition("PumpAndTankFrontWaterReturnCloseLimit_23", "泵和水箱前回水关到位-23"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankWaterReturnOpenLimit_24", "预热水箱回水开到位-24"));
                    _columnDefinitions.Add(new ColumnDefinition("WaterResistLiftMotorUpClosingDetect", "水阻升降电机上升合闸检测"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPreSupplyPumpPowerDetect", "预供机油泵电源检测"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatPumpInOpenLimit_16", "预热水泵进口开到位-16"));
                    _columnDefinitions.Add(new ColumnDefinition("OilSumpFillOpenLimit_122", "油底壳加油开到位-122"));
                    _columnDefinitions.Add(new ColumnDefinition("DrawHighTempWaterOpenLimit_21", "抽高温水开到位-21"));
                    _columnDefinitions.Add(new ColumnDefinition("ExhaustDuctRightValveOpen", "排气风道右调节阀开"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankWaterFillCloseLimit_26", "预热水箱加水关到位-26"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontOilFilter1OpenLimit_91", "机滤器前1开到位-91"));
                    _columnDefinitions.Add(new ColumnDefinition("WasteOilDrainPumpThermalRelayDetect", "污油排出泵热继检测"));
                    _columnDefinitions.Add(new ColumnDefinition("IntakeDuctLeftValveClose", "进气风道左调节阀关"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankWaterOutCloseLimit_15", "预热水箱出水关到位-15"));
                    _columnDefinitions.Add(new ColumnDefinition("OilSumpDrawOilCloseLimit_92", "油底壳抽油关到位-92"));
                    _columnDefinitions.Add(new ColumnDefinition("BarringInterlockSwitch", "盘车连锁开关"));
                    _columnDefinitions.Add(new ColumnDefinition("PendingOilTankOilInCloseLimit_116", "待处理机油箱进油关到位-116"));
                    _columnDefinitions.Add(new ColumnDefinition("OilOutlet1CloseLimit_190", "出油路1关到位-190"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatPumpThermalRelayDetect", "预热水泵热继检测"));
                    _columnDefinitions.Add(new ColumnDefinition("MidCoolWaterExpTankLowLevel", "中冷水膨胀水箱低液位"));
                    _columnDefinitions.Add(new ColumnDefinition("PumpAndTankFrontWaterReturnOpenLimit_23", "泵和水箱前回水开到位-23"));
                    _columnDefinitions.Add(new ColumnDefinition("WasteOilDrainPumpMainContactDetect", "污油排出泵主接检测"));
                    _columnDefinitions.Add(new ColumnDefinition("InnerCircWaterInCloseLimit_28", "内循环水来水关到位-28"));
                    _columnDefinitions.Add(new ColumnDefinition("OilProcessTankOutCloseLimit_136", "机油处理箱出关到位-136"));
                    _columnDefinitions.Add(new ColumnDefinition("IntakeHeaterPowerDetect", "进气加热电源检测"));
                    _columnDefinitions.Add(new ColumnDefinition("MainFan1MainContactDetect", "主发通风机1主接检测"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatPumpInCloseLimit_16", "预热水泵进口关到位-16"));
                    _columnDefinitions.Add(new ColumnDefinition("HighTempWaterExpTankHighLevel", "高温水膨胀水箱高液位"));
                    _columnDefinitions.Add(new ColumnDefinition("MachineRoomDistCabFrontDoorDetect", "机器间配电柜体前门检测"));
                    _columnDefinitions.Add(new ColumnDefinition("DrawMidCoolWaterCloseLimit_31", "抽中冷水关到位-31"));
                    _columnDefinitions.Add(new ColumnDefinition("WasteOilDrainPumpPowerDetect", "污油排出泵电源检测"));
                    _columnDefinitions.Add(new ColumnDefinition("DrawHighTempWaterCloseLimit_21", "抽高温水关到位-21"));
                    _columnDefinitions.Add(new ColumnDefinition("BackupPowerDetect", "备用电源检测"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelMeterCommuteInOpenLimit_183", "油耗仪换向进开到位-183"));
                    _columnDefinitions.Add(new ColumnDefinition("DrawOilPumpClosingDetect", "抽油泵合闸检测"));
                    _columnDefinitions.Add(new ColumnDefinition("WaterResistLiftMotorThermalRelayDetect", "水阻升降电机热继检测"));
                    _columnDefinitions.Add(new ColumnDefinition("CoolWaterInOpenLimit_61", "冷却水进水开到位-61"));
                    _columnDefinitions.Add(new ColumnDefinition("MachineRoomDistCabRearDoorDetect", "机器间配电柜体后门检测"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPump1ValveFullyOpen_170", "燃油泵1电动调节阀全开-170"));
                    _columnDefinitions.Add(new ColumnDefinition("ExpTankWaterMakeupInOpenLimit_29", "膨胀水箱补水进开到位-29"));
                    _columnDefinitions.Add(new ColumnDefinition("HighTempWaterOpenLimit_3", "高温水开到位-3"));
                    _columnDefinitions.Add(new ColumnDefinition("MainFan1ThermalRelayDetect", "主发通风机1热继检测"));
                    _columnDefinitions.Add(new ColumnDefinition("IntakeDuctRightValveClose", "进气风道右调节阀关"));
                    _columnDefinitions.Add(new ColumnDefinition("WaterPumpOutValveFault_18", "水泵出口电动调节阀故障-18"));
                    _columnDefinitions.Add(new ColumnDefinition("PendingOilTankSupplementCloseLimit_139", "待处理机油箱补油关到位-139"));
                    _columnDefinitions.Add(new ColumnDefinition("MachineRoomPowerDetect", "机器间电源检测"));
                    _columnDefinitions.Add(new ColumnDefinition("EquipmentRoomCabRearDoorDetect", "设备间控制柜体后门检测"));
                    _columnDefinitions.Add(new ColumnDefinition("HighTempCoolWaterInValveFault_87", "高温水冷却水进口电动调节阀故障-87"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatPumpMainContactDetect", "预热水泵主接检测"));
                    _columnDefinitions.Add(new ColumnDefinition("OilReturnCloseLimit_179", "回油关到位-179"));
                    _columnDefinitions.Add(new ColumnDefinition("MainFan2YDetect", "主发通风机2Y检测"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPump1ValveFullyClose_170", "燃油泵1电动调节阀全关-170"));
                    _columnDefinitions.Add(new ColumnDefinition("MainFan2MainContactDetect", "主发通风机2主接检测"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankWaterFillOpenLimit_26", "预热水箱加水开到位-26"));
                    _columnDefinitions.Add(new ColumnDefinition("DieselEngineStop", "柴油机停机"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPreSupplyPumpClosingDetect", "预供机油泵合闸检测"));
                    _columnDefinitions.Add(new ColumnDefinition("WasteOilDrainPumpDetect", "污油排出泵检测"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatPumpOutCloseLimit_17", "预热水泵出口关到位-17"));
                    _columnDefinitions.Add(new ColumnDefinition("EngineDC24VPowerDist", "发动机DC24V配电"));
                    _columnDefinitions.Add(new ColumnDefinition("WaterResistLifterPower", "水阻升降机电源"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTankOilOutCloseLimit_111", "机油箱出油关到位-111"));
                    _columnDefinitions.Add(new ColumnDefinition("PendingOilTankConnectProcessorCloseLimit_134", "待处理机油箱通处理机关到位-134"));
                    _columnDefinitions.Add(new ColumnDefinition("ExhaustDuctLeftValveClose", "排气风道左调节阀关"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPump2ClosingDetect", "燃油泵2合闸检测"));
                    _columnDefinitions.Add(new ColumnDefinition("PumpAndTankWaterOutCloseLimit_20", "泵和水箱出水关到位-20"));
                    _columnDefinitions.Add(new ColumnDefinition("EmergencyStop1CloseLimit_181", "紧急停车1关到位-181"));
                    _columnDefinitions.Add(new ColumnDefinition("MainFan2ThermalRelayDetect", "主发通风机2热继检测"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTankCleanOilInOpenLimit_137", "机油箱清洁来油开到位-137"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatPumpOutOpenLimit_17", "预热水泵出口开到位-17"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPumpPipeOilInOpenLimit_95", "油泵管路进油开到位-95"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPump1ClosingDetect", "燃油泵1合闸检测"));
                    _columnDefinitions.Add(new ColumnDefinition("OilProcessTankInCloseLimit_135", "机油处理箱进关到位-135"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontOilFilter2OpenLimit_100", "机滤器前2开到位-100"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPreSupplyPumpStart", "机油预供泵启动"));
                    _columnDefinitions.Add(new ColumnDefinition("ExpTankWaterMakeupInCloseLimit_29", "膨胀水箱补水进关到位-29"));
                    _columnDefinitions.Add(new ColumnDefinition("OilOutlet1OpenLimit_190", "出油路1开到位-190"));
                    _columnDefinitions.Add(new ColumnDefinition("CoolerOilReturnOpenLimit_90", "冷却器回油开到位-90"));
                    _columnDefinitions.Add(new ColumnDefinition("DieselEngineStop", "柴油机停止"));
                    _columnDefinitions.Add(new ColumnDefinition("HighTempWaterExpTankLowLevel", "高温水膨胀水箱低液位"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankWaterFillCloseLimit_27", "预热水箱加水关到位-27"));
                    _columnDefinitions.Add(new ColumnDefinition("MidCoolWaterCircCloseLimit_41", "中冷水循环关到位-41"));
                    _columnDefinitions.Add(new ColumnDefinition("ControlRoom3CabFrontDoorDetect", "控制间3柜前门检测"));
                    _columnDefinitions.Add(new ColumnDefinition("WaterResistLiftMotorDownClosingDetect", "水阻升降电机下降合闸检测"));
                    _columnDefinitions.Add(new ColumnDefinition("IntakeDuctLeftValveOpen", "进气风道左调节阀开"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTankPumpOilInCloseLimit_115", "机油箱油泵来油关到位-115"));
                    _columnDefinitions.Add(new ColumnDefinition("IntakeDuctLeftValveFault", "进气风道左调节阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("CrankcasePressureSwitch", "曲轴箱压力开关"));
                    _columnDefinitions.Add(new ColumnDefinition("WaterPumpOutValveFullyClose_18", "水泵出口电动调节阀全关-18"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatPumpDetect", "预热水泵检测"));
                    _columnDefinitions.Add(new ColumnDefinition("ExhaustDuctLeftValveFault", "排气风道左调节阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelMeterCommuteReturnOpenLimit_184", "油耗仪换向回开到位-184"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTankToProcessorOpenLimit_133", "机油箱到处理机开到位-133"));
                    _columnDefinitions.Add(new ColumnDefinition("WaterPumpOutValveLocal_18", "水泵出口电动调节阀就地-18"));
                    _columnDefinitions.Add(new ColumnDefinition("IntakeDuctRightValveFault", "进气风道右调节阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPump2ThermalRelayDetect", "燃油泵2热继检测"));
                    _columnDefinitions.Add(new ColumnDefinition("ExhaustTreatmentUnitPowerDetect", "尾气处理装置电源检测"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankWaterReturnCloseLimit_22", "预热水箱回水关到位-22"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTankToProcessorCloseLimit_133", "机油箱到处理机关到位-133"));
                    _columnDefinitions.Add(new ColumnDefinition("OilProcessorPowerDetect", "机油处理机电源检测"));
                    _columnDefinitions.Add(new ColumnDefinition("ControlRoom1CabFrontDoorDetect", "控制间1柜前门检测"));
                    _columnDefinitions.Add(new ColumnDefinition("EmergencyStop2CloseLimit_182", "紧急停车2关到位-182"));
                    _columnDefinitions.Add(new ColumnDefinition("OilSumpDrawOilOpenLimit_92", "油底壳抽油开到位-92"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankHeaterCtrl3Detect", "预热水箱加热器控制3检测"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPreSupplyPumpThermalRelayDetect", "预供机油泵热继检测"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankWaterReturnOpenLimit_22", "预热水箱回水开到位-22"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPump1ThermalRelayDetect", "燃油泵1热继检测"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankHeaterCtrl6Detect", "预热水箱加热器控制6检测"));
                }
                if (keyNameList.Any(key => key == "DODataGrp"))
                {
                    _columnDefinitions.Add(new ColumnDefinition("No2LeftExhaustPipeValve1", "2号左排气管阀1"));
                    _columnDefinitions.Add(new ColumnDefinition("Y139ValveCtrl", "Y139阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y184ValveCtrl", "Y184阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y23ValveCtrl", "Y23阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y93ValveCtrl", "Y93阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y179ValveCtrl", "Y179阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y41ValveCtrl", "Y41阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y61ValveCtrl", "Y61阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("FaultReset", "故障复位"));
                    _columnDefinitions.Add(new ColumnDefinition("Y27ValveCtrl", "Y27阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y97ValveCtrl", "Y97阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y100ValveCtrl", "Y100阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("WasteOilDrainPumpCloseCtrl", "污油排出泵合闸控制"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatPumpCloseCtrl", "预热水泵合闸控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y115ValveCtrl", "Y115阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y181ValveCtrl", "Y181阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y134ValveCtrl", "Y134阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("EngineDC24VPower", "发动机DC24V供电"));
                    _columnDefinitions.Add(new ColumnDefinition("BuzzerCtrl", "蜂鸣器控制"));
                    _columnDefinitions.Add(new ColumnDefinition("No1RightIntakePipeValve1", "1号右进气管阀1"));
                    _columnDefinitions.Add(new ColumnDefinition("Y122ValveCtrl", "Y122阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("WaterResistBoxValveClose", "水阻箱调节阀关"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPump2CloseCtrl", "燃油泵2合闸控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y183ValveCtrl", "Y183阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("No1RightIntakePipeValve2", "1号右进气管阀2"));
                    _columnDefinitions.Add(new ColumnDefinition("Y16ValveCtrl", "Y16阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y137ValveCtrl", "Y137阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("DrawOilPumpCloseCtrl", "抽油泵合闸控制"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankHeatCtrl", "预热水箱加热控制"));
                    _columnDefinitions.Add(new ColumnDefinition("No1RightExhaustPipeValve2", "1号右排气管阀2"));
                    _columnDefinitions.Add(new ColumnDefinition("WaterResistDownCtrl", "水阻下降控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y111ValveCtrl", "Y111阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("MainFan1CloseCtrl", "主发通风机1合闸控制"));
                    _columnDefinitions.Add(new ColumnDefinition("MainFan2CloseCtrl", "主发通风机2合闸控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y15ValveCtrl", "Y15阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("WaterResistBoxValveOpen", "水阻箱调节阀开"));
                    _columnDefinitions.Add(new ColumnDefinition("No1RightExhaustPipeValve1", "1号右排气管阀1"));
                    _columnDefinitions.Add(new ColumnDefinition("Y29ValveCtrl", "Y29阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y164ValveCtrl", "Y164阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y28ValveCtrl", "Y28阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y22ValveCtrl", "Y22阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y92ValveCtrl", "Y92阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("No2LeftIntakePipeValve1", "2号左进气管阀1"));
                    _columnDefinitions.Add(new ColumnDefinition("Y133ValveCtrl", "Y133阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("WaterResistUpCtrl", "水阻上升控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y31ValveCtrl", "Y31阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("EnginePreStart", "发动机启停预启动"));
                    _columnDefinitions.Add(new ColumnDefinition("Y03ValveCtrl", "Y03阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y21ValveCtrl", "Y21阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y26ValveCtrl", "Y26阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y96ValveCtrl", "Y96阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y91ValveCtrl", "Y91阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y17ValveCtrl", "Y17阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("No2LeftIntakePipeValve2", "2号左进气管阀2"));
                    _columnDefinitions.Add(new ColumnDefinition("Y116ValveCtrl", "Y116阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y30ValveCtrl", "Y30阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("PreSupplyOilPumpCloseCtrl", "预供机油泵合闸控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y182ValveCtrl", "Y182阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y135ValveCtrl", "Y135阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y20ValveCtrl", "Y20阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("No2LeftExhaustPipeValve2", "2号左排气管阀2"));
                    _columnDefinitions.Add(new ColumnDefinition("Y95ValveCtrl", "Y95阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y90ValveCtrl", "Y90阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y190ValveCtrl", "Y190阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y136ValveCtrl", "Y136阀控制"));
                    _columnDefinitions.Add(new ColumnDefinition("OilPump1CloseCtrl", "燃油泵1合闸控制"));
                    _columnDefinitions.Add(new ColumnDefinition("Y24ValveCtrl", "Y24阀控制"));
                    //_columnDefinitions.Add(new ColumnDefinition("MainFan2MainContactCtrl", "主发通风机2主接控制"));
                    //_columnDefinitions.Add(new ColumnDefinition("MainFan2Ctrl", "主发通风机2控制"));
                    //_columnDefinitions.Add(new ColumnDefinition("MainFan2YCtrl", "主发通风机2Y控制"));
                    //_columnDefinitions.Add(new ColumnDefinition("MainFan1MainContactCtrl", "主发通风机1主接控制"));
                    //_columnDefinitions.Add(new ColumnDefinition("MainFan1Ctrl", "主发通风机1控制"));
                    //_columnDefinitions.Add(new ColumnDefinition("MainFan1YCtrl", "主发通风机1Y控制"));
                    //_columnDefinitions.Add(new ColumnDefinition("PreHeatTankHeat6", "预热水箱加热6"));
                    //_columnDefinitions.Add(new ColumnDefinition("PreHeatTankHeat5", "预热水箱加热5"));
                    //_columnDefinitions.Add(new ColumnDefinition("PreHeatTankHeat4", "预热水箱加热4"));
                    //_columnDefinitions.Add(new ColumnDefinition("PreHeatTankHeat3", "预热水箱加热3"));
                    //_columnDefinitions.Add(new ColumnDefinition("PreHeatTankHeat2", "预热水箱加热2"));
                    //_columnDefinitions.Add(new ColumnDefinition("PreHeatTankHeat1", "预热水箱加热1"));
                    //_columnDefinitions.Add(new ColumnDefinition("PreHeatPumpMainContact", "预热水泵主接触器"));
                    //_columnDefinitions.Add(new ColumnDefinition("PreHeatPumpStart", "预热水泵启动"));
                    //_columnDefinitions.Add(new ColumnDefinition("PreHeatPumpYStart", "预热水泵Y启动"));
                    //_columnDefinitions.Add(new ColumnDefinition("PreSupplyOilPumpCtrl", "预供机油泵控制"));
                    //_columnDefinitions.Add(new ColumnDefinition("WasteOilDrainPumpMainContact", "污油排出泵主接触器"));
                    //_columnDefinitions.Add(new ColumnDefinition("WasteOilDrainPumpStart", "污油排出泵启动"));
                    //_columnDefinitions.Add(new ColumnDefinition("WasteOilDrainPumpYStart", "污油排出泵Y启动"));
                    //_columnDefinitions.Add(new ColumnDefinition("WaterResistLiftMotorDownCtrl", "水阻升降电机下降控制"));
                    //_columnDefinitions.Add(new ColumnDefinition("WaterResistLiftMotorUpCtrl", "水阻升降电机上升控制"));
                    //_columnDefinitions.Add(new ColumnDefinition("OilPump2Ctrl", "燃油泵2控制"));
                    //_columnDefinitions.Add(new ColumnDefinition("OilPump1Ctrl", "燃油泵1控制"));
                    //_columnDefinitions.Add(new ColumnDefinition("OilPreSupplyPumpStopIndicate", "机油预供泵停止指示"));
                    //_columnDefinitions.Add(new ColumnDefinition("OilPreSupplyPumpStartIndicate", "机油预供泵启动指示"));
                    //_columnDefinitions.Add(new ColumnDefinition("DrawOilPumpCtrl", "抽油泵控制"));
                    //_columnDefinitions.Add(new ColumnDefinition("EngineStopIndicate", "柴油机停止指示"));
                    //_columnDefinitions.Add(new ColumnDefinition("EngineStartIndicate", "柴油机启动指示"));
                    //_columnDefinitions.Add(new ColumnDefinition("Y55ValveCtrl", "Y55阀控制"));
                }
                if (keyNameList.Any(key => key == "ExChangeDataGrpDouble"))
                {
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankTemperature", "预热水箱温度"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankLevel", "预热水箱液位"));
                    _columnDefinitions.Add(new ColumnDefinition("HighTempWaterOutMachineTemperature", "高温水出机温度"));
                    _columnDefinitions.Add(new ColumnDefinition("OilOutMachinePressure", "机油出机压力"));
                    _columnDefinitions.Add(new ColumnDefinition("DieselEngineRotateSpeed", "柴油机转速"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankHeatTempSet", "预热水箱加热温度设定"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTankTemperature", "机油箱温度"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelInMachinePressure", "燃油进机压力"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTankLevel", "机油箱液位"));
                    _columnDefinitions.Add(new ColumnDefinition("InnerCircWaterTankLevel", "内循环水箱液位"));
                    _columnDefinitions.Add(new ColumnDefinition("PendingOilTankTemperature", "待处理机油箱温度"));
                    _columnDefinitions.Add(new ColumnDefinition("HighTempWaterInMachineTemperature", "高温水进机温度"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelInMachineTemperature", "燃油进机温度"));
                    _columnDefinitions.Add(new ColumnDefinition("PendingOilTankLevel", "待处理机油箱液位"));
                    _columnDefinitions.Add(new ColumnDefinition("OilInMachinePressure", "机油进机压力"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelTankLevel", "燃油箱液位"));
                }
                if (keyNameList.Any(key => key == "ExChangeDataGrpBool"))
                {
                    _columnDefinitions.Add(new ColumnDefinition("HighTempWaterPreheatCycle", "高温水预热循环"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelConsumptionMeasurePumpSelect", "燃油耗测量油泵选择"));
                    _columnDefinitions.Add(new ColumnDefinition("OilSumpExtractSelectTank", "油底壳抽油选择油箱"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelCycle", "燃油循环"));
                    _columnDefinitions.Add(new ColumnDefinition("OilSumpExtract", "油底壳抽油"));
                    _columnDefinitions.Add(new ColumnDefinition("HighTempWaterColdWaterBackExtract", "高温水中冷水回抽"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTankRefuel", "机油箱加油"));
                    _columnDefinitions.Add(new ColumnDefinition("UpperComputerShutdownCtrl", "上位机停机控制"));
                    _columnDefinitions.Add(new ColumnDefinition("PreSupplyOilCycle", "预供机油循环"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankHeating", "预热水箱加热"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelConsumptionMeasure", "燃油耗测量"));
                    _columnDefinitions.Add(new ColumnDefinition("OilSumpRefuel", "油底壳加油"));
                    _columnDefinitions.Add(new ColumnDefinition("OilBackExtract", "机油回抽"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankWaterAdd", "预热水箱加水"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelTankReturnOilCool", "燃油箱回油冷却"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelCyclePumpSelect", "燃油循环油泵选择"));
                    _columnDefinitions.Add(new ColumnDefinition("OilHeatProcessCycle", "机油加热处理循环"));
                    //_columnDefinitions.Add(new ColumnDefinition("ColdWaterExpansionTankWaterAdd", "中冷水膨胀水箱加水"));
                    //_columnDefinitions.Add(new ColumnDefinition("PreheatOilCycle", "预热机油循环"));
                    //_columnDefinitions.Add(new ColumnDefinition("FuelTankRefuelCycle", "燃油箱加油循环"));
                    //_columnDefinitions.Add(new ColumnDefinition("HighTempWaterExpansionTankWaterAdd", "高温水膨胀水箱加水"));
                }
                if (keyNameList.Any(key => key == "PipelineFaultDataGrp"))
                {
                    _columnDefinitions.Add(new ColumnDefinition("OilTankLevelHigh_OilBackExtractStop", "机油箱液位高，机油回抽已停止"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve164Fault", "164阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve137Fault", "137阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve03Fault", "03阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelPump1OverCurrent", "燃油泵1过流"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve29Fault", "29阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("OilControlBoxDistIOCommDisconnect", "机油控制箱分布式IO通讯掉线"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve135Fault", "135阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve28Fault", "28阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("StartCabinetCommDisconnect", "启动柜通讯掉线"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankLevelHigh_WaterAddStop", "预热水箱水位高，预热水箱加水已停止"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTankLevelHigh_OilSumpExtractStop", "机油箱液位高，油底壳抽油已停止"));
                    _columnDefinitions.Add(new ColumnDefinition("PendingOilTankLevelHigh_OilSumpExtractStop", "待处理机油箱液位高，油底壳抽油已停止"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve133Fault", "133阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve115Fault", "115阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankLevelHigh_ColdHotWaterBackExtractStop", "预热水箱箱液位高，中冷水/高温水回抽己停止"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatPumpOverCurrent", "预热水泵过流"));
                    _columnDefinitions.Add(new ColumnDefinition("InExhaustControlBoxDistIOCommDisconnect", "进排气控制箱分布式IO通讯掉线"));
                    _columnDefinitions.Add(new ColumnDefinition("ControlRoomPowerCabinetDistIOCommDisconnect", "控制间配电柜分布式IO通讯掉线"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve97Fault", "97阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve111Fault", "111阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve27Fault", "27阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelPump2OverCurrent", "燃油泵2过流"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve31Fault", "31阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve96Fault", "96阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelControlBoxDistIOCommDisconnect", "燃油控制箱分布式IO通讯掉线"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve26Fault", "26阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve61Fault", "61阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve30Fault", "30阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve95Fault", "95阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("MainFan2OverCurrent", "主发通风机2过流"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTankLevelLow_OilSumpRefuelStop", "机油箱液位低，油底壳加油已停止"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve136Fault", "136阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve24Fault", "24阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve190Fault", "190阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankLevelLow_HighTempWaterPreheatStop", "预热水箱水位低，高温水预热循环已停止"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve93Fault", "93阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("MachineRoomPowerCabinetDistIOCommDisconnect", "机器间配电柜分布式IO通讯掉线"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve23Fault", "23阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("MainFan1OverCurrent", "主发通风机1过流"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve17Fault", "17阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("PreSupplyOilPumpOverCurrent", "预供机油泵过流"));
                    _columnDefinitions.Add(new ColumnDefinition("WasteOilDrainPumpOverCurrent", "污油排出泵过流"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve92Fault", "92阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve22Fault", "22阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve16Fault", "16阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTankLevelHigh_OilTankRefuelStop", "机油箱液位高，机油箱加油已停止"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve100Fault", "100阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve91Fault", "91阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve21Fault", "21阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve15Fault", "15阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve134Fault", "134阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve116Fault", "116阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("WaterResistLiftMotorOverCurrent", "水阻升降电机过流"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve90Fault", "90阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve20Fault", "20阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("DrawOilPumpOverCurrent", "抽油泵过流"));
                    _columnDefinitions.Add(new ColumnDefinition("EmergencyStop_RotateSpeedNotDrop", "急停后转速不下降"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve139Fault", "139阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("WaterSystemControlBoxDistIOCommDisconnect", "水系统控制箱分布式IO通讯掉线"));
                    _columnDefinitions.Add(new ColumnDefinition("EquipmentRoomPowerCabinetDistIOCommDisconnect", "设备间配电柜分布式IO通讯掉线"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve122Fault", "122阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("FuelTankLevelLow_FuelCycleStop", "燃油箱液位低，燃油循环已停止"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve41Fault", "41阀故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Valve179Fault", "179阀故障"));
                }
                if (keyNameList.Any(key => key == "EngineOilDataGrp"))
                {
                    _columnDefinitions.Add(new ColumnDefinition("FlowMeterRearPressureDetectP29", "流量计口后压力检测-P29"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTankOutletRearPressureDetectP23", "机油箱出口后压力检测-P23"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTankOutletFrontPressureDetectP22", "机油箱出口前压力检测-P22"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTemperaturePassword", "机油温度密码"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTemperatureRealTimePID", "机油温度实时PID"));
                    _columnDefinitions.Add(new ColumnDefinition("PendingOilTankLevelDetectL19", "待处理机油箱液位检测-L19"));
                    _columnDefinitions.Add(new ColumnDefinition("CoolerInletOilTemperatureT25", "冷却器进口油温-T25"));
                    _columnDefinitions.Add(new ColumnDefinition("PreSupplyOilPressureDetectP19", "预供机油压力检测-P19"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontPressureDetectP24", "前压力检测-P24"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTemperatureSetPID", "机油温度设置PID"));
                    _columnDefinitions.Add(new ColumnDefinition("RearPressureDetectP25", "后压力检测-P25"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTankLevelDetectL18", "机油箱液位检测-L18"));
                    _columnDefinitions.Add(new ColumnDefinition("OilTankTemperatureDetectT23", "机油箱温度检测-T23"));
                    _columnDefinitions.Add(new ColumnDefinition("FlowMeterFrontPressureDetectP28", "流量计口前压力检测-P28"));
                    _columnDefinitions.Add(new ColumnDefinition("Front1PressureDetectP26", "前1压力检测-P26"));
                    _columnDefinitions.Add(new ColumnDefinition("Rear1PressureDetectP27", "后1压力检测-P27"));
                    _columnDefinitions.Add(new ColumnDefinition("PendingOilTankTemperatureDetectT24", "待处理机油箱温度检测-T24"));
                    //_columnDefinitions.Add(new ColumnDefinition("OilTemperaturePIDUpperLimit", "机油温度PID上限值"));
                }
                if (keyNameList.Any(key => key == "FuelDataGrp"))
                {
                    _columnDefinitions.Add(new ColumnDefinition("WaterResistBoxTemperatureDetect", "水阻箱温度检测"));
                    _columnDefinitions.Add(new ColumnDefinition("FineFilter2FrontPressureDetectP36", "精滤器2前压力检测-P36"));
                    _columnDefinitions.Add(new ColumnDefinition("WaterResistBoxPolarPlateDisplacementDetect", "水阻箱极板位移检测"));
                    _columnDefinitions.Add(new ColumnDefinition("CoarseFilter2RearPressureDetectP33", "粗滤器2后压力检测-P33"));
                    _columnDefinitions.Add(new ColumnDefinition("FineFilter1FrontPressureDetectP34", "精滤器1前压力检测-P34"));
                    _columnDefinitions.Add(new ColumnDefinition("CoarseFilter1RearPressureDetectP31", "粗滤器1后压力检测-P31"));
                    _columnDefinitions.Add(new ColumnDefinition("FineFilter2RearPressureDetectP37", "精滤器2后压力检测-P37"));
                    _columnDefinitions.Add(new ColumnDefinition("CoarseFilter2FrontPressureDetectP32", "粗滤器2前压力检测-P32"));
                    _columnDefinitions.Add(new ColumnDefinition("DieselTankLevelDetectL29", "柴油箱液位检测-L29"));
                    _columnDefinitions.Add(new ColumnDefinition("FineFilter1RearPressureDetectP35", "精滤器1后压力检测-P35"));
                    _columnDefinitions.Add(new ColumnDefinition("CoarseFilter1FrontPressureDetectP30", "粗滤器1前压力检测-P30"));
                }
                if (keyNameList.Any(key => key == "ThreePhaseElectricData"))
                {
                    _columnDefinitions.Add(new ColumnDefinition("ActivePower", "有功功率"));
                    _columnDefinitions.Add(new ColumnDefinition("CurrentIc", "Ic"));
                    _columnDefinitions.Add(new ColumnDefinition("TotalCurrent", "电流"));
                    _columnDefinitions.Add(new ColumnDefinition("CurrentIb", "Ib"));
                    _columnDefinitions.Add(new ColumnDefinition("VoltageUvw", "Uvw"));
                    _columnDefinitions.Add(new ColumnDefinition("TotalVoltage", "电压"));
                    _columnDefinitions.Add(new ColumnDefinition("CurrentIa", "Ia"));
                    _columnDefinitions.Add(new ColumnDefinition("VoltageUuv", "Uuv"));
                    _columnDefinitions.Add(new ColumnDefinition("VoltageUwu", "Uwu"));
                    //_columnDefinitions.Add(new ColumnDefinition("ReactivePower", "无功功率"));
                    //_columnDefinitions.Add(new ColumnDefinition("ApparentPower", "视在功率"));
                    //_columnDefinitions.Add(new ColumnDefinition("Frequency", "频率"));
                }
                if (keyNameList.Any(key => key == "WaterDataGrp"))
                {
                    _columnDefinitions.Add(new ColumnDefinition("CoolWaterTemperatureSetPID", "中冷水温度设置PID"));
                    _columnDefinitions.Add(new ColumnDefinition("HighTempWaterFilterFrontPressureDetectP6", "高温水过滤器前压力检测-P6"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankLevelDetect", "预热水箱液位检测"));
                    _columnDefinitions.Add(new ColumnDefinition("CoolWaterFilterFrontPressureDetectP9", "中冷水过滤器前压力检测-P9"));
                    _columnDefinitions.Add(new ColumnDefinition("CoolWaterFilterRearPressureDetectP10", "中冷水过滤器后压力检测-P10"));
                    _columnDefinitions.Add(new ColumnDefinition("HighTempWaterTemperaturePassword", "高温水温度密码"));
                    _columnDefinitions.Add(new ColumnDefinition("HighTempWaterFilterRearPressureDetectP7", "高温水过滤器后压力检测-P7"));
                    _columnDefinitions.Add(new ColumnDefinition("HighTempWaterTemperatureSetPID", "高温水温度设置PID"));
                    _columnDefinitions.Add(new ColumnDefinition("CoolWaterCoolerInletTemperatureDetectT14", "中冷水冷却器进口温度检测-T14"));
                    _columnDefinitions.Add(new ColumnDefinition("HighTempWaterTemperatureRealTimePID", "高温水温度实时PID"));
                    _columnDefinitions.Add(new ColumnDefinition("PreHeatTankTemperatureDetectT12", "预热水箱温度检测-T12"));
                    _columnDefinitions.Add(new ColumnDefinition("CoolWaterTemperaturePassword", "中冷水温度密码"));
                    _columnDefinitions.Add(new ColumnDefinition("CoolWaterTemperatureRealTimePID", "中冷水温度实时PID"));
                    _columnDefinitions.Add(new ColumnDefinition("HighTempWaterCoolerInletTemperatureDetectT13", "高温水冷却器进口温度检测-T13"));
                    //_columnDefinitions.Add(new ColumnDefinition("HighTempWaterTemperaturePIDUpperLimit", "高温水温度PID上限值"));
                    //_columnDefinitions.Add(new ColumnDefinition("CoolWaterTemperaturePIDUpperLimit", "中冷水温度PID上限值"));
                }
                if (keyNameList.Any(key => key == "PLC2AIDataGrp"))
                {
                    // 励磁
                    _columnDefinitions.Add(new ColumnDefinition("ExcitationVoltageDetect", "励磁电压检测"));
                    _columnDefinitions.Add(new ColumnDefinition("ExcitationCurrentDetect", "励磁电流检测"));
                    // 测功机相温
                    _columnDefinitions.Add(new ColumnDefinition("DynamometerUPhaseTemperature", "测功机U相温度"));
                    _columnDefinitions.Add(new ColumnDefinition("DynamometerDPhaseTemperature", "测功机D相温度"));
                    _columnDefinitions.Add(new ColumnDefinition("DynamometerWPhaseTemperature", "测功机W相温度"));
                    _columnDefinitions.Add(new ColumnDefinition("DynamometerNPhaseTemperature", "测功机N相温度"));
                    _columnDefinitions.Add(new ColumnDefinition("DynamometerVPhaseTemperature", "测功机V相温度"));
                    // A 列缸排气温度
                    _columnDefinitions.Add(new ColumnDefinition("台位_A1CylinderExhaustTemperature", "台位_A1缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("台位_A2CylinderExhaustTemperature", "台位_A2缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("台位_A3CylinderExhaustTemperature", "台位_A3缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("台位_A4CylinderExhaustTemperature", "台位_A4缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("台位_A5CylinderExhaustTemperature", "台位_A5缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("台位_A6CylinderExhaustTemperature", "台位_A6缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("台位_A7CylinderExhaustTemperature", "台位_A7缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("台位_A8CylinderExhaustTemperature", "台位_A8缸排气温度"));
                    // B 列缸排气温度
                    _columnDefinitions.Add(new ColumnDefinition("台位_B1CylinderExhaustTemperature", "台位_B1缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("台位_B2CylinderExhaustTemperature", "台位_B2缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("台位_B3CylinderExhaustTemperature", "台位_B3缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("台位_B4CylinderExhaustTemperature", "台位_B4缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("台位_B5CylinderExhaustTemperature", "台位_B5缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("台位_B6CylinderExhaustTemperature", "台位_B6缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("台位_B7CylinderExhaustTemperature", "台位_B7缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("台位_B8CylinderExhaustTemperature", "台位_B8缸排气温度"));
                    // 机油 / 主油道
                    _columnDefinitions.Add(new ColumnDefinition("T21MainOilChannelInletOilTemperature", "T21主油道进口油温"));
                    _columnDefinitions.Add(new ColumnDefinition("T20OilPumpOutletOilTemperature", "T20机油泵出口油温"));
                    _columnDefinitions.Add(new ColumnDefinition("P20OilPumpOutletPressure", "P20机油泵出口压力"));
                    _columnDefinitions.Add(new ColumnDefinition("P21MainOilChannelInletOilPressure", "P21主油道进口油压"));
                    _columnDefinitions.Add(new ColumnDefinition("台位_MainOilChannelEndOilPressure", "台位_主油道末端油压"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontSuperchargerOilInletPressure", "前增压器机油进口压力"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontSuperchargerOilInletTemperature", "前增压器机油进口温度"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontSuperchargerOilOutletTemperature", "前增压器机油出口温度"));
                    _columnDefinitions.Add(new ColumnDefinition("RearSuperchargerOilInletPressure", "后增压器机油进口压力"));
                    _columnDefinitions.Add(new ColumnDefinition("RearSuperchargerOilInletTemperature", "后增压器机油进口温度"));
                    _columnDefinitions.Add(new ColumnDefinition("RearSuperchargerOilOutletTemperature", "后增压器机油出口温度"));
                    // 燃油
                    _columnDefinitions.Add(new ColumnDefinition("T31FuelPumpInletOilTemperature", "T31燃油泵进口油温"));
                    _columnDefinitions.Add(new ColumnDefinition("P38FuelSupplyPressure", "P38燃油供油压力"));
                    _columnDefinitions.Add(new ColumnDefinition("T30FuelReturnOilTemperature", "T30燃油回油温度"));
                    // 机油耗测量
                    _columnDefinitions.Add(new ColumnDefinition("OilConsumptionMeasurePressure", "机油耗测量压力"));
                    _columnDefinitions.Add(new ColumnDefinition("OilConsumptionMeasureLevel", "机油耗测量液位"));
                    // 高温水
                    _columnDefinitions.Add(new ColumnDefinition("T1HighTempWaterOutletTemperature", "T1高温水出机温度"));
                    _columnDefinitions.Add(new ColumnDefinition("T2HighTempWaterInletTemperature", "T2高温水进机温度"));
                    _columnDefinitions.Add(new ColumnDefinition("P1HighTempWaterOutletPressure", "P1高温水出机压力"));
                    _columnDefinitions.Add(new ColumnDefinition("P2HighTempWaterPumpInletPressure", "P2高温水泵进口压力"));
                    _columnDefinitions.Add(new ColumnDefinition("台位_HighTempWaterPumpOutletPressure", "台位_高温水泵出口压力"));
                    // 中冷水
                    _columnDefinitions.Add(new ColumnDefinition("T3CoolWaterInletTemperature", "T3中冷水进机温度"));
                    _columnDefinitions.Add(new ColumnDefinition("T5CoolWaterOutletTemperature", "T5中冷水出机温度"));
                    _columnDefinitions.Add(new ColumnDefinition("P3CoolWaterPumpInletPressure", "P3中冷水泵进口压力"));
                    _columnDefinitions.Add(new ColumnDefinition("P5CoolWaterOutletPressure", "P5中冷水出机压力"));
                    _columnDefinitions.Add(new ColumnDefinition("台位_CoolWaterPumpOutletPressure", "台位_中冷水泵出口压力"));
                    _columnDefinitions.Add(new ColumnDefinition("InterCoolerInletWaterTemperature", "中冷器进口水温"));
                    _columnDefinitions.Add(new ColumnDefinition("InterCoolerOutletWaterTemperature", "中冷器出口水温"));
                    // 前增压器 / 涡轮
                    _columnDefinitions.Add(new ColumnDefinition("FrontSuperchargerIntakeVacuumDegree", "前增压器进气真空度"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontSuperchargerIntakeTemperature", "前增压器进气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontSuperchargerExhaustBackPressure", "前增压器排气背压"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontTurboInletExhaustGasPressure", "前涡轮进口废气压力"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontTurboInletExhaustGasTemperature", "前涡轮进口废气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontTurboOutletExhaustGasTemperature", "前涡轮出口废气温度"));
                    // 后增压器 / 涡轮
                    _columnDefinitions.Add(new ColumnDefinition("RearSuperchargerIntakeVacuumDegree", "后增压器进气真空度"));
                    _columnDefinitions.Add(new ColumnDefinition("RearSuperchargerIntakeTemperature", "后增压器进气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("RearSuperchargerExhaustBackPressure", "后增压器排气背压"));
                    _columnDefinitions.Add(new ColumnDefinition("RearTurboInletExhaustGasPressure", "后涡轮进口废气压力"));
                    _columnDefinitions.Add(new ColumnDefinition("RearTurboInletExhaustGasTemperature", "后涡轮进口废气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("RearTurboOutletExhaustGasTemperature", "后涡轮出口废气温度"));
                    // 前中冷空气
                    _columnDefinitions.Add(new ColumnDefinition("FrontInterCoolerFrontAirPressure", "前中冷前空气压力"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontInterCoolerFrontAirTemperature", "前中冷前空气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontInterCoolerRearAirPressure", "前中冷后空气压力"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontInterCoolerRearAirTemperature", "前中冷后空气温度"));
                    // 后中冷空气
                    _columnDefinitions.Add(new ColumnDefinition("RearInterCoolerFrontAirPressure", "后中冷前空气压力"));
                    _columnDefinitions.Add(new ColumnDefinition("RearInterCoolerFrontAirTemperature", "后中冷前空气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("台位_RearInterCoolerRearAirPressure", "台位_后中冷后空气压力"));
                    _columnDefinitions.Add(new ColumnDefinition("台位_RearInterCoolerRearAirTemperature", "台位_后中冷后空气温度"));
                    //_columnDefinitions.Add(new ColumnDefinition("RearCoolerFrontAirTemperature", "后冷前空气温度"));
                }
                if (keyNameList.Any(key => key == "StartPLCDataGrp"))
                {
                    _columnDefinitions.Add(new ColumnDefinition("FaultReset", "故障复位"));
                    _columnDefinitions.Add(new ColumnDefinition("InverterRunning", "变频器运行中"));
                    _columnDefinitions.Add(new ColumnDefinition("InverterOutputDetect", "变频器输出检测"));
                    _columnDefinitions.Add(new ColumnDefinition("InverterFault", "变频器故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Auto", "自动模式"));
                    _columnDefinitions.Add(new ColumnDefinition("InverterOutputSwitch", "变频器输出合分闸"));
                    _columnDefinitions.Add(new ColumnDefinition("Scram", "急停"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontDoorDetect", "前门检测"));
                    _columnDefinitions.Add(new ColumnDefinition("RearDoorDetect", "后门检测"));
                }
                if (keyNameList.Any(key => key == "SpeedDataGrp"))
                {
                    // Speed转速分组列定义
                    _columnDefinitions.Add(new ColumnDefinition("Speed3", "转速3"));
                    _columnDefinitions.Add(new ColumnDefinition("PulsesPerRevolution", "每转感应点数"));
                    _columnDefinitions.Add(new ColumnDefinition("Speed1", "转速1"));
                    _columnDefinitions.Add(new ColumnDefinition("Speed2", "转速2"));
                }
                if (keyNameList.Any(key => key == "GD350_1Data"))
                {
                    // GD350_1变频器分组列定义
                    _columnDefinitions.Add(new ColumnDefinition("OutputPowerDetect", "输出功率检测"));
                    _columnDefinitions.Add(new ColumnDefinition("StartStop", "启动_停止"));
                    _columnDefinitions.Add(new ColumnDefinition("RunTimeout", "运行超时时间"));
                    _columnDefinitions.Add(new ColumnDefinition("OutputCurrentDetect", "输出电流检测"));
                    _columnDefinitions.Add(new ColumnDefinition("CabinetRunningStatus", "启动柜运行状态"));
                    _columnDefinitions.Add(new ColumnDefinition("Ready", "就绪"));
                    _columnDefinitions.Add(new ColumnDefinition("FaultCode", "故障代码"));
                    _columnDefinitions.Add(new ColumnDefinition("BusVoltageDetect", "母线电压检测"));
                    _columnDefinitions.Add(new ColumnDefinition("CabinetFrequencySet", "启动柜频率设定"));
                    _columnDefinitions.Add(new ColumnDefinition("OutputVoltageDetect", "输出电压检测"));
                    _columnDefinitions.Add(new ColumnDefinition("CabinetStart", "启动柜启动"));
                    _columnDefinitions.Add(new ColumnDefinition("RunningStatus", "运行状态"));
                    _columnDefinitions.Add(new ColumnDefinition("RunningFrequency", "运行频率"));
                }
                if (keyNameList.Any(key => key == "AirDuctData1Grp"))
                {
                    // 一号：AI -> DI -> DO -> Fault
                    _columnDefinitions.Add(new ColumnDefinition("Unit1OutletTempDisplay", "一号显示出口温度"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit1CavityTemp1Display", "一号显示内腔温度1"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit1CavityTemp2Display", "一号显示内腔温度2"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit1FanFrequencyDisplay", "一号显示风机频率"));

                    _columnDefinitions.Add(new ColumnDefinition("Unit1OutletTempSet", "一号设置出口温度"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit1CavityTempSet", "一号设置内腔温度"));

                    _columnDefinitions.Add(new ColumnDefinition("Unit1FanRunning", "一号风机运行"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit1HeatRunning1", "一号加热运行1"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit1HeatRunning2", "一号加热运行2"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit1HeatRunning3", "一号加热运行3"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit1HeatRunning4", "一号加热运行4"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit1HeatRunning5", "一号加热运行5"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit1HeatRunning6", "一号加热运行6"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit1StopPressedMiddle", "一号停止按下中间"));

                    _columnDefinitions.Add(new ColumnDefinition("Unit1OneKeyStart", "一号一键启动"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit1OneKeyStop", "一号一键停止"));

                    _columnDefinitions.Add(new ColumnDefinition("Unit1FanInverterFault", "一号风机变频器故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit1FanCoolingFanOverload", "一号风机冷却风扇过载"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit1OutletOverTemp", "一号出口超温"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit1CavityProtect1OverTemp", "一号内腔保护1超温"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit1CavityProtect2OverTemp", "一号内腔保护2超温"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit1TempControllerCommError", "一号温控器通讯错误"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit1InverterCommError", "一号变频器通讯错误"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit1PhaseSequenceFault", "一号相序保护故障"));
                }
                if (keyNameList.Any(key => key == "AirDuctData2Grp"))
                {
                    // 二号：AI -> DI -> DO -> Fault
                    _columnDefinitions.Add(new ColumnDefinition("Unit2OutletTempDisplay", "二号显示出口温度"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit2CavityTemp1Display", "二号显示内腔温度1"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit2CavityTemp2Display", "二号显示内腔温度2"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit2FanFrequencyDisplay", "二号显示风机频率"));

                    _columnDefinitions.Add(new ColumnDefinition("Unit2OutletTempSet", "二号设置出口温度"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit2CavityTempSet", "二号设置内腔温度"));

                    _columnDefinitions.Add(new ColumnDefinition("Unit2FanRunning", "二号风机运行"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit2HeatRunning1", "二号加热运行1"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit2HeatRunning2", "二号加热运行2"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit2HeatRunning3", "二号加热运行3"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit2HeatRunning4", "二号加热运行4"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit2HeatRunning5", "二号加热运行5"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit2HeatRunning6", "二号加热运行6"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit2StopPressedMiddle", "二号停止按下中间"));

                    _columnDefinitions.Add(new ColumnDefinition("Unit2OneKeyStart", "二号一键启动"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit2OneKeyStop", "二号一键停止"));

                    _columnDefinitions.Add(new ColumnDefinition("Unit2FanInverterFault", "二号风机变频器故障"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit2FanCoolingFanOverload", "二号风机冷却风扇过载"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit2OutletOverTemp", "二号出口超温"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit2CavityProtect1OverTemp", "二号内腔保护1超温"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit2CavityProtect2OverTemp", "二号内腔保护2超温"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit2TempControllerCommError", "二号温控器通讯错误"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit2InverterCommError", "二号变频器通讯错误"));
                    _columnDefinitions.Add(new ColumnDefinition("Unit2PhaseSequenceFault", "二号相序保护故障"));
                }

            }
            return _columnDefinitions;
        }


        /// <summary>
        /// 将 MonitorData JSON 按勾选模块解析为行字典。
        /// 返回的 key 为数据列表中的行索引，value 中再按模块名存放对应实体对象。
        /// </summary>
        public Dictionary<int, Dictionary<string, object>> jsonToObject(List<TestParaAllData> _allData,
                          Dictionary<int, Dictionary<string, object>> RowDictionary, 
                          List<string> KeyNameList)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            int index = 0;
            RowDictionary.Clear();
            foreach (var item in _allData)
            {
                string json = item.MonitorData;
                RowDictionary[index] = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(json))
                {
                    var root = JObject.Parse(json);

                    if (json.Contains("BaseGrp") && KeyNameList.Any(key => key == "BaseDataGrp"))
                    {
                        BaseDataGrp BaseGrp = root["BaseGrp"].ToObject<BaseDataGrp>();
                        RowDictionary[index]["BaseDataGrp"] = CloneRowEntity(BaseGrp);
                    }

                    if (json.Contains("TRDP") )
                    {
                        if(KeyNameList.Any(key => key == "TRDPDataGrp"))
                        {
                            TRDPDataGrp TRDPGrp = root["TRDP"].ToObject<TRDPDataGrp>();
                            RowDictionary[index]["TRDPDataGrp"] = CloneRowEntity(TRDPGrp);
                        }
                        if (KeyNameList.Any(key => key == "TRDPData1Grp"))
                        {
                            TRDPData1Grp TRDPGrp = root["TRDP"].ToObject<TRDPData1Grp>();
                            RowDictionary[index]["TRDPData1Grp"] = CloneRowEntity(TRDPGrp);
                        }
                        
                    }

                    if (json.Contains("AIGrp") && KeyNameList.Any(key => key == "AIDataGrp"))
                    {
                        AIDataGrp AIGrp = root["AIGrp"].ToObject<AIDataGrp>();
                        RowDictionary[index]["AIDataGrp"] = CloneRowEntity(AIGrp);
                    }

                    if (json.Contains("AOGrp") && KeyNameList.Any(key => key == "AODataGrp"))
                    {
                        AODataGrp AOGrp = root["AOGrp"].ToObject<AODataGrp>();
                        RowDictionary[index]["AODataGrp"] = CloneRowEntity(AOGrp);
                    }

                    if (json.Contains("DIGrp") && KeyNameList.Any(key => key == "DIDataGrp"))
                    {
                        DIDataGrp DIGrp = root["DIGrp"].ToObject<DIDataGrp>();
                        RowDictionary[index]["DIDataGrp"] = CloneRowEntity(DIGrp);
                    }

                    if (json.Contains("DOGrp") && KeyNameList.Any(key => key == "DODataGrp"))
                    {
                        DODataGrp DOGrp = root["DOGrp"].ToObject<DODataGrp>();
                        RowDictionary[index]["DODataGrp"] = CloneRowEntity(DOGrp);
                    }

                    if (json.Contains("ExChangeGrpDouble") && KeyNameList.Any(key => key == "ExChangeDataGrpDouble"))
                    {
                        ExChangeDataGrpDouble ExChangeGrpDouble = root["ExChangeGrpDouble"].ToObject<ExChangeDataGrpDouble>();
                        RowDictionary[index]["ExChangeDataGrpDouble"] = CloneRowEntity(ExChangeGrpDouble);
                    }

                    if (json.Contains("ExChangeGrpBool") && KeyNameList.Any(key => key == "ExChangeDataGrpBool"))
                    {
                        ExChangeDataGrpBool ExChangeGrpBool = root["ExChangeGrpBool"].ToObject<ExChangeDataGrpBool>();
                        RowDictionary[index]["ExChangeDataGrpBool"] = CloneRowEntity(ExChangeGrpBool);
                    }

                    if (json.Contains("PipelineFaultGrp") && KeyNameList.Any(key => key == "PipelineFaultDataGrp"))
                    {
                        PipelineFaultDataGrp PipelineFaultGrp = root["PipelineFaultGrp"].ToObject<PipelineFaultDataGrp>();
                        RowDictionary[index]["PipelineFaultDataGrp"] = CloneRowEntity(PipelineFaultGrp);
                    }

                    if (json.Contains("EngineOilGrp") && KeyNameList.Any(key => key == "EngineOilDataGrp"))
                    {
                        EngineOilDataGrp EngineOilGrp = root["EngineOilGrp"].ToObject<EngineOilDataGrp>();
                        RowDictionary[index]["EngineOilDataGrp"] = CloneRowEntity(EngineOilGrp);
                    }

                    if (json.Contains("FuelGrp") && KeyNameList.Any(key => key == "FuelDataGrp"))
                    {
                        FuelDataGrp FuelGrp = root["FuelGrp"].ToObject<FuelDataGrp>();
                        RowDictionary[index]["FuelDataGrp"] = CloneRowEntity(FuelGrp);
                    }

                    if (json.Contains("ThreePhaseElectric") && KeyNameList.Any(key => key == "ThreePhaseElectricData"))
                    {
                        ThreePhaseElectricData ThreePhaseElectric = root["ThreePhaseElectric"].ToObject<ThreePhaseElectricData>();
                        RowDictionary[index]["ThreePhaseElectricData"] = CloneRowEntity(ThreePhaseElectric);
                    }

                    if (json.Contains("WaterGrp") && KeyNameList.Any(key => key == "WaterDataGrp"))
                    {
                        WaterDataGrp WaterGrp = root["WaterGrp"].ToObject<WaterDataGrp>();
                        RowDictionary[index]["WaterDataGrp"] = CloneRowEntity(WaterGrp);
                    }

                    if (json.Contains("PLC2AIGrp") && KeyNameList.Any(key => key == "PLC2AIDataGrp"))
                    {
                        PLC2AIDataGrp PLC2AIGrp = root["PLC2AIGrp"].ToObject<PLC2AIDataGrp>();
                        RowDictionary[index]["PLC2AIDataGrp"] = CloneRowEntity(PLC2AIGrp);
                    }

                    if (json.Contains("StartPLCGrp") && KeyNameList.Any(key => key == "StartPLCDataGrp"))
                    {
                        StartPLCDataGrp StartPLCGrp = root["StartPLCGrp"].ToObject<StartPLCDataGrp>();
                        RowDictionary[index]["StartPLCDataGrp"] = CloneRowEntity(StartPLCGrp);
                    }

                    if (json.Contains("SpeedGrp") && KeyNameList.Any(key => key == "SpeedDataGrp"))
                    {
                        SpeedDataGrp SpeedGrp = root["SpeedGrp"].ToObject<SpeedDataGrp>();
                        RowDictionary[index]["SpeedDataGrp"] = CloneRowEntity(SpeedGrp);
                    }

                    if (json.Contains("GD350_1") && KeyNameList.Any(key => key == "GD350_1Data"))
                    {
                        GD350_1Data GD350 = root["GD350_1"].ToObject<GD350_1Data>();
                        RowDictionary[index]["GD350_1Data"] = CloneRowEntity(GD350);
                    }

                    if (KeyNameList.Any(key => key == "AirDuctData1Grp")
                        && (root["AirDuct1Grp"] != null || root["AirDuctGrp"] != null || root["AirDuctGrpDouble"] != null || root["AirDuctGrpBool"] != null))
                    {
                        AirDuctData1Grp airDuct1 = new AirDuctData1Grp();
                        if (root["AirDuct1Grp"] != null)
                        {
                            JsonConvert.PopulateObject(root["AirDuct1Grp"].ToString(), airDuct1);
                        }
                        if (root["AirDuctGrp"] != null)
                        {
                            JsonConvert.PopulateObject(root["AirDuctGrp"].ToString(), airDuct1);
                        }
                        if (root["AirDuctGrpDouble"] != null)
                        {
                            JsonConvert.PopulateObject(root["AirDuctGrpDouble"].ToString(), airDuct1);
                        }
                        if (root["AirDuctGrpBool"] != null)
                        {
                            JsonConvert.PopulateObject(root["AirDuctGrpBool"].ToString(), airDuct1);
                        }
                        RowDictionary[index]["AirDuctData1Grp"] = CloneRowEntity(airDuct1);
                    }

                    if (KeyNameList.Any(key => key == "AirDuctData2Grp")
                        && (root["AirDuct2Grp"] != null || root["AirDuctGrp"] != null || root["AirDuctGrpDouble"] != null || root["AirDuctGrpBool"] != null))
                    {
                        AirDuctData2Grp airDuct2 = new AirDuctData2Grp();
                        if (root["AirDuct2Grp"] != null)
                        {
                            JsonConvert.PopulateObject(root["AirDuct2Grp"].ToString(), airDuct2);
                        }
                        if (root["AirDuctGrp"] != null)
                        {
                            JsonConvert.PopulateObject(root["AirDuctGrp"].ToString(), airDuct2);
                        }
                        if (root["AirDuctGrpDouble"] != null)
                        {
                            JsonConvert.PopulateObject(root["AirDuctGrpDouble"].ToString(), airDuct2);
                        }
                        if (root["AirDuctGrpBool"] != null)
                        {
                            JsonConvert.PopulateObject(root["AirDuctGrpBool"].ToString(), airDuct2);
                        }
                        RowDictionary[index]["AirDuctData2Grp"] = CloneRowEntity(airDuct2);
                    }
                }

                //累加行
                index++;
            }
            return RowDictionary;
        }


     
        /// <summary>
        /// 为每行保存独立的模块对象副本，避免多行共用同一实例
        /// </summary>
        private static T CloneRowEntity<T>(T source)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
        }



        private const string AllDataSheetName = "Sheet1";
        private const string StartupSheetName = "Sheet2";

        /// <summary>
        /// 导出本页：总数据写入 Sheet1，启动柜数据写入 Sheet2
        /// </summary>
        public void Report_Excel(DataGridView allDataGrid, DataGridView startupGrid)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "请选择保存路径";
                saveFileDialog.Filter = "Excel 文件 (*.xlsx)|*.xlsx|CSV 文件 (*.csv)|*.csv|所有文件 (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.FileName = $"数据分析_本页_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                saveFileDialog.DefaultExt = "xlsx";
                saveFileDialog.AddExtension = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var sheets = new Dictionary<string, object>
                    {
                        [AllDataSheetName] = BuildRowsFromGrid(allDataGrid),
                        [StartupSheetName] = BuildRowsFromGrid(startupGrid),
                    };
                    MiniExcel.SaveAs(saveFileDialog.FileName, sheets);
                }
            }
        }

        /// <summary>
        /// 导出全部：总数据写入 Sheet1，启动柜数据写入 Sheet2。
        /// 选定路径后弹出“保存中”，后台组装并写文件，完成后可点关闭。
        /// </summary>
        public void Report_Excel_All(
            List<ColumnDefinition> columnDefinitions,
            List<TestParaAllData> allData,
            Dictionary<int, Dictionary<string, object>> rowDictionary,
            List<StartupTestPara> startupData,
            IWin32Window owner = null)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "请选择保存路径";
                saveFileDialog.Filter = "Excel 文件 (*.xlsx)|*.xlsx|CSV 文件 (*.csv)|*.csv|所有文件 (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.FileName = $"数据分析_全部_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                saveFileDialog.DefaultExt = "xlsx";
                saveFileDialog.AddExtension = true;
                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                string fileName = saveFileDialog.FileName;
                using (var savingDlg = new frmExportSaving(() =>
                {
                    var sheets = new Dictionary<string, object>
                    {
                        [AllDataSheetName] = BuildAllDataExportRows(columnDefinitions, allData, rowDictionary),
                        [StartupSheetName] = BuildStartupExportRows(startupData),
                    };
                    MiniExcel.SaveAs(fileName, sheets);
                }))
                {
                    Form ownerForm = owner as Form;
                    if (ownerForm != null && !ownerForm.IsDisposed)
                    {
                        savingDlg.ShowDialog(ownerForm);
                    }
                    else
                    {
                        savingDlg.ShowDialog();
                    }
                }
            }
        }

        /// <summary>
        /// 从 DataGridView 读取可见列，组装导出行
        /// </summary>
        private static List<Dictionary<string, object>> BuildRowsFromGrid(DataGridView dgv)
        {
            var rows = new List<Dictionary<string, object>>();
            if (dgv == null)
            {
                return rows;
            }

            var exportColumns = dgv.Columns
                .Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .OrderBy(c => c.DisplayIndex)
                .ToList();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                var dict = new Dictionary<string, object>();
                foreach (var col in exportColumns)
                {
                    string header = string.IsNullOrEmpty(col.HeaderText)
                        ? col.Name
                        : col.HeaderText;
                    object cellValue = row.Cells[col.Index].FormattedValue ?? "";
                    dict[header] = cellValue;
                }
                rows.Add(dict);
            }

            return rows;
        }

        /// <summary>
        /// 组装总数据全量导出行
        /// </summary>
        private static List<Dictionary<string, object>> BuildAllDataExportRows(
            List<ColumnDefinition> columnDefinitions,
            List<TestParaAllData> allData,
            Dictionary<int, Dictionary<string, object>> rowDictionary)
        {
            var rows = new List<Dictionary<string, object>>();
            if (columnDefinitions == null || allData == null || allData.Count == 0)
            {
                return rows;
            }

            for (int i = 0; i < allData.Count; i++)
            {
                var record = allData[i];
                var dict = new Dictionary<string, object>();
                int recordNumber = i + 1;

                foreach (var column in columnDefinitions)
                {
                    object value = GetExportCellValue(column, record, recordNumber, rowDictionary, i);
                    dict[column.DisplayName] = FormatExportValue(value);
                }

                rows.Add(dict);
            }

            return rows;
        }

        /// <summary>
        /// 组装启动柜全量导出行，列头与界面表格保持一致
        /// </summary>
        private static List<Dictionary<string, object>> BuildStartupExportRows(List<StartupTestPara> startupData)
        {
            var rows = new List<Dictionary<string, object>>();
            if (startupData == null || startupData.Count == 0)
            {
                return rows;
            }

            for (int i = 0; i < startupData.Count; i++)
            {
                var record = startupData[i];
                rows.Add(new Dictionary<string, object>
                {
                    ["序号"] = i + 1,
                    ["记录时间"] = record.RecordDataTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    ["类型"] = record.TestName,
                    ["转速"] = record.RPM,
                    ["扭矩"] = record.Torque,
                    ["功率"] = record.Power,
                    ["励磁电压"] = record.ExcitationVoltage,
                    ["励磁电流"] = record.ExcitationCurrent,
                    ["变频电压"] = record.InvertVoltage,
                    ["变频电流"] = record.InvertCurrent,
                    ["变频转速"] = record.InvertRPM,
                    ["变频功率"] = record.InvertPower,
                    ["故障代码"] = record.InvertFaultCode,
                });
            }

            return rows;
        }

        /// <summary>
        /// 按列定义从基础字段或 MonitorData 解析出的模块对象中取值
        /// </summary>
        private static object GetExportCellValue(
            ColumnDefinition column,
            TestParaAllData record,
            int recordNumber,
            Dictionary<int, Dictionary<string, object>> rowDictionary,
            int rowIndex)
        {
            if (column.PropertyName == "Index")
            {
                return recordNumber;
            }

            if (string.IsNullOrEmpty(column.GroupName))
            {
                return column.PropertyInfo?.GetValue(record);
            }

            if (rowDictionary != null
                && rowDictionary.TryGetValue(rowIndex, out var modules)
                && modules != null
                && modules.TryGetValue(column.GroupName, out object moduleObj)
                && moduleObj != null
                && column.PropertyInfo != null)
            {
                return column.PropertyInfo.GetValue(moduleObj);
            }

            return null;
        }

        /// <summary>
        /// 将单元格值格式化为与界面显示一致的字符串
        /// </summary>
        private static object FormatExportValue(object value)
        {
            if (value == null)
            {
                return "";
            }

            if (value is double doubleValue)
            {
                return doubleValue.ToString();
            }

            if (value is DateTime dateTimeValue)
            {
                return dateTimeValue.ToString("yyyy-MM-dd HH:mm:ss");
            }

            if (value is bool booleanValue)
            {
                return booleanValue ? "1" : "0";
            }

            return value;
        }
    }
}

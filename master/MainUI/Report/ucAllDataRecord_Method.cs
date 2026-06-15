using MainUI.FSql.AllCollectData;
using MainUI.FSql.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Report
{
    /// <summary>
    /// 增加表格字段
    /// 
    /// </summary>
    public class ucAllDataRecord_Method
    {

        BaseDataGrp Base_DataGrp = new BaseDataGrp();
        TRDPDataGrp trdp = new TRDPDataGrp();
        AIDataGrp aiGrp = new AIDataGrp();
        AODataGrp aoGrp = new AODataGrp();
        DIDataGrp diGrp = new DIDataGrp();
        DODataGrp doGrp = new DODataGrp();
        ExChangeDataGrpDouble exChangeGrpDouble = new ExChangeDataGrpDouble();
        ExChangeDataGrpBool exChangeGrpBool = new ExChangeDataGrpBool();
        PipelineFaultDataGrp pipelineFaultGrp = new PipelineFaultDataGrp();
        EngineOilDataGrp engineOilGrp = new EngineOilDataGrp();
        FuelDataGrp fuelGrp = new FuelDataGrp();
        ThreePhaseElectricData threePhaseElectric = new ThreePhaseElectricData();
        WaterDataGrp waterGrp = new WaterDataGrp();
        PLC2AIDataGrp plc2AiGrp = new PLC2AIDataGrp();
        StartPLCDataGrp startPlcGrp = new StartPLCDataGrp();
        SpeedDataGrp speedGrp = new SpeedDataGrp();
        GD350_1Data gd350_1Grp = new GD350_1Data();




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
                    _columnDefinitions.Add(new ColumnDefinition("OilConsumptionMeasurePressure", "机油耗测量压力"));
                    _columnDefinitions.Add(new ColumnDefinition("T21MainOilChannelInletOilTemperature", "T21主油道进口油温"));
                    _columnDefinitions.Add(new ColumnDefinition("T5CoolWaterOutletTemperature", "T5中冷水出机温度"));
                    _columnDefinitions.Add(new ColumnDefinition("B8CylinderExhaustTemperature", "B8缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("DynamometerUPhaseTemperature", "测功机U相温度"));
                    _columnDefinitions.Add(new ColumnDefinition("HighTempWaterPumpOutletPressure", "高温水泵出口压力"));
                    _columnDefinitions.Add(new ColumnDefinition("RearSuperchargerIntakeVacuumDegree", "后增压器进气真空度"));
                    _columnDefinitions.Add(new ColumnDefinition("B4CylinderExhaustTemperature", "B4缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("InterCoolerInletWaterTemperature", "中冷器进口水温"));
                    _columnDefinitions.Add(new ColumnDefinition("DynamometerDPhaseTemperature", "测功机D相温度"));
                    _columnDefinitions.Add(new ColumnDefinition("A8CylinderExhaustTemperature", "A8缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("ExcitationVoltageDetect", "励磁电压检测"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontSuperchargerIntakeVacuumDegree", "前增压器进气真空度"));
                    _columnDefinitions.Add(new ColumnDefinition("P20OilPumpOutletPressure", "P20机油泵出口压力"));
                    _columnDefinitions.Add(new ColumnDefinition("B7CylinderExhaustTemperature", "B7缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontInterCoolerRearAirPressure", "前中冷后空气压力"));
                    _columnDefinitions.Add(new ColumnDefinition("B2CylinderExhaustTemperature", "B2缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("MainOilChannelEndOilPressure", "主油道末端油压"));
                    _columnDefinitions.Add(new ColumnDefinition("A4CylinderExhaustTemperature", "A4缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("DynamometerWPhaseTemperature", "测功机W相温度"));
                    _columnDefinitions.Add(new ColumnDefinition("T31FuelPumpInletOilTemperature", "T31燃油泵进口油温"));
                    _columnDefinitions.Add(new ColumnDefinition("P21MainOilChannelInletOilPressure", "P21主油道进口油压"));
                    _columnDefinitions.Add(new ColumnDefinition("DynamometerNPhaseTemperature", "测功机N相温度"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontInterCoolerFrontAirTemperature", "前中冷前空气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("A7CylinderExhaustTemperature", "A7缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontSuperchargerOilInletPressure", "前增压器机油进口压力"));
                    _columnDefinitions.Add(new ColumnDefinition("A2CylinderExhaustTemperature", "A2缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("InterCoolerOutletWaterTemperature", "中冷器出口水温"));
                    _columnDefinitions.Add(new ColumnDefinition("B6CylinderExhaustTemperature", "B6缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontSuperchargerOilInletTemperature", "前增压器机油进口温度"));
                    _columnDefinitions.Add(new ColumnDefinition("RearSuperchargerOilInletTemperature", "后增压器机油进口温度"));
                    _columnDefinitions.Add(new ColumnDefinition("RearSuperchargerExhaustBackPressure", "后增压器排气背压"));
                    _columnDefinitions.Add(new ColumnDefinition("RearTurboOutletExhaustGasTemperature", "后涡轮出口废气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("P38FuelSupplyPressure", "P38燃油供油压力"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontTurboInletExhaustGasPressure", "前涡轮进口废气压力"));
                    _columnDefinitions.Add(new ColumnDefinition("RearSuperchargerOilInletPressure", "后增压器机油进口压力"));
                    _columnDefinitions.Add(new ColumnDefinition("P2HighTempWaterPumpInletPressure", "P2高温水泵进口压力"));
                    _columnDefinitions.Add(new ColumnDefinition("RearInterCoolerRearAirPressure", "后中冷后空气压力"));
                    _columnDefinitions.Add(new ColumnDefinition("RearSuperchargerIntakeTemperature", "后增压器进气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontInterCoolerRearAirTemperature", "前中冷后空气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("A6CylinderExhaustTemperature", "A6缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontSuperchargerExhaustBackPressure", "前增压器排气背压"));
                    _columnDefinitions.Add(new ColumnDefinition("T20OilPumpOutletOilTemperature", "T20机油泵出口油温"));
                    _columnDefinitions.Add(new ColumnDefinition("T1HighTempWaterOutletTemperature", "T1高温水出机温度"));
                    _columnDefinitions.Add(new ColumnDefinition("RearTurboInletExhaustGasTemperature", "后涡轮进口废气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("RearInterCoolerFrontAirTemperature", "后中冷前空气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("ExcitationCurrentDetect", "励磁电流检测"));
                    _columnDefinitions.Add(new ColumnDefinition("T2HighTempWaterInletTemperature", "T2高温水进机温度"));
                    _columnDefinitions.Add(new ColumnDefinition("T3CoolWaterInletTemperature", "T3中冷水进机温度"));
                    _columnDefinitions.Add(new ColumnDefinition("OilConsumptionMeasureLevel", "机油耗测量液位"));
                    _columnDefinitions.Add(new ColumnDefinition("T30FuelReturnOilTemperature", "T30燃油回油温度"));
                    _columnDefinitions.Add(new ColumnDefinition("B3CylinderExhaustTemperature", "B3缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("RearTurboInletExhaustGasPressure", "后涡轮进口废气压力"));
                    _columnDefinitions.Add(new ColumnDefinition("RearInterCoolerRearAirTemperature", "后中冷后空气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("CoolWaterPumpOutletPressure", "中冷水泵出口压力"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontInterCoolerFrontAirPressure", "前中冷前空气压力"));
                    _columnDefinitions.Add(new ColumnDefinition("DynamometerVPhaseTemperature", "测功机V相温度"));
                    _columnDefinitions.Add(new ColumnDefinition("B1CylinderExhaustTemperature", "B1缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontSuperchargerOilOutletTemperature", "前增压器机油出口温度"));
                    _columnDefinitions.Add(new ColumnDefinition("RearSuperchargerOilOutletTemperature", "后增压器机油出口温度"));
                    _columnDefinitions.Add(new ColumnDefinition("A3CylinderExhaustTemperature", "A3缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("P3CoolWaterPumpInletPressure", "P3中冷水泵进口压力"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontTurboOutletExhaustGasTemperature", "前涡轮出口废气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("A1CylinderExhaustTemperature", "A1缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontSuperchargerIntakeTemperature", "前增压器进气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("P1HighTempWaterOutletPressure", "P1高温水出机压力"));
                    _columnDefinitions.Add(new ColumnDefinition("B5CylinderExhaustTemperature", "B5缸排气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("P5CoolWaterOutletPressure", "P5中冷水出机压力"));
                    _columnDefinitions.Add(new ColumnDefinition("FrontTurboInletExhaustGasTemperature", "前涡轮进口废气温度"));
                    _columnDefinitions.Add(new ColumnDefinition("RearInterCoolerFrontAirPressure", "后中冷前空气压力"));
                    _columnDefinitions.Add(new ColumnDefinition("A5CylinderExhaustTemperature", "A5缸排气温度"));
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

                
            }

            return _columnDefinitions;
        }


        //解析json数据，填入到对应的实体类中
        public Dictionary<int, Dictionary<string, object>> AddRowValue(List<Dictionary<string, object>> allDataList, List<string> KeyNameList, Dictionary<int, Dictionary<string, object>> RowDictionary)
        {
            int index = 0;
            RowDictionary.Clear();
            foreach (var item in allDataList)
            {
                RowDictionary[index] = new Dictionary<string, object>();
                if (item.TryGetValue("BaseGrp", out var baseObj) && baseObj is JObject baseJObj && KeyNameList.Any(key => key == "BaseDataGrp"))
                {

                    var baseDataGrp = baseJObj.ToObject<Dictionary<string, object>>();
                    Base_DataGrp.Weight = Convert.ToDouble(baseDataGrp["Base_Weight"]);
                    Base_DataGrp.RPM = Convert.ToDouble(baseDataGrp["Base_EngineSpeed"]);
                    Base_DataGrp.Torque = Convert.ToDouble(baseDataGrp["Base_EngineTorque"]);
                    Base_DataGrp.Power = Convert.ToDouble(baseDataGrp["Base_EnginePower"]);
                    RowDictionary[index]["BaseDataGrp"] = CloneRowEntity(Base_DataGrp);

                }


                // 3. 取 TRDP 某个测点
                if (item.TryGetValue("TRDP", out var trdpObj) && trdpObj is JObject trdpJObj && KeyNameList.Any(key => key == "TRDPDataGrp"))
                {
                    var trdpData = trdpJObj.ToObject<Dictionary<string, object>>();

                    trdp.FrontTurbochargerRPM = Convert.ToDouble(trdpData["TRDP_前增压器转速"]);
                    trdp.AfterTurbochargerRPM = Convert.ToDouble(trdpData["TRDP_后增压器转速"]);
                    trdp.RotateSpeedSensor1 = Convert.ToDouble(trdpData["TRDP_转速传感器1#"]);
                    trdp.RotateSpeedSensor2 = Convert.ToDouble(trdpData["TRDP_转速传感器2#"]);
                    trdp.PhaseSensor = Convert.ToDouble(trdpData["TRDP_相位传感器"]);
                    trdp.RotateSpeedSet = Convert.ToDouble(trdpData["TRDP_转速设定"]);
                    trdp.FuelQuantity = Convert.ToDouble(trdpData["TRDP_燃油量"]);
                    trdp.PowerA = Convert.ToDouble(trdpData["TRDP_电源A"]);
                    trdp.PowerB = Convert.ToDouble(trdpData["TRDP_电源B"]);
                    trdp.EmergencyAlarm = Convert.ToDouble(trdpData["TRDP_紧急报警"]);
                    trdp.PublicAlarm = Convert.ToDouble(trdpData["TRDP_公共报警"]);
                    trdp.Duration = Convert.ToDouble(trdpData["TRDP_持续期"]);
                    trdp.AdvanceAngle = Convert.ToDouble(trdpData["TRDP_提前角"]);
                    trdp.SyncStatus = Convert.ToDouble(trdpData["TRDP_同步状态"]);
                    trdp.ECURunTime = Convert.ToDouble(trdpData["TRDP_ECU运行时间"]);
                    trdp.SolenoidValveFault1 = Convert.ToDouble(trdpData["TRDP_电磁阀故障1#"]);
                    trdp.SolenoidValveFault2 = Convert.ToDouble(trdpData["TRDP_电磁阀故障2#"]);
                    trdp.SolenoidValveFault3 = Convert.ToDouble(trdpData["TRDP_电磁阀故障3#"]);
                    trdp.SolenoidValveFault4 = Convert.ToDouble(trdpData["TRDP_电磁阀故障4#"]);
                    trdp.SolenoidValveFault5 = Convert.ToDouble(trdpData["TRDP_电磁阀故障5#"]);
                    trdp.SolenoidValveFault6 = Convert.ToDouble(trdpData["TRDP_电磁阀故障6#"]);
                    trdp.SolenoidValveFault7 = Convert.ToDouble(trdpData["TRDP_电磁阀故障7#"]);
                    trdp.SolenoidValveFault8 = Convert.ToDouble(trdpData["TRDP_电磁阀故障8#"]);
                    trdp.SolenoidValveFault9 = Convert.ToDouble(trdpData["TRDP_电磁阀故障9#"]);
                    trdp.SolenoidValveFault10 = Convert.ToDouble(trdpData["TRDP_电磁阀故障10#"]);
                    trdp.SolenoidValveFault11 = Convert.ToDouble(trdpData["TRDP_电磁阀故障11#"]);
                    trdp.SolenoidValveFault12 = Convert.ToDouble(trdpData["TRDP_电磁阀故障12#"]);
                    trdp.PowerSupplyFault = Convert.ToDouble(trdpData["TRDP_供电电源故障"]);
                    trdp.RotateSpeedSensorFault1 = Convert.ToDouble(trdpData["TRDP_转速传感器故障1#"]);
                    trdp.RotateSpeedSensorFault2 = Convert.ToDouble(trdpData["TRDP_转速传感器故障2#"]);
                    trdp.PhaseSensorFault = Convert.ToDouble(trdpData["TRDP_相位传感器故障"]);
                    trdp.OverSpeedFault = Convert.ToDouble(trdpData["TRDP_超速故障"]);
                    trdp.SyncInputFault = Convert.ToDouble(trdpData["TRDP_同步输入故障"]);
                    trdp.HardwareFault = Convert.ToDouble(trdpData["TRDP_硬件故障"]);
                    trdp.SyncFault = Convert.ToDouble(trdpData["TRDP_同步故障"]);
                    trdp.VoltageAmplifierFaultA = Convert.ToDouble(trdpData["TRDP_电压放大器故障A"]);
                    trdp.VoltageAmplifierFaultB = Convert.ToDouble(trdpData["TRDP_电压放大器故障B"]);
                    trdp.VoltageAmplifierFaultC = Convert.ToDouble(trdpData["TRDP_电压放大器故障C"]);
                    trdp.OilPumpOutletOilPressure = Convert.ToDouble(trdpData["TRDP_机油泵出口油压"]);
                    trdp.FuelFineFilterFrontOilPressure = Convert.ToDouble(trdpData["TRDP_燃油精滤器前油压"]);
                    trdp.FuelFineFilterRearOilPressure = Convert.ToDouble(trdpData["TRDP_燃油精滤器后油压"]);
                    trdp.FrontSuperchargerInletOilPressure = Convert.ToDouble(trdpData["TRDP_前增压器进口油压"]);
                    trdp.RearSuperchargerInletOilPressure = Convert.ToDouble(trdpData["TRDP_后增压器进口油压"]);
                    trdp.HighTempWaterPumpOutletPressure = Convert.ToDouble(trdpData["TRDP_高温水泵出口压力"]);
                    trdp.MainOilChannelEndOilPressure = Convert.ToDouble(trdpData["TRDP_主油道末端油压"]);
                    trdp.MainOilChannelInletOilPressure = Convert.ToDouble(trdpData["TRDP_主油道进口油压"]);
                    trdp.HighTempWaterOutletTemperature = Convert.ToDouble(trdpData["TRDP_高温水出水温度"]);
                    trdp.CoolWaterInletTemperature = Convert.ToDouble(trdpData["TRDP_中冷水进水温度"]);
                    trdp.CoolWaterOutletTemperature = Convert.ToDouble(trdpData["TRDP_中冷水出水温度"]);
                    trdp.MainOilChannelInletOilTemperature = Convert.ToDouble(trdpData["TRDP_主油道进口油温"]);
                    trdp.OilPumpOutletOilTemperature = Convert.ToDouble(trdpData["TRDP_机油泵出口油温"]);
                    trdp.CoolWaterPumpOutletPressure = Convert.ToDouble(trdpData["TRDP_中冷水泵出口压力"]);
                    trdp.FrontCompressorOutletAirTemperature = Convert.ToDouble(trdpData["TRDP_前压气机出口空气温度"]);
                    trdp.RearCompressorOutletAirTemperature = Convert.ToDouble(trdpData["TRDP_后压气机出口空气温度"]);
                    trdp.FrontSuperchargerReturnOilTemperature = Convert.ToDouble(trdpData["TRDP_前增压器回油温度"]);
                    trdp.RearInterCoolerRearAirTemperature = Convert.ToDouble(trdpData["TRDP_后中冷器后空气温度"]);
                    trdp.RearSuperchargerReturnOilTemperature = Convert.ToDouble(trdpData["TRDP_后增压器回油温度"]);
                    trdp.RearInterCoolerRearAirPressure = Convert.ToDouble(trdpData["TRDP_后中冷后空气压力"]);
                    trdp.A1CylinderExhaustTemperature = Convert.ToDouble(trdpData["TRDP_A1缸排气温度"]);
                    trdp.A2CylinderExhaustTemperature = Convert.ToDouble(trdpData["TRDP_A2缸排气温度"]);
                    trdp.A3CylinderExhaustTemperature = Convert.ToDouble(trdpData["TRDP_A3缸排气温度"]);
                    trdp.A4CylinderExhaustTemperature = Convert.ToDouble(trdpData["TRDP_A4缸排气温度"]);
                    trdp.A5CylinderExhaustTemperature = Convert.ToDouble(trdpData["TRDP_A5缸排气温度"]);
                    trdp.A6CylinderExhaustTemperature = Convert.ToDouble(trdpData["TRDP_A6缸排气温度"]);
                    trdp.AVortexFrontExhaustTemperature = Convert.ToDouble(trdpData["TRDP_A涡前排气温度"]);
                    trdp.B1CylinderExhaustTemperature = Convert.ToDouble(trdpData["TRDP_B1缸排气温度"]);
                    trdp.B2CylinderExhaustTemperature = Convert.ToDouble(trdpData["TRDP_B2缸排气温度"]);
                    trdp.B3CylinderExhaustTemperature = Convert.ToDouble(trdpData["TRDP_B3缸排气温度"]);
                    trdp.B4CylinderExhaustTemperature = Convert.ToDouble(trdpData["TRDP_B4缸排气温度"]);
                    trdp.B5CylinderExhaustTemperature = Convert.ToDouble(trdpData["TRDP_B5缸排气温度"]);
                    trdp.B6CylinderExhaustTemperature = Convert.ToDouble(trdpData["TRDP_B6缸排气温度"]);
                    trdp.BVortexFrontExhaustTemperature = Convert.ToDouble(trdpData["TRDP_B涡前排气温度"]);
                    trdp.FirstGearShaftTemperature = Convert.ToDouble(trdpData["TRDP_一档轴温"]);
                    trdp.SecondGearShaftTemperature = Convert.ToDouble(trdpData["TRDP_二档轴温"]);
                    trdp.ThirdGearShaftTemperature = Convert.ToDouble(trdpData["TRDP_三档轴温"]);
                    trdp.FourthGearShaftTemperature = Convert.ToDouble(trdpData["TRDP_四档轴温"]);
                    trdp.FifthGearShaftTemperature = Convert.ToDouble(trdpData["TRDP_五档轴温"]);
                    trdp.SixthGearShaftTemperature = Convert.ToDouble(trdpData["TRDP_六档轴温"]);
                    trdp.SeventhGearShaftTemperature = Convert.ToDouble(trdpData["TRDP_七档轴温"]);
                    trdp.DeviceLifeSignal = Convert.ToDouble(trdpData["TRDP_设备生命信号"]);
                    trdp.NetworkPort0Fault = Convert.ToDouble(trdpData["TRDP_网口0故障"]);
                    trdp.Slave1SerialPortFault = Convert.ToDouble(trdpData["TRDP_从站1串口故障"]);
                    trdp.Slave2SerialPortFault = Convert.ToDouble(trdpData["TRDP_从站2串口故障"]);
                    trdp.Slave3SerialPortFault = Convert.ToDouble(trdpData["TRDP_从站3串口故障"]);
                    trdp.Slave4SerialPortFault = Convert.ToDouble(trdpData["TRDP_从站4串口故障"]);
                    trdp.Slave5SerialPortFault = Convert.ToDouble(trdpData["TRDP_从站5串口故障"]);
                    trdp.Slave6SerialPortFault = Convert.ToDouble(trdpData["TRDP_从站6串口故障"]);
                    trdp.Slave7SerialPortFault = Convert.ToDouble(trdpData["TRDP_从站7串口故障"]);
                    RowDictionary[index]["TRDPDataGrp"] = CloneRowEntity(trdp);
                }



                // 3. 取 AI 某个测点
                if (item.TryGetValue("AIGrp", out var AIObj) && AIObj is JObject AIJObj && KeyNameList.Any(key => key == "AIDataGrp"))
                {
                    var aiGrpData = AIJObj.ToObject<Dictionary<string, object>>();
                    aiGrp.AT = Convert.ToDouble(aiGrpData["AI_大气温度"]);
                    aiGrp.AP = Convert.ToDouble(aiGrpData["AI_大气压力"]);
                    aiGrp.AH = Convert.ToDouble(aiGrpData["AI_大气湿度"]);
                    aiGrp.HWaterFlow = Convert.ToDouble(aiGrpData["AI_高温水流量测量-L3"]);
                    aiGrp.InAirFlowLeft = Convert.ToDouble(aiGrpData["AI_进气流量测量左"]);
                    aiGrp.LWaterFlow = Convert.ToDouble(aiGrpData["AI_中冷水流量测量-L8"]);
                    aiGrp.LWaterBoxLevel = Convert.ToDouble(aiGrpData["AI_中冷水膨胀水箱液位检测"]);
                    aiGrp.cleanOilFlow = Convert.ToDouble(aiGrpData["AI_清洁油罐来油流量"]);
                    aiGrp.EngineOilFlow = Convert.ToDouble(aiGrpData["AI_机油流量"]);
                    aiGrp.OilFlow_L31 = Convert.ToDouble(aiGrpData["AI_燃油回油流量测量-L31"]);
                    aiGrp.InAirFlowRight = Convert.ToDouble(aiGrpData["AI_进气流量测量右"]);
                    aiGrp.BeforeInAirTemp = Convert.ToDouble(aiGrpData["AI_前增压器进气流量计前温度"]);
                    aiGrp.AfterInAirTemp = Convert.ToDouble(aiGrpData["AI_后增压器进气流量计前温度"]);
                    aiGrp.FactoryAirPressureIn1 = Convert.ToDouble(aiGrpData["AI_厂房进气压力检测1"]);
                    aiGrp.FactoryAirPressureIn2 = Convert.ToDouble(aiGrpData["AI_厂房进气压力检测2"]);
                    aiGrp.L32 = Convert.ToDouble(aiGrpData["AI_L32"]);
                    aiGrp.OilFlowIn_L30 = Convert.ToDouble(aiGrpData["AI_燃油进油流量测量-L30"]);
                    aiGrp.HWaterBoxLevel = Convert.ToDouble(aiGrpData["AI_高温水膨胀水箱液位检测"]);

                    RowDictionary[index]["AIDataGrp"] = CloneRowEntity(aiGrp);
                }

                // 4. 取 AO 某个测点
                if (item.TryGetValue("AOGrp", out var AOObj) && AOObj is JObject AOJObj && KeyNameList.Any(key => key == "AODataGrp"))
                {
                    var aoGrpData = AOJObj.ToObject<Dictionary<string, object>>();
                    aoGrp.AirOutChannelValve = Convert.ToDouble(aoGrpData["AO_排气风道右调节阀控制"]);
                    aoGrp.EngineLowSpeed = Convert.ToDouble(aoGrpData["AO_设置发动机最低转速"]);
                    aoGrp.WaterOutValve_18 = Convert.ToDouble(aoGrpData["AO_水泵出口电动调节阀控制-18"]);
                    aoGrp.EngineOilValve = Convert.ToDouble(aoGrpData["AO_发动机油门调节"]);
                    aoGrp.AirOutFlowValveLeft = Convert.ToDouble(aoGrpData["AO_排气风道左调节阀控制"]);
                    aoGrp.Excitation = Convert.ToDouble(aoGrpData["AO_励磁调节"]);
                    aoGrp.AO_OilBy_Pass1Valve_194 = Convert.ToDouble(aoGrpData["AO_燃油泵旁路1电动调节阀控制-194"]);
                    aoGrp.AirInFlowleftValve = Convert.ToDouble(aoGrpData["AO_进气风道左调节阀控制"]);
                    aoGrp.Oil1Vlave_170 = Convert.ToDouble(aoGrpData["AO_燃油泵1电动调节阀控制-170"]);
                    aoGrp.AirFlowRightValve = Convert.ToDouble(aoGrpData["AO_进气风道右调节阀控制"]);
                    aoGrp.WaterResistanceBoxValve = Convert.ToDouble(aoGrpData["AO_水阻箱进水电动调节阀"]);

                    RowDictionary[index]["AODataGrp"] = CloneRowEntity(aoGrp);
                }


                // 5. 取 DI 某个测点
                if (item.TryGetValue("DIGrp", out var DIObj) && DIObj is JObject DIJObj && KeyNameList.Any(key => key == "DIDataGrp"))
                {
                    var diGrpData = DIJObj.ToObject<Dictionary<string, object>>();
                    diGrp.OilByPass1ValveLocal_194 = Convert.ToBoolean(diGrpData["DI_燃油泵旁路1电动调节阀就地-194"]);
                    diGrp.DrawMidCoolWaterOpenLimit_31 = Convert.ToBoolean(diGrpData["DI_抽中冷水开到位-31"]);
                    diGrp.PreHeatTankHeaterCtrl5Detect = Convert.ToBoolean(diGrpData["DI_预热水箱加热器控制5检测"]);
                    diGrp.OilProcessTankOutOpenLimit_136 = Convert.ToBoolean(diGrpData["DI_机油处理箱出开到位-136"]);
                    diGrp.DieselTankOilInOpenLimit_164 = Convert.ToBoolean(diGrpData["DI_柴油箱进油开到位-164"]);
                    diGrp.PumpAndTankWaterOutOpenLimit_20 = Convert.ToBoolean(diGrpData["DI_泵和水箱出水开到位-20"]);
                    diGrp.CoolerOilReturnCloseLimit_90 = Convert.ToBoolean(diGrpData["DI_冷却器回油关到位-90"]);
                    diGrp.PendingOilTankSupplementOpenLimit_139 = Convert.ToBoolean(diGrpData["DI_待处理机油箱补油开到位-139"]);
                    diGrp.DieselEngineStart = Convert.ToBoolean(diGrpData["DI_柴油机启动"]);
                    diGrp.OilPumpOilInOpenLimit_96 = Convert.ToBoolean(diGrpData["DI_油泵进油开到位-96"]);
                    diGrp.OilCoolerWaterInValveFault_89 = Convert.ToBoolean(diGrpData["DI_机油冷却器进水电动调节阀故障-89"]);
                    diGrp.ExhaustDuctRightValveClose = Convert.ToBoolean(diGrpData["DI_排气风道右调节阀关"]);
                    diGrp.PreHeatTankWaterOutOpenLimit_15 = Convert.ToBoolean(diGrpData["DI_预热水箱出水开到位-15"]);
                    diGrp.HighTempWaterExpTankOutCloseLimit_30 = Convert.ToBoolean(diGrpData["DI_高温水膨胀水箱出关到位-30"]);
                    diGrp.PendingOilTankConnectProcessorOpenLimit_134 = Convert.ToBoolean(diGrpData["DI_待处理机油箱通处理机开到位-134"]);
                    diGrp.OilPumpPreSupplyCloseLimit_93 = Convert.ToBoolean(diGrpData["DI_机油泵预供油关到位-93"]);
                    diGrp.CoolWaterInCloseLimit_61 = Convert.ToBoolean(diGrpData["DI_冷却水进水关到位-61"]);
                    diGrp.EmergencyStop2OpenLimit_182 = Convert.ToBoolean(diGrpData["DI_紧急停车2开到位-182"]);
                    diGrp.OilPumpOilOutCloseLimit_97 = Convert.ToBoolean(diGrpData["DI_油泵出油关到位-97"]);
                    diGrp.DrawOilPumpThermalRelayDetect = Convert.ToBoolean(diGrpData["DI_抽油泵热继检测"]);
                    diGrp.OilTankOilOutOpenLimit_111 = Convert.ToBoolean(diGrpData["DI_机油箱出油开到位-111"]);
                    diGrp.ControlRoom2CabFrontDoorDetect = Convert.ToBoolean(diGrpData["DI_控制间2柜前门检测"]);
                    diGrp.EmergencyStop1OpenLimit_181 = Convert.ToBoolean(diGrpData["DI_紧急停车1开到位-181"]);
                    diGrp.MainFanPower = Convert.ToBoolean(diGrpData["DI_主发通风机电源"]);
                    diGrp.HighTempWaterExpTankOutOpenLimit_30 = Convert.ToBoolean(diGrpData["DI_高温水膨胀水箱出开到位-30"]);
                    diGrp.HighTempWaterCloseLimit_3 = Convert.ToBoolean(diGrpData["DI_高温水关到位-3"]);
                    diGrp.OilPumpPreSupplyOpenLimit_93 = Convert.ToBoolean(diGrpData["DI_机油泵预供油开到位-93"]);
                    diGrp.OilPumpPipeOilInCloseLimit_95 = Convert.ToBoolean(diGrpData["DI_油泵管路进油关到位-95"]);
                    diGrp.PreHeatPumpYDetect = Convert.ToBoolean(diGrpData["DI_预热水泵Y检测"]);
                    diGrp.DieselTankOilInCloseLimit_164 = Convert.ToBoolean(diGrpData["DI_柴油箱进油关到位-164"]);
                    diGrp.MainFan1YDetect = Convert.ToBoolean(diGrpData["DI_主发通风机1Y检测"]);
                    diGrp.OilTankPumpOilInOpenLimit_115 = Convert.ToBoolean(diGrpData["DI_机油箱油泵来油开到位-115"]);
                    diGrp.PreHeatTankWaterReturnCloseLimit_24 = Convert.ToBoolean(diGrpData["DI_预热水箱回水关到位-24"]);
                    diGrp.OilByPass1ValveFullyClose_194 = Convert.ToBoolean(diGrpData["DI_燃油泵旁路1电动调节阀全关-194"]);
                    diGrp.OilByPass1ValveFullyOpen_194 = Convert.ToBoolean(diGrpData["DI_燃油泵旁路1电动调节阀全开-194"]);
                    diGrp.MainFan2Detect = Convert.ToBoolean(diGrpData["DI_主发通风机2检测"]);
                    diGrp.StartCabinetPowerDetect = Convert.ToBoolean(diGrpData["DI_启动柜电源检测"]);
                    diGrp.MainFan1Detect = Convert.ToBoolean(diGrpData["DI_主发通风机1检测"]);
                    diGrp.PendingOilTankOilInOpenLimit_116 = Convert.ToBoolean(diGrpData["DI_待处理机油箱进油开到位-116"]);
                    diGrp.OilPumpOilInCloseLimit_96 = Convert.ToBoolean(diGrpData["DI_油泵进油关到位-96"]);
                    diGrp.DieselEngineUnload = Convert.ToBoolean(diGrpData["DI_柴油机卸载"]);
                    diGrp.MidCoolWaterExpTankHighLevel = Convert.ToBoolean(diGrpData["DI_中冷水膨胀水箱高液位"]);
                    diGrp.OilSumpFillCloseLimit_122 = Convert.ToBoolean(diGrpData["DI_油底壳加油关到位-122"]);
                    diGrp.WaterResistLiftLowerLimitDetect = Convert.ToBoolean(diGrpData["DI_水阻升降下极限检测"]);
                    diGrp.OilPumpPreHeatPumpPowerDetect = Convert.ToBoolean(diGrpData["DI_燃油泵/预热水泵电源检测"]);
                    diGrp.OilByPass1ValveFault_194 = Convert.ToBoolean(diGrpData["DI_燃油泵旁路1电动调节阀故障-194"]);
                    diGrp.FrontOilFilter1CloseLimit_91 = Convert.ToBoolean(diGrpData["DI_机滤器前1关到位-91"]);
                    diGrp.EmergencyStop = Convert.ToBoolean(diGrpData["DI_紧急停止"]);
                    diGrp.OilPump1ValveLocal_170 = Convert.ToBoolean(diGrpData["DI_燃油泵1电动调节阀就地-170"]);
                    diGrp.FuelMeterCommuteInCloseLimit_183 = Convert.ToBoolean(diGrpData["DI_油耗仪换向进关到位-183"]);
                    diGrp.EquipmentRoomPowerDetect = Convert.ToBoolean(diGrpData["DI_设备间电源检测"]);
                    diGrp.MidCoolWaterInValveFault_88 = Convert.ToBoolean(diGrpData["DI_中冷水冷却水进口电动调节阀故障-88"]);
                    diGrp.OilPumpOilOutOpenLimit_97 = Convert.ToBoolean(diGrpData["DI_油泵出油开到位-97"]);
                    diGrp.ControlPowerDetect = Convert.ToBoolean(diGrpData["DI_控制电源检测"]);
                    diGrp.MidCoolWaterCircOpenLimit_41 = Convert.ToBoolean(diGrpData["DI_中冷水循环开到位-41"]);
                    diGrp.OilTankCleanOilInCloseLimit_137 = Convert.ToBoolean(diGrpData["DI_机油箱清洁来油关到位-137"]);
                    diGrp.ExhaustDuctLeftValveOpen = Convert.ToBoolean(diGrpData["DI_排气风道左调节阀开"]);
                    diGrp.MainExcitationCabPower = Convert.ToBoolean(diGrpData["DI_主发励磁柜电源"]);
                    diGrp.PreHeatTankWaterFillOpenLimit_27 = Convert.ToBoolean(diGrpData["DI_预热水箱加水开到位-27"]);
                    diGrp.OilProcessTankInOpenLimit_135 = Convert.ToBoolean(diGrpData["DI_机油处理箱进开到位-135"]);
                    diGrp.OilPreSupplyPumpStop = Convert.ToBoolean(diGrpData["DI_机油预供泵停止"]);
                    diGrp.PreHeatTankHeaterCtrl2Detect = Convert.ToBoolean(diGrpData["DI_预热水箱加热器控制2检测"]);
                    diGrp.PreHeatTankHeaterCtrl1Detect = Convert.ToBoolean(diGrpData["DI_预热水箱加热器控制1检测"]);
                    diGrp.WaterPumpOutValveFullyOpen_18 = Convert.ToBoolean(diGrpData["DI_水泵出口电动调节阀全开-18"]);
                    diGrp.WasteOilDrainPumpYDetect = Convert.ToBoolean(diGrpData["DI_污油排出泵Y检测"]);
                    diGrp.PreHeatTankHeaterCtrl4Detect = Convert.ToBoolean(diGrpData["DI_预热水箱加热器控制4检测"]);
                    diGrp.OilPump1ValveFault_170 = Convert.ToBoolean(diGrpData["DI_燃油泵1电动调节阀故障-170"]);
                    diGrp.OilReturnOpenLimit_179 = Convert.ToBoolean(diGrpData["DI_回油开到位-179"]);
                    diGrp.InnerCircWaterInOpenLimit_28 = Convert.ToBoolean(diGrpData["DI_内循环水来水开到位-28"]);
                    diGrp.WaterResistLiftUpperLimitDetect = Convert.ToBoolean(diGrpData["DI_水阻升降上极限检测"]);
                    diGrp.FuelMeterCommuteReturnCloseLimit_184 = Convert.ToBoolean(diGrpData["DI_油耗仪换向回关到位-184"]);
                    diGrp.IntakeDuctRightValveOpen = Convert.ToBoolean(diGrpData["DI_进气风道右调节阀开"]);
                    diGrp.FuelFillStart = Convert.ToBoolean(diGrpData["DI_燃油加油开始"]);
                    diGrp.ExhaustDuctRightValveFault = Convert.ToBoolean(diGrpData["DI_排气风道右调节阀故障"]);
                    diGrp.EquipmentRoomCabFrontDoorDetect = Convert.ToBoolean(diGrpData["DI_设备间控制柜体前门检测"]);
                    diGrp.PreHeatTankPowerDetect = Convert.ToBoolean(diGrpData["DI_预热水箱电源检测"]);
                    diGrp.FrontOilFilter2CloseLimit_100 = Convert.ToBoolean(diGrpData["DI_机滤器前2关到位-100"]);
                    diGrp.PumpAndTankFrontWaterReturnCloseLimit_23 = Convert.ToBoolean(diGrpData["DI_泵和水箱前回水关到位-23"]);
                    diGrp.PreHeatTankWaterReturnOpenLimit_24 = Convert.ToBoolean(diGrpData["DI_预热水箱回水开到位-24"]);
                    diGrp.WaterResistLiftMotorUpClosingDetect = Convert.ToBoolean(diGrpData["DI_水阻升降电机上升合闸检测"]);
                    diGrp.OilPreSupplyPumpPowerDetect = Convert.ToBoolean(diGrpData["DI_预供机油泵电源检测"]);
                    diGrp.PreHeatPumpInOpenLimit_16 = Convert.ToBoolean(diGrpData["DI_预热水泵进口开到位-16"]);
                    diGrp.OilSumpFillOpenLimit_122 = Convert.ToBoolean(diGrpData["DI_油底壳加油开到位-122"]);
                    diGrp.DrawHighTempWaterOpenLimit_21 = Convert.ToBoolean(diGrpData["DI_抽高温水开到位-21"]);
                    diGrp.ExhaustDuctRightValveOpen = Convert.ToBoolean(diGrpData["DI_排气风道右调节阀开"]);
                    diGrp.PreHeatTankWaterFillCloseLimit_26 = Convert.ToBoolean(diGrpData["DI_预热水箱加水关到位-26"]);
                    diGrp.FrontOilFilter1OpenLimit_91 = Convert.ToBoolean(diGrpData["DI_机滤器前1开到位-91"]);
                    diGrp.WasteOilDrainPumpThermalRelayDetect = Convert.ToBoolean(diGrpData["DI_污油排出泵热继检测"]);
                    diGrp.IntakeDuctLeftValveClose = Convert.ToBoolean(diGrpData["DI_进气风道左调节阀关"]);
                    diGrp.PreHeatTankWaterOutCloseLimit_15 = Convert.ToBoolean(diGrpData["DI_预热水箱出水关到位-15"]);
                    diGrp.OilSumpDrawOilCloseLimit_92 = Convert.ToBoolean(diGrpData["DI_油底壳抽油关到位-92"]);
                    diGrp.BarringInterlockSwitch = Convert.ToBoolean(diGrpData["DI_盘车连锁开关"]);
                    diGrp.PendingOilTankOilInCloseLimit_116 = Convert.ToBoolean(diGrpData["DI_待处理机油箱进油关到位-116"]);
                    diGrp.OilOutlet1CloseLimit_190 = Convert.ToBoolean(diGrpData["DI_出油路1关到位-190"]);
                    diGrp.PreHeatPumpThermalRelayDetect = Convert.ToBoolean(diGrpData["DI_预热水泵热继检测"]);
                    diGrp.MidCoolWaterExpTankLowLevel = Convert.ToBoolean(diGrpData["DI_中冷水膨胀水箱低液位"]);
                    diGrp.PumpAndTankFrontWaterReturnOpenLimit_23 = Convert.ToBoolean(diGrpData["DI_泵和水箱前回水开到位-23"]);
                    diGrp.WasteOilDrainPumpMainContactDetect = Convert.ToBoolean(diGrpData["DI_污油排出泵主接检测"]);
                    diGrp.InnerCircWaterInCloseLimit_28 = Convert.ToBoolean(diGrpData["DI_内循环水来水关到位-28"]);
                    diGrp.OilProcessTankOutCloseLimit_136 = Convert.ToBoolean(diGrpData["DI_机油处理箱出关到位-136"]);
                    diGrp.IntakeHeaterPowerDetect = Convert.ToBoolean(diGrpData["DI_进气加热电源检测"]);
                    diGrp.MainFan1MainContactDetect = Convert.ToBoolean(diGrpData["DI_主发通风机1主接检测"]);
                    diGrp.PreHeatPumpInCloseLimit_16 = Convert.ToBoolean(diGrpData["DI_预热水泵进口关到位-16"]);
                    diGrp.HighTempWaterExpTankHighLevel = Convert.ToBoolean(diGrpData["DI_高温水膨胀水箱高液位"]);
                    diGrp.MachineRoomDistCabFrontDoorDetect = Convert.ToBoolean(diGrpData["DI_机器间配电柜体前门检测"]);
                    diGrp.DrawMidCoolWaterCloseLimit_31 = Convert.ToBoolean(diGrpData["DI_抽中冷水关到位-31"]);
                    diGrp.WasteOilDrainPumpPowerDetect = Convert.ToBoolean(diGrpData["DI_污油排出泵电源检测"]);
                    diGrp.DrawHighTempWaterCloseLimit_21 = Convert.ToBoolean(diGrpData["DI_抽高温水关到位-21"]);
                    diGrp.BackupPowerDetect = Convert.ToBoolean(diGrpData["DI_备用电源检测"]);
                    diGrp.FuelMeterCommuteInOpenLimit_183 = Convert.ToBoolean(diGrpData["DI_油耗仪换向进开到位-183"]);
                    diGrp.DrawOilPumpClosingDetect = Convert.ToBoolean(diGrpData["DI_抽油泵合闸检测"]);
                    diGrp.WaterResistLiftMotorThermalRelayDetect = Convert.ToBoolean(diGrpData["DI_水阻升降电机热继检测"]);
                    diGrp.CoolWaterInOpenLimit_61 = Convert.ToBoolean(diGrpData["DI_冷却水进水开到位-61"]);
                    diGrp.MachineRoomDistCabRearDoorDetect = Convert.ToBoolean(diGrpData["DI_机器间配电柜体后门检测"]);
                    diGrp.OilPump1ValveFullyOpen_170 = Convert.ToBoolean(diGrpData["DI_燃油泵1电动调节阀全开-170"]);
                    diGrp.ExpTankWaterMakeupInOpenLimit_29 = Convert.ToBoolean(diGrpData["DI_膨胀水箱补水进开到位-29"]);
                    diGrp.HighTempWaterOpenLimit_3 = Convert.ToBoolean(diGrpData["DI_高温水开到位-3"]);
                    diGrp.MainFan1ThermalRelayDetect = Convert.ToBoolean(diGrpData["DI_主发通风机1热继检测"]);
                    diGrp.IntakeDuctRightValveClose = Convert.ToBoolean(diGrpData["DI_进气风道右调节阀关"]);
                    diGrp.WaterPumpOutValveFault_18 = Convert.ToBoolean(diGrpData["DI_水泵出口电动调节阀故障-18"]);
                    diGrp.PendingOilTankSupplementCloseLimit_139 = Convert.ToBoolean(diGrpData["DI_待处理机油箱补油关到位-139"]);
                    diGrp.MachineRoomPowerDetect = Convert.ToBoolean(diGrpData["DI_机器间电源检测"]);
                    diGrp.EquipmentRoomCabRearDoorDetect = Convert.ToBoolean(diGrpData["DI_设备间控制柜体后门检测"]);
                    diGrp.HighTempCoolWaterInValveFault_87 = Convert.ToBoolean(diGrpData["DI_高温水冷却水进口电动调节阀故障-87"]);
                    diGrp.PreHeatPumpMainContactDetect = Convert.ToBoolean(diGrpData["DI_预热水泵主接检测"]);
                    diGrp.OilReturnCloseLimit_179 = Convert.ToBoolean(diGrpData["DI_回油关到位-179"]);
                    diGrp.MainFan2YDetect = Convert.ToBoolean(diGrpData["DI_主发通风机2Y检测"]);
                    diGrp.OilPump1ValveFullyClose_170 = Convert.ToBoolean(diGrpData["DI_燃油泵1电动调节阀全关-170"]);
                    diGrp.MainFan2MainContactDetect = Convert.ToBoolean(diGrpData["DI_主发通风机2主接检测"]);
                    diGrp.PreHeatTankWaterFillOpenLimit_26 = Convert.ToBoolean(diGrpData["DI_预热水箱加水开到位-26"]);
                    diGrp.DieselEngineStop = Convert.ToBoolean(diGrpData["DI_柴油机停机"]);
                    diGrp.OilPreSupplyPumpClosingDetect = Convert.ToBoolean(diGrpData["DI_预供机油泵合闸检测"]);
                    diGrp.WasteOilDrainPumpDetect = Convert.ToBoolean(diGrpData["DI_污油排出泵检测"]);
                    diGrp.PreHeatPumpOutCloseLimit_17 = Convert.ToBoolean(diGrpData["DI_预热水泵出口关到位-17"]);
                    diGrp.EngineDC24VPowerDist = Convert.ToBoolean(diGrpData["DI_发动机DC24V配电"]);
                    diGrp.WaterResistLifterPower = Convert.ToBoolean(diGrpData["DI_水阻升降机电源"]);
                    diGrp.OilTankOilOutCloseLimit_111 = Convert.ToBoolean(diGrpData["DI_机油箱出油关到位-111"]);
                    diGrp.PendingOilTankConnectProcessorCloseLimit_134 = Convert.ToBoolean(diGrpData["DI_待处理机油箱通处理机关到位-134"]);
                    diGrp.ExhaustDuctLeftValveClose = Convert.ToBoolean(diGrpData["DI_排气风道左调节阀关"]);
                    diGrp.OilPump2ClosingDetect = Convert.ToBoolean(diGrpData["DI_燃油泵2合闸检测"]);
                    diGrp.PumpAndTankWaterOutCloseLimit_20 = Convert.ToBoolean(diGrpData["DI_泵和水箱出水关到位-20"]);
                    diGrp.EmergencyStop1CloseLimit_181 = Convert.ToBoolean(diGrpData["DI_紧急停车1关到位-181"]);
                    diGrp.MainFan2ThermalRelayDetect = Convert.ToBoolean(diGrpData["DI_主发通风机2热继检测"]);
                    diGrp.OilTankCleanOilInOpenLimit_137 = Convert.ToBoolean(diGrpData["DI_机油箱清洁来油开到位-137"]);
                    diGrp.PreHeatPumpOutOpenLimit_17 = Convert.ToBoolean(diGrpData["DI_预热水泵出口开到位-17"]);
                    diGrp.OilPumpPipeOilInOpenLimit_95 = Convert.ToBoolean(diGrpData["DI_油泵管路进油开到位-95"]);
                    diGrp.OilPump1ClosingDetect = Convert.ToBoolean(diGrpData["DI_燃油泵1合闸检测"]);
                    diGrp.OilProcessTankInCloseLimit_135 = Convert.ToBoolean(diGrpData["DI_机油处理箱进关到位-135"]);
                    diGrp.FrontOilFilter2OpenLimit_100 = Convert.ToBoolean(diGrpData["DI_机滤器前2开到位-100"]);
                    diGrp.OilPreSupplyPumpStart = Convert.ToBoolean(diGrpData["DI_机油预供泵启动"]);
                    diGrp.ExpTankWaterMakeupInCloseLimit_29 = Convert.ToBoolean(diGrpData["DI_膨胀水箱补水进关到位-29"]);
                    diGrp.OilOutlet1OpenLimit_190 = Convert.ToBoolean(diGrpData["DI_出油路1开到位-190"]);
                    diGrp.CoolerOilReturnOpenLimit_90 = Convert.ToBoolean(diGrpData["DI_冷却器回油开到位-90"]);
                    diGrp.HighTempWaterExpTankLowLevel = Convert.ToBoolean(diGrpData["DI_高温水膨胀水箱低液位"]);
                    diGrp.PreHeatTankWaterFillCloseLimit_27 = Convert.ToBoolean(diGrpData["DI_预热水箱加水关到位-27"]);
                    diGrp.MidCoolWaterCircCloseLimit_41 = Convert.ToBoolean(diGrpData["DI_中冷水循环关到位-41"]);
                    diGrp.ControlRoom3CabFrontDoorDetect = Convert.ToBoolean(diGrpData["DI_控制间3柜前门检测"]);
                    diGrp.WaterResistLiftMotorDownClosingDetect = Convert.ToBoolean(diGrpData["DI_水阻升降电机下降合闸检测"]);
                    diGrp.IntakeDuctLeftValveOpen = Convert.ToBoolean(diGrpData["DI_进气风道左调节阀开"]);
                    diGrp.OilTankPumpOilInCloseLimit_115 = Convert.ToBoolean(diGrpData["DI_机油箱油泵来油关到位-115"]);
                    diGrp.IntakeDuctLeftValveFault = Convert.ToBoolean(diGrpData["DI_进气风道左调节阀故障"]);
                    diGrp.CrankcasePressureSwitch = Convert.ToBoolean(diGrpData["DI_曲轴箱压力开关"]);
                    diGrp.WaterPumpOutValveFullyClose_18 = Convert.ToBoolean(diGrpData["DI_水泵出口电动调节阀全关-18"]);
                    diGrp.PreHeatPumpDetect = Convert.ToBoolean(diGrpData["DI_预热水泵检测"]);
                    diGrp.ExhaustDuctLeftValveFault = Convert.ToBoolean(diGrpData["DI_排气风道左调节阀故障"]);
                    diGrp.FuelMeterCommuteReturnOpenLimit_184 = Convert.ToBoolean(diGrpData["DI_油耗仪换向回开到位-184"]);
                    diGrp.OilTankToProcessorOpenLimit_133 = Convert.ToBoolean(diGrpData["DI_机油箱到处理机开到位-133"]);
                    diGrp.WaterPumpOutValveLocal_18 = Convert.ToBoolean(diGrpData["DI_水泵出口电动调节阀就地-18"]);
                    diGrp.IntakeDuctRightValveFault = Convert.ToBoolean(diGrpData["DI_进气风道右调节阀故障"]);
                    diGrp.OilPump2ThermalRelayDetect = Convert.ToBoolean(diGrpData["DI_燃油泵2热继检测"]);
                    diGrp.ExhaustTreatmentUnitPowerDetect = Convert.ToBoolean(diGrpData["DI_尾气处理装置电源检测"]);
                    diGrp.PreHeatTankWaterReturnCloseLimit_22 = Convert.ToBoolean(diGrpData["DI_预热水箱回水关到位-22"]);
                    diGrp.OilTankToProcessorCloseLimit_133 = Convert.ToBoolean(diGrpData["DI_机油箱到处理机关到位-133"]);
                    diGrp.OilProcessorPowerDetect = Convert.ToBoolean(diGrpData["DI_机油处理机电源检测"]);
                    diGrp.ControlRoom1CabFrontDoorDetect = Convert.ToBoolean(diGrpData["DI_控制间1柜前门检测"]);
                    diGrp.EmergencyStop2CloseLimit_182 = Convert.ToBoolean(diGrpData["DI_紧急停车2关到位-182"]);
                    diGrp.OilSumpDrawOilOpenLimit_92 = Convert.ToBoolean(diGrpData["DI_油底壳抽油开到位-92"]);
                    diGrp.PreHeatTankHeaterCtrl3Detect = Convert.ToBoolean(diGrpData["DI_预热水箱加热器控制3检测"]);
                    diGrp.OilPreSupplyPumpThermalRelayDetect = Convert.ToBoolean(diGrpData["DI_预供机油泵热继检测"]);
                    diGrp.PreHeatTankWaterReturnOpenLimit_22 = Convert.ToBoolean(diGrpData["DI_预热水箱回水开到位-22"]);
                    diGrp.OilPump1ThermalRelayDetect = Convert.ToBoolean(diGrpData["DI_燃油泵1热继检测"]);
                    diGrp.PreHeatTankHeaterCtrl6Detect = Convert.ToBoolean(diGrpData["DI_预热水箱加热器控制6检测"]);


                    RowDictionary[index]["DIDataGrp"] = CloneRowEntity(diGrp);
                }


                // 5. 取 DO 某个测点
                if (item.TryGetValue("DOGrp", out var DOObj) && DOObj is JObject DOJObj && KeyNameList.Any(key => key == "DODataGrp"))
                {
                    var doGrpData = DOJObj.ToObject<Dictionary<string, object>>();
                    doGrp.No2LeftExhaustPipeValve1 = Convert.ToBoolean(doGrpData["DO_2号左排气管阀1"]);
                    doGrp.Y139ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y139阀控制"]);
                    doGrp.Y184ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y184阀控制"]);
                    doGrp.Y23ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y23阀控制"]);
                    doGrp.Y93ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y93阀控制"]);
                    doGrp.Y179ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y179阀控制"]);
                    doGrp.Y41ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y41阀控制"]);
                    doGrp.Y61ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y61阀控制"]);
                    doGrp.FaultReset = Convert.ToBoolean(doGrpData["DO_故障复位"]);
                    doGrp.Y27ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y27阀控制"]);
                    doGrp.Y97ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y97阀控制"]);
                    doGrp.Y100ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y100阀控制"]);
                    doGrp.WasteOilDrainPumpCloseCtrl = Convert.ToBoolean(doGrpData["DO_污油排出泵合闸控制"]);
                    doGrp.PreHeatPumpCloseCtrl = Convert.ToBoolean(doGrpData["DO_预热水泵合闸控制"]);
                    doGrp.Y115ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y115阀控制"]);
                    doGrp.Y181ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y181阀控制"]);
                    doGrp.Y134ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y134阀控制"]);
                    doGrp.EngineDC24VPower = Convert.ToBoolean(doGrpData["DO_发动机DC24V供电"]);
                    doGrp.BuzzerCtrl = Convert.ToBoolean(doGrpData["DO_蜂鸣器控制"]);
                    doGrp.No1RightIntakePipeValve1 = Convert.ToBoolean(doGrpData["DO_1号右进气管阀1"]);
                    doGrp.Y122ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y122阀控制"]);
                    doGrp.WaterResistBoxValveClose = Convert.ToBoolean(doGrpData["DO_水阻箱调节阀关"]);
                    doGrp.OilPump2CloseCtrl = Convert.ToBoolean(doGrpData["DO_燃油泵2合闸控制"]);
                    doGrp.Y183ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y183阀控制"]);
                    doGrp.No1RightIntakePipeValve2 = Convert.ToBoolean(doGrpData["DO_1号右进气管阀2"]);
                    doGrp.Y16ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y16阀控制"]);
                    doGrp.Y137ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y137阀控制"]);
                    doGrp.DrawOilPumpCloseCtrl = Convert.ToBoolean(doGrpData["DO_抽油泵合闸控制"]);
                    doGrp.PreHeatTankHeatCtrl = Convert.ToBoolean(doGrpData["DO_预热水箱加热控制"]);
                    doGrp.No1RightExhaustPipeValve2 = Convert.ToBoolean(doGrpData["DO_1号右排气管阀2"]);
                    doGrp.WaterResistDownCtrl = Convert.ToBoolean(doGrpData["DO_水阻下降控制"]);
                    doGrp.Y111ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y111阀控制"]);
                    doGrp.MainFan1CloseCtrl = Convert.ToBoolean(doGrpData["DO_主发通风机1合闸控制"]);
                    doGrp.MainFan2CloseCtrl = Convert.ToBoolean(doGrpData["DO_主发通风机2合闸控制"]);
                    doGrp.Y15ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y15阀控制"]);
                    doGrp.WaterResistBoxValveOpen = Convert.ToBoolean(doGrpData["DO_水阻箱调节阀开"]);
                    doGrp.No1RightExhaustPipeValve1 = Convert.ToBoolean(doGrpData["DO_1号右排气管阀1"]);
                    doGrp.Y29ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y29阀控制"]);
                    doGrp.Y164ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y164阀控制"]);
                    doGrp.Y28ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y28阀控制"]);
                    doGrp.Y22ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y22阀控制"]);
                    doGrp.Y92ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y92阀控制"]);
                    doGrp.No2LeftIntakePipeValve1 = Convert.ToBoolean(doGrpData["DO_2号左进气管阀1"]);
                    doGrp.Y133ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y133阀控制"]);
                    doGrp.WaterResistUpCtrl = Convert.ToBoolean(doGrpData["DO_水阻上升控制"]);
                    doGrp.Y31ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y31阀控制"]);
                    doGrp.EnginePreStart = Convert.ToBoolean(doGrpData["DO_发动机启停预启动"]);
                    doGrp.Y03ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y03阀控制"]);
                    doGrp.Y21ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y21阀控制"]);
                    doGrp.Y26ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y26阀控制"]);
                    doGrp.Y96ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y96阀控制"]);
                    doGrp.Y91ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y91阀控制"]);
                    doGrp.Y17ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y17阀控制"]);
                    doGrp.No2LeftIntakePipeValve2 = Convert.ToBoolean(doGrpData["DO_2号左进气管阀2"]);
                    doGrp.Y116ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y116阀控制"]);
                    doGrp.Y30ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y30阀控制"]);
                    doGrp.PreSupplyOilPumpCloseCtrl = Convert.ToBoolean(doGrpData["DO_预供机油泵合闸控制"]);
                    doGrp.Y182ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y182阀控制"]);
                    doGrp.Y135ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y135阀控制"]);
                    doGrp.Y20ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y20阀控制"]);
                    doGrp.No2LeftExhaustPipeValve2 = Convert.ToBoolean(doGrpData["DO_2号左排气管阀2"]);
                    doGrp.Y95ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y95阀控制"]);
                    doGrp.Y90ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y90阀控制"]);
                    doGrp.Y190ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y190阀控制"]);
                    doGrp.Y136ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y136阀控制"]);
                    doGrp.OilPump1CloseCtrl = Convert.ToBoolean(doGrpData["DO_燃油泵1合闸控制"]);
                    doGrp.Y24ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y24阀控制"]);
                    //doGrp.MainFan2MainContactCtrl = Convert.ToBoolean(doGrpData["DO_主发通风机2主接控制"]);
                    //doGrp.MainFan2Ctrl = Convert.ToBoolean(doGrpData["DO_主发通风机2控制"]);
                    //doGrp.MainFan2YCtrl = Convert.ToBoolean(doGrpData["DO_主发通风机2Y控制"]);
                    //doGrp.MainFan1MainContactCtrl = Convert.ToBoolean(doGrpData["DO_主发通风机1主接控制"]);
                    //doGrp.MainFan1Ctrl = Convert.ToBoolean(doGrpData["DO_主发通风机1控制"]);
                    //doGrp.MainFan1YCtrl = Convert.ToBoolean(doGrpData["DO_主发通风机1Y控制"]);
                    //doGrp.PreHeatTankHeat6 = Convert.ToBoolean(doGrpData["DO_预热水箱加热6"]);
                    //doGrp.PreHeatTankHeat5 = Convert.ToBoolean(doGrpData["DO_预热水箱加热5"]);
                    //doGrp.PreHeatTankHeat4 = Convert.ToBoolean(doGrpData["DO_预热水箱加热4"]);
                    //doGrp.PreHeatTankHeat3 = Convert.ToBoolean(doGrpData["DO_预热水箱加热3"]);
                    //doGrp.PreHeatTankHeat2 = Convert.ToBoolean(doGrpData["DO_预热水箱加热2"]);
                    //doGrp.PreHeatTankHeat1 = Convert.ToBoolean(doGrpData["DO_预热水箱加热1"]);
                    //doGrp.PreHeatPumpMainContact = Convert.ToBoolean(doGrpData["DO_预热水泵主接触器"]);
                    //doGrp.PreHeatPumpStart = Convert.ToBoolean(doGrpData["DO_预热水泵启动"]);
                    //doGrp.PreHeatPumpYStart = Convert.ToBoolean(doGrpData["DO_预热水泵Y启动"]);
                    //doGrp.PreSupplyOilPumpCtrl = Convert.ToBoolean(doGrpData["DO_预供机油泵控制"]);
                    //doGrp.WasteOilDrainPumpMainContact = Convert.ToBoolean(doGrpData["DO_污油排出泵主接触器"]);
                    //doGrp.WasteOilDrainPumpStart = Convert.ToBoolean(doGrpData["DO_污油排出泵启动"]);
                    //doGrp.WasteOilDrainPumpYStart = Convert.ToBoolean(doGrpData["DO_污油排出泵Y启动"]);
                    //doGrp.WaterResistLiftMotorDownCtrl = Convert.ToBoolean(doGrpData["DO_水阻升降电机下降控制"]);
                    //doGrp.WaterResistLiftMotorUpCtrl = Convert.ToBoolean(doGrpData["DO_水阻升降电机上升控制"]);
                    //doGrp.OilPump2Ctrl = Convert.ToBoolean(doGrpData["DO_燃油泵2控制"]);
                    //doGrp.OilPump1Ctrl = Convert.ToBoolean(doGrpData["DO_燃油泵1控制"]);
                    //doGrp.OilPreSupplyPumpStopIndicate = Convert.ToBoolean(doGrpData["DO_机油预供泵停止指示"]);
                    //doGrp.OilPreSupplyPumpStartIndicate = Convert.ToBoolean(doGrpData["DO_机油预供泵启动指示"]);
                    //doGrp.DrawOilPumpCtrl = Convert.ToBoolean(doGrpData["DO_抽油泵控制"]);
                    //doGrp.EngineStopIndicate = Convert.ToBoolean(doGrpData["DO_柴油机停止指示"]);
                    //doGrp.EngineStartIndicate = Convert.ToBoolean(doGrpData["DO_柴油机启动指示"]);
                    //doGrp.Y55ValveCtrl = Convert.ToBoolean(doGrpData["DO_Y55阀控制"]);

                    RowDictionary[index]["DODataGrp"] = CloneRowEntity(doGrp);
                }


                // 6. 取 ExChangeGrpDouble 某个测点
                if (item.TryGetValue("ExChangeGrpDouble", out var ExChangeObj) && ExChangeObj is JObject ExChangeJObj && KeyNameList.Any(key => key == "ExChangeDataGrpDouble"))
                {
                    var ExChangeGrpData = ExChangeJObj.ToObject<Dictionary<string, object>>();
                    exChangeGrpDouble.PreHeatTankTemperature = Convert.ToDouble(ExChangeGrpData["ExChangeGrpDouble_预热水箱温度"]);
                    exChangeGrpDouble.PreHeatTankLevel = Convert.ToDouble(ExChangeGrpData["ExChangeGrpDouble_预热水箱液位"]);
                    exChangeGrpDouble.HighTempWaterOutMachineTemperature = Convert.ToDouble(ExChangeGrpData["ExChangeGrpDouble_高温水出机温度"]);
                    exChangeGrpDouble.OilOutMachinePressure = Convert.ToDouble(ExChangeGrpData["ExChangeGrpDouble_机油出机压力"]);
                    exChangeGrpDouble.DieselEngineRotateSpeed = Convert.ToDouble(ExChangeGrpData["ExChangeGrpDouble_柴油机转速"]);
                    exChangeGrpDouble.PreHeatTankHeatTempSet = Convert.ToDouble(ExChangeGrpData["ExChangeGrpDouble_预热水箱加热温度设定"]);
                    exChangeGrpDouble.OilTankTemperature = Convert.ToDouble(ExChangeGrpData["ExChangeGrpDouble_机油箱温度"]);
                    exChangeGrpDouble.FuelInMachinePressure = Convert.ToDouble(ExChangeGrpData["ExChangeGrpDouble_燃油进机压力"]);
                    exChangeGrpDouble.OilTankLevel = Convert.ToDouble(ExChangeGrpData["ExChangeGrpDouble_机油箱液位"]);
                    exChangeGrpDouble.InnerCircWaterTankLevel = Convert.ToDouble(ExChangeGrpData["ExChangeGrpDouble_内循环水箱液位"]);
                    exChangeGrpDouble.PendingOilTankTemperature = Convert.ToDouble(ExChangeGrpData["ExChangeGrpDouble_待处理机油箱温度"]);
                    exChangeGrpDouble.HighTempWaterInMachineTemperature = Convert.ToDouble(ExChangeGrpData["ExChangeGrpDouble_高温水进机温度"]);
                    exChangeGrpDouble.FuelInMachineTemperature = Convert.ToDouble(ExChangeGrpData["ExChangeGrpDouble_燃油进机温度"]);
                    exChangeGrpDouble.PendingOilTankLevel = Convert.ToDouble(ExChangeGrpData["ExChangeGrpDouble_待处理机油箱液位"]);
                    exChangeGrpDouble.OilInMachinePressure = Convert.ToDouble(ExChangeGrpData["ExChangeGrpDouble_机油进机压力"]);
                    exChangeGrpDouble.FuelTankLevel = Convert.ToDouble(ExChangeGrpData["ExChangeGrpDouble_燃油箱液位"]);

                    RowDictionary[index]["ExChangeDataGrpDouble"] = CloneRowEntity(exChangeGrpDouble);
                }


                // 7. 取 ExChangeGrpBool 某个测点
                if (item.TryGetValue("ExChangeGrpBool", out var ExChangebObj) && ExChangebObj is JObject ExChangebJObj && KeyNameList.Any(key => key == "ExChangeDataGrpBool"))
                {
                    var ExChangebGrpData = ExChangebJObj.ToObject<Dictionary<string, object>>();
                    exChangeGrpBool.HighTempWaterPreheatCycle = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_高温水预热循环"]);
                    exChangeGrpBool.FuelConsumptionMeasurePumpSelect = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_燃油耗测量油泵选择"]);
                    exChangeGrpBool.OilSumpExtractSelectTank = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_油底壳抽油选择油箱"]);
                    exChangeGrpBool.FuelCycle = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_燃油循环"]);
                    exChangeGrpBool.OilSumpExtract = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_油底壳抽油"]);
                    exChangeGrpBool.HighTempWaterColdWaterBackExtract = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_高温水中冷水回抽"]);
                    exChangeGrpBool.OilTankRefuel = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_机油箱加油"]);
                    exChangeGrpBool.UpperComputerShutdownCtrl = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_上位机停机控制"]);
                    exChangeGrpBool.PreSupplyOilCycle = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_预供机油循环"]);
                    exChangeGrpBool.PreHeatTankHeating = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_预热水箱加热"]);
                    exChangeGrpBool.FuelConsumptionMeasure = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_燃油耗测量"]);
                    exChangeGrpBool.OilSumpRefuel = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_油底壳加油"]);
                    exChangeGrpBool.OilBackExtract = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_机油回抽"]);
                    exChangeGrpBool.PreHeatTankWaterAdd = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_预热水箱加水"]);
                    exChangeGrpBool.FuelTankReturnOilCool = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_燃油箱回油冷却"]);
                    exChangeGrpBool.FuelCyclePumpSelect = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_燃油循环油泵选择"]);
                    exChangeGrpBool.OilHeatProcessCycle = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_机油加热处理循环"]);
                    //exChangeGrpBool.ColdWaterExpansionTankWaterAdd = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_中冷水膨胀水箱加水"]);
                    //exChangeGrpBool.PreheatOilCycle = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_预热机油循环"]);
                    //exChangeGrpBool.FuelTankRefuelCycle = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_燃油箱加油循环"]);
                    //exChangeGrpBool.HighTempWaterExpansionTankWaterAdd = Convert.ToBoolean(ExChangebGrpData["ExChangeGrpBool_高温水膨胀水箱加水"]);


                    RowDictionary[index]["ExChangeDataGrpBool"] = CloneRowEntity(exChangeGrpBool);
                }


                // 8. 取 PipelineFaultGrp 某个测点
                if (item.TryGetValue("PipelineFaultGrp", out var PFObj) && PFObj is JObject PFJObj && KeyNameList.Any(key => key == "PipelineFaultDataGrp"))
                {
                    var pfGrpData = PFJObj.ToObject<Dictionary<string, object>>();
                    pipelineFaultGrp.OilTankLevelHigh_OilBackExtractStop = Convert.ToBoolean(pfGrpData["Fault_机油箱液位高，机油回抽已停止"]);
                    pipelineFaultGrp.Valve164Fault = Convert.ToBoolean(pfGrpData["Fault_164阀故障"]);
                    pipelineFaultGrp.Valve137Fault = Convert.ToBoolean(pfGrpData["Fault_137阀故障"]);
                    pipelineFaultGrp.Valve03Fault = Convert.ToBoolean(pfGrpData["Fault_03阀故障"]);
                    pipelineFaultGrp.FuelPump1OverCurrent = Convert.ToBoolean(pfGrpData["Fault_燃油泵1过流"]);
                    pipelineFaultGrp.Valve29Fault = Convert.ToBoolean(pfGrpData["Fault_29阀故障"]);
                    pipelineFaultGrp.OilControlBoxDistIOCommDisconnect = Convert.ToBoolean(pfGrpData["Fault_机油控制箱分布式IO通讯掉线"]);
                    pipelineFaultGrp.Valve135Fault = Convert.ToBoolean(pfGrpData["Fault_135阀故障"]);
                    pipelineFaultGrp.Valve28Fault = Convert.ToBoolean(pfGrpData["Fault_28阀故障"]);
                    pipelineFaultGrp.StartCabinetCommDisconnect = Convert.ToBoolean(pfGrpData["Fault_启动柜通讯掉线"]);
                    pipelineFaultGrp.PreHeatTankLevelHigh_WaterAddStop = Convert.ToBoolean(pfGrpData["Fault_预热水箱水位高，预热水箱加水已停止"]);
                    pipelineFaultGrp.OilTankLevelHigh_OilSumpExtractStop = Convert.ToBoolean(pfGrpData["Fault_机油箱液位高，油底壳抽油已停止"]);
                    pipelineFaultGrp.PendingOilTankLevelHigh_OilSumpExtractStop = Convert.ToBoolean(pfGrpData["Fault_待处理机油箱液位高，油底壳抽油已停止"]);
                    pipelineFaultGrp.Valve133Fault = Convert.ToBoolean(pfGrpData["Fault_133阀故障"]);
                    pipelineFaultGrp.Valve115Fault = Convert.ToBoolean(pfGrpData["Fault_115阀故障"]);
                    pipelineFaultGrp.PreHeatTankLevelHigh_ColdHotWaterBackExtractStop = Convert.ToBoolean(pfGrpData["Fault_预热水箱箱液位高，中冷水/高温水回抽己停止"]);
                    pipelineFaultGrp.PreHeatPumpOverCurrent = Convert.ToBoolean(pfGrpData["Fault_预热水泵过流"]);
                    pipelineFaultGrp.InExhaustControlBoxDistIOCommDisconnect = Convert.ToBoolean(pfGrpData["Fault_进排气控制箱分布式IO通讯掉线"]);
                    pipelineFaultGrp.ControlRoomPowerCabinetDistIOCommDisconnect = Convert.ToBoolean(pfGrpData["Fault_控制间配电柜分布式IO通讯掉线"]);
                    pipelineFaultGrp.Valve97Fault = Convert.ToBoolean(pfGrpData["Fault_97阀故障"]);
                    pipelineFaultGrp.Valve111Fault = Convert.ToBoolean(pfGrpData["Fault_111阀故障"]);
                    pipelineFaultGrp.Valve27Fault = Convert.ToBoolean(pfGrpData["Fault_27阀故障"]);
                    pipelineFaultGrp.FuelPump2OverCurrent = Convert.ToBoolean(pfGrpData["Fault_燃油泵2过流"]);
                    pipelineFaultGrp.Valve31Fault = Convert.ToBoolean(pfGrpData["Fault_31阀故障"]);
                    pipelineFaultGrp.Valve96Fault = Convert.ToBoolean(pfGrpData["Fault_96阀故障"]);
                    pipelineFaultGrp.FuelControlBoxDistIOCommDisconnect = Convert.ToBoolean(pfGrpData["Fault_燃油控制箱分布式IO通讯掉线"]);
                    pipelineFaultGrp.Valve26Fault = Convert.ToBoolean(pfGrpData["Fault_26阀故障"]);
                    pipelineFaultGrp.Valve61Fault = Convert.ToBoolean(pfGrpData["Fault_61阀故障"]);
                    pipelineFaultGrp.Valve30Fault = Convert.ToBoolean(pfGrpData["Fault_30阀故障"]);
                    pipelineFaultGrp.Valve95Fault = Convert.ToBoolean(pfGrpData["Fault_95阀故障"]);
                    pipelineFaultGrp.MainFan2OverCurrent = Convert.ToBoolean(pfGrpData["Fault_主发通风机2过流"]);
                    pipelineFaultGrp.OilTankLevelLow_OilSumpRefuelStop = Convert.ToBoolean(pfGrpData["Fault_机油箱液位低，油底壳加油已停止"]);
                    pipelineFaultGrp.Valve136Fault = Convert.ToBoolean(pfGrpData["Fault_136阀故障"]);
                    pipelineFaultGrp.Valve24Fault = Convert.ToBoolean(pfGrpData["Fault_24阀故障"]);
                    pipelineFaultGrp.Valve190Fault = Convert.ToBoolean(pfGrpData["Fault_190阀故障"]);
                    pipelineFaultGrp.PreHeatTankLevelLow_HighTempWaterPreheatStop = Convert.ToBoolean(pfGrpData["Fault_预热水箱水位低，高温水预热循环已停止"]);
                    pipelineFaultGrp.Valve93Fault = Convert.ToBoolean(pfGrpData["Fault_93阀故障"]);
                    pipelineFaultGrp.MachineRoomPowerCabinetDistIOCommDisconnect = Convert.ToBoolean(pfGrpData["Fault_机器间配电柜分布式IO通讯掉线"]);
                    pipelineFaultGrp.Valve23Fault = Convert.ToBoolean(pfGrpData["Fault_23阀故障"]);
                    pipelineFaultGrp.MainFan1OverCurrent = Convert.ToBoolean(pfGrpData["Fault_主发通风机1过流"]);
                    pipelineFaultGrp.Valve17Fault = Convert.ToBoolean(pfGrpData["Fault_17阀故障"]);
                    pipelineFaultGrp.PreSupplyOilPumpOverCurrent = Convert.ToBoolean(pfGrpData["Fault_预供机油泵过流"]);
                    pipelineFaultGrp.WasteOilDrainPumpOverCurrent = Convert.ToBoolean(pfGrpData["Fault_污油排出泵过流"]);
                    pipelineFaultGrp.Valve92Fault = Convert.ToBoolean(pfGrpData["Fault_92阀故障"]);
                    pipelineFaultGrp.Valve22Fault = Convert.ToBoolean(pfGrpData["Fault_22阀故障"]);
                    pipelineFaultGrp.Valve16Fault = Convert.ToBoolean(pfGrpData["Fault_16阀故障"]);
                    pipelineFaultGrp.OilTankLevelHigh_OilTankRefuelStop = Convert.ToBoolean(pfGrpData["Fault_机油箱液位高，机油箱加油已停止"]);
                    pipelineFaultGrp.Valve100Fault = Convert.ToBoolean(pfGrpData["Fault_100阀故障"]);
                    pipelineFaultGrp.Valve91Fault = Convert.ToBoolean(pfGrpData["Fault_91阀故障"]);
                    pipelineFaultGrp.Valve21Fault = Convert.ToBoolean(pfGrpData["Fault_21阀故障"]);
                    pipelineFaultGrp.Valve15Fault = Convert.ToBoolean(pfGrpData["Fault_15阀故障"]);
                    pipelineFaultGrp.Valve134Fault = Convert.ToBoolean(pfGrpData["Fault_134阀故障"]);
                    pipelineFaultGrp.Valve116Fault = Convert.ToBoolean(pfGrpData["Fault_116阀故障"]);
                    pipelineFaultGrp.WaterResistLiftMotorOverCurrent = Convert.ToBoolean(pfGrpData["Fault_水阻升降电机过流"]);
                    pipelineFaultGrp.Valve90Fault = Convert.ToBoolean(pfGrpData["Fault_90阀故障"]);
                    pipelineFaultGrp.Valve20Fault = Convert.ToBoolean(pfGrpData["Fault_20阀故障"]);
                    pipelineFaultGrp.DrawOilPumpOverCurrent = Convert.ToBoolean(pfGrpData["Fault_抽油泵过流"]);
                    pipelineFaultGrp.EmergencyStop_RotateSpeedNotDrop = Convert.ToBoolean(pfGrpData["Fault_急停后转速不下降"]);
                    pipelineFaultGrp.Valve139Fault = Convert.ToBoolean(pfGrpData["Fault_139阀故障"]);
                    pipelineFaultGrp.WaterSystemControlBoxDistIOCommDisconnect = Convert.ToBoolean(pfGrpData["Fault_水系统控制箱分布式IO通讯掉线"]);
                    pipelineFaultGrp.EquipmentRoomPowerCabinetDistIOCommDisconnect = Convert.ToBoolean(pfGrpData["Fault_设备间配电柜分布式IO通讯掉线"]);
                    pipelineFaultGrp.Valve122Fault = Convert.ToBoolean(pfGrpData["Fault_122阀故障"]);
                    pipelineFaultGrp.FuelTankLevelLow_FuelCycleStop = Convert.ToBoolean(pfGrpData["Fault_燃油箱液位低，燃油循环已停止"]);
                    pipelineFaultGrp.Valve41Fault = Convert.ToBoolean(pfGrpData["Fault_41阀故障"]);
                    pipelineFaultGrp.Valve179Fault = Convert.ToBoolean(pfGrpData["Fault_179阀故障"]);


                    RowDictionary[index]["PipelineFaultDataGrp"] = CloneRowEntity(pipelineFaultGrp);
                }


                // 9. 取 EngineOilGrp 某个测点
                if (item.TryGetValue("EngineOilGrp", out var EngineOilObj) && EngineOilObj is JObject EngineOilJObj && KeyNameList.Any(key => key == "EngineOilDataGrp"))
                {
                    var EngineOilGrpData = EngineOilJObj.ToObject<Dictionary<string, object>>();
                    engineOilGrp.FlowMeterRearPressureDetectP29 = Convert.ToDouble(EngineOilGrpData["Oil_流量计口后压力检测-P29"]);
                    engineOilGrp.OilTankOutletRearPressureDetectP23 = Convert.ToDouble(EngineOilGrpData["Oil_机油箱出口后压力检测-P23"]);
                    engineOilGrp.OilTankOutletFrontPressureDetectP22 = Convert.ToDouble(EngineOilGrpData["Oil_机油箱出口前压力检测-P22"]);
                    engineOilGrp.OilTemperaturePassword = Convert.ToDouble(EngineOilGrpData["Oil_机油温度密码"]);
                    engineOilGrp.OilTemperatureRealTimePID = Convert.ToDouble(EngineOilGrpData["Oil_机油温度实时PID"]);
                    engineOilGrp.PendingOilTankLevelDetectL19 = Convert.ToDouble(EngineOilGrpData["Oil_待处理机油箱液位检测-L19"]);
                    engineOilGrp.CoolerInletOilTemperatureT25 = Convert.ToDouble(EngineOilGrpData["Oil_冷却器进口油温-T25"]);
                    engineOilGrp.PreSupplyOilPressureDetectP19 = Convert.ToDouble(EngineOilGrpData["Oil_预供机油压力检测-P19"]);
                    engineOilGrp.FrontPressureDetectP24 = Convert.ToDouble(EngineOilGrpData["Oil_前压力检测-P24"]);
                    engineOilGrp.OilTemperatureSetPID = Convert.ToDouble(EngineOilGrpData["Oil_机油温度设置PID"]);
                    engineOilGrp.RearPressureDetectP25 = Convert.ToDouble(EngineOilGrpData["Oil_后压力检测-P25"]);
                    engineOilGrp.OilTankLevelDetectL18 = Convert.ToDouble(EngineOilGrpData["Oil_机油箱液位检测-L18"]);
                    engineOilGrp.OilTankTemperatureDetectT23 = Convert.ToDouble(EngineOilGrpData["Oil_机油箱温度检测-T23"]);
                    engineOilGrp.FlowMeterFrontPressureDetectP28 = Convert.ToDouble(EngineOilGrpData["Oil_流量计口前压力检测-P28"]);
                    engineOilGrp.Front1PressureDetectP26 = Convert.ToDouble(EngineOilGrpData["Oil_前1压力检测-P26"]);
                    engineOilGrp.Rear1PressureDetectP27 = Convert.ToDouble(EngineOilGrpData["Oil_后1压力检测-P27"]);
                    engineOilGrp.PendingOilTankTemperatureDetectT24 = Convert.ToDouble(EngineOilGrpData["Oil_待处理机油箱温度检测-T24"]);
                    //engineOilGrp.OilTemperaturePIDUpperLimit = Convert.ToDouble(EngineOilGrpData["Oil_机油温度PID上限值"]);


                    RowDictionary[index]["EngineOilDataGrp"] = CloneRowEntity(engineOilGrp);
                }


                // 10. 取 FuelGrp 某个测点
                if (item.TryGetValue("FuelGrp", out var FuelObj) && FuelObj is JObject FuelJObj && KeyNameList.Any(key => key == "FuelDataGrp"))
                {
                    var FuelGrpData = FuelJObj.ToObject<Dictionary<string, object>>();
                    fuelGrp.WaterResistBoxTemperatureDetect = Convert.ToDouble(FuelGrpData["Fuel_水阻箱温度检测"]);
                    fuelGrp.FineFilter2FrontPressureDetectP36 = Convert.ToDouble(FuelGrpData["Fuel_精滤器2前压力检测-P36"]);
                    fuelGrp.WaterResistBoxPolarPlateDisplacementDetect = Convert.ToDouble(FuelGrpData["Fuel_水阻箱极板位移检测"]);
                    fuelGrp.CoarseFilter2RearPressureDetectP33 = Convert.ToDouble(FuelGrpData["Fuel_粗滤器2后压力检测-P33"]);
                    fuelGrp.FineFilter1FrontPressureDetectP34 = Convert.ToDouble(FuelGrpData["Fuel_精滤器1前压力检测-P34"]);
                    fuelGrp.CoarseFilter1RearPressureDetectP31 = Convert.ToDouble(FuelGrpData["Fuel_粗滤器1后压力检测-P31"]);
                    fuelGrp.FineFilter2RearPressureDetectP37 = Convert.ToDouble(FuelGrpData["Fuel_精滤器2后压力检测-P37"]);
                    fuelGrp.CoarseFilter2FrontPressureDetectP32 = Convert.ToDouble(FuelGrpData["Fuel_粗滤器2前压力检测-P32"]);
                    fuelGrp.DieselTankLevelDetectL29 = Convert.ToDouble(FuelGrpData["Fuel_柴油箱液位检测-L29"]);
                    fuelGrp.FineFilter1RearPressureDetectP35 = Convert.ToDouble(FuelGrpData["Fuel_精滤器1后压力检测-P35"]);
                    fuelGrp.CoarseFilter1FrontPressureDetectP30 = Convert.ToDouble(FuelGrpData["Fuel_粗滤器1前压力检测-P30"]);


                    RowDictionary[index]["FuelDataGrp"] = CloneRowEntity(fuelGrp);
                }


                // 11. 取 ThreePhaseElectric 某个测点
                if (item.TryGetValue("ThreePhaseElectric", out var TEObj) && TEObj is JObject TEJObj && KeyNameList.Any(key => key == "ThreePhaseElectricData"))
                {
                    var TEGrpData = TEJObj.ToObject<Dictionary<string, object>>();
                    threePhaseElectric.ActivePower = Convert.ToDouble(TEGrpData["Electric_有功功率"]);
                    threePhaseElectric.CurrentIc = Convert.ToDouble(TEGrpData["Electric_Ic"]);
                    threePhaseElectric.TotalCurrent = Convert.ToDouble(TEGrpData["Electric_电流"]);
                    threePhaseElectric.CurrentIb = Convert.ToDouble(TEGrpData["Electric_Ib"]);
                    threePhaseElectric.VoltageUvw = Convert.ToDouble(TEGrpData["Electric_Uvw"]);
                    threePhaseElectric.TotalVoltage = Convert.ToDouble(TEGrpData["Electric_电压"]);
                    threePhaseElectric.CurrentIa = Convert.ToDouble(TEGrpData["Electric_Ia"]);
                    threePhaseElectric.VoltageUuv = Convert.ToDouble(TEGrpData["Electric_Uuv"]);
                    threePhaseElectric.VoltageUwu = Convert.ToDouble(TEGrpData["Electric_Uwu"]);
                    //threePhaseElectric.ReactivePower = Convert.ToDouble(TEGrpData["Electric_无功功率"]);
                    //threePhaseElectric.ApparentPower = Convert.ToDouble(TEGrpData["Electric_视在功率"]);
                    //threePhaseElectric.Frequency = Convert.ToDouble(TEGrpData["Electric_频率"]);



                    RowDictionary[index]["ThreePhaseElectricData"] = CloneRowEntity(threePhaseElectric);
                }


                // 12. 取 WaterGrp 某个测点
                if (item.TryGetValue("WaterGrp", out var WaterObj) && WaterObj is JObject WaterJObj && KeyNameList.Any(key => key == "WaterDataGrp"))
                {
                    var WaterGrpData = WaterJObj.ToObject<Dictionary<string, object>>();
                    waterGrp.CoolWaterTemperatureSetPID = Convert.ToDouble(WaterGrpData["Water_中冷水温度设置PID"]);
                    waterGrp.HighTempWaterFilterFrontPressureDetectP6 = Convert.ToDouble(WaterGrpData["Water_高温水过滤器前压力检测-P6"]);
                    waterGrp.PreHeatTankLevelDetect = Convert.ToDouble(WaterGrpData["Water_预热水箱液位检测"]);
                    waterGrp.CoolWaterFilterFrontPressureDetectP9 = Convert.ToDouble(WaterGrpData["Water_中冷水过滤器前压力检测-P9"]);
                    waterGrp.CoolWaterFilterRearPressureDetectP10 = Convert.ToDouble(WaterGrpData["Water_中冷水过滤器后压力检测-P10"]);
                    waterGrp.HighTempWaterTemperaturePassword = Convert.ToDouble(WaterGrpData["Water_高温水温度密码"]);
                    waterGrp.HighTempWaterFilterRearPressureDetectP7 = Convert.ToDouble(WaterGrpData["Water_高温水过滤器后压力检测-P7"]);
                    waterGrp.HighTempWaterTemperatureSetPID = Convert.ToDouble(WaterGrpData["Water_高温水温度设置PID"]);
                    waterGrp.CoolWaterCoolerInletTemperatureDetectT14 = Convert.ToDouble(WaterGrpData["Water_中冷水冷却器进口温度检测-T14"]);
                    waterGrp.HighTempWaterTemperatureRealTimePID = Convert.ToDouble(WaterGrpData["Water_高温水温度实时PID"]);
                    waterGrp.PreHeatTankTemperatureDetectT12 = Convert.ToDouble(WaterGrpData["Water_预热水箱温度检测-T12"]);
                    waterGrp.CoolWaterTemperaturePassword = Convert.ToDouble(WaterGrpData["Water_中冷水温度密码"]);
                    waterGrp.CoolWaterTemperatureRealTimePID = Convert.ToDouble(WaterGrpData["Water_中冷水温度实时PID"]);
                    waterGrp.HighTempWaterCoolerInletTemperatureDetectT13 = Convert.ToDouble(WaterGrpData["Water_高温水冷却器进口温度检测-T13"]);
                    //waterGrp.HighTempWaterTemperaturePIDUpperLimit = Convert.ToDouble(WaterGrpData["Water_高温水温度PID上限值"]);
                    //waterGrp.CoolWaterTemperaturePIDUpperLimit = Convert.ToDouble(WaterGrpData["Water_中冷水温度PID上限值"]);



                    RowDictionary[index]["WaterDataGrp"] = CloneRowEntity(waterGrp);

                }
                // 13. 取 PLC2AIGrp 某个测点
                if (item.TryGetValue("PLC2AIGrp", out var PLC2AIObj) && PLC2AIObj is JObject PLC2AIJObj && KeyNameList.Any(key => key == "PLC2AIDataGrp"))
                {
                    var PLC2AIGrpData = PLC2AIJObj.ToObject<Dictionary<string, object>>();
                    plc2AiGrp.OilConsumptionMeasurePressure = Convert.ToDouble(PLC2AIGrpData["AI2_机油耗测量压力"]);
                    plc2AiGrp.T21MainOilChannelInletOilTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_T21主油道进口油温"]);
                    plc2AiGrp.T5CoolWaterOutletTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_T5中冷水出机温度"]);
                    plc2AiGrp.B8CylinderExhaustTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_B8缸排气温度"]);
                    plc2AiGrp.DynamometerUPhaseTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_测功机U相温度"]);
                    plc2AiGrp.HighTempWaterPumpOutletPressure = Convert.ToDouble(PLC2AIGrpData["AI2_高温水泵出口压力"]);
                    plc2AiGrp.RearSuperchargerIntakeVacuumDegree = Convert.ToDouble(PLC2AIGrpData["AI2_后增压器进气真空度"]);
                    plc2AiGrp.B4CylinderExhaustTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_B4缸排气温度"]);
                    plc2AiGrp.InterCoolerInletWaterTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_中冷器进口水温"]);
                    plc2AiGrp.DynamometerDPhaseTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_测功机D相温度"]);
                    plc2AiGrp.A8CylinderExhaustTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_A8缸排气温度"]);
                    plc2AiGrp.ExcitationVoltageDetect = Convert.ToDouble(PLC2AIGrpData["AI2_励磁电压检测"]);
                    plc2AiGrp.FrontSuperchargerIntakeVacuumDegree = Convert.ToDouble(PLC2AIGrpData["AI2_前增压器进气真空度"]);
                    plc2AiGrp.P20OilPumpOutletPressure = Convert.ToDouble(PLC2AIGrpData["AI2_P20机油泵出口压力"]);
                    plc2AiGrp.B7CylinderExhaustTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_B7缸排气温度"]);
                    plc2AiGrp.FrontInterCoolerRearAirPressure = Convert.ToDouble(PLC2AIGrpData["AI2_前中冷后空气压力"]);
                    plc2AiGrp.B2CylinderExhaustTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_B2缸排气温度"]);
                    plc2AiGrp.MainOilChannelEndOilPressure = Convert.ToDouble(PLC2AIGrpData["AI2_主油道末端油压"]);
                    plc2AiGrp.A4CylinderExhaustTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_A4缸排气温度"]);
                    plc2AiGrp.DynamometerWPhaseTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_测功机W相温度"]);
                    plc2AiGrp.T31FuelPumpInletOilTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_T31燃油泵进口油温"]);
                    plc2AiGrp.P21MainOilChannelInletOilPressure = Convert.ToDouble(PLC2AIGrpData["AI2_P21主油道进口油压"]);
                    plc2AiGrp.DynamometerNPhaseTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_测功机N相温度"]);
                    plc2AiGrp.FrontInterCoolerFrontAirTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_前中冷前空气温度"]);
                    plc2AiGrp.A7CylinderExhaustTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_A7缸排气温度"]);
                    plc2AiGrp.FrontSuperchargerOilInletPressure = Convert.ToDouble(PLC2AIGrpData["AI2_前增压器机油进口压力"]);
                    plc2AiGrp.A2CylinderExhaustTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_A2缸排气温度"]);
                    plc2AiGrp.InterCoolerOutletWaterTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_中冷器出口水温"]);
                    plc2AiGrp.B6CylinderExhaustTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_B6缸排气温度"]);
                    plc2AiGrp.FrontSuperchargerOilInletTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_前增压器机油进口温度"]);
                    plc2AiGrp.RearSuperchargerOilInletTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_后增压器机油进口温度"]);
                    plc2AiGrp.RearSuperchargerExhaustBackPressure = Convert.ToDouble(PLC2AIGrpData["AI2_后增压器排气背压"]);
                    plc2AiGrp.RearTurboOutletExhaustGasTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_后涡轮出口废气温度"]);
                    plc2AiGrp.P38FuelSupplyPressure = Convert.ToDouble(PLC2AIGrpData["AI2_P38燃油供油压力"]);
                    plc2AiGrp.FrontTurboInletExhaustGasPressure = Convert.ToDouble(PLC2AIGrpData["AI2_前涡轮进口废气压力"]);
                    plc2AiGrp.RearSuperchargerOilInletPressure = Convert.ToDouble(PLC2AIGrpData["AI2_后增压器机油进口压力"]);
                    plc2AiGrp.P2HighTempWaterPumpInletPressure = Convert.ToDouble(PLC2AIGrpData["AI2_P2高温水泵进口压力"]);
                    plc2AiGrp.RearInterCoolerRearAirPressure = Convert.ToDouble(PLC2AIGrpData["AI2_后中冷后空气压力"]);
                    plc2AiGrp.RearSuperchargerIntakeTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_后增压器进气温度"]);
                    plc2AiGrp.FrontInterCoolerRearAirTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_前中冷后空气温度"]);
                    plc2AiGrp.A6CylinderExhaustTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_A6缸排气温度"]);
                    plc2AiGrp.FrontSuperchargerExhaustBackPressure = Convert.ToDouble(PLC2AIGrpData["AI2_前增压器排气背压"]);
                    plc2AiGrp.T20OilPumpOutletOilTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_T20机油泵出口油温"]);
                    plc2AiGrp.T1HighTempWaterOutletTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_T1高温水出机温度"]);
                    plc2AiGrp.RearTurboInletExhaustGasTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_后涡轮进口废气温度"]);
                    plc2AiGrp.RearInterCoolerFrontAirTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_后中冷前空气温度"]);
                    plc2AiGrp.ExcitationCurrentDetect = Convert.ToDouble(PLC2AIGrpData["AI2_励磁电流检测"]);
                    plc2AiGrp.T2HighTempWaterInletTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_T2高温水进机温度"]);
                    plc2AiGrp.T3CoolWaterInletTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_T3中冷水进机温度"]);
                    plc2AiGrp.OilConsumptionMeasureLevel = Convert.ToDouble(PLC2AIGrpData["AI2_机油耗测量液位"]);
                    plc2AiGrp.T30FuelReturnOilTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_T30燃油回油温度"]);
                    plc2AiGrp.B3CylinderExhaustTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_B3缸排气温度"]);
                    plc2AiGrp.RearTurboInletExhaustGasPressure = Convert.ToDouble(PLC2AIGrpData["AI2_后涡轮进口废气压力"]);
                    plc2AiGrp.RearInterCoolerRearAirTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_后中冷后空气温度"]);
                    plc2AiGrp.CoolWaterPumpOutletPressure = Convert.ToDouble(PLC2AIGrpData["AI2_中冷水泵出口压力"]);
                    plc2AiGrp.FrontInterCoolerFrontAirPressure = Convert.ToDouble(PLC2AIGrpData["AI2_前中冷前空气压力"]);
                    plc2AiGrp.DynamometerVPhaseTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_测功机V相温度"]);
                    plc2AiGrp.B1CylinderExhaustTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_B1缸排气温度"]);
                    plc2AiGrp.FrontSuperchargerOilOutletTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_前增压器机油出口温度"]);
                    plc2AiGrp.RearSuperchargerOilOutletTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_后增压器机油出口温度"]);
                    plc2AiGrp.A3CylinderExhaustTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_A3缸排气温度"]);
                    plc2AiGrp.P3CoolWaterPumpInletPressure = Convert.ToDouble(PLC2AIGrpData["AI2_P3中冷水泵进口压力"]);
                    plc2AiGrp.FrontTurboOutletExhaustGasTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_前涡轮出口废气温度"]);
                    plc2AiGrp.A1CylinderExhaustTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_A1缸排气温度"]);
                    plc2AiGrp.FrontSuperchargerIntakeTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_前增压器进气温度"]);
                    plc2AiGrp.P1HighTempWaterOutletPressure = Convert.ToDouble(PLC2AIGrpData["AI2_P1高温水出机压力"]);
                    plc2AiGrp.B5CylinderExhaustTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_B5缸排气温度"]);
                    plc2AiGrp.P5CoolWaterOutletPressure = Convert.ToDouble(PLC2AIGrpData["AI2_P5中冷水出机压力"]);
                    plc2AiGrp.FrontTurboInletExhaustGasTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_前涡轮进口废气温度"]);
                    plc2AiGrp.RearInterCoolerFrontAirPressure = Convert.ToDouble(PLC2AIGrpData["AI2_后中冷前空气压力"]);
                    plc2AiGrp.A5CylinderExhaustTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_A5缸排气温度"]);
                    //plc2AiGrp.RearCoolerFrontAirTemperature = Convert.ToDouble(PLC2AIGrpData["AI2_后冷前空气温度"]);


                    RowDictionary[index]["PLC2AIDataGrp"] = CloneRowEntity(plc2AiGrp);
                }




                // 15. 取 StartPLCGrp 某个测点
                if (item.TryGetValue("StartPLCGrp", out var StartPLCObj) && StartPLCObj is JObject StartPLCJObj && KeyNameList.Any(key => key == "StartPLCDataGrp"))
                {
                    var StartPLC = StartPLCJObj.ToObject<Dictionary<string, object>>();
                    startPlcGrp.FaultReset = Convert.ToBoolean(StartPLC["StartPLC_FaultReset"]);
                    startPlcGrp.InverterRunning = Convert.ToBoolean(StartPLC["StartPLC_变频器运行中"]);
                    startPlcGrp.InverterOutputDetect = Convert.ToBoolean(StartPLC["StartPLC_变频器输出检测"]);
                    startPlcGrp.InverterFault = Convert.ToBoolean(StartPLC["StartPLC_变频器故障"]);
                    startPlcGrp.Auto = Convert.ToBoolean(StartPLC["StartPLC_Auto"]);
                    startPlcGrp.InverterOutputSwitch = Convert.ToBoolean(StartPLC["StartPLC_变频器输出合分闸"]);
                    startPlcGrp.Scram = Convert.ToBoolean(StartPLC["StartPLC_Scram"]);
                    startPlcGrp.FrontDoorDetect = Convert.ToBoolean(StartPLC["StartPLC_前门检测"]);
                    startPlcGrp.RearDoorDetect = Convert.ToBoolean(StartPLC["StartPLC_后门检测"]);



                    RowDictionary[index]["StartPLCDataGrp"] = CloneRowEntity(startPlcGrp);
                }




                // 16. 取 SpeedGrp SpeedGrp
                if (item.TryGetValue("SpeedGrp", out var speedObj) && speedObj is JObject speedJObj && KeyNameList.Any(key => key == "SpeedDataGrp"))
                {
                    var speed = speedJObj.ToObject<Dictionary<string, object>>();
                    speedGrp.Speed3 = Convert.ToDouble(speed["Speed_转速3"]);
                    speedGrp.PulsesPerRevolution = Convert.ToDouble(speed["Speed_每转感应点数"]);
                    speedGrp.Speed1 = Convert.ToDouble(speed["Speed_转速1"]);
                    speedGrp.Speed2 = Convert.ToDouble(speed["Speed_转速2"]);



                    RowDictionary[index]["SpeedDataGrp"] = CloneRowEntity(speedGrp);
                }





                // 17. 取 gd350_1 某个测点
                if (item.TryGetValue("GD350_1", out var gd350_1Obj) && gd350_1Obj is JObject gd350_1JObj && KeyNameList.Any(key => key == "GD350_1Data"))
                {
                    var gd350_1 = gd350_1JObj.ToObject<Dictionary<string, object>>();
                    gd350_1Grp.OutputPowerDetect = Convert.ToDouble(gd350_1["Inverter_输出功率检测"]);
                    gd350_1Grp.StartStop = Convert.ToDouble(gd350_1["Inverter_启动_停止"]);
                    gd350_1Grp.RunTimeout = Convert.ToDouble(gd350_1["Inverter_运行超时时间"]);
                    gd350_1Grp.OutputCurrentDetect = Convert.ToDouble(gd350_1["Inverter_输出电流检测"]);
                    gd350_1Grp.CabinetRunningStatus = Convert.ToDouble(gd350_1["Inverter_启动柜运行状态"]);
                    gd350_1Grp.Ready = Convert.ToDouble(gd350_1["Inverter_就绪"]);
                    gd350_1Grp.FaultCode = Convert.ToDouble(gd350_1["Inverter_故障代码"]);
                    gd350_1Grp.BusVoltageDetect = Convert.ToDouble(gd350_1["Inverter_母线电压检测"]);
                    gd350_1Grp.CabinetFrequencySet = Convert.ToDouble(gd350_1["Inverter_启动柜频率设定"]);
                    gd350_1Grp.OutputVoltageDetect = Convert.ToDouble(gd350_1["Inverter_输出电压检测"]);
                    gd350_1Grp.CabinetStart = Convert.ToDouble(gd350_1["Inverter_启动柜启动"]);
                    gd350_1Grp.RunningStatus = Convert.ToDouble(gd350_1["Inverter_运行状态"]);
                    gd350_1Grp.RunningFrequency = Convert.ToDouble(gd350_1["Inverter_运行频率"]);



                    RowDictionary[index]["GD350_1Data"] = CloneRowEntity(gd350_1Grp);
                }
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
    }
}

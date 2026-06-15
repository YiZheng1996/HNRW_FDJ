using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class DIDataGrp
    {

        //DI
        /// <summary>
        /// 燃油泵旁路1电动调节阀控制-194
        /// </summary>
        public double AO_OilBy_Pass1Valve_194 { get; set; }


        /// <summary>
        /// 进气风道左调节阀控制
        /// </summary>
        public double AirInFlowleftValve { get; set; }

        /// <summary>
        /// 燃油泵1电动调节阀控制-170
        /// </summary>
        public double Oil1Vlave_170 { get; set; }

        /// <summary>
        /// 进气风道右调节阀控制
        /// </summary>
        public double AirFlowRightValve { get; set; }


        /// <summary>
        /// 水阻箱进水电动调节阀
        /// </summary>
        public double WaterResistanceBoxValve { get; set; }

        /// <summary>
        /// 燃油泵旁路1电动调节阀就地-194
        /// </summary>
        public bool OilByPass1ValveLocal_194 { get; set; }

        /// <summary>
        /// 抽中冷水开到位-31
        /// </summary>
        public bool DrawMidCoolWaterOpenLimit_31 { get; set; }

        /// <summary>
        /// 预热水箱加热器控制5检测
        /// </summary>
        public bool PreHeatTankHeaterCtrl5Detect { get; set; }

        /// <summary>
        /// 机油处理箱出开到位-136
        /// </summary>
        public bool OilProcessTankOutOpenLimit_136 { get; set; }

        /// <summary>
        /// 柴油箱进油开到位-164
        /// </summary>
        public bool DieselTankOilInOpenLimit_164 { get; set; }

        /// <summary>
        /// 泵和水箱出水开到位-20
        /// </summary>
        public bool PumpAndTankWaterOutOpenLimit_20 { get; set; }

        /// <summary>
        /// 冷却器回油关到位-90
        /// </summary>
        public bool CoolerOilReturnCloseLimit_90 { get; set; }

        /// <summary>
        /// 待处理机油箱补油开到位-139
        /// </summary>
        public bool PendingOilTankSupplementOpenLimit_139 { get; set; }

        /// <summary>
        /// 柴油机启动
        /// </summary>
        public bool DieselEngineStart { get; set; }

        /// <summary>
        /// 油泵进油开到位-96
        /// </summary>
        public bool OilPumpOilInOpenLimit_96 { get; set; }

        /// <summary>
        /// 机油冷却器进水电动调节阀故障-89
        /// </summary>
        public bool OilCoolerWaterInValveFault_89 { get; set; }

        /// <summary>
        /// 排气风道右调节阀关
        /// </summary>
        public bool ExhaustDuctRightValveClose { get; set; }

        /// <summary>
        /// 预热水箱出水开到位-15
        /// </summary>
        public bool PreHeatTankWaterOutOpenLimit_15 { get; set; }

        /// <summary>
        /// 高温水膨胀水箱出关到位-30
        /// </summary>
        public bool HighTempWaterExpTankOutCloseLimit_30 { get; set; }

        /// <summary>
        /// 待处理机油箱通处理机开到位-134
        /// </summary>
        public bool PendingOilTankConnectProcessorOpenLimit_134 { get; set; }

        /// <summary>
        /// 机油泵预供油关到位-93
        /// </summary>
        public bool OilPumpPreSupplyCloseLimit_93 { get; set; }

        /// <summary>
        /// 冷却水进水关到位-61
        /// </summary>
        public bool CoolWaterInCloseLimit_61 { get; set; }

        /// <summary>
        /// 紧急停车2开到位-182
        /// </summary>
        public bool EmergencyStop2OpenLimit_182 { get; set; }

        /// <summary>
        /// 油泵出油关到位-97
        /// </summary>
        public bool OilPumpOilOutCloseLimit_97 { get; set; }

        /// <summary>
        /// 抽油泵热继检测
        /// </summary>
        public bool DrawOilPumpThermalRelayDetect { get; set; }

        /// <summary>
        /// 机油箱出油开到位-111
        /// </summary>
        public bool OilTankOilOutOpenLimit_111 { get; set; }

        /// <summary>
        /// 控制间2柜前门检测
        /// </summary>
        public bool ControlRoom2CabFrontDoorDetect { get; set; }

        /// <summary>
        /// 紧急停车1开到位-181
        /// </summary>
        public bool EmergencyStop1OpenLimit_181 { get; set; }

        /// <summary>
        /// 主发通风机电源
        /// </summary>
        public bool MainFanPower { get; set; }

        /// <summary>
        /// 高温水膨胀水箱出开到位-30
        /// </summary>
        public bool HighTempWaterExpTankOutOpenLimit_30 { get; set; }

        /// <summary>
        /// 高温水关到位-3
        /// </summary>
        public bool HighTempWaterCloseLimit_3 { get; set; }

        /// <summary>
        /// 机油泵预供油开到位-93
        /// </summary>
        public bool OilPumpPreSupplyOpenLimit_93 { get; set; }

        /// <summary>
        /// 油泵管路进油关到位-95
        /// </summary>
        public bool OilPumpPipeOilInCloseLimit_95 { get; set; }

        /// <summary>
        /// 预热水泵Y检测
        /// </summary>
        public bool PreHeatPumpYDetect { get; set; }

        /// <summary>
        /// 柴油箱进油关到位-164
        /// </summary>
        public bool DieselTankOilInCloseLimit_164 { get; set; }

        /// <summary>
        /// 主发通风机1Y检测
        /// </summary>
        public bool MainFan1YDetect { get; set; }

        /// <summary>
        /// 机油箱油泵来油开到位-115
        /// </summary>
        public bool OilTankPumpOilInOpenLimit_115 { get; set; }

        /// <summary>
        /// 预热水箱回水关到位-24
        /// </summary>
        public bool PreHeatTankWaterReturnCloseLimit_24 { get; set; }

        /// <summary>
        /// 燃油泵旁路1电动调节阀全关-194
        /// </summary>
        public bool OilByPass1ValveFullyClose_194 { get; set; }

        /// <summary>
        /// 燃油泵旁路1电动调节阀全开-194
        /// </summary>
        public bool OilByPass1ValveFullyOpen_194 { get; set; }

        /// <summary>
        /// 主发通风机2检测
        /// </summary>
        public bool MainFan2Detect { get; set; }

        /// <summary>
        /// 启动柜电源检测
        /// </summary>
        public bool StartCabinetPowerDetect { get; set; }

        /// <summary>
        /// 主发通风机1检测
        /// </summary>
        public bool MainFan1Detect { get; set; }

        /// <summary>
        /// 待处理机油箱进油开到位-116
        /// </summary>
        public bool PendingOilTankOilInOpenLimit_116 { get; set; }

        /// <summary>
        /// 油泵进油关到位-96
        /// </summary>
        public bool OilPumpOilInCloseLimit_96 { get; set; }

        /// <summary>
        /// 柴油机卸载
        /// </summary>
        public bool DieselEngineUnload { get; set; }

        /// <summary>
        /// 中冷水膨胀水箱高液位
        /// </summary>
        public bool MidCoolWaterExpTankHighLevel { get; set; }

        /// <summary>
        /// 油底壳加油关到位-122
        /// </summary>
        public bool OilSumpFillCloseLimit_122 { get; set; }

        /// <summary>
        /// 水阻升降下极限检测
        /// </summary>
        public bool WaterResistLiftLowerLimitDetect { get; set; }

        /// <summary>
        /// 燃油泵/预热水泵电源检测
        /// </summary>
        public bool OilPumpPreHeatPumpPowerDetect { get; set; }

        /// <summary>
        /// 燃油泵旁路1电动调节阀故障-194
        /// </summary>
        public bool OilByPass1ValveFault_194 { get; set; }

        /// <summary>
        /// 机滤器前1关到位-91
        /// </summary>
        public bool FrontOilFilter1CloseLimit_91 { get; set; }

        /// <summary>
        /// 紧急停止
        /// </summary>
        public bool EmergencyStop { get; set; }

        /// <summary>
        /// 燃油泵1电动调节阀就地-170
        /// </summary>
        public bool OilPump1ValveLocal_170 { get; set; }

        /// <summary>
        /// 油耗仪换向进关到位-183
        /// </summary>
        public bool FuelMeterCommuteInCloseLimit_183 { get; set; }

        /// <summary>
        /// 设备间电源检测
        /// </summary>
        public bool EquipmentRoomPowerDetect { get; set; }

        /// <summary>
        /// 中冷水冷却水进口电动调节阀故障-88
        /// </summary>
        public bool MidCoolWaterInValveFault_88 { get; set; }

        /// <summary>
        /// 油泵出油开到位-97
        /// </summary>
        public bool OilPumpOilOutOpenLimit_97 { get; set; }

        /// <summary>
        /// 控制电源检测
        /// </summary>
        public bool ControlPowerDetect { get; set; }

        /// <summary>
        /// 中冷水循环开到位-41
        /// </summary>
        public bool MidCoolWaterCircOpenLimit_41 { get; set; }

        /// <summary>
        /// 机油箱清洁来油关到位-137
        /// </summary>
        public bool OilTankCleanOilInCloseLimit_137 { get; set; }

        /// <summary>
        /// 排气风道左调节阀开
        /// </summary>
        public bool ExhaustDuctLeftValveOpen { get; set; }

        /// <summary>
        /// 主发励磁柜电源
        /// </summary>
        public bool MainExcitationCabPower { get; set; }

        /// <summary>
        /// 预热水箱加水开到位-27
        /// </summary>
        public bool PreHeatTankWaterFillOpenLimit_27 { get; set; }

        /// <summary>
        /// 机油处理箱进开到位-135
        /// </summary>
        public bool OilProcessTankInOpenLimit_135 { get; set; }

        /// <summary>
        /// 机油预供泵停止
        /// </summary>
        public bool OilPreSupplyPumpStop { get; set; }

        /// <summary>
        /// 预热水箱加热器控制2检测
        /// </summary>
        public bool PreHeatTankHeaterCtrl2Detect { get; set; }

        /// <summary>
        /// 预热水箱加热器控制1检测
        /// </summary>
        public bool PreHeatTankHeaterCtrl1Detect { get; set; }

        /// <summary>
        /// 水泵出口电动调节阀全开-18
        /// </summary>
        public bool WaterPumpOutValveFullyOpen_18 { get; set; }

        /// <summary>
        /// 污油排出泵Y检测
        /// </summary>
        public bool WasteOilDrainPumpYDetect { get; set; }

        /// <summary>
        /// 预热水箱加热器控制4检测
        /// </summary>
        public bool PreHeatTankHeaterCtrl4Detect { get; set; }

        /// <summary>
        /// 燃油泵1电动调节阀故障-170
        /// </summary>
        public bool OilPump1ValveFault_170 { get; set; }

        /// <summary>
        /// 回油开到位-179
        /// </summary>
        public bool OilReturnOpenLimit_179 { get; set; }

        /// <summary>
        /// 内循环水来水开到位-28
        /// </summary>
        public bool InnerCircWaterInOpenLimit_28 { get; set; }

        /// <summary>
        /// 水阻升降上极限检测
        /// </summary>
        public bool WaterResistLiftUpperLimitDetect { get; set; }

        /// <summary>
        /// 油耗仪换向回关到位-184
        /// </summary>
        public bool FuelMeterCommuteReturnCloseLimit_184 { get; set; }

        /// <summary>
        /// 进气风道右调节阀开
        /// </summary>
        public bool IntakeDuctRightValveOpen { get; set; }

        /// <summary>
        /// 燃油加油开始
        /// </summary>
        public bool FuelFillStart { get; set; }

        /// <summary>
        /// 排气风道右调节阀故障
        /// </summary>
        public bool ExhaustDuctRightValveFault { get; set; }

        /// <summary>
        /// 设备间控制柜体前门检测
        /// </summary>
        public bool EquipmentRoomCabFrontDoorDetect { get; set; }

        /// <summary>
        /// 预热水箱电源检测
        /// </summary>
        public bool PreHeatTankPowerDetect { get; set; }

        /// <summary>
        /// 机滤器前2关到位-100
        /// </summary>
        public bool FrontOilFilter2CloseLimit_100 { get; set; }

        /// <summary>
        /// 泵和水箱前回水关到位-23
        /// </summary>
        public bool PumpAndTankFrontWaterReturnCloseLimit_23 { get; set; }

        /// <summary>
        /// 预热水箱回水开到位-24
        /// </summary>
        public bool PreHeatTankWaterReturnOpenLimit_24 { get; set; }

        /// <summary>
        /// 水阻升降电机上升合闸检测
        /// </summary>
        public bool WaterResistLiftMotorUpClosingDetect { get; set; }

        /// <summary>
        /// 预供机油泵电源检测
        /// </summary>
        public bool OilPreSupplyPumpPowerDetect { get; set; }

        /// <summary>
        /// 预热水泵进口开到位-16
        /// </summary>
        public bool PreHeatPumpInOpenLimit_16 { get; set; }

        /// <summary>
        /// 油底壳加油开到位-122
        /// </summary>
        public bool OilSumpFillOpenLimit_122 { get; set; }

        /// <summary>
        /// 抽高温水开到位-21
        /// </summary>
        public bool DrawHighTempWaterOpenLimit_21 { get; set; }

        /// <summary>
        /// 排气风道右调节阀开
        /// </summary>
        public bool ExhaustDuctRightValveOpen { get; set; }

        /// <summary>
        /// 预热水箱加水关到位-26
        /// </summary>
        public bool PreHeatTankWaterFillCloseLimit_26 { get; set; }

        /// <summary>
        /// 机滤器前1开到位-91
        /// </summary>
        public bool FrontOilFilter1OpenLimit_91 { get; set; }

        /// <summary>
        /// 污油排出泵热继检测
        /// </summary>
        public bool WasteOilDrainPumpThermalRelayDetect { get; set; }

        /// <summary>
        /// 进气风道左调节阀关
        /// </summary>
        public bool IntakeDuctLeftValveClose { get; set; }

        /// <summary>
        /// 预热水箱出水关到位-15
        /// </summary>
        public bool PreHeatTankWaterOutCloseLimit_15 { get; set; }

        /// <summary>
        /// 油底壳抽油关到位-92
        /// </summary>
        public bool OilSumpDrawOilCloseLimit_92 { get; set; }

        /// <summary>
        /// 盘车连锁开关
        /// </summary>
        public bool BarringInterlockSwitch { get; set; }

        /// <summary>
        /// 待处理机油箱进油关到位-116
        /// </summary>
        public bool PendingOilTankOilInCloseLimit_116 { get; set; }

        /// <summary>
        /// 出油路1关到位-190
        /// </summary>
        public bool OilOutlet1CloseLimit_190 { get; set; }

        /// <summary>
        /// 预热水泵热继检测
        /// </summary>
        public bool PreHeatPumpThermalRelayDetect { get; set; }

        /// <summary>
        /// 中冷水膨胀水箱低液位
        /// </summary>
        public bool MidCoolWaterExpTankLowLevel { get; set; }

        /// <summary>
        /// 泵和水箱前回水开到位-23
        /// </summary>
        public bool PumpAndTankFrontWaterReturnOpenLimit_23 { get; set; }

        /// <summary>
        /// 污油排出泵主接检测
        /// </summary>
        public bool WasteOilDrainPumpMainContactDetect { get; set; }

        /// <summary>
        /// 内循环水来水关到位-28
        /// </summary>
        public bool InnerCircWaterInCloseLimit_28 { get; set; }

        /// <summary>
        /// 机油处理箱出关到位-136
        /// </summary>
        public bool OilProcessTankOutCloseLimit_136 { get; set; }

        /// <summary>
        /// 进气加热电源检测
        /// </summary>
        public bool IntakeHeaterPowerDetect { get; set; }

        /// <summary>
        /// 主发通风机1主接检测
        /// </summary>
        public bool MainFan1MainContactDetect { get; set; }

        /// <summary>
        /// 预热水泵进口关到位-16
        /// </summary>
        public bool PreHeatPumpInCloseLimit_16 { get; set; }

        /// <summary>
        /// 高温水膨胀水箱高液位
        /// </summary>
        public bool HighTempWaterExpTankHighLevel { get; set; }

        /// <summary>
        /// 机器间配电柜体前门检测
        /// </summary>
        public bool MachineRoomDistCabFrontDoorDetect { get; set; }

        /// <summary>
        /// 抽中冷水关到位-31
        /// </summary>
        public bool DrawMidCoolWaterCloseLimit_31 { get; set; }

        /// <summary>
        /// 污油排出泵电源检测
        /// </summary>
        public bool WasteOilDrainPumpPowerDetect { get; set; }

        /// <summary>
        /// 抽高温水关到位-21
        /// </summary>
        public bool DrawHighTempWaterCloseLimit_21 { get; set; }

        /// <summary>
        /// 备用电源检测
        /// </summary>
        public bool BackupPowerDetect { get; set; }

        /// <summary>
        /// 油耗仪换向进开到位-183
        /// </summary>
        public bool FuelMeterCommuteInOpenLimit_183 { get; set; }

        /// <summary>
        /// 抽油泵合闸检测
        /// </summary>
        public bool DrawOilPumpClosingDetect { get; set; }

        /// <summary>
        /// 水阻升降电机热继检测
        /// </summary>
        public bool WaterResistLiftMotorThermalRelayDetect { get; set; }

        /// <summary>
        /// 冷却水进水开到位-61
        /// </summary>
        public bool CoolWaterInOpenLimit_61 { get; set; }

        /// <summary>
        /// 机器间配电柜体后门检测
        /// </summary>
        public bool MachineRoomDistCabRearDoorDetect { get; set; }

        /// <summary>
        /// 燃油泵1电动调节阀全开-170
        /// </summary>
        public bool OilPump1ValveFullyOpen_170 { get; set; }

        /// <summary>
        /// 膨胀水箱补水进开到位-29
        /// </summary>
        public bool ExpTankWaterMakeupInOpenLimit_29 { get; set; }

        /// <summary>
        /// 高温水开到位-3
        /// </summary>
        public bool HighTempWaterOpenLimit_3 { get; set; }

        /// <summary>
        /// 主发通风机1热继检测
        /// </summary>
        public bool MainFan1ThermalRelayDetect { get; set; }

        /// <summary>
        /// 进气风道右调节阀关
        /// </summary>
        public bool IntakeDuctRightValveClose { get; set; }

        /// <summary>
        /// 水泵出口电动调节阀故障-18
        /// </summary>
        public bool WaterPumpOutValveFault_18 { get; set; }

        /// <summary>
        /// 待处理机油箱补油关到位-139
        /// </summary>
        public bool PendingOilTankSupplementCloseLimit_139 { get; set; }

        /// <summary>
        /// 机器间电源检测
        /// </summary>
        public bool MachineRoomPowerDetect { get; set; }

        /// <summary>
        /// 设备间控制柜体后门检测
        /// </summary>
        public bool EquipmentRoomCabRearDoorDetect { get; set; }

        /// <summary>
        /// 高温水冷却水进口电动调节阀故障-87
        /// </summary>
        public bool HighTempCoolWaterInValveFault_87 { get; set; }

        /// <summary>
        /// 预热水泵主接检测
        /// </summary>
        public bool PreHeatPumpMainContactDetect { get; set; }

        /// <summary>
        /// 回油关到位-179
        /// </summary>
        public bool OilReturnCloseLimit_179 { get; set; }

        /// <summary>
        /// 主发通风机2Y检测
        /// </summary>
        public bool MainFan2YDetect { get; set; }

        /// <summary>
        /// 燃油泵1电动调节阀全关-170
        /// </summary>
        public bool OilPump1ValveFullyClose_170 { get; set; }

        /// <summary>
        /// 主发通风机2主接检测
        /// </summary>
        public bool MainFan2MainContactDetect { get; set; }

        /// <summary>
        /// 预热水箱加水开到位-26
        /// </summary>
        public bool PreHeatTankWaterFillOpenLimit_26 { get; set; }

        /// <summary>
        /// 柴油机停机
        /// </summary>
        public bool DieselEngineStop { get; set; }

        /// <summary>
        /// 预供机油泵合闸检测
        /// </summary>
        public bool OilPreSupplyPumpClosingDetect { get; set; }

        /// <summary>
        /// 污油排出泵检测
        /// </summary>
        public bool WasteOilDrainPumpDetect { get; set; }

        /// <summary>
        /// 预热水泵出口关到位-17
        /// </summary>
        public bool PreHeatPumpOutCloseLimit_17 { get; set; }

        /// <summary>
        /// 发动机DC24V配电
        /// </summary>
        public bool EngineDC24VPowerDist { get; set; }

        /// <summary>
        /// 水阻升降机电源
        /// </summary>
        public bool WaterResistLifterPower { get; set; }

        /// <summary>
        /// 机油箱出油关到位-111
        /// </summary>
        public bool OilTankOilOutCloseLimit_111 { get; set; }

        /// <summary>
        /// 待处理机油箱通处理机关到位-134
        /// </summary>
        public bool PendingOilTankConnectProcessorCloseLimit_134 { get; set; }

        /// <summary>
        /// 排气风道左调节阀关
        /// </summary>
        public bool ExhaustDuctLeftValveClose { get; set; }

        /// <summary>
        /// 燃油泵2合闸检测
        /// </summary>
        public bool OilPump2ClosingDetect { get; set; }

        /// <summary>
        /// 泵和水箱出水关到位-20
        /// </summary>
        public bool PumpAndTankWaterOutCloseLimit_20 { get; set; }

        /// <summary>
        /// 紧急停车1关到位-181
        /// </summary>
        public bool EmergencyStop1CloseLimit_181 { get; set; }

        /// <summary>
        /// 主发通风机2热继检测
        /// </summary>
        public bool MainFan2ThermalRelayDetect { get; set; }

        /// <summary>
        /// 机油箱清洁来油开到位-137
        /// </summary>
        public bool OilTankCleanOilInOpenLimit_137 { get; set; }

        /// <summary>
        /// 预热水泵出口开到位-17
        /// </summary>
        public bool PreHeatPumpOutOpenLimit_17 { get; set; }

        /// <summary>
        /// 油泵管路进油开到位-95
        /// </summary>
        public bool OilPumpPipeOilInOpenLimit_95 { get; set; }

        /// <summary>
        /// 燃油泵1合闸检测
        /// </summary>
        public bool OilPump1ClosingDetect { get; set; }

        /// <summary>
        /// 机油处理箱进关到位-135
        /// </summary>
        public bool OilProcessTankInCloseLimit_135 { get; set; }

        /// <summary>
        /// 机滤器前2开到位-100
        /// </summary>
        public bool FrontOilFilter2OpenLimit_100 { get; set; }

        /// <summary>
        /// 机油预供泵启动
        /// </summary>
        public bool OilPreSupplyPumpStart { get; set; }

        /// <summary>
        /// 膨胀水箱补水进关到位-29
        /// </summary>
        public bool ExpTankWaterMakeupInCloseLimit_29 { get; set; }

        /// <summary>
        /// 出油路1开到位-190
        /// </summary>
        public bool OilOutlet1OpenLimit_190 { get; set; }

        /// <summary>
        /// 冷却器回油开到位-90
        /// </summary>
        public bool CoolerOilReturnOpenLimit_90 { get; set; }

        /// <summary>
        /// 高温水膨胀水箱低液位
        /// </summary>
        public bool HighTempWaterExpTankLowLevel { get; set; }

        /// <summary>
        /// 预热水箱加水关到位-27
        /// </summary>
        public bool PreHeatTankWaterFillCloseLimit_27 { get; set; }

        /// <summary>
        /// 中冷水循环关到位-41
        /// </summary>
        public bool MidCoolWaterCircCloseLimit_41 { get; set; }

        /// <summary>
        /// 控制间3柜前门检测
        /// </summary>
        public bool ControlRoom3CabFrontDoorDetect { get; set; }

        /// <summary>
        /// 水阻升降电机下降合闸检测
        /// </summary>
        public bool WaterResistLiftMotorDownClosingDetect { get; set; }

        /// <summary>
        /// 进气风道左调节阀开
        /// </summary>
        public bool IntakeDuctLeftValveOpen { get; set; }

        /// <summary>
        /// 机油箱油泵来油关到位-115
        /// </summary>
        public bool OilTankPumpOilInCloseLimit_115 { get; set; }

        /// <summary>
        /// 进气风道左调节阀故障
        /// </summary>
        public bool IntakeDuctLeftValveFault { get; set; }

        /// <summary>
        /// 曲轴箱压力开关
        /// </summary>
        public bool CrankcasePressureSwitch { get; set; }

        /// <summary>
        /// 水泵出口电动调节阀全关-18
        /// </summary>
        public bool WaterPumpOutValveFullyClose_18 { get; set; }

        /// <summary>
        /// 预热水泵检测
        /// </summary>
        public bool PreHeatPumpDetect { get; set; }

        /// <summary>
        /// 排气风道左调节阀故障
        /// </summary>
        public bool ExhaustDuctLeftValveFault { get; set; }

        /// <summary>
        /// 油耗仪换向回开到位-184
        /// </summary>
        public bool FuelMeterCommuteReturnOpenLimit_184 { get; set; }

        /// <summary>
        /// 机油箱到处理机开到位-133
        /// </summary>
        public bool OilTankToProcessorOpenLimit_133 { get; set; }

        /// <summary>
        /// 水泵出口电动调节阀就地-18
        /// </summary>
        public bool WaterPumpOutValveLocal_18 { get; set; }

        /// <summary>
        /// 进气风道右调节阀故障
        /// </summary>
        public bool IntakeDuctRightValveFault { get; set; }

        /// <summary>
        /// 燃油泵2热继检测
        /// </summary>
        public bool OilPump2ThermalRelayDetect { get; set; }

        /// <summary>
        /// 尾气处理装置电源检测
        /// </summary>
        public bool ExhaustTreatmentUnitPowerDetect { get; set; }

        /// <summary>
        /// 预热水箱回水关到位-22
        /// </summary>
        public bool PreHeatTankWaterReturnCloseLimit_22 { get; set; }

        /// <summary>
        /// 机油箱到处理机关到位-133
        /// </summary>
        public bool OilTankToProcessorCloseLimit_133 { get; set; }

        /// <summary>
        /// 机油处理机电源检测
        /// </summary>
        public bool OilProcessorPowerDetect { get; set; }

        /// <summary>
        /// 控制间1柜前门检测
        /// </summary>
        public bool ControlRoom1CabFrontDoorDetect { get; set; }

        /// <summary>
        /// 紧急停车2关到位-182
        /// </summary>
        public bool EmergencyStop2CloseLimit_182 { get; set; }

        /// <summary>
        /// 油底壳抽油开到位-92
        /// </summary>
        public bool OilSumpDrawOilOpenLimit_92 { get; set; }

        /// <summary>
        /// 预热水箱加热器控制3检测
        /// </summary>
        public bool PreHeatTankHeaterCtrl3Detect { get; set; }

        /// <summary>
        /// 预供机油泵热继检测
        /// </summary>
        public bool OilPreSupplyPumpThermalRelayDetect { get; set; }

        /// <summary>
        /// 预热水箱回水开到位-22
        /// </summary>
        public bool PreHeatTankWaterReturnOpenLimit_22 { get; set; }

        /// <summary>
        /// 燃油泵1热继检测
        /// </summary>
        public bool OilPump1ThermalRelayDetect { get; set; }

        /// <summary>
        /// 预热水箱加热器控制6检测
        /// </summary>
        public bool PreHeatTankHeaterCtrl6Detect { get; set; }

    }
}

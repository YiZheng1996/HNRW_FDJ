using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class DIDataGrp
    {
        [JsonProperty("DI_燃油泵旁路1电动调节阀就地-194")]
        public bool OilByPass1ValveLocal_194 { get; set; }

        [JsonProperty("DI_抽中冷水开到位-31")]
        public bool DrawMidCoolWaterOpenLimit_31 { get; set; }

        [JsonProperty("DI_预热水箱加热器控制5检测")]
        public bool PreHeatTankHeaterCtrl5Detect { get; set; }

        [JsonProperty("DI_机油处理箱出开到位-136")]
        public bool OilProcessTankOutOpenLimit_136 { get; set; }

        [JsonProperty("DI_柴油箱进油开到位-164")]
        public bool DieselTankOilInOpenLimit_164 { get; set; }

        [JsonProperty("DI_泵和水箱出水开到位-20")]
        public bool PumpAndTankWaterOutOpenLimit_20 { get; set; }

        [JsonProperty("DI_冷却器回油关到位-90")]
        public bool CoolerOilReturnCloseLimit_90 { get; set; }

        [JsonProperty("DI_待处理机油箱补油开到位-139")]
        public bool PendingOilTankSupplementOpenLimit_139 { get; set; }

        [JsonProperty("DI_柴油机启动")]
        public bool DieselEngineStart { get; set; }

        [JsonProperty("DI_油泵进油开到位-96")]
        public bool OilPumpOilInOpenLimit_96 { get; set; }

        [JsonProperty("DI_机油冷却器进水电动调节阀故障-89")]
        public bool OilCoolerWaterInValveFault_89 { get; set; }

        [JsonProperty("DI_排气风道右调节阀关")]
        public bool ExhaustDuctRightValveClose { get; set; }

        [JsonProperty("DI_预热水箱出水开到位-15")]
        public bool PreHeatTankWaterOutOpenLimit_15 { get; set; }

        [JsonProperty("DI_高温水膨胀水箱出关到位-30")]
        public bool HighTempWaterExpTankOutCloseLimit_30 { get; set; }

        [JsonProperty("DI_待处理机油箱通处理机开到位-134")]
        public bool PendingOilTankConnectProcessorOpenLimit_134 { get; set; }

        [JsonProperty("DI_机油泵预供油关到位-93")]
        public bool OilPumpPreSupplyCloseLimit_93 { get; set; }

        [JsonProperty("DI_冷却水进水关到位-61")]
        public bool CoolWaterInCloseLimit_61 { get; set; }

        [JsonProperty("DI_紧急停车2开到位-182")]
        public bool EmergencyStop2OpenLimit_182 { get; set; }

        [JsonProperty("DI_油泵出油关到位-97")]
        public bool OilPumpOilOutCloseLimit_97 { get; set; }

        [JsonProperty("DI_抽油泵热继检测")]
        public bool DrawOilPumpThermalRelayDetect { get; set; }

        [JsonProperty("DI_机油箱出油开到位-111")]
        public bool OilTankOilOutOpenLimit_111 { get; set; }

        [JsonProperty("DI_控制间2柜前门检测")]
        public bool ControlRoom2CabFrontDoorDetect { get; set; }

        [JsonProperty("DI_紧急停车1开到位-181")]
        public bool EmergencyStop1OpenLimit_181 { get; set; }

        [JsonProperty("DI_主发通风机电源")]
        public bool MainFanPower { get; set; }

        [JsonProperty("DI_高温水膨胀水箱出开到位-30")]
        public bool HighTempWaterExpTankOutOpenLimit_30 { get; set; }

        [JsonProperty("DI_高温水关到位-3")]
        public bool HighTempWaterCloseLimit_3 { get; set; }

        [JsonProperty("DI_机油泵预供油开到位-93")]
        public bool OilPumpPreSupplyOpenLimit_93 { get; set; }

        [JsonProperty("DI_油泵管路进油关到位-95")]
        public bool OilPumpPipeOilInCloseLimit_95 { get; set; }

        [JsonProperty("DI_预热水泵Y检测")]
        public bool PreHeatPumpYDetect { get; set; }

        [JsonProperty("DI_柴油箱进油关到位-164")]
        public bool DieselTankOilInCloseLimit_164 { get; set; }

        [JsonProperty("DI_主发通风机1Y检测")]
        public bool MainFan1YDetect { get; set; }

        [JsonProperty("DI_机油箱油泵来油开到位-115")]
        public bool OilTankPumpOilInOpenLimit_115 { get; set; }

        [JsonProperty("DI_预热水箱回水关到位-24")]
        public bool PreHeatTankWaterReturnCloseLimit_24 { get; set; }

        [JsonProperty("DI_燃油泵旁路1电动调节阀全关-194")]
        public bool OilByPass1ValveFullyClose_194 { get; set; }

        [JsonProperty("DI_燃油泵旁路1电动调节阀全开-194")]
        public bool OilByPass1ValveFullyOpen_194 { get; set; }

        [JsonProperty("DI_主发通风机2检测")]
        public bool MainFan2Detect { get; set; }

        [JsonProperty("DI_启动柜电源检测")]
        public bool StartCabinetPowerDetect { get; set; }

        [JsonProperty("DI_主发通风机1检测")]
        public bool MainFan1Detect { get; set; }

        [JsonProperty("DI_待处理机油箱进油开到位-116")]
        public bool PendingOilTankOilInOpenLimit_116 { get; set; }

        [JsonProperty("DI_油泵进油关到位-96")]
        public bool OilPumpOilInCloseLimit_96 { get; set; }

        [JsonProperty("DI_柴油机卸载")]
        public bool DieselEngineUnload { get; set; }

        [JsonProperty("DI_中冷水膨胀水箱高液位")]
        public bool MidCoolWaterExpTankHighLevel { get; set; }

        [JsonProperty("DI_油底壳加油关到位-122")]
        public bool OilSumpFillCloseLimit_122 { get; set; }

        [JsonProperty("DI_水阻升降下极限检测")]
        public bool WaterResistLiftLowerLimitDetect { get; set; }

        [JsonProperty("DI_燃油泵/预热水泵电源检测")]
        public bool OilPumpPreHeatPumpPowerDetect { get; set; }

        [JsonProperty("DI_燃油泵旁路1电动调节阀故障-194")]
        public bool OilByPass1ValveFault_194 { get; set; }

        [JsonProperty("DI_机滤器前1关到位-91")]
        public bool FrontOilFilter1CloseLimit_91 { get; set; }

        [JsonProperty("DI_紧急停止")]
        public bool EmergencyStop { get; set; }

        [JsonProperty("DI_燃油泵1电动调节阀就地-170")]
        public bool OilPump1ValveLocal_170 { get; set; }

        [JsonProperty("DI_油耗仪换向进关到位-183")]
        public bool FuelMeterCommuteInCloseLimit_183 { get; set; }

        [JsonProperty("DI_设备间电源检测")]
        public bool EquipmentRoomPowerDetect { get; set; }

        [JsonProperty("DI_中冷水冷却水进口电动调节阀故障-88")]
        public bool MidCoolWaterInValveFault_88 { get; set; }

        [JsonProperty("DI_油泵出油开到位-97")]
        public bool OilPumpOilOutOpenLimit_97 { get; set; }

        [JsonProperty("DI_控制电源检测")]
        public bool ControlPowerDetect { get; set; }

        [JsonProperty("DI_中冷水循环开到位-41")]
        public bool MidCoolWaterCircOpenLimit_41 { get; set; }

        [JsonProperty("DI_机油箱清洁来油关到位-137")]
        public bool OilTankCleanOilInCloseLimit_137 { get; set; }

        [JsonProperty("DI_排气风道左调节阀开")]
        public bool ExhaustDuctLeftValveOpen { get; set; }

        [JsonProperty("DI_主发励磁柜电源")]
        public bool MainExcitationCabPower { get; set; }

        [JsonProperty("DI_预热水箱加水开到位-27")]
        public bool PreHeatTankWaterFillOpenLimit_27 { get; set; }

        [JsonProperty("DI_机油处理箱进开到位-135")]
        public bool OilProcessTankInOpenLimit_135 { get; set; }

        [JsonProperty("DI_机油预供泵停止")]
        public bool OilPreSupplyPumpStop { get; set; }

        [JsonProperty("DI_预热水箱加热器控制2检测")]
        public bool PreHeatTankHeaterCtrl2Detect { get; set; }

        [JsonProperty("DI_预热水箱加热器控制1检测")]
        public bool PreHeatTankHeaterCtrl1Detect { get; set; }

        [JsonProperty("DI_水泵出口电动调节阀全开-18")]
        public bool WaterPumpOutValveFullyOpen_18 { get; set; }

        [JsonProperty("DI_污油排出泵Y检测")]
        public bool WasteOilDrainPumpYDetect { get; set; }

        [JsonProperty("DI_预热水箱加热器控制4检测")]
        public bool PreHeatTankHeaterCtrl4Detect { get; set; }

        [JsonProperty("DI_燃油泵1电动调节阀故障-170")]
        public bool OilPump1ValveFault_170 { get; set; }

        [JsonProperty("DI_回油开到位-179")]
        public bool OilReturnOpenLimit_179 { get; set; }

        [JsonProperty("DI_内循环水来水开到位-28")]
        public bool InnerCircWaterInOpenLimit_28 { get; set; }

        [JsonProperty("DI_水阻升降上极限检测")]
        public bool WaterResistLiftUpperLimitDetect { get; set; }

        [JsonProperty("DI_油耗仪换向回关到位-184")]
        public bool FuelMeterCommuteReturnCloseLimit_184 { get; set; }

        [JsonProperty("DI_进气风道右调节阀开")]
        public bool IntakeDuctRightValveOpen { get; set; }

        [JsonProperty("DI_燃油加油开始")]
        public bool FuelFillStart { get; set; }

        [JsonProperty("DI_排气风道右调节阀故障")]
        public bool ExhaustDuctRightValveFault { get; set; }

        [JsonProperty("DI_设备间控制柜体前门检测")]
        public bool EquipmentRoomCabFrontDoorDetect { get; set; }

        [JsonProperty("DI_预热水箱电源检测")]
        public bool PreHeatTankPowerDetect { get; set; }

        [JsonProperty("DI_机滤器前2关到位-100")]
        public bool FrontOilFilter2CloseLimit_100 { get; set; }

        [JsonProperty("DI_泵和水箱前回水关到位-23")]
        public bool PumpAndTankFrontWaterReturnCloseLimit_23 { get; set; }

        [JsonProperty("DI_预热水箱回水开到位-24")]
        public bool PreHeatTankWaterReturnOpenLimit_24 { get; set; }

        [JsonProperty("DI_水阻升降电机上升合闸检测")]
        public bool WaterResistLiftMotorUpClosingDetect { get; set; }

        [JsonProperty("DI_预供机油泵电源检测")]
        public bool OilPreSupplyPumpPowerDetect { get; set; }

        [JsonProperty("DI_预热水泵进口开到位-16")]
        public bool PreHeatPumpInOpenLimit_16 { get; set; }

        [JsonProperty("DI_油底壳加油开到位-122")]
        public bool OilSumpFillOpenLimit_122 { get; set; }

        [JsonProperty("DI_抽高温水开到位-21")]
        public bool DrawHighTempWaterOpenLimit_21 { get; set; }

        [JsonProperty("DI_排气风道右调节阀开")]
        public bool ExhaustDuctRightValveOpen { get; set; }

        [JsonProperty("DI_预热水箱加水关到位-26")]
        public bool PreHeatTankWaterFillCloseLimit_26 { get; set; }

        [JsonProperty("DI_机滤器前1开到位-91")]
        public bool FrontOilFilter1OpenLimit_91 { get; set; }

        [JsonProperty("DI_污油排出泵热继检测")]
        public bool WasteOilDrainPumpThermalRelayDetect { get; set; }

        [JsonProperty("DI_进气风道左调节阀关")]
        public bool IntakeDuctLeftValveClose { get; set; }

        [JsonProperty("DI_预热水箱出水关到位-15")]
        public bool PreHeatTankWaterOutCloseLimit_15 { get; set; }

        [JsonProperty("DI_油底壳抽油关到位-92")]
        public bool OilSumpDrawOilCloseLimit_92 { get; set; }

        [JsonProperty("DI_盘车连锁开关")]
        public bool BarringInterlockSwitch { get; set; }

        [JsonProperty("DI_待处理机油箱进油关到位-116")]
        public bool PendingOilTankOilInCloseLimit_116 { get; set; }

        [JsonProperty("DI_出油路1关到位-190")]
        public bool OilOutlet1CloseLimit_190 { get; set; }

        [JsonProperty("DI_预热水泵热继检测")]
        public bool PreHeatPumpThermalRelayDetect { get; set; }

        [JsonProperty("DI_中冷水膨胀水箱低液位")]
        public bool MidCoolWaterExpTankLowLevel { get; set; }

        [JsonProperty("DI_泵和水箱前回水开到位-23")]
        public bool PumpAndTankFrontWaterReturnOpenLimit_23 { get; set; }

        [JsonProperty("DI_污油排出泵主接检测")]
        public bool WasteOilDrainPumpMainContactDetect { get; set; }

        [JsonProperty("DI_内循环水来水关到位-28")]
        public bool InnerCircWaterInCloseLimit_28 { get; set; }

        [JsonProperty("DI_机油处理箱出关到位-136")]
        public bool OilProcessTankOutCloseLimit_136 { get; set; }

        [JsonProperty("DI_进气加热电源检测")]
        public bool IntakeHeaterPowerDetect { get; set; }

        [JsonProperty("DI_主发通风机1主接检测")]
        public bool MainFan1MainContactDetect { get; set; }

        [JsonProperty("DI_预热水泵进口关到位-16")]
        public bool PreHeatPumpInCloseLimit_16 { get; set; }

        [JsonProperty("DI_高温水膨胀水箱高液位")]
        public bool HighTempWaterExpTankHighLevel { get; set; }

        [JsonProperty("DI_机器间配电柜体前门检测")]
        public bool MachineRoomDistCabFrontDoorDetect { get; set; }

        [JsonProperty("DI_抽中冷水关到位-31")]
        public bool DrawMidCoolWaterCloseLimit_31 { get; set; }

        [JsonProperty("DI_污油排出泵电源检测")]
        public bool WasteOilDrainPumpPowerDetect { get; set; }

        [JsonProperty("DI_抽高温水关到位-21")]
        public bool DrawHighTempWaterCloseLimit_21 { get; set; }

        [JsonProperty("DI_备用电源检测")]
        public bool BackupPowerDetect { get; set; }

        [JsonProperty("DI_油耗仪换向进开到位-183")]
        public bool FuelMeterCommuteInOpenLimit_183 { get; set; }

        [JsonProperty("DI_抽油泵合闸检测")]
        public bool DrawOilPumpClosingDetect { get; set; }

        [JsonProperty("DI_水阻升降电机热继检测")]
        public bool WaterResistLiftMotorThermalRelayDetect { get; set; }

        [JsonProperty("DI_冷却水进水开到位-61")]
        public bool CoolWaterInOpenLimit_61 { get; set; }

        [JsonProperty("DI_机器间配电柜体后门检测")]
        public bool MachineRoomDistCabRearDoorDetect { get; set; }

        [JsonProperty("DI_燃油泵1电动调节阀全开-170")]
        public bool OilPump1ValveFullyOpen_170 { get; set; }

        [JsonProperty("DI_膨胀水箱补水进开到位-29")]
        public bool ExpTankWaterMakeupInOpenLimit_29 { get; set; }

        [JsonProperty("DI_高温水开到位-3")]
        public bool HighTempWaterOpenLimit_3 { get; set; }

        [JsonProperty("DI_主发通风机1热继检测")]
        public bool MainFan1ThermalRelayDetect { get; set; }

        [JsonProperty("DI_进气风道右调节阀关")]
        public bool IntakeDuctRightValveClose { get; set; }

        [JsonProperty("DI_水泵出口电动调节阀故障-18")]
        public bool WaterPumpOutValveFault_18 { get; set; }

        [JsonProperty("DI_待处理机油箱补油关到位-139")]
        public bool PendingOilTankSupplementCloseLimit_139 { get; set; }

        [JsonProperty("DI_机器间电源检测")]
        public bool MachineRoomPowerDetect { get; set; }

        [JsonProperty("DI_设备间控制柜体后门检测")]
        public bool EquipmentRoomCabRearDoorDetect { get; set; }

        [JsonProperty("DI_高温水冷却水进口电动调节阀故障-87")]
        public bool HighTempCoolWaterInValveFault_87 { get; set; }

        [JsonProperty("DI_预热水泵主接检测")]
        public bool PreHeatPumpMainContactDetect { get; set; }

        [JsonProperty("DI_回油关到位-179")]
        public bool OilReturnCloseLimit_179 { get; set; }

        [JsonProperty("DI_主发通风机2Y检测")]
        public bool MainFan2YDetect { get; set; }

        [JsonProperty("DI_燃油泵1电动调节阀全关-170")]
        public bool OilPump1ValveFullyClose_170 { get; set; }

        [JsonProperty("DI_主发通风机2主接检测")]
        public bool MainFan2MainContactDetect { get; set; }

        [JsonProperty("DI_预热水箱加水开到位-26")]
        public bool PreHeatTankWaterFillOpenLimit_26 { get; set; }

        [JsonProperty("DI_柴油机停机")]
        public bool DieselEngineStop { get; set; }

        [JsonProperty("DI_预供机油泵合闸检测")]
        public bool OilPreSupplyPumpClosingDetect { get; set; }

        [JsonProperty("DI_污油排出泵检测")]
        public bool WasteOilDrainPumpDetect { get; set; }

        [JsonProperty("DI_预热水泵出口关到位-17")]
        public bool PreHeatPumpOutCloseLimit_17 { get; set; }

        [JsonProperty("DI_发动机DC24V配电")]
        public bool EngineDC24VPowerDist { get; set; }

        [JsonProperty("DI_水阻升降机电源")]
        public bool WaterResistLifterPower { get; set; }

        [JsonProperty("DI_机油箱出油关到位-111")]
        public bool OilTankOilOutCloseLimit_111 { get; set; }

        [JsonProperty("DI_待处理机油箱通处理机关到位-134")]
        public bool PendingOilTankConnectProcessorCloseLimit_134 { get; set; }

        [JsonProperty("DI_排气风道左调节阀关")]
        public bool ExhaustDuctLeftValveClose { get; set; }

        [JsonProperty("DI_燃油泵2合闸检测")]
        public bool OilPump2ClosingDetect { get; set; }

        [JsonProperty("DI_泵和水箱出水关到位-20")]
        public bool PumpAndTankWaterOutCloseLimit_20 { get; set; }

        [JsonProperty("DI_紧急停车1关到位-181")]
        public bool EmergencyStop1CloseLimit_181 { get; set; }

        [JsonProperty("DI_主发通风机2热继检测")]
        public bool MainFan2ThermalRelayDetect { get; set; }

        [JsonProperty("DI_机油箱清洁来油开到位-137")]
        public bool OilTankCleanOilInOpenLimit_137 { get; set; }

        [JsonProperty("DI_预热水泵出口开到位-17")]
        public bool PreHeatPumpOutOpenLimit_17 { get; set; }

        [JsonProperty("DI_油泵管路进油开到位-95")]
        public bool OilPumpPipeOilInOpenLimit_95 { get; set; }

        [JsonProperty("DI_燃油泵1合闸检测")]
        public bool OilPump1ClosingDetect { get; set; }

        [JsonProperty("DI_机油处理箱进关到位-135")]
        public bool OilProcessTankInCloseLimit_135 { get; set; }

        [JsonProperty("DI_机滤器前2开到位-100")]
        public bool FrontOilFilter2OpenLimit_100 { get; set; }

        [JsonProperty("DI_机油预供泵启动")]
        public bool OilPreSupplyPumpStart { get; set; }

        [JsonProperty("DI_膨胀水箱补水进关到位-29")]
        public bool ExpTankWaterMakeupInCloseLimit_29 { get; set; }

        [JsonProperty("DI_出油路1开到位-190")]
        public bool OilOutlet1OpenLimit_190 { get; set; }

        [JsonProperty("DI_冷却器回油开到位-90")]
        public bool CoolerOilReturnOpenLimit_90 { get; set; }

        [JsonProperty("DI_高温水膨胀水箱低液位")]
        public bool HighTempWaterExpTankLowLevel { get; set; }

        [JsonProperty("DI_预热水箱加水关到位-27")]
        public bool PreHeatTankWaterFillCloseLimit_27 { get; set; }

        [JsonProperty("DI_中冷水循环关到位-41")]
        public bool MidCoolWaterCircCloseLimit_41 { get; set; }

        [JsonProperty("DI_控制间3柜前门检测")]
        public bool ControlRoom3CabFrontDoorDetect { get; set; }

        [JsonProperty("DI_水阻升降电机下降合闸检测")]
        public bool WaterResistLiftMotorDownClosingDetect { get; set; }

        [JsonProperty("DI_进气风道左调节阀开")]
        public bool IntakeDuctLeftValveOpen { get; set; }

        [JsonProperty("DI_机油箱油泵来油关到位-115")]
        public bool OilTankPumpOilInCloseLimit_115 { get; set; }

        [JsonProperty("DI_进气风道左调节阀故障")]
        public bool IntakeDuctLeftValveFault { get; set; }

        [JsonProperty("DI_曲轴箱压力开关")]
        public bool CrankcasePressureSwitch { get; set; }

        [JsonProperty("DI_水泵出口电动调节阀全关-18")]
        public bool WaterPumpOutValveFullyClose_18 { get; set; }

        [JsonProperty("DI_预热水泵检测")]
        public bool PreHeatPumpDetect { get; set; }

        [JsonProperty("DI_排气风道左调节阀故障")]
        public bool ExhaustDuctLeftValveFault { get; set; }

        [JsonProperty("DI_油耗仪换向回开到位-184")]
        public bool FuelMeterCommuteReturnOpenLimit_184 { get; set; }

        [JsonProperty("DI_机油箱到处理机开到位-133")]
        public bool OilTankToProcessorOpenLimit_133 { get; set; }

        [JsonProperty("DI_水泵出口电动调节阀就地-18")]
        public bool WaterPumpOutValveLocal_18 { get; set; }

        [JsonProperty("DI_进气风道右调节阀故障")]
        public bool IntakeDuctRightValveFault { get; set; }

        [JsonProperty("DI_燃油泵2热继检测")]
        public bool OilPump2ThermalRelayDetect { get; set; }

        [JsonProperty("DI_尾气处理装置电源检测")]
        public bool ExhaustTreatmentUnitPowerDetect { get; set; }

        [JsonProperty("DI_预热水箱回水关到位-22")]
        public bool PreHeatTankWaterReturnCloseLimit_22 { get; set; }

        [JsonProperty("DI_机油箱到处理机关到位-133")]
        public bool OilTankToProcessorCloseLimit_133 { get; set; }

        [JsonProperty("DI_机油处理机电源检测")]
        public bool OilProcessorPowerDetect { get; set; }

        [JsonProperty("DI_控制间1柜前门检测")]
        public bool ControlRoom1CabFrontDoorDetect { get; set; }

        [JsonProperty("DI_紧急停车2关到位-182")]
        public bool EmergencyStop2CloseLimit_182 { get; set; }

        [JsonProperty("DI_油底壳抽油开到位-92")]
        public bool OilSumpDrawOilOpenLimit_92 { get; set; }

        [JsonProperty("DI_预热水箱加热器控制3检测")]
        public bool PreHeatTankHeaterCtrl3Detect { get; set; }

        [JsonProperty("DI_预供机油泵热继检测")]
        public bool OilPreSupplyPumpThermalRelayDetect { get; set; }

        [JsonProperty("DI_预热水箱回水开到位-22")]
        public bool PreHeatTankWaterReturnOpenLimit_22 { get; set; }

        [JsonProperty("DI_燃油泵1热继检测")]
        public bool OilPump1ThermalRelayDetect { get; set; }

        [JsonProperty("DI_预热水箱加热器控制6检测")]
        public bool PreHeatTankHeaterCtrl6Detect { get; set; }
        }
    }

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class PipelineFaultDataGrp
    {
        [JsonProperty("Fault_机油箱液位高，机油回抽已停止")]
        public bool OilTankLevelHigh_OilBackExtractStop { get; set; }

        [JsonProperty("Fault_164阀故障")]
        public bool Valve164Fault { get; set; }

        [JsonProperty("Fault_137阀故障")]
        public bool Valve137Fault { get; set; }

        [JsonProperty("Fault_03阀故障")]
        public bool Valve03Fault { get; set; }

        [JsonProperty("Fault_燃油泵1过流")]
        public bool FuelPump1OverCurrent { get; set; }

        [JsonProperty("Fault_29阀故障")]
        public bool Valve29Fault { get; set; }

        [JsonProperty("Fault_机油控制箱分布式IO通讯掉线")]
        public bool OilControlBoxDistIOCommDisconnect { get; set; }

        [JsonProperty("Fault_135阀故障")]
        public bool Valve135Fault { get; set; }

        [JsonProperty("Fault_28阀故障")]
        public bool Valve28Fault { get; set; }

        [JsonProperty("Fault_启动柜通讯掉线")]
        public bool StartCabinetCommDisconnect { get; set; }

        [JsonProperty("Fault_预热水箱水位高，预热水箱加水已停止")]
        public bool PreHeatTankLevelHigh_WaterAddStop { get; set; }

        [JsonProperty("Fault_机油箱液位高，油底壳抽油已停止")]
        public bool OilTankLevelHigh_OilSumpExtractStop { get; set; }

        [JsonProperty("Fault_待处理机油箱液位高，油底壳抽油已停止")]
        public bool PendingOilTankLevelHigh_OilSumpExtractStop { get; set; }

        [JsonProperty("Fault_133阀故障")]
        public bool Valve133Fault { get; set; }

        [JsonProperty("Fault_115阀故障")]
        public bool Valve115Fault { get; set; }

        [JsonProperty("Fault_预热水箱箱液位高，中冷水/高温水回抽己停止")]
        public bool PreHeatTankLevelHigh_ColdHotWaterBackExtractStop { get; set; }

        [JsonProperty("Fault_预热水泵过流")]
        public bool PreHeatPumpOverCurrent { get; set; }

        [JsonProperty("Fault_进排气控制箱分布式IO通讯掉线")]
        public bool InExhaustControlBoxDistIOCommDisconnect { get; set; }

        [JsonProperty("Fault_控制间配电柜分布式IO通讯掉线")]
        public bool ControlRoomPowerCabinetDistIOCommDisconnect { get; set; }

        [JsonProperty("Fault_97阀故障")]
        public bool Valve97Fault { get; set; }

        [JsonProperty("Fault_111阀故障")]
        public bool Valve111Fault { get; set; }

        [JsonProperty("Fault_27阀故障")]
        public bool Valve27Fault { get; set; }

        [JsonProperty("Fault_燃油泵2过流")]
        public bool FuelPump2OverCurrent { get; set; }

        [JsonProperty("Fault_31阀故障")]
        public bool Valve31Fault { get; set; }

        [JsonProperty("Fault_96阀故障")]
        public bool Valve96Fault { get; set; }

        [JsonProperty("Fault_燃油控制箱分布式IO通讯掉线")]
        public bool FuelControlBoxDistIOCommDisconnect { get; set; }

        [JsonProperty("Fault_26阀故障")]
        public bool Valve26Fault { get; set; }

        [JsonProperty("Fault_61阀故障")]
        public bool Valve61Fault { get; set; }

        [JsonProperty("Fault_30阀故障")]
        public bool Valve30Fault { get; set; }

        [JsonProperty("Fault_95阀故障")]
        public bool Valve95Fault { get; set; }

        [JsonProperty("Fault_主发通风机2过流")]
        public bool MainFan2OverCurrent { get; set; }

        [JsonProperty("Fault_机油箱液位低，油底壳加油已停止")]
        public bool OilTankLevelLow_OilSumpRefuelStop { get; set; }

        [JsonProperty("Fault_136阀故障")]
        public bool Valve136Fault { get; set; }

        [JsonProperty("Fault_24阀故障")]
        public bool Valve24Fault { get; set; }

        [JsonProperty("Fault_190阀故障")]
        public bool Valve190Fault { get; set; }

        [JsonProperty("Fault_预热水箱水位低，高温水预热循环已停止")]
        public bool PreHeatTankLevelLow_HighTempWaterPreheatStop { get; set; }

        [JsonProperty("Fault_93阀故障")]
        public bool Valve93Fault { get; set; }

        [JsonProperty("Fault_机器间配电柜分布式IO通讯掉线")]
        public bool MachineRoomPowerCabinetDistIOCommDisconnect { get; set; }

        [JsonProperty("Fault_23阀故障")]
        public bool Valve23Fault { get; set; }

        [JsonProperty("Fault_主发通风机1过流")]
        public bool MainFan1OverCurrent { get; set; }

        [JsonProperty("Fault_17阀故障")]
        public bool Valve17Fault { get; set; }

        [JsonProperty("Fault_预供机油泵过流")]
        public bool PreSupplyOilPumpOverCurrent { get; set; }

        [JsonProperty("Fault_污油排出泵过流")]
        public bool WasteOilDrainPumpOverCurrent { get; set; }

        [JsonProperty("Fault_92阀故障")]
        public bool Valve92Fault { get; set; }

        [JsonProperty("Fault_22阀故障")]
        public bool Valve22Fault { get; set; }

        [JsonProperty("Fault_16阀故障")]
        public bool Valve16Fault { get; set; }

        [JsonProperty("Fault_机油箱液位高，机油箱加油已停止")]
        public bool OilTankLevelHigh_OilTankRefuelStop { get; set; }

        [JsonProperty("Fault_100阀故障")]
        public bool Valve100Fault { get; set; }

        [JsonProperty("Fault_91阀故障")]
        public bool Valve91Fault { get; set; }

        [JsonProperty("Fault_21阀故障")]
        public bool Valve21Fault { get; set; }

        [JsonProperty("Fault_15阀故障")]
        public bool Valve15Fault { get; set; }

        [JsonProperty("Fault_134阀故障")]
        public bool Valve134Fault { get; set; }

        [JsonProperty("Fault_116阀故障")]
        public bool Valve116Fault { get; set; }

        [JsonProperty("Fault_水阻升降电机过流")]
        public bool WaterResistLiftMotorOverCurrent { get; set; }

        [JsonProperty("Fault_90阀故障")]
        public bool Valve90Fault { get; set; }

        [JsonProperty("Fault_20阀故障")]
        public bool Valve20Fault { get; set; }

        [JsonProperty("Fault_抽油泵过流")]
        public bool DrawOilPumpOverCurrent { get; set; }

        [JsonProperty("Fault_急停后转速不下降")]
        public bool EmergencyStop_RotateSpeedNotDrop { get; set; }

        [JsonProperty("Fault_139阀故障")]
        public bool Valve139Fault { get; set; }

        [JsonProperty("Fault_水系统控制箱分布式IO通讯掉线")]
        public bool WaterSystemControlBoxDistIOCommDisconnect { get; set; }

        [JsonProperty("Fault_设备间配电柜分布式IO通讯掉线")]
        public bool EquipmentRoomPowerCabinetDistIOCommDisconnect { get; set; }

        [JsonProperty("Fault_122阀故障")]
        public bool Valve122Fault { get; set; }

        [JsonProperty("Fault_燃油箱液位低，燃油循环已停止")]
        public bool FuelTankLevelLow_FuelCycleStop { get; set; }

        [JsonProperty("Fault_41阀故障")]
        public bool Valve41Fault { get; set; }

        [JsonProperty("Fault_179阀故障")]
        public bool Valve179Fault { get; set; }
    }
}

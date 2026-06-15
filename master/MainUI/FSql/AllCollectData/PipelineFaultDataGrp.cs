using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class PipelineFaultDataGrp
    {

        //PipelineFaultGrp
        /// <summary>
        /// 机油箱液位高，机油回抽已停止
        /// </summary>
        public bool OilTankLevelHigh_OilBackExtractStop { get; set; }

        /// <summary>
        /// 164阀故障
        /// </summary>
        public bool Valve164Fault { get; set; }

        /// <summary>
        /// 137阀故障
        /// </summary>
        public bool Valve137Fault { get; set; }

        /// <summary>
        /// 03阀故障
        /// </summary>
        public bool Valve03Fault { get; set; }

        /// <summary>
        /// 燃油泵1过流
        /// </summary>
        public bool FuelPump1OverCurrent { get; set; }

        /// <summary>
        /// 29阀故障
        /// </summary>
        public bool Valve29Fault { get; set; }

        /// <summary>
        /// 机油控制箱分布式IO通讯掉线
        /// </summary>
        public bool OilControlBoxDistIOCommDisconnect { get; set; }

        /// <summary>
        /// 135阀故障
        /// </summary>
        public bool Valve135Fault { get; set; }

        /// <summary>
        /// 28阀故障
        /// </summary>
        public bool Valve28Fault { get; set; }

        /// <summary>
        /// 启动柜通讯掉线
        /// </summary>
        public bool StartCabinetCommDisconnect { get; set; }

        /// <summary>
        /// 预热水箱水位高，预热水箱加水已停止
        /// </summary>
        public bool PreHeatTankLevelHigh_WaterAddStop { get; set; }

        /// <summary>
        /// 机油箱液位高，油底壳抽油已停止
        /// </summary>
        public bool OilTankLevelHigh_OilSumpExtractStop { get; set; }

        /// <summary>
        /// 待处理机油箱液位高，油底壳抽油已停止
        /// </summary>
        public bool PendingOilTankLevelHigh_OilSumpExtractStop { get; set; }

        /// <summary>
        /// 133阀故障
        /// </summary>
        public bool Valve133Fault { get; set; }

        /// <summary>
        /// 115阀故障
        /// </summary>
        public bool Valve115Fault { get; set; }

        /// <summary>
        /// 预热水箱箱液位高，中冷水/高温水回抽己停止
        /// </summary>
        public bool PreHeatTankLevelHigh_ColdHotWaterBackExtractStop { get; set; }

        /// <summary>
        /// 预热水泵过流
        /// </summary>
        public bool PreHeatPumpOverCurrent { get; set; }

        /// <summary>
        /// 进排气控制箱分布式IO通讯掉线
        /// </summary>
        public bool InExhaustControlBoxDistIOCommDisconnect { get; set; }

        /// <summary>
        /// 控制间配电柜分布式IO通讯掉线
        /// </summary>
        public bool ControlRoomPowerCabinetDistIOCommDisconnect { get; set; }

        /// <summary>
        /// 97阀故障
        /// </summary>
        public bool Valve97Fault { get; set; }

        /// <summary>
        /// 111阀故障
        /// </summary>
        public bool Valve111Fault { get; set; }

        /// <summary>
        /// 27阀故障
        /// </summary>
        public bool Valve27Fault { get; set; }

        /// <summary>
        /// 燃油泵2过流
        /// </summary>
        public bool FuelPump2OverCurrent { get; set; }

        /// <summary>
        /// 31阀故障
        /// </summary>
        public bool Valve31Fault { get; set; }

        /// <summary>
        /// 96阀故障
        /// </summary>
        public bool Valve96Fault { get; set; }

        /// <summary>
        /// 燃油控制箱分布式IO通讯掉线
        /// </summary>
        public bool FuelControlBoxDistIOCommDisconnect { get; set; }

        /// <summary>
        /// 26阀故障
        /// </summary>
        public bool Valve26Fault { get; set; }

        /// <summary>
        /// 61阀故障
        /// </summary>
        public bool Valve61Fault { get; set; }

        /// <summary>
        /// 30阀故障
        /// </summary>
        public bool Valve30Fault { get; set; }

        /// <summary>
        /// 95阀故障
        /// </summary>
        public bool Valve95Fault { get; set; }

        /// <summary>
        /// 主发通风机2过流
        /// </summary>
        public bool MainFan2OverCurrent { get; set; }

        /// <summary>
        /// 机油箱液位低，油底壳加油已停止
        /// </summary>
        public bool OilTankLevelLow_OilSumpRefuelStop { get; set; }

        /// <summary>
        /// 136阀故障
        /// </summary>
        public bool Valve136Fault { get; set; }

        /// <summary>
        /// 24阀故障
        /// </summary>
        public bool Valve24Fault { get; set; }

        /// <summary>
        /// 190阀故障
        /// </summary>
        public bool Valve190Fault { get; set; }

        /// <summary>
        /// 预热水箱水位低，高温水预热循环已停止
        /// </summary>
        public bool PreHeatTankLevelLow_HighTempWaterPreheatStop { get; set; }

        /// <summary>
        /// 93阀故障
        /// </summary>
        public bool Valve93Fault { get; set; }

        /// <summary>
        /// 机器间配电柜分布式IO通讯掉线
        /// </summary>
        public bool MachineRoomPowerCabinetDistIOCommDisconnect { get; set; }

        /// <summary>
        /// 23阀故障
        /// </summary>
        public bool Valve23Fault { get; set; }

        /// <summary>
        /// 主发通风机1过流
        /// </summary>
        public bool MainFan1OverCurrent { get; set; }

        /// <summary>
        /// 17阀故障
        /// </summary>
        public bool Valve17Fault { get; set; }

        /// <summary>
        /// 预供机油泵过流
        /// </summary>
        public bool PreSupplyOilPumpOverCurrent { get; set; }

        /// <summary>
        /// 污油排出泵过流
        /// </summary>
        public bool WasteOilDrainPumpOverCurrent { get; set; }

        /// <summary>
        /// 92阀故障
        /// </summary>
        public bool Valve92Fault { get; set; }

        /// <summary>
        /// 22阀故障
        /// </summary>
        public bool Valve22Fault { get; set; }

        /// <summary>
        /// 16阀故障
        /// </summary>
        public bool Valve16Fault { get; set; }

        /// <summary>
        /// 机油箱液位高，机油箱加油已停止
        /// </summary>
        public bool OilTankLevelHigh_OilTankRefuelStop { get; set; }

        /// <summary>
        /// 100阀故障
        /// </summary>
        public bool Valve100Fault { get; set; }

        /// <summary>
        /// 91阀故障
        /// </summary>
        public bool Valve91Fault { get; set; }

        /// <summary>
        /// 21阀故障
        /// </summary>
        public bool Valve21Fault { get; set; }

        /// <summary>
        /// 15阀故障
        /// </summary>
        public bool Valve15Fault { get; set; }

        /// <summary>
        /// 134阀故障
        /// </summary>
        public bool Valve134Fault { get; set; }

        /// <summary>
        /// 116阀故障
        /// </summary>
        public bool Valve116Fault { get; set; }

        /// <summary>
        /// 水阻升降电机过流
        /// </summary>
        public bool WaterResistLiftMotorOverCurrent { get; set; }

        /// <summary>
        /// 90阀故障
        /// </summary>
        public bool Valve90Fault { get; set; }

        /// <summary>
        /// 20阀故障
        /// </summary>
        public bool Valve20Fault { get; set; }

        /// <summary>
        /// 抽油泵过流
        /// </summary>
        public bool DrawOilPumpOverCurrent { get; set; }

        /// <summary>
        /// 急停后转速不下降
        /// </summary>
        public bool EmergencyStop_RotateSpeedNotDrop { get; set; }

        /// <summary>
        /// 139阀故障
        /// </summary>
        public bool Valve139Fault { get; set; }

        /// <summary>
        /// 水系统控制箱分布式IO通讯掉线
        /// </summary>
        public bool WaterSystemControlBoxDistIOCommDisconnect { get; set; }

        /// <summary>
        /// 设备间配电柜分布式IO通讯掉线
        /// </summary>
        public bool EquipmentRoomPowerCabinetDistIOCommDisconnect { get; set; }

        /// <summary>
        /// 122阀故障
        /// </summary>
        public bool Valve122Fault { get; set; }

        /// <summary>
        /// 燃油箱液位低，燃油循环已停止
        /// </summary>
        public bool FuelTankLevelLow_FuelCycleStop { get; set; }

        /// <summary>
        /// 41阀故障
        /// </summary>
        public bool Valve41Fault { get; set; }

        /// <summary>
        /// 179阀故障
        /// </summary>
        public bool Valve179Fault { get; set; }

    }
}

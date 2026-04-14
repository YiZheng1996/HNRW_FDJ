using MainUI.Modules;
using MainUI.Model;
using MainUI.Equip;
using System.Threading;
using System;
using RW.Modules;
using System.Collections.Generic;

namespace MainUI
{
    public static class Common
    {
        /// <summary>
        /// OPC交互-接收数据
        /// </summary>
        public static OpcExChangeReceive opcExChangeReceiveGrp = new OpcExChangeReceive();

        /// <summary>
        /// OPC交互-发送数据
        /// </summary>
        public static OpcExChangeSend opcExChangeSendGrp = new OpcExChangeSend();

        public static OpcStatusGrp opcStatus = new OpcStatusGrp();
        public static AIGrp AIgrp = new AIGrp();
        public static AOGrp AOgrp = new AOGrp();
        public static DIGrp DIgrp = new DIGrp();
        public static DOGrp DOgrp = new DOGrp();

        /// <summary>
        /// 进气风道加热
        /// </summary> 
        public static AirDuctGrp AirDuctGrp = new AirDuctGrp(); 

        /// <summary>
        /// 数据交互
        /// </summary> 
        public static ExChangeGrp ExChangeGrp = new ExChangeGrp();

        /// <summary>
        /// 故障检测
        /// </summary> 
        public static PipelineFaultGrp PipelineFaultGrp = new PipelineFaultGrp();

        /// <summary>
        /// 机油系统
        /// </summary>
        public static EngineOilGrp engineOilGrp = new EngineOilGrp();

        /// <summary>
        /// 燃油系统
        /// </summary>
        public static FuelGrp fuelGrp = new FuelGrp();

        /// <summary>
        /// 电参数仪
        /// </summary>
        public static ThreePhaseElectric threePhaseElectric = new ThreePhaseElectric();

        /// <summary>
        /// 水系统
        /// </summary>
        public static WaterGrp waterGrp = new WaterGrp();

        /// <summary>
        /// 检测柜PLC
        /// </summary>
        public static PLC2AIGrp AI2Grp = new PLC2AIGrp();

        /// <summary>
        /// 启动柜PLC
        /// </summary>
        public static StartPLCGrp startPLCGrp = new StartPLCGrp();

        /// <summary>
        /// 转速模块
        /// </summary> 
        public static SpeedGrp speedGrp = new SpeedGrp();

        /// <summary>
        /// 配电柜硬件较准
        /// </summary>  
        public static PLCCalibration plcc = new PLCCalibration();

        /// <summary>
        /// 控制柜硬件较准
        /// </summary>   
        public static PLCCalibration2 plcc2 = new PLCCalibration2();

        public static TestViewModel mTestViewModel = new TestViewModel();

        /// <summary>
        /// 变频器
        /// </summary>
        public static GD350_1 gd350_1 = new GD350_1();

        /// <summary>
        /// 励磁模块
        /// </summary>
        public static ExcitationGrp excitationGrp = new ExcitationGrp();

        /// <summary>
        /// 加载所有模块（只加载一次）
        /// </summary>
        public static void InitModule()
        {
            AIgrp.Fresh();
            AOgrp.Fresh();
            DIgrp.Fresh();
            DOgrp.Fresh();
            AI2Grp.Fresh();
            ExChangeGrp.Fresh();
            PipelineFaultGrp.Fresh();
            engineOilGrp.Fresh();
            fuelGrp.Fresh();
            threePhaseElectric.Fresh();
            waterGrp.Fresh();
            speedGrp.Fresh();
            gd350_1.Fresh();
            excitationGrp.Fresh();
            opcExChangeReceiveGrp.Fresh();
            AirDuctGrp.Fresh();
        }
    }
}

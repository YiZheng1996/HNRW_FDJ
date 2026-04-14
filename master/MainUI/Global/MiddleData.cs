using MainUI.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Global
{
    public class MiddleData
    {
        public static MiddleData instnce = new MiddleData();

        /// <summary>
        /// 实时测试状态
        /// </summary> 
        public TestData testDataView { get; set; } = new TestData();

        /// <summary>
        /// 实时试验状态 进入配置文件
        /// </summary>
        public CurrentStatusConfig CurrentStatusData { get; set; }

        /// <summary>
        /// 自动试验调节励磁参数
        /// </summary>
        public ExcitationRegulationConfig ExcitationConfig { get; set; }

        /// <summary>
        /// 发动机基础参数
        /// </summary>  
        public ParaConfig SelectModelConfig { get; set; } = new ParaConfig();

        /// <summary>
        /// 配方参数
        /// </summary>
        public PubConfig PubsConfig { get; set; } = new PubConfig(); 

        /// <summary>
        /// 启机，甩车名称
        /// </summary>
        public string StartupName { get; set; } = "启机";

        /// <summary>
        /// 是否正在启机 / 甩车
        /// </summary>
        public bool isStartupRecording { get; set; } = false;

        /// <summary>
        /// 启机 / 甩车时间
        /// </summary>
        public DateTime? StartupReleaseTime { get; set; } = null;

        /// <summary>
        /// 获取发动机数据(使用485与trdp数据进行对比)
        /// </summary>
        /// <returns></returns>
        public double GetEngineSpeed()
        {
            try
            {
                var speed1 = Var.TRDP.GetDicValue("柴油机转速");
                var speed2 = Common.speedGrp["转速1"];

                // trdp有转速
                if (speed1 > 10)
                {
                    return speed1;
                }
                // 485有转速
                else if (speed2 > 10)
                {
                    return speed2;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private double _engineSpeed;
        /// <summary>
        /// 发动机转速
        /// </summary>
        public double EngineSpeed
        {
            get { return GetEngineSpeed(); } //暂时注释 Common.speedGrp.Speed1
            //set { _engineSpeed = value; }
        }

        /// <summary>
        /// 发动机转速 rpm 百分比
        /// </summary>
        public double EngineSpeedPercent
        {
            get
            {
                return Math.Round(EngineSpeed / SelectModelConfig.RatedSpeed * 100, 1);
            }
        }

        /// <summary>
        /// 运行小时数
        /// </summary>
        public double RunHour { get; set; }

        private double _ptfWeight;
        /// <summary>
        /// PTF650重量（用于计算扭矩）
        /// </summary>
        public double PTFWeight
        {
            get { return _ptfWeight; }
            set
            {
                _ptfWeight = value;
                _torque = Math.Round(_ptfWeight * 9.8, 1);
            }
        }

        /// <summary>
        /// 发动机扭矩 N.m（通过重量计算得出）
        /// </summary>
        private double _torque;
        public double EngineTorque
        {
            get
            {
                return _torque;
            }
        }

        /// <summary>
        /// 发动机扭矩 N.m 百分比
        /// </summary>
        public double EngineTorquePercent
        {
            get
            {
                return Math.Round(EngineTorque / SelectModelConfig.RatedTorque * 100, 1);
            }
        }

        /// <summary>
        /// 发动机功率（转速*扭矩/9550）
        /// </summary>
        public double EnginePower
        {
            get
            {
                if (EngineSpeed <= 0) return 0;
                return Math.Round((EngineSpeed * EngineTorque) / 9550.0, 1);
            }
        }

        /// <summary>
        /// 燃油泵是否打开（1/2）
        /// </summary>
        public bool IsOpenFuelPump()
        {
            return Common.DIgrp["燃油泵1合闸检测"] || Common.DIgrp["燃油泵2合闸检测"];
        }

        /// <summary>
        /// 燃油循环是否打开
        /// </summary>
        public bool IsOpenFuelCycle()
        {
            // 一键燃油循环
            var status1 = Common.ExChangeGrp.GetBool("燃油循环") == true;
            // 正常循环
            var status3 = !Common.DOgrp["Y183阀控制"] && !Common.DOgrp["Y190阀控制"] && Common.DOgrp["Y179阀控制"] && (Common.DOgrp["燃油泵2合闸控制"] || Common.DOgrp["燃油泵1合闸控制"]);

            // 一键油耗测量
            var status2 = Common.ExChangeGrp.GetBool("燃油耗测量") == true;
            // 测量油耗
            //var status4 = !Common.DOgrp["Y181阀控制"] && !Common.DOgrp["Y182阀控制"] && Common.DOgrp["Y183阀控制"] && Common.DOgrp["Y184阀控制"] && Common.DOgrp["Y190阀控制"] && (Common.DOgrp["燃油泵2合闸控制"] || Common.DOgrp["燃油泵1合闸控制"]);

            return status1 || status2 || status3;// || status4;
        }

        /// <summary>
        /// 机油循环是否打开
        /// </summary>
        public bool IsOpenEngineCycle()
        {
            // 一键机油循环
            var status1 = Common.ExChangeGrp.GetBool("预供机油循环") == true;
            // 正常循环
            var status2 = Common.DOgrp["Y92阀控制"] && Common.DOgrp["Y95阀控制"] && Common.DOgrp["Y96阀控制"] && Common.DOgrp["Y97阀控制"] && Common.DOgrp["Y100阀控制"] && Common.DOgrp["预供机油泵合闸控制"];

            return status1 || status2;
        }

    }

    /// <summary>
    /// 试验的实时状态存储(方便存储数据)
    /// </summary>
    public class TestData
    {

        /// <summary>
        /// 试验名称
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// 是否正在测试
        /// </summary>
        public bool IsTest { get; set; }

        /// <summary>
        /// 秒 数（0-59）
        /// </summary>
        public int Second { get; set; }

        /// <summary>
        /// 暂停的秒 数（0-59）
        /// </summary>
        public int StopSecond { get; set; }

        /// <summary>
        /// 暂停的分 数（0-59）
        /// </summary>
        public int StopMiunte { get; set; }

        /// <summary>
        /// 试验目标转速百分比
        /// </summary>
        public double TestSpeedPercent { get; set; }

        /// <summary>
        /// 试验目标扭矩百分比
        /// </summary>
        public double TestTorquePercent { get; set; }

        /// <summary>
        /// 下一个 试验目标转速百分比（弹窗用）
        /// </summary>
        public double TestNextSpeedPercent { get; set; }

        /// <summary>
        /// 下一个 试验目标扭矩百分比（弹窗用）
        /// </summary>
        public double TestNextTorquePercent { get; set; }

        /// <summary>
        /// 下一个 试验目标时间（弹窗用）
        /// </summary>
        public double TestNextTime { get; set; }

        /// <summary>
        /// 下一个 试验目标工况编号（弹窗用）
        /// </summary>
        public string TestNextGKNo { get; set; }

        /// <summary>
        /// 最后采集时间
        /// </summary>
        public DateTime NextCollectTime = DateTime.MinValue;

        /// <summary>
        /// 采集间隔时间(min)
        /// </summary>
        public double CollectTime { get; set; }

        /// <summary>
        /// 是否正在采集
        /// </summary>
        public bool IsCollecting { get; set; }

        /// <summary>
        /// 当前步骤剩余时间（秒）
        /// </summary>
        public int StepRemainingSeconds { get; set; }

        /// <summary>
        /// 当前步骤总时间（秒）
        /// </summary>
        public int StepTotalSeconds { get; set; }

        /// <summary>
        /// 当前步骤序号
        /// </summary>
        public int StepIndex { get; set; }

        /// <summary>
        /// 阶段记录数量
        /// </summary>
        public int StepNameCollectIndex { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        public double TotalCollectCount { get; set; }

    }
}
using MainUI.Config;
using MainUI.Config.Test;
using MainUI.Global;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MainUI.Procedure.Test.Performance
{
    /// <summary>
    /// 100h 试验逻辑
    /// </summary>
    public class Test100hProc : BaseTest
    {
       public ExcitationRegulationConfig excitationRegulation = new ExcitationRegulationConfig(Var.SysConfig.LastModel);

        /// <summary>
        /// 记录每个 NodeName 出现的次数（基于历史数据）
        /// </summary>
        private ConcurrentDictionary<string, int> nodeNameCounts = new ConcurrentDictionary<string, int>();

        /// <summary>
        /// 主阶段
        /// </summary>

        public List<TestBasePara> currentAllData { get; set; } = null;

        /// <summary>
        /// 子阶段工况 
        /// </summary>
        public TestBasePara currentStep { get; set; } = null;

        /// <summary>
        /// 工况数据（通过工况编号查询）
        /// </summary>
        public GKData gkData { get; set; } = null;

        /// <summary>
        /// 采集间隔时间
        /// </summary>
        double collectIntervalTime { get; set; } = 0;

        /// <summary>
        /// 当前试验类型 (100h 或 360h)
        /// </summary>
        private string CurrentTestType => MiddleData.instnce.testDataView.TestName == "性能试验" ? "100h" : "360h";

        /// <summary>
        /// 是否为360h试验
        /// </summary>
        private bool Is360hTest => CurrentTestType == "360h";

        /// <summary>
        /// 360小时具体步骤
        /// </summary>
        DurStepConfig durStepConfig { get; set; }

        /// <summary>
        /// 记录每秒的励磁 调试用
        /// </summary>
        List<double> excitationData = new List<double>();

        /// <summary>
        /// 试验逻辑
        /// </summary>
        /// <returns></returns>
        public override TestStatusEnum Execute()
        {
            try
            {
                // 初始化 NodeName 计数
                InitializeNodeNameCounts();

                // 升压速率参数刷新
                excitationRegulation = new ExcitationRegulationConfig(Var.SysConfig.LastModel);

                // 先手动设置限值值
                TVar.ExcitationDiffFast = excitationRegulation.Phase1Current;
                TVar.ExcitationDiffSlow = excitationRegulation.Phase2Current;
                TVar.ExcitationDiffMin = 5;
                TVar.TSecond = 0;

                TestStatus(true);
                TxtTips($"{CurrentTestType}测试开始");

                TestingStatus = TestStatusEnum.IsTest;
                var allCount = 0;
                if (Is360hTest)
                {
                    allCount = hmi.TestConfig360.DurabilityDatas.Count();
                }
                else
                {
                    allCount = hmi.TestConfig100.testStepLists.Count();
                }

                // 启动自动测试循环
                while (MiddleData.instnce.CurrentStatusData.Sore <= allCount && IsTesting)
                {
                    string dayStr = ""; // 日期
                    string phaseStr = ""; // 阶段名

                    if (Common.DIgrp["紧急停止"] == false)
                    {
                        // 拍急停了，
                        Common.AOgrp["发动机油门调节"] = 0;
                        IsTesting = false;
                        break;
                    }

                    if (Is360hTest)
                    {
                        // 360小时试验逻辑
                        var stepList360 = hmi.TestConfig360.DurabilityDatas;

                        if (MiddleData.instnce.CurrentStatusData.Sore > 0 && MiddleData.instnce.CurrentStatusData.Sore <= stepList360.Count)
                        {
                            // 循环代码
                            string node = stepList360[MiddleData.instnce.CurrentStatusData.Sore - 1].NodeName;
                            durStepConfig = new DurStepConfig(Common.mTestViewModel.ModelName, node);

                            // 如果存在就累加 循环代码号
                            UpdateNodeSequence(node);

                            // 总试验步骤
                            currentAllData = durStepConfig.testBasePara;
                            phaseStr = stepList360[MiddleData.instnce.CurrentStatusData.Sore - 1].PhaseName;
                            dayStr = stepList360[MiddleData.instnce.CurrentStatusData.Sore - 1].DayNum;

                            if (currentAllData != null && currentAllData.Count > 0)
                            {
                                // 试验阶段
                                currentStep = currentAllData.FirstOrDefault(d => d.Index == MiddleData.instnce.CurrentStatusData.PhaseIndex);
                                if (currentStep != null)
                                {
                                    // 通过工况编号获取具体转速，励磁电流
                                    MiddleData.instnce.CurrentStatusData.NodeName = node; // 循环代码
                                    MiddleData.instnce.CurrentStatusData.TargetSpeedPercent = MiddleData.instnce.testDataView.TestSpeedPercent = currentStep.RPM;
                                    MiddleData.instnce.CurrentStatusData.TargetTorquePercent = MiddleData.instnce.testDataView.TestTorquePercent = currentStep.Torque;
                                    gkData = hmi.gkConfig.DurabilityDatas.FirstOrDefault(d => d.GKNo == currentStep.GKNo);

                                    var nextCurrentStep = currentAllData.FirstOrDefault(d => d.Index == MiddleData.instnce.CurrentStatusData.PhaseIndex + 1);
                                    if (nextCurrentStep == null)
                                    {
                                        // 当前 循环代码 没找到下一步，找主步骤
                                        if (MiddleData.instnce.CurrentStatusData.Sore + 1 > stepList360.Count)
                                        {
                                            // 主步骤超过最大值，则说明没有下一步了
                                            MiddleData.instnce.testDataView.TestNextSpeedPercent = -1;
                                            MiddleData.instnce.testDataView.TestNextTorquePercent = -1;
                                            MiddleData.instnce.testDataView.TestNextTime = -1;
                                            MiddleData.instnce.testDataView.TestNextGKNo = "/";
                                        }
                                        else
                                        {
                                            // 下一主步骤的第1个
                                            var nextMainStep = stepList360[MiddleData.instnce.CurrentStatusData.Sore]; // 下一主步骤
                                            var nextMainStepConfig = new DurStepConfig(Common.mTestViewModel.ModelName, nextMainStep.NodeName);
                                            if (nextMainStepConfig.testBasePara != null && nextMainStepConfig.testBasePara.Count > 0)
                                            {
                                                var firstStepOfNextMain = nextMainStepConfig.testBasePara.FirstOrDefault();
                                                MiddleData.instnce.testDataView.TestNextSpeedPercent = firstStepOfNextMain.RPM;
                                                MiddleData.instnce.testDataView.TestNextTorquePercent = firstStepOfNextMain.Torque;
                                                MiddleData.instnce.testDataView.TestNextTime = firstStepOfNextMain.RunTime;
                                                MiddleData.instnce.testDataView.TestNextGKNo = firstStepOfNextMain.GKNo;
                                            }
                                            else
                                            {
                                                MiddleData.instnce.testDataView.TestNextSpeedPercent = -1;
                                                MiddleData.instnce.testDataView.TestNextTorquePercent = -1;
                                                MiddleData.instnce.testDataView.TestNextTime = -1;
                                                MiddleData.instnce.testDataView.TestNextGKNo = "/";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MiddleData.instnce.testDataView.TestNextSpeedPercent = nextCurrentStep.RPM;
                                        MiddleData.instnce.testDataView.TestNextTorquePercent = nextCurrentStep.Torque;
                                        MiddleData.instnce.testDataView.TestNextTime = nextCurrentStep.RunTime;
                                        MiddleData.instnce.testDataView.TestNextGKNo = nextCurrentStep.GKNo; ;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // 100小时试验逻辑
                        var stepList100 = hmi.TestConfig100.testStepLists;

                        if (MiddleData.instnce.CurrentStatusData.Sore > 0 && MiddleData.instnce.CurrentStatusData.Sore <= stepList100.Count)
                        {
                            // 总试验步骤
                            currentAllData = stepList100[MiddleData.instnce.CurrentStatusData.Sore - 1].testBasePara;
                            phaseStr = stepList100[MiddleData.instnce.CurrentStatusData.Sore - 1].Index.ToString();
                            dayStr = stepList100[MiddleData.instnce.CurrentStatusData.Sore - 1].DayNum;

                            if (currentAllData != null && currentAllData.Count > 0)
                            {
                                // 试验阶段
                                currentStep = currentAllData.FirstOrDefault(d => d.Index == MiddleData.instnce.CurrentStatusData.PhaseIndex);
                                if (currentStep != null)
                                {
                                    // 通过工况编号获取具体转速，励磁电流
                                    MiddleData.instnce.CurrentStatusData.NodeName = currentStep.TestName; // 循环代码
                                    MiddleData.instnce.CurrentStatusData.TargetSpeedPercent = MiddleData.instnce.testDataView.TestSpeedPercent = currentStep.RPM;
                                    MiddleData.instnce.CurrentStatusData.TargetTorquePercent = MiddleData.instnce.testDataView.TestTorquePercent = currentStep.Torque;
                                    gkData = hmi.gkConfig.DurabilityDatas.FirstOrDefault(d => d.GKNo == currentStep.GKNo);
                                }

                                var nextCurrentStep = currentAllData.FirstOrDefault(d => d.Index == MiddleData.instnce.CurrentStatusData.PhaseIndex + 1);
                                if (nextCurrentStep == null)
                                {
                                    if (MiddleData.instnce.CurrentStatusData.Sore + 1 > stepList100.Count)
                                    {
                                        // 主步骤超过最大值，则说明没有下一步了
                                        MiddleData.instnce.testDataView.TestNextSpeedPercent = -1;
                                        MiddleData.instnce.testDataView.TestNextTorquePercent = -1;
                                        MiddleData.instnce.testDataView.TestNextTime = -1;
                                        MiddleData.instnce.testDataView.TestNextGKNo = "/";
                                    }
                                    else
                                    {
                                        var nextMainStep = stepList100[MiddleData.instnce.CurrentStatusData.Sore].testBasePara; // 下一主步骤
                                        if (nextMainStep.Count != 0)
                                        {
                                            MiddleData.instnce.testDataView.TestNextSpeedPercent = nextMainStep[0].RPM;
                                            MiddleData.instnce.testDataView.TestNextTorquePercent = nextMainStep[0].Torque;
                                            MiddleData.instnce.testDataView.TestNextTime = nextMainStep[0].RunTime;
                                            MiddleData.instnce.testDataView.TestNextGKNo = nextMainStep[0].GKNo;
                                        }
                                        else
                                        {
                                            MiddleData.instnce.testDataView.TestNextSpeedPercent = -1;
                                            MiddleData.instnce.testDataView.TestNextTorquePercent = -1;
                                            MiddleData.instnce.testDataView.TestNextTime = -1;
                                            MiddleData.instnce.testDataView.TestNextGKNo = "/";
                                        }
                                    }
                                }
                                else
                                {
                                    MiddleData.instnce.testDataView.TestNextSpeedPercent = nextCurrentStep.RPM;
                                    MiddleData.instnce.testDataView.TestNextTorquePercent = nextCurrentStep.Torque;
                                    MiddleData.instnce.testDataView.TestNextTime = nextCurrentStep.RunTime;
                                    MiddleData.instnce.testDataView.TestNextGKNo = nextCurrentStep.GKNo;
                                }
                            }
                        }
                    }

                    // 验证数据有效性
                    if (currentAllData == null || currentAllData.Count == 0)
                    {
                        TxtTips($"错误：未找到当前步骤的试验数据，步骤号：{MiddleData.instnce.CurrentStatusData.Sore}");
                        TestingStatus = TestStatusEnum.Error;
                        break;
                    }

                    if (currentStep == null)
                    {
                        TxtTips($"错误：未找到当前阶段的试验步骤，阶段索引：{MiddleData.instnce.CurrentStatusData.PhaseIndex}");
                        TestingStatus = TestStatusEnum.Error;
                        break;
                    }

                    if (gkData == null)
                    {
                        TxtTips($"错误：未找到工况编号对应的数据，工况编号：{currentStep.GKNo}");
                        TestingStatus = TestStatusEnum.Error;
                        break;
                    }

                    // 刷新状态
                    MiddleData.instnce.CurrentStatusData.PhaseIndex = currentStep.Index; // 序号
                    MiddleData.instnce.CurrentStatusData.PhaseName = phaseStr; // 阶段
                    MiddleData.instnce.CurrentStatusData.CycleName = currentStep.CycleName == "NULL" ? "-" : currentStep.CycleName; // 周期
                    MiddleData.instnce.CurrentStatusData.DayName = dayStr; // 天数

                    MiddleData.instnce.CurrentStatusData.AllPhaseIndex = currentAllData.Count; // 总序号
                    MiddleData.instnce.CurrentStatusData.GKBH = currentStep.GKNo; // 工况编号
                    MiddleData.instnce.CurrentStatusData.Speed = gkData.Speed; // 转速
                    MiddleData.instnce.CurrentStatusData.ExcitationCurrent = gkData.ExcitationCurrent; //励磁电流
                    MiddleData.instnce.CurrentStatusData.TargetPower = gkData.Power; // 功率
                    MiddleData.instnce.CurrentStatusData.TargetOperationTime = currentStep.RunTime; // 目标运行时间
                    MiddleData.instnce.CurrentStatusData.Save();

                    // 更新UI显示
                    hmi.UpdateLastStepData();

                    // 重启动步骤计时器
                    hmi.StopWatch();
                    hmi.BeginWatch();

                    TestLog.UpdateTestPara($"{DateTime.Now}：序号:{MiddleData.instnce.CurrentStatusData.Sore} 设定速度:{MiddleData.instnce.CurrentStatusData.Speed} 励磁电流：{ MiddleData.instnce.CurrentStatusData.ExcitationCurrent} 目标功率：{MiddleData.instnce.CurrentStatusData.TargetPower}");

                    // 计算当前步骤需要执行的时间  工况运行时间-已执行的时间
                    double waitTimeMinute = currentStep.RunTime;

                    TVar.Second = 0;
                    TVar.Minute = 0;
                    TVar.ExAdjustSecond = 0;
                    TVar.LastAdjustVal = 0;
                    TVar.TSecond = 0;
                    excitationData.Clear();

                    // 试验逻辑（调转速/调励磁/试验时间）
                    while (IsTesting)
                    {
                        // 急停或故障停机中取消试验
                        if (Common.DIgrp["紧急停止"] == false && Var.FaultService.IsStopDoing)
                        {
                            // 拍急停了，
                            Common.AOgrp["发动机油门调节"] = 0;
                            IsTesting = false;
                            break;
                        }

                        if (MiddleData.instnce.CurrentStatusData.StepTime >= waitTimeMinute)
                            break;

                        // 计算差值
                        CalculateDifferences();

                        // 非暂停，并且处于运行中
                        if (TestingStatus == TestStatusEnum.IsTest)
                        {
                            TVar.Second++;

                            // 励磁到位后开始计时（TVar.ExcitationDiffFast = 实际允许差值）
                            if (TVar.ExcitationDifference <= TVar.ExcitationDiffMin || TVar.TSecond > 0)
                            {
                                TVar.TSecond++;
                                if (TVar.TSecond == 1)
                                    Console.WriteLine(string.Join(", ", excitationData));

                                // 励磁微调修正逻辑 （TVar.TSecond 大于0后，通过功率进行微调，降弓不判断 TSecond）
                                HandleExcitationCorrection();
                            }
                            else
                            {
                                // 调节励磁
                                HandleExcitationControlLogic();
                            }

                            // 时间累计
                            if (TVar.Second >= 60)
                            {
                                TVar.Second = 0;
                                TVar.Minute++;
                            }
                        }
                        else if (TestingStatus == TestStatusEnum.Pause)
                        {
                            // 暂停

                        }
                        else if (TestingStatus == TestStatusEnum.Continue)
                        {
                            // 继续（复位状态）
                            TVar.ExAdjustSecond = 0;
                            TVar.LastAdjustVal = 0;
                            TVar.TSecond = 0;

                            TestingStatus = TestStatusEnum.IsTest;
                        }

                        Thread.Sleep(990);
                    }
                    if (!IsTesting)
                    {
                        // 测试被中止
                        TestingStatus = TestStatusEnum.Stop;
                        break;
                    }

                    // 停止步骤计时器
                    hmi.StopWatch();

                    MiddleData.instnce.CurrentStatusData.StepTime = 0;

                    // 移动到阶段（下一循环代码）
                    if (MiddleData.instnce.CurrentStatusData.PhaseIndex + 1 > MiddleData.instnce.CurrentStatusData.AllPhaseIndex)
                    {
                        lock (hmi.locked)
                        {
                            MiddleData.instnce.CurrentStatusData.Sore++;
                            MiddleData.instnce.CurrentStatusData.PhaseIndex = 1;
                            MiddleData.instnce.CurrentStatusData.Save();
                        }
                    }
                    else
                    {
                        lock (hmi.locked)
                        {
                            MiddleData.instnce.CurrentStatusData.PhaseIndex++;
                            MiddleData.instnce.CurrentStatusData.Save();
                        }
                    }
                }

                if (IsTesting)
                {
                    // 所有测试步骤完成
                    if (currentAllData != null && MiddleData.instnce.CurrentStatusData.Sore > currentAllData.Count)
                    {
                        // todo 所有试验状态回到第一步即可
                        MiddleData.instnce.CurrentStatusData.Sore = currentAllData.Count;
                        MiddleData.instnce.CurrentStatusData.TestStatus = true;
                        MiddleData.instnce.CurrentStatusData.StepTime = 0;
                        MiddleData.instnce.CurrentStatusData.Save();

                        TestingStatus = TestStatusEnum.Success;

                        TestLog.UpdateTestPara($"{DateTime.Now}：测试完成，总共采集{MiddleData.instnce.testDataView.TotalCollectCount}次数据");
                        TxtTips("100h测试完成");
                    }
                }
                else
                {
                    TxtTips("测试中止");
                }

                MiddleData.instnce.testDataView.TestSpeedPercent = 0;
                MiddleData.instnce.testDataView.TestTorquePercent = 0;

                TestStatus(false);
                return TestingStatus;
            }
            catch (Exception ex)
            {
                TxtTips($"测试异常: {ex.Message}");
                return TestStatusEnum.Error;
            }
        }

        /// <summary>
        /// 励磁控制主逻辑
        /// </summary>
        private void HandleExcitationControlLogic()
        {
            double targetExcitation = MiddleData.instnce.CurrentStatusData.ExcitationCurrent; // 目标励磁
            double currentExcitation = Common.AOgrp["励磁调节"]; // 实时设置励磁
            double targetSpeed = MiddleData.instnce.CurrentStatusData.Speed; // 目标转速
            double currentSpeed = MiddleData.instnce.EngineSpeed; // 实时转速

            // 判定目标励磁电流是否大于当前设定励磁电流
            if (targetExcitation > currentExcitation)
            {
                // 分支1：目标励磁大于当前励磁
                HandleGreaterExcitationCondition(targetExcitation, currentExcitation, targetSpeed, currentSpeed);
            }
            else
            {
                // 分支2：目标励磁小于等于当前励磁
                HandleLessOrEqualExcitationCondition(targetExcitation, currentExcitation, targetSpeed, currentSpeed);
            }
        }

        /// <summary>
        /// 处理目标励磁大于当前励磁的情况
        /// </summary>
        /// <param name="targetExcitation">目标励磁</param>
        /// <param name="currentExcitation">实时设定励磁</param>
        /// <param name="targetSpeed">目标转速</param>
        /// <param name="currentSpeed">实时转速</param>
        private void HandleGreaterExcitationCondition(double targetExcitation, double currentExcitation, double targetSpeed, double currentSpeed)
        {
            // 等待转速达到目标转速的±30%范围内
            double speedTolerance = targetSpeed * 0.3;
            bool isSpeedInRange = Math.Abs(currentSpeed - targetSpeed) <= speedTolerance;

            if (isSpeedInRange)
            {
                // 2. 转速达标后开始调节励磁
                double excitationAdjustment = CalculateExcitationAdjustment(targetExcitation, currentExcitation);
                double MaxExcitation = 0;
                if (excitationAdjustment > 0)
                {
                    // 励磁调节，最大不超过目标值
                    TVar.Variable = excitationAdjustment;
                    MaxExcitation = Math.Min(currentExcitation + excitationAdjustment, targetExcitation);
                    Common.AOgrp.SetExcitationVal(MaxExcitation);
                    excitationData.Add(MaxExcitation);
                }

                // 检测励磁是否达到目标值的±3A范围内
                if (Math.Abs(Common.AOgrp["励磁调节"] - targetExcitation) <= TVar.ExcitationDiffMin)
                {
                    // 到达励磁后再次确认转速
                    Common.AOgrp["发动机油门调节"] = targetSpeed;
                }
            }
            else
            {
                // 下发一次转速
                if (TVar.Second == 1)
                {
                    // 1. 先直接设定目标转速
                    Common.AOgrp["发动机油门调节"] = targetSpeed;
                }
            }
        }

        /// <summary>
        /// 处理目标励磁小于等于当前励磁的情况
        /// </summary>
        /// <param name="targetExcitation">目标励磁</param>
        /// <param name="currentExcitation">实时设定励磁</param>
        /// <param name="targetSpeed">目标转速</param>
        /// <param name="currentSpeed">实时转速</param>
        private void HandleLessOrEqualExcitationCondition(double targetExcitation, double currentExcitation, double targetSpeed, double currentSpeed)
        {
            // 直接计算并调节励磁差值
            double excitationAdjustment = CalculateDownAdjustment(targetExcitation, currentExcitation); //CalculateExcitationAdjustment
            double MaxExcitation = 0;
            if (excitationAdjustment > 0)
            {
                // 励磁调节，最小不低于目标值
                TVar.Variable = excitationAdjustment;
                MaxExcitation = Math.Max(currentExcitation - excitationAdjustment, targetExcitation);
                // 应用励磁调节（降励磁），最小值不能低于目标值
                Common.AOgrp.SetExcitationVal(MaxExcitation);
                excitationData.Add(MaxExcitation);
            }

            // 检测励磁是否达到目标值的±3A范围内
            if (Math.Abs(MaxExcitation - targetExcitation) <= TVar.ExcitationDiffMin)
            {
                // 励磁达标后下发目标转速
                Common.AOgrp["发动机油门调节"] = targetSpeed;
            }
        }

        /// <summary>
        /// 升励磁 计算励磁调节量
        /// </summary>
        /// <param name="targetExcitation">目标励磁</param>
        /// <param name="currentExcitation">设定励磁</param>
        /// <returns></returns>
        private double CalculateExcitationAdjustment(double targetExcitation, double currentExcitation)
        {
            double excitationDifference = Math.Abs(targetExcitation - currentExcitation);
            double adjustment = 0;

            // 目标励磁 > 当前励磁
            if (targetExcitation > currentExcitation)
            {
                // 使用原有的分段调节逻辑
                if (excitationDifference >= TVar.ExcitationDiffFast)
                {
                    adjustment = CalculateFastAdjustment(excitationDifference);
                }
                else if (excitationDifference >= TVar.ExcitationDiffSlow)
                {
                    adjustment = CalculateMediumAdjustment(excitationDifference);
                }
                else
                {
                    // 慢速统一调整 差值的 25%
                    adjustment = excitationDifference * 0.25; //CalculateSlowAdjustment(excitationDifference);
                }
            }
            return Math.Round(adjustment, 1);
        }

        /// <summary>
        /// 快速调节段（差值≥150A）
        /// </summary>
        private double CalculateFastAdjustment(double difference)
        {
            if (TVar.LastAdjustVal != 1)
            {
                TVar.LastAdjustVal = 1;
                TVar.ExAdjustSecond = 1;
            }

            double adjustment = 0;
            // 升功率分段给励磁（5s一个区间，35 %，35 %，30 %，30 %, 默认 35,换算成每秒*0.2）
            switch (TVar.ExAdjustSecond)
            {
                case int n when (n >= 1 && n <= 5):
                    adjustment = difference * (excitationRegulation.Phase1Rise1 / 100);
                    break;
                case int n when (n >= 6 && n <= 10):
                    adjustment = difference * (excitationRegulation.Phase1Rise2 / 100);
                    break;
                case int n when (n >= 11 && n <= 15):
                    adjustment = difference * (excitationRegulation.Phase1Rise3 / 100);
                    break;
                case int n when (n >= 16 && n <= 20):
                    adjustment = difference * (excitationRegulation.Phase1Rise4 / 100);
                    break;
                default:
                    adjustment = difference * (excitationRegulation.Phase1Rise5 / 100);
                    break;
            }

            adjustment = Math.Round(adjustment * 0.2, 1);
            TVar.ExAdjustSecond++;
            return adjustment;
        }

        /// <summary>
        /// 中速调节段（差值40A-150A）
        /// </summary>
        private double CalculateMediumAdjustment(double difference)
        {
            if (TVar.LastAdjustVal != 2)
            {
                TVar.LastAdjustVal = 2;
                TVar.ExAdjustSecond = 1;
            }

            double adjustment = 0;
            //（5s一个区间，40 %，35 %，35 %，35 %，30 %，默认30 ，换算成每秒*0.2）
            switch (TVar.ExAdjustSecond)
            {
                case int n when (n >= 1 && n <= 5):
                    adjustment = difference * (excitationRegulation.Phase2Rise1 / 100);
                    break;
                case int n when (n >= 6 && n <= 10):
                    adjustment = difference * (excitationRegulation.Phase2Rise2 / 100);
                    break;
                case int n when (n >= 11 && n <= 15):
                    adjustment = difference * (excitationRegulation.Phase2Rise3 / 100);
                    break;
                case int n when (n >= 16 && n <= 20):
                    adjustment = difference * (excitationRegulation.Phase2Rise4 / 100);
                    break;
                case int n when (n >= 21 && n <= 25):
                    adjustment = difference * (excitationRegulation.Phase2Rise5 / 100);
                    break;
                default:
                    adjustment = difference * (excitationRegulation.Phase2Rise6 / 100);
                    break;
            }

            adjustment = Math.Round(adjustment * 0.2, 1);
            TVar.ExAdjustSecond++;
            return adjustment;
        }

        /// <summary>
        /// 降励磁 计算励磁调节量
        /// </summary>
        private double CalculateDownAdjustment(double targetExcitation, double currentExcitation)
        {
            if (TVar.LastAdjustVal != 4)
            {
                TVar.LastAdjustVal = 4;
                TVar.ExAdjustSecond = 4;
            }

            double difference = Math.Abs(targetExcitation - currentExcitation);

            double adjustment = 0;
            // 降功率分段给励磁（每秒调整差，20 %，30 %）
            switch (TVar.ExAdjustSecond)
            {
                case int n when (n >= 1 && n <= 3):
                    adjustment = difference * (excitationRegulation.Phase3Rise1 / 100);
                    break;
                case int n when (n >= 4 && n <= 6):
                    adjustment = difference * (excitationRegulation.Phase3Rise2 / 100);
                    break;
                default:
                    adjustment = difference * (excitationRegulation.Phase3Rise3 / 100);
                    break;
            }
            TVar.ExAdjustSecond++;
            return Math.Round(adjustment, 1);
        }

        ///// <summary>
        ///// 慢速调节段（差值<50A）
        ///// </summary>
        //private double CalculateSlowAdjustment(double difference)
        //{
        //    if (TVar.LastAdjustVal != 3)
        //    {
        //        TVar.LastAdjustVal = 3;
        //        TVar.ExAdjustSecond = 1;
        //    }

        //    double adjustment = 0;
        //    //（每2s，40 %，30 %，30 %）
        //    switch (TVar.ExAdjustSecond)
        //    {
        //        case int n when (n >= 1 && n <= 5):
        //            adjustment = difference * 0.40;
        //            break;
        //        case int n when (n >= 6 && n <= 10):
        //            adjustment = difference * 0.40;
        //            break;
        //        case int n when (n >= 11 && n <= 15):
        //            adjustment = difference * 0.38;
        //            break;
        //        default:
        //            adjustment = difference * 0.38;
        //            break;
        //    }

        //    adjustment = Math.Round(adjustment * 0.5, 1);
        //    TVar.ExAdjustSecond++;
        //    return adjustment;
        //}

        /// <summary>
        /// 励磁微调修正处理
        /// </summary>
        private void HandleExcitationCorrection()
        {
            // 功率差值大于目标值则进行微调
            var pDifference = Math.Abs(Common.AOgrp["励磁调节"] - MiddleData.instnce.CurrentStatusData.TargetPower);
            if (pDifference > MiddleData.instnce.CurrentStatusData.TargetPower * 0.005 && MiddleData.instnce.CurrentStatusData.TargetPower > 0)
            {
                double power = MiddleData.instnce.EnginePower;

                // 根据功率范围和功率差计算修正值
                TVar.XVariable = CalculateCorrectionValue(power, TVar.PowerDifference);

                // 目标功率 > 当前功率
                if (MiddleData.instnce.CurrentStatusData.TargetPower > power)
                {
                    // 升功率 （前提调节 励磁基本到位）

                    // 功率在2500kW 以上，励磁稳定5秒后，每隔 8 秒进行一次调节
                    if ((TVar.TSecond >= 5 && (TVar.TSecond - 5) % 8 == 0) || TVar.TSecond == 5 && MiddleData.instnce.CurrentStatusData.TargetPower >= 2500)
                    {
                        if (Common.AOgrp["励磁调节"] <= MiddleData.instnce.CurrentStatusData.ExcitationCurrent * 1.01 && MiddleData.instnce.CurrentStatusData.TargetPower >= 2500)
                        {
                            Common.AOgrp.SetExcitationVal(Common.AOgrp["励磁调节"] + TVar.XVariable);
                        }
                    }

                    // 功率在2500kW 以下，励磁稳定5秒后，每隔 6 秒进行一次调节
                    if ((TVar.TSecond >= 5 && (TVar.TSecond - 5) % 6 == 0) || TVar.TSecond == 5)
                    {
                        if (Common.AOgrp["励磁调节"] <= MiddleData.instnce.CurrentStatusData.ExcitationCurrent * 1.02 && MiddleData.instnce.CurrentStatusData.TargetPower < 2500)
                        {
                            Common.AOgrp.SetExcitationVal(Common.AOgrp["励磁调节"] + TVar.XVariable);
                        }
                    }
                }
                else
                {
                    // 降功率 10 秒后每隔2秒调节一次
                    if ((TVar.TSecond % 2 == 0 && TVar.TSecond > 10) || TVar.TSecond == 10)
                    {
                        Common.AOgrp.SetExcitationVal(Common.AOgrp["励磁调节"] - TVar.XVariable);
                    }
                }
            }
        }

        /// </summary>
        /// 计算修正值
        /// </summary>
        /// <param name="power">目标功率</param>
        /// <param name="powerDiff">功率差值</param>
        /// <returns></returns>
        private double CalculateCorrectionValue(double power, double powerDiff)
        {
            double result = 0;
            if (power >= 3500 && power <= 5000)
            {
                switch (powerDiff)
                {
                    case var _ when powerDiff >= 0 && powerDiff <= 29:
                        result = 0.3;
                        break;
                    case var _ when powerDiff >= 30 && powerDiff <= 49:
                        result = 0.6;
                        break;
                    case var _ when powerDiff >= 50 && powerDiff <= 99:
                        result = 1.3;
                        break;
                    case var _ when powerDiff >= 100 && powerDiff <= 199:
                        result = 1.9;
                        break;
                    case var _ when powerDiff >= 200 && powerDiff <= 299:
                        result = 2.5;
                        break;
                    case var _ when powerDiff >= 300 && powerDiff <= 500:
                        result = 3.8;
                        break;
                    default:
                        result = 0;
                        break;
                }
            }
            else if (power >= 2500 && power < 3500)
            {
                switch (powerDiff)
                {
                    case var _ when powerDiff >= 0 && powerDiff <= 19:
                        return 0.3;
                    case var _ when powerDiff >= 20 && powerDiff <= 49:
                        return 0.6;
                    case var _ when powerDiff >= 50 && powerDiff <= 99:
                        return 1.6;
                    case var _ when powerDiff >= 100 && powerDiff <= 199:
                        return 1.9;
                    case var _ when powerDiff >= 200 && powerDiff <= 299:
                        return 2.5;
                    case var _ when powerDiff >= 300 && powerDiff <= 500:
                        return 3.8;
                    default:
                        return 0;
                }
            }
            else if (power >= 1500 && power < 2500)
            {
                switch (powerDiff)
                {
                    case var _ when powerDiff >= 0 && powerDiff <= 19:
                        return 0.5;
                    case var _ when powerDiff >= 20 && powerDiff <= 49:
                        return 0.9;
                    case var _ when powerDiff >= 50 && powerDiff <= 99:
                        return 1.6;
                    case var _ when powerDiff >= 100 && powerDiff <= 199:
                        return 2.5;
                    case var _ when powerDiff >= 200 && powerDiff <= 299:
                        return 3.1;
                    case var _ when powerDiff >= 300 && powerDiff <= 500:
                        return 4.7;
                    case var _ when powerDiff >= 501 && powerDiff <= 1000:
                        return 6.3;
                    default:
                        return 0;
                }
            }
            else if (power >= 50 && power < 1500)
            {
                switch (powerDiff)
                {
                    case var _ when powerDiff >= 0 && powerDiff <= 19:
                        return 0.6;
                    case var _ when powerDiff >= 20 && powerDiff <= 49:
                        return 1.6;
                    case var _ when powerDiff >= 50 && powerDiff <= 99:
                        return 3.1;
                    case var _ when powerDiff >= 100 && powerDiff <= 199:
                        return 6.3;
                    case var _ when powerDiff >= 200 && powerDiff <= 299:
                        return 9.4;
                    case var _ when powerDiff >= 300 && powerDiff <= 500:
                        return 9.4;
                    default:
                        return 0;
                }
            }

            return result;
        }

        /// <summary>
        /// 计算各种差值
        /// </summary>
        private void CalculateDifferences()
        {
            // 计算功率和实际功率的差值
            TVar.PowerDifference = Math.Abs(MiddleData.instnce.EnginePower - MiddleData.instnce.CurrentStatusData.TargetPower);

            // 计算目标励磁和当前励磁差值
            TVar.ExcitationDifference = Math.Abs(Common.AOgrp["励磁调节"] - MiddleData.instnce.CurrentStatusData.ExcitationCurrent);

            // 试验总小时数
            MiddleData.instnce.RunHour = (int)MiddleData.instnce.CurrentStatusData.StepTimeTotal / 60;
        }


        /// <summary>
        /// 初始化 NodeName 计数（基于当前试验进度）
        /// </summary>
        /// <summary>
        /// 初始化 NodeName 计数（统计到当前步骤的前一步）
        /// </summary>
        private void InitializeNodeNameCounts()
        {
            nodeNameCounts.Clear();
            MiddleData.instnce.CurrentStatusData.NodeNameAccumulate = "";

            // 如果是第一步，不需要统计历史
            if (MiddleData.instnce.CurrentStatusData.Sore <= 1)
            {
                return;
            }

            var stepList360 = hmi.TestConfig360.DurabilityDatas;
            int stepsToCount = MiddleData.instnce.CurrentStatusData.Sore - 1; // 统计到前一步

            // 统计历史数据
            for (int i = 0; i < stepsToCount; i++)
            {
                if (i < stepList360.Count)
                {
                    string nodeName = stepList360[i].NodeName;
                    int currentCount = nodeNameCounts.AddOrUpdate(nodeName, 1, (k, oldValue) => oldValue + 1);
                    TxtTips($"步骤{i + 1}: NodeName='{nodeName}'，累计计数={currentCount}");
                }
            }
        }

        /// <summary>
        /// 更新 NodeName 序号
        /// </summary>
        private void UpdateNodeSequence(string nodeName)
        {
            nodeNameCounts.AddOrUpdate(nodeName, 1, (k, oldValue) => oldValue + 1);
            MiddleData.instnce.CurrentStatusData.NodeNameAccumulate = $"{nodeNameCounts[nodeName]}{nodeName}";
        }

        #region 暂时注释
        ///// <summary>
        ///// 计算步骤剩余时间（基于时间区间的新方法）
        ///// </summary>
        ///// <param name="resumeStep">恢复的步骤</param>
        ///// <param name="phaseBeginSore">阶段开始序号</param>
        ///// <param name="phaseCollectNum">阶段内采集次数</param>
        ///// <param name="phaseCollectTime">采集间隔时间</param>
        ///// <returns>步骤剩余时间（分钟）</returns>
        //private double CalculateRemainingStepTimeByTimeRange(AutoTestStep resumeStep, int phaseBeginSore, int phaseCollectNum, double phaseCollectTime)
        //{
        //    if (resumeStep == null) return 0;

        //    // 验证参数
        //    if (!ValidateStepIndex(resumeStep.Sore) || !ValidateStepIndex(phaseBeginSore))
        //    {
        //        return resumeStep.RunTime;
        //    }

        //    // 获取阶段内所有步骤的时间区间
        //    var timeRanges = GetPhaseTimeRanges(phaseBeginSore);

        //    // 计算已过去的时间
        //    double elapsedTime = phaseCollectNum * phaseCollectTime;

        //    // 找到当前步骤的时间区间
        //    var currentRange = timeRanges.FirstOrDefault(r => r.Sore == resumeStep.Sore);
        //    if (currentRange == null)
        //    {
        //        TxtTips($"找不到步骤{resumeStep.Sore}的时间区间");
        //        return resumeStep.RunTime;
        //    }

        //    // 计算步骤剩余时间
        //    double remainingTime;

        //    // 未采集到和不在采集区间的方法
        //    if (elapsedTime < currentRange.StartTime)
        //    {
        //        // 情况1：已过去时间小于当前步骤开始时间，间隔时间应该要减去已经运行的时间
        //        var diffTime = currentRange.StartTime - elapsedTime;

        //        // 间隔时间
        //        collectIntervalTime = resumeStep.CollectIntervalTime - diffTime;
        //        remainingTime = resumeStep.RunTime;
        //    }
        //    else if (elapsedTime >= currentRange.StartTime && elapsedTime < currentRange.EndTime)
        //    {
        //        // 情况2：已过去时间在当前步骤的时间区间内
        //        remainingTime = currentRange.EndTime - elapsedTime;
        //        // 间隔时间
        //        collectIntervalTime = resumeStep.CollectIntervalTime;

        //        TxtTips($"步骤{resumeStep.Sore}：已过去{elapsedTime}分钟，剩余{remainingTime:F1}分钟");
        //    }
        //    else
        //    {
        //        // 情况3：已过去时间超过当前步骤结束时间
        //        // 这种情况不应该发生，因为我们应该已经记录了下一步
        //        TxtTips($"时间计算异常：已过去时间{elapsedTime}超过步骤{resumeStep.Sore}结束时间{currentRange.EndTime}");
        //        remainingTime = 0;
        //    }

        //    // 确保剩余时间在合理范围内
        //    remainingTime = Math.Max(0, Math.Min(resumeStep.RunTime, remainingTime));

        //    return remainingTime;
        //}

        ///// <summary>
        ///// 获取阶段内所有步骤的时间区间
        ///// </summary>
        ///// <param name="phaseBeginSore">阶段开始序号</param>
        ///// <returns>步骤时间区间列表</returns>
        //private List<StepTimeRange> GetPhaseTimeRanges(int phaseBeginSore)
        //{
        //    var timeRanges = new List<StepTimeRange>();

        //    if (hmi.AutoTestStepList == null || hmi.AutoTestStepList.Count == 0)
        //        return timeRanges;

        //    // 找到阶段内的所有步骤
        //    var phaseSteps = hmi.AutoTestStepList
        //        .Where(s => s.Sore >= phaseBeginSore && s.TestName == hmi.AutoTestStepList[phaseBeginSore - 1].TestName)
        //        .OrderBy(s => s.Sore)
        //        .ToList();

        //    if (phaseSteps.Count == 0)
        //        return timeRanges;

        //    double currentTime = 0;

        //    foreach (var step in phaseSteps)
        //    {
        //        var timeRange = new StepTimeRange
        //        {
        //            Sore = step.Sore,
        //            StepName = step.TestName,
        //            StartTime = currentTime,
        //            EndTime = currentTime + step.RunTime,
        //            RunTime = step.RunTime
        //        };

        //        timeRanges.Add(timeRange);
        //        currentTime += step.RunTime;

        //        TxtTips($"步骤{step.Sore}: [{timeRange.StartTime}-{timeRange.EndTime})分钟");
        //    }

        //    return timeRanges;
        //}

        ///// <summary>
        ///// 为采集调整步骤时间（基于时间区间的新方法）
        ///// </summary>
        //private double CalculateAdjustedStepTimeForCollectionByTimeRange(AutoTestStep currentStep, int phaseBeginSore, int phaseCollectNum)
        //{
        //    // 获取阶段内所有步骤的时间区间
        //    var timeRanges = GetPhaseTimeRanges(phaseBeginSore);

        //    // 找到当前步骤的时间区间
        //    var currentRange = timeRanges.FirstOrDefault(r => r.Sore == currentStep.Sore);
        //    if (currentRange == null)
        //    {
        //        TxtTips($"找不到步骤{currentStep.Sore}的时间区间");
        //        return currentStep.RunTime;
        //    }

        //    // 计算下一次采集的时间点
        //    double nextCollectTime = (phaseCollectNum + 1) * currentStep.CollectIntervalTime;

        //    // 检查下一次采集是否在当前步骤的时间区间内
        //    if (nextCollectTime > currentRange.StartTime && nextCollectTime <= currentRange.EndTime)
        //    {
        //        // 调整步骤时间为到采集点的时间
        //        double adjustedTime = nextCollectTime - currentRange.StartTime;
        //        TxtTips($"正常执行：步骤{currentStep.Sore}调整为{adjustedTime:F1}分钟以匹配采集时间点");
        //        return adjustedTime;
        //    }

        //    // 否则正常执行完整步骤
        //    TxtTips($"正常执行：步骤{currentStep.Sore}完整执行{currentStep.RunTime:F1}分钟");
        //    return currentStep.RunTime;
        //}

        ///// <summary>
        ///// 计算从阶段开始到指定步骤开始的总时间（不包括指定步骤本身）
        ///// </summary>
        ///// <param name="phaseBeginSore">阶段开始序号</param>
        ///// <param name="targetSore">目标步骤序号</param>
        ///// <returns>从阶段开始到目标步骤开始的总时间（分钟）</returns>
        //private double CalculateTimeToStep(int phaseBeginSore, int targetSore)
        //{
        //    // 参数验证
        //    if (phaseBeginSore <= 0 || targetSore <= 0)
        //    {
        //        TxtTips($"参数错误：phaseBeginSore={phaseBeginSore}, targetSore={targetSore}");
        //        return 0;
        //    }

        //    if (phaseBeginSore > targetSore)
        //    {
        //        TxtTips($"阶段开始序号{phaseBeginSore}不能大于目标步骤序号{targetSore}");
        //        return 0;
        //    }

        //    if (hmi.AutoTestStepList == null || hmi.AutoTestStepList.Count == 0)
        //    {
        //        TxtTips("步骤列表为空");
        //        return 0;
        //    }

        //    // 如果阶段开始序号等于目标步骤序号，则时间为0
        //    if (phaseBeginSore == targetSore)
        //    {
        //        return 0;
        //    }

        //    double totalTime = 0;

        //    try
        //    {
        //        // 遍历从阶段开始到目标步骤之前的所有步骤
        //        for (int i = phaseBeginSore - 1; i < targetSore - 1; i++)
        //        {
        //            if (i < 0 || i >= hmi.AutoTestStepList.Count)
        //            {
        //                TxtTips($"步骤索引{i}超出范围，列表长度={hmi.AutoTestStepList.Count}");
        //                break;
        //            }

        //            var step = hmi.AutoTestStepList[i];
        //            totalTime += step.RunTime;
        //        }

        //        TxtTips($"从阶段开始步骤{phaseBeginSore}到目标步骤{targetSore}开始的总时间：{totalTime}分钟");
        //    }
        //    catch (Exception ex)
        //    {
        //        TxtTips($"计算时间出错：{ex.Message}");
        //        return 0;
        //    }

        //    return totalTime;
        //}

        ///// <summary>
        ///// 计算步骤内采集次数
        ///// </summary>
        //private int CalculateStepCollectNum(AutoTestStep currentStep, int phaseCollectNum)
        //{
        //    if (currentStep == null) return 1;

        //    // 获取当前阶段的所有步骤
        //    var stageSteps = hmi.AutoTestStepList
        //        .Where(s => s.TestName == currentStep.TestName)
        //        .OrderBy(s => s.Sore)
        //        .ToList();

        //    if (stageSteps.Count == 0) return 1;

        //    // 找到当前步骤在阶段中的索引
        //    int stepIndexInStage = stageSteps.FindIndex(s => s.Sore == currentStep.Sore);
        //    if (stepIndexInStage < 0) return 1;

        //    // 计算阶段开始到当前步骤开始的总时间
        //    double timeToStepStart = 0;
        //    for (int i = 0; i < stepIndexInStage; i++)
        //    {
        //        timeToStepStart += stageSteps[i].RunTime;
        //    }

        //    // 计算当前步骤内已运行时间
        //    double elapsedTimeInStep = (phaseCollectNum * currentStep.CollectIntervalTime) - timeToStepStart;

        //    // 确保不会小于0
        //    elapsedTimeInStep = Math.Max(0, elapsedTimeInStep);

        //    // 计算步骤内采集次数
        //    int stepCollectNum = (int)Math.Floor(elapsedTimeInStep / currentStep.CollectIntervalTime) + 1;

        //    return Math.Max(1, stepCollectNum);
        //}

        ///// <summary>
        ///// 验证步骤序号是否有效
        ///// </summary>
        //private bool ValidateStepIndex(int sore)
        //{
        //    if (hmi.AutoTestStepList == null || hmi.AutoTestStepList.Count == 0)
        //    {
        //        TxtTips("步骤列表为空");
        //        return false;
        //    }

        //    if (sore <= 0 || sore > hmi.AutoTestStepList.Count)
        //    {
        //        TxtTips($"步骤序号{sore}超出有效范围[1, {hmi.AutoTestStepList.Count}]");
        //        return false;
        //    }

        //    var step = hmi.AutoTestStepList.FirstOrDefault(s => s.Sore == sore);
        //    if (step == null)
        //    {
        //        TxtTips($"找不到步骤{sore}");
        //        return false;
        //    }

        //    return true;
        //}

        ///// <summary>
        ///// 保存当前试验状态
        ///// </summary>
        //private void SaveCurrentStatus(AutoTestStep currentStep, int phaseCollectNum, bool stepCompleted = false)
        //{
        //    if (currentStep == null) return;

        //    // 计算步骤内采集次数
        //    int stepCollectNum = CalculateStepCollectNum(currentStep, phaseCollectNum);

        //    MiddleData.instnce.CurrentStatusData.TestStatus = false;
        //    MiddleData.instnce.CurrentStatusData.Sore = stepCompleted ? currentStep.Sore + 1 : currentStep.Sore;
        //    MiddleData.instnce.CurrentStatusData.PhaseIndex = currentStep.StepIndex;
        //    MiddleData.instnce.CurrentStatusData.PhaseName = currentStep.TestName;
        //    MiddleData.instnce.CurrentStatusData.CycleName = currentStep.CycleName;
        //    MiddleData.instnce.CurrentStatusData.NodeName = currentStep.StepName;
        //    MiddleData.instnce.CurrentStatusData.PhaseCollectNum = phaseCollectNum;
        //    MiddleData.instnce.CurrentStatusData.PhaseCollectTime = currentStep.CollectIntervalTime;
        //    MiddleData.instnce.CurrentStatusData.Save();

        //    TestLog.UpdateTestPara($"{DateTime.Now}：保存状态 - 步骤{currentStep.Sore}，阶段'{currentStep.TestName}'，步骤采集{stepCollectNum}次，阶段采集{phaseCollectNum}次");
        //}

        ///// <summary>
        ///// 采集数据并设置转速扭矩
        ///// </summary>
        ///// <param name="Sore"></param>
        ///// <param name="Speed"></param>
        ///// <param name="Excitation"></param>
        ///// <param name="Power"></param>
        ///// <returns></returns>
        //public async Task CollectData(int Sore, double Speed, double Excitation, double Power)
        //{
        //    try
        //    {
        //        // 记录试验进程
        //        TestLog.UpdateTestPara($"{DateTime.Now}：序号:{Sore} 设定 速度:{Speed} 励磁电流:{Excitation} 目标功率");


        //    }
        //    catch (Exception ex)
        //    {
        //        TestLog.UpdateTestPara($"{DateTime.Now}：设置转速扭矩失败 - {ex.Message}");
        //    }
        //}
        #endregion


    }



}

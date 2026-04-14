using MainUI.Config;
using MainUI.Config.Test;
using MainUI.Global;
using System;
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
        /// <summary>
        /// 中间变量表
        /// </summary>
        TestSystemVariables TVar = new TestSystemVariables();

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
        private string CurrentTestType => hmi.testDataView.TestName == "性能试验" ? "100h" : "360h";

        /// <summary>
        /// 是否为360h试验
        /// </summary>
        private bool Is360hTest => CurrentTestType == "360h";

        /// <summary>
        /// 360小时具体步骤
        /// </summary>
        DurStepConfig durStepConfig { get; set; }

        /// <summary>
        /// 试验逻辑
        /// </summary>
        /// <returns></returns>
        public override TestStatusEnum Execute()
        {
            try
            {
                // 先手动设置限值值
                TVar.ExcitationDiffFast = 100;
                TVar.ExcitationDiffSlow = 50;
                TVar.ExcitationDiffMin = 3;

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
                while (hmi.CurrentStatusData.Sore <= allCount && IsTesting)
                {
                    if (Is360hTest)
                    {
                        // 360小时试验逻辑
                        var stepList360 = hmi.TestConfig360.DurabilityDatas;

                        if (hmi.CurrentStatusData.Sore > 0 && hmi.CurrentStatusData.Sore <= stepList360.Count)
                        {
                            // 循环代码
                            string node = stepList360[hmi.CurrentStatusData.Sore - 1].NodeName;
                            durStepConfig = new DurStepConfig(Common.mTestViewModel.ModelName, node);

                            // 总试验步骤
                            currentAllData = durStepConfig.testBasePara;

                            if (currentAllData != null && currentAllData.Count > 0)
                            {
                                // 试验阶段
                                currentStep = currentAllData.FirstOrDefault(d => d.Index == hmi.CurrentStatusData.PhaseIndex);
                                hmi.testDataView.TestSpeedPercent = currentStep.RPM;
                                hmi.testDataView.TestTorquePercent = currentStep.Torque;

                                if (currentStep != null)
                                {
                                    // 通过工况编号获取具体转速，励磁电流
                                    gkData = hmi.gkConfig.DurabilityDatas.FirstOrDefault(d => d.GKNo == currentStep.GKNo);
                                }
                            }
                        }
                    }
                    else
                    {
                        // 100小时试验逻辑
                        var stepList100 = hmi.TestConfig100.testStepLists;

                        if (hmi.CurrentStatusData.Sore > 0 && hmi.CurrentStatusData.Sore <= stepList100.Count)
                        {
                            // 总试验步骤
                            currentAllData = stepList100[hmi.CurrentStatusData.Sore - 1].testBasePara;

                            if (currentAllData != null && currentAllData.Count > 0)
                            {
                                // 试验阶段
                                currentStep = currentAllData.FirstOrDefault(d => d.Index == hmi.CurrentStatusData.PhaseIndex);
                                hmi.testDataView.TestSpeedPercent = currentStep.RPM;
                                hmi.testDataView.TestTorquePercent = currentStep.Torque;

                                if (currentStep != null)
                                {
                                    // 通过工况编号获取具体转速，励磁电流
                                    gkData = hmi.gkConfig.DurabilityDatas.FirstOrDefault(d => d.GKNo == currentStep.GKNo);
                                }
                            }
                        }
                    }

                    // 验证数据有效性
                    if (currentAllData == null || currentAllData.Count == 0)
                    {
                        TxtTips($"错误：未找到当前步骤的试验数据，步骤号：{hmi.CurrentStatusData.Sore}");
                        TestingStatus = TestStatusEnum.Error;
                        break;
                    }

                    if (currentStep == null)
                    {
                        TxtTips($"错误：未找到当前阶段的试验步骤，阶段索引：{hmi.CurrentStatusData.PhaseIndex}");
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
                    hmi.CurrentStatusData.PhaseIndex = currentStep.Index; // 序号
                    hmi.CurrentStatusData.PhaseName = currentStep.Index.ToString(); // 阶段
                    hmi.CurrentStatusData.CycleName = currentStep.CycleName == "NULL" ? "-" : currentStep.CycleName; // 周期
                    hmi.CurrentStatusData.NodeName = currentStep.TestName; // 循环代码

                    hmi.CurrentStatusData.AllPhaseIndex = currentAllData.Count; // 总序号
                    hmi.CurrentStatusData.GKBH = currentStep.GKNo; // 工况编号
                    hmi.CurrentStatusData.Speed = gkData.Speed; // 转速
                    hmi.CurrentStatusData.ExcitationCurrent = gkData.ExcitationCurrent; //励磁电流
                    hmi.CurrentStatusData.TargetPower = gkData.Power; // 功率
                    hmi.CurrentStatusData.TargetOperationTime = currentStep.RunTime; // 目标运行时间
                    hmi.CurrentStatusData.Save();

                    // 更新UI显示
                    hmi.UpdateLastStepData();

                    // 重启动步骤计时器
                    hmi.StopWatch();
                    hmi.BeginWatch();

                    CollectData(hmi.CurrentStatusData.Sore, hmi.CurrentStatusData.Speed, hmi.CurrentStatusData.ExcitationCurrent, hmi.CurrentStatusData.TargetPower);

                    // 计算当前步骤需要执行的时间  工况运行时间-已执行的时间
                    double waitTimeMinute = currentStep.RunTime;

                    // 试验逻辑（调转速/调励磁/试验时间）
                    while (IsTesting)
                    {
                        if (hmi.CurrentStatusData.StepTime >= waitTimeMinute)
                            break;

                        // 计算差值
                        CalculateDifferences();

                        //// 计时器的秒速(0-59)
                        //var second = hmi.testDataView.Second;
                        //// 差值2.5A

                        //TVar.TorqueDifference1

                        ExecuteState();

                        Thread.Sleep(1000);
                    }
                    if (!IsTesting)
                    {
                        // 测试被中止
                        TestingStatus = TestStatusEnum.Stop;
                        break;
                    }

                    // 停止步骤计时器
                    hmi.StopWatch();

                    TVar.Minute = 0;
                    hmi.CurrentStatusData.StepTime = 0;

                    // 移动到阶段（下一循环代码）
                    if (hmi.CurrentStatusData.PhaseIndex + 1 > hmi.CurrentStatusData.AllPhaseIndex)
                    {
                        lock (hmi.locked)
                        {
                            hmi.CurrentStatusData.Sore++;
                            hmi.CurrentStatusData.PhaseIndex = 1;
                            hmi.CurrentStatusData.Save();
                        }
                    }
                    else
                    {
                        lock (hmi.locked)
                        {
                            hmi.CurrentStatusData.PhaseIndex++;
                            hmi.CurrentStatusData.Save();
                        }
                    }
                }

                // 所有测试步骤完成
                if (hmi.CurrentStatusData.Sore > currentAllData.Count)
                {
                    // todo 所有试验状态回到第一步即可
                    hmi.CurrentStatusData.Sore = currentAllData.Count;
                    hmi.CurrentStatusData.TestStatus = true;
                    hmi.CurrentStatusData.StepTime = 0;

                    TestingStatus = TestStatusEnum.Success;

                    TestLog.UpdateTestPara($"{DateTime.Now}：测试完成，总共采集{hmi.testDataView.TotalCollectCount}次数据");
                    TxtTips("100h测试完成");
                }
                else
                {
                    TxtTips("测试中止");
                }

                hmi.testDataView.TestSpeedPercent = 0;
                hmi.testDataView.TestTorquePercent = 0;

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
        /// 励磁控制逻辑
        /// </summary>
        private void ExecuteState()
        {
            if (TestingStatus != TestStatusEnum.Pause)
            {
                TVar.Second++;

                // 励磁到位后开始计时（TVar.ExcitationDiffFast = 实际允许差值）
                if (TVar.ExcitationDifference <= TVar.ExcitationDiffMin || TVar.TSecond > 0)
                {
                    TVar.TSecond++;
                }

                // 转速和分段励磁逻辑
                HandleSpeedAndExcitationSegmentation();

                // 励磁微调修正逻辑 （TVar.TSecond 大于0后，通过功率进行微调，降弓不判断 TSecond）
                HandleExcitationCorrection();

                // 励磁输出控制
                HandleExcitationOutput();

                //// 下一个工况停车报警
                //CheckNextConditionAlert();

                // 时间累计
                if (TVar.Second >= 60)
                {
                    TVar.Second = 0;
                    TVar.Minute++;
                }
            }
            else
            {
                // 暂停

            }
        }

        /// <summary>
        /// 转速和分段励磁处理
        /// </summary>
        private void HandleSpeedAndExcitationSegmentation()
        {
            // 当目标功率比实际功率大

            double WVariable = 0;
            if (MiddleData.instnce.EnginePower < hmi.CurrentStatusData.TargetPower)
            {
                // 升功率
                if (TVar.ExcitationDifference >= TVar.ExcitationDiffFast)
                {
                    // 升功率分段给励磁（每5s，35%，20%，15%，15%，15%）
                    switch (TVar.Second)
                    {
                        case int n when (n >= 4 && n <= 9):
                            WVariable = TVar.ExcitationDifference * 0.35;
                            break;
                        case int n when (n >= 10 && n <= 14):
                            WVariable = TVar.ExcitationDifference * 0.20;
                            break;
                        case int n when (n >= 15 && n <= 19):
                            WVariable = TVar.ExcitationDifference * 0.15;
                            break;
                        case int n when (n >= 20 && n <= 24):
                            WVariable = TVar.ExcitationDifference * 0.15;
                            break;
                        case 25:
                            WVariable = TVar.ExcitationDifference * 0.15;
                            break;
                        default:
                            WVariable = 0;
                            break;
                    }
                    TVar.Variable = WVariable * 0.2;
                }
                else if (TVar.ExcitationDifference >= TVar.ExcitationDiffSlow)
                {
                    // 升功率分段给励磁（25%，25%，20%，15%，15%）
                    switch (TVar.Second)
                    {
                        case 4:
                            WVariable = TVar.ExcitationDifference * 0.25;
                            break;
                        case 7:
                            WVariable = TVar.ExcitationDifference * 0.25;
                            break;
                        case 10:
                            WVariable = TVar.ExcitationDifference * 0.20;
                            break;
                        case 13:
                            WVariable = TVar.ExcitationDifference * 0.15;
                            break;
                        case 16:
                            WVariable = TVar.ExcitationDifference * 0.15;
                            break;
                        default:
                            WVariable = 0;
                            break;
                    }
                    TVar.Variable = WVariable * 0.35;
                }
                else if (TVar.ExcitationDifference < TVar.ExcitationDiffSlow)
                {
                    // 升功率分段给励磁
                    switch (TVar.Second)
                    {
                        case 4:
                            WVariable = TVar.ExcitationDifference * 0.4;
                            break;
                        case 9:
                            WVariable = TVar.ExcitationDifference * 0.3;
                            break;
                        case 14:
                            WVariable = TVar.ExcitationDifference * 0.3;
                            break;
                        default:
                            WVariable = 0;
                            break;
                    }
                    TVar.Variable = WVariable * 0.2;
                }
            }
            else
            {
                // 降功率分段给励磁（每3s，30%，30%，40%）
                switch (TVar.Second)
                {
                    case int n when (n >= 0 && n <= 1):
                        TVar.Variable = TVar.ExcitationDifference * 0.3;
                        break;
                    case 2:
                        TVar.Variable = TVar.ExcitationDifference * 0.3;
                        break;
                    case 5:
                        TVar.Variable = TVar.ExcitationDifference * 0.4;
                        break;
                    default:
                        TVar.Variable = 0;
                        break;
                }
            }
        }

        /// <summary>
        /// 励磁输出控制
        /// </summary>
        private void HandleExcitationOutput()
        {
            // 目标励磁电流 > 实时设定励磁
            if (hmi.CurrentStatusData.ExcitationCurrent > Common.AOgrp["励磁调节"])
            {
                // 实时功率 < 目标功率
                if (MiddleData.instnce.EnginePower < hmi.CurrentStatusData.TargetPower)
                {
                    // 升功率
                    Common.AOgrp["发动机油门调节"] = hmi.CurrentStatusData.Speed;

                    if (hmi.CurrentStatusData.StepTime == 0 && TVar.Second >= 5 && TVar.Second <= 30 && TVar.ExcitationDifference >= TVar.ExcitationDiffFast)
                    {
                        Common.AOgrp["励磁调节"] = Math.Min(Common.AOgrp["励磁调节"] + TVar.Variable, hmi.CurrentStatusData.ExcitationCurrent);
                    }

                    if (TVar.Minute == 0 && TVar.Second >= 5 && TVar.Second < 25 && TVar.ExcitationDifference < TVar.ExcitationDiffFast)
                    {
                        Common.AOgrp["励磁调节"] = Math.Min(Common.AOgrp["励磁调节"] + TVar.Variable, hmi.CurrentStatusData.ExcitationCurrent);
                    }
                }
                else
                {
                    // 实时功率 > 目标功率

                    // 降功率
                    if ((TVar.Minute == 0 && TVar.Second % 3 == 0 && TVar.Second <= 10) ||
                        (TVar.Minute == 0 && TVar.Second == 1))
                    {
                        Common.AOgrp["励磁调节"] = Math.Min(Common.AOgrp["励磁调节"] + TVar.Variable, hmi.CurrentStatusData.ExcitationCurrent);
                    }

                    if (TVar.Minute == 0 && TVar.Second % 3 == 0 && TVar.Second > 0 && TVar.Second < 27)
                    {
                        Common.AOgrp["发动机转速"] = hmi.CurrentStatusData.Speed;
                    }
                }
            }
            else
            {
                // 目标励磁电流 < 实时设定励磁

                // 实时功率 < 目标功率
                if (MiddleData.instnce.EnginePower < hmi.CurrentStatusData.TargetPower)
                {
                    // 升功率
                    Common.AOgrp["发动机转速"] = hmi.CurrentStatusData.Speed;

                    if (TVar.Minute == 0 && TVar.Second >= 5 && TVar.Second <= 30 && TVar.ExcitationDifference >= TVar.ExcitationDiffFast)
                    {
                        Common.AOgrp["励磁调节"] = Math.Max(Common.AOgrp["励磁调节"] - TVar.Variable, hmi.CurrentStatusData.ExcitationCurrent);
                    }

                    if (TVar.Minute == 0 && TVar.Second >= 5 && TVar.Second < 25 && TVar.ExcitationDifference < TVar.ExcitationDiffFast)
                    {
                        Common.AOgrp["励磁调节"] = Math.Min(Common.AOgrp["励磁调节"] + TVar.Variable, hmi.CurrentStatusData.ExcitationCurrent);
                    }
                }
                else
                {
                    // 降功率
                    if ((TVar.Minute == 0 && TVar.Second % 3 == 0 && TVar.Second <= 10) ||
                        (TVar.Minute == 0 && TVar.Second == 1))
                    {
                        Common.AOgrp["励磁调节"] = Math.Max(Common.AOgrp["励磁调节"] - TVar.Variable, hmi.CurrentStatusData.ExcitationCurrent);
                    }

                    if (TVar.Minute == 0 && TVar.Second % 3 == 0 && TVar.Second > 0 && TVar.Second < 27)
                    {
                        Common.AOgrp["发动机转速"] = hmi.CurrentStatusData.Speed;
                    }
                }
            }
        }

        /// <summary>
        /// 励磁微调修正处理
        /// </summary>
        private void HandleExcitationCorrection()
        {
            if (Common.AOgrp["励磁调节"] > hmi.CurrentStatusData.TargetPower * 0.005 && hmi.CurrentStatusData.TargetPower > 0)
            {
                double power = MiddleData.instnce.EnginePower;

                // 根据功率范围和功率差计算修正值
                TVar.XVariable = CalculateCorrectionValue(power, TVar.PowerDifference);

                // 目标功率 > 当前功率
                if (hmi.CurrentStatusData.TargetPower > power)
                {
                    // 升功率 （前提调节 励磁基本到位）

                    // 功率在2500kW 以上，励磁稳定5秒后，每隔 8 秒进行一次调节
                    if ((TVar.TSecond >= 5 && (TVar.TSecond - 5) % 8 == 0) || TVar.TSecond == 5)
                    {
                        if (Common.AOgrp["励磁调节"] <= hmi.CurrentStatusData.ExcitationCurrent * 1.03 && hmi.CurrentStatusData.TargetPower >= 2500)
                        {
                            // 微调励磁
                            if (Common.AOgrp["励磁调节"] + TVar.XVariable > hmi.CurrentStatusData.ExcitationCurrent)
                            {
                                Common.AOgrp["励磁调节"] = hmi.CurrentStatusData.ExcitationCurrent;
                            }
                            else
                            {
                                Common.AOgrp["励磁调节"] = Common.AOgrp["励磁调节"] + TVar.XVariable;
                            }
                        }
                    }

                    // 功率在2500kW 以下，励磁稳定5秒后，每隔 6 秒进行一次调节
                    if ((TVar.TSecond >= 5 && (TVar.TSecond - 5) % 6 == 0) || TVar.TSecond == 5)
                    {
                        if (Common.AOgrp["励磁调节"] <= hmi.CurrentStatusData.ExcitationCurrent * 1.05 && hmi.CurrentStatusData.TargetPower < 2500)
                        {
                            if (Common.AOgrp["励磁调节"] + TVar.XVariable > hmi.CurrentStatusData.ExcitationCurrent)
                            {
                                Common.AOgrp["励磁调节"] = hmi.CurrentStatusData.ExcitationCurrent;
                            }
                            else
                            {
                                Common.AOgrp["励磁调节"] = Common.AOgrp["励磁调节"] + TVar.XVariable;
                            }
                        }
                    }
                }
                else
                {
                    // 降功率
                    if ((TVar.TSecond % 2 == 0 && TVar.TSecond > 10) || TVar.TSecond == 10)
                    {
                        Common.AOgrp["励磁调节"] = Common.AOgrp["励磁调节"] - TVar.XVariable;
                    }
                }
            }
        }

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
            TVar.PowerDifference = Math.Abs(MiddleData.instnce.EnginePower - hmi.CurrentStatusData.TargetPower);

            // 计算目标励磁和当前励磁差值
            TVar.ExcitationDifference = Math.Abs(Common.AOgrp["励磁调节"] - hmi.CurrentStatusData.ExcitationCurrent);

            // 试验总小时数
            MiddleData.instnce.RunHour = (int) hmi.CurrentStatusData.StepTimeTotal / 60;
        }

        #region 暂时注释
        /// <summary>
        /// 计算步骤剩余时间（基于时间区间的新方法）
        /// </summary>
        /// <param name="resumeStep">恢复的步骤</param>
        /// <param name="phaseBeginSore">阶段开始序号</param>
        /// <param name="phaseCollectNum">阶段内采集次数</param>
        /// <param name="phaseCollectTime">采集间隔时间</param>
        /// <returns>步骤剩余时间（分钟）</returns>
        private double CalculateRemainingStepTimeByTimeRange(AutoTestStep resumeStep, int phaseBeginSore, int phaseCollectNum, double phaseCollectTime)
        {
            if (resumeStep == null) return 0;

            // 验证参数
            if (!ValidateStepIndex(resumeStep.Sore) || !ValidateStepIndex(phaseBeginSore))
            {
                return resumeStep.RunTime;
            }

            // 获取阶段内所有步骤的时间区间
            var timeRanges = GetPhaseTimeRanges(phaseBeginSore);

            // 计算已过去的时间
            double elapsedTime = phaseCollectNum * phaseCollectTime;

            // 找到当前步骤的时间区间
            var currentRange = timeRanges.FirstOrDefault(r => r.Sore == resumeStep.Sore);
            if (currentRange == null)
            {
                TxtTips($"找不到步骤{resumeStep.Sore}的时间区间");
                return resumeStep.RunTime;
            }

            // 计算步骤剩余时间
            double remainingTime;

            // 未采集到和不在采集区间的方法
            if (elapsedTime < currentRange.StartTime)
            {
                // 情况1：已过去时间小于当前步骤开始时间，间隔时间应该要减去已经运行的时间
                var diffTime = currentRange.StartTime - elapsedTime;

                // 间隔时间
                collectIntervalTime = resumeStep.CollectIntervalTime - diffTime;
                remainingTime = resumeStep.RunTime;
            }
            else if (elapsedTime >= currentRange.StartTime && elapsedTime < currentRange.EndTime)
            {
                // 情况2：已过去时间在当前步骤的时间区间内
                remainingTime = currentRange.EndTime - elapsedTime;
                // 间隔时间
                collectIntervalTime = resumeStep.CollectIntervalTime;

                TxtTips($"步骤{resumeStep.Sore}：已过去{elapsedTime}分钟，剩余{remainingTime:F1}分钟");
            }
            else
            {
                // 情况3：已过去时间超过当前步骤结束时间
                // 这种情况不应该发生，因为我们应该已经记录了下一步
                TxtTips($"时间计算异常：已过去时间{elapsedTime}超过步骤{resumeStep.Sore}结束时间{currentRange.EndTime}");
                remainingTime = 0;
            }

            // 确保剩余时间在合理范围内
            remainingTime = Math.Max(0, Math.Min(resumeStep.RunTime, remainingTime));

            return remainingTime;
        }

        /// <summary>
        /// 获取阶段内所有步骤的时间区间
        /// </summary>
        /// <param name="phaseBeginSore">阶段开始序号</param>
        /// <returns>步骤时间区间列表</returns>
        private List<StepTimeRange> GetPhaseTimeRanges(int phaseBeginSore)
        {
            var timeRanges = new List<StepTimeRange>();

            if (hmi.AutoTestStepList == null || hmi.AutoTestStepList.Count == 0)
                return timeRanges;

            // 找到阶段内的所有步骤
            var phaseSteps = hmi.AutoTestStepList
                .Where(s => s.Sore >= phaseBeginSore && s.TestName == hmi.AutoTestStepList[phaseBeginSore - 1].TestName)
                .OrderBy(s => s.Sore)
                .ToList();

            if (phaseSteps.Count == 0)
                return timeRanges;

            double currentTime = 0;

            foreach (var step in phaseSteps)
            {
                var timeRange = new StepTimeRange
                {
                    Sore = step.Sore,
                    StepName = step.TestName,
                    StartTime = currentTime,
                    EndTime = currentTime + step.RunTime,
                    RunTime = step.RunTime
                };

                timeRanges.Add(timeRange);
                currentTime += step.RunTime;

                TxtTips($"步骤{step.Sore}: [{timeRange.StartTime}-{timeRange.EndTime})分钟");
            }

            return timeRanges;
        }

        /// <summary>
        /// 为采集调整步骤时间（基于时间区间的新方法）
        /// </summary>
        private double CalculateAdjustedStepTimeForCollectionByTimeRange(AutoTestStep currentStep, int phaseBeginSore, int phaseCollectNum)
        {
            // 获取阶段内所有步骤的时间区间
            var timeRanges = GetPhaseTimeRanges(phaseBeginSore);

            // 找到当前步骤的时间区间
            var currentRange = timeRanges.FirstOrDefault(r => r.Sore == currentStep.Sore);
            if (currentRange == null)
            {
                TxtTips($"找不到步骤{currentStep.Sore}的时间区间");
                return currentStep.RunTime;
            }

            // 计算下一次采集的时间点
            double nextCollectTime = (phaseCollectNum + 1) * currentStep.CollectIntervalTime;

            // 检查下一次采集是否在当前步骤的时间区间内
            if (nextCollectTime > currentRange.StartTime && nextCollectTime <= currentRange.EndTime)
            {
                // 调整步骤时间为到采集点的时间
                double adjustedTime = nextCollectTime - currentRange.StartTime;
                TxtTips($"正常执行：步骤{currentStep.Sore}调整为{adjustedTime:F1}分钟以匹配采集时间点");
                return adjustedTime;
            }

            // 否则正常执行完整步骤
            TxtTips($"正常执行：步骤{currentStep.Sore}完整执行{currentStep.RunTime:F1}分钟");
            return currentStep.RunTime;
        }

        /// <summary>
        /// 计算从阶段开始到指定步骤开始的总时间（不包括指定步骤本身）
        /// </summary>
        /// <param name="phaseBeginSore">阶段开始序号</param>
        /// <param name="targetSore">目标步骤序号</param>
        /// <returns>从阶段开始到目标步骤开始的总时间（分钟）</returns>
        private double CalculateTimeToStep(int phaseBeginSore, int targetSore)
        {
            // 参数验证
            if (phaseBeginSore <= 0 || targetSore <= 0)
            {
                TxtTips($"参数错误：phaseBeginSore={phaseBeginSore}, targetSore={targetSore}");
                return 0;
            }

            if (phaseBeginSore > targetSore)
            {
                TxtTips($"阶段开始序号{phaseBeginSore}不能大于目标步骤序号{targetSore}");
                return 0;
            }

            if (hmi.AutoTestStepList == null || hmi.AutoTestStepList.Count == 0)
            {
                TxtTips("步骤列表为空");
                return 0;
            }

            // 如果阶段开始序号等于目标步骤序号，则时间为0
            if (phaseBeginSore == targetSore)
            {
                return 0;
            }

            double totalTime = 0;

            try
            {
                // 遍历从阶段开始到目标步骤之前的所有步骤
                for (int i = phaseBeginSore - 1; i < targetSore - 1; i++)
                {
                    if (i < 0 || i >= hmi.AutoTestStepList.Count)
                    {
                        TxtTips($"步骤索引{i}超出范围，列表长度={hmi.AutoTestStepList.Count}");
                        break;
                    }

                    var step = hmi.AutoTestStepList[i];
                    totalTime += step.RunTime;
                }

                TxtTips($"从阶段开始步骤{phaseBeginSore}到目标步骤{targetSore}开始的总时间：{totalTime}分钟");
            }
            catch (Exception ex)
            {
                TxtTips($"计算时间出错：{ex.Message}");
                return 0;
            }

            return totalTime;
        }

        /// <summary>
        /// 计算步骤内采集次数
        /// </summary>
        private int CalculateStepCollectNum(AutoTestStep currentStep, int phaseCollectNum)
        {
            if (currentStep == null) return 1;

            // 获取当前阶段的所有步骤
            var stageSteps = hmi.AutoTestStepList
                .Where(s => s.TestName == currentStep.TestName)
                .OrderBy(s => s.Sore)
                .ToList();

            if (stageSteps.Count == 0) return 1;

            // 找到当前步骤在阶段中的索引
            int stepIndexInStage = stageSteps.FindIndex(s => s.Sore == currentStep.Sore);
            if (stepIndexInStage < 0) return 1;

            // 计算阶段开始到当前步骤开始的总时间
            double timeToStepStart = 0;
            for (int i = 0; i < stepIndexInStage; i++)
            {
                timeToStepStart += stageSteps[i].RunTime;
            }

            // 计算当前步骤内已运行时间
            double elapsedTimeInStep = (phaseCollectNum * currentStep.CollectIntervalTime) - timeToStepStart;

            // 确保不会小于0
            elapsedTimeInStep = Math.Max(0, elapsedTimeInStep);

            // 计算步骤内采集次数
            int stepCollectNum = (int)Math.Floor(elapsedTimeInStep / currentStep.CollectIntervalTime) + 1;

            return Math.Max(1, stepCollectNum);
        }

        /// <summary>
        /// 验证步骤序号是否有效
        /// </summary>
        private bool ValidateStepIndex(int sore)
        {
            if (hmi.AutoTestStepList == null || hmi.AutoTestStepList.Count == 0)
            {
                TxtTips("步骤列表为空");
                return false;
            }

            if (sore <= 0 || sore > hmi.AutoTestStepList.Count)
            {
                TxtTips($"步骤序号{sore}超出有效范围[1, {hmi.AutoTestStepList.Count}]");
                return false;
            }

            var step = hmi.AutoTestStepList.FirstOrDefault(s => s.Sore == sore);
            if (step == null)
            {
                TxtTips($"找不到步骤{sore}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 保存当前试验状态
        /// </summary>
        private void SaveCurrentStatus(AutoTestStep currentStep, int phaseCollectNum, bool stepCompleted = false)
        {
            if (currentStep == null) return;

            // 计算步骤内采集次数
            int stepCollectNum = CalculateStepCollectNum(currentStep, phaseCollectNum);

            hmi.CurrentStatusData.TestStatus = false;
            hmi.CurrentStatusData.Sore = stepCompleted ? currentStep.Sore + 1 : currentStep.Sore;
            hmi.CurrentStatusData.PhaseIndex = currentStep.StepIndex;
            hmi.CurrentStatusData.PhaseName = currentStep.TestName;
            hmi.CurrentStatusData.CycleName = currentStep.CycleName;
            hmi.CurrentStatusData.NodeName = currentStep.StepName;
            hmi.CurrentStatusData.PhaseCollectNum = phaseCollectNum;
            hmi.CurrentStatusData.PhaseCollectTime = currentStep.CollectIntervalTime;
            hmi.CurrentStatusData.Save();

            TestLog.UpdateTestPara($"{DateTime.Now}：保存状态 - 步骤{currentStep.Sore}，阶段'{currentStep.TestName}'，步骤采集{stepCollectNum}次，阶段采集{phaseCollectNum}次");
        }

        /// <summary>
        /// 采集数据并设置转速扭矩
        /// </summary>
        /// <param name="Sore"></param>
        /// <param name="Speed"></param>
        /// <param name="Excitation"></param>
        /// <param name="Power"></param>
        /// <returns></returns>
        public async Task CollectData(int Sore, double Speed, double Excitation, double Power)
        {
            try
            {
                // 记录试验进程
                TestLog.UpdateTestPara($"{DateTime.Now}：序号:{Sore} 设定 速度:{Speed} 励磁电流:{Excitation} 目标功率");


            }
            catch (Exception ex)
            {
                TestLog.UpdateTestPara($"{DateTime.Now}：设置转速扭矩失败 - {ex.Message}");
            }
        }
        #endregion


    }

    /// <summary>
    /// 试验中间状态
    /// </summary>
    public class TestSystemVariables
    {
        /// <summary>
        /// 励磁差值快速调节的限值
        /// 目标励磁与设置励磁差值超过此值 升励磁速率快
        /// </summary>
        public double ExcitationDiffFast { get; set; }

        /// <summary>
        /// 励磁差值慢速调节的限值
        /// 目标励磁与设置励磁差值超过此值 升励磁速率慢
        /// </summary>
        public double ExcitationDiffSlow { get; set; }

        /// <summary>
        /// 励磁差稳定后的限值
        /// 目标励磁与设置励磁差值
        /// </summary>
        public double ExcitationDiffMin { get; set; }

        /// <summary>
        /// 每次励磁增加值
        /// 根据TorqueDifference 计算出
        /// 当前时间段（X秒-Y秒）应增加的励磁量
        /// </summary>
        public double Variable { get; set; }

        /// <summary>
        /// 扭矩到达后的计时器（单位：秒）
        /// 从励磁到位开始计时
        /// 用于励磁修正的时间判断
        /// </summary>
        public int TSecond { get; set; }

        /// <summary>
        /// 计时器计时（单位：秒）
        /// 从工况开始计时（工况结束清0）
        /// </summary>
        public int Second { get; set; }

        /// <summary>
        /// 计时器计时（单位：分）
        /// 从工况开始计时（工况结束清0）
        /// </summary>
        public int Minute { get; set; }

        /// <summary>
        /// 功率差值（单位：kW）
        /// 实际功率与目标功率的绝对差值
        /// 用于励磁修正计算
        /// </summary>
        public double PowerDifference { get; set; }

        /// <summary>
        /// 目标励磁与 实际设置励磁的差值
        /// </summary>
        public double ExcitationDifference { get; set; }

        /// <summary>
        /// 励磁修正值
        /// 根据功率差计算出的励磁修正量
        /// </summary>
        public double XVariable { get; set; }

    }

}

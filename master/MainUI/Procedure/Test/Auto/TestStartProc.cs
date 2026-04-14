using MainUI.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainUI.Procedure.Test.Performance
{
    public class TestStartProc : BaseTest
    {
        /// <summary>
        /// 采集间隔时间
        /// </summary>
        double collectIntervalTime { get; set; } = 0;


        public override TestStatusEnum Execute()
        {
            try
            {
                TestingStatus = TestStatusEnum.IsTest;

                // 重新开始测试时，初始化试验名称
                hmi.testDataView.StepName = "";

                TestStatus(true);
                TxtTips("测试开始");

                // 检查是否需要从断点恢复
                bool isResume = hmi.CurrentStatusData != null &&
                               !hmi.CurrentStatusData.TestStatus &&
                               hmi.CurrentStatusData.Sore > 0;

                // 重置当前步骤索引
                int currentStepIndex = 1;
                int resumeStepIndex = 0;
                int resumeStepCollectNum = 0; // 步骤内采集次数
                int resumePhaseCollectNum = 0; // 阶段内采集次数
                double remainingStepTime = 0; // 步骤剩余执行时间
                string currentPhaseName = "";
                int currentPhaseBeginSore = 1;

                // 如果是恢复模式，从断点开始
                if (isResume)
                {
                    resumeStepIndex = hmi.CurrentStatusData.Sore;
                    resumePhaseCollectNum = hmi.CurrentStatusData.PhaseCollectNum;
                    //currentPhaseBeginSore = hmi.CurrentStatusData.PhaseBeginSore;

                    // 找到断点步骤在列表中的索引
                    var resumeStep = hmi.AutoTestStepList.FirstOrDefault(s => s.Sore == resumeStepIndex);
                    if (resumeStep != null)
                    {
                        currentStepIndex = hmi.AutoTestStepList.IndexOf(resumeStep) + 1;
                        currentPhaseName = resumeStep.TestName;

                        // 计算步骤剩余执行时间
                        remainingStepTime = CalculateRemainingStepTimeByTimeRange(
                            resumeStep,
                            currentPhaseBeginSore,
                            resumePhaseCollectNum,
                            hmi.CurrentStatusData.PhaseCollectTime);

                        // 设置恢复的采集状态
                        hmi.testDataView.StepNameCollectIndex = resumePhaseCollectNum;

                        TxtTips($"从断点恢复: 步骤{resumeStepIndex}({resumeStep.TestName})，阶段开始序号{currentPhaseBeginSore}，步骤内采集{resumeStepCollectNum}次，阶段内采集{resumePhaseCollectNum}次，步骤剩余时间{remainingStepTime:F1}分钟");
                    }
                }

                // 启动自动测试循环
                while (currentStepIndex <= hmi.AutoTestStepList.Count && hmi.testDataView.IsTest)
                {
                    var currentStep = hmi.AutoTestStepList[currentStepIndex - 1];

                    // 检查阶段是否变化
                    if (currentStep.TestName != currentPhaseName)
                    {
                        currentPhaseName = currentStep.TestName;
                        currentPhaseBeginSore = currentStep.Sore;

                        // 新阶段开始，重置阶段采集计数
                        hmi.testDataView.StepNameCollectIndex = 0;

                        // 保存阶段开始序号
                        //hmi.CurrentStatusData.PhaseBeginSore = currentPhaseBeginSore;
                        hmi.CurrentStatusData.PhaseCollectNum = 0;
                        hmi.CurrentStatusData.Save();

                        TxtTips($"开始新阶段: {currentPhaseName}，阶段开始序号: {currentPhaseBeginSore}");
                    }

                    // 为界面更新数据
                    hmi.testDataView.Sore = currentStep.Sore;
                    hmi.testDataView.StepIndex = currentStep.StepIndex;
                    hmi.testDataView.StepName = currentStep.TestName;
                    hmi.testDataView.CollectTime = collectIntervalTime;// currentStep.CollectIntervalTime;

                    // 处理恢复模式的步骤剩余时间调整
                    if (isResume && currentStep.Sore == resumeStepIndex)
                    {
                        // 使用调整后的步骤时间（剩余时间）
                        hmi.testDataView.StepTotalSeconds = (int)(remainingStepTime * 60);
                        hmi.testDataView.StepRemainingSeconds = (int)(remainingStepTime * 60);

                        isResume = false; // 只恢复一次
                    }
                    else
                    {
                        // 正常情况：检查是否需要为采集调整步骤时间
                        double adjustedStepTime = CalculateAdjustedStepTimeForCollectionByTimeRange(
                            currentStep, currentPhaseBeginSore, hmi.testDataView.StepNameCollectIndex);

                        hmi.testDataView.StepTotalSeconds = (int)(adjustedStepTime * 60);
                        hmi.testDataView.StepRemainingSeconds = (int)(adjustedStepTime * 60);
                    }

                    // 开始当前步骤
                    currentStep.TestStatus = TestStatusEnum.IsTest;
                    currentStep.StarDateTime = DateTime.Now;

                    // 更新UI显示
                    hmi.UpdateDataGridView(currentStep);

                    //// 重启动步骤计时器
                    //hmi.StopWatchStep();
                    //hmi.BeginWatchStep();

                    // 异步下发转速扭矩（受 testData.IsTest 影响）
                    CollectData(currentStepIndex, currentStep.RPM, currentStep.Torque);

                    // 计算当前步骤需要等待的时间
                    double waitTimeMinutes = hmi.testDataView.StepTotalSeconds / 60.0;

                    // 等待当前步骤完成
                    var stepCompleted = Delay((int)(waitTimeMinutes * 60));

                    if (!stepCompleted)
                    {
                        // 测试被中止
                        currentStep.TestStatus = TestStatusEnum.Stop;
                        currentStep.EndDateTime = DateTime.Now;

                        // 保存当前状态以便恢复
                        SaveCurrentStatus(currentStep, hmi.testDataView.StepNameCollectIndex);

                        // 更新数据表格显示
                        hmi.UpdateDataGridView(currentStep);
                        break;
                    }

                    // 步骤完成
                    currentStep.TestStatus = TestStatusEnum.Success;
                    currentStep.EndDateTime = DateTime.Now;

                    //// 停止步骤计时器
                    //hmi.StopWatchStep();

                    // 更新数据表格显示
                    hmi.UpdateDataGridView(currentStep);

                    // 保存完成状态
                    SaveCurrentStatus(currentStep, hmi.testDataView.StepNameCollectIndex, true);

                    // 移动到下一步
                    currentStepIndex++;
                }

                // 所有测试步骤完成
                if (currentStepIndex > hmi.AutoTestStepList.Count && hmi.testDataView.IsTest)
                {
                    TestingStatus = TestStatusEnum.Success;

                    // 完成状态保存到ini
                    SaveCompletionStatus();

                    TestLog.UpdateTestPara($"{DateTime.Now}：测试完成，总共采集{hmi.testDataView.TotalCollectCount}次数据");
                    TxtTips("测试完成");
                }
                else
                {
                    TxtTips("测试中止");
                }

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
        /// 保存完成状态
        /// </summary>
        private void SaveCompletionStatus()
        {
            hmi.CurrentStatusData.TestStatus = true;
            hmi.CurrentStatusData.Sore = 0;
            hmi.CurrentStatusData.PhaseIndex = 0;
            //hmi.CurrentStatusData.PhaseBeginSore = 0;
            hmi.CurrentStatusData.PhaseName = "";
            hmi.CurrentStatusData.CycleName = "";
            hmi.CurrentStatusData.NodeName = "";
            hmi.CurrentStatusData.PhaseCollectNum = 0;
            hmi.CurrentStatusData.PhaseCollectTime = 0;
            hmi.CurrentStatusData.Save();
        }

        /// <summary>
        /// 采集数据并设置转速扭矩
        /// </summary>
        public async Task CollectData(int Sore, double Speed, double Torque)
        {
            try
            {
                // 记录试验进程
                TestLog.UpdateTestPara($"{DateTime.Now}：序号:{Sore} 速度:{Speed} 扭矩:{Torque}");

                // 设置转速扭矩
                await SetSpeedTorque(Speed, Torque);
            }
            catch (Exception ex)
            {
                TestLog.UpdateTestPara($"{DateTime.Now}：设置转速扭矩失败 - {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 设置转速扭矩
        /// </summary>
        public async Task SetSpeedTorque(double Speed, double Torque)
        {
            try
            {
                // TODO: 实现实际的转速扭矩设置逻辑

                // Common.SomeControlInterface.SetRPM(Speed);
                // Common.SomeControlInterface.SetTorque(Torque);

                await Task.Delay(100); // 模拟设置时间

                TxtTips($"设置转速: {Speed:F1} RPM, 扭矩: {Torque:F1} N·m");
            }
            catch (Exception ex)
            {
                TxtTips($"设置转速扭矩异常: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 设置转速扭矩（无参数版本，保持兼容性）
        /// </summary>
        public Task SetSpeedTorque()
        {
            return Task.Delay(1);
        }
    }

    /// <summary>
    /// 步骤时间区间类
    /// </summary>
    public class StepTimeRange
    {
        /// <summary>
        /// 步骤序号
        /// </summary>
        public int Sore { get; set; }

        /// <summary>
        /// 步骤名称
        /// </summary>
        public string StepName { get; set; }

        /// <summary>
        /// 开始时间（从阶段开始算起）
        /// </summary>
        public double StartTime { get; set; }

        /// <summary>
        /// 结束时间（从阶段开始算起）
        /// </summary>
        public double EndTime { get; set; }

        /// <summary>
        /// 步骤运行时间
        /// </summary>
        public double RunTime { get; set; }
    }
}

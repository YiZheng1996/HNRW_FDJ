using MainUI.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainUI.Procedure.Test.Performance
{
    public class TestStartDur : BaseTest
    {
        public override TestStatusEnum Execute()
        {
            try
            {
                TestingStatus = TestStatusEnum.IsTest;
                // 重新开始测试时，初始化试验名称
                hmi.testDataView.StepName = "";

                TestStatus(true);
                TxtTips("测试开始");

                // 重置当前步骤索引
                int currentStepIndex = 1;

                // 启动自动测试循环
                while (currentStepIndex <= hmi.AutoTestStepList.Count && hmi.testDataView.IsTest)
                {
                    
                    // 为界面
                    var currentStep = hmi.AutoTestStepList[currentStepIndex - 1];
                    hmi.testDataView.Sore = currentStep.Sore;
                    hmi.testDataView.CollectTime = currentStep.CollectIntervalTime;
                    hmi.testDataView.StepTotalSeconds = currentStep.TotalSeconds; //步骤总时间
                    hmi.testDataView.StepRemainingSeconds = currentStep.TotalSeconds; //步骤剩余时间

                    // 每次更新名称时，更新阶段记录总数index
                    if (hmi.testDataView.StepName != currentStep.TestName) 
                    {
                        hmi.testDataView.StepNameCollectIndex = 1;
                        hmi.OnCollectIntervalChanged();
                    }
                    hmi.testDataView.StepName = currentStep.TestName;

                    // 开始当前步骤
                    currentStep.TestStatus = TestStatusEnum.IsTest;
                    currentStep.StarDateTime = DateTime.Now;

                    // 更新UI显示
                    hmi.UpdateDataGridView(currentStep);

                    // 重启动步骤计时器
                    //hmi.StopWatchStep();
                    //hmi.BeginWatchStep();

                    // 异步下发转速扭矩（受 testData.IsTest 影响）
                    CollectData(currentStepIndex, currentStep.RPM, currentStep.Torque);

                    // 等待当前步骤完成
                    var allsecond = currentStep.RunTime * 60;
                    var stepCompleted = Delay(allsecond);
                    if (!stepCompleted)
                    {
                        // 测试被中止
                        currentStep.TestStatus = TestStatusEnum.Stop;
                        currentStep.EndDateTime = DateTime.Now;

                        // 步骤异常
                        // 结束时更新数据表格显示
                        hmi.UpdateDataGridView(currentStep);
                        break;
                    }

                    // 步骤完成
                    currentStep.TestStatus = TestStatusEnum.Success;
                    currentStep.EndDateTime = DateTime.Now;

                    // 停止步骤计时器
                    //hmi.StopWatchStep();

                    // 结束时更新数据表格显示
                    hmi.UpdateDataGridView(currentStep);

                    // 移动到下一步
                    currentStepIndex++;
                }

                // 所有测试步骤完成才算合格
                if (currentStepIndex >= hmi.AutoTestStepList.Count)
                {
                    TestingStatus = TestStatusEnum.Success;
                    TestLog.UpdateTestPara($"{DateTime.Now}：测试完成，总共采集{hmi.testDataView.TotalCollectCount}次数据");
                }

                TestStatus(false);
                TxtTips("测试结束");
                return TestingStatus;
            }
            catch (Exception ex)
            {
                return TestStatusEnum.Error;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sore"></param>
        /// <param name="Speed"></param>
        /// <param name="Torque"></param>
        /// <returns></returns>
        public async Task CollectData(int Sore, double Speed, double Torque)
        {
            // 测试试验进程
            TestLog.UpdateTestPara($"{DateTime.Now} ：序号:{Sore} 速度:{Speed} 扭矩:{Torque}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task SetSpeedTorque()
        {
            return Task.Delay(1);
        }
    }
}

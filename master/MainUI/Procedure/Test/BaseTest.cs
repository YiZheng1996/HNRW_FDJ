using MainUI.Config;
using MainUI.Config.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainUI.Procedure
{
    public class BaseTest
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern uint GetTickCount();
        public static ParaConfig para = new ParaConfig();

        public delegate void TestStateHandler(bool isTesting);
        public static event TestStateHandler TestStateChanged;

        public delegate void TipsHandler(object sender, object info);
        public static event TipsHandler TipsChanged;

        public delegate void TipsTiming(object sender, int s);
        public static event TipsTiming TimingChanged;

        public delegate bool WaitCallback();

        public delegate void WaitTicked(int tick);
        public event WaitTicked WaitTick;

        public static event WaitStepTicked WaitStepTick;
        public delegate void WaitStepTicked(int tick, string stepName);

        public delegate void Del();

        // 360界面
        public ucAutoHMI hmi = null;

        /// <summary>
        /// 中间变量表
        /// </summary>
        public TestSystemVariables TVar = new TestSystemVariables();

        /// <summary>
        /// 测试进行中（为false时自动退出测试）
        /// </summary>
        public bool IsTesting { get; set; }

        //private bool Delay(double sj, int interval, WaitCallback wait)
        //{
        //    int t = 0;
        //    double timeout = sj;
        //    var second = hmi.testDataView.StepRemainingSeconds;
        //    // int timeout = sj * 100; // 模拟测试，暂时把延时时间改短。
        //    while (second > 0 && IsTesting && wait())
        //    {
        //        second = hmi.testDataView.StepRemainingSeconds;
        //        //t += interval;
        //        Thread.Sleep(500);
        //        if (WaitTick != null) WaitTick(t);
        //    }
        //    return second == 0;
        //}

        /// <summary>
        /// 等待时间
        /// </summary>
        /// <param name="seconds">等待秒数</param>
        /// <param name="waitName">等待名称</param>
        public void Delay(int seconds, string waitName)
        {
            Delay(seconds, 100, delegate { return true; }, waitName);
        }

        public bool Delay(int sj, int interval, WaitCallback wait, string waitName)
        {
            int t = 0;
            int timeout = sj * 1000;
            // int timeout = sj * 100; // 模拟测试，暂时把延时时间改短。
            while (t <= timeout && wait() && IsTesting)
            {
                t += interval;
                Thread.Sleep(interval);
                if (WaitStepTick != null) WaitStepTick(t, waitName);
            }
            return t > timeout;
        }
        /// <summary>
        /// 显示已执行的时间
        /// </summary>
        public void lblTime(int time, string waitName)
        {
            if (WaitStepTick != null) WaitStepTick(time, waitName);
        }
        /// <summary>
        /// 提示信息
        /// </summary>
        public void TxtTips(object str)
        {
            if (TipsChanged != null)
                TipsChanged(this, str);
        }
        public void TxtTiming(int s)
        {
            if (TimingChanged != null)
            {
                TimingChanged(this, s);
            }
        }
        /// <summary>
        /// 试验状态
        /// </summary>
        public void TestStatus(bool isTest)
        {
            IsTesting = isTest;
            if (TestStateChanged != null)
            {
                TestStateChanged(isTest);
            }
        }

        /// <summary>
        /// 在子类中执行试验过程
        /// </summary>
        public virtual TestStatusEnum Execute() { return TestingStatus; }
        public virtual bool Execute(string model, string durTest) { return false; }
        /// <summary>
        /// 试验项点初始化
        /// </summary>
        public virtual void Init() { }

  
        public string rn = Environment.NewLine;


        /// <summary>
        /// 测试是否合格
        /// </summary>
        public TestStatusEnum TestingStatus { get; set; } = TestStatusEnum.NotStarted;

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
        /// 上一次励磁调节的范围(0：未开始调节 1: 快速（与目标值数值差较大） 2：中速  3：快速（与目标值数值差较小）  4:降励磁)
        /// </summary>
        public int LastAdjustVal { get; set; }

        /// <summary>
        /// 阶段内励磁调节时间
        /// 励磁调节计时（切换 励磁调节范围时清0）
        /// </summary>
        public int ExAdjustSecond { get; set; }

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

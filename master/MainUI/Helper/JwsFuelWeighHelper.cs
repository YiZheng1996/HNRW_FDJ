using System;

namespace MainUI.Helper
{
    /// <summary>
    /// 称重油耗：每秒采样，用最近最多60秒滑动窗口实时算 kg/h。
    /// 油耗侧：油耗 = 流量差 - 重量变化率(kg/h)；油耗率 = 油耗 * 1000 / 功率。
    /// 检测到重量接近0时复位窗口；不做「相对上一秒骤降立即复位」。
    /// </summary>
    public static class JwsFuelWeighHelper
    {
        private static readonly object Sync = new object();

        /// <summary>滑动窗口长度（秒），与原先60s拍一致</summary>
        private const int WindowSeconds = 60;

        /// <summary>判定清零的重量阈值（kg）</summary>
        private const double ClearWeightKg = 0.01;

        private static readonly double[] WeightRing = new double[WindowSeconds];
        private static int _ringCount;
        private static int _ringIndex;
        private static double _lastWeight;
        private static bool _hasLastWeight;
        private static DateTime _lastTickUtc = DateTime.MinValue;

        /// <summary>称重计秒（已采样秒数）</summary>
        public static int WSecond { get; private set; }

        /// <summary>兼容旧字段：偶数拍重量</summary>
        public static double WeightEven { get; private set; }

        /// <summary>兼容旧字段：奇数拍/窗口起点重量</summary>
        public static double WeightOdd { get; private set; }

        /// <summary>兼容旧字段（滑动窗口不再使用奇偶标志）</summary>
        public static int OddEvenFlag { get; private set; }

        /// <summary>重量变化率 kg/h（每秒刷新）</summary>
        public static double WeightVariationKgH { get; private set; }

        /// <summary>是否已有有效差值（至少2个采样点）</summary>
        public static bool HasValidVariation { get; private set; }

        /// <summary>启动/复位计时与窗口</summary>
        public static void Start()
        {
            lock (Sync)
            {
                ResetWindow(SafeWeight());
            }
        }

        public static void Stop()
        {
            lock (Sync)
            {
                WSecond = 0;
                WeightVariationKgH = 0;
                HasValidVariation = false;
            }
        }

        /// <summary>手动清零称重油耗结果（去皮后可调用）</summary>
        public static void ResetVariation()
        {
            lock (Sync)
            {
                ResetWindow(SafeWeight());
            }
        }

        /// <summary>每秒调用一次：实时更新 kg/h（多处调用时按真实时间节流，避免双计）</summary>
        public static void TickOnce()
        {
            lock (Sync)
            {
                var now = DateTime.UtcNow;
                if (_lastTickUtc != DateTime.MinValue && (now - _lastTickUtc).TotalMilliseconds < 800)
                    return;
                _lastTickUtc = now;

                if (!IsWeightReady())
                {
                    // 通讯断开：清零，避免界面继续显示旧的大值
                    if (WSecond > 0)
                        ResetWindow(0);
                    return;
                }

                double w = SafeWeight();

                // 未启动则自动启动
                if (WSecond < 1)
                {
                    ResetWindow(w);
                    WSecond = 1;
                    return;
                }

                // 重量接近0时复位窗口
                bool cleared = w <= ClearWeightKg && _lastWeight > ClearWeightKg;
                if (cleared)
                {
                    ResetWindow(w);
                    WSecond = 1;
                    return;
                }

                PushSample(w);
                WSecond++;

                // 窗口内最早一点 vs 当前点：Δkg / Δt(h) = Δkg * (3600/Δt_s)
                // Δt_s = 已填满则60，否则为当前样本间隔秒数
                int span = _ringCount >= 2 ? (_ringCount - 1) : 0;
                if (span < 1)
                {
                    WeightVariationKgH = 0;
                    HasValidVariation = false;
                    _lastWeight = w;
                    _hasLastWeight = true;
                    return;
                }

                double oldest = WeightRing[OldestIndex()];
                WeightOdd = oldest;
                WeightEven = w;
                WeightVariationKgH = Math.Abs(w - oldest) * (3600.0 / span);
                HasValidVariation = WeightVariationKgH > 0;

                _lastWeight = w;
                _hasLastWeight = true;
            }
        }

        /// <summary>称重法油耗率 g/kWh</summary>
        public static double CalcWeighSpecificFuel(double powerKw)
        {
            lock (Sync)
            {
                if (powerKw <= 0 || !HasValidVariation) return 0;
                return WeightVariationKgH * 1000.0 / powerKw;
            }
        }

        private static void ResetWindow(double seedWeight)
        {
            Array.Clear(WeightRing, 0, WeightRing.Length);
            _ringCount = 0;
            _ringIndex = 0;
            WeightVariationKgH = 0;
            HasValidVariation = false;
            OddEvenFlag = 0;
            WeightEven = seedWeight;
            WeightOdd = seedWeight;
            _lastWeight = seedWeight;
            _hasLastWeight = true;
            WSecond = 0;
            if (!double.IsNaN(seedWeight))
                PushSample(seedWeight);
        }

        /// <summary>
        /// 存储60秒的重量数据，形成环形队列
        /// 压入新样本，覆盖最旧
        /// </summary>
        private static void PushSample(double w)
        {
            WeightRing[_ringIndex] = w;
            _ringIndex = (_ringIndex + 1) % WindowSeconds;
            if (_ringCount < WindowSeconds)
                _ringCount++;
        }

        private static int OldestIndex()
        {
            if (_ringCount < WindowSeconds)
                return 0;
            return _ringIndex; // 下一写入位置即最旧
        }

        private static bool IsWeightReady()
        {
            try
            {
                return MainUI.Common.weight3102QGrp != null && MainUI.Common.weight3102QGrp.IsNoError;
            }
            catch
            {
                return false;
            }
        }

        private static double SafeWeight()
        {
            try
            {
                if (!IsWeightReady()) return 0;
                // WeightKg 已是工程 kg
                return MainUI.Common.weight3102QGrp.WeightKg;
            }
            catch
            {
                return 0;
            }
        }
    }
}
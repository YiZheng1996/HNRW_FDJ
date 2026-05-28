using System;
using System.Collections.Generic;
using System.Threading;
using MainUI.Config.Modules;
using static MainUI.Modules.EventArgsModel;

namespace MainUI.Modules
{
    /// <summary>阀门/泵显示状态（三态）</summary>
    public enum ValveDisplayState
    {
        Closed,   // 关到位
        Opened,   // 开到位
        Fault     // 故障：双反馈同时为1、或5秒内反馈未到位
    }

    public class ValveStateChangedEventArgs : EventArgs
    {
        public string ControlPoint { get; set; }
        public ValveDisplayState State { get; set; }
        public bool OpenLimit { get; set; }
        public bool CloseLimit { get; set; }
        public string FaultReason { get; set; }
    }

    /// <summary>
    /// 阀门/泵状态合成服务（单例）
    /// - 双反馈阀：开到位+关到位 合成 Opened/Closed/Fault
    /// - 单反馈泵：OpenPoint == ClosePoint 时，1=Opened/0=Closed，不走超时
    /// - 无反馈阀：OpenPoint 和 ClosePoint 都为空时，由 UI 用 DO 兜底（HasFeedback 返回 false）
    /// - 超时：DO 翻转后 5 秒反馈未到位 → Fault
    /// </summary>
    public class ValveStateService
    {
        private static readonly Lazy<ValveStateService> _instance
            = new Lazy<ValveStateService>(() => new ValveStateService());
        public static ValveStateService Instance { get { return _instance.Value; } }

        /// <summary>0+0 状态去抖计时器</summary>
        private readonly Dictionary<string, Timer> _zeroDebounceTimers
            = new Dictionary<string, Timer>();

        /// <summary>1+1 状态去抖计时器</summary>
        private readonly Dictionary<string, Timer> _doubleDebounceTimers
            = new Dictionary<string, Timer>();

        /// <summary>0+0 / 1+1 去抖时长（毫秒）</summary>
        public int ZeroDebounceMs { get; set; } = 500;

        private readonly Dictionary<string, ValveConfig.ValveInfo> _byCtrl
            = new Dictionary<string, ValveConfig.ValveInfo>();
        private readonly Dictionary<string, string> _openToCtrl
            = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _closeToCtrl
            = new Dictionary<string, string>();
        private readonly Dictionary<string, bool> _openLimit
            = new Dictionary<string, bool>();
        private readonly Dictionary<string, bool> _closeLimit
            = new Dictionary<string, bool>();
        private readonly Dictionary<string, ValveDisplayState> _currentState
            = new Dictionary<string, ValveDisplayState>();
        private readonly Dictionary<string, Timer> _timers
            = new Dictionary<string, Timer>();

        private readonly object _lock = new object();
        private bool _ready = false;

        /// <summary>超时阈值（毫秒），默认 5000</summary>
        public int TransitTimeoutMs { get; set; } = 5000;

        /// <summary>启动宽限期（毫秒），该时段内 Fault/Recover 只刷新 UI，不写日志，默认 3000</summary>
        public int StartupGraceMs { get; set; } = 3000;

        private DateTime _startupTime;

        public event EventHandler<ValveStateChangedEventArgs> ValveStateChanged;

        private ValveStateService() { }

        /// <summary>初始化：构建索引、订阅 DO/DI、刷新初始反馈</summary>
        public void Init(ValveConfig cfg)
        {
            if (cfg == null || cfg.valveInfo == null) return;

            lock (_lock)
            {
                _byCtrl.Clear();
                _openToCtrl.Clear();
                _closeToCtrl.Clear();

                foreach (var v in cfg.valveInfo)
                {
                    if (string.IsNullOrEmpty(v.ValvePoint)) continue;
                    _byCtrl[v.ValvePoint] = v;
                    if (!string.IsNullOrEmpty(v.ValveOpenPoint))
                        _openToCtrl[v.ValveOpenPoint] = v.ValvePoint;
                    if (!string.IsNullOrEmpty(v.ValveClosePoint))
                        _closeToCtrl[v.ValveClosePoint] = v.ValvePoint;
                }
            }

            Common.DIgrp.KeyValueChange += DIgrp_KeyValueChange;
            Common.DOgrp.KeyValueChange += DOgrp_KeyValueChange;

            // _ready 还是 false，Fresh 不会触发计时器
            try { Common.DIgrp.Fresh(); } catch { }
            try { Common.DOgrp.Fresh(); } catch { }

            _ready = true;
            _startupTime = DateTime.Now;   // 记录启动时间

            // 为每个阀门评估一次初始状态（写入 _currentState，等 BroadcastAll 时广播）
            List<string> allCtrls;
            lock (_lock) { allCtrls = new List<string>(_byCtrl.Keys); }
            foreach (var ctrl in allCtrls) EvaluateState(ctrl, isInitial: true);
        }

        /// <summary>把所有阀门当前状态对订阅者广播一次（外部界面订阅后调用）</summary>
        public void BroadcastAll()
        {
            List<string> ctrls;
            lock (_lock) { ctrls = new List<string>(_currentState.Keys); }

            foreach (var ctrl in ctrls)
            {
                ValveDisplayState st;
                bool o, c;
                lock (_lock)
                {
                    if (!_currentState.TryGetValue(ctrl, out st)) continue;
                    _openLimit.TryGetValue(ctrl, out o);
                    _closeLimit.TryGetValue(ctrl, out c);
                }
                var handler = ValveStateChanged;
                if (handler != null)
                {
                    handler(this, new ValveStateChangedEventArgs
                    {
                        ControlPoint = ctrl,
                        State = st,
                        OpenLimit = o,
                        CloseLimit = c
                    });
                }
            }
        }

        private void StartDoubleDebounceTimer(string ctrl)
        {
            CancelDoubleDebounceTimer(ctrl);

            Timer t = new Timer(_ =>
            {
                try { OnDoubleDebounceElapsed(ctrl); } catch { }
            }, null, ZeroDebounceMs, Timeout.Infinite);

            lock (_lock) { _doubleDebounceTimers[ctrl] = t; }
        }

        private void CancelDoubleDebounceTimer(string ctrl)
        {
            Timer t = null;
            lock (_lock)
            {
                if (_doubleDebounceTimers.TryGetValue(ctrl, out t)) _doubleDebounceTimers.Remove(ctrl);
            }
            if (t != null) try { t.Dispose(); } catch { }
        }

        private void OnDoubleDebounceElapsed(string ctrl)
        {
            bool o, c;
            lock (_lock)
            {
                _openLimit.TryGetValue(ctrl, out o);
                _closeLimit.TryGetValue(ctrl, out c);
                _doubleDebounceTimers.Remove(ctrl);
            }

            // 500ms 后再检查：如果仍然是 1+1，判定真故障
            if (o && c)
            {
                UpdateStateAndRaise(ctrl, ValveDisplayState.Fault, o, c, "双反馈同时为1（物理不可能）");
            }
            // 如果反馈已经变回 1+0 或 0+1，不做任何事
        }

        private void StartZeroDebounceTimer(string ctrl)
        {
            CancelZeroDebounceTimer(ctrl);

            Timer t = new Timer(_ =>
            {
                try { OnZeroDebounceElapsed(ctrl); } catch { }
            }, null, ZeroDebounceMs, Timeout.Infinite);

            lock (_lock) { _zeroDebounceTimers[ctrl] = t; }
        }

        private void CancelZeroDebounceTimer(string ctrl)
        {
            Timer t = null;
            lock (_lock)
            {
                if (_zeroDebounceTimers.TryGetValue(ctrl, out t)) _zeroDebounceTimers.Remove(ctrl);
            }
            if (t != null) try { t.Dispose(); } catch { }
        }

        private void OnZeroDebounceElapsed(string ctrl)
        {
            bool o, c;
            lock (_lock)
            {
                _openLimit.TryGetValue(ctrl, out o);
                _closeLimit.TryGetValue(ctrl, out c);
                _zeroDebounceTimers.Remove(ctrl);
            }

            // 500ms 后再检查：如果仍然是 0+0，判定真故障
            if (!o && !c)
            {
                UpdateStateAndRaise(ctrl, ValveDisplayState.Fault, o, c, "双反馈同时为0（反馈丢失或卡阻）");
            }
            // 如果反馈已经变回 1+0 或 0+1，不做任何事
        }

        /// <summary>UI 用：判断这个控制点位是否在本服务管辖（有反馈配置）</summary>
        public bool HasFeedback(string ctrlPoint)
        {
            if (string.IsNullOrEmpty(ctrlPoint)) return false;
            lock (_lock)
            {
                ValveConfig.ValveInfo v;
                if (!_byCtrl.TryGetValue(ctrlPoint, out v)) return false;
                return !string.IsNullOrEmpty(v.ValveOpenPoint)
                    || !string.IsNullOrEmpty(v.ValveClosePoint);
            }
        }

        /// <summary>查询当前状态</summary>
        public ValveDisplayState GetState(string ctrlPoint)
        {
            lock (_lock)
            {
                ValveDisplayState s;
                if (_currentState.TryGetValue(ctrlPoint, out s)) return s;
                return ValveDisplayState.Closed;
            }
        }

        // ================ DO 翻转 ================
        private void DOgrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (!_ready) return;
            if (string.IsNullOrEmpty(e.Key)) return;

            ValveConfig.ValveInfo v;
            lock (_lock) { if (!_byCtrl.TryGetValue(e.Key, out v)) return; }

            // 无反馈 → 不计时
            if (string.IsNullOrEmpty(v.ValveOpenPoint) && string.IsNullOrEmpty(v.ValveClosePoint))
                return;
            // 单反馈泵 → 反馈本身就是状态，不需要计时
            if (v.ValveOpenPoint == v.ValveClosePoint)
                return;

            StartTimeoutTimer(e.Key);
        }

        // ================ DI 反馈 ================
        private void DIgrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Key)) return;

            string ctrl = null;
            lock (_lock)
            {
                if (_openToCtrl.TryGetValue(e.Key, out ctrl))
                {
                    _openLimit[ctrl] = e.Value;
                }
                else if (_closeToCtrl.TryGetValue(e.Key, out ctrl))
                {
                    _closeLimit[ctrl] = e.Value;
                }
                else return;

                // 单反馈：开点位=关点位 同一字符串，同步把另一个缓存设为反值
                ValveConfig.ValveInfo v;
                if (_byCtrl.TryGetValue(ctrl, out v)
                    && v.ValveOpenPoint == v.ValveClosePoint
                    && !string.IsNullOrEmpty(v.ValveOpenPoint))
                {
                    _openLimit[ctrl] = e.Value;
                    _closeLimit[ctrl] = !e.Value;
                }
            }

            if (_ready) EvaluateState(ctrl, isInitial: false);
        }

        // ================ 状态评估 ================
        private void EvaluateState(string ctrl, bool isInitial)
        {
            ValveConfig.ValveInfo v;
            bool o, c;
            lock (_lock)
            {
                if (!_byCtrl.TryGetValue(ctrl, out v)) return;
                _openLimit.TryGetValue(ctrl, out o);
                _closeLimit.TryGetValue(ctrl, out c);
            }

            bool isSingleFb = v.ValveOpenPoint == v.ValveClosePoint
                              && !string.IsNullOrEmpty(v.ValveOpenPoint);

            ValveDisplayState newState;
            string faultReason = null;

            if (isSingleFb)
            {
                newState = o ? ValveDisplayState.Opened : ValveDisplayState.Closed;
            }
            else
            {
                if (o && !c) { newState = ValveDisplayState.Opened; CancelTimeoutTimer(ctrl); CancelZeroDebounceTimer(ctrl); CancelDoubleDebounceTimer(ctrl); }
                else if (!o && c) { newState = ValveDisplayState.Closed; CancelTimeoutTimer(ctrl); CancelZeroDebounceTimer(ctrl); CancelDoubleDebounceTimer(ctrl); }

                else if (o && c)
                {
                    // 1+1：可能是切换过渡的瞬态，也可能是真故障，启动去抖
                    CancelZeroDebounceTimer(ctrl);
                    StartDoubleDebounceTimer(ctrl);
                    return;
                }
                else  // 0+0：可能是切换过渡，也可能是真故障，启动去抖
                {
                    CancelDoubleDebounceTimer(ctrl);  // 取消另一侧去抖
                    StartZeroDebounceTimer(ctrl);
                    return;  // 不立刻更新状态，等去抖结果
                }
            }

            UpdateStateAndRaise(ctrl, newState, o, c, faultReason);
        }

        // ================ 计时器 ================
        private void StartTimeoutTimer(string ctrl)
        {
            CancelTimeoutTimer(ctrl);

            // 当前反馈已经稳态 → 不计时（DO 重复设置同一值）
            bool o, c;
            lock (_lock)
            {
                _openLimit.TryGetValue(ctrl, out o);
                _closeLimit.TryGetValue(ctrl, out c);
                if ((o && !c) || (!o && c)) return;
            }

            Timer t = new Timer(_ =>
            {
                try { OnTimeoutElapsed(ctrl); } catch { }
            }, null, TransitTimeoutMs, Timeout.Infinite);

            lock (_lock) { _timers[ctrl] = t; }
        }

        private void CancelTimeoutTimer(string ctrl)
        {
            Timer t = null;
            lock (_lock)
            {
                if (_timers.TryGetValue(ctrl, out t)) _timers.Remove(ctrl);
            }
            if (t != null) try { t.Dispose(); } catch { }
        }

        private void OnTimeoutElapsed(string ctrl)
        {
            bool o, c;
            lock (_lock)
            {
                _openLimit.TryGetValue(ctrl, out o);
                _closeLimit.TryGetValue(ctrl, out c);
                _timers.Remove(ctrl);
            }
            if (!o && !c)
            {
                UpdateStateAndRaise(ctrl, ValveDisplayState.Fault, o, c, "5秒内反馈未到位");
            }
        }

        // ================ 状态更新 + 广播 + 日志 ================
        private void UpdateStateAndRaise(string ctrl, ValveDisplayState newState,
                                         bool o, bool c, string faultReason)
        {
            ValveDisplayState prevState;
            bool hadPrev;
            lock (_lock)
            {
                hadPrev = _currentState.TryGetValue(ctrl, out prevState);
                if (hadPrev && prevState == newState) return;
                _currentState[ctrl] = newState;
            }

            // 日志：启动宽限期内不写（避开 PLC 上电时反馈未稳定造成的虚警）
            try
            {
                bool inGrace = (DateTime.Now - _startupTime).TotalMilliseconds < StartupGraceMs;

                if (newState == ValveDisplayState.Fault
                    && (!hadPrev || prevState != ValveDisplayState.Fault)
                    && !inGrace)
                {
                    Fault.ValveFaultLogger.LogFault(ctrl, o, c, faultReason);
                }
                else if (hadPrev && prevState == ValveDisplayState.Fault
                         && newState != ValveDisplayState.Fault
                         && !inGrace)
                {
                    Fault.ValveFaultLogger.LogRecover(ctrl, newState, o, c);
                }
            }
            catch { }

            // 广播
            var handler = ValveStateChanged;
            if (handler != null)
            {
                try
                {
                    handler(this, new ValveStateChangedEventArgs
                    {
                        ControlPoint = ctrl,
                        State = newState,
                        OpenLimit = o,
                        CloseLimit = c,
                        FaultReason = faultReason
                    });
                }
                catch { }
            }
        }
    }
}
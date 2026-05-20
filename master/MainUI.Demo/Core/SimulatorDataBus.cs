using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MainUI.Demo.Core
{
    /// <summary>
    /// 仿真数据总线（Demo 项目专用）。
    /// 
    /// 设计要点（来自客户的核心要求："不改变当前软件功能"）：
    ///   1. 完全独立于主程序的 Common.AIgrp 等模块, 数据流物理隔离
    ///   2. 任何想读仿真数据的 UI 控件，订阅本总线即可
    ///   3. 主程序的真机数据流不受任何影响
    /// 
    /// 信号命名空间与主程序保持一致（"高温水出机温度"、"柴油机转速"等），
    /// 这样未来如果要在主程序里加双源支持，无缝迁移。
    /// </summary>
    public class SimulatorDataBus
    {
        #region 单例

        private static readonly SimulatorDataBus _inst = new SimulatorDataBus();
        public static SimulatorDataBus Instance { get { return _inst; } }
        private SimulatorDataBus() { }

        #endregion

        #region 数据缓存

        public ConcurrentDictionary<string, double> DoubleValues
            { get; } = new ConcurrentDictionary<string, double>();

        public ConcurrentDictionary<string, bool> BoolValues
            { get; } = new ConcurrentDictionary<string, bool>();

        public ConcurrentDictionary<string, string> StringValues
            { get; } = new ConcurrentDictionary<string, string>();

        #endregion

        #region 事件

        public class DoubleEventArgs : EventArgs
        {
            public string Key { get; set; }
            public double OldValue { get; set; }
            public double Value { get; set; }
            public DoubleEventArgs(string k, double oldV, double v)
            { Key = k; OldValue = oldV; Value = v; }
        }

        public class BoolEventArgs : EventArgs
        {
            public string Key { get; set; }
            public bool OldValue { get; set; }
            public bool Value { get; set; }
            public BoolEventArgs(string k, bool oldV, bool v)
            { Key = k; OldValue = oldV; Value = v; }
        }

        /// <summary>double 值变化（仪表盘订阅）</summary>
        public event EventHandler<DoubleEventArgs> DoubleChanged;

        /// <summary>bool 值变化（DI 等订阅）</summary>
        public event EventHandler<BoolEventArgs> BoolChanged;

        /// <summary>会话状态变化（仿真启停）</summary>
        public event EventHandler<bool> SessionChanged;

        #endregion

        #region 会话状态

        public bool IsSimulating { get; private set; }
        public string SessionId { get; private set; } = "";
        public DateTime SessionStartTime { get; private set; }

        public void EnterSimulation()
        {
            IsSimulating = true;
            SessionId = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpperInvariant();
            SessionStartTime = DateTime.Now;
            try { SessionChanged?.Invoke(this, true); } catch { }
        }

        public void ExitSimulation()
        {
            IsSimulating = false;
            try { SessionChanged?.Invoke(this, false); } catch { }
        }

        public TimeSpan Elapsed
        {
            get { return IsSimulating ? DateTime.Now - SessionStartTime : TimeSpan.Zero; }
        }

        #endregion

        #region 发布 API

        /// <summary>发布一个 double 信号值（会触发 DoubleChanged 事件）</summary>
        public void PublishDouble(string key, double value)
        {
            if (string.IsNullOrEmpty(key)) return;

            double old = DoubleValues.TryGetValue(key, out var v) ? v : 0;
            DoubleValues.AddOrUpdate(key, value, (k, oldVal) => value);

            // 只有值实际变化才触发事件, 避免噪声
            if (Math.Abs(old - value) > 1e-6)
            {
                try { DoubleChanged?.Invoke(this, new DoubleEventArgs(key, old, value)); }
                catch { }
            }
        }

        /// <summary>发布一个 bool 信号值</summary>
        public void PublishBool(string key, bool value)
        {
            if (string.IsNullOrEmpty(key)) return;

            bool old = BoolValues.TryGetValue(key, out var v) && v;
            BoolValues.AddOrUpdate(key, value, (k, oldVal) => value);

            if (old != value)
            {
                try { BoolChanged?.Invoke(this, new BoolEventArgs(key, old, value)); }
                catch { }
            }
        }

        public void PublishString(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) return;
            StringValues.AddOrUpdate(key, value ?? "", (k, oldVal) => value ?? "");
        }

        #endregion

        #region 查询 API

        public double GetDouble(string key, double defaultValue = 0)
        {
            return DoubleValues.TryGetValue(key, out var v) ? v : defaultValue;
        }

        public bool GetBool(string key, bool defaultValue = false)
        {
            return BoolValues.TryGetValue(key, out var v) ? v : defaultValue;
        }

        public IReadOnlyDictionary<string, double> SnapshotDoubles()
        {
            return new Dictionary<string, double>(DoubleValues);
        }

        #endregion

        #region 重置

        public void Reset()
        {
            DoubleValues.Clear();
            BoolValues.Clear();
            StringValues.Clear();
        }

        #endregion
    }
}

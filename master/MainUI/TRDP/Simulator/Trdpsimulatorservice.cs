using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using BogieIdling.UI.TRDP;
using MainUI.Driver;
using MetorSignalSimulator.UI.Model;

namespace MainUI.Simulate
{
    /// <summary>
    /// TRDP 数据模拟服务（单例）
    ///
    /// 原理：直接写入 Var.TRDP.trdpValue 字典并触发 KeyValueChange 事件，
    ///       ECMFaultDetectionService 每秒轮询 GetDicValue() 时自动拿到注入值，
    ///       不需要真实 UDP 包，不需要修改驱动层。
    /// </summary>
    public class TRDPSimulatorService
    {
        // ── 单例 ─────────────────────────────────────────────────────────────
        private static readonly TRDPSimulatorService _inst = new TRDPSimulatorService();
        public static TRDPSimulatorService Instance => _inst;
        private TRDPSimulatorService() { }

        // ── 状态 ─────────────────────────────────────────────────────────────
        public bool IsRunning { get; private set; }
        public bool LifeAutoMode { get; private set; } = true;
        public uint LifeCounter { get; private set; }
        public int TickCount { get; private set; }

        // ── 日志事件（UI 订阅，注意跨线程用 Invoke）────────────────────────
        public enum LogLevel { Info, OK, Warn, Fault }
        public event Action<string, LogLevel> OnLog;

        // ── 渐增任务 ─────────────────────────────────────────────────────────
        private Thread _rampThread;
        private bool _rampRunning;

        // ── 心跳定时器 ───────────────────────────────────────────────────────
        private System.Timers.Timer _heartbeat;

        // ── 当前所有已注入的模拟量快照（方便 GetSnapshot 给 UI 刷新）────────
        private readonly Dictionary<string, decimal> _snapshot
            = new Dictionary<string, decimal>();

        // ═════════════════════════════════════════════════════════════════════
        // 启动 / 停止
        // ═════════════════════════════════════════════════════════════════════

        public void Start()
        {
            if (IsRunning) return;
            IsRunning = true;
            ForceConnected();

            _heartbeat = new System.Timers.Timer(1000);
            _heartbeat.Elapsed += OnHeartbeat;
            _heartbeat.Start();

            Log("TRDP 模拟已启动，生命信号开始推送", LogLevel.OK);
        }

        public void Stop()
        {
            IsRunning = false;
            StopRamp();
            _heartbeat?.Stop();
            _heartbeat?.Dispose();
            _heartbeat = null;
            Log("TRDP 模拟已停止", LogLevel.Info);
        }

        private void OnHeartbeat(object s, System.Timers.ElapsedEventArgs e)
        {
            TickCount++;
            if (LifeAutoMode)
            {
                LifeCounter = (LifeCounter + 1) % 256;
                InjectValue("设备生命信号", (decimal)LifeCounter);
            }
            ForceConnected();
        }

        // ═════════════════════════════════════════════════════════════════════
        // 核心注入
        // ═════════════════════════════════════════════════════════════════════

        /// <summary>
        /// 向 TRDP 字典注入单个值，并触发 KeyValueChange 事件。
        /// ECMFaultDetectionService 下一轮轮询时自动读取。
        /// </summary>
        public void InjectValue(string signalName, decimal value)
        {
            try
            {
                Var.TRDP.trdpValue.AddOrUpdate(signalName, value, (k, o) => value);

                var tag = new FullTags { DataLabel = signalName };

                Var.TRDP.RaiseKeyValueChange(tag, signalName, value);

                lock (_snapshot) { _snapshot[signalName] = value; }
            }
            catch (Exception ex)
            {
                Log($"注入失败 [{signalName}={value}]: {ex.Message}", LogLevel.Fault);
            }
        }

        // ═════════════════════════════════════════════════════════════════════
        // 故障清除
        // ═════════════════════════════════════════════════════════════════════

        public void ClearAllFaults()
        {
            var faultBits = new[]
            {
                "紧急报警", "公共报警",
                "电磁阀故障1#",  "电磁阀故障2#",  "电磁阀故障3#",  "电磁阀故障4#",
                "电磁阀故障5#",  "电磁阀故障6#",  "电磁阀故障7#",  "电磁阀故障8#",
                "电磁阀故障9#",  "电磁阀故障10#", "电磁阀故障11#", "电磁阀故障12#",
                "供电电源故障",
                "转速传感器故障1#", "转速传感器故障2#",
                "相位传感器故障", "超速故障", "同步输入故障", "硬件故障",
                "网口0故障",
                "从站1串口故障", "从站2串口故障",
                "从站3串口故障", "从站6串口故障"
            };
            foreach (var sig in faultBits)
                InjectValue(sig, 0m);

            InjectValue("同步状态", 1m);
            Log("所有故障位已清除，系统恢复正常", LogLevel.OK);
        }

        // ═════════════════════════════════════════════════════════════════════
        // 预设故障场景
        // ═════════════════════════════════════════════════════════════════════

        public void RunPreset(string presetKey)
        {
            switch (presetKey)
            {
                case "shed_axis":
                    InjectValue("三档轴温", 108m);
                    InjectValue("公共报警", 1m);
                    Log("预设①：三档轴温=108℃ → 触发降载(F28 Shedding)", LogLevel.Warn);
                    break;

                case "stop_axis":
                    InjectValue("三档轴温", 125m);
                    InjectValue("紧急报警", 1m);
                    Log("预设②：三档轴温=125℃ → 触发停机(F28 Stop)", LogLevel.Fault);
                    break;

                case "overspeed":
                    InjectValue("超速故障", 1m);
                    InjectValue("转速传感器1#", 1150m);
                    InjectValue("转速传感器2#", 1148m);
                    InjectValue("紧急报警", 1m);
                    Log("预设③：超速故障位=1 + 传感器1150 → 停机(F17/F30)", LogLevel.Fault);
                    break;

                case "inj_overspeed":
                    InjectValue("电喷转速1#", 1150m);
                    InjectValue("电喷转速2#", 1145m);
                    InjectValue("紧急报警", 1m);
                    Log("预设④：电喷转速1#=1150 → 停机(F30/F31)", LogLevel.Fault);
                    break;

                case "slave6_comm":
                    InjectValue("从站6串口故障", 1m);
                    InjectValue("公共报警", 1m);
                    Log("预设⑤：从站6串口故障=1 → 轴温监控报警(F29)", LogLevel.Warn);
                    break;

                case "solenoid":
                    InjectValue("电磁阀故障5#", 1m);
                    InjectValue("电磁阀故障6#", 1m);
                    InjectValue("公共报警", 1m);
                    Log("预设⑥：电磁阀5#+6# 故障 → 公共报警(F05/F06)", LogLevel.Warn);
                    break;

                case "power_low":
                    InjectValue("电源A", 21.0m);
                    InjectValue("电源B", 20.8m);
                    InjectValue("供电电源故障", 1m);
                    InjectValue("公共报警", 1m);
                    Log("预设⑦：电源降至21V + 供电电源故障=1", LogLevel.Fault);
                    break;

                case "normal":
                    ClearAllFaults();
                    InjectValue("柴油机转速", 1000m);
                    InjectValue("转速传感器1#", 1000m);
                    InjectValue("转速传感器2#", 1000m);
                    InjectValue("电喷转速1#", 1000m);
                    InjectValue("电喷转速2#", 1000m);
                    InjectValue("电源A", 27.8m);
                    InjectValue("电源B", 27.6m);
                    for (int i = 0; i < 7; i++)
                        InjectValue(GearName(i), 70m + i * 2.5m);
                    Log("预设⑧：恢复全部正常工况", LogLevel.OK);
                    break;
            }
        }

        // ═════════════════════════════════════════════════════════════════════
        // 渐进式轴温注入（独立线程，模拟缓慢过热）
        // ═════════════════════════════════════════════════════════════════════

        public void StartRamp(int gearIndex, double from, double to, double stepPerSec)
        {
            StopRamp();
            _rampRunning = true;
            _rampThread = new Thread(() =>
            {
                double cur = from;
                string name = GearName(gearIndex);
                Log($"渐进注入开始：{name} {from}→{to}℃，步进{stepPerSec}℃/s", LogLevel.Warn);
                while (_rampRunning && cur <= to)
                {
                    InjectValue(name, (decimal)Math.Round(cur, 1));
                    Thread.Sleep(1000);
                    cur += stepPerSec;
                }
                if (!_rampRunning) { Log("渐进注入已中断", LogLevel.Info); return; }
                Log($"渐进注入完成：{name}={to}℃", LogLevel.Fault);
                _rampRunning = false;
            })
            { IsBackground = true, Name = "TRDP_Ramp" };
            _rampThread.Start();
        }

        public void StopRamp()
        {
            _rampRunning = false;
            _rampThread = null;
        }

        public bool IsRamping => _rampRunning;

        // ═════════════════════════════════════════════════════════════════════
        // 生命信号控制
        // ═════════════════════════════════════════════════════════════════════

        public void SetLifeAuto(bool auto)
        {
            LifeAutoMode = auto;
            Log(auto ? "生命信号：自动递增" : "生命信号：已冻结（模拟通讯断开）",
                auto ? LogLevel.OK : LogLevel.Warn);
        }

        // ═════════════════════════════════════════════════════════════════════
        // 辅助
        // ═════════════════════════════════════════════════════════════════════

        private static readonly string[] GearNames =
            { "一档轴温", "二档轴温", "三档轴温", "四档轴温", "五档轴温", "六档轴温", "七档轴温" };

        public static string GearName(int index) => GearNames[index % 7];

        /// <summary>
        /// 通过反射刷新 UpdateTime，使 IsConnected 保持为 true（避免心跳超时断开）
        /// </summary>
        private static void ForceConnected()
        {
            try
            {
                var fi = Var.TRDP.GetType().GetField("UpdateTime",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                fi?.SetValue(Var.TRDP, DateTime.Now);
            }
            catch { /* 不影响主流程 */ }
        }

        private void Log(string msg, LogLevel level)
        {
            try { OnLog?.Invoke(msg, level); } catch { }
            Var.LogInfo($"[TRDPSim] {msg}");
        }
    }
}
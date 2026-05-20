using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;

namespace MainUI.Demo.Core
{
    /// <summary>一阶惯性环节: y(k) = y(k-1) + (target - y(k-1)) * dt / tau</summary>
    public class FirstOrderInertia
    {
        private double _current;
        private readonly double _tau;
        private readonly double _noiseAmp;
        private static readonly Random _rnd = new Random();

        public FirstOrderInertia(double initValue, double tau, double noiseAmp = 0)
        {
            _current = initValue;
            _tau = tau <= 0 ? 1 : tau;
            _noiseAmp = noiseAmp;
        }

        public double Step(double target, double dt = 1.0)
        {
            _current += (target - _current) * dt / _tau;
            double noise = _noiseAmp > 0
                ? (_rnd.NextDouble() - 0.5) * 2 * _noiseAmp
                : 0;
            return Math.Round(_current + noise, 2);
        }

        public void ForceSet(double value) { _current = value; }
        public double Current { get { return _current; } }
    }

    /// <summary>
    /// 仿真引擎核心。
    /// 单例。负责按 1Hz 推进虚拟柴油机状态, 输出到 SimulatorDataBus。
    /// </summary>
    public class SimulationEngine
    {
        #region 单例

        private static readonly SimulationEngine _inst = new SimulationEngine();
        public static SimulationEngine Instance { get { return _inst; } }
        private SimulationEngine() { }

        #endregion

        #region 控制状态

        public bool IsRunning { get; private set; }

        /// <summary>0~100, 油门百分比</summary>
        public double ThrottlePercent { get; set; } = 80;

        /// <summary>0~100, 励磁百分比</summary>
        public double ExcitationPercent { get; set; } = 80;

        public int TickCount { get; private set; }

        #endregion

        #region 工况点

        private readonly StaticPoint _cal = new StaticPoint
        {
            Speed = 1000, Power = 2700,
            TurboF = 38000, TurboR = 37800,
            HWaterTOut = 85, LWaterTOut = 45, OilT = 78,
            ExhaustA = 580, ExhaustB = 575, AxisT = 95,
            MainOilP = 480, FuelP = 380,
            HWaterP = 320, BoostP = 220,
            FuelCons = 580,
            GenU = 670, GenI = 2700
        };

        #endregion

        #region 滤波器

        private readonly Dictionary<string, FirstOrderInertia> _f
            = new Dictionary<string, FirstOrderInertia>();

        private void InitFilters()
        {
            _f.Clear();
            _f["转速"]       = new FirstOrderInertia(0,  3.0, 2);
            _f["功率"]       = new FirstOrderInertia(0,  4.0, 5);
            _f["前增压"]     = new FirstOrderInertia(0,  5.0, 50);
            _f["后增压"]     = new FirstOrderInertia(0,  5.0, 50);
            _f["高温水温"]   = new FirstOrderInertia(25, 300.0, 0.3);
            _f["中冷水温"]   = new FirstOrderInertia(25, 200.0, 0.3);
            _f["机油温"]     = new FirstOrderInertia(25, 250.0, 0.3);
            _f["排温A"]      = new FirstOrderInertia(25, 30.0, 8);
            _f["排温B"]      = new FirstOrderInertia(25, 30.0, 8);
            _f["轴温"]       = new FirstOrderInertia(25, 400.0, 0.5);
            _f["主油压"]     = new FirstOrderInertia(0,  3.0, 5);
            _f["燃油压"]     = new FirstOrderInertia(0,  3.0, 4);
            _f["高温水压"]   = new FirstOrderInertia(0,  3.0, 3);
            _f["增压压力"]   = new FirstOrderInertia(0,  4.0, 3);
            _f["油耗"]       = new FirstOrderInertia(0,  10.0, 5);
            _f["发电压"]     = new FirstOrderInertia(0,  2.0, 3);
            _f["发电流"]     = new FirstOrderInertia(0,  2.0, 8);
        }

        #endregion

        #region 启停

        private System.Timers.Timer _timer;

        public void Start()
        {
            if (IsRunning) return;

            InitFilters();
            TickCount = 0;

            SimulatorDataBus.Instance.EnterSimulation();
            SimulationLogger.Instance.LogMilestone("✓ 仿真引擎已启动（标定功率工况, 油门=80%, 励磁=80%）");

            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += OnTick;
            _timer.AutoReset = true;
            _timer.Start();

            IsRunning = true;
        }

        public void Stop()
        {
            if (!IsRunning) return;

            try
            {
                _timer?.Stop();
                _timer?.Dispose();
                _timer = null;

                SimulatorDataBus.Instance.ExitSimulation();
                SimulationLogger.Instance.LogMilestone("◾ 仿真引擎已停止");
            }
            catch (Exception ex)
            {
                SimulationLogger.Instance.LogException("停止引擎", ex);
            }
            finally
            {
                IsRunning = false;
            }
        }

        #endregion

        #region 主循环

        private readonly Dictionary<string, double> _lastVal = new Dictionary<string, double>();

        private void OnTick(object sender, ElapsedEventArgs e)
        {
            try
            {
                TickCount++;

                double k = ThrottlePercent / 100.0;
                double kp = k * (ExcitationPercent / 100.0);

                double speed   = _f["转速"].Step(_cal.Speed * k);
                double power   = _f["功率"].Step(_cal.Power * kp);
                double turboF  = _f["前增压"].Step(_cal.TurboF * k);
                double turboR  = _f["后增压"].Step(_cal.TurboR * k);
                double hWaterT = _f["高温水温"].Step(_cal.HWaterTOut * (0.4 + 0.6 * k));
                double lWaterT = _f["中冷水温"].Step(_cal.LWaterTOut * (0.6 + 0.4 * k));
                double oilT    = _f["机油温"].Step(_cal.OilT * (0.4 + 0.6 * k));
                double exhA    = _f["排温A"].Step(_cal.ExhaustA * (0.3 + 0.7 * k));
                double exhB    = _f["排温B"].Step(_cal.ExhaustB * (0.3 + 0.7 * k));
                double axisT   = _f["轴温"].Step(_cal.AxisT * (0.5 + 0.5 * k));
                double mOilP   = _f["主油压"].Step(_cal.MainOilP * k);
                double fuelP   = _f["燃油压"].Step(_cal.FuelP * k);
                double hWaterP = _f["高温水压"].Step(_cal.HWaterP * k);
                double boostP  = _f["增压压力"].Step(_cal.BoostP * k);
                double fuelC   = _f["油耗"].Step(_cal.FuelCons * kp);
                double genU    = _f["发电压"].Step(_cal.GenU * k);
                double genI    = _f["发电流"].Step(_cal.GenI * kp);

                // 发布到总线（仪表盘订阅这个）
                Publish("柴油机转速", speed);
                Publish("柴油机功率", power);
                Publish("前增压器转速", turboF);
                Publish("后增压器转速", turboR);
                Publish("高温水出机温度", hWaterT);
                Publish("中冷水出机温度", lWaterT);
                Publish("机油泵出口油温", oilT);
                Publish("A涡前排气温度", exhA);
                Publish("B涡前排气温度", exhB);
                Publish("轴温平均", axisT);
                Publish("主油道末端油压", mOilP);
                Publish("燃油精滤后油压", fuelP);
                Publish("高温水出机压力", hWaterP);
                Publish("增压压力", boostP);
                Publish("燃油消耗率", fuelC);
                Publish("发电机电压", genU);
                Publish("发电机电流", genI);
                Publish("有功功率", power);

                // 故障注入处理
                ProcessFault();

                // 每分钟心跳
                if (TickCount % 60 == 0)
                {
                    SimulationLogger.Instance.Log(
                        LogCategory.Business, LogLevel.Info,
                        $"心跳 {TickCount / 60}min: 转速={speed:F0}rpm, 功率={power:F0}kW, 高温水={hWaterT:F1}℃, 油门={ThrottlePercent:F0}%",
                        source: "Heartbeat");
                }
            }
            catch (Exception ex)
            {
                SimulationLogger.Instance.LogException("Tick", ex);
            }
        }

        private void Publish(string signal, double newVal)
        {
            double oldVal = _lastVal.TryGetValue(signal, out var v) ? v : 0;
            SimulatorDataBus.Instance.PublishDouble(signal, newVal);
            _lastVal[signal] = newVal;

            double diff = Math.Abs(newVal - oldVal);
            double rel = oldVal != 0 ? diff / Math.Abs(oldVal) : diff;
            bool periodic = (TickCount % 10 == 0);

            if (rel > 0.01 || diff > 1.0 || periodic)
            {
                SimulationLogger.Instance.LogInjection(signal, oldVal, newVal, source: "SimEngine");
            }
        }

        #endregion

        #region 故障注入

        public enum FaultPreset
        {
            Normal,
            HighWaterTempAlarm,
            HighWaterTempStop,
            AxisOverTempStop,
            MainOilPressureLow,
            EmergencyStop
        }

        private FaultPreset _pendingFault = FaultPreset.Normal;
        private string _faultTraceId;

        public FaultPreset CurrentFault { get { return _pendingFault; } }

        public void InjectFault(FaultPreset preset)
        {
            _faultTraceId = SimulationLogger.Instance.BeginTrace(
                $"模拟故障: {GetFaultName(preset)}");

            _pendingFault = preset;

            SimulationLogger.Instance.LogFault(
                faultName: GetFaultName(preset),
                trigger: GetFaultTrigger(preset),
                source: "Operator/UI");

            // 模拟上层响应（演示用：3秒后自动触发"上层响应"日志，方便客户看完整因果链）
            ThreadPool.QueueUserWorkItem(s => SimulateUpperResponse(preset));
        }

        private void SimulateUpperResponse(FaultPreset preset)
        {
            // 等故障值注入到位
            Thread.Sleep(1000);

            switch (preset)
            {
                case FaultPreset.HighWaterTempAlarm:
                    SimulationLogger.Instance.LogResponse("FaultService",
                        "检测到温度超阈值 (>101℃)", "高温水温过高报警");
                    break;

                case FaultPreset.HighWaterTempStop:
                    Thread.Sleep(50);
                    SimulationLogger.Instance.LogResponse("FaultService",
                        "检测到温度超阈值 (>103℃)", "触发降载逻辑");
                    Thread.Sleep(16);
                    // 油门归零（仿真器响应）
                    ThrottlePercent = 0;
                    SimulationLogger.Instance.Log(LogCategory.Response, LogLevel.OK,
                        "仿真器闭环响应油门归零", "AO.发动机油门调节",
                        oldValue: "80.0", newValue: "0.0", source: "SimEngine.ClosedLoop");
                    Thread.Sleep(5);
                    SimulationLogger.Instance.Log(LogCategory.Audit, LogLevel.Info,
                        "记录故障事件到 TestParaALL (停机原因=高温水超温)",
                        source: "AutoRecord");
                    break;

                case FaultPreset.AxisOverTempStop:
                    Thread.Sleep(38);
                    SimulationLogger.Instance.LogResponse("FaultService",
                        "检测到轴温≥123℃", "执行紧急停机");
                    ThrottlePercent = 0;
                    break;

                case FaultPreset.MainOilPressureLow:
                    Thread.Sleep(52);
                    SimulationLogger.Instance.LogResponse("FaultService",
                        "主油道压力低 (转速>760时<250kPa)", "触发降载");
                    ThrottlePercent = 0;
                    break;

                case FaultPreset.EmergencyStop:
                    Thread.Sleep(12);
                    SimulationLogger.Instance.LogResponse("Test100hProc",
                        "检测到 DI[紧急停止]=false", "试验自动终止");
                    ThrottlePercent = 0;
                    SimulationLogger.Instance.Log(LogCategory.Audit, LogLevel.Critical,
                        "试验中断记录写入 (中断原因=急停触发)",
                        source: "TestParaService");
                    break;
            }
        }

        public void ClearFault()
        {
            var prev = _pendingFault;
            _pendingFault = FaultPreset.Normal;
            ThrottlePercent = 80; // 恢复油门

            SimulationLogger.Instance.Log(LogCategory.Scenario, LogLevel.OK,
                $"清除故障: {GetFaultName(prev)}, 恢复正常工况", source: "Operator/UI");

            if (_faultTraceId != null)
            {
                SimulationLogger.Instance.EndTrace(_faultTraceId, "故障已清除");
                _faultTraceId = null;
            }
        }

        private void ProcessFault()
        {
            switch (_pendingFault)
            {
                case FaultPreset.HighWaterTempAlarm:
                    SimulatorDataBus.Instance.PublishDouble("高温水出机温度", 102);
                    break;
                case FaultPreset.HighWaterTempStop:
                    SimulatorDataBus.Instance.PublishDouble("高温水出机温度", 105);
                    break;
                case FaultPreset.AxisOverTempStop:
                    SimulatorDataBus.Instance.PublishDouble("轴温平均", 125);
                    break;
                case FaultPreset.MainOilPressureLow:
                    SimulatorDataBus.Instance.PublishDouble("主油道末端油压", 80);
                    break;
                case FaultPreset.EmergencyStop:
                    SimulatorDataBus.Instance.PublishBool("紧急停止", false);
                    break;
            }
        }

        public static string GetFaultName(FaultPreset p)
        {
            switch (p)
            {
                case FaultPreset.HighWaterTempAlarm: return "高温水温≥101℃报警";
                case FaultPreset.HighWaterTempStop:  return "高温水温≥103℃降载/停机";
                case FaultPreset.AxisOverTempStop:   return "轴温≥123℃停机";
                case FaultPreset.MainOilPressureLow: return "主油道末端油压过低";
                case FaultPreset.EmergencyStop:      return "急停按钮触发";
                default: return p.ToString();
            }
        }

        public static string GetFaultTrigger(FaultPreset p)
        {
            switch (p)
            {
                case FaultPreset.HighWaterTempAlarm: return "注入 高温水出机温度=102℃";
                case FaultPreset.HighWaterTempStop:  return "注入 高温水出机温度=105℃";
                case FaultPreset.AxisOverTempStop:   return "注入 轴温=125℃";
                case FaultPreset.MainOilPressureLow: return "注入 主油道末端油压=80kPa";
                case FaultPreset.EmergencyStop:      return "注入 DI[紧急停止]=false";
                default: return "";
            }
        }

        #endregion

        private class StaticPoint
        {
            public double Speed, Power, TurboF, TurboR;
            public double HWaterTOut, LWaterTOut, OilT, ExhaustA, ExhaustB, AxisT;
            public double MainOilP, FuelP, HWaterP, BoostP;
            public double FuelCons, GenU, GenI;
        }
    }
}

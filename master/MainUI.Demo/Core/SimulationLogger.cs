using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MainUI.Demo.Core
{
    public enum LogCategory
    {
        Business = 0, Scenario = 1, Injection = 2, Response = 3,
        Operator = 4, Communication = 5, System = 6, Audit = 7
    }

    public enum LogLevel
    {
        Debug = 0, Info = 1, OK = 2, Warn = 3, Fault = 4, Critical = 5
    }

    public class LogEntry
    {
        public string Id { get; set; }
        public DateTime Timestamp { get; set; }
        public LogCategory Category { get; set; }
        public LogLevel Level { get; set; }
        public string TraceId { get; set; }
        public string Message { get; set; }
        public string Signal { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Source { get; set; }
        public int ThreadId { get; set; }
        public string Exception { get; set; }
        public string Hash { get; set; }

        public string ToLine()
        {
            var sb = new StringBuilder();
            sb.Append(Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            sb.Append(" [").Append(Level.ToString().PadRight(8)).Append("]");
            sb.Append("[").Append(Category.ToString().PadRight(12)).Append("]");
            sb.Append(" {").Append(TraceId ?? "--------").Append("}");
            if (!string.IsNullOrEmpty(Signal))
                sb.Append(" [").Append(Signal).Append("]");
            sb.Append(" ").Append(Message);
            if (!string.IsNullOrEmpty(OldValue) || !string.IsNullOrEmpty(NewValue))
                sb.Append(" (").Append(OldValue ?? "-").Append(" → ").Append(NewValue ?? "-").Append(")");
            if (!string.IsNullOrEmpty(Source))
                sb.Append(" src=").Append(Source);
            if (!string.IsNullOrEmpty(Exception))
                sb.AppendLine().Append("    EX: ").Append(Exception);
            return sb.ToString();
        }
    }

    /// <summary>
    /// 仿真器中央日志服务（Demo 项目专用, 文件版, 无 MySQL 依赖）。
    /// 
    /// 提供：
    ///   - 结构化日志记录（每条带 TraceId、新旧值、来源等）
    ///   - 文件写盘（按天滚动）
    ///   - 内存缓存（供 UI 实时显示）
    ///   - 链式哈希防篡改
    ///   - 跨线程安全
    /// </summary>
    public class SimulationLogger
    {
        #region 单例

        private static readonly SimulationLogger _inst = new SimulationLogger();
        public static SimulationLogger Instance { get { return _inst; } }
        private SimulationLogger() { }

        #endregion

        public string LogRoot { get; set; }
            = Path.Combine(Application.StartupPath, "SimLog");
        public LogLevel MinLevel { get; set; } = LogLevel.Info;
        public int MaxMemoryCache { get; set; } = 5000;
        public int FlushIntervalMs { get; set; } = 500;
        public bool IsRunning { get; private set; }

        public event Action<LogEntry> OnLogAppended;

        private readonly ConcurrentQueue<LogEntry> _queue = new ConcurrentQueue<LogEntry>();
        private readonly LinkedList<LogEntry> _memory = new LinkedList<LogEntry>();
        private readonly object _memLock = new object();
        private Thread _flushThread;
        private volatile bool _stopFlag;
        private string _lastHash = "";

        private readonly ThreadLocal<Stack<string>> _traceStack
            = new ThreadLocal<Stack<string>>(() => new Stack<string>());

        #region 启停

        public void Start()
        {
            if (IsRunning) return;
            try
            {
                if (!Directory.Exists(LogRoot)) Directory.CreateDirectory(LogRoot);
                _stopFlag = false;
                _flushThread = new Thread(FlushLoop)
                {
                    IsBackground = true, Name = "SimLog.Flush"
                };
                _flushThread.Start();
                IsRunning = true;
                Log(LogCategory.System, LogLevel.OK, "SimulationLogger 已启动",
                    source: "System", extra: "LogRoot=" + LogRoot);
            }
            catch { }
        }

        public void Stop()
        {
            if (!IsRunning) return;
            _stopFlag = true;
            try { _flushThread?.Join(2000); } catch { }
            FlushOnce();
            IsRunning = false;
        }

        #endregion

        #region TraceId

        public string BeginTrace(string description)
        {
            string traceId = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpperInvariant();
            _traceStack.Value.Push(traceId);
            Log(LogCategory.Audit, LogLevel.Info, "▶ 开始: " + description, source: "TraceBegin");
            return traceId;
        }

        public void EndTrace(string traceId = null, string conclusion = null)
        {
            if (_traceStack.Value.Count == 0) return;
            _traceStack.Value.Pop();
            Log(LogCategory.Audit, LogLevel.Info,
                conclusion != null ? "◀ 结束: " + conclusion : "◀ 结束",
                source: "TraceEnd");
        }

        public string CurrentTraceId
        {
            get { return _traceStack.Value.Count > 0 ? _traceStack.Value.Peek() : null; }
        }

        #endregion

        #region 写入 API

        public void Log(LogCategory cat, LogLevel level, string message,
            string signal = null, string oldValue = null, string newValue = null,
            string source = null, Exception ex = null, string extra = null)
        {
            if (level < MinLevel) return;
            try
            {
                var entry = new LogEntry
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Timestamp = DateTime.Now,
                    Category = cat,
                    Level = level,
                    TraceId = CurrentTraceId,
                    Message = message ?? "",
                    Signal = signal,
                    OldValue = oldValue,
                    NewValue = newValue,
                    Source = source,
                    ThreadId = Thread.CurrentThread.ManagedThreadId,
                    Exception = ex?.ToString()
                };
                entry.Hash = ComputeHash(entry);

                _queue.Enqueue(entry);
                AddToMemory(entry);
                try { OnLogAppended?.Invoke(entry); } catch { }
            }
            catch { }
        }

        public void LogInjection(string signal, double oldVal, double newVal, string source = "Simulator")
        {
            Log(LogCategory.Injection, LogLevel.Info, "注入信号值",
                signal: signal, oldValue: oldVal.ToString("F2"),
                newValue: newVal.ToString("F2"), source: source);
        }

        public void LogFault(string faultName, string trigger, string source = "Operator")
        {
            Log(LogCategory.Scenario, LogLevel.Warn,
                "触发故障: " + faultName + " (" + trigger + ")", source: source);
        }

        public void LogResponse(string responder, string action, string detail = null)
        {
            Log(LogCategory.Response, LogLevel.Info,
                responder + " 响应: " + action + (detail != null ? " - " + detail : ""),
                source: responder);
        }

        public void LogMilestone(string message)
        {
            Log(LogCategory.Business, LogLevel.OK, message, source: "Business");
        }

        public void LogException(string context, Exception ex)
        {
            Log(LogCategory.System, LogLevel.Fault,
                context + " 异常: " + ex.Message, ex: ex, source: "System");
        }

        public void LogOperator(string action, string detail = null)
        {
            Log(LogCategory.Operator, LogLevel.Info, action + (detail != null ? " - " + detail : ""),
                source: "Operator/UI");
        }

        #endregion

        #region 查询

        public List<LogEntry> RecentLogs(int n = 200)
        {
            lock (_memLock)
            {
                return _memory.Reverse().Take(n).ToList();
            }
        }

        public List<LogEntry> QueryByTrace(string traceId)
        {
            if (string.IsNullOrEmpty(traceId)) return new List<LogEntry>();
            lock (_memLock)
            {
                return _memory.Where(e => e.TraceId == traceId).ToList();
            }
        }

        public int MemoryCount
        {
            get { lock (_memLock) { return _memory.Count; } }
        }

        #endregion

        #region 导出

        public bool ExportCsv(string filePath, IEnumerable<LogEntry> entries = null)
        {
            try
            {
                var list = entries?.ToList() ?? RecentLogs(int.MaxValue);
                var sb = new StringBuilder();
                sb.AppendLine("时间,类别,等级,TraceId,信号,旧值,新值,描述,来源,异常,哈希");
                foreach (var e in list)
                {
                    sb.Append(e.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff")).Append(",");
                    sb.Append(e.Category).Append(",");
                    sb.Append(e.Level).Append(",");
                    sb.Append(Esc(e.TraceId)).Append(",");
                    sb.Append(Esc(e.Signal)).Append(",");
                    sb.Append(Esc(e.OldValue)).Append(",");
                    sb.Append(Esc(e.NewValue)).Append(",");
                    sb.Append(Esc(e.Message)).Append(",");
                    sb.Append(Esc(e.Source)).Append(",");
                    sb.Append(Esc(e.Exception)).Append(",");
                    sb.Append(Esc(e.Hash));
                    sb.AppendLine();
                }
                File.WriteAllText(filePath, sb.ToString(), new UTF8Encoding(true));
                return true;
            }
            catch { return false; }
        }

        private static string Esc(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            if (s.Contains(",") || s.Contains("\"") || s.Contains("\n"))
                return "\"" + s.Replace("\"", "\"\"") + "\"";
            return s;
        }

        #endregion

        #region 内部

        private void AddToMemory(LogEntry e)
        {
            lock (_memLock)
            {
                _memory.AddLast(e);
                while (_memory.Count > MaxMemoryCache) _memory.RemoveFirst();
            }
        }

        private string ComputeHash(LogEntry e)
        {
            try
            {
                using (var sha = SHA256.Create())
                {
                    string raw = _lastHash + "|" + e.Id + "|" + e.Timestamp.Ticks + "|" +
                                 (int)e.Category + "|" + (int)e.Level + "|" + e.Message + "|" +
                                 e.Signal + "|" + e.NewValue + "|" + e.TraceId;
                    byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(raw));
                    var hex = BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
                    _lastHash = hex;
                    return hex.Substring(0, 16);
                }
            }
            catch { return ""; }
        }

        private void FlushLoop()
        {
            while (!_stopFlag)
            {
                try
                {
                    Thread.Sleep(FlushIntervalMs);
                    FlushOnce();
                }
                catch { }
            }
        }

        private static readonly object _fileLock = new object();

        private void FlushOnce()
        {
            if (_queue.IsEmpty) return;
            var batch = new List<LogEntry>();
            while (_queue.TryDequeue(out var e) && batch.Count < 500) batch.Add(e);
            if (batch.Count == 0) return;

            var groups = batch.GroupBy(e => e.Timestamp.ToString("yyyy-MM-dd"));
            foreach (var grp in groups)
            {
                try
                {
                    string monthDir = Path.Combine(LogRoot,
                        DateTime.Parse(grp.Key).ToString("yyyy-MM"));
                    if (!Directory.Exists(monthDir)) Directory.CreateDirectory(monthDir);
                    string filePath = Path.Combine(monthDir, grp.Key + ".log");

                    var sb = new StringBuilder();
                    foreach (var e in grp) sb.AppendLine(e.ToLine());

                    lock (_fileLock)
                    {
                        File.AppendAllText(filePath, sb.ToString(), Encoding.UTF8);
                    }
                }
                catch { }
            }
        }

        #endregion
    }
}

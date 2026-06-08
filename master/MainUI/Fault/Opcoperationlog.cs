using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using RW.UI;

namespace MainUI.Fault
{
    /// <summary>
    /// OPC 写入操作日志（独立文件 logs/opc_yyyyMMdd.log）。
    ///
    /// 设计原则：
    ///   1. 对生产路径零侵入：只在 OPC 写入入口"成功调用 Write 之后"追加一行，
    ///      任何异常都被吞掉，绝不影响 OPC 下发主流程；
    ///   2. 控件类型无关：不关心 SunnyUI / 自定义 / 系统控件，
    ///      统一靠 OperationContext 把"哪个窗体哪个控件"带过来；
    ///   3. 异步写盘：操作产生写入很快(人按按钮)，主线程入队就走，
    ///      后台线程批量刷盘，避免锁竞争；
    ///   4. 总开关 Enabled，默认 true；问题排查时可临时关闭。
    /// </summary>
    public static class OpcOperationLog
    {
        /// <summary>总开关。默认开启；如需排查可临时置 false。</summary>
        public static bool Enabled = true;

        /// <summary>是否启用 StackTrace 兜底（没有 OperationContext 时）。可关闭以提升性能。</summary>
        public static bool StackTraceFallback = true;

        private static readonly BlockingCollection<string> _queue =
            new BlockingCollection<string>(new ConcurrentQueue<string>(), 10000);

        private static readonly object _fileLock = new object();
        private static Thread _writer;
        private static volatile bool _started;
        private static string _logDir;

        /// <summary>
        /// 程序启动时调用一次。一般和 GlobalClickLogger.Start() 放在一起。
        /// </summary>
        public static void Start()
        {
            if (_started) return;
            _started = true;

            try
            {
                _logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                Directory.CreateDirectory(_logDir);
            }
            catch { /* 目录失败就降级，写入时再尝试 */ }

            _writer = new Thread(WriterLoop)
            {
                IsBackground = true,
                Name = "OpcOperationLog-Writer"
            };
            _writer.Start();

            // 写一条启动标记，方便确认生效
            Enqueue(BuildLine("系统", "-", "-", "-", "OpcOperationLog.Start", "-", "-"));
        }

        /// <summary>
        /// OPC 写入埋点入口。在 AOGrp / DOGrp 等 setter 调 this.Write(...) 成功之后调用。
        ///
        /// 用法示例（DOGrp 索引器 setter）：
        ///     set
        ///     {
        ///         this.Write("DO." + key, value);
        ///         OpcOperationLog.LogWrite("DO." + key, value);
        ///     }
        /// </summary>
        public static void LogWrite(string opcTag, object value)
        {
            if (!Enabled) return;

            // 如果在 SuppressLog 上下文里，直接跳过
            if (OperationContext.IsSuppressed) return;

            try
            {
                var ctx = OperationContext.Current;

                string user = TryGetUser();
                string formName, formTitle, ctrlName, ctrlType, operation, source;

                if (ctx != null)
                {
                    // 有显式上下文：用户已主动包 using 块，信息最准
                    formName = ctx.FormName;
                    formTitle = ctx.FormTitle;
                    ctrlName = ctx.ControlName;
                    ctrlType = ctx.ControlType;
                    operation = ctx.Operation;
                    source = "ctx"; // 改造已完成，信息齐全
                }
                else if (StackTraceFallback)
                {
                    // 兜底：用 StackTrace 反推调用方
                    string caller = GetCallerLocation();
                    formName = "-";
                    formTitle = "-";
                    ctrlName = "-";
                    ctrlType = "-";
                    operation = caller;
                    source = "stack"; // 兜底机制，看方法名再判断是"漏改造"还是"自动任务"
                }
                else
                {
                    formName = "-";
                    formTitle = "-";
                    ctrlName = "-";
                    ctrlType = "-";
                    operation = "-";
                    source = "none";
                }

                string valStr = value == null ? "(null)" : value.ToString();

                string line = BuildLine(user, formTitle, formName,
                                        ctrlType + "/" + ctrlName,
                                        operation, opcTag, valStr,
                                        source);
                Enqueue(line);
            }
            catch
            {
                /* 日志任何异常都不准上抛 */
            }
        }


        /// <summary>
        /// 非OPC配置变更日志，detail 由调用方自己拼。
        /// </summary>
        public static void LogConfig(string operation, string detail)
        {
            if (!Enabled) return;
            try
            {
                var ctx = OperationContext.Current;
                string line = BuildLine(
                    TryGetUser(),
                    ctx != null ? ctx.FormTitle : "-",
                    ctx != null ? ctx.FormName : "-",
                    "config/-",
                    operation,
                    "-",
                    Safe(detail),
                    "config");
                Enqueue(line);
            }
            catch { }
        }

        /// <summary>
        /// 用反射把任意对象的所有公共属性拼成 "Key=Value Key=Value" 字符串，
        /// 再交给 LogConfig 写入日志，省去手动列举字段。
        /// </summary>
        public static void LogConfigObject(string operation, object obj)
        {
            if (!Enabled || obj == null) return;
            try
            {
                var sb = new StringBuilder();
                foreach (var prop in obj.GetType().GetProperties(
                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    try
                    {
                        var val = prop.GetValue(obj, null);
                        sb.Append(prop.Name).Append('=').Append(val).Append(' ');
                    }
                    catch { }
                }
                LogConfig(operation, sb.ToString().TrimEnd());
            }
            catch { }
        }

        /// <summary>
        /// 字符串值的写入（OpcExChangeSend 等场景）。
        /// </summary>
        public static void LogWriteString(string opcTag, string value)
        {
            LogWrite(opcTag, (object)value);
        }

        // ──────────────────────────────────────────────────────────────────
        // 内部实现
        // ──────────────────────────────────────────────────────────────────

        private static string TryGetUser()
        {
            try
            {
                return RWUser.User != null && !string.IsNullOrEmpty(RWUser.User.Username)
                    ? RWUser.User.Username
                    : "未登录";
            }
            catch { return "-"; }
        }

        /// <summary>
        /// 反推调用者：跳过 OpcOperationLog 自身、AOGrp/DOGrp setter，
        /// 找到第一个不在 MainUI.Modules 命名空间的栈帧。
        /// 仅在无上下文时调用，频率不高，开销可接受。
        /// </summary>
        private static string GetCallerLocation()
        {
            try
            {
                var st = new StackTrace(false);
                for (int i = 0; i < st.FrameCount; i++)
                {
                    var m = st.GetFrame(i).GetMethod();
                    if (m == null || m.DeclaringType == null) continue;
                    string ns = m.DeclaringType.Namespace ?? "";
                    string typeName = m.DeclaringType.Name ?? "";

                    // 跳过自己 + 各 Grp 模块
                    if (typeName == "OpcOperationLog") continue;
                    if (ns.StartsWith("MainUI.Modules")) continue;
                    if (ns.StartsWith("RW.Modules")) continue;

                    return typeName + "." + m.Name;
                }
            }
            catch { }
            return "-";
        }

        private static string BuildLine(string user, string formTitle, string formName,
                                        string control, string operation,
                                        string opcTag, string value)
        {
            return BuildLine(user, formTitle, formName, control, operation, opcTag, value, "-");
        }

        private static string BuildLine(string user, string formTitle, string formName,
                                        string control, string operation,
                                        string opcTag, string value, string source)
        {
            // 格式：时间 | 用户 | 窗体 | 控件 | 操作 | OPC | 值 | 来源
            return string.Format(
                "[{0:yyyy-MM-dd HH:mm:ss.fff}] 用户={1,-10} 窗体={2}({3})  控件={4}  操作={5}  OPC={6}  值={7}  来源={8}",
                DateTime.Now, user,
                Safe(formTitle), Safe(formName),
                Safe(control),
                Safe(operation),
                Safe(opcTag),
                Safe(value),
                Safe(source));
        }

        private static string Safe(string s)
        {
            if (string.IsNullOrEmpty(s)) return "-";
            // 防止换行/管道污染单行日志
            return s.Replace("\r", " ").Replace("\n", " ").Replace("|", "/");
        }

        private static void Enqueue(string line)
        {
            try { _queue.TryAdd(line); }
            catch { /* 队列满或已 dispose 不抛 */ }
        }

        private static void WriterLoop()
        {
            var buffer = new StringBuilder(8192);
            try
            {
                foreach (var line in _queue.GetConsumingEnumerable())
                {
                    buffer.Length = 0;
                    buffer.AppendLine(line);

                    // 批量合并：尽量一次 IO 把队列里现存的也带走
                    string more;
                    int batch = 0;
                    while (batch < 64 && _queue.TryTake(out more))
                    {
                        buffer.AppendLine(more);
                        batch++;
                    }

                    WriteToFile(buffer.ToString());
                }
            }
            catch
            {
                /* 写线程任何异常都不影响主程序 */
            }
        }

        private static void WriteToFile(string content)
        {
            try
            {
                if (string.IsNullOrEmpty(_logDir))
                {
                    _logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                    Directory.CreateDirectory(_logDir);
                }

                string path = Path.Combine(_logDir,
                    string.Format("手动日志_{0:yyyyMMdd}.log", DateTime.Now));

                lock (_fileLock)
                {
                    File.AppendAllText(path, content, Encoding.UTF8);
                }
            }
            catch
            {
                /* 写盘失败也不能影响主程序 */
            }
        }
    }
}
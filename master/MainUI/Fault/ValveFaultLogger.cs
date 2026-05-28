using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MainUI.Modules;

namespace MainUI.Fault
{
    /// <summary>
    /// 阀门故障日志：写到 Application.StartupPath\Logs\ValveFault\yyyyMMdd.log
    /// </summary>
    public static class ValveFaultLogger
    {
        private static readonly object _writeLock = new object();

        private static string GetLogPath()
        {
            var dir = Path.Combine(Application.StartupPath, "Logs", "ValveFault");
            try { if (!Directory.Exists(dir)) Directory.CreateDirectory(dir); } catch { }
            return Path.Combine(dir, DateTime.Now.ToString("yyyyMMdd") + ".log");
        }

        public static void LogFault(string ctrlPoint, bool openLimit, bool closeLimit, string reason)
        {
            string line = string.Format(
                "{0:yyyy-MM-dd HH:mm:ss.fff} | FAULT   | {1,-25} | 开到位={2} 关到位={3} | {4}",
                DateTime.Now, ctrlPoint,
                openLimit ? 1 : 0, closeLimit ? 1 : 0,
                reason ?? "");
            WriteLine(line);
        }

        public static void LogRecover(string ctrlPoint, ValveDisplayState newState, bool openLimit, bool closeLimit)
        {
            string line = string.Format(
                "{0:yyyy-MM-dd HH:mm:ss.fff} | RECOVER | {1,-25} | 恢复至 {2} | 开到位={3} 关到位={4}",
                DateTime.Now, ctrlPoint, newState,
                openLimit ? 1 : 0, closeLimit ? 1 : 0);
            WriteLine(line);
        }

        private static void WriteLine(string line)
        {
            lock (_writeLock)
            {
                try { File.AppendAllText(GetLogPath(), line + Environment.NewLine, Encoding.UTF8); }
                catch { }
            }
        }
    }
}
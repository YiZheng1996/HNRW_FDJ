using System;
using System.Windows.Forms;
using MainUI.Demo.Core;
using MainUI.Demo.UI;

namespace MainUI.Demo
{
    /// <summary>
    /// MainUI.Demo 程序入口。
    /// 独立的 WinForms 应用，不依赖主程序运行。
    /// </summary>
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 全局异常处理
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                try
                {
                    SimulationLogger.Instance.LogException("UnhandledException",
                        e.ExceptionObject as Exception ?? new Exception("Unknown"));
                }
                catch { }
            };
            Application.ThreadException += (s, e) =>
            {
                try { SimulationLogger.Instance.LogException("ThreadException", e.Exception); }
                catch { }
            };

            // 启动日志服务
            SimulationLogger.Instance.Start();

            Application.Run(new FrmDemoMain());
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MainUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 日志在登录前启动，登录界面的操作也会被记录
            GlobalClickLogger.Instance.Start();
            MainUI.Fault.OpcOperationLog.Start();

            frmLogin login = new frmLogin
            {
                Icon = new System.Drawing.Icon("logo.ico")
            };

            var files = Directory.GetFiles(Application.StartupPath, "logo.*");
            var f = files.Where(x => !x.Contains("logo.ico")).FirstOrDefault();
            if (f != null)
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(f);
                login.Logo.Image = image;
                login.Logo.SizeMode = PictureBoxSizeMode.Zoom;
            }

            #region 单例模式
            string softname = Application.ProductName;
            bool flag = false;
            System.Threading.Mutex mutex =
                new System.Threading.Mutex(true, softname, out flag);
            if (!flag)
            {
                MessageBox.Show("只能运行一个程序！", "请确定",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Environment.Exit(0);
            }
            #endregion

            // 登录 + 试验信息确认
            DialogResult dr = login.ShowDialog();
            if (dr == DialogResult.OK)
            {
                try
                {
                    var initResult = Var.Init();
                    if (!initResult) return;

                    frmMainMenu main = new frmMainMenu
                    {
                        Icon = new System.Drawing.Icon("logo.ico")
                    };
                    if (f != null)
                    {
                        System.Drawing.Image image = System.Drawing.Image.FromFile(f);
                        main.Logo.Image = image;
                    }
                    Application.Run(main);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("OPC初始化失败" + ex.Message, "系统提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
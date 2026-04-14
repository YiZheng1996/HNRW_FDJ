using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using RW.UI;
using RW.UI.Manager.User;
using System.IO;

namespace MainUI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmLogin login = new frmLogin();
            login.Icon = new System.Drawing.Icon("logo.ico");
            var files = Directory.GetFiles(Application.StartupPath, "logo.*");
            var f = files.Where(x => !x.Contains("logo.ico")).FirstOrDefault();
            if (f != null)
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(f);//登录界面和主界面的图片
                login.Logo.Image = image;
                login.Logo.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            #region 单例模式
            string softname = Application.ProductName;
            bool flag = false;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, softname, out flag);
            //第一个参数:true--给调用线程赋予互斥体的初始所属权  
            //第一个参数:互斥体的名称  
            //第三个参数:返回值,如果调用线程已被授予互斥体的初始所属权,则返回true  
            if (!flag)
            {
                MessageBox.Show("只能运行一个程序！", "请确定", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Environment.Exit(0);
            }
            #endregion

            DialogResult dr = login.ShowDialog();
            if (dr == DialogResult.OK)
            {
                try
                {
                    var initResult = Var.Init();
                    if (!initResult) return;
                    //Var.Connect();
                    //MainForm main = new MainForm();
                    //main.Show();

                    frmMainMenu main = new frmMainMenu();
                    main.Icon = new System.Drawing.Icon("logo.ico");
                    if (f != null)
                    {
                        System.Drawing.Image image = System.Drawing.Image.FromFile(f);//登录界面和主界面的图片
                        main.Logo.Image = image;
                    }
                    Application.Run(main);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("OPC初始化失败"+ ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}

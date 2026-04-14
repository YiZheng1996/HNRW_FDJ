using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.TestScreen
{
    /// <summary>
    /// 手动/自动主界面
    /// </summary>
    public partial class ucHMI : UserControl
    {
        ConcurrentDictionary<string, UserControl> DataValue = new ConcurrentDictionary<string, UserControl>() { };

        public ucHMI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            ucAutoHMI autoHMI = new ucAutoHMI();
            autoHMI.Init();
            DataValue.AddOrUpdate("Auto", autoHMI, (k, oldValue) => autoHMI);
            this.panel1.Controls.Add(autoHMI);

            ucFormMainControl manualHMI = new ucFormMainControl();
            manualHMI.Init();
            DataValue.AddOrUpdate("Manual", manualHMI, (k, oldValue) => manualHMI);
            this.panel1.Controls.Add(manualHMI);

            ucWarnList1.Init();
        }

        /// <summary>
        /// 手/自动 页面切换
        /// </summary>
        public void SelectPage(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key) || DataValue.IsEmpty)
                    return;

                DataValue["Auto"]?.Hide();
                DataValue["Manual"]?.Hide();

                if (key == "Auto" && DataValue.ContainsKey("Auto"))
                {
                    DataValue["Auto"].Visible = true;
                    DataValue["Auto"].BringToFront();
                }
                else if (key == "Manual" && DataValue.ContainsKey("Manual"))
                {
                    DataValue["Manual"].Visible = true;
                    DataValue["Manual"].BringToFront();
                }
            }
            catch (Exception ex)
            {
                // 记录日志或处理异常
                Var.MsgBoxWarn(this, $"页面切换错误: {ex.Message}");
            }
        }
    }
}

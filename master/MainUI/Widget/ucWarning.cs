using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MainUI.Model;
using RW.UI.Controls;
using RW.EventLog;
using RW.UI;

namespace MainUI.Widget
{
    public partial class ucWarning : UserControl
    {
        public ucWarning()
        {
            InitializeComponent();
        }

        private void ucWarning_Load(object sender, EventArgs e)
        {
        }

        [Description("当点击故障复位时触发该事件")]
        public event Action FaultReset;

        // 启动柜故障
        // 一级故障列表
        Dictionary<string, Label> QDdicFault1 = new Dictionary<string, Label>();
        // 二级故障列表
        Dictionary<string, Label> QDdicFault2 = new Dictionary<string, Label>();
        // 三级故障列表
        Dictionary<string, Label> QDdicFault3 = new Dictionary<string, Label>();


        public void InitFault()
        {
            this.flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.Controls.Clear();
            QDdicFault1.Clear();
            QDdicFault2.Clear();
            QDdicFault3.Clear();

            // 使用统一的公共方法创建故障标签
            //CreateFaultLabels(Config.ConfigManager.FaultConfig.QDdicFault1, QDdicFault1);
            //CreateFaultLabels(Config.ConfigManager.FaultConfig.QDdicFault2, QDdicFault2);
            //CreateFaultLabels(Config.ConfigManager.FaultConfig.QDdicFault3, QDdicFault3);

            this.flowLayoutPanel1.ResumeLayout();

            Common.startPLCGrp.FaultgrpChanged1 += Testbed_FaultgrpChanged1;
            Common.startPLCGrp.FaultgrpChanged2 += Testbed_FaultgrpChanged2;
            Common.startPLCGrp.FaultgrpChanged3 += Testbed_FaultgrpChanged3;
        }

        ///// <summary>
        ///// 创建故障标签的公共方法 (核心优化)
        ///// 此方法通过参数化配置来源和目标字典，消除了三个几乎完全相同的循环代码块。
        ///// </summary>
        ///// <param name="faultConfig">故障配置字典</param>
        ///// <param name="targetDictionary">目标字典（dicFault1/2/3）</param>
        //private void CreateFaultLabels(Dictionary<string, Config.FaultEntity> faultConfig, Dictionary<string, Label> targetDictionary)
        //{
        //    // 参数有效性检查，增强代码健壮性
        //    if (faultConfig == null || targetDictionary == null)
        //        return;

        //    foreach (var configItem in faultConfig)
        //    {
        //        // 创建标签并设置基本属性
        //        Label label = new Label
        //        {
        //            Tag = configItem.Key,
        //            Text = configItem.Value.FaultName,
        //            Font = new Font("微软雅黑", 15),
        //            BorderStyle = BorderStyle.FixedSingle,
        //            AutoSize = true,
        //            Margin = new Padding(0, 0, 10, 0),
        //            Visible = false // 初始状态为不可见
        //        };

        //        // 将标签添加到布局面板和对应的字典中
        //        this.flowLayoutPanel1.Controls.Add(label);
        //        targetDictionary.Add(label.Tag.ToString(), label);
        //    }
        //}

        /// <summary>
        /// 统一处理故障状态变化的事件方法 (可选优化)
        /// 此方法将三个事件处理逻辑统一，进一步减少重复。
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="index">故障索引</param>
        /// <param name="value">故障状态值</param>
        /// <param name="faultDictionary">对应的故障字典</param>
        /// <param name="faultLevel">故障级别（用于生成Key）</param>
        private void HandleFaultStateChanged(object sender, int index, bool value, Dictionary<string, Label> faultDictionary, int faultLevel)
        {
            int sequenceNumber = index + 1;
            // 生成Key，格式为：故障级别（1位） + 序号（3位，左补零）
            string key = $"{faultLevel}{sequenceNumber:D3}";

            if (faultDictionary.ContainsKey(key))
            {
                Label faultLabel = faultDictionary[key];
                if (value)
                {
                    // 故障激活状态
                    this.Invoke(new Action(() =>
                    {
                        faultLabel.Visible = true;
                        faultLabel.BackColor = Color.Salmon;
                    }));
                   
                    EventLogHelper.Log(EventLogType.Warning, RWUser.User.Username, faultLabel.Text);
                }
                else
                {
                    // 故障恢复正常状态
                    this.Invoke(new Action(() =>
                    {
                        faultLabel.BackColor = SystemColors.Control;
                        faultLabel.Visible = false;
                    }));
              
                }
            }
        }

        // 以下三个事件处理器现在可以调用统一的处理方法
        private void Testbed_FaultgrpChanged1(object sender, int index, bool value)
        {
            HandleFaultStateChanged(sender, index, value, QDdicFault1, 1);
        }

        private void Testbed_FaultgrpChanged2(object sender, int index, bool value)
        {
            HandleFaultStateChanged(sender, index, value, QDdicFault2, 2);
        }

        private void Testbed_FaultgrpChanged3(object sender, int index, bool value)
        {
            HandleFaultStateChanged(sender, index, value, QDdicFault3, 3);
        }

        private void btnFaultLog_Click(object sender, EventArgs e)
        {
            frmEventLogs frmlog = new frmEventLogs();
            frmlog.ShowDialog();
        }

        private void btnFaultReset_Click(object sender, EventArgs e)
        {
            FaultReset?.Invoke();
        }
    }
}
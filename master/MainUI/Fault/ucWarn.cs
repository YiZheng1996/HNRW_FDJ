using MainUI.Fault.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Fault
{
    public partial class ucWarn : UserControl
    {
        public ucWarn()
        {
            InitializeComponent();
        }

        private string _key = "";
        /// <summary>
        /// 标识
        /// </summary>
        [Description("标识")]
        [DefaultValue("")]
        public string Key
        {
            get { return _key; }
            set
            {
                _key = value;
            }
        }

        private string _fTitle = "";
        /// <summary>
        /// 标题
        /// </summary>
        [Description("标题")]
        [DefaultValue("")]
        public string Title
        {
            get { return _fTitle; }
            set
            {
                lblTitle.Text = value;
                _fTitle = value;
            }
        }

        private bool _showAlarmButton = true;
        /// <summary>
        /// 显示/隐藏报警按钮
        /// </summary>
        [Description("显示/隐藏报警按钮")]
        [DefaultValue(true)]
        public bool ShowAlarmButton
        {
            get { return _showAlarmButton; }
            set
            {
                _showAlarmButton = value;
                rbtAlarm.Visible = value;
            }
        }

        private bool _showSheddingButton = true;
        /// <summary>
        /// 显示/隐藏甩负荷按钮
        /// </summary>
        [Description("显示/隐藏甩负荷按钮")]
        [DefaultValue(true)]
        public bool ShowSheddingButton
        {
            get { return _showSheddingButton; }
            set
            {
                _showSheddingButton = value;
                rbtShedding.Visible = value;
            }
        }

        private bool _showStopButton = true;
        /// <summary>
        /// 显示/隐藏停机按钮
        /// </summary>
        [Description("显示/隐藏停机按钮")]
        [DefaultValue(true)]
        public bool ShowStopButton
        {
            get { return _showStopButton; }
            set
            {
                _showStopButton = value;
                rbtStop.Visible = value;
            }
        }

        private WarnTypeEnum _currentFault = WarnTypeEnum.None;
        /// <summary>
        /// 当前故障类型
        /// </summary>
        [Description("当前故障类型")]
        [DefaultValue(WarnTypeEnum.None)]
        public WarnTypeEnum CurrentFault
        {
            get { return _currentFault; }
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        _currentFault = value;
                        UpdateFaultDisplay();
                    }));
                }
                else
                {
                    _currentFault = value;
                    UpdateFaultDisplay();
                }
            }
        }

        /// <summary>
        /// 更新故障显示
        /// </summary>
        private void UpdateFaultDisplay()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(UpdateFaultDisplay));
                return;
            }

            // 重置所有按钮颜色为默认颜色 （不重置颜色）
            //rbtAlarm.Switch = false;
            //rbtShedding.Switch = false; //SystemColors.ControlLight;
            //rbtStop.Switch = false; //SystemColors.ControlLight;

            // 根据当前故障类型设置对应按钮颜色为红色
            switch (_currentFault)
            {
                case WarnTypeEnum.Alarm:
                    rbtAlarm.Switch = true;
                    break;
                case WarnTypeEnum.Shedding:
                    rbtShedding.Switch = true;
                    break;
                case WarnTypeEnum.Stop:
                    rbtStop.Switch = true;
                    break;
                case WarnTypeEnum.None:
                default:
                    // 保持默认颜色
                    break;
            }
        }

        /// <summary>
        /// 重置三级故障按钮状态颜色为默认颜色
        /// </summary>
        public void RestartSwitch() 
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(RestartSwitch));
                return;
            }

            rbtAlarm.Switch = false;
            rbtShedding.Switch = false; //SystemColors.ControlLight;
            rbtStop.Switch = false; //SystemColors.ControlLight;
        }
    }

    ///// <summary>
    ///// 故障类型枚举
    ///// </summary>
    //public enum WarnType
    //{
    //    /// <summary>
    //    /// 无故障
    //    /// </summary>
    //    None,
    //    /// <summary>
    //    /// 报警
    //    /// </summary>
    //    Alarm,
    //    /// <summary>
    //    /// 降载
    //    /// </summary>
    //    Shedding,
    //    /// <summary>
    //    /// 停机
    //    /// </summary>
    //    Stop
    //}
}
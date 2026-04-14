using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MainUI.Widget
{
    /// <summary>
    /// 工况切换前窗体
    /// </summary>
    public partial class FrmGKChangeInfo : Form
    {
        private int _countdownSeconds = 15; // 倒计时秒数

        public FrmGKChangeInfo()
        {
            InitializeComponent();
        }

        public FrmGKChangeInfo(string title, string msg)
        {
            InitializeComponent();
        }

        /// <summary>
        /// 测试阶段
        /// </summary>
        private string _testStep;
        public string TestStep
        {
            get
            {
                return _testStep;
            }
            set
            {
                _testStep = value;
                this.lblTestStep.Text = value;
            }
        }

        /// <summary>
        /// 测试时间
        /// </summary>
        private string _testTime;
        public string TestTime
        {
            get
            {
                return _testTime;
            }
            set
            {
                _testTime = value;
                this.lblTestTime.Text = value;
            }
        }

        /// <summary>
        /// 旧扭矩
        /// </summary>
        private string _oldTorque;
        public string OldTorque
        {
            get
            {
                return _oldTorque;
            }
            set
            {
                _oldTorque = value;
                this.lblOldTorque.Text = value;
            }
        }

        /// <summary>
        /// 新扭矩
        /// </summary>
        private string _newTorque;
        public string NewTorque
        {
            get
            {
                return _newTorque;
            }
            set
            {
                _newTorque = value;
                this.lblNewTorque.Text = value;
            }
        }

        /// <summary>
        /// 旧转速
        /// </summary>
        private string _oldRpm;
        public string OldRPM
        {
            get
            {
                return _oldRpm;
            }
            set
            {
                _oldRpm = value;
                this.lblOldRPM.Text = value; // 修正：应该设置控件文本
            }
        }

        /// <summary>
        /// 新转速
        /// </summary>
        private string _newRpm;
        public string NewRPM
        {
            get
            {
                return _newRpm;
            }
            set
            {
                _newRpm = value;
                this.lblNewRPM.Text = value; // 修正：应该设置控件文本
            }
        }

        /// <summary>
        /// 更换时间
        /// </summary>
        private string _changeTime;
        public string ChangeTime
        {
            get
            {
                return _changeTime;
            }
            set
            {
                _changeTime = value;
                this.lblChangeTime.Text = value; // 修正：应该设置控件文本
            }
        }

        /// <summary>
        /// 是否正在弹窗
        /// </summary>
        private bool _isOpen;
        public bool IsOpen
        {
            get
            {
                return _isOpen;
            }
            set
            {
                _isOpen = value;
            }
        }


        private int x, y;
        private AlertFormAction action;

        /// <summary>
        /// 计时器
        /// 进行倒计时与退出的逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (action)
            {
                case AlertFormAction.Start:
                    // 倒计时逻辑
                    _countdownSeconds--;
                    this.lblChangeTime.Text = _countdownSeconds.ToString() + "秒";

                    if (_countdownSeconds <= 0)
                    {
                        action = AlertFormAction.Close;
                    }
                    break;
                case AlertFormAction.Close:
                    // 切换状态（避免重复调用Close）
                    IsOpen = false;
                    action = AlertFormAction.Wait;
                    this.timer1.Enabled = false;

                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 弹出窗体 
        /// </summary>
        /// <param name="speed">本工况转速</param>
        /// <param name="torque">本工况扭矩</param>
        /// <param name="nextSpeed">下一步转速</param>
        /// <param name="nextTorque">下一步转速</param>
        /// <param name="time">下一步时间</param>
        /// <param name="gkNo">下一步工况编号</param>
        public void ShowInfo(double speed, double torque, double nextSpeed, double nextTorque, double time,string gkNo)
        {
            // 重置倒计时
            _countdownSeconds = 15;
            this.lblChangeTime.Text = _countdownSeconds.ToString() + "秒";
            this.lblChangeTime.ForeColor = System.Drawing.Color.DarkRed;

            OldTorque = torque.ToString();
            OldRPM = speed.ToString();
            NewTorque = nextTorque.ToString();
            NewRPM = nextSpeed.ToString();
            TestTime = time.ToString();
            TestStep = gkNo;

            // 设置窗口启始位置为屏幕中央
            this.StartPosition = FormStartPosition.CenterScreen;

            /// 自定义 设置窗口启始位置
            //this.StartPosition = FormStartPosition.Manual;
            //this.x = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            //this.y = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            //this.Location = new Point(x, y);
            //this.Opacity = 0.0;

            base.Show();

            //启动时钟
            timer1.Enabled = true;
            timer1.Start();

            IsOpen = true;
            action = AlertFormAction.Start;
        }

        /// <summary>
        /// 退出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            action = AlertFormAction.Close;
        }
    }

    /// <summary>
    /// 工况切换计时器状态
    /// </summary>
    public enum AlertFormAction
    {
        /// <summary>
        /// 开始倒计时
        /// </summary>
        Start,
        /// <summary>
        /// 等待（）
        /// </summary>
        Wait,
        /// <summary>
        /// 关闭窗体
        /// </summary>
        Close,
    }
}
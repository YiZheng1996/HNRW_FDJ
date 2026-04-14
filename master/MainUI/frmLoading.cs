using RW.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI
{
    /// <summary>
    /// 登录后的加载页面
    /// </summary>
    public partial class frmLoading : Form //RWBaseForm
    {
        /// <summary>
        /// 页面加载完成，是否自动关闭。默认自动关闭
        /// </summary>
        public bool AutoClose { get; set; } = true;
        /// <summary>
        /// 加载页面
        /// </summary>
        public frmLoading()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void FrmLoading_InitBefore(string obj)
        {
            throw new NotImplementedException();
        }

        private void frmLoading_Load(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";

            ThreadPool.QueueUserWorkItem(delegate
            {
                bool result = Init();
                if (result && AutoClose)
                {
                    this.LogLine();
                    this.AddText("准备进入主界面。");
                    Thread.Sleep(1000);
                    ControlHelper.Invoke(this, delegate
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    });
                }
                else
                {
                    this.button1.Enabled = true;
                }
            });
        }


        /// <summary>
        /// 添加一行
        /// </summary>
        public void LogLine()
        {
            this.richTextBox1.AppendText("\r\n");
        }

        /// <summary>
        /// 事件初始化开始的事件
        /// </summary>
        internal event Action<string> InitBefore;
        /// <summary>
        /// 事件初始化完成的事件
        /// </summary>
        public event Action<bool, double> Inited;
        /// <summary>
        /// 事件初始化错误的事件
        /// </summary>
        public event Action InitError;

        /// <summary>
        /// 添加记录日志
        /// </summary>
        public void AddText(string text)
        {
            this.richTextBox1.ScrollToCaret();
            this.richTextBox1.AppendText(text);
        }

        /// <summary>
        /// 添加一条带颜色的文本日志
        /// </summary>
        public void AddColorText(string text, Color color)
        {
            int index = this.richTextBox1.TextLength;
            int len = text.Length;
            this.richTextBox1.ScrollToCaret();
            this.richTextBox1.AppendText(text);
            this.richTextBox1.Select(index, len);
            this.richTextBox1.SelectionColor = color;
        }

        /// <summary>
        /// 添加一条日志
        /// </summary>
        public void AddTextResult(string text, bool result)
        {
            AddColorText(text, result ? Color.Green : Color.Red);
            LogLine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 通用初始化某个方法，并进行回调。调用此方法将会触发初始化事件。
        /// </summary>
        bool InvokeInited(string name, Action callback)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            try
            {
                OnInitBefore($"初始化[{name}]：");
                callback?.Invoke();
                watch.Stop();
                OnInited(true, watch.ElapsedMilliseconds);
                Thread.Sleep(50);
                return true;
            }
            catch (Exception ex)
            {
                watch.Stop();
                Inited?.Invoke(false, watch.ElapsedMilliseconds);
                LogLine();
                AddColorText("详细：" + ex.Message, Color.Red);
                LogLine();
                return false;
            }
        }

        void OnInitBefore(string msg)
        {
            if (InitBefore == null)
            {
                this.LogLine();
                this.AddText(msg);
            }
            else
                InitBefore.Invoke(msg);
        }

        void OnInited(bool result, double ms)
        {
            if (Inited == null)
            {
                this.AddColorText((result ? "成功" : "失败") + "！", result ? Color.Green : Color.Red);
                this.richTextBox1.SelectionStart = this.richTextBox1.Text.Length;
                this.richTextBox1.SelectionColor = ForeColor;
                this.AddText("用时" + ms + "毫秒。");
            }
            else
                Inited(result, ms);
        }

        //所有的动作
        Dictionary<string, Action> actions = new Dictionary<string, Action>();
        //加载顺序
        Dictionary<string, int> indexes = new Dictionary<string, int>();
        /// <summary>
        /// 添加一个加载事件
        /// </summary>
        public void AddInitInvoke(string name, Action callback)
        {
            if (actions.ContainsKey(name))
                throw new Exception("无法添加相同的事件，请先清空或");
            actions.Add(name, callback);
            indexes.Add(name, actions.Count);
        }


        bool Init()
        {
            var orderedNames = indexes.OrderBy(x => x.Value).Select(x => x.Key).ToArray();
            bool result = true;
            foreach (var item in orderedNames)
            {
                ControlHelper.Invoke(this, delegate
                {
                    result &= InvokeInited(item, actions[item]);
                });
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;
using RW.UI.Controls;
using System.Threading;
using MainUI.Config.Modules;
using System.Text.RegularExpressions;
using static MainUI.Modules.EventArgsModel;

namespace MainUI.Pipeline.Quick
{
    public partial class BeforeStart : Form
    {
        bool isWork = false;
        Dictionary<string, List<UILight>> dicLight = new Dictionary<string, List<UILight>>();
        Dictionary<string, List<RButton>> dicButton = new Dictionary<string, List<RButton>>();
        Dictionary<string, Button> dicOperateBtn = new Dictionary<string, Button>();
        Thread th;
        ValveConfig valveConfig = new ValveConfig();


        public BeforeStart()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            //控件点位绑定初始化
            EachControl(this);

            //值改变事件
            Common.DIgrp.KeyValueChange += DIgrp_KeyValueChange;
            Common.DOgrp.KeyValueChange += DOgrp_KeyValueChange;

            //控件状态初始化
            Common.DIgrp.Fresh();
            Common.DOgrp.Fresh();

            //开启计时器
            timerWork.Start();
        }

        private void DOgrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DIValueChangedEventArgs>(DOgrp_KeyValueChange), sender, e);
                return;
            }

            if (dicButton.ContainsKey(e.Key))
            {
                foreach (var item in dicButton[e.Key])
                {
                    item.Switch = e.Value == false ? false : true;
                }
            }
        }

        private void DIgrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            //泵类信号灯特殊处理
            if(e.Key == "预热水泵主接检测")
            {
                if(e.Value)
                {
                    this.uiLight9.State = UILightState.On;
                    this.uiLight10.State = UILightState.Off;
                }
                else
                {
                    this.uiLight9.State = UILightState.Off;
                    this.uiLight10.State = UILightState.On;
                }
            }
            if(e.Key == "预供机油泵合闸检测")
            {
                if (e.Value)
                {
                    this.uiLight37.State = UILightState.On;
                    this.uiLight38.State = UILightState.Off;
                }
                else
                {
                    this.uiLight37.State = UILightState.Off;
                    this.uiLight38.State = UILightState.On;
                }
            }

            //阀类信号灯
            if (dicLight.ContainsKey(e.Key))
            {
                foreach (var item in dicLight[e.Key])
                {
                    item.State = e.Value == false ? UILightState.Off : UILightState.On;
                }
            }
        }
      
        /// <summary>
        /// 添加界面所有灯和按钮
        /// </summary>
        /// <param name="con"></param>
        void EachControl(Control con)
        {
            foreach (Control item in con.Controls)
            {
                EachControl(item);
            }
            if (con is UILight)
            {
                //添加灯
                UILight light = con as UILight;
                if (light.Tag != null && light.Tag.ToString() != string.Empty)
                {
                    List<UILight> UILightList;
                    dicLight.TryGetValue(light.Tag.ToString(), out UILightList);
                    if (UILightList == null)
                    {
                        UILightList = new List<UILight>() { };
                        UILightList.Add(light);
                        dicLight.Add(light.Tag.ToString(), UILightList);
                    }
                    else
                    {
                        dicLight[light.Tag.ToString()].Add(light);
                    }
                }
            }
            if (con is RButton)
            {
                //添加按钮
                RButton btn = con as RButton;
                if (btn.Tag != null && btn.Tag.ToString() != string.Empty)
                {
                    List<RButton> ButtonList;
                    dicButton.TryGetValue(btn.Tag.ToString(), out ButtonList);
                    if (ButtonList == null)
                    {
                        ButtonList = new List<RButton>() { };
                        ButtonList.Add(btn);
                        dicButton.Add(btn.Tag.ToString(), ButtonList);
                    }
                    else
                    {
                        dicButton[btn.Tag.ToString()].Add(btn);
                    }

                }
            }
            if (con is Button)
            {
                //添加一件操作按钮
                Button opBtn = con as Button;

                dicOperateBtn.Add(opBtn.Name.ToString(), opBtn);
            }
        }


        private void timerWork_Tick(object sender, EventArgs e)
        {
            //刷新是否在操作状态
            if (isWork == false && th != null)
            {
                th.Abort();

            }
        }


        #region 一键按钮要操作的阀和泵   数字对应Valve.ini里的ValveNum
        List<string> start1 = new List<string>()
        {
            "28","29"
        };
        List<string> start2 = new List<string>()
        {
            "28","30"
        };
        List<string> start3 = new List<string>()
        {
            "28","26"
        };
        List<string> start5 = new List<string>()
        {
            "15","16","4","17","20","03","21","22"
        };
        List<string> start6 = new List<string>()
        {
            "92","95","96","97","115","39"
        };
        List<string> start7 = new List<string>()
        {
            "133","135","136","137"
        };
        List<string> start8 = new List<string>()
        {
            "179","190",
        };

        #endregion



        string btnNum = "";
        string btnStartName = "";
        string btnStopName = "";
        private void btnStart_Click(object sender, EventArgs e)
        {
            isWork = true;
            Button btn = sender as Button;
            if (btn != null)
            {
                Tag = btn.Name;
                //取Tag中的数字
                Match match = Regex.Match(Tag.ToString(), @"\d+");
                btnNum = match.Value;
            }
            switch (btnNum)
            {
                case "1":
                    {
                        ValveOperate(start1);
                        break;
                    }
                case "2":
                    {
                        ValveOperate(start2);
                        break;
                    }
                case "3":
                    {
                        ValveOperate(start3);
                        break;
                    }
                case "4":
                    {
                        //判断条件自动加热停止
                        break;
                    }
                case "5":
                    {
                        ValveOperate(start5);
                        break;
                    }
                case "6":
                    {
                        ValveOperate(start6);
                        break;
                    }
                case "7":
                    {
                        ValveOperate(start7);
                        break;
                    }
                case "8":
                    {
                        ValveOperate(start8);
                        break;
                    }
                default:
                    break;
            }
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            isWork = false;
            Stop();
        }

        /// <summary>
        /// 点击开始后的一键操作方法
        /// </summary>
        private void ValveOperate(List<string> startList)
        {
            btnStartName = "btnStart" + btnNum;     //开始按钮Name
            btnStopName = "btnStop" + btnNum;       //关闭按钮Name
            dicOperateBtn[btnStartName].Visible = false;
            dicOperateBtn[btnStopName].Visible = true;

            th = new Thread(() => Start(startList));
            th.Start();
        }

        public void Start(List<string> startList)
        {
            for (int i = 0; i < startList.Count; i++)
            {
                if (!isWork) return;
                //要执行的操作中阀信息列表
                foreach (var item in valveConfig.valveInfo)
                {
                    //查到要执行操作的ValveInfo
                    if (item.ValveNum == startList[i])
                    {
                        if (item.ValveNum == "03")
                        {
                            var result = CloseValve(item.ValvePoint, item.ValveClosePoint);
                            if (!result)
                            {
                                this.Invoke(new Action(() =>
                                {
                                    Var.MsgBoxWarn(this, $"关闭{item.ValveOpenPoint}失败");
                                    Stop();
                                }));
                            }
                        }
                        else
                        {
                            var result = OpenValve(item.ValvePoint, item.ValveOpenPoint);
                            if (!result)
                            {
                                this.Invoke(new Action(() =>
                                {
                                    Var.MsgBoxWarn(this, $"开启{item.ValveOpenPoint}失败");
                                    Stop();
                                }));
                            }
                        }
                        
                        break;
                    }
                }
            }

            this.Invoke(new Action(() =>
            {
                if (isWork)
                    Var.MsgBoxSuccess(this, $"已完成操作");
                isWork = false;
                Stop();
            }));
        }


        /// <summary>
        /// 停止方法
        /// </summary>
        public void Stop()
        {
            //九组开始停止按钮
            for (int i = 1; i < 10; i++)
            {
                dicOperateBtn[$"btnStart{i}"].Visible = true;
                dicOperateBtn[$"btnStop{i}"].Visible = false;
            }
        }


        /// <summary>
        /// 开阀方法
        /// </summary>
        private bool OpenValve(string valveName, string valveOpenName)
        {
            //开阀
            Common.DOgrp[valveName] = true;
            Thread.Sleep(3000);

            //测试用
            Common.DIgrp[valveOpenName] = true;
            Thread.Sleep(2000);

            if (Common.DIgrp[valveOpenName] == false)
            {
                //如果该阀没开到位
                isWork = false;
            }
            return isWork;
        }
        /// <summary>
        /// 关阀方法
        /// </summary>
        /// <param name="valveName">阀点位名</param>
        /// <param name="valveCloseName">阀门关到位点位名</param>
        private bool CloseValve(string valveName, string valveCloseName)
        {
            Common.DOgrp[valveName] = false;

            //测试用
            Common.DIgrp[valveCloseName] = true;
            Thread.Sleep(2000);

            if (Common.DIgrp[valveCloseName] == false)
            {
                isWork = false;
                MessageBox.Show($"开启{valveName}失败");
            }
            return isWork;
        }
    }
}

using MainUI.Modules;
using MainUI.Widget;
using RW;
using RW.UI.Controls;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainUI.Config.Modules;
using System.IO;
using System.Threading;
using MainUI.Config;
using RW.Modules;
using MainUI.Global;
using static MainUI.Modules.EventArgsModel;

namespace MainUI
{
    public partial class ucControlHMI : UserControl
    {
        SysConstantParas SysConstantParasConfig = new SysConstantParas();

        // 预热水箱温度
        int WaterTempValue = 0;

        /// <summary>
        /// 开/关到位，泵控制 字典
        /// </summary>
        Dictionary<string, List<RButton>> dicBtn = new Dictionary<string, List<RButton>>();
        /// <summary>
        /// 故障灯状态
        /// </summary>
        Dictionary<string, List<UILedBulb>> dicLight = new Dictionary<string, List<UILedBulb>>();
        /// <summary>
        /// 数值状态
        /// </summary>
        Dictionary<string, Label> dicLabel = new Dictionary<string, Label>();

        /// <summary>
        /// 通用测试列表
        /// </summary>
        public Dictionary<string, List<ContronlPoint>> _contronlMap = new Dictionary<string, List<ContronlPoint>>();

        /// <summary>
        /// 是否正在测试
        /// </summary>
        public Dictionary<string, bool> _deviceCommandMap = new Dictionary<string, bool>();

        /// <summary>
        /// 一键合闸开始点位
        /// </summary>
        Dictionary<string, RButton> dicTestBtn = new Dictionary<string, RButton>();

        string _model = null;   //存储型号;

        public ucControlHMI()
        {
            InitializeComponent();
            // 监听型号更新
            EventTriggerModel.OnModelNameChanged += EventTriggerModel_OnModelNameChanged;
        }

        /// <summary>
        /// 监听型号更新
        /// </summary>
        /// <param name="obj"></param>
        private void EventTriggerModel_OnModelNameChanged(string model)
        {
            _model = model;

            // todo 暂时先注释 根据型号更新各个pid配方值
            //// 读取配方文件 初始化控件
            //pub = new PubConfig(_model);

            //if (pub.PubParaList.Count == 0)
            //    return;
            //var pubInfo = pub.PubParaList[0];
            ////水阀出口流量
            //this.lblFlowWater.Text = pubInfo.DefaultWaterPumpOutletFlow.ToString();
            //Common.AOgrp["水泵出口电动调节阀控制-18"] = pubInfo.DefaultWaterPumpOutletFlow;
            ////燃油泵1、2流量
            //this.lblFlowRY1.Text = pubInfo.DefaultFuelPump1Flow.ToString();
            //Common.AOgrp["燃油泵1电动调节阀控制-170"] = pubInfo.DefaultFuelPump1Flow;
            //this.lblFlowRY2.Text = pubInfo.DefaultFuelPump2Flow.ToString();
            //Common.AOgrp["燃油泵旁路1电动调节阀控制-194"] = pubInfo.DefaultFuelPump2Flow;
            ////机油热交换器温度
            //this.lblTempJY.Text = pubInfo.DefaultOilTemperature.ToString();
            //Common.engineOilGrp.EngineOilPID = pubInfo.DefaultOilTemperature;
            ////中冷水热交换器温度
            //this.lblTempLWater.Text = pubInfo.DefaultMediumColdWaterTemperature.ToString();
            //Common.waterGrp.MediumColdPID = pubInfo.DefaultMediumColdWaterTemperature;
            ////高温水热交换器温度
            //this.lblTempHWater.Text = pubInfo.DefaultHighTemperatureWater.ToString();
            //Common.waterGrp.HeightTempWaterPID = pubInfo.DefaultHighTemperatureWater;

            //// 进排气
            //this.lblJQY.Text = pubInfo.DefaultIntakeDuctRightFlow.ToString();
            //Common.AOgrp["进气风道右调节阀控制"] = pubInfo.DefaultIntakeDuctRightFlow;
            //this.lblJQZ.Text = pubInfo.DefaultIntakeDuctLeftFlow.ToString();
            //Common.AOgrp["进气风道左调节阀控制"] = pubInfo.DefaultIntakeDuctLeftFlow;
            //this.lblPQY.Text = pubInfo.DefaultExhaustDuctRight.ToString();
            //Common.AOgrp["排气风道右调节阀控制"] = pubInfo.DefaultExhaustDuctRight;
            //this.lblPQZ.Text = pubInfo.DefaultExhaustDuctLeft.ToString();
            //Common.AOgrp["排气风道左调节阀控制"] = pubInfo.DefaultExhaustDuctLeft;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            // 创建配置
            //CreateValveConfig();
            // 创建测试步骤
            InitDic();
            // 加载手动控制点位
            EachControl(panelManual);
            // 绑定数值
            EachValueControl();

            Common.DIgrp.KeyValueChange += DIgrp_KeyValueChange;
            Common.AOgrp.KeyValueChange += AOgrp_KeyValueChange;
            Common.waterGrp.KeyValueChange += WaterGrp_KeyValueChange;
            Common.engineOilGrp.KeyValueChange += EngineOilGrp_KeyValueChange;
            Common.PipelineFaultGrp.KeyValueChange += PipelineFaultGrp_KeyValueChange;

            // 绑定仪表盘
            this.ucParamPressure1.SetRand(0, 800, 700);
            this.ucParamPressure2.SetRand(0, 800, 700);

            timer1.Enabled = true;
            timer1.Start();
        }

        /// <summary>
        /// 故障灯的状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PipelineFaultGrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DIValueChangedEventArgs>(PipelineFaultGrp_KeyValueChange), sender, e);
                return;
            }

            if (dicLight.ContainsKey(e.Key))
            {
                if (e.Value)
                {
                    // todo 为 true时，需要记录到故障中
                }

                var list = dicLight[e.Key];
                foreach (var item in list)
                {
                    item.Visible = e.Value;
                }
            }
        }

        /// <summary>
        /// 机油数据值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EngineOilGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DoubleValueChangedEventArgs>(EngineOilGrp_KeyValueChange), sender, e);
                return;
            }
            if (dicLabel.ContainsKey(e.Key))
            {
                dicLabel[e.Key].Text = e.Value.ToString();
            }
        }

        /// <summary>
        /// 水数据值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WaterGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DoubleValueChangedEventArgs>(WaterGrp_KeyValueChange), sender, e);
                return;
            }

            if (dicLabel.ContainsKey(e.Key))
            {
                dicLabel[e.Key].Text = e.Value.ToString();
            }
        }

        /// <summary>
        /// AO值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AOgrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DoubleValueChangedEventArgs>(AOgrp_KeyValueChange), sender, e);
                return;
            }

            if (dicLabel.ContainsKey(e.Key))
            {
                dicLabel[e.Key].Text = e.Value.ToString();
            }
        }

        /// <summary>
        /// 值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DIgrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DIValueChangedEventArgs>(DIgrp_KeyValueChange), sender, e);
                return;
            }

            //泵类信号灯特殊处理
            if (e.Key == "预热水泵主接检测")
            {
                if (e.Value)
                {
                    this.btnYRSBK.Switch = true;
                    this.btnYRSBG.Switch = false;
                }
                else
                {
                    this.btnYRSBK.Switch = false;
                    this.btnYRSBG.Switch = true;
                }
            }
            else if (e.Key == "预供机油泵合闸检测")
            {
                if (e.Value)
                {
                    this.btnYGJYK.Switch = true;
                    this.btnYGJYG.Switch = false;
                }
                else
                {
                    this.btnYGJYK.Switch = false;
                    this.btnYGJYG.Switch = true;
                }
            }
            else if (e.Key == "污油排出泵主接检测")
            {
                if (e.Value)
                {
                    this.btnWYPCK.Switch = true;
                    this.btnWYPCG.Switch = false;
                }
                else
                {
                    this.btnWYPCK.Switch = false;
                    this.btnWYPCG.Switch = true;
                }
            }
            else if (e.Key == "燃油泵1合闸检测")
            {
                if (e.Value)
                {
                    this.btnRYB1K.Switch = true;
                    this.btnRYB1G.Switch = false;
                }
                else
                {
                    this.btnRYB1K.Switch = false;
                    this.btnRYB1G.Switch = true;
                }
            }
            else if (e.Key == "燃油泵2合闸检测")
            {
                if (e.Value)
                {
                    this.btnRYB2K.Switch = true;
                    this.btnRYB2G.Switch = false;
                }
                else
                {
                    this.btnRYB2K.Switch = false;
                    this.btnRYB2G.Switch = true;
                }
            }
            else if (dicBtn.ContainsKey(e.Key))
            {
                foreach (var item in dicBtn[e.Key])
                {
                    item.Switch = e.Value;
                }
            }
        }

        /// <summary>
        /// 添加界面所有灯和按钮
        /// </summary>
        /// <param name="con"></param>
        private void EachControl(Control con)
        {
            foreach (Control item in con.Controls)
            {
                EachControl(item);
            }
            if (con is UILedBulb)
            {
                //添加灯
                UILedBulb light = con as UILedBulb;
                if (light.Tag != null && light.Tag.ToString() != string.Empty)
                {
                    List<UILedBulb> UILightList;
                    dicLight.TryGetValue(light.Tag.ToString(), out UILightList);
                    if (UILightList == null)
                    {
                        UILightList = new List<UILedBulb>() { };
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
                if (btn.InputTagName != null && btn.InputTagName.ToString() != string.Empty)
                {
                    List<RButton> ButtonList;
                    dicBtn.TryGetValue(btn.InputTagName.ToString(), out ButtonList);
                    if (ButtonList == null)
                    {
                        ButtonList = new List<RButton>() { };
                        ButtonList.Add(btn);
                        dicBtn.Add(btn.InputTagName.ToString(), ButtonList);
                    }
                    else
                    {
                        dicBtn[btn.InputTagName.ToString()].Add(btn);
                    }

                }
            }
        }

        /// <summary>
        /// 添加数值绑定
        /// </summary>
        /// <param name="con"></param>
        private void EachValueControl()
        {
            dicLabel.Add("机油温度设置PID", lblTempJY);
            dicLabel.Add("高温水温度设置PID", lblTempHWater);
            dicLabel.Add("中冷水温度设置PID", lblTempLWater);
            dicLabel.Add("水泵出口电动调节阀控制-18", lblFlowWater);
            dicLabel.Add("燃油泵1电动调节阀控制-170", lblFlowRY1);
            dicLabel.Add("燃油泵旁路1电动调节阀控制-194", lblFlowRY2);

            // 进排气控制 （移动至进气加热控制）
            //dicLabel.Add("进气风道右调节阀控制", lblJQY);
            //dicLabel.Add("进气风道左调节阀控制", lblJQZ);
            //dicLabel.Add("排气风道右调节阀控制", lblPQY);
            //dicLabel.Add("排气风道左调节阀控制", lblPQZ);
        }

        /// <summary>
        /// 添加界面的一键控制按钮
        /// </summary>
        /// <param name="con"></param>
        private void EachControlTest(Control con)
        {
            foreach (Control item in con.Controls)
            {
                EachControlTest(item);
            }
            if (con is RButton)
            {
                //添加按钮
                RButton btn = con as RButton;
                if (btn.Tag != null && btn.Tag.ToString() != "-")
                {
                    RButton ButtonList;
                    dicTestBtn.TryGetValue($"{btn.Tag.ToString()}_{btn.Text}", out ButtonList);
                    if (ButtonList == null)
                    {
                        ButtonList = btn;
                    }
                }
            }
        }

        /// <summary>
        /// 通用点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sw_Valve_Click(object sender, EventArgs e)
        {
            RButton sw = sender as RButton;
            if (sw.Tag == null)
            {
                Var.MsgBoxWarn(this, "程序异常，未绑定Tag值。");
                return;
            }
            if (string.IsNullOrEmpty(sw.Tag.ToString()))
            {
                Var.MsgBoxWarn(this, "程序异常，未绑定Tag值。");
                return;
            }
            if (string.IsNullOrEmpty(sw.OutputTagName.ToString()))
            {
                Var.MsgBoxWarn(this, "程序异常，未绑定OutputTagName值。");
                return;
            }
            // 没气压，不能操作
            if (ucParamPressure1.GaugeValue < 400 || ucParamPressure2.GaugeValue < 400)
            {
                Var.MsgBoxWarn(this, "厂房压力低，不能操作阀/泵。");
                return;
            }
            // 如果已经打开/关闭 则过滤
            if (sw.Switch == true)
            {
                return;
            }


            string str = sw.Text.Contains("开") ? "打开" : "关闭";
            string th = sw.OutputTagName.ToString().Replace("控制", "").Replace("合闸", "");
            string strMessage = $"是否要{str}{th}?";
            bool result = Var.MsgBoxYesNo(this, strMessage);
            if (result == false)
            {
                return;
            }
            else
            {
                try
                {
                    Common.DOgrp[sw.OutputTagName.ToString()] = Convert.ToBoolean(sw.Tag.ToInt()); //!Common.DOgrp[sw.Tag.ToString()];
                }
                catch (Exception ex)
                {

                    throw new Exception($"{th}+{str}失败！原因：" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 创建测试逻辑字典
        /// </summary>
        private void InitDic()
        {
            #region 启机前
            // HT膨胀水箱加水
            _contronlMap.Add("HT膨胀水箱加水", new List<ContronlPoint>
            {
                new ContronlPoint{ key = "28", contronl = true, check = true, checkResult = true },
                new ContronlPoint{ key = "29", contronl = true, check = true },
            });
            _deviceCommandMap.Add("HT膨胀水箱加水", false);

            // LT膨胀水箱加水
            _contronlMap.Add("LT膨胀水箱加水", new List<ContronlPoint>
            {
                new ContronlPoint{ key = "28", contronl = true, check = true },
                new ContronlPoint{ key = "30", contronl = true, check = true },
            });
            _deviceCommandMap.Add("LT膨胀水箱加水", false);

            // 预热水箱加水
            _contronlMap.Add("预热水箱加水", new List<ContronlPoint>
            {
                new ContronlPoint{ key = "28", contronl = true, check = true },
                new ContronlPoint{ key = "26", contronl = true, check = true },
            });
            _deviceCommandMap.Add("预热水箱加水", false);

            // 水加热另启逻辑
            _contronlMap.Add("预热水箱水加热", new List<ContronlPoint>
            {
            });
            _deviceCommandMap.Add("预热水箱水加热", false);

            // 高温水系统预热循环
            _contronlMap.Add("高温水系统预热循环", new List<ContronlPoint>
            {
                new ContronlPoint{ key = "15", contronl = true, check = true },
                new ContronlPoint{ key = "16", contronl = true, check = true },
                //预热水泵 移动到最后
                new ContronlPoint{ key = "17", contronl = true, check = true },
                new ContronlPoint{ key = "20", contronl = true, check = true },
                new ContronlPoint{ key = "03", contronl = true, check = false },
                new ContronlPoint{ key = "21", contronl = true, check = true },
                new ContronlPoint{ key = "22", contronl = true, check = true },
                new ContronlPoint{ key = "预热水泵", contronl = true },
            });
            _deviceCommandMap.Add("高温水系统预热循环", false);

            // 抽油
            _contronlMap.Add("抽油", new List<ContronlPoint>
            {
                new ContronlPoint{ key = "92", contronl = true },
                new ContronlPoint{ key = "95", contronl = true },
                new ContronlPoint{ key = "96", contronl = true },
                new ContronlPoint{ key = "97", contronl = true },
                new ContronlPoint{ key = "115", contronl = true },
                new ContronlPoint{ key = "预供机油泵", contronl = true },
            });
            _deviceCommandMap.Add("抽油", false);

            // 机油预热 另启动逻辑（还需要加热到50℃）
            _contronlMap.Add("机油预热", new List<ContronlPoint>
            {
                //new ContronlPoint{ key = "133", contronl = true },
                //new ContronlPoint{ key = "135", contronl = true },
                //new ContronlPoint{ key = "136", contronl = true },
                //new ContronlPoint{ key = "137", contronl = true },
                //new ContronlPoint{ key = "预供机油泵", contronl = true },
            });
            _deviceCommandMap.Add("机油预热", false);

            // 机油预供循环
            _contronlMap.Add("机油预供循环", new List<ContronlPoint>
            {
                new ContronlPoint{ key = "93", contronl = true, check = true },
                new ContronlPoint{ key = "95", contronl = true, check = true },
                new ContronlPoint{ key = "96", contronl = true , check = true},
                new ContronlPoint{ key = "97", contronl = true, check = true },
                new ContronlPoint{ key = "100", contronl = false, check = true },
            });
            _deviceCommandMap.Add("机油预供循环", false);

            // 燃油箱加油 另启逻辑
            _contronlMap.Add("燃油箱加油", new List<ContronlPoint>
            {
            });
            _deviceCommandMap.Add("燃油箱加油", false);

            // 启动燃油循环
            _contronlMap.Add("启动燃油循环", new List<ContronlPoint>
            {
                new ContronlPoint{ key = "179", contronl = true , check = true},
                new ContronlPoint{ key = "190", contronl = true , check = true},
                new ContronlPoint{ key = "燃油泵1", contronl = true, check = true },
            });
            _deviceCommandMap.Add("启动燃油循环", false);

            #endregion

            #region 启机后
            // 高温水补水
            _contronlMap.Add("高温水补水", new List<ContronlPoint>
            {
                new ContronlPoint{ key = "28", contronl = true, check = true },
                new ContronlPoint{ key = "29", contronl = true , check = true},
            });
            _deviceCommandMap.Add("高温水补水", false);

            // 低温水补水
            _contronlMap.Add("低温水补水", new List<ContronlPoint>
            {
                new ContronlPoint{ key = "28", contronl = true, check = true },
                new ContronlPoint{ key = "30", contronl = true, check = true },
            });
            _deviceCommandMap.Add("低温水补水", false);

            // 燃油系统循环
            _contronlMap.Add("燃油系统循环", new List<ContronlPoint>
            {
                new ContronlPoint{ key = "179", contronl = true, check = true },
                new ContronlPoint{ key = "190", contronl = true, check = true },
                new ContronlPoint{ key = "燃油泵1", contronl = true, check = true },
            });
            _deviceCommandMap.Add("燃油系统循环", false);

            //// 冷却燃油
            //_contronlMap.Add("冷却燃油", new List<ContronlPoint>
            //{
            //    new ContronlPoint{ key = "61", contronl = true },
            //});
            //_deviceCommandMap.Add("冷却燃油", false);
            #endregion

            #region 下台后
            // 机油回抽
            _contronlMap.Add("机油回抽", new List<ContronlPoint>
            {
                new ContronlPoint{ key = "90", contronl = true, check = true },
                new ContronlPoint{ key = "91", contronl = true, check = true },
                new ContronlPoint{ key = "92", contronl = true, check = true },
                new ContronlPoint{ key = "93", contronl = true, check = true },
                new ContronlPoint{ key = "95", contronl = true, check = true },
                new ContronlPoint{ key = "96", contronl = true, check = true },
                new ContronlPoint{ key = "97", contronl = true, check = true },
                new ContronlPoint{ key = "100", contronl = true, check = false }, // 关
                new ContronlPoint{ key = "115", contronl = true, check = true },
                new ContronlPoint{ key = "预供机油泵", contronl = true },
            });
            _deviceCommandMap.Add("机油回抽", false);

            // 高温水回抽
            _contronlMap.Add("高温水回抽", new List<ContronlPoint>
            {
                new ContronlPoint{ key = "21", contronl = true, check = true },
                new ContronlPoint{ key = "23", contronl = true, check = true },
                new ContronlPoint{ key = "16", contronl = true, check = true },
                new ContronlPoint{ key = "17", contronl = true, check = true },
                new ContronlPoint{ key = "24", contronl = true, check = true },
                new ContronlPoint{ key = "预热水泵", contronl = true, check = true },
            });
            _deviceCommandMap.Add("高温水回抽", false);

            // 中冷水回抽
            _contronlMap.Add("中冷水回抽", new List<ContronlPoint>
            {
                new ContronlPoint{ key = "31", contronl = true, check = true },
                new ContronlPoint{ key = "23", contronl = true, check = true },
                new ContronlPoint{ key = "16", contronl = true, check = true },
                new ContronlPoint{ key = "17", contronl = true, check = true },
                new ContronlPoint{ key = "24", contronl = true, check = true },
                new ContronlPoint{ key = "预热水泵", contronl = true, check = true },
            });
            _deviceCommandMap.Add("中冷水回抽", false);
            #endregion
        }

        /// <summary>
        /// 实时检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            var pressure1 = Common.AIgrp["厂房进气压力检测1"];
            var pressure2 = Common.AIgrp["厂房进气压力检测2"];

            this.ucParamPressure1.GaugeValue = pressure1;
            this.ucParamPressure2.GaugeValue = pressure2;

            // 如果突然没气了，把所有的泵关掉
            if (pressure1 < 100 || pressure2 < 100) 
            {
                // 水
                if (Common.DOgrp["预热水泵合闸控制"]) 
                {
                    Common.DOgrp["预热水泵合闸控制"] = false;
                }

                // 机油
                if (Common.DOgrp["预供机油泵合闸控制"])
                {
                    Common.DOgrp["预供机油泵合闸控制"] = false;
                }

                if (Common.DOgrp["污油排出泵合闸控制"])
                {
                    Common.DOgrp["污油排出泵合闸控制"] = false;
                }

                // 燃油
                if (Common.DOgrp["燃油泵1合闸控制"])
                {
                    Common.DOgrp["燃油泵1合闸控制"] = false;
                }

                if (Common.DOgrp["燃油泵2合闸控制"])
                {
                    Common.DOgrp["燃油泵2合闸控制"] = false;
                }
            }
            //// 预热水箱是否打开检测
            //var switchT = YRsxCheck();

            //this.btnYRSXK.Switch = switchT;
            //this.btnYRSXG.Switch = !switchT;
        }

        /// <summary>
        /// 预热水箱检测
        /// </summary>
        /// <returns></returns>
        public bool YRsxCheck()
        {
            return (Common.DIgrp["预热水箱加热器控制1检测"]
             || Common.DIgrp["预热水箱加热器控制2检测"]
             || Common.DIgrp["预热水箱加热器控制3检测"]
             || Common.DIgrp["预热水箱加热器控制4检测"]
             || Common.DIgrp["预热水箱加热器控制5检测"]
             || Common.DIgrp["预热水箱加热器控制6检测"]);
        }

        /// <summary>
        /// 温度调节
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetTemp_Click(object sender, EventArgs e)
        {
            var pipePara = sender as RButton;
            RW.Components.Widgets.frmSetValue set = new RW.Components.Widgets.frmSetValue();
            set.DecimalNumber = 1;
            set.MaxValue = 150;
            set.Unit = UnitEnum.celsius;
            set.Text = "温度设置";

            if (pipePara.Tag.ToString().Contains("机油温度"))
            {
                set.Value = Common.engineOilGrp[pipePara.Tag.ToString()];
                var dr = set.ShowDialog(this);
                if (dr == DialogResult.OK)
                    Common.engineOilGrp.EngineOilPID = set.Value;
            }
            else
            {
                set.Value = Common.waterGrp[pipePara.Tag.ToString()];
                var dr = set.ShowDialog(this);
                if (dr == DialogResult.OK)
                    if (pipePara.Tag.ToString().Contains("高温水"))
                    {
                        Common.waterGrp.HeightTempWaterPID = set.Value;
                    }
                    else
                    {
                        Common.waterGrp.MediumColdPID = set.Value;
                    }
            }
        }

        /// <summary>
        /// 流量调节
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetFlow_Click(object sender, EventArgs e)
        {
            var pipePara = sender as RButton;
            RW.Components.Widgets.frmSetValue set = new RW.Components.Widgets.frmSetValue();
            set.DecimalNumber = 0;

            var pub = MiddleData.instnce.PubsConfig;
            if (pub.PubParaList.Count != 0)
            {
                if (pipePara.Tag.ToString() == "水泵出口电动调节阀控制-18")
                {
                    set.MaxValue = pub.PubParaList[0].MaxWaterPumpOutletFlow;
                    set.MinValue = pub.PubParaList[0].MinWaterPumpOutletFlow;
                }
                if (pipePara.Tag.ToString() == "燃油泵1电动调节阀控制-170")
                {
                    set.MaxValue = pub.PubParaList[0].MaxFuelPump1Flow;
                    set.MinValue = pub.PubParaList[0].MinFuelPump1Flow;
                }
                if (pipePara.Tag.ToString() == "燃油泵旁路1电动调节阀控制-194")
                {
                    set.MaxValue = pub.PubParaList[0].MaxFuelPump2Flow;
                    set.MinValue = pub.PubParaList[0].MinFuelPump2Flow;
                }
                if (pipePara.Tag.ToString() == "机油温度设置PID")
                {
                    set.MaxValue = pub.PubParaList[0].MaxOilTemperature;
                    set.MinValue = pub.PubParaList[0].MinOilTemperature;
                }
                if (pipePara.Tag.ToString() == "中冷水温度设置PID")
                {
                    set.MaxValue = pub.PubParaList[0].MaxMediumColdWaterTemperature;
                    set.MinValue = pub.PubParaList[0].MinMediumColdWaterTemperature;
                }
                if (pipePara.Tag.ToString() == "高温水温度设置PID")
                {
                    set.MaxValue = pub.PubParaList[0].MaxHighTemperatureWater;
                    set.MinValue = pub.PubParaList[0].MinHighTemperatureWater;
                }

                if (pipePara.Tag.ToString() == "进气风道右调节阀控制")
                {
                    set.MaxValue = pub.PubParaList[0].MaxIntakeDuctRightFlow;
                    set.MinValue = pub.PubParaList[0].MinIntakeDuctRightFlow;
                }
                if (pipePara.Tag.ToString() == "进气风道左调节阀控制")
                {
                    set.MaxValue = pub.PubParaList[0].MaxIntakeDuctLeftFlow;
                    set.MinValue = pub.PubParaList[0].MinIntakeDuctLeftFlow;
                }
                if (pipePara.Tag.ToString() == "排气风道右调节阀控制")
                {
                    set.MaxValue = pub.PubParaList[0].MaxExhaustDuctRight;
                    set.MinValue = pub.PubParaList[0].MinExhaustDuctRight;
                }
                if (pipePara.Tag.ToString() == "排气风道左调节阀控制")
                {
                    set.MaxValue = pub.PubParaList[0].MaxExhaustDuctLeft;
                    set.MinValue = pub.PubParaList[0].MinExhaustDuctLeft;
                }
            }
            else
            {
                set.MaxValue = 100;
            }

            set.Unit = UnitEnum.percent;
            set.Text = "流量设置";
            set.Value = Common.AOgrp[pipePara.Tag.ToString()];
            var dr = set.ShowDialog(this);
            if (dr == DialogResult.OK)
                Common.AOgrp[pipePara.Tag.ToString()] = set.Value;
        }
    }


    /// <summary>
    /// 控制点
    /// </summary>
    public class ContronlPoint
    {
        /// <summary>
        /// 点位
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// true 为合/false 为分
        /// </summary>
        public bool contronl { get; set; }

        /// <summary>
        /// true 检测开到位/false 检测关到位
        /// </summary>
        public bool check { get; set; }

        /// <summary>
        /// true 开关到位为1 / false 开关到为位为0
        /// </summary>
        public bool checkResult { get; set; }
    }

}

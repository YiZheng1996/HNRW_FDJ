using BogieIdling.UI.TRDP;
using MainUI.Config;
using MainUI.Global;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainUI.Graph;
using static MainUI.Modules.EventArgsModel;

namespace MainUI.TestScreen
{
    public partial class ucForm1 : UserControl
    {
        /// <summary>
        /// 第二个界面（TRDP数据/管路数据）
        /// </summary>
        frmEngineData frmEngineDataHMI;

        // 数据字典
        Dictionary<string, ucValueLabel> dicValueLabel = new Dictionary<string, ucValueLabel>();
        // 模拟量的集合
        Dictionary<string, ucParamKeyUI> ucParamKeyList = new Dictionary<string, ucParamKeyUI>();

        Dictionary<string, Label> dicLabel = new Dictionary<string, Label>();

        public ucForm1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            //CreateGraph();
            //var uiTitle = new UITitle { Text = "图表一", SubText = "XX数据" };

            timer1.Enabled = true;
            timer1.Start();

            // 仪表盘量程
            ucParamSpeed.SetRand(0, 1100, 1000); // 转速
            ucParamPower.SetRand(0, 6000, 5000); //功率
            ucParamTorque.SetRand(0, 47750, 47000); //扭矩 需要重新调整
            ucParamFrontTurbocharger.SetRand(0, 1200, 1120); // 柴油机飞轮转速2
            ucParamAfterTurbocharger.SetRand(0, 1200, 1120); // 柴油机飞轮转速2
            ucParamFrontTurboP.SetRand(0, 5, 4); // 前增压器排气背压
            ucParamAfterTurboP.SetRand(0, 5, 4); // 后增压器排气背压
            ucParamFuelInletP.SetRand(0, 1000, 800); // 燃油进口压力 
            ucParamFuelInletT.SetRand(0, 150, 130); // 燃油进口温度
            ucParamHWaterT.SetRand(0, 150, 130); // 高温水出机温度 
            ucParamHWaterP.SetRand(0, 500, 450); // 高温水出机压力 
            ucParamEngineInP.SetRand(0, 1000, 900); // 机油进口压力 
            ucParamEngineOutP.SetRand(0, 1000, 900); // 机油出口压力 

            // 加载所有控件
            LoadAllValve();
            EachControl(this);
            //LoadLabelValue();

            Common.DIgrp.KeyValueChange += DIgrp_KeyValueChange;
            Common.AOgrp.KeyValueChange += AOgrp_KeyValueChange;
            Common.AIgrp.KeyValueChange += AIgrp_KeyValueChange;
            Common.AI2Grp.KeyValueChange += AI2Grp_KeyValueChange;
            Common.engineOilGrp.KeyValueChange += EngineOilGrp_KeyValueChange;
            Common.fuelGrp.KeyValueChange += FuelGrp_KeyValueChange;
            Common.waterGrp.KeyValueChange += WaterGrp_KeyValueChange;
            Common.threePhaseElectric.KeyValueChange += ThreePhaseElectric_KeyValueChange;
            Common.speedGrp.KeyValueChange += SpeedGrp_KeyValueChange;
            //Var.TRDP.KeyValueChange += TRDP_KeyValueChange;

            // 监听型号更新
            EventTriggerModel.OnModelNameChanged += OnMyModelNameChanged;

            // 接收型号
            Common.opcExChangeReceiveGrp.KeyValueChangeStr += OpcExChangeReceiveGrp_KeyValueChangeStr;

            try
            {
                int moveIndex = 0;
                // 打开另一个窗体
                if (Screen.AllScreens.Count() > 1)
                {
                    moveIndex = 1;
                }

                // 如果窗体不存在则创建，否则激活
                if (frmEngineDataHMI == null || frmEngineDataHMI.IsDisposed)
                {
                    frmEngineDataHMI = new frmEngineData();
                    frmEngineDataHMI.Init();
                    frmEngineDataHMI.Show();
                }
                else
                {
                    frmEngineDataHMI.Activate();
                }
                // 如果存在另一个窗体，弹出到另一个页面
                MoveFormToMonitor(frmEngineDataHMI, moveIndex);

            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, $"屏幕1初始化加载出现异常 {ex.ToString()}");
            }
        }

        private void SpeedGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            // 确保在UI线程上更新
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DoubleValueChangedEventArgs>(FuelGrp_KeyValueChange), sender, e);
                return;
            }
            if (dicValueLabel.ContainsKey(e.Key))
            {
                dicValueLabel[e.Key].Value = e.Value;
            }
            if (ucParamKeyList.ContainsKey(e.Key))
            {
                ucParamKeyList[e.Key].GaugeValue = e.Value;
            }
        }

        /// <summary>
        /// 接收型号进行绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpcExChangeReceiveGrp_KeyValueChangeStr(object sender, StringValueChangedEventArgs e)
        {
            if (e.Key == "当前型号")
            {

            }
        }

        /// <summary>
        /// 检测急停拍下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DIgrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (e.Key == "紧急停止")
            {
                EventTriggerModel.ScramChanged(e.Value);
            }
        }

        /// <summary>
        /// 8930电参数仪器 数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreePhaseElectric_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            // 确保在UI线程上更新
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DoubleValueChangedEventArgs>(ThreePhaseElectric_KeyValueChange), sender, e);
                return;
            }

            if (dicValueLabel.ContainsKey(e.Key))
            {
                dicValueLabel[e.Key].Value = e.Value;
            }
        }

        /// <summary>
        /// TRDP的转速
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TRDP_KeyValueChange(object sender, TRDPValueChangedEventArgs e)
        {
            if (e.Key == "转速传感器1#" || e.Key == "转速传感器2#")
            {
                ucParamKeyList[e.Key].GaugeValue = e.Value.ToDouble();
            }
            //if(dicParamKeyList.ContainsKey(e.Key))
            //{
            //    dicParamKeyList[e.Key].GaugeValue = e.Value.ToDouble();
            //}
        }

        // 将窗体移动到指定的显示器
        private void MoveFormToMonitor(Form form, int monitorIndex)
        {
            if (monitorIndex >= 0 && monitorIndex < Screen.AllScreens.Length)
            {
                Screen targetScreen = Screen.AllScreens[monitorIndex];

                // 设置窗体位置到目标显示器的中心
                form.Location = new Point(
                    targetScreen.Bounds.Left + (targetScreen.Bounds.Width - form.Width) / 2,
                    targetScreen.Bounds.Top + (targetScreen.Bounds.Height - form.Height) / 2
                );
            }
        }

        /// <summary>
        /// 型号更新时刷新数据
        /// </summary>
        /// <param name="obj"></param>
        private void OnMyModelNameChanged(string model)
        {
            //flowLayoutPanel1.Controls.Clear();
            // S100 为kep的默认地址
            if (model == "S100") return;
            Var.SysConfig.LastModel = model;
            Var.SysConfig.Save();

            // 通过配置文件更新
            DashboardConfig dashboard = new DashboardConfig(model);
            foreach (var item in dashboard.dashboardDatas)
            {
                ucParamKeyUI ucParamKeyUI = new ucParamKeyUI();
                ucParamKeyUI.Key = item.Point;
                ucParamKeyUI.Title = item.Name;
                ucParamKeyUI.Unit = item.Unit;
                ucParamKeyUI.Size = new Size(290, 322);
                // 设置控件自身的 Margin（上下左右间距）
                ucParamKeyUI.Margin = new Padding(15, 15, 15, 15); // 上下 5px 间距
                ucParamKeyUI.Font = new Font("宋体", 18F, FontStyle.Regular);
                ucParamKeyUI.SetRand(item.MinVal, item.MaxVal, item.ScarmVal);

                //flowLayoutPanel1.Controls.Add(ucParamKeyUI);
            }

            LoadAllValve();
        }

        /// <summary>
        /// 测量plc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AI2Grp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            //if (ucParamList.ContainsKey(e.Key))
            //{
            //    ucParamList[e.Key].GaugeValue = e.Value;
            //}
            // 确保在UI线程上更新
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DoubleValueChangedEventArgs>(AI2Grp_KeyValueChange), sender, e);
                return;
            }

            if (dicValueLabel.ContainsKey(e.Key))
            {
                dicValueLabel[e.Key].Value = e.Value;
            }
            if (ucParamKeyList.ContainsKey(e.Key))
            {
                ucParamKeyList[e.Key].GaugeValue = e.Value;
            }

        }

        /// <summary>
        /// 仪表盘数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WaterGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            // 确保在UI线程上更新
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DoubleValueChangedEventArgs>(WaterGrp_KeyValueChange), sender, e);
                return;
            }
            if (dicValueLabel.ContainsKey(e.Key))
            {
                dicValueLabel[e.Key].Value = e.Value;
            }
            if (ucParamKeyList.ContainsKey(e.Key))
            {
                ucParamKeyList[e.Key].GaugeValue = e.Value;
            }
        }

        private void FuelGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            // 确保在UI线程上更新
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DoubleValueChangedEventArgs>(FuelGrp_KeyValueChange), sender, e);
                return;
            }
            if (dicValueLabel.ContainsKey(e.Key))
            {
                dicValueLabel[e.Key].Value = e.Value;
            }
            if (ucParamKeyList.ContainsKey(e.Key))
            {
                ucParamKeyList[e.Key].GaugeValue = e.Value;
            }
        }

        private void EngineOilGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            // 确保在UI线程上更新
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DoubleValueChangedEventArgs>(EngineOilGrp_KeyValueChange), sender, e);
                return;
            }
            if (dicValueLabel.ContainsKey(e.Key))
            {
                dicValueLabel[e.Key].Value = e.Value;
            }
            if (ucParamKeyList.ContainsKey(e.Key))
            {
                ucParamKeyList[e.Key].GaugeValue = e.Value;
            }
        }

        /// <summary>
        /// 数值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AIgrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            // 确保在UI线程上更新
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DoubleValueChangedEventArgs>(AIgrp_KeyValueChange), sender, e);
                return;
            }
            if (dicValueLabel.ContainsKey(e.Key))
            {
                dicValueLabel[e.Key].Value = e.Value;
            }
            if (ucParamKeyList.ContainsKey(e.Key))
            {
                ucParamKeyList[e.Key].GaugeValue = e.Value;
            }
        }

        private void AOgrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (ucParamKeyList.ContainsKey(e.Key))
            {
                ucParamKeyList[e.Key].GaugeValue = e.Value;
            }
        }

        /// <summary>
        /// 把所有点位添加到字典
        /// </summary>
        private void LoadAllValve()
        {
            try
            {
                //ucParamKeyList.Clear();

                //foreach (var item in this.flowLayoutPanel1.Controls)
                //{

                //    if (item is ucParamKeyUI)
                //    {
                //        ucParamKeyUI upp = item as ucParamKeyUI;
                //        if (upp.Tag != null && string.IsNullOrEmpty(upp.Tag.ToString()) == false)
                //        {
                //            ucParamList.Add(upp.Tag.ToString(), upp);
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "仪表盘添加控件错误" + ex.ToString());
            }
        }

        /// <summary>
        /// 添加控件到字典
        /// </summary>
        /// <param name="con"></param>
        private void EachControl(Control con)
        {
            foreach (Control item in con.Controls)
            {
                EachControl(item);
            }

            if (con is ucValueLabel)
            {
                //添加数值显示条
                ucValueLabel valueLabel = con as ucValueLabel;
                if (valueLabel.Tag != null && valueLabel.Tag.ToString() != string.Empty)
                {
                    dicValueLabel.Add(valueLabel.Tag.ToString(), valueLabel);
                }
            }
            else if (con is ucParamKeyUI)
            {
                //添加仪表盘
                ucParamKeyUI paramKey = con as ucParamKeyUI;
                if (paramKey.Tag != null && paramKey.Tag.ToString() != string.Empty)
                {
                    ucParamKeyList.Add(paramKey.Tag.ToString(), paramKey);
                }
            }
            else if (con is Label)
            {
                //添加机组测量值数值显示标签
                Label lbl = con as Label;
                if (lbl.Tag != null && lbl.Tag.ToString() != string.Empty)
                {
                    dicLabel.Add(lbl.Tag.ToString(), lbl);
                }
            }
        }


        /// <summary>
        /// 快速刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.ucParamSpeed.GaugeValue = MiddleData.instnce.EngineSpeed;
            this.ucParamPower.GaugeValue = MiddleData.instnce.EnginePower;
            this.ucParamTorque.GaugeValue = MiddleData.instnce.EngineTorque;
        }

        /// <summary>
        /// 加载折线图
        /// </summary>
        public void CreateGraph()
        {
            // 创建一个GraphPane对象
            ZedGraph.GraphPane myPane = new ZedGraph.GraphPane();
            // 添加一个折线图到GraphPane
            //LineItem myCurve = myPane.AddCurve("Data", null, null, Color.Red);
            // 设置图表范围
            myPane.XAxis.MinorTic.IsAllTics = true;
            myPane.XAxis.Title.Text = "X Axis";
            myPane.YAxis.Title.Text = "Y Axis";
            // 创建一个ZedGraphControl控件
            ZedGraph.ZedGraphControl zg1 = new ZedGraph.ZedGraphControl();
            zg1.GraphPane = myPane;
            zg1.Location = new System.Drawing.Point(10, 10);
            zg1.Size = new System.Drawing.Size(500, 400);
            //this.tabPage2.Controls.Add(zg1);
            ZedGraph.LineItem myCurve = myPane.AddCurve("数据系列1", null, null, Color.Blue);
            // 假设有一组数据点
            double[] x = { 1, 2, 3, 4, 5 };
            double[] y = { 10, 20, 30, 40, 50 };
            //PointPair list = new PointPairList(x, y);
            //myCurve.AddPoint(list);


        }


        private void 更换数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGraphPointSelect selectPoint = new frmGraphPointSelect(Var.SysConfig.LastModel);
            DialogResult result = selectPoint.ShowDialog();
            if (result == DialogResult.Cancel)
                return;
            string point = selectPoint.SelectedPoint;       //取到点位名称

            GraphConfig graphConfig = new GraphConfig(Var.SysConfig.LastModel);     //暂时写死
            var pointInfo = graphConfig.graphDatas.Where(d => d.Name == point).ToList();
            if (pointInfo == null)
            {
                Var.MsgBoxWarn(this, "错误,该点位数值未添加！");
                return;
            }
            ToolStripMenuItem tsi = (ToolStripMenuItem)sender;//转换类型
            ContextMenuStrip strip = tsi.GetCurrentParent() as ContextMenuStrip;//检索作为当前ToolStripItem的容器
            ucParamKeyUI paramKeyUI = strip.SourceControl as ucParamKeyUI;//获取使ContextMenuStrip此被显示的控件

            //给仪表盘控件重新赋值
            paramKeyUI.SetRand(pointInfo[0].MinVal, pointInfo[0].MaxVal, pointInfo[0].ScarmVal);
            paramKeyUI.Tag = pointInfo[0].Point;
            paramKeyUI.Title = pointInfo[0].Name + "(" + pointInfo[0].Unit + ")";
            //更新对应的字典key值
            string keyToRemove = null;
            foreach (var item in ucParamKeyList)
            {
                if (item.Value == paramKeyUI)
                {
                    keyToRemove = item.Key;
                    break;
                }
            }
            if (keyToRemove != null)
            {
                ucParamKeyList.Remove(keyToRemove);
            }
            ucParamKeyList.Add(paramKeyUI.Tag.ToString(), paramKeyUI);
        }
    }
}

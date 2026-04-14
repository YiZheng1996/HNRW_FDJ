using System;
using Sunny.UI;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.Instrumentation;
using MainUI.Widget;
using MainUI.Modules;
using MainUI.Global;
using MainUI.Config;
using ZedGraph;
using RW.UI.Controls;
using Label = System.Windows.Forms.Label;
using BogieIdling.UI.TRDP;

namespace MainUI
{
    //显示器2数据
    public partial class frmkeyParameter : UIForm
    {
        frmEngineData frmEngineDataHMI;


        //新定字典
        Dictionary<string, ucValueLabel> dicValueLabel = new Dictionary<string, ucValueLabel>();
        Dictionary<string, ucParamKeyUI> dicParamKeyList = new Dictionary<string, ucParamKeyUI>();
        Dictionary<string, System.Windows.Forms.Label> dicLabel = new Dictionary<string, System.Windows.Forms.Label>();
        // 模拟量的集合
        Dictionary<string, ucParamKeyUI> ucParamList = new Dictionary<string, ucParamKeyUI>();
        List<string> parameterList = new List<string>
{
            // 温度参数
            "A1缸排气温度",
            "A2缸排气温度",
            "A3缸排气温度",
            "A4缸排气温度",
            "A5缸排气温度",
            "A6缸排气温度",
            "A7缸排气温度",
            "A8缸排气温度",
            "B1缸排气温度",
            "B2缸排气温度",
            "B3缸排气温度",
            "B4缸排气温度",
            "B5缸排气温度",
            "B6缸排气温度",
            "B7缸排气温度",
            "B8缸排气温度",
            "前增压器涡轮进口废气温度",
            "后增压器涡轮进口废气温度",
            "前增压器涡轮出口废气温度",
            "后增压器涡轮出口废气温度",
            "燃油泵进口油温",
            "燃油回油温度",
            "主油道进口油温",
            "机油泵出口油温",
            "前增压器机油进口温度",
            "后增压器机油进口温度",
            "前增压器机油出口温度",
            "后增压器机油出口温度",
            "机油热交换器出口油温",
            "机油热交换器进口油温",
            "高温水进机温度",
            "高温水出机温度",
            "中冷水进机温度",
            "中冷水出机温度",
            "中冷器进口水温",
            "中冷器出口水温",
            "前增压器进气流量计前温度",
            "后增压器进气流量计前温度",
            "前增压器进气温度",
            "后增压器进气温度",
            "前中冷前空气温度",
            "后冷前空气温度",
            "前中冷后空气温度",
            "后中冷后空气温度",
            "机油热交换器进口水温",
            "机油热交换器出口水温",
    
            // 压力参数
            "燃油供油压力",
            "预供机油进口压力",
            "主油道进口油压",
            "机油泵出口压力",
            "主油道末端油压",
            "前增压器机油进口压力",
            "后增压器机油进口压力",
            "高温水泵进口压力",
            "高温水泵出口压力",
            "高温水出机压力",
            "中冷水泵进口压力",
            "中冷水泵出口压力",
            "中冷水出机压力",
            "前增压器进气真空度",
            "后增压器进气真空度",
            "前中冷前空气压力",
            "后中冷前空气压力",
            "前中冷后空气压力",
            "后中冷后空气压力",
            "前涡轮进口废气压力",
            "后涡轮进口废气压力",
            "前增压器排气背压",
            "后增压器排气背压",
    
            // 流量和其他参数
            "机油流量",
            "高温水流量",
            "中冷水流量",
            "柴油机转速",
            "增压器转速1",
            "增压器转速2",
            "燃油消耗率",
            "前增压器进气流量",
            "后增压器进气流量",
            "柴油机功率",
            "大气温湿度计",
            "大气压力",
            "机油消耗量",
            "排气流量"
};
        public frmkeyParameter()
        {
            InitializeComponent();
            CreateGraph();
            var uiTitle = new UITitle { Text = "图表一", SubText = "XX数据" };

            timer1.Interval = 1000;
            timer1.Enabled = true;
            //paramKeyUI1.SetRand(0, 800, 700);

            // 加载所有控件
            LoadAllValve();
            EachControl(this);
            //LoadLabelValue();
            Common.AOgrp.KeyValueChange += AOgrp_KeyValueChange;
            Common.AIgrp.KeyValueChange += AIgrp_KeyValueChange;
            Common.engineOilGrp.KeyValueChange += EngineOilGrp_KeyValueChange;
            Common.fuelGrp.KeyValueChange += FuelGrp_KeyValueChange;
            Common.waterGrp.KeyValueChange += WaterGrp_KeyValueChange;
            

            //页面改版后
            Var.TRDP.KeyValueChange += TRDP_KeyValueChange;
            Common.threePhaseElectric.KeyValueChange += ThreePhaseElectric_KeyValueChange;
            Common.engineParaGrp.KeyValueChange += EngineParaGrp_KeyValueChange;

            // 监听型号更新
            EventTriggerModel.OnModelNameChanged += OnMyModelNameChanged;


            //初始化数据
            Common.AOgrp.Fresh();
            Common.threePhaseElectric.Fresh();
            Common.engineParaGrp.Fresh();

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

                throw ex;
            }
        }

        private void ThreePhaseElectric_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if(dicLabel.ContainsKey(e.Key))
            {
                if(this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        dicLabel[e.Key].Text = e.Value.ToString();
                    }));
                }
                else
                {
                    dicLabel[e.Key].Text = e.Value.ToString();
                }
            }
        }

        private void TRDP_KeyValueChange(object sender, TRDPValueChangedEventArgs e)
        {
            if(e.Key == "转速传感器1#" || e.Key == "转速传感器2#")
            {
                dicParamKeyList[e.Key].GaugeValue = e.Value.ToDouble();
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

        private void EngineParaGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            //if (ucParamList.ContainsKey(e.Key))
            //{
            //    ucParamList[e.Key].GaugeValue = e.Value;
            //}
            if (dicValueLabel.ContainsKey(e.Key))
            {
                dicValueLabel[e.Key].Value = e.Value;
            }
            if(dicParamKeyList.ContainsKey(e.Key))
            {
                dicParamKeyList[e.Key].GaugeValue = e.Value;
            }

        }

        private void WaterGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (ucParamList.ContainsKey(e.Key))
            {
                ucParamList[e.Key].GaugeValue = e.Value;
            }
        }

        private void FuelGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (ucParamList.ContainsKey(e.Key))
            {
                ucParamList[e.Key].GaugeValue = e.Value;
            }
        }

        private void EngineOilGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (ucParamList.ContainsKey(e.Key))
            {
                ucParamList[e.Key].GaugeValue = e.Value;
            }
        }

        private void AIgrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            //if (ucParamList.ContainsKey(e.Key))
            //{
            //    ucParamList[e.Key].GaugeValue = e.Value;
            //}
            if (dicValueLabel.ContainsKey(e.Key))
            {
                dicValueLabel[e.Key].Value = e.Value;
            }
        }

        private void AOgrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (ucParamList.ContainsKey(e.Key))
            {
                ucParamList[e.Key].GaugeValue = e.Value;
            }
        }

        /// <summary>
        /// 把所有点位添加到字典
        /// </summary>
        private void LoadAllValve()
        {
            try
            {
                ucParamList.Clear();

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
            if (con is ucParamKeyUI)
            {
                //添加仪表盘
                ucParamKeyUI paramKey = con as ucParamKeyUI;
                if (paramKey.Tag != null && paramKey.Tag.ToString() != string.Empty)
                {
                    dicParamKeyList.Add(paramKey.Tag.ToString(), paramKey);
                }
            }
            if(con is Label)
            {
                //添加机组测量值数值显示标签
                Label lbl = con as Label;
                if(lbl.Tag != null && lbl.Tag.ToString()!= string.Empty)
                {
                    dicLabel.Add(lbl.Tag.ToString(), lbl);
                }
            }
        }



        Random rand = new Random();
        private void timer1_Tick(object sender, EventArgs e)
        {



        }
        /// <summary>
        /// 加载折线图
        /// </summary>
        public void CreateGraph()
        {
            // 创建一个GraphPane对象
            GraphPane myPane = new GraphPane();
            // 添加一个折线图到GraphPane
            //LineItem myCurve = myPane.AddCurve("Data", null, null, Color.Red);
            // 设置图表范围
            myPane.XAxis.MinorTic.IsAllTics = true;
            myPane.XAxis.Title.Text = "X Axis";
            myPane.YAxis.Title.Text = "Y Axis";
            // 创建一个ZedGraphControl控件
            ZedGraphControl zg1 = new ZedGraphControl();
            zg1.GraphPane = myPane;
            zg1.Location = new System.Drawing.Point(10, 10);
            zg1.Size = new System.Drawing.Size(500, 400);
            //this.tabPage2.Controls.Add(zg1);
            LineItem myCurve = myPane.AddCurve("数据系列1", null, null, Color.Blue);
            // 假设有一组数据点
            double[] x = { 1, 2, 3, 4, 5 };
            double[] y = { 10, 20, 30, 40, 50 };
            //PointPair list = new PointPairList(x, y);
            //myCurve.AddPoint(list);


        }
        //private void LoadLabelValue()
        //{
        //    for (int i = 0; i < parameterList.Count; i++)
        //    {
        //        AILabel lbl = new AILabel();
        //        lbl.Text = parameterList[i];
        //        lbl.Size = new Size(290, 23);
        //        lbl.Font = new Font("微软雅黑", 16, FontStyle.Regular);
        //        lbl.Margin = new Padding(5, 5, 5, 5);
        //        lbl.BackColor = Color.FromArgb(230, 242, 255);
        //        this.flowLayoutPanel1.Controls.Add(lbl);
        //    }
        //}
    }
}

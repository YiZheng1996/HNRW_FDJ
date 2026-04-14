using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using System.Threading;
using System.Diagnostics;
using System.Linq;
using System.IO;
using RW.Components;
using RW.Log;

namespace MainUI.Wave
{
    /// <summary>
    /// 曲线组件，依赖ZEDGraph控件
    /// </summary>
    public partial class ucBaseWave : UserControl
    {
        /// <summary>
        /// 初始化曲线控件
        /// </summary>
        public ucBaseWave()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.Disposed += new EventHandler(ucWave_Disposed);
            this.zedGraphControl1.GraphPane.YAxis.Scale.Min = 0;
            this.zedGraphControl1.GraphPane.YAxis.Scale.Max = 10;
            //this.zedGraphControl1.GraphPane.XAxis.Scale.Max = 5;
            //this.zedGraphControl1.GraphPane.YAxis.MajorGrid.PenWidth = 2;
            //this.zedGraphControl1.GraphPane.YAxis.MajorTic.PenWidth = 2;
            //this.zedGraphControl1.GraphPane.YAxis.MinorTic.PenWidth = 5;
            //this.zedGraphControl1.GraphPane.XAxis.MajorGrid.PenWidth = 3;
            //this.zedGraphControl1.GraphPane.YAxis.Scale.FontSpec.Size = 30;
            //this.zedGraphControl1.GraphPane.IsPenWidthScaled = true;
            zedGraphControl1.GraphPane.Legend.IsVisible = false;
            zedGraphControl1.GraphPane.Legend.Border.IsVisible = false;
            //zedGraphControl1.GraphPane.Legend.Position = LegendPos.Right;
            zedGraphControl1.GraphPane.Legend.FontSpec = new FontSpec("微软雅黑", 11, this.ForeColor, Font.Bold, Font.Italic, Font.Underline);
            zedGraphControl1.GraphPane.Legend.FontSpec.Border.IsVisible = false;
            zedGraphControl1.GraphPane.YAxis.Title.FontSpec = new FontSpec("微软雅黑", 18, this.ForeColor, Font.Bold, Font.Italic, false);
            zedGraphControl1.GraphPane.YAxis.Title.FontSpec.Border.IsVisible = false;
            zedGraphControl1.GraphPane.YAxis.Title.FontSpec.Angle = 180;
            zedGraphControl1.GraphPane.XAxis.Title.FontSpec = new FontSpec("微软雅黑", 18, this.ForeColor, Font.Bold, Font.Italic, false);
            zedGraphControl1.GraphPane.XAxis.Title.FontSpec.Border.IsVisible = false;

            //zedGraphControl1.GraphPane.Legend.FontSpec.Size = 16;
            //zedGraphControl1.GraphPane.Legend.FontSpec.Family = this.Font.FontFamily.Name;
            //this.zedGraphControl1.GraphPane.YAxis.Scale.Min = 0;
            //this.zedGraphControl1.GraphPane.XAxis.Scale.Min = 0;
            this.zedGraphControl1.GraphPane.YAxis.MajorGrid.IsVisible = true;
            //this.zedGraphControl1.GraphPane.YAxis.MinorGrid.IsVisible = true;

            this.zedGraphControl1.GraphPane.XAxis.MajorGrid.IsVisible = true;
            //this.zedGraphControl1.GraphPane.XAxis.Type = ZedGraph.AxisType.Text;

            //this.zedGraphControl1.GraphPane.YAxis.Scale.MajorStepAuto = false;
            //this.zedGraphControl1.GraphPane.YAxis.Scale.MinorStepAuto = false;
            this.zedGraphControl1.IsEnableZoom = false;
            this.zedGraphControl1.GraphPane.XAxis.Scale.MaxGrace = 0;
            this.zedGraphControl1.GraphPane.XAxis.Scale.MinGrace = 1;
            this.zedGraphControl1.GraphPane.XAxis.Scale.MinorStep = 1;
            this.zedGraphControl1.GraphPane.XAxis.Scale.MajorStep = 5;
            this.zedGraphControl1.GraphPane.XAxis.Scale.Min = 0;
            this.zedGraphControl1.GraphPane.YAxis.MajorGrid.IsVisible = true;
            this.zedGraphControl1.GraphPane.YAxis.MinorGrid.IsVisible = true;

        }

        void ucWave_Disposed(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 保存图片到指定路径中
        /// </summary>
        public void SaveImage(string filePath)
        {
            zedGraphControl1.MasterPane.GetImage().Save(filePath);
        }

        /// <summary>
        /// 按指定大小将文件保存到指定文件
        /// </summary>
        public void SaveImage(string filename, int width, int height)
        {
            zedGraphControl1.MasterPane.GetImage(width, height, 96).Save(filename);
        }

        /// <summary>
        /// 按照CSV方式将数据保存到指定文件
        /// </summary>
        public void SaveData(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                for (int i = 0; i < this.Waves.Length; i++)
                {
                    string txt = "";
                    if (this.waves[i].List.Count > 0)
                    {
                        var data = this.waves[i].List.Select(x => x.Y.ToString("0.##")).ToList();
                        //切记，请勿直接使用List.Aggreegate，由于数据一直在添加，容易引发"集合已经修改"的异常
                        //请放入一个不再变更的集合中进行Aggregate操作。
                        txt = data.Aggregate((a, b) => a + "," + b);
                    }
                    writer.WriteLine(txt);
                }
                writer.Flush();
            }
        }

        /// <summary>
        /// 小数点位数
        /// </summary>
        [DefaultValue(2)]
        public int DecimalNumber { get; set; } = 2;
        /// <summary>
        /// 将指定的CSV文件加载到曲线中
        /// </summary>
        public void LoadData(string filePath, int period = 1)
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException($"指定的文件不存在。[{filePath}]");

            var lines = File.ReadLines(filePath, Encoding.UTF8).ToList();
            for (int i = 0; i < lines.Count && i < this.Waves.Length; i++)
            {
                var line = lines[i];
                var arr = line.Split(',');
                this.Waves[i].List.AddRange(
                    arr.Select(
                        (value, index) =>
                            new PointPair(index * period / 60.0, Math.Round(Convert.ToDouble(value), DecimalNumber))
                    )
                );
            }
        }


        private WaveInfo[] waves;
        /// <summary>
        /// 获取或设置定义的曲线
        /// </summary>
        [DefaultValue(null)]
        public WaveInfo[] Waves
        {
            get { return waves; }
            set
            {
                waves = value;
                RemoveWave();

                for (int i = 0; Waves != null && i < Waves.Length; i++)
                {
                    WaveInfo w = Waves[i];
                    LineItem line = zedGraphControl1.GraphPane.AddCurve(w.Name, w.List, w.Color, SymbolType.None);
                    line.Line.Width = 2f;

                    CheckBox chk = new CheckBox();
                    chk.AutoSize = true;
                    chk.Text = w.Name;
                    chk.Checked = true;
                    int temp = i;

                    chk.Height = 20;
                    chk.Margin = new Padding(2);
                    chk.BackColor = w.Color;

                    chk.CheckedChanged += delegate
                    {
                        SetWaveVisible(temp, chk.Checked);
                    };
                    this.flowLayoutPanel1.Controls.Add(chk);
                }

                this.Refresh();
            }
        }

        /// <summary>
        /// 所有的曲线
        /// </summary>
        public CurveList Curve => zedGraphControl1.GraphPane.CurveList;

        private Font legentFont = new Font("微软雅黑", 11);
        /// <summary>
        /// 图例字体样式
        /// </summary>
        [DefaultValue(typeof(Font), "微软雅黑, 11pt")]
        [Description("图例字体样式")]
        public Font LegentFont
        {
            get { return legentFont; }
            set
            {
                legentFont = value;
                if (value == null) return;

                zedGraphControl1.GraphPane.Legend.FontSpec.Family = legentFont.FontFamily.Name;
                zedGraphControl1.GraphPane.Legend.FontSpec.Size = legentFont.Size;
                zedGraphControl1.GraphPane.Legend.FontSpec.IsBold = legentFont.Bold;
                zedGraphControl1.GraphPane.Legend.FontSpec.IsItalic = legentFont.Italic;
                zedGraphControl1.GraphPane.Legend.FontSpec.IsUnderline = legentFont.Underline;
            }
        }

        /// <summary>
        /// 移除所有曲线
        /// </summary>
        public void RemoveWave()
        {
            zedGraphControl1.GraphPane.CurveList.Clear();
            while (this.flowLayoutPanel1.Controls.Count > 1)
                this.flowLayoutPanel1.Controls.RemoveAt(1);
        }

        /// <summary>
        /// 设置曲线的显示状态
        /// </summary>
        public void SetWaveVisible(int i, bool value)
        {
            zedGraphControl1.GraphPane.CurveList[i].IsVisible = value;
            //zedGraphControl1.GraphPane.CurveList[temp].IsVisible = chk.Checked;
            //this.Refresh();
        }

        //int selecteIndex = -1;
        /// <summary>
        /// 设置一条参考线，用于对比三条曲线
        /// </summary>
        public LineObj AddLine(double x1, double y1, double x2, double y2)
        {
            LineObj line = null;
            if (line == null)
            {
                line = new LineObj(Color.Red, x1, y1, x2, y2);

                //line.Location.CoordinateFrame = CoordType.PaneFraction;
                line.ZOrder = ZOrder.B_BehindLegend;
                line.Location.AlignH = AlignH.Center;
                line.Location.AlignV = AlignV.Center;
                //line.Location.Width = 0;
                //line.Location.Height = 100;
                //线条位置
                this.zedGraphControl1.GraphPane.GraphObjList.Add(line);
            }



            //if (selecteIndex == -1)
            //{
            //    float xx = this.zedGraphControl1.GraphPane.XAxis.Scale.Transform(ms);
            //    float x2 = this.zedGraphControl1.GraphPane.XAxis.Scale.LocalTransform(ms);

            //    double v = ms / this.zedGraphControl1.GraphPane.XAxis.Scale.Max * this.zedGraphControl1.GraphPane.CurveList[0].Points.Count;

            //    selecteIndex = (int)v;
            //    if (selecteIndex >= 500) selecteIndex = 499;
            //}
            //if (selecteIndex != -1)
            //{

            //    double xx1 = this.zedGraphControl1.MasterPane.PaneList[0].XAxis.Scale.LocalTransform(ms);
            //    line.Location.X = (xx1 + this.zedGraphControl1.MasterPane.PaneList[0].Chart.Rect.Left + this.zedGraphControl1.MasterPane.PaneList[0].Rect.Left - this.zedGraphControl1.MasterPane.Margin.Left) / this.zedGraphControl1.Width;

            //    if (SelectedLine != null)
            //    {
            //        List<string> items = SelectedLine(selecteIndex, ms);
            //        for (int i = 0; items != null && i < items.Count; i++)
            //        {
            //            this.SetCurveText(i, items[i]);
            //        }
            //    }
            //}

            this.zedGraphControl1.Refresh();

            return line;
        }

        /// <summary>
        /// 移除添加的指定对象
        /// </summary>
        public void RemoveObj(GraphObj obj)
        {
            this.zedGraphControl1.GraphPane.GraphObjList.Remove(obj);
            this.UpdateChanged();
        }

        /// <summary>
        /// 移除所有额外添加的对象
        /// </summary>
        public void RemoveObjs()
        {
            this.zedGraphControl1.GraphPane.GraphObjList.Clear();
            this.UpdateChanged();
        }

        /// <summary>
        /// 在指定位置添加一个文本框
        /// </summary>
        public TextObj AddText(string text, double x, double y)
        {
            TextObj obj = new TextObj(text, x, y);
            this.zedGraphControl1.GraphPane.GraphObjList.Add(obj);
            return obj;
        }

        private double maxX = 5;
        /// <summary>
        /// X轴最大值
        /// </summary>
        [DefaultValue(5d)]
        public double MaxX
        {
            get { return maxX; }
            set
            {
                this.maxX = value;
                this.zedGraphControl1.GraphPane.XAxis.Scale.Max = value;
            }
        }

        private double maxY = 10;
        /// <summary>
        /// Y轴最大值
        /// </summary>
        [DefaultValue(10d)]
        public double MaxY
        {
            get { return maxY; }
            set
            {
                this.maxY = value;
                this.zedGraphControl1.GraphPane.YAxis.Scale.Max = value;
            }
        }

        private double minY;
        /// <summary>
        /// Y轴最小值
        /// </summary>
        [DefaultValue(0d)]
        public double MinY
        {
            get { return minY; }
            set
            {
                this.minY = value;
                this.zedGraphControl1.GraphPane.YAxis.Scale.Min = value;
            }
        }

        private string title;
        /// <summary>
        /// 显示标题
        /// </summary>
        [Localizable(true)]
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                this.zedGraphControl1.GraphPane.Title.Text = value;
                this.Refresh();
            }
        }

        private bool titleEnabled;

        /// <summary>
        /// 是否显示标题
        /// </summary>
        [DefaultValue(true)]
        [Description("是否显示标题")]
        public bool TitleEnabled
        {
            get { return titleEnabled; }
            set
            {
                titleEnabled = value;
                this.zedGraphControl1.GraphPane.Title.IsVisible = value;
            }
        }


        private string titleX;
        /// <summary>
        /// 获取或设置X轴的标题名称
        /// </summary>
        [Localizable(true)]
        public string TitleX
        {
            get { return titleX; }
            set
            {
                titleX = value;
                this.zedGraphControl1.GraphPane.XAxis.Title.Text = value;
                this.Refresh();
            }
        }

        private string titleY;
        /// <summary>
        /// 获取或设置Y轴的标题名称
        /// </summary>
        [Localizable(true)]
        [Description("Y轴标题")]
        public string TitleY
        {
            get { return titleY; }
            set
            {
                titleY = value;
                this.zedGraphControl1.GraphPane.YAxis.Title.Text = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// 获取或设置曲线更新间隔时间（毫秒）
        /// </summary>
        [DefaultValue(1000)]
        [Description("更新间隔时间（毫秒）")]
        public int Interval { get; set; } = 1000;

        /// <summary>
        /// 是否开启工具箱
        /// </summary>
        [DefaultValue(true)]
        [Description("是否开启工具箱")]
        public bool EnabledToolBox
        {
            get { return this.flowLayoutPanel1.Visible; }
            set { this.flowLayoutPanel1.Visible = value; }

        }

        private bool black = false;
        /// <summary>
        /// 是否是黑背景
        /// </summary>
        [DefaultValue(false)]

        public bool Black
        {
            get { return black; }
            set
            {
                black = value;
                if (value)
                {
                    this.zedGraphControl1.GraphPane.Fill.Color = Color.Black;
                    this.zedGraphControl1.GraphPane.Title.FontSpec.FontColor = Color.White;

                    this.zedGraphControl1.GraphPane.Legend.Fill.Color = Color.Black;
                    this.zedGraphControl1.GraphPane.Legend.Fill.Brush = new SolidBrush(Color.Black);
                    this.zedGraphControl1.GraphPane.Legend.FontSpec.DropShadowColor = Color.White;
                    this.zedGraphControl1.GraphPane.Chart.Fill.Color = Color.Black;
                    this.zedGraphControl1.GraphPane.Chart.Fill.Brush = new SolidBrush(Color.Black);
                    this.zedGraphControl1.GraphPane.Chart.Border.Color = Color.White;

                    //Y轴属性
                    this.zedGraphControl1.GraphPane.YAxis.Title.FontSpec.FontColor = Color.White;
                    this.zedGraphControl1.GraphPane.YAxis.MajorTic.Color = Color.White;
                    this.zedGraphControl1.GraphPane.YAxis.MinorTic.Color = Color.White;
                    this.zedGraphControl1.GraphPane.YAxis.MajorGrid.Color = Color.White;
                    this.zedGraphControl1.GraphPane.YAxis.Scale.FontSpec.FontColor = Color.White;

                    //X轴属性

                    this.zedGraphControl1.GraphPane.XAxis.Title.FontSpec.FontColor = Color.White;
                    this.zedGraphControl1.GraphPane.XAxis.MajorTic.Color = Color.White;
                    this.zedGraphControl1.GraphPane.XAxis.MinorTic.Color = Color.White;
                    this.zedGraphControl1.GraphPane.XAxis.Scale.FontSpec.FontColor = Color.White;
                }
            }
        }

        private bool isEnableLegend = false;
        /// <summary>
        /// 是否显示说明
        /// </summary>
        [DefaultValue(false)]
        public bool IsEnableLegend
        {
            get { return isEnableLegend; }
            set
            {
                isEnableLegend = value;
                zedGraphControl1.GraphPane.Legend.IsVisible = value;
            }
        }
        Stopwatch watch = new Stopwatch();

        /// <summary>
        /// 获取曲线运行的时间
        /// </summary>
        public TimeSpan Times => watch.Elapsed;

        /// <summary>
        /// 开始曲线显示
        /// </summary>
        public void Start()
        {
            watch.Start();
        }

        /// <summary>
        /// 停止曲线显示
        /// </summary>
        public void Stop()
        {
            watch.Stop();
        }

        /// <summary>
        /// 手动更新曲线数据
        /// </summary>
        public void UpdateChanged()
        {
            this.zedGraphControl1.AxisChange();
            this.zedGraphControl1.Refresh();
        }

        /// <summary>
        /// 将Y轴颜色与线条颜色对应
        /// </summary>
        public void ColorSync()
        {
            this.zedGraphControl1.GraphPane.YAxis.Scale.FontSpec.FontColor = this.Waves[0].Color;
            this.zedGraphControl1.GraphPane.Y2Axis.Scale.FontSpec.FontColor = this.waves[1].Color;
        }

        public void SetYAxis(int index, string title, double min, double max)
        {
            Axis yaxis = this.zedGraphControl1.GraphPane.YAxisList[index];
            if (index > 0)
            {
                if (this.zedGraphControl1.GraphPane.Y2AxisList[index - 1] == null)
                    this.zedGraphControl1.GraphPane.Y2AxisList.Add(title);
                yaxis = this.zedGraphControl1.GraphPane.Y2AxisList[index - 1];
                yaxis.IsVisible = true;
                yaxis.Title.Text = title;
                yaxis.IsVisible = true;
            }
            else
            {
                yaxis = this.zedGraphControl1.GraphPane.YAxisList[index];
                yaxis.Title.Text = title;
            }
            yaxis.Scale.Min = min;
            yaxis.Scale.Max = max;
        }

        /// <summary>
        /// 根据Interval触发的事件
        /// </summary>
        public event EventHandler Ticked;

        private void ucWave_Load(object sender, EventArgs e)
        {

            //  zedGraphControl1.GraphPane.CurveList[1].IsSelectable = true;


            // zedGraphControl1.GraphPane.CurveList[0].IsVisible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        /// <summary>
        /// 清空曲线
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < zedGraphControl1.GraphPane.CurveList.Count; i++)
            {
                zedGraphControl1.GraphPane.CurveList[i].Clear();
            }
            UpdateChanged();
            watch.Restart();
        }

        void SelectAll(bool b)
        {
            for (int i = 0; i < this.flowLayoutPanel1.Controls.Count; i++)
            {
                var con = this.flowLayoutPanel1.Controls[i];
                if (con is CheckBox)
                {
                    (con as CheckBox).Checked = b;
                }
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            SelectAll(true);
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            SelectAll(false);
        }



        private void zedGraphControl1_MouseClick(object sender, MouseEventArgs e)
        {

            double x = 0, y = 0;
            this.zedGraphControl1.GraphPane.ReverseTransform(new PointF(e.X, e.Y), out x, out y);

            Debug.WriteLine($"mouse point:[x:{e.X},y:{e.Y}]");
            Debug.WriteLine($"trans point:[x:{x},y:{y}]");

            WaveClicked?.Invoke(this, new WaveClickArgs(x, y));

        }
        /// <summary>
        /// 曲线点击委托
        /// </summary>
        public delegate void WaveClickHandler(object sender, WaveClickArgs e);
        /// <summary>
        /// 曲线点击事件
        /// </summary>
        public event WaveClickHandler WaveClicked;

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                Ticked?.Invoke(this, EventArgs.Empty);
                this.UpdateChanged();
            }
            catch (Exception ex)
            {
                timer1.Stop();
                //LogHelper.WriteLine("曲线计时器异常：" + ex.ToString());
                Var.MsgBoxWarn(this, "曲线刷新异常:" + ex.Message);// .Error;
            }
        }
        /// <summary>
        /// 刷新名称
        /// </summary>
        public void RefreshNames()
        {

            for (int i = 0; i < Waves.Length; i++)
            {
                var w = Waves[i];
                zedGraphControl1.GraphPane.CurveList[i].Label.Text = w.Name;
            }
            this.UpdateChanged();
        }
    }

    /// <summary>
    /// 曲线点击参数
    /// </summary>
    public class WaveClickArgs : EventArgs
    {
        /// <summary>
        /// 初始化曲线点击参数
        /// </summary>
        public WaveClickArgs()
        {

        }

        /// <summary>
        /// 初始化曲线点击参数
        /// </summary>
        public WaveClickArgs(double x, double y)
        {
            this.Location = new PointF((float)x, (float)y);
        }

        /// <summary>
        /// 点击的位置
        /// </summary>
        public PointF Location { get; set; }
    }

    /// <summary>
    /// 曲线信息
    /// </summary>
    public class WaveInfo
    {
        /// <summary>
        /// 初始化曲线信息
        /// </summary>
        public WaveInfo()
        {
            Initlist();
        }

        /// <summary>
        /// 初始化曲线信息
        /// </summary>
        public WaveInfo(string name)
        {
            this.Name = name;
            Initlist();
        }

        /// <summary>
        /// 初始化曲线信息
        /// </summary>
        public WaveInfo(string name, Color color)
        {
            this.Name = name;
            this.Color = color;
            Initlist();
        }

        /// <summary>
        /// 曲线的名称
        /// </summary>
        [Localizable(true)]
        public string Name { get; set; }

        /// <summary>
        /// 曲线的颜色
        /// </summary>
        public Color Color { get; set; }

        private void Initlist()
        {
            //for (int i = 0; i < 1000; i++)
            //{
            //    list.Add(i, 0);
            //}
        }

        private PointPairList list = new PointPairList();//每秒一次，600秒=10分钟，UI上保存的数据量

        /// <summary>
        /// 曲线的数据
        /// </summary>
        [DefaultValue(null)]
        [Localizable(false)]
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PointPairList List
        {
            get { return list; }
            set { list = value; }
        }

    }
}

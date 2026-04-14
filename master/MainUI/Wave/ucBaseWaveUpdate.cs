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
using System.Collections.Concurrent;

namespace MainUI.Wave
{
    /// <summary>
    /// 曲线组件，依赖ZEDGraph控件
    /// </summary>
    public partial class ucBaseWaveUpdate : UserControl
    {
        private System.Windows.Forms.Timer realtimeTimer;
        private DateTime startTime;
        private ConcurrentQueue<DataPoint>[] dataQueues;
        private int maxDataPoints; // 最大数据点数
        private double timeInterval = 1.0; // 时间间隔（秒）

        /// <summary>
        /// 数据点结构
        /// </summary>
        private struct DataPoint
        {
            public DateTime Time;
            public double Value;

            public DataPoint(DateTime time, double value)
            {
                Time = time;
                Value = value;
            }
        }

        /// <summary>
        /// 初始化曲线控件
        /// </summary>
        public ucBaseWaveUpdate()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            InitializeRealtimeComponents();

            this.Disposed += new EventHandler(ucWave_Disposed);
            this.zedGraphControl1.GraphPane.YAxis.Scale.Min = 0;
            this.zedGraphControl1.GraphPane.YAxis.Scale.Max = 10;

            // 修改1：启用缩放功能
            this.zedGraphControl1.IsEnableZoom = true;
            this.zedGraphControl1.IsEnableHPan = true;  // 启用水平平移
            this.zedGraphControl1.IsEnableVPan = true;  // 启用垂直平移
            this.zedGraphControl1.IsEnableHZoom = true; // 启用水平缩放
            this.zedGraphControl1.IsEnableVZoom = true; // 启用垂直缩放
            this.zedGraphControl1.IsEnableWheelZoom = true; // 启用滚轮缩放

            // 修改2：设置缩放事件
            this.zedGraphControl1.ZoomEvent += ZedGraphControl1_ZoomEvent1;

            // 修改3：调整默认时间轴格式，减少子刻度密度
            SetDefaultTimeAxisFormat();

            // 确保子刻度线显示
            this.zedGraphControl1.GraphPane.XAxis.MinorTic.IsAllTics = true;
            this.zedGraphControl1.GraphPane.XAxis.MinorTic.IsOutside = true;
            this.zedGraphControl1.GraphPane.XAxis.MinorTic.IsInside = true;

            zedGraphControl1.GraphPane.Legend.IsVisible = false;
            zedGraphControl1.GraphPane.Legend.Border.IsVisible = false;
            zedGraphControl1.GraphPane.Legend.FontSpec = new FontSpec("微软雅黑", 11, this.ForeColor, Font.Bold, Font.Italic, Font.Underline);
            zedGraphControl1.GraphPane.Legend.FontSpec.Border.IsVisible = false;
            zedGraphControl1.GraphPane.YAxis.Title.FontSpec = new FontSpec("微软雅黑", 18, this.ForeColor, Font.Bold, Font.Italic, false);
            zedGraphControl1.GraphPane.YAxis.Title.FontSpec.Border.IsVisible = false;
            zedGraphControl1.GraphPane.YAxis.Title.FontSpec.Angle = 180;
            zedGraphControl1.GraphPane.XAxis.Title.FontSpec = new FontSpec("微软雅黑", 18, this.ForeColor, Font.Bold, Font.Italic, false);
            zedGraphControl1.GraphPane.XAxis.Title.FontSpec.Border.IsVisible = false;

            this.zedGraphControl1.GraphPane.YAxis.MajorGrid.IsVisible = true;
            this.zedGraphControl1.GraphPane.XAxis.MajorGrid.IsVisible = true;
            //this.zedGraphControl1.IsEnableZoom = false;
            this.zedGraphControl1.GraphPane.XAxis.Scale.MaxGrace = 0;
            this.zedGraphControl1.GraphPane.XAxis.Scale.MinGrace = 1;

            this.zedGraphControl1.GraphPane.YAxis.MajorGrid.IsVisible = true;
            this.zedGraphControl1.GraphPane.YAxis.MinorGrid.IsVisible = true;

        }

        /// <summary>
        /// 缩放事件处理器
        /// </summary>
        private void ZedGraphControl1_ZoomEvent1(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
        {
            // 更新显示时间格式，根据缩放级别调整
            AdjustTimeAxisFormatBasedOnScale();
        }


        /// <summary>
        /// 根据X轴范围调整时间轴格式
        /// </summary>
        private void AdjustTimeAxisFormatBasedOnScale()
        {
            double xRange = this.zedGraphControl1.GraphPane.XAxis.Scale.Max - this.zedGraphControl1.GraphPane.XAxis.Scale.Min;

            // 将XDate转换为天数
            double daysRange = xRange;

            if (daysRange > 2) // 超过2天
            {
                SetTimeAxisFormat(DateUnit.Day, 1.0, DateUnit.Hour, 6.0, "yyyy-MM-dd", true);
            }
            else if (daysRange > 0.5) // 超过12小时
            {
                SetTimeAxisFormat(DateUnit.Hour, 6.0, DateUnit.Hour, 1.0, "MM-dd HH:mm", true);
            }
            else if (daysRange > 0.083) // 超过2小时
            {
                SetTimeAxisFormat(DateUnit.Hour, 1.0, DateUnit.Minute, 30.0, "HH:mm", true);
            }
            else // 小于2小时
            {
                SetTimeAxisFormat(DateUnit.Minute, 30.0, DateUnit.Minute, 10.0, "HH:mm:ss", true);
            }

            // 强制刷新
            this.zedGraphControl1.Refresh();
        }

        // 修改默认时间轴格式方法
        public void SetDefaultTimeAxisFormat()
        {
            // 修改：使用更合理的默认值，包含年月日，减少子刻度密度
            SetTimeAxisFormat(DateUnit.Hour, 1.0, DateUnit.Minute, 30.0, "yyyy-MM-dd HH:mm", true);
        }

        /// <summary>
        /// 设置时间轴显示格式
        /// </summary>
        /// <param name="majorUnit">主刻度单位</param>
        /// <param name="majorStep">主刻度步长</param>
        /// <param name="minorUnit">子刻度单位</param>
        /// <param name="minorStep">子刻度步长</param>
        /// <param name="format">时间显示格式</param>
        /// <param name="showMinorTics">是否显示子刻度线</param>
        public void SetTimeAxisFormat(DateUnit majorUnit = DateUnit.Hour, double majorStep = 1.0,
                                      DateUnit minorUnit = DateUnit.Minute, double minorStep = 10.0,
                                      string format = "HH:mm:ss", bool showMinorTics = true)
        {
            // 设置X轴类型和格式
            this.zedGraphControl1.GraphPane.XAxis.Type = AxisType.Date;
            this.zedGraphControl1.GraphPane.XAxis.Scale.Format = format;

            // 设置主刻度
            this.zedGraphControl1.GraphPane.XAxis.Scale.MajorUnit = majorUnit;
            this.zedGraphControl1.GraphPane.XAxis.Scale.MajorStep = majorStep;

            // 设置子刻度
            this.zedGraphControl1.GraphPane.XAxis.Scale.MinorUnit = minorUnit;
            this.zedGraphControl1.GraphPane.XAxis.Scale.MinorStep = minorStep;

            // 设置子刻度线显示
            if (showMinorTics)
            {
                this.zedGraphControl1.GraphPane.XAxis.MinorTic.IsAllTics = true;
                this.zedGraphControl1.GraphPane.XAxis.MinorTic.IsOutside = true;
                this.zedGraphControl1.GraphPane.XAxis.MinorTic.IsInside = true;
            }
            else
            {
                this.zedGraphControl1.GraphPane.XAxis.MinorTic.IsAllTics = false;
            }

            // 刷新显示
            this.UpdateChanged();
        }


        /// <summary>
        /// 初始化实时组件
        /// </summary>
        private void InitializeRealtimeComponents()
        {
            // 初始化实时定时器
            realtimeTimer = new System.Windows.Forms.Timer();
            realtimeTimer.Interval = 1000; // 1秒更新一次
            realtimeTimer.Tick += RealtimeTimer_Tick;

            // 默认显示2小时数据（每秒1个点）
            MaxDisplayTime = TimeSpan.FromHours(2);
        }

        /// <summary>
        /// 实时定时器事件
        /// </summary>
        private void RealtimeTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                UpdateRealtimeGraph();
                Ticked?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"实时曲线更新异常: {ex.Message}");
            }
        }

        void ucWave_Disposed(object sender, EventArgs e)
        {
            StopRealtime();
            realtimeTimer?.Dispose();
        }

        #region 实时曲线相关属性

        private TimeSpan maxDisplayTime = TimeSpan.FromHours(2);
        /// <summary>
        /// 最大显示时间（默认2小时）
        /// </summary>
        [DefaultValue(typeof(TimeSpan), "02:00:00")]
        [Description("最大显示时间")]
        public TimeSpan MaxDisplayTime
        {
            get { return maxDisplayTime; }
            set
            {
                maxDisplayTime = value;
                maxDataPoints = (int)(maxDisplayTime.TotalSeconds / timeInterval);
                InitializeDataQueues();
            }
        }

        /// <summary>
        /// 采样间隔（秒）
        /// </summary>
        [DefaultValue(1.0)]
        [Description("采样间隔（秒）")]
        public double TimeInterval
        {
            get { return timeInterval; }
            set
            {
                if (value > 0)
                {
                    timeInterval = value;
                    maxDataPoints = (int)(maxDisplayTime.TotalSeconds / timeInterval);
                    InitializeDataQueues();
                }
            }
        }

        /// <summary>
        /// 是否启用实时模式
        /// </summary>
        [DefaultValue(false)]
        [Description("是否启用实时模式")]
        public bool RealtimeEnabled { get; private set; }

        #endregion

        #region 实时曲线方法

        /// <summary>
        /// 初始化数据队列
        /// </summary>
        private void InitializeDataQueues()
        {
            if (Waves != null && Waves.Length > 0)
            {
                dataQueues = new ConcurrentQueue<DataPoint>[Waves.Length];
                for (int i = 0; i < Waves.Length; i++)
                {
                    dataQueues[i] = new ConcurrentQueue<DataPoint>();
                }
            }
        }

        /// <summary>
        /// 开始实时曲线
        /// </summary>
        public void StartRealtime()
        {
            if (Waves == null || Waves.Length == 0)
            {
                throw new InvalidOperationException("请先设置Waves属性");
            }

            // 清空现有数据
            Clear();

            // 初始化数据队列
            InitializeDataQueues();

            // 记录开始时间
            startTime = DateTime.Now;

            // 启动定时器
            realtimeTimer.Start();
            RealtimeEnabled = true;
            watch.Start();

            Debug.WriteLine($"实时曲线已启动，最大显示时间: {MaxDisplayTime}");
        }

        /// <summary>
        /// 停止实时曲线
        /// </summary>
        public void StopRealtime()
        {
            realtimeTimer.Stop();
            RealtimeEnabled = false;
            watch.Stop();
            Debug.WriteLine("实时曲线已停止");
        }

        /// <summary>
        /// 添加实时数据点
        /// </summary>
        /// <param name="waveIndex">曲线索引</param>
        /// <param name="value">数据值</param>
        public void AddRealtimeData(int waveIndex, double value)
        {
            if (waveIndex < 0 || waveIndex >= dataQueues.Length)
                throw new ArgumentOutOfRangeException(nameof(waveIndex));

            var dataPoint = new DataPoint(DateTime.Now, value);
            dataQueues[waveIndex].Enqueue(dataPoint);

            // 保持队列长度不超过最大值
            while (dataQueues[waveIndex].Count > maxDataPoints)
            {
                DataPoint oldPoint;
                dataQueues[waveIndex].TryDequeue(out oldPoint);
            }
        }

        /// <summary>
        /// 批量添加实时数据点
        /// </summary>
        /// <param name="values">各曲线的数据值数组</param>
        public void AddRealtimeData(double[] values)
        {
            if (values.Length != dataQueues.Length)
                throw new ArgumentException("数据数组长度必须与曲线数量一致");

            var currentTime = DateTime.Now;
            for (int i = 0; i < values.Length; i++)
            {
                var dataPoint = new DataPoint(currentTime, values[i]);
                dataQueues[i].Enqueue(dataPoint);

                // 保持队列长度不超过最大值
                while (dataQueues[i].Count > maxDataPoints)
                {
                    DataPoint oldPoint;
                    dataQueues[i].TryDequeue(out oldPoint);
                }
            }
        }

        /// <summary>
        /// 更新实时曲线显示
        /// </summary>
        private void UpdateRealtimeGraph()
        {
            if (!RealtimeEnabled || dataQueues == null)
                return;

            // 更新各曲线数据
            for (int i = 0; i < Waves.Length; i++)
            {
                var waveList = Waves[i].List;
                waveList.Clear();

                // 从队列中获取数据并添加到曲线
                var dataPoints = dataQueues[i].ToArray();
                foreach (var dataPoint in dataPoints)
                {
                    // 使用XDate将DateTime转换为ZedGraph可用的double值
                    double x = new XDate(dataPoint.Time);
                    waveList.Add(x, dataPoint.Value);
                }

                // 自动调整X轴范围显示最近的数据
                if (dataPoints.Length > 0)
                {
                    DateTime latestTime = dataPoints.Last().Time;
                    DateTime earliestTime = latestTime - MaxDisplayTime;

                    this.zedGraphControl1.GraphPane.XAxis.Scale.Min = new XDate(earliestTime);
                    this.zedGraphControl1.GraphPane.XAxis.Scale.Max = new XDate(latestTime);

                    // 修改：根据时间范围动态调整显示格式
                    AdjustTimeAxisFormatBasedOnScale();
                }
            }

            // 刷新显示
            UpdateChanged();
        }

        #endregion

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
        /// 按照CSV方式将数据保存到指定文件（包含时间戳）
        /// </summary>
        public void SaveData(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // 写入表头
                writer.Write("Time");
                for (int i = 0; i < this.Waves.Length; i++)
                {
                    writer.Write($",{this.Waves[i].Name}");
                }
                writer.WriteLine();

                // 收集所有时间点
                var allTimes = new List<DateTime>();
                for (int i = 0; i < dataQueues.Length; i++)
                {
                    var times = dataQueues[i].Select(dp => dp.Time).Distinct();
                    allTimes.AddRange(times);
                }
                allTimes = allTimes.Distinct().OrderBy(t => t).ToList();

                // 写入数据
                foreach (var time in allTimes)
                {
                    writer.Write(time.ToString("yyyy-MM-dd HH:mm:ss"));

                    for (int i = 0; i < this.Waves.Length; i++)
                    {
                        var dataPoint = dataQueues[i].FirstOrDefault(dp => dp.Time == time);
                        writer.Write($",{dataPoint.Value:0.##}");
                    }
                    writer.WriteLine();
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

                // 初始化数据队列
                InitializeDataQueues();

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
        }

        /// <summary>
        /// 设置一条参考线，用于对比三条曲线
        /// </summary>
        public LineObj AddLine(double x1, double y1, double x2, double y2)
        {
            LineObj line = null;
            if (line == null)
            {
                line = new LineObj(Color.Red, x1, y1, x2, y2);
                line.ZOrder = ZOrder.B_BehindLegend;
                line.Location.AlignH = AlignH.Center;
                line.Location.AlignV = AlignV.Center;
                this.zedGraphControl1.GraphPane.GraphObjList.Add(line);
            }

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
        /// X轴最大值（在实时模式下自动管理）
        /// </summary>
        [DefaultValue(5d)]
        public double MaxX
        {
            get { return maxX; }
            set
            {
                this.maxX = value;
                if (!RealtimeEnabled)
                {
                    this.zedGraphControl1.GraphPane.XAxis.Scale.Max = value;
                }
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
        public int Interval
        {
            get { return realtimeTimer.Interval; }
            set
            {
                realtimeTimer.Interval = value;
                TimeInterval = value / 1000.0; // 转换为秒
            }
        }

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
            // 初始化代码
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

            // 清空数据队列
            if (dataQueues != null)
            {
                for (int i = 0; i < dataQueues.Length; i++)
                {
                    dataQueues[i] = new ConcurrentQueue<DataPoint>();
                }
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
                // 原有的定时器逻辑
                if (!RealtimeEnabled)
                {
                    Ticked?.Invoke(this, EventArgs.Empty);
                    this.UpdateChanged();
                }
            }
            catch (Exception ex)
            {
                timer1.Stop();
                Var.MsgBoxWarn(this, "曲线刷新异常:" + ex.Message);
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

        /// <summary>
        /// 获取实时数据的统计信息
        /// </summary>
        public Dictionary<int, DataStatistics> GetRealtimeStatistics()
        {
            var stats = new Dictionary<int, DataStatistics>();

            if (dataQueues != null)
            {
                for (int i = 0; i < dataQueues.Length; i++)
                {
                    var dataPoints = dataQueues[i].ToArray();
                    if (dataPoints.Length > 0)
                    {
                        var values = dataPoints.Select(dp => dp.Value).ToArray();
                        stats[i] = new DataStatistics
                        {
                            Count = dataPoints.Length,
                            Min = values.Min(),
                            Max = values.Max(),
                            Average = values.Average(),
                            LatestValue = values.Last(),
                            LatestTime = dataPoints.Last().Time
                        };
                    }
                }
            }

            return stats;
        }
    }

    /// <summary>
    /// 数据统计信息
    /// </summary>
    public class DataStatistics
    {
        public int Count { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public double Average { get; set; }
        public double LatestValue { get; set; }
        public DateTime LatestTime { get; set; }
    }

}
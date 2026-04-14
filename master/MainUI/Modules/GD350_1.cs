using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RW.Modules;
using static MainUI.Modules.EventArgsModel;

namespace MainUI.Modules
{
    /// <summary>
    /// 英威腾变频器 ,280kw
    /// </summary>
    public partial class GD350_1 : BaseModule
    {
        // 值改变后更新
        [Description("值改变后触发")]
        public event EventHandler<DoubleValueChangedEventArgs> KeyValueChange;

        public ConcurrentDictionary<string, double> DataValue = new ConcurrentDictionary<string, double>() { };

        public GD350_1()
        {
            this.Driver = Var.opcInverterModbus;
            InitializeComponent();
        }

        public GD350_1(IContainer container)
            : base(container)
        {
            this.Driver = Var.opcInverterModbus;
        }


        // 对象索引器
        public double this[string key]
        {
            get
            {
                return DataValue[key];
            }
            set
            {
                this.Write(key, value);
            }
        }

        public void Fresh()
        {
            foreach (var item in DataValue)
            {
                KeyValueChange?.Invoke(this, new DoubleValueChangedEventArgs(item.Key, item.Value));
            }
        }

        //10个 input  //0-9

        /// <summary>
        /// 1为正转，2为反转，3为停机
        /// </summary>
        public int RunStatusAI { get { return DataValue["运行状态"].ToInt(); } set { this.Write("运行状态", value); } }
        /// <summary>
        /// 就绪
        /// </summary>
        public int Ready { get { return DataValue["就绪"].ToInt(); } set { this.Write("就绪", value); } }
        /// <summary>
        /// 母线电压
        /// </summary>
        public int LineVoltage { get { return DataValue["母线电压检测"].ToInt(); } set { this.Write("母线电压检测", value); } }
        /// <summary>
        /// 输出电压
        /// </summary>
        public int OutputVoltage { get { return DataValue["输出电压检测"].ToInt(); } set { this.Write("输出电压检测", value); } }
        /// <summary>
        /// 输出电流
        /// </summary>
        public int OutputCurrent { get { return DataValue["输出电流检测"].ToInt(); } set { this.Write("输出电流检测", value); } }

        /// <summary>
        /// 输出频率
        /// </summary>
        public int OutputFrequency { get { return DataValue["运行频率"].ToInt(); } set { this.Write("运行频率", value); } }

        /// <summary>
        /// 输出转速
        /// </summary>
        public double OutputSpeed
        {
            get
            {
                if (!DataValue.ContainsKey("运行频率"))
                {
                    return 0;
                }

                double frequency = DataValue["运行频率"];
                // 方法2：检查频率值是否有效（例如，非负）
                if (frequency <= 0)
                {
                    return 0;
                }

                // 执行计算：转速 = (频率 * 60) / 7
                return Math.Round((frequency * 60) / 7, 1);
            }
        }

        /// <summary>
        /// 输出功率
        /// </summary>
        public int OutputPower { get { return DataValue["输出功率检测"].ToInt(); } set { this.Write("输出功率检测", value); } }

        /// <summary>
        /// 设置运行状态（1：运行  0：停止）
        /// </summary>
        public int SetRunStatus { get { return DataValue["启动_停止"].ToInt(); } set { this.Write("启动_停止", value); } }
        /// <summary>
        /// 设置频率
        /// </summary>
        public double SetFrequency { get { return DataValue["启动柜频率设定"].ToDouble(); } set { this.Write("启动柜频率设定", value); } }

        /// <summary>
        /// 启动柜启动
        /// </summary>
        public bool SetRun { get { return DataValue["启动柜启动"] == 1 ? true : false; } set { this.Write("启动柜启动", value); } }

        public int FaultCode { get { return DataValue["故障代码"].ToInt(); } set { this.Write("故障代码", value); } }


        public List<string> GetTag()
        {
            List<string> lst = new List<string>();
            //0 - 10
            lst.Add("运行状态");
            lst.Add("就绪");
            lst.Add("母线电压检测");
            lst.Add("输出电压检测");
            lst.Add("输出电流检测");
            lst.Add("输出功率检测");
            lst.Add("运行频率");

            lst.Add("启动_停止");
            lst.Add("启动柜频率设定");
            lst.Add("故障代码");
            return lst;
        }

        public List<string> GetDOTag()
        {
            List<string> lst = new List<string>();
            lst.Add("启动柜启动");
            return lst;
        }

        /// <summary>
        /// true 正常；false 异常
        /// </summary>
        public bool NoError { get; set; }
        /// <summary>
        /// true 模拟；false 非模拟
        /// </summary>
        public bool Simulated { get; set; }

        public override void Init()
        {
            List<string> lstTag = GetTag();
            List<string> lstDOTag = GetDOTag();

            // 先赋一个默认值
            foreach (var item in lstTag)
            {
                string pattern1 = @"[^.]+$";
                Match match1 = Regex.Match(item, pattern1);
                string key = match1.Value;
                DataValue.AddOrUpdate(key, 0, (k, oldValue) => 0);
            }
            DataValue.AddOrUpdate("启动柜启动", 0, (k, oldValue) => 0);

            for (int i = 0; i < lstTag.Count; i++)
            {
                int idx = i; // 循环中的i需要用临时变量存储。
                string opcTag = lstTag[i];
                this.Register<object>(opcTag, delegate (object value)
                {
                    double disPlayValue = Math.Round(value.ToDouble(), 2);

                    // 插入到线程字典
                    string pattern1 = @"[^.]+$";
                    Match match1 = Regex.Match(opcTag, pattern1);
                    if (match1.Success)
                    {
                        // 触发值改变事件
                        string key = match1.Value;
                        DataValue.AddOrUpdate(key, disPlayValue, (k, oldValue) => disPlayValue);

                        // 事件触发
                        KeyValueChange?.Invoke(this, new DoubleValueChangedEventArgs(key, disPlayValue));
                    }
                });
            }

            for (int i = 0; i < lstDOTag.Count; i++)
            {
                int idx = i; // 循环中的i需要用临时变量存储。
                string opcTag = lstDOTag[i];
                this.Register<bool>(opcTag, delegate (bool value)
                {
                    // 插入到线程字典
                    string pattern1 = @"[^.]+$";
                    Match match1 = Regex.Match(opcTag, pattern1);
                    if (match1.Success)
                    {
                        // 触发值改变事件
                        string key = match1.Value;
                        DataValue.AddOrUpdate(key, value ? 1 : 0, (k, oldValue) => value ? 1 : 0);

                        // 事件触发
                        KeyValueChange?.Invoke(this, new DoubleValueChangedEventArgs(key, value ? 1 : 0));
                    }
                });
            }

            //this.Register<bool>("Inverter._System._NoError", delegate (bool value)
            //{
            //    this.NoError = value;
            //});

            //this.Register<bool>("Inverter._System._Simulated", delegate (bool value)
            //{
            //    this.Simulated = value;
            //});


            base.Init();
        }
    }
}

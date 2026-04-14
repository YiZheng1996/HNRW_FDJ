using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RW.Modules;
using System.ComponentModel;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.Threading;
using static MainUI.Modules.EventArgsModel;

namespace MainUI.Modules
{
    /// <summary>
    /// 水系统数据采集
    /// </summary>
    public class WaterGrp : BaseSensorGroup
    {
        // 原始值
        ConcurrentDictionary<string, double> DataValue = new ConcurrentDictionary<string, double>() { };

        // 更新值
        public ConcurrentDictionary<string, double> NewDataValue = new ConcurrentDictionary<string, double>() { };

        // 校准参数字典(由零点增益界面进行赋值)
        public ConcurrentDictionary<string, CalibrationParams> calibrationParams = new ConcurrentDictionary<string, CalibrationParams>();

        public WaterGrp()
        {
            this.Driver = Var.opcWaterModbus;
        }

        public WaterGrp(IContainer container) : base(container) { }


        protected override void InitComponts()
        {
            this.Driver = Var.opcWaterModbus;
        }

        // 对象索引器
        public double this[string key]
        {
            get
            {
                if (NewDataValue.TryGetValue(key, out var val))
                {
                    return val;
                }
                return DataValue[key];
            }
        }

        // 值改变后更新
        [Description("值改变后触发")]
        public event EventHandler<DoubleValueChangedEventArgs> KeyValueChange;

        /// <summary>
        /// true 正常；false 异常
        /// </summary>
        public bool[] NoError { get; set; } = new bool[] { true, true, true, true };
        /// <summary>
        /// true 模拟；false 非模拟
        /// </summary>
        public bool[] Simulated { get; set; } = new bool[] { false, false, false, false };

        public bool IsSimulated => Simulated.All(x => x == true);

        public bool IsNoError => NoError.All(x => x == true);

        /// <summary>
        /// 高温水PID
        /// </summary>
        public double HeightTempWaterPID
        {
            get { return DataValue["高温水温度实时PID"]; }
            set
            {
                // 设置前一定需要发1111密码
                this.Write("Value1.高温水温度密码", 1111);
                Thread.Sleep(100);
                this.Write("Value1.高温水温度设置PID", value);
            }
        }

        /// <summary>
        /// 中冷水PID
        /// </summary>
        public double MediumColdPID
        {
            get { return DataValue["中冷水温度实时PID"]; }
            set
            {
                // 设置前一定需要发1111密码
                this.Write("Value2.中冷水温度密码", 1111);
                Thread.Sleep(100);
                this.Write("Value2.中冷水温度设置PID", value);
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public void Fresh()
        {
            foreach (var item in DataValue)
            {
                KeyValueChange?.Invoke(this, new DoubleValueChangedEventArgs(item.Key, item.Value));
            }
        }

        public override void Init()
        {
            string[] items = new string[] {
                "Value1.高温水温度实时PID","Value1.高温水温度设置PID","Value1.高温水温度密码",
                "Value2.中冷水温度实时PID","Value2.中冷水温度设置PID","Value2.中冷水温度密码",
                "Value3.中冷水冷却器进口温度检测-T14", "Value3.高温水冷却器进口温度检测-T13", "Value3.预热水箱温度检测-T12", "Value3.中冷水过滤器前压力检测-P9", "Value3.中冷水过滤器后压力检测-P10","Value3.高温水过滤器前压力检测-P6",
                "Value4.高温水过滤器后压力检测-P7", "Value4.预热水箱液位检测"
            };

            for (int i = 0; i < items.Count(); i++)
            {
                int tempIndex = i;
                var opcTag = items[tempIndex];

                //先赋一个默认值
                string inikey = GetKey(opcTag);
                DataValue.AddOrUpdate(inikey, 0, (k, oldValue) => 0);

                this.Register<double>(opcTag, delegate (double value)
                {
                    // 触发值改变事件
                    double disPlayValue = Math.Round(value, 1);
                    if (disPlayValue < 0 && disPlayValue > -200)
                    {
                        disPlayValue = 0;
                    }

                    // 插入到线程字典
                    string pattern1 = @"[^.]+$";
                    Match match1 = Regex.Match(opcTag, pattern1);
                    if (match1.Success)
                    {
                        // 触发值改变事件
                        string key = match1.Value;
                        DataValue.AddOrUpdate(key, disPlayValue, (k, oldValue) => disPlayValue);

                        // 立即应用校准
                        var newVal = ApplyCalibration(key, disPlayValue);

                        // 事件触发
                        KeyValueChange?.Invoke(this, new DoubleValueChangedEventArgs(key, newVal));
                    }
                });
            }


            for (int i = 0; i < 4; i++)
            {
                int tempIndex = i;
                this.Register<bool>($"Value{tempIndex + 1}._System._NoError", delegate (bool value)
                {
                    this.NoError[tempIndex] = value;
                });

                this.Register<bool>($"Value{tempIndex + 1}._System._Simulated", delegate (bool value)
                {
                    this.Simulated[tempIndex] = value;
                });
            }

            base.Init();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="rawValue"></param>
        private double ApplyCalibration(string key, double rawValue)
        {
            var returnData = rawValue;
            if (calibrationParams.TryGetValue(key, out var calParams))
            {
                double calibratedValue = Math.Round(rawValue * calParams.Gain - calParams.Zero, 1);
                returnData = calibratedValue;
            }
            NewDataValue.AddOrUpdate(key, returnData, (k, old) => returnData);
            return returnData;
        }

        /// <summary>
        /// 添加零点增益值
        /// </summary>
        /// <param name="key">Key值</param>
        /// <param name="calibration">零点增益值</param>
        public void AddOrUpdateCalibrationParams(string key, CalibrationParams calibration)
        {
            calibrationParams.AddOrUpdate(key, calibration, (k, old) => calibration);
        }

        // 从完整点位名中提取键名
        private string GetKey(string tag)
        {
            var match = Regex.Match(tag, @"[^.]+$");
            return match.Success ? match.Value : tag;
        }

    }
}

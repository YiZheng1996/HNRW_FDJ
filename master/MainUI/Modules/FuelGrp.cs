using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RW.Modules;
using System.ComponentModel;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using static MainUI.Modules.EventArgsModel;

namespace MainUI.Modules
{
    /// <summary>
    /// 燃油系统数据采集
    /// </summary>
    public class FuelGrp : BaseSensorGroup
    {
        // 原始值
        ConcurrentDictionary<string, double> DataValue = new ConcurrentDictionary<string, double>() { };

        // 更新值
        public ConcurrentDictionary<string, double> NewDataValue = new ConcurrentDictionary<string, double>() { };

        // 校准参数字典(由零点增益界面进行赋值)
        public ConcurrentDictionary<string, CalibrationParams> calibrationParams = new ConcurrentDictionary<string, CalibrationParams>();

        public FuelGrp()
        {
            this.Driver = Var.opcFuelModbus;
        }

        public FuelGrp(IContainer container) : base(container) { }

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

        protected override void InitComponts()
        {
            this.Driver = Var.opcFuelModbus;
        }

        // 值改变后更新
        [Description("值改变后触发")]
        public event EventHandler<DoubleValueChangedEventArgs> KeyValueChange;

        public void Fresh()
        {
            foreach (var item in DataValue)
            {
                KeyValueChange?.Invoke(this, new DoubleValueChangedEventArgs(item.Key, item.Value));
            }
        }

        /// <summary>
        /// true 正常；false 异常
        /// </summary>
        public bool[] NoError { get; set; } = new bool[] { true, true };
        /// <summary>
        /// true 模拟；false 非模拟
        /// </summary>
        public bool[] Simulated { get; set; } = new bool[] { false, false };

        public bool IsSimulated => Simulated.All(x => x == true);

        public bool IsNoError => NoError.All(x => x == true);

        public override void Init()
        {
            string[] items = new string[] {
                "Value1.粗滤器1前压力检测-P30", "Value1.粗滤器1后压力检测-P31", "Value1.粗滤器2前压力检测-P32", "Value1.粗滤器2后压力检测-P33", "Value1.精滤器1前压力检测-P34", "Value1.精滤器1后压力检测-P35",
                "Value2.精滤器2前压力检测-P36", "Value2.精滤器2后压力检测-P37", "Value2.柴油箱液位检测-L29", "Value2.水阻箱温度检测", "Value2.水阻箱极板位移检测"
            };

            // 先赋一个默认值
            foreach (var item in items)
            {
                string pattern1 = @"[^.]+$";
                Match match1 = Regex.Match(item, pattern1);
                string key = match1.Value;
                DataValue.AddOrUpdate(key, 0, (k, oldValue) => 0);
            }

            for (int i = 0; i < items.Count(); i++)
            {
                int tempIndex = i;
                var opcTag = items[tempIndex];
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

            for (int i = 0; i < 2; i++)
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

    }
}

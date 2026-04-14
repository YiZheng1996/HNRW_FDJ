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
    /// 转速数据采集
    /// </summary>
    public class SpeedGrp : BaseSensorGroup
    {
        public ConcurrentDictionary<string, double> DataValue = new ConcurrentDictionary<string, double>() { };

        public SpeedGrp()
        {
            this.Driver = Var.opcSpeedModbus;
        }
        public SpeedGrp(IContainer container) : base(container) { }


        protected override void InitComponts()
        {
            this.Driver = Var.opcSpeedModbus;
        }

        // 对象索引器
        public double this[string key]
        {
            get
            {
                return DataValue[key];
            }
        }

        // 值改变后更新
        [Description("值改变后触发")]
        public event EventHandler<DoubleValueChangedEventArgs> KeyValueChange;

        /// <summary>
        /// true 正常；false 异常
        /// </summary>
        public bool[] NoError { get; set; } = new bool[] { true, true, true };
        /// <summary>
        /// true 模拟；false 非模拟
        /// </summary>
        public bool[] Simulated { get; set; } = new bool[] { false, false, false };

        public bool IsSimulated => Simulated.All(x => x == true);

        public bool IsNoError => NoError.All(x => x == true);

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

        public double Speed1 { get { return DataValue["转速1"].ToDouble(); } set { this.Write("Value1.转速1", value); } }
        public double Speed2 { get { return DataValue["转速2"].ToDouble(); } set { this.Write("Value2.转速2", value); } }
        public double Speed3 { get { return DataValue["转速3"].ToDouble(); } set { this.Write("Value3.转速3", value); } }

        public override void Init()
        {
            string[] items = new string[] {
                "Value1.转速1","Value2.转速2","Value3.转速3"
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
                    // 插入到线程字典
                    string pattern1 = @"[^.]+$";
                    Match match1 = Regex.Match(opcTag, pattern1);
                    if (match1.Success)
                    {
                        // 触发值改变事件
                        double disPlayValue = Math.Round(value, 1);

                        // 触发值改变事件
                        string key = match1.Value;
                        if (key == "转速2" || key == "转速3") 
                        {
                            disPlayValue = disPlayValue / 180 * 60;
                        }

                        DataValue.AddOrUpdate(key, disPlayValue, (k, oldValue) => disPlayValue);

                        // 事件触发
                        KeyValueChange?.Invoke(this, new DoubleValueChangedEventArgs(key, disPlayValue));
                    }
                });
            }


            for (int i = 0; i < 3; i++)
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

        // 从完整点位名中提取键名
        private string GetKey(string tag)
        {
            var match = Regex.Match(tag, @"[^.]+$");
            return match.Success ? match.Value : tag;
        }
    }
}

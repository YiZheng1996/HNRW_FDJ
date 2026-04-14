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
    /// 励磁柜数据采集
    /// </summary>
    public class ExcitationGrp : BaseSensorGroup
    {
        // 实时值
        public ConcurrentDictionary<string, double> DataValue = new ConcurrentDictionary<string, double>() { };

        public ExcitationGrp()
        {
            this.Driver = Var.opcExcitationModbus;
        }

        public ExcitationGrp(IContainer container) : base(container) { }


        protected override void InitComponts()
        {
            this.Driver = Var.opcExcitationModbus;
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
                "励磁电流检测","励磁电压检测",
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

            base.Init();
        }

    }
}

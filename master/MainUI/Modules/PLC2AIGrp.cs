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
    /// 测量柜采集
    /// </summary>
    public class PLC2AIGrp : BaseSensorGroup
    {
        ConcurrentDictionary<string, double> DataValue = new ConcurrentDictionary<string, double>() { };

        public PLC2AIGrp()
        {
            this.Driver = Var.opcAI2Modbus;
        }
        public PLC2AIGrp(IContainer container) : base(container) { }


        protected override void InitComponts()
        {
            this.Driver = Var.opcAI2Modbus;
        }

        public ConcurrentDictionary<string, double> AIListData
        {
            get { return DataValue; }
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
        public bool NoError { get; set; } = true;
        /// <summary>
        /// true 模拟；false 非模拟
        /// </summary>
        public bool Simulated { get; set; } = false;


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
                "AI.A1缸排气温度",
                "AI.A2缸排气温度",
                "AI.A3缸排气温度",
                "AI.A4缸排气温度",
                "AI.A5缸排气温度",
                "AI.A6缸排气温度",
                "AI.A7缸排气温度",
                "AI.A8缸排气温度",
                "AI.B1缸排气温度",
                "AI.B2缸排气温度",
                "AI.B3缸排气温度",
                "AI.B4缸排气温度",
                "AI.B5缸排气温度",
                "AI.B6缸排气温度",
                "AI.B7缸排气温度",
                "AI.B8缸排气温度",
                "AI.P1高温水出机压力",
                "AI.P20机油泵出口压力",
                "AI.P21主油道进口油压",
                "AI.P2高温水泵进口压力",
                "AI.P38燃油供油压力",
                "AI.P3中冷水泵进口压力",
                "AI.P5中冷水出机压力",
                "AI.T1高温水出机温度",
                "AI.T20机油泵出口油温",
                "AI.T21主油道进口油温",
                "AI.T2高温水进机温度",
                "AI.T30燃油回油温度",
                "AI.T31燃油泵进口油温",
                "AI.T3中冷水进机温度",
                "AI.T5中冷水出机温度",
                "AI.高温水泵出口压力",
                "AI.后中冷前空气温度",
                "AI.后涡轮出口废气温度",
                "AI.后涡轮进口废气温度",
                "AI.后涡轮进口废气压力",
                "AI.后增压器机油出口温度",
                "AI.后增压器机油进口温度",
                "AI.后增压器机油进口压力",
                "AI.后增压器进气温度",
                "AI.后增压器进气真空度",
                "AI.后增压器排气背压",
                "AI.后中冷后空气温度",
                "AI.后中冷后空气压力",
                "AI.后中冷前空气压力",
                "AI.机油耗测量压力",
                "AI.机油耗测量液位",
                "AI.前涡轮出口废气温度",
                "AI.前涡轮进口废气温度",
                "AI.前涡轮进口废气压力",
                "AI.前增压器机油出口温度",
                "AI.前增压器机油进口温度",
                "AI.前增压器机油进口压力",
                "AI.前增压器进气温度",
                "AI.前增压器进气真空度",
                "AI.前增压器排气背压",
                "AI.前中冷后空气温度",
                "AI.前中冷后空气压力",
                "AI.前中冷前空气温度",
                "AI.前中冷前空气压力",
                "AI.中冷器出口水温",
                "AI.中冷器进口水温",
                "AI.中冷水泵出口压力",
                "AI.主油道末端油压",
                "AI.测功机U相温度",
                "AI.测功机V相温度",
                "AI.测功机W相温度",
                "AI.测功机D相温度",
                "AI.测功机N相温度",
                "AI.励磁电流检测",
                "AI.励磁电压检测",
            };
            // 先赋一个默认值
            foreach (var item in items)
            {
                string pattern1 = @"[^.]+$";
                Match match1 = Regex.Match(item, pattern1);
                string key = match1.Value;
                DataValue.AddOrUpdate(key, 0, (k, oldValue) => 0);
            }

            //string[] itemsDI = new string[] {
            //    //新增点位
            //    "DI.曲轴箱压力开关",
            //    "DI.盘车连锁开关",
            //    "DI.柴油机停机",
            //    "DI.柴油机卸载",
            //};
            //// 先赋一个默认值
            //foreach (var item in itemsDI)
            //{
            //    string pattern1 = @"[^.]+$";
            //    Match match1 = Regex.Match(item, pattern1);
            //    string key = match1.Value;
            //    DataValue.AddOrUpdate(key, 0, (k, oldValue) => 0);
            //}

            for (int i = 0; i < items.Count(); i++)
            {
                int tempIndex = i;
                var opcTag = items[tempIndex];
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
                        DataValue.AddOrUpdate(key, disPlayValue, (k, oldValue) => disPlayValue);

                        // 事件触发
                        KeyValueChange?.Invoke(this, new DoubleValueChangedEventArgs(key, disPlayValue));
                    }
                });
            }

            //for (int i = 0; i < itemsDI.Count(); i++)
            //{
            //    int tempIndex = i;
            //    var opcTag = itemsDI[tempIndex];
            //    this.Register<bool>(opcTag, delegate (bool value)
            //    {
            //        // 插入到线程字典
            //        string pattern1 = @"[^.]+$";
            //        Match match1 = Regex.Match(opcTag, pattern1);
            //        if (match1.Success)
            //        {
            //            // 触发值改变事件
            //            double disPlayValue = value ? 1 : 0;
            //            // 触发值改变事件
            //            string key = match1.Value;
            //            DataValue.AddOrUpdate(key, disPlayValue, (k, oldValue) => disPlayValue);

            //            // 事件触发
            //            KeyValueChange?.Invoke(this, new DoubleValueChangedEventArgs(key, disPlayValue));
            //        }
            //    });
            //}

            this.Register<bool>($"_System._NoError", delegate (bool value)
            {
                this.NoError = value;
            });

            this.Register<bool>($"_System._Simulated", delegate (bool value)
            {
                this.Simulated = value;
            });


            base.Init();
        }
    }
}

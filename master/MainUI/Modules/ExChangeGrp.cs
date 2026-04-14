using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using RW.Modules;
using static MainUI.Modules.EventArgsModel;

namespace MainUI.Modules
{
    public partial class ExChangeGrp : BaseModule
    {
        // 分开存储两种类型的数据
        public ConcurrentDictionary<string, double> _doubles = new ConcurrentDictionary<string, double>();
        public ConcurrentDictionary<string, bool> _bools = new ConcurrentDictionary<string, bool>();

        public ExChangeGrp()
        {
            this.Driver = Var.opcAOGroup;
            InitializeComponent();
        }

        // 设置 double 值
        public void SetDouble(string key, double value)
        {
            this.Write("交互." + key, value);
        }

        // 设置 bool 值
        public void SetBool(string key, bool value)
        {
            this.Write("交互.DO." + key, value);
        }

        // 获取 double 值
        public double GetDouble(string key)
        {
            return _doubles.TryGetValue(key, out var value) ? value : 0;
        }

        // 获取 bool 值
        public bool GetBool(string key)
        {
            return _bools.TryGetValue(key, out var value) && value;
        }

        public void Fresh()
        {
            foreach (var item in _doubles)
            {
                DoubleChanged?.Invoke(this, new DoubleValueChangedEventArgs(item.Key, item.Value));
            }
            foreach (var item in _bools)
            {
                KeyValueChange?.Invoke(this, new DIValueChangedEventArgs(item.Key, item.Value));
            }
        }

        // 事件定义
        [Description("值改变后触发")]
        public event EventHandler<DoubleValueChangedEventArgs> DoubleChanged;

        // 值改变后更新
        [Description("值改变后触发")]
        public event EventHandler<DIValueChangedEventArgs> KeyValueChange;

        public override void Init()
        {
            // double 类型的点位
            var doubleTags = new List<string>
            {
                "交互.待处理机油箱温度",
                "交互.待处理机油箱液位",
                "交互.高温水出机温度",
                "交互.高温水进机温度",
                "交互.机油出机压力",
                "交互.机油进机压力",
                "交互.机油箱温度",
                "交互.机油箱液位",
                "交互.内循环水箱液位",
                "交互.燃油进机温度",
                "交互.燃油进机压力",
                "交互.燃油箱液位",
                "交互.预热水箱温度",
                "交互.预热水箱液位",
                "交互.预热水箱加热温度设定",
                "交互.柴油机转速"
            };

            // bool 类型的点位
            var boolTags = new List<string>
            {
                "交互.DO.预热水箱加水",
                "交互.DO.预热水箱加热",
                "交互.DO.高温水预热循环",
                "交互.DO.预供机油循环",
                "交互.DO.机油箱加油",
                "交互.DO.机油加热处理循环",
                "交互.DO.油底壳加油",
                "交互.DO.油底壳抽油",
                "交互.DO.燃油耗测量",
                "交互.DO.机油回抽",
                "交互.DO.高温水中冷水回抽",
                "交互.DO.燃油循环",
                "交互.DO.燃油箱回油冷却",
                "交互.DO.油底壳抽油选择油箱",
                "交互.DO.燃油耗测量油泵选择",
                "交互.DO.燃油循环油泵选择",
                "交互.DO.上位机停机控制"
            };

            // 注册 double 点位
            foreach (var tag in doubleTags)
            {
                string pattern1 = @"[^.]+$";
                Match match1 = Regex.Match(tag, pattern1);
                string inkey = match1.Value;
                _doubles.AddOrUpdate(inkey, 0, (k, oldValue) => 0);

                this.Register<double>(tag, value =>
                {
                    var key = GetKey(tag);
                    var displayValue = Math.Round(value, 1);
                    _doubles[key] = displayValue;
                    DoubleChanged?.Invoke(this, new DoubleValueChangedEventArgs(key, displayValue));
                });
            }

            // 注册 bool 点位
            foreach (var tag in boolTags)
            {
                string pattern1 = @"[^.]+$";
                Match match1 = Regex.Match(tag, pattern1);
                string inkey = match1.Value;
                _bools.AddOrUpdate(inkey, false, (k, oldValue) => false);

                this.Register<bool>(tag, value =>
                {
                    var key = GetKey(tag);
                    _bools[key] = value;
                    KeyValueChange?.Invoke(this, new DIValueChangedEventArgs(key, value));
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
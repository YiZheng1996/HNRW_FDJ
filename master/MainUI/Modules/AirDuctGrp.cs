using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using RW.Modules;
using static MainUI.Modules.EventArgsModel;

namespace MainUI.Modules
{
    /// <summary>
    /// 风道加热
    /// </summary>
    public partial class AirDuctGrp : BaseModule
    {
        // 分开存储两种类型的数据
        public ConcurrentDictionary<string, double> _doubles = new ConcurrentDictionary<string, double>();
        public ConcurrentDictionary<string, bool> _bools = new ConcurrentDictionary<string, bool>();

        public AirDuctGrp()
        {
            this.Driver = Var.opcAirDuctModbus;
            InitializeComponent();
        }

        // 设置 double 值
        public void SetDouble(string key, double value)
        {
            this.Write(key, value);
        }

        // 设置 bool 值
        public void SetBool(string key, bool value)
        {
            this.Write(key, value);
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
                DoubleChange?.Invoke(this, new DoubleValueChangedEventArgs(item.Key, item.Value));
            }
            foreach (var item in _bools)
            {
                KeyValueChange?.Invoke(this, new DIValueChangedEventArgs(item.Key, item.Value));
            }
        }

        // 事件定义
        [Description("值改变后触发")]
        public event EventHandler<DoubleValueChangedEventArgs> DoubleChange;

        // 值改变后更新
        [Description("值改变后触发")]
        public event EventHandler<DIValueChangedEventArgs> KeyValueChange;

        // 值改变后更新
        [Description("值改变后触发")]
        public event EventHandler<DIValueChangedEventArgs> FaultKeyValueChange;

        public override void Init()
        {
            // double 类型的点位
            var doubleTags = new List<string>
            {
                "一号AI.一号显示出口温度",
                "一号AI.一号显示内腔温度1",
                "一号AI.一号显示内腔温度2",
                "一号AI.一号显示风机频率",

                "一号DO.一号设置出口温度",
                "一号DO.一号设置内腔温度",

                "二号AI.二号显示出口温度",
                "二号AI.二号显示内腔温度1",
                "二号AI.二号显示内腔温度2",
                "二号AI.二号显示风机频率",

                "二号DO.二号设置出口温度",
                "二号DO.二号设置内腔温度",
            };

            // bool 类型的点位
            var boolTags = new List<string>
            {
                "一号DO.一号一键启动",
                "一号DO.一号一键停止",
  
                "一号DI.一号风机运行",
                "一号DI.一号加热运行1",
                "一号DI.一号加热运行2",
                "一号DI.一号加热运行3",
                "一号DI.一号加热运行4",
                "一号DI.一号加热运行5",
                "一号DI.一号加热运行6",
                "一号DI.一号停止按下中间",

                "二号DO.二号一键启动",
                "二号DO.二号一键停止",

                "二号DI.二号风机运行",
                "二号DI.二号加热运行1",
                "二号DI.二号加热运行2",
                "二号DI.二号加热运行3",
                "二号DI.二号加热运行4",
                "二号DI.二号加热运行5",
                "二号DI.二号加热运行6",
                "二号DI.二号停止按下中间",
            };

            // 故障 bool 类型的点位
            var faultTags = new List<string>
            {
                "一号Fault.一号风机变频器故障",
                "一号Fault.一号风机冷却风扇过载",
                "一号Fault.一号出口超温",
                "一号Fault.一号内腔保护1超温",
                "一号Fault.一号内腔保护2超温",
                "一号Fault.一号温控器通讯错误",
                "一号Fault.一号变频器通讯错误",
                "一号Fault.一号相序保护故障",

                "二号Fault.二号风机变频器故障",
                "二号Fault.二号风机冷却风扇过载",
                "二号Fault.二号出口超温",
                "二号Fault.二号内腔保护1超温",
                "二号Fault.二号内腔保护2超温",
                "二号Fault.二号温控器通讯错误",
                "二号Fault.二号变频器通讯错误",
                "二号Fault.二号相序保护故障",
            };

            // 注册 double 点位
            foreach (var tag in doubleTags)
            {
                string inkey = GetKey(tag);
                _doubles.AddOrUpdate(inkey, 0, (k, oldValue) => 0);

                this.Register<double>(tag, value =>
                {
                    var key = GetKey(tag);
                    var displayValue = Math.Round(value, 1);
                    _doubles[key] = displayValue;
                    DoubleChange?.Invoke(this, new DoubleValueChangedEventArgs(key, displayValue));
                });
            }

            // 注册 bool 点位
            foreach (var tag in boolTags)
            {
                string inkey = GetKey(tag);
                _bools.AddOrUpdate(inkey, false, (k, oldValue) => false);

                this.Register<bool>(tag, value =>
                {
                    var key = GetKey(tag);
                    _bools[key] = value;
                    KeyValueChange?.Invoke(this, new DIValueChangedEventArgs(key, value));
                });
            }

            // 注册 bool 点位
            foreach (var tag in faultTags)
            {
                string inkey = GetKey(tag);
                _bools.AddOrUpdate(inkey, false, (k, oldValue) => false);

                this.Register<bool>(tag, value =>
                {
                    var key = GetKey(tag);
                    _bools[key] = value;
                    FaultKeyValueChange?.Invoke(this, new DIValueChangedEventArgs(key, value));
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
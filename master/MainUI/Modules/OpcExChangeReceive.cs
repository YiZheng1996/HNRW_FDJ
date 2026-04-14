using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MainUI.Global;
using RW.Modules;
using static MainUI.Modules.EventArgsModel;

namespace MainUI.Modules
{
    /// <summary>
    /// OPC 交互模块-接收端加载部分
    /// </summary>
    public partial class OpcExChangeReceive : BaseModule
    {
        // 值改变后更新
        [Description("值改变后触发")]
        public event EventHandler<DoubleValueChangedEventArgs> KeyValueChange;

        // 值改变后更新
        [Description("值改变后触发")]
        public event EventHandler<DIValueChangedEventArgs> KeyValueChangeBool;

        // 值改变后更新
        [Description("值改变后触发")]
        public event EventHandler<StringValueChangedEventArgs> KeyValueChangeStr;
        
        public ConcurrentDictionary<string, bool> BoolDataValue = new ConcurrentDictionary<string, bool>() { };

        public ConcurrentDictionary<string, double> DataValue = new ConcurrentDictionary<string, double>() { };

        public ConcurrentDictionary<string, string> SetDataValue = new ConcurrentDictionary<string, string>() { };

        public OpcExChangeReceive()
        {
            this.Driver = Var.opcExChangeReceive;
            InitializeComponent();
        }

        public OpcExChangeReceive(IContainer container)
            : base(container)
        {
            this.Driver = Var.opcExChangeReceive;
        }

        // 获取 double 值
        public double GetDouble(string key)
        {
            return DataValue.TryGetValue(key, out var value) ? value : 0;
        }

        // 获取 bool 值
        public bool GetBool(string key)
        {
            return BoolDataValue.TryGetValue(key, out var value) && value;
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
            foreach (var item in BoolDataValue)
            {
                KeyValueChangeBool?.Invoke(this, new DIValueChangedEventArgs(item.Key, item.Value));
            }
        }

        /// <summary>
        /// 重量
        /// </summary>
        public int Weight { get { return DataValue["重量"].ToInt(); } set { this.Write("重量", value); } }

        /// <summary>
        /// 当前型号
        /// </summary>
        //public string SetRun { get { return DataValue["启动柜启动"] == 1 ? true : false; } set { this.Write("启动柜启动", value); } }


        public List<string> GetDoubleTag()
        {
            List<string> lst = new List<string>();
            lst.Add("重量");
            lst.Add("剩余油量");
            lst.Add("油耗量");
            lst.Add("油耗仪状态");
            lst.Add("油量百分比");
            lst.Add("油耗仪_NoError");
            lst.Add("称重仪_NoError");
            return lst;
        }

        public List<string> GetBoolTag()
        {
            List<string> lst = new List<string>();
            return lst;
        }

        public List<string> GetStringTag()
        {
            List<string> lst = new List<string>();
            lst.Add("当前型号");
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
            List<string> lstDoubleTag = GetDoubleTag();
            List<string> lstStringTag = GetStringTag();
            List<string> lstBoolTag = GetBoolTag(); 
            // 先赋一个默认值
            foreach (var item in lstDoubleTag)
            {
                string opcTag = item;
                string pattern1 = @"[^.]+$";
                Match match1 = Regex.Match(item, pattern1);
                string key = match1.Value;
                DataValue.AddOrUpdate(key, 0, (k, oldValue) => 0);

                this.Register<double>(opcTag, delegate (double value)
                {
                    double disPlayValue = Math.Round(value.ToDouble(), 1);

                    // 插入到线程字典
                    string pattern = @"[^.]+$";
                    Match match = Regex.Match(opcTag, pattern);
                    if (match.Success)
                    {
                        // 触发值改变事件
                        string mkey = match.Value;
                        DataValue.AddOrUpdate(mkey, disPlayValue, (k, oldValue) => disPlayValue);

                        if (mkey == "重量")
                        {
                            MiddleData.instnce.PTFWeight = disPlayValue;
                        }

                        // 事件触发
                        KeyValueChange?.Invoke(this, new DoubleValueChangedEventArgs(key, disPlayValue));
                    }
                });
            }

            foreach (var item in lstStringTag)
            {
                string opcTag = item;
                string pattern1 = @"[^.]+$";
                Match match1 = Regex.Match(item, pattern1);
                string key = match1.Value;
                DataValue.AddOrUpdate(key, 0, (k, oldValue) => 0);

                this.Register<string>(opcTag, delegate (string value)
                {
                    // 插入到线程字典
                    string pattern = @"[^.]+$";
                    Match match = Regex.Match(opcTag, pattern);
                    if (match.Success)
                    {
                        // 触发值改变事件
                        string mkey = match.Value;
                        SetDataValue.AddOrUpdate(mkey, value, (k, oldValue) => value);

                        // 事件触发
                        KeyValueChangeStr?.Invoke(this, new StringValueChangedEventArgs(key, value));
                    }
                });
            }

            foreach (var item in lstBoolTag)
            {
                string opcTag = item;
                string pattern1 = @"[^.]+$";
                Match match1 = Regex.Match(item, pattern1);
                string key = match1.Value;
                BoolDataValue.AddOrUpdate(key, false, (k, oldValue) => false);

                this.Register<bool>(opcTag, delegate (bool value)
                {
                    // 插入到线程字典
                    string pattern = @"[^.]+$";
                    Match match = Regex.Match(opcTag, pattern);
                    if (match.Success)
                    {
                        // 触发值改变事件
                        string mkey = match.Value;
                        BoolDataValue.AddOrUpdate(mkey, value, (k, oldValue) => value);

                        // 事件触发
                        KeyValueChangeBool?.Invoke(this, new DIValueChangedEventArgs(key, value));
                    }
                });
            }

            this.Register<bool>("System.NoError", delegate (bool value)
            {
                this.NoError = value;
            });

            this.Register<bool>("System.Simulated", delegate (bool value)
            {
                this.Simulated = value;
            });


            base.Init();
        }
    }
}

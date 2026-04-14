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

namespace MainUI.Modules
{
    /// <summary>
    /// OPC 交互模块-发送端加载部分
    /// </summary>
    public partial class OpcExChangeSend : BaseModule
    {
        public ConcurrentDictionary<string, double> DataValue = new ConcurrentDictionary<string, double>() { };

        public ConcurrentDictionary<string, string> SetDataValue = new ConcurrentDictionary<string, string>() { };

        public OpcExChangeSend()
        {
            this.Driver = Var.opcExChangeSend;
            InitializeComponent();
        }

        public OpcExChangeSend(IContainer container)
            : base(container)
        {
            this.Driver = Var.opcExChangeSend;
        }

        // 设置 double 值
        public void SetDouble(string key, double value)
        {
            // 检查是否与上一次发送的值相同（考虑浮点数精度）
            if (DataValue.TryGetValue(key, out double lastValue))
            {
                if (Math.Abs(value - lastValue) < 0.05)
                {
                    // 值相同，不发送
                    return;
                }
            }

            // 值不同发送
            this.Write(key, value);
        }

        // 获取 double 值
        public double GetDouble(string key)
        {
            return DataValue.TryGetValue(key, out var value) ? value : 0;
        }

        /// <summary>
        /// 重量
        /// </summary>
        public int RunStatusAI { get { return DataValue["重量"].ToInt(); } set { this.Write("重量", value); } }
       
        /// <summary>
        /// 当前型号
        /// </summary>
        public string SetModel { get { return SetDataValue["当前型号"]; } set { this.Write("当前型号", value); } }

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
                    }
                });
            }

            base.Init();
        }
    }
}

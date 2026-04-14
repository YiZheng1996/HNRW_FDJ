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
    public partial class AIGrp : BaseModule
    {
        public const int AIcount = 17;
        public Hardware[] hardwares = new Hardware[AIcount];
        private double[] _AiList = new double[AIcount];
        ConcurrentDictionary<string, double> DataValue = new ConcurrentDictionary<string, double>() { };


        public double[] AIList
        {
            get { return _AiList; }
        }
        public double this[int index]
        {
            get { return AIList[index]; }
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

        public event ValueListHandler<double> AIvalueListChanged;
        public event ValueGroupHandler<double> AIvalueGrpChanged;
        public AIGrp()
        {
            this.Driver = Var.opcAIGroup;
            InitializeComponent();
            //for (int i = 0; i < hardwares.Length; i++)
            //{
            //    hardwares[i] = new Hardware();
            //}
        }
        public AIGrp(IContainer container)
            : base(container)
        {
            this.Driver = Var.opcAIGroup;
        }

        public void Fresh()
        {
            foreach (var item in DataValue)
            {
                KeyValueChange?.Invoke(this, new DoubleValueChangedEventArgs(item.Key, item.Value));
            }
        }


        public List<string> GetTag()
        {
            List<string> lst = new List<string>();
            // 0-3
            lst.Add("AI.大气温度");
            lst.Add("AI.大气湿度");
            lst.Add("AI.大气压力");
            lst.Add("AI.机油流量");
            // 4-7
            lst.Add("AI.清洁油罐来油流量");
            lst.Add("AI.燃油进油流量测量-L30");
            lst.Add("AI.燃油回油流量测量-L31");
            lst.Add("AI.L32");
            // 8-11
            lst.Add("AI.高温水流量测量-L3");
            lst.Add("AI.中冷水流量测量-L8");
            lst.Add("AI.进气流量测量左");
            lst.Add("AI.进气流量测量右");
            // 12 - 16
            lst.Add("AI.水阻箱进水调节阀开度");
            lst.Add("AI.前增压器进气流量计前温度");
            lst.Add("AI.后增压器进气流量计前温度");
            lst.Add("AI.厂房进气压力检测1");
            // 17 - 19
            lst.Add("AI.厂房进气压力检测2");
            return lst;
        }


        public override void Init()
        {
            List<string> lstTag = GetTag();

            // 先赋一个默认值
            foreach (var item in lstTag)
            {
                string pattern1 = @"[^.]+$";
                Match match1 = Regex.Match(item, pattern1);
                string key = match1.Value;
                DataValue.AddOrUpdate(key, 0, (k, oldValue) => 0);
            }

            for (int i = 0; i < AIcount; i++)
            {
                int idx = i; // 循环中的i需要用临时变量存储。
                string opcTag = lstTag[i];
                this.Register<double>(opcTag, delegate (double value)
                {
                    double disPlayValue = Math.Round(value, 1);
                    _AiList[idx] = disPlayValue;

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

                    if (AIvalueListChanged != null)
                        AIvalueListChanged(this, _AiList);
                    if (AIvalueGrpChanged != null)
                        AIvalueGrpChanged(this, idx, disPlayValue);
                });
            }
            base.Init();
        }

    }
}

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
    public partial class AOGrp : BaseModule
    {
        private double[] _AoList = new double[11];
        ConcurrentDictionary<string, double> DataValue = new ConcurrentDictionary<string, double>() { };

        public double[] AOList
        {
            get { return _AoList; }
        }

        public AOGrp()
        {
            this.Driver = Var.opcAOGroup;
            InitializeComponent();
        }

        public ConcurrentDictionary<string, double> AOListData
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
            set
            {
                this.Write("AO." + key, value);
            }
        }

        public void Fresh()
        {
            foreach (var item in DataValue)
            {
                KeyValueChange?.Invoke(this, new DoubleValueChangedEventArgs(item.Key, item.Value));
            }
        }

        /// <summary>
        /// 设置励磁值
        /// </summary>
        /// <param name="val"></param>
        public void SetExcitationVal(double val)
        {
            try
            {
                DataValue["励磁调节"] = val;
                this.Write("AO.励磁调节", val);
            }
            catch (Exception ex)
            {
                Var.LogInfo($"下发励磁电流出现问题。");
            }

        }

        public List<string> GetTag()
        {
            List<string> lst = new List<string>();
            //0-4
            lst.Add("AO.燃油泵1电动调节阀控制-170");
            lst.Add("AO.燃油泵旁路1电动调节阀控制-194");
            lst.Add("AO.水阻箱进水电动调节阀");
            lst.Add("AO.水泵出口电动调节阀控制-18");

            //5-8
            lst.Add("AO.进气风道左调节阀控制");
            lst.Add("AO.进气风道右调节阀控制");
            lst.Add("AO.排气风道左调节阀控制");
            lst.Add("AO.排气风道右调节阀控制");

            //9-10 （新增）
            lst.Add("AO.励磁调节");
            lst.Add("AO.发动机油门调节");
            lst.Add("AO.设置发动机最低转速");
            return lst;
        }

        public event ValueGroupHandler<double> AOvalueGrpChaned;
        // 值改变后更新
        [Description("值改变后触发")]
        public event EventHandler<DoubleValueChangedEventArgs> KeyValueChange;


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

            for (int i = 0; i < lstTag.Count(); i++)
            {
                int idx = i; // 循环中的i需要用临时变量存储。
                string opcTag = lstTag[i];

                this.Register<double>(opcTag, delegate (double value)
                {
                    double disPlayValue = Math.Round(value, 1);
                    _AoList[idx] = disPlayValue;

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

                    if (AOvalueGrpChaned != null)
                        AOvalueGrpChaned(this, idx, disPlayValue);
                });
            }
            base.Init();
        }
    }
}

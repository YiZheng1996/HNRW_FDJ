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
    /// 电流/电压传感器（三相） （输入）
    /// </summary>
    public class ThreePhaseElectric : BaseModule
    {
        string pre = "Value1.";

        /// <summary>
        /// true 正常；false 异常
        /// </summary>
        public bool NoError { get; set; }
        /// <summary>
        /// true 模拟；false 非模拟
        /// </summary>
        public bool Simulated { get; set; }

        //public override void Init()
        //{
        //    string[] items = new string[] { "Uuv", "Uvw", "Uwu", "Ia", "Ib", "Ic", "有功功率", "功率因数" };
        //    for (int i = 0; i < this.Hardwares.Count; i++)
        //    {
        //        int tempIndex = i;
        //        this.Register<double>(pre + items[tempIndex], delegate (double value)
        //        {
        //            OnValueGroupChanged(tempIndex, value);
        //        });
        //    }

        //    this.Register<bool>(pre + "_System._NoError", delegate (bool value)
        //     {
        //         this.NoError = value;
        //     });

        //    this.Register<bool>(pre + "_System._Simulated", delegate (bool value)
        //    {
        //        this.Simulated = value;
        //    });

        //    base.Init();
        //}

        //protected override void OnValueGroupChanged(int index, double value)
        //{
        //    // 触发值改变事件
        //    double disPlayValue = Math.Round(value, 1);

        //    switch (index)
        //    {
        //        case 0: VoltageU = disPlayValue; break;
        //        case 1: VoltageV = disPlayValue; break;
        //        case 2: VoltageW = disPlayValue; break;
        //        case 3: CurrentU = disPlayValue; break;
        //        case 4: CurrentV = disPlayValue; break;
        //        case 5: CurrentW = disPlayValue; break;
        //        case 6: Power = disPlayValue; break;
        //        case 7: PowerFactory = disPlayValue; break;
        //        default:
        //            break;
        //    }
        //    VoltageAvg = (VoltageU + VoltageV + VoltageW) / 3;
        //    CurrentAvg = (CurrentU + CurrentV + CurrentW) / 3;
        //    OnVoltageAvgChanged(VoltageAvg);
        //    OnCurrentAvgChanged(CurrentAvg);
        //}

        [Description("当平均电压发生变化时触发事件")]
        public event ValueHandler<double> VoltageAvgChanged;
        [Description("当平均电压发生变化时触发事件")]
        public event ValueHandler<double> CurrentAvgChanged;

        protected virtual void OnCurrentAvgChanged(double value)
        {
            if (CurrentAvgChanged != null) CurrentAvgChanged(this, value);
        }

        protected virtual void OnVoltageAvgChanged(double value)
        {
            if (VoltageAvgChanged != null) VoltageAvgChanged(this, value);
        }

        [DisplayName("U相电压")]
        [ReadOnly(true)]
        public double VoltageU { get; set; }

        [DisplayName("V相电压")]
        [ReadOnly(true)]
        public double VoltageV { get; set; }

        [DisplayName("W相电压")]
        [ReadOnly(true)]
        public double VoltageW { get; set; }

        [DisplayName("平均电压")]
        [ReadOnly(true)]
        public double VoltageAvg { get; set; }

        [DisplayName("U相电流")]
        [ReadOnly(true)]
        public double CurrentU { get; set; }

        [DisplayName("V相电流")]
        [ReadOnly(true)]
        public double CurrentV { get; set; }

        [DisplayName("W相电流")]
        [ReadOnly(true)]
        public double CurrentW { get; set; }

        [DisplayName("平均电流")]
        [ReadOnly(true)]
        public double CurrentAvg { get; set; }

        /// <summary>
        /// 有功功率 kW
        /// </summary>
        [DisplayName("有功功率")]
        public double Power { get; set; }

        [DisplayName("功率因数")]
        public double PowerFactory { get; set; }


        //新增修改点位绑定
        public const int AIcount = 7;
        private double[] _AiList = new double[7];
        public ConcurrentDictionary<string, double> DataValue = new ConcurrentDictionary<string, double>() { };
        // 值改变后更新
        [Description("值改变后触发")]
        public event EventHandler<DoubleValueChangedEventArgs> KeyValueChange;

        public event ValueListHandler<double> AIvalueListChanged;
        public event ValueGroupHandler<double> AIvalueGrpChanged;
        public ThreePhaseElectric()
        {
            this.Driver = Var.opcThreePhaseElectric;
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
            //0-4
            lst.Add($"{pre}Uwu");
            lst.Add($"{pre}Uvw");
            lst.Add($"{pre}Uuv");
            lst.Add($"{pre}Ia");
            //5-6
            lst.Add($"{pre}Ib");
            lst.Add($"{pre}Ic");
            lst.Add($"{pre}有功功率"); 
            return lst;
        }


        public override void Init()
        {
            List<string> lstTag = GetTag();

            for (int i = 0; i < AIcount; i++)
            {
                int idx = i; // 循环中的i需要用临时变量存储。
                string opcTag = lstTag[i];

                //先赋一个默认值
                string inikey = GetKey(opcTag);
                DataValue.AddOrUpdate(inikey, 0, (k, oldValue) => 0);

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

                this.Register<bool>($"{pre}_System._NoError", delegate (bool value)
                {
                    this.NoError = value;
                });

                this.Register<bool>($"{pre}_System._Simulated", delegate (bool value)
                {
                    this.Simulated = value;
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

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MainUI.Config.Modules;
using RW.Modules;
using static MainUI.Modules.EventArgsModel;

namespace MainUI.Modules
{
    public partial class DIGrp : BaseModule
    {
        public ConcurrentDictionary<string, bool> DataValue = new ConcurrentDictionary<string, bool>() { };
        
        DIModulesConfig dIModulesConfig = new DIModulesConfig();

        public DIGrp()
        {
            this.Driver = Var.opcDIGroup;
            dIModulesConfig.Save();
            InitializeComponent();
        }

        public DIGrp(IContainer container)
                        : base(container)
        {
            this.Driver = Var.opcDIGroup;
        }

        // 原始的事件
        public event ValueGroupHandler<bool> DIGroupChanged;

        // 值改变后更新
        [Description("值改变后触发")]
        public event EventHandler<DIValueChangedEventArgs> KeyValueChange;

        private const int DIcnt = 178;
        private bool[] _diList = new bool[DIcnt];
        public bool[] DIList
        {
            get { return _diList; }
        }

        public void Fresh()
        {
            foreach (var item in DataValue)
            {
                KeyValueChange?.Invoke(this, new DIValueChangedEventArgs(item.Key, item.Value));
            }

            //for (int i = 0; i < _diList.Length; i++)
            //{
            //    // 事件触发
            //    if (DIGroupChanged != null)
            //    {
            //        DIGroupChanged(this, i, _diList[i]);
            //    }
            //}
        }

        /// <summary>
        /// 操作台急停
        /// </summary>
        public bool EmergencyTestBed { get { return DIList[1]; } }

        // 本地模拟测试DI点变化； 现场实际不用
        public bool this[string key]
        {
            get
            {
                return DataValue[key];
            }
            set
            {
                this.Write("DI." + key, value);
            }
        }

        // 本地模拟测试DI点变化； 现场实际不用
        public bool this[int index]
        {
            get
            {
                return DIList[index];
            }
            set
            {

            }
        }


        public override void Init()
        {
            var lstTag = dIModulesConfig.Tags;
            dIModulesConfig.Save();

            // 先赋一个默认值
            foreach (var item in lstTag)
            {
                string pattern1 = @"[^.]+$";
                Match match1 = Regex.Match(item, pattern1);
                string key = match1.Value;
                DataValue.AddOrUpdate(key, false, (k, oldValue) => false);
            }

            for (int i = 0; i < DIcnt; i++)
            {
                int idx = i; // 循环中的i需要用临时变量存储。
                string opcTag = "DI." + lstTag[i];
                this.Register<bool>(opcTag, delegate (bool value)
                {
                    // 数组赋值
                    _diList[idx] = value;

                    // 插入到线程字典
                    string pattern1 = @"[^.]+$";
                    Match match1 = Regex.Match(opcTag, pattern1);
                    if (match1.Success)
                    {
                        // 触发值改变事件
                        string key = match1.Value;
                        DataValue.AddOrUpdate(key, value, (k, oldValue) => value);

                        // 事件触发
                        KeyValueChange?.Invoke(this, new DIValueChangedEventArgs(key, value));
                    }

                    if (DIGroupChanged != null)
                        DIGroupChanged(this, idx, value);
                });
            }
            base.Init();
        }

    }
}

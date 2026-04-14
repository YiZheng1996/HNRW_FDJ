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
    public partial class DOGrp : BaseModule
    {
        public ConcurrentDictionary<string, bool> DataValue = new ConcurrentDictionary<string, bool>() { };

        DOModulesConfig dOModulesConfig = new DOModulesConfig();

        public DOGrp()
        {
            this.Driver = Var.opcDOGroup;
            InitializeComponent();
        }

        public DOGrp(IContainer container)
            : base(container)
        {
            this.Driver = Var.opcDOGroup;
        }

        public const int DOCount = 59;

        private bool[] _doList = new bool[DOCount];
        public bool[] DOlist
        {
            get { return _doList; }
        }

        // 对象索引器
        public bool this[int index]
        {
            get
            {
                return DOlist[index];
            }
        }

        // 对象索引器
        public bool this[string key]
        {
            get
            {
                return DataValue[key];
            }
            set
            {
                this.Write("DO." + key, value);
            }
        }

        public void Fresh()
        {
            foreach (var item in DataValue)
            {
                KeyValueChange?.Invoke(this, new DIValueChangedEventArgs(item.Key, item.Value));
            }
        }

        // 原始的事件
        public event ValueGroupHandler<bool> DOgrpChanged;
        // 值改变后更新
        [Description("值改变后触发")]
        public event EventHandler<DIValueChangedEventArgs> KeyValueChange;

        public override void Init()
        {
            var lstTag = dOModulesConfig.Tags;
            dOModulesConfig.Save();

            // 先赋一个默认值
            foreach (var item in lstTag)
            {
                string pattern1 = @"[^.]+$";
                Match match1 = Regex.Match(item, pattern1);
                string key = match1.Value;
                DataValue.AddOrUpdate(key, false, (k, oldValue) => false);
            }

            for (int i = 0; i < lstTag.Count(); i++)
            {

                int idx = i; // 循环中的i需要用临时变量存储。
                string opcTag = "DO." + lstTag[i];
                this.Register<bool>(opcTag, delegate (bool value)
                {
                    // 数组赋值
                    _doList[idx] = value;

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

                    if (DOgrpChanged != null)
                        DOgrpChanged(this, idx, value);
                });
            }
            base.Init();
        }
    }
}

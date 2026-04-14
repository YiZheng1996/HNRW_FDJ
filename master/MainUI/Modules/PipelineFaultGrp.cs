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
    /// <summary>
    /// 管路故障模块
    /// </summary>
    public partial class PipelineFaultGrp : BaseModule
    {
        ConcurrentDictionary<string, FaultValueChangedEventArgs> faultEvents = new ConcurrentDictionary<string, FaultValueChangedEventArgs>();

        public ConcurrentDictionary<string, bool> DataValue = new ConcurrentDictionary<string, bool>() { };

        public PipelineFaultGrp()
        {
            this.Driver = Var.opcPipelineFaultGroup;
            InitializeComponent();
        }

        public PipelineFaultGrp(IContainer container)
                        : base(container)
        {
            this.Driver = Var.opcPipelineFaultGroup;
        }

        // 值改变后更新
        [Description("值改变后触发")]
        public event EventHandler<DIValueChangedEventArgs> KeyValueChange;

        /// <summary>
        /// 刷新
        /// </summary>
        public void Fresh()
        {
            foreach (var item in DataValue)
            {
                KeyValueChange?.Invoke(this, new DIValueChangedEventArgs(item.Key, item.Value));
            }
        }

        public List<string> GetTag()
        {
            List<string> lst = new List<string>();
            // 0-7
            lst.Add("Fault.燃油泵1过流");
            lst.Add("Fault.燃油泵2过流");
            lst.Add("Fault.预热水泵过流");
            lst.Add("Fault.污油排出泵过流");
            lst.Add("Fault.预供机油泵过流");
            lst.Add("Fault.抽油泵过流");
            lst.Add("Fault.主发通风机1过流");
            lst.Add("Fault.主发通风机2过流");
            lst.Add("Fault.水阻升降电机过流");
            // 水系统
            lst.Add("FaultWater.03阀故障");
            lst.Add("FaultWater.15阀故障");
            lst.Add("FaultWater.16阀故障");
            lst.Add("FaultWater.17阀故障");
            lst.Add("FaultWater.20阀故障");
            lst.Add("FaultWater.21阀故障");
            lst.Add("FaultWater.22阀故障");
            lst.Add("FaultWater.23阀故障");
            lst.Add("FaultWater.24阀故障");
            lst.Add("FaultWater.26阀故障");
            lst.Add("FaultWater.27阀故障");
            lst.Add("FaultWater.28阀故障");
            lst.Add("FaultWater.29阀故障");
            lst.Add("FaultWater.30阀故障");
            lst.Add("FaultWater.31阀故障");
            lst.Add("FaultWater.41阀故障");
            // 机油
            lst.Add("FaultEO.90阀故障");
            lst.Add("FaultEO.91阀故障");
            lst.Add("FaultEO.92阀故障");
            lst.Add("FaultEO.93阀故障");
            lst.Add("FaultEO.95阀故障");
            lst.Add("FaultEO.96阀故障");
            lst.Add("FaultEO.97阀故障");
            lst.Add("FaultEO.100阀故障");
            lst.Add("FaultEO.111阀故障");
            lst.Add("FaultEO.115阀故障");
            lst.Add("FaultEO.116阀故障");
            lst.Add("FaultEO.122阀故障");
            lst.Add("FaultEO.133阀故障");
            lst.Add("FaultEO.134阀故障");
            lst.Add("FaultEO.135阀故障");
            lst.Add("FaultEO.136阀故障");
            lst.Add("FaultEO.137阀故障");
            lst.Add("FaultEO.139阀故障");
            //燃油
            lst.Add("FaultFO.61阀故障");
            lst.Add("FaultFO.164阀故障");
            lst.Add("FaultFO.179阀故障");
            lst.Add("FaultFO.190阀故障");
            // 通讯
            lst.Add("Fault.控制间配电柜分布式IO通讯掉线");
            lst.Add("Fault.设备间配电柜分布式IO通讯掉线");
            lst.Add("Fault.机器间配电柜分布式IO通讯掉线");
            lst.Add("Fault.机油控制箱分布式IO通讯掉线");
            lst.Add("Fault.燃油控制箱分布式IO通讯掉线");
            lst.Add("Fault.水系统控制箱分布式IO通讯掉线");
            lst.Add("Fault.进排气控制箱分布式IO通讯掉线");
            lst.Add("Fault.启动柜通讯掉线");
            // 管路一键动作报错
            lst.Add("Fault.预热水箱水位低，高温水预热循环已停止");
            lst.Add("Fault.机油箱液位低，油底壳加油已停止");
            lst.Add("Fault.机油箱液位高，油底壳抽油已停止");
            lst.Add("Fault.待处理机油箱液位高，油底壳抽油已停止");
            lst.Add("Fault.燃油箱液位低，燃油循环已停止");
            lst.Add("Fault.机油箱液位高，机油回抽已停止");
            lst.Add("Fault.机油箱液位高，机油箱加油已停止");
            lst.Add("Fault.预热水箱水位高，预热水箱加水已停止");
            lst.Add("Fault.预热水箱箱液位高，中冷水/高温水回抽己停止");
            // 其他
            lst.Add("Fault.急停后转速不下降");
            return lst;
        }


        // 本地模拟测试DI点变化； 现场实际不用
        public bool this[string key]
        {
            get
            {
                return DataValue[key];
            }
        }

        public override void Init()
        {
            var tags = GetTag();
            for (int i = 0; i < tags.Count(); i++)
            {
                int idx = i; // 循环中的i需要用临时变量存储。
                string opcTag = tags[i];

                //先赋一个默认值
                string inikey = GetKey(opcTag);
                DataValue.AddOrUpdate(inikey, false, (k, oldValue) => false);

                // 根据标签前缀判断故障类型
                string type = "操作"; // 默认类型
                if (opcTag.StartsWith("FaultWater."))
                    type = "水系统";
                else if (opcTag.StartsWith("FaultEO."))
                    type = "机油系统";
                else if (opcTag.StartsWith("FaultFO."))
                    type = "燃油系统";
                else if (opcTag.StartsWith("Fault."))
                    type = ""; // 通用故障类型

                // 初始值设为false
                faultEvents.AddOrUpdate(inikey, new FaultValueChangedEventArgs(inikey, type, false), (k, oldValue) => new FaultValueChangedEventArgs(inikey, type, false));

                this.Register<bool>(opcTag, delegate (bool value)
                {
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


    // 故障事件
    public class FaultValueChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 点位名称
        /// </summary>
        public string Key { get; }
        /// <summary>
        /// 故障类型（水系统/燃油系统/机油系统/操作）
        /// </summary>
        public string Type { get; }
        /// <summary>
        /// 值
        /// </summary>
        public bool Value { get; }

        public FaultValueChangedEventArgs(string key,string type, bool value)
        {
            Key = key;
            Type = type;
            Value = value;
        }
    }
}

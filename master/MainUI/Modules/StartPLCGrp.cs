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
    /// 启动柜 S7200 
    /// </summary>
    public partial class StartPLCGrp : BaseModule
    {
        public ConcurrentDictionary<string, bool> DataValue = new ConcurrentDictionary<string, bool>() { };

        public event ValueGroupHandler<object> AOvalueGrpChaned;
        public StartPLCGrp()
        {
            this.Driver = Var.opcStartPLCModbus;
            InitializeComponent();
        }

        public StartPLCGrp(IContainer container)
            : base(container)
        {
            this.Driver = Var.opcStartPLCModbus;
        }

        //10个 input  //0-9
        public bool Auto { get { return Convert.ToBoolean(_AiList[0]); } set { this.Write("DI.Auto", value); } }
        public bool Scram { get { return Convert.ToBoolean(_AiList[1]); } set { this.Write("DI.Scram", value); } }
        public bool Fault { get { return Convert.ToBoolean(_AiList[2]); } set { this.PluseWrite("DI.变频器故障", 1000); } }
        public bool InverterOut { get { return Convert.ToBoolean(_AiList[3]); } set { this.Write("DI.变频器输出检测", value); } }
        public bool InverterRun { get { return Convert.ToBoolean(_AiList[4]); } set { this.Write("DI.变频器运行中", value); } }
        public bool OutputCurrent { get { return Convert.ToBoolean(_AiList[5]); } set { this.Write("DI.后门检测", value); } }
        public bool backDoor { get { return Convert.ToBoolean(_AiList[6]); } set { this.Write("DI.前门检测", value); } }

        public bool FaultReset
        {
            get { return Convert.ToBoolean(_AiList[7]); }
            set
            {
                if (NoError)
                    this.PluseWrite("DO.FaultReset", value, 1000);
            }
        }

        public bool InverterOutDo { get { return Convert.ToBoolean(_AiList[8]); } set { this.PluseWrite("DO.变频器输出合分闸", value, 1000); } }

        public List<string> GetTag()
        {
            List<string> lst = new List<string>();
            //0 - 6
            lst.Add("DI.Auto");
            lst.Add("DI.Scram");
            lst.Add("DI.变频器故障");
            lst.Add("DI.变频器输出检测");
            lst.Add("DI.变频器运行中");
            lst.Add("DI.后门检测");
            lst.Add("DI.前门检测");

            //9 - 10
            lst.Add("DO.FaultReset");
            lst.Add("DO.变频器输出合分闸");

            return lst;
        }

        private object[] _AiList = new object[10];

        /// <summary>
        /// true 正常；false 异常
        /// </summary>
        public bool NoError { get; set; }
        /// <summary>
        /// true 模拟；false 非模拟
        /// </summary>
        public bool Simulated { get; set; }

        public const int DICount = 32;
        private bool[] _listFault1 = new bool[DICount];
        public bool[] ListFault1
        {
            get { return _listFault1; }
        }

        private bool[] _listFault2 = new bool[DICount];
        public bool[] ListFault2
        {
            get { return _listFault2; }
        }

        private bool[] _listFault3 = new bool[DICount];
        public bool[] ListFault3
        {
            get { return _listFault3; }
        }

        public event ValueGroupHandler<bool> FaultgrpChanged1;
        public event ValueGroupHandler<bool> FaultgrpChanged2;
        public event ValueGroupHandler<bool> FaultgrpChanged3;

        public override void Init()
        {
            List<string> lstTag = GetTag();

            //// 先赋一个默认值
            //foreach (var item in lstTag)
            //{
            //    string pattern1 = @"[^.]+$";
            //    Match match1 = Regex.Match(item, pattern1);
            //    string key = match1.Value;
            //}

            for (int i = 0; i < lstTag.Count; i++)
            {
                int idx = i; // 循环中的i需要用临时变量存储。
                string opcTag = lstTag[i];
                this.Register<bool>(opcTag, delegate (bool value)
                {
                    // 数组赋值
                    _AiList[idx] = value;

                    if (AOvalueGrpChaned != null)
                        AOvalueGrpChaned(this, idx, value);

                    // 插入到线程字典
                    string pattern1 = @"[^.]+$";
                    Match match1 = Regex.Match(opcTag, pattern1);
                    if (match1.Success)
                    {
                        // 触发值改变事件
                        string key = match1.Value;
                        DataValue.AddOrUpdate(key, value, (k, oldValue) => value);
                    }

      
                });
            }

            //用VD700 表示32个故障，OPC不直观，代码不直观。改为32个bool量点位。
            for (int i = 0; i < DICount; i++)
            {
                int idx = i;
                int xh = i + 1;
                string tag = "FaultGrp1.Fault" + xh.ToString().PadLeft(2, '0');
                this.Register<bool>(tag, delegate (bool value)
                {
                    _listFault1[idx] = value;
                    if (FaultgrpChanged1 != null)
                        FaultgrpChanged1(this, idx, value);
                });
            }

            for (int i = 0; i < DICount; i++)
            {
                int idx = i;
                int xh = i + 1;
                string tag = "FaultGrp2.Fault" + xh.ToString().PadLeft(2, '0');
                this.Register<bool>(tag, delegate (bool value)
                {
                    _listFault2[idx] = value;
                    if (FaultgrpChanged2 != null)
                        FaultgrpChanged2(this, idx, value);
                });
            }

            for (int i = 0; i < DICount; i++)
            {
                int idx = i;
                int xh = i + 1;
                string tag = "FaultGrp3.Fault" + xh.ToString().PadLeft(2, '0');
                this.Register<bool>(tag, delegate (bool value)
                {
                    _listFault3[idx] = value;
                    if (FaultgrpChanged3 != null)
                        FaultgrpChanged3(this, idx, value);
                });
            }

            this.Register<bool>("_System._NoError", delegate (bool value)
            {
                this.NoError = value;
            });

            this.Register<bool>("_System._Simulated", delegate (bool value)
            {
                this.Simulated = value;
            });

            base.Init();
        }
    }
}

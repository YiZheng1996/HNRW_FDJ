using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.Modules;

namespace MainUI.Modules
{
    /// <summary>
    /// OPC状态类
    /// </summary>
    public partial class OpcStatusGrp : BaseModule
    {
        public OpcStatusGrp()
        {
            this.Driver = Var.opcStatus;
            InitializeComponent();
        }
        public OpcStatusGrp(IContainer container)
              : base(container)
        {
            this.Driver = Var.opcStatus;
        }
        [DefaultValue(false)]
        public bool NoError { get; set; }
        public bool Simulated { get; set; }
        public override void Init()
        {
            this.Register<bool>("_System._NoError", delegate (bool value) 
            { 
                NoError = value; 
            });
            this.Register<bool>("_System._Simulated", delegate (bool value) 
            {
                Simulated = value;
            });
            base.Init();
        }
    }
}

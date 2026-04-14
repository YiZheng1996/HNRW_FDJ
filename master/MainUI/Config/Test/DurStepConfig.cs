
using RW.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Config.Test
{
    /// <summary>
    /// 360小时具体试验步骤
    /// </summary>
    public class DurStepConfig: IniConfig
    {
        public DurStepConfig(string model, string step)
            : base(Application.StartupPath + $"\\config\\{model}_{step}.ini")
        {
            this.SetSectionName(step);
            Load();
        }

        /// <summary>
        /// 循环节点数据
        /// </summary>
        [IniKeyName("试验参数")]
        public List<TestBasePara> testBasePara { get; set; } = new List<TestBasePara>();

    }
}

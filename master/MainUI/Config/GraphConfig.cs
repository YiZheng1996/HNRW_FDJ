using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.Configuration;
using System.Windows.Forms;
using System.ComponentModel;

namespace MainUI.Config
{
    public class GraphConfig : IniConfig
    {

        public GraphConfig()
     : base(Application.StartupPath + $"\\config\\Graph.ini")
        {

        }

        public GraphConfig(string model)
          : base(Application.StartupPath + $"\\config\\Graph.ini")
        {
            this.SetSectionName(model);
            this.Load();
        }

        /// <summary>
        /// 图表参数列表
        /// </summary>
        [IniKeyName("图表参数")]
        public List<GraphData> graphDatas { get; set; } = new List<GraphData>();

    }

    /// <summary>
    /// 具体的仪表盘参数类
    /// </summary>
    public class GraphData
    {
        /// <summary>
        /// 最大值
        /// </summary>
        [IniKeyName("最大值")]
        public double MaxVal { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        [IniKeyName("最小值")]
        public double MinVal { get; set; }

        /// <summary>
        /// 报警值
        /// </summary>
        [IniKeyName("报警值")]
        public double ScarmVal { get; set; }

        /// <summary>
        /// 绑定点位（程序用）
        /// </summary>
        [IniKeyName("绑定点位")]
        public string Point { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [IniKeyName("单位")]
        public string Unit { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [IniKeyName("显示名称")]
        public string Name { get; set; }


        /// <summary>
        /// 报警处理（0：默认报警  1：停机）
        /// </summary>
        [IniKeyName("报警处理")]
        public int ScarmTodo { get; set; }
    }
}

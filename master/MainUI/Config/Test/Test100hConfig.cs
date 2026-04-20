using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RW.Configuration;

namespace MainUI.Config.Test
{
    public class Test100hConfig : IniConfig
    {
        public Test100hConfig()
           : base(Application.StartupPath + "\\config\\Para.ini")
        {
        }

        public Test100hConfig(string model,string testName)
            : base(Application.StartupPath + $"\\config\\{model}.ini")
        {
            this.SetSectionName("性能试验");
            Load();
        }

        /// <summary>
        /// 执行阶段列表
        /// </summary>
        [IniKeyName("执行阶段列表")]
        public List<TestStepList> testStepLists { get; set; } = new List<TestStepList>();

    }

    /// <summary>
    /// 列表
    /// </summary>
    public class TestStepList
    {
        /// <summary>
        /// 步骤号
        /// </summary>
        [DisplayName("步骤号")]
        public int Index { get; set; }

        /// <summary>
        /// 阶段名称
        /// </summary>
        [DisplayName("所属试验阶段名")]
        public string TestName { get; set; }

        /// <summary>
        /// 天数(备注，展示用无实际效果)
        /// </summary>
        [IniKeyName("天数")]
        public string DayNum { get; set; }

        /// <summary>
        /// 采集时间间隔(min)，暂时没有用到，保留
        /// </summary>
        [DisplayName("采集时间间隔")]
        public double CollectIntervalTime { get; set; }

        /// <summary>
        /// 列表循环次数（用于生成最终的列表），暂时没有用到，保留
        /// </summary>
        [DisplayName("列表循环次数")]
        public int ForeachNum { get; set; } = 1;

        /// <summary>
        /// 具体执行列表
        /// </summary>
        public List<TestBasePara> testBasePara { get; set; } = new List<TestBasePara>();
    }

    /// <summary>
    /// 子步骤列表
    /// </summary>
    [Serializable]
    public class TestBasePara
    {
        /// <summary>
        /// 阶段名称
        /// </summary>
        [DisplayName("所属试验阶段名")]
        public string TestName { get; set; }

        /// <summary>
        /// 所属阶段
        /// </summary>
        [DisplayName("所属阶段")]
        public string PhaseName { get; set; }

        /// <summary>
        /// 周期号
        /// </summary>
        [DisplayName("所属周期号")]
        public string CycleName { get; set; }

        /// <summary>
        /// 循环代码名（A/B/C/A`）
        /// </summary>
        [DisplayName("循环代码名")]
        public string StepName { get; set; }

        /// <summary>
        /// 工况编号
        /// </summary>
        [DisplayName("工况编号")]
        public string GKNo { get; set; }

        /// <summary>
        /// 步骤号
        /// </summary>
        [DisplayName("步骤号")]
        public int Index { get; set; }

        /// <summary>
        /// 扭矩，显示用，无实际效果
        /// </summary>
        [DisplayName("发电机扭矩")]
        public double Torque { get; set; }

        /// <summary>
        /// 转速，显示用，无实际效果
        /// </summary>
        [DisplayName("发动机转速")]
        public double RPM { get; set; }

        /// <summary>
        /// 试验运行时间
        /// </summary>
        [DisplayName("试验运行时间")]
        public double RunTime { get; set; }

        /// <summary>
        /// 采集时间间隔(min)
        /// </summary>
        [DisplayName("采集时间间隔")]
        public double CollectIntervalTime { get; set; }

    }

}

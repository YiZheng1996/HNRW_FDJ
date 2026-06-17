using System.Windows.Forms;
using RW.Configuration;
using MainUI.Global;

namespace MainUI.Config
{
    /// <summary>
    /// 按"型号 + 试验类型"区分的标定参数
    /// 存储于 {model}.ini 的 [{model}_例行] / [{model}_型式] 段
    /// </summary>
    public class TrialParaConfig : IniConfig
    {
        public TrialParaConfig() : base(Application.StartupPath + "\\config\\Para.ini")
        {
        }

        public TrialParaConfig(string model, TrialTypeEnum trialType)
            : base(Application.StartupPath + $"\\config\\{model}.ini")
        {
            this.SetSectionName($"{model}_{trialType.SectionSuffix()}");
            Load();
        }

        /// <summary>
        /// 极对数（转速传感器换算用）
        /// </summary>
        [IniKeyName("极对数")]
        public int PolePairs { get; set; } = 0;

        /// <summary>
        /// 最小工作转速
        /// </summary>
        [IniKeyName("最小工作转速")]
        public int MinSpeed { get; set; } = 350;

        /// <summary>
        /// 最大工作转速
        /// </summary>
        [IniKeyName("最大工作转速")]
        public int MaxSpeed { get; set; } = 0;
    }
}
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
    /// <summary>
    /// 实时试验状态存储类
    /// </summary>
    public class CurrentStatusConfig : IniConfig
    {
        public CurrentStatusConfig()
          : base(Application.StartupPath + "\\config\\CurrentStatusConfig.ini")
        {

        }
        public CurrentStatusConfig(string model,string sectionName)
            : base(Application.StartupPath + "\\config\\CurrentStatusConfig.ini")
        {
            this.SetSectionName(sectionName);
            Load();
        }

        /// <summary>
        /// 是否测试完成(true：完成  false：未完成)
        /// </summary>
        [IniKeyName("是否测试完成")]
        public bool TestStatus { get; set; }

        /// <summary>
        /// 当前执行步骤数
        /// </summary>
        [IniKeyName("当前执行步骤数")]
        public int Sore { get; set; }

        /// <summary>
        /// 步骤总步数
        /// </summary>
        [IniKeyName("步骤总步数")]
        public int AllSore { get; set; }

        ///// <summary>
        ///// 阶段开始序号
        ///// </summary>
        //[IniKeyName("阶段开始序号")]
        //public int PhaseBeginSore { get; set; }

        /// <summary>
        /// 当前阶段
        /// </summary>
        [IniKeyName("当前阶段")]
        public string PhaseName { get; set; }

        /// <summary>
        /// 当前试验周期
        /// </summary>
        [IniKeyName("当前试验周期")]
        public string CycleName { get; set; }

        /// <summary>
        /// 当前所在天数
        /// </summary>
        [IniKeyName("当前所在天数")]
        public string DayName { get; set; }

        /// <summary>
        /// 当前循环代码(A/B/C)（标定功率，交替负荷..）
        /// </summary>
        [IniKeyName("当前循环代码")]
        public string NodeName { get; set; }

        /// <summary>
        /// 当前循环代码累加 360h(1A/1B/1C/2A) 100h保持不变（标定功率，交替负荷..）
        /// </summary>
        [IniKeyName("当前循环代码累加")]
        public string NodeNameAccumulate { get; set; }

        /// <summary>
        /// 当前工况编号
        /// </summary>
        [IniKeyName("当前工况编号")]
        public string GKBH { get; set; }

        /// <summary>
        /// 目标转速
        /// </summary>
        [IniKeyName("目标转速")]
        public double Speed { get; set; }

        /// <summary>
        /// 目标励磁电流
        /// </summary>
        [IniKeyName("目标励磁电流")]
        public double ExcitationCurrent { get; set; }

        /// <summary>
        /// 目标转速百分比
        /// </summary>
        [IniKeyName("当前转速百分比")]
        public double TargetSpeedPercent { get; set; }

        /// <summary>
        /// 目标扭矩百分比
        /// </summary>
        [IniKeyName("当前扭矩百分比")]
        public double TargetTorquePercent { get; set; }

        /// <summary>
        /// 当前工况试验时间
        /// </summary>
        [IniKeyName("当前工况试验时间")]
        public double TargetOperationTime { get; set; }

        /// <summary>
        /// 目标功率
        /// </summary>
        [IniKeyName("目标功率")]
        public double TargetPower { get; set; }

        /// <summary>
        /// 当前代码步数
        /// </summary>
        [IniKeyName("当前代码步数")]
        public int PhaseIndex { get; set; }

        /// <summary>
        /// 代码总步数
        /// </summary>
        [IniKeyName("代码总步数")]
        public int AllPhaseIndex { get; set; }

        /// <summary>
        /// 阶段已经采集的次数
        /// </summary>
        [IniKeyName("阶段采集次数")]
        public int PhaseCollectNum { get; set; }

        /// <summary>
        /// 阶段采集间隔时间（min）
        /// </summary>
        [IniKeyName("阶段采集间隔时间")]
        public double PhaseCollectTime { get; set; }

        /// <summary>
        /// 工况已运行时间min
        /// </summary>
        [IniKeyName("工况已运行时间")]
        public double StepTime { get; set; }


        /// <summary>
        /// 试验已运行时间min
        /// </summary>
        [IniKeyName("试验已运行时间")]
        public double StepTimeTotal { get; set; }
    }
}

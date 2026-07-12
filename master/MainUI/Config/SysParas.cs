using MainUI.Global;
using RW.Configuration;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MainUI.Config
{
    /// <summary>
    /// 系统参数
    /// </summary>
    public class SysParas : IniConfig
    {
        public SysParas()
            : base(Application.StartupPath + "\\config\\SysParas.ini")
        {
            this.SetSectionName("SysParas");
            this.Load();
        }

        private int _LastLoginIndex = -1;
        /// <summary>
        /// 上一次登录的用户ID
        /// </summary>
        public int LastLoginIndex { get { return _LastLoginIndex; } set { _LastLoginIndex = value; } }

        /// <summary>
        /// 检修模式
        /// </summary>
        public bool RepairModel { get; set; } = false;

        /// <summary>
        /// 发动机运行时间 min
        /// </summary>
        [IniKeyName("发动机运行时间")]
        public double RunTime { get; set; }


        /// <summary>
        /// 发动机怠速运行时间 min
        /// </summary>
        [IniKeyName("发动机怠速运行时间")]
        public double dsRunTime { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceModel { get; set; }

        /// <summary>
        /// 最后选择的型号
        /// </summary>
        [IniKeyName("最后选择的型号")]
        public string LastModel { get; set; }

        /// <summary>
        /// 最后选择的型号类型
        /// </summary>
        [IniKeyName("最后选择的型号类型")]
        public string LastModelType { get; set; }

        /// <summary>
        /// 最后选择的试验类型 0：例行试验  1：型式试验
        /// </summary>
        [IniKeyName("最后选择的试验类型")]
        public int LastTrialType { get; set; } = 0;

        /// <summary>
        /// 最后选择的试验类型（枚举形式，便于代码判断；写入会同步持久化字段）
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public TrialTypeEnum LastTrialTypeEnum
        {
            get { return (TrialTypeEnum)LastTrialType; }
            set { LastTrialType = (int)value; }
        }

        /// <summary>
        /// 当前试验编号
        /// </summary>
        [IniKeyName("试验编号")]
        public string TestNo { get; set; }

        /// <summary>
        /// 手动记录当前批次主表ID（断电续接用）。
        /// 非空表示有未结束的批次，下次记录直接追加到该批次；
        /// 为空表示需要新建批次。
        /// </summary>
        [IniKeyName("手动记录批次ID")]
        public string ManualRecordMGid { get; set; } = "";

        /// <summary>
        /// 手动记录当前批次已记录条数（断电续接时从此值继续自增）。
        /// </summary>
        [IniKeyName("手动记录条数")]
        public int ManualRecordIndex { get; set; } = 0;

        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceNo { get; set; }

        /// <summary>
        /// 程序类型 1：主控程序  2：数据监控
        /// </summary>
        public int ExeType { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string DeviceLimit { get; set; }

        /// <summary>
        /// 扭矩称重端口
        /// </summary>
        [IniKeyName("扭矩称重端口")]
        public int PT650FCOM { get; set; }

        /// <summary>
        /// 燃油耗仪称重端口
        /// </summary>
        [IniKeyName("燃油耗仪称重端口")]
        public int ET4500COM { get; set; }

        /// <summary>
        /// 磅秤端口
        /// </summary>
        [IniKeyName("磅秤端口")]
        public int BCCOM { get; set; }

        /// <summary>
        /// 360小时流程步骤
        /// </summary>
        [IniKeyName("360小时流程")]
        public List<string> TestStep360 { get; set; } = new List<string>
        {
            "A", "A1","B", "C", "D", "E", "F", "G", "H", "I",
            "L", "M", "N", "O", "P", "Q", "R", "S", "T"
        };

        ////允许设置电压，电流，功率值范围，软件界面显示处理，范围内字体绿色，范围外字体红色
        //public double VoltMin { get; set; }
        //public double VoltMax { get; set; }
        //public double CurrentMin { get; set; }
        //public double CurrentMax { get; set; }
        //public double PowerMin { get; set; }
        //public double PowerMax { get; set; }

        /// <summary>
        /// 报表开始行数
        /// </summary>
        public int RowStart { get; set; } = 6;

        /// <summary>
        ///电压小数点位数
        /// </summary>
        [IniKeyName("电压小数点位数")]
        public string DotStrVolt { get; set; } = "f2";
        /// <summary>
        ///电流小数点位数
        /// </summary>
        [IniKeyName("电流小数点位数")]
        public string DotStrCurrent { get; set; } = "f2";
        /// <summary>
        ///功率小数点位数
        /// </summary>
        [IniKeyName("功率小数点位数")]
        public string DotStrPowr { get; set; } = "f2";

        /// <summary>
        ///启机甩车长按超时ms
        /// </summary>
        [IniKeyName("启机甩车长按超时")]
        public int StartupHoldTimeoutMs { get; set; } = 5000;

        /// <summary>
        ///励磁输出单次调节幅度
        /// </summary>
        [IniKeyName("励磁输出单次调节幅度")]
        public int MaxExcitationSingleStep { get; set; } = 80;

        /// <summary>
        /// 燃油油耗率
        /// </summary>
        [IniKeyName("燃油油耗率")]
        public double FOilNum { get; set; }



        #region 手动出厂试验表头数据

        [IniKeyName("出厂报表_试验项目")]
        public string ManualReportTestProject { get; set; } = "";

        [IniKeyName("出厂报表_增压器型号")]
        public string ManualReportSuperchargerModel { get; set; } = "";

        [IniKeyName("出厂报表_增压器出厂编号")]
        public string ManualReportSuperchargerSN { get; set; } = "";

        [IniKeyName("出厂报表_试验台位号")]
        public string ManualReportTestBenchNo { get; set; } = "";

        [IniKeyName("出厂报表_主发电机编号")]
        public string ManualReportMainGeneratorNo { get; set; } = "";

        [IniKeyName("出厂报表_平均外温")]
        public double ManualReportAvgOutsideTemp { get; set; } = 0;

        [IniKeyName("出厂报表_平均大气压力")]
        public double ManualReportAvgAtmPressure { get; set; } = 0;

        [IniKeyName("出厂报表_相对湿度")]
        public double ManualReportHumidity { get; set; } = 0;

        [IniKeyName("出厂报表_机油牌号")]
        public string ManualReportOilGrade { get; set; } = "";

        [IniKeyName("出厂报表_燃油牌号")]
        public string ManualReportFuelGrade { get; set; } = "";

        [IniKeyName("出厂报表_前增压器编号")]
        public string ManualReportSuperchargerSNFront { get; set; } = "";

        [IniKeyName("出厂报表_后增压器编号")]
        public string ManualReportSuperchargerSNAfter { get; set; } = "";

        #endregion
    }



}

using RW.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }



}

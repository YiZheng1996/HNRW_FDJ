using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetorSignalSimulator.UI.Model
{
    public class ExcelModel
    {
        public ExcelModel() { }

        public string CarType { get; set; }
        public string SignalName { get; set; }
        public string DataType { get; set; }

        [MiniExcelLibs.Attributes.ExcelColumnName("comID")]
        public string ComID { get; set; } = "12500"; //comID
        public int ByteOffset { get; set; }
        public int BitOffset { get; set; }

        /// <summary>
        /// 缩放
        /// </summary>
        public string Scale { get; set; }


        /// <summary>
        /// 原始高
        /// </summary>
        public int RawHight { get; set; }

        /// <summary>
        /// 原始低
        /// </summary>
        public int RawLow { get; set; }

        /// <summary>
        /// 缩放高
        /// </summary>
        public int ScaledHight { get; set; }

        /// <summary>
        /// 缩放低
        /// </summary>
        public int ScaledLow { get; set; }

        public bool IsLife { get; set; }
        
     

        public string yuLiu4 { get; set; } = "1000";//通讯周期

        //过程数据（PD）：PD 主要用于列车控制，传输命令和状态信息，一般为周期性传送，其有效载荷大小限制为 1436 字节（不包含安全层 SDT）。
        //消息数据（MD）：MD 主要用于故障和诊断信息，一般都是按需传送。当使用 UDP 传输时，MD 的有效载荷最大可达约 64KB；当使用 TCP 传输时，MD 的有效载荷最大可达约 4GB。
        public string yuLiu3 { get; set; } = "200";// 数据字节长度
        public string yuLiu5 { get; set; } = "以太网";
        public string yuLiu6 { get; set; } = "源设备";
        public string yuLiu8 { get; set; } = "12500"; //SIM
        public string yuLiu11 { get; set; } = "端口名称";




    }
}

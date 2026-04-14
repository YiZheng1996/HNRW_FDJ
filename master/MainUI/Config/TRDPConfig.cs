using RW.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetorSignalSimulator.UI.Model
{
    /// <summary>
    /// TRDP 双工控机交互数据参数
    /// </summary>
    public class TRDPConfig : IniConfig
    {
        public TRDPConfig()
   : base(Application.StartupPath + "\\config\\TRDPConfig.ini")
        {
            this.SetSectionName("TRDPConfig");
            this.Load();
        }

        /// <summary>
        /// UDP发送端IP
        /// </summary>
        [IniKeyName("UDP发送端IP")]
        public string UDPSendIP { get; set; } = "192.168.0.122";

        /// <summary>
        /// UDP接收端IP
        /// </summary>
        [IniKeyName("UDP接收端IP")]
        public string UDPReceiveIP { get; set; } = "192.168.0.123";

        /// <summary>
        /// 本机IP
        /// </summary>
        [IniKeyName("本机IP")]
        public string LocalMulticastIP { get; set; } = "192.168.0.26";

        /// <summary>
        /// 组播端口
        /// </summary>
        [IniKeyName("组播端口")]
        public string LocalMulticastPort { get; set; } = "17224";

        /// <summary>
        /// 接收COMID
        /// </summary>
        [IniKeyName("接收COMID")]
        public int ReceiveCOMID { get; set; } = 1025;

        /// <summary>
        /// 发送COMID
        /// </summary>
        [IniKeyName("发送COMID")]
        public int SendCOMID { get; set; } = 12500;

        /// <summary>
        /// 本机接收组播IP
        /// </summary>
        [IniKeyName("本机接收组播IP")]
        public string ReceiveMulticastIP { get; set; } = "239.255.0.1";

        /// <summary>
        /// 本机发送组播IP
        /// </summary>
        [IniKeyName("本机发送组播IP")]
        public string SendMulticastIP { get; set; } = "239.255.0.25";

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetorSignalSimulator.UI.Model
{
    public class TRDPConfigModel
    {
        /// <summary>
        /// 本机组播IP
        /// </summary>
        public string LocalMulticastIP { get; set; }= "239.255.0.25";

        /// <summary>
        ///本机组播端口
        /// </summary>
        public string LocalMulticastPort { get; set; } = "17224";

        /// <summary>
        /// 目标组播IP
        /// </summary>
        public string TargetMulticastIP { get; set; } = "239.255.0.1";

        /// <summary>
        ///目标组播端口
        /// </summary>
        public string TargetMulticastPort { get; set; } = "17224";
    }
}

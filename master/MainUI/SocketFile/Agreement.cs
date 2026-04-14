using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetorSignalSimulator.UI.SocketFile
{
    /// <summary>
    /// 接受TRDP模型类
    /// </summary>
    public class Agreement
    {
        /// <summary>
        /// 响应提示消息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 响应数据总记录
        /// </summary>
        public string total { get; set; }

        /// <summary>
        /// 工装类型编码
        /// </summary>
        public string equipTypeCode { get; set; }

        /// <summary>
        /// 协议详细数据
        /// </summary>
        public List<AgreementData> data { get; set; }

    }
}

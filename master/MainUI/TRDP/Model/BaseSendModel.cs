using BogieIdling.UI.Model;
using System;
using System.Collections.Generic;

using System.Text;

namespace BogieIdling.UI.TRDP.Model
{
    /// <summary>
    /// 发送实体的抽象类
    /// </summary>
    public abstract class BaseSendModel
    {
        public BaseSendModel()
        {
            this.Start = 0xFE;
        }

        /// <summary>
        /// 起始位，固定为0xFE
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// CRC16校验算法位
        /// </summary>
        public byte[] CRC { get; set; }

        /// <summary>
        /// 将发送的实体转换为字节数组
        /// </summary>
        /// <returns></returns>
        public abstract byte[] ToBytes();

        public void SetCRC(byte[] data)
        {
            byte[] crc = CRCHelper.CRC16(data, 4, data.Length - 2);
            this.CRC = crc;
        }
    }
}

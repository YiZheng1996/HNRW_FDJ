using System;
using System.Collections.Generic;

using System.Text;

namespace BogieIdling.UI.TRDP.Model
{
    /// <summary>
    /// 配置DNS域名 MCU->TRDP模块
    /// </summary>
    public class DNSSend : BaseSendModel
    {
        public DNSSend()
        {
        }

        /// <summary>
        /// 配置DNS域名命令类型
        /// </summary>
        public DNSCommandTypes CommandType { get; set; }
        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataLength { get; set; }
        /// <summary>
        /// DNS域名字符串
        /// </summary>
        public string Domain { get; set; }

        public override byte[] ToBytes()
        {
            byte[] bts = new byte[86];

            bts[0] = (byte)this.Start;
            bts[1] = (byte)this.CommandType;
            byte[] length = BitConverter.GetBytes((short)DataLength);
            bts[2] = length[1];
            bts[3] = length[0];

            byte[] data = new byte[80];
            for (int i = 0; i < Domain.Length; i++)
            {
                data[i] = (byte)Domain[i];
            }

            Array.Copy(data, 0, bts, 5, data.Length);

            SetCRC(data);

            return bts;

        }
    }

    public enum DNSCommandTypes
    {
        None = 0,
        Eth0 = 0x03,
        Eth1 = 0x83,
    }
}

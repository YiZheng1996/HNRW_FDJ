using BogieIdling.UI.Model;
using System;
using System.Collections.Generic;

using System.Text;

namespace BogieIdling.UI.TRDP.Model
{
    public abstract class BaseRecieveModel
    {
        /// <summary>
        /// 起始位，固定为0xFE
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// CRC16校验算法位
        /// </summary>
        public byte[] CRC { get; set; }


        public abstract void InitData(byte[] data);

        public bool ValidCRC(byte[] data)
        {
            byte[] crc = CRCHelper.CRC16(data, 4, data.Length - 2);
            for (int i = 0; i < crc.Length; i++)
            {
                if (crc[i] != CRC[i]) return false;
            }
            return true;
        }
    }
}

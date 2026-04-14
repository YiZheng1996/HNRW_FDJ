using BogieIdling.UI.Model;
using System;
using System.Collections.Generic;

using System.Text;

namespace BogieIdling.UI.TRDP.Model
{
    public class SinglecastRecieve : BaseRecieveModel
    {
        public SinglecastRecieveCommandTypes CommandType { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataLength { get; set; }
        /// <summary>
        /// 序列计数器，每次发送+1
        /// </summary>
        public int SequenceCounter { get; set; }
        /// <summary>
        /// 协议版本，默认值 0x01 0x00
        /// </summary>
        public int ProtocolVersion { get; set; }
        /// <summary>
        /// 接收通讯端口标识
        /// </summary>
        public int ComId { get; set; }
        /// <summary>
        /// 应用数据长度
        /// </summary>
        public int DatasetLength { get; set; }
        /// <summary>
        /// 端口数据字节数组
        /// </summary>
        public byte[] DatasetData { get; set; }

        public override void InitData(byte[] data)
        {
            this.Start = data[0];
            this.CommandType = (SinglecastRecieveCommandTypes)data[1];
            this.DataLength = BitConverter.ToInt16(new byte[] { data[3], data[2] }, 0);
            this.SequenceCounter = BitConverter.ToInt32(new byte[] { data[7], data[6], data[5], data[4] }, 0);
            this.ProtocolVersion = BitConverter.ToInt16(new byte[] { data[9], data[8] }, 0);
            this.ComId = BitConverter.ToInt32(new byte[] { data[13], data[12], data[11], data[10] }, 0);
            this.DatasetLength = BitConverter.ToInt32(new byte[] { data[17], data[16], data[15], data[14] }, 0);
            this.DatasetData = new byte[this.DatasetLength];
            Array.Copy(data, 18, this.DatasetData, 0, this.DatasetData.Length);
            this.CRC = new byte[] { data[18 + this.DatasetLength], data[19 + this.DatasetLength] };
            if (!ValidCRC(data))
                throw new FormatException("CRC校验失败！");
        }
    }

    public enum SinglecastRecieveCommandTypes
    {
        Eth01 = 0x21,
        Eth02 = 0x22,
        Eth11 = 0xA1,
        Eth12 = 0xA2,
    }
}

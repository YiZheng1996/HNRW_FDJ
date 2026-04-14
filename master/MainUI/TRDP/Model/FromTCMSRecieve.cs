using BogieIdling.UI.Model;
using System;
using System.Collections.Generic;

using System.Text;

namespace BogieIdling.UI.TRDP.Model
{
    /// <summary>
    /// 从TCMS接受数据帧（组播接收），TCMS  TRDP模块  MCU
    /// </summary>
    public class FromTCMSRecieve : BaseRecieveModel
    {
        public FromTCMSCommandTypes CommandType { get; set; }
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
        public int EtbTopoCnt { get; set; }
        public int OpTrnTopoCnt { get; set; }
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
            this.CommandType = (FromTCMSCommandTypes)data[1];
            this.DataLength = BitConverter.ToInt16(new byte[] { data[3], data[2] }, 0);
            this.SequenceCounter = BitConverter.ToInt32(new byte[] { data[7], data[6], data[5], data[4] }, 0);
            this.ProtocolVersion = BitConverter.ToInt16(new byte[] { data[9], data[8] }, 0);
            this.ComId = BitConverter.ToInt32(new byte[] { data[13], data[12], data[11], data[10] }, 0);
            this.EtbTopoCnt = BitConverter.ToInt32(new byte[] { data[17], data[16], data[15], data[14] }, 0);
            this.OpTrnTopoCnt = BitConverter.ToInt32(new byte[] { data[21], data[20], data[19], data[18] }, 0);
            this.DatasetLength = BitConverter.ToInt32(new byte[] { data[25], data[24], data[23], data[22] }, 0);
            this.DatasetData = new byte[this.DatasetLength];
            Array.Copy(data, 26, this.DatasetData, 0, this.DatasetData.Length);
            this.CRC = new byte[] { data[26 + this.DatasetLength], data[27 + this.DatasetLength] };
            if (!ValidCRC(data))
                throw new FormatException("CRC校验失败！");

        }
    }

    public enum FromTCMSCommandTypes
    {
        None = 0,
        Eth0 = 0x07,
        Eth1 = 0x87,
    }
}

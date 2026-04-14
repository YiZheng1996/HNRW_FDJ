using System;
using System.Collections.Generic;

using System.Text;

namespace BogieIdling.UI.TRDP.Model
{
    /// <summary>
    /// 自由单播发送数据帧，MCU-TRDP-其他TRDP
    /// </summary>
    public class SinglecastSend : BaseSendModel
    {
        public SinglecastSendCommandTypes CommandType { get; set; }
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

        public override byte[] ToBytes()
        {
            byte[] bts = new byte[20 + this.DatasetLength];
            bts[0] = (byte)this.Start;
            bts[1] = (byte)this.CommandType;
            byte[] length = BitConverter.GetBytes((short)this.DataLength);
            bts[2] = length[1];
            bts[3] = length[0];
            byte[] seq = BitConverter.GetBytes(this.SequenceCounter);
            bts[4] = seq[3];
            bts[5] = seq[2];
            bts[6] = seq[1];
            bts[7] = seq[0];
            byte[] pro = BitConverter.GetBytes((short)this.ProtocolVersion);
            bts[8] = pro[1];
            bts[9] = pro[0];
            byte[] comID = BitConverter.GetBytes(this.ComId);
            bts[10] = comID[3];
            bts[11] = comID[2];
            bts[12] = comID[1];
            bts[13] = comID[0];
            byte[] datasetLength = BitConverter.GetBytes(this.DatasetLength);
            bts[14] = datasetLength[3];
            bts[15] = datasetLength[2];
            bts[16] = datasetLength[1];
            bts[17] = datasetLength[0];
            for (int i = 0; i < DatasetData.Length; i++)
            {
                bts[18 + i] = DatasetData[i];
            }

            return bts;
        }
    }


    public enum SinglecastSendCommandTypes
    {
        Eth01 = 0x11,
        Eth02 = 0x12,
        Eth11 = 0x91,
        Eth12 = 0x92,
    }
}

using BogieIdling.UI.Model;
using MainUI.Config;
using System;
using System.Collections.Generic;

using System.Text;

namespace BogieIdling.UI.TRDP.Model
{
    /// <summary>
    /// 发送数据帧给TCMS(UDP单播) MCU->TRDP模块->TCMS
    /// </summary>
    public class ToTCMSSend : BaseSendModel
    {
        public ToTCMSSend()
        {
            this.CommandType = TOTCMSCommandTypes.Eth1;
            this.ProtocolVersion = 0x0100;
        }
        public ToTCMSSend(string iniName)
        {
            ETHConfig ethconfig = new ETHConfig(iniName);
            if (ethconfig.ETH == 0)
                CommandType = TOTCMSCommandTypes.Eth0;
            else
                CommandType = TOTCMSCommandTypes.Eth1;
            this.ProtocolVersion = 0x0100;
        }
        public TOTCMSCommandTypes CommandType { get; set; }
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
            byte[] bts = new byte[DatasetData.Length + 20];
            bts[0] = (byte)this.Start;
            bts[1] = (byte)this.CommandType;
            this.DataLength = bts.Length - 6;//自动计算报文长度
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
            this.DatasetLength = bts.Length - 20;//自动计算数据报文长度
            byte[] datasetLength = BitConverter.GetBytes(this.DatasetLength);
            bts[14] = datasetLength[3];
            bts[15] = datasetLength[2];
            bts[16] = datasetLength[1];
            bts[17] = datasetLength[0];
            for (int i = 0; i < DatasetData.Length; i++)
            {
                bts[18 + i] = DatasetData[i];
            }
           
            CRC = CRCHelper.CRC16(bts, 4, bts.Length - 2);
            bts[18 + this.DatasetLength] = CRC[0];
            bts[19 + this.DatasetLength] = CRC[1];

            return bts;

        }





    }

    public enum TOTCMSCommandTypes
    {
        None = 0,
        Eth0 = 0x09,
        Eth1 = 0x89,
    }
}

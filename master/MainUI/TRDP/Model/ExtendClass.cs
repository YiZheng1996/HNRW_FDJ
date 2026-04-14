using BogieIdling.UI.Model;
using RW.Core;
using System;
using System.Collections.Generic;

using System.Text;

namespace BogieIdling.UI.TRDP.Model
{
    public class ExtendClass
    {

        /// <summary>
        /// 起始位
        /// </summary>
        public int Start = 0xFE;

        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataLength { get; set; }
        /// <summary>
        /// 扩展的组播IP地址
        /// </summary>
        public string multicastIP { get; set; }



        /// <summary>
        /// 扩展的COMid
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

        /// <summary>
        /// CRC16校验算法位
        /// </summary>
        public byte[] CRC { get; set; }

        public byte[] ToBytes()
        {
            byte[] bts = new byte[18];
            bts[0] = (byte)this.Start;
            bts[1] = 0x52; //0x52   0x82
            bts[2] = 0x00;
            bts[3] = 12;


            //发送的 COMID，源 IP 地址

            bts[4] = 0x00;
            bts[5] = 0x00;
            bts[6] = 0x00;
            bts[7] = 0x00;


            //组播地址
            byte[] ip = RWConvert.ToIPBytes(multicastIP);
            Array.Copy(ip, 0, bts, 8, ip.Length);



            //comid
            byte[] comID = BitConverter.GetBytes(ComId);
            bts[12] = comID[3];
            bts[13] = comID[2];
            bts[14] = comID[1];
            bts[15] = comID[0];


            this.CRC = CRCHelper.CRC16(bts, 4, bts.Length - 2);
            bts[16] = CRC[0];
            bts[17] = CRC[1];



            return bts;
        }


        public byte[] dataToByte() {

            byte[] bts = new byte[DatasetData.Length + 22];
            bts[0] = (byte)this.Start;
            bts[1] = 0xd3;  //0x53:Eth0     0xd3:Eth1
            this.DataLength = bts.Length - 6;//自动计算报文长度

            byte[] length = BitConverter.GetBytes((short)this.DataLength);
            bts[2] = length[1];
            bts[3] = length[0];
            //发送的 COMID 数据，源IP 地址  填 0 
            bts[4] = 0x00;
            bts[5] = 0x00;
            bts[6] = 0x00;
            bts[7] = 0x00;
            //发送的 COMID数据对应的目标组播地址  填 0 
            bts[8] = 0x00;
            bts[9] = 0x00;
            bts[10] = 0x00;
            bts[11] = 0x00;

            byte[] comID = BitConverter.GetBytes(this.ComId);
            bts[12] = comID[3];
            bts[13] = comID[2];
            bts[14] = comID[1];
            bts[15] = comID[0];
            this.DatasetLength = bts.Length - 22;//自动计算数据报文长度
            byte[] datasetLength = BitConverter.GetBytes(this.DatasetLength);
            bts[16] = datasetLength[3];
            bts[17] = datasetLength[2];
            bts[18] = datasetLength[1];
            bts[19] = datasetLength[0];

            for (int i = 0; i < DatasetData.Length; i++)
            {
                bts[20 + i] = DatasetData[i];
            }
            CRC = CRCHelper.CRC16(bts, 4, bts.Length - 2);
            bts[20 + this.DatasetLength] = CRC[0];
            bts[21 + this.DatasetLength] = CRC[1];

            return bts;

        

    }


    }
}

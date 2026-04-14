using System;
using System.Collections.Generic;

using System.Text;
using System.Net;
using RW.Configuration;
using System.Windows.Forms;
using RW.Core;
using BogieIdling.UI.Model;

namespace BogieIdling.UI.TRDP.Model
{
    /// <summary>
    /// TRDP配置帧主帧 3.7
    /// </summary>
    public class TRDPSMI : IniConfig
    {
        public TRDPSMI()
            : base(Application.StartupPath + "\\config\\trdp_njcj.ini")
        {
            //CommandType = TRDPMainCommandTypes.Eth0;
            //DNSPort = 0x4349;
            //TTDBMulticastPort = 0x4348;
            //TCMSMulticastPort = 0x4348;
            //TCMSPort = 0x4348;
            //MinorTCMSPort = 0x4348;

            //Mac = "00:00:00:00:00:00";
            //IP = GateWay = SubnetMask = DNS = TTDBMulticastIP = TCMSMulticastIP = TCMSIP = MinorTCMSIP = SinglecastIP1 = SinglecastIP2 = "0.0.0.0";
            this.Load();
        }

        /// <summary>
        /// 起始位
        /// </summary>
        public const int START = 0xFE;


        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataLength { get; set; }
        /// <summary>
        /// SMI地址
        /// </summary>
        public int SMI { get; set; }


        /// <summary>
        /// 以后版本 (高)：
        /// </summary>
        public int UserDataVerH { get; set; }
        /// <summary>
        /// 以后版本 (低)：
        /// </summary>
        public int UserDataVerL { get; set; }


        /// <summary>
        /// 注册接收 SMI 数量
        /// </summary>
        public int SMIcount { get; set; }
        /// <summary>
        /// SMI1
        /// </summary>
        public int SendSMI1 { get; set; }
        /// <summary>
        /// SMI2
        /// </summary>
        public int SendSMI2 { get; set; }

        /// <summary>
        /// SMI3
        /// </summary>
        public int SendSMI3 { get; set; }

        /// <summary>
        /// SMI4
        /// </summary>
        public int SendSMI4 { get; set; }


        /// <summary>
        /// SMI5
        /// </summary>
        public int SendSMI5 { get; set; }

        /// <summary>
        /// SMI6
        /// </summary>
        public int SendSMI6 { get; set; }

        /// <summary>
        /// CRC校验
        /// </summary>
        public byte[] CRC { get; set; }

        public void InitData(byte[] data)
        {
            if (data.Length != 89) return;


        }

        public byte[] ToBytes()
        {
            byte[] bts = new byte[89];
            bts[0] = (byte)START;
            bts[1] = 0x04;
            bts[2] = 0x00;
            bts[3] = 57;


            //发送 SMI 安全信息标识
            byte[] dnsport = BitConverter.GetBytes(SMI);
            bts[4] = dnsport[3];
            bts[5] = dnsport[2];
            bts[6] = dnsport[1];
            bts[7] = dnsport[0];


            //重要过程数据主版本号 默认 0x01
            bts[8] = (byte)UserDataVerH;
            //重要过程数据子版本号 默认 0x01
            bts[9] = (byte)UserDataVerL;

            //编组唯一标识符  不确定情况下全部填 0
            bts[9] = 0x00;
            bts[10] = 0x00;
            bts[11] = 0x00;
            bts[12] = 0x00;
            bts[13] = 0x00;
            bts[14] = 0x00;
            bts[15] = 0x00;
            bts[16] = 0x00;
            bts[17] = 0x00;
            bts[18] = 0x00;
            bts[19] = 0x00;
            bts[20] = 0x00;
            bts[21] = 0x00;
            bts[22] = 0x00;
            bts[23] = 0x00;
            bts[24] = 0x00;
            bts[25] = 0x00;
            //安全拓扑计数器
            bts[26] = 0x00;
            bts[27] = 0x00;
            bts[28] = 0x00;
            bts[29] = 0x00;

            //注册接收 SMI 数量
            bts[30] = (byte)SMIcount;

            //允许接收 SMI 安全信息标识 1
            byte[] SMI1 = BitConverter.GetBytes(SendSMI1);
            bts[31] = SMI1[3];
            bts[32] = SMI1[2];
            bts[33] = SMI1[1];
            bts[34] = SMI1[0];

            //允许接收 SMI 安全信息标识 2
            byte[] SMI2 = BitConverter.GetBytes(SendSMI2);
            bts[35] = SMI2[3];
            bts[36] = SMI2[2];
            bts[37] = SMI2[1];
            bts[38] = SMI2[0];

            //允许接收 SMI 安全信息标识 3
            byte[] SMI3 = BitConverter.GetBytes(SendSMI3);
            bts[38] = SMI3[3];
            bts[40] = SMI3[2];
            bts[41] = SMI3[1];
            bts[42] = SMI3[0];


            //允许接收 SMI 安全信息标识 4
            byte[] SMI4 = BitConverter.GetBytes(SendSMI4);
            bts[43] = SMI4[3];
            bts[44] = SMI4[2];
            bts[45] = SMI4[1];
            bts[46] = SMI4[0];


            //允许接收 SMI 安全信息标识 5
            byte[] SMI5 = BitConverter.GetBytes(SendSMI5);
            bts[47] = SMI5[3];
            bts[48] = SMI5[2];
            bts[49] = SMI5[1];
            bts[50] = SMI5[0];

            //允许接收 SMI 安全信息标识 6
            byte[] SMI6 = BitConverter.GetBytes(SendSMI6);
            bts[51] = SMI6[3];
            bts[52] = SMI6[2];
            bts[53] = SMI6[1];
            bts[54] = SMI6[0];



            this.CRC = CRCHelper.CRC16(bts, 4, bts.Length - 2);

            //var tempbytes = RWConvert.HexStringToBytes("FE 05 00 53 38 00 22 33 44 55 66 0A 80 01 1C 0A 80 01 01 FF FF FF 00 00 00 00 00 43 49 00 00 00 00 E0 01 01 09 43 49 50 44 0A 80 01 04 50 44 00 00 00 00 50 44 01 00 00 03 E8 00 00 03 E8 00 00 03 E8 00 00 03 E8 00 00 00 00 00 1F 4F 1F 4F 00 00 00 00 1F 4F 1F 4F 70 C2");

            //var tempCRC = CRCHelper.CRC16(tempbytes, 4, tempbytes.Length - 2);

            bts[55] = CRC[0];
            bts[56] = CRC[1];

            return bts;
        }



    }

}

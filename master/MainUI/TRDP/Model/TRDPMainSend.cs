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
    public class TRDPMainSend : IniConfig
    {
        public TRDPMainSend()
            : base(Application.StartupPath + "\\config\\" + iniName + ".ini")
        {

        }

        public TRDPMainSend(string ininame)
        {
            iniName = ininame;
            this.Name = ininame;
        }

        /// <summary>
        /// 根据ini名称加载不同 ini文件
        /// </summary>
        public static string iniName = "Address";


        private string _Name;

        /// <summary>
        /// Name
        /// </summary>
        protected string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                this.Filename = Application.StartupPath + "\\config\\" + iniName + ".ini";
                CommandType = TRDPMainCommandTypes.Eth1;
                DNSPort = 0x4349;
                TTDBMulticastPort = 0x4348;
                TCMSMulticastPort = 0x4348;
                TCMSPort = 0x4348;
                MinorTCMSPort = 0x4348;

                Mac = "00:00:00:00:00:00";
                IP = GateWay = SubnetMask = DNS = TTDBMulticastIP = TCMSMulticastIP = TCMSIP = MinorTCMSIP = SinglecastIP1 = SinglecastIP2 = "0.0.0.0";
                this.Load();
            }
        }

        /// <summary>
        /// 起始位
        /// </summary>
        public const int START = 0xFE;
        /// <summary>
        /// 命令类型，0x05，0x85
        /// </summary>
        public TRDPMainCommandTypes CommandType { get; set; }
        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataLength { get; set; }
        /// <summary>
        /// MAC配置选择，默认为0x00
        /// </summary>
        public int MACAuto { get; set; }
        /// <summary>
        /// MAC地址，6字节
        /// </summary>
        public string Mac { get; set; }
        /// <summary>
        /// 模块IP地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 网关IP
        /// </summary>
        public string GateWay { get; set; }
        /// <summary>
        /// 子网掩码地址
        /// </summary>
        public string SubnetMask { get; set; }
        /// <summary>
        /// DNS地址
        /// </summary>
        public string DNS { get; set; }
        /// <summary>
        /// DNS端口号
        /// </summary>
        public int DNSPort { get; set; }
        /// <summary>
        /// TTDB组播地址
        /// </summary>
        public string TTDBMulticastIP { get; set; }
        /// <summary>
        /// TCMS组播地址
        /// </summary>
        public string TCMSMulticastIP { get; set; }
        /// <summary>
        /// TTDB组播端口号
        /// </summary>
        public int TTDBMulticastPort { get; set; }
        /// <summary>
        /// TCMS组播端口号
        /// </summary>
        public int TCMSMulticastPort { get; set; }
        /// <summary>
        /// 主TCMS IP 地址
        /// </summary>
        public string TCMSIP { get; set; }
        /// <summary>
        /// 主TCMS 的端口
        /// </summary>
        public int TCMSPort { get; set; }
        /// <summary>
        /// 备用TCMS IP地址
        /// </summary>
        public string MinorTCMSIP { get; set; }
        /// <summary>
        /// 备用TCMS 端口
        /// </summary>
        public int MinorTCMSPort { get; set; }
        /// <summary>
        /// 注册接收TCMS的COMID数量
        /// </summary>
        public int COMIDCount { get; set; }
        /// <summary>
        /// 注册接收TCMS的COMID1字节数据，从高到低 4字节
        /// </summary>
        public int COMID1 { get; set; }
        /// <summary>
        /// 注册接收TCMS的COMID2字节数据，从高到低 4字节
        /// </summary>
        public int COMID2 { get; set; }
        /// <summary>
        /// 注册接收TCMS的COMID3字节数据，从高到低 4字节
        /// </summary>
        public int COMID3 { get; set; }
        /// <summary>
        /// 注册接收TCMS的COMID4字节数据，从高到低 4字节
        /// </summary>
        public int COMID4 { get; set; }
        /// <summary>
        /// 注册自由单播通讯端口数量0为禁止，1，2
        /// </summary>
        public int SinglecastCount { get; set; }
        /// <summary>
        /// 单播1的目标ID地址，高低字节
        /// </summary>
        public string SinglecastIP1 { get; set; }
        /// <summary>
        /// 单播1的目标端口，高低字节
        /// </summary>
        public int SinglecastPort1 { get; set; }
        /// <summary>
        /// 单播1的源端口，高低字节
        /// </summary>
        public int SinglecastSourcePort1 { get; set; }

        /// <summary>
        /// 单播2的目标ID地址，高低字节
        /// </summary>
        public string SinglecastIP2 { get; set; }
        /// <summary>
        /// 单播2的目标端口，高低字节
        /// </summary>
        public int SinglecastPort2 { get; set; }
        /// <summary>
        /// 单播2的源端口，高低字节
        /// </summary>
        public int SinglecastSourcePort2 { get; set; }

        /// <summary>
        /// CRC校验
        /// </summary>
        public byte[] CRC { get; set; }


        /// <summary>
        /// SMI地址
        /// </summary>
        public int SMI { get; set; }


        /// <summary>
        /// 以后主版本：
        /// </summary>
        public int UserDataVerH { get; set; }
        /// <summary>
        /// 以后子版本：
        /// </summary>
        public int UserDataVerL { get; set; }


        /// <summary>
        /// 编组唯一标识符
        /// </summary>
        public int ConsistID { get; set; }
        /// <summary>
        /// 安全拓扑计数器
        /// </summary>
        public int SafeTopoCount { get; set; }

        /// <summary>
        /// 注册接收 SMI 数量
        /// </summary>
        public int SMIcount { get; set; }
        /// <summary>
        /// SMI1
        /// </summary>
        public int ReceiveSMI1 { get; set; }
        /// <summary>
        /// SMI2
        /// </summary>
        public int ReceiveSMI2 { get; set; }

        /// <summary>
        /// SMI3
        /// </summary>
        public int ReceiveSMI3 { get; set; }

        /// <summary>
        /// SMI4
        /// </summary>
        public int ReceiveSMI4 { get; set; }


        /// <summary>
        /// SMI5
        /// </summary>
        public int ReceiveSMI5 { get; set; }

        /// <summary>
        /// SMI6
        /// </summary>
        public int ReceiveSMI6 { get; set; }


        public void InitData(byte[] data)
        {
            if (data.Length != 89) return;


        }

        public byte[] ToBytes()
        {
            byte[] bts = new byte[89];
            bts[0] = (byte)START;
            bts[1] = (byte)this.CommandType;
            bts[2] = 0x00;
            bts[3] = 83;
            bts[4] = 0x38;

            var byteMac = RWConvert.ToMACBytes(Mac);


            Array.Copy(byteMac, 0, bts, 5, byteMac.Length);

            byte[] ip = RWConvert.ToIPBytes(IP);
            Array.Copy(ip, 0, bts, 11, ip.Length);

            byte[] gateway = RWConvert.ToIPBytes(GateWay);
            Array.Copy(gateway, 0, bts, 15, gateway.Length);
            byte[] subnet = RWConvert.ToIPBytes(SubnetMask);
            Array.Copy(subnet, 0, bts, 19, subnet.Length);
            byte[] dns = RWConvert.ToIPBytes(DNS);
            Array.Copy(dns, 0, bts, 23, dns.Length);
            byte[] dnsport = BitConverter.GetBytes((short)DNSPort);
            bts[27] = dnsport[1];
            bts[28] = dnsport[0];

            byte[] ttdbMultiIP = RWConvert.ToIPBytes(TTDBMulticastIP);
            Array.Copy(ttdbMultiIP, 0, bts, 29, ttdbMultiIP.Length);
            byte[] tcmsMultiIP = RWConvert.ToIPBytes(TCMSMulticastIP);
            Array.Copy(tcmsMultiIP, 0, bts, 33, tcmsMultiIP.Length);

            byte[] ttdbPort = BitConverter.GetBytes((short)TTDBMulticastPort);
            bts[37] = ttdbPort[1];
            bts[38] = ttdbPort[0];
            byte[] tcmsMultiPort = BitConverter.GetBytes((short)TCMSMulticastPort);
            bts[39] = tcmsMultiPort[1];
            bts[40] = tcmsMultiPort[0];

            byte[] tcmsIP = RWConvert.ToIPBytes(TCMSIP);
            Array.Copy(tcmsIP, 0, bts, 41, tcmsIP.Length);
            byte[] tcmsPort = BitConverter.GetBytes((short)TCMSPort);
            bts[45] = tcmsPort[1];
            bts[46] = tcmsPort[0];

            byte[] minorTCMSIP = RWConvert.ToIPBytes(this.MinorTCMSIP);
            Array.Copy(minorTCMSIP, 0, bts, 47, minorTCMSIP.Length);
            byte[] minorTCMSPort = BitConverter.GetBytes((short)this.MinorTCMSPort);
            bts[51] = minorTCMSPort[1];
            bts[52] = minorTCMSPort[0];

            bts[53] = (byte)COMIDCount;
            //byte[] com1 = BitConverter.GetBytes(COMID1).Reverse().ToArray();
            byte[] com1 = BitConverter.GetBytes(COMID1);
            Array.Reverse(com1);
            //20220310
            //byte[] crcValue = CRC16(temp);
            //Array.Reverse(crcValue);
            //return crcValue;

            Array.Copy(com1, 0, bts, 54, com1.Length);
           // byte[] com2 = BitConverter.GetBytes(COMID2).Reverse().ToArray();
            byte[] com2 = BitConverter.GetBytes(COMID2);
            Array.Reverse(com2);


            Array.Copy(com2, 0, bts, 58, com2.Length);
           // byte[] com3 = BitConverter.GetBytes(COMID3).Reverse().ToArray();
            byte[] com3 = BitConverter.GetBytes(COMID3);
            Array.Reverse(com3);

            Array.Copy(com3, 0, bts, 62, com3.Length);
            //byte[] com4 = BitConverter.GetBytes(COMID4).Reverse().ToArray();
            byte[] com4 = BitConverter.GetBytes(COMID4);
            Array.Reverse(com4);

            Array.Copy(com4, 0, bts, 66, com4.Length);

            bts[70] = (byte)SinglecastCount;
            byte[] singlecastIP1 = RWConvert.ToIPBytes(this.SinglecastIP1);
            Array.Copy(singlecastIP1, 0, bts, 71, singlecastIP1.Length);
            byte[] singlecastPort1 = BitConverter.GetBytes((short)this.SinglecastPort1);
            bts[75] = singlecastPort1[1];
            bts[76] = singlecastPort1[0];
            byte[] singlecastSouncePort1 = BitConverter.GetBytes((short)this.SinglecastSourcePort1);
            bts[77] = singlecastSouncePort1[1];
            bts[78] = singlecastSouncePort1[0];

            byte[] singlecastIP2 = RWConvert.ToIPBytes(this.SinglecastIP2);
            Array.Copy(singlecastIP2, 0, bts, 79, singlecastIP2.Length);
            byte[] singlecastPort2 = BitConverter.GetBytes((short)this.SinglecastPort2);
            bts[83] = singlecastPort2[1];
            bts[84] = singlecastPort2[0];
            byte[] singlecastSouncePort2 = BitConverter.GetBytes((short)this.SinglecastSourcePort2);
            bts[85] = singlecastSouncePort2[1];
            bts[86] = singlecastSouncePort2[0];

            this.CRC = CRCHelper.CRC16(bts, 4, bts.Length - 2);

            //var tempbytes = RWConvert.HexStringToBytes("FE 05 00 53 38 00 22 33 44 55 66 0A 80 01 1C 0A 80 01 01 FF FF FF 00 00 00 00 00 43 49 00 00 00 00 E0 01 01 09 43 49 50 44 0A 80 01 04 50 44 00 00 00 00 50 44 01 00 00 03 E8 00 00 03 E8 00 00 03 E8 00 00 03 E8 00 00 00 00 00 1F 4F 1F 4F 00 00 00 00 1F 4F 1F 4F 70 C2");

            //var tempCRC = CRCHelper.CRC16(tempbytes, 4, tempbytes.Length - 2);

            bts[87] = CRC[0];
            bts[88] = CRC[1];

            return bts;
        }



        public byte[] SMIToBytes()
        {
            byte[] bts = new byte[57];
            bts[0] = (byte)START;
            bts[1] = 0x04;
            bts[2] = 0x00;
            bts[3] = 0x33;


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
            byte[] SafeTopoCountByte = BitConverter.GetBytes(SafeTopoCount);
            bts[26] = SafeTopoCountByte[3];
            bts[27] = SafeTopoCountByte[2];
            bts[28] = SafeTopoCountByte[1];
            bts[29] = SafeTopoCountByte[0];

            //注册接收 SMI 数量
            bts[30] = (byte)SMIcount;

            //允许接收 SMI 安全信息标识 1
            byte[] SMI1 = BitConverter.GetBytes(ReceiveSMI1);
            bts[31] = SMI1[3];
            bts[32] = SMI1[2];
            bts[33] = SMI1[1];
            bts[34] = SMI1[0];

            //允许接收 SMI 安全信息标识 2
            byte[] SMI2 = BitConverter.GetBytes(ReceiveSMI2);
            bts[35] = SMI2[3];
            bts[36] = SMI2[2];
            bts[37] = SMI2[1];
            bts[38] = SMI2[0];

            //允许接收 SMI 安全信息标识 3
            byte[] SMI3 = BitConverter.GetBytes(ReceiveSMI3);
            bts[38] = SMI3[3];
            bts[40] = SMI3[2];
            bts[41] = SMI3[1];
            bts[42] = SMI3[0];


            //允许接收 SMI 安全信息标识 4
            byte[] SMI4 = BitConverter.GetBytes(ReceiveSMI4);
            bts[43] = SMI4[3];
            bts[44] = SMI4[2];
            bts[45] = SMI4[1];
            bts[46] = SMI4[0];


            //允许接收 SMI 安全信息标识 5
            byte[] SMI5 = BitConverter.GetBytes(ReceiveSMI5);
            bts[47] = SMI5[3];
            bts[48] = SMI5[2];
            bts[49] = SMI5[1];
            bts[50] = SMI5[0];

            //允许接收 SMI 安全信息标识 6
            byte[] SMI6 = BitConverter.GetBytes(ReceiveSMI6);
            bts[51] = SMI6[3];
            bts[52] = SMI6[2];
            bts[53] = SMI6[1];
            bts[54] = SMI6[0];


            this.CRC = CRCHelper.CRC16(bts, 4, bts.Length - 2);
            bts[55] = CRC[0];
            bts[56] = CRC[1];


            //var tempbytes = RWConvert.HexStringToBytes("FE 05 00 53 38 00 22 33 44 55 66 0A 80 01 1C 0A 80 01 01 FF FF FF 00 00 00 00 00 43 49 00 00 00 00 E0 01 01 09 43 49 50 44 0A 80 01 04 50 44 00 00 00 00 50 44 01 00 00 03 E8 00 00 03 E8 00 00 03 E8 00 00 03 E8 00 00 00 00 00 1F 4F 1F 4F 00 00 00 00 1F 4F 1F 4F 70 C2");

            //var tempCRC = CRCHelper.CRC16(tempbytes, 4, tempbytes.Length - 2);


            return bts;
        }





    }

    public enum TRDPMainCommandTypes
    {
        Eth0 = 0x05,
        Eth1 = 0x85,
    }





}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.IO.Ports;
using System.Net.Sockets;
using System.Net;
using System.Text;

using Newtonsoft.Json;
using System.Diagnostics;
using System.Windows.Forms;
using RW.Driver;
using MetorSignalSimulator.UI.Model;
using MetorSignalSimulator.UI.Driver;
using MetorSignalSimulator.UI.SocketFile;
using System.Collections.Concurrent;

namespace MainUI.Driver
{
    /// <summary>
    /// TRDP 模块，（串口模式）
    /// 用于接收、发送以太网通讯数据，请参照协议使用此模块。
    /// 常用流程，
    /// 1、发送数据配置（SetSetting）成功后，等待2秒
    /// 2、发送数据到TCMS（SetToTCMS）
    /// 3、发送2，并刷新生命信号
    /// </summary>
    public class TRDPDriverOld : IDriver
    {
        public TRDPDriverOld()
        {
            ReadTimeout = 3000;
            CRC32Cls.GetCRC32Table();
        }

        static UInt32 lifeSignals = 0;
        static byte[] lifeSignalss = new byte[4];

        static UInt16 lifeSignals1 = 0;
        static byte[] lifeSignalss1 = new byte[2];

        /// <summary>
        /// 实时值更新
        /// </summary>
        public ConcurrentDictionary<string, decimal> trdpValue = new ConcurrentDictionary<string, decimal>();//存储该端口生命信号的类型，U16，还是U32

        Dictionary<int, uint> mcuCount = new Dictionary<int, uint>();

        public List<string[]> zubocanshu = new List<string[]>(); //发送的组播动态数组，进行遍历
        Dictionary<int, string> lifesigallength = new Dictionary<int, string>();//存储该端口生命信号的类型，U16，还是U32
        public static List<string> listenComID = new List<string>();

        TRDPConfig tRDPConfig = new TRDPConfig();

        /// <summary>
        /// 设置帧头信息
        /// </summary>
        /// <param name="comID"></param>
        /// <returns></returns>
        public byte[] SetMcu(int comID, int SMIvalue)
        {
            // 发送40个数据
            byte[] mcuByte = new byte[40 + SendData[comID].Count()];
            if (!mcuCount.Keys.Contains(comID))
                mcuCount[comID] = 0;
            else
                mcuCount[comID]++;

            //计数器，4byte
            byte[] mcuCounts = BitConverter.GetBytes(mcuCount[comID]).Reverse().ToArray();//整数转换为字节，数值1 经Reverse由1000变为了0001，大端模式
            Array.Copy(mcuCounts, 0, mcuByte, 0, 4);// 大端模式（Big - Endian）又称为网络字节序，指的是数据的高位字节存储在低地址处，而数据的低位字节存储在高地址处。
                                                    // 这与我们平常的阅读顺序相同，先看到的是高位，后看到的是低位，因此被称为“大端”。

            //协议版本，2byte
            mcuByte[4] = 0x01;
            mcuByte[5] = 0x00;

            //通信模式，2byte
            mcuByte[6] = 0x50;
            mcuByte[7] = 0x64;

            //comID,4byte
            byte[] comIDs = BitConverter.GetBytes(comID).Reverse().ToArray();
            Array.Copy(comIDs, 0, mcuByte, 8, 4);

            //列车静态拓扑序列,4byte
            //列车动态拓扑序列,4byte

            //应用数据长度,4byte
            byte[] dataLength = BitConverter.GetBytes(SendData[comID].Length).Reverse().ToArray();
            Array.Copy(dataLength, 0, mcuByte, 20, 4);

            //保留,4byte
            //应答数据通信端口标识,4byte
            //应答数据通信IP地址,4byte

            byte[] dv = new byte[36];
            Array.Copy(mcuByte, 0, dv, 0, 36);
            //byte[] dv = new byte[36] { 0x00, 0x00, 0x00, 0xdb, 0x01, 0x00, 0x50, 0x64, 0x00, 0x00, 0x04, 0x61, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            //首部校验和
            byte[] dataVerify = CRC32Cls.GetCRC32Str(dv);// by前面 36个字节数据;
            Array.Copy(dataVerify, 0, mcuByte, 36, 4);

            //todo 40个字节数据 - 根据Excel协议 CCU→ECM
            if (SendData[comID].Length >= 40)
            {
                // 0: 生命信号 (USINT)
                lifeSignals = mcuCount[comID];//侯向盼2024.4.29修订，生命信号连续   
                lifeSignalss = BitConverter.GetBytes(lifeSignals);

                SendData[comID][0] = (byte)(lifeSignals & 0xFF); // 生命信号0-255

                // 1,2: 预留 (UINT)
                SendData[comID][1] = 0x00;
                SendData[comID][2] = 0x00;

                // 3: 时间有效标志 (BOOL) - 字节3的位0-3
                byte timeValidByte = SendData[comID][3];
                // 设置位0为时间有效标志，这里假设需要设置为1
                timeValidByte |= 0x01; // 设置位0为1
                SendData[comID][3] = timeValidByte;

                // 字节3的位4-7预留

                // 4: 预留字节
                SendData[comID][4] = 0x00;

                // 5: 年 (USINT) - 实际值=通信值+2000
                SendData[comID][5] = (byte)(DateTime.Now.Year - 2000);

                // 6: 月 (USINT)
                SendData[comID][6] = (byte)DateTime.Now.Month;

                // 7: 日 (USINT)
                SendData[comID][7] = (byte)DateTime.Now.Day;

                // 8: 时 (USINT)
                SendData[comID][8] = (byte)DateTime.Now.Hour;

                // 9: 分 (USINT)
                SendData[comID][9] = (byte)DateTime.Now.Minute;

                // 10: 秒 (USINT)
                SendData[comID][10] = (byte)DateTime.Now.Second;

                // 11-12: 预留 (UINT)
                SendData[comID][11] = 0x00;
                SendData[comID][12] = 0x00;

                // 13-14: 预留 (UINT)
                SendData[comID][13] = 0x00;
                SendData[comID][14] = 0x00;

                // 15-16: 预留 (INT)
                SendData[comID][15] = 0x00;
                SendData[comID][16] = 0x00;

                // 17-18: 预留 (UINT)
                SendData[comID][17] = 0x00;
                SendData[comID][18] = 0x00;

                // 19-20: 预留 (UINT)
                SendData[comID][19] = 0x00;
                SendData[comID][20] = 0x00;

                // 21-22: 预留 (UINT)
                SendData[comID][21] = 0x00;
                SendData[comID][22] = 0x00;

                // 23-24: 预留 (UINT)
                SendData[comID][23] = 0x00;
                SendData[comID][24] = 0x00;

                // 25-26: 预留 (UDINT) - 4字节数据，需要大端模式
                byte[] udintReserved = BitConverter.GetBytes(0u).Reverse().ToArray();
                SendData[comID][25] = udintReserved[0];
                SendData[comID][26] = udintReserved[1];
                // 注意：UDINT是4字节，但表格显示25,26，可能需要确认实际长度

                // 27-28: uiReServer2 (UINT)
                SendData[comID][27] = 0x00;
                SendData[comID][28] = 0x00;

                // 29-30: usReServer1 (USINT) - 但类型标注为USINT，长度可能为1字节
                SendData[comID][29] = 0x00;

                // 31-32: usReServer2 (USINT)
                SendData[comID][31] = 0x00;

                // 33-34: usReServer3 (USINT)
                SendData[comID][33] = 0x00;

                // 35-36: usReServer4 (USINT)
                SendData[comID][35] = 0x00;

                // 37-38: usReServer5 (USINT)
                SendData[comID][37] = 0x00;

                // 39-40: usReServer6 (USINT)
                SendData[comID][39] = 0x00;
            }

            //TRDPCrc(comID, SMIvalue);

            //应用数据
            Array.Copy(SendData[comID], 0, mcuByte, 40, SendData[comID].Length);

            return mcuByte;
        }

        /// <summary>
        /// 获取存储的数据
        /// </summary>
        /// <returns></returns>
        public double GetDicValue(string key)
        {
            try
            {
                //读取字典
                if (trdpValue.TryGetValue(key, out var sectionDict))
                {
                    return sectionDict.ToDouble();
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 设置帧尾验证码
        /// </summary>
        public void TRDPCrc(int comID, int smiValue)
        {
            //生命信号
            //lifeSignals++;
            lifeSignals = mcuCount[comID];//侯向盼2024.4.29修订，生命信号连续   
            lifeSignalss = BitConverter.GetBytes(lifeSignals);
            Array.Reverse(lifeSignalss);

            //第一次校验
            byte[] send = new byte[32];
            //SMI----SID数据结构
            byte[] smi = BitConverter.GetBytes(smiValue);
            Array.Reverse(smi);
            Array.Copy(smi, 0, send, 0, 4);

            //reserved01----SID数据结构
            byte[] reserved01 = new byte[2];
            Array.Copy(reserved01, 0, send, 4, 2);

            //SDTProtVers----SID数据结构
            byte[] SDTProtVers = new byte[2];
            SDTProtVers[0] = 0x00;
            SDTProtVers[1] = 0x02;
            Array.Copy(SDTProtVers, 0, send, 6, 2);

            //cstUUID----SID数据结构
            byte[] cstUUID = new byte[16];
            Array.Copy(cstUUID, 0, send, 8, 16);

            //SafeTopoCount----SID数据结构
            byte[] SafeTopoCount = new byte[4];

            Array.Copy(SafeTopoCount, 0, send, 24, 4);

            //reserved02----SID数据结构
            byte[] reserved02 = new byte[4];
            Array.Copy(reserved02, 0, send, 28, 4);

            //第一次校验结果值
            UInt32 crc = sdt_crc32(send, 32, 0xffffffff);

            //第二次校验
            int sinkSize = fullData[comID].Length;
            fullData[comID][sinkSize - 10] = 0x01;//VDP尾部用户数据主版本号
            fullData[comID][sinkSize - 9] = 0x02;//VDP尾部用户数据副版本号 //,不同的交换机厂家不一样，目前时代底层是0102，但应用层可配置
            Array.Copy(lifeSignalss, 0, fullData[comID], sinkSize - 8, 4);//SafeSecucount，安全序列计数器
            byte[] buf = new byte[sinkSize - 4];
            for (int i = 0; i < sinkSize - 4; i++)
            {
                buf[i] = fullData[comID][i];
            }
            //byte[] buf0 = new byte[100 - 4] { 0x00, 0xcd, 0xbb, 0x06, 0x00, 0x03, 0x48, 0x03, 0x48, 0x0c, 0x00, 0x00, 0x00, 0x03, 0x62, 0x00, 0x00, 0x01, 0x18, 0x00, 0x00, 0x07, 0x3a, 0x00, 0x00, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0xc8, 0x02, 0x00, 0x00, 0x00, 0x00, 0x01, 0x09, 0x01, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x02, 0x00, 0x00, 0x00, 0xcc };
            UInt32 crc1 = sdt_crc32(buf, Convert.ToUInt32(sinkSize - 4), crc);
            byte[] b2 = BitConverter.GetBytes(crc1);
            //最后算出来的校验放到要发送的数组中
            fullData[comID][sinkSize - 1] = b2[0];
            fullData[comID][sinkSize - 2] = b2[1];
            fullData[comID][sinkSize - 3] = b2[2];
            fullData[comID][sinkSize - 4] = b2[3];
        }
        static UInt32 sdt_crc32(byte[] buf, UInt32 len, UInt32 crc)
        {
            UInt32 i;
            for (i = 0; i < len; i++)
            {
                crc = crctab32[((UInt32)(crc >> 24) ^ buf[i]) & 0xff] ^ (crc << 8);
            }
            return crc;
        }


        static UInt32[] crctab32 =
        {
0x00000000U, 0xF4ACFB13U, 0x1DF50D35U, 0xE959F626U,
0x3BEA1A6AU, 0xCF46E179U, 0x261F175FU, 0xD2B3EC4CU,
0x77D434D4U, 0x8378CFC7U, 0x6A2139E1U, 0x9E8DC2F2U,
0x4C3E2EBEU, 0xB892D5ADU, 0x51CB238BU, 0xA567D898U,
0xEFA869A8U, 0x1B0492BBU, 0xF25D649DU, 0x06F19F8EU,
0xD44273C2U, 0x20EE88D1U, 0xC9B77EF7U, 0x3D1B85E4U,
0x987C5D7CU, 0x6CD0A66FU, 0x85895049U, 0x7125AB5AU,
0xA3964716U, 0x573ABC05U, 0xBE634A23U, 0x4ACFB130U,
0x2BFC2843U, 0xDF50D350U, 0x36092576U, 0xC2A5DE65U,
0x10163229U, 0xE4BAC93AU, 0x0DE33F1CU, 0xF94FC40FU,
0x5C281C97U, 0xA884E784U, 0x41DD11A2U, 0xB571EAB1U,
0x67C206FDU, 0x936EFDEEU, 0x7A370BC8U, 0x8E9BF0DBU,
0xC45441EBU, 0x30F8BAF8U, 0xD9A14CDEU, 0x2D0DB7CDU,
0xFFBE5B81U, 0x0B12A092U, 0xE24B56B4U, 0x16E7ADA7U,
0xB380753FU, 0x472C8E2CU, 0xAE75780AU, 0x5AD98319U,
0x886A6F55U, 0x7CC69446U, 0x959F6260U, 0x61339973U,
0x57F85086U, 0xA354AB95U, 0x4A0D5DB3U, 0xBEA1A6A0U,
0x6C124AECU, 0x98BEB1FFU, 0x71E747D9U, 0x854BBCCAU,
0x202C6452U, 0xD4809F41U, 0x3DD96967U, 0xC9759274U,
0x1BC67E38U, 0xEF6A852BU, 0x0633730DU, 0xF29F881EU,
0xB850392EU, 0x4CFCC23DU, 0xA5A5341BU, 0x5109CF08U,
0x83BA2344U, 0x7716D857U, 0x9E4F2E71U, 0x6AE3D562U,
0xCF840DFAU, 0x3B28F6E9U, 0xD27100CFU, 0x26DDFBDCU,
0xF46E1790U, 0x00C2EC83U, 0xE99B1AA5U, 0x1D37E1B6U,
0x7C0478C5U, 0x88A883D6U, 0x61F175F0U, 0x955D8EE3U,
0x47EE62AFU, 0xB34299BCU, 0x5A1B6F9AU, 0xAEB79489U,
0x0BD04C11U, 0xFF7CB702U, 0x16254124U, 0xE289BA37U,
0x303A567BU, 0xC496AD68U, 0x2DCF5B4EU, 0xD963A05DU,
0x93AC116DU, 0x6700EA7EU, 0x8E591C58U, 0x7AF5E74BU,
0xA8460B07U, 0x5CEAF014U, 0xB5B30632U, 0x411FFD21U,
0xE47825B9U, 0x10D4DEAAU, 0xF98D288CU, 0x0D21D39FU,
0xDF923FD3U, 0x2B3EC4C0U, 0xC26732E6U, 0x36CBC9F5U,
0xAFF0A10CU, 0x5B5C5A1FU, 0xB205AC39U, 0x46A9572AU,
0x941ABB66U, 0x60B64075U, 0x89EFB653U, 0x7D434D40U,
0xD82495D8U, 0x2C886ECBU, 0xC5D198EDU, 0x317D63FEU,
0xE3CE8FB2U, 0x176274A1U, 0xFE3B8287U, 0x0A977994U,
0x4058C8A4U, 0xB4F433B7U, 0x5DADC591U, 0xA9013E82U,
0x7BB2D2CEU, 0x8F1E29DDU, 0x6647DFFBU, 0x92EB24E8U,
0x378CFC70U, 0xC3200763U, 0x2A79F145U, 0xDED50A56U,
0x0C66E61AU, 0xF8CA1D09U, 0x1193EB2FU, 0xE53F103CU,
0x840C894FU, 0x70A0725CU, 0x99F9847AU, 0x6D557F69U,
0xBFE69325U, 0x4B4A6836U, 0xA2139E10U, 0x56BF6503U,
0xF3D8BD9BU, 0x07744688U, 0xEE2DB0AEU, 0x1A814BBDU,
0xC832A7F1U, 0x3C9E5CE2U, 0xD5C7AAC4U, 0x216B51D7U,
0x6BA4E0E7U, 0x9F081BF4U, 0x7651EDD2U, 0x82FD16C1U,
0x504EFA8DU, 0xA4E2019EU, 0x4DBBF7B8U, 0xB9170CABU,
0x1C70D433U, 0xE8DC2F20U, 0x0185D906U, 0xF5292215U,
0x279ACE59U, 0xD336354AU, 0x3A6FC36CU, 0xCEC3387FU,
0xF808F18AU, 0x0CA40A99U, 0xE5FDFCBFU, 0x115107ACU,
0xC3E2EBE0U, 0x374E10F3U, 0xDE17E6D5U, 0x2ABB1DC6U,
0x8FDCC55EU, 0x7B703E4DU, 0x9229C86BU, 0x66853378U,
0xB436DF34U, 0x409A2427U, 0xA9C3D201U, 0x5D6F2912U,
0x17A09822U, 0xE30C6331U, 0x0A559517U, 0xFEF96E04U,
0x2C4A8248U, 0xD8E6795BU, 0x31BF8F7DU, 0xC513746EU,
0x6074ACF6U, 0x94D857E5U, 0x7D81A1C3U, 0x892D5AD0U,
0x5B9EB69CU, 0xAF324D8FU, 0x466BBBA9U, 0xB2C740BAU,
0xD3F4D9C9U, 0x275822DAU, 0xCE01D4FCU, 0x3AAD2FEFU,
0xE81EC3A3U, 0x1CB238B0U, 0xF5EBCE96U, 0x01473585U,
0xA420ED1DU, 0x508C160EU, 0xB9D5E028U, 0x4D791B3BU,
0x9FCAF777U, 0x6B660C64U, 0x823FFA42U, 0x76930151U,
0x3C5CB061U, 0xC8F04B72U, 0x21A9BD54U, 0xD5054647U,
0x07B6AA0BU, 0xF31A5118U, 0x1A43A73EU, 0xEEEF5C2DU,
0x4B8884B5U, 0xBF247FA6U, 0x567D8980U, 0xA2D17293U,
0x70629EDFU, 0x84CE65CCU, 0x6D9793EAU, 0x993B68F9U
};

        // 添加端口到标签列表的映射
        private Dictionary<int, List<FullTags>> portToTagsMap = new Dictionary<int, List<FullTags>>();

        public void LoadData(List<AgreementData> data)
        {
            tRDPConfig.Save();

            foreach (var item in tags)
            {

                if (item.Identity && item.DataType == "U16")//2024.5.30侯向盼新增，区分生命信号U16,U32
                {
                    lifesigallength[Convert.ToInt32(item.comID)] = "U16";
                }
                else if (item.Identity && item.DataType == "U32")
                {
                    lifesigallength[Convert.ToInt32(item.comID)] = "U32";
                }
                else if (item.Identity && item.DataType == "U8")
                {
                    lifesigallength[Convert.ToInt32(item.comID)] = "U8";
                }
                else if (item.Identity)
                {

                    MessageBox.Show("目前生命信号仅支持U16,U32类型，如需其他类型，请联系开发人员");

                }
            }

            List<string> asd = new List<string>();
            List<string> asd1 = new List<string>();
            List<string> asd2 = new List<string>();
            foreach (var item in data)
            {
                if (!asd.Contains(item.yuLiu7) && item.yuLiu6 == "源设备")
                {
                    asd.Add(item.yuLiu7);//yuliu7 组播地址
                                         //zubocanshu.Add(new string[] { item.yuLiu9, item.yuLiu7, item.yuLiu2 });//IP地址，组播地址，端口号
                }
                if (!asd1.Contains(item.yuLiu7) && item.yuLiu6 == "宿设备")
                {
                    asd1.Add(item.yuLiu7);
                    listenComID.Add(item.yuLiu7);
                }
                if (!asd2.Contains(item.yuLiu9) && item.yuLiu6 == "源设备")
                {
                    asd2.Add(item.yuLiu9);//yuliu7 组播地址
                    //zubocanshu.Add(new string[] { item.yuLiu9, item.yuLiu7, item.yuLiu2 });//IP地址，组播地址，端口号
                    zubocanshu.Add(new string[] { tRDPConfig.LocalMulticastIP, tRDPConfig.SendMulticastIP, tRDPConfig.SendCOMID.ToString() }); //发送 IP地址，组播地址，端口号

                    // 给发送的地址
                    SendData[tRDPConfig.SendCOMID] = new byte[40];
                }
            }

            // 建立端口号到标签的映射
            portToTagsMap.Clear();
            foreach (var tag in tags)
            {
                int portNum = Convert.ToInt32(tag.comID);
                if (!portToTagsMap.ContainsKey(portNum))
                {
                    portToTagsMap[portNum] = new List<FullTags>();
                }
                portToTagsMap[portNum].Add(tag);
            }
        }

        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public List<Ports> ports = new List<Ports>();
        public List<FullTags> tags = new List<FullTags>();
        public Dictionary<int, byte[]> fullData = new Dictionary<int, byte[]>();//所有要接受数据，通过端口号进行存储
        public Dictionary<int, byte[]> SendData = new Dictionary<int, byte[]>();//所有要发送的数据，通过端口号进行存储
        public Dictionary<COMMData, FullTags> dicItems = new Dictionary<COMMData, FullTags>();
        public bool isInitSucess = false;
        //SerialPort sp1 = new SerialPort();
        /// <summary>
        /// 待发送源端口数据
        /// </summary>
        public Dictionary<int, byte[]> SourceData = new Dictionary<int, byte[]>();
        public bool isc = false;
        private Socket _socket;
        private byte[] _buffer = new byte[65535];
        public void Connect()
        {
            isc = true;

            IPAddress[] multiCastGroups = new IPAddress[zubocanshu.Count];
            IPAddress[] ipAddresses = new IPAddress[zubocanshu.Count];
            string ipgroupoutput = null;
            int[] multport = new int[zubocanshu.Count];
            for (int i = 0; i < zubocanshu.Count; i++)
            {

                try
                {
                    ipAddresses[i] = IPAddress.Parse(zubocanshu[i][0]);//IP地址
                    multiCastGroups[i] = IPAddress.Parse(zubocanshu[i][1]);//组播地址
                    multport[i] = int.Parse(zubocanshu[i][2]);//端口号

                }
                catch
                {
                    MessageBox.Show("请检查DMS中源设备的IP地址、组播地址、端口号是否正常");
                    return;
                }

            }
            // 组播组地址列表
            if (zubocanshu.Count > 0)
            {
                Socket client0 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//测试
                Socket client1 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//测试
                Socket client2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//测试
                Socket client3 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//测试
                Socket client4 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//测试
                Socket client5 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//测试
                Socket client6 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//测试
                Socket client7 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//测试
                Socket client8 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//测试
                Socket client9 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//测试

                try
                {
                    client0.Bind(new IPEndPoint(ipAddresses[0], 0));
                    client0.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership,
                           new MulticastOption(multiCastGroups[0], ipAddresses[0]));
                    // 设置TTL
                    byte ttl = 64; // 设置TTL为64，Time to live
                    client0.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, ttl);

                    // 设置服务类型为101，这是多播组所需的IP服务类型
                    byte tos = 160; // 设置服务类型为101000 00
                    client0.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.TypeOfService, tos);//未成功显示，待定
                    if (zubocanshu.Count > 1)
                    {
                        client1.Bind(new IPEndPoint(ipAddresses[1], 0));
                        client1.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership,
                               new MulticastOption(multiCastGroups[1], ipAddresses[1]));
                        client1.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, ttl);
                        // 设置服务类型为101，这是多播组所需的IP服务类型
                        client1.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.TypeOfService, tos);
                    }
                    if (zubocanshu.Count > 2)
                    {
                        client2.Bind(new IPEndPoint(ipAddresses[2], 0));
                        client2.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership,
                               new MulticastOption(multiCastGroups[2], ipAddresses[2]));
                        client2.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, ttl);
                        client2.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.TypeOfService, tos);//未成功显示，待定
                    }
                    if (zubocanshu.Count > 3)
                    {
                        client3.Bind(new IPEndPoint(ipAddresses[3], 0));
                        client3.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership,
                               new MulticastOption(multiCastGroups[3], ipAddresses[3]));
                        client3.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, ttl);
                        client3.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.TypeOfService, tos);//未成功显示，待定
                    }
                    if (zubocanshu.Count > 4)
                    {
                        client4.Bind(new IPEndPoint(ipAddresses[4], 0));
                        client4.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership,
                               new MulticastOption(multiCastGroups[4], ipAddresses[4]));
                        client4.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, ttl);
                        client4.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.TypeOfService, tos);//未成功显示，待定
                    }
                    if (zubocanshu.Count > 5)
                    {
                        client5.Bind(new IPEndPoint(ipAddresses[5], 0));
                        client5.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership,
                               new MulticastOption(multiCastGroups[5], ipAddresses[5]));
                        client5.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, ttl);
                        client5.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.TypeOfService, tos);//未成功显示，待定
                    }
                    if (zubocanshu.Count > 6)
                    {
                        client6.Bind(new IPEndPoint(ipAddresses[6], 0));
                        client6.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership,
                               new MulticastOption(multiCastGroups[6], ipAddresses[6]));
                        client6.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, ttl);
                        client6.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.TypeOfService, tos);//未成功显示，待定
                    }
                    if (zubocanshu.Count > 7)
                    {
                        client7.Bind(new IPEndPoint(ipAddresses[7], 0));
                        client7.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership,
                               new MulticastOption(multiCastGroups[7], ipAddresses[7]));
                        client7.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, ttl);
                        client7.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.TypeOfService, tos);//未成功显示，待定
                    }
                    if (zubocanshu.Count > 8)
                    {
                        client8.Bind(new IPEndPoint(ipAddresses[8], 0));
                        client8.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership,
                               new MulticastOption(multiCastGroups[8], ipAddresses[8]));
                        client8.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, ttl);
                        client8.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.TypeOfService, tos);//未成功显示，待定
                    }
                    if (zubocanshu.Count > 9)
                    {
                        client9.Bind(new IPEndPoint(ipAddresses[9], 0));
                        client9.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership,
                               new MulticastOption(multiCastGroups[9], ipAddresses[9]));
                        client9.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, ttl);
                        client9.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.TypeOfService, tos);//未成功显示，待定
                    }
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                    string msg = $"TRDP连接提示：设置的IP地址[{ipAddresses[0]}]，未在正通信的网卡中匹配到IP地址,请联系工艺人员确认IP地址";
                    MessageBox.Show(msg, "TRDP连接提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    throw new Exception(msg);

                }
                isInitSucess = true;

                // 读TRDP数据
                Read();
                ReadTimer();


                // 主控制才发送心跳
                if (Var.SysConfig.ExeType == 1)
                {
                    //暂时注释，不需要回传数据
                    IEnumerable<IGrouping<int, Ports>> group = ports.Where(p => !p.IsRead).GroupBy(p => p.Rate);//找到源设备
                    foreach (var item in group)
                    {
                        Thread t = new Thread(new ThreadStart(delegate
                        {
                            while (true)
                            {
                                if (isInitSucess)
                                {
                                    foreach (var item1 in item.ToList())
                                    {

                                        if (fullData.ContainsKey(item1.PortNum1) == false)
                                        {
                                            //运行过程中，如果点击重新下载，会把fullData字典清空，导致没有comID，运行就出错了。
                                            //下载过程中，此线程暂时跳出
                                            continue;
                                        }

                                        byte[] buf = SetMcu(tRDPConfig.SendCOMID, item1.SMIValue);  //item1.PortNum1, item1.SMIValue
                                        try
                                        {
                                            for (int i = 0; i < zubocanshu.Count; i++)
                                            {
                                                if (multport[i] == tRDPConfig.SendCOMID) //multport[i] == item1.PortNum1
                                                {
                                                    if (i == 0)
                                                    {
                                                        client0.SendTo(buf, new IPEndPoint(multiCastGroups[i], int.Parse("17224")));
                                                    }
                                                    else if (i == 1)
                                                    {
                                                        client1.SendTo(buf, new IPEndPoint(multiCastGroups[i], int.Parse("17224")));
                                                    }
                                                    else if (i == 2)
                                                    {
                                                        client2.SendTo(buf, new IPEndPoint(multiCastGroups[i], int.Parse("17224")));
                                                    }
                                                    else if (i == 3)
                                                    {
                                                        client3.SendTo(buf, new IPEndPoint(multiCastGroups[i], int.Parse("17224")));
                                                    }
                                                    else if (i == 4)
                                                    {
                                                        client4.SendTo(buf, new IPEndPoint(multiCastGroups[i], int.Parse("17224")));
                                                    }
                                                    else if (i == 5)
                                                    {
                                                        client5.SendTo(buf, new IPEndPoint(multiCastGroups[i], int.Parse("17224")));
                                                    }
                                                    else if (i == 6)
                                                    {
                                                        client6.SendTo(buf, new IPEndPoint(multiCastGroups[i], int.Parse("17224")));
                                                    }
                                                    else if (i == 7)
                                                    {
                                                        client7.SendTo(buf, new IPEndPoint(multiCastGroups[i], int.Parse("17224")));
                                                    }
                                                    else if (i == 8)
                                                    {
                                                        client8.SendTo(buf, new IPEndPoint(multiCastGroups[i], int.Parse("17224")));
                                                    }
                                                    else if (i == 9)
                                                    {
                                                        client9.SendTo(buf, new IPEndPoint(multiCastGroups[i], int.Parse("17224")));
                                                    }

                                                    else
                                                    {
                                                        MessageBox.Show("未在本设备正通信的网卡中匹配到源设备IP地址,请联系工艺人员确认IP地址");
                                                        throw new Exception("设置的IP地址，未在正通信的网卡中匹配到IP地址,请联系工艺人员确认IP地址");
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {

                                            MessageBox.Show("发送数据异常:" + ex);
                                            return;
                                        }
                                        //Thread.Sleep(10);//侯向盼2024.5.5进行注释
                                    }
                                    Thread.Sleep(item.Key);
                                }
                            }
                        }));
                        t.IsBackground = true;
                        t.Name = "TRDP_Set" + item.Key;
                        t.Start();
                    }
                }
            }
        }

        /// <summary>
        /// 读数据
        /// </summary>
        public void Read()
        {
            Thread rt = new Thread(new ThreadStart(delegate
            {
                // 创建UDP Socket
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                IPAddress _localIp = IPAddress.Parse(zubocanshu[0][0]);
                // 绑定到本地IP和端口
                IPEndPoint localEndPoint = new IPEndPoint(_localIp, 17224);
                _socket.Bind(localEndPoint);

                // 我需要监听的
                listenComID.Add(tRDPConfig.ReceiveMulticastIP);
                // 加入所有组播组System.Net.Sockets.SocketException:“在其上下文中，该请求的地址无效。”
                for (int i = 0; i < listenComID.Count; i++)
                {
                    var multicastOption = new MulticastOption(IPAddress.Parse(listenComID[i]), _localIp);
                    _socket.SetSocketOption(
                        SocketOptionLevel.IP,
                        SocketOptionName.AddMembership,
                        multicastOption);

                    Console.WriteLine($"成功加入组播组: {listenComID[i]}");
                }
                UdpClient client = new UdpClient(int.Parse("17224"));//正式
                for (int i = 0; i < listenComID.Count; i++)
                {
                    client.JoinMulticastGroup(IPAddress.Parse(listenComID[i]));//加入监听的组播地址
                }
                while (true)
                {
                    try
                    {
                        IPEndPoint remoteEndPoint = null;
                        byte[] buf = client.Receive(ref remoteEndPoint);//正式
                        byte[] comid = new byte[4];
                        Array.Copy(buf, 8, comid, 0, 4);
                        int comid1 = BitConverter.ToInt32(comid.Reverse().ToArray(), 0);
                        if (tRDPConfig.ReceiveCOMID == comid1) //fullData.ContainsKey(comid1)
                        {
                            //if (ports.FirstOrDefault(p => p.Port == comid1.ToString()) != null) {
                            //是读取端口才赋值
                            Array.Copy(buf, 40, fullData[BitConverter.ToInt32(comid.Reverse().ToArray(), 0)], 0, buf.Length - 40);//正式

                            // 更新 FullTags 中的实时数据
                            UpdateTagsRealTimeData(comid1, fullData[comid1]);
                            //}

                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
            }));
            rt.IsBackground = true;
            rt.Name = "TRDP_Read";
            rt.Start();
        }

        /// <summary>
        /// 读数据计时
        /// </summary>
        public void ReadTimer()
        {
            Thread rt = new Thread(new ThreadStart(delegate
            {
                while (true)
                {
                    try
                    {
                        // 如果掉线4秒，则连接中断
                        if (tags[0].LastUpdateTime < DateTime.Now.AddSeconds(-3))
                        {
                            IsConnected = false;
                        }
                        else
                        {
                            IsConnected = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                    Thread.Sleep(1000);
                }
            }));
            rt.IsBackground = true;
            rt.Name = "TRDP_Read_Time";
            rt.Start();
        }

        /// <summary>
        /// 使用实时数据更新对应端口的所有标签
        /// </summary>
        private void UpdateTagsRealTimeData(int port, byte[] realTimeData)
        {
            if (!portToTagsMap.ContainsKey(port) || realTimeData == null)
                return;

            var tagsForPort = portToTagsMap[port];

            foreach (var tag in tagsForPort)
            {
                try
                {
                    // 使用现有的 GetByte 方法解析数据
                    decimal realTimeValue = GetByte(port, tag.Offset, tag.Bit, tag.DataType, tag.dataFormat);
                    if (tag.RawHight != 0)
                    {
                        // 线性换算公式：工程值 = (原始值 - 原始低) / (原始高 - 原始低) * (工程高 - 工程低) + 工程低
                        realTimeValue = (realTimeValue - tag.RawLow) / (tag.RawHight - tag.RawLow) * (tag.ScaledHight - tag.ScaledLow) + tag.ScaledLow;
                    }
                    // 更新标签的实时值
                    tag.RealTimeValue = realTimeValue;
                    tag.LastUpdateTime = DateTime.Now;
                    tag.IsValueUpdated = true;

                    // 触发值改变事件
                    OnTagValueChanged(tag, realTimeValue);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"更新标签{tag.DataLabel}数据失败: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 触发标签值改变事件
        /// </summary>
        private void OnTagValueChanged(FullTags tag, decimal newValue)
        {
            // 事件触发
            trdpValue.AddOrUpdate(tag.DataLabel, newValue, (k, oldValue) => newValue);

            KeyValueChange?.Invoke(this, new TRDPValueChangedEventArgs(tag, tag.DataLabel, newValue));
        }

        /// <summary>
        /// TRDP读取位
        /// </summary>
        /// <param name="targetNum"></param>
        /// <param name="bitNum"></param>
        /// <returns></returns>
        public bool GetByBit(int port, int targetNum, int bitNum)
        {
            byte value = fullData[port][targetNum];
            int tmpInt = 1 << bitNum;
            return (value & tmpInt) / tmpInt == 0 ? false : true;
        }

        /// <summary>
        /// TRDP读取值
        /// </summary>
        /// <param name="targetNum"></param>
        /// <param name="bitNum"></param>
        /// <returns></returns>
        public decimal GetByte(int port, int targetNum, int bit, string DataType, decimal dataFormat)
        {
            // 暂时注释，已经在解析数组的时候就已经换算了，所以不在此处进行16进制换算。
            //port = int.Parse(Convert.ToString(port, 16));
            decimal value = 0;
            if (!fullData.ContainsKey(port))
                return 0;
            GetVal(ref value, DataType, targetNum, bit, fullData[port], 0, port, dataFormat);
            return value;
        }

        public void GetVal(ref decimal value, string dataType, int offset, int Bit, byte[] data, int bits, int port, decimal dataFormat)
        {
            byte[] temp = null;
            //offset = ConvertBit(offset, Bit)[0];
            //Bit = ConvertBit(offset, Bit)[1];
            switch (dataType)
            {
                case "B1":
                    value = GetByBit(port, offset, Bit) ? 1 : 0;
                    break;
                case "U8":
                    offset += Bit;
                    value = data[offset] * dataFormat;
                    break;
                case "I8":
                    value = (sbyte)data[offset] * dataFormat;//TODO：请注意，此处负数的处理
                    break;
                case "U16":
                    temp = new byte[2];
                    Array.Copy(data, offset, temp, bits, temp.Length);
                    value = BitConverter.ToUInt16(temp.Reverse().ToArray(), 0) * dataFormat;
                    break;
                case "I16":
                    temp = new byte[2];
                    Array.Copy(data, offset, temp, bits, temp.Length);
                    value = BitConverter.ToInt16(temp.Reverse().ToArray(), 0) * dataFormat;
                    break;
                case "U32":
                    temp = new byte[4];
                    Array.Copy(data, offset, temp, bits, temp.Length);
                    value = BitConverter.ToUInt32(temp.Reverse().ToArray(), 0) * dataFormat;
                    break;
                case "I32":
                    temp = new byte[4];
                    Array.Copy(data, offset, temp, bits, temp.Length);
                    value = BitConverter.ToInt32(temp.Reverse().ToArray(), 0) * dataFormat;
                    break;
                case "U64":
                    temp = new byte[8];
                    Array.Copy(data, offset, temp, bits, temp.Length);
                    value = BitConverter.ToUInt64(temp.Reverse().ToArray(), 0) * dataFormat;
                    break;
                case "I64":
                    temp = new byte[8];
                    Array.Copy(data, offset, temp, bits, temp.Length);
                    value = BitConverter.ToInt64(temp.Reverse().ToArray(), 0) * dataFormat;
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 将字偏移装换为字节偏移
        /// </summary>
        /// <param name="offset">字偏移</param>
        /// <param name="bit">位偏移</param>
        /// <returns></returns>
        public List<int> ConvertBit(int offset, int bit)
        {
            return new List<int>() { offset * 2 + bit / 8, bit % 8 };
        }

        /// <summary>
        /// 数据写入通用方法，自动判断当前是
        /// </summary>
        /// <param name="commType">通讯类型，0为以太网，1为MVB</param>
        /// <param name="port">通讯的端口号</param>
        /// <param name="Offset">字节偏移量</param>
        /// <param name="bit">位偏移量</param>
        /// <param name="value">写入的值，</param>
        public void DataWrite(int port, int OffsetSrc, int bitSrc, object value)
        {
            int Offset = OffsetSrc;// ConvertBit(OffsetSrc, bitSrc)[0];
            int bit = ConvertBit(OffsetSrc, bitSrc)[1];

            byte[] bts = null;
            switch (value.GetType().Name)
            {
                case "Boolean":
                    if (value.Equals(true))
                        fullData[port][Offset] = (byte)(fullData[port][Offset] | (1 << bit));
                    else
                        fullData[port][Offset] = (byte)(fullData[port][Offset] & ~(1 << bit));
                    break;
                case "Byte":
                    bts = new byte[] { Convert.ToByte(value) };
                    Offset += bitSrc;
                    break;
                case "Int16": bts = BitConverter.GetBytes(Convert.ToInt16(value)); break;
                case "UInt16": bts = BitConverter.GetBytes(Convert.ToUInt16(value)); break;
                case "Int32": bts = BitConverter.GetBytes(Convert.ToInt32(value)); break;
                case "UInt32": bts = BitConverter.GetBytes(Convert.ToUInt32(value)); break;
                case "Int64": bts = BitConverter.GetBytes(Convert.ToInt64(value)); break;
                case "UInt64": bts = BitConverter.GetBytes(Convert.ToUInt64(value)); break;
                case "Single": bts = BitConverter.GetBytes(Convert.ToSingle(value)); break;
                case "Double": bts = BitConverter.GetBytes(Convert.ToDouble(value)); break;
                default:
                    break;
            }


            if (bts != null)
            {
                byte[] w = bts.Reverse().ToArray();

                for (int i = 0; i < w.Length; i++)
                {
                    fullData[port][Offset + i] = w[i];
                }

                // Array.Copy(w, 0, fullData[port], Offset, w.Length);

            }
        }

        private void SetValue(int port, int byteSet, int bitSet, object value)
        {
            if (value is bool)
            {
                if (Convert.ToBoolean(value))
                    SourceData[port][byteSet] |= (byte)(1 << bitSet);
                else
                    SourceData[port][byteSet] &= (byte)~(1 << bitSet);
            }
            else if (value is byte || value is sbyte)
            {
                SourceData[port][byteSet] = Convert.ToByte(value);
            }
            else
            {
                byte[] bts = null;
                if (value is short)
                {
                    short val = (short)value;
                    bts = BitConverter.GetBytes(val).Reverse().ToArray();
                }
                else if (value is ushort)
                {
                    ushort val = (ushort)value;
                    bts = BitConverter.GetBytes(val).Reverse().ToArray();
                }
                else if (value is int)
                {
                    int val = (int)value;
                    bts = BitConverter.GetBytes(val).Reverse().ToArray();
                }
                else if (value is uint)
                {
                    uint val = (uint)value;
                    bts = BitConverter.GetBytes(val).Reverse().ToArray();
                }
                else if (value is long)
                {
                    long val = (long)value;
                    bts = BitConverter.GetBytes(val).Reverse().ToArray();
                }
                else if (value is ulong)
                {
                    ulong val = (ulong)value;
                    bts = BitConverter.GetBytes(val).Reverse().ToArray();
                }
                else if (value is float)
                {
                    float val = (float)value;
                    bts = BitConverter.GetBytes(val).Reverse().ToArray();
                }
                else if (value is double)
                {
                    double val = (double)value;
                    bts = BitConverter.GetBytes(val).Reverse().ToArray();
                }
                bts.CopyTo(SourceData[port], byteSet);
            }
        }

        public event EventHandler Error;
        public event EventHandler Connected;
        public event ValueChangeHandler ValueChanged;

        // 值改变后更新
        public event EventHandler<TRDPValueChangedEventArgs> KeyValueChange;

        public event ErrorHandler Errored;

        bool closeed = false;

        public void Close()
        {
            closeed = true;
        }

        //写互斥锁，防止同时写入，导致接收数据不对齐的问题
        static object locker = new object();
        public byte[] buffer;
        public bool reds = false;//标识是否配置TRDP模块


        public bool IsInitialized
        {
            get { return true; }
        }

        public int ReadTimeout { get; set; }


        // 是否连接
        public bool isConnect { get; set; }
        public bool IsConnected
        {
            get
            {
                return isConnect;
            }
            set
            {
                isConnect = value;
            }
        }

        public void OnError()
        {
            if (Error != null) Error(this, EventArgs.Empty);
        }

        #region IDriver 成员

        public object this[string key]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public object Read(string key)
        {
            throw new NotImplementedException();
        }

        public void Write(string key, object value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            this.Close();
        }

        #endregion

        #region IDriver 成员


        public void LoadConfig(IDriverConfig config)
        {
            throw new NotImplementedException();
        }

        #endregion
    }


    public delegate void RecievedHandler1(object sender, int comID, byte[] data);

    public class TRDPValueChangedEventArgs : EventArgs
    {
        public FullTags Tags { get; }
        public string Key { get; }
        public decimal Value { get; }

        public TRDPValueChangedEventArgs(FullTags tags, string key, decimal value)
        {
            Tags = tags;
            Key = key;
            Value = value;
        }
    }
}
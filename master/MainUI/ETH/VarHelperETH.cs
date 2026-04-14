using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using BogieIdling.UI;
using TestFunction;
using MainUI;

namespace TRDP
{
    public class VarHelperETH
    {



        /// <summary>
        /// 以10进制格式打印
        /// </summary>
        /// <param name="buffer"></param>
        public static void PrintOutByte(byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                Debug.WriteLine(i.ToString() + ":" + buffer[i].ToString().PadLeft(2, '0'));
            }
        }

        /// <summary>
        /// 以16进制格式打印
        /// </summary>
        /// <param name="buffer"></param>
        public static void PrintOutHex(byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                Debug.WriteLine(i.ToString() + ":" + Convert.ToString(buffer[i], 16).ToUpper().PadLeft(2, '0'));
            }
        }

        /// <summary>
        /// 获取16进制格式字符串
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string GetStrAryHex(byte[] buffer)
        {
            string rst = "";
            foreach (var item in buffer)
            {
                rst += Convert.ToString(item, 16).ToUpper().PadLeft(2, '0') + " ";
            }
            return rst.TrimEnd();
        }


        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }





        public const int CRC_CCITT = 0x1021;
        /// <summary>
        /// 获取CRC校验码 CRC-16 /XMODEM  X16+X12+X5+1
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static int crc_cal_by_bit(byte[] ptr, int len)
        {
            int crc = 0;
            int count = 0;
            while (len-- != 0)
            {
                for (uint i = 0x80; i != 0; i /= 2)
                {
                    crc *= 2;
                    if ((crc & 0x10000) != 0) //上一位CRC乘 2后，若首位是1，则除以 0x11021
                        crc ^= 0x11021;

                    if ((ptr[count] & i) != 0)    //如果本位是1，那么CRC = 上一位的CRC + 本位/CRC_CCITT
                        crc ^= CRC_CCITT;
                }
                count++;
            }
            return crc;
        }
        public static UInt16 Cal_crc16(byte[] data, int size)
        {

            UInt32 i = 0;
            UInt16 crc = 0;
            for (i = 0; i < size; i++)
            {
                crc = UpdateCRC16(crc, data[i]);
            }
            crc = UpdateCRC16(crc, 0);
            crc = UpdateCRC16(crc, 0);

            return (UInt16)(crc);
        }
        public static UInt16 UpdateCRC16(UInt16 crcIn, byte bytee)
        {
            UInt32 crc = crcIn;
            UInt32 ins = (UInt32)bytee | 0x100;

            do
            {
                crc <<= 1;
                ins <<= 1;
                if ((ins & 0x100) == 0x100)
                {
                    ++crc;
                }
                if ((crc & 0x10000) == 0x10000)
                {
                    crc ^= 0x1021;
                }
            }
            while (!((ins & 0x10000) == 0x10000));
            return (UInt16)crc;
        }


        /// <summary>
        /// 获取CRC校验码 CRC-16 /XMODEM  X16+X12+X5+1
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static int crc_cal_by_bit(byte[] ptr, int start, int len)
        {
            int crc = 0;
            int count = start;
            while (len-- != 0)
            {
                for (uint i = 0x80; i != 0; i /= 2)
                {
                    crc *= 2;
                    if ((crc & 0x10000) != 0) //上一位CRC乘 2后，若首位是1，则除以 0x11021
                        crc ^= 0x11021;

                    if ((ptr[count] & i) != 0)    //如果本位是1，那么CRC = 上一位的CRC + 本位/CRC_CCITT
                        crc ^= CRC_CCITT;
                }
                count++;
            }
            return crc;
        }

        public static byte[] byteSend = new byte[40];//100
        public static byte[] byteSend2 = new byte[64];//100
        public static byte[] byteSend3 = new byte[64];//100
        public static byte[] byteSend4 = new byte[64];//100
        public static byte[] EBVbyteSend = new byte[20];//100

        /// <summary>
        /// BCU 发CCU数据
        /// </summary>
        public static byte[] BCU_CCU;


        public static byte[] BCU_CCU2;
        public static byte[] BCU_CCU3;
        public static byte[] BCU_CCU4;
        public static byte[] BCU_CCUEBV;
        /// <summary>
        /// 四方赋值方法
        /// </summary>
        /// <param name="lifesignal"></param>
        /// <returns></returns>
        public static byte[] GetLifeBytes(int lifesignal)
        {

            byte[] bytelife = IntToBytes(lifesignal.ToString());
            //byteSend[0] = bytelife[2];
            //byteSend[1] = bytelife[3];
            byteSend[17] = bytelife[3];
            byteSend[20] = 0xAA;
            byteSend[33] = bytelife[3];

            byteSend[64] = 0xA5;
            byteSend[65] = 0x5A;

            byteSend[66] = bytelife[3];
            //byteSend[66] = 0xaa;
            byteSend[98] = bytelife[3];
            byteSend[130] = bytelife[3];
            byteSend[96] = 0x00;
            byteSend[97] = 0x3A;

            //获取校验码 0-221
            byte[] tmpByte = new byte[32];

            byte[] tmpByte2 = new byte[32];

            Array.Copy(byteSend, 64, tmpByte, 0, tmpByte.Length);
            // int crc = VarHelper.crc_cal_by_bit(tmpByte, tmpByte.Length);
            Array.Copy(byteSend, 96, tmpByte2, 0, tmpByte2.Length);

            int crc = VarHelperETH.Cal_crc16(tmpByte, tmpByte.Length);
            byte[] crcByte = IntToBytes(crc.ToString());

            // 校验码 需要确认是否为此种计算方式  ？？？？
            byteSend[222] = crcByte[2];
            byteSend[223] = crcByte[3];


            // TRDP crc16校验
            int ing = GetCrc16(tmpByte);
            string str = (ing.ToString("X")).PadLeft(4, '0');

            ConvertInt16ToByte(ing, out   byteSend[94], out byteSend[95]);//1bd7
            //byte[] by = GetModbusCrc16(tmpByte);
            //byteSend[94] = by[0];
            //byteSend[95] = by[1];
            ing = GetCrc16(tmpByte2);
            str = (ing.ToString("X")).PadLeft(4, '0');

            ConvertInt16ToByte(ing, out   byteSend[126], out byteSend[127]);

          
            return byteSend;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lifesignal">生命信号值</param>
        /// <returns></returns>
        public static byte[] GetLifeBytes_ZNCG(int lifesignal)
        {
            byte[] bytelife = IntToBytes(lifesignal.ToString());
            byteSend[Var.lifeport] = bytelife[3];
            return byteSend;
        }
        public static byte[] GetLifeBytes_ZNCG2(int lifesignal)
        {

            byte[] bytelife = IntToBytes(lifesignal.ToString());

            //byteSend[0] = bytelife[2];
            byteSend2[Var.lifeport] = bytelife[3];

            return byteSend2;
        }
        public static byte[] GetLifeBytes_ZNCG3(int lifesignal)
        {

            byte[] bytelife = IntToBytes(lifesignal.ToString());
            
            byteSend3[0] = bytelife[3];
            byteSend3[1] = bytelife[2];
            byte[] CRC = Var.crc16.CRC16(VarHelperETH.byteSend3, 0, 94);
            VarHelperETH.byteSend3[94] = CRC[1];
            VarHelperETH.byteSend3[95] = CRC[0];
            return byteSend3;
        }
        public static byte[] GetLifeBytes_ZNCG4(int lifesignal)
        {

            byte[] bytelife = IntToBytes(lifesignal.ToString());
            
            byteSend4[0] = bytelife[3];
            byteSend4[1] = bytelife[2];
            byte[] CRC = Var.crc16.CRC16(VarHelperETH.byteSend4, 0, 94);
            VarHelperETH.byteSend4[94] = CRC[1];
            VarHelperETH.byteSend4[95] = CRC[0];
            return byteSend4;
        }
        public static byte[] GetLifeBytes_ZNCG5(int lifesignal)
        {

            byte[] bytelife = IntToBytes(lifesignal.ToString());

            //byteSend[0] = bytelife[2];
            byteSend3[6] = bytelife[3];
            CRC1();
            return byteSend3;
        }
        public static byte[] GetLifeBytes_ZNCG6(int lifesignal)
        {

            byte[] bytelife = IntToBytes(lifesignal.ToString());

            //byteSend[0] = bytelife[2];
            byteSend4[6] = bytelife[3];
            CRC2();
            return byteSend4;
        }
        static void CRC1()
        {

            byte[] CRC = Var.crc16.CRC16(VarHelperETH.byteSend3, 0, 30);
            VarHelperETH.byteSend3[30] = CRC[1];
            VarHelperETH.byteSend3[31] = CRC[0];
        }
        static void CRC2()
        {

            byte[] CRC = Var.crc16.CRC16(VarHelperETH.byteSend4,0,30);
            VarHelperETH.byteSend4[30] = CRC[1];
            VarHelperETH.byteSend4[31] = CRC[0];
        }
        public static byte[] GetLifeBytes_EBV(int lifesignal)
        {

            byte[] bytelife = IntToBytes(lifesignal.ToString());
            EBVbyteSend[6] = bytelife[3];
            return EBVbyteSend;
        }

       
        public static byte[] GetModbusCrc16(byte[] bytes)
        {
            byte crcRegister_H = 0xFF, crcRegister_L = 0xFF;// 预置一个值为 0xFFFF 的 16 位寄存器

            byte polynomialCode_H = 0xA0, polynomialCode_L = 0x01;// 多项式码 0xA001

            for (int i = 0; i < bytes.Length; i++)
            {
                crcRegister_L = (byte)(crcRegister_L ^ bytes[i]);

                for (int j = 0; j < 8; j++)
                {
                    byte tempCRC_H = crcRegister_H;
                    byte tempCRC_L = crcRegister_L;

                    crcRegister_H = (byte)(crcRegister_H >> 1);
                    crcRegister_L = (byte)(crcRegister_L >> 1);
                    // 高位右移前最后 1 位应该是低位右移后的第 1 位：如果高位最后一位为 1 则低位右移后前面补 1
                    if ((tempCRC_H & 0x01) == 0x01)
                    {
                        crcRegister_L = (byte)(crcRegister_L | 0x80);
                    }

                    if ((tempCRC_L & 0x01) == 0x01)
                    {
                        crcRegister_H = (byte)(crcRegister_H ^ polynomialCode_H);
                        crcRegister_L = (byte)(crcRegister_L ^ polynomialCode_L);
                    }
                }
            }

            return new byte[] { crcRegister_L, crcRegister_H };
        }



        private static byte[] IntToBytes(string input)
        {
            int id = Convert.ToInt32(input);
            byte[] tmp = BitConverter.GetBytes(id);
            tmp = tmp.Reverse().ToArray(); //默认为低字节在前，需要反转成高字节在前
            return tmp;
        }

        public static void LogInfo(string txt)
        {
            try
            {
                string logPath = System.Windows.Forms.Application.StartupPath + "\\Log.txt";
                string myLogInfo = string.Format("{0}：{1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), txt);
                System.IO.File.AppendAllText(logPath, myLogInfo);
            }
            catch
            { }
        }


        //        //十进制转二进制
        //Console.WriteLine("十进制166的二进制表示: "+Convert.ToString(166, 2));
        ////十进制转八进制
        //Console.WriteLine("十进制166的八进制表示: "+Convert.ToString(166, 8));
        ////十进制转十六进制
        //Console.WriteLine("十进制166的十六进制表示: "+Convert.ToString(166, 16));

        ////二进制转十进制
        //Console.WriteLine("二进制 111101 的十进制表示: "+Convert.ToInt32("111101", 2));
        ////八进制转十进制
        //Console.WriteLine("八进制 44 的十进制表示: "+Convert.ToInt32("44", 8));
        ////十六进制转十进制
        //Console.WriteLine("十六进制 CC的十进制表示: "+Convert.ToInt32("CC", 16));

        //将十六进制转成十进制，再将十进制转为二进制即可。


        public static void ConvertInt16ToByte(int Int16, out byte bH, out byte bL)
        {
            //bH = (byte)(Int16 / 256);
            //bL = (byte)(Int16 % 256);
            bH = (byte)(Int16 >> 8);
            bL = (byte)(Int16 & 0xFF);
        }

        public static void ConvertInt16ToByte1(int Int16, out byte bH, out byte bL)
        {
            bH = (byte)(Int16 / 256);
            bL = (byte)(Int16 % 256);
        }
        /// <summary>
        /// 计算给定长度数据的16位CRC
        /// </summary>
        /// <param name="data">要计算CRC的字节数组</param>
        /// <returns>CRC值</returns>
        public static int GetCrc16(Byte[] data)
        {   // 初始化
            byte i = 0, crc_H8 = 0;
            int CRC = 0xffff;
            for (i = 0; i < (data.Length - 2); i++)
            {
                crc_H8 = (byte)(CRC >> 8);
                CRC = CRC << 8;
                CRC = CRC ^ CRC16TABLE[crc_H8 ^ data[i]];
            }
            return CRC;


        }
        /// <summary>
        /// CRC16查表
        /// </summary>
        private static readonly int[] CRC16TABLE = {
            0x0000, 0x1021, 0x2042, 0x3063, 0x4084, 0x50a5, 0x60c6, 0x70e7,
            0x8108, 0x9129, 0xa14a, 0xb16b, 0xc18c, 0xd1ad, 0xe1ce, 0xf1ef,
            0x1231, 0x0210, 0x3273, 0x2252, 0x52b5, 0x4294, 0x72f7, 0x62d6,
            0x9339, 0x8318, 0xb37b, 0xa35a, 0xd3bd, 0xc39c, 0xf3ff, 0xe3de,
            0x2462, 0x3443, 0x0420, 0x1401, 0x64e6, 0x74c7, 0x44a4, 0x5485,
            0xa56a, 0xb54b, 0x8528, 0x9509, 0xe5ee, 0xf5cf, 0xc5ac, 0xd58d,
            0x3653, 0x2672, 0x1611, 0x0630, 0x76d7, 0x66f6, 0x5695, 0x46b4,
            0xb75b, 0xa77a, 0x9719, 0x8738, 0xf7df, 0xe7fe, 0xd79d, 0xc7bc,
            0x48c4, 0x58e5, 0x6886, 0x78a7, 0x0840, 0x1861, 0x2802, 0x3823,
            0xc9cc, 0xd9ed, 0xe98e, 0xf9af, 0x8948, 0x9969, 0xa90a, 0xb92b,
            0x5af5, 0x4ad4, 0x7ab7, 0x6a96, 0x1a71, 0x0a50, 0x3a33, 0x2a12,
            0xdbfd, 0xcbdc, 0xfbbf, 0xeb9e, 0x9b79, 0x8b58, 0xbb3b, 0xab1a,
            0x6ca6, 0x7c87, 0x4ce4, 0x5cc5, 0x2c22, 0x3c03, 0x0c60, 0x1c41,
            0xedae, 0xfd8f, 0xcdec, 0xddcd, 0xad2a, 0xbd0b, 0x8d68, 0x9d49,
            0x7e97, 0x6eb6, 0x5ed5, 0x4ef4, 0x3e13, 0x2e32, 0x1e51, 0x0e70,
            0xff9f, 0xefbe, 0xdfdd, 0xcffc, 0xbf1b, 0xaf3a, 0x9f59, 0x8f78,
            0x9188, 0x81a9, 0xb1ca, 0xa1eb, 0xd10c, 0xc12d, 0xf14e, 0xe16f,
            0x1080, 0x00a1, 0x30c2, 0x20e3, 0x5004, 0x4025, 0x7046, 0x6067,
            0x83b9, 0x9398, 0xa3fb, 0xb3da, 0xc33d, 0xd31c, 0xe37f, 0xf35e,
            0x02b1, 0x1290, 0x22f3, 0x32d2, 0x4235, 0x5214, 0x6277, 0x7256,
            0xb5ea, 0xa5cb, 0x95a8, 0x8589, 0xf56e, 0xe54f, 0xd52c, 0xc50d,
            0x34e2, 0x24c3, 0x14a0, 0x0481, 0x7466, 0x6447, 0x5424, 0x4405,
            0xa7db, 0xb7fa, 0x8799, 0x97b8, 0xe75f, 0xf77e, 0xc71d, 0xd73c,
            0x26d3, 0x36f2, 0x0691, 0x16b0, 0x6657, 0x7676, 0x4615, 0x5634,
            0xd94c, 0xc96d, 0xf90e, 0xe92f, 0x99c8, 0x89e9, 0xb98a, 0xa9ab,
            0x5844, 0x4865, 0x7806, 0x6827, 0x18c0, 0x08e1, 0x3882, 0x28a3,
            0xcb7d, 0xdb5c, 0xeb3f, 0xfb1e, 0x8bf9, 0x9bd8, 0xabbb, 0xbb9a,
            0x4a75, 0x5a54, 0x6a37, 0x7a16, 0x0af1, 0x1ad0, 0x2ab3, 0x3a92,
            0xfd2e, 0xed0f, 0xdd6c, 0xcd4d, 0xbdaa, 0xad8b, 0x9de8, 0x8dc9,
            0x7c26, 0x6c07, 0x5c64, 0x4c45, 0x3ca2, 0x2c83, 0x1ce0, 0x0cc1,
            0xef1f, 0xff3e, 0xcf5d, 0xdf7c, 0xaf9b, 0xbfba, 0x8fd9, 0x9ff8,
            0x6e17, 0x7e36, 0x4e55, 0x5e74, 0x2e93, 0x3eb2, 0x0ed1, 0x1ef0
        };

        /// <summary>
        /// 8进制整数转换
        /// </summary>
        /// <param name="Int16">具体的结果值</param>
        /// <param name="bH"></param>
        /// <param name="bL"></param>
        public static byte ConvertInt8ToByte(int Int8)
        {
            return (byte)Int8;
        }
        /// <summary>
        /// 16进制整数转换
        /// </summary>
        /// <param name="Int16"></param>
        /// <param name="U16"></param>
        /// <returns></returns>
        public static byte[] ConvertInt16ToByte(int Int16, string U16)
        {
            byte[] date = new byte[2];
            if (U16 == "U16")
            {
                date = BitConverter.GetBytes(Convert.ToUInt16(Int16));
            }
            else if (U16 == "I16")
            {
                date = BitConverter.GetBytes(Convert.ToInt16(Int16));
            }
            return date;
        }
        public static byte[] ConvertInt32ToByte(int Int32, string U32)
        {
            byte[] date = new byte[4];
            if (U32 == "U32")
            {
                date = BitConverter.GetBytes(Convert.ToUInt32(Int32));
            }
            else if (U32 == "I32")
            {
                date = BitConverter.GetBytes(Convert.ToInt32(Int32));
            }
            return date;
        }
        public static byte[] ConvertInt24ToByte(int Int24)
        {
            byte[] date = new byte[3];
            date = BitConverter.GetBytes(Int24);
            return date;
        }
        public static byte[] ConvertInt128ToByte(int Int128)
        {
            byte[] date = new byte[128];
            date = BitConverter.GetBytes(Int128);
            return date;
        }
        /// <summary>
        /// byte数组指定字节数组指定bit的值
        /// </summary>
        /// <param name="byteIndex"></param>
        /// <param name="bitIndex"></param>
        /// <param name="date"></param>
        /// <param name="intvalue"></param>
        /// <param name="value"></param>
        public static void ConvertBoolToByte(int byteIndex, int bitIndex, byte[] date, bool value)
        {
            byte bytevalue = date[byteIndex];

            bool[] barr = conversion2((int)date[byteIndex]);
            barr[bitIndex] = value;

            bytevalue = (byte)conversion10(barr[0], barr[1], barr[2], barr[3], barr[4], barr[5], barr[6], barr[7]);
            date[byteIndex] = bytevalue;

        }
        /// <summary>
        /// 十进制数据 → bool[]  转换
        /// </summary>
        /// <param name="i10">要进行转换的十进制数</param>
        ///<returns>换换后的bool[]</returns>
        public static bool[] conversion2(int i10)
        {
            bool[] barray = new bool[8];
            try
            {
                string str2 = Convert.ToString(i10, 2);
                str2 = str2.PadLeft(8, '0');
                barray[0] = (str2.Substring(7, 1) == "1") ? true : false;
                barray[1] = (str2.Substring(6, 1) == "1") ? true : false;
                barray[2] = (str2.Substring(5, 1) == "1") ? true : false;
                barray[3] = (str2.Substring(4, 1) == "1") ? true : false;
                barray[4] = (str2.Substring(3, 1) == "1") ? true : false;
                barray[5] = (str2.Substring(2, 1) == "1") ? true : false;
                barray[6] = (str2.Substring(1, 1) == "1") ? true : false;
                barray[7] = (str2.Substring(0, 1) == "1") ? true : false;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("十制数据转换二进制数据出错 \r\n" + ex.Message);
            }
            return barray;
        }
        public static byte conversion3(int i10)
        {
            bool[] barray = new bool[8];
            byte a = byteSend[17];
            try
            {
                string str2 = Convert.ToString(i10, 2);
                str2 = str2.PadLeft(8, '0');
                barray[0] = (str2.Substring(0, 1) == "1") ? true : false;
                barray[1] = (str2.Substring(1, 1) == "1") ? true : false;
                barray[2] = (str2.Substring(2, 1) == "1") ? true : false;
                barray[3] = (str2.Substring(3, 1) == "1") ? true : false;
                barray[4] = (str2.Substring(7, 1) == "1") ? true : false;
                barray[5] = (str2.Substring(6, 1) == "1") ? true : false;
                barray[6] = (str2.Substring(5, 1) == "1") ? true : false;
                barray[7] = (str2.Substring(4, 1) == "1") ? true : false;

                a = (byte)conversion10(barray[0], barray[1], barray[2], barray[3], barray[4], barray[5], barray[6], barray[7]);
                //date[byteIndex] = bytevalue;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("十制数据转换二进制数据出错 \r\n" + ex.Message);
            }
            return a;
        }
        /// <summary>
        /// (+9重载)    二进制数据 → 十进制数据  转换
        /// </summary>
        /// <param name="b0">字节第 0 位值（右起第0位）</param>
        /// <param name="b1">字节第 1 位值（右起第1位）</param>
        /// <param name="b2">字节第 2 位值（右起第2位）</param>
        /// <param name="b3">字节第 3 位值（右起第3位）</param>
        /// <param name="b4">字节第 4 位值（右起第4位）</param>
        /// <param name="b5">字节第 5 位值（右起第5位）</param>
        /// <param name="b6">字节第 6 位值（右起第6位）</param>
        /// <param name="b7">字节第 7 位值（右起第7位）</param>
        /// <returns>转换后的十进制数</returns>
        public static int conversion10(bool b0, bool b1, bool b2, bool b3, bool b4, bool b5, bool b6, bool b7)
        {
            int iadd10 = 0;
            string sb0 = (b0 == true) ? "1" : "0";
            string sb1 = (b1 == true) ? "1" : "0";
            string sb2 = (b2 == true) ? "1" : "0";
            string sb3 = (b3 == true) ? "1" : "0";
            string sb4 = (b4 == true) ? "1" : "0";
            string sb5 = (b5 == true) ? "1" : "0";
            string sb6 = (b6 == true) ? "1" : "0";
            string sb7 = (b7 == true) ? "1" : "0";
            try
            {
                string stradd = sb7.Trim() + sb6.Trim() + sb5.Trim() + sb4.Trim() + sb3.Trim() + sb2.Trim() + sb1.Trim() + sb0.Trim();
                stradd = stradd.PadLeft(8, '0');
                iadd10 = Convert.ToInt32(stradd, 2);
            }
            catch (Exception)
            {
                //MessageBox.Show("二制数据转换十进制数据出错");
            }
            return iadd10;
        }
    }
}

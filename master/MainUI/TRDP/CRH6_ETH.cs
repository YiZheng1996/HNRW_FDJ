using RW.Driver;
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BogieIdling.UI.TRDP
{


    public static class CRH6_ETH
    {

      





        /// <summary>
        /// CCU 发BCU组播下标
        /// </summary>
        public static int CUU_BCU_index = 0;

        /// <summary>
        /// TCU2A 发出数据组播下标
        /// </summary>
        public static int TCU2A_BCU_index = 1;

        /// <summary>
        /// TCU4A 发出数据组播下标
        /// </summary>
        public static int TCU4A_BCU_index = 2;


        /// <summary>
        /// CCU发送给BCU数据 
        /// </summary>
        public static byte[] CCU_BCU = new byte[256];


        /// <summary>
        /// TCU发送给BCU数据
        /// </summary>
        public static byte[] TCU2A_BCU = new byte[512];

        /// <summary>
        /// TCU发送给BCU数据
        /// </summary>
        public static byte[] TCU2B_BCU = new byte[512];

        /// <summary>
        /// TCU发送给BCU数据
        /// </summary>
        public static byte[] TCU4A_BCU = new byte[512];

        /// <summary>
        /// TCU发送给BCU数据
        /// </summary>
        public static byte[] TCU4B_BCU = new byte[512];


        public struct ETH
        {

            #region  生命信号累加中断标志位


            private static bool _LifeInterrupt;

            /// <summary>
            /// 生命信号累加中断标志位
            /// </summary>
            public static bool LifeInterrupt
            {
                get { return _LifeInterrupt; }
                set
                {
                    
                    _LifeInterrupt = value;

                }

            }
            #endregion

            #region  运行指令有效

            private static bool _Run;

            /// <summary>
            /// 运行有效
            /// </summary>
            public static bool Run
            {
                get { return _Run; }
                set
                {
                    ConvertBoolToByte(ref CCU_BCU, 36, 0, value);
                    _Run = value;

                }

            }
            #endregion

            #region  清洁制动指令

            private static bool _CleanBrake;

            /// <summary>
            /// 清洁制动指令
            /// </summary>
            public static bool CleanBrake
            {
                get { return _CleanBrake; }
                set
                {
                    ConvertBoolToByte(ref CCU_BCU, 38, 0, value);
                    _CleanBrake = value;
                }

            }
            #endregion

            #region  制动级位



            public static bool N1
            {
                set { ConvertBoolToByte(ref CCU_BCU, 37, 0, value); }
            }

            public static bool N2
            {
                set { ConvertBoolToByte(ref CCU_BCU, 37, 1, value); }
            }

            public static bool N3
            {
                set { ConvertBoolToByte(ref CCU_BCU, 37, 2, value); }
            }

            public static bool N4
            {
                set { ConvertBoolToByte(ref CCU_BCU, 37, 3, value); }
            }

            public static bool N5
            {
                set { ConvertBoolToByte(ref CCU_BCU, 37, 4, value); }
            }

            public static bool N6
            {
                set { ConvertBoolToByte(ref CCU_BCU, 37, 5, value); }
            }

            public static bool N7
            {
                set { ConvertBoolToByte(ref CCU_BCU, 37, 6, value); }
            }

            private static int _BrakeGrade;

            public static int BrakeGrade
            {
                get { return _BrakeGrade; }
                set
                {

                    //byte by = CCU_BCU[37];

                    switch (value)
                    {
                        case 0:
                            ConvertBoolToByte(ref CCU_BCU, 37, 6, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 5, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 4, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 3, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 2, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 1, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 0, false);
                            break;
                        case 1:
                            ConvertBoolToByte(ref CCU_BCU, 37, 6, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 5, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 4, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 3, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 2, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 1, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 0, true);
                            break;
                        case 2:
                            ConvertBoolToByte(ref CCU_BCU, 37, 6, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 5, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 4, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 3, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 2, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 1, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 0, true);
                            break;
                        case 3:
                            ConvertBoolToByte(ref CCU_BCU, 37, 6, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 5, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 4, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 3, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 2, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 1, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 0, true);
                            break;
                        case 4:
                            ConvertBoolToByte(ref CCU_BCU, 37, 6, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 5, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 4, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 3, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 2, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 1, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 0, true);
                            break;
                        case 5:
                            ConvertBoolToByte(ref CCU_BCU, 37, 6, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 5, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 4, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 3, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 2, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 1, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 0, true);
                            break;
                        case 6:
                            ConvertBoolToByte(ref CCU_BCU, 37, 6, false);
                            ConvertBoolToByte(ref CCU_BCU, 37, 5, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 4, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 3, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 2, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 1, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 0, true);
                            break;
                        case 7:
                            ConvertBoolToByte(ref CCU_BCU, 37, 6, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 5, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 4, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 3, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 2, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 1, true);
                            ConvertBoolToByte(ref CCU_BCU, 37, 0, true);
                            break;

                        default:

                            break;

                    }

                    _BrakeGrade = value;
                }

            }


            #endregion

            #region  再生反馈力

            /// <summary>
            /// 
            /// </summary>
            private static double ed_brake;

            /// <summary>
            /// 再生反馈力  
            /// </summary>
            public static double ED_brake
            {
                get { return ed_brake; }
                set
                {

                    object bitvalue = value / 0.04882;
                    byte[] bts = BitConverter.GetBytes(Convert.ToUInt16(bitvalue));
                    Array.Reverse(bts);
                    

                    Array.Copy(bts, 0, TCU2A_BCU, 82, bts.Length);
                    Array.Copy(bts, 0, TCU2B_BCU, 82, bts.Length);
                    Array.Copy(bts, 0, TCU4B_BCU, 82, bts.Length);
                    Array.Copy(bts, 0, TCU4B_BCU, 82, bts.Length);


                    ed_brake = value;

                }

            }

            #endregion

            #region  3km/h以下速度信号

            private static bool _km3;


            public static bool Km3
            {
                get { return _km3; }
                set
                {
                    ConvertBoolToByte(ref CCU_BCU, 46, 7, value);
                    _km3 = value;
                }

            }


            #endregion
            
            #region  HB无效

            private static bool _hbwx;


            public static bool HBWX
            {
                get { return _hbwx; }
                set
                {
                    ConvertBoolToByte(ref CCU_BCU, 46, 5, value);
                    _hbwx = value;
                }

            }


            #endregion

            #region  HB缓解信号

            private static bool _hbhj;


            public static bool HBHJ
            {
                get { return _hbhj; }
                set
                {

                    ConvertBoolToByte(ref CCU_BCU, 46, 4, value);
                    _hbhj = value;
                }

            }


            #endregion


        }



        static void DataWrite(ref byte[] by, int Offset, int bitSrc, bool bl)
        {



            byte[] bts = null;

            if (bl)
            {

                ConvertBoolToByte(ref by, Offset, bitSrc, true);
            }
            else
            {

                ConvertBoolToByte(ref by, Offset, bitSrc, false);
            }


        }


        public static void ConvertBoolToByte(ref byte[] sendbyte, int byteIndex, int bitIndex, bool value)
        {
            byte bytevalue = sendbyte[byteIndex];

            bool[] barr = conversion2((int)bytevalue);
            barr[bitIndex] = value;

            bytevalue = (byte)conversion10(barr[0], barr[1], barr[2], barr[3], barr[4], barr[5], barr[6], barr[7]);
            sendbyte[byteIndex] = bytevalue;

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
                MessageBox.Show("十制数据转换二进制数据出错 \r\n" + ex.Message);
            }
            return barray;
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

                MessageBox.Show("二制数据转换十进制数据出错");
            }
            return iadd10;
        }















    }
}

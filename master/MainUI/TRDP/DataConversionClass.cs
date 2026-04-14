using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestFunction
{
    public class DataConversionClass
    {
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetorSignalSimulator.UI.Driver
{
    public static class CRC32Cls
    {
        public static uint[] Crc32Table;
        //生成CRC32码表
        public static void GetCRC32Table()
        {
            uint Crc;
            Crc32Table = new uint[256];
            int i, j;
            for (i = 0; i < 256; i++)
            {
                Crc = (uint)i;
                for (j = 8; j > 0; j--)
                {
                    if ((Crc & 1) == 1)
                        Crc = (Crc >> 1) ^ 0xEDB88320;
                    else
                        Crc >>= 1;
                }
                Crc32Table[i] = Crc;
            }
        }

        //获取字符串的CRC32校验值
        public static byte[] GetCRC32Str(byte[] buffer)
        {
            //byte[] buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(sInputString);
            uint value = 0xffffffff;
            int len = buffer.Length;
            for (int i = 0; i < len; i++)
            {
                value = (value >> 8) ^ Crc32Table[(value & 0xFF) ^ buffer[i]];
            }
            ;
            byte[] ret = BitConverter.GetBytes(value ^ 0xffffffff);

            return ret;
        }

    }
}

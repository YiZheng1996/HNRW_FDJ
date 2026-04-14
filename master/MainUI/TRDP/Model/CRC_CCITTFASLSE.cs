using System;
using System.Collections.Generic;

using System.Text;
using TRDP;

namespace BogieIdling.UI.Model
{
    public class CRC16_FALSEHelper
    {

        private const ushort polynomial = 0x1021;
        private ushort[] table = new ushort[256];
        private ushort initialValue = 0xFFFF;

        public CRC16_FALSEHelper()
        {
            GenerateTable();
        }

        private void GenerateTable()
        {
            for (ushort i = 0; i < table.Length; ++i)
            {
                ushort value = 0;
                ushort temp = (ushort)(i << 8);
                for (byte j = 0; j < 8; ++j)
                {
                    if (((value ^ temp) & 0x8000) != 0)
                    {
                        value = (ushort)((value << 1) ^ polynomial);
                    }
                    else
                    {
                        value <<= 1;
                    }
                    temp <<= 1;
                }
                table[i] = value;
            }
        }

        public ushort ComputeChecksum(byte[] bytes)
        {
           
            ushort crc = initialValue;
            foreach (byte b in bytes)
            {
                byte index = (byte)((crc >> 8) ^ b);
                crc = (ushort)((crc << 8) ^ table[index]);
            }
            return crc;
        }
        public byte[] ComputeChecksumBytes(byte[] bytes)
        {
            ushort crc = ComputeChecksum(bytes);
            return BitConverter.GetBytes(crc);
        }
        public byte[] CRC16(byte[] data, int fromIndex, int endIndex)
        {
            byte[] temp = new byte[endIndex - fromIndex];
            Array.Copy(data, fromIndex, temp, 0, endIndex - fromIndex);


            return ComputeChecksumBytes(temp);
        }
    }
}

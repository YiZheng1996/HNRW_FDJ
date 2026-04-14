using BogieIdling.UI.Model;
using System;
using System.Collections.Generic;

using System.Text;

namespace BogieIdling.UI.TRDP.Model
{
    /// <summary>
    /// 配置应答帧 : TRDP 模块 ----> MCU
    /// </summary>
    public class TRDPMainRecieve : BaseRecieveModel
    {

        public TRDPRecieveCommandTypes CommandType { get; set; }

        public int DataLength { get; set; }

        public int ModuleVersion { get; set; }

        public override void InitData(byte[] data)
        {
            if (data.Length != 8) throw new Exception("TRDP返回数据长度不正确");
            this.Start = data[0];
            this.CommandType = (TRDPRecieveCommandTypes)data[1];
            this.DataLength = BitConverter.ToInt16(new byte[] { data[3], data[2] }, 0);
            this.ModuleVersion = BitConverter.ToInt16(new byte[] { data[5], data[4] }, 0);
            this.CRC = new byte[2];
            CRC[0] = data[6];
            CRC[1] = data[7];
            if (!ValidCRC(data))
                throw new FormatException("CRC校验失败！");
        }
    }

    /// <summary>
    /// 配置应答类型
    /// </summary>
    public enum TRDPRecieveCommandTypes
    {
        None = 0,
        Eth0_Success = 0x06,
        Eth0_Error = 0x86,
        Eth1_Success = 0x05,
        Eth2_Error = 0x85,
    }
}

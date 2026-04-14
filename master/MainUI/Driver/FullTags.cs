using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data;

namespace MetorSignalSimulator.UI.Model
{

    [DebuggerDisplay("{ID},{Identity},{COMMData},{DataLabel}")]
    public class FullTags
    {
        public int ID { get; set; }
        public string DataLabel { get; set; }
        public string DataName { get; set; }
        public string DataType { get; set; }
        /// <summary>
        /// 用于倍数，采集数*0.1
        /// </summary>
        public decimal dataFormat { get; set; }

        /// <summary>
        /// 原始高
        /// </summary>
        public int RawHight { get; set; }

        /// <summary>
        /// 原始低
        /// </summary>
        public int RawLow { get; set; }

        /// <summary>
        /// 缩放高
        /// </summary>
        public int ScaledHight { get; set; }

        /// <summary>
        /// 缩放低
        /// </summary>
        public int ScaledLow { get; set; }

        public string DataRange { get; set; }
        public string DataUnit { get; set; }
        public string Description { get; set; }
        public string guzhangfenlei { get; set; }


        public COMMData COMMData { get; set; }

        public int Port { get { return COMMData.Port; } }
        public int Offset { get { return COMMData.Offset; } }
        public int Bit { get { return COMMData.Bit; } }
        public bool Identity { get; set; }

        public string TxType { get; set; }
        public int comID { get; set; }

        // 添加实时值属性
        public decimal RealTimeValue { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public bool IsValueUpdated { get; set; }

        // 添加原始字节数据（可选）
        public byte[] RawData { get; set; }

        // 为了方便访问，可以添加一个格式化后的实时值字符串
        public string RealTimeValueString
        {
            get
            {
                return $"{RealTimeValue} {DataUnit}";
            }
        }

        //public COMMData Ethernet { get; set; }

        //public int MVBPort { get; set; }
        //public int MVBOffset { get; set; }
        //public int MVBBit { get; set; }

        //public int EthernetPort { get; set; }
        //public int EthernetOffset { get; set; }
        //public int EthernetBit { get; set; }

        public override string ToString()
        {
            return string.Join(",", new string[] { this.ID.ToString(), this.Identity.ToString(), this.COMMData.ToString(), this.DataLabel.ToString() });
            //return base.ToString();
        }
    }
}

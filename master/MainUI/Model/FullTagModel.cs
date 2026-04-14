using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MainUI.Model
{


    public class FullTagModel
    {
        public FullTagModel()
        {

        }

        public FullTagModel(DataRow row)
        {
            Init(row);
        }

        private void Init(DataRow row)
        {
            //string sql = string.Format("select CANID as CANID,DataLabel as 信号名称,DataType as 数据类型,Offset as 字节偏移,BitStart as 位开始,BitEnd as 位结束,Description as 备注 FROM FullTags");
            编号 = Convert.ToInt32(row["编号"].ToString());
            CANID = row["CANID"].ToString();
            信号名称 = row["信号名称"].ToString();
            数据类型 = row["数据类型"].ToString();
            字节偏移 = Convert.ToInt32(row["字节偏移"].ToString());
            位开始 = Convert.ToInt32(row["位开始"].ToString());
            位结束 = Convert.ToInt32(row["位结束"].ToString());
            备注 = row["备注"].ToString();
        }


        public int 编号 { get; set; }

        public string 信号名称 { get; set; }   //利用miniExcel时，属性名称需要与Excel列名相同，才能自动转换为对象。或者利用Attributes特征修饰ExcelColumnNameAttribute

        public string 数据类型 { get; set; }
        public string CANID { get; set; }

        /// <summary>
        /// 10进制的CANID
        /// </summary>
        public uint PortNum { get { return Convert.ToUInt32(CANID, 16); } }

        public int 字节偏移 { get; set; }
        public int 位开始 { get; set; }
        public int 位结束 { get; set; }
        public string 是否生命信号 { get; set; } = "否";
        public string 备注 { get; set; }

        /// <summary>
        /// Ports 表的主键，FullTags 外键
        /// </summary>
        public int PortID { get; set; }

        public override string ToString()
        {
            string str = "";
            str = $"信号名称:{信号名称},数据类型:{数据类型},CANID:{CANID},字节偏移:{字节偏移},位开始:{位开始},位结束:{位结束},备注:{备注},";
            return str;
        }


    }
}

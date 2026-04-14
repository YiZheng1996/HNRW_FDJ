using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Model
{
    public class PortModel
    {
        public PortModel()
        {

        }

        public PortModel(DataRow row)
        {
            Init(row);
        }

       
        private void Init(DataRow row)
        {
            //        string sql = string.Format("select ID AS 序号, ProjectID AS 信号设备, PortName as 端口名称,Rate as 端口周期,CANID as CANID,DataSize as 端口大小,PortPattern as 源宿端口 FROM Ports");

            ID = row["序号"].ToString();
            信号设备 = row["信号设备"].ToString();
            端口名称 = row["端口名称"].ToString();
            端口周期 = row["端口周期"].ToString();
            CANID = row["CANID"].ToString();
            int sizePort = 8;
            int.TryParse(row["端口大小"].ToString(),out sizePort);
            端口大小 = sizePort;
            源宿端口 = row["源宿端口"].ToString();

        }


        //利用miniExcel时，属性名称需要与Excel列名相同，才能自动转换为对象。或者利用Attributes特征修饰ExcelColumnNameAttribute
        public string ID { get; set; }
        public string 信号设备 { get; set; }

        public string 端口名称 { get; set; }   

        public string CANID { get; set; }

        /// <summary>
        /// 10进制的CANID
        /// </summary>
        public uint PortNum { get { return Convert.ToUInt32(CANID, 16); } }


        public string 端口周期 { get; set; }
        public int 端口大小 { get; set; }
        public string 源宿端口 { get; set; }
        public bool IsRead1 { get { return 源宿端口.Contains("源") ? false : true; } }
        public string 端口类型 { get; set; } = "CAN";
      




        public override string ToString()
        {
            string str ="";
             str = $"端口名称:{端口名称},CANID:{CANID},端口周期:{端口周期},端口大小:{端口大小},源宿端口:{源宿端口}";
            return str;
        }


    }
}

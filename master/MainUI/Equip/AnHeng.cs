using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace MainUI.Equip
{
    /// <summary>
    /// 品牌:安衡电子秤 （AnHeng） 型号:CHS-D    通讯接口RS-232
    /// </summary>
    public class AnHeng
    {
        public AnHeng()
        {

        }

        /// <summary>
        /// 工位号 1：低速撒砂；2 高速撒砂
        /// </summary>
        public int GWNo { get; set; }
        public AnHeng(int gw)
        {
            GWNo = gw;
        }

        public SerialPort spCOM;

        /// <summary>
        /// 当前电子秤重量 单位g
        /// </summary>
        public double Weight { get; set; }

        public void Init()
        {
            spCOM.DataReceived -= SpCOM_DataReceived;
            spCOM.DataReceived += SpCOM_DataReceived;
        }

        private void SpCOM_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(100);
            //解析数据,具体格式咱时不确定
            //            通讯方式【2】
            //兼容A27格式，正向发送净重数据，一帧11组数据，换行结束：
            //例如：
            //毛重ww0023.45kg
            //净重wn0023.45kg
            //皮重wt0023.45kg，

            try
            {
                int len = spCOM.BytesToRead;
                byte[] receByte = new byte[len];
                spCOM.Read(receByte, 0, len);
                string str = Encoding.ASCII.GetString(receByte);
                double w = 0;
                bool convOK = false;
                if (str.Length == 11)
                {
                    string subStr = str.Substring(2, 7);
                    convOK = double.TryParse(subStr, out w);
                }
                if (convOK)
                {
                    Weight = w;
                }
                else
                    Weight = 0;
            }
            catch (Exception ex)
            {
                string err = "解析电子秤数据有误；原因：" + ex.Message;
                Var.LogInfo(err);
                Weight = 0;
            }

           
        }
    }
}

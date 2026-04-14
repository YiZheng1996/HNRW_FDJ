using System;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;

namespace MainUI.Equip
{
    public class TH2683B
    {
        public static SerialPort serialPort2516B;

        public static void TH2683B_Test(string sDC, string sTime)
        {
            if (!serialPort2516B.IsOpen)
            {
                serialPort2516B.Open();
            }
            System.Threading.Thread.Sleep(500);
            if (serialPort2516B.IsOpen)
            {
                System.Threading.Thread.Sleep(50);
                serialPort2516B.DiscardInBuffer();
                serialPort2516B.DiscardOutBuffer();
                System.Threading.Thread.Sleep(50);
                serialPort2516B.WriteLine("FUNCtion: MTIMe " + sTime); //持续时间
                serialPort2516B.WriteLine("FUNCtion:DTIMe 5");       //放电时间
                serialPort2516B.WriteLine("FUNCtion: CTIMe 5");      //充电时间
                serialPort2516B.WriteLine("FUNCtion:OVOL " + sDC);
                serialPort2516B.WriteLine("FUNCtion:MMOD SINGle");
                TH2683B_TestStart();

            }
        }
        public static void TH2683B_TestStart()
        {
            if (!serialPort2516B.IsOpen)
            {
                serialPort2516B.Open();
            }
            System.Threading.Thread.Sleep(500);
            if (serialPort2516B.IsOpen)
            {
                System.Threading.Thread.Sleep(50);
                serialPort2516B.DiscardInBuffer();
                serialPort2516B.DiscardOutBuffer();
                System.Threading.Thread.Sleep(50);
                serialPort2516B.WriteLine("TRIG");
            }
        }


        public static string TH2683B_ReadData()
        {
            try
            {
                string str = "";
                System.Threading.Thread.Sleep(10000);
                serialPort2516B.DiscardInBuffer();
                serialPort2516B.DiscardOutBuffer();
                System.Threading.Thread.Sleep(5000);
                serialPort2516B.WriteLine("FETC?");
                Thread.Sleep(1000);
                str = serialPort2516B.ReadExisting();
                serialPort2516B.WriteLine("FUNCtion: DTIMe 5.0");
                string[] err = str.Split(',');
                string sTemp = err[0];
                sTemp = sTemp.Trim();
                if (!string.IsNullOrEmpty(sTemp))
                {
                    float fTemp = Convert.ToSingle(sTemp);
                    fTemp = fTemp / 1000000;
                    return fTemp.ToString();
                }
                else
                {
                    return sTemp;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.WriteLine("TH2683通讯有误，" + ex.Message);
                return "TH2683通讯有误";
            }
        }
    }
}

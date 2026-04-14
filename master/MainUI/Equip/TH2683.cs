using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace MainUI.Equip
{
    public class TH2683
    {


        public static SerialPort serPortTH2683 = new SerialPort();


        /// <summary>
        /// 允许仪器发送数据，仪器开机默认为不发送，计算机收不到数据。
        /// </summary>
        public static void SendOpen()
        {
            string CurCmd = "<O>";
            PCWriteData(CurCmd);
        }

        /// <summary>
        /// 不允许仪器发送数据，计算机将接收不到数据，但计算机发送的控制命令有效。
        /// </summary>
        public static void SendClose()
        {
            string CurCmd = "<F>";
            PCWriteData(CurCmd);
        }

        /// <summary>
        /// 电阻下限值设定。
        /// <para>lowValue:4位数字整数</para>
        /// <para>dotIndex：小数点位置。1千位后。2百位后，3十位后，4个位后</para>
        /// <para>unit 单位。0，M。1，G。 （M=10^6, G=10^9）</para>
        /// </summary>
        /// <param name="lowValue"></param>
        /// <param name="dotIndex"></param>
        /// <param name="unit"></param>
        public static void LowLimit(int lowValue, int dotIndex, int unit)
        {
            string CurCmd = string.Format("<L{0}{1}{2}>", lowValue, dotIndex, unit);
            PCWriteData(CurCmd);
        }


        /// <summary>
        /// 电阻上限值设定。
        /// <para>lowValue:4位数字整数</para>
        /// <para>dotIndex：小数点位置。1千位后。2百位后，3十位后，4个位后</para>
        /// <para>unit 单位。0，M。1，G。 （M=10^6, G=10^9）</para>
        /// </summary>
        /// <param name="lowValue"></param>
        /// <param name="dotIndex"></param>
        /// <param name="unit"></param>
        public static void HighLimit(int highValue, int dotIndex, int unit)
        {
            string CurCmd = string.Format("<H{0}{1}{2}>", highValue, dotIndex, unit);
            PCWriteData(CurCmd);
        }


        /// <summary>
        /// 电压档位设置 (仅放电状态有效)
        /// <para>volRange:0-9 (10V / 25V / 50V / 75V / 100V/ 125V / 250V / 500V / 750V /1000V)</para>
        /// </summary>
        /// <param name="volRange"></param>
        public static void VRange(int volRange)
        {
            //电压档号0-9（10V-1000V）
            //10V / 25V / 50V / 75V / 100V/ 125V / 250V / 500V / 750V /1000V


            // 先把测试仪设置在放电状态
            Discharge();

            Thread.Sleep(100);

            if (volRange < 0)
                volRange = 0;
            if (volRange > 9)
                volRange = 9;

            //命令V  电压档改变  仅放电状态有效
            string CurCmd = string.Format("<V{0}>", volRange);

            PCWriteData(CurCmd);
        }


        /// <summary>
        /// 电阻量程
        /// </summary>
        /// <param name="Rrange"></param>
        public static void RRange(int Rrange)
        {

            if (Rrange < 1)
                Rrange = 1;
            if (Rrange > 6)
                Rrange = 6;

            string CurCmd = string.Format("<R{0}>", Rrange.ToString());
            PCWriteData(CurCmd);

        }


        /// <summary>
        /// 放电
        /// </summary>
        public static void Discharge()
        {
            string CurCmd = "<D>";
            PCWriteData(CurCmd);
            Thread.Sleep(100);
        }

        /// <summary>
        /// 清零，仅放电状态有效
        /// </summary>
        public static void Clear()
        {
            Discharge();
            string CurCmd = "<E>";
            PCWriteData(CurCmd);
        }

        /// <summary>
        /// 自动键，仅放电/测试状态有效 。当测量时仪器自动切换最佳的量程。
        /// </summary>
        public static void Auto()
        {
            Discharge();

            string CurCmd = "<A>";
            PCWriteData(CurCmd);
        }

        public static void Test()
        {
            string CurCmd = "<T>";
            PCWriteData(CurCmd);
            // 只有在测试状态下，实时读取数据。
            Thread.Sleep(2000);

            PCReadDataDZ();
        }

        /// <summary>
        /// 讯响设置
        /// <para>beepType:0,不合格。1,合格。2，关闭。</para>
        /// </summary>
        /// <param name="beepType"></param>
        public static void Beep(string beepType)
        {
            string CurCmd = string.Format("<B{0}>", beepType);
            PCWriteData(CurCmd);
        }



        /// <summary>
        /// 计算机写入数据到仪表 TH2683电阻测试仪
        /// </summary>
        /// <param name="dangwei"></param>
        private static void PCWriteData(string TEXT)
        {
            try
            {

                if (!serPortTH2683.IsOpen)
                {
                    serPortTH2683.Open();
                }
                serPortTH2683.DiscardInBuffer();
                serPortTH2683.DiscardOutBuffer();

                serPortTH2683.Write(TEXT);

                Thread.Sleep(200);

                //PCReadDataDZ();
            }
            catch (Exception ex)
            {
                MessageBox.Show("TH2683通讯有误，" + ex.Message);
                Debug.WriteLine("TH2683通讯有误，" + ex.Message);
            }

        }



        /// <summary>
        /// 计算机读取数据，从仪表 读取2683的电阻
        /// </summary>
        public static string PCReadDataDZ()
        {

            try
            {
                string str = "";

                serPortTH2683.DiscardInBuffer();
                serPortTH2683.DiscardOutBuffer();
                //serPort1.Write("<T>");
                //Thread.Sleep(300);

                serPortTH2683.Write("<O>");//命令O  允许仪器发送数据，仪器开机默认为不发送,计算机收不到数据
                Thread.Sleep(1000);
                str = serPortTH2683.ReadExisting();
                // str="<T0000000.015n110162100.0M::::.G><T0000000.018n110162100.0M::::.G><T0000000.017n110162100.0M::::.G><T0000000.017n110162100.0M::::.G><T0000000.011n110162100.0M::::.G>"
                //serPort1.Write("<F>");//命令F  不允许仪器发送数据，以后计算机将接收不到数据，但计算机发送的控制有效

                if (str.Length >= 33)
                {
                    string result = AnalysisDataTH2683A(str);
                    //  txtDianya.Text = result;



                    //txtDianzu.Text = CurDianZu + "Ω";
                    //txtDianliu.Text = CurDianLiu + "A";
                    //lbDZYState.Text = CurType;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.WriteLine("TH2683通讯有误，" + ex.Message);
            }
            return CurRrst;
        }


        /// <summary>
        /// 测试仪当前状态
        /// </summary>
        public static string CurType = "";

        /// <summary>
        /// 测试状态下，当前电阻
        /// </summary>
        public static string CurRrst = "∞";

        /// <summary>
        /// 测试状态下，当前电流
        /// </summary>
        public static string CurIrst = "0";

        public static string CurVol = "0";

        private static string AnalysisDataTH2683A(string txt)
        {
            string rst = "";
            if (txt.Length < 33)
            {
                rst = "读取的返回值不符合规范";
                return rst;
            }
            string txt33 = txt.Substring(0, 33);

            // T测试中， D放电状态， S设置状态， E清零状态， J仪器启动时发送给计算机的同步信号(该功能不受命令O控)
            string functionType = txt33.Substring(1, 1);
            string voltage = "0";
            string dwNo = txt33.Substring(19, 1);
            voltage = GetVoltageByNO(dwNo);
            CurVol = voltage;
            switch (functionType)
            {
                case "T":
                    CurType = "测试状态";
                    CurRrst = txt33.Substring(2, 6); // 读取出来后，需要换算单位 ？？？
                    if (CurRrst.Substring(5, 1) == "G")
                    {
                        CurRrst = ((Convert.ToDouble(txt33.Substring(2, 5))) * 1000.0).ToString();
                    }

                    CurIrst = txt33.Substring(8, 6);
                    if (CurRrst == "000000")
                        CurRrst = "∞";

                    break;
                case "D":
                    CurType = "放电状态";
                    break;
                case "S":
                    CurType = "设置状态";
                    break;
                case "E":
                    CurType = "清零状态";
                    break;
                case "J":
                    CurType = "启动状态";
                    break;
            }
            return CurRrst;
        }



        /// <summary>
        ///  电压档号0-9（10V-1000V）10V / 25V / 50V / 75V / 100V/ 125V / 250V / 500V / 750V /1000V
        /// </summary>
        /// <param name="dangwei"></param>
        /// <returns></returns>
        private static string GetVoltageByNO(string dangweiNo)
        {
            string rst = "10";
            switch (dangweiNo)
            {
                #region 10个档位
                case "0":
                    rst = "10";
                    break;
                case "1":
                    rst = "25";
                    break;
                case "2":
                    rst = "50";
                    break;
                case "3":
                    rst = "75";
                    break;
                case "4":
                    rst = "100";
                    break;
                case "5":
                    rst = "125";
                    break;
                case "6":
                    rst = "250";
                    break;
                case "7":
                    rst = "500";
                    break;
                case "8":
                    rst = "750";
                    break;
                case "9":
                    rst = "1000";
                    break;
                    #endregion
            }

            return rst;
        }





    }
}

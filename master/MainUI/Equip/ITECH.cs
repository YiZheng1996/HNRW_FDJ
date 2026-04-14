using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;


namespace MainUI.Equip
{

    /// <summary>
    /// 艾德克斯电源
    /// </summary>
    public class ITECH
    {
        static SerialPort serialPortVolt = null;
        public ITECH()
        { }

        public ITECH(SerialPort sport)
        {
            serialPortVolt = sport;
        }

        public static void Init(SerialPort sport)
        {
            serialPortVolt = sport;
        }

        public static int VoltZero()
        {
            try
            {
               
                if (serialPortVolt == null)
                    return 0;
                // 爱德克斯IT6953A，可调电源输出
                if (serialPortVolt.IsOpen == false)
                    serialPortVolt.Open();
                System.Threading.Thread.Sleep(50);
                if (serialPortVolt.IsOpen == false)
                    return 0;
                serialPortVolt.DiscardInBuffer();

                serialPortVolt.WriteLine("SYSTem:REMote\r");
                System.Threading.Thread.Sleep(50);
                serialPortVolt.WriteLine("VOLTage 0\r");
                System.Threading.Thread.Sleep(50);
                serialPortVolt.WriteLine("OUTPut Off\r");
                Var.LogInfo("输出电压V：0V");
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Var.LogInfo("输出电压错误：" + err);
                return 0;
            }
            return 1;
        }


        /// <summary>
        /// 可调电源输出 0-150V 
        /// </summary>
        /// <param name="VoltOutput"></param>
        /// <returns></returns>
        public static int VoltOut(double VoltOutput)
        {
            try
            {
                if (VoltOutput < 0.0)
                    VoltOutput = 0.0d;
                if (VoltOutput > 150.0)
                    VoltOutput = 150.0d;
                if (serialPortVolt == null)
                    return 0;
                // 爱德克斯IT6953A，可调电源输出
                if (serialPortVolt.IsOpen == false)
                    serialPortVolt.Open();
                System.Threading.Thread.Sleep(50);
                if (serialPortVolt.IsOpen == false)
                    return 0;
                serialPortVolt.DiscardInBuffer();

                serialPortVolt.WriteLine("SYSTem:REMote\r");
                System.Threading.Thread.Sleep(50);
                serialPortVolt.WriteLine("VOLTage " + VoltOutput.ToString() + "\r");
                System.Threading.Thread.Sleep(50);
                serialPortVolt.WriteLine("OUTPut ON\r");
                Var.LogInfo("输出电压V：" + VoltOutput.ToString());
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Var.LogInfo("输出电压错误：" + err);
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 软件控制
        /// </summary>
        public static void RemoteControl()
        {
            //该命令用来通过 RS232 接口设置电源为远程控制模式。前面板上除了 Local 键，其他的键都被锁定不能使用。
            if (serialPortVolt == null)
                return;
            // 爱德克斯IT6953A，可调电源输出
            if (serialPortVolt.IsOpen == false)
                serialPortVolt.Open();
            System.Threading.Thread.Sleep(50);
            if (serialPortVolt.IsOpen == false)
                return;
            serialPortVolt.WriteLine("SYSTem:REMote\r");

        }

        /// <summary>
        /// 电源面板控制
        /// </summary>
        public static void LocalControl()
        {
            //该命令用来通过 RS232 接口设置电源为面板控制模式。执行该命令后前面板上所有的按键都将可用。
            if (serialPortVolt == null)
                return;
            // 爱德克斯IT6953A，可调电源输出
            if (serialPortVolt.IsOpen == false)
                serialPortVolt.Open();
            System.Threading.Thread.Sleep(50);
            if (serialPortVolt.IsOpen == false)
                return;
            serialPortVolt.WriteLine("SYSTem:LOCal\r");
        }

        /// <summary>
        /// 清除错误信息。
        /// </summary>
        public static void ClearError()
        {

            if (serialPortVolt == null)
                return;
            // 爱德克斯IT6953A，可调电源输出
            if (serialPortVolt.IsOpen == false)
                serialPortVolt.Open();
            System.Threading.Thread.Sleep(50);
            if (serialPortVolt.IsOpen == false)
                return;
            //《IT6900 Programming Guide-CN.pdf》第三章，系统命令
            //错误队列里最多可以存储 20 组错误信息。发送一次该命令从错误队列中读取一条错误信息。
            for (int i = 0; i < 20; i++)
            {
                serialPortVolt.WriteLine("SYSTem:ERRor?\r");
            }


        }

        /// <summary>
        /// 脉冲输出
        /// </summary>
        public class PWMout
        {
            private static bool _PWM0start = false;
            /// <summary>
            /// 脉冲0启动输出
            /// </summary>
            public static bool PWMstart0
            {
                get
                {
                    return _PWM0start;
                }
                set
                {
                    _PWM0start = value;
                    //if (value)
                    //    RWOPC.OpcClass.Write_String(RWOPC.OpcObject.Pub.PWM0start, "1");
                    //else
                    //    RWOPC.OpcClass.Write_String(RWOPC.OpcObject.Pub.PWM0start, "0");

                }
            }


            private static bool _PWM1start = false;
            /// <summary>
            /// 脉冲1启动输出
            /// </summary>
            public static bool PWMstart1
            {
                get
                {
                    return _PWM1start;
                }
                set
                {
                    _PWM1start = value;

                    //if (value)
                    //    RWOPC.OpcClass.Write_String(RWOPC.OpcObject.Pub.PWM1start, "1");
                    //else
                    //    RWOPC.OpcClass.Write_String(RWOPC.OpcObject.Pub.PWM1start, "0");

                }
            }

            private static double _HZ0 = 500.0;
            /// <summary>
            /// 频率0
            /// </summary>
            public static double HZ0
            {
                get { return _HZ0; }
                set
                {

                    _HZ0 = value;

                    double tmp1 = 1 / _HZ0;
                    Int32 tmpZQ = Convert.ToInt32(tmp1 * 1000000);

                    //if (RWOPC.OpcObject.Pub.Hz0 != null)
                    //{
                    //    RWOPC.OpcClass.Write_String(RWOPC.OpcObject.Pub.Hz0, tmpZQ.ToString());
                    //    System.Threading.Thread.Sleep(200);
                    //}
                }
            }

            private static double _HZ1 = 500.0;
            /// <summary>
            /// 频率1
            /// </summary>
            public static double HZ1
            {
                get { return _HZ1; }
                set
                {
                    _HZ1 = value;
                    double tmp1 = 1 / _HZ1;
                    Int32 tmpZQ = Convert.ToInt32(tmp1 * 1000000);

                    //if (RWOPC.OpcObject.Pub.Hz1 != null)
                    //{
                    //    RWOPC.OpcClass.Write_String(RWOPC.OpcObject.Pub.Hz1, tmpZQ.ToString());
                    //    System.Threading.Thread.Sleep(200);

                    //}
                }
            }


            private static double _pulse0 = 0.0;
            /// <summary>
            /// 占空比0；（0-100）
            /// </summary>
            public static double Pulse0
            {

                get { return _pulse0; }
                set
                {
                    // 哈萨克斯坦项目装置，占空比与制动级位的换算关系：
                    // 制动级位0%- 100% 对应 占空比10 - 90。 
                    //占空比 = 0.8 * 制动级位 + 10  。如制动级位 10 % ，占空比= 0.8 * 10 + 10 = 18 。
                    // 南延线是否需要换算，待定

                    _pulse0 = value;

                    //double tmp0 = Convert.ToDouble(RWOPC.OpcObject.Pub.Hz0.Value.ToString()) * _pulse0 / 100;
                    //Int16 tmp1 = Convert.ToInt16(tmp0);

                    //RWOPC.OpcClass.Write_String(RWOPC.OpcObject.Pub.pulse0, tmp1.ToString());
                }
            }

            private static double _pulse1 = 0.0;
            /// <summary>
            /// 占空比1；（0-100）
            /// </summary>
            public static double Pulse1
            {
                get { return _pulse1; }
                set
                {
                    _pulse1 = value;

                    //double tmp0 = Convert.ToDouble(RWOPC.OpcObject.Pub.Hz1.Value.ToString()) * _pulse1 / 100;
                    //Int16 tmp1 = Convert.ToInt16(tmp0);

                    //RWOPC.OpcClass.Write_String(RWOPC.OpcObject.Pub.pulse1, tmp1.ToString());

                }
            }
        }







    }


}

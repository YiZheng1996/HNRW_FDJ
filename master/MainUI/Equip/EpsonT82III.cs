using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Equip
{
    /// <summary>
    /// 爱普生TM-T82III 微型热敏打印机
    /// </summary>
    public class EpsonT82III
    {
        /*
        本类内容依据爱普生官网提供的ESC/P 指令，根据实际需求摘取部分常用指令经过多日调试、测试，精心研制而成；
        ESCP: Epson Standard Code for Printer；
        是EPSON公司自己制定的针式打印机的标准化指令集，现在已成为针式打印机控制语言事实上的工业标准。ESC/POS打印命令集是ESC打印控制命令的简化版本，现在大多数票据打印都采用ESC/POS指令集。其显著特征是：其中很大一部分指令都是以ESC控制符开始的一串代码
        https://download4.epson.biz/sec_pubs/pos/reference_en/escpos/tmt82iii.html
          TM-T82III 出厂IP地址为192.168.192.168；  端口 9100；
            
            自动打印打印机信息方法：
            1、先关闭电源；2、摁住顶部右侧的圆形按钮不松开；3、打开电源按钮；4、自动打印出信息；
        */

        public EpsonT82III()
        {

        }
        TcpClient tcp = null;
        NetworkStream stream = null;

        /// <summary>
        /// 初始化打印机
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void Init(string ip,int port)
        {
            try
            {
                tcp = new TcpClient(ip, port);
                stream = tcp.GetStream();
                byte[] initCmd = Encoding.ASCII.GetBytes(new char[] { (char)0x1b, '@' });
                stream.Write(initCmd, 0, initCmd.Length);
               // MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                string err = "打印机初始化有误；原因：" + ex.Message;
                MessageBox.Show(err);
            }
        }

        public void Close()
        {
            if (stream != null)
            {
                stream.Close();
                stream.Dispose();
            }
            if (tcp != null)
            {
                tcp.Close();
            }
        }

        public void SendInitCMD()
        {
            stream = tcp.GetStream();
            byte[] initCmd = Encoding.ASCII.GetBytes(new char[] { (char)0x1b, '@' });
            stream.Write(initCmd, 0, initCmd.Length);
        }


        /// <summary>
        /// 打印内容 不换行
        /// </summary>
        /// <param name="txt"></param>
        public void Print(string txt)
        {
            byte[] txtPrint = Encoding.Default.GetBytes(txt);
            stream.Write(txtPrint, 0, txtPrint.Length);
        }

        /// <summary>
        /// 打印内容 并换行
        /// </summary>
        /// <param name="txt"></param>
        public void PrintNewline(string txt)
        {
            byte[] txtPrint = Encoding.Default.GetBytes(txt); 
            stream.Write(txtPrint, 0, txtPrint.Length);
            NewLine();
        }

        /// <summary>
        /// 换行
        /// </summary>
        public void NewLine()
        {
            byte[] newline = Encoding.ASCII.GetBytes(new char[] { (char)0x0A });
            stream.Write(newline, 0, newline.Length);
        }

        /// <summary>
        /// 切纸
        /// </summary>
        public void CutPapar()
        {
            PrintNewline("");
            PrintNewline("");
            PrintNewline("");
            PrintNewline("");
            byte[] cutPaper = Encoding.Default.GetBytes(new char[] { (char)0x1B, 'm', (char)0x01 });
            stream.Write(cutPaper, 0, cutPaper.Length);
        }

        /// <summary>
        /// 设置水平制表符的位置
        /// </summary>
        /// <param name="pos"></param>
        public void SetHorTabPos(byte pos)
        {
            // //Set horizontal tab positions: 35th column，设置水平制表符的位置 ESC D  nL nH
            byte[] txtPrint = new byte[] { 0x1B, 0x44, pos, 0 };
            stream.Write(txtPrint, 0, txtPrint.Length);
        }

        /// <summary>
        /// 整体，设置左边距
        /// </summary>
        /// <param name="leftMargin"></param>
        public void SetLeftMargin_All(byte leftMargin)
        {
            // //整体，设置左边位置 GS L nL nH
            byte[] txtPrint = new byte[] { 0x1D, 0x4C, leftMargin, 0 };
            stream.Write(txtPrint, 0, txtPrint.Length);
        }

        /// <summary>
        /// 每行，设置左边位置
        /// </summary>
        /// <param name="leftMargin"></param>
        public void SetLeftMargin_OneLine(byte leftMargin)
        {
            // 每行，设置左边位置  ESC $  nL nH
            byte[] txtPrint = new byte[] { 0x1B, 0x24, leftMargin, 0 };
            stream.Write(txtPrint, 0, txtPrint.Length);
        }

        /// <summary>
        /// 设置下划线
        /// </summary>
        public void SetUnderLine()
        {
            // //FS -  设置下划线
            byte[] txtPrint = new byte[] { 0x1C, 0x2D, 0x2 };
            stream.Write(txtPrint, 0, txtPrint.Length);
        }

        /// <summary>
        /// 取消下划线
        /// </summary>
        public void CancelUnderLine()
        {
            // //FS -  取消设置下划线
            byte[] txtPrint = new byte[] { 0x1C, 0x2D, 0 };
            stream.Write(txtPrint, 0, txtPrint.Length);
        }


        //public static byte[] ALIGN_LEFT = { ESC, 0x61, 0x00 };
        //public static byte[] ALIGN_CENTER = { ESC, 0x61, 0x01 };
        //public static byte[] ALIGN_RIGHT = { ESC, 0x61, 0x02 };


        /// <summary>
        /// 左对齐
        /// </summary>
        public void AlignLeft()
        {
            //左对齐
            byte[] txtPrint = new byte[] { 0x1B, 0x61, 0x00 };
            stream.Write(txtPrint, 0, txtPrint.Length);
        }

        /// <summary>
        /// 居中
        /// </summary>
        public void AlignCenter()
        {
            //居中
            byte[] txtPrint = new byte[] { 0x1B, 0x61, 0x01 };
            stream.Write(txtPrint, 0, txtPrint.Length);
        }

        /// <summary>
        /// 右对齐
        /// </summary>
        public void AlignRight()
        {
            //居中
            byte[] txtPrint = new byte[] { 0x1B, 0x61, 0x02 };
            stream.Write(txtPrint, 0, txtPrint.Length);
        }

        /// <summary>
        /// 小字体A
        /// </summary>
        public void Font_A()
        {
            byte[] cmd = new byte[] { 0x1C, 0x21, 0x00 };
            stream.Write(cmd, 0, cmd.Length);
        }

        /// <summary>
        /// 小字体B
        /// </summary>
        public void Font_B()
        {
            byte[] cmd = new byte[] { 0x1C, 0x21, 0x01 };
            stream.Write(cmd, 0, cmd.Length);
        }
      
        /// <summary>
        /// 大字体,整体有效
        /// </summary>
        public void FONT_GS01()
        {
            byte[] cmd = new byte[] { 0x1D, 0x21, 0x01 };
            stream.Write(cmd, 0, cmd.Length);
        }

        /// <summary>
         /// 输出水平制表（横向列表）
        /// </summary>
        public void HT_NEXT()
        {
            byte[] cmd = new byte[] { 9 };
            stream.Write(cmd, 0, cmd.Length);
        }

        //  epson.SetCmd(new byte[] { 0x1B, 0x33, 16 }); // ESC 3  n ; Set line spacing
        //epson.SetCmd(new byte[] { 0x1B, 0x32 }); // ESC 2  ; select default line spacing
        // epson.SetCmd(new byte[] { 0x1B, 0x21, 0x38 }); //加粗
        // epson.SetCmd(new byte[] { 0x1B, 0x21, 0 }); //不加粗
        // epson.SetCmd(new byte[]  { FS, 0x53, 0x0, 0x0 }); //整体字间距

            /// <summary>
            /// 提交打印
            /// </summary>
        public void Commit()
        {
            CutPapar();
            stream.Flush(); //提交打印；
        }
    }
}

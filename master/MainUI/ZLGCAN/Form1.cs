using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZLGCAN;

namespace ZLGCAN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const int NULL = 0;
        IntPtr device_handle_;
        IntPtr channel_handle_;
        recvdatathread recv_data_thread_;
        List<string> list_box_data_ = new List<string>();
        bool m_bOpen = false;
        bool m_bInit = false;

        bool m_bStart = false;

        /* CAN payload length and DLC definitions according to ISO 11898-1 */
        const int CAN_MAX_DLC = 8;
        const int CAN_MAX_DLEN = 8;

        /* CAN FD payload length and DLC definitions according to ISO 11898-7 */
        const int CANFD_MAX_DLC = 15;
        const int CANFD_MAX_DLEN = 64;

        uint[] kBaudrate =
     {
            1000000,//1000kbps
            800000,//800kbps
            500000,//500kbps
            250000,//250kbps
            125000,//125kbps
            100000,//100kbps
            50000,//50kbps
            20000,//20kbps
            10000,//10kbps
            5000 //5kbps
        };

        /// <summary>
        /// CAN设备类型
        /// </summary>
        uint Device_type = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            //zhuanlan.zhihu.com/p/538834760?utm_id=0 CAN通信讲解

            Device_type = Define.ZCAN_USBCAN2;
            comboBox_index.SelectedIndex = 0;
            comboBox_channel.SelectedIndex = 0;
            comboBox_baud.SelectedIndex = 3;
            comboBox_frametype.SelectedIndex = 0;

            try
            {
                //button_open_Click(null, null);
                //button_init_Click(null, null);
                //button_start_Click(null, null);
            }
            catch (Exception ex)
            {
                string err = "打开，初始化，启动USBCAN设备。其中步骤有误；原因：" + ex.Message;
                MessageBox.Show(err);
            }

        }

        int channel_index_;
        private void comboBox_channel_SelectedIndexChanged(object sender, EventArgs e)
        {
            channel_index_ = comboBox_channel.SelectedIndex;
        }

        private void button_open_Click(object sender, EventArgs e)
        {
            device_handle_ = Method.ZCAN_OpenDevice(Device_type, 0, 0);
            if (NULL == (int)device_handle_)
            {
                MessageBox.Show("打开设备失败,请检查设备类型和设备索引号是否正确", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                m_bOpen = true;
            }
           
        }

        private void button_init_Click(object sender, EventArgs e)
        {
            if (!m_bOpen)
            {
                MessageBox.Show("设备还没打开", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            uint bdrate = kBaudrate[comboBox_baud.SelectedIndex];
            bool bdOK = Method.SetBaudrate(device_handle_, channel_index_, bdrate);
            if (bdOK == false)
            {
                MessageBox.Show("设置波特率失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            ZCAN_CHANNEL_INIT_CONFIG config_ = new ZCAN_CHANNEL_INIT_CONFIG();
            config_.can_type = Define.TYPE_CAN;
            config_.can.filter = 0;
            config_.can.acc_code = 0;
            config_.can.acc_mask = 0xFFFFFFFF;
            config_.can.mode = 0;
            IntPtr pConfig = Marshal.AllocHGlobal(Marshal.SizeOf(config_));
            Marshal.StructureToPtr(config_, pConfig, true);
            channel_handle_ = Method.ZCAN_InitCAN(device_handle_, (uint)channel_index_, pConfig);
            Marshal.FreeHGlobal(pConfig);

            //Marshal.FreeHGlobal(ptr);

            if (NULL == (int)channel_handle_)
            {
                MessageBox.Show("初始化CAN失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                m_bInit = true;
            }

        }


        private void button_start_Click(object sender, EventArgs e)
        {
            if (!m_bInit)
            {
                MessageBox.Show("设备还没初始化", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (Method.ZCAN_StartCAN(channel_handle_) != Define.STATUS_OK)
            {
                MessageBox.Show("启动CAN失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            btn_start.Enabled = false;
            m_bStart = true;
            if (null == recv_data_thread_)
            {
                recv_data_thread_ = new recvdatathread();
                recv_data_thread_.setChannelHandle(channel_handle_);
                recv_data_thread_.setStart(m_bStart);
                recv_data_thread_.RecvCANData += this.AddData;

            }
            else
            {
                recv_data_thread_.setChannelHandle(channel_handle_);
            }
        }

        static object lock_obj = new object();

        private void AddData(ZCAN_Receive_Data[] data, uint len)
        {
            string text = "";
            for (uint i = 0; i < len; ++i)
            {
                ZCAN_Receive_Data can = data[i];
                uint id = data[i].frame.can_id;
                string eff = Method.IsEFF(id) ? "扩展帧" : "标准帧";
                string rtr = Method.IsRTR(id) ? "远程帧" : "数据帧";
                text = String.Format("接收到CAN ID:0x{0:X8} {1:G} {2:G} 长度:{3:D1} 数据:", Method.GetId(id), eff, rtr, can.frame.can_dlc);

                for (uint j = 0; j < can.frame.can_dlc; ++j)
                {
                    text = String.Format("{0:G}{1:X2} ", text, can.frame.data[j]);
                }
                lock (lock_obj)
                {
                    list_box_data_.Add(text);

                    //listBox.Items.Add(text); // 线程间操作无效: 从不是创建控件“listBox”的线程访问它。
                }
            }

            //必须利用委托给主线程的控件赋值
            Object[] list = { this, System.EventArgs.Empty };
           // this.listBox.BeginInvoke(new EventHandler(SetListBox),null);
           // this.listBox.BeginInvoke(new Del(SetListBox222), null);
            this.BeginInvoke(new Del(SetListBox222));
        }

        public delegate void Del();
        private void SetListBox222()
        {
            int index = 0;
            lock (lock_obj)
            {
                foreach (string text in list_box_data_)
                {
                    index = listBox.Items.Add(text);
                }
                list_box_data_.Clear();
            }
            listBox.SelectedIndex = index;
        }

        private void SetListBox(object sender, EventArgs e)
        {
            int index = 0;
            lock (lock_obj)
            {
                foreach (string text in list_box_data_)
                {
                    index = listBox.Items.Add(text);
                }
                list_box_data_.Clear();
            }
            listBox.SelectedIndex = index;
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            bool ok = Method.Reset(device_handle_, channel_handle_, channel_index_);
            if (ok == false)
            {
                MessageBox.Show("复位失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("复位成功。", "提示",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button_close_Click(object sender, EventArgs e)
        {
            Method.ZCAN_CloseDevice(device_handle_);
            m_bOpen = false;
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_senddata.Text.Length == 0)
                {
                    MessageBox.Show("数据为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                uint id = (uint)System.Convert.ToInt32(textBox_ID.Text, 16);
                string data = textBox_senddata.Text;
                int frame_type_index = comboBox_frametype.SelectedIndex; //0 标准帧，1 扩展帧
                int protocol_index = 0; //0 CAN;1 CANFD;
                uint result; //发送的帧数
                ZCAN_Transmit_Data can_data = new ZCAN_Transmit_Data();
                can_data.frame.can_id = Method.MakeCanId(id, frame_type_index, 0, 0);
                can_data.frame.data = new byte[8];
                can_data.frame.can_dlc = (byte)SplitData(data, ref can_data.frame.data, CAN_MAX_DLEN);
                can_data.transmit_type = (uint)0; // 0  正常发送；1 单次发送；2 自发自收；3 单次自发自收；
                IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(can_data));
                Marshal.StructureToPtr(can_data, ptr, true);
                result = Method.ZCAN_Transmit(channel_handle_, ptr, 1);
                Marshal.FreeHGlobal(ptr);
            }
            catch (Exception ex)
            {
                string err = "发送数据有误；原因：" + ex.Message;
                MessageBox.Show(err);
            }
        }

        //拆分text到发送data数组
        private int SplitData(string data, ref byte[] transData, int maxLen)
        {
            string[] dataArray = data.Split(' ');
            for (int i = 0; (i < maxLen) && (i < dataArray.Length); i++)
            {
                transData[i] = Convert.ToByte(dataArray[i].Substring(0, 2), 16);
            }

            return dataArray.Length;
        }
    }
}

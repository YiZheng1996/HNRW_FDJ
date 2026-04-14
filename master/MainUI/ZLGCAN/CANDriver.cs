using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainUI;
using System.Threading;

namespace ZLGCAN
{

    /// <summary>
    /// 周立功CAN 驱动类
    /// </summary>
    public class CANDriver
    {
        public static CANDriver Instance = new CANDriver(0);
        public static CANDriver Instance2 = new CANDriver(1);

        public uint DeviceIndex { get; set; }
        public CANDriver(uint deviceIndex)
        {
            DeviceIndex = deviceIndex;
        }

        const int NULL = 0;
        IntPtr device_handle_;
        IntPtr channel_handle_1;
        IntPtr channel_handle_2;

        bool m_bOpen = false;
        bool m_bInit = false;

        bool m_bStart = false;

        /// <summary>
        /// CAN设备类型
        /// </summary>
        uint Device_type = 0;


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


        public int Can_Conn()
        {

            bool open = Can_Open();

            Thread.Sleep(500);
            bool init = Can_Init(0);
            init = Can_Init(1);

            Thread.Sleep(500);
            bool start = CAN_Start1();

            start = CAN_Start2();

            if (open & init & start)
                return 1;
            if (!open)
                return -1;
            else if (!init)
                return -2;
            else if (!start)
                return -3;
            else
                return 1;


        }


        private bool Can_Open()
        {
            Device_type = Define.ZCAN_USBCAN2;

            //device_handle_ = Method.ZCAN_OpenDevice(Device_type, 0, 0);
            device_handle_ = Method.ZCAN_OpenDevice(Device_type, DeviceIndex, 0);

            if (NULL == (int)device_handle_)
            {
                m_bOpen = false;
                MessageBox.Show("打开设备失败,请检查设备类型和设备索引号是否正确", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                m_bOpen = true;
            }
            return m_bOpen;
        }


        int channel_index_ = 1;

        /// <summary>
        /// chIndex 0或1
        /// </summary>
        /// <param name="chIndex"></param>
        /// <returns></returns>
        private bool Can_Init(int chIndex = 0)
        {
            if (!m_bOpen)
            {
                MessageBox.Show("设备还没打开", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return false;
            }
            uint bdrate = kBaudrate[3];//索引3, 250kbps
                                       // bool bdOK = Method.SetBaudrate(device_handle_, channel_index_, bdrate);
            bool bdOK = Method.SetBaudrate(device_handle_, chIndex, bdrate);
            if (bdOK == false)
            {
                MessageBox.Show("通道" + chIndex + "设置波特率失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            ZCAN_CHANNEL_INIT_CONFIG config_ = new ZCAN_CHANNEL_INIT_CONFIG();
            config_.can_type = Define.TYPE_CAN;
            config_.can.filter = 0;
            config_.can.acc_code = 0;
            config_.can.acc_mask = 0xFFFFFFFF;
            config_.can.mode = 0;
            IntPtr pConfig = Marshal.AllocHGlobal(Marshal.SizeOf(config_));
            Marshal.StructureToPtr(config_, pConfig, true);

            if (chIndex == 0)
                channel_handle_1 = Method.ZCAN_InitCAN(device_handle_, (uint)0, pConfig);
            else
                channel_handle_2 = Method.ZCAN_InitCAN(device_handle_, (uint)1, pConfig);

            Marshal.FreeHGlobal(pConfig);

            //Marshal.FreeHGlobal(ptr);

            if (NULL == (int)channel_handle_1)
            {
                MessageBox.Show("初始化CAN失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                m_bInit = false;
            }
            else
            {
                m_bInit = true;
            }


            return m_bInit;
        }


        public recvdatathread recv_data_thread_1;
        private bool CAN_Start1()
        {
            if (!m_bInit)
            {
                MessageBox.Show("设备还没初始化", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (Method.ZCAN_StartCAN(channel_handle_1) != Define.STATUS_OK)
            {
                MessageBox.Show("启动CAN失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }


            m_bStart = true;
            if (null == recv_data_thread_1)
            {
                recv_data_thread_1 = new recvdatathread();
                recv_data_thread_1.setChannelHandle(channel_handle_1);
                recv_data_thread_1.setStart(m_bStart);
                recv_data_thread_1.RecvCANData += this.AddData1;
            }
            else
            {
                recv_data_thread_1.setChannelHandle(channel_handle_1);
            }

            return m_bStart;
        }


        static object lock_obj = new object();

        public delegate void CanReceiveHandler(uint CanID, byte[] dataReceive);

        /// <summary>
        /// 通道1返回数据事件
        /// </summary>
        public event CanReceiveHandler CanReceiveData;

        /// <summary>
        /// 通道1返回数据事件
        /// </summary>
        public event CanReceiveHandler CanReceiveData2;


        private void AddData1(ZCAN_Receive_Data[] data, uint len)
        {
            string text = "";
            for (uint i = 0; i < len; ++i)
            {
                ZCAN_Receive_Data can = data[i];
                uint id = data[i].frame.can_id;
                string eff = Method.IsEFF(id) ? "扩展帧" : "标准帧";
                string rtr = Method.IsRTR(id) ? "远程帧" : "数据帧";

                text = String.Format("接收到CAN ID:0x{0:X8} {1:G} {2:G} 长度:{3:D1} 数据:", Method.GetId(id), eff, rtr, can.frame.can_dlc);
                uint canid = Method.GetId(id); //注意：ZLGCAN 收到CANID 需要 且 0x1FFFFFFF;  29位1； 0001 1111 1111 1111；

                if (CanReceiveData != null)
                    CanReceiveData(canid, can.frame.data);

                for (uint j = 0; j < can.frame.can_dlc; ++j)
                {
                    text = String.Format("{0:G}{1:X2} ", text, can.frame.data[j]);
                }
                string str = text;
            }

        }



        public recvdatathread recv_data_thread_2;
        private bool CAN_Start2()
        {
            if (!m_bInit)
            {
                MessageBox.Show("设备还没初始化", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (Method.ZCAN_StartCAN(channel_handle_2) != Define.STATUS_OK)
            {
                MessageBox.Show("启动CAN失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }


            m_bStart = true;
            if (null == recv_data_thread_2)
            {
                recv_data_thread_2 = new recvdatathread();
                recv_data_thread_2.setChannelHandle(channel_handle_2);
                recv_data_thread_2.setStart(m_bStart);
                recv_data_thread_2.RecvCANData += this.AddData2;
            }
            else
            {
                recv_data_thread_2.setChannelHandle(channel_handle_2);
            }

            return m_bStart;
        }

        private void AddData2(ZCAN_Receive_Data[] data, uint len)
        {
            string text = "";
            for (uint i = 0; i < len; ++i)
            {
                ZCAN_Receive_Data can = data[i];
                uint id = data[i].frame.can_id;
                string eff = Method.IsEFF(id) ? "扩展帧" : "标准帧";
                string rtr = Method.IsRTR(id) ? "远程帧" : "数据帧";

                text = String.Format("接收到CAN ID:0x{0:X8} {1:G} {2:G} 长度:{3:D1} 数据:", Method.GetId(id), eff, rtr, can.frame.can_dlc);
                uint canid = Method.GetId(id); //注意：ZLGCAN 收到CANID 需要 且 0x1FFFFFFF;  29位1； 0001 1111 1111 1111；

                if (CanReceiveData2 != null)
                    CanReceiveData2(canid, can.frame.data);

                for (uint j = 0; j < can.frame.can_dlc; ++j)
                {
                    text = String.Format("{0:G}{1:X2} ", text, can.frame.data[j]);
                }
                string str = text;
            }

        }


        /* CAN payload length and DLC definitions according to ISO 11898-1 */
        const int CAN_MAX_DLC = 8;
        const int CAN_MAX_DLEN = 8;

        /* CAN FD payload length and DLC definitions according to ISO 11898-7 */
        const int CANFD_MAX_DLC = 15;
        const int CANFD_MAX_DLEN = 64;

        public uint SendCanData(string canID, byte[] canData, int channelIndex = 0)
        {
            uint result = 0; //发送的帧数
            try
            {
                uint id = (uint)System.Convert.ToInt32(canID, 16);
                int frame_type_index = 1;//0 标准帧，1 扩展帧 comboBox_frametype.SelectedIndex;
                int protocol_index = 0; //0 CAN;1 CANFD;

                ZCAN_Transmit_Data can_data = new ZCAN_Transmit_Data();
                can_data.frame.can_id = Method.MakeCanId(id, frame_type_index, 0, 0);
                can_data.frame.data = new byte[8];
                can_data.frame.data = canData;
                can_data.frame.can_dlc = CAN_MAX_DLC;
                can_data.transmit_type = (uint)0; // 0  正常发送；1 单次发送；2 自发自收；3 单次自发自收；
                IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(can_data));
                Marshal.StructureToPtr(can_data, ptr, true);

                if (channelIndex == 0)
                    result = Method.ZCAN_Transmit(channel_handle_1, ptr, 1);
                else
                    result = Method.ZCAN_Transmit(channel_handle_2, ptr, 1);
                Marshal.FreeHGlobal(ptr);
            }
            catch (Exception ex)
            {
                string err = "发送数据有误；原因：" + ex.Message;
                MessageBox.Show(err);
            }
            return result;
        }

        public bool CloseCan()
        {
            uint rst = Method.ZCAN_CloseDevice(device_handle_);
            GC.Collect();
            if (rst > 0) return true;
            else
                return false;

        }




    }
}

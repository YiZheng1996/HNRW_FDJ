using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Text.RegularExpressions;
using MainUI.Global;
using System.Timers;

namespace MainUI.Equip
{
    /// <summary>
    /// 品牌：耀华A27 磅秤 通讯接口RS-232（用于测量）
    /// </summary>
    public class YHA27
    {
        public static readonly YHA27 Instance = new YHA27();

        /// <summary>
        /// 串口类
        /// </summary>
        public SerialPort _serialPort = new SerialPort();

        /// <summary>
        /// 连接状态检测定时器
        /// </summary>
        private System.Timers.Timer _connectionTimer;

        /// <summary>
        /// 最后接收数据的时间戳
        /// </summary>
        private DateTime _lastReceivedTime = DateTime.MinValue;

        /// <summary>
        /// 连接状态变化事件
        /// </summary>
        public event System.Action<bool> OnConnectionStatusChanged;

        /// <summary>
        /// 是否正在通讯 （1： 通讯  0：不通讯）
        /// </summary>
        private int _connect = 0;
        public int Connnect
        {
            get { return _connect; }
            private set
            {
                if (_connect != value)
                {
                    _connect = value;
                    OnConnectionStatusChanged?.Invoke(_connect == 1);
                }
            }
        }

        /// <summary>
        /// 当前电子秤重量 单位 KG
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event System.Action<string> OnDataReceived;

        /// <summary>
        /// 构造函数
        /// </summary>
        public YHA27()
        {
            // 初始化连接状态检测定时器
            _connectionTimer = new System.Timers.Timer(1000); // 每秒检查一次
            _connectionTimer.Elapsed += CheckConnectionStatus;
            _connectionTimer.AutoReset = true;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public bool Init()
        {
            try
            {
                _serialPort.PortName = "COM" + Var.SysConfig.BCCOM;
                _serialPort.BaudRate = 9600;
                _serialPort.DataBits = 7;
                _serialPort.Parity = Parity.Even;
                _serialPort.StopBits = StopBits.One;
                _serialPort.ReadTimeout = 3000;         // 读超时（毫秒），根据实际情况调整
                _serialPort.WriteTimeout = 3000;        // 写超时（毫秒），根据实际情况调整
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                    System.Threading.Thread.Sleep(100);
                }
                _serialPort.DataReceived -= SpCOM_DataReceived;
                _serialPort.DataReceived += SpCOM_DataReceived;

                // 启动连接状态检测
                _lastReceivedTime = DateTime.Now;
                Connnect = 1;
                Common.opcExChangeSendGrp.SetDouble("磅秤_NoError", Connnect);
                _connectionTimer.Start();

                return true;
            }
            catch (Exception ex)
            {
                Var.LogInfo($"YHA27磅秤初始化失败：{ex.Message}");
                Connnect = 0;
                Common.opcExChangeSendGrp.SetDouble("磅秤_NoError", Connnect);
                return false;
            }
        }

        /// <summary>
        /// 检查连接状态
        /// </summary>
        private void CheckConnectionStatus(object sender, ElapsedEventArgs e)
        {
            try
            {
                TimeSpan timeSinceLastData = DateTime.Now - _lastReceivedTime;

                if (timeSinceLastData.TotalSeconds > 5)
                {
                    // 超过5秒没有收到数据，判定为断线
                    if (Connnect == 1)
                    {
                        Connnect = 0;
                        Common.opcExChangeSendGrp.SetDouble("磅秤_NoError", Connnect);
                        Var.LogInfo("YHA27磅秤连接超时，设备断线");
                    }
                }
                else
                {
                    // 在5秒内有数据接收，判定为连接正常
                    if (Connnect == 0)
                    {
                        Connnect = 1;
                        Common.opcExChangeSendGrp.SetDouble("磅秤_NoError", Connnect);
                        Var.LogInfo("YHA27磅秤连接恢复");
                    }
                }
            }
            catch (Exception ex)
            {
                Var.LogInfo($"YHA27磅秤连接状态检查错误：{ex.Message}");
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            try
            {
                _connectionTimer.Stop();
                Connnect = 0;
                if (_serialPort != null && _serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
            }
            catch (Exception ex)
            {
                Var.LogInfo($"YHA27磅秤关闭失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 接受数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpCOM_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int len = _serialPort.BytesToRead;
                byte[] receByte = new byte[len];
                _serialPort.Read(receByte, 0, len);
                string hexString = BitConverter.ToString(receByte).Replace("-", " ");
                string asciiStr = Encoding.ASCII.GetString(receByte);

                // 更新最后接收数据时间
                _lastReceivedTime = DateTime.Now;

                // 触发数据接收事件
                OnDataReceived?.Invoke(asciiStr);

                // 解析数据
                ParseData(asciiStr);
            }
            catch (Exception ex)
            {
                string err = "解析磅秤数据有误；原因：" + ex.Message;
                Var.LogInfo(err);
                Weight = 0;
            }
        }

        /// <summary>
        /// 解析数据帧
        /// </summary>
        /// <param name="asciiData">接收到的ASCII字符串数据</param>
        private void ParseData(string asciiData)
        {
            try
            {
                // 根据YHA27文档说明，数据格式为11个字符，例如 "wn00008.0kg"
                if (asciiData.Length == 11)
                {
                    // 提取中间7位数字字符串 (位置2到8)
                    string weightStr = asciiData.Substring(2, 7);
                    bool convOK = double.TryParse(weightStr, out double w);
                    if (convOK)
                    {
                        Weight = w;
                    }
                    else
                    {
                        Weight = 0;
                        Var.LogInfo($"YHA27磅秤重量解析失败，字符串：{weightStr}");
                    }
                }
                else
                {
                    Weight = 0;
                    // 可选择性记录非标准长度数据
                    // Var.LogInfo($"YHA27磅秤数据长度异常，期望11字节，实际{asciiData.Length}字节：{asciiData}");
                }

                // 更新连接状态
                if (Connnect == 0)
                {
                    Connnect = 1;
                }

                // 将重量值更新到OPC变量
                Common.opcExChangeSendGrp.SetDouble("磅秤重量", Weight);
            }
            catch (Exception ex)
            {
                Var.LogInfo($"YHA27磅秤数据解析错误：{ex.Message}");
            }
        }

        /// <summary>
        /// 检查串口连接状态
        /// </summary>
        public bool IsConnected
        {
            get { return _serialPort != null && _serialPort.IsOpen; }
        }
    }
}
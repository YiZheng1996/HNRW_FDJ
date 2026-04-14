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
    /// 品牌：珠海志美PT650F 通讯接口RS-485（用于测量扭矩）
    /// </summary>
    public class ZMPT650F
    {
        public static readonly ZMPT650F Instance = new ZMPT650F();

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
        public double Weight { get; set; } = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ZMPT650F()
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
                _serialPort.PortName = "COM" + Var.SysConfig.PT650FCOM;
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
                Common.opcExChangeSendGrp.SetDouble("称重仪_NoError", Connnect);
                _connectionTimer.Start();

                return true;
            }
            catch (Exception ex)
            {
                Var.LogInfo($"ZMPT650F初始化失败：{ex.Message}");
                Connnect = 0;
                Common.opcExChangeSendGrp.SetDouble("称重仪_NoError", Connnect);
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
                        Common.opcExChangeSendGrp.SetDouble("称重仪_NoError", Connnect);
                        Var.LogInfo("ZMPT650F连接超时，设备断线");
                    }
                }
                else
                {
                    // 在5秒内有数据接收，判定为连接正常
                    if (Connnect == 0)
                    {
                        Connnect = 1;
                        Common.opcExChangeSendGrp.SetDouble("称重仪_NoError", Connnect);
                        Var.LogInfo("ZMPT650F连接恢复");
                    }
                }
            }
            catch (Exception ex)
            {
                Var.LogInfo($"ZMPT650F连接状态检查错误：{ex.Message}");
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
                Common.opcExChangeSendGrp.SetDouble("称重仪_NoError", Connnect);
                if (_serialPort != null && _serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
            }
            catch (Exception ex)
            {
                Var.LogInfo($"ZMPT650F关闭失败：{ex.Message}");
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
                string str = Encoding.ASCII.GetString(receByte);

                // 更新最后接收数据时间
                _lastReceivedTime = DateTime.Now;

                // 首先检查是否包含负号（-），如果包含则重量为0
                if (str.Contains("-"))
                {
                    // 只在有值到负值变化之后写日志，避免写太多
                    if (Weight != 0)
                    {
                        Weight = 0;
                        MiddleData.instnce.PTFWeight = Weight;
                        Common.opcExChangeSendGrp.SetDouble("重量", Weight);
                        Var.LogInfo("检测到负号，重量设置为0");
                    }
                    return; // 直接返回，不进行后续处理
                }

                // 清理字符串，移除不必要的字符
                str = str.Trim().Replace("\r", "").Replace("\n", "");
                // 兼容多种前缀格式
                string pattern = @"(ST|US|OL),GS,\s*[+\-]?\s*(\d+)kg";

                // 匹配第一个出现的有效数据
                Match match = Regex.Match(str, pattern);
                if (match.Success)
                {
                    // 提取数字部分并转换为double
                    string weightValue = match.Groups[2].Value;
                    Weight = Convert.ToDouble(weightValue);
                    MiddleData.instnce.PTFWeight = Weight;
                    Common.opcExChangeSendGrp.SetDouble("重量", Weight);

                    if (Connnect == 0)
                    {
                        Connnect = 1;
                        Common.opcExChangeSendGrp.SetDouble("称重仪_NoError", Connnect);
                    }
                }
                else
                {
                    // 如果没有匹配到有效数据
                    Weight = 0;
                    MiddleData.instnce.PTFWeight = Weight;
                    Common.opcExChangeSendGrp.SetDouble("重量", Weight);
                    Var.LogInfo("未找到有效的电子秤数据格式");
                }
            }
            catch (Exception ex)
            {
                string err = "解析电子秤数据有误；原因：" + ex.Message;
                Var.LogInfo(err);
                Weight = 0;
                Common.opcExChangeSendGrp.SetDouble("重量", Weight);
            }
        }

        /// <summary>
        /// 检查串口连接状态
        /// </summary>
        public bool IsConnected
        {
            get { return _serialPort != null && _serialPort.IsOpen && Connnect == 1; }
        }
    }
}
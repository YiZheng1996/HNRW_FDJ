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
    /// 品牌：成都燃油耗仪 ET4500 通讯接口 串口 自定义协议
    /// </summary>
    public class ET4500
    {
        public static readonly ET4500 Instance = new ET4500();

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
        /// 剩余油量kg
        /// </summary>
        public double remainingFuel { get; set; } = 0;

        /// <summary>
        /// 油耗量 kg/h
        /// </summary>
        public double fuelConsumption { get; set; } = 0;

        /// <summary> 
        /// 油耗仪状态 00=空闲  01=暂停 02=充油 03=等待 04=测量
        /// </summary>
        public string fuelStatus { get; set; } = "未知";

        /// <summary> 
        /// 油量百分比
        /// </summary>
        public double fuelPercentage { get; set; } = 0;

        /// <summary>
        /// 油量量程（需要根据实际设置调整）
        /// </summary>
        public double FuelRange { get; set; } = 30.0; // 默认30kg

        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event System.Action<string> OnDataReceived;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ET4500()
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
                _serialPort.PortName = "COM" + Var.SysConfig.ET4500COM;
                _serialPort.BaudRate = 9600;
                _serialPort.DataBits = 8;
                _serialPort.Parity = Parity.None;
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
                Common.opcExChangeSendGrp.SetDouble("油耗仪_NoError", Connnect);
                _connectionTimer.Start();

                return true;
            }
            catch (Exception ex)
            {
                Var.LogInfo($"ET4500初始化失败：{ex.Message}");
                Connnect = 0;
                Common.opcExChangeSendGrp.SetDouble("油耗仪_NoError", Connnect);
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
                        Common.opcExChangeSendGrp.SetDouble("油耗仪_NoError", Connnect);
                        Var.LogInfo("ET4500连接超时，设备断线");
                    }
                }
                else
                {
                    // 在5秒内有数据接收，判定为连接正常
                    if (Connnect == 0)
                    {
                        Connnect = 1;
                        Common.opcExChangeSendGrp.SetDouble("油耗仪_NoError", Connnect);
                        Var.LogInfo("ET4500连接恢复");
                    }
                }
            }
            catch (Exception ex)
            {
                Var.LogInfo($"ET4500连接状态检查错误：{ex.Message}");
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
                Var.LogInfo($"ET4500关闭失败：{ex.Message}");
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

                // 更新最后接收数据时间
                _lastReceivedTime = DateTime.Now;

                // 触发数据接收事件
                OnDataReceived?.Invoke(hexString);

                // 解析数据
                ParseData(receByte);
            }
            catch (Exception ex)
            {
                string err = "解析燃油耗数据有误；原因：" + ex.Message;
                Var.LogInfo(err);
            }
        }

        /// <summary>
        /// 解析数据帧
        /// </summary>
        /// <param name="data">接收到的字节数据</param>
        private void ParseData(byte[] data)
        {
            try
            {
                // 检查数据长度，至少需要9个字节
                if (data == null || data.Length < 9)
                {
                    Var.LogInfo($"ET4500数据长度不足：{data?.Length ?? 0}");
                    return;
                }

                // 检查帧头
                if (data[0] != 0x01 || data[1] != 0x01)
                {
                    Var.LogInfo("ET4500帧头校验失败");
                    return;
                }

                // 1. 解析油耗量（第3,4,5,6字节，高低字节交换）
                // 原始顺序：data[2], data[3], data[4], data[5]
                // 交换后顺序：data[3], data[2], data[5], data[4]
                byte[] fuelConsumptionBytes = new byte[4]
                {
                    data[3],  // 第4字节 -> 第1字节（高字节）
                    data[2],  // 第3字节 -> 第2字节
                    data[5],  // 第6字节 -> 第3字节
                    data[4]   // 第5字节 -> 第4字节（低字节）
                };

                // 转换为单精度浮点数
                fuelConsumption = BitConverter.ToSingle(fuelConsumptionBytes, 0);

                // 2. 解析油量百分比（第7个字节）
                byte weightByte = data[6]; // 第7个字节
                fuelPercentage = weightByte; // 转换为百分比

                // 3. 计算剩余油量
                remainingFuel = (fuelPercentage / 100.0) * FuelRange;

                // 4. 解析状态（第8个字节）
                byte statusByte = data[7]; // 第8个字节
                fuelStatus = GetStatusText(statusByte);

                if (Connnect == 0)
                {
                    Connnect = 1;
                }

                Common.opcExChangeSendGrp.SetDouble("剩余油量", remainingFuel);
                Common.opcExChangeSendGrp.SetDouble("油耗量", fuelConsumption);
                Common.opcExChangeSendGrp.SetDouble("油耗仪状态", statusByte.ToInt());
                Common.opcExChangeSendGrp.SetDouble("油量百分比", fuelPercentage);
            }
            catch (Exception ex)
            {
                //Var.LogInfo($"ET4500数据解析错误：{ex.Message}");
            }
        }

        /// <summary>
        /// 获取状态文本描述
        /// </summary>
        /// <param name="status">状态码</param>
        /// <returns>状态描述</returns>
        public string GetStatusText(byte statusByte)
        {
            switch (statusByte)
            {
                case 0x00: return "空闲";
                case 0x01: return "充油";
                case 0x02: return "充油";
                case 0x03: return "等待";
                case 0x04: return "测量";
                default: return "未知";
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
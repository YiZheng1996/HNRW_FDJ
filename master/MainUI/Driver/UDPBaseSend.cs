using System;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading;
using System.Text;
using MetorSignalSimulator.UI.Model;
using TRDP;
using MainUI.Global;

namespace MainUI.Driver
{
    /// <summary>
    /// UDP发送器
    /// </summary>
    public class UDPBaseSend
    {
        private string _targetIp = "192.168.0.122";  // 接收端IP
        private int _targetPort = 17226;            // 接收端端口
        private int _localReceivePort = 17227;     // 本地接收端口

        private UdpClient _udpSender;   //用于发送
        private UdpClient _udpReceiver;    // 用于接收

        private bool _isRunning = false;
        private int _pingTimeout = 2000;             // 2秒ping超时
        private int _heartbeatInterval = 5000;      // 5秒心跳间隔
        private Thread _heartbeatThread; // 心跳线程
        private readonly object _lockObject = new object();
        public bool _isConnected = false;

        private Thread _receiveThread; //接收数据线程

        // 事件
        public event Action<string> OnLogMessage;
        public event Action<bool> OnConnectionStatusChanged;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UDPBaseSend()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="targetIp">目标IP</param>
        /// <param name="targetPort">目标端口</param>
        /// <param name="localReceivePort">本地接收端口</param>
        public UDPBaseSend(string targetIp, int targetPort, int localReceivePort)
        {
            _targetIp = targetIp;
            _targetPort = targetPort;
            _localReceivePort = localReceivePort;
        }

        /// <summary>
        /// 启动发送器
        /// </summary>
        public void Start()
        {
            if (_isRunning)
            {
                LogMessage("发送器已在运行中");
                return;
            }

            _isRunning = true;
            InitializeConnection();

            // 启动心跳线程 避免死机(如果是控制端，就发送数据，如果是接收端，就发送心跳)
            _heartbeatThread = new Thread(SendHeartbeatLoop)
            {
                IsBackground = true,
                Name = "UdpSend_Receive"
            };
            _heartbeatThread.Start();

            // 启动接收线程
            _receiveThread = new Thread(ReceiveLoop)
            {
                IsBackground = true,
                Name = "UdpReceiver_Receive"
            };
            _receiveThread.Start();

            LogMessage("UDP发送器已启动");
        }

        /// <summary>
        /// 停止发送器
        /// </summary>
        public void Stop()
        {
            _isRunning = false;

            lock (_lockObject)
            {
                _isConnected = false;
                _udpSender?.Close();
                _udpReceiver?.Close();
                _udpSender = null;
                _udpReceiver = null;
            }

            _heartbeatThread?.Join(1000);
            _receiveThread?.Join(2000);

            LogMessage("UDP发送器已停止");
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="message">消息内容</param>
        public void Send(string message)
        {
            try
            {
                if (string.IsNullOrEmpty(message))
                {
                    LogMessage("消息不能为空");
                    return;
                }

                SendData(message);
            }
            catch (Exception ex)
            {
                LogMessage($"发送失败: {ex.Message}");

                // 尝试重新初始化
                if (!_isConnected)
                {
                    LogMessage("正在尝试重新初始化连接...");
                    InitializeConnection();
                }
            }
        }

        /// <summary>
        /// 发送字节数组
        /// </summary>
        /// <param name="data">字节数组</param>
        public void Send(byte[] data)
        {
            try
            {
                if (data == null || data.Length == 0)
                {
                    LogMessage("数据不能为空");
                    return;
                }

                SendData(data);
            }
            catch (Exception ex)
            {
                LogMessage($"发送失败: {ex.Message}");

                if (!_isConnected)
                {
                    LogMessage("正在尝试重新初始化连接...");
                    InitializeConnection();
                }
            }
        }

        /// <summary>
        /// 初始化连接
        /// </summary>
        private void InitializeConnection()
        {
            try
            {
                lock (_lockObject)
                {
                    _udpSender = new UdpClient();

                    // 接收端 - 绑定到特定端口
                    _udpReceiver = new UdpClient(_localReceivePort); // 绑定到目标端口

                    _isConnected = true;
                }

                OnConnectionStatusChanged?.Invoke(true);
                LogMessage($"UDP发送器已初始化，目标: {_targetIp}:{_targetPort}，接收端口: {_localReceivePort}");
            }
            catch (Exception ex)
            {
                LogMessage($"初始化失败: {ex.Message}");
                _isConnected = false;
                OnConnectionStatusChanged?.Invoke(false);
            }
        }

        public void SendData(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            SendData(data);
        }

        public void SendData(byte[] data)
        {
            lock (_lockObject)
            {
                if (_udpSender != null && _isConnected)
                {
                    _udpSender.Send(data, data.Length, _targetIp, _targetPort);
                    LogMessage($"[{DateTime.Now:HH:mm:ss}] 已发送: {data.Length}字节");
                }
                else
                {
                    LogMessage("发送器未初始化，无法发送数据");
                }
            }
        }

        /// <summary>
        /// 接收数据循环
        /// </summary>
        private void ReceiveLoop()
        {
            while (_isRunning && _isConnected)
            {
                try
                {
                    IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receivedData = _udpReceiver.Receive(ref remoteEP);
                    if (remoteEP.Address.ToString() != _targetIp)
                    {
                        // 干扰数据，只需要接收 发动端的数据即可
                        continue;
                    }
                    string message = Encoding.UTF8.GetString(receivedData);

                    // 记录接收信息
                    LogMessage($"从 {remoteEP.Address}:{remoteEP.Port} 收到 {receivedData.Length} 字节数据");

                    // 处理接收到的数据
                    ProcessReceivedData(receivedData, remoteEP);
                }
                catch (SocketException ex) when (ex.SocketErrorCode == SocketError.Interrupted)
                {
                    // 正常关闭
                    LogMessage("接收线程被中断");
                    break;
                }
                catch (ObjectDisposedException)
                {
                    // UDP客户端被释放
                    LogMessage("UDP客户端已被释放");
                    break;
                }
                catch (Exception ex)
                {
                    if (_isRunning && _isConnected)
                    {
                        LogMessage($"接收数据时出错: {ex.Message}");

                        // 检查连接状态
                        if (!PingSender())
                        {
                            LogMessage("发送端连接已断开");
                            lock (_lockObject)
                            {
                                _isConnected = false;
                                OnConnectionStatusChanged?.Invoke(false);
                            }
                            break;
                        }
                    }
                }
            }

            LogMessage("接收线程已退出");
        }

        /// <summary>
        /// 处理接收到的数据
        /// </summary>
        private void ProcessReceivedData(byte[] receivedData, IPEndPoint remoteEP)
        {
            try
            {
                // 检查是否是心跳包
                if (receivedData.Length == 9 && Encoding.UTF8.GetString(receivedData) == "HEARTBEAT")
                {
                    LogMessage("收到心跳包");
                    return;
                }

                // 检测帧头前两个字节是否为99
                if (receivedData.Length >= 4 && receivedData[0] == 99 && receivedData[1] == 99)
                {
                    LogMessage($"✓ 帧头检测通过: [{receivedData[0]}, {receivedData[1]}， 通讯状态：{receivedData[2]}]");

                    if (receivedData.Length > 4)
                    {
                        // 主站 通讯成功（用于底部状态栏状态更新）
                        if (receivedData[2] == 1)
                        {
                            // 赋值重量
                            //UInt16 scaledWeight = BitConverter.ToUInt16(receivedData, 2);
                            //MiddleData.instnce.PTFWeight = scaledWeight;
                            // 后续还存在字节则按照模式进行设置 

                            // 创建新数组，只去除 帧头和数据
                            int additionalDataSize = receivedData.Length - 3;
                            byte[] additionalData = new byte[additionalDataSize];
                            Array.Copy(receivedData, 3, additionalData, 0, additionalDataSize);

                            // 更新 FullTags 中的实时数据
                            Var.TRDP.UpdateTagsRealTimeData(Var.tRDPConfig.ReceiveCOMID, additionalData);

                            LogMessage($"✓ 数据处理完成，提取数据长度: {additionalData}字节");
                        }
                    }
                    else
                    {
                        LogMessage($"× 数据长度不足: {receivedData.Length}字节，只有帧头没有数据");
                    }
                }
                else
                {
                    if (receivedData.Length >= 4)
                    {
                        LogMessage($"× 帧头检测失败: [{receivedData[0]}, {receivedData[1]}]，预期: [99, 99 ,0]");
                    }
                    else
                    {
                        LogMessage($"× 数据长度不足2字节，无法检测帧头");
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage($"处理接收数据时出错: {ex.Message}");
            }
        }

        /// <summary>
        /// 发送心跳包
        /// </summary>
        private void SendHeartbeatLoop()
        {
            if (Var.SysConfig.ExeType == 1)
            {
                _heartbeatInterval = 200;
            }

            while (_isRunning)
            {
                try
                {
                    Thread.Sleep(_heartbeatInterval);

                    if (!_isRunning || !_isConnected)
                        continue;

                    // 控制端发送数据
                    if (Var.SysConfig.ExeType == 1)
                    {
                        // 如果UDP服务启动了，那么通过udp服务发送数据给另一台电脑 (本机处于通讯并且)
                        if (Var.UDPSend._isConnected && VarHelperETH.BCU_CCU != null)
                        {
                            // 额外的字节
                            int baseSize = 3; // 3字节帧头 + X个UInt16(一个数据 2字节) 

                            // 创建新数组 目前我只多传一个实时重量
                            byte[] sendUdpByte = new byte[VarHelperETH.BCU_CCU.Length + baseSize];

                            // 设置前两个字节为帧头 [99],[99]
                            sendUdpByte[0] = 99;  // 第一个字节
                            sendUdpByte[1] = 99;  // 第二个字节
                            sendUdpByte[2] = (byte)(Var.TRDP.IsConnected ? 1 : 0);  // 第三个字节 表示通讯状态 1：通讯成功 0：通讯失败

                            // 重量
                            //UInt16 weight = (UInt16)MiddleData.instnce.PTFWeight; 
                            //byte[] weightBytes = BitConverter.GetBytes(weight);
                            //Array.Copy(weightBytes, 0, sendUdpByte, 2, 2);
                            // 下一个数据
                            //Array.Copy(weightBytes, 0, sendUdpByte, 4, 2);

                            // 复制原始数据到新数组的后面部分
                            Array.Copy(VarHelperETH.BCU_CCU, 0, sendUdpByte, baseSize, VarHelperETH.BCU_CCU.Length);

                            //// 通过UDP发送添加了帧头的数据
                            _udpSender.Send(sendUdpByte, sendUdpByte.Length, _targetIp, _targetPort);
                        }
                    }
                    else
                    {
                        // 接收端发送心跳 
                        // 发送心跳包
                        string heartbeatMsg = "HEARTBEAT";
                        byte[] data = Encoding.UTF8.GetBytes(heartbeatMsg);

                        lock (_lockObject)
                        {
                            if (_udpSender != null && _isConnected)
                            {
                                _udpSender.Send(data, data.Length, _targetIp, _targetPort);
                                LogMessage($"[{DateTime.Now:HH:mm:ss}] 心跳包已发送");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogMessage($"心跳发送失败: {ex.Message}");
                    lock (_lockObject)
                    {
                        _isConnected = false;
                        OnConnectionStatusChanged?.Invoke(false);
                    }

                    // 尝试重新连接
                    Thread.Sleep(1000);
                    if (_isRunning)
                    {
                        InitializeConnection();
                    }
                }
            }
        }

        private void LogMessage(string message)
        {
            OnLogMessage?.Invoke($"[{DateTime.Now:HH:mm:ss}] {message}");
        }

        /// <summary>
        /// Ping发送端
        /// </summary>
        private bool PingSender()
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send(_targetIp, _pingTimeout);
                    bool reachable = reply != null && reply.Status == IPStatus.Success;

                    if (!reachable)
                    {
                        LogMessage($"Ping检测失败: 目标端 {_targetIp} 不可达");
                    }

                    return reachable;
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Ping检测出错: {ex.Message}");
                return false;
            }
        }
    }
}
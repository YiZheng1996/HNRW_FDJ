using RW.Modules;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modbus.Device;
using Modbus.Utility;
using System.Globalization;
using static MainUI.Modules.EventArgsModel;

namespace MainUI.Equip
{
    /// <summary>
    /// 励磁柜通讯类（使用NModbus4库进行Modbus TCP通信）
    /// </summary>
    public class RS36Excitation : IDisposable
    {
        /// <summary>
        /// 单例实例
        /// </summary>
        public static readonly RS36Excitation Instance = new RS36Excitation();

        #region 私有字段
        private TcpClient _tcpClient;
        private IModbusMaster _modbusMaster;
        private string _deviceIP = Var.SysConfig.ExcitationAdress;
        private int _devicePort = 502;
        private int _pollingInterval = 1000; // 刷新周期（ms）
        private byte _deviceId = 1; // Modbus设备ID

        // 线程控制
        private Thread _workThread;
        private volatile bool _isRunning;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        // 连接状态
        private volatile bool _isConnected;
        private readonly object _connectionLock = new object();
        private int _reconnectAttempts; // 重连计时
        private const int RECONNECT_DELAY_MS = 2000;

        // 值改变后更新
        [Description("值改变后触发")]
        public event EventHandler<DIValueChangedEventArgs> KeyValueChange;

        // 数据缓存
        private double _voltage;
        private double _current;
        private bool _fault63;
        private readonly object _syncLock = new object();

        // 寄存器地址定义
        private const ushort CURRENT_REGISTER_START = 28;     // 电流寄存器起始地址
        private const ushort VOLTAGE_REGISTER_START = 32;     // 电压寄存器起始地址
        private const ushort FAULT_COIL_ADDRESS = 62;         // 故障线圈地址
        private const ushort REGISTER_COUNT = 4;              // 读取的寄存器数量（每个值需要4个寄存器，64位）
        #endregion

        #region 公共属性
        /// <summary>
        /// 电压值（线程安全）
        /// </summary>
        public double Voltage
        {
            get { lock (_syncLock) return _voltage; }
            private set { lock (_syncLock) _voltage = value; }
        }

        /// <summary>
        /// 电流值（线程安全）
        /// </summary>
        public double Current
        {
            get { lock (_syncLock) return _current; }
            private set { lock (_syncLock) _current = value; }
        }

        /// <summary>
        /// 起励失败故障状态（线程安全）
        /// </summary>
        public bool Fault63
        {
            get { lock (_syncLock) return _fault63; }
            private set { lock (_syncLock) _fault63 = value; }
        }

        /// <summary>
        /// 是否已连接
        /// </summary>
        public bool IsConnected => _isConnected;

        /// <summary>
        /// 是否正在轮询
        /// </summary>
        public bool IsPolling => _isRunning;
        #endregion

        #region 事件
        /// <summary>
        /// 数据更新事件参数
        /// </summary>
        public class ExcitationDataEventArgs : EventArgs
        {
            public double Voltage { get; set; }
            public double Current { get; set; }
            public bool Fault63 { get; set; }
        }

        /// <summary>
        /// 数据更新事件
        /// </summary>
        public event EventHandler<ExcitationDataEventArgs> DataUpdated;

        /// <summary>
        /// 连接状态变化事件
        /// </summary>
        public event EventHandler<bool> ConnectionStatusChanged;
        #endregion

        #region 构造函数
        private RS36Excitation()
        {
            // 私有构造函数，确保单例
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 设置连接参数
        /// </summary>
        public void SetConnectionParameters(string ip, int port = 502, byte deviceId = 5)
        {
            _deviceIP = ip;
            _devicePort = port;
            _deviceId = deviceId;
        }

        /// <summary>
        /// 设置设备ID
        /// </summary>
        public void SetDeviceId(byte deviceId)
        {
            _deviceId = deviceId;
        }

        /// <summary>
        /// 开始轮询数据
        /// </summary>
        /// <param name="interval">轮询间隔（毫秒）</param>
        /// <returns>是否成功启动</returns>
        public bool StartPolling(int interval = 1000)
        {
            try
            {
                if (_isRunning)
                {
                    Var.LogInfo("轮询已在进行中");
                    return false;
                }

                _pollingInterval = interval;
                _isRunning = true;
                KeyValueChange?.Invoke(this, new DIValueChangedEventArgs("Running", _isRunning));

                // 启动工作线程
                _workThread = new Thread(WorkThreadProc)
                {
                    IsBackground = true,
                    Name = "RS36 励磁柜通讯线程(NModbus)"
                };
                _workThread.Start();

                Var.LogInfo($"励磁通讯开始轮询，间隔：{interval}ms，设备：{_deviceIP}:{_devicePort}，设备ID：{_deviceId}");
                return true;
            }
            catch (Exception ex)
            {
                Var.LogInfo($"开始轮询错误：{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 停止轮询
        /// </summary>
        public void StopPolling()
        {
            try
            {
                _isRunning = false;
                KeyValueChange?.Invoke(this, new DIValueChangedEventArgs("Running", _isRunning));
                _cts.Cancel();

                if (_workThread != null && _workThread.IsAlive)
                {
                    if (!_workThread.Join(2000)) // 等待2秒
                    {
                        _workThread.Interrupt(); // 温和地中断线程
                    }
                    _workThread = null;
                }

                Var.LogInfo("励磁通讯停止轮询");
            }
            catch (Exception ex)
            {
                Var.LogInfo($"停止轮询错误：{ex.Message}");
            }
        }

        /// <summary>
        /// 立即手动读取一次数据
        /// </summary>
        public void ForceRead()
        {
            if (_isConnected)
            {
                PerformPolling();
            }
        }
        #endregion

        #region 私有方法 - 连接管理
        /// <summary>
        /// 初始化TCP连接
        /// </summary>
        public bool InitConnection()
        {
            lock (_connectionLock)
            {
                try
                {
                    // 关闭现有连接
                    DisconnectInternal();

                    // 创建TCP客户端
                    _tcpClient = new TcpClient(_deviceIP, _devicePort)
                    {
                        SendTimeout = 3000,
                        ReceiveTimeout = 3000,
                        NoDelay = true
                    };

                    if (_tcpClient.Connected)
                    {
                        // 创建Modbus TCP Master
                        _modbusMaster = ModbusIpMaster.CreateIp(_tcpClient);
                        _modbusMaster.Transport.ReadTimeout = 3000;
                        _modbusMaster.Transport.WriteTimeout = 3000;

                        _isConnected = true;
                        _reconnectAttempts = 0; // 重置重连计数
                        OnConnectionStatusChanged(true);
                        Var.LogInfo($"成功连接到设备：{_deviceIP}:{_devicePort}");

                        // 测试读取一个寄存器，验证通信是否正常
                        try
                        {
                            ushort[] testData = _modbusMaster.ReadInputRegisters(_deviceId, CURRENT_REGISTER_START, 1);
                            Var.LogInfo($"连接测试成功，读取到测试数据");
                            return true;
                        }
                        catch (Exception testEx)
                        {
                            Var.LogInfo($"连接测试失败：{testEx.Message}");
                            DisconnectInternal();
                            return false;
                        }
                    }

                    OnConnectionStatusChanged(false);
                    Var.LogInfo($"连接失败：{_deviceIP}:{_devicePort}");
                    return false;
                }
                catch (Exception ex)
                {
                    _isConnected = false;
                    OnConnectionStatusChanged(false);
                    Var.LogInfo($"连接失败：{ex.Message}");
                    return false;
                }
            }
        }

        /// <summary>
        /// 断开连接（内部方法）
        /// </summary>
        private void DisconnectInternal()
        {
            lock (_connectionLock)
            {
                try
                {
                    if (_modbusMaster != null)
                    {
                        // NModbus4 的 Master 没有 Dispose 方法，我们只处理 TCP 客户端
                    }

                    _tcpClient?.Close();
                    _tcpClient = null;
                    _modbusMaster = null;

                    if (_isConnected)
                    {
                        _isConnected = false;
                        OnConnectionStatusChanged(false);
                        Var.LogInfo("连接已断开");
                    }
                }
                catch (Exception ex)
                {
                    Var.LogInfo($"断开连接错误：{ex.Message}");
                }
            }
        }

        /// <summary>
        /// 智能重连机制
        /// </summary>
        private bool SmartReconnect()
        {
            _reconnectAttempts++;
            int delay = RECONNECT_DELAY_MS * _reconnectAttempts; // 指数退避策略 最长多等10秒
            Var.LogInfo($"第{_reconnectAttempts}次尝试重连，等待{delay}ms...");
            if (delay > 10000)
                delay = 10000;
            Thread.Sleep(delay);

            bool result = InitConnection();
            if (result)
            {
                Var.LogInfo("重连成功");
            }

            return result;
        }

        /// <summary>
        /// 检查连接状态并尝试恢复
        /// </summary>
        private bool EnsureConnected()
        {
            // 检查当前连接状态
            bool isCurrentlyConnected = _tcpClient?.Connected == true && _isConnected && _modbusMaster != null;

            if (!isCurrentlyConnected)
            {
                Var.LogInfo("连接已断开，尝试重新连接...");
                return SmartReconnect();
            }

            return true;
        }
        #endregion

        #region 私有方法 - 工作线程
        /// <summary>
        /// 工作线程主循环
        /// </summary>
        private void WorkThreadProc()
        {
            DateTime lastPollTime = DateTime.Now;

            while (_isRunning && !_cts.Token.IsCancellationRequested)
            {
                try
                {
                    // 计算等待时间
                    int timeSinceLastPoll = (int)(DateTime.Now - lastPollTime).TotalMilliseconds;
                    int waitTime = Math.Max(0, _pollingInterval - timeSinceLastPoll);

                    // 使用CancellationToken等待，支持优雅停止
                    if (waitTime > 0 && !_cts.Token.WaitHandle.WaitOne(waitTime))
                    {
                        // 到达轮询时间，执行数据读取
                        if (EnsureConnected())
                        {
                            PerformPolling();
                            lastPollTime = DateTime.Now;
                        }
                        else
                        {
                            // 连接失败，稍后重试
                            Thread.Sleep(1000);
                        }
                    }
                }
                catch (ThreadInterruptedException)
                {
                    // 线程被中断，正常退出
                    break;
                }
                catch (Exception ex)
                {
                    Var.LogInfo($"工作线程错误：{ex.Message}");
                    Thread.Sleep(1000); // 出错后等待1秒
                }
            }

            Var.LogInfo("工作线程结束");
        }

        /// <summary>
        /// 执行轮询操作
        /// </summary>
        private void PerformPolling()
        {
            try
            {
                bool success1 = PollCurrent();
                bool success2 = PollVoltage();
                bool success3 = PollFault63();

                // 只有当至少一个数据读取成功时才触发更新
                if (success1 || success2 || success3)
                {
                    OnDataUpdated();
                }
            }
            catch (Exception ex)
            {
                Var.LogInfo($"轮询数据错误：{ex.Message}");
                _isConnected = false;
            }
        }
        #endregion

        #region 私有方法 - Modbus通讯（使用NModbus4）
        /// <summary>
        /// 读取励磁电流（4区输入寄存器，地址28，小端字节序）
        /// 注意：NModbus的寄存器地址是从0开始的，所以设备地址28对应NModbus地址27
        /// </summary>
        /// <summary>
        /// 读取励磁电流（4区输入寄存器，地址28，64位双精度浮点数，小端模式）
        /// 注意：需要读取4个寄存器（64位）
        /// </summary>
        private bool PollCurrent()
        {
            try
            {
                // 读取4个输入寄存器（64位双精度浮点数）
                ushort[] registers = _modbusMaster.ReadInputRegisters(_deviceId,
                    CURRENT_REGISTER_START, // 起始地址
                    REGISTER_COUNT); // 读取4个寄存器（64位）

                if (registers.Length >= REGISTER_COUNT)
                {
                    // 64位双精度浮点数，小端模式字节交换
                    // 寄存器顺序：[0]=低16位, [1]=次低16位, [2]=次高16位, [3]=高16位
                    byte[] bytes = new byte[8];

                    // 小端模式：先存低字节，后存高字节
                    Buffer.BlockCopy(BitConverter.GetBytes(registers[0]), 0, bytes, 0, 2);
                    Buffer.BlockCopy(BitConverter.GetBytes(registers[1]), 0, bytes, 2, 2);
                    Buffer.BlockCopy(BitConverter.GetBytes(registers[2]), 0, bytes, 4, 2);
                    Buffer.BlockCopy(BitConverter.GetBytes(registers[3]), 0, bytes, 6, 2);

                    // 转换为double
                    double value = BitConverter.ToDouble(bytes, 0);
                    Current = value;

                    Var.LogInfo($"读取电流成功: {Current}");
                    return true;
                }

                Var.LogInfo($"读取电流失败：寄存器数量不足，期望4个，实际{registers.Length}个");
                return false;
            }
            catch (Exception ex)
            {
                Var.LogInfo($"读取励磁电流错误：{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 读取励磁电压（4区输入寄存器，地址32，大端字节序）
        /// 注意：NModbus的寄存器地址是从0开始的，所以设备地址32对应NModbus地址31
        /// </summary>
        /// <summary>
        /// 读取励磁电压（4区输入寄存器，地址32，64位双精度浮点数，小端模式）
        /// 注意：需要读取4个寄存器（64位）
        /// </summary>
        private bool PollVoltage()
        {
            try
            {
                // 读取4个输入寄存器（64位双精度浮点数）
                ushort[] registers = _modbusMaster.ReadInputRegisters(_deviceId,
                    VOLTAGE_REGISTER_START, // 起始地址
                    REGISTER_COUNT); // 读取4个寄存器（64位）

                if (registers.Length >= REGISTER_COUNT)
                {
                    // 64位双精度浮点数，小端模式字节交换
                    // 寄存器顺序：[0]=低16位, [1]=次低16位, [2]=次高16位, [3]=高16位
                    byte[] bytes = new byte[8];

                    // 小端模式：先存低字节，后存高字节
                    Buffer.BlockCopy(BitConverter.GetBytes(registers[0]), 0, bytes, 0, 2);
                    Buffer.BlockCopy(BitConverter.GetBytes(registers[1]), 0, bytes, 2, 2);
                    Buffer.BlockCopy(BitConverter.GetBytes(registers[2]), 0, bytes, 4, 2);
                    Buffer.BlockCopy(BitConverter.GetBytes(registers[3]), 0, bytes, 6, 2);

                    // 转换为double
                    double value = BitConverter.ToDouble(bytes, 0);
                    Voltage = value;

                    Var.LogInfo($"读取电压成功: {Voltage}");
                    return true;
                }

                Var.LogInfo($"读取电压失败：寄存器数量不足，期望4个，实际{registers.Length}个");
                return false;
            }
            catch (Exception ex)
            {
                Var.LogInfo($"读取励磁电压错误：{ex.Message}");
                return false;
            }
        }


        /// <summary>
        /// 读取故障状态（读线圈，地址62）
        /// 注意：NModbus的线圈地址是从0开始的，所以设备地址62对应NModbus地址61
        /// </summary>
        private bool PollFault63()
        {
            try
            {
                // 读取线圈（功能码01）
                bool[] coils = _modbusMaster.ReadCoils(_deviceId,
                    FAULT_COIL_ADDRESS, // NModbus地址从0开始
                    1); // 读取1个线圈

                if (coils.Length >= 1)
                {
                    Fault63 = coils[0];
                    Var.LogInfo($"读取故障状态成功: {Fault63}");
                    return true;
                }

                Var.LogInfo("读取故障状态失败：线圈数量不足");
                return false;
            }
            catch (Exception ex)
            {
                Var.LogInfo($"读取故障状态错误：{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 测试读取单个寄存器（用于调试）
        /// </summary>
        public bool TestReadRegister(ushort address, ushort count, bool isInputRegister = true)
        {
            try
            {
                if (!EnsureConnected())
                {
                    Var.LogInfo("测试失败：未连接");
                    return false;
                }

                Var.LogInfo($"测试读取: 地址={address}, 数量={count}, 类型={(isInputRegister ? "输入寄存器" : "保持寄存器")}");

                if (isInputRegister)
                {
                    ushort[] data = _modbusMaster.ReadInputRegisters(_deviceId, address, count);
                    Var.LogInfo($"测试成功: 读取到 {data.Length} 个寄存器");

                    // 打印寄存器值
                    for (int i = 0; i < data.Length; i++)
                    {
                        Var.LogInfo($"寄存器[{address + i}] = {data[i]} (0x{data[i]:X4})");
                    }
                }
                else
                {
                    ushort[] data = _modbusMaster.ReadHoldingRegisters(_deviceId, address, count);
                    Var.LogInfo($"测试成功: 读取到 {data.Length} 个寄存器");

                    // 打印寄存器值
                    for (int i = 0; i < data.Length; i++)
                    {
                        Var.LogInfo($"寄存器[{address + i}] = {data[i]} (0x{data[i]:X4})");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Var.LogInfo($"测试错误: {ex.Message}");
                return false;
            }
        }
        #endregion

        #region 私有方法 - 事件处理
        /// <summary>
        /// 触发数据更新事件（线程安全）
        /// </summary>
        private void OnDataUpdated()
        {
            var handler = DataUpdated;
            if (handler != null)
            {
                // 创建事件参数
                var args = new ExcitationDataEventArgs
                {
                    Voltage = this.Voltage,
                    Current = this.Current,
                    Fault63 = this.Fault63
                };

                // 检查是否需要切换到UI线程
                if (Application.OpenForms.Count > 0 && Application.OpenForms[0].InvokeRequired)
                {
                    Application.OpenForms[0].BeginInvoke(new Action(() =>
                    {
                        handler(this, args);
                    }));
                }
                else
                {
                    handler(this, args);
                }
            }
        }

        /// <summary>
        /// 触发连接状态变化事件
        /// </summary>
        private void OnConnectionStatusChanged(bool isConnected)
        {
            var handler = ConnectionStatusChanged;
            if (handler != null)
            {
                // 在UI线程上触发事件
                if (Application.OpenForms.Count > 0 && Application.OpenForms[0].InvokeRequired)
                {
                    Application.OpenForms[0].BeginInvoke(new Action(() =>
                    {
                        handler(this, isConnected);
                    }));
                }
                else
                {
                    handler(this, isConnected);
                }
            }
        }
        #endregion

        #region IDisposable实现
        private bool _disposed = false;

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // 释放托管资源
                    StopPolling();
                    DisconnectInternal();
                    _cts?.Dispose();
                }

                _disposed = true;
            }
        }

        ~RS36Excitation()
        {
            Dispose(false);
        }
        #endregion
    }
}
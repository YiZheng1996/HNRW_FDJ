using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using RW.Modules;
using RW.Driver;
using System.Threading;
using System.Diagnostics;
using System.IO.Ports;
using RW.Core;
using System.Net.Sockets;
using System.Net;
using BogieIdling.UI.TRDP.Model;
using BogieIdling.UI.Model;
using TestFunction;
using System.Linq;
using BogieIdling.UI;
using System.Collections.Concurrent;
using MetorSignalSimulator.UI.Model;

namespace RW.Driver
{
    /// <summary>
    /// TRDP 模块，（串口模式）
    /// 用于接收、发送以太网通讯数据，请参照协议使用此模块。
    /// 常用流程，
    /// 1、发送数据配置（SetSetting）成功后，等待2秒
    /// 2、发送数据到TCMS（SetToTCMS）
    /// 3、发送2，并刷新生命信号
    /// </summary>
    public class TRDPDriver : IDriver
    {
        /// <summary>
        /// 实时值更新
        /// </summary>
        public ConcurrentDictionary<string, decimal> trdpValue = new ConcurrentDictionary<string, decimal>();//存储该端口生命信号的类型，U16，还是U32
        public List<Ports> ports = new List<Ports>();
        public List<FullTags> tags = new List<FullTags>();

        public TRDPDriver()
        {
            //port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            ReadTimeout = 3000;
            string[] arr = Enum.GetNames(typeof(TRDPCommandTypes));
            foreach (var item in arr)
            {
                dicWait[(TRDPCommandTypes)Enum.Parse(typeof(TRDPCommandTypes), item)] = new AutoResetEvent(false);
            }

        }

        void AlwaysRead()
        {
            while (!closeed)
            {
                try
                {
                    var bts = udp.Receive(ref reviceip);//直到有数据，按次接受
                    string text = RWConvert.BytesToHexString(bts, " ");
                    // todo 暂时注释
                    Debug.WriteLine("接收到IP：" + reviceip.Address + " recived:" + text);

                    int offset = 0;
                    int byteCount = bts.Length;

                    TRDPCommandTypes cmd = (TRDPCommandTypes)bts[1];
                    switch (cmd)
                    {
                        case TRDPCommandTypes.Setting0:
                        case TRDPCommandTypes.Setting1:
                        case TRDPCommandTypes.SettingSuccess0:
                        case TRDPCommandTypes.SettingSuccess1:
                            {
                                //3.4 配置应答帧
                                if (byteCount + offset < 8) { offset = byteCount; continue; }
                                //说明数据填充完毕

                                byte[] recieve = new byte[8];
                                Array.Copy(bts, 0, recieve, 0, recieve.Length);

                                TRDPMainRecieve rec = new TRDPMainRecieve();
                                rec.InitData(recieve);
                                OnRecieved(cmd, rec);
                                if (cmd == TRDPCommandTypes.Setting0 || cmd == TRDPCommandTypes.SettingSuccess0)
                                    dicWait[TRDPCommandTypes.Setting0].Set();
                                if (cmd == TRDPCommandTypes.Setting1 || cmd == TRDPCommandTypes.SettingSuccess1)
                                    dicWait[TRDPCommandTypes.Setting1].Set();
                            }
                            break;
                        case TRDPCommandTypes.SettingDNS0:
                            break;
                        case TRDPCommandTypes.SendTCMS0:
                        case TRDPCommandTypes.SendTCMS1:
                            break;
                        case TRDPCommandTypes.RecieveTCMS0:
                        case TRDPCommandTypes.RecieveTCMS1:
                            {
                                //3.6 从TCMS接收数据帧
                                if (byteCount + offset < 26) { offset = byteCount; continue; }
                                int count = BitConverter.ToInt32(new byte[] { bts[25], bts[24], bts[23], bts[22] }, 0);
                                if (byteCount + offset < 26 + count + 2) { offset += byteCount; continue; }

                                byte[] recieve = new byte[26 + count + 2];
                                Array.Copy(bts, 0, recieve, 0, recieve.Length);

                                FromTCMSRecieve rec = new FromTCMSRecieve();
                                rec.InitData(recieve);
                                OnRecieved(cmd, rec);
                                if (cmd == TRDPCommandTypes.RecieveTCMS0)
                                    dicWait[TRDPCommandTypes.SendTCMS0].Set();
                                if (cmd == TRDPCommandTypes.RecieveTCMS1)
                                    dicWait[TRDPCommandTypes.SendTCMS1].Set();
                            }
                            break;
                        case TRDPCommandTypes.SendSinglecast10:
                        case TRDPCommandTypes.SendSinglecast20:
                        case TRDPCommandTypes.SendSinglecast11:
                        case TRDPCommandTypes.SendSinglecast21:
                            break;
                        case TRDPCommandTypes.RecieveSinglecast10:
                        case TRDPCommandTypes.RecieveSinglecast20:
                        case TRDPCommandTypes.RecieveSinglecast11:
                        case TRDPCommandTypes.RecieveSinglecast21:
                            {
                                //3.9 自由单播接收数据帧
                                if (byteCount + offset < 18) { offset += byteCount; continue; }
                                int count = BitConverter.ToInt32(new byte[] { bts[17], bts[16], bts[15], bts[14] }, 0);
                                if (byteCount + offset < 18 + count + 2) { offset += byteCount; continue; }

                                byte[] recieve = new byte[18 + count + 2];
                                Array.Copy(bts, 0, recieve, 0, recieve.Length);

                                SinglecastRecieve rec = new SinglecastRecieve();
                                rec.InitData(recieve);
                                OnRecieved(cmd, rec);
                                if (cmd == TRDPCommandTypes.RecieveSinglecast10)
                                    dicWait[TRDPCommandTypes.SendSinglecast10].Set();
                                if (cmd == TRDPCommandTypes.RecieveSinglecast11)
                                    dicWait[TRDPCommandTypes.SendSinglecast11].Set();
                                if (cmd == TRDPCommandTypes.RecieveSinglecast20)
                                    dicWait[TRDPCommandTypes.SendSinglecast20].Set();
                                if (cmd == TRDPCommandTypes.RecieveSinglecast21)
                                    dicWait[TRDPCommandTypes.SendSinglecast21].Set();
                            }
                            break;
                        case TRDPCommandTypes.None:
                        default:
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("TRDP接收数据异常：" + ex.Message);
                }
                finally
                {
                    Thread.Sleep(10);
                }

            }
        }

        IPEndPoint sendip;//= new IPEndPoint(IPAddress.Parse("192.168.1.1"), 6666);//发送数据所用的端口，TRDP默认IP为192.168.1.1
        IPEndPoint reviceip;//= new IPEndPoint(IPAddress.Parse("0.0.0.0"), 6666);//表示本机的指定端口，用于监听
        UdpClient udp = new UdpClient();

        Dictionary<TRDPCommandTypes, AutoResetEvent> dicWait = new Dictionary<TRDPCommandTypes, AutoResetEvent>();

        /// <summary>
        /// 通讯模块 初始化
        /// </summary>
        /// <param name="desIP">目标IP地址 众志城IP 地址</param>
        /// <param name="desport">目标端口 众志城端口  填写 66666</param>
        /// <param name="localIP">本机IP地址    默认填写 0.0.0.0  </param>
        /// <param name="localPort">本机端口</param>
        public void Init(string desIP, int desport, string localIP, int localPort)
        {
            sendip = new IPEndPoint(IPAddress.Parse(desIP), desport);//发送数据所用的端口，TRDP默认IP为192.168.1.1
            reviceip = new IPEndPoint(IPAddress.Parse(localIP), localPort);//表示本机的指定端口，用于监听

            
            udp.Client.Bind(reviceip);
        }




        public void Connect()
        {
            Thread t = new Thread(new ThreadStart(AlwaysRead));
            t.IsBackground = true;
            t.Start();
        }

        public bool TRDPStatus()
        {
            bool a;
            try
            {
                Connect();

                a = true;
            }
            catch (Exception)
            {
                a = false;
            }
            finally
            {
                Close();
            }
            return a;
        }

        public event EventHandler Connected;
        public event ValueChangeHandler ValueChanged;
        public event RecievedHandler Recieved;
        public event EventHandler Error;
        public event ErrorHandler Errored;

        bool closeed = false;

        public void Close()
        {
            closeed = true;
            udp.Close();
        }

        public void Write(BaseSendModel send)
        {
            this.Write(send.ToBytes());
        }

        //写互斥锁，防止同时写入，导致接收数据不对齐的问题
        static object locker = new object();
        public byte[] buffer;
        public bool reds = false;//标识是否配置TRDP模块

        /// <summary>
        /// 按字节数据发送数据到TRDP模块，（串口模式）
        /// </summary>
        /// <param name="data"></param>
        public void Write(byte[] data)
        {
            lock (locker)
            {
                string txt = RWConvert.BytesToHexString(data, " ");
                // 暂时注释发送注释
                // Debug.WriteLine("TRDP模块发送：" + txt);
                // byte[] buffer = new byte[1];
                int count = udp.Send(data, data.Length, sendip);
                if (reds)
                {
                    Thread.Sleep(1000);
                    buffer = udp.Receive(ref sendip);
                }

            }
        }


        /// <summary>
        /// 按字节数据发送数据到TRDP模块， 不接收数据返回
        /// </summary>
        /// <param name="data"></param>
        public void Write_old(byte[] data)
        {
            lock (locker)
            {
                string txt = RWConvert.BytesToHexString(data, " ");
                // 暂时注释发送注释
                //Debug.WriteLine("TRDP模块发送：" + txt);
                // byte[] buffer = new byte[1];
                int count = udp.Send(data, data.Length, sendip);


            } 
        }




        public bool IsInitialized
        {
            get { return true; }
        }

        public int ReadTimeout { get; set; }

        public bool IsConnected
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 配置主帧
        /// </summary>
        /// <param name="mcu"></param>
        public byte[] SetSetting(TRDPMainSend mcu)
        {
            byte[] send = mcu.ToBytes();
            this.Write(send);

            return send;


            //bool b = dicWait[(TRDPCommandTypes)mcu.CommandType].WaitOne(ReadTimeout);

            //if (!b)
            //    throw new TimeoutException("配置主帧响应超时");
        }





        /// <summary>
        /// 配置主帧
        /// </summary>
        /// <param name="mcu"></param>
        public byte[] SetSMIting(TRDPMainSend mcu)
        {
            byte[] send = mcu.SMIToBytes();
            this.Write(send);

            return send;


            //bool b = dicWait[(TRDPCommandTypes)mcu.CommandType].WaitOne(ReadTimeout);

            //if (!b)
            //    throw new TimeoutException("配置主帧响应超时");
        }

        /// <summary>
        /// 扩张 COMID配置数据发送
        /// </summary>
        public byte[] SetExtendComid(ExtendClass extend)
        {
            byte[] send = extend.ToBytes();
            this.Write_old(send);

            return send;

        }




        public void SetDomain(bool isChannel0, string domain)
        {
            DNSSend dns = new DNSSend();
            dns.CommandType = (DNSCommandTypes)Enum.Parse(typeof(DNSCommandTypes), "Eth" + (isChannel0 ? 0 : 1));
            dns.Domain = domain;
            this.Write(dns.ToBytes());
        }

        public void SetToTCMS(ToTCMSSend send)
        {
            CacheSend = send;
            this.Write(send.ToBytes());
            dicWait[(TRDPCommandTypes)send.CommandType].Reset();
            OnError();


            //bool b = dicWait[(TRDPCommandTypes)send.CommandType].WaitOne(ReadTimeout);

            //if (!b) throw new TimeoutException("未能从TCMS接收到数据");
        }

        /// <summary>
        /// 发送数据  无数据返回方法
        /// </summary>
        /// <param name="send"></param>
        public void SetToTCMS_old(ToTCMSSend send)
        {
            CacheSend = send;
            this.Write_old(send.ToBytes());
            dicWait[(TRDPCommandTypes)send.CommandType].Reset();
            OnError();


            //bool b = dicWait[(TRDPCommandTypes)send.CommandType].WaitOne(ReadTimeout);

            //if (!b) throw new TimeoutException("未能从TCMS接收到数据");
        }
      
        public void SetToTCMS_old(byte[] send)
        {

            this.Write_old(send);
            //dicWait[(TRDPCommandTypes)send.CommandType].Reset();
            OnError();


            //bool b = dicWait[(TRDPCommandTypes)send.CommandType].WaitOne(ReadTimeout);

            //if (!b) throw new TimeoutException("未能从TCMS接收到数据");
        }





        public static ToTCMSSend CacheSend = new ToTCMSSend();

        public void OnRecieved(TRDPCommandTypes commandType, BaseRecieveModel recieved)
        {
            if (Recieved != null) Recieved(this, commandType, recieved);
        }

        public void OnError()
        {
            if (Error != null) Error(this, EventArgs.Empty);
        }

        #region IDriver 成员

        public object this[string key]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public object Read(string key)
        {
            throw new NotImplementedException();
        }

        public void Write(string key, object value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            this.Close();
        }

        #endregion

        #region IDriver 成员


        public void LoadConfig(IDriverConfig config)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public delegate void RecievedHandler(object sender, TRDPCommandTypes commandType, BaseRecieveModel recieved);

    /// <summary>
    /// 包含了所有TRDP的命令类型，主要用于通用解析
    /// 共七种类型的命令，其中四种为发送数据帧，三种为返回数据帧
    /// </summary>
    public enum TRDPCommandTypes
    {
        None = 0x00,
        //3.2+3.3  配置帧
        Setting0 = 0x05,
        Setting1 = 0x85,
        //3.3 配置应答帧
        SettingSuccess0 = 0x06,
        SettingSuccess1 = 0x86,
        //3.4 配置DNS帧
        SettingDNS0 = 0x03,
        SettingDNS1 = 0x08,
        //3.5 发送数据帧给TCMS
        SendTCMS0 = 0x09,
        SendTCMS1 = 0x89,
        //3.6 从TCMS接收数据帧
        RecieveTCMS0 = 0x07,
        RecieveTCMS1 = 0x87,
        //3.7 自由单播发送数据帧 
        SendSinglecast10 = 0x11,
        SendSinglecast20 = 0x12,
        SendSinglecast11 = 0x91,
        SendSinglecast21 = 0x92,
        //3.8 自由单播接收数据帧
        RecieveSinglecast10 = 0x21,
        RecieveSinglecast20 = 0x22,
        RecieveSinglecast11 = 0xA1,
        RecieveSinglecast21 = 0xA2,

    }
}
using BogieIdling.UI.TRDP.Model;
using MainUI;
using MetorSignalSimulator.UI.Model;
using MetorSignalSimulator.UI.SocketFile;
using Newtonsoft.Json;
using RW.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TRDP;

namespace BogieIdling.UI.TRDP
{
    public class TRDPClass
    {

        #region  TRDP模块 相关

        // 值改变后更新
        public event EventHandler<TRDPValueChangedEventArgs> KeyValueChange;

        /// <summary>
        /// 是否连接
        /// </summary>
        public bool IsConnected { get; set; }

        /// <summary>
        /// TRDP的基础配置
        /// </summary>
        TRDPConfig tRDPConfig = new TRDPConfig();

        /// <summary>
        /// 实时值更新
        /// </summary>
        public ConcurrentDictionary<string, decimal> trdpValue = new ConcurrentDictionary<string, decimal>();//存储该端口生命信号的类型，U16，还是U32

        int lifeSignal;   // 生命信号

        byte[] SendBuf;   // 所有发动的数据

        ToTCMSSend CCU_Send = null; //网关1ETH0

        TRDPDriver TRDP_CCU = null; //众志城模块 模拟CCU

        TRDPMainSend config_CCU; //CCU 配置文件
        #endregion


        public List<Ports> ports = new List<Ports>();
        public List<FullTags> tags { get; set; } = new List<FullTags>();

        public Dictionary<int, byte[]> fullData = new Dictionary<int, byte[]>();//所有要接受数据，通过端口号进行存储

        /// <summary>
        ///  CCU 发BCU数据
        /// </summary>
        public static byte[] byteSend = new byte[40];

        // 添加锁对象
        private readonly object _tagsLock = new object();
        private readonly object _portsLock = new object();

        #region 
        [DllImport("winmm")]
        static extern uint timeGetTime();

        [DllImport("winmm")]
        static extern void timeBeginPeriod(int t);

        [DllImport("winmm")]
        static extern uint timeEndPeriod(int t);

        #endregion


        /// <summary>
        /// TRDP初始化
        /// </summary>
        public bool TRDPstart()
        {
            try
            {
                tRDPConfig.Save();
                if (TRDP_CCU == null)
                {
                    TRDP_CCU = new TRDPDriver();
                    // todo 目标ip地址可能需要调整   "192.168.0.130" 为模块的固定id，不会变更
                    TRDP_CCU.Init("192.168.0.130", 6666, tRDPConfig.LocalMulticastIP, 7881); //tRDPConfig.LocalMulticastIP
                }

                CCU_Send = new ToTCMSSend("trdp");
                CCU_Send.CommandType = TOTCMSCommandTypes.Eth0;

                config_CCU = new TRDPMainSend("trdp");

                //配置主帧数据发送
                TRDP_CCU.SetSetting(config_CCU);
                Thread.Sleep(50);

                //监听数据返回
                TRDP_CCU.Connect();
                TRDP_CCU.Recieved += new RecievedHandler(trdp_Recieved);

                //数据发送线程 启动
                Timestart();

                // 检测是否连接成功
                CheckConnect();

                return true;
            }
            catch (Exception ex)
            {
                Var.LogInfo($"TRDP连接异常{ex.ToString()}");
                return false;
            }
        }

        Thread timerthread_30;

        /// <summary>
        /// 发送
        /// </summary>
        public void Timestart()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            timerthread_30 = new Thread(timer_30);
            timeBeginPeriod(1);
            timerthread_30.Start();
        }

        /// <summary>
        /// 初始化Tag（TRDP字典）
        /// </summary>
        public void InitTag()
        {
            lock (_portsLock)
            {
                Var.TRDP.ports.Clear();
            }

            lock (_tagsLock)
            {
                Var.TRDP.tags.Clear();
            }

            // 连接TRDP
            string json = File.ReadAllText(Application.StartupPath + "\\config\\trdpTest.txt", Encoding.UTF8);//测试，
            Agreement data = JsonConvert.DeserializeObject<Agreement>(json);

            // 临时列表用于存储新数据
            var newPorts = new List<Ports>();
            var newTags = new List<FullTags>();

            foreach (var item in data.data)
            {
                if (item.signalName == null)
                {
                    continue;
                }

                FullTags ft = new FullTags();
                ft.ID = item.id;
                if (item.dataFormat == "" || item.dataFormat == "0" || item.dataFormat == null)
                {
                    ft.dataFormat = 1;
                }
                else
                {
                    ft.dataFormat = decimal.Parse(item.dataFormat);
                }
                ft.RawLow = item.RawLow;
                ft.RawHight = item.RawHight;
                ft.ScaledLow = item.ScaledLow;
                ft.ScaledHight = item.ScaledHight;
                ft.DataLabel = item.signalName;
                ft.DataType = item.dataType;
                ft.DataRange = item.vhecileNo;
                ft.Description = item.carNo;
                ft.guzhangfenlei = item.yuLiu10;
                ft.Identity = item.signalName.Contains("生命信号");
                ft.TxType = item.yuLiu5.Contains("以太网") ? "以太网" : "MVB";
                ft.comID = Convert.ToInt32(item.yuLiu2);
                COMMData cd = new COMMData();
                cd.Port = Convert.ToInt32(item.yuLiu2, 16);
                cd.Offset = item.byteOffset;
                cd.Bit = item.binaryOffset;
                ft.COMMData = cd;
                if (item.yuLiu5.Contains("以太网"))
                    newTags.Add(ft);

                AddPorts(item);
            }

            // 替换集合内容
            lock (_portsLock)
            {
                Var.TRDP.ports.Clear();
                Var.TRDP.ports.AddRange(newPorts);
            }

            lock (_tagsLock)
            {
                Var.TRDP.tags.Clear();
                Var.TRDP.tags.AddRange(newTags);
            }
        }

        /// <summary>
        /// 加载excel模板
        /// </summary>
        /// <param name="xlsFile"></param>
        /// <returns></returns>
        public string InitExcel(string xlsFile)
        {
            try
            {
                List<ExcelModel> lstXLSmodel = new List<ExcelModel>();

                //根据Excel文件列名，检索列名，判断文件是否正确 ？
                var requiredColumns = new List<string> { "SignalName", "DataType", "ByteOffset", "BitOffset", "Scale", "RawHight", "RawLow", "ScaledHight", "ScaledLow" };
                var actualColumnNames = MiniExcelLibs.MiniExcel.GetColumns(xlsFile, true).ToList();
                // 检查是否包含所有必需列
                bool allRequiredExists = requiredColumns.All(required => actualColumnNames.Contains(required));
                if (!allRequiredExists)
                {
                    // 找出缺失的列名，便于提示用户
                    var missingCols = requiredColumns.Where(required => !actualColumnNames.Contains(required));
                    string msg = $"请选择正确的TRDP配置Excel文件。当前Excel文件缺少必需列：{string.Join("、", missingCols)}";
                    Console.WriteLine($"错误：Excel文件缺少必需列：{string.Join("、", missingCols)}");
                    return msg;
                }

                //加载===================================================
                lstXLSmodel = MiniExcelLibs.MiniExcel.Query<ExcelModel>(xlsFile, "Sheet1").ToList();

                List<AgreementData> lst = new List<AgreementData>();
                foreach (ExcelModel model in lstXLSmodel)
                {
                    AgreementData ad = new AgreementData();
                    ad.signalName = model.SignalName;
                    ad.dataType = model.DataType;
                    ad.dataFormat = model.Scale;
                    ad.RawLow = model.RawLow;
                    ad.RawHight = model.RawHight;
                    ad.ScaledLow = model.ScaledLow;
                    ad.ScaledHight = model.ScaledHight;

                    ad.yuLiu2 = Var.tRDPConfig.ReceiveCOMID.ToString();
                    ad.yuLiu5 = model.yuLiu5;
                    ad.byteOffset = model.ByteOffset;
                    ad.binaryOffset = model.BitOffset;
                    ad.yuLiu4 = model.yuLiu4; //通讯周期
                    ad.yuLiu3 = model.yuLiu3;
                    ad.yuLiu8 = Var.tRDPConfig.ReceiveCOMID.ToString(); //SMI 码
                    ad.yuLiu6 = model.yuLiu6;
                    ad.yuLiu11 = model.yuLiu11;
                    lst.Add(ad);
                }

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(lst, Newtonsoft.Json.Formatting.Indented);
                string str = "{\"msg\":\"数据获取成功!\",\"total\":1,\"equipTypeCode\":\"1\",\"data\":" + json + ",\"projectNumber\":\"1\",\"state\":\"1\"}";
                File.WriteAllText(Application.StartupPath + "\\config\\trdpTest.txt", str);

                // 初始化字典
                // todo 后续还需更新2屏页面
                InitTag();

                return "初始化成功";
            }
            catch (Exception ex)
            {
                string err = "" + ex.Message;
                return err;
            }
        }

        /// <summary>
        /// Timer方法
        /// </summary>
        private void timer_30()
        {
            uint timerstart = timeGetTime();
            while (true)
            {
                uint i = 0;
                while (i < 100)     //N为时间间隔（ms） 
                {
                    i = timeGetTime() - timerstart;
                }
                timerstart = timeGetTime();

                Tcmslife_30();               //需要循环运行的函数；   
            }
        }


        public void Tcmslife_30()
        {
            try
            {
                // return;
                lifeSignal++;

                if (lifeSignal > 255)
                    lifeSignal = 0;

                int tmp = Convert.ToInt32(lifeSignal);


                SendBuf = VarHelperETH.GetLifeBytes_ZNCG(tmp);
                CCU_Send.SequenceCounter++;

                CCU_Send.DatasetData = SendBuf;// TRDP_NJCJ.CCU_ALL;
                CCU_Send.DatasetLength = SendBuf.Length;// TRDP_NJCJ.CCU_ALL.Length;
                CCU_Send.DataLength = CCU_Send.DatasetLength + 20;//

                CCU_Send.ComId = tRDPConfig.SendCOMID;
                TRDP_CCU.SetToTCMS_old(CCU_Send);

                //CCU_Send.ComId = 41070;
                //TRDP_CCU.SetToTCMS_old(CCU_Send);
            }
            catch (Exception ex)
            {
                string err = "自动发送生命信号有误，具体原因：" + ex.Message;
                Var.LogInfo(err);
            }

        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Timestop()
        {
            try
            {
                if (timerthread_30 != null)
                {
                    timerthread_30.Abort();
                    timeEndPeriod(1);
                }
            }
            catch (Exception ex)
            {
                Var.LogInfo($"TRDP 关闭失败 {ex.ToString()}");
            }

        }


        /// <summary>
        /// 接受TRDP数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="commandType"></param>
        /// <param name="recieved"></param>
        private void trdp_Recieved(object sender, TRDPCommandTypes commandType, BogieIdling.UI.TRDP.Model.BaseRecieveModel recieved)
        {
            if (commandType == TRDPCommandTypes.RecieveTCMS0 || commandType == TRDPCommandTypes.RecieveTCMS1)//只有从TCMS接收的数据才能
            {
                FromTCMSRecieve tcms = recieved as FromTCMSRecieve;
                if (tcms.ComId == tRDPConfig.ReceiveCOMID)
                {
                    VarHelperETH.BCU_CCU = tcms.DatasetData;

                    UpdateTagsRealTimeData(tcms.ComId, VarHelperETH.BCU_CCU);
                }
            }
        }

        /// <summary>
        /// 使用实时数据更新对应端口的所有标签
        /// </summary>
        public void UpdateTagsRealTimeData(int port, byte[] ByteData)
        {
            Array.Copy(ByteData, 0, fullData[port], 0, ByteData.Length);//正式
            foreach (var tag in tags)
            {
                try
                {
                    // 使用现有的 GetByte 方法解析数据
                    decimal realTimeValue = GetByte(port, tag.Offset, tag.Bit, tag.DataType, tag.dataFormat);
                    if (tag.RawHight != 0)
                    {
                        // 如果原始数据大于原始高 说明数据不可取，直接置为0
                        if (realTimeValue > tag.RawHight)
                        {
                            realTimeValue = 0;
                        }
                        else
                        {
                            // 线性换算公式：工程值 = (原始值 - 原始低) / (原始高 - 原始低) * (工程高 - 工程低) + 工程低
                            realTimeValue = (realTimeValue - tag.RawLow) / (tag.RawHight - tag.RawLow) * (tag.ScaledHight - tag.ScaledLow) + tag.ScaledLow;
                        }
                    }

                    // 给特殊的标签调整数值
                    if (tag.DataLabel == "前增压器转速" || tag.DataLabel == "后增压器转速")
                    {
                        // 增压器转速低于80转，直接设置为0
                        if (realTimeValue < 80)
                        {
                            realTimeValue = 0;
                        }
                    }

                    // 更新标签的实时值
                    tag.RealTimeValue = realTimeValue;
                    tag.LastUpdateTime = DateTime.Now;
                    tag.IsValueUpdated = true;

                    // 触发值改变事件
                    OnTagValueChanged(tag, realTimeValue);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"更新标签{tag.DataLabel}数据失败: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 获取存储的数据
        /// </summary>
        /// <returns></returns>
        public double GetDicValue(string key)
        {
            try
            {
                //读取字典
                if (trdpValue.TryGetValue(key, out var sectionDict))
                {
                    return sectionDict.ToDouble();
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// TRDP读取值
        /// </summary>
        /// <param name="targetNum"></param>
        /// <param name="bitNum"></param>
        /// <returns></returns>
        public decimal GetByte(int port, int targetNum, int bit, string DataType, decimal dataFormat)
        {
            // 暂时注释，已经在解析数组的时候就已经换算了，所以不在此处进行16进制换算。
            //port = int.Parse(Convert.ToString(port, 16));
            decimal value = 0;
            if (!fullData.ContainsKey(port))
                return 0;
            GetVal(ref value, DataType, targetNum, bit, fullData[port], 0, port, dataFormat);
            return value;
        }

        public void GetVal(ref decimal value, string dataType, int offset, int Bit, byte[] data, int bits, int port, decimal dataFormat)
        {
            byte[] temp = null;
            //offset = ConvertBit(offset, Bit)[0];
            //Bit = ConvertBit(offset, Bit)[1];
            switch (dataType)
            {
                case "B1":
                    value = GetByBit(port, offset, Bit) ? 1 : 0;
                    break;
                case "U8":
                    offset += Bit;
                    value = data[offset] * dataFormat;
                    break;
                case "I8":
                    value = (sbyte)data[offset] * dataFormat;//TODO：请注意，此处负数的处理
                    break;
                case "U16":
                    temp = new byte[2];
                    Array.Copy(data, offset, temp, bits, temp.Length);
                    value = BitConverter.ToUInt16(temp.Reverse().ToArray(), 0) * dataFormat;
                    break;
                case "I16":
                    temp = new byte[2];
                    Array.Copy(data, offset, temp, bits, temp.Length);
                    value = BitConverter.ToInt16(temp.Reverse().ToArray(), 0) * dataFormat;
                    break;
                case "U32":
                    temp = new byte[4];
                    Array.Copy(data, offset, temp, bits, temp.Length);
                    value = BitConverter.ToUInt32(temp.Reverse().ToArray(), 0) * dataFormat;
                    break;
                case "I32":
                    temp = new byte[4];
                    Array.Copy(data, offset, temp, bits, temp.Length);
                    value = BitConverter.ToInt32(temp.Reverse().ToArray(), 0) * dataFormat;
                    break;
                case "U64":
                    temp = new byte[8];
                    Array.Copy(data, offset, temp, bits, temp.Length);
                    value = BitConverter.ToUInt64(temp.Reverse().ToArray(), 0) * dataFormat;
                    break;
                case "I64":
                    temp = new byte[8];
                    Array.Copy(data, offset, temp, bits, temp.Length);
                    value = BitConverter.ToInt64(temp.Reverse().ToArray(), 0) * dataFormat;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// TRDP读取位
        /// </summary>
        /// <param name="targetNum"></param>
        /// <param name="bitNum"></param>
        /// <returns></returns>
        public bool GetByBit(int port, int targetNum, int bitNum)
        {
            byte value = fullData[port][targetNum];
            int tmpInt = 1 << bitNum;
            return (value & tmpInt) / tmpInt == 0 ? false : true;
        }

        /// <summary>
        /// 将字偏移装换为字节偏移
        /// </summary>
        /// <param name="offset">字偏移</param>
        /// <param name="bit">位偏移</param>
        /// <returns></returns>
        public List<int> ConvertBit(int offset, int bit)
        {
            return new List<int>() { offset * 2 + bit / 8, bit % 8 };
        }

        /// <summary>
        /// 触发标签值改变事件
        /// </summary>
        private void OnTagValueChanged(FullTags tag, decimal newValue)
        {
            // 事件触发
            trdpValue.AddOrUpdate(tag.DataLabel, newValue, (k, oldValue) => newValue);

            KeyValueChange?.Invoke(this, new TRDPValueChangedEventArgs(tag, tag.DataLabel, newValue));
        }

        /// <summary>
        /// 添加端口
        /// </summary>
        public void AddPorts(AgreementData item)
        {
            Ports pt = new Ports();
            pt.ID = item.id;
            pt.Port = item.yuLiu2;
            pt.IsRead = !item.yuLiu6.Contains("源");
            pt.IsUse = true;
            pt.Rate = int.Parse(item.yuLiu4);
            pt.DataSize = int.Parse(item.yuLiu3);
            pt.SMIValue = string.IsNullOrEmpty(item.yuLiu8) ? 0 : int.Parse(item.yuLiu8);
            pt.duankoumingcheng = string.IsNullOrEmpty(item.yuLiu11) ? null : item.yuLiu11;

            if (item.yuLiu5.Contains("以太网"))
            {
                if (ports.FirstOrDefault(x => x.Port.Equals(item.yuLiu2)) == null)
                {
                    pt.MulticastAddress = pt.MulticastAddress;
                    ports.Add(pt);
                    fullData[pt.PortNum1] = new byte[pt.DataSize];
                }
            }
        }

        /// <summary>
        /// 读数据计时
        /// </summary>
        public void CheckConnect()
        {
            Thread rt = new Thread(new ThreadStart(delegate
            {
                while (true)
                {
                    try
                    {
                        // 如果掉线4秒，则连接中断
                        if (tags[0].LastUpdateTime < DateTime.Now.AddSeconds(-3))
                        {
                            IsConnected = false;
                        }
                        else
                        {
                            IsConnected = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                    Thread.Sleep(1000);
                }
            }));
            rt.IsBackground = true;
            rt.Name = "TRDP_Read_Time";
            rt.Start();
        }

    }

    public class TRDPValueChangedEventArgs : EventArgs
    {
        public FullTags Tags { get; }
        public string Key { get; }
        public decimal Value { get; }

        public TRDPValueChangedEventArgs(FullTags tags, string key, decimal value)
        {
            Tags = tags;
            Key = key;
            Value = value;
        }
    }
}

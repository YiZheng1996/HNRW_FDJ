using MainUI.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using MainUI.FSql.Model;

namespace MainUI.FSql
{
    /// <summary>
    /// 手动记录数据
    /// </summary>
    public class ManualRecordService
    {
        /// <summary>
        /// 单例模式
        /// </summary>

        public static ManualRecordService instnce = new ManualRecordService();

        // 计时频率
        public System.Timers.Timer timerRecord = new System.Timers.Timer();

        /// <summary>
        /// 记录主表id
        /// </summary>
        public string MGid { get; set; }

        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 记录间隔时间
        /// </summary>
        public int Second { get; set; } = 1;

        /// <summary>
        /// 记录状态
        /// </summary>
        public bool IsRunning { get; set; } = false;

        /// <summary>
        /// 记录的序号
        /// </summary>
        public int Index { get; set; } = 1;

        //单条数据保存完成的事件委托
        public delegate void DataSavedEventHandler(object sender, DataSavedEventArgs e);
        public event DataSavedEventHandler DataSaved;

        // 传递保存数据对象
        public class DataSavedEventArgs : EventArgs
        {
            public ManualRecordPara manualRecord { get; set; }
        }

        /// <summary>
        /// 开始记录
        /// </summary>
        public bool StartRecord(Dictionary<string, string> saveInfo)
        {
            IsRunning = true;
            Second = saveInfo["recordFrequency"].ToInt();
            Index = saveInfo["MaxIndex"].ToInt();

            // 默认为""，或者清空数据把 MGid == ""
            if (string.IsNullOrEmpty(MGid))
            {
                // 先记录主表id，如果操作数据库失败，则不需要再进入线程
                MGid = Guid.NewGuid().ToString("N");
                ManualRecordMain manualRecordMain = new ManualRecordMain()
                {
                    gid = MGid, // 生成正整数ID
                    BeginTime = DateTime.Now,
                    UserName = RW.UI.RWUser.User.Username,
                    DieselEngineModel = saveInfo["Model"],
                    DieselEngineNo = saveInfo["No"],
                    TestName = saveInfo["TestName"]
                };
                var result = SaveMain(manualRecordMain);
                if (result == 0)
                {
                    IsSuccess = false;
                    // 数据库操作异常了
                    return IsSuccess;
                }
            }
            IsSuccess = true;

            Thread thread = new Thread(new ThreadStart(() =>
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                int currentSecond = Second; // 保存当前周期值
                long targetIntervalMs = currentSecond * 1000;

                while (IsRunning)
                {
                    long startTime = stopwatch.ElapsedMilliseconds;
                    try
                    {
                        // 进气压降
                        var frontPressure = Common.AI2Grp["前中冷前空气压力"] - Common.AI2Grp["前中冷后空气压力"];
                        var afterPressure = Common.AI2Grp["后中冷前空气压力"] - Common.AI2Grp["后中冷后空气压力"];

                        string recordName = $"";
                        if (MiddleData.instnce.testDataView.IsTest) 
                        {
                            if (MiddleData.instnce.testDataView.TestName == "性能试验")
                            {
                                recordName = $"{MiddleData.instnce.CurrentStatusData.NodeName}-{MiddleData.instnce.CurrentStatusData.StepTime}-{MiddleData.instnce.testDataView.Second}";
                            }
                            else {
                                recordName = $"{MiddleData.instnce.CurrentStatusData.PhaseName}-{MiddleData.instnce.CurrentStatusData.CycleName}-{MiddleData.instnce.CurrentStatusData.NodeNameAccumulate}{MiddleData.instnce.CurrentStatusData.StepTime}";
                            }
                        }
                        else
                        {
                            recordName = "手动记录";
                        }

                        var testData = new ManualRecordPara
                        {
                            gid = Guid.NewGuid().ToString("N"), // 生成正整数ID
                            mgid = MGid,
                            Index = Index++,
                            DieselEngineModel = Var.SysConfig.LastModel,     //柴油机型号
                            DieselEngineNo = Common.mTestViewModel.ModelNo,  //柴油机编号
                            RecordName = recordName,//交变-4-1 表示在交变负荷试验4分钟工况时的第一次记录  Ⅰ-1-1A010 表示在第一阶段第一周期第1A循环的第10分钟记录参数。
                            TestName = MiddleData.instnce.testDataView.TestName,//性能测试
                            TestStage = MiddleData.instnce.CurrentStatusData.PhaseName,//Ⅰ，Ⅱ，Ⅲ
                            TestCycle = MiddleData.instnce.CurrentStatusData.CycleName,//1.2.3.4.5 （最长5个）
                            TestStep = MiddleData.instnce.CurrentStatusData.NodeName,//A,B,C,D,E.  交变.标定 ....
                            DataTime = DateTime.Now.ToString("yyyy-MM-dd"),
                            Time = DateTime.Now.ToString("HH:mm:ss"),
                            RecordDataTime = DateTime.Now,
                            HourNum = MiddleData.instnce.RunHour, // 运行小时数
                            AT = Common.AIgrp["大气温度"],    // 1 环境温度
                            AP = Common.AIgrp["大气压力"],  // 2 大气压力
                            AH = Common.AIgrp["大气湿度"],   // 3 空气湿度
                            RPM = MiddleData.instnce.EngineSpeed,  // 4 柴油机转速
                            Torque = MiddleData.instnce.EngineTorque, // 5 柴油机有效扭矩
                            Power = MiddleData.instnce.EnginePower, // 6 柴油机有效功率

                            LWaterTempIn = Common.waterGrp["中冷水冷却器进口温度检测-T14"],//7 中冷水进机温度
                            HWaterTempIn = Common.waterGrp["高温水冷却器进口温度检测-T13"], // 8 高温水进机温度
                            LWaterTempOut = Common.waterGrp["中冷水温度实时PID"],  // 9 中冷水出机温度
                            HWaterTempOut = Common.waterGrp["高温水温度实时PID"],  // 10 高温水出机温度


                            EngineWaterTempIn = 0,//Common.engineOilGrp[""], // 11 机油热交换器进口水温 未找到点
                            EngineWaterTempOut = 0,//Common.engineOilGrp[""],// 12 机油热交换器出口水温 未找到点


                            // 压力
                            LPressureIn = Common.AI2Grp["P3中冷水泵进口压力"],  // 13 中冷水泵进口压力
                            HPressureIn = Common.AI2Grp["P2高温水泵进口压力"],  // 14 高温水泵进口压力
                            LPressureOut = Common.AI2Grp["P5中冷水出机压力"],  // 15 中冷水泵出口压力
                            HPressureOut = Common.AI2Grp["P1高温水出机压力"], // 16 高温水泵出口压力
                            LWaterPressureOut = Common.AI2Grp["P5中冷水出机压力"], // 17 中冷水出机压力
                            HWaterPressureOut = Common.AI2Grp["P1高温水出机压力"], // 18 高温水出机压力

                            // 机油温度
                            HeatExchangerTempIn = Common.engineOilGrp["冷却器进口油温-T25"],   // 19 机油热交换器进口油温
                            HeatExchangerTempOut = Common.engineOilGrp["机油温度实时PID"],    // 20 机油热交换器出口油温
                            EOPressure2 = Common.AI2Grp["主油道末端油压"],               // 21 主油道末端油压        
                            EOPressure1 = Common.AI2Grp["P20机油泵出口压力"],            // 22 机油泵出口油压
                            EngineOilOutletTemp = Common.AI2Grp["T20机油泵出口油温"],  // 23 机油泵出口油温

                            //机油数据分析
                            EOAnalysis = 0,     // 24 机油分析，缺失
                            EOConsumption = 0,  // 25 机油消耗，缺失

                            //空气温度
                            FrontAirTempIn = Common.AI2Grp["前中冷前空气温度"],     // 26 前中冷前空气温度   
                            AfterAirTempIn = Common.AI2Grp["后中冷前空气温度"],     // 27 后中冷前空气温度
                            FrontAirTempOut = Common.AI2Grp["前中冷后空气温度"],    // 28 前中冷后空气温度
                            AfterAirTempOut = Common.AI2Grp["后中冷后空气温度"],    // 29 后中冷后空气温度

                            //空气压力
                            FrontAirPressureIn = Common.AI2Grp["前中冷前空气压力"], // 30 前中冷前空气压力
                            AfterAirPressureIn = Common.AI2Grp["后中冷前空气压力"], // 31 后中冷前空气压力
                            FrontAirPressureOut = Common.AI2Grp["前中冷后空气压力"],// 32 前中冷后空气压力
                            AfterAirPressureOut = Common.AI2Grp["后中冷后空气压力"],// 33 后中冷后空气压力

                            //增压器转速
                            FrontTurbochargerRPM = Var.TRDP.GetDicValue("前增压器转速"),  // 34 前增压器转速
                            AfterTurbochargerRPM = Var.TRDP.GetDicValue("后增压器转速"),  // 35 后增压器转速

                            // 排气压力
                            FrontTurbochargerPressureIn = Common.AI2Grp["前增压器进气真空度"],       // 36 前增压器进气真空度
                            AfterTurbochargerPressureIn = Common.AI2Grp["后增压器进气真空度"],       // 37 后增压器进气真空度
                            FrontTurbochargerPressureOut = Common.AI2Grp["前增压器排气背压"],       // 38 前增压器排气背压
                            AfterTurbochargerPressureOut = Common.AI2Grp["后增压器排气背压"],       // 39 后增压器排气背压

                            //缸体温度
                            EGTempA1 = Common.AI2Grp["A1缸排气温度"],      // 40 A1缸排气温度
                            EGTempA2 = Common.AI2Grp["A2缸排气温度"],      // 41 A2缸排气温度
                            EGTempA3 = Common.AI2Grp["A3缸排气温度"],      // 42 A3缸排气温度
                            EGTempA4 = Common.AI2Grp["A4缸排气温度"],      // 43 A4缸排气温度
                            EGTempA5 = Common.AI2Grp["A5缸排气温度"],      // 44 A5缸排气温度
                            EGTempA6 = Common.AI2Grp["A6缸排气温度"],      // 45 A6缸排气温度
                            EGTempA7 = Common.AI2Grp["A7缸排气温度"],      // 46 A7缸排气温度
                            EGTempA8 = Common.AI2Grp["A8缸排气温度"],      // 47 A8缸排气温度
                            EGTempB1 = Common.AI2Grp["B1缸排气温度"],      // 48 B1缸排气温度
                            EGTempB2 = Common.AI2Grp["B2缸排气温度"],      // 49 B2缸排气温度
                            EGTempB3 = Common.AI2Grp["B3缸排气温度"],      // 50 B3缸排气温度
                            EGTempB4 = Common.AI2Grp["B4缸排气温度"],      // 51 B4缸排气温度
                            EGTempB5 = Common.AI2Grp["B5缸排气温度"],      // 52 B5缸排气温度
                            EGTempB6 = Common.AI2Grp["B6缸排气温度"],      // 53 B6缸排气温度
                            EGTempB7 = Common.AI2Grp["B7缸排气温度"],      // 54 B7缸排气温度
                            EGTempB8 = Common.AI2Grp["B8缸排气温度"],      // 55 B8缸排气温度

                            // 排气温度
                            FrontTurbochargerTempIn = Common.AI2Grp["前涡轮进口废气温度"],       // 56 前涡轮进口废气温度
                            AfterTurbochargerTempIn = Common.AI2Grp["后涡轮进口废气温度"],       // 57 后涡轮进口废气温度
                            FrontTurbochargerTempOut = Common.AI2Grp["前涡轮出口废气温度"],      // 58 前涡轮出口废气温度
                            AfterTurbochargerTempOut = Common.AI2Grp["后涡轮出口废气温度"],      // 59 后涡轮出口废气温度

                            //废气压力
                            FrontTurbochargerPressureIn2 = Common.AI2Grp["前涡轮进口废气压力"],     // 60 前涡轮进口废气压力    
                            AfterTurbochargerPressureIn2 = Common.AI2Grp["后涡轮进口废气压力"],     // 61 后涡轮进口废气压力

                            //其他
                            Smoke = 0,      // 62 烟度，缺失
                            ECOTime = 0,    // 63 燃油消耗时间，缺失
                            ECOQuantity = 0,// 64 燃油消耗量，缺失
                            ECORate = 0,    // 65 燃油消耗率，缺失
                            OilTempIn = 0,  // 66 燃油泵进口油温，缺失
                            InjectionParameter = 0, // 67 喷射参数，缺失
                        };
                        Save(testData);
                        // 数据保存后触发
                        OnDataSaved(testData);

                        long endTime = stopwatch.ElapsedMilliseconds;
                        long elapsed = endTime - startTime;
                        long sleepTime = Second * 1000 - elapsed;

                        if (sleepTime > 0)
                        {
                            Thread.Sleep((int)sleepTime);
                        }
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(1000);
                        Debug.WriteLine($"手动采集进入数据库异常 每秒记录数据库出现异常： {ex.ToString()}");
                        continue;
                        //Var.LogInfo($"每秒记录数据库出现异常： {ex.ToString()}");
                    }

                    stopwatch.Stop();
                }

                try
                {
                    // 更新主表的结束时间
                    DB.mysql.Update<ManualRecordMain>()
                    .Where(d => d.gid == MGid)
                    .Set(d => d.EndTime, DateTime.UtcNow)
                    .ExecuteAffrows();
                }
                catch (Exception ex)
                {

                }
            }));
            thread.IsBackground = true;
            thread.Name = "记录数据后台线程";
            thread.Start();

            return IsSuccess;
        }

        /// <summary>
        /// 暂停记录
        /// </summary>
        public void StopRecord()
        {
            IsRunning = false;
        }

        /// <summary>
        /// 保存后传递对象
        /// </summary>
        /// <param name="recordGid"></param>
        protected virtual void OnDataSaved(ManualRecordPara recordData)
        {
            DataSaved?.Invoke(this, new DataSavedEventArgs
            {
                manualRecord = recordData
            });
        }

        /// <summary>
        /// 保存TestParaALL记录
        /// </summary>
        /// <param name="testParaAll">TestParaALL对象</param>
        /// <returns>受影响的行数</returns>
        public int SaveTestParaALL(TestParaALL testParaAll)
        {
            try
            {
                return DB.mysql.Insert<TestParaALL>(testParaAll).ExecuteAffrows();
            }
            catch (Exception ex)
            {
                throw new Exception("TestParaALL记录保存失败！原因： " + ex.Message);
            }
        }

        /// <summary>
        /// 开始记录所有模块数据
        /// </summary>
        public void StartRecordAll()
        {
            IsRunning = true;
            Thread thread = new Thread(new ThreadStart(() =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                while (IsRunning)
                {
                    try
                    {
                        long startTime = stopwatch.ElapsedMilliseconds;

                        // 同步采集所有模块数据
                        var allModuleData = CollectAllModuleData();

                        // 保存所有模块的实时数据到TestParaALL表
                        SaveAllModuleDataToTestParaALL(allModuleData);

                        long endTime = stopwatch.ElapsedMilliseconds;
                        long elapsed = endTime - startTime;
                        long sleepTime = Second * 1000 - elapsed - 1;

                        if (sleepTime > 0)
                        {
                            Thread.Sleep((int)sleepTime);
                        }
                    }
                    catch (Exception ex)
                    {
                        Var.LogInfo($"每秒记录数据库出现异常： {ex.ToString()}");
                    }
                }

                stopwatch.Stop();
            }));
            thread.IsBackground = true;
            thread.Name = "记录所有数据后台线程";
            thread.Start();
        }

        /// <summary>
        /// 保存所有模块数据到TestParaALL表
        /// </summary>
        /// <param name="allModuleData">所有模块数据字典</param>
        private void SaveAllModuleDataToTestParaALL(Dictionary<string, object> allModuleData)
        {
            try
            {
                // 将字典序列化为JSON字符串
                string monitorDataJson = SerializeModuleData(allModuleData);

                // 创建TestParaALL记录
                var testParaAll = new TestParaALL
                {
                    gid = Guid.NewGuid().ToString(),
                    Index = Index++, // 使用与TestPara相同的Index
                    RecordName = "",
                    TestName = "",
                    TestStage = "",
                    TestCycle = "",
                    TestStep = "",
                    DataTime = DateTime.Now.ToString("yyyy-MM-dd"),
                    Time = DateTime.Now.ToString("HH:mm:ss"),
                    HourNum = 0,
                    RecordDataTime = DateTime.Now,
                    MonitorData = monitorDataJson
                };

                // 保存到数据库
                int result = SaveTestParaALL(testParaAll);

                Var.LogInfo($"所有模块数据已保存到TestParaALL，Index: {testParaAll.Index}, 数据大小: {monitorDataJson.Length} bytes");
            }
            catch (Exception ex)
            {
                Var.LogInfo($"保存所有模块数据到TestParaALL异常：{ex.Message}");
            }
        }

        /// <summary>
        /// 序列化模块数据为JSON字符串
        /// </summary>
        private string SerializeModuleData(Dictionary<string, object> allModuleData)
        {
            try
            {
                // 创建序列化设置
                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.None, // 不格式化以减少存储空间
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    DateFormatString = "yyyy-MM-dd HH:mm:ss.fff"
                };

                return JsonConvert.SerializeObject(allModuleData, settings);
            }
            catch (Exception ex)
            {
                Var.LogInfo($"序列化模块数据异常：{ex.Message}");
                return "{}";
            }
        }

        /// <summary>
        /// 采集所有模块的实时数据
        /// </summary>
        /// <returns>包含所有模块数据的字典</returns>
        private Dictionary<string, object> CollectAllModuleData()
        {
            var allData = new Dictionary<string, object>();
            var timestamp = DateTime.Now;

            try
            {
                // 基础模块数据
                if (MiddleData.instnce != null)
                {
                    Dictionary<string, object> BaseDic = new Dictionary<string, object>();
                    BaseDic.Add($"Base_Weight", (object)MiddleData.instnce.PTFWeight);
                    BaseDic.Add($"Base_EngineSpeed", (object)MiddleData.instnce.EngineSpeed);
                    BaseDic.Add($"Base_EngineTorque", MiddleData.instnce.EngineTorque);
                    BaseDic.Add($"Base_EnginePower", MiddleData.instnce.EnginePower);
                    allData["BaseGrp"] = BaseDic;
                }

                // TRDP数据
                if (Var.TRDP.trdpValue != null)
                {
                    var aiData = Var.TRDP.trdpValue.ToDictionary(kv => $"TRDP_{kv.Key}", kv => (object)kv.Value);
                    allData["TRDP"] = aiData;
                }

                // AI模块数据
                if (Common.AIgrp?.AIListData != null)
                {
                    var aiData = Common.AIgrp.AIListData.ToDictionary(kv => $"AI_{kv.Key}", kv => (object)kv.Value);
                    allData["AIGrp"] = aiData;
                }

                // AO模块数据
                if (Common.AOgrp?.AOListData != null)
                {
                    var aoData = Common.AOgrp.AOListData.ToDictionary(kv => $"AO_{kv.Key}", kv => (object)kv.Value);
                    allData["AOGrp"] = aoData;
                }

                // DI模块数据
                if (Common.DIgrp?.DataValue != null)
                {
                    var diData = Common.DIgrp.DataValue.ToDictionary(kv => $"DI_{kv.Key}", kv => (object)kv.Value);
                    allData["DIGrp"] = diData;
                }

                // DO模块数据
                if (Common.DOgrp?.DataValue != null)
                {
                    var doData = Common.DOgrp.DataValue.ToDictionary(kv => $"DO_{kv.Key}", kv => (object)kv.Value);
                    allData["DOGrp"] = doData;
                }

                // 数据交互模块
                if (Common.ExChangeGrp != null)
                {
                    var exchangeData = Common.ExChangeGrp._doubles.ToDictionary(kv => $"ExChangeGrpDouble_{kv.Key}", kv => (object)kv.Value);
                    // 这里需要根据ExChangeGrp的实际数据结构来获取数据
                    allData["ExChangeGrpDouble"] = exchangeData;

                    var exchangeDataBool = Common.ExChangeGrp._bools.ToDictionary(kv => $"ExChangeGrpBool_{kv.Key}", kv => (object)kv.Value);
                    // 这里需要根据ExChangeGrp的实际数据结构来获取数据
                    allData["ExChangeGrpBool"] = exchangeDataBool;
                }

                // 故障检测模块
                if (Common.PipelineFaultGrp?.DataValue != null)
                {
                    var faultData = Common.PipelineFaultGrp.DataValue.ToDictionary(kv => $"Fault_{kv.Key}", kv => (object)kv.Value);
                    allData["PipelineFaultGrp"] = faultData;
                }

                // 机油系统模块
                if (Common.engineOilGrp?.NewDataValue != null)
                {
                    var oilData = Common.engineOilGrp.NewDataValue.ToDictionary(kv => $"Oil_{kv.Key}", kv => (object)kv.Value);
                    allData["EngineOilGrp"] = oilData;
                }

                // 燃油系统模块
                if (Common.fuelGrp?.NewDataValue != null)
                {
                    var fuelData = Common.fuelGrp.NewDataValue.ToDictionary(kv => $"Fuel_{kv.Key}", kv => (object)kv.Value);
                    allData["FuelGrp"] = fuelData;
                }

                // 三相电模块
                if (Common.threePhaseElectric?.DataValue != null)
                {
                    var electricData = Common.threePhaseElectric.DataValue.ToDictionary(kv => $"Electric_{kv.Key}", kv => (object)kv.Value);
                    allData["ThreePhaseElectric"] = electricData;
                }

                // 水系统模块
                if (Common.waterGrp?.NewDataValue != null)
                {
                    var waterData = Common.waterGrp.NewDataValue.ToDictionary(kv => $"Water_{kv.Key}", kv => (object)kv.Value);
                    allData["WaterGrp"] = waterData;
                }

                // PLC2 AI模块
                if (Common.AI2Grp?.AIListData != null)
                {
                    var ai2Data = Common.AI2Grp.AIListData.ToDictionary(kv => $"AI2_{kv.Key}", kv => (object)kv.Value);
                    allData["PLC2AIGrp"] = ai2Data;
                }

                // 启动PLC模块
                if (Common.startPLCGrp != null)
                {
                    var startPLCData = Common.startPLCGrp.DataValue.ToDictionary(kv => $"StartPLC_{kv.Key}", kv => (object)kv.Value);
                    // 这里需要根据StartPLCGrp的实际数据结构来获取数据
                    allData["StartPLCGrp"] = startPLCData;
                }

                // 转速模块
                if (Common.speedGrp != null)
                {
                    var speedData = Common.speedGrp.DataValue.ToDictionary(kv => $"Speed_{kv.Key}", kv => (object)kv.Value);
                    // 这里需要根据SpeedGrp的实际数据结构来获取数据
                    allData["SpeedGrp"] = speedData;
                }

                // 变频器模块
                if (Common.gd350_1?.DataValue != null)
                {
                    var inverterData = Common.gd350_1.DataValue.ToDictionary(kv => $"Inverter_{kv.Key}", kv => (object)kv.Value);
                    allData["GD350_1"] = inverterData;
                }

                // 添加时间戳
                allData["Timestamp"] = timestamp;
                allData["BatchId"] = Guid.NewGuid().ToString();

                return allData;
            }
            catch (Exception ex)
            {
                Var.LogInfo($"采集模块数据异常：{ex.Message}");
                return allData;
            }
        }

        /// <summary>
        /// 保存试验主表
        /// </summary>
        /// <param name="manualRecordPara">手动记录的参数</param>
        /// <returns>受影响的行数</returns>
        /// <exception cref="Exception"></exception>
        public int SaveMain(ManualRecordMain manualRecordMain)
        {
            int result = 0;
            try
            {
                result = DB.mysql.Insert<ManualRecordMain>(manualRecordMain).ExecuteAffrows();
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        /// <summary>
        /// 保存试验记录
        /// </summary>
        /// <param name="manualRecordPara">手动记录的参数</param>
        /// <returns>受影响的行数</returns>
        /// <exception cref="Exception"></exception>
        public int Save(ManualRecordPara manualRecordPara)
        {
            int result = 0;
            try
            {
                result = DB.mysql.Insert<ManualRecordPara>(manualRecordPara).ExecuteAffrows();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("试验记录保存失败！原因： " + ex.Message);
            }
        }

        /// <summary>
        /// 查询所有试验记录
        /// </summary>
        /// <param name="model">型号</param>
        /// <param name="engineNo">编号</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isDes">是否倒序，true=倒序(默认)，false=正序</param>
        /// <returns>测试参数列表</returns>
        public List<ManualRecordPara> GetAllRecord(string model, string engineNo, DateTime beginTime, DateTime endTime, bool isDes = true)
        {
            var query = DB.mysql.Select<ManualRecordPara>()
                .Where(d => d.RecordDataTime >= beginTime && d.RecordDataTime <= endTime)
                .Where(d => d.DieselEngineModel == model)
                .WhereIf(!string.IsNullOrEmpty(engineNo), d => d.DieselEngineNo == engineNo);

            if (isDes)
            {
                // 倒序：最新的数据在前
                query = query.OrderByDescending(d => d.RecordDataTime);
            }
            else
            {
                // 正序：最早的数据在前
                query = query.OrderBy(d => d.RecordDataTime);
            }

            var list = query.ToList();
            return list;
        }

        /// <summary>
        /// 查询所有试验记录到DataTable
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>DataTable</returns>
        public DataTable GetAllRecordToTable(DateTime beginTime, DateTime endTime)
        {
            var list = DB.mysql.Select<ManualRecordPara>()
                .Where(d => d.RecordDataTime >= beginTime && d.RecordDataTime <= endTime)
                .OrderByDescending(d => d.RecordDataTime)
                .ToDataTable("*");
            return list;
        }

        /// <summary>
        /// 通过试验阶段查询记录
        /// </summary>
        /// <param name="testStage">试验阶段</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataByTestStage(string testStage)
        {
            // 使用参数化查询避免SQL注入
            var list = DB.mysql.Select<ManualRecordPara>()
                .Where(d => d.TestStage == testStage)
                .ToDataTable("*");
            return list;
        }

        /// <summary>
        /// 通过试验阶段和周期查找记录
        /// </summary>
        /// <param name="testStage">试验阶段</param>
        /// <param name="testCycle">试验周期</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataByTestCycle(string testStage, string testCycle)
        {
            var list = DB.mysql.Select<ManualRecordPara>()
                .Where(d => d.TestStage == testStage && d.TestCycle == testCycle)
                .ToDataTable("*");
            return list;
        }

        /// <summary>
        /// 通过试验阶段，试验周期，和循环节点查找记录
        /// </summary>
        /// <param name="testStage">试验阶段</param>
        /// <param name="testCycle">试验周期</param>
        /// <param name="testStep">循环节点</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataByTestStep(string testStage, string testCycle, string testStep)
        {
            var list = DB.mysql.Select<ManualRecordPara>()
                .Where(d => d.TestStage == testStage && d.TestCycle == testCycle && d.TestStep == testStep)
                .ToDataTable("*");
            return list;
        }

        /// <summary>
        /// 获取最新的记录索引
        /// </summary>
        /// <returns>最大索引值</returns>
        public double GetMaxIndex()
        {
            var maxIndex = DB.mysql.Select<ManualRecordPara>().Max(d => d.Index);
            return maxIndex;
        }

        /// <summary>
        /// 根据GID删除记录
        /// </summary>
        /// <param name="gid">记录ID</param>
        /// <returns>受影响的行数</returns>
        public int DeleteByGid(string gid)
        {
            try
            {
                return DB.mysql.Delete<ManualRecordPara>().Where(d => d.gid == gid).ExecuteAffrows();
            }
            catch (Exception ex)
            {
                throw new Exception($"删除记录失败：{ex.Message}");
            }
        }
    }
}
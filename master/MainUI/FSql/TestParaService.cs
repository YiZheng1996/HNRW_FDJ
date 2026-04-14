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
    public class TestParaService
    {
        public static TestParaService instnce = new TestParaService();

        // 计时频率
        public System.Timers.Timer timerRecord = new System.Timers.Timer();

        /// <summary>
        /// 记录主表id
        /// </summary>
        public string MGid { get; set; }

        /// <summary>
        /// 记录间隔时间
        /// </summary>
        public int Second { get; set; } = 1;

        /// <summary>
        /// 记录间隔时间
        /// </summary>
        public bool IsRunning { get; set; } = false;

        /// <summary>
        /// 记录的
        /// </summary>
        public int Index { get; set; } = 0;

        /// <summary>
        /// 暂停记录
        /// </summary>
        public void StopRecord()
        {
            IsRunning = false;
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
        public bool StartRecordAll()
        {
            try
            {
                // 先记录主表id，如果操作数据库失败，则不需要再进入线程
                MGid = Guid.NewGuid().ToString("N");
                TestParaALLMain manualRecordMain = new TestParaALLMain()
                {
                    gid = MGid, // 生成正整数ID
                    BeginTime = DateTime.Now,
                    UserName = RW.UI.RWUser.User.Username,
                    DieselEngineModel = "-",
                    DieselEngineNo = "-",
                    TestName = "-"
                };
                var result = SaveMain(manualRecordMain);
                if (result == 0)
                {
                    // 数据库操作异常了
                    return false;
                }

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

                            // 转速超200rpm记录
                            if (MiddleData.instnce.EngineSpeed > 200 && Common.DOgrp["发动机DC24V供电"])
                            {
                                // 同步采集所有模块数据
                                var allModuleData = CollectAllModuleData();

                                // 保存所有模块的实时数据到TestParaALL表
                                SaveAllModuleDataToTestParaALL(allModuleData);
                            }

                            long endTime = stopwatch.ElapsedMilliseconds;
                            long elapsed = endTime - startTime;
                            long sleepTime = Second * 10000 - elapsed - 1;

                            // 10秒备份记录一条
                            if (sleepTime > 0)
                            {
                                Thread.Sleep((int)sleepTime);
                            }
                        }
                        catch (Exception ex)
                        {
                            Thread.Sleep(1000);
                            Debug.WriteLine($"StartRecordAll 保存所有模块数据到TestParaALL异常：{ex.Message}");
                        }
                    }

                    stopwatch.Stop();
                }));
                thread.IsBackground = true;
                thread.Name = "记录所有数据后台线程";
                thread.Start();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 保存所有模块数据到TestParaALL表
        /// </summary>
        /// <param name="allModuleData">所有模块数据字典</param>
        private void SaveAllModuleDataToTestParaALL(Dictionary<string, object> allModuleData)
        {
            // 将字典序列化为JSON字符串
            string monitorDataJson = SerializeModuleData(allModuleData);

            // 创建TestParaALL记录
            var testParaAll = new TestParaALL
            {
                gid = Guid.NewGuid().ToString("N"),
                Index = Index++, // 使用与TestPara相同的Index
                RecordName = "",
                mgid = MGid,
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
                    BaseDic.Add($"Base_EngineTorque", (object)MiddleData.instnce.EngineTorque);
                    BaseDic.Add($"Base_EnginePower", (object)MiddleData.instnce.EnginePower);
                    //BaseDic.Add($"Base_EnginePower", (object)MiddleData.instnce.EnginePower);
                    //BaseDic.Add($"Base_EnginePower", (object)MiddleData.instnce.EnginePower);
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
        /// 记录启机的监控的数据
        /// </summary>
        public int SaveRecordStartup(StartupTestPara startupTestPara)
        {
            return DB.mysql.Insert<StartupTestPara>(startupTestPara).ExecuteAffrows();
        }

        /// <summary>
        /// 保存试验主表
        /// </summary>
        /// <param name="manualRecordPara">手动记录的参数</param>
        /// <returns>受影响的行数</returns>
        /// <exception cref="Exception"></exception>
        public int SaveMain(TestParaALLMain manualRecordMain)
        {
            int result = 0;
            try
            {
                result = DB.mysql.Insert<TestParaALLMain>(manualRecordMain).ExecuteAffrows();
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        /// <summary>
        /// 查询所有试验记录
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>测试参数列表</returns>
        public List<TestParaALL> GetAllRecord(DateTime beginTime, DateTime endTime)
        {
            var list = DB.mysql.Select<TestParaALL>()
                .Where(d => d.RecordDataTime >= beginTime && d.RecordDataTime <= endTime)
                .OrderByDescending(d => d.RecordDataTime)
                .ToList();
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
            var list = DB.mysql.Select<TestParaALL>()
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
            var list = DB.mysql.Select<TestParaALL>()
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
            var list = DB.mysql.Select<TestParaALL>()
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
            var list = DB.mysql.Select<TestParaALL>()
                .Where(d => d.TestStage == testStage && d.TestCycle == testCycle && d.TestStep == testStep)
                .ToDataTable("*");
            return list;
        }

        /// <summary>
        /// 获取最新的记录索引
        /// </summary>
        /// <returns>最大索引值</returns>
        public int GetMaxIndex()
        {
            var maxIndex = DB.mysql.Select<TestParaALL>().Max(d => d.Index);
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
                return DB.mysql.Delete<TestParaALL>().Where(d => d.gid == gid).ExecuteAffrows();
            }
            catch (Exception ex)
            {
                throw new Exception($"删除记录失败：{ex.Message}");
            }
        }
    }
}
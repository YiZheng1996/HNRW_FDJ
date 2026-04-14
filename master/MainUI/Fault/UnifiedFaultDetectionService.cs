// UnifiedFaultDetectionService.cs
using MainUI.Config;
using MainUI.Fault;
using MainUI.Fault.Model;
using MainUI.Global;
using MainUI.FSql;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Diagnostics;

namespace MainUI.Services
{
    /// <summary>
    /// 统一故障检测服务（包含所有类型的故障：ECM、通讯、OPC检测、程序逻辑）
    /// </summary>
    public class UnifiedFaultDetectionService
    {
        /// <summary>
        /// 是否正在停机故障中
        /// </summary>
        public bool IsStopDoing { get; set; } = false;

        /// <summary>
        /// 是否正在打开机油预供循环
        /// </summary>
        public bool IsJYYGDoing { get; set; } = false;

        /// <summary>
        /// 压力差报警（压力差值）
        /// </summary>
        public const int PressureDiff = 200;

        private readonly FaultEventService faultEventService = new FaultEventService();

        /// <summary>
        /// ECM故障逻辑列表（特殊的检测逻辑）
        /// </summary>
        private Dictionary<string, FaultCondition> _ecmFaultConditions;

        /// <summary>
        /// 所有点位的故障状态列表（包括所有类型的故障）
        /// </summary>
        private ConcurrentDictionary<string, FaultState> _faultStates = new ConcurrentDictionary<string, FaultState>();

        /// <summary>
        /// 故障分组（按故障类型分组）
        /// </summary>
        private ConcurrentDictionary<FaultTypeEnum, List<string>> _faultGroups = new ConcurrentDictionary<FaultTypeEnum, List<string>>();

        /// <summary>
        /// 实时值存储的结构类
        /// </summary>
        public SensorData CurrentData { get; set; }

        /// <summary>
        /// 出现故障后的委托
        /// </summary>
        public event Action<string, FaultState, WarnTypeEnum> FaultDetected;

        /// <summary>
        /// 当前是否存在故障
        /// </summary>
        public bool HasActiveFaults => _faultStates.Any(f =>
            f.Value?.CurrentActiveFault != null &&
            f.Value.CurrentActiveFault != WarnTypeEnum.None);


        public object locked = new object();


        /// <summary>
        /// ECM模块当前是否存在故障
        /// </summary>
        public bool HasActiveFaultsECM()
        {
            if (_faultGroups.TryGetValue(FaultTypeEnum.ecm, out var ecmFaults))
            {
                foreach (var faultName in ecmFaults)
                {
                    var faultKey = GetFaultKey(FaultTypeEnum.ecm, faultName);
                    if (_faultStates.TryGetValue(faultKey, out var state) &&
                        state?.CurrentActiveFault != null &&
                        state.CurrentActiveFault != WarnTypeEnum.None)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 试验台检测当前是否存在故障
        /// </summary>
        public bool HasActiveFaultsTestBed()
        {
            // 检测通讯故障
            if (_faultGroups.TryGetValue(FaultTypeEnum.communication, out var commFaults))
            {
                foreach (var faultName in commFaults)
                {
                    var faultKey = GetFaultKey(FaultTypeEnum.communication, faultName);
                    if (_faultStates.TryGetValue(faultKey, out var state) &&
                        state?.CurrentActiveFault != null &&
                        state.CurrentActiveFault != WarnTypeEnum.None)
                    {
                        return true;
                    }
                }
            }

            // 检测OPC检测故障
            if (_faultGroups.TryGetValue(FaultTypeEnum.opcDetection, out var opcFaults))
            {
                foreach (var faultName in opcFaults)
                {
                    var faultKey = GetFaultKey(FaultTypeEnum.opcDetection, faultName);
                    if (_faultStates.TryGetValue(faultKey, out var state) &&
                        state?.CurrentActiveFault != null &&
                        state.CurrentActiveFault != WarnTypeEnum.None)
                    {
                        return true;
                    }
                }
            }

            // 检测程序逻辑故障
            if (_faultGroups.TryGetValue(FaultTypeEnum.calculate, out var calcFaults))
            {
                foreach (var faultName in calcFaults)
                {
                    var faultKey = GetFaultKey(FaultTypeEnum.calculate, faultName);
                    if (_faultStates.TryGetValue(faultKey, out var state) &&
                        state?.CurrentActiveFault != null &&
                        state.CurrentActiveFault != WarnTypeEnum.None)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        private readonly object _lock = new object();

        // 获取当前的故障数据
        FaultData currentFaultData { get; set; }

        /// <summary>
        /// 更新故障列表
        /// </summary>
        /// <returns></returns>
        public void GetCurrentFaultData()
        {
            // 确保 FaultConfig 和 FaultDataLists 不为空
            if (Var.FaultConfig == null)
            {
                Var.FaultConfig = new FaultConfig();
            }

            if (Var.FaultConfig.FaultDataLists == null || Var.FaultConfig.FaultDataLists.Count == 0)
            {
                // 如果没有配置，创建一个默认的 FaultData
                Var.FaultConfig.FaultDataLists = new List<FaultData> { new FaultData() };
            }

            currentFaultData = Var.FaultConfig.FaultDataLists[0];
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        public void Init()
        {
            // 确保 FaultConfig 不为空
            if (Var.FaultConfig == null)
            {
                // 使用最后一个型号或默认配置
                Var.FaultConfig = new FaultConfig(Var.SysConfig.LastModel);
            }

            // 初始化ECM故障配置
            //Var.FaultConfig = new FaultConfig(Var.SysConfig.LastModel);

            // 监听型号变更
            EventTriggerModel.OnModelNameChanged += EventTriggerModel_OnModelNameChanged;

            // ECM模块 实时值
            CurrentData = new SensorData();

            // 初始化数组
            CurrentData.A1A6缸排气温度 = new double[6];
            CurrentData.B1B6缸排气温度 = new double[6];
            CurrentData._1_7档轴温 = new double[7];

            _ecmFaultConditions = CreateECMFaultConditions();

            // 初始化所有故障
            InitializeAllFaults();

            // 启动故障检测线程
            StartFaultDetectionThread();

            // 启动需要停机线程
            StartFaultTestBedStopThread();
        }

        /// <summary>
        /// 初始化ECM故障
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, FaultCondition> CreateECMFaultConditions()
        {
            var conditions = new Dictionary<string, FaultCondition>();

            // 获取当前的故障数据
            GetCurrentFaultData();

            // 故障1: 高温水出水温度
            conditions["高温水出水温度"] = new FaultCondition
            {
                Name = "高温水出水温度",
                CheckAlarm = (data) => data.高温水出水温度 >= currentFaultData.F1V1,
                CheckShedding = (data) => data.高温水出水温度 >= currentFaultData.F1V2,
                CheckStop = null,
            };

            // 故障2: 中冷水进水温度
            conditions["中冷水进水温度"] = new FaultCondition
            {
                Name = "中冷水进水温度",
                CheckAlarm = (data) => data.中冷水进水温度 >= currentFaultData.F2V1,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障3: 中冷水出水温度
            conditions["中冷水出水温度"] = new FaultCondition
            {
                Name = "中冷水出水温度",
                CheckAlarm = (data) => data.中冷水出水温度 >= currentFaultData.F3V1,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障6: 后中冷后空气温度
            conditions["后中冷后空气温度"] = new FaultCondition
            {
                Name = "后中冷后空气温度",
                CheckAlarm = (data) => data.后中冷后空气温度 >= currentFaultData.F6V1,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障7: 主油道进口油温
            conditions["主油道进口油温"] = new FaultCondition
            {
                Name = "主油道进口油温",
                CheckAlarm = (data) => data.主油道进口油温 >= currentFaultData.F7V1,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障8: 前压气机出口空气温度
            conditions["前压气机出口空气温度"] = new FaultCondition
            {
                Name = "前压气机出口空气温度",
                CheckAlarm = (data) => data.前压气机出口空气温度 >= currentFaultData.F8V1,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障9: 后压气机出口空气温度
            conditions["后压气机出口空气温度"] = new FaultCondition
            {
                Name = "后压气机出口空气温度",
                CheckAlarm = (data) => data.后压气机出口空气温度 >= currentFaultData.F9V1,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障11: 主油道进口油压
            conditions["主油道进口油压"] = new FaultCondition
            {
                Name = "主油道进口油压",
                CheckAlarm = (data) => data.主油道进口油压 < currentFaultData.F11V1 && data.发动机转速 > currentFaultData.F11V2,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障14.1: 燃油精滤器后油压 (2 < p < 250 且 n > 600 rpm)
            conditions["燃油精滤器后油压1"] = new FaultCondition
            {
                Name = "燃油精滤器后油压1",
                CheckAlarm = (data) =>
                    data.燃油精滤器后油压 > currentFaultData.F141V1 &&
                    data.燃油精滤器后油压 < currentFaultData.F141V2 &&
                    data.发动机转速 > currentFaultData.F141V3,
                AlarmDuration = currentFaultData.F141V4,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障14.2: 燃油精滤器后前后压差大于100kPa
            conditions["燃油精滤器后油压2"] = new FaultCondition
            {
                Name = "燃油精滤器后油压2",
                CheckAlarm = (data) =>
                {
                    var diff = Math.Abs(data.燃油精滤器后油压 - data.燃油精滤器前油压);
                    return diff > currentFaultData.F142V1;
                },
                AlarmDuration = currentFaultData.F142V2,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障17: 机油泵出口油温
            conditions["机油泵出口油温"] = new FaultCondition
            {
                Name = "机油泵出口油温",
                CheckAlarm = (data) => data.机油泵出口油温 >= currentFaultData.F17V1,
                CheckShedding = (data) => data.机油泵出口油温 >= currentFaultData.F17V2,
                CheckStop = null,
            };

            // 故障18: 主油道末端油压
            conditions["主油道末端油压"] = new FaultCondition
            {
                Name = "主油道末端油压",
                CheckAlarm = (data) =>
                {
                    var warn1 = data.主油道末端油压 > currentFaultData.F18V1 && data.主油道末端油压 < currentFaultData.F18V2 && data.发动机转速 > currentFaultData.F18V3;
                    var warn2 = data.主油道末端油压 > currentFaultData.F18V4 && data.主油道末端油压 <= currentFaultData.F18V5 && data.发动机转速 >= currentFaultData.F18V6 && data.发动机转速 <= currentFaultData.F18V7;
                    return warn1 || warn2;
                },
                CheckShedding = (data) => data.主油道末端油压 > currentFaultData.F18V8 && data.主油道末端油压 < currentFaultData.F18V9 && data.发动机转速 > currentFaultData.F18V10,
                CheckStop = (data) => data.主油道末端油压 > currentFaultData.F18V11 && data.主油道末端油压 < currentFaultData.F18V12 && data.发动机转速 >= currentFaultData.F18V13,
            };

            // 故障20: 后增压器进油压
            conditions["后增压器进油压"] = new FaultCondition
            {
                Name = "后增压器进油压",
                CheckAlarm = null,
                CheckShedding = (data) => data.后增压器进口油压 <= currentFaultData.F20V1,
                CheckStop = (data) => data.后增压器进口油压 <= currentFaultData.F20V2,
            };

            // 故障22: 前增压器转速
            conditions["前增压器转速"] = new FaultCondition
            {
                Name = "前增压器转速",
                CheckAlarm = (data) => data.前增压器转速 >= currentFaultData.F22V1,
                AlarmDuration = 3,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障23: 后增压器转速
            conditions["后增压器转速"] = new FaultCondition
            {
                Name = "后增压器转速",
                CheckAlarm = (data) => data.后增压器转速 >= currentFaultData.F23V1,
                AlarmDuration = 3,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障24: A1-A6缸排气温度
            conditions["A1-A6缸排气温度"] = new FaultCondition
            {
                Name = "A1-A6缸排气温度",
                CheckAlarm = (data) => data.A1A6缸排气温度.Max() >= currentFaultData.F24V1,
                CheckShedding = (data) =>
                {
                    // todo 发动机功率需要计算
                    var result = data.发动机功率 >= currentFaultData.F24V2;
                    var max = data.A1A6缸排气温度.Max();
                    var min = data.A1A6缸排气温度.Min();
                    return (max - min) > currentFaultData.F24V3 && result;
                },
                SheddingDuration = currentFaultData.F24V4,
                CheckStop = null,
                StopDuration = 0
            };

            // 故障25: B1-B6缸排气温度
            conditions["B1-B6缸排气温度"] = new FaultCondition
            {
                Name = "B1-B6缸排气温度",
                CheckAlarm = (data) => data.B1B6缸排气温度.Max() >= currentFaultData.F25V1,
                CheckShedding = (data) =>
                {
                    var result = data.发动机功率 >= currentFaultData.F25V2;
                    var max = data.B1B6缸排气温度.Max();
                    var min = data.B1B6缸排气温度.Min();
                    return (max - min) > currentFaultData.F25V3 && result;
                },
                SheddingDuration = currentFaultData.F25V4,
                CheckStop = null,
                StopDuration = 0
            };

            // 故障26: A涡前排气温度
            conditions["A涡前排气温度"] = new FaultCondition
            {
                Name = "A涡前排气温度",
                CheckAlarm = (data) => data.A涡前排气温度 >= currentFaultData.F26V1,
                AlarmDuration = 0,
                CheckShedding = null,
                SheddingDuration = 0,
                CheckStop = null,
                StopDuration = 0
            };

            // 故障27: B涡前排气温度
            conditions["B涡前排气温度"] = new FaultCondition
            {
                Name = "B涡前排气温度",
                CheckAlarm = (data) => data.B涡前排气温度 >= currentFaultData.F27V1,
                AlarmDuration = 0,
                CheckShedding = null,
                SheddingDuration = 0,
                CheckStop = null,
                StopDuration = 0
            };

            // 故障28: 1-7档轴温
            conditions["1-7档轴温"] = new FaultCondition
            {
                Name = "1-7档轴温",
                CheckAlarm = null,
                AlarmDuration = 0,
                CheckShedding = (data) => data._1_7档轴温.Max() >= currentFaultData.F28V1,
                SheddingDuration = 0,
                CheckStop = (data) => data._1_7档轴温.Max() >= currentFaultData.F28V2 && data._1_7档轴温.Max() < currentFaultData.F28V3,
                StopDuration = 0
            };

            // 故障29: 轴温监控装置通讯故障 为1报警
            conditions["轴温监控装置通讯故障"] = new FaultCondition
            {
                Name = "轴温监控装置通讯故障",
                CheckAlarm = (data) => data.轴温监控装置通讯故障 == currentFaultData.F29V1,
                AlarmDuration = 0, // 通讯故障可以立即报警
                CheckShedding = null,
                SheddingDuration = 0,
                CheckStop = null,
                StopDuration = 0
            };

            // 故障30: 电喷转速1
            conditions["电喷转速1"] = new FaultCondition
            {
                Name = "电喷转速1",
                CheckAlarm = null,
                AlarmDuration = 0,
                CheckShedding = null,
                SheddingDuration = 0,
                CheckStop = (data) => data.电喷转速1 >= currentFaultData.F30V1,
                StopDuration = 0 // 转速超限可以立即停机
            };

            // 故障31: 电喷转速2
            conditions["电喷转速2"] = new FaultCondition
            {
                Name = "电喷转速2",
                CheckAlarm = null,
                AlarmDuration = 0,
                CheckShedding = null,
                SheddingDuration = 0,
                CheckStop = (data) => data.电喷转速2 >= currentFaultData.F31V1,
                StopDuration = 0
            };

            // 故障32: 电喷状态
            conditions["电喷状态"] = new FaultCondition
            {
                Name = "电喷状态",
                CheckAlarm = null,
                AlarmDuration = 0,
                CheckShedding = null,
                SheddingDuration = 0,
                CheckStop = (data) => data.电喷状态 == currentFaultData.F32V1,
                StopDuration = 0
            };

            // 故障33: 发动机转速1_飞轮
            conditions["发动机转速1_飞轮"] = new FaultCondition
            {
                Name = "发动机转速1_飞轮",
                CheckAlarm = null,
                AlarmDuration = 0,
                CheckShedding = null,
                SheddingDuration = 0,
                CheckStop = (data) => data.飞轮发动机转速1 >= currentFaultData.F33V1,
                StopDuration = 0
            };

            // 故障34: 发动机转速2 飞轮
            conditions["发动机转速2_飞轮"] = new FaultCondition
            {
                Name = "发动机转速2_飞轮",
                CheckAlarm = null,
                AlarmDuration = 0,
                CheckShedding = null,
                SheddingDuration = 0,
                CheckStop = (data) => data.飞轮发动机转速2 >= currentFaultData.F34V1,
                StopDuration = 0
            };

            // 故障35: 后增进油压卸载开关
            conditions["后增进油压卸载开关"] = new FaultCondition
            {
                Name = "后增进油压卸载开关",
                CheckAlarm = null,
                AlarmDuration = 0,
                CheckShedding = (data) => data.后增进油压卸载开关 == currentFaultData.F35V1,
                SheddingDuration = 4,
                CheckStop = null,
                StopDuration = 0
            };

            // 故障36: 后增进油压停机开关
            conditions["后增进油压停机开关"] = new FaultCondition
            {
                Name = "后增进油压停机开关",
                CheckAlarm = null,
                AlarmDuration = 0,
                CheckShedding = null,
                SheddingDuration = 0,
                CheckStop = (data) => data.后增进油压停机开关 == currentFaultData.F36V1,
                StopDuration = 4
            };

            // 故障37: 曲轴箱差压开关
            conditions["曲轴箱差压开关"] = new FaultCondition
            {
                Name = "曲轴箱差压开关",
                CheckAlarm = null,
                AlarmDuration = 0,
                CheckShedding = null,
                SheddingDuration = 0,
                CheckStop = (data) => data.曲轴箱差压开关 == currentFaultData.F37V1,
                StopDuration = 1
            };

            return conditions;
        }

        /// <summary>
        /// 初始化所有故障
        /// </summary>
        private void InitializeAllFaults()
        {
            // 清空现有数据
            _faultStates.Clear();
            _faultGroups.Clear();

            // 初始化ECM故障
            InitializeECMFaults();

            // 初始化其他类型故障
            InitializeCommunicationFaults();
            InitializeOpcDetectionFaults();
            InitializeCalculateFaults();
        }

        /// <summary>
        /// 初始化ECM故障
        /// </summary>
        private void InitializeECMFaults()
        {
            var ecmFaults = new List<string>();

            foreach (var faultId in _ecmFaultConditions.Keys)
            {
                var faultState = new FaultState()
                {
                    Name = faultId,
                    Desc = faultId,
                    FaultType = FaultTypeEnum.ecm
                };
                _faultStates[faultId] = faultState;
                ecmFaults.Add(faultId);
            }

            _faultGroups[FaultTypeEnum.ecm] = ecmFaults;
        }

        /// <summary>
        /// 初始化通讯故障
        /// </summary>
        private void InitializeCommunicationFaults()
        {
            var communicationFaults = new Dictionary<string, string>
            {
                { "台位控制","PLC" },
                { "测量柜" ,"PLC"},
                { "启动柜","PLC"},
                { "发动机电参数","串口转网络"},
                { "机油系统1","串口转网络"},
                { "机油系统2","串口转网络"},
                { "机油系统3","串口转网络"},
                { "机油系统4","串口转网络"},
                { "燃油系统1","串口转网络"},
                { "燃油系统2","串口转网络"},
                { "水系统1","串口转网络"},
                { "水系统2","串口转网络"},
                { "水系统3","串口转网络"},
                { "水系统4","串口转网络"},
                { "柴油机控制器","TRDP网络"},
                { "台位主从通讯","网络"},
                { "燃油耗仪","串口转网络"},
                { "称重仪","串口转网络"},
            };

            foreach (var faultName in communicationFaults)
            {
                var faultKey = GetFaultKey(FaultTypeEnum.communication, faultName.Key);
                _faultStates[faultKey] = new FaultState
                {
                    Name = faultName.Key,
                    Desc = $"{faultName.Key}，{faultName.Value}通讯掉线",
                    FaultType = FaultTypeEnum.communication,
                };
            }

            _faultGroups[FaultTypeEnum.communication] = communicationFaults.Keys.ToList();
        }

        /// <summary>
        /// 初始化OPC检测故障
        /// </summary>
        private void InitializeOpcDetectionFaults()
        {
            var opcFaults = new List<string>();

            // 与PLC控制柜故障点位
            foreach (var item in Common.PipelineFaultGrp.DataValue)
            {
                FaultTypeEnum typeEnum = FaultTypeEnum.opcDetection;
                if (item.Key.Contains("通讯掉线"))
                {
                    typeEnum = FaultTypeEnum.communication;
                }
                var faultKey = GetFaultKey(typeEnum, item.Key);

                _faultStates[faultKey] = new FaultState
                {
                    Name = item.Key,
                    Desc = item.Key,
                    FaultType = typeEnum
                };
                opcFaults.Add(item.Key);
            }

            _faultGroups[FaultTypeEnum.opcDetection] = opcFaults;
        }

        /// <summary>
        /// 初始化逻辑判定故障
        /// </summary>
        private void InitializeCalculateFaults()
        {
            var calculateFaults = new List<string>
            {
                "【燃油】粗滤器1前后压差过大",
                "【燃油】粗滤器2前后压差过大",
                "【燃油】精滤器1前后压差过大",
                "【燃油】精滤器2前后压差过大",
                "【机油】机滤器1前后压差过大",
                "【机油】机滤器2前后压差过大",
                "厂房总气压不足"
            };

            foreach (var faultName in calculateFaults)
            {
                var faultKey = GetFaultKey(FaultTypeEnum.calculate, faultName);
                _faultStates[faultKey] = new FaultState
                {
                    Name = faultName,
                    Desc = faultName.Contains("粗滤器") ? $"{faultName}，粗滤器堵塞。" :
                           faultName.Contains("精滤器") ? $"{faultName}，精滤器堵塞。" :
                           faultName.Contains("机滤器") ? $"{faultName}，机滤器堵塞。" :
                           $"{faultName}",
                    FaultType = FaultTypeEnum.calculate,
                };
            }

            _faultGroups[FaultTypeEnum.calculate] = calculateFaults;
        }

        /// <summary>
        /// 生成故障的Key
        /// </summary>
        private string GetFaultKey(FaultTypeEnum faultEnum, string faultName)
        {
            return faultName;

            //// ECM故障使用原始名称作为Key，其他类型加上前缀
            //if (faultEnum == FaultTypeEnum.ecm)
            //    return faultName;
            //else
            //    return $"{GetFaultCode(faultEnum)}_{faultName}";
        }

        /// <summary>
        /// 故障类型中文名
        /// </summary>
        private string GetFaultCode(FaultTypeEnum faultEnum)
        {
            switch (faultEnum)
            {
                case FaultTypeEnum.communication:
                    return "通讯"; // 通讯
                case FaultTypeEnum.opcDetection:
                    return "OPC反馈"; // OPC反馈
                case FaultTypeEnum.calculate:
                    return "内部检测"; // 内部检测
                case FaultTypeEnum.ecm:
                    return "发动机控制器"; // 发动机控制器
                default:
                    return "通讯"; // 默认报警
            }
        }

        /// <summary>
        /// 启动故障检测线程
        /// </summary>
        private void StartFaultDetectionThread()
        {
            // 增加一个状态计时器，用于跟踪发动机转速满足条件的持续时间
            Stopwatch engineRunningTimer = new Stopwatch();
            bool wasEngineRunning = false; // 上次循环中发动机是否在运行

            Thread t = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        // 获取当前发动机转速
                        
                        bool isEngineRunningNow = (CurrentData.发动机转速 > MiddleData.instnce.SelectModelConfig.MinSpeed - 10 && Common.DOgrp["发动机DC24V供电"]);

                        // ECM故障检测（仅在发动机稳定运行超过5秒后检测）
                        if (isEngineRunningNow && Var.FaultConfig?.FaultDataLists?.Count > 0)
                        {
                            // 如果发动机刚达到运行条件，开始计时
                            if (!wasEngineRunning)
                            {
                                engineRunningTimer.Restart();
                                wasEngineRunning = true;
                            }

                            // 检查是否持续运行了5秒
                            if (engineRunningTimer.Elapsed.TotalSeconds > 5.0)
                            {
                                DetectECMFaults();
                            }
                        }
                        else
                        {
                            // 如果发动机不满足运行条件，重置计时器
                            if (wasEngineRunning)
                            {
                                engineRunningTimer.Stop();
                                wasEngineRunning = false;
                            }
                        }

                        // 其他类型故障检测（始终运行）
                        DetectOtherFaults();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"故障检测异常: {ex.Message}");
                        Var.LogInfo($"故障检测异常: {ex.Message}");
                    }

                    Thread.Sleep(200);
                }
            });
            t.IsBackground = true;
            t.Name = "统一故障检测线程";
            t.Start();
        }

   
        /// <summary>
        /// 开始检测试验台检测停机故障线程
        /// </summary>
        private void StartFaultTestBedStopThread()
        {
            // 增加一个状态计时器，用于跟踪发动机转速满足条件的持续时间
            //Stopwatch stepWatch = new Stopwatch();

            Thread t = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        // 获取当前停机故障列表
                        var stopList = GetActiveFaultsByType(WarnTypeEnum.Stop);
                        bool isEngineRunningNow = (CurrentData.发动机转速 > MiddleData.instnce.SelectModelConfig.MinSpeed - 10 && Common.DOgrp["发动机DC24V供电"]);

                        // 停机故障检测
                        if (Var.FaultConfig?.FaultDataLists?.Count > 0 && stopList.Count > 0 && isEngineRunningNow)
                        {
                            // 如果满足停机条件，则下发停机指令
                            if (!IsStopDoing)
                            {
                                //stepWatch.Restart();
                                IsStopDoing = true;

                                Common.ExChangeGrp.SetBool("上位机停机控制", true);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"故障检测异常: {ex.Message}");
                        Var.LogInfo($"故障检测异常: {ex.Message}");
                    }

                    Thread.Sleep(500);
                }
            });
            t.IsBackground = true;
            t.Name = "检测需要停机线程";
            t.Start();


            //Thread t2 = new Thread(() =>
            //{
            //    while (true)
            //    {
            //        try
            //        {
            //            // 避免0转时进行判断
            //            bool isEngineRunningNow = (CurrentData.发动机转速 < 300 && CurrentData.发动机转速 > 50);

            //            if (isEngineRunningNow)
            //            {
            //                // 如果满足停机条件，则下发停机指令
            //                if (!IsJYYGDoing)
            //                {
            //                    //stepWatch.Restart();
            //                    IsJYYGDoing = true;

            //                    // 打开92
            //                }
            //            }

            //            if (CurrentData.发动机转速 > 300) 
            //            {
                            
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            System.Diagnostics.Debug.WriteLine($"故障检测异常: {ex.Message}");
            //            Var.LogInfo($"故障检测异常: {ex.Message}");
            //        }

            //        Thread.Sleep(500);
            //    }
            //});
            //t2.IsBackground = true;
            //t2.Name = "停机后自打开预供机油循环线程";
            //t2.Start();

            //// 检测PLC Fault组 的故障
            //Common.PipelineFaultGrp.KeyValueChange += (object sender, Modules.EventArgsModel.DIValueChangedEventArgs e) =>
            //{
            //    if (e.Key.Contains("通讯掉线"))
            //    {
            //        FaultStatusChange(FaultTypeEnum.communication, e.Value ? WarnTypeEnum.Alarm : WarnTypeEnum.None, e.Key);
            //    }
            //    else
            //    {
            //        FaultStatusChange(FaultTypeEnum.opcDetection, e.Value ? WarnTypeEnum.Alarm : WarnTypeEnum.None, e.Key);
            //    }
            //};
        }

        /// <summary>
        /// 检测ECM故障（使用特殊的检测逻辑）
        /// </summary>
        private void DetectECMFaults()
        {
            foreach (var fault in _ecmFaultConditions)
            {
                var faultId = fault.Key;
                var condition = fault.Value;
                var state = _faultStates[faultId];

                DetectFault(faultId, condition, state);
            }
        }

        /// <summary>
        /// 检测其他类型故障
        /// </summary>
        private void DetectOtherFaults()
        {
            // 通讯故障检测由主界面进行

            // OPC点位故障
            foreach (var item in Common.PipelineFaultGrp.DataValue)
            {
                if (item.Key.Contains("通讯掉线"))
                {
                    FaultStatusChange(FaultTypeEnum.communication, item.Value ? WarnTypeEnum.Alarm : WarnTypeEnum.None, item.Key);
                }
                else if (item.Key.Contains("过流") || item.Key.Contains("阀故障")) 
                {
                    FaultStatusChange(FaultTypeEnum.opcDetection, item.Value ? WarnTypeEnum.Tip : WarnTypeEnum.None, item.Key);
                }
                else if(item.Key.Contains("已停止") || item.Key.Contains("不下降"))
                {
                    FaultStatusChange(FaultTypeEnum.opcDetection, item.Value ? WarnTypeEnum.Alarm : WarnTypeEnum.None, item.Key);
                }
            }

            //// 检测PLC Fault组 的故障
            //Common.PipelineFaultGrp.KeyValueChange += (object sender, Modules.EventArgsModel.DIValueChangedEventArgs e) =>
            //{
            //    if (e.Key.Contains("通讯掉线"))
            //    {
            //        FaultStatusChange(FaultTypeEnum.communication, e.Value ? WarnTypeEnum.Alarm : WarnTypeEnum.None, e.Key);
            //    }
            //    else
            //    {
            //        FaultStatusChange(FaultTypeEnum.opcDetection, e.Value ? WarnTypeEnum.Alarm : WarnTypeEnum.None, e.Key);
            //    }
            //};

            // 逻辑计算部分

            // 燃油
            var ryjfront1 = Common.fuelGrp["精滤器1前压力检测-P34"];
            var ryjdown1 = Common.fuelGrp["精滤器1后压力检测-P35"];
            bool Isfault1 = false;
            if (Math.Abs(ryjfront1 - ryjdown1) > PressureDiff)
            {
                Isfault1 = true;
            }
            FaultStatusChange(FaultTypeEnum.calculate, Isfault1 ? WarnTypeEnum.Alarm : WarnTypeEnum.None, "【燃油】精滤器1前后压差过大");

            var ryjfront2 = Common.fuelGrp["精滤器2前压力检测-P36"];
            var ryjdown2 = Common.fuelGrp["精滤器2后压力检测-P37"];
            bool Isfault2 = false;
            if (Math.Abs(ryjfront2 - ryjdown2) > PressureDiff)
            {
                Isfault2 = true;
            }
            FaultStatusChange(FaultTypeEnum.calculate, Isfault2 ? WarnTypeEnum.Alarm : WarnTypeEnum.None, "【燃油】精滤器2前后压差过大");

            var rycfront1 = Common.fuelGrp["粗滤器1前压力检测-P30"];
            var rycdown1 = Common.fuelGrp["粗滤器1后压力检测-P31"];
            bool Isfault3 = false;
            if (Math.Abs(rycfront1 - rycdown1) > PressureDiff)
            {
                Isfault3 = true;
            }
            FaultStatusChange(FaultTypeEnum.calculate, Isfault3 ? WarnTypeEnum.Alarm : WarnTypeEnum.None, "【燃油】粗滤器1前后压差过大");

            var rycfront2 = Common.fuelGrp["粗滤器2前压力检测-P32"];
            var rycdown2 = Common.fuelGrp["粗滤器2后压力检测-P33"];
            bool Isfault4 = false;
            if (Math.Abs(rycfront2 - rycdown2) > PressureDiff)
            {
                Isfault4 = true;
            }
            FaultStatusChange(FaultTypeEnum.calculate, Isfault4 ? WarnTypeEnum.Alarm : WarnTypeEnum.None, "【燃油】粗滤器2前后压差过大");

            var pressure1 = Common.AIgrp["厂房进气压力检测1"];
            var pressure2 = Common.AIgrp["厂房进气压力检测2"];
            bool Isfault5 = false;
            if (pressure1 < 400 || pressure2 < 400)
            {
                Isfault5 = true;
            }
            FaultStatusChange(FaultTypeEnum.calculate, Isfault5 ? WarnTypeEnum.Stop : WarnTypeEnum.None, "厂房总气压不足");
        }

        /// <summary>
        /// 检测单个故障（主要用于ECM故障）
        /// </summary>
        private void DetectFault(string faultId, FaultCondition condition, FaultState state)
        {
            WarnTypeEnum detectedType = WarnTypeEnum.None;
            DateTime now = DateTime.Now;

            // 按照优先级检测：停机 -> 降载 -> 报警
            if (condition.CheckStop != null && condition.CheckStop(CurrentData))
            {
                lock (_lock)
                {
                    if ((now - state.StopStartTime).TotalSeconds >= condition.StopDuration)
                    {
                        detectedType = WarnTypeEnum.Stop;
                        if (!state.StopConditionMet)
                        {
                            state.StopConditionMet = true;
                            state.StopStartTime = now;
                        }
                    }
                }
            }
            else
            {
                state.StopConditionMet = false;
            }

            if (detectedType == WarnTypeEnum.None && condition.CheckShedding != null && condition.CheckShedding(CurrentData))
            {
                lock (_lock)
                {
                    if ((now - state.SheddingStartTime).TotalSeconds >= condition.SheddingDuration)
                    {
                        detectedType = WarnTypeEnum.Shedding;
                        if (!state.SheddingConditionMet)
                        {
                            state.SheddingConditionMet = true;
                        }
                    }

                }
            }
            else
            {
                state.SheddingConditionMet = false;
            }

            if (detectedType == WarnTypeEnum.None && condition.CheckAlarm != null && condition.CheckAlarm(CurrentData))
            {
                lock (_lock)
                {
                    if ((now - state.AlarmStartTime).TotalSeconds >= condition.AlarmDuration)
                    {
                        detectedType = WarnTypeEnum.Alarm;
                        if (!state.AlarmConditionMet)
                        {
                            state.AlarmConditionMet = true;
                            state.AlarmStartTime = now;
                        }
                    }
                }
            }
            else
            {
                state.AlarmConditionMet = false;
            }

            // 只有故障更新后才更新故障状态
            if (detectedType != WarnTypeEnum.None && state.CurrentActiveFault != detectedType)
            {
                state.CurrentActiveFault = detectedType;
                FaultDetected?.Invoke(state.Name, state, detectedType);
                RecordFaultToDatabase(faultId, state, detectedType);
            }
            else if (detectedType == WarnTypeEnum.None && state.CurrentActiveFault != WarnTypeEnum.None)
            {
                // 恢复故障
                state.CurrentActiveFault = WarnTypeEnum.None;
                FaultDetected?.Invoke(faultId, state, WarnTypeEnum.None);
                UpdateFaultInDatabase(faultId);
            }
        }

        /// <summary>
        /// 试验台检测到更新某个点位状态
        /// </summary>
        /// <param name="faultType">故障类型</param>
        /// <param name="warnType">故障级别</param>
        /// <param name="faultName">模块名称</param>
        public void FaultStatusChange(FaultTypeEnum faultType, WarnTypeEnum warnType, string faultName)
        {
            try
            {
                var faultKey = GetFaultKey(faultType, faultName);

                // 获取当前故障状态(1为故障)
                var faultState = _faultStates[faultKey];

                // 状态发生变化
                UpdateFaultStatus(faultKey, faultState, warnType);
            }
            catch (Exception ex)
            {
                Var.LogInfo("更新故障状态出现异常 " + ex.ToString());
            }
        }

        ///// <summary>
        ///// 处理故障
        ///// </summary>
        //public void HandleFault(string faultKey, FaultState state, WarnTypeEnum warnType)
        //{
        //    lock (_lock)
        //    {
        //        var currentType = state.CurrentActiveFault;
        //        if (currentType != warnType)
        //        {
        //            state.CurrentActiveFault = warnType;
        //            UpdateFaultTimestamps(state, warnType);

        //            // 触发事件
        //            FaultDetected?.Invoke(faultKey, state, warnType);

        //            // 记录到数据库
        //            RecordFaultToDatabase(faultKey, state, warnType);
        //        }
        //    }
        //}

        ///// <summary>
        ///// 清除故障
        ///// </summary>
        //public void ClearFault(string faultKey, FaultState state)
        //{
        //    lock (_lock)
        //    {
        //        if (state.CurrentActiveFault != WarnTypeEnum.None)
        //        {
        //            state.CurrentActiveFault = WarnTypeEnum.None;

        //            // 触发事件
        //            FaultDetected?.Invoke(faultKey, state, WarnTypeEnum.None);

        //            // 更新数据库
        //            UpdateFaultInDatabase(faultKey);
        //        }
        //    }
        //}

        /// <summary>
        /// 更新故障状态，故障状态 报警->降载->停机
        /// </summary>
        private void UpdateFaultStatus(string faultId, FaultState state, WarnTypeEnum detectedType)
        {
            // 出现故障
            if (detectedType != WarnTypeEnum.None && state.CurrentActiveFault != detectedType)
            {
                lock (_lock)
                {
                    state.CurrentActiveFault = detectedType;
                    if (detectedType == WarnTypeEnum.Alarm)
                    {
                        state.AlarmStartTime = DateTime.Now;
                    }
                    else if (detectedType == WarnTypeEnum.Shedding)
                    {
                        state.SheddingStartTime = DateTime.Now;
                    }
                    else if (detectedType == WarnTypeEnum.Tip)
                    {
                        state.TipStartTime = DateTime.Now;
                    }
                    else
                    {
                        state.StopStartTime = DateTime.Now;
                    }
                }
                FaultDetected?.Invoke(state.Name, state, detectedType);
                RecordFaultToDatabase(faultId, state, detectedType);
            }
            else if (detectedType == WarnTypeEnum.None && state.CurrentActiveFault != WarnTypeEnum.None)
            {
                // 故障恢复
                state.CurrentActiveFault = WarnTypeEnum.None;
                FaultDetected?.Invoke(faultId, state, WarnTypeEnum.None);
                UpdateFaultInDatabase(faultId);
            }
        }


        /// <summary>
        /// 记录故障到数据库
        /// </summary>
        private void RecordFaultToDatabase(string faultCode, FaultState state, WarnTypeEnum warnType)
        {
            try 
            {
                string id = Guid.NewGuid().ToString("N");
                FaultEvent faultEventRecord = new FaultEvent()
                {
                    Id = id,
                    FaultCode = faultCode,
                    OccurTime = DateTime.Now,
                    Status = 0,
                    Description = state.Desc ?? state.Name,
                    Severity = GetFaultSeverity(warnType),
                    FaultType = GetFaultType(state.FaultType)
                };

                int rows = faultEventService.AddFaultEventRecord(faultEventRecord);
                if (rows == 0)
                {
                    string msg = $"记录故障失败：{state.Name}";
                    Var.LogInfo(msg);
                    Console.WriteLine(msg);
                }
            }
            catch (Exception ex)
            {
                Var.LogInfo($"记录故障时发生异常：{ex.Message}");
            }
        }

        /// <summary>
        /// 更新数据库中的故障记录
        /// </summary>
        private void UpdateFaultInDatabase(string faultCode)
        {
            try
            {
                var dataTable = faultEventService.GetFaultEventRecordByFaultCode(faultCode);
                if (dataTable != null)
                {
                    int successCount = 0;
                    foreach (var record in dataTable)
                    {
                        int affectedRows = faultEventService.UpdateFaultEventRecordById(record.Id, DateTime.Now, 1);
                        if (affectedRows > 0)
                        {
                            successCount++;
                            Console.WriteLine($"故障恢复成功：{faultCode}，ID：{record.Id}");
                        }
                    }

                    if (successCount == 0)
                    {
                        string msg = $"警告：未能恢复任何故障记录：{faultCode}";
                        Var.LogInfo($"{msg}");
                        Console.WriteLine($"{msg}");
                    }
                }
                else
                {
                    string msg = $"警告：找不到对应的故障记录：{faultCode}";
                    Var.LogInfo($"{msg}");
                    Console.WriteLine($"{msg}");
                }
            }
            catch (Exception ex)
            {
                Var.LogInfo($"更新故障记录时发生异常：{ex.ToString()}");
            }
        }

        /// <summary>
        /// 根据严重程度获取告警类型
        /// </summary>
        private WarnTypeEnum GetWarnTypeFromSeverity(int severity)
        {
            switch (severity)
            {
                case 1: return WarnTypeEnum.Alarm;
                case 2: return WarnTypeEnum.Shedding;
                case 3: return WarnTypeEnum.Stop;
                default: return WarnTypeEnum.Alarm;
            }
        }

        /// <summary>
        /// 更新故障时间戳
        /// </summary>
        private void UpdateFaultTimestamps(FaultState state, WarnTypeEnum warnType)
        {
            DateTime now = DateTime.Now;
            switch (warnType)
            {
                case WarnTypeEnum.Alarm:
                    state.AlarmStartTime = now;
                    state.AlarmConditionMet = true;
                    break;
                case WarnTypeEnum.Shedding:
                    state.SheddingStartTime = now;
                    state.SheddingConditionMet = true;
                    break;
                case WarnTypeEnum.Tip:
                    state.TipStartTime = now;
                    state.TipConditionMet = true;
                    break;
                case WarnTypeEnum.Stop:
                    state.StopStartTime = now;
                    state.StopConditionMet = true;
                    break;
            }
        }

        /// <summary>
        /// 刷新数据的方法
        /// </summary>
        public void UpdateSensorData(Action<SensorData> updateAction)
        {
            lock (_lock)
            {
                updateAction(CurrentData);
            }
        }

        /// <summary>
        /// 根据故障类型获取严重程度(1:故障 2：降载 3：停机 4：只提示不蜂鸣)
        /// </summary>
        private int GetFaultSeverity(WarnTypeEnum warnType)
        {
            switch (warnType)
            {
                case WarnTypeEnum.Alarm: return 1;
                case WarnTypeEnum.Shedding: return 2;
                case WarnTypeEnum.Stop: return 3;
                case WarnTypeEnum.Tip: return 4;
                default: return 1;
            }
        }


        /// <summary>
        /// 根据故障类型获取严重程度
        /// </summary>
        private int GetFaultType(FaultTypeEnum faultType)
        {
            switch (faultType)
            {
                case FaultTypeEnum.communication: return 0;
                case FaultTypeEnum.opcDetection: return 1;
                case FaultTypeEnum.calculate: return 2;
                case FaultTypeEnum.ecm: return 3;
                default: return 1;
            }
        }

        /// <summary>
        /// 故障重新检测
        /// </summary>
        public void FaultCheckResend()
        {
            foreach (var fault in _faultStates)
            {
                if (fault.Value.CurrentActiveFault != WarnTypeEnum.None)
                {
                    FaultDetected?.Invoke(fault.Key, fault.Value, fault.Value.CurrentActiveFault);
                }
            }
        }

        /// <summary>
        /// 故障复位
        /// </summary>
        public void FaultReset()
        {
            lock (_lock)
            {
                foreach (var fault in _faultStates)
                {
                    if (fault.Value.CurrentActiveFault != WarnTypeEnum.None)
                    {
                        UpdateFaultInDatabase(fault.Key);
                        fault.Value.CurrentActiveFault = WarnTypeEnum.None;
                        FaultDetected?.Invoke(fault.Key, fault.Value, WarnTypeEnum.None);
                    }
                }
            }
        }

        /// <summary>
        /// 获取所有活跃故障
        /// </summary>
        public Dictionary<string, FaultState> GetActiveFaults()
        {
            return _faultStates
                .Where(f => f.Value != null && f.Value.CurrentActiveFault != WarnTypeEnum.None)
                .ToDictionary(k => k.Key, v => v.Value);
        }

        /// <summary>
        /// 获取特定类型的活跃故障
        /// </summary>
        public List<string> GetActiveFaultsByType(WarnTypeEnum faultType)
        {
            //var excludePatterns = new[] { "发动机转速1_飞轮", "发动机转速2_飞轮" };  // 要排除的字符串列表

            //return _faultStates
            //    .Where(f => f.Value != null && f.Value.CurrentActiveFault == faultType)
            //    .Select(f => f.Key)
            //    .Where(key => !excludePatterns.Any(pattern => key.Contains(pattern)))
            //    .ToList();

            // 正式
            return _faultStates
                .Where(f => f.Value != null && f.Value.CurrentActiveFault == faultType)
                .Select(f => f.Key)
                .ToList();
        }

        /// <summary>
        /// 获取特定故障类型的故障状态
        /// </summary>
        public List<FaultState> GetFaultStatesByType(FaultTypeEnum faultEnum)
        {
            if (_faultGroups.TryGetValue(faultEnum, out var faultNames))
            {
                var states = new List<FaultState>();
                foreach (var name in faultNames)
                {
                    var key = GetFaultKey(faultEnum, name);
                    if (_faultStates.TryGetValue(key, out var state))
                    {
                        states.Add(state);
                    }
                }
                return states;
            }
            return new List<FaultState>();
        }

        /// <summary>
        /// 检查特定故障类型是否存在活跃故障
        /// </summary>
        /// <param name="faultEnum">故障类型</param>
        /// <returns>是否存在活跃故障</returns>
        public bool HasActiveFaultsByType(FaultTypeEnum faultEnum)
        {
            return GetActiveFaultStatesByType(faultEnum).Any();
        }

        /// <summary>
        /// 获取特定故障类型的活跃故障
        /// </summary>
        /// <param name="faultEnum">故障类型</param>
        /// <returns>活跃故障状态列表</returns>
        public List<FaultState> GetActiveFaultStatesByType(FaultTypeEnum faultEnum)
        {
            var activeFaults = new List<FaultState>();

            if (_faultGroups.TryGetValue(faultEnum, out var faultNames))
            {
                foreach (var name in faultNames)
                {
                    var key = GetFaultKey(faultEnum, name);
                    if (_faultStates.TryGetValue(key, out var state) &&
                        state != null &&
                        state.CurrentActiveFault != WarnTypeEnum.None)
                    {
                        activeFaults.Add(state);
                    }
                }
            }

            return activeFaults;
        }

        /// <summary>
        /// 获取特定故障类型的特定活跃故障
        /// </summary>
        /// <param name="faultEnum">故障类型</param>
        /// <param name="warnType">类型</param>
        /// <returns>活跃故障状态列表</returns>
        public List<FaultState> GetActiveFaultStatesByType(FaultTypeEnum faultEnum, WarnTypeEnum warnType)
        {
            var activeFaults = new List<FaultState>();

            if (_faultGroups.TryGetValue(faultEnum, out var faultNames))
            {
                foreach (var name in faultNames)
                {
                    var key = GetFaultKey(faultEnum, name);
                    if (_faultStates.TryGetValue(key, out var state) &&
                        state != null &&
                        state.CurrentActiveFault == warnType)
                    {
                        activeFaults.Add(state);
                    }
                }
            }

            return activeFaults;
        }

        /// <summary>
        /// 获取所有故障分组
        /// </summary>
        public Dictionary<FaultTypeEnum, List<string>> GetAllFaultGroups()
        {
            return _faultGroups.ToDictionary(k => k.Key, v => v.Value);
        }

        /// <summary>
        /// 获取所有故障（按故障类型分组）
        /// </summary>
        public ConcurrentDictionary<FaultTypeEnum, List<FaultState>> GetAllFault()
        {
            var result = new ConcurrentDictionary<FaultTypeEnum, List<FaultState>>();

            foreach (var faultGroup in _faultGroups)
            {
                var faultType = faultGroup.Key;
                var faultNames = faultGroup.Value;
                var faultStates = new List<FaultState>();

                foreach (var faultName in faultNames)
                {
                    var faultKey = GetFaultKey(faultType, faultName);
                    if (_faultStates.TryGetValue(faultKey, out var state) && state != null)
                    {
                        faultStates.Add(state);
                    }
                }

                result[faultType] = faultStates;
            }

            return result;
        }

        /// <summary>
        /// 型号改变时
        /// </summary>
        private void EventTriggerModel_OnModelNameChanged(string model)
        {
            try
            {
                Var.FaultConfig = new FaultConfig(model);
                // 重新初始化ECM故障条件
                _ecmFaultConditions = CreateECMFaultConditions();
            }
            catch (Exception ex)
            {
                // 创建默认配置
                Var.FaultConfig = new FaultConfig();
            }
          
        }
    }
}
using MainUI.Fault.Model;
using MainUI.FSql;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Fault
{
    /// <summary>
    /// 试验台内部检测报警
    /// </summary>
    public class TestBedFaultDetectionService
    {
        FaultEventService faultEventService = new FaultEventService();

        /// <summary>
        /// 所有点位的报警状态列表（通讯故障/OPC监控故障/逻辑判定故障）
        /// </summary>
        public ConcurrentDictionary<FaultTypeEnum, List<FaultState>> _faultStates { get; set; } = new ConcurrentDictionary<FaultTypeEnum, List<FaultState>>();

        public object locked { get; set; } = new object();

        /// <summary>
        /// 出现故障后的委托
        /// </summary>
        public event Action<string, FaultState, WarnTypeEnum> FaultDetected;

        /// <summary>
        /// 当前是否存在故障
        /// </summary>
        public bool HasActiveFaults()
        {
            foreach (var item in _faultStates.Values)
            {
                var IsFault = item.Any(f => f.CurrentActiveFault != null && f.CurrentActiveFault != WarnTypeEnum.None);
                if (IsFault) 
                {
                    return true;
                } 
            }
            return false;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            // 通讯故障(实时扫描)
            CreateCommunication();
            // OPC故障点位（触发式）
            CreateOpcDetection();
            // 逻辑判定故障 （实时扫描）
            CreateCalculateFault();

            // 通讯类由主页面进行触发

        }


        /// <summary>
        /// 创建通讯故障信息表
        /// </summary>
        public void CreateCommunication()
        {
            var faults = new List<FaultState>
            {
                new FaultState { Name = "台位控制", Desc = "台位控制通讯异常" },
                new FaultState { Name = "测量柜", Desc = "测量柜通讯异常" },
                new FaultState { Name = "启动柜", Desc = "启动柜通讯异常" },
                new FaultState { Name = "发动机电参数", Desc = "发动机电参数通讯异常" },
                new FaultState { Name = "机油系统1", Desc = "机油系统1通讯异常" },
                new FaultState { Name = "机油系统2", Desc = "机油系统2通讯异常" },
                new FaultState { Name = "机油系统3", Desc = "机油系统3通讯异常" },
                new FaultState { Name = "机油系统4", Desc = "机油系统4通讯异常" },
                new FaultState { Name = "燃油系统1", Desc = "燃油系统1通讯异常" },
                new FaultState { Name = "燃油系统2", Desc = "燃油系统2通讯异常" },
                new FaultState { Name = "水系统1", Desc = "水系统1通讯异常" },
                new FaultState { Name = "水系统2", Desc = "水系统2通讯异常" },
                new FaultState { Name = "水系统3", Desc = "水系统3通讯异常" },
                new FaultState { Name = "水系统4", Desc = "水系统4通讯异常" },
                new FaultState { Name = "柴油机控制器", Desc = "柴油机控制器通讯异常" },
                new FaultState { Name = "台位主从通讯", Desc = "台位主从通讯异常" },
                new FaultState { Name = "励磁柜", Desc = "励磁柜通讯异常" },
            };

            _faultStates[FaultTypeEnum.communication] = faults;
        }

        /// <summary>
        /// 创建OPC检测故障信息表
        /// </summary>
        private void CreateOpcDetection()
        {
            var faults = new List<FaultState>() { };
            // 与PLC控制柜故障点位
            foreach (var item in Common.PipelineFaultGrp.DataValue)
            {
                faults.Add(new FaultState { Name = item.Key, Desc = item.Key });
            }

            // todo 后续哪些点位为1或0 记录故障
            _faultStates[FaultTypeEnum.opcDetection] = faults;
        }

        /// <summary>
        /// 创建逻辑判定故障信息表
        /// </summary>
        private void CreateCalculateFault()
        {
            var faults = new List<FaultState>
            {
                new FaultState { Name = "【燃油】粗滤器1前后压差过大", Desc = "【燃油】粗滤器1前后压差过大，粗滤器堵塞。" },
                new FaultState { Name = "【燃油】粗滤器2前后压差过大", Desc = "【燃油】粗滤器2前后压差过大，粗滤器堵塞。" },
                new FaultState { Name = "【燃油】精滤器1前后压差过大", Desc = "【燃油】精滤器1前后压差过大，精滤器堵塞。" },
                new FaultState { Name = "【燃油】精滤器2前后压差过大", Desc = "【燃油】精滤器2前后压差过大，精滤器堵塞。" },
                new FaultState { Name = "【机油】机滤器1前后压差过大", Desc = "【机油】机滤器1前后压差过大，机滤器堵塞。" },
                new FaultState { Name = "【机油】机滤器2前后压差过大", Desc = "【机油】机滤器2前后压差过大，机滤器堵塞。" },
            };

            _faultStates[FaultTypeEnum.calculate] = faults;
        }

        /// <summary>
        /// 更新模块状态
        /// </summary>
        /// <param name="faultEnum">故障类型</param>
        /// <param name="faultEnum">故障类型</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="currentIsNg">当前是否NG（1为NG）</param>
        public void CheckAndLogStatusChange(FaultTypeEnum faultEnum, WarnTypeEnum warnType, string moduleName, bool currentIsNg)
        {
            // 获取当前故障状态(1为故障)
            bool lastIsNg = GetLastFaultStatus(faultEnum, warnType, moduleName);

            // 状态发生变化
            if (currentIsNg != lastIsNg)
            {
                if (currentIsNg)
                {
                    // 新增故障：记录报警
                    RecordPlcNgRecord(faultEnum, warnType, moduleName);
                }
                else
                {
                    // 故障恢复：修改报警恢复时间
                    UpdateFaultEventRecord(faultEnum, moduleName);
                }

                // 更新对应的FaultState状态
                //UpdateFaultStateStatus(faultEnum, moduleName, currentIsNg);
            }
        }

        /// <summary>
        /// 获取上次故障状态 1：故障  0：无故障
        /// </summary>
        private bool GetLastFaultStatus(FaultTypeEnum faultEnum, WarnTypeEnum warnType, string moduleName)
        {
            string faultCode = $"{faultEnum}_{moduleName}";

            try
            {
                // 查询该故障代码的最新记录
                if (_faultStates.TryGetValue(faultEnum, out List<FaultState> faultList))
                {
                    var faultState = faultList.FirstOrDefault(f => f.Name == moduleName);
                    if (faultState != null)
                    {
                        return faultState.CurrentActiveFault == WarnTypeEnum.None ? false : true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"查询故障状态时发生异常：{ex.Message}");
            }

            // 默认返回非故障状态
            return false;
        }

        /// <summary>
        /// 更新FaultState的状态
        /// </summary>
        private void UpdateFaultStateStatus(FaultTypeEnum faultEnum, string moduleName, bool isNg)
        {
            if (_faultStates.TryGetValue(faultEnum, out List<FaultState> faultList))
            {
                var faultState = faultList.FirstOrDefault(f => f.Name == moduleName);
                if (faultState != null)
                {
                    faultState.AlarmConditionMet = isNg;
                    if (isNg)
                    {
                        faultState.AlarmStartTime = DateTime.Now;
                        faultState.CurrentActiveFault = WarnTypeEnum.Alarm;
                    }
                    else
                    {
                        faultState.CurrentActiveFault = WarnTypeEnum.None;
                    }
                }
            }
        }

        /// <summary>
        /// 记录NG报警信息
        /// </summary>
        /// <param name="faultEnum">故障类型</param>
        /// <param name="warnType">故障类型</param>
        /// <param name="moduleName">PLC模块名</param>
        private void RecordPlcNgRecord(FaultTypeEnum faultEnum, WarnTypeEnum warnType, string moduleName)
        {
            try
            {
                // 获取故障描述
                string faultDescription = "";
                if (_faultStates.TryGetValue(faultEnum, out List<FaultState> faultList))
                {
                    var faultState = faultList.FirstOrDefault(f => f.Name == moduleName);
                    faultDescription = faultState?.Desc ?? $" {moduleName}故障";

                    // 检查是否已存在未恢复的相同故障
                    if (faultState.CurrentActiveFault == warnType)
                    {
                        return;
                    }

                    // 通讯
                    faultState.CurrentActiveFault = warnType;
                    if (warnType == WarnTypeEnum.Alarm)
                    {
                        faultState.AlarmStartTime = DateTime.Now;
                    }
                    else if (warnType == WarnTypeEnum.Shedding)
                    {
                        faultState.SheddingStartTime = DateTime.Now;
                    }
                    else
                    {
                        faultState.StopStartTime = DateTime.Now;
                    }
                    // 记录故障到数据库
                    string id = Guid.NewGuid().ToString("N");
                    string faultCode = $"{GetFaultCode(faultEnum)}_{moduleName}";
                    FaultEvent faultEventRecord = new FaultEvent()
                    {
                        Id = id,
                        FaultCode = faultCode,
                        OccurTime = DateTime.Now,
                        Status = 0, // 0-未恢复
                        Description = faultDescription,
                        Severity = GetFaultSeverity(faultState.CurrentActiveFault),
                        CreatedAt = DateTime.Now
                    };
                    FaultDetected?.Invoke(faultCode, faultState, warnType);

                    int rows = faultEventService.AddFaultEventRecord(faultEventRecord);
                    if (rows == 0)
                    {
                        string msg = $"记录故障失败：{faultDescription}";
                        Var.LogInfo(msg);
                        Console.WriteLine(msg);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"记录故障时发生异常：{ex.Message}");
            }
        }

        /// <summary>
        /// 根据故障类型获取严重程度
        /// </summary>
        private int GetFaultSeverity(WarnTypeEnum warnType)
        {
            switch (warnType)
            {
                case WarnTypeEnum.Alarm:
                    return 1; // 报警
                case WarnTypeEnum.Shedding:
                    return 2; // 降载
                case WarnTypeEnum.Stop:
                    return 3; // 停机
                default:
                    return 1; // 默认报警
            }
        }

        /// <summary>
        /// 根据故障类型获取严重程度
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
                default:
                    return "通讯"; // 默认报警
            }
        }

        /// <summary>
        /// 当NG恢复时，更新故障记录
        /// </summary>
        private void UpdateFaultEventRecord(FaultTypeEnum faultEnum, string moduleName)
        {
            try
            {
                string faultCode = $"{GetFaultCode(faultEnum)}_{moduleName}";
                DateTime resetTime = DateTime.Now;
                int status = 1; // 1-已恢复

                // 查询所有未恢复的故障记录
                if (_faultStates.TryGetValue(faultEnum, out List<FaultState> faultList))
                {
                    var faultState = faultList.FirstOrDefault(f => f.Name == moduleName);
                    faultState.CurrentActiveFault = WarnTypeEnum.None;
                    FaultDetected?.Invoke(faultCode, faultState, WarnTypeEnum.None);
                }

                var dataTable = faultEventService.GetFaultEventRecordByFaultCode(faultCode);
                if (dataTable != null)
                {
                    int successCount = 0;
                    foreach (var record in dataTable)
                    {
                        int affectedRows = faultEventService.UpdateFaultEventRecordById(record.Id, resetTime, 1);
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
        /// 获取指定故障类型的当前状态
        /// </summary>
        public List<FaultState> GetFaultStates(FaultTypeEnum faultEnum)
        {
            return _faultStates.TryGetValue(faultEnum, out List<FaultState> states) ? states : new List<FaultState>();
        }

        /// <summary>
        /// 获取所有故障状态
        /// </summary>
        public Dictionary<FaultTypeEnum, List<FaultState>> GetAllFaultStates()
        {
            return _faultStates.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        /// <summary>
        /// 同步数据库状态到内存（用于程序启动时恢复状态）
        /// </summary>
        public void SyncDatabaseStatusToMemory()
        {
            try
            {
                // 查询最近24小时内所有未恢复的故障
                DateTime startTime = DateTime.Now.AddDays(-1);
                DateTime endTime = DateTime.Now;

                var unresolvedFaults = faultEventService.GetAllFaultEventRecord(startTime, endTime)
                    .Where(f => f.Status == 0)
                    .ToList();

                foreach (var fault in unresolvedFaults)
                {
                    // 解析故障代码：faultEnum_moduleName
                    var parts = fault.FaultCode.Split('_');
                    if (parts.Length >= 2)
                    {
                        // 尝试解析故障类型
                        if (Enum.TryParse<FaultTypeEnum>(parts[0], out var faultEnum))
                        {
                            string moduleName = string.Join("_", parts.Skip(1));
                            UpdateFaultStateStatus(faultEnum, moduleName, true);
                        }
                    }
                }

                Console.WriteLine($"同步完成，共恢复 {unresolvedFaults.Count} 个未恢复故障状态");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"同步数据库状态时发生异常：{ex.Message}");
            }
        }

        /// <summary>
        /// 故障复位
        /// </summary>
        public void FaultReset()
        {
            foreach (var item in _faultStates)
            {
                foreach (var fault in item.Value)
                {
                    // 如果有故障被检测到，并且与当前激活的故障不同，则触发事件
                    if (fault.CurrentActiveFault != WarnTypeEnum.None)
                    {
                        lock (locked)
                        {
                            // 将数据库的故障复位
                            var dataTable = faultEventService.GetFaultEventRecordByFaultCode(fault.Name);
                            if (dataTable != null)
                            {
                                int successCount = 0;
                                foreach (var record in dataTable)
                                {
                                    int affectedRows = faultEventService.UpdateFaultEventRecordById(record.Id, DateTime.Now, 1);
                                    if (affectedRows > 0)
                                    {
                                        successCount++;
                                        Console.WriteLine($"故障恢复成功：{fault.Name}，ID：{record.Id}");
                                    }
                                }

                                if (successCount == 0)
                                {
                                    string msg = $"警告：未能恢复任何故障记录：{fault.Name}";
                                    Var.LogInfo($"{msg}");
                                    Console.WriteLine($"{msg}");
                                }
                            }
                            else
                            {
                                string msg = $"警告：找不到对应的故障记录：{fault.Name}";
                                Var.LogInfo($"{msg}");
                                Console.WriteLine($"{msg}");
                            }

                            // 设置字典的状态
                            if (fault.CurrentActiveFault == WarnTypeEnum.Alarm)
                            {
                                fault.AlarmStartTime = DateTime.Now;
                            }
                            else if (fault.CurrentActiveFault == WarnTypeEnum.Shedding)
                            {
                                fault.SheddingStartTime = DateTime.Now;
                            }
                            else
                            {
                                fault.StopStartTime = DateTime.Now;
                            }

                            fault.CurrentActiveFault = WarnTypeEnum.None;
                            FaultDetected?.Invoke(fault.Name, fault, fault.CurrentActiveFault);
                        }
                    }
                }
            }
           
        }

        /// <summary>
        /// 强制恢复所有未恢复故障（用于特殊情况）
        /// </summary>
        public void ForceRecoverAllFaults()
        {
            try
            {
                DateTime resetTime = DateTime.Now;
                int status = 1;

                // 查询所有未恢复的故障
                var dataTable = faultEventService.GetFaultEventRecordByStatus(0);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    int successCount = 0;
                    foreach (DataRow record in dataTable.Rows)
                    {
                        string recordId = record.Field<string>("Id");
                        string faultCode = record.Field<string>("FaultCode");

                        int affectedRows = faultEventService.UpdateFaultEventRecordById(recordId, resetTime, status);
                        if (affectedRows > 0)
                        {
                            successCount++;

                            // 同时更新内存状态
                            var parts = faultCode.Split('_');
                            if (parts.Length >= 2 && Enum.TryParse<FaultTypeEnum>(parts[0], out var faultEnum))
                            {
                                string moduleName = string.Join("_", parts.Skip(1));
                                UpdateFaultStateStatus(faultEnum, moduleName, false);
                            }
                        }
                    }

                    Console.WriteLine($"强制恢复完成，成功恢复 {successCount} 条故障记录");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"强制恢复故障时发生异常：{ex.Message}");
            }
        }

        ///// <summary>
        ///// 更新模块状态
        ///// </summary>
        ///// <param name="moduleName"></param>
        ///// <param name="currentIsNg"></param>
        //public void CheckAndLogStatusChange(TestBedFaultEnum faultEnum, string moduleName, bool currentIsNg)
        //{
        //    _faultStates.TryGetValue(faultEnum, out var FaultList);

        //    // 获取该模块上次的状态（如果不存在，则默认为false-非NG）
        //    _lastModuleStatus.TryGetValue(moduleName, out bool lastIsNg);

        //    // 状态发生变化
        //    if (currentIsNg != lastIsNg)
        //    {
        //        if (currentIsNg && !_moduleLastFaultId.ContainsKey(moduleName))
        //        {
        //            //记录报警
        //            RecordPlcNgRecord(moduleName);
        //        }
        //        else
        //        {
        //            if (_moduleLastFaultId.ContainsKey(moduleName))
        //            {
        //                //修改报警恢复时间
        //                UpdateFaultEventRecord(moduleName);
        //            }

        //        }
        //        // 更新字典中的状态
        //        _lastModuleStatus[moduleName] = currentIsNg;
        //    }
        //}

        ///// <summary>
        ///// 记录NG报警信息
        ///// </summary>
        ///// <param name="moduleName">PLC模块名</param>
        //private void RecordPlcNgRecord(string moduleName)
        //{
        //    //记录故障到数据库
        //    string Id = Guid.NewGuid().ToString("N");
        //    FaultEvent faultEventRecord = new FaultEvent()
        //    {
        //        Id = Id,
        //        FaultCode = moduleName,
        //        OccurTime = DateTime.Now,
        //        Status = 0,
        //        Description = "",
        //        Severity = 0,     //暂无后续添加
        //        CreatedAt = DateTime.Now
        //    };
        //    FaultEventService faultEventService = new FaultEventService();
        //    int rows = faultEventService.AddFaultEventRecord(faultEventRecord);
        //    // 将ID存储到字典中，关联模块名
        //    _moduleLastFaultId[moduleName] = Id;
        //}

        ///// <summary>
        ///// 当NG恢复时，通过模块名对应的报警ID更新故障记录
        ///// </summary>
        //private void UpdateFaultEventRecord(string moduleName)
        //{
        //    // 1. 从字典中取出该模块最后一次报警的ID
        //    if (_moduleLastFaultId.TryGetValue(moduleName, out string faultId))
        //    {
        //        try
        //        {
        //            DateTime resetTime = DateTime.Now;
        //            int status = 1;
        //            // 2. 直接使用ID定位记录并更新
        //            int affectedRows = faultEventService.UpdateFaultEventRecordById(faultId, resetTime, status);

        //            if (affectedRows > 0)
        //            {
        //                _moduleLastFaultId.Remove(moduleName);
        //                Console.WriteLine($"模块[{moduleName}]的报警记录(ID:{faultId})已恢复。");
        //            }
        //            else
        //            {
        //                // 可能记录已被其他进程更新，或ID不对应
        //                Console.WriteLine($"警告：未能更新模块[{moduleName}]的报警记录(ID:{faultId})，可能已被处理。");
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }
        //    else
        //    {
        //        // 字典中没有找到ID，可能是程序重启后内存丢失，可以降级为按模块名和时间查询
        //        Console.WriteLine($"警告：模块[{moduleName}]恢复时找不到对应的报警ID，尝试按描述和状态查找...");
        //        // 这里可以调用一个备用更新方法（例如像之前一样用模块名查询）
        //        // FallbackUpdateByModuleName(moduleName);
        //    }
        //}

    }
}

// FaultDetectionService.cs
using MainUI.Config;
using MainUI.Fault;
using MainUI.Fault.Model;
using MainUI.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using MainUI.FSql;

namespace MainUI.Services
{
    /// <summary>
    /// ECM模块故障检测（发动机控制器）
    /// </summary>
    public class ECMFaultDetectionService
    {
        FaultEventService faultEventService = new FaultEventService();

        /// <summary>
        /// 所有点位的报警逻辑列表
        /// </summary>
        public Dictionary<string, FaultCondition> _faultConditions;
        /// <summary>
        /// 所有点位的报警状态列表
        /// </summary>
        public Dictionary<string, FaultState> _faultStates;

        /// <summary>
        /// 实时值存储的结构类
        /// </summary>
        public SensorData CurrentData { get; private set; }

        /// <summary>
        /// 出现故障后的委托
        /// </summary>
        public event Action<string, FaultState, WarnTypeEnum> FaultDetected;

        /// <summary>
        /// 报警的配置文件
        /// </summary>
        public FaultConfig faultConfig { get; set; }

        /// <summary>
        /// 当前是否存在故障
        /// </summary>
        public bool HasActiveFaults => _faultStates?.Any(f =>
            f.Value?.CurrentActiveFault != null &&
            f.Value.CurrentActiveFault != WarnTypeEnum.None) ?? false;


        /// <summary>
        /// 初始化加载
        /// </summary>
        public void Init()
        {
            // 监听型号变更
            EventTriggerModel.OnModelNameChanged += EventTriggerModel_OnModelNameChanged;

            CurrentData = new SensorData(); // 初始化报警实体类
            _faultStates = new Dictionary<string, FaultState>();

            faultConfig = new FaultConfig(Var.SysConfig.LastModel);
            _faultConditions = CreateFaultConditions(); // 故障逻辑

            // 初始化数组
            CurrentData.A1A6缸排气温度 = new double[6];
            CurrentData.B1B6缸排气温度 = new double[6];
            CurrentData._1_7档轴温 = new double[7];

            // 初始化故障状态
            foreach (var faultId in _faultConditions.Keys)
            {
                _faultStates[faultId] = new FaultState() { Name = faultId, Desc = faultId };
            }

            // 计时器实时扫描故障
            OnDetectionTimerElapsed();

            ////监听PLC故障点位
            //Common.PipelineFaultGrp.KeyValueChange += PipelineFaultGrp_KeyValueChange;
        }

        ///// <summary>
        ///// 记录发生在PLC中的故障
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void PipelineFaultGrp_KeyValueChange(object sender, Modules.EventArgsModel.DIValueChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Value == true && !faultRecorded.ContainsKey(e.Key))
        //        {
        //            //记录故障到数据库
        //            FaultEvent faultEventRecord = new FaultEvent()
        //            {
        //                Id = Guid.NewGuid().ToString("N"),
        //                FaultCode = e.Key,
        //                OccurTime = DateTime.Now,
        //                Status = 0,
        //                Description = e.Key,
        //                Severity = 0,     //暂无后续添加
        //                CreatedAt = DateTime.Now
        //            };

        //            faultEventService.AddFaultEventRecord(faultEventRecord);
        //            faultRecorded.Add(e.Key, faultEventRecord.Id);
        //        }
        //        if (e.Value == false && faultRecorded.ContainsKey(e.Key))
        //        {
        //            //接触故障修改写入恢复时间和故障状态
        //            DateTime resetTime = DateTime.Now;
        //            var dataTable = faultEventService.GetFaultEventRecordByFaultCode(e.Key);
        //            if (dataTable != null)
        //            {
        //                int successCount = 0;
        //                foreach (var record in dataTable)
        //                {
        //                    int affectedRows = faultEventService.UpdateFaultEventRecordById(record.Id, resetTime, 1);
        //                    if (affectedRows > 0)
        //                    {
        //                        successCount++;
        //                        Console.WriteLine($"故障恢复成功：{e.Key}，ID：{record.Id}");
        //                    }
        //                }

        //                if (successCount == 0)
        //                {
        //                    string msg = $"警告：未能恢复任何故障记录：{e.Key}";
        //                    Var.LogInfo($"{msg}");
        //                    Console.WriteLine($"{msg}");
        //                }
        //            }
        //            else
        //            {
        //                string msg = $"警告：找不到对应的故障记录：{e.Key}";
        //                Var.LogInfo($"{msg}");
        //                Console.WriteLine($"{msg}");
        //            }

        //            faultRecorded.Remove(e.Key);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Var.LogInfo($"更新故障记录时发生异常：{ex.ToString()}");
        //    }
        //}

        /// <summary>
        /// 型号改变时
        /// </summary>
        /// <param name="obj"></param>
        private void EventTriggerModel_OnModelNameChanged(string model)
        {
            // 通过配置文件加载
            faultConfig = new FaultConfig(model);
        }

        /// <summary>
        /// 创建故障逻辑
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, FaultCondition> CreateFaultConditions()
        {
            var conditions = new Dictionary<string, FaultCondition>();

            // 故障1: 高温水出水温度
            conditions["高温水出水温度"] = new FaultCondition
            {
                Name = "高温水出水温度",
                CheckAlarm = (data) => data.高温水出水温度 >= faultConfig.F1V1,
                CheckShedding = (data) => data.高温水出水温度 >= faultConfig.F1V2,
                CheckStop = null,
            };

            // 故障2: 中冷水进水温度
            conditions["中冷水进水温度"] = new FaultCondition
            {
                Name = "中冷水进水温度",
                CheckAlarm = (data) => data.中冷水进水温度 >= faultConfig.F2V1,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障3: 中冷水出水温度
            conditions["中冷水出水温度"] = new FaultCondition
            {
                Name = "中冷水出水温度",
                CheckAlarm = (data) => data.中冷水出水温度 >= faultConfig.F3V1,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障6: 后中冷后空气温度
            conditions["后中冷后空气温度"] = new FaultCondition
            {
                Name = "后中冷后空气温度",
                CheckAlarm = (data) => data.后中冷后空气温度 >= faultConfig.F6V1,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障7: 主油道进口油温
            conditions["主油道进口油温"] = new FaultCondition
            {
                Name = "主油道进口油温",
                CheckAlarm = (data) => data.主油道进口油温 >= faultConfig.F7V1,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障8: 前压气机出口空气温度
            conditions["前压气机出口空气温度"] = new FaultCondition
            {
                Name = "前压气机出口空气温度",
                CheckAlarm = (data) => data.前压气机出口空气温度 >= faultConfig.F8V1,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障9: 后压气机出口空气温度
            conditions["后压气机出口空气温度"] = new FaultCondition
            {
                Name = "后压气机出口空气温度",
                CheckAlarm = (data) => data.后压气机出口空气温度 >= faultConfig.F9V1,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障11: 主油道进口油压
            conditions["主油道进口油压"] = new FaultCondition
            {
                Name = "主油道进口油压",
                CheckAlarm = (data) => data.主油道进口油压 < faultConfig.F11V1 && data.发动机转速 > faultConfig.F11V2,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障14.1: 燃油精滤器后油压 (2 < p < 250 且 n > 600 rpm)
            conditions["燃油精滤器后油压1"] = new FaultCondition
            {
                Name = "燃油精滤器后油压1",
                CheckAlarm = (data) =>
                    data.燃油精滤器后油压 > faultConfig.F141V1 &&
                    data.燃油精滤器后油压 < faultConfig.F141V2 &&
                    data.发动机转速 > faultConfig.F141V3,
                AlarmDuration = faultConfig.F141V4,
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
                    return diff > faultConfig.F142V1;
                },
                AlarmDuration = faultConfig.F142V2,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障17: 机油泵出口油温
            conditions["机油泵出口油温"] = new FaultCondition
            {
                Name = "机油泵出口油温",
                CheckAlarm = (data) => data.机油泵出口油温 >= faultConfig.F17V1,
                CheckShedding = (data) => data.机油泵出口油温 >= faultConfig.F17V2,
                CheckStop = null,
            };

            // 故障18: 主油道末端油压
            conditions["主油道末端油压"] = new FaultCondition
            {
                Name = "主油道末端油压",
                CheckAlarm = (data) =>
                {
                    var warn1 = data.主油道末端油压 > faultConfig.F18V1 && data.主油道末端油压 < faultConfig.F18V2 && data.发动机转速 > faultConfig.F18V3;
                    var warn2 = data.主油道末端油压 > faultConfig.F18V4 && data.主油道末端油压 <= faultConfig.F18V5 && data.发动机转速 >= faultConfig.F18V6 && data.发动机转速 <= faultConfig.F18V7;
                    return warn1 || warn2;
                },
                CheckShedding = (data) => data.主油道末端油压 > faultConfig.F18V8 && data.主油道末端油压 < faultConfig.F18V9 && data.发动机转速 > faultConfig.F18V10,
                CheckStop = (data) => data.主油道末端油压 > faultConfig.F18V11 && data.主油道末端油压 < faultConfig.F18V12 && data.发动机转速 >= faultConfig.F18V13,
            };

            // 故障20: 后增压器进油压
            conditions["后增压器进油压"] = new FaultCondition
            {
                Name = "后增压器进油压",
                CheckAlarm = (data) => data.后增压器进口油压 <= faultConfig.F20V1,
                CheckShedding = (data) => data.后增压器进口油压 <= faultConfig.F20V2,
                CheckStop = null,
            };

            // 故障22: 前增压器转速
            conditions["前增压器转速"] = new FaultCondition
            {
                Name = "前增压器转速",
                CheckAlarm = (data) => data.前增压器转速 >= faultConfig.F22V1,
                AlarmDuration = 3,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障23: 后增压器转速
            conditions["后增压器转速"] = new FaultCondition
            {
                Name = "后增压器转速",
                CheckAlarm = (data) => data.后增压器转速 >= faultConfig.F23V1,
                AlarmDuration = 3,
                CheckShedding = null,
                CheckStop = null,
            };

            // 故障24: A1-A6缸排气温度
            conditions["A1-A6缸排气温度"] = new FaultCondition
            {
                Name = "A1-A6缸排气温度",
                CheckAlarm = (data) => data.A1A6缸排气温度.Max() >= faultConfig.F24V1,
                CheckShedding = (data) =>
                {
                    // todo 发动机功率需要计算
                    var result = data.发动机功率 >= faultConfig.F24V2;
                    var max = data.A1A6缸排气温度.Max();
                    var min = data.A1A6缸排气温度.Min();
                    return (max - min) > faultConfig.F24V3 && result;
                },
                SheddingDuration = faultConfig.F24V4,
                CheckStop = null,
                StopDuration = 0
            };

            // 故障25: B1-B6缸排气温度
            conditions["B1-B6缸排气温度"] = new FaultCondition
            {
                Name = "B1-B6缸排气温度",
                CheckAlarm = (data) => data.B1B6缸排气温度.Max() >= faultConfig.F25V1,
                CheckShedding = (data) =>
                {
                    // todo 发动机功率需要计算
                    var result = data.发动机功率 >= faultConfig.F25V2;
                    var max = data.B1B6缸排气温度.Max();
                    var min = data.B1B6缸排气温度.Min();
                    return (max - min) > faultConfig.F25V3 && result;
                },
                SheddingDuration = faultConfig.F25V4,
                CheckStop = null,
                StopDuration = 0
            };

            // 故障26: A涡前排气温度
            conditions["A涡前排气温度"] = new FaultCondition
            {
                Name = "A涡前排气温度",
                CheckAlarm = (data) => data.A涡前排气温度 >= faultConfig.F26V1,
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
                CheckAlarm = (data) => data.B涡前排气温度 >= faultConfig.F27V1,
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
                CheckShedding = (data) => data._1_7档轴温.Max() >= faultConfig.F28V1,
                SheddingDuration = 0,
                CheckStop = (data) => data._1_7档轴温.Max() >= faultConfig.F28V2 && data._1_7档轴温.Max() < faultConfig.F28V3,
                StopDuration = 0
            };

            // 故障29: 轴温监控装置通讯故障 为1报警
            conditions["轴温监控装置通讯故障"] = new FaultCondition
            {
                Name = "轴温监控装置通讯故障",
                CheckAlarm = (data) => data.轴温监控装置通讯故障 == faultConfig.F29V1,
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
                CheckStop = (data) => data.电喷转速1 >= faultConfig.F30V1,
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
                CheckStop = (data) => data.电喷转速2 >= faultConfig.F31V1,
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
                CheckStop = (data) => data.电喷状态 == faultConfig.F32V1,
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
                CheckStop = (data) => data.飞轮发动机转速1 >= faultConfig.F33V1,
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
                CheckStop = (data) => data.飞轮发动机转速2 >= faultConfig.F34V1,
                StopDuration = 0
            };

            // 故障35: 后增进油压卸载开关
            conditions["后增进油压卸载开关"] = new FaultCondition
            {
                Name = "后增进油压卸载开关",
                CheckAlarm = null,
                AlarmDuration = 0,
                CheckShedding = (data) => data.后增进油压卸载开关 == faultConfig.F35V1,
                SheddingDuration = 0,
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
                CheckStop = (data) => data.后增进油压停机开关 == faultConfig.F36V1 && !MiddleData.instnce.isStartupRecording,
                StopDuration = 0
            };

            // 故障37: 曲轴箱差压开关
            conditions["曲轴箱差压开关"] = new FaultCondition
            {
                Name = "曲轴箱差压开关",
                CheckAlarm = null,
                AlarmDuration = 0,
                CheckShedding = null,
                SheddingDuration = 0,
                CheckStop = (data) => data.曲轴箱差压开关 == faultConfig.F37V1 && !MiddleData.instnce.isStartupRecording,
                StopDuration = 0
            };

            return conditions;
        }

        /// <summary>
        /// 刷新数据的方法
        /// </summary>
        /// <param name="updateAction"></param>
        public void UpdateSensorData(Action<SensorData> updateAction)
        {
            lock (this)
            {
                updateAction(CurrentData);
            }
        }

        /// <summary>
        /// 循环检测故障
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDetectionTimerElapsed()
        {
            Thread t = new Thread(xxx =>
            {
                while (true)
                {
                    try
                    {
                        // 运行才检测（需要电机故障复位才清除原有故障） 并且有配置文件再检测
                        if (CurrentData.发动机转速 > 300 && faultConfig != null)
                        {
                            foreach (var fault in _faultConditions)
                            {
                                var faultId = fault.Key;
                                var condition = fault.Value;
                                var state = _faultStates[faultId];

                                // 按照优先级检测：停机 -> 降载 -> 报警
                                WarnTypeEnum detectedType = WarnTypeEnum.None;
                                DateTime now = DateTime.Now;

                                // 检测停机故障
                                if (condition.CheckStop != null && condition.CheckStop(CurrentData))
                                {
                                    lock (locked)
                                    {
                                        if (!state.StopConditionMet)
                                        {
                                            state.StopConditionMet = true;
                                            state.StopStartTime = now;
                                        }
                                        if ((now - state.StopStartTime).TotalSeconds >= condition.StopDuration)
                                        {
                                            // 时间维持才算故障
                                            detectedType = WarnTypeEnum.Stop;
                                        }

                                    }
                                }
                                else
                                {
                                    state.StopConditionMet = false;
                                }

                                // 如果没有停机故障，检测降载故障
                                if (detectedType == WarnTypeEnum.None && condition.CheckShedding != null && condition.CheckShedding(CurrentData))
                                {
                                    lock (locked)
                                    {
                                        if (!state.SheddingConditionMet)
                                        {
                                            state.SheddingConditionMet = true;
                                            state.SheddingStartTime = now;
                                        }
                                        if ((now - state.SheddingStartTime).TotalSeconds >= condition.SheddingDuration)
                                        {
                                            detectedType = WarnTypeEnum.Shedding;
                                        }
                                    }
                                }
                                else
                                {
                                    state.SheddingConditionMet = false;
                                }

                                // 如果没有停机或降载故障，检测报警故障
                                if (detectedType == WarnTypeEnum.None && condition.CheckAlarm != null && condition.CheckAlarm(CurrentData))
                                {
                                    lock (locked)
                                    {
                                        if (!state.AlarmConditionMet)
                                        {
                                            state.AlarmConditionMet = true;
                                            state.AlarmStartTime = now;
                                        }
                                        if ((now - state.AlarmStartTime).TotalSeconds >= condition.AlarmDuration)
                                        {
                                            detectedType = WarnTypeEnum.Alarm;
                                        }
                                    }
                                }
                                else
                                {
                                    state.AlarmConditionMet = false;
                                }

                                // 如果有故障被检测到，并且与当前激活的故障不同，则触发事件
                                if (detectedType != WarnTypeEnum.None && state.CurrentActiveFault != detectedType)
                                {
                                    lock (locked)
                                    {
                                        state.CurrentActiveFault = detectedType;
                                    }
                                    FaultDetected?.Invoke(state.Name, state, detectedType);

                                    // 记录故障到数据库
                                    string id = Guid.NewGuid().ToString("N");
                                    string faultCode = $"{state.Name}";
                                    FaultEvent faultEventRecord = new FaultEvent()
                                    {
                                        Id = id,
                                        FaultCode = faultCode,
                                        OccurTime = DateTime.Now,
                                        Status = 0, // 0-未恢复
                                        Description = state.Name,
                                        Severity = GetFaultSeverity(detectedType),
                                        CreatedAt = DateTime.Now
                                    };

                                    int rows = faultEventService.AddFaultEventRecord(faultEventRecord);
                                    if (rows == 0)
                                    {
                                        string msg = $"记录故障失败：{state.Name}";
                                        Var.LogInfo(msg);
                                        Console.WriteLine(msg);
                                    }
                                }
                                // 如果故障消失，也要触发None状态
                                else if (detectedType == WarnTypeEnum.None && state.CurrentActiveFault != WarnTypeEnum.None)
                                {
                                    state.CurrentActiveFault = WarnTypeEnum.None;
                                    FaultDetected?.Invoke(faultId, state, WarnTypeEnum.None);

                                    var dataTable = faultEventService.GetFaultEventRecordByFaultCode(faultId);
                                    if (dataTable != null)
                                    {
                                        int successCount = 0;
                                        foreach (var record in dataTable)
                                        {
                                            int affectedRows = faultEventService.UpdateFaultEventRecordById(record.Id, DateTime.Now, 1);
                                            if (affectedRows > 0)
                                            {
                                                successCount++;
                                                Console.WriteLine($"故障恢复成功：{faultId}，ID：{record.Id}");
                                            }
                                        }

                                        if (successCount == 0)
                                        {
                                            string msg = $"警告：未能恢复任何故障记录：{faultId}";
                                            Var.LogInfo($"{msg}");
                                            Console.WriteLine($"{msg}");
                                        }
                                    }
                                    else
                                    {
                                        string msg = $"警告：找不到对应的故障记录：{faultId}";
                                        Var.LogInfo($"{msg}");
                                        Console.WriteLine($"{msg}");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"故障检测异常: {ex.Message}");
                    }
                    Thread.Sleep(300);
                }
            });
            t.IsBackground = true;
            t.Name = "实时检测故障列表线程";
            t.Start();
        }


        /// <summary>
        /// 根据故障类型获取严重程度
        /// </summary>
        private int GetFaultSeverity(WarnTypeEnum WarnTypeEnum)
        {
            switch (WarnTypeEnum)
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
        /// 故障重新检测
        /// </summary>
        public void FaultCheckResend()
        {
            foreach (var fault in _faultStates)
            {
                // 如果有故障被检测到，并且与当前激活的故障不同，则触发事件
                if (fault.Value.CurrentActiveFault != WarnTypeEnum.None)
                {
                    FaultDetected?.Invoke(fault.Key, fault.Value, fault.Value.CurrentActiveFault);
                }
            }
        }

        public object locked { get; set; } = new object();

        /// <summary>
        /// 故障复位
        /// </summary>
        public void FaultReset()
        {

            foreach (var fault in _faultStates)
            {
                // 如果有故障被检测到，并且与当前激活的故障不同，则触发事件
                if (fault.Value.CurrentActiveFault != WarnTypeEnum.None)
                {
                    lock (locked)
                    {
                        // 将数据库的故障复位
                        var dataTable = faultEventService.GetFaultEventRecordByFaultCode(fault.Key);
                        if (dataTable != null)
                        {
                            int successCount = 0;
                            foreach (var record in dataTable)
                            {
                                int affectedRows = faultEventService.UpdateFaultEventRecordById(record.Id, DateTime.Now, 1);
                                if (affectedRows > 0)
                                {
                                    successCount++;
                                    Console.WriteLine($"故障恢复成功：{fault.Key}，ID：{record.Id}");
                                }
                            }

                            if (successCount == 0)
                            {
                                string msg = $"警告：未能恢复任何故障记录：{fault.Key}";
                                Var.LogInfo($"{msg}");
                                Console.WriteLine($"{msg}");
                            }
                        }
                        else
                        {
                            string msg = $"警告：找不到对应的故障记录：{fault.Key}";
                            Var.LogInfo($"{msg}");
                            Console.WriteLine($"{msg}");
                        }

                        // 设置字典的状态
                        if (fault.Value.CurrentActiveFault == WarnTypeEnum.Alarm)
                        {
                            fault.Value.AlarmStartTime = DateTime.Now;
                        }
                        else if (fault.Value.CurrentActiveFault == WarnTypeEnum.Shedding)
                        {
                            fault.Value.SheddingStartTime = DateTime.Now;
                        }
                        else
                        {
                            fault.Value.StopStartTime = DateTime.Now;
                        }

                        fault.Value.CurrentActiveFault = WarnTypeEnum.None;
                        FaultDetected?.Invoke(fault.Key, fault.Value, fault.Value.CurrentActiveFault);
                    }
                }
            }
        }

        /// <summary>
        /// 获取所有活跃故障
        /// </summary>
        /// <returns>字典：故障ID -> 故障状态</returns>
        public Dictionary<string, FaultState> GetActiveFaults()
        {
            var result = new Dictionary<string, FaultState>();

            if (_faultStates == null)
                return result;

            foreach (var fault in _faultStates)
            {
                if (fault.Value != null && fault.Value.CurrentActiveFault != WarnTypeEnum.None)
                {
                    result.Add(fault.Key, fault.Value);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取特定类型的活跃故障
        /// </summary>
        /// <param name="faultType">故障类型</param>
        /// <returns>故障ID列表</returns>
        public List<string> GetActiveFaultsByType(WarnTypeEnum faultType)
        {
            if (_faultStates == null)
                return new List<string>();

            return _faultStates
                .Where(f => f.Value != null && f.Value.CurrentActiveFault == faultType)
                .Select(f => f.Key)
                .ToList();
        }

        /// <summary>
        /// 获取故障详情（包括故障名称）
        /// </summary>
        /// <returns>故障详情列表</returns>
        public List<FaultDetail> GetFaultDetails()
        {
            var details = new List<FaultDetail>();

            if (_faultStates == null || _faultConditions == null)
                return details;

            foreach (var fault in _faultStates)
            {
                if (fault.Value.CurrentActiveFault != WarnTypeEnum.None)
                {
                    var faultId = fault.Key;
                    var faultCondition = _faultConditions.ContainsKey(faultId) ? _faultConditions[faultId] : null;

                    details.Add(new FaultDetail
                    {
                        FaultId = faultId,
                        FaultName = faultCondition?.Name ?? "未知故障",
                        FaultState = fault.Value,
                        FaultType = fault.Value.CurrentActiveFault,
                        DetectionTime = GetDetectionTime(fault.Value, fault.Value.CurrentActiveFault)
                    });
                }
            }

            return details;
        }

        /// <summary>
        /// 根据故障类型获取对应的检测时间
        /// </summary>
        private DateTime GetDetectionTime(FaultState state, WarnTypeEnum faultType)
        {
            if (faultType == WarnTypeEnum.Alarm)
            {
                return state.AlarmStartTime;
            }
            else if (faultType == WarnTypeEnum.Shedding)
            {
                return state.SheddingStartTime;
            }
            else if (faultType == WarnTypeEnum.Stop)
            {
                return state.StopStartTime;
            }
            else
            {
                return DateTime.MinValue;
            }
        }
    }


    /// <summary>
    /// 故障详情类
    /// </summary>
    public class FaultDetail
    {
        public string FaultId { get; set; }
        public string FaultName { get; set; }
        public FaultState FaultState { get; set; }
        public WarnTypeEnum FaultType { get; set; }
        public DateTime DetectionTime { get; set; }

        public override string ToString()
        {
            return $"故障ID: {FaultId}, 名称: {FaultName}, 类型: {FaultType}, 检测时间: {DetectionTime}";
        }
    }
}

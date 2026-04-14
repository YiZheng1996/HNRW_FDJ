using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Procedure.Test.Auto
{
    using System;
    using System.Timers;

    namespace ExcitationControlSystem
    {
        /// <summary>
        /// 励磁控制系统
        /// 实现励磁的分段控制、功率修正、转速控制等功能
        /// </summary>
        public class ExcitationController
        {
            // 系统变量定义
            private readonly SystemVariables _vars = new SystemVariables();
            private readonly Timer _timer;

            // 数据访问接口（模拟Report和Report1）
            private readonly IDataAccess _dataAccess;

            public ExcitationController(IDataAccess dataAccess)
            {
                _dataAccess = dataAccess;
                _timer = new Timer(1000); // 1000ms间隔
                _timer.Elapsed += OnTimerElapsed;
            }

            /// <summary>
            /// 启动控制系统
            /// </summary>
            public void Start()
            {
                _timer.Start();
                Console.WriteLine("励磁控制系统已启动");
            }

            /// <summary>
            /// 停止控制系统
            /// </summary>
            public void Stop()
            {
                _timer.Stop();
                Console.WriteLine("励磁控制系统已停止");
            }

            /// <summary>
            /// 定时器事件处理
            /// </summary>
            private void OnTimerElapsed(object sender, ElapsedEventArgs e)
            {
                try
                {
                    ExecuteMainLogic();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"执行错误: {ex.Message}");
                }
            }

            /// <summary>
            /// 主逻辑执行
            /// </summary>
            private void ExecuteMainLogic()
            {
                // 主状态机
                if (_vars.StartState == 3)
                {
                    ExecuteState3();
                }
                else if (_vars.StartState == 2)
                {
                    ExecuteState2();
                }
                else if (_vars.StartState == 1)
                {
                    ExecuteState1();
                }
                else if (_vars.StartState == 100)
                {
                    HandleTestEnd();
                }

                // 暂停处理
                if (_vars.VB600_08 && _vars.StopFlag >= 1)
                {
                    HandlePauseState();
                }

                // 计算差值
                CalculateDifferences();

                // 转速为0处理
                if (_vars.Speed == 0)
                {
                    _vars.DA11 = 330;
                }

                // 下一个工况停车报警
                CheckNextConditionAlert();
            }

            /// <summary>
            /// 状态3：励磁控制逻辑
            /// </summary>
            private void ExecuteState3()
            {
                if (!_vars.VB600_08)
                {
                    _vars.Second++;

                    // 励磁到位后开始计时
                    if (_vars.TorqueDifference1 <= 0.08 || _vars.TSecond > 0)
                    {
                        _vars.TSecond++;
                    }

                    // 励磁修正逻辑
                    if (_vars.DriverControlNO == 0)
                    {
                        HandleExcitationCorrection();

                        // 转速和分段励磁逻辑
                        HandleSpeedAndExcitationSegmentation();

                        // 励磁输出控制
                        HandleExcitationOutput();
                    }

                    // 时间累计
                    if (_vars.Second >= 60)
                    {
                        _vars.Minute++;
                        _vars.Second = 0;
                        _vars.TotalTime++;
                    }

                    // 判断运行时间是否到达设定值
                    if (_vars.Minute < _vars.SetupTime)
                    {
                        // 继续运行
                        return;
                    }

                    // 时间到达，切换到下一个工况
                    _vars.TorqueCValue = _vars.DA12;
                    _vars.TorqueVariable = _vars.DA12;
                    _vars.NextTorque = _dataAccess.GetCellDouble(-1, _vars.NTorqueConditionNo + 2, 7);
                    _vars.LastPower = _dataAccess.GetCellDouble(-1, _vars.ConditionNo + 2, 9);
                    _vars.StopFlag = 0;
                    _vars.Yg1++;
                    _vars.StartState = 2;
                    _vars.Setup1++;
                }
            }

            /// <summary>
            /// 励磁修正处理
            /// </summary>
            private void HandleExcitationCorrection()
            {
                if (_vars.DA12 > _vars.TargetPower * 0.005 && _vars.TargetPower > 0)
                {
                    double power = _vars.ActualPower;

                    // 根据功率范围和功率差计算修正值
                    _vars.XVariable = CalculateCorrectionValue(power, _vars.PowerDifference);

                    // 励磁修正
                    if (_vars.TargetPower > _vars.ActualPower)
                    {
                        // 正修正
                        if ((_vars.TSecond >= 5 && (_vars.TSecond - 5) % 8 == 0) || _vars.TSecond == 5)
                        {
                            if (_vars.DA12 <= _vars.Torque * 1.03 && _vars.TargetPower >= 2500)
                            {
                                _vars.XTorqueVariable = _vars.DA12 + _vars.XVariable;
                                _vars.DA12 = _vars.XTorqueVariable;
                            }
                        }

                        if ((_vars.TSecond >= 5 && (_vars.TSecond - 5) % 6 == 0) || _vars.TSecond == 5)
                        {
                            if (_vars.DA12 <= _vars.Torque * 1.05 && _vars.TargetPower < 2500)
                            {
                                _vars.XTorqueVariable = _vars.DA12 + _vars.XVariable;
                                _vars.DA12 = _vars.XTorqueVariable;
                            }
                        }
                    }
                    else
                    {
                        // 负修正
                        if ((_vars.TSecond % 2 == 0 && _vars.TSecond > 10) || _vars.TSecond == 10)
                        {
                            _vars.XTorqueVariable = _vars.DA12 - _vars.XVariable;
                            _vars.DA12 = _vars.XTorqueVariable;
                        }
                    }
                }
            }

            /// <summary>
            /// 计算修正值
            /// </summary>
            private double CalculateCorrectionValue(double power, double powerDiff)
            {
                if (power >= 3500 && power <= 4600)
                {
                    return powerDiff switch
                    {
                        >= 0 and <= 29 => 0.01,
                        >= 30 and <= 49 => 0.02,
                        >= 50 and <= 99 => 0.04,
                        >= 100 and <= 199 => 0.06,
                        >= 200 and <= 299 => 0.08,
                        >= 300 and <= 500 => 0.12,
                        _ => 0
                    };
                }
                else if (power >= 2500 && power < 3500)
                {
                    return powerDiff switch
                    {
                        >= 0 and <= 19 => 0.01,
                        >= 20 and <= 49 => 0.02,
                        >= 50 and <= 99 => 0.05,
                        >= 100 and <= 199 => 0.06,
                        >= 200 and <= 299 => 0.08,
                        >= 300 and <= 500 => 0.12,
                        _ => 0
                    };
                }
                else if (power >= 1500 && power < 2500)
                {
                    return powerDiff switch
                    {
                        >= 0 and <= 19 => 0.015,
                        >= 20 and <= 49 => 0.03,
                        >= 50 and <= 99 => 0.05,
                        >= 100 and <= 199 => 0.08,
                        >= 200 and <= 299 => 0.1,
                        >= 300 and <= 500 => 0.15,
                        >= 501 and <= 1000 => 0.2,
                        _ => 0
                    };
                }
                else if (power >= 50 && power < 1500)
                {
                    return powerDiff switch
                    {
                        >= 0 and <= 19 => 0.02,
                        >= 20 and <= 49 => 0.05,
                        >= 50 and <= 99 => 0.1,
                        >= 100 and <= 199 => 0.2,
                        >= 200 and <= 299 => 0.3,
                        >= 300 and <= 500 => 0.3,
                        _ => 0
                    };
                }

                return 0;
            }

            /// <summary>
            /// 转速和分段励磁处理
            /// </summary>
            private void HandleSpeedAndExcitationSegmentation()
            {
                if (_vars.LastPower < _vars.TargetPower)
                {
                    if (_vars.TorqueDifference >= 5)
                    {
                        // 升功率分段给励磁（每5s，35%，20%，15%，15%，15%）
                        _vars.WVariable = _vars.Second switch
                        {
                            >= 4 and <= 9 => _vars.TorqueDifference * 0.35,
                            >= 10 and <= 14 => _vars.TorqueDifference * 0.20,
                            >= 15 and <= 19 => _vars.TorqueDifference * 0.15,
                            >= 20 and <= 24 => _vars.TorqueDifference * 0.15,
                            25 => _vars.TorqueDifference * 0.15,
                            _ => 0
                        };
                        _vars.Variable = _vars.WVariable * 0.2;
                    }
                    else if (_vars.TorqueDifference >= 2)
                    {
                        // 升功率分段给励磁（25%，25%，20%，15%，15%）
                        _vars.WVariable = _vars.Second switch
                        {
                            4 => _vars.TorqueDifference * 0.25,
                            7 => _vars.TorqueDifference * 0.25,
                            10 => _vars.TorqueDifference * 0.20,
                            13 => _vars.TorqueDifference * 0.15,
                            16 => _vars.TorqueDifference * 0.15,
                            _ => 0
                        };
                        _vars.Variable = _vars.WVariable * 0.35;
                    }
                    else if (_vars.TorqueDifference < 2)
                    {
                        // 升功率分段给励磁
                        _vars.WVariable = _vars.Second switch
                        {
                            4 => _vars.TorqueDifference * 0.4,
                            9 => _vars.TorqueDifference * 0.3,
                            14 => _vars.TorqueDifference * 0.3,
                            _ => 0
                        };
                        _vars.Variable = _vars.WVariable * 0.2;
                    }
                }
                else
                {
                    // 降功率分段给励磁（每3s，30%，30%，40%）
                    _vars.Variable = _vars.Second switch
                    {
                        >= 0 and <= 1 => _vars.TorqueDifference * 0.3,
                        2 => _vars.TorqueDifference * 0.3,
                        5 => _vars.TorqueDifference * 0.4,
                        _ => 0
                    };
                }
            }

            /// <summary>
            /// 励磁输出控制
            /// </summary>
            private void HandleExcitationOutput()
            {
                if (_vars.Torque > _vars.TorqueVariable)
                {
                    if (_vars.LastPower < _vars.TargetPower)
                    {
                        // 升功率
                        _vars.DA11 = _vars.Speed;

                        if (_vars.Minute == 0 && _vars.Second >= 5 && _vars.Second <= 30 && _vars.TorqueDifference >= 5)
                        {
                            _vars.TorqueVariable = Math.Min(_vars.TorqueVariable + _vars.Variable, _vars.Torque);
                            _vars.DA12 = _vars.TorqueVariable;
                        }

                        if (_vars.Minute == 0 && _vars.Second >= 5 && _vars.Second < 25 && _vars.TorqueDifference < 5)
                        {
                            _vars.TorqueVariable = Math.Min(_vars.TorqueVariable + _vars.Variable, _vars.Torque);
                            _vars.DA12 = _vars.TorqueVariable;
                        }
                    }
                    else
                    {
                        // 降功率
                        if ((_vars.Minute == 0 && _vars.Second % 3 == 0 && _vars.Second <= 10) ||
                            (_vars.Minute == 0 && _vars.Second == 1))
                        {
                            _vars.TorqueVariable = Math.Min(_vars.TorqueVariable + _vars.Variable, _vars.Torque);
                            _vars.DA12 = _vars.TorqueVariable;
                        }

                        if (_vars.Minute == 0 && _vars.Second % 3 == 0 && _vars.Second > 0 && _vars.Second < 27)
                        {
                            _vars.DA11 = _vars.Speed;
                        }
                    }
                }
                else
                {
                    if (_vars.LastPower < _vars.TargetPower)
                    {
                        // 升功率
                        _vars.DA11 = _vars.Speed;

                        if (_vars.Minute == 0 && _vars.Second >= 5 && _vars.Second <= 30 && _vars.TorqueDifference >= 5)
                        {
                            _vars.TorqueVariable = Math.Max(_vars.TorqueVariable - _vars.Variable, _vars.Torque);
                            _vars.DA12 = _vars.TorqueVariable;
                        }

                        if (_vars.Minute == 0 && _vars.Second >= 5 && _vars.Second < 25 && _vars.TorqueDifference < 5)
                        {
                            _vars.TorqueVariable = Math.Min(_vars.TorqueVariable + _vars.Variable, _vars.Torque);
                            _vars.DA12 = _vars.TorqueVariable;
                        }
                    }
                    else
                    {
                        // 降功率
                        if ((_vars.Minute == 0 && _vars.Second % 3 == 0 && _vars.Second <= 10) ||
                            (_vars.Minute == 0 && _vars.Second == 1))
                        {
                            _vars.TorqueVariable = Math.Max(_vars.TorqueVariable - _vars.Variable, _vars.Torque);
                            _vars.DA12 = _vars.TorqueVariable;
                        }

                        if (_vars.Minute == 0 && _vars.Second % 3 == 0 && _vars.Second > 0 && _vars.Second < 27)
                        {
                            _vars.DA11 = _vars.Speed;
                        }
                    }
                }
            }

            /// <summary>
            /// 状态2：循环试验逻辑
            /// </summary>
            private void ExecuteState2()
            {
                _vars.Xg1 = (_vars.Content2 - 1) * 5 + 1;
                _vars.Setup1B = _dataAccess.GetCellDouble(-1, 1, _vars.Xg1);

                if (_vars.Setup1B >= _vars.Setup1)
                {
                    // 读取当前工况参数
                    _vars.SetupTime = _dataAccess.GetCellDouble(-1, _vars.Yg1 + 2, _vars.Xg1 + 4);
                    _vars.SetupSpeed = _dataAccess.GetCellDouble(-1, _vars.Yg1 + 2, _vars.Xg1 + 3);
                    _vars.NJSetup = _dataAccess.GetCellDouble(-1, _vars.Yg1 + 2, _vars.Xg1 + 2);

                    string conditionDesc = _dataAccess.GetCellString(-1, _vars.Yg1 + 2, _vars.Xg1 + 1);
                    _vars.ConditionNo = ParseConditionNo(conditionDesc);

                    string nextTorqueDesc = _dataAccess.GetCellString(-1, _vars.Yg1 + 3, _vars.Xg1 + 1);
                    _vars.NTorqueConditionNo = ParseConditionNo(nextTorqueDesc);

                    _vars.Torque = _dataAccess.GetCellDouble(-1, _vars.ConditionNo + 2, 7);

                    // 第一个工况点或切入工况的特殊处理
                    if (_vars.Setup1 == 1)
                    {
                        _vars.TorqueCValue = _vars.DA12;
                        _vars.NextTorque = _dataAccess.GetCellDouble(-1, _vars.ConditionNo + 2, 7);
                    }

                    if (_vars.CutInFlag)
                    {
                        _vars.LastPower = 0;
                        _vars.TorqueCValue = _vars.DA12;
                        _vars.NextTorque = _dataAccess.GetCellDouble(-1, _vars.ConditionNo + 2, 7);
                    }

                    _vars.Day360Desc = _dataAccess.GetCellString(-1, _vars.Yg2, 4);
                    _vars.Speed = _dataAccess.GetCellDouble(-1, _vars.ConditionNo + 2, 6);
                    _vars.TargetPower = _dataAccess.GetCellDouble(-1, _vars.ConditionNo + 2, 9);
                    _vars.StartState = 3;

                    if (!_vars.PauseRecoveryFlag)
                    {
                        _vars.Second = 0;
                        _vars.Minute = 0;
                    }

                    _vars.TSecond = 0;
                    _vars.CutInFlag = false;
                    _vars.PauseRecoveryFlag = false;
                }
                else
                {
                    _vars.Yg2++;
                    _vars.StartState = 1;
                }
            }

            /// <summary>
            /// 解析工况号
            /// </summary>
            private int ParseConditionNo(string description)
            {
                return description switch
                {
                    "1.1a" => 43,
                    "1a" => 44,
                    _ => int.TryParse(description, out int result) ? result : 0
                };
            }

            /// <summary>
            /// 状态1：开始试验，获取步骤
            /// </summary>
            private void ExecuteState1()
            {
                _vars.Setup1A = _dataAccess.GetCellDouble(-1, _vars.Yg2, 1);

                if (_vars.Setup1A > 0)
                {
                    _vars.Setup2 = _dataAccess.GetCellDouble(-1, _vars.Yg2, 1);
                    _vars.Content2 = _dataAccess.GetCellDouble(-1, _vars.Yg2, 3);
                    _vars.CycleCodeDesc = _dataAccess.GetCellString(-1, _vars.Yg2, 2);
                    _vars.Day360Desc = _dataAccess.GetCellString(-1, _vars.Yg2, 4);
                    _vars.StartState = 2;

                    if (_vars.CutInFlag)
                    {
                        _vars.Yg1 = _vars.Setup1;
                    }
                    else
                    {
                        _vars.Setup1 = 1;
                        _vars.Yg1 = 1;
                    }
                }
                else
                {
                    _vars.StartState = 100;
                    Console.WriteLine("试验结束");
                }
            }

            /// <summary>
            /// 处理测试结束
            /// </summary>
            private void HandleTestEnd()
            {
                _vars.VB600_01 = false;
                _vars.Torque = 0;
                Console.WriteLine("测试结束");
            }

            /// <summary>
            /// 暂停状态处理
            /// </summary>
            private void HandlePauseState()
            {
                _vars.StopSecond++;

                if (_vars.DriverControlNO == 0)
                {
                    // 降功率分段给励磁增加（每5s增加，40%，30%，30%）
                    _vars.StopVariable = _vars.StopSecond switch
                    {
                        >= 0 and <= 1 => _vars.StopDA12 * 0.4,
                        2 => _vars.StopDA12 * 0.3,
                        5 => _vars.StopDA12 * 0.3,
                        _ => 0
                    };

                    if ((_vars.StopMinute == 0 && _vars.StopSecond % 3 == 0) ||
                        (_vars.StopMinute == 0 && _vars.StopSecond == 1))
                    {
                        _vars.StopTorqueVariable = Math.Max(_vars.StopTorqueVariable - _vars.StopVariable, 4);
                    }

                    _vars.DA12 = _vars.StopTorqueVariable;

                    if (_vars.StopMinute == 0 && _vars.StopSecond >= 5 && _vars.StopSecond <= 10)
                    {
                        _vars.DA11 = 400;
                    }
                }

                if (_vars.StopSecond >= 60)
                {
                    _vars.StopMinute++;
                    _vars.StopSecond = 0;
                }

                _vars.TorqueVariable = 4;
            }

            /// <summary>
            /// 计算各种差值
            /// </summary>
            private void CalculateDifferences()
            {
                // 计算下一个励磁和当前励磁的差值
                _vars.TorqueDifference = Math.Abs(_vars.NextTorque - _vars.TorqueCValue);

                // 计算功率和实际功率的差值
                _vars.PowerDifference = Math.Abs(_vars.ActualPower - _vars.TargetPower);

                // 计算目标励磁和当前励磁差值
                _vars.TorqueDifference1 = Math.Abs(_vars.DA12 - _vars.Torque);

                // 计算小时
                _vars.Hour = _vars.TotalTime / 60.0;

                // 更新VD464
                _vars.VD464 = _vars.Torque;
            }

            /// <summary>
            /// 检查下一个工况停车报警
            /// </summary>
            private void CheckNextConditionAlert()
            {
                int nextConditionNo = _dataAccess.GetCellDouble(-1, _vars.Yg1 + 3, _vars.Xg1 + 1);

                if (nextConditionNo == 0 && _vars.StopFlag == 0 &&
                    _vars.Setup1 < _vars.Setup1B && _vars.Setup1 > 0)
                {
                    _vars.DO1_03 = true; // 蜂鸣器报警
                    Console.WriteLine("停机工况提示");
                }
            }
        }

        /// <summary>
        /// 系统变量集合
        /// </summary>
        /// <summary>
        /// 系统变量集合
        /// 包含励磁控制系统中的所有状态变量和控制变量
        /// </summary>
        public class SystemVariables
        {
            // ===== 状态和控制变量 =====

            /// <summary>
            /// 系统启动状态
            /// 1: 开始试验，获取步骤
            /// 2: 循环试验逻辑
            /// 3: 励磁控制主逻辑
            /// 100: 试验结束
            /// </summary>
            public int StartState { get; set; }

            /// <summary>
            /// VB600的第8位状态
            /// 表示系统暂停/运行状态
            /// 0: 运行状态
            /// 1: 暂停状态
            /// </summary>
            public bool VB600_08 { get; set; }

            /// <summary>
            /// VB600的第1位状态
            /// 系统运行标志位
            /// 0: 系统停止
            /// 1: 系统运行
            /// </summary>
            public bool VB600_01 { get; set; }

            /// <summary>
            /// 驱动控制编号
            /// 0: 自动控制模式
            /// 非0: 手动控制模式或其他特殊模式
            /// </summary>
            public int DriverControlNO { get; set; }

            /// <summary>
            /// 停止标志位
            /// 0: 正常运行
            /// >=1: 停止状态（不同值可能表示不同的停止原因）
            /// </summary>
            public int StopFlag { get; set; }

            /// <summary>
            /// 切入工况标志
            /// true: 处于切入工况状态
            /// false: 非切入工况状态
            /// </summary>
            public bool CutInFlag { get; set; }

            /// <summary>
            /// 暂停恢复标志
            /// true: 从暂停状态恢复运行
            /// false: 正常状态
            /// </summary>
            public bool PauseRecoveryFlag { get; set; }

            // ===== 时间相关变量 =====

            /// <summary>
            /// 秒计时器（单位：秒）
            /// 每1000ms自动加1，范围0-59
            /// 用于控制逻辑的时间分段
            /// </summary>
            public int Second { get; set; }

            /// <summary>
            /// 分钟计时器（单位：分钟）
            /// 当Second达到60时加1
            /// 用于计算总运行时间
            /// </summary>
            public int Minute { get; set; }

            /// <summary>
            /// 总运行时间（单位：分钟）
            /// 从试验开始累计的总运行分钟数
            /// </summary>
            public int TotalTime { get; set; }

            /// <summary>
            /// 小时时间（单位：小时）
            /// 由TotalTime/60计算得出
            /// 用于显示运行小时数
            /// </summary>
            public double Hour { get; set; }

            /// <summary>
            /// 扭矩到达后的计时器（单位：秒）
            /// 从励磁到位开始计时
            /// 用于励磁修正的时间判断
            /// </summary>
            public int TSecond { get; set; }

            // ===== 暂停相关变量 =====

            /// <summary>
            /// 暂停秒计时器（单位：秒）
            /// 系统暂停期间的秒计时
            /// </summary>
            public int StopSecond { get; set; }

            /// <summary>
            /// 暂停分钟计时器（单位：分钟）
            /// 系统暂停期间的分钟计时
            /// </summary>
            public int StopMinute { get; set; }

            /// <summary>
            /// 暂停期间的励磁变量
            /// 用于暂停状态下励磁的分段减少
            /// </summary>
            public double StopVariable { get; set; }

            /// <summary>
            /// 暂停前DA12（励磁）的值
            /// 记录暂停时的励磁值，用于恢复计算
            /// </summary>
            public double StopDA12 { get; set; }

            /// <summary>
            /// 暂停期间的扭矩变量
            /// 暂停期间逐步减少的励磁值
            /// </summary>
            public double StopTorqueVariable { get; set; }

            // ===== 功率相关变量 =====

            /// <summary>
            /// 目标功率（单位：根据实际系统定义）
            /// 当前工况设定的目标功率值
            /// </summary>
            public double TargetPower { get; set; }

            /// <summary>
            /// 实际功率（单位：根据实际系统定义）
            /// 从功率传感器读取的实际功率值
            /// </summary>
            public double ActualPower { get; set; }

            /// <summary>
            /// 上一个功率值（单位：根据实际系统定义）
            /// 上一个工况的功率值，用于判断升降功率趋势
            /// </summary>
            public double LastPower { get; set; }

            /// <summary>
            /// 功率差值（单位：根据实际系统定义）
            /// 实际功率与目标功率的绝对差值
            /// 用于励磁修正计算
            /// </summary>
            public double PowerDifference { get; set; }

            // ===== 扭矩/励磁相关变量 =====

            /// <summary>
            /// 目标扭矩/励磁值（单位：根据实际系统定义）
            /// 当前工况设定的目标扭矩/励磁值
            /// </summary>
            public double Torque { get; set; }

            /// <summary>
            /// 参与计算的励磁值（单位：根据实际系统定义）
            /// 用于计算下一个励磁和当前励磁差值的参考值
            /// </summary>
            public double TorqueCValue { get; set; }

            /// <summary>
            /// 励磁设定变量（单位：根据实际系统定义）
            /// 当前给励磁的设定值，分段励磁的中间变量
            /// </summary>
            public double TorqueVariable { get; set; }

            /// <summary>
            /// 下一个励磁值（单位：根据实际系统定义）
            /// 下一个工况的励磁设定值
            /// </summary>
            public double NextTorque { get; set; }

            /// <summary>
            /// 扭矩差值（单位：根据实际系统定义）
            /// 下一个励磁和当前励磁的绝对差值
            /// 用于分段励磁计算
            /// </summary>
            public double TorqueDifference { get; set; }

            /// <summary>
            /// 扭矩差值1（单位：根据实际系统定义）
            /// 目标扭矩与当前扭矩的绝对差值
            /// 用于判断励磁是否到位
            /// </summary>
            public double TorqueDifference1 { get; set; }

            /// <summary>
            /// 励磁修正值（单位：根据实际系统定义）
            /// 根据功率差计算出的励磁修正量
            /// 正值为增加励磁，负值为减少励磁
            /// </summary>
            public double XVariable { get; set; }

            /// <summary>
            /// 功率中转值（单位：根据实际系统定义）
            /// 励磁修正计算中的中间变量
            /// 用于临时存储修正后的励磁值
            /// </summary>
            public double XTorqueVariable { get; set; }

            // ===== 转速相关变量 =====

            /// <summary>
            /// 转速设定值（单位：RPM）
            /// 当前工况设定的转速值
            /// 发送给DA11（转速控制通道）
            /// </summary>
            public double Speed { get; set; }

            /// <summary>
            /// 设定转速（单位：RPM）
            /// 从工况表中读取的转速设定值
            /// </summary>
            public double SetupSpeed { get; set; }

            // ===== DA输出变量 =====

            /// <summary>
            /// DA11输出值（单位：根据实际系统定义）
            /// 实际下发的转速控制信号
            /// 对应DAM3060C\DA11.PV
            /// </summary>
            public double DA11 { get; set; }

            /// <summary>
            /// DA12输出值（单位：根据实际系统定义）
            /// 实际下发的励磁控制信号
            /// 对应DAM3060C\DA12.PV
            /// </summary>
            public double DA12 { get; set; }

            // ===== 修正变量 =====

            /// <summary>
            /// 修正中间值（单位：根据实际系统定义）
            /// 励磁修正计算中的中间变量
            /// 用于存储按时间分段计算的励磁增量
            /// </summary>
            public double WVariable { get; set; }

            /// <summary>
            /// 每次励磁增加值（单位：根据实际系统定义）
            /// 根据TorqueDifference和WVariable计算出的
            /// 当前时间片段应增加的励磁量
            /// </summary>
            public double Variable { get; set; }

            // ===== 工况和步骤变量 =====

            /// <summary>
            /// 当前工况号
            /// 从工况表中读取的当前工况编号
            /// 用于查询工况参数
            /// </summary>
            public int ConditionNo { get; set; }

            /// <summary>
            /// 下一个励磁工况号
            /// 从工况表中读取的下一个工况编号
            /// 用于计算下一个励磁值
            /// </summary>
            public int NTorqueConditionNo { get; set; }

            /// <summary>
            /// 设定运行时间（单位：分钟）
            /// 当前工况设定的运行时间
            /// 用于判断工况是否完成
            /// </summary>
            public int SetupTime { get; set; }

            /// <summary>
            /// 当前步骤序号
            /// 当前正在执行的试验步骤序号
            /// 从1开始计数
            /// </summary>
            public int Setup1 { get; set; }

            /// <summary>
            /// 步骤检查变量A
            /// 从Report1读取的步骤检查值
            /// 用于判断是否有更多步骤
            /// </summary>
            public int Setup1A { get; set; }

            /// <summary>
            /// 步骤总数B
            /// 当前循环的总步骤数
            /// 用于判断是否完成当前循环
            /// </summary>
            public int Setup1B { get; set; }

            /// <summary>
            /// 总步骤数
            /// 从Report1读取的总试验步骤数
            /// </summary>
            public int Setup2 { get; set; }

            /// <summary>
            /// 内容2（循环标识）
            /// 表示当前是A、B、C等哪个循环过程
            /// 对应Report1的第3列
            /// </summary>
            public int Content2 { get; set; }

            /// <summary>
            /// 行索引1（工况行索引）
            /// 用于在Report中定位当前工况的行位置
            /// </summary>
            public int Yg1 { get; set; }

            /// <summary>
            /// 行索引2（步骤行索引）
            /// 用于在Report1中定位当前步骤的行位置
            /// </summary>
            public int Yg2 { get; set; }

            /// <summary>
            /// 列索引1（工况参数列索引）
            /// 用于在Report中定位工况参数的列位置
            /// 计算公式：(Content2-1)*5+1
            /// </summary>
            public int Xg1 { get; set; }

            /// <summary>
            /// NJ设定值
            /// 从Report读取的NJ相关设定参数
            /// 具体含义根据实际系统定义
            /// </summary>
            public int NJSetup { get; set; }

            // ===== 描述字符串 =====

            /// <summary>
            /// 天数描述
            /// 显示当前试验运行的天数
            /// 从Report1的第4列读取
            /// </summary>
            public string Day360Desc { get; set; } = "";

            /// <summary>
            /// 循环代码描述
            /// 显示当前循环的字母标识（A、B、C等）
            /// 从Report1的第2列读取
            /// </summary>
            public string CycleCodeDesc { get; set; } = "";

            // ===== 其他变量 =====

            /// <summary>
            /// VD464变量（扭矩显示）
            /// 用于显示或输出的扭矩值
            /// 对应VD464.PV
            /// </summary>
            public double VD464 { get; set; }

            /// <summary>
            /// DO1第3位（蜂鸣器控制）
            /// true: 蜂鸣器报警
            /// false: 蜂鸣器关闭
            /// 对应S7\DO\DO1.PV.03
            /// </summary>
            public bool DO1_03 { get; set; }
        }

        /// <summary>
        /// 数据访问接口
        /// </summary>
        public interface IDataAccess
        {
            double GetCellDouble(int sheetIndex, int row, int column);
            string GetCellString(int sheetIndex, int row, int column);
        }

        /// <summary>
        /// 主程序示例
        /// </summary>
        class Program
        {
            static void Main(string[] args)
            {
                // 创建数据访问实例（需要根据实际情况实现）
                IDataAccess dataAccess = new SampleDataAccess();

                // 创建励磁控制器
                var controller = new ExcitationController(dataAccess);

                // 启动控制器
                controller.Start();

                Console.WriteLine("按任意键停止...");
                Console.ReadKey();

                controller.Stop();
            }
        }

        /// <summary>
        /// 示例数据访问实现
        /// </summary>
        public class SampleDataAccess : IDataAccess
        {
            private readonly double[,] _reportData = new double[100, 20];
            private readonly double[,] _report1Data = new double[100, 20];

            public SampleDataAccess()
            {
                // 初始化示例数据
                InitializeSampleData();
            }

            private void InitializeSampleData()
            {
                // 这里可以初始化一些示例数据
                // 实际使用时应该从数据库或文件读取
            }

            public double GetCellDouble(int sheetIndex, int row, int column)
            {
                // 简化实现，实际使用时需要根据sheetIndex选择不同的数据源
                if (row >= 0 && row < 100 && column >= 0 && column < 20)
                {
                    return _reportData[row, column];
                }
                return 0;
            }

            public string GetCellString(int sheetIndex, int row, int column)
            {
                // 简化实现
                if (row >= 0 && row < 100 && column >= 0 && column < 20)
                {
                    return _reportData[row, column].ToString();
                }
                return "";
            }
        }
    }
}

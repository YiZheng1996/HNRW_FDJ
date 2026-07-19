using System;
using MainUI.Global;

namespace MainUI.Fault.Engine
{
    /// <summary>
    /// 按 SignalDef 的 Source 从对应数据源读取实时值。
    /// 型式试验分流（2026-07 v2 方案）：
    ///   - Source=TRDP 的信号在型式试验下按 OpcSource/OpcLabel 处置：
    ///       OpcSource="TRDP"          → ECM 仍发送，保持 TRDP 读取；
    ///       OpcLabel 有值             → 重定向到台位源（默认 AI2）；
    ///       两者皆空                  → 休眠，恒返回 0（启动自检显性化）。
    ///   - 重定向读值前检查源健康标志（AI2Grp.NoError / speedGrp.IsNoError），
    ///     异常返回 0 并节流日志，禁止拿缓存旧值参与判断（D4）。
    ///   - 例行试验：行为与原版完全一致，零影响。
    /// 所有读取 try/catch 兜底返回 0。
    /// </summary>
    public class EcmSignalProvider
    {
        /// <summary>健康门控节流日志：每源最短间隔（秒）</summary>
        private const int GATE_LOG_INTERVAL_SEC = 30;
        private DateTime _lastAi2GateLog = DateTime.MinValue;
        private DateTime _lastSpeedGateLog = DateTime.MinValue;

        /// <summary>当前是否型式试验（登录后固定，运行期不变）</summary>
        private static bool IsTypeTest
        {
            get { return Var.SysConfig != null && Var.SysConfig.LastTrialType == 1; }
        }

        /// <summary>
        /// 读取单个信号当前值。Source 不识别或异常时返回 0。
        /// </summary>
        public double Read(SignalDef sig)
        {
            if (sig == null) return 0;
            try
            {
                string src = (sig.Source ?? "").Trim().ToUpperInvariant();

                // >>> 新增：型式试验下 TRDP 源分流
                if (src == "TRDP" && IsTypeTest)
                {
                    string opcSrc = (sig.OpcSource ?? "").Trim().ToUpperInvariant();

                    // a) ECM 仍发送（轴温、曲轴箱压力、电喷转速/状态等）：保持 TRDP 读取
                    if (opcSrc == "TRDP")
                        return Var.TRDP.GetDicValue(sig.Label);

                    // b) 无重定向点位：休眠，恒 0（0 不触发 >=/==1 类判据，方向安全）
                    if (string.IsNullOrEmpty(sig.OpcLabel))
                        return 0;

                    // c) 重定向到台位源（默认 AI2）
                    return ReadRedirected(opcSrc, sig.OpcLabel);
                }
                // 新增结束

                switch (src)
                {
                    // ECM 电喷控制盒
                    case "TRDP":
                        return Var.TRDP.GetDicValue(sig.Label);

                    case "AI2":
                        // 管路/测量柜 PLC2
                        return SafeGroup(() => Common.AI2Grp[sig.Label]);

                    case "AI":
                        return SafeGroup(() => Common.AIgrp[sig.Label]);

                    // 飞轮盘转速
                    case "SPEED":
                        return SafeGroup(() => Common.speedGrp[sig.Label]);

                    case "DI":
                        // 数字量：true=>1，false=>0，与原 ? 1 : 0 写法一致
                        return SafeBool(() => Common.DIgrp[sig.Label]);

                    case "DO":
                        return SafeBool(() => Common.DOgrp[sig.Label]);

                    //  新增：串口设备组（燃油精滤后油压、水泵出口压力的台位点在此）
                    case "FUEL":
                        return SafeGroup(() => Common.fuelGrp[sig.Label]);

                    case "WATER":
                        return SafeGroup(() => Common.waterGrp[sig.Label]);
                    // <<< 新增结束

                    // 有功功率
                    case "POWER":
                        return MiddleData.instnce != null ? MiddleData.instnce.EnginePower : 0;

                    case "SPEEDVALUE":
                        // 当前发动机转速（中间层口径，与故障门控一致；型式分支已被 ① 吸收）
                        return MiddleData.instnce != null ? MiddleData.instnce.EngineSpeed : 0;

                    case "CONST":
                        return sig.Const;

                    default:
                        Var.LogInfo($"[ECM引擎] 未知数据源 Source={sig.Source} 信号={sig.Name}");
                        return 0;
                }
            }
            catch (Exception ex)
            {
                Var.LogInfo($"[ECM引擎] 读取信号失败 {sig.Name}({sig.Source}:{sig.Label}): {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 新增：型式试验重定向读取，带源健康门控（D4）。
        /// 通讯异常时返回 0 并节流日志，相关判断自然挂起（低压类规则有 GT 有效性前置，不误报）。
        /// </summary>
        private double ReadRedirected(string opcSrc, string opcLabel)
        {
            switch (opcSrc)
            {
                case "":
                case "AI2":
                    if (!Common.AI2Grp.NoError)
                    {
                        ThrottleLog(ref _lastAi2GateLog, "[型式试验] 测量柜(AI2)通讯异常，重定向信号按 0 处理，相关判断挂起。");
                        return 0;
                    }
                    return SafeGroup(() => Common.AI2Grp[opcLabel]);

                case "SPEED":
                    if (!Common.speedGrp.IsNoError)
                    {
                        ThrottleLog(ref _lastSpeedGateLog, "[型式试验] 转速模块通讯异常，重定向信号按 0 处理，相关判断挂起。");
                        return 0;
                    }
                    return SafeGroup(() => Common.speedGrp[opcLabel]);

                case "AI":
                    return SafeGroup(() => Common.AIgrp[opcLabel]);

                case "DI":
                    return SafeBool(() => Common.DIgrp[opcLabel]);

                case "DO":
                    return SafeBool(() => Common.DOgrp[opcLabel]);

                case "FUEL":
                    // TODO(自查)：fuelGrp 如有 NoError 类健康标志，在此挂门控
                    return SafeGroup(() => Common.fuelGrp[opcLabel]);

                case "WATER":
                    // TODO(自查)：waterGrp 如有 NoError 类健康标志，在此挂门控
                    return SafeGroup(() => Common.waterGrp[opcLabel]);

                default:
                    Var.LogInfo($"[ECM引擎] 型式试验未知重定向源 OpcSource={opcSrc} OpcLabel={opcLabel}");
                    return 0;
            }
        }

        private void ThrottleLog(ref DateTime last, string msg)
        {
            if ((DateTime.Now - last).TotalSeconds < GATE_LOG_INTERVAL_SEC) return;
            last = DateTime.Now;
            Var.LogInfo(msg);
        }

        private static double SafeGroup(Func<double> f)
        {
            try { return f(); } catch { return 0; }
        }

        private static double SafeBool(Func<bool> f)
        {
            try { return f() ? 1.0 : 0.0; } catch { return 0; }
        }
    }
}
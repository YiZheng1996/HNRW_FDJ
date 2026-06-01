using System;
using MainUI.Global;

namespace MainUI.Fault.Engine
{
    /// <summary>
    /// 按 SignalDef 的 Source 从对应数据源读取实时值，替代 ucWarnList 里硬编码的
    /// Var.TRDP.GetDicValue("固定名") 取值桥。所有读取都 try/catch 兜底返回 0，
    /// 行为与原 GetDicValue 一致（查不到/异常返回 0）。
    /// </summary>
    public class EcmSignalProvider
    {
        /// <summary>
        /// 读取单个信号当前值。Source 不识别或异常时返回 0。
        /// </summary>
        public double Read(SignalDef sig)
        {
            if (sig == null) return 0;
            try
            {
                string src = (sig.Source ?? "").Trim().ToUpperInvariant();
                switch (src)
                {
                    case "TRDP":
                        return Var.TRDP.GetDicValue(sig.Label);

                    case "AI2":
                        // 管路/测量柜 PLC2
                        return SafeGroup(() => Common.AI2Grp[sig.Label]);

                    case "AI":
                        return SafeGroup(() => Common.AIgrp[sig.Label]);

                    case "SPEED":
                        return SafeGroup(() => Common.speedGrp[sig.Label]);

                    case "DI":
                        // 数字量：true=>1，false=>0，与原 ? 1 : 0 写法一致
                        return SafeBool(() => Common.DIgrp[sig.Label]);

                    case "DO":
                        return SafeBool(() => Common.DOgrp[sig.Label]);

                    case "POWER":
                        return MiddleData.instnce != null ? MiddleData.instnce.EnginePower : 0;

                    case "SPEEDVALUE":
                        // 当前发动机转速（中间层口径，与故障门控一致）
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
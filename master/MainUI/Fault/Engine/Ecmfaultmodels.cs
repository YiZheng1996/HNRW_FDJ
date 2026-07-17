using System;
using System.Collections.Generic;

namespace MainUI.Fault.Engine
{
    /// <summary>
    /// 数据驱动 ECM 故障引擎的配置模型（每个型号一份 JSON，反序列化到此结构）。
    /// 设计目标：新增型号只需新增一份 {型号}.faults.json，不改任何代码。
    /// 这些 POCO 只承载配置，所有判定逻辑在 EcmFaultEngine 中。
    /// </summary>
    public class EcmFaultProfile
    {
        /// <summary>型号名（与 TRDPConfig/{型号}.xlsx、config/{型号}.ini 一致）</summary>
        public string Model { get; set; }

        /// <summary>引擎自检/调试用版本号，可空</summary>
        public string Version { get; set; }

        /// <summary>
        /// 该型号 ECM 故障检测涉及的信号清单：把“规范名”绑定到“具体数据源 + 该源里的键”。
        /// 这一层解决了不同型号信号名不一致的问题（如 高温水出水温度 vs 气缸盖出水温度）。
        /// </summary>
        public List<SignalDef> Signals { get; set; } = new List<SignalDef>();

        /// <summary>命名信号组，用于 MAX/MIN/SPREAD 聚合（如各缸排气温度、各档轴温）。</summary>
        public List<GroupDef> Groups { get; set; } = new List<GroupDef>();

        /// <summary>故障规则。引擎为每条规则生成一个 FaultCondition 接入现有检测主循环。</summary>
        public List<RuleDef> Rules { get; set; } = new List<RuleDef>();
    }

    /// <summary>信号定义：规范名 ↔ 数据源 + 键</summary>
    public class SignalDef
    {
        /// <summary>规范名（规则里引用的名字，型号无关）</summary>
        public string Name { get; set; }

        /// <summary>数据源：TRDP / AI2 / AI / Speed / DI / DO / Power / Const</summary>
        public string Source { get; set; }

        /// <summary>在该数据源字典里的键（型号相关，对应该型号协议的 DataLabel）。Power/Const 可忽略。</summary>
        public string Label { get; set; }

        /// <summary>Source=Const 时使用的常量值</summary>
        public double Const { get; set; }

        /// <summary>
        /// 新增（型式试验分流）：
        /// 型式试验下的重定向数据源：TRDP / AI2 / AI / SPEED / DI / DO / FUEL / WATER。
        /// - 填 "TRDP"：该信号型式下 ECM 仍发送，保持 TRDP 读取（如 轴温、曲轴箱压力、电喷转速/状态）。
        /// - 填其他源：按 OpcLabel 重定向到台位点位（默认 AI2，可省略）。
        /// - 与 OpcLabel 均为空：型式下该信号休眠（恒 0），启动自检显性化。
        /// 例行试验下本字段完全忽略。
        /// </summary>
        public string OpcSource { get; set; }

        /// <summary>
        /// 新增（型式试验分流）：型式试验下在重定向数据源里的键名。
        /// OpcSource="TRDP" 时可省略（沿用 Label）。
        /// </summary>
        public string OpcLabel { get; set; }
    }

    /// <summary>信号组（聚合用）</summary>
    public class GroupDef
    {
        public string Name { get; set; }
        public List<string> Members { get; set; } = new List<string>();
    }

    /// <summary>
    /// 一条故障规则。一条规则对应一个故障名（= 故障 Id / 描述）。
    /// 普通规则用 Checks；双传感器表决规则用 Vote。
    /// </summary>
    public class RuleDef
    {
        public string Name { get; set; }

        /// <summary>同级别多条 Check 之间是“或”关系；不同级别按 停机&gt;降载&gt;报警&gt;仅记录 优先级取最高。</summary>
        public List<CheckDef> Checks { get; set; } = new List<CheckDef>();

        /// <summary>双传感器/多传感器表决（规范 11.1 / 11.2），可空。</summary>
        public VoteDef Vote { get; set; }

        /// <summary>备注，引擎忽略</summary>
        public string Note { get; set; }
    }

    /// <summary>
    /// 一个判定条件。Terms 之间为“与”关系，全部为真该 Check 才成立。
    /// </summary>
    public class CheckDef
    {
        /// <summary>级别：Stop / Shedding / Alarm / Record（Record=仅记录不报警，映射 WarnTypeEnum.Tip）</summary>
        public string Level { get; set; }

        /// <summary>判定项（AND）</summary>
        public List<TermDef> Terms { get; set; } = new List<TermDef>();

        /// <summary>持续时间（秒）。0=立即。对应 FaultCondition 的 XxxDuration。</summary>
        public int Duration { get; set; }

        /// <summary>降载百分比（如 50 表示降到当前功率 50%）。引擎仅作为元数据透出，执行由现有降载通道完成。</summary>
        public double DeratePercent { get; set; }

        /// <summary>降载后持续 N 秒卸载（轴瓦温度规则）。0=不卸载。元数据。</summary>
        public int UnloadAfterSec { get; set; }
    }

    /// <summary>
    /// 单个判定项。Left 支持：
    ///   普通信号名；
    ///   MAX(组名) / MIN(组名) / SPREAD(组名)（=max-min）；
    ///   ABSDIFF(信号A,信号B)（=|A-B|）。
    /// 右值用 Value（常量）或 RightSignal（另一信号）。
    /// </summary>
    public class TermDef
    {
        public string Left { get; set; }

        /// <summary>GE(&gt;=) GT(&gt;) LE(&lt;=) LT(&lt;) EQ(==) NE(!=)</summary>
        public string Op { get; set; }

        public double? Value { get; set; }

        public string RightSignal { get; set; }
    }

    /// <summary>
    /// 多传感器表决（屏蔽故障传感器后再判定）。规范 11.1 增压器进油压、11.2 曲轴箱压力。
    /// 逻辑：剔除“故障传感器”后，剩余有效传感器全部满足比较条件才输出该级别；无有效传感器则不触发。
    /// </summary>
    public class VoteDef
    {
        public string Level { get; set; }

        /// <summary>参与表决的信号名</summary>
        public List<string> Sensors { get; set; } = new List<string>();

        /// <summary>每个传感器对应的“故障标志”信号名（值≠0 视为该传感器故障被屏蔽）。可空。</summary>
        public List<string> FaultFlags { get; set; } = new List<string>();

        /// <summary>无故障标志时，用有效量程判断传感器是否故障：超出 [ValidLow,ValidHigh] 视为故障。可空。</summary>
        public double? ValidLow { get; set; }
        public double? ValidHigh { get; set; }

        /// <summary>比较运算与阈值（如 LE 80、GE 0.6）</summary>
        public string Op { get; set; }
        public double Value { get; set; }

        public int Duration { get; set; }
    }
}
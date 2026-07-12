using MainUI.Global;
using MainUI.FSql.Model;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MainUI.FSql
{
    /// <summary>
    /// 手动出厂试验数据记录服务。
    /// 实现断电续接（重启后 MGid 不丢失，继续在同一批次后面追加）。
    /// </summary>
    public class ManualRecordService
    {
        public static ManualRecordService instnce = new ManualRecordService();

        /// <summary>
        /// 记录主表id（保留原属性名，但读写改为持久化到 SysParas）
        /// </summary>
        public string MGid
        {
            get => Var.SysConfig.ManualRecordMGid;
            set { Var.SysConfig.ManualRecordMGid = value; Var.SysConfig.Save(); }
        }

        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 记录的序号（保留原属性名，但读写改为持久化到 SysParas）
        /// </summary>
        public int Index
        {
            get => Var.SysConfig.ManualRecordIndex;
            set { Var.SysConfig.ManualRecordIndex = value; Var.SysConfig.Save(); }
        }

        //单条数据保存完成的事件委托
        public delegate void DataSavedEventHandler(object sender, DataSavedEventArgs e);
        public event DataSavedEventHandler DataSaved;

        public class DataSavedEventArgs : EventArgs
        {
            public ManualRecordPara manualRecord { get; set; }
        }

        /// <summary>
        /// 手动点击记录一条数据。
        /// saveInfo 需包含："Model"（柴油机型号）、"No"（柴油机编号）、"TestName"（试验项目名称）。
        /// 返回 true 表示保存成功；失败时通过 IsSuccess=false 及异常消息提示。
        /// </summary>
        public bool SaveOneRecord(Dictionary<string, string> saveInfo, out ManualRecordPara record, out string errorMsg)
        {
            record = null;
            errorMsg = "";

            try
            {
                // ── 1. 确保主表存在（首次记录或新批次时创建）──────────────
                if (string.IsNullOrEmpty(MGid))
                {
                    string newGid = Guid.NewGuid().ToString("N");
                    var main = new ManualRecordMain
                    {
                        gid = newGid,
                        BeginTime = DateTime.Now,
                        UserName = RW.UI.RWUser.User.Username,
                        DieselEngineModel = saveInfo["Model"],
                        DieselEngineNo = saveInfo["No"],
                        TestName = saveInfo["TestName"]
                    };

                    int mainResult = SaveMain(main);
                    if (mainResult == 0)
                    {
                        IsSuccess = false;
                        errorMsg = "创建记录主表失败，请检查数据库连接。";
                        return false;
                    }

                    MGid = newGid;   // 持久化
                    Index = 0;
                }

                // 采集快照
                record = CollectSnapshot(saveInfo);

                // 写库
                int rows = Save(record);
                if (rows == 0)
                {
                    IsSuccess = false;
                    errorMsg = "数据库写入返回0行，请检查连接。";
                    return false;
                }

                // ── 4. 更新计数（持久化）────────────────────────────────
                Index = (int)record.Index + 1;
                IsSuccess = true;

                DataSaved?.Invoke(this, new DataSavedEventArgs { manualRecord = record });
                return true;
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                errorMsg = "记录失败：" + ex.Message;
                return false;
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
        /// 开始新批次：清空 MGid 和计数，下次 SaveOneRecord 时自动创建新主表行。
        /// 旧批次数据不会被删除，可在报表界面按批次查看。
        /// </summary>
        public void StartNewBatch()
        {
            MGid = "";
            Index = 0;
        }

        /// <summary>
        /// 优先从 TRDP 取值；TRDP 没有这个信号（未配置或还没收到过推送）时，回退到原有数据源（OPC/AI2Grp等）。
        /// </summary>
        private static double GetValuePreferTRDP(string trdpKey, Func<double> fallback)
        {
            if (Var.TRDP != null && Var.TRDP.TryGetDicValue(trdpKey, out double v))
                return v;
            return SafeGet(fallback);
        }

        // 快照采集
        private ManualRecordPara CollectSnapshot(Dictionary<string, string> saveInfo)
        {
            var now = DateTime.Now;

            var r = new ManualRecordPara
            {
                // ══════════════════ 基类（IRecordData）通用字段 ══════════════════
                gid = Guid.NewGuid().ToString("N"),
                mgid = MGid,
                Index = Index,
                DieselEngineModel = Var.SysConfig.LastModel,
                DieselEngineNo = Common.mTestViewModel?.ModelNo ?? "",
                RecordName = "手动记录",
                TestName = "出厂试验",
                RecordDataTime = now,
                DataTime = now.ToString("yyyy-MM-dd"),
                Time = now.ToString("HH:mm:ss"),

                // ══════════════════ 出厂表 列1-10：基本参数 ══════════════════
                TestHour = now.Hour,
                TestMinute = now.Minute,
                NominalRPM = ParseDoubleOrZero(saveInfo, "NominalRPM"),
                RPM = MiddleData.instnce.EngineSpeed,
                NominalPower = ParseDoubleOrZero(saveInfo, "NominalPower"),
                Power = MiddleData.instnce.EnginePower,
                ECOQuantity = SafeGet(() => Equip.ET4500.Instance.fuelConsumption), // 串口油耗仪（ET4500）
                ECORate = SafeGet(() => CalcFuelRate()),

                // ══════════════════ 出厂表 列11-18：油压/水压 ══════════════════
                PFuelInlet = GetValuePreferTRDP("燃油精滤器后油压", () => Common.AI2Grp["P38燃油供油压力"]),
                LPressureOut = GetValuePreferTRDP("中冷水泵出口压力", () => Common.AI2Grp["P3中冷水泵进口压力"]),
                HPressureOut = GetValuePreferTRDP("高温水泵出口压力", () => Common.AI2Grp["P1高温水出机压力"]),
                EOPressure1 = GetValuePreferTRDP("机油泵出口油压", () => Common.AI2Grp["P20机油泵出口压力"]),
                POilInlet = GetValuePreferTRDP("主油道进口油压", () => Common.AI2Grp["主油道进口油压"]),
                EOPressure2 = GetValuePreferTRDP("主油道末端油压", () => Common.AI2Grp["主油道末端油压"]),
                PTurboOilFront = GetValuePreferTRDP("前增压器进口油压", () => Common.AI2Grp["前增压器进油压"]),
                PTurboOilAfter = GetValuePreferTRDP("后增压器进口油压", () => Common.AI2Grp["后增压器进油压"]),

                // ══════════════════ 出厂表 列19-26：油温/水温 + 增压器转速 ══════════════════
                HeatExchangerTempIn = GetValuePreferTRDP("主油道进口油温", () => Common.AI2Grp["机油热交换器进口油温"]),
                HeatExchangerTempOut = GetValuePreferTRDP("机油泵出口油温", () => Common.AI2Grp["机油热交换器出口油温"]),
                HWaterTempIn = SafeGet(() => Common.waterGrp.NewDataValue["T2高温水进机温度"]),
                HWaterTempOut = GetValuePreferTRDP("高温水出水温度", () => Common.waterGrp.NewDataValue["T1高温水出机温度"]),
                LWaterTempIn = GetValuePreferTRDP("中冷水进水温度", () => Common.waterGrp.NewDataValue["T3中冷水进机温度"]),
                LWaterTempOut = GetValuePreferTRDP("中冷水出水温度", () => Common.waterGrp.NewDataValue["T5中冷水出机温度"]),
                FrontTurbochargerRPM = GetValuePreferTRDP("前增压器转速", () => 0),
                AfterTurbochargerRPM = GetValuePreferTRDP("后增压器转速", () => 0),

                // ══════════════════ 出厂表 列27-37：增压器子表（压力） ══════════════════
                PCompressorFront = SafeGet(() => Common.AI2Grp["前增压器进气真空度"]),
                PCompressorAfter = SafeGet(() => Common.AI2Grp["后增压器进气真空度"]), 
                PTurboOutPressureFront = SafeGet(() => Common.AI2Grp["前增压器排气背压"]),
                PTurboOutPressureAfter = SafeGet(() => Common.AI2Grp["后增压器排气背压"]),
                PCrankcase = SafeGet(() => Common.AI2Grp["曲轴箱压力"]),
                PInterCoolerFrontFront = SafeGet(() => Common.AI2Grp["前中冷前空气压力"]),
                PInterCoolerFrontAfter = SafeGet(() => Common.AI2Grp["前中冷后空气压力"]),
                PInterCoolerAfterFront = SafeGet(() => Common.AI2Grp["后中冷前空气压力"]),
                PInterCoolerAfterAfter = GetValuePreferTRDP("后中冷后空气压力", () => Common.AI2Grp["后中冷后空气压力"]),
                FrontTurbochargerPressureIn2 = SafeGet(() => Common.AI2Grp["前涡轮进口废气压力"]),
                AfterTurbochargerPressureIn2 = SafeGet(() => Common.AI2Grp["后涡轮进口废气压力"]),

                // ══════════════════ 出厂表 列38-41：增压器子表（温度） ══════════════════
                TInterCoolerFrontFront = GetValuePreferTRDP("前压气机出口空气温度", () => Common.AI2Grp["前中冷前空气温度"]),
                TInterCoolerFrontAfter = GetValuePreferTRDP("后压气机出口空气温度", () => Common.AI2Grp["前中冷后空气温度"]),
                TInterCoolerAfterFront = SafeGet(() => Common.AI2Grp["后中冷前空气温度"]),
                TInterCoolerAfterAfter = GetValuePreferTRDP("后中冷器后空气温度", () => Common.AI2Grp["后中冷后空气温度"]),

                // ══════════════════ 出厂表 列59-62：涡轮前/后废气温度 ══════════════════
                FrontTurbochargerTempIn = GetValuePreferTRDP("A涡前排气温度", () => Common.AI2Grp["前涡轮进口废气温度"]),
                AfterTurbochargerTempIn = GetValuePreferTRDP("B涡前排气温度", () => Common.AI2Grp["后涡轮进口废气温度"]),
                FrontTurbochargerTempOut = SafeGet(() => Common.AI2Grp["前涡轮出口废气温度"]),
                AfterTurbochargerTempOut = SafeGet(() => Common.AI2Grp["后涡轮出口废气温度"]),

                // ══════════════════ 出厂表 各缸排气温度，已确认无需改动 ══════════════════
                EGTempA1 = GetValuePreferTRDP("A1缸排气温度", () => Common.AI2Grp["A1缸排气温度"]),
                EGTempA2 = GetValuePreferTRDP("A2缸排气温度", () => Common.AI2Grp["A2缸排气温度"]),
                EGTempA3 = GetValuePreferTRDP("A3缸排气温度", () => Common.AI2Grp["A3缸排气温度"]),
                EGTempA4 = GetValuePreferTRDP("A4缸排气温度", () => Common.AI2Grp["A4缸排气温度"]),
                EGTempA5 = GetValuePreferTRDP("A5缸排气温度", () => Common.AI2Grp["A5缸排气温度"]),
                EGTempA6 = GetValuePreferTRDP("A6缸排气温度", () => Common.AI2Grp["A6缸排气温度"]),
                EGTempB1 = GetValuePreferTRDP("B1缸排气温度", () => Common.AI2Grp["B1缸排气温度"]),
                EGTempB2 = GetValuePreferTRDP("B2缸排气温度", () => Common.AI2Grp["B2缸排气温度"]),
                EGTempB3 = GetValuePreferTRDP("B3缸排气温度", () => Common.AI2Grp["B3缸排气温度"]),
                EGTempB4 = GetValuePreferTRDP("B4缸排气温度", () => Common.AI2Grp["B4缸排气温度"]),
                EGTempB5 = GetValuePreferTRDP("B5缸排气温度", () => Common.AI2Grp["B5缸排气温度"]),
                EGTempB6 = GetValuePreferTRDP("B6缸排气温度", () => Common.AI2Grp["B6缸排气温度"]),

                // 各缸爆发压力：人工打印纸质表后手动填写，软件不采集、TRDP也没有，字段保留不赋值（默认0，导出留空）
            };

            return r;
        }

        /// <summary>
        /// 燃油消耗率 g/kWh。优先用进/回油流量传感器差值(质量流量法)；
        /// 传感器差值为0(未接入/无读数)时，退回油耗仪(ET4500)读数。
        /// </summary>
        private static double CalcFuelRate()
        {
            double power = MiddleData.instnce.EnginePower;
            if (power == 0) return 0;

            // 方式一：传感器差值法（进油流量 - 回油流量）
            double massFlow = SafeGet(() => Common.AIgrp["燃油进油流量测量-L30"] - Common.AIgrp["燃油回油流量测量-L31"]);
            if (massFlow != 0)
                return Math.Round(massFlow * 1000 / power, 1);

            // 方式二：传感器没值，退回油耗仪
            return Math.Round(SafeGet(() => Equip.ET4500.Instance.fuelConsumption) * 1000 / power, 1);
        }

        private static double SafeGet(Func<double> func)
        {
            try { return func(); }
            catch { return 0; }
        }

        /// <summary>
        /// 从 saveInfo 里取一个数值型字段，取不到或解析失败返回0
        /// </summary>
        /// <param name="saveInfo"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static double ParseDoubleOrZero(Dictionary<string, string> saveInfo, string key)
        {
            if (saveInfo != null && saveInfo.TryGetValue(key, out var s) && double.TryParse(s, out var v))
                return v;
            return 0;
        }

        // 数据库操作
        public int Save(ManualRecordPara record)
        {
            try
            {
                return DB.mysql.Insert<ManualRecordPara>(record).ExecuteAffrows();
            }
            catch (Exception ex)
            {
                throw new Exception("试验记录保存失败：" + ex.Message);
            }
        }

        public int SaveMain(ManualRecordMain main)
        {
            try
            {
                return DB.mysql.Insert<ManualRecordMain>(main).ExecuteAffrows();
            }
            catch (Exception ex)
            {
                throw new Exception("主表保存失败：" + ex.Message);
            }
        }

        /// <summary>按编号/型号查询所有批次（主表），供报表界面下拉选择</summary>
        public List<ManualRecordMain> GetAllMain(string model, string engineNo)
        {
            var q = DB.mysql.Select<ManualRecordMain>()
                .WhereIf(!string.IsNullOrEmpty(model), d => d.DieselEngineModel == model)
                .WhereIf(!string.IsNullOrEmpty(engineNo), d => d.DieselEngineNo == engineNo)
                .OrderByDescending(d => d.BeginTime);
            return q.ToList();
        }

        /// <summary>按主表 mgid 查询明细记录（正序）</summary>
        public List<ManualRecordPara> GetByMgid(string mgid)
        {
            return DB.mysql.Select<ManualRecordPara>()
                .Where(d => d.mgid == mgid)
                .OrderBy(d => d.Index)
                .ToList();
        }

        public List<ManualRecordPara> GetAllRecordByMGid(string mgid)
        {
            if (string.IsNullOrEmpty(mgid)) return new List<ManualRecordPara>();
            return DB.mysql.Select<ManualRecordPara>()
                .Where(d => d.mgid == mgid)
                .OrderBy(d => d.Index)
                .ToList();
        }



        /// <summary>
        /// 手动出厂记录的统一列定义。
        /// 供 ucAutoHMI（实时记录表格）与 ucAutoRecord（报表查询表格）共用，
        /// 避免两处各维护一份、字段增删时漏改。
        /// Visible 语义：仅供"实时记录表格"参考是否默认隐藏；
        /// 报表查询表格一律全部显示（含人工事后手填列），不读取该标志。
        /// </summary>
        public static readonly List<ManualColumnDefinition> ManualColumns = new List<ManualColumnDefinition>
        {
            new ManualColumnDefinition("Index",               "序号"),
            new ManualColumnDefinition("RecordDataTime",      "采集时间", visible: false), // 实时表格不需要，报表查询表格会显示
            new ManualColumnDefinition("TestHour",            "时"),
            new ManualColumnDefinition("TestMinute",          "分"),
            new ManualColumnDefinition("NominalRPM",          "名义转速 rpm"),
            new ManualColumnDefinition("RPM",                 "实测转速 rpm"),
            new ManualColumnDefinition("NominalPower",        "名义功率 kW"),
            new ManualColumnDefinition("Power",               "实测功率 kW"),
            new ManualColumnDefinition("ECOQuantity",         "测油耗量 g"),
            new ManualColumnDefinition("ECORate",             "燃油消耗率 g/kWh"),
            new ManualColumnDefinition("PFuelInlet",          "燃油进口压力 kPa"),
            new ManualColumnDefinition("LPressureOut",        "中冷泵出口压力 kPa"),
            new ManualColumnDefinition("HPressureOut",        "高温泵出口压力 kPa"),
            new ManualColumnDefinition("EOPressure1",         "机油泵出口压力 kPa"),
            new ManualColumnDefinition("POilInlet",           "机油进口压力 kPa"),
            new ManualColumnDefinition("EOPressure2",         "机油总管末端压力 kPa"),
            new ManualColumnDefinition("PTurboOilFront",      "前增压器油压 kPa"),
            new ManualColumnDefinition("PTurboOilAfter",      "后增压器油压 kPa"),
            new ManualColumnDefinition("HeatExchangerTempIn", "机油进口温度 ℃"),
            new ManualColumnDefinition("HeatExchangerTempOut","机油出口温度 ℃"),
            new ManualColumnDefinition("HWaterTempIn",        "高温水进口温度 ℃"),
            new ManualColumnDefinition("HWaterTempOut",       "高温水出口温度 ℃"),
            new ManualColumnDefinition("LWaterTempIn",        "中冷水进口温度 ℃"),
            new ManualColumnDefinition("LWaterTempOut",       "中冷水出口温度 ℃"),
            new ManualColumnDefinition("FrontTurbochargerRPM","前增压器转速 rpm"),
            new ManualColumnDefinition("AfterTurbochargerRPM","后增压器转速 rpm"),

            new ManualColumnDefinition("PCompressorFront",       "压气机前压力(前) Pa"),
            new ManualColumnDefinition("PCompressorAfter",       "压气机前压力(后) Pa"),
            new ManualColumnDefinition("PTurboOutPressureFront", "涡轮后压力(前) Pa"),
            new ManualColumnDefinition("PTurboOutPressureAfter", "涡轮后压力(后) Pa"),
            new ManualColumnDefinition("PCrankcase",             "曲轴箱压力 ×10Pa"),
            new ManualColumnDefinition("PInterCoolerFrontFront", "中冷器前压力(前) ×100Pa"),
            new ManualColumnDefinition("PInterCoolerFrontAfter", "中冷器前压力(后) ×100Pa"),
            new ManualColumnDefinition("PInterCoolerAfterFront", "中冷器后压力(前) ×100Pa"),
            new ManualColumnDefinition("PInterCoolerAfterAfter", "中冷器后压力(后) ×100Pa"),
            new ManualColumnDefinition("FrontTurbochargerPressureIn2", "涡轮前压力(前) Pa"),
            new ManualColumnDefinition("AfterTurbochargerPressureIn2", "涡轮前压力(后) Pa"),
            new ManualColumnDefinition("TInterCoolerFrontFront", "中冷器前温度(前) ℃"),
            new ManualColumnDefinition("TInterCoolerFrontAfter", "中冷器前温度(后) ℃"),
            new ManualColumnDefinition("TInterCoolerAfterFront", "中冷器后温度(前) ℃"),
            new ManualColumnDefinition("TInterCoolerAfterAfter", "中冷器后温度(后) ℃"),

            new ManualColumnDefinition("EGTempA1", "A1缸排温 ℃"),
            new ManualColumnDefinition("EGTempA2", "A2缸排温 ℃"),
            new ManualColumnDefinition("EGTempA3", "A3缸排温 ℃"),
            new ManualColumnDefinition("EGTempA4", "A4缸排温 ℃"),
            new ManualColumnDefinition("EGTempA5", "A5缸排温 ℃"),
            new ManualColumnDefinition("EGTempA6", "A6缸排温 ℃"),
            new ManualColumnDefinition("EGTempB1", "B1缸排温 ℃"),
            new ManualColumnDefinition("EGTempB2", "B2缸排温 ℃"),
            new ManualColumnDefinition("EGTempB3", "B3缸排温 ℃"),
            new ManualColumnDefinition("EGTempB4", "B4缸排温 ℃"),
            new ManualColumnDefinition("EGTempB5", "B5缸排温 ℃"),
            new ManualColumnDefinition("EGTempB6", "B6缸排温 ℃"),

            new ManualColumnDefinition("FrontTurbochargerTempIn",  "涡轮前温度(前) ℃"),
            new ManualColumnDefinition("AfterTurbochargerTempIn",  "涡轮前温度(后) ℃"),
            new ManualColumnDefinition("FrontTurbochargerTempOut", "涡轮后温度(前) ℃"),
            new ManualColumnDefinition("AfterTurbochargerTempOut", "涡轮后温度(后) ℃"),
        
            // 各缸爆发压力：软件不采集，人工打印后手填；实时表格默认隐藏，报表查询表格始终显示
            new ManualColumnDefinition("BurstPA1", "A1爆发压力", visible: false),
            new ManualColumnDefinition("BurstPA2", "A2爆发压力", visible: false),
            new ManualColumnDefinition("BurstPA3", "A3爆发压力", visible: false),
            new ManualColumnDefinition("BurstPA4", "A4爆发压力", visible: false),
            new ManualColumnDefinition("BurstPA5", "A5爆发压力", visible: false),
            new ManualColumnDefinition("BurstPA6", "A6爆发压力", visible: false),
            new ManualColumnDefinition("BurstPB1", "B1爆发压力", visible: false),
            new ManualColumnDefinition("BurstPB2", "B2爆发压力", visible: false),
            new ManualColumnDefinition("BurstPB3", "B3爆发压力", visible: false),
            new ManualColumnDefinition("BurstPB4", "B4爆发压力", visible: false),
            new ManualColumnDefinition("BurstPB5", "B5爆发压力", visible: false),
            new ManualColumnDefinition("BurstPB6", "B6爆发压力", visible: false),

            new ManualColumnDefinition("Remark", "备注", visible: false),
        };
    }

    public class ManualColumnDefinition
    {
        public string PropertyName { get; }
        public string DisplayName { get; }
        public bool Visible { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
        public ManualColumnDefinition(string propertyName, string displayName, bool visible = true)
        {
            PropertyName = propertyName;
            DisplayName = displayName;
            Visible = visible;
        }
    }
}
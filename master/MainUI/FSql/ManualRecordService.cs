using MainUI.Global;
using MainUI.FSql.Model;
using System;
using System.Collections.Generic;

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
                record = CollectSnapshot();

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

        // 传感器快照采集
        private ManualRecordPara CollectSnapshot()
        {
            var now = DateTime.Now;

            var r = new ManualRecordPara
            {
                // ── 基类（IRecordData）通用字段 ─────────────────────────
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

                // ── 子类新增字段 ─────────────────────────────────────────
                TestHour = now.Hour,
                TestMinute = now.Minute,

                NominalRPM = MiddleData.instnce.SelectModelConfig?.RatedSpeed ?? 0,
                NominalPower = MiddleData.instnce.SelectModelConfig?.RatedPower ?? 0,

                PFuelInlet = SafeGet(() => Common.AI2Grp["燃油精滤器后油压"]),
                POilInlet = SafeGet(() => Common.AI2Grp["主油道进口油压"]),
                PTurboOilFront = SafeGet(() => Common.AI2Grp["前增压器进油压"]),
                PTurboOilAfter = SafeGet(() => Common.AI2Grp["后增压器进油压"]),

                PCompressorFront = SafeGet(() => Common.AI2Grp["前压气机前空气压力"]),
                PCompressorAfter = SafeGet(() => Common.AI2Grp["后压气机前空气压力"]),
                PTurboOutPressureFront = SafeGet(() => Common.AI2Grp["前涡轮后废气压力"]),
                PTurboOutPressureAfter = SafeGet(() => Common.AI2Grp["后涡轮后废气压力"]),
                PCrankcase = SafeGet(() => Common.AI2Grp["曲轴箱压力"]),
                PInterCoolerFrontFront = SafeGet(() => Common.AI2Grp["前中冷前空气压力"]),
                PInterCoolerFrontAfter = SafeGet(() => Common.AI2Grp["后中冷前空气压力"]),
                PInterCoolerAfterFront = SafeGet(() => Common.AI2Grp["前中冷后空气压力"]),
                PInterCoolerAfterAfter = SafeGet(() => Common.AI2Grp["后中冷后空气压力"]),
                TInterCoolerFrontFront = SafeGet(() => Common.AI2Grp["前中冷前空气温度"]),
                TInterCoolerFrontAfter = SafeGet(() => Common.AI2Grp["后中冷前空气温度"]),
                TInterCoolerAfterFront = SafeGet(() => Common.AI2Grp["前中冷后空气温度"]),
                TInterCoolerAfterAfter = SafeGet(() => Common.AI2Grp["后中冷后空气温度"]),

                // 各缸爆发压力：人工打印纸质表后手动填写，软件不采集。
                // 字段保留（预留），此处不赋值（保持默认0，导出时该列留空）。

                // 基类已有字段，直接赋值
                RPM = MiddleData.instnce.EngineSpeed,
                Power = MiddleData.instnce.EnginePower,

                ECOQuantity = SafeGet(() => Equip.ET4500.Instance.fuelConsumption),
                //TODO:油耗具体如何计算 待优化
                //ECORate = SafeGet(() => MiddleData.instnce.FuelConsumptionRate),

                LPressureOut = SafeGet(() => Common.AI2Grp["中冷水泵出口压力"]),
                HPressureOut = SafeGet(() => Common.AI2Grp["高温水泵出口压力"]),
                EOPressure1 = SafeGet(() => Common.AI2Grp["机油泵出口油压"]),
                EOPressure2 = SafeGet(() => Common.AI2Grp["主油道末端油压"]),

                HeatExchangerTempIn = SafeGet(() => Common.AI2Grp["机油热交换器进口油温"]),
                HeatExchangerTempOut = SafeGet(() => Common.AI2Grp["机油热交换器出口油温"]),
                HWaterTempIn = SafeGet(() => Common.waterGrp.NewDataValue["高温水进机温度"]),
                HWaterTempOut = SafeGet(() => Common.waterGrp.NewDataValue["高温水出机温度"]),
                LWaterTempIn = SafeGet(() => Common.waterGrp.NewDataValue["中冷水进机温度"]),
                LWaterTempOut = SafeGet(() => Common.waterGrp.NewDataValue["中冷水出机温度"]),

                FrontTurbochargerRPM = SafeGet(() => (double)Var.TRDP.GetDicValue("前增压器转速")),
                AfterTurbochargerRPM = SafeGet(() => (double)Var.TRDP.GetDicValue("后增压器转速")),

                EGTempA1 = SafeGet(() => Common.AI2Grp["A1缸排气温度"]),
                EGTempA2 = SafeGet(() => Common.AI2Grp["A2缸排气温度"]),
                EGTempA3 = SafeGet(() => Common.AI2Grp["A3缸排气温度"]),
                EGTempA4 = SafeGet(() => Common.AI2Grp["A4缸排气温度"]),
                EGTempA5 = SafeGet(() => Common.AI2Grp["A5缸排气温度"]),
                EGTempA6 = SafeGet(() => Common.AI2Grp["A6缸排气温度"]),
                EGTempB1 = SafeGet(() => Common.AI2Grp["B1缸排气温度"]),
                EGTempB2 = SafeGet(() => Common.AI2Grp["B2缸排气温度"]),
                EGTempB3 = SafeGet(() => Common.AI2Grp["B3缸排气温度"]),
                EGTempB4 = SafeGet(() => Common.AI2Grp["B4缸排气温度"]),
                EGTempB5 = SafeGet(() => Common.AI2Grp["B5缸排气温度"]),
                EGTempB6 = SafeGet(() => Common.AI2Grp["B6缸排气温度"]),

                FrontTurbochargerTempIn = SafeGet(() => Common.AI2Grp["前涡轮进口废气温度"]),
                AfterTurbochargerTempIn = SafeGet(() => Common.AI2Grp["后涡轮进口废气温度"]),
                FrontTurbochargerTempOut = SafeGet(() => Common.AI2Grp["前涡轮出口废气温度"]),
                AfterTurbochargerTempOut = SafeGet(() => Common.AI2Grp["后涡轮出口废气温度"]),

                FrontTurbochargerPressureIn2 = SafeGet(() => Common.AI2Grp["前涡轮进口废气压力"]),
                AfterTurbochargerPressureIn2 = SafeGet(() => Common.AI2Grp["后涡轮进口废气压力"]),
            };

            return r;
        }

        private static double SafeGet(Func<double> func)
        {
            try { return func(); }
            catch { return 0; }
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
    }
}
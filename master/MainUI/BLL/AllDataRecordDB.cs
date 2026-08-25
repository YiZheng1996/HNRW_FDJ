using FreeSql;
using MainUI.FSql;
using MainUI.FSql.Model;
using System;
using System.Collections.Generic;

namespace MainUI.BLL
{
    /// <summary>
    /// 数据分析页数据库访问
    /// </summary>
    public class AllDataRecordDB
    {
        #region 总数据记录查询（TestParaALL + TestParaALLMain）

        /// <summary>
        /// 统计总数据记录总数，用于分页计算总页数
        /// </summary>
        /// <param name="beginTime">查询开始时间</param>
        /// <param name="endTime">查询结束时间</param>
        /// <param name="dieselEngineModel">柴油机型号，为空时不筛选</param>
        /// <param name="dieselEngineNo">发动机编号，为空时不筛选</param>
        public long SelectDataCount(DateTime beginTime, DateTime endTime, string dieselEngineModel, string dieselEngineNo)
        {
            return BuildSelectQuery(beginTime, endTime, dieselEngineModel, dieselEngineNo)
                .Count();
        }

        /// <summary>
        /// 按页查询总数据记录，在数据库层完成 Skip/Take，避免一次性加载全部记录
        /// </summary>
        /// <param name="beginTime">查询开始时间</param>
        /// <param name="endTime">查询结束时间</param>
        /// <param name="dieselEngineModel">柴油机型号，为空时不筛选</param>
        /// <param name="dieselEngineNo">发动机编号，为空时不筛选</param>
        /// <param name="pageIndex">页码，从 1 开始</param>
        /// <param name="pageSize">每页条数</param>
        public List<TestParaAllData> SelectDataPage(
            DateTime beginTime,
            DateTime endTime,
            string dieselEngineModel,
            string dieselEngineNo,
            int pageIndex,
            int pageSize)
        {
            var paging = NormalizePaging(pageIndex, pageSize);

            // FreeSql 的 ToList 需要 Expression 投影，不能传方法组，必须用内联 lambda
            return BuildSelectQuery(beginTime, endTime, dieselEngineModel, dieselEngineNo)
                .OrderByDescending((d, m) => d.RecordDataTime)
                .Skip(paging.skip)
                .Take(paging.pageSize)
                .ToList((d, m) => new TestParaAllData
                {
                    gid = d.gid,
                    DieselEngineModel = m.DieselEngineModel,
                    DieselEngineNo = m.DieselEngineNo,
                    TestName = string.IsNullOrEmpty(d.TestName) ? m.TestName : d.TestName,
                    UserName = m.UserName,
                    BeginTime = m.BeginTime,
                    EndTime = m.EndTime,
                    Index = d.Index,
                    RecordName = d.RecordName,
                    TestStage = d.TestStage,
                    TestCycle = d.TestCycle,
                    TestStep = d.TestStep,
                    DataTime = d.DataTime,
                    Time = d.Time,
                    HourNum = d.HourNum,
                    RecordDataTime = d.RecordDataTime,
                    MonitorData = d.MonitorData
                });
        }

        /// <summary>
        /// 查询全部总数据记录，仅用于“导出全部”场景
        /// </summary>
        /// <param name="beginTime">查询开始时间</param>
        /// <param name="endTime">查询结束时间</param>
        /// <param name="dieselEngineModel">柴油机型号，为空时不筛选</param>
        /// <param name="dieselEngineNo">发动机编号，为空时不筛选</param>
        public List<TestParaAllData> selectData(
            DateTime beginTime,
            DateTime endTime,
            string dieselEngineModel,
            string dieselEngineNo)
        {
            // FreeSql 的 ToList 需要 Expression 投影，不能传方法组，必须用内联 lambda
            return BuildSelectQuery(beginTime, endTime, dieselEngineModel, dieselEngineNo)
                .OrderByDescending((d, m) => d.RecordDataTime)
                .ToList((d, m) => new TestParaAllData
                {
                    gid = d.gid,
                    DieselEngineModel = m.DieselEngineModel,
                    DieselEngineNo = m.DieselEngineNo,
                    TestName = string.IsNullOrEmpty(d.TestName) ? m.TestName : d.TestName,
                    UserName = m.UserName,
                    BeginTime = m.BeginTime,
                    EndTime = m.EndTime,
                    Index = d.Index,
                    RecordName = d.RecordName,
                    TestStage = d.TestStage,
                    TestCycle = d.TestCycle,
                    TestStep = d.TestStep,
                    DataTime = d.DataTime,
                    Time = d.Time,
                    HourNum = d.HourNum,
                    RecordDataTime = d.RecordDataTime,
                    MonitorData = d.MonitorData
                });
        }

        #endregion

        #region 启动柜数据查询（StartupTestPara）

        /// <summary>
        /// 统计启动柜数据总数，用于分页计算总页数
        /// </summary>
        /// <param name="beginTime">查询开始时间</param>
        /// <param name="endTime">查询结束时间</param>
        /// <param name="dieselEngineNo">发动机编号，为空时不筛选</param>
        public long SelectStartupDataCount(DateTime beginTime, DateTime endTime, string dieselEngineNo)
        {
            return BuildStartupSelectQuery(beginTime, endTime, dieselEngineNo)
                .Count();
        }

        /// <summary>
        /// 按页查询启动柜数据，在数据库层完成 Skip/Take
        /// </summary>
        /// <param name="beginTime">查询开始时间</param>
        /// <param name="endTime">查询结束时间</param>
        /// <param name="dieselEngineNo">发动机编号，为空时不筛选</param>
        /// <param name="pageIndex">页码，从 1 开始</param>
        /// <param name="pageSize">每页条数</param>
        public List<StartupTestPara> SelectStartupDataPage(
            DateTime beginTime,
            DateTime endTime,
            string dieselEngineNo,
            int pageIndex,
            int pageSize)
        {
            var paging = NormalizePaging(pageIndex, pageSize);

            return BuildStartupSelectQuery(beginTime, endTime, dieselEngineNo)
                .OrderByDescending(d => d.RecordDataTime)
                .Skip(paging.skip)
                .Take(paging.pageSize)
                .ToList();
        }

        /// <summary>
        /// 查询全部启动柜数据，仅用于“导出全部”场景
        /// </summary>
        /// <param name="beginTime">查询开始时间</param>
        /// <param name="endTime">查询结束时间</param>
        /// <param name="dieselEngineNo">发动机编号，为空时不筛选</param>
        public List<StartupTestPara> selectStartupData(DateTime beginTime, DateTime endTime, string dieselEngineNo)
        {
            return BuildStartupSelectQuery(beginTime, endTime, dieselEngineNo)
                .OrderByDescending(d => d.RecordDataTime)
                .ToList();
        }

        #endregion

        #region 公用方法

        /// <summary>
        /// 构建总数据查询条件，供统计、分页、全量导出复用
        /// </summary>
        private ISelect<TestParaALL, TestParaALLMain> BuildSelectQuery(
            DateTime beginTime,
            DateTime endTime,
            string dieselEngineModel,
            string dieselEngineNo)
        {
            return DB.mysql
                .Select<TestParaALL, TestParaALLMain>()
                .LeftJoin((d, m) => d.mgid == m.gid)
                .Where((d, m) => beginTime <= d.RecordDataTime && d.RecordDataTime <= endTime)
                .WhereIf(!string.IsNullOrEmpty(dieselEngineModel), (d, m) => m.DieselEngineModel == dieselEngineModel)
                .WhereIf(!string.IsNullOrEmpty(dieselEngineNo), (d, m) => m.DieselEngineNo == dieselEngineNo)
                .CommandTimeout(120);
        }

        /// <summary>
        /// 构建启动柜数据查询条件，供统计、分页、全量导出复用
        /// </summary>
        private ISelect<StartupTestPara> BuildStartupSelectQuery(
            DateTime beginTime,
            DateTime endTime,
            string dieselEngineNo)
        {
            return DB.mysql
                .Select<StartupTestPara>()
                .Where(d => beginTime <= d.RecordDataTime && d.RecordDataTime <= endTime)
                .WhereIf(!string.IsNullOrEmpty(dieselEngineNo), d => d.DieselEngineNo == dieselEngineNo)
                .CommandTimeout(120);
        }

        /// <summary>
        /// 规范化分页参数，确保页码和每页条数合法
        /// </summary>
        /// <param name="pageIndex">页码，从 1 开始</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>规范化后的页码、每页条数及 Skip 偏移量</returns>
        private static (int pageIndex, int pageSize, int skip) NormalizePaging(int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 1;
            }

            return (pageIndex, pageSize, (pageIndex - 1) * pageSize);
        }

        #endregion
    }
}

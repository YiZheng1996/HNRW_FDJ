using MainUI.FSql;
using MainUI.FSql.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MainUI.BLL
{
    /// <summary>
    /// pym
    /// </summary>
    public class AllDataRecordDB
    {

        public List<TestParaAllData> selectData(DateTime beginTime, DateTime endTime, string DieselEngineModel, string DieselEngineNo)
        {
            List<TestParaAllData> AllDataList =
                DB.mysql
                .Select<TestParaALL, TestParaALLMain>()
                .LeftJoin((d, m) => d.mgid == m.gid)
                .Where((d, m) => beginTime <= d.RecordDataTime && d.RecordDataTime <= endTime)
                .WhereIf(!string.IsNullOrEmpty(DieselEngineModel), (d, m) => m.DieselEngineModel == DieselEngineModel)
                .WhereIf(!string.IsNullOrEmpty(DieselEngineNo), (d, m) => m.DieselEngineNo == DieselEngineNo)
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
            return AllDataList;
        }


        public List<Dictionary<string, object>> jsonToDictionary(List<TestParaAllData> _allData)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (var item in _allData)
            {
                string json = item.MonitorData;
                // 1. 反序列化为字典
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    DateFormatString = "yyyy-MM-dd HH:mm:ss.fff"
                };
                var raw = JsonConvert.DeserializeObject<Dictionary<string, object>>(json, settings);
                list.Add(raw);
            }
            return list;
        }

    }
}

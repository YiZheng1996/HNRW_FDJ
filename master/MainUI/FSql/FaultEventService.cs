using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql
{
    /// <summary>
    /// 故障类的BLL 增删改查
    /// </summary>
    public class FaultEventService
    {

        /// <summary>
        /// 插入单条故障记录
        /// </summary>
        /// <param name="faultEventRecord">要插入的故障记录</param>
        /// <returns>插入故障记录的id</returns>
        public int AddFaultEventRecord(FaultEvent faultEventRecord)
        {
            try
            {
                var result = DB.mysql.Insert<FaultEvent>(faultEventRecord).ExecuteAffrows();
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 通过记录id删除该故障记录
        /// </summary>
        /// <param name="faultEventRecordId">故障记录id号</param>
        /// <returns>删除的记录条数</returns>
        public int DeleteFaultEventRecordById(string faultEventRecordId)
        {
            int result = 0;
            try
            {
                result = DB.mysql.Delete<FaultEvent>().Where(d => d.Id == faultEventRecordId).ExecuteAffrows();
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception("删除故障记录失败！原因：" + ex.Message);
            }
        }

        /// <summary>
        /// 故障恢复时通过id，修改该记录的恢复时间和状态
        /// </summary>
        /// <param name="faultEventRecordId">故障记录Id</param>
        /// <param name="occurTime">恢复时间</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public int UpdateFaultEventRecordById(string faultEventRecordId, DateTime resetTime, int status)
        {
            int result = 0;
            try
            {
                result = DB.mysql.Select<FaultEvent>().Where(d => d.Id == faultEventRecordId)
                                 .ToUpdate()
                                 .Set(d => d.ResetTime, resetTime)
                                 .Set(d => d.Status, status)
                                 .ExecuteAffrows();
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception("更新故障记录失败！原因：" + ex.Message);
            }
        }

        /// <summary>
        /// 查询所有故障记录
        /// </summary>
        /// <param name="CreatedAt">记录创建时间</param>
        /// <param name="UpdatedAt">记录最后更新时间</param>
        /// <returns>测试参数列表</returns>
        public List<FaultEvent> GetAllFaultEventRecord(DateTime CreatedAt, DateTime UpdatedAt)
        {
            var list = DB.mysql.Select<FaultEvent>()
                .Where(d => d.OccurTime >= CreatedAt && d.OccurTime <= UpdatedAt)
                .OrderByDescending(d => d.OccurTime)
                .ToList();
            return list;
        }

        /// <summary>
        /// 根据条件查询故障记录
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="faultType">故障类型（null或"全部"表示不筛选）</param>
        /// <param name="description">故障描述（null或空表示不筛选）</param>
        /// <returns>DataTable</returns>
        public DataTable GetFaultEventRecordByCondition(DateTime startTime, DateTime endTime, string faultType, string description)
        {
            try
            {
                var query = DB.mysql.Select<FaultEvent>()
                    .Where(d => d.OccurTime >= startTime && d.OccurTime <= endTime);

                // 根据故障类型筛选
                if (!string.IsNullOrEmpty(faultType) && faultType != "全部")
                {
                    // 将中文类型转换为对应的数字类型
                    int typeValue = ConvertFaultTypeToInt(faultType);
                    query = query.Where(d => d.FaultType == typeValue);
                }

                // 根据故障描述模糊查询
                if (!string.IsNullOrEmpty(description))
                {
                    query = query.Where(d => d.Description.Contains(description));
                }

                // 按创建时间倒序排列
                var list = query.OrderByDescending(d => d.OccurTime).ToDataTable("*");
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception($"查询故障记录失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 将故障类型中文转换为对应的整数值
        /// </summary>
        private int ConvertFaultTypeToInt(string faultType)
        {
            switch (faultType)
            {
                case "通讯": return 0;
                case "OPC检测": return 1;
                case "逻辑判断": return 2;
                case "发动机控制器": return 3;
                default: return 0;
            }
        }

        /// <summary>
        /// 查询所有试验记录到DataTable
        /// </summary>
        /// <param name="CreatedAt">记录创建时间</param>
        /// <param name="UpdatedAt">记录最后更新时间</param>
        /// <returns>DataTable</returns>
        public DataTable GetAllFaultEventRecordToTable(DateTime CreatedAt, DateTime UpdatedAt)
        {
            var list = DB.mysql.Select<FaultEvent>()
                .Where(d => d.OccurTime >= CreatedAt && d.OccurTime <= UpdatedAt)
                .OrderByDescending(d => d.OccurTime)
                .ToDataTable("*");
            return list;
        }

        /// <summary>
        /// 通过记录id查询故障记录并返回
        /// </summary>
        /// <param name="FaultEventRecordId">故障记录id</param>
        /// <returns>DataTable</returns>
        public DataTable GetFaultEventRecordByID(string FaultEventRecordId)
        {
            var list = DB.mysql.Select<FaultEvent>()
                .Where(d => d.Id == FaultEventRecordId)
                .ToDataTable("*");
            return list;
        }

        /// <summary>
        /// 通过故障代码查询故障记录并返回
        /// </summary>
        /// <param name="FaultCode">故障代码</param>
        /// <returns>DataTable</returns>
        public List<FaultEvent> GetFaultEventRecordByFaultCode(string FaultCode)
        {
            var list = DB.mysql.Select<FaultEvent>()
                .Where(d => d.FaultCode == FaultCode)
                .Where(d => d.Status == 0)
                .ToList();
            return list;
        }

        /// <summary>
        /// 通过故障状态查询记录并返回
        /// </summary>
        /// <param name="Status">故障状态</param>
        /// <returns>DataTable</returns>
        public DataTable GetFaultEventRecordByStatus(int Status)
        {
            var list = DB.mysql.Select<FaultEvent>()
                .Where(d => d.Status == Status)
                .ToDataTable("*");
            return list;
        }

        /// <summary>
        /// 通过故障类型查询记录并返回
        /// </summary>
        /// <param name="Severity">故障严重程度：（1-报警，2-降载，3-停机）</param>
        /// <returns>DataTable</returns>
        public DataTable GetFaultEventRecordBySeverity(int Severity)
        {
            var list = DB.mysql.Select<FaultEvent>()
                .Where(d => d.Severity == Severity)
                .ToDataTable("*");
            return list;
        }
    }
}

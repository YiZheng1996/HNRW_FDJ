using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RW.Data;
using System.Data;
using RW.Components.Core.BLL;
using RW.UI;
using MainUI.Data;
using MySql.Data.MySqlClient;   // ← 新增

namespace MainUI.BLL
{
    public class SysLogBLL : BaseBLL
    {
        protected override void Init()
        {
            // 只改这一处：SQLiteDB → MySqlAdoDb
            this.Database = new MySqlAdoDb(Var.ConnectionString);
            this.ConnectionString = Var.ConnectionString;
            this.Database.ConnectionString = this.ConnectionString;
            this.TableName = "SysLog";
        }

        public DataTable GetByCondition(string condition)
        {
            // 原逻辑不变，无方括号，无需修改 SQL
            string sql = "select a.ID as 序号, a.UName as 操作人员,a.LogTime as 操作时间,a.detailInfo as 详情 from SysLog a where 1=1  and status='1'" + condition + " order by a.id";
            return this.GetDataTable(sql);
        }

        public bool Add(string detailInfo, string nowStr)
        {
            string uname = RWUser.User.Username ?? "";

            string sql = "insert into SysLog(Uname,LogTime,DetailInfo,status) " +
                         "values(@uname,@logtime,@detail,'1')";

            var ps = new MySqlParameter[]
            {
                new MySqlParameter("@uname",   uname),
                new MySqlParameter("@logtime", nowStr ?? ""),
                new MySqlParameter("@detail",  detailInfo ?? ""),
            };

            var db = (MainUI.Data.MySqlAdoDb)base.Database;
            int cnt = db.ExecuteNonQuery(sql, ps);   // 走 params DbParameter[] 重载
            return cnt > 0;
        }

        public bool Delete(int ID)
        {
            // 逻辑删除，去掉 [SysLog] 方括号
            string sql = string.Format("update SysLog set status='0' where ID={0}", ID);
            int cnt = base.Database.ExecuteNonQuery(sql);
            return cnt > 0;
        }
    }
}

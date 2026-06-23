using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RW.Data;
using System.Data;
using RW.Components.Core.BLL;
using RW.UI;
using MainUI.Data;   // ← 新增

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
            int userid = RWUser.User.ID;
            string uname = RWUser.User.Username;
            string logtime = nowStr;

            // 去掉 [SysLog] 的方括号（其余字段名无括号，不用改）
            string sql = string.Format(
                "insert into SysLog(Uname,LogTime,DetailInfo,status) values('{0}','{1}','{2}','1')",
                uname, logtime, detailInfo);

            int cnt = base.Database.ExecuteNonQuery(sql);
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

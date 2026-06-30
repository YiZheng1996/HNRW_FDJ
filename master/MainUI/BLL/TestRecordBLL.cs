using MainUI.Data;
using MySql.Data.MySqlClient;
using RW.Components.Core.BLL;
using System;
using System.Collections.Generic;
using System.Data;

namespace MainUI.BLL
{
    public class TestRecordBLL : BaseBLL
    {
        protected override void Init()
        {
            this.ConnectionString = Var.ConnectionString;
            this.TableName = "Record";
            this.Database = new MySqlAdoDb(Var.ConnectionString);
            this.Database.ConnectionString = this.ConnectionString;
            base.Init();                                        
        }

        public DataTable GetList()
        {
            string sql = "select * from Record ";
            return this.GetDataTable(sql);
        }

        public int SaveData(string kind, string model, string testid, string tester, string testtime, string reportPath)
        {
            var db = (MySqlAdoDb)base.Database;
            string sql = "insert into Record (Kind,Model,TestID,Tester,TestTime,ReportPath,status) " +
                         "values(@kind,@model,@testid,@tester,@testtime,@report,1)";
            var ps = new MySqlParameter[]
            {
                new MySqlParameter("@kind",     kind ?? ""),       // Kind 为整型外键，驱动会把 "3" 转 3
                new MySqlParameter("@model",    model ?? ""),
                new MySqlParameter("@testid",   testid ?? ""),
                new MySqlParameter("@tester",   tester ?? ""),
                new MySqlParameter("@testtime", testtime ?? ""),
                new MySqlParameter("@report",   reportPath ?? ""), // 反斜杠路径参数化后不再被转义
            };
            return db.ExecuteNonQuery(sql, ps);
        }

        public DataTable FindList(string lx, string xh, string bh, string czy, DateTime from, DateTime to)
        {
            var db = (MySqlAdoDb)base.Database;
            var ps = new List<MySqlParameter>();

            string where = " and a.status=1 ";
            if (!string.IsNullOrEmpty(lx)) { where += " and a.kind=@kind "; ps.Add(new MySqlParameter("@kind", lx)); }
            if (!string.IsNullOrEmpty(xh)) { where += " and a.Model=@model "; ps.Add(new MySqlParameter("@model", xh)); }
            if (!string.IsNullOrEmpty(bh)) { where += " and a.TestID like @testid "; ps.Add(new MySqlParameter("@testid", bh)); }
            if (!string.IsNullOrEmpty(czy)) { where += " and a.Tester=@tester "; ps.Add(new MySqlParameter("@tester", czy)); }

            where += " and a.testTime between @from and @to ";
            ps.Add(new MySqlParameter("@from", from.Date));                          // 当天 00:00:00
            ps.Add(new MySqlParameter("@to", to.Date.AddDays(1).AddSeconds(-1)));  // 当天 23:59:59
            where += " order by a.TestTime desc";

            string sql = "select a.ID,a.model,a.testid,a.tester,a.testtime,a.reportpath,b.modeltype " +
                         "from Record a, modeltype b where a.kind=b.id " + where;

            return db.GetDataSet(sql, ps.ToArray()).Tables[0];
        }

        public int DelData(int id)
        {
            var db = (MySqlAdoDb)base.Database;
            return db.ExecuteNonQuery(
                "update Record set status=0 where id=@id",
                new MySqlParameter("@id", id));
        }
    }
}
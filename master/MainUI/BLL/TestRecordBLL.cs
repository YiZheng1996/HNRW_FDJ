using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.UI;
using System.Data;
using RW.Data;
using RW.Components.Core.BLL;
using MainUI.Data;   // ← 新增

namespace MainUI.BLL
{
    public class TestRecordBLL : BaseBLL
    {
        protected override void Init()
        {
            // 只改这一处：SQLiteDB → MySqlAdoDb
            this.Database = new MySqlAdoDb(Var.ConnectionString);
            this.ConnectionString = Var.ConnectionString;
            this.Database.ConnectionString = this.ConnectionString;
            this.TableName = "Record";
            base.Init();
        }

        public DataTable GetList()
        {
            // 原逻辑不变
            string sql = string.Format("select * from Record ");
            return this.GetDataTable(sql);
        }

        public int SaveData(string kind, string model, string testid, string tester, string testtime, string reportPath)
        {
            // 原逻辑不变，SQL 本身无方括号
            string sql = string.Format(
                "insert into Record (Kind,Model,TestID,Tester,TestTime,ReportPath,status) values ({0},'{1}','{2}','{3}','{4}','{5}',1)",
                kind, model, testid, tester, testtime, reportPath);
            return base.Database.ExecuteNonQuery(sql, null);
        }

        public DataTable FindList(string lx, string xh, string bh, string czy, DateTime from, DateTime to)
        {
            // 原逻辑完全不变：
            // Record.Kind = ModelType.ID，两表现已同在 MySQL，JOIN 直接生效
            string where = "and 1=1 ";
            where += " and status=1 ";
            if (!string.IsNullOrEmpty(lx))
                where += " and kind=" + lx;
            if (!string.IsNullOrEmpty(xh))
                where += " and Model='" + xh + "'";
            if (!string.IsNullOrEmpty(bh))
                where += " and TestID like '" + bh + "'";
            if (!string.IsNullOrEmpty(czy))
                where += " and Tester = '" + czy + "'";

            where += " and testTime BETWEEN '" + from.Date.ToString("yyyy-MM-dd 00:00:00") +
                     "' and '" + to.Date.ToString("yyyy-MM-dd 23:59:59") + "'";
            where += " order by TestTime desc";

            string sql1 = "select a.ID,a.model,a.testid,a.tester,a.testtime,a.reportpath,b.modeltype from Record a,modeltype b where a.kind=b.id " + where;
            return this.GetDataTable(sql1);
        }

        public int DelData(int id)
        {
            // 原逻辑不变
            string sql = string.Format("update Record set status=0 where id = {0}", id);
            return base.Database.ExecuteNonQuery(sql, null);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RW.Data;
using System.Data;
using RW.Components.Core.BLL;

using RW.UI;

namespace MainUI.BLL
{
    
    public class SysLogBLL : BaseBLL
    {
        protected override void Init()
        {
            //this.Database = new OleDB();
            //this.ConnectionString = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\DB.mdb;jet oledb:database password=ok";
            //this.Database.ConnectionString = this.ConnectionString;
            //this.TableName = "SysLog";

            this.Database = new SQLiteDB(Var.ConnectionString);
            this.ConnectionString = Var.ConnectionString;
            this.Database.ConnectionString = this.ConnectionString;

            this.TableName = "SysLog";
        }


        public DataTable GetByCondition(string condition)
        {
            //string sql = "select a.ID as 序号, a.UName as 操作人员,a.LogTime as 操作时间,a.FormName as 位置,a.detailInfo as 详情 from SysLog a where 1=1 " + condition +" order by a.id desc";
            string sql = "select a.ID as 序号, a.UName as 操作人员,a.LogTime as 操作时间,a.detailInfo as 详情 from SysLog a where 1=1  and status='1'" + condition + " order by a.id";

            return this.GetDataTable(sql);
        }


        public bool Add(string detailInfo,string nowStr)
        {
            //注 access的日期时间类型，sql语句中，当作字符串处理即可。
            int userid = RWUser.User.ID;
            string uname = RWUser.User.Username;
            string logtime = nowStr;// System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
           
            string sql = string.Format("insert into [SysLog]([Uname],LogTime,DetailInfo,status) values('{0}','{1}','{2}','1')",uname, logtime, detailInfo);
           
            int cnt = base.Database.ExecuteNonQuery(sql);
            if (cnt > 0)
                return true;
            else
                return false;
        }

        public  bool Delete(int ID)
        {
            //只进行逻辑删除。
            //需要对所有操作追踪溯源。
            string sql = string.Format("update [SysLog] set status='0' where ID={0}", ID);

            int cnt = base.Database.ExecuteNonQuery(sql);
            if (cnt > 0)
                return true;
            else
                return false;
        
        }

   


       



    }
}

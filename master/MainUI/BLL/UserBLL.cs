using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RW.Security;

using System.Data;
//using RW.UI.BLL;
using RW.Data;
using RW.UI.Model;
using RW.Components.Core.BLL;

namespace MainUI.BLL
{
    public class UserBLL: BaseBLL
    {
     

        protected override void Init()
        {
            //this.Database = new OleDB();
            //this.ConnectionString = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\DB.mdb;jet oledb:database password=ok";

            this.Database = new SQLiteDB(Var.ConnectionString);
            this.ConnectionString = Var.ConnectionString;

            this.Database.ConnectionString = this.ConnectionString;
            this.TableName = "Users";
            
        }

        public DataSet GetSortedList()
        {
            DataSet ds = new DataSet();
            string s = "select [ID],[Username],[Password],[Permission],[Sort] from [Users] order by [Sort]";
            ds = this.Database.GetDataSet(s);
            return ds;
        }

        public bool IsExist(string username)
        {
            DataTable dt = new DataTable();
            string s = "select [ID],[Username],[Password],[Permission],[Sort] from [Users] where username='"+ username +"'";
            dt = this.GetDataTable(s);
            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public int Save(UserInfo User)
        {
            string sql = string.Format("select * from Users where Username='{0}'", User.Username);
            DataSet ds = this.Database.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0 && Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]) != User.ID)
            {
                throw new Exception("用户名已存在，请重新输入！");
            }
            if (User.ID != 0)
            {
                sql = string.Format("update Users set [Username]='{0}',[Password]='{1}',[Permission]='{2}' where [ID]={3}",
                    User.Username, User.Password, User.Permission, User.ID);
            }
            else
            {
                //获取最大的Sort字段填充
                string sql2 = "select max([Sort]) from Users";
                ds = this.Database.GetDataSet(sql2);
                int paixu = 0;
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int.TryParse(ds.Tables[0].Rows[0][0].ToString(), out paixu);
                    paixu++;
                }

                sql = string.Format("insert into Users([Username],[Password],[Permission],[Sort]) values('{0}','{1}','{2}',{3})",
                    User.Username, User.Password, User.Permission, paixu);
            }
            int count = this.Database.ExecuteNonQuery(sql);
            return count;
            //return 0;
        }

        public int RemoveByUsername(string username)
        {
            string sql = string.Format("delete from Users where [Username]='{0}'", username);
            return this.Database.ExecuteNonQuery(sql);
        }

        public bool ChangePwd(string username, string password)
        {
            string sql = string.Format("Update [Users] set [password]='{1}' where [username]='{0}'", username, password);
            int cnt = this.Database.ExecuteNonQuery(sql);
            if (cnt >= 1)
                return true;
            else
                return false;
        }

        public int GetPermissionLevel(string permission)
        {
            int plevel = 0;
            switch (permission)
            {
                case "1":
                case "Adminsitrator":
                case "系统管理员":
                case "管理员": plevel = 0; break;
                case "2":
                case "系统测试人员":
                case "工艺员":
                case "工程师": plevel = 1; break;
                case "3":
                case "操作人员":
                case "操作员":
                default: plevel = 2; break;
            }
            return plevel;
        }





    }
}

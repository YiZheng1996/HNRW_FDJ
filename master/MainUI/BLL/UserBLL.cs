using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RW.Security;
using System.Data;
using RW.Data;
using RW.UI.Model;
using RW.Components.Core.BLL;
using MainUI.Data;   // ← 新增

namespace MainUI.BLL
{
    public class UserBLL : BaseBLL
    {
        protected override void Init()
        {
            // 只改这一处：SQLiteDB → MySqlAdoDb
            this.Database = new MySqlAdoDb(Var.ConnectionString);
            this.ConnectionString = Var.ConnectionString;
            this.Database.ConnectionString = this.ConnectionString;
            this.TableName = "Users";
        }

        public DataSet GetSortedList()
        {
            // 去掉 [] 方括号；Password 是 MySQL 函数名，加反引号保险
            string s = "select ID, Username, `Password`, Permission, Sort from Users order by Sort";
            return this.Database.GetDataSet(s);
        }

        public DataTable GetById(int id)
        {
            string sql = string.Format(
                "select ID, Username, `Password`, Permission, Sort from Users where ID={0}", id);
            return this.GetDataTable(sql);
        }

        public bool IsExist(string username)
        {
            string s = "select ID, Username, `Password`, Permission, Sort from Users where Username='" + username + "'";
            DataTable dt = this.GetDataTable(s);
            return dt != null && dt.Rows.Count > 0;
        }

        public int Save(UserInfo User)
        {
            string sql = string.Format("select * from Users where Username='{0}'", User.Username);
            DataSet ds = this.Database.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0 &&
                Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]) != User.ID)
            {
                throw new Exception("用户名已存在，请重新输入！");
            }

            if (User.ID != 0)
            {
                sql = string.Format(
                    "update Users set Username='{0}', `Password`='{1}', Permission='{2}' where ID={3}",
                    User.Username, User.Password, User.Permission, User.ID);
            }
            else
            {
                string sql2 = "select max(Sort) from Users";
                ds = this.Database.GetDataSet(sql2);
                int paixu = 0;
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int.TryParse(ds.Tables[0].Rows[0][0].ToString(), out paixu);
                    paixu++;
                }

                sql = string.Format(
                    "insert into Users(Username, `Password`, Permission, Sort) values('{0}','{1}','{2}',{3})",
                    User.Username, User.Password, User.Permission, paixu);
            }

            return this.Database.ExecuteNonQuery(sql);
        }

        public int RemoveByUsername(string username)
        {
            string sql = string.Format("delete from Users where Username='{0}'", username);
            return this.Database.ExecuteNonQuery(sql);
        }

        public bool ChangePwd(string username, string password)
        {
            string sql = string.Format(
                "Update Users set `Password`='{1}' where Username='{0}'", username, password);
            return this.Database.ExecuteNonQuery(sql) >= 1;
        }

        public int GetPermissionLevel(string permission)
        {
            int plevel = 0;
            switch (permission)
            {
                case "1":
                case "Adminsitrator":
                case "系统管理员":
                case "管理员":      plevel = 0; break;
                case "2":
                case "系统测试人员":
                case "工艺员":
                case "工程师":      plevel = 1; break;
                case "3":
                case "操作人员":
                case "操作员":
                default:            plevel = 2; break;
            }
            return plevel;
        }
    }
}

using MainUI.Data;
using MySql.Data.MySqlClient; 
using RW.Components.Core.BLL;
using RW.UI.Model;
using System;
using System.Data;

namespace MainUI.BLL
{
    public class UserBLL : BaseBLL
    {
        protected override void Init()
        {
            this.Database = new MySqlAdoDb(Var.ConnectionString);
            this.ConnectionString = Var.ConnectionString;
            this.Database.ConnectionString = this.ConnectionString;
            this.TableName = "Users";
        }

        public DataSet GetSortedList()
        {
            string s = "select ID, Username, `Password`, Permission, Sort from Users order by Sort";
            return this.Database.GetDataSet(s);
        }

        public DataTable GetById(int id)
        {
            var db = (MySqlAdoDb)this.Database;
            return db.GetDataSet(
                "select ID, Username, `Password`, Permission, Sort from Users where ID=@id",
                new MySqlParameter("@id", id)).Tables[0];
        }

        public bool IsExist(string username)
        {
            var db = (MySqlAdoDb)this.Database;
            DataTable dt = db.GetDataSet(
                "select ID from Users where Username=@u",
                new MySqlParameter("@u", username ?? "")).Tables[0];
            return dt != null && dt.Rows.Count > 0;
        }

        public int Save(UserInfo User)
        {
            var db = (MySqlAdoDb)this.Database;

            // 重名检查
            DataSet ds = db.GetDataSet(
                "select ID from Users where Username=@u",
                new MySqlParameter("@u", User.Username ?? ""));
            if (ds.Tables[0].Rows.Count > 0 &&
                Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]) != User.ID)
            {
                throw new Exception("用户名已存在，请重新输入！");
            }

            string sql;
            MySqlParameter[] ps;

            if (User.ID != 0)
            {
                sql = "update Users set Username=@u, `Password`=@p, Permission=@perm where ID=@id";
                ps = new MySqlParameter[]
                {
                    new MySqlParameter("@u",    User.Username ?? ""),
                    new MySqlParameter("@p",    User.Password ?? ""),
                    new MySqlParameter("@perm", (object)User.Permission ?? ""),
                    new MySqlParameter("@id",   User.ID),
                };
            }
            else
            {
                int paixu = 0;
                DataSet ds2 = db.GetDataSet("select max(Sort) from Users");
                if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    int.TryParse(ds2.Tables[0].Rows[0][0].ToString(), out paixu);
                    paixu++;
                }

                sql = "insert into Users(Username, `Password`, Permission, Sort) values(@u,@p,@perm,@sort)";
                ps = new MySqlParameter[]
                {
                    new MySqlParameter("@u",    User.Username ?? ""),
                    new MySqlParameter("@p",    User.Password ?? ""),
                    new MySqlParameter("@perm", (object)User.Permission ?? ""),
                    new MySqlParameter("@sort", paixu),
                };
            }

            return db.ExecuteNonQuery(sql, ps);
        }

        public int RemoveByUsername(string username)
        {
            var db = (MySqlAdoDb)this.Database;
            return db.ExecuteNonQuery(
                "delete from Users where Username=@u",
                new MySqlParameter("@u", username ?? ""));
        }

        public bool ChangePwd(string username, string password)
        {
            var db = (MySqlAdoDb)this.Database;
            int n = db.ExecuteNonQuery(
                "update Users set `Password`=@p where Username=@u",
                new MySqlParameter("@p", password ?? ""),
                new MySqlParameter("@u", username ?? ""));
            return n >= 1;
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
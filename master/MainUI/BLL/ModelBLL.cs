using MainUI.Data;
using MainUI.Model;
using MySql.Data.MySqlClient;
using RW.Components.Core.BLL;
using System;
using System.Data;

namespace MainUI.BLL
{
    public class ModelBLL : BaseBLL
    {
        protected override void Init()
        {
            this.Database = new MySqlAdoDb(Var.ConnectionString);
            this.ConnectionString = Var.ConnectionString;
            this.Database.ConnectionString = this.ConnectionString;
            this.TableName = "Model";
        }

        public DataTable GetAllKind()
        {
            string sql = "select a.ID, Name,a.typeid,b.modeltype,a.mark from Model a,ModelType b where a.typeID = b.ID order by a.id";
            return this.GetDataTable(sql);
        }

        public DataTable GetAllKindByCon(string condition)
        {
            string sql = "select a.ID, Name,a.typeid,b.modeltype,a.mark from Model a,ModelType b where a.typeID = b.ID " + condition + " order by a.id";
            return this.GetDataTable(sql);
        }

        public bool IsExist(string modelName)
        {
            var db = (MySqlAdoDb)base.Database;
            DataTable dt = db.GetDataSet(
                "select name from Model where name=@name",
                new MySqlParameter("@name", modelName ?? "")).Tables[0];
            return dt != null && dt.Rows.Count > 0;
        }

        public bool Add(string modelName, int typeid, string mark)
        {
            var db = (MySqlAdoDb)base.Database;
            string sql = "insert into Model(name,typeid,mark,CreateTime) values(@name,@typeid,@mark,@ct)";
            var ps = new MySqlParameter[]
            {
                new MySqlParameter("@name",   modelName ?? ""),
                new MySqlParameter("@typeid", typeid),
                new MySqlParameter("@mark",   mark ?? ""),
                new MySqlParameter("@ct",     DateTime.Now),   // 直接传 DateTime，免区域格式问题
            };
            return db.ExecuteNonQuery(sql, ps) >= 1;
        }

        public bool Delete(string modelID)
        {
            var db = (MySqlAdoDb)base.Database;
            return db.ExecuteNonQuery(
                "delete from Model where ID=@id",
                new MySqlParameter("@id", modelID ?? "")) >= 1;
        }

        public bool Update(string modelID, string name, int typeid, string mark)
        {
            var db = (MySqlAdoDb)base.Database;
            string sql = "update Model set name=@name, typeid=@typeid, mark=@mark, CreateTime=@ct where ID=@id";
            var ps = new MySqlParameter[]
            {
                new MySqlParameter("@name",   name ?? ""),
                new MySqlParameter("@typeid", typeid),
                new MySqlParameter("@mark",   mark ?? ""),
                new MySqlParameter("@ct",     DateTime.Now),
                new MySqlParameter("@id",     modelID ?? ""),
            };
            return db.ExecuteNonQuery(sql, ps) >= 1;
        }

        public TestViewModel GetModel(System.Data.DataRow row)
        {
            TestViewModel mTestViewModel = new TestViewModel();
            if (row == null)
                return mTestViewModel;
            mTestViewModel.ModelID = int.Parse(row["ID"].ToString());
            mTestViewModel.ModelName = row["Name"].ToString();
            mTestViewModel.ModelType = row["modeltype"].ToString();
            mTestViewModel.Mark = row["mark"].ToString();
            return mTestViewModel;
        }
    }
}
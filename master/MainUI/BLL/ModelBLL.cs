using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.Data;
using System.Data;
using MainUI.Model;
using RW.Components.Core.BLL;
using MainUI.Data;   // ← 新增

namespace MainUI.BLL
{
    public class ModelBLL : BaseBLL
    {
        protected override void Init()
        {
            // 只改这一处：SQLiteDB → MySqlAdoDb
            this.Database = new MySqlAdoDb(Var.ConnectionString);
            this.ConnectionString = Var.ConnectionString;
            this.Database.ConnectionString = this.ConnectionString;
            this.TableName = "Model";
        }

        public DataTable GetAllKind()
        {
            // 原逻辑不变，无方括号
            string sql = "select a.ID, Name,a.typeid,b.modeltype,a.mark from Model a,ModelType b where a.typeID = b.ID order by a.id";
            return this.GetDataTable(sql);
        }

        public DataTable GetAllKindByCon(string condition)
        {
            // 原逻辑不变
            string sql = "select a.ID, Name,a.typeid,b.modeltype,a.mark from Model a,ModelType b where a.typeID = b.ID " + condition + " order by a.id";
            return this.GetDataTable(sql);
        }

        public bool IsExist(string modelName)
        {
            // 原逻辑不变，无方括号
            string sql = string.Format("select name from Model where name='{0}'", modelName);
            DataTable dt = Database.GetDataSet(sql).Tables[0];
            return dt != null && dt.Rows.Count > 0;
        }

        public bool Add(string modelName, int typeid, string mark)
        {
            // 去掉 [Model]、[name] 的方括号
            string sql = string.Format(
                "insert into Model(name,typeid,mark,CreateTime) values('{0}',{1},'{2}','{3}')",
                modelName, typeid, mark, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            return base.Database.ExecuteNonQuery(sql, null) >= 1;
        }

        public bool Delete(string modelID)
        {
            // 去掉 [Model] 的方括号
            string sql = string.Format("delete from Model where ID={0}", modelID);
            return base.Database.ExecuteNonQuery(sql, null) >= 1;
        }

        public bool Update(string modelID, string name, int typeid, string mark)
        {
            // 去掉 [Model]、[name] 的方括号
            string sql = string.Format(
                "update Model set name='{1}',typeid={2},mark='{3}',CreateTime='{4}' where ID={0}",
                modelID, name, typeid, mark, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            return base.Database.ExecuteNonQuery(sql, null) >= 1;
        }

        public TestViewModel GetModel(System.Data.DataRow row)
        {
            // 原逻辑完全不变
            TestViewModel mTestViewModel = new TestViewModel();
            if (row == null)
                return mTestViewModel;
            mTestViewModel.ModelID   = int.Parse(row["ID"].ToString());
            mTestViewModel.ModelName = row["Name"].ToString();
            mTestViewModel.ModelType = row["modeltype"].ToString();
            mTestViewModel.Mark      = row["mark"].ToString();
            return mTestViewModel;
        }
    }
}

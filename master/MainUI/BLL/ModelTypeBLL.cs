using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.Data;
using System.Data;
using RW.Components.Core.BLL;
using MainUI.Data;   // ← 新增

namespace MainUI.BLL
{
    public class ModelTypeBLL : BaseBLL
    {
        protected override void Init()
        {
            // 只改这一处：SQLiteDB → MySqlAdoDb
            this.Database = new MySqlAdoDb(Var.ConnectionString);
            this.ConnectionString = Var.ConnectionString;
            this.Database.ConnectionString = this.ConnectionString;
            this.TableName = "ModelType";
            base.Init();
        }

        public DataTable GetAllModelType()
        {
            // 原逻辑不变，无方括号
            string sql = "select ID, ModelType from ModelType order by ID ";
            return this.GetDataTable(sql);
        }

        public DataTable GetAllKind()
        {
            // MySQL 5.7 默认开启 ONLY_FULL_GROUP_BY：
            // 原句 "group by a.modelType" 只列了一个列，而 SELECT 里还有
            // b.TypeID、b.id、b.Name 未聚合，MySQL 会直接报错。
            // 修复：把 SELECT 中所有非聚合列都加进 GROUP BY。
            string sql = "select b.TypeID,a.modelType,b.id,b.Name from ModelType a,Model b where a.ID=b.TypeID group by b.TypeID,a.modelType,b.id,b.Name";
            return this.GetDataTable(sql);
        }

        public DataTable GetAllKind(string con)
        {
            // 原逻辑不变，无方括号
            string sql = "select b.TypeID,a.modeltype,b.id,b.Name from ModelType a,Model b where a.ID=b.TypeID  and " + con;
            return this.GetDataTable(sql);
        }

        public bool IsExist(string typeName)
        {
            // 原逻辑不变，无方括号
            string sql = "select modeltype from ModelType  where modeltype='" + typeName + "'";
            DataTable dt = this.GetDataTable(sql);
            return dt != null && dt.Rows.Count > 0;
        }

        public bool Add(string typeName)
        {
            // 去掉 [ModelType]([ModelType]) 的方括号
            string sql = string.Format("insert into ModelType(ModelType) values('{0}')", typeName);
            return this.Database.ExecuteNonQuery(sql) > 0;
        }

        public bool Update(string modelID, string typeName)
        {
            // 去掉 [ModelType]、[ModelType] 的方括号
            string sql = string.Format(
                "update ModelType set ModelType='{1}' where ID={0}", modelID, typeName);
            return this.Database.ExecuteNonQuery(sql) > 0;
        }

        public bool Delete(string modelTypeID)
        {
            // 去掉 [ModelType] 的方括号
            string sql = string.Format("delete from ModelType where ID={0}", modelTypeID);
            return this.Database.ExecuteNonQuery(sql) > 0;
        }
    }
}

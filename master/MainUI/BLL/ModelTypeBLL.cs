using MainUI.Data;
using MySql.Data.MySqlClient;
using RW.Components.Core.BLL;
using System.Data;

namespace MainUI.BLL
{
    public class ModelTypeBLL : BaseBLL
    {
        protected override void Init()
        {
            this.ConnectionString = Var.ConnectionString;
            this.TableName = "ModelType";
            this.Database = new MySqlAdoDb(Var.ConnectionString)
            {
                ConnectionString = this.ConnectionString
            };
            base.Init();
        }

        public DataTable GetAllModelType()
        {
            string sql = "select ID, ModelType from ModelType order by ID ";
            return this.GetDataTable(sql);
        }

        public DataTable GetAllKind()
        {
            // MySQL 5.7 ONLY_FULL_GROUP_BY：SELECT 中非聚合列全部进 GROUP BY
            string sql = "select b.TypeID,a.modelType,b.id,b.Name from ModelType a,Model b where a.ID=b.TypeID group by b.TypeID,a.modelType,b.id,b.Name";
            return this.GetDataTable(sql);
        }

        public DataTable GetAllKind(string con)
        {
            string sql = "select b.TypeID,a.modeltype,b.id,b.Name from ModelType a,Model b where a.ID=b.TypeID  and " + con;
            return this.GetDataTable(sql);
        }

        public bool IsExist(string typeName)
        {
            var db = (MySqlAdoDb)this.Database;
            DataTable dt = db.GetDataSet(
                "select modeltype from ModelType where modeltype=@t",
                new MySqlParameter("@t", typeName ?? "")).Tables[0];
            return dt != null && dt.Rows.Count > 0;
        }

        public bool Add(string typeName)
        {
            var db = (MySqlAdoDb)this.Database;
            return db.ExecuteNonQuery(
                "insert into ModelType(ModelType) values(@t)",
                new MySqlParameter("@t", typeName ?? "")) > 0;
        }

        public bool Update(string modelID, string typeName)
        {
            var db = (MySqlAdoDb)this.Database;
            return db.ExecuteNonQuery(
                "update ModelType set ModelType=@t where ID=@id",
                new MySqlParameter("@t", typeName ?? ""),
                new MySqlParameter("@id", modelID ?? "")) > 0;
        }

        public bool Delete(string modelTypeID)
        {
            var db = (MySqlAdoDb)this.Database;
            return db.ExecuteNonQuery(
                "delete from ModelType where ID=@id",
                new MySqlParameter("@id", modelTypeID ?? "")) > 0;
        }
    }
}
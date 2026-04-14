using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.Data;
using System.Data;
using RW.Components.Core.BLL;

namespace MainUI.BLL
{
    public class ModelTypeBLL : BaseBLL
    {
     

        protected override void Init()
        {
            this.Database = new SQLiteDB(Var.ConnectionString);
            this.ConnectionString = Var.ConnectionString;
            this.Database.ConnectionString = this.ConnectionString;

            this.TableName = "ModelType";

            base.Init();
        }



        public DataTable GetAllModelType()
        {

            string sql = "";
            sql = "select ID, ModelType from ModelType order by ID ";
            return this.GetDataTable(sql);
        }


  



        public DataTable GetAllKind()
        {
            string sql = "select b.TypeID,a.modelType,b.id,b.Name from ModelType a,Model b where a.ID=b.TypeID group by a.modelType";
            DataTable dt = this.GetDataTable(sql);
            return dt;
        }


        public DataTable GetAllKind(string con)
        {
            string sql = "select b.TypeID,a.modeltype,b.id,b.Name from ModelType a,Model b where a.ID=b.TypeID  and " + con;
            DataTable dt = this.GetDataTable(sql);
            return dt;
        }

        public bool IsExist(string typeName)
        {
            string sql = "select modeltype from ModelType  where modeltype='" + typeName + "'";
            DataTable dt = this.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public bool Add(string typeName)
        {
            string sql = string.Format("insert into  [ModelType]([ModelType]) values('{0}')", typeName);
            int cnt = this.Database.ExecuteNonQuery(sql);
            if (cnt > 0)
                return true;
            else
                return false;
        }

        public bool Update(string modelID, string typeName)
        {
            //注 access的日期时间类型，sql语句中，当作字符串处理即可。
            string sql = string.Format("update [ModelType] set [ModelType]='{1}' where ID={0}", modelID, typeName);

            int cnt = this.Database.ExecuteNonQuery(sql);
            if (cnt > 0)
                return true;
            else
                return false;

        }

        public bool Delete(string modelTypeID)
        {
            string sql = string.Format("delete from [ModelType] where ID={0}", modelTypeID);

            int cnt = this.Database.ExecuteNonQuery(sql);
            if (cnt > 0)
                return true;
            else
                return false;

        }
    }
}

using System;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using RW.Data;

namespace MainUI.Data
{
    /// <summary>
    /// 基于 MySql.Data 的适配器，实现 RW.Data.IDbBase 接口。
    /// 用于替代原 SQLiteDB，供 BaseBLL 子类（UserBLL / SysLogBLL 等）使用。
    /// MySql.Data 已随 FreeSql MySQL 驱动一同引入，无需额外 NuGet 包。
    /// </summary>
    public class MySqlAdoDb : IDbBase
    {
        private string _connStr;

        public string ConnectionString
        {
            get => _connStr;
            set => _connStr = value;
        }

        public MySqlAdoDb() { }

        public MySqlAdoDb(string connectionString)
        {
            _connStr = connectionString;
        }

        // 连接测试
        public void Connect()
        {
            try
            {
                using (var conn = new MySqlConnection(_connStr))
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                Var.LogInfo("MySqlAdoDb.Connect 异常: " + ex.Message);
                throw;
            }
        }

        // 查询：返回 DataSet
        public DataSet GetDataSet(string sql)
        {
            try
            {
                var ds = new DataSet();
                using (var conn = new MySqlConnection(_connStr))
                using (var adapter = new MySqlDataAdapter(sql, conn))
                {
                    adapter.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                Var.LogInfo("MySqlAdoDb.GetDataSet 异常: " + ex.Message + " SQL: " + sql);
                throw;
            }
        }

        // 执行非查询（不带参数）
        public int ExecuteNonQuery(string sql)
        {
            try
            {
                using (var conn = new MySqlConnection(_connStr))
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Var.LogInfo("MySqlAdoDb.ExecuteNonQuery 异常: " + ex.Message + " SQL: " + sql);
                throw;
            }
        }

        // 执行非查询（object[] 参数，当前 BLL 均传 null）
        public int ExecuteNonQuery(string sql, object[] param)
        {
            if (param is null)
            {
                throw new ArgumentNullException(nameof(param));
            }

            return ExecuteNonQuery(sql);
        }

        // 执行非查询（DbParameter[] 参数）
        public int ExecuteNonQuery(string sql, params DbParameter[] parameters)
        {
            try
            {
                using (var conn = new MySqlConnection(_connStr))
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    conn.Open();
                    if (parameters != null && parameters.Length > 0)
                        cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Var.LogInfo("MySqlAdoDb.ExecuteNonQuery(params) 异常: " + ex.Message + " SQL: " + sql);
                throw;
            }
        }

        // 使用 CloseConnection，reader 关闭时连接自动释放
        public DbDataReader ExecuteReader(string sql)
        {
            try
            {
                var conn = new MySqlConnection(_connStr);
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                Var.LogInfo("MySqlAdoDb.ExecuteReader 异常: " + ex.Message + " SQL: " + sql);
                throw;
            }
        }

        // 标量查询
        public object ExecuteScalar(string sql)
        {
            try
            {
                using (var conn = new MySqlConnection(_connStr))
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Var.LogInfo("MySqlAdoDb.ExecuteScalar 异常: " + ex.Message + " SQL: " + sql);
                throw;
            }
        }
    }
}
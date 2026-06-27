using System;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using RW.Data;

namespace MainUI.Data
{
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

        // 查询：返回 DataSet（不带参数）
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

        // 查询：返回 DataSet（带参数）—— 新增，供 BLL 做参数化 SELECT
        public DataSet GetDataSet(string sql, params DbParameter[] parameters)
        {
            try
            {
                var ds = new DataSet();
                using (var conn = new MySqlConnection(_connStr))
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    if (parameters != null && parameters.Length > 0)
                        cmd.Parameters.AddRange(parameters);
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(ds);
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                Var.LogInfo("MySqlAdoDb.GetDataSet(params) 异常: " + ex.Message + " SQL: " + sql);
                throw;
            }
        }

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

        // object[] 参数：当前 BLL 普遍传 null，视为无参直接执行
        public int ExecuteNonQuery(string sql, object[] param)
        {
            return ExecuteNonQuery(sql);
        }

        // DbParameter[] 参数
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
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSql;

namespace MainUI.FSql
{
    /// <summary>
    /// FreeSql 基础类
    /// </summary>
    public class DB
    {
        static Lazy<IFreeSql> sqlLazy = new Lazy<IFreeSql>(() =>
        {
            // 从配置文件读取连接字符串
            string connectionString = GetConnectionString();

            var fsql = new FreeSql.FreeSqlBuilder()
                .UseMonitorCommand(cmd => Trace.WriteLine($"Sql：{cmd.CommandText}"))
                .UseAdoConnectionPool(true)
                .UseConnectionString(FreeSql.DataType.MySql, connectionString)
                .UseAutoSyncStructure(true)
                .UseNoneCommandParameter(false)
                .UseLazyLoading(false)
                .UseMonitorCommand(cmd =>
                {
                    // 暂时注释
                    //Debug.WriteLine($"SQL: {cmd.CommandText}");
                })
                .Build();
            return fsql;
        });

        public static IFreeSql mysql => sqlLazy.Value;

        /// <summary>
        /// 从配置文件获取连接字符串
        /// </summary>
        private static string GetConnectionString()
        {
            try
            {
                // 方式1：从 connectionStrings 节点读取
                var connectionStringSettings = ConfigurationManager.ConnectionStrings["MySQLConnection"];
                if (connectionStringSettings != null)
                {
                    return connectionStringSettings.ConnectionString;
                }

                // 如果配置文件中没有找到，使用默认值
                throw new ConfigurationErrorsException("未在配置文件中找到数据库连接字符串配置");
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"读取连接字符串配置失败: {ex.Message}");
                throw;
            }
        }
    }
}
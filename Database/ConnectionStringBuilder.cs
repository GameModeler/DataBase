using DataBase.Interfaces;
using DataBase.Utils;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Reflection;

namespace DataBase.Database
{
    /// <summary>
    /// Build the database connection string
    /// </summary>
    public static class ConnectionStringBuilder
    {
        private static DbConfig dbConfig = new DbConfig();

        public static string BuildConnectionString(ProviderType provider, IDbSettings settings = null)
        {
           switch (provider)
            {
                case ProviderType.MySQL:
                    if (settings == null || DataBaseUtils.IsAllNullOrEmpty(settings)) //database settings is empty
                    { 
                        settings = new DbSettings(dbConfig.GetDefaultDbName(DatabaseManager.Instance.NbDefaultDb) , provider);                       
                    }

                    //DbConfiguration.SetConfiguration(new MySqlEFConfiguration());

                    break;
                default:
                    if (settings == null || DataBaseUtils.IsAllNullOrEmpty(settings))
                    {
                        settings = new DbSettings(dbConfig.GetDefaultDbName(DatabaseManager.Instance.NbDefaultDb), provider);
                    }
                    break;
            }
            return dbConfig.ProviderConnectionString(provider, settings);
        }

        public static DbConnection GetConnection(ProviderType provider, IDbSettings settings = null)
        {
            var factory = DbProviderFactories.GetFactory(ProviderType.MySQL.GetStringValue());
            var connection = factory.CreateConnection();
            connection.ConnectionString = ProviderConnectionString(provider, settings);
            return connection;
        }

        private static string ProviderConnectionString(ProviderType provider, IDbSettings settings)
        {
            if (string.IsNullOrEmpty(settings.ConnectionString))
            {
                switch (provider)
                {
                    case ProviderType.MySQL:
                        return BuildMySQLConnectionString(settings);
                    case ProviderType.SQLite:
                        return BuildSQLiteConnectionString(settings);
                    default:
                        return BuildSQLConnectionString(settings);
                }
            }
            else
            {
                return settings.ConnectionString;
            }
        }

        /// <summary>
        /// Build MySQL connection string
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string BuildMySQLConnectionString(IDbSettings settings)
        {
            // Initialize the connection string builder for the
            // underlying provider.
            MySqlConnectionStringBuilder mysqlBuilder = new MySqlConnectionStringBuilder();

            if (!string.IsNullOrEmpty(settings.DatabaseName))
            {
                mysqlBuilder.Database = settings.DatabaseName;
            }
            if (!string.IsNullOrEmpty(settings.Server))
            {
                mysqlBuilder.Server = settings.Server;
            }
            if (!string.IsNullOrEmpty(settings.UserId))
            {
                mysqlBuilder.UserID = settings.UserId;
            }
            if (!string.IsNullOrEmpty(settings.Password))
            {
                mysqlBuilder.Password = settings.Password;
            }
            if (!string.IsNullOrEmpty(settings.Port))
            {
                mysqlBuilder.Port = Convert.ToUInt32(settings.Port, 16);
            }

            return mysqlBuilder.ToString();
        }

        /// <summary>
        /// Build SQL connection string
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string BuildSQLConnectionString(IDbSettings settings)
        {
            // Initialize the connection string builder for the
            // underlying provider.
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();

            if (!string.IsNullOrEmpty(settings.DatabaseName))
            {
                sqlBuilder.InitialCatalog = settings.DatabaseName;
            }
            if (!string.IsNullOrEmpty(settings.Server))
            {
                sqlBuilder.DataSource = DataBaseUtils.BuildDataSource(settings.Server, settings.Port);
            }
            if (!string.IsNullOrEmpty(settings.UserId))
            {
                sqlBuilder.UserID = settings.UserId;
            }
            if (!string.IsNullOrEmpty(settings.Password))
            {
                sqlBuilder.Password = settings.Password;
            }

            sqlBuilder.IntegratedSecurity = true;

            sqlBuilder.MultipleActiveResultSets = true;

            // Build the SqlConnection connection string.
            return sqlBuilder.ToString();
        }

        /// <summary>
        /// Build SQLite connection string
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string BuildSQLiteConnectionString(IDbSettings settings)
        {
            // Initialize the connection string builder for the
            // underlying provider.

            SQLiteConnectionStringBuilder sqlBuilder = new SQLiteConnectionStringBuilder();

            if (!string.IsNullOrEmpty(settings.DatabaseName))
            {
                sqlBuilder.DataSource = settings.DatabaseName;
            }
            if (!string.IsNullOrEmpty(settings.Server))
            {
                sqlBuilder.DataSource = settings.Server;
            }
            if (!string.IsNullOrEmpty(settings.Password))
            {
                sqlBuilder.Password = settings.Password;
            }

            //sqlBuilder.Version = 3;
            //sqlBuilder.

            //sqlBuilder.ForeignKeys = true;

            //sqlBuilder.


            // Build the SqlConnection connection string.
            return sqlBuilder.ToString();
        }
    }
}

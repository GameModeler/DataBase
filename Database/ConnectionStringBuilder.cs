using GMDataBase.Database.DbSettings;
using GMDataBase.Database.DbSettings.DbClasses;
using GMDataBase.Database.DbSettings.Interface;
using GMDataBase.Interfaces;
using GMDataBase.Utils;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Reflection;

namespace GMDataBase.Database
{
    /// <summary>
    /// Build the database connection string
    /// </summary>
    public class ConnectionStringBuilder
    {
        private static GmDbManager dbManager = GmDbManager.Instance;

        public static string BuildConnectionString(ProviderType provider, IDbSettings settings = null)
        {
            if (settings == null || DataBaseUtils.IsAllNullOrEmpty(settings)) //database settings is empty
            {
                int nbName = dbManager.NbDefaultDb;
                List<Type> classList = GenericUtils.AllClassesFromNamespace("DbClasses");
                var clazz = GenericUtils.GetClassesFromProperty(classList, "Provider", provider);
                GenericUtils.CallMathodByReflection(typeof(DatabaseFactory), "DatabaseSettings", clazz, GetDefaultDbName(nbName));

                settings = dbManager.GetDatabase(GetDefaultDbName(nbName));

            }

            return ProviderConnectionString(provider, settings);
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
                        return BuildMySQLConnectionString((MySqlDatabase)settings);

                    case ProviderType.SQLite:
                        return BuildSQLiteConnectionString((SqLiteDatabase)settings);

                    default:
                        return "";
                }
            }
            else
            {
                return settings.ConnectionString;
            }
        }

        private static string GetDefaultDbName(int nb)
        {
            return Assembly.GetExecutingAssembly().GetName().Name + "_" + nb.ToString();
        }

        /// <summary>
        /// Build MySQL connection string
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string BuildMySQLConnectionString(MySqlDatabase settings)
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
        /// Build SQLite connection string
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string BuildSQLiteConnectionString(SqLiteDatabase settings)
        {
            // Initialize the connection string builder for the
            // underlying provider.
            SQLiteConnectionStringBuilder sqlBuilder = new SQLiteConnectionStringBuilder();

            if (!string.IsNullOrEmpty(settings.DataSource))
            {
                sqlBuilder.DataSource = settings.DataSource;
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

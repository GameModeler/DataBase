using System;
using DataBase.Database.DbSettings.DbClasses;
using DataBase.Utils;
using DataBase.Database.DbSettings.Interface;

namespace DataBase.Database.DbSettings.FluentInterface
{
    /// <summary>
    /// MySql Fluent API
    /// </summary>
    public class MySqlDatabaseFI : IDbSettingsFI, IMySqlDatabaseFI
    {
        private readonly MySqlDatabase mysql;
        private GmDbManager dbManager = GmDbManager.Instance;

        /// <summary>
        /// Constructor
        /// </summary>
        public MySqlDatabaseFI(MySqlDatabase mysqlFI)
        {
            mysql = mysqlFI;
        }

        /// <summary>
        /// Sets MySql Database Connection String
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public MySqlDatabaseFI ConnectionString(string connectionString)
        {
            mysql.ConnectionString = connectionString;
            return this;
        }

        /// <summary>
        /// Sets MySql Database's nama
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public MySqlDatabaseFI DatabaseName(string databaseName)
        {
            IDbSettings db;
            if (mysql.DatabaseName != null && dbManager.Databases.TryGetValue(mysql.DatabaseName, out db))
            {
                DataBaseUtils.UpdateKey<string, IDbSettings>(dbManager.Databases, mysql.DatabaseName, databaseName);
            }
            else
            {
                // Register database into the database manager
                dbManager.Databases.Add(databaseName, mysql);
            }

            mysql.DatabaseName = databaseName;
            return this;
        }

        /// <summary>
        /// Sets MySql Database Password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public MySqlDatabaseFI Password(string password)
        {
            mysql.Password = password;
            return this;
        }

        /// <summary>
        /// Sets Mysql Database Port
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public MySqlDatabaseFI Port(int port)
        {
            mysql.Port = port.ToString();
            return this;
        }

        /// <summary>
        /// Sets Mysql Database Server address
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        public MySqlDatabaseFI Server(string server)
        {
            mysql.Server = server;
            return this;
        }
    
        /// <summary>
        /// Sets Mysql Database User's id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public MySqlDatabaseFI UserId(string userId)
        {
            mysql.UserId = userId;
            return this;
        }

        /// <summary>
        /// Build the Mysql database connection string
        /// </summary>
        /// <returns></returns>
        public string ToConnectionString()
        {
            return ConnectionStringBuilder.BuildConnectionString(ProviderType.MySQL, mysql);
        }
    }
}

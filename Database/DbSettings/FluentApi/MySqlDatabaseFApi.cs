using DataBase.Database.DbSettings.DbClasses;
using DataBase.Database.DbSettings.FluentApi.Interfaces;
using DataBase.Database.Utils;

namespace DataBase.Database.DbSettings.FluentApi
{
    /// <summary>
    /// MySql Fluent API
    /// </summary>
    public class MySqlDatabaseFApi : IMySqlDatabaseFApi
    {
        private readonly MySqlDatabase mysql;
        private DbManager dbManager = DbManager.Instance;

        /// <summary>
        /// Constructor
        /// </summary>
        public MySqlDatabaseFApi(MySqlDatabase mysqlFI)
        {
            mysql = mysqlFI;
        }

        /// <summary>
        /// Sets MySql Database Connection String
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public IMySqlDatabaseFApi ConnectionString(string connectionString)
        {
            mysql.ConnectionString = connectionString;
            return this;
        }

        /// <summary>
        /// Sets MySql Database's nama
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public IMySqlDatabaseFApi DatabaseName(string databaseName)
        {
            mysql.DatabaseName = databaseName;
            return this;
        }

        /// <summary>
        /// Sets MySql Database Password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public IMySqlDatabaseFApi Password(string password)
        {
            mysql.Password = password;
            return this;
        }

        /// <summary>
        /// Sets Mysql Database Port
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public IMySqlDatabaseFApi Port(int port)
        {
            mysql.Port = port.ToString();
            return this;
        }

        /// <summary>
        /// Sets Mysql Database Server address
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        public IMySqlDatabaseFApi Server(string server)
        {
            mysql.Server = server;
            return this;
        }
    
        /// <summary>
        /// Sets Mysql Database User's id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IMySqlDatabaseFApi UserId(string userId)
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

        /// <summary>
        /// Convert to MySqlDatabse type
        /// </summary>
        /// <returns></returns>
        public MySqlDatabase ToMySqlDatabase
        {
            get { return mysql; }
        }

    }
}

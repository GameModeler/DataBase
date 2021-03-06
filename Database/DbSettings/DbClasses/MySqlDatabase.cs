﻿using DataBase.Database.DbSettings.FluentApi;
using DataBase.Database.DbSettings.Interfaces;
using DataBase.Database.Utils;

namespace DataBase.Database.DbSettings.DbClasses
{
    /// <summary>
    /// MySql Database settings implementation
    /// </summary>
    public class MySqlDatabase : IMySqlDatabase
    {

        DbManager dbManager = DbManager.Instance;

        const ProviderType PROVIDER = ProviderType.MySQL;

        private readonly MySqlDatabaseFApi mySqlFI;

        #region Database basic settings

        private string databaseName;

        /// <summary>
        /// Database name
        /// </summary>
        public string DatabaseName
        {
            get { return databaseName; }
            set {

                IDbSettings db;
                if(DatabaseName != null && dbManager.Databases.TryGetValue(DatabaseName, out db))
                {
                    DataBaseUtils.UpdateKey<string, IDbSettings>(dbManager.Databases, DatabaseName, value);
                } else
                {
                    // Register database into the database manager
                    dbManager.Databases.Add(value, this);
                }

                databaseName = value;
            }
        }


        /// <summary>
        /// Server
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Port
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnectionString { get; set; }

        private ProviderType provider = PROVIDER;

        /// <summary>
        /// Provider
        /// </summary>
        public ProviderType Provider
        {
            get { return provider; }
        }
        #endregion

        /// <summary>
        /// Initialize the fluent API
        /// </summary>
        public MySqlDatabaseFApi Set
        {
            get { return mySqlFI; }
        }

        #region Constructor


        /// <summary>
        /// Default constructor
        /// </summary>
        public MySqlDatabase() {

            // Sets a default name to the database
            DatabaseName = ConnectionStringBuilder.GetDefaultDbName(dbManager.GetAndIncrNbDefaultDb());

            // Intitialization of the Fluent API
            mySqlFI = new MySqlDatabaseFApi(this);

        }

        /// <summary>
        /// Constructor
        /// All params are optionals
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="port"></param>
        /// <param name="server"></param>
        /// <param name="connectionString"></param>
        public MySqlDatabase(
                          string databaseName = "",
                          string userId = "",
                          string password = "",
                          string port = "",
                          string server = "",
                          string connectionString = "")
        {
            DatabaseName = databaseName;
            Port = port;
            UserId = userId;
            Password = password;
            Port = port;
            Server = server;
            ConnectionString = "";

            // Fluent API initialization
            mySqlFI = new MySqlDatabaseFApi(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbName"></param>
        public MySqlDatabase(string dbName)
        {
            MySqlDbDefault(dbName);

            // Fluent API initialization
            mySqlFI = new MySqlDatabaseFApi(this);
        }
        #endregion

        /// <summary>
        /// MySQL default database settings
        /// </summary>
        /// <param name="dbName"></param>
        private void MySqlDbDefault(string dbName)
        {
            DatabaseName = dbName;
            Server = "localhost";
            Port = "";
            UserId = "root";
            Password = "";
            ConnectionString = "";

        }

        /// <summary>
        /// Build the database connection string
        /// </summary>
        /// <returns></returns>
        public string ToConnectionString()
        {
            return ConnectionStringBuilder.BuildConnectionString(ProviderType.MySQL, this);
        }
    }
}

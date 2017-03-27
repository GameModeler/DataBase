using DataBase.Interfaces;
using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Database
{
    public class DbSettings : IDbSettings
    {
        #region Database basic settings
        public string DatabaseName {get; set;}
        public string Server { get; set; }
        public string Port { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string ConfigConnectionStringName { get; set; }
        public string ConnectionString { get; set; }
        #endregion

        #region Constructor
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
        public DbSettings(string databaseName = "",
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
        }

        /// <summary>
        /// Default settings from a given provider
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="provider"></param>
        public DbSettings(String dbName, ProviderType provider)
        {
            switch (provider)
            {
                case ProviderType.MySQL:
                    MySqlSettings(dbName);
                    break;
                default:
                    LocalDbSettings(dbName);
                    break;
            }
        }
        #endregion

        public string GetCrible(string crible)
        {
           
            switch (crible)
            {
                case "DatabaseName":
                    return DatabaseName;
                case "Server":
                    return Server;
                case "UserId":
                    return UserId;
                case "Password":
                    return Password;
                default:
                    return "";
            }
        }

        /// <summary>
        /// MySQL default database settings
        /// </summary>
        /// <param name="dbName"></param>
        public void MySqlSettings(string dbName)
        {
            DatabaseName = dbName;
            Server = "localhost";
            Port = "";
            UserId = "root";
            Password = "";
            ConfigConnectionStringName = "";
            ConnectionString = "";

        }

        /// <summary>
        /// LocalDb default database settings
        /// </summary>
        /// <param name="dbName"></param>
        public void LocalDbSettings(string dbName)
        {
            DatabaseName = dbName;
            Server = @"(localdb)\mssqllocaldb";
            Port = "";
            UserId = "";
            Password = "";
            ConfigConnectionStringName = "";
            ConnectionString = "";
        }

        /// <summary>
        /// SQLite default database settings
        /// </summary>
        /// <param name="dbName"></param>
        public void SQLiteSettings(string dbName)
        {
            throw new NotImplementedException();
        }
    }
}

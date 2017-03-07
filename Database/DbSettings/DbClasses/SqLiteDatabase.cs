using GMDataBase.Database.DbSettings.Interface;
using GMDataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMDataBase.Database.DbSettings.DbClasses
{
    public class SqLiteDatabase : IDbSettings
    {
        const ProviderType PROVIDER = ProviderType.SQLite;

        #region Database basic settings
        public string DatabaseName { get; set; }
        public string DataSource { get; set; }
        public int Version { get; set; }
        public bool New { get; set; }
        public bool UseUTF16Encoding { get; set; }
        public string Port { get; set; }
        public string Password { get; set; }
        public bool LegacyFormat { get; set; }
        public bool Pooling { get; set; }
        public int MaxPoolSize { get; set; }
        public bool ReadOnly { get; set; }
        public string DateTimeFormat { get; set; }
        public int CacheSize { get; set; }
        public string ConnectionString { get; set; }

        // Provider
        private ProviderType provider = PROVIDER;

        public ProviderType Provider
        {
            get { return provider; }
        }
        #endregion

        #region Constructor

        public SqLiteDatabase() { }

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
        public SqLiteDatabase(
                          string databaseName = "",
                          string dataSource = "",
                          string password = "",
                          int version = 3,
                          string connectionString = "")
        {
            DatabaseName = databaseName;
            DataSource = dataSource;
            Version = version;
            ConnectionString = "";

            GmDbManager.Instance.Databases.Add(DatabaseName, this);
        }

        /// <summary>
        /// Default settings from a given provider
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="provider"></param>
        public SqLiteDatabase(string dbName)
        {
            SqLiteDbDefault(dbName);

            GmDbManager.Instance.Databases.Add(DatabaseName, this);

        }
        #endregion

        /// <summary>
        /// MySQL default database settings
        /// </summary>
        /// <param name="dbName"></param>
        private void SqLiteDbDefault(string dbName)
        {
            DatabaseName = dbName;
            DataSource = System.Reflection.Assembly.GetEntryAssembly().Location;
            Version = 3;
            Password = "";
            ConnectionString = "";
        }
    }
}

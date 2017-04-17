using DataBase.Database.DbSettings.FluentApi;
using DataBase.Database.DbSettings.Interfaces;
using DataBase.Database.Utils;

namespace DataBase.Database.DbSettings.DbClasses
{
    /// <summary>
    /// Sqlite database settings
    /// </summary>
    public class SqLiteDatabase : IDbSettings, ISqLiteDatabase
    {
        const ProviderType PROVIDER = ProviderType.SQLite;

        private DbManager dbManager = DbManager.Instance;

        private readonly SqLiteDatabaseFApi sqliteFI;

        #region Database basic settings
        private string databaseName;

        /// <summary>
        /// Database name
        /// </summary>
        public string DatabaseName
        {
            get { return databaseName; }
            set
            {
                IDbSettings db;
                if (DatabaseName != null && dbManager.Databases.TryGetValue(DatabaseName, out db))
                {
                    DataBaseUtils.UpdateKey<string, IDbSettings>(dbManager.Databases, DatabaseName, value);
                }
                else
                {
                    // Register database into the database manager
                    dbManager.Databases.Add(value, this);

                }

                databaseName = value;
            }
        }

        /// <summary>
        /// Data source
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// Database version
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// New
        /// </summary>
        public bool New { get; set; }

        /// <summary>
        /// Database UTF16 encoding
        /// </summary>
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

        private ProviderType provider = PROVIDER;

        /// <summary>
        /// Provider
        /// </summary>
        public ProviderType Provider
        {
            get { return provider; }
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SqLiteDatabase() {

            // Sets a default name to the database
            DatabaseName = ConnectionStringBuilder.GetDefaultDbName(dbManager.GetAndIncrNbDefaultDb());

            // Intitialization of the Fluent API
            sqliteFI = new SqLiteDatabaseFApi(this);
        }

        /// <summary>
        /// Initialize the fluent API
        /// </summary>
        public SqLiteDatabaseFApi Set
        {
            get { return sqliteFI; }
        }

        /// <summary>
        /// Constructor
        /// All params are optionals
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="dataSource"></param>
        /// <param name="password"></param>
        /// <param name="version"></param>
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

            sqliteFI = new SqLiteDatabaseFApi(this);
        }

        /// <summary>
        /// Default settings from a given provider
        /// </summary>
        /// <param name="dbName"></param>
        public SqLiteDatabase(string dbName)
        {
            SqLiteDbDefault(dbName);
            sqliteFI = new SqLiteDatabaseFApi(this);
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

        /// <summary>
        /// Build the database connection string
        /// </summary>
        /// <returns></returns>
        public string ToConnectionString()
        {
            return ConnectionStringBuilder.BuildConnectionString(ProviderType.SQLite, this);
        }
    }
}

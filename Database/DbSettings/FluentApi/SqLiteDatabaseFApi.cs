using DataBase.Database.DbSettings.DbClasses;
using DataBase.Database.DbSettings.FluentApi.Interfaces;
using DataBase.Database.Utils;

namespace DataBase.Database.DbSettings.FluentApi
{
    /// <summary>
    /// SqLite Fluent API
    /// </summary>
    public class SqLiteDatabaseFApi : ISqLiteDatabaseFApi
    {
        readonly SqLiteDatabase sqlite;
        private DbManager dbManager = DbManager.Instance;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sqliteDb"></param>
        /// <returns></returns>
        public SqLiteDatabaseFApi(SqLiteDatabase sqliteDb)
        {
            sqlite = sqliteDb;
        }

        /// <summary>
        /// Sets the sqlite database name
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public ISqLiteDatabaseFApi DatabaseName(string databaseName)
        {
            sqlite.DatabaseName = databaseName;
            return this;
        }

        /// <summary>
        /// Sets the sqlite data source
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public ISqLiteDatabaseFApi DataSource(string dataSource)
        {
            sqlite.DataSource = dataSource;
            return this;
        }

        /// <summary>
        /// Sets the sqlite database version
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public ISqLiteDatabaseFApi Version(int version)
        {
            sqlite.Version = version;
            return this;
        }

        /// <summary>
        /// Sets the sqlite database UTF16 encoding
        /// </summary>
        /// <param name="isNew"></param>
        /// <returns></returns>
        public ISqLiteDatabaseFApi New(bool isNew)
        {
            sqlite.New = isNew;
            return this;
        }

        /// <summary>
        /// Sets the sqlite database UTF16 encoding
        /// </summary>
        /// <param name="useUTF16encoding"></param>
        /// <returns></returns>
        public ISqLiteDatabaseFApi UseUTF16Encoding(bool useUTF16encoding)
        {
            sqlite.UseUTF16Encoding = useUTF16encoding;
            return this;
        }

        /// <summary>
        /// Sets the sqlite database port
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public ISqLiteDatabaseFApi Port(string port)
        {
            sqlite.Port = port;
            return this;
        }

        /// <summary>
        /// Sets the sqlite database user password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public ISqLiteDatabaseFApi Password(string password)
        {
            sqlite.Password = password;
            return this;
        }

        /// <summary>
        /// Sets the sqlite database legacy formate
        /// </summary>
        /// <param name="legacyFormat"></param>
        /// <returns></returns>
        public ISqLiteDatabaseFApi LegacyFormat(bool legacyFormat)
        {
            sqlite.LegacyFormat = legacyFormat;
            return this;
        }

        /// <summary>
        /// Sets the sqlite database pooling
        /// </summary>
        /// <param name="pooling"></param>
        /// <returns></returns>
        public ISqLiteDatabaseFApi Pooling(bool pooling)
        {
            sqlite.Pooling = pooling;
            return this;
        }

        /// <summary>
        /// Sets the sqlite database max pool size
        /// </summary>
        /// <param name="maxPoolSize"></param>
        /// <returns></returns>
        public ISqLiteDatabaseFApi MaxPoolSize(int maxPoolSize)
        {
            sqlite.MaxPoolSize = maxPoolSize;
            return this;
        }

        /// <summary>
        /// Sets the sqlite database as readonly
        /// </summary>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public ISqLiteDatabaseFApi ReadOnly(bool readOnly)
        {
            sqlite.ReadOnly = readOnly;
            return this;
        }

        /// <summary>
        /// Sets the sqlite database time format
        /// </summary>
        /// <param name="dateTimeFormat"></param>
        /// <returns></returns>
        public ISqLiteDatabaseFApi DateTimeFormat(string dateTimeFormat)
        {
            sqlite.DateTimeFormat = dateTimeFormat;
            return this;
        }

        /// <summary>
        /// Sets the sqlite database cache size
        /// </summary>
        /// <param name="cacheSize"></param>
        /// <returns></returns>
        public ISqLiteDatabaseFApi CacheSize(int cacheSize)
        {
            sqlite.CacheSize = cacheSize;
            return this;
        }

        /// <summary>
        /// Build the SqLite database connection string
        /// </summary>
        /// <returns></returns>
        public string ToConnectionString()
        {
            return ConnectionStringBuilder.BuildConnectionString(ProviderType.SQLite, sqlite);
        }

        /// <summary>
        /// Convert to SqLiteDatabase type
        /// </summary>
        /// <returns></returns>
        public SqLiteDatabase ToSqLiteDatabase
        {
            get { return sqlite; }
        }
    }
}

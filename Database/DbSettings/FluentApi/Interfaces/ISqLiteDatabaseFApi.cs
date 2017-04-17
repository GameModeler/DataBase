
namespace DataBase.Database.DbSettings.FluentApi.Interfaces
{
    /// <summary>
    /// SqLite Fluent Interface
    /// </summary>
    public interface ISqLiteDatabaseFApi
    {
        /// <summary>
        /// Sets the sqlite database name
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        SqLiteDatabaseFApi DatabaseName(string databaseName);

        /// <summary>
        /// Sets the sqlite data source
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        SqLiteDatabaseFApi DataSource(string dataSource);

        /// <summary>
        /// Sets the sqlite database version
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        SqLiteDatabaseFApi Version(int version);

        /// <summary>
        /// Sets the sqlite database UTF16 encoding
        /// </summary>
        /// <param name="isNew"></param>
        /// <returns></returns>
        SqLiteDatabaseFApi New(bool isNew);

        /// <summary>
        /// Sets the sqlite database UTF16 encoding
        /// </summary>
        /// <param name="useUTF16encoding"></param>
        /// <returns></returns>
        SqLiteDatabaseFApi UseUTF16Encoding(bool useUTF16encoding);

        /// <summary>
        /// Sets the sqlite database port
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        SqLiteDatabaseFApi Port(string port);

        /// <summary>
        /// Sets the sqlite database user password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        SqLiteDatabaseFApi Password(string password);

        /// <summary>
        /// Sets the sqlite database legacy formate
        /// </summary>
        /// <param name="legacyFormat"></param>
        /// <returns></returns>
        SqLiteDatabaseFApi LegacyFormat(bool legacyFormat);

        /// <summary>
        /// Sets the sqlite database pooling
        /// </summary>
        /// <param name="pooling"></param>
        /// <returns></returns>
        SqLiteDatabaseFApi Pooling(bool pooling);

        /// <summary>
        /// Sets the sqlite database max pool size
        /// </summary>
        /// <param name="maxPoolSize"></param>
        /// <returns></returns>
        SqLiteDatabaseFApi MaxPoolSize(int maxPoolSize);

        /// <summary>
        /// Sets the sqlite database as readonly
        /// </summary>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        SqLiteDatabaseFApi ReadOnly(bool readOnly);

        /// <summary>
        /// Sets the sqlite database time format
        /// </summary>
        /// <param name="dateTimeFormat"></param>
        /// <returns></returns>
        SqLiteDatabaseFApi DateTimeFormat(string dateTimeFormat);

        /// <summary>
        /// Sets the sqlite database cache size
        /// </summary>
        /// <param name="cacheSize"></param>
        /// <returns></returns>
        SqLiteDatabaseFApi CacheSize(int cacheSize);
    }
}

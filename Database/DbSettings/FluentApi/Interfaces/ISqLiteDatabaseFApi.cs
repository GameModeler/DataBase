
using DataBase.Database.DbSettings.DbClasses;

namespace DataBase.Database.DbSettings.FluentApi.Interfaces
{
    /// <summary>
    /// SqLite Fluent Interface
    /// </summary>
    public interface ISqLiteDatabaseFApi : IDbSettingsFApi
    {
        /// <summary>
        /// Sets the sqlite database name
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        ISqLiteDatabaseFApi DatabaseName(string databaseName);

        /// <summary>
        /// Sets the sqlite data source
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        ISqLiteDatabaseFApi DataSource(string dataSource);

        /// <summary>
        /// Sets the sqlite database version
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        ISqLiteDatabaseFApi Version(int version);

        /// <summary>
        /// Sets the sqlite database UTF16 encoding
        /// </summary>
        /// <param name="isNew"></param>
        /// <returns></returns>
        ISqLiteDatabaseFApi New(bool isNew);

        /// <summary>
        /// Sets the sqlite database UTF16 encoding
        /// </summary>
        /// <param name="useUTF16encoding"></param>
        /// <returns></returns>
        ISqLiteDatabaseFApi UseUTF16Encoding(bool useUTF16encoding);

        /// <summary>
        /// Sets the sqlite database port
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        ISqLiteDatabaseFApi Port(string port);

        /// <summary>
        /// Sets the sqlite database user password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        ISqLiteDatabaseFApi Password(string password);

        /// <summary>
        /// Sets the sqlite database legacy formate
        /// </summary>
        /// <param name="legacyFormat"></param>
        /// <returns></returns>
        ISqLiteDatabaseFApi LegacyFormat(bool legacyFormat);

        /// <summary>
        /// Sets the sqlite database pooling
        /// </summary>
        /// <param name="pooling"></param>
        /// <returns></returns>
        ISqLiteDatabaseFApi Pooling(bool pooling);

        /// <summary>
        /// Sets the sqlite database max pool size
        /// </summary>
        /// <param name="maxPoolSize"></param>
        /// <returns></returns>
        ISqLiteDatabaseFApi MaxPoolSize(int maxPoolSize);

        /// <summary>
        /// Sets the sqlite database as readonly
        /// </summary>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        ISqLiteDatabaseFApi ReadOnly(bool readOnly);

        /// <summary>
        /// Sets the sqlite database time format
        /// </summary>
        /// <param name="dateTimeFormat"></param>
        /// <returns></returns>
        ISqLiteDatabaseFApi DateTimeFormat(string dateTimeFormat);

        /// <summary>
        /// Sets the sqlite database cache size
        /// </summary>
        /// <param name="cacheSize"></param>
        /// <returns></returns>
        ISqLiteDatabaseFApi CacheSize(int cacheSize);

        /// <summary>
        /// Convert to SqLiteDatabase type
        /// </summary>
        /// <returns></returns>
        SqLiteDatabase ToSqLiteDatabase { get; }
    }
}

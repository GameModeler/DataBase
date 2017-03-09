using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Database.DbSettings.Interface
{
    /// <summary>
    /// SqLite database settings interface
    /// </summary>
    public interface ISqLiteDatabase
    {
        /// <summary>
        /// SqLite database name
        /// </summary>
        /// <returns></returns>
        string DatabaseName { get; set; }

        /// <summary>
        /// SqLite data source
        /// </summary>
        /// <returns></returns>
        string DataSource { get; set; }

        /// <summary>
        /// SqLite database version
        /// </summary>
        /// <returns></returns>
        int Version { get; set; }

        /// <summary>
        /// SqLite database UTF16 encoding option
        /// </summary>
        /// <returns></returns>
        bool New { get; set; }

        /// <summary>
        /// SqLite database UTF16 encoding
        /// </summary>
        /// <returns></returns>
        bool UseUTF16Encoding { get; set; }

        /// <summary>
        /// SqLite database port
        /// </summary>
        /// <returns></returns>
        string Port { get; set; }

        /// <summary>
        /// SqLite database user password
        /// </summary>
        /// <returns></returns>
        string Password { get; set; }

        /// <summary>
        /// SqLite database legacy formate option
        /// </summary>
        /// <returns></returns>
        bool LegacyFormat { get; set; }

        /// <summary>
        /// SqLite database pooling option
        /// </summary>
        /// <returns></returns>
        bool Pooling { get; set; }

        /// <summary>
        /// SqLite database max pool size
        /// </summary>
        /// <returns></returns>
        int MaxPoolSize { get; set; }

        /// <summary>
        /// SqLite database readonly option
        /// </summary>
        /// <returns></returns>
        bool ReadOnly { get; set; }

        /// <summary>
        /// SqLite database time format
        /// </summary>
        /// <returns></returns>
        string DateTimeFormat { get; set; }

        /// <summary>
        /// SqLite database cache size
        /// </summary>
        /// <returns></returns>
        int CacheSize { get; set; }
    }
}

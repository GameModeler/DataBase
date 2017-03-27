using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Database.DbSettings.Interface
{
    /// <summary>
    /// General database settings interface
    /// </summary>
    public interface IDbSettings
    {
        /// <summary>
        /// Name of the database
        /// </summary>
        string DatabaseName { get; set; }

        /// <summary>
        /// Database connection string
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Database provider
        /// </summary>
        ProviderType Provider { get; }

        /// <summary>
        /// Build the database connection string
        /// </summary>
        /// <returns></returns>
        string ToConnectionString();
    }
}

using DataBase.Database.DbSettings.Interfaces;
using DataBase.Database.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Database.Interfaces
{
    /// <summary>
    /// Database Manager Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDatabaseManager<T>
    {
        /// <summary>
        /// Provider
        /// </summary>
        ProviderType Provider { get; }

        /// <summary>
        /// Get the database from its name
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        T GetDatabase(String dbName);

        /// <summary>
        /// List of databases
        /// </summary>
        Dictionary<string, IDbSettings> Databases { get;  }
    }
}

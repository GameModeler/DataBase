using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.SQLite.EF6;

namespace DataBase.Database.DbContexts.Configurations
{
    /// <summary>
    /// SqLite Database configuration
    /// </summary>
    public class SqliteConfiguration : DbConfiguration
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SqliteConfiguration()
        {
            SetProviderServices("System.Data.SQLite.EF6", (DbProviderServices)SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices)));
        }
    }
}

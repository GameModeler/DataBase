using MySql.Data.Entity;
using MySql.Data.MySqlClient;
using System.Data.Entity;

namespace DataBase.Database.DbContexts.Configurations
{
    /// <summary>
    /// MySql Database configuration
    /// </summary>
    public class MySQLConfiguration : DbConfiguration
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MySQLConfiguration()
        {
            SetProviderServices(MySqlProviderInvariantName.ProviderName, new MySqlProviderServices());
        }
    }
}

using MySql.Data.Entity;
using MySql.Data.MySqlClient;
using System.Data.Entity;
using System.Data.Entity.Migrations.Sql;

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
            // Set the provider
            SetProviderServices(MySqlProviderInvariantName.ProviderName, new MySqlProviderServices());
        }
    }
}

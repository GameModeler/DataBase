using DataBase.Database.DbContexts.Configurations;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.DbSettings.Interfaces;
using DataBase.Database.Utils;
using MySql.Data.MySqlClient;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DataBase.Database.DbContexts
{
    /// <summary>
    /// MySql Context
    /// </summary>
    [DbConfigurationType(typeof(MySQLConfiguration))]
    public class MySqlContext : UniversalContext, IProvider
    {
        private ProviderType provider;
        /// <summary>
        /// The Provider
        /// </summary>
        public ProviderType Provider
        {
            get { return provider; }
            set { provider = ProviderType.MySQL; }
        }

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings"></param>
        public MySqlContext(IDbSettings settings) : base(new MySqlConnection(ConnectionStringBuilder.BuildConnectionString(ProviderType.MySQL, settings)), true)
        {
            dbSettings = settings;
            dbManager.ApplicationContexts.Add(this);
            Initialize();
        }

        #endregion
    }
}

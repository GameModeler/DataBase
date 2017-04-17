using DataBase.Database.DbContexts.Configurations;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.DbSettings.Interfaces;
using DataBase.Database.Utils;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        #endregion

        /// <summary>
        /// Method called during the creation of the model
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

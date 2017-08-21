// <copyright file="MySQLConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DataBase.Database.DbContexts.Configurations
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations.Sql;
    using MySql.Data.Entity;
    using MySql.Data.MySqlClient;

    /// <summary>
    /// MySql Database configuration
    /// </summary>
    public class MySQLConfiguration : DbConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MySQLConfiguration"/> class.
        /// Constructor
        /// </summary>
        public MySQLConfiguration()
        {
            // Set the provider
            this.SetProviderServices(MySqlProviderInvariantName.ProviderName, new MySqlProviderServices());
        }
    }
}

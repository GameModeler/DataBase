// <copyright file="MySqlContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DataBase.Database.DbContexts
{
    using System.Data.Entity;
    using DataBase.Database.DbContexts.Configurations;
    using DataBase.Database.DbSettings.Interfaces;
    using DataBase.Database.Utils;
    using Interfaces;
    using MySql.Data.MySqlClient;

    /// <summary>
    /// MySql Context
    /// </summary>
    [DbConfigurationType(typeof(MySQLConfiguration))]
    public class MySqlContext : UniversalContext, IProvider
    {
        private ProviderType provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="MySqlContext"/> class.
        /// </summary>
        /// <param name="settings">the database settings</param>
        public MySqlContext(IDbSettings settings)
            : base(new MySqlConnection(ConnectionStringBuilder.BuildConnectionString(ProviderType.MySQL, settings)), true)
        {
            this.dbSettings = settings;
            this.dbManager.ApplicationContexts.Add(this);
            this.Initialize();
        }

        /// <summary>
        /// Gets or sets the Provider
        /// </summary>
        public ProviderType Provider
        {
            get { return this.provider; }
            set { this.provider = ProviderType.MySQL; }
        }
    }
}

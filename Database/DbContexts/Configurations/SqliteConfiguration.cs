// <copyright file="SqliteConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DataBase.Database.DbContexts.Configurations
{
    using System.Data.Entity;
    using System.Data.Entity.Core.Common;
    using System.Data.SQLite.EF6;

    /// <summary>
    /// SqLite Database configuration
    /// </summary>
    public class SqliteConfiguration : DbConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqliteConfiguration"/> class.
        /// Constructor
        /// </summary>
        public SqliteConfiguration()
        {
            // Set the provider for sqlite databases
            this.SetProviderServices("System.Data.SQLite.EF6", (DbProviderServices)SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices)));
        }
    }
}

// <copyright file="IUniversalContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DataBase.Database.DbContexts.Interfaces
{
    using System;
    using System.Data.Entity;
    using DataBase.Database.DbSettings.Interfaces;
    using DataBase.Database.Repositories.Interfaces;
    using static DataBase.Database.Utils.GenericUtils;

    /// <summary>
    /// Universal context interface
    /// </summary>
    public interface IUniversalContext :IDisposable
    {

        /// <summary>
        /// Gets database settings
        /// </summary>
        IDbSettings DbSettings { get; }

        /// <summary>
        /// Gets list of DbSets
        /// </summary>
        GenericDictionary DbSets { get; }

        /// <summary>
        /// Gets or sets a value indicating whether enable or disable lazy loading mode
        /// </summary>
        bool EnableLazyLoading { get; set; }

        /// <summary>
        /// Gets all the entities
        /// </summary>
        GenericDictionary Entities { get;  }

        /// <summary>
        /// Gets the access to the Entity framework DbContext methods
        /// </summary>
        DbContext DbContext { get; }

        /// <summary>
        /// Get a repository from a given entity or create it if not exists
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <returns>IRepository<TEntity></returns>
        IRepository<TEntity> Entity<TEntity>()
            where TEntity : class;

        /// <summary>
        /// Manually initialize a context
        /// </summary>
        void Initialize();

    }
}

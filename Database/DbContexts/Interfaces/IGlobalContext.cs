// <copyright file="IGlobalContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DataBase.Database.DbContexts.Interfaces
{
    using System;
    using System.Collections.Generic;
    using DataBase.Database.DbSettings.Interfaces;
    using DataBase.Database.Repositories.Interfaces;

    /// <summary>
    /// Global Context Interface
    /// </summary>
    public interface IGlobalContext : IDisposable
    {
        /// <summary>
        /// Gets all the contexts of the global context
        /// </summary>
        Dictionary<IDbSettings, IUniversalContext> Contexts { get; }

        /// <summary>
        /// Create or Get the repository from the given entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>IGlobalRepository<TEntity></returns>
        IGlobalRepository<TEntity> Entity<TEntity>()
            where TEntity : class;

        /// <summary>
        /// Add a context to the global context
        /// </summary>
        /// <param name="context">The context to add</param>
        /// <returns>IGlobalContext</returns>
        IGlobalContext Add(IUniversalContext context);
    }
}

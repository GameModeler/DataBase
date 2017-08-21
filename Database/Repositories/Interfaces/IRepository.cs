// <copyright file="IRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DataBase.Database.Repositories.Interfaces
{
    using System.Data.Entity;
    using DataBase.Database.DbContexts.Interfaces;

    /// <summary>
    /// Repository interface
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IRepository<TEntity> : IAsyncCrudMethods<TEntity>, ISyncCrudMethods<TEntity> where TEntity : class

    {
        /// <summary>
        /// Gets context
        /// </summary>
        IUniversalContext Context { get;  }

        /// <summary>
        /// Gets dbSet
        /// </summary>
        /// <returns>DbSet</returns>
        DbSet<TEntity> DbSet { get; }

    }
}

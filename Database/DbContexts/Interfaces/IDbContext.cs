// <copyright file="IDbContext.cs" company="GameModeler">
// Copyright (c) GameModeler. All rights reserved.
// </copyright>

namespace DataBase.Database.DbContexts.Interfaces
{
    using Repositories;

    /// <summary>
    /// IDbContext interface
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// DbSet
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <returns>A repository of the given entity type</returns>
        Repository<TEntity> Entity<TEntity>()
            where TEntity : class;
    }
}

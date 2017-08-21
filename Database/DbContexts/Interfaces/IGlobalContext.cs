using DataBase.Database.DbSettings.Interfaces;
using DataBase.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace DataBase.Database.DbContexts.Interfaces
{
    /// <summary>
    /// Global Context Interface
    /// </summary>
    public interface IGlobalContext : IDisposable
    {
        /// <summary>
        /// All the contexts of the global context
        /// </summary>
        Dictionary<IDbSettings, IUniversalContext> Contexts { get; }

        /// <summary>
        /// Create or Get the repository from the given entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IGlobalRepository<TEntity> Entity<TEntity>() where TEntity : class;

        /// <summary>
        /// Add a context to the global context
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        IGlobalContext Add(IUniversalContext context);
    }
}

using DataBase.Database.DbContexts.Interfaces;
using System.Collections.Generic;

namespace DataBase.Database.Repositories.Interfaces
{
    /// <summary>
    /// Global Repository Interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IGlobalRepository<TEntity> : IGlobalAsyncCrudMethods<TEntity>, IGlobalSyncCrudMethods<TEntity> where TEntity : class
    {
        /// <summary>
        /// Context
        /// </summary>
        IGlobalContext Context { get; }

        /// <summary>
        /// Repositories
        /// </summary>
        Dictionary<IUniversalContext, IRepository<TEntity>> Repositories { get;  }
    }
}

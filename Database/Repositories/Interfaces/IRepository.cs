using DataBase.Database.DbContexts;
using DataBase.Database.DbContexts.Interfaces;
using System.Data.Entity;

namespace DataBase.Database.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface
    /// </summary>
    public interface IRepository<TEntity> : IAsyncCrudMethods<TEntity>, ISyncCrudMethods<TEntity> where TEntity : class

    {
        /// <summary>
        /// Context
        /// </summary>
        IUniversalContext Context { get;  }

        /// <summary>
        /// DbSet
        /// </summary>
        /// <returns></returns>
        DbSet<TEntity> DbSet { get; }

    }
}

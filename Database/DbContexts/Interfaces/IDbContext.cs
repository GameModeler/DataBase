using DataBase.Database.Repositories;

namespace DataBase.Database.DbContexts.Interfaces
{
    /// <summary>
    /// IDbContext interface
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// DbSet
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        Repository<TEntity> Entity<TEntity>() where TEntity : class;
    }
}

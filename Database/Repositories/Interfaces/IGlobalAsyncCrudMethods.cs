using DataBase.Database.Criterias;
using DataBase.Database.DbContexts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Database.Repositories.Interfaces
{
    /// <summary>
    /// Global Async Crud Methods Interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IGlobalAsyncCrudMethods<TEntity>
    {

        /// <summary>
        /// Inserts an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<Dictionary<IUniversalContext, int>> InsertAsync(TEntity item);

        /// <summary>
        /// Inserts entities
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<Dictionary<IUniversalContext, int>> InsertAsync(IEnumerable<TEntity> items);

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<Dictionary<IUniversalContext, int>> UpdateAsync(TEntity item);

        /// <summary>
        /// Updates entities
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<Dictionary<IUniversalContext, int>> UpdateAsync(IEnumerable<TEntity> items);

        /// <summary>
        /// Gets an entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Dictionary<IUniversalContext, TEntity>> GetAsync(Int32 id);

        /// <summary>
        /// Gets entities
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<IUniversalContext, IEnumerable<TEntity>>> GetAsync();

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<Dictionary<IUniversalContext, Int32>> DeleteAsync(TEntity item);

        /// <summary>
        /// Deletes entities
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<Dictionary<IUniversalContext, Int32>> DeleteAsync(IEnumerable<TEntity> items);

        /// <summary>
        /// Allows to execute a custom query
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<Dictionary<IUniversalContext, IEnumerable<TEntity>>> CustomQueryAsync(Criteria criteria);
    }
}

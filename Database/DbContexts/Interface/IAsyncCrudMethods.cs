using DataBase.Criteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Database.DbContexts.Interface
{
    /// <summary>
    /// Asynchrone CRUD methods
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IAsyncCrudMethods<TEntity>
    {
        /// <summary>
        /// Inserts an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<int> InsertAsync(TEntity item);

        /// <summary>
        /// Inserts entities
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<int> InsertAsync(IEnumerable<TEntity> items);

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(TEntity item);

        /// <summary>
        /// Updates entities
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(IEnumerable<TEntity> items);

        /// <summary>
        /// Gets an entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Int32 id);

        /// <summary>
        /// Gets entities
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAsync();

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<Int32> DeleteAsync(TEntity item);

        /// <summary>
        /// Deletes entities
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<Int32> DeleteAsync(IEnumerable<TEntity> items);

        /// <summary>
        /// Allows to execute a custom query
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> CustomQueryAsync(Criteria.Criteria criteria);
    }
}

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
        Task<int> Insert(TEntity item);

        /// <summary>
        /// Inserts entities
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<int> Insert(IEnumerable<TEntity> items);

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<int> Update(TEntity item);

        /// <summary>
        /// Updates entities
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<int> Update(IEnumerable<TEntity> items);

        /// <summary>
        /// Gets an entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> Get(Int32 id);

        /// <summary>
        /// Gets entities
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> Get();

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<Int32> Delete(TEntity item);

        /// <summary>
        /// Deletes entities
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<Int32> Delete(IEnumerable<TEntity> items);

        /// <summary>
        /// Allows to execute a custom query
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> CustomQuery(Criteria.Criteria criteria);
    }
}

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
    /// Global Sync Crud Methods Interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IGlobalSyncCrudMethods<TEntity>
    {
        /// <summary>
        /// Inserts an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Dictionary<IUniversalContext, int> Insert(TEntity item);

        /// <summary>
        /// Inserts entities
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Dictionary<IUniversalContext, int> Insert(IEnumerable<TEntity> items);

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Dictionary<IUniversalContext, int> Update(TEntity item);

        /// <summary>
        /// Updates entities
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Dictionary<IUniversalContext, int> Update(IEnumerable<TEntity> items);

        /// <summary>
        /// Gets an entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Dictionary<IUniversalContext, TEntity> Get(Int32 id);

        /// <summary>
        /// Gets entities
        /// </summary>
        /// <returns></returns>
        Dictionary<IUniversalContext, IEnumerable<TEntity>> Get();

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Dictionary<IUniversalContext, Int32> Delete(TEntity item);

        /// <summary>
        /// Deletes entities
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Dictionary<IUniversalContext, Int32> Delete(IEnumerable<TEntity> items);

        /// <summary>
        /// Allows to execute a custom query
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Dictionary<IUniversalContext, IEnumerable<TEntity>> CustomQuery(Criteria criteria);
    }
}

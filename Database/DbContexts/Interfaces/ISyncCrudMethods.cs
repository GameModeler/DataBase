using DataBase.Database.Criterias;
using System;
using System.Collections.Generic;

namespace DataBase.Database.DbContexts.Interfaces
{

    /// <summary>
    /// Synchrone CRUD methods
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ISyncCrudMethods<TEntity>
    {

        /// <summary>
        /// Inserts an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        int Insert(TEntity item);
 
        /// <summary>
        /// Inserts entities
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        int Insert(IEnumerable<TEntity> items);

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        int Update(TEntity item);

        /// <summary>
        /// Updates entities
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        int Update(IEnumerable<TEntity> items);

        /// <summary>
        /// Gets an entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(Int32 id);

        /// <summary>
        /// Gets entities
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> Get();

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Int32 Delete(TEntity item);

        /// <summary>
        /// Deletes entities
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Int32 Delete(IEnumerable<TEntity> items);

        /// <summary>
        /// Allows to execute a custom query
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IEnumerable<TEntity> CustomQuery(Criteria criteria);
    }
}

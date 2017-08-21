// <copyright file="IGlobalAsyncCrudMethods.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DataBase.Database.Repositories.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DataBase.Database.Criterias;
    using DataBase.Database.DbContexts.Interfaces;

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
        Task<Dictionary<IUniversalContext, TEntity>> GetAsync(int id);

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
        Task<Dictionary<IUniversalContext, int>> DeleteAsync(TEntity item);

        /// <summary>
        /// Deletes entities
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<Dictionary<IUniversalContext, int>> DeleteAsync(IEnumerable<TEntity> items);

        /// <summary>
        /// Allows to execute a custom query
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<Dictionary<IUniversalContext, IEnumerable<TEntity>>> CustomQueryAsync(Criteria criteria);
    }
}

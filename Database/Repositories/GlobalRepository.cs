// <copyright file="GlobalRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DataBase.Database.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataBase.Database.Criterias;
    using DataBase.Database.DbContexts;
    using DataBase.Database.DbContexts.Interfaces;
    using DataBase.Database.Repositories.Interfaces;

    /// <summary>
    /// Global Repository
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class GlobalRepository<TEntity> : IGlobalRepository<TEntity>
        where TEntity : class
    {
        private GlobalContext context;

        private Dictionary<IUniversalContext, IRepository<TEntity>> repositories;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context</param>
        public GlobalRepository(GlobalContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<IUniversalContext, IRepository<TEntity>>();

            this.InitReposFromContexts();
        }

        /// <summary>
        /// Gets context
        /// </summary>
        public IGlobalContext Context
        {
            get { return this.context; }
        }

        /// <summary>
        /// Gets all the repositories
        /// </summary>
        public Dictionary<IUniversalContext, IRepository<TEntity>> Repositories
        {
            get { return this.repositories; }
        }

        /// <summary>
        /// Insert item Asynchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Dictionary<IUniversalContext, int>> InsertAsync(TEntity item)
        {
            Dictionary<IUniversalContext, int> result = new Dictionary<IUniversalContext, int>();

            foreach (var context in this.context.ContextList)
            {
                var res = await this.repositories[context].InsertAsync(item);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Insert item Synchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Dictionary<IUniversalContext, int> Insert(TEntity item)
        {
            Dictionary<IUniversalContext, int> result = new Dictionary<IUniversalContext, int>();

            foreach (var context in this.context.ContextList)
            {
                var res = this.repositories[context].Insert(item);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Insert items Asynchrone method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<Dictionary<IUniversalContext, int>> InsertAsync(IEnumerable<TEntity> items)
        {
            Dictionary<IUniversalContext, int> result = new Dictionary<IUniversalContext, int>();

            foreach (var context in this.context.ContextList)
            {
                var res = await this.repositories[context].InsertAsync(items);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Insert items Synchrone method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Dictionary<IUniversalContext, int> Insert(IEnumerable<TEntity> items)
        {
            Dictionary<IUniversalContext, int> result = new Dictionary<IUniversalContext, int>();

            foreach (var context in this.context.ContextList)
            {
                var res = this.repositories[context].Insert(items);
                result.Add(context, res);
            }
            return result;
        }

        /// <summary>
        /// Update item Asynchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Dictionary<IUniversalContext, int>> UpdateAsync(TEntity item)
        {
            Dictionary<IUniversalContext, int> result = new Dictionary<IUniversalContext, int>();

            foreach (var context in this.context.ContextList)
            {
                var res = await this.repositories[context].UpdateAsync(item);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Update item Synchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Dictionary<IUniversalContext, int> Update(TEntity item)
        {
            Dictionary<IUniversalContext, int> result = new Dictionary<IUniversalContext, int>();

            foreach (var context in this.context.ContextList)
            {
                var res = this.repositories[context].Update(item);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Update items Asynchrone method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<Dictionary<IUniversalContext, int>> UpdateAsync(IEnumerable<TEntity> items)
        {
            Dictionary<IUniversalContext, int> result = new Dictionary<IUniversalContext, int>();

            foreach (var context in this.context.ContextList)
            {
                var res = await this.repositories[context].UpdateAsync(items);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Update items Synchrone method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Dictionary<IUniversalContext, int> Update(IEnumerable<TEntity> items)
        {
            Dictionary<IUniversalContext, int> result = new Dictionary<IUniversalContext, int>();

            foreach (var context in this.context.ContextList)
            {
                var res = this.repositories[context].Update(items);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Get item Asynchrone method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Dictionary<IUniversalContext, TEntity>> GetAsync(int id)
        {
            Dictionary<IUniversalContext, TEntity> result = new Dictionary<IUniversalContext, TEntity>();

            foreach (var context in this.context.ContextList)
            {
                var res = await this.repositories[context].GetAsync(id);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Get item Synchrone method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<IUniversalContext, TEntity> Get(int id)
        {
            Dictionary<IUniversalContext, TEntity> result = new Dictionary<IUniversalContext, TEntity>();

            foreach (var context in this.context.ContextList)
            {
                var res = this.repositories[context].Get(id);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Get all items Asynchrone method
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<IUniversalContext, IEnumerable<TEntity>>> GetAsync()
        {
            Dictionary<IUniversalContext, IEnumerable<TEntity>> result = new Dictionary<IUniversalContext, IEnumerable<TEntity>>();

            foreach (var context in this.context.ContextList)
            {
                var res = await this.repositories[context].GetAsync();
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Get all items Synchrone method
        /// </summary>
        /// <returns></returns>
        public Dictionary<IUniversalContext, IEnumerable<TEntity>> Get()
        {
            Dictionary<IUniversalContext, IEnumerable<TEntity>> result = new Dictionary<IUniversalContext, IEnumerable<TEntity>>();

            foreach (var context in this.context.ContextList)
            {
                var res = this.repositories[context].Get();
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Delete item Asynchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Dictionary<IUniversalContext, int>> DeleteAsync(TEntity item)
        {
            Dictionary<IUniversalContext, int> result = new Dictionary<IUniversalContext, Int32>();

            foreach (var context in this.context.ContextList)
            {
                var res = await this.repositories[context].DeleteAsync(item);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Delete item Synchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Dictionary<IUniversalContext, int> Delete(TEntity item)
        {
            Dictionary<IUniversalContext, int> result = new Dictionary<IUniversalContext, Int32>();

            foreach (var context in this.context.ContextList)
            {
                var res = this.repositories[context].Delete(item);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Delete items Asynchrone method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<Dictionary<IUniversalContext, int>> DeleteAsync(IEnumerable<TEntity> items)
        {
            Dictionary<IUniversalContext, int> result = new Dictionary<IUniversalContext, Int32>();

            foreach (var context in this.context.ContextList)
            {
                var res = await this.repositories[context].DeleteAsync(items);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Delete items Synchrone method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Dictionary<IUniversalContext, int> Delete(IEnumerable<TEntity> items)
        {
            Dictionary<IUniversalContext, int> result = new Dictionary<IUniversalContext, Int32>();

            foreach (var context in this.context.ContextList)
            {
                var res = this.repositories[context].Delete(items);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Custom Query Asynchrone method
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public async Task<Dictionary<IUniversalContext, IEnumerable<TEntity>>> CustomQueryAsync(Criteria criteria)
        {
            Dictionary<IUniversalContext, IEnumerable<TEntity>> result = new Dictionary<IUniversalContext, IEnumerable<TEntity>>();

            foreach (var context in this.context.ContextList)
            {
                var res = await this.repositories[context].CustomQueryAsync(criteria);              
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Custom Query Synchrone method
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public Dictionary<IUniversalContext, IEnumerable<TEntity>> CustomQuery(Criteria criteria)
        {
            Dictionary<IUniversalContext, IEnumerable<TEntity>> result = new Dictionary<IUniversalContext, IEnumerable<TEntity>>();

            foreach (var context in this.context.ContextList)
            {
                var res = this.repositories[context].CustomQuery(criteria);
                result.Add(context, res);
            }

            return result;
        }

        private void InitReposFromContexts()
        {
            foreach (IUniversalContext ctx in this.context.ContextList)
            {
                var repo = ctx.Entity<TEntity>();
                this.repositories.Add(ctx, repo);
            }
        }
    }
}

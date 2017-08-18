using DataBase.Database.Criterias;
using DataBase.Database.DbContexts;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBase.Database.Repositories
{
    /// <summary>
    /// Global Repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GlobalRepository<TEntity> : IGlobalRepository<TEntity> where TEntity : class
    {
        private GlobalContext context;

        private Dictionary<IUniversalContext, IRepository<TEntity>> repositories;

        /// <summary>
        /// Context
        /// </summary>
        public IGlobalContext Context
        {
            get { return context; }
        }

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public GlobalRepository(GlobalContext context)
        {
            this.context = context;
            repositories = new Dictionary<IUniversalContext, IRepository<TEntity>>();

            InitReposFromContexts();
        }

        private void InitReposFromContexts()
        {
            foreach (IUniversalContext ctx in context.ContextList)
            {
                var repo = ctx.Entity<TEntity>();
                repositories.Add(ctx, repo);
            }
        }

        /// <summary>
        /// Get All the repositories
        /// </summary>
        public Dictionary<IUniversalContext, IRepository<TEntity>> Repositories
        {
            get { return repositories; }
        }

        #endregion

        #region SQL methods
        /// <summary>
        /// Insert item Asynchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Dictionary<IUniversalContext, int>> InsertAsync(TEntity item)
        {
            Dictionary<IUniversalContext, int> result = new Dictionary<IUniversalContext, int>();

            foreach(var context in context.ContextList)
            {
                var res = await repositories[context].InsertAsync(item);           
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

            foreach (var context in context.ContextList)
            {
                var res = repositories[context].Insert(item);
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

            foreach (var context in context.ContextList)
            {
                var res = await repositories[context].InsertAsync(items);           
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

            foreach (var context in context.ContextList)
            {
                var res = repositories[context].Insert(items);
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

            foreach (var context in context.ContextList)
            {
                var res = await repositories[context].UpdateAsync(item);
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

            foreach (var context in context.ContextList)
            {
                var res = repositories[context].Update(item);               
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

            foreach (var context in context.ContextList)
            {
                var res = await repositories[context].UpdateAsync(items);
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

            foreach (var context in context.ContextList)
            {
                var res = repositories[context].Update(items);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Get item Asynchrone method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Dictionary<IUniversalContext, TEntity>> GetAsync(Int32 id)
        {
            Dictionary<IUniversalContext, TEntity> result = new Dictionary<IUniversalContext, TEntity>();

            foreach (var context in context.ContextList)
            {
                var res = await repositories[context].GetAsync(id);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Get item Synchrone method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<IUniversalContext, TEntity> Get(Int32 id)
        {
            Dictionary<IUniversalContext, TEntity> result = new Dictionary<IUniversalContext, TEntity>();

            foreach (var context in context.ContextList)
            {
                var res = repositories[context].Get(id);
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

            foreach (var context in context.ContextList)
            {
                var res = await repositories[context].GetAsync();
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

            foreach (var context in context.ContextList)
            {
                var res = repositories[context].Get();             
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Delete item Asynchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Dictionary<IUniversalContext, Int32>> DeleteAsync(TEntity item)
        {
            Dictionary<IUniversalContext, Int32> result = new Dictionary<IUniversalContext, Int32>();

            foreach (var context in context.ContextList)
            {
                var res = await repositories[context].DeleteAsync(item);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Delete item Synchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Dictionary<IUniversalContext, Int32> Delete(TEntity item)
        {
            Dictionary<IUniversalContext, Int32> result = new Dictionary<IUniversalContext, Int32>();

            foreach (var context in context.ContextList)
            {
                var res = repositories[context].Delete(item);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Delete items Asynchrone method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<Dictionary<IUniversalContext, Int32>> DeleteAsync(IEnumerable<TEntity> items)
        {
            Dictionary<IUniversalContext, Int32> result = new Dictionary<IUniversalContext, Int32>();

            foreach (var context in context.ContextList)
            {
                var res = await repositories[context].DeleteAsync(items);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Delete items Synchrone method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Dictionary<IUniversalContext, Int32> Delete(IEnumerable<TEntity> items)
        {
            Dictionary<IUniversalContext, Int32> result = new Dictionary<IUniversalContext, Int32>();

            foreach (var context in context.ContextList)
            {
                var res = repositories[context].Delete(items);               
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

            foreach (var context in context.ContextList)
            {
                var res = await repositories[context].CustomQueryAsync(criteria);              
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

            foreach (var context in context.ContextList)
            {
                var res = repositories[context].CustomQuery(criteria);
                result.Add(context, res);
            }
            return result;
        }
        #endregion
    }
}

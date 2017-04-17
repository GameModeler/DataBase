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
    public class GlobalRepository<TEntity> : IRepository where TEntity : class
    {
        private GlobalContext context;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public GlobalRepository(GlobalContext context)
        {
            this.context = context;
        }
        #endregion



        #region SQL methods
        /// <summary>
        /// Insert item Asynchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Dictionary<IDbContext, int>> InsertAsync(TEntity item)
        {
            Dictionary<IDbContext, int> result = new Dictionary<IDbContext, int>();

            foreach(var context in context.Contexts)
            {
                var res = await context.Entity<TEntity>().InsertAsync(item);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Insert item Synchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Dictionary<IDbContext, int> Insert(TEntity item)
        {
            Dictionary<IDbContext, int> result = new Dictionary<IDbContext, int>();

            foreach (var context in context.Contexts)
            {
                var res = context.Entity<TEntity>().Insert(item);
                result.Add(context, res);
            }
            return result;
        }

        /// <summary>
        /// Insert items Asynchrone method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<Dictionary<IDbContext, int>> InsertAsync(IEnumerable<TEntity> items)
        {
            Dictionary<IDbContext, int> result = new Dictionary<IDbContext, int>();

            foreach (var context in context.Contexts)
            {
                var res = await context.Entity<TEntity>().InsertAsync(items);
                result.Add(context, res);
            }
          
            return result;
        }

        /// <summary>
        /// Insert items Synchrone method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Dictionary<IDbContext, int> Insert(IEnumerable<TEntity> items)
        {
            Dictionary<IDbContext, int> result = new Dictionary<IDbContext, int>();

            foreach (var context in context.Contexts)
            {
                var res = context.Entity<TEntity>().Insert(items);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Update item Asynchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Dictionary<IDbContext, int>> UpdateAsync(TEntity item)
        {
            Dictionary<IDbContext, int> result = new Dictionary<IDbContext, int>();

            foreach (var context in context.Contexts)
            {
                var res = await context.Entity<TEntity>().UpdateAsync(item);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Update item Synchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Dictionary<IDbContext, int> Update(TEntity item)
        {
            Dictionary<IDbContext, int> result = new Dictionary<IDbContext, int>();

            foreach (var context in context.Contexts)
            {
                var res = context.Entity<TEntity>().Update(item);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Update items Asynchrone method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<Dictionary<IDbContext, int>> UpdateAsync(IEnumerable<TEntity> items)
        {
            Dictionary<IDbContext, int> result = new Dictionary<IDbContext, int>();

            foreach (var context in context.Contexts)
            {
                var res = await context.Entity<TEntity>().UpdateAsync(items);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Update items Synchrone method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Dictionary<IDbContext, int> Update(IEnumerable<TEntity> items)
        {
            Dictionary<IDbContext, int> result = new Dictionary<IDbContext, int>();

            foreach (var context in context.Contexts)
            {
                var res = context.Entity<TEntity>().Update(items);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Get item Asynchrone method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Dictionary<IDbContext, TEntity>> GetAsync(Int32 id)
        {
            Dictionary<IDbContext, TEntity> result = new Dictionary<IDbContext, TEntity>();

            foreach (var context in context.Contexts)
            {
                var res = await context.Entity<TEntity>().GetAsync(id);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Get item Synchrone method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<IDbContext, TEntity> Get(Int32 id)
        {
            Dictionary<IDbContext, TEntity> result = new Dictionary<IDbContext, TEntity>();

            foreach (var context in context.Contexts)
            {
                var res = context.Entity<TEntity>().Get(id);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Get all items Asynchrone method
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<IDbContext, IEnumerable<TEntity>>> GetAsync()
        {
            Dictionary<IDbContext, IEnumerable<TEntity>> result = new Dictionary<IDbContext, IEnumerable<TEntity>>();

            foreach (var context in context.Contexts)
            {
                var res = await context.Entity<TEntity>().GetAsync();
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Get all items Synchrone method
        /// </summary>
        /// <returns></returns>
        public Dictionary<IDbContext, IEnumerable<TEntity>> Get()
        {
            Dictionary<IDbContext, IEnumerable<TEntity>> result = new Dictionary<IDbContext, IEnumerable<TEntity>>();

            foreach (var context in context.Contexts)
            {
                var res = context.Entity<TEntity>().Get();
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Delete item Asynchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Dictionary<IDbContext, Int32>> DeleteAsync(TEntity item)
        {
            Dictionary<IDbContext, Int32> result = new Dictionary<IDbContext, Int32>();

            foreach (var context in context.Contexts)
            {
                var res = await context.Entity<TEntity>().DeleteAsync(item);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Delete item Synchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Dictionary<IDbContext, Int32> Delete(TEntity item)
        {
            Dictionary<IDbContext, Int32> result = new Dictionary<IDbContext, Int32>();

            foreach (var context in context.Contexts)
            {
                var res = context.Entity<TEntity>().Delete(item);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Delete items Asynchrone method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<Dictionary<IDbContext, Int32>> DeleteAsync(IEnumerable<TEntity> items)
        {
            Dictionary<IDbContext, Int32> result = new Dictionary<IDbContext, Int32>();

            foreach (var context in context.Contexts)
            {
                var res = await context.Entity<TEntity>().DeleteAsync(items);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Delete items Synchrone method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Dictionary<IDbContext, Int32> Delete(IEnumerable<TEntity> items)
        {
            Dictionary<IDbContext, Int32> result = new Dictionary<IDbContext, Int32>();

            foreach (var context in context.Contexts)
            {
                var res = context.Entity<TEntity>().Delete(items);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Custom Query Asynchrone method
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public async Task<Dictionary<IDbContext, IEnumerable<TEntity>>> CustomQueryAsync(Criteria criteria)
        {
            Dictionary<IDbContext, IEnumerable<TEntity>> result = new Dictionary<IDbContext, IEnumerable<TEntity>>();

            foreach (var context in context.Contexts)
            {
                var res = await context.Entity<TEntity>().CustomQueryAsync(criteria);
                result.Add(context, res);
            }

            return result;
        }

        /// <summary>
        /// Custom Query Synchrone method
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public Dictionary<IDbContext, IEnumerable<TEntity>> CustomQuery(Criteria criteria)
        {
            Dictionary<IDbContext, IEnumerable<TEntity>> result = new Dictionary<IDbContext, IEnumerable<TEntity>>();

            foreach (var context in context.Contexts)
            {
                var res = context.Entity<TEntity>().CustomQuery(criteria);
                result.Add(context, res);
            }

            return result;
        }
        #endregion
    }
}

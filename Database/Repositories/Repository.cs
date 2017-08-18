using DataBase.Database.Criterias;
using DataBase.Database.DbContexts;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.Repositories;
using DataBase.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace DataBase.Database.Repositories
{
    /// <summary>
    /// Repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        /// <summary>
        /// Context
        /// </summary>
        private UniversalContext context;

        /// <summary>
        /// Context
        /// </summary>
        public IUniversalContext Context
        {
            get { return context; }
        }


        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public Repository(UniversalContext context)
        {
            this.context = context;
        }
        #endregion

        /// <summary>
        /// Create DbSet if not exists yet
        /// </summary>
        public DbSet<TEntity> DbSet
        {
            get
            {
                return context.Set<TEntity>();
            }
        }

        #region SQL asynchrone CRUD methods
        /// <summary>
        /// Inserts an entity asynchronously
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync(TEntity item)
        {
            DbSet<TEntity> localDbSet = DbSet;
            waitForDbSetLocal(localDbSet);
            localDbSet.Add(item);
            return await context.SaveChangesAsync();
        }

        /// <summary>
        /// Inserts entities asynchronously
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync(IEnumerable<TEntity> items)
        {

            DbSet<TEntity> localDbSet = DbSet;
            waitForDbSetLocal(localDbSet);

            foreach (var item in items)
            {
                localDbSet.Add(item);
            }
            return await context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an entity asynchronously
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(TEntity item)
        {
            await Task.Factory.StartNew(() =>
            {
                context.Entry<TEntity>(item).State = EntityState.Modified;
            });
            return await context.SaveChangesAsync();
        }


        /// <summary>
        /// Updates entities asynchronously
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(IEnumerable<TEntity> items)
        {
            await Task.Factory.StartNew(() =>
            {
                foreach (var item in items)
                {
                    context.Entry<TEntity>(item).State = EntityState.Modified;
                }
            });
            return await context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets an entity asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> GetAsync(Int32 id)
        {
            return await this.DbSet.FindAsync(id) as TEntity;
        }

        /// <summary>
        /// Gets entities asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            DbSet<TEntity> temp = default(DbSet<TEntity>);
            List<TEntity> result = new List<TEntity>();
            await Task.Factory.StartNew(() =>
            {
                temp = context.Set<TEntity>();
            });
            result.AddRange(temp);
            return result;
        }

        /// <summary>
        /// Deletes an entity asynchronously
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Int32> DeleteAsync(TEntity item)
        {
            await Task.Factory.StartNew(() =>
            {
                DbSet.Attach(item);
                DbSet.Remove(item);
            });
            return await context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes entities asynchronously
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<Int32> DeleteAsync(IEnumerable<TEntity> items)
        {
            await Task.Factory.StartNew(() =>
            {
                DbSet.Attach((items as List<TEntity>)[0]);
                DbSet.RemoveRange(items);
            });
            var res = await context.SaveChangesAsync();
            return res;
        }

        /// <summary>
        /// Allows to execute a custom query asynchronously
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> CustomQueryAsync(Criteria criteria)
        {
            DbSet<TEntity> dbset = DbSet;
            waitForDbSetLocal(dbset);

            return await dbset.SqlQuery(criteria.Compute()).ToListAsync();
        }
        #endregion

        #region SQL synchrone Crud methods

        /// <summary>
        /// Inserts an entity synchronously
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Insert(TEntity item)
        {

            DbSet<TEntity> localDbSet = DbSet;
            waitForDbSetLocal(localDbSet);
            localDbSet.Add(item);
            return context.SaveChanges();
        }

        /// <summary>
        /// Inserts entities asynchronously
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public int Insert(IEnumerable<TEntity> items)
        {

            DbSet<TEntity> localDbSet = DbSet;

            waitForDbSetLocal(localDbSet);

            foreach (var item in items)
            {
                localDbSet.Add(item);
            }
            return context.SaveChanges();
        }

        /// <summary>
        /// Updates an entity synchronously
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Update(TEntity item)
        {
            context.Entry<TEntity>(item).State = EntityState.Modified;
            return context.SaveChanges();
        }

        /// <summary>
        /// Updates entities synchronously
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public int Update(IEnumerable<TEntity> items)
        {
            foreach (var item in items)
            {
                context.Entry<TEntity>(item).State = EntityState.Modified;
            }
            return context.SaveChanges();
        }

        /// <summary>
        /// Gets an entity synchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Get(int id)
        {
            return DbSet.Find(id) as TEntity;
        }

        /// <summary>
        /// Gets all entities synchronously
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> Get()
        {
            DbSet<TEntity> temp = default(DbSet<TEntity>);
            List<TEntity> result = new List<TEntity>();
            temp = context.Set<TEntity>();
            result.AddRange(temp);
            return result;
        }

        /// <summary>
        /// Delete an entity synchronously
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Int32 Delete(TEntity item)
        {
            DbSet.Attach(item);
            DbSet.Remove(item);

            return context.SaveChanges();
        }

        /// <summary>
        /// Delete entities 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Int32 Delete(IEnumerable<TEntity> items)
        {
            DbSet.Attach((items as List<TEntity>)[0]);
            DbSet.RemoveRange(items);
            return context.SaveChanges();
        }

        /// <summary>
        /// Allows to execute a custom query synchronously
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> CustomQuery(Criteria criteria)
        {
            DbSet<TEntity> dbset = DbSet;
            waitForDbSetLocal(dbset);
            return dbset.SqlQuery(criteria.Compute()).ToList();
        }
        #endregion

        /// <summary>
        /// Workaround to wait for DbSet.Local to be defined
        /// </summary>
        /// <returns></returns>
        private void waitForDbSetLocal(DbSet<TEntity> dbset)
        {
            try
            {
                var localType = dbset.Local;
            }
            catch (Exception e)
            {
                var ex = e;
                Task task = waitLocal();

                try
                {
                    task.Wait();
                }
                catch (AggregateException ae)
                {
                    throw ae.Flatten();
                }
            }
        }

        private async Task<ObservableCollection<TEntity>> waitLocal()
        {
            ObservableCollection<TEntity> result = null;
            await Task.Factory.StartNew(() =>
            {
                result = DbSet.Local;

            });

            return result;
        }

    }
}

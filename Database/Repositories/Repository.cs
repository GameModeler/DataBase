// <copyright file="Repository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DataBase.Database.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Criterias;
    using DbContexts;
    using DbContexts.Interfaces;
    using Interfaces;

    /// <summary>
    /// Repository
    /// </summary>
    /// <typeparam name="TEntity">The entity</typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Context
        /// </summary>
        private UniversalContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context</param>
        public Repository(UniversalContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets context
        /// </summary>
        public IUniversalContext Context
        {
            get { return this.context; }
        }

        /// <summary>
        /// Gets create DbSet if not exists yet
        /// </summary>
        public DbSet<TEntity> DbSet
        {
            get
            {
                return this.context.Set<TEntity>();
            }
        }

        /// <summary>
        /// Inserts an entity asynchronously
        /// </summary>
        /// <param name="item">The item to insert</param>
        /// <returns>Task</returns>
        public async Task<int> InsertAsync(TEntity item)
        {
            DbSet<TEntity> localDbSet = this.DbSet;
            this.WaitForDbSetLocal(localDbSet);
            localDbSet.Add(item);
            return await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Inserts entities asynchronously
        /// </summary>
        /// <param name="items">The items to insert</param>
        /// <returns>Task</returns>
        public async Task<int> InsertAsync(IEnumerable<TEntity> items)
        {
            DbSet<TEntity> localDbSet = DbSet;
            this.WaitForDbSetLocal(localDbSet);

            foreach (var item in items)
            {
                localDbSet.Add(item);
            }

            return await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an entity asynchronously
        /// </summary>
        /// <param name="item">The item to update</param>
        /// <returns>Task</returns>
        public async Task<int> UpdateAsync(TEntity item)
        {
            await Task.Factory.StartNew(() =>
            {
                this.context.Entry<TEntity>(item).State = EntityState.Modified;
            });
            return await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates entities asynchronously
        /// </summary>
        /// <param name="items">The items to update</param>
        /// <returns>Task</returns>
        public async Task<int> UpdateAsync(IEnumerable<TEntity> items)
        {
            await Task.Factory.StartNew(() =>
            {
                foreach (var item in items)
                {
                    this.context.Entry<TEntity>(item).State = EntityState.Modified;
                }
            });
            return await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets an entity asynchronously
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Task</returns>
        public async Task<TEntity> GetAsync(int id)
        {
            return await this.DbSet.FindAsync(id) as TEntity;
        }

        /// <summary>
        /// Gets entities asynchronously
        /// </summary>
        /// <returns>Task</returns>
        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            DbSet<TEntity> temp = default(DbSet<TEntity>);
            List<TEntity> result = new List<TEntity>();
            await Task.Factory.StartNew(() =>
            {
                temp = this.context.Set<TEntity>();
            });
            result.AddRange(temp);
            return result;
        }

        /// <summary>
        /// Deletes an entity asynchronously
        /// </summary>
        /// <param name="item">The item to delete</param>
        /// <returns>Task</returns>
        public async Task<int> DeleteAsync(TEntity item)
        {
            await Task.Factory.StartNew(() =>
            {
                this.DbSet.Attach(item);
                this.DbSet.Remove(item);
            });
            return await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes entities asynchronously
        /// </summary>
        /// <param name="items">the items to delete</param>
        /// <returns>Task</returns>
        public async Task<int> DeleteAsync(IEnumerable<TEntity> items)
        {
            await Task.Factory.StartNew(() =>
            {
                this.DbSet.Attach((items as List<TEntity>)[0]);
                this.DbSet.RemoveRange(items);
            });
            var res = await this.context.SaveChangesAsync();
            return res;
        }

        /// <summary>
        /// Allows to execute a custom query asynchronously
        /// </summary>
        /// <param name="criteria">Criteria</param>
        /// <returns>Task</returns>
        public async Task<IEnumerable<TEntity>> CustomQueryAsync(Criteria criteria)
        {
            DbSet<TEntity> dbset = DbSet;
            this.WaitForDbSetLocal(dbset);

            return await dbset.SqlQuery(criteria.Compute()).ToListAsync();
        }


        /// <summary>
        /// Inserts an entity synchronously
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Insert(TEntity item)
        {

            DbSet<TEntity> localDbSet = this.DbSet;
            this.WaitForDbSetLocal(localDbSet);
            localDbSet.Add(item);
            return this.context.SaveChanges();
        }

        /// <summary>
        /// Inserts entities asynchronously
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public int Insert(IEnumerable<TEntity> items)
        {

            DbSet<TEntity> localDbSet = this.DbSet;

            this.WaitForDbSetLocal(localDbSet);

            foreach (var item in items)
            {
                localDbSet.Add(item);
            }

            return this.context.SaveChanges();
        }

        /// <summary>
        /// Updates an entity synchronously
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Update(TEntity item)
        {
            this.context.Entry<TEntity>(item).State = EntityState.Modified;
            return this.context.SaveChanges();
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
                this.context.Entry<TEntity>(item).State = EntityState.Modified;
            }
            return this.context.SaveChanges();
        }

        /// <summary>
        /// Gets an entity synchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Get(int id)
        {
            return this.DbSet.Find(id) as TEntity;
        }

        /// <summary>
        /// Gets all entities synchronously
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> Get()
        {
            DbSet<TEntity> temp = default(DbSet<TEntity>);
            List<TEntity> result = new List<TEntity>();
            temp = this.context.Set<TEntity>();
            result.AddRange(temp);
            return result;
        }

        /// <summary>
        /// Delete an entity synchronously
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Delete(TEntity item)
        {
            this.DbSet.Attach(item);
            this.DbSet.Remove(item);

            return this.context.SaveChanges();
        }

        /// <summary>
        /// Delete entities 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Int32 Delete(IEnumerable<TEntity> items)
        {
            this.DbSet.Attach((items as List<TEntity>)[0]);
            this.DbSet.RemoveRange(items);
            return this.context.SaveChanges();
        }

        /// <summary>
        /// Allows to execute a custom query synchronously
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> CustomQuery(Criteria criteria)
        {
            DbSet<TEntity> dbset = this.DbSet;
            this.WaitForDbSetLocal(dbset);
            return dbset.SqlQuery(criteria.Compute()).ToList();
        }

        /// <summary>
        /// Workaround to wait for DbSet.Local to be defined
        /// </summary>
        /// <returns></returns>
        private void WaitForDbSetLocal(DbSet<TEntity> dbset)
        {
            try
            {
                var localType = dbset.Local;
            }
            catch (Exception e)
            {
                var ex = e;
                Task task = this.WaitLocal();

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

        private async Task<ObservableCollection<TEntity>> WaitLocal()
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

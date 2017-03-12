using DataBase.Database.DbContexts.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Database.DbContexts
{
    /// <summary>
    /// Base context
    /// </summary>
    public class DbContextBase<TEntity> : DbContext, ISyncCrudMethods<TEntity>, IAsyncCrudMethods<TEntity> where TEntity : class
    {

        /// <summary>
        /// The DbSet
        /// </summary>
        public DbSet<TEntity> DbSetT { get; set; }

        #region Constructors
        /// <summary>
        /// Create a database with a given provider and settings
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="option"></param>
        public DbContextBase(DbConnection connection, bool option) : base(connection, option)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public DbContextBase(string connectionString) : base(connectionString) {

        }
        #endregion

        #region SQL asynchrone CRUD methods
        /// <summary>
        /// Inserts an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async Task<int> IAsyncCrudMethods<TEntity>.Insert(TEntity item)
        {
            waitForDbSetLocal();

            this.DbSetT.Add(item);
            return await this.SaveChangesAsync();
        }

        ///// <summary>
        ///// Inserts entities
        ///// </summary>
        ///// <param name="items"></param>
        ///// <returns></returns>
        async Task<int> IAsyncCrudMethods<TEntity>.Insert(IEnumerable<TEntity> items)
        {
            waitForDbSetLocal();

            foreach (var item in items)
            {
                DbSetT.Add(item);
            }
            return await SaveChangesAsync();
        }

        ///// <summary>
        ///// Updates an entity
        ///// </summary>
        ///// <param name="item"></param>
        ///// <returns></returns>
        async Task<int> IAsyncCrudMethods<TEntity>.Update(TEntity item)
        {
            await Task.Factory.StartNew(() =>
            {
                Entry<TEntity>(item).State = EntityState.Modified;
            });
            return await this.SaveChangesAsync();
        }

        ///// <summary>
        ///// Updates entities
        ///// </summary>
        ///// <param name="items"></param>
        ///// <returns></returns>
        async Task<int> IAsyncCrudMethods<TEntity>.Update(IEnumerable<TEntity> items)
        {
            await Task.Factory.StartNew(() =>
            {
                foreach (var item in items)
                {
                    Entry<TEntity>(item).State = EntityState.Modified;
                }
            });
            return await this.SaveChangesAsync();
        }

        ///// <summary>
        ///// Gets an entity
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        async Task<TEntity> IAsyncCrudMethods<TEntity>.Get(Int32 id)
        {
            return await this.DbSetT.FindAsync(id) as TEntity;
        }

        ///// <summary>
        ///// Gets entities
        ///// </summary>
        ///// <returns></returns>
        async Task<IEnumerable<TEntity>> IAsyncCrudMethods<TEntity>.Get()
        {
            DbSet<TEntity> temp = default(DbSet<TEntity>);
            List<TEntity> result = new List<TEntity>();
            await Task.Factory.StartNew(() =>
            {
                temp = base.Set<TEntity>();
            });
            result.AddRange(temp);
            return result;
        }

        ///// <summary>
        ///// Deletes an entity
        ///// </summary>
        ///// <param name="item"></param>
        ///// <returns></returns>
        async Task<Int32> IAsyncCrudMethods<TEntity>.Delete(TEntity item)
        {
            await Task.Factory.StartNew(() =>
            {
                DbSetT.Attach(item);
                DbSetT.Remove(item);
            });
            return await this.SaveChangesAsync();
        }

        ///// <summary>
        ///// Deletes entities
        ///// </summary>
        ///// <param name="items"></param>
        ///// <returns></returns>
        async Task<Int32> IAsyncCrudMethods<TEntity>.Delete(IEnumerable<TEntity> items)
        {
            await Task.Factory.StartNew(() =>
            {
                DbSetT.Attach((items as List<TEntity>)[0]);
                DbSetT.RemoveRange(items);
            });
            var res = await this.SaveChangesAsync();
            return res;
        }

        ///// <summary>
        ///// Allows to execute a custom query
        ///// </summary>
        ///// <param name="criteria"></param>
        ///// <returns></returns>
        async Task<IEnumerable<TEntity>> IAsyncCrudMethods<TEntity>.CustomQuery(Criteria.Criteria criteria)
        {
            waitForDbSetLocal();

            return await DbSetT.SqlQuery(criteria.MySQLCompute()).ToListAsync();
        }
        #endregion

        #region SQL synchrone Crud methods

        int ISyncCrudMethods<TEntity>.Insert(TEntity item)
        {
            waitForDbSetLocal();

            DbSetT.Add(item);
            return SaveChanges();
        }

        int ISyncCrudMethods<TEntity>.Insert(IEnumerable<TEntity> items)
        {
            waitForDbSetLocal();

            foreach (var item in items)
            {
                this.DbSetT.Add(item);
            }
            return SaveChanges();
        }

        int ISyncCrudMethods<TEntity>.Update(TEntity item)
        {
            Entry<TEntity>(item).State = EntityState.Modified;
            return SaveChanges();
        }

        int ISyncCrudMethods<TEntity>.Update(IEnumerable<TEntity> items)
        {
            foreach (var item in items)
            {
                this.Entry<TEntity>(item).State = EntityState.Modified;
            }
            return SaveChanges();
        }

        TEntity ISyncCrudMethods<TEntity>.Get(int id)
        {
            return DbSetT.FindAsync(id) as TEntity;
        }

        IEnumerable<TEntity> ISyncCrudMethods<TEntity>.Get()
        {
            DbSet<TEntity> temp = default(DbSet<TEntity>);
            List<TEntity> result = new List<TEntity>();
            temp = base.Set<TEntity>();
            result.AddRange(temp);
            return result;
        }

        Int32 ISyncCrudMethods<TEntity>.Delete(TEntity item)
        {
            DbSetT.Attach(item);
            DbSetT.Remove(item);

            return SaveChanges();
        }

        Int32 ISyncCrudMethods<TEntity>.Delete(IEnumerable<TEntity> items)
        {
            DbSetT.Attach((items as List<TEntity>)[0]);
            DbSetT.RemoveRange(items);
            return SaveChanges();
        }

        IEnumerable<TEntity> ISyncCrudMethods<TEntity>.CustomQuery(Criteria.Criteria criteria)
        {
            waitForDbSetLocal();
            return DbSetT.SqlQuery(criteria.MySQLCompute()).ToList();
        }
        #endregion

        /// <summary>
        /// Workaround to wait for DbSet.Local to be defined
        /// </summary>
        /// <returns></returns>
        private void waitForDbSetLocal()
        {
            try
            {
                var localType = DbSetT.Local;
            }
            catch (Exception e)
            {
                var ex = e;
                Task task = waitLocal();
                task.Wait();
            }
        }

        private async Task<ObservableCollection<TEntity>> waitLocal()
        {
            ObservableCollection<TEntity> result = null;
            await Task.Factory.StartNew(() =>
            {
                result = DbSetT.Local;

            });

            return result;
        }
    }
}

using DataBase.Criterias;
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
        /// Inserts an entity asynchronously
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync(TEntity item)
        {
            waitForDbSetLocal();

            this.DbSetT.Add(item);
            return await this.SaveChangesAsync();
        }

        /// <summary>
        /// Inserts entities asynchronously
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync(IEnumerable<TEntity> items)
        {
            waitForDbSetLocal();

            foreach (var item in items)
            {
                DbSetT.Add(item);
            }
            return await SaveChangesAsync();
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
                Entry<TEntity>(item).State = EntityState.Modified;
            });
            return await this.SaveChangesAsync();
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
                    Entry<TEntity>(item).State = EntityState.Modified;
                }
            });
            return await this.SaveChangesAsync();
        }

        /// <summary>
        /// Gets an entity asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> GetAsync(Int32 id)
        {
            return await this.DbSetT.FindAsync(id) as TEntity;
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
                temp = base.Set<TEntity>();
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
                DbSetT.Attach(item);
                DbSetT.Remove(item);
            });
            return await this.SaveChangesAsync();
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
                DbSetT.Attach((items as List<TEntity>)[0]);
                DbSetT.RemoveRange(items);
            });
            var res = await this.SaveChangesAsync();
            return res;
        }

        /// <summary>
        /// Allows to execute a custom query asynchronously
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> CustomQueryAsync(Criteria criteria)
        {
            waitForDbSetLocal();

            return await DbSetT.SqlQuery(criteria.MySQLCompute()).ToListAsync();
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
            waitForDbSetLocal();

            DbSetT.Add(item);
            return SaveChanges();
        }

        /// <summary>
        /// Inserts entities asynchronously
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public int Insert(IEnumerable<TEntity> items)
        {
            waitForDbSetLocal();

            foreach (var item in items)
            {
                this.DbSetT.Add(item);
            }
            return SaveChanges();
        }

        /// <summary>
        /// Updates an entity synchronously
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Update(TEntity item)
        {
            Entry<TEntity>(item).State = EntityState.Modified;
            return SaveChanges();
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
                this.Entry<TEntity>(item).State = EntityState.Modified;
            }
            return SaveChanges();
        }

        /// <summary>
        /// Gets an entity synchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Get(int id)
        {
            return DbSetT.FindAsync(id) as TEntity;
        }

        /// <summary>
        /// Gets all entities synchronously
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> Get()
        {
            DbSet<TEntity> temp = default(DbSet<TEntity>);
            List<TEntity> result = new List<TEntity>();
            temp = base.Set<TEntity>();
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
            DbSetT.Attach(item);
            DbSetT.Remove(item);

            return SaveChanges();
        }

        /// <summary>
        /// Delete entities 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Int32 Delete(IEnumerable<TEntity> items)
        {
            DbSetT.Attach((items as List<TEntity>)[0]);
            DbSetT.RemoveRange(items);
            return SaveChanges();
        }

        /// <summary>
        /// Allows to execute a custom query synchronously
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> CustomQuery(Criteria criteria)
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

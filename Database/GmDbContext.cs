using DataBase.Interfaces;
using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Database
{
    public abstract class GmDbContext<TEntity> : DbContext where TEntity : class
    {
        public DbSet<TEntity> DbSetT { get; set; }

        #region Constructors
        /// <summary>
        /// Create a database with a given provider and settings
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="provider"></param>
        public GmDbContext(DbConnection connection, bool option) : base(connection, option)
        { 

        }

        public GmDbContext(string connectionString) : base(connectionString) {

        }

        /// <summary>
        /// Create a database with a given provider
        /// Used default settings
        /// </summary>
        /// <param name="provider"></param>
        //public GmDbContext(ProviderType provider) : base(ConnectionStringBuilder.BuildConnectionString(provider))
        //{

        //    InitDb();

        //}

        /// <summary>
        /// Create a database with a given settings
        /// </summary>
        /// <param name="settings"></param>
        //public GmDbContext(IDbSettings settings) : base(ConnectionStringBuilder.BuildConnectionString(DatabaseManager.Instance.Provider, settings))
        //{

        //    InitDb(settings);

        //}

        /// <summary>
        /// Default constructor
        /// Use default provider if any provider is provided
        /// Use default settings
        /// </summary>
        //public GmDbContext() : base(ConnectionStringBuilder.BuildConnectionString(DatabaseManager.Instance.Provider))
        //{
        //    InitDb();

        //}
        #endregion

        //private void InitDb(IDbSettings settings,ProviderType provider)
        //{
        //    Type entityType = typeof(TEntity);
        //    object[] dbSettings = { settings };

        //    switch (provider)
        //    {
        //        case ProviderType.MySQL:

        //            Type mySqlContextType = typeof(MySqlContext<>);
        //            GenericUtils.InstantiateGeneric(mySqlContextType, entityType, dbSettings);
        //            break;

        //        case ProviderType.SQLite:

        //            Type sqliteContextType = typeof(SqliteContext<>);
        //            GenericUtils.InstantiateGeneric(sqliteContextType, entityType, dbSettings);
        //            break;

        //        default:
        //            break;
        //    }
        //}

        #region SQL methods
        public async Task<TEntity> Insert(TEntity item)
        {
            try
            {
                this.DbSetT.Add(item);
                await this.SaveChangesAsync();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }

            return item;
        }

        public async Task<IEnumerable<TEntity>> Insert(IEnumerable<TEntity> items)
        {
            foreach (var item in items)
            {
                this.DbSetT.Add(item);
            }
            await this.SaveChangesAsync();
            return items;
        }

        public async Task<TEntity> Update(TEntity item)
        {
            await Task.Factory.StartNew(() =>
            {
                this.Entry<TEntity>(item).State = EntityState.Modified;
            });
            await this.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> items)
        {
            await Task.Factory.StartNew(() =>
            {
                foreach (var item in items)
                {
                    this.Entry<TEntity>(item).State = EntityState.Modified;
                }
            });
            await this.SaveChangesAsync();
            return items;
        }

        public async Task<TEntity> Get(Int32 id)
        {
            return await this.DbSetT.FindAsync(id) as TEntity;
        }

        public async Task<IEnumerable<TEntity>> Get()
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

        public async Task<Int32> Delete(TEntity item)
        {
            await Task.Factory.StartNew(() =>
            {
                this.DbSetT.Attach(item);
                this.DbSetT.Remove(item);
            });
            return await this.SaveChangesAsync();
        }

        public async Task<Int32> Delete(IEnumerable<TEntity> items)
        {
            await Task.Factory.StartNew(() =>
            {
                this.DbSetT.Attach((items as List<TEntity>)[0]);
                this.DbSetT.RemoveRange(items);
            });
            var res = await this.SaveChangesAsync();
            return res;
        }

        //public async Task<IEnumerable<TEntity>> CustomQuery(Criteria criteria)
        //{
        //    return await this.DbSetT.SqlQuery(criteria.MySQLCompute()).ToListAsync();
        //}
        #endregion
    }
}

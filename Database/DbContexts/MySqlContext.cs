using DataBase.Database;
using DataBase.Interfaces;
using DataBase.Utils;
using DataBase.Criteria;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DataBase.Database.DbContexts.Interface;
using DataBase.Database.DbSettings.Interface;

namespace DataBase.Database.DbContexts
{
    /// <summary>
    /// The MySql Database Context
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    //[DbConfigurationType(typeof(MySQLConfiguration))]
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public partial class MySqlContext<TEntity> : DbContext, IDbContexts where TEntity : class
    {
        /// <summary>
        /// The DbSet
        /// </summary>
        public DbSet<TEntity> DbSetT { get; set; }

        private ProviderType provider;
        /// <summary>
        /// The Provider
        /// </summary>
        public ProviderType Provider
        {
            get { return provider; }
            set { provider = ProviderType.MySQL; }
        }


        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings"></param>
        public MySqlContext(IDbSettings settings) : base(ConnectionStringBuilder.BuildConnectionString(ProviderType.MySQL, settings))
        {

        }

        #endregion

        //public MySqlContext(IDbSettings settings) : base(new MySqlConnection(ConnectionStringBuilder.BuildConnectionString(ProviderType.MySQL, settings)), true)
        //{
        //    //System.Data.Entity.Database.SetInitializer(new CreateDatabaseIfNotExists<MySqlContext<TEntity>>());

        //}

        /// <summary>
        /// Method called during the model creation
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            DataBaseUtils.CreateModel(modelBuilder);
        }

        #region SQL methods
        /// <summary>
        /// Inserts an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> Insert(TEntity item)
        {
            this.DbSetT.Add(item);
            return await this.SaveChangesAsync();
        }

        /// <summary>
        /// Inserts entities
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<int> Insert(IEnumerable<TEntity> items)
        {
            foreach (var item in items)
            {
                this.DbSetT.Add(item);
            }
            return await this.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> Update(TEntity item)
        {
            await Task.Factory.StartNew(() =>
            {
                this.Entry<TEntity>(item).State = EntityState.Modified;
            });
            return await this.SaveChangesAsync();
        }

        /// <summary>
        /// Updates entities
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<int> Update(IEnumerable<TEntity> items)
        {
            await Task.Factory.StartNew(() =>
            {
                foreach (var item in items)
                {
                    this.Entry<TEntity>(item).State = EntityState.Modified;
                }
            });
            return await this.SaveChangesAsync();
        }

        /// <summary>
        /// Gets an entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> Get(Int32 id)
        {
            return await this.DbSetT.FindAsync(id) as TEntity;
        }

        /// <summary>
        /// Gets entities
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Int32> Delete(TEntity item)
        {
            await Task.Factory.StartNew(() =>
            {
                this.DbSetT.Attach(item);
                this.DbSetT.Remove(item);
            });
            return await this.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes entities
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Allows to execute a custom query
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> CustomQuery(Criteria.Criteria criteria)
        {
            return await this.DbSetT.SqlQuery(criteria.MySQLCompute()).ToListAsync();
        }
        #endregion

    }
}

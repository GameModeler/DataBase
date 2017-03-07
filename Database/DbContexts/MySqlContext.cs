using GMDataBase.Database;
using GMDataBase.Interfaces;
using GMDataBase.Utils;
using GMDataBase.Criteria;
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
using GMDataBase.Database.DbContexts.Interface;
using GMDataBase.Database.DbSettings.Interface;

namespace GMDataBase.Database.DbContexts
{

    //[DbConfigurationType(typeof(MySQLConfiguration))]
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public partial class MySqlContext<TEntity> : DbContext, IDbContexts where TEntity : class
    {
        public DbSet<TEntity> DbSetT { get; set; }

        private ProviderType provider;

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // System.Data.Entity.Database.SetInitializer(new CreateDatabaseIfNotExists<MySqlContext<TEntity>>());
            DataBaseUtils.CreateModel(modelBuilder);
        }

        #region SQL methods
        public async Task<int> Insert(TEntity item)
        {
            this.DbSetT.Add(item);
            return await this.SaveChangesAsync();
        }

        public async Task<int> Insert(IEnumerable<TEntity> items)
        {
            foreach (var item in items)
            {
                this.DbSetT.Add(item);
            }
            return await this.SaveChangesAsync();
        }

        public async Task<int> Update(TEntity item)
        {
            await Task.Factory.StartNew(() =>
            {
                this.Entry<TEntity>(item).State = EntityState.Modified;
            });
            return await this.SaveChangesAsync();
        }

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

        public async Task<IEnumerable<TEntity>> CustomQuery(Criteria.Criteria criteria)
        {
            return await this.DbSetT.SqlQuery(criteria.MySQLCompute()).ToListAsync();
        }
        #endregion

    }
}

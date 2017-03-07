using DataBase.Database;
using DataBase.Interfaces;
using DataBase.Utils;
using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Database
{
    [DbConfigurationType(typeof(SqliteConfiguration))]
    class SqliteContext<TEntity> : DbContext where TEntity : class
    {
        //GmDbContext<TEntity> where TEntity : class
        public DbSet<TEntity> DbSetT { get; set; }

        public SqliteContext(IDbSettings settings) : base(new SQLiteConnection() { ConnectionString = ConnectionStringBuilder.BuildConnectionString(ProviderType.SQLite, settings) }, true)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<Database.SqliteContext<TEntity>>(modelBuilder);
            System.Data.Entity.Database.SetInitializer(sqliteConnectionInitializer);

            DataBaseUtils.CreateModel(modelBuilder);
        }


        #region SQL methods
        //public async Task<TEntity> Insert(TEntity item)
        //{

        //    this.DbSetT.Add(item);
        //    await this.SaveChangesAsync();

        //    return item;
        //}

        public async Task<IEnumerable<TEntity>> Insert(IEnumerable<TEntity> items)
        {

            foreach (var item in items)
            {
                this.DbSetT.Add(item);

            }
            await this.SaveChangesAsync();
            return items;
        }

        //public async Task<TEntity> Update(TEntity item)
        //{
        //    await Task.Factory.StartNew(() =>
        //    {
        //        this.Entry<TEntity>(item).State = EntityState.Modified;
        //    });
        //    await this.SaveChangesAsync();
        //    return item;
        //}

        //public async Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> items)
        //{
        //    await Task.Factory.StartNew(() =>
        //    {
        //        foreach (var item in items)
        //        {
        //            this.Entry<TEntity>(item).State = EntityState.Modified;
        //        }
        //    });
        //    await this.SaveChangesAsync();
        //    return items;
        //}

        //public async Task<TEntity> Get(Int32 id)
        //{
        //    return await this.DbSetT.FindAsync(id) as TEntity;
        //}

        //public async Task<IEnumerable<TEntity>> Get()
        //{
        //    DbSet<TEntity> temp = default(DbSet<TEntity>);
        //    List<TEntity> result = new List<TEntity>();
        //    await Task.Factory.StartNew(() =>
        //    {
        //        temp = base.Set<TEntity>();
        //    });
        //    result.AddRange(temp);
        //    return result;
        //}

        //public async Task<Int32> Delete(TEntity item)
        //{
        //    await Task.Factory.StartNew(() =>
        //    {
        //        this.DbSetT.Attach(item);
        //        this.DbSetT.Remove(item);
        //    });
        //    return await this.SaveChangesAsync();
        //}

        //public async Task<Int32> Delete(IEnumerable<TEntity> items)
        //{
        //    await Task.Factory.StartNew(() =>
        //    {
        //        this.DbSetT.Attach((items as List<TEntity>)[0]);
        //        this.DbSetT.RemoveRange(items);
        //    });
        //    var res = await this.SaveChangesAsync();
        //    return res;
        //}

        //public async Task<IEnumerable<TEntity>> CustomQuery(Criteria.Criteria criteria)
        //{
        //    return await this.DbSetT.SqlQuery(criteria.MySQLCompute()).ToListAsync();
        //}

        #endregion
    }
}

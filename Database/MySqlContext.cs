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

namespace DataBase.Database
{

    [DbConfigurationType(typeof(MySQLConfiguration))]
    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    public partial class MySqlContext<TEntity> : DbContext where TEntity : class
    {

        //GmDbContext<TEntity> where TEntity: class
        public DbSet<TEntity> DbSetT { get; set; }

        //public MySqlContext(IDbSettings settings) : base(ConnectionStringBuilder.BuildConnectionString(ProviderType.MySQL, settings))
        //{
        //    //Database.CreateIfNotExists();
        //}

        public MySqlContext(IDbSettings settings) : base(new MySqlConnection(ConnectionStringBuilder.BuildConnectionString(ProviderType.MySQL, settings)), true)
        {
            //DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
            //Database.CreateIfNotExists();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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

            //while (this.DbSetT.Local.Count != 0)
            //{
            //    Thread.SpinWait(1000);
            //}

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

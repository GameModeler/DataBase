using DataBase.Database.DbContexts;
using DataBase.Database.DbSettings.Interface;
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
using static DataBase.Utils.GenericUtils;

namespace DataBase.Database
{
    /// <summary>
    /// Global context
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GmDbContext<TEntity> where TEntity : class
    {

        GmDbManager dbManager = GmDbManager.Instance;

        private const string nsp = "DbContexts";

        private Dictionary<IDbSettings, DbContext> databaseContexts;

        /// <summary>
        /// List of contexts
        /// </summary>
        public Dictionary<IDbSettings, DbContext> DatabaseContexts
        {
            get { return databaseContexts; }
            set { databaseContexts = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public GmDbContext()
        {
            DatabaseContexts = new Dictionary<IDbSettings, DbContext>();
        }

        /// <summary>
        /// Global Context to chained database
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public GmDbContext<TEntity> Context(IDbSettings settings)
        {
            ProviderType provider = settings.Provider;
            Type tentity = typeof(TEntity);
            object[] dbSettings = { settings };

            switch (provider)
            {
                case ProviderType.MySQL:

                    Type mySqlContextType = typeof(MySqlContext<>);

                    DbContext mySqlContext = (DbContext)GenericUtils.InstantiateGeneric(mySqlContextType, tentity, dbSettings);
                    DatabaseContexts.Add(settings, mySqlContext);

                    return this;

                case ProviderType.SQLite:

                    Type sqliteContextType = typeof(SqliteContext<>);
                    DbContext sqliteContext = (DbContext)GenericUtils.InstantiateGeneric(sqliteContextType, tentity, dbSettings);

                    DatabaseContexts.Add(settings, sqliteContext);

                    return this;

                default:
                    
                    // Get all classes from DbContexts namespace
                    List<Type> listTypeDbClasses = AllClassesFromNamespace(nsp);

                    // Get the class type to instantiate
                    var clazz = GenericUtils.GetClassesFromProperty(listTypeDbClasses, "Provider", provider);
                    DbContext defaultContext = (DbContext)GenericUtils.InstantiateGeneric(clazz, tentity, dbSettings);

                    return this;
            }
        }

        /// <summary>
        /// MySQL contexts
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public GmDbContext<TEntity> MySqlContext(IDbSettings settings)
        {
            Type tentity = typeof(TEntity);
            object[] dbSettings = { settings };

            Type mySqlContextType = typeof(MySqlContext<>);

            DbContext mySqlContext = (DbContext)GenericUtils.InstantiateGeneric(mySqlContextType, tentity, dbSettings);
            DatabaseContexts.Add(settings, mySqlContext);

            return this;
         }

        /// <summary>
        /// SQLite contexts
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public GmDbContext<TEntity> SqlLiteContext(IDbSettings settings)
        {
            Type tentity = typeof(TEntity);
            object[] dbSettings = { settings };

            Type sqliteContextType = typeof(SqliteContext<>);

            DbContext context = (DbContext)GenericUtils.InstantiateGeneric(sqliteContextType, tentity, dbSettings);
            DatabaseContexts.Add(settings, context);

            return this;
        }

        #region SQL methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Dictionary<IDbSettings, int>> Insert(TEntity item) 
        {
            Dictionary<IDbSettings, int> result = new Dictionary<IDbSettings, int>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;

                switch(provider)
                {
                    case ProviderType.MySQL:

                        MySqlContext<TEntity> contextMySql = (MySqlContext<TEntity>)entry.Value;
                        var resMySql = await contextMySql.Insert(item);
                        result.Add(dbSettings, resMySql);
                        break;

                    case ProviderType.SQLite:

                        SqliteContext<TEntity> contextSql = (SqliteContext<TEntity>)entry.Value;
                        var resSqlite = await contextSql.Insert(item);
                        result.Add(dbSettings, resSqlite);
                        break;

                    default:
                        break;
                }   
            }       
            return result;
        }

        public async Task<Dictionary<IDbSettings, int>> Insert(IEnumerable<TEntity> items)
        {
            Dictionary<IDbSettings, int> result = new Dictionary<IDbSettings, int>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;

                switch (provider)
                {
                    case ProviderType.MySQL:
                        MySqlContext<TEntity> contextMysql = (MySqlContext<TEntity>)entry.Value;
                        var resMysql = await contextMysql.Insert(items);
                        result.Add(dbSettings, resMysql);
                        break;


                    case ProviderType.SQLite:
                        SqliteContext<TEntity> contextSqlite = (SqliteContext<TEntity>)entry.Value;
                        var resSqlite = await contextSqlite.Insert(items);
                        result.Add(dbSettings, resSqlite);
                        break;
                }
            }

            return result;
        }

        //public Dictionary<IDbSettings, int> Insert(IEnumerable<TEntity> items)
        //{
        //    Dictionary<IDbSettings, int> result = new Dictionary<IDbSettings, int>();

        //    foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
        //    {
        //        IDbSettings dbSettings = entry.Key;
        //        ProviderType provider = dbSettings.Provider;

        //        switch (provider)
        //        {
        //            case ProviderType.MySQL:
        //                MySqlContext<TEntity> contextMysql = (MySqlContext<TEntity>)entry.Value;
        //                var resMysql = contextMysql.Insert(items);
        //                result.Add(dbSettings, resMysql);
        //                break;


        //            case ProviderType.SQLite:
        //                SqliteContext<TEntity> contextSqlite = (SqliteContext<TEntity>)entry.Value;
        //                var resSqlite = contextSqlite.Insert(items);
        //                result.Add(dbSettings, resSqlite);
        //                break;
        //        }
        //    }

        //    return result;
        //}

        public async Task<Dictionary<IDbSettings, int>> Update(TEntity item)
        {
            Dictionary<IDbSettings, int> result = new Dictionary<IDbSettings, int>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;

                switch (provider)
                {
                    case ProviderType.MySQL:
                        MySqlContext<TEntity> contextMysql = (MySqlContext<TEntity>)entry.Value;
                        var resMysql = await contextMysql.Update(item);
                        result.Add(dbSettings, resMysql);
                        break;


                    case ProviderType.SQLite:
                        SqliteContext<TEntity> contextSqlite = (SqliteContext<TEntity>)entry.Value;
                        var resSqlite = await contextSqlite.Update(item);
                        result.Add(dbSettings, resSqlite);
                        break;
                }
            }

            return result;
        }

        public async Task<Dictionary<IDbSettings, int>> Update(IEnumerable<TEntity> items)
        {
            Dictionary<IDbSettings, int> result = new Dictionary<IDbSettings, int>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;

                switch (provider)
                {
                    case ProviderType.MySQL:
                        MySqlContext<TEntity> contextMysql = (MySqlContext<TEntity>)entry.Value;
                        var resMysql = await contextMysql.Update(items);
                        result.Add(dbSettings, resMysql);
                        break;


                    case ProviderType.SQLite:
                        SqliteContext<TEntity> contextSqlite = (SqliteContext<TEntity>)entry.Value;
                        var resSqlite = await contextSqlite.Update(items);
                        result.Add(dbSettings, resSqlite);
                        break;
                }
            }
            return result;
        }

        public async Task<Dictionary<IDbSettings, TEntity>> Get(Int32 id)
        {
            Dictionary<IDbSettings, TEntity> result = new Dictionary<IDbSettings, TEntity>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;

                switch (provider)
                {
                    case ProviderType.MySQL:
                        MySqlContext<TEntity> contextMysql = (MySqlContext<TEntity>)entry.Value;
                        var resMysql = await contextMysql.Get(id);
                        result.Add(dbSettings, resMysql);
                        break;


                    case ProviderType.SQLite:
                        SqliteContext<TEntity> contextSqlite = (SqliteContext<TEntity>)entry.Value;
                        var resSqlite = await contextSqlite.Get(id);
                        result.Add(dbSettings, resSqlite);
                        break;
                }
            }

            return result;
        }

        public async Task<Dictionary<IDbSettings, IEnumerable<TEntity>>> Get()
        {
            Dictionary<IDbSettings, IEnumerable<TEntity>> result = new Dictionary<IDbSettings, IEnumerable<TEntity>>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;

                switch (provider)
                {
                    case ProviderType.MySQL:
                        MySqlContext<TEntity> contextMysql = (MySqlContext<TEntity>)entry.Value;
                        var resMysql = await contextMysql.Get();
                        result.Add(dbSettings, resMysql);
                        break;


                    case ProviderType.SQLite:
                        SqliteContext<TEntity> contextSqlite = (SqliteContext<TEntity>)entry.Value;
                        var resSqlite = await contextSqlite.Get();
                        result.Add(dbSettings, resSqlite);
                        break;
                }
            }

            return result;
        }

        public async Task<Dictionary<IDbSettings, Int32>> Delete(TEntity item)
        {
            Dictionary<IDbSettings, Int32> result = new Dictionary<IDbSettings, Int32>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;

                switch (provider)
                {
                    case ProviderType.MySQL:
                        MySqlContext<TEntity> contextMysql = (MySqlContext<TEntity>)entry.Value;
                        var resMysql = await contextMysql.Delete(item);
                        result.Add(dbSettings, resMysql);
                        break;


                    case ProviderType.SQLite:
                        SqliteContext<TEntity> contextSqlite = (SqliteContext<TEntity>)entry.Value;
                        var resSqlite = await contextSqlite.Delete(item);
                        result.Add(dbSettings, resSqlite);
                        break;
                }
            }

            return result;
        }

        public async Task<Dictionary<IDbSettings, Int32>> Delete(IEnumerable<TEntity> items)
        {
            Dictionary<IDbSettings, Int32> result = new Dictionary<IDbSettings, Int32>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;

                switch (provider)
                {
                    case ProviderType.MySQL:
                        MySqlContext<TEntity> contextMysql = (MySqlContext<TEntity>)entry.Value;
                        var resMysql = await contextMysql.Delete(items);
                        result.Add(dbSettings, resMysql);
                        break;


                    case ProviderType.SQLite:
                        SqliteContext<TEntity> contextSqlite = (SqliteContext<TEntity>)entry.Value;
                        var resSqlite = await contextSqlite.Delete(items);
                        result.Add(dbSettings, resSqlite);
                        break;
                }
            }

            return result;
        }

        public async Task<Dictionary<IDbSettings, IEnumerable<TEntity>>> CustomQuery(Criteria.Criteria criteria)
        {
            Dictionary<IDbSettings, IEnumerable<TEntity>> result = new Dictionary<IDbSettings, IEnumerable<TEntity>>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;

                switch (provider)
                {
                    case ProviderType.MySQL:
                        MySqlContext<TEntity> contextMysql = (MySqlContext<TEntity>)entry.Value;
                        var resMysql = await contextMysql.CustomQuery(criteria);
                        result.Add(dbSettings, resMysql);
                        break;
                 
                    case ProviderType.SQLite:
                        SqliteContext<TEntity> contextSqlite = (SqliteContext<TEntity>)entry.Value;
                        var resSqlite = await contextSqlite.CustomQuery(criteria);
                        result.Add(dbSettings, resSqlite);
                        break;
                }
            }
            return result;
        }
        #endregion
    }
}

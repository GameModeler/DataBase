using DataBase.Database.DbContexts;
using DataBase.Database.DbContexts.Interface;
using DataBase.Database.DbSettings.Interface;
using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DataBase.Database
{
    /// <summary>
    /// Global context
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GlobalContext<TEntity> where TEntity : class
    {

        DbManager dbManager = DbManager.Instance;

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
        public GlobalContext()
        {
            DatabaseContexts = new Dictionary<IDbSettings, DbContext>();
        }

        /// <summary>
        /// Global Context to chained database
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public GlobalContext<TEntity> Context(IDbSettings settings)
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
                    List<Type> listTypeDbClasses = GenericUtils.AllClassesFromNamespace(nsp);

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
        public GlobalContext<TEntity> MySqlContext(IDbSettings settings)
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
        public GlobalContext<TEntity> SqlLiteContext(IDbSettings settings)
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
        /// Insert item Asynchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Dictionary<IDbSettings, int>> InsertAsync(TEntity item) 
        {
            Dictionary<IDbSettings, int> result = new Dictionary<IDbSettings, int>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;
                DbContextBase<TEntity> context = GetContext(entry, provider);
                var resMySql = await context.InsertAsync(item);
                result.Add(dbSettings, resMySql);
            }       
            return result;
        }

        /// <summary>
        /// Insert item Synchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Dictionary<IDbSettings, int> Insert(TEntity item)
        {
            Dictionary<IDbSettings, int> result = new Dictionary<IDbSettings, int>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;
                DbContextBase<TEntity> context = GetContext(entry, provider);
                var resMySql = context.Insert(item);
                result.Add(dbSettings, resMySql);
            }
            return result;
        }

        /// <summary>
        /// Insert items Asynchrone method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<Dictionary<IDbSettings, int>> InsertAsync(IEnumerable<TEntity> items)
        {
            Dictionary<IDbSettings, int> result = new Dictionary<IDbSettings, int>();


            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;

                DbContextBase<TEntity> context = GetContext(entry, provider);
                var resMysql = await context.InsertAsync(items);
                result.Add(dbSettings, resMysql);
               
            }

            return result;
        }

        /// <summary>
        /// Insert items Synchrone method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Dictionary<IDbSettings, int> Insert(IEnumerable<TEntity> items)
        {
            Dictionary<IDbSettings, int> result = new Dictionary<IDbSettings, int>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;
                DbContextBase<TEntity> context = GetContext(entry, provider);

                var resMysql = context.Insert(items);
                result.Add(dbSettings, resMysql);
            }

            return result;
        }

        /// <summary>
        /// Update item Asynchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Dictionary<IDbSettings, int>> UpdateAsync(TEntity item)
        {
            Dictionary<IDbSettings, int> result = new Dictionary<IDbSettings, int>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;
                DbContextBase<TEntity> context = GetContext(entry, provider);
                var resMysql = await context.UpdateAsync(item);
                result.Add(dbSettings, resMysql);
            }

            return result;
        }

        /// <summary>
        /// Update item Synchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Dictionary<IDbSettings, int> Update(TEntity item)
        {
            Dictionary<IDbSettings, int> result = new Dictionary<IDbSettings, int>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;
                DbContextBase<TEntity> context = GetContext(entry, provider);
                var resMysql = context.Update(item);
                result.Add(dbSettings, resMysql);
            }
            return result;
        }

        /// <summary>
        /// Update items Asynchrone method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<Dictionary<IDbSettings, int>> UpdateAsync(IEnumerable<TEntity> items)
        {
            Dictionary<IDbSettings, int> result = new Dictionary<IDbSettings, int>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;
                DbContextBase<TEntity> context = GetContext(entry, provider);
                var resMysql = await context.UpdateAsync(items);
                result.Add(dbSettings, resMysql);
            }
            return result;
        }

        /// <summary>
        /// Update items Synchrone method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Dictionary<IDbSettings, int> Update(IEnumerable<TEntity> items)
        {
            Dictionary<IDbSettings, int> result = new Dictionary<IDbSettings, int>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;
                DbContextBase<TEntity> context = GetContext(entry, provider);

                var resMysql = context.Update(items);
                result.Add(dbSettings, resMysql);
            }
            return result;
        }
        
        /// <summary>
        /// Get item Asynchrone method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Dictionary<IDbSettings, TEntity>> GetAsync(Int32 id)
        {
            Dictionary<IDbSettings, TEntity> result = new Dictionary<IDbSettings, TEntity>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;
                DbContextBase<TEntity> context = GetContext(entry, provider);
                var resMysql = await context.GetAsync(id);
                result.Add(dbSettings, resMysql);
            }
            return result;
        }

        /// <summary>
        /// Get item Synchrone method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<IDbSettings, TEntity> Get(Int32 id)
        {
            Dictionary<IDbSettings, TEntity> result = new Dictionary<IDbSettings, TEntity>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;
                DbContextBase<TEntity> context = GetContext(entry, provider);
                var resMysql = context.Get(id);
                result.Add(dbSettings, resMysql);
            }
            return result;
        }

        /// <summary>
        /// Get all items Asynchrone method
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<IDbSettings, IEnumerable<TEntity>>> GetAsync()
        {
            Dictionary<IDbSettings, IEnumerable<TEntity>> result = new Dictionary<IDbSettings, IEnumerable<TEntity>>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;
                DbContextBase<TEntity> context = GetContext(entry, provider);
                var resMysql = await context.GetAsync();
                result.Add(dbSettings, resMysql);
            }
            return result;
        }

        /// <summary>
        /// Get all items Synchrone method
        /// </summary>
        /// <returns></returns>
        public Dictionary<IDbSettings, IEnumerable<TEntity>> Get()
        {
            Dictionary<IDbSettings, IEnumerable<TEntity>> result = new Dictionary<IDbSettings, IEnumerable<TEntity>>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;
                DbContextBase<TEntity> context = GetContext(entry, provider);

                var resMysql = context.Get();
                result.Add(dbSettings, resMysql);
            }
            return result;
        }

        /// <summary>
        /// Delete item Asynchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Dictionary<IDbSettings, Int32>> DeleteAsync(TEntity item)
        {
            Dictionary<IDbSettings, Int32> result = new Dictionary<IDbSettings, Int32>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;
                DbContextBase<TEntity> context = GetContext(entry, provider);
                var resMysql = await context.DeleteAsync(item);
                result.Add(dbSettings, resMysql);
            }
            return result;
        }

        /// <summary>
        /// Delete item Synchrone method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Dictionary<IDbSettings, Int32> Delete(TEntity item)
        {
            Dictionary<IDbSettings, Int32> result = new Dictionary<IDbSettings, Int32>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;
                DbContextBase<TEntity> context = GetContext(entry, provider);
                var resMysql = context.Delete(item);
                result.Add(dbSettings, resMysql);
            }
            return result;
        }

        /// <summary>
        /// Delete items Asynchrone method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<Dictionary<IDbSettings, Int32>> DeleteAsync(IEnumerable<TEntity> items)
        {
            Dictionary<IDbSettings, Int32> result = new Dictionary<IDbSettings, Int32>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;
                DbContextBase<TEntity> context = GetContext(entry, provider);
                var resMysql = await context.DeleteAsync(items);
                result.Add(dbSettings, resMysql);
            }
            return result;
        }

        /// <summary>
        /// Delete items Synchrone method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Dictionary<IDbSettings, Int32> Delete(IEnumerable<TEntity> items)
        {
            Dictionary<IDbSettings, Int32> result = new Dictionary<IDbSettings, Int32>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;
                DbContextBase<TEntity> context = GetContext(entry, provider);

                var resMysql = context.Delete(items);
                result.Add(dbSettings, resMysql);   
            }
            return result;
        }

        /// <summary>
        /// Custom Query Asynchrone method
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public async Task<Dictionary<IDbSettings, IEnumerable<TEntity>>> CustomQueryAsync(Criteria.Criteria criteria)
        {
            Dictionary<IDbSettings, IEnumerable<TEntity>> result = new Dictionary<IDbSettings, IEnumerable<TEntity>>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;
                DbContextBase<TEntity> context = GetContext(entry, provider);
                var resMysql = await context.CustomQueryAsync(criteria);
                result.Add(dbSettings, resMysql);
            }
            return result;
        }

        /// <summary>
        /// Custom Query Synchrone method
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public Dictionary<IDbSettings, IEnumerable<TEntity>> CustomQuery(Criteria.Criteria criteria)
        {
            Dictionary<IDbSettings, IEnumerable<TEntity>> result = new Dictionary<IDbSettings, IEnumerable<TEntity>>();

            foreach (KeyValuePair<IDbSettings, DbContext> entry in DatabaseContexts)
            {
                IDbSettings dbSettings = entry.Key;
                ProviderType provider = dbSettings.Provider;
                DbContextBase<TEntity> context = GetContext(entry, provider);             
                var resMysql = context.CustomQuery(criteria);
                result.Add(dbSettings, resMysql);
            }
            return result;
        }
        #endregion

        private DbContextBase<TEntity> GetContext(KeyValuePair<IDbSettings, DbContext> entry, ProviderType provider)
        {
            switch (provider)
            {
                case ProviderType.MySQL:
                    return (MySqlContext<TEntity>)entry.Value;
                    
                case ProviderType.SQLite:
                    return (SqliteContext<TEntity>)entry.Value;
                default:
                    return null;
            }
        }
    }
}

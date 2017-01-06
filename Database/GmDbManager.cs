using DataBase.Database;
using DataBase.Interfaces;
using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Database
{
    /// <summary>
    /// To manage databases
    /// </summary>
    public class GmDbManager
    {
        Dictionary<string, IDbSettings> Databases;


        #region Constructors
        /// <summary>
        /// GmDbManager constructor with a provider
        /// </summary>
        /// <param name="provider"></param>
        public GmDbManager(ProviderType provider)
        {
            Databases = new Dictionary<string, IDbSettings>(); 
        }

        /// <summary>
        /// GmDbManager default constructor
        /// </summary>
        public GmDbManager()
        {
            Databases = new Dictionary<string, IDbSettings>();
        }
        #endregion


        //public GmDbContext<T> GetDbEntity<T>(T entity, IDbSettings settings) where T : class
        //{
        //    Activator.CreateInstance(typeof(T));
        //    Type gmDbContextType = typeof(GmDbContext<>);
        //    Type tentity = typeof(T);

        //    switch (Provider)
        //    {
        //        case ProviderType.MySQL:

        //            Type mySqlContextType = typeof(MySqlContext<>);


        //            GmDbContext<T> result = (GmDbContext <T>) GenericUtils.InstantiateGeneric(mySqlContextType, tentity);

        //            return result;

        //        case ProviderType.SQLite:

        //            Type sqliteContextType = typeof(SqliteContext<>);
        //            return (GmDbContext<T>)GenericUtils.InstantiateGeneric(sqliteContextType, tentity);
                    
        //        default:
        //            return null;
        //    }
        //}

        //public MySqlContext<T> GetDbEntity<T>(T entity, IDbSettings settings) where T : class
        //{
        //    return new MySqlContext<T>
        //}

        //public T GetContext<T, U>(U entity, IDbSettings settings, ProviderType provider) where T : DbContext
        //                                                                                 where U : class, new()
        //{
        //    var tent = new U();
        //    Type tentity = typeof(U);
        //    object[] dbSettings = { settings };

        //    switch (provider)
        //    {
        //        case ProviderType.MySQL:
        //            Type mySqlContextType = typeof(MySqlContext<>);
        //            return (T) GenericUtils.InstantiateGeneric(mySqlContextType, tentity, dbSettings);
        //        case ProviderType.SQLite:
        //            Type sqliteContextType = typeof(SqliteContext<>);
        //            return (T) GenericUtils.InstantiateGeneric(sqliteContextType, tentity, dbSettings);
        //        default:
        //            return null;
        //    }
        //}


        public GmDbContext<T> CreateContext<T> (IDbSettings settings, ProviderType provider) where T : class
        {
            Type tentity = typeof(T);
            object[] dbSettings = { settings };

            switch (provider)
            {
                case ProviderType.MySQL:
                    Type mySqlContextType = typeof(MySqlContext<>);
                    return (GmDbContext<T>)GenericUtils.InstantiateGeneric(mySqlContextType, tentity, dbSettings);
                case ProviderType.SQLite:
                    Type sqliteContextType = typeof(SqliteContext<>);
                    return (GmDbContext<T>)GenericUtils.InstantiateGeneric(sqliteContextType, tentity, dbSettings);
                default:
                    return null;
            }
        }
    }  


}

using DataBase.Database.DbContexts;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.DbSettings.DbClasses;
using DataBase.Database.DbSettings.Interfaces;
using DataBase.Database.Utils;
using System;
using System.Collections.Generic;

namespace DataBase.Database.DbSettings
{
    /// <summary>
    /// Database facotry
    /// </summary>
    public class DatabaseFactory
    {

        private const string nsp = "DbContexts";

        /// <summary>
        /// Set a new Database settings from a IDbSettings
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dnName"></param>
        /// <returns></returns>
        public static T DatabaseSettings<T>(string dnName) where T : IDbSettings, new()
        {
            T obj = new T();
            obj.DatabaseName = dnName;
            return obj;
        }

        /// <summary>
        /// Get a MySql database
        /// </summary>
        public static MySqlDatabase MySqlDb
        {
            get { return new MySqlDatabase(); }
        }

        /// <summary>
        /// Get a SqLite database
        /// </summary>
        public static SqLiteDatabase SqLiteDb
        {
            get { return new SqLiteDatabase(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public static IUniversalContext CreateContext(IDbSettings settings)
        {
            ProviderType provider = settings.Provider;
            
            switch (provider)
            {
                case ProviderType.MySQL:

                    return new MySqlContext(settings);

                case ProviderType.SQLite:

                    return new SqLiteContext(settings);

                default:

                    // Get all classes from DbContexts namespace
                    List<Type> listTypeDbClasses = GenericUtils.AllClassesFromNamespace(nsp);

                    // Get the class type to instantiate
                    var clazz = GenericUtils.GetClassesFromProperty(listTypeDbClasses, "Provider", provider);
                    return (IUniversalContext)Activator.CreateInstance(clazz, settings);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IGlobalContext CreateGlobalContext()
        {
            return new GlobalContext();
        }
    }
}

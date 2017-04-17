using DataBase.Database.DbContexts;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.DbSettings.Interfaces;
using DataBase.Database.Utils;
using System;
using System.Collections.Generic;

namespace DataBase.Database
{
    /// <summary>
    /// To manage databases
    /// </summary>
    public class DbManager
    {

        private Dictionary<string, IDbSettings> databases;

        private List<IDbContext> applicationContexts;

        /// <summary>
        /// All contexts of the application
        /// </summary>
        public List<IDbContext> ApplicationContexts
        {
            get { return applicationContexts; }
            set { applicationContexts = value; }
        }


        private const string nsp = "DbContexts";

        /// <summary>
        /// List of databases
        /// </summary>
        public Dictionary<string, IDbSettings> Databases
        {
            get { return databases; }
            set { databases = value; }
        }

        private int nbDefaultDb;

        /// <summary>
        /// Number for database default name
        /// </summary>
        public int NbDefaultDb
        {
            get
            {
                return nbDefaultDb;
            }
            set
            {
                nbDefaultDb = value;
            }
        }

        /// <summary>
        /// Return and incrcement the database default number
        /// </summary>
        /// <returns></returns>
        public int GetAndIncrNbDefaultDb()
        {
            return nbDefaultDb++;
        }

        #region Singleton
        private static volatile DbManager instance;
        private static object syncRoot = new Object();

        private DbManager()
        {
            nbDefaultDb = 0;
            Databases = new Dictionary<string, IDbSettings>();
            ApplicationContexts = new List<IDbContext>();
        }

        /// <summary>
        /// Gets the database manager Instance
        /// </summary>
        public static DbManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DbManager();
                    }
                }
                return instance;
            }
        }
        #endregion

        /// <summary>
        /// Get database from a database name
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public IDbSettings GetDatabase(String dbName)
        {
            IDbSettings dbResult;
            if (Databases.TryGetValue(dbName, out dbResult))
            {
                return dbResult;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a universal context
        /// </summary>
        /// <param name="dbsettings"></param>
        /// <returns></returns>

        public UniversalContext CreateContext(IDbSettings dbsettings)
        {
            ProviderType provider = dbsettings.Provider;

            switch(provider)
            {
                case ProviderType.MySQL:
                    return new MySqlContext(dbsettings);

                case ProviderType.SQLite:
                    return new SqLiteContext(dbsettings);

                default:
                    // Get all classes from DbContexts namespace
                    List<Type> listTypeDbClasses = GenericUtils.AllClassesFromNamespace(nsp);

                    // Get the class type to instantiate
                    var clazz = GenericUtils.GetClassesFromProperty(listTypeDbClasses, "Provider", provider);
                    return (UniversalContext)Activator.CreateInstance(clazz, dbsettings);
            }
        }
        
        /// <summary>
        /// Gets a global context
        /// </summary>
        /// <returns></returns>
        public GlobalContext CreateGlobalContext()
        {
            return new GlobalContext();
        }
    }
}

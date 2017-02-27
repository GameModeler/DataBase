
using GMDataBase.Database.DbSettings.Interface;
using GMDataBase.Interfaces;
using GMDataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GMDataBase.Database
{
    public class DatabaseManager
    {

        public Dictionary<string, IDbSettings> databases;
        public ProviderType provider = ProviderType.Default;

        private int nbDefaultDb;

        #region Singleton
        private static volatile DatabaseManager instance;
        private static object syncRoot = new Object();

        private DatabaseManager() {
            nbDefaultDb = 1;
            databases = new Dictionary<String, IDbSettings>();
            provider = ProviderType.Default;
        }

        public static DatabaseManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DatabaseManager();
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
            } else
            {
                //database not found
                return null;
            }
        }

        /// <summary>
        /// Get all the databases
        /// </summary>
        public Dictionary<String, IDbSettings> Databases
        {
            get
            {
                return databases;
            }
        }

        public void SetProvider(ProviderType providerType)
        {
            if(Provider == ProviderType.Default)
            {
                provider = providerType;
            } else
            {
                // log : un provider est déjà configuré
            }
        }

        /// <summary>
        /// Access the provider
        /// Get the default provider if any provider was provided
        /// </summary>
        public ProviderType Provider
        {
            get {
                return provider;
            }
        }

        public int NbDefaultDb
        {
            get
            {
                return nbDefaultDb++;
            }
            set
            {
                nbDefaultDb = value;
            }
        }
    }
}

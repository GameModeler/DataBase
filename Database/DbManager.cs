using DataBase.Database.DbSettings.Interface;
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
        }

        /// <summary>
        /// 
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
                //database not found
                return null;
            }
        }

        public GlobalContext<T> ContextFactory<T>() where T : class
        {
            return new GlobalContext<T>();
        }
    }
}

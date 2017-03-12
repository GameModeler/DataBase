using DataBase.Database.DbSettings.Interface;
using System;
using System.Collections.Generic;

namespace DataBase.Database
{
    /// <summary>
    /// To manage databases
    /// </summary>
    public class GmDbManager
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
                return nbDefaultDb++;
            }
            set
            {
                nbDefaultDb = value;
            }
        }

        #region Singleton
        private static volatile GmDbManager instance;
        private static object syncRoot = new Object();

        private GmDbManager()
        {
            NbDefaultDb = 1;
            Databases = new Dictionary<string, IDbSettings>();
        }

        /// <summary>
        /// 
        /// </summary>
        public static GmDbManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GmDbManager();
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

        /// <summary>
        /// Provide a way to look for database information into the list of databases
        /// </summary>
        /// <param name="crible"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        //public IEnumerable<IDbSettings> SearchInDatabases(String crible, String value)
        //{
        //    return from db in Databases.Values
        //           where db.GetCrible(crible) == value
        //           select db;
        //}

        public GmDbContext<T> ContextFactory<T>() where T : class
        {
            return new GmDbContext<T>();
        }
    }
}

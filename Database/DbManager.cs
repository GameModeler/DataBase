// <copyright file="DbManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DataBase.Database
{
    using System;
    using System.Collections.Generic;
    using DataBase.Database.DbContexts;
    using DataBase.Database.DbContexts.Initializer;
    using DataBase.Database.DbContexts.Interfaces;
    using DataBase.Database.DbSettings.Interfaces;
    using DataBase.Database.Utils;

    /// <summary>
    /// To manage databases
    /// </summary>
    public class DbManager
    {

        private const string Nsp = "DbContexts";

        private static volatile DbManager instance;
        private static object syncRoot = new object();

        private Dictionary<string, IDbSettings> databases;

        private List<IUniversalContext> applicationContexts;

        private int nbDefaultDb;

        private DbManager()
        {
            this.nbDefaultDb = 0;
            this.Databases = new Dictionary<string, IDbSettings>();
            this.ApplicationContexts = new List<IUniversalContext>();
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

        /// <summary>
        /// Gets or sets all created contexts
        /// </summary>
        public List<IUniversalContext> ApplicationContexts
        {
            get { return this.applicationContexts; }
            set { this.applicationContexts = value; }
        }

        /// <summary>
        /// Gets or sets list of databases
        /// </summary>
        public Dictionary<string, IDbSettings> Databases
        {
            get { return this.databases; }
            set { this.databases = value; }
        }

        /// <summary>
        /// Gets or sets number for database default name
        /// </summary>
        public int NbDefaultDb
        {
            get
            {
                return this.nbDefaultDb;
            }

            set
            {
                this.nbDefaultDb = value;
            }
        }

        /// <summary>
        /// Return and incrcement the database default number
        /// </summary>
        /// <returns>the incremented default number</returns>
        public int GetAndIncrNbDefaultDb()
        {
            return this.nbDefaultDb++;
        }

        /// <summary>
        /// Get database from a database name
        /// </summary>
        /// <param name="dbName">The database name</param>
        /// <returns>IDbSettings</returns>
        public IDbSettings GetDatabase(string dbName)
        {
            IDbSettings dbResult;
            if (this.Databases.TryGetValue(dbName, out dbResult))
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
        /// <param name="dbsettings">the database settings</param>
        /// <returns>IUniversalContext</returns>
        public IUniversalContext CreateContext(IDbSettings dbsettings)
        {
            ProviderType provider = dbsettings.Provider;

            switch (provider)
            {
                case ProviderType.MySQL:
                    return new MySqlContext(dbsettings);

                case ProviderType.SQLite:
                    return new SqLiteContext(dbsettings);

                default:
                    // Get all classes from DbContexts namespace
                    List<Type> listTypeDbClasses = GenericUtils.AllClassesFromNamespace(Nsp);

                    // Get the class type to instantiate
                    var clazz = GenericUtils.GetClassesFromProperty(listTypeDbClasses, "Provider", provider);
                    return (IUniversalContext)Activator.CreateInstance(clazz, dbsettings);
            }
        }

        /// <summary>
        /// Gets a global context
        /// </summary>
        /// <returns>IGlobalContext</returns>
        public IGlobalContext CreateGlobalContext()
        {
            return new GlobalContext();
        }
    }
}

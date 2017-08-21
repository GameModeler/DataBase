using DataBase.Database.DbContexts.Initializer;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.DbSettings.DbClasses;
using DataBase.Database.DbSettings.Interfaces;
using DataBase.Database.Repositories;
using DataBase.Database.Repositories.Interfaces;
using DataBase.Database.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using static DataBase.Database.Utils.GenericUtils;

namespace DataBase.Database.DbContexts
{
    /// <summary>
    /// Universal Context
    /// </summary>
    public class UniversalContext : DbContext, IUniversalContext
    {
        /// <summary>
        /// Databse settings related the context
        /// </summary>
        protected IDbSettings dbSettings;

        /// <summary>
        /// DbManager instance
        /// </summary>
        protected DbManager dbManager = DbManager.Instance;
                                                                                                    
        /// <summary>
        /// Database settings
        /// </summary>
        public IDbSettings DbSettings
        {
            get { return dbSettings; }
        }

        /// <summary>
        /// List of DbSet
        /// </summary>
        public GenericDictionary DbSets { get; set; }

        private GenericDictionary entities = new GenericDictionary();

        /// <summary>
        /// Repositories of the context
        /// </summary>
        public GenericDictionary Entities
        {
            get { return entities; }
        }

        /// <summary>
        /// Enable Lazy Loading
        /// </summary>
        public bool EnableLazyLoading
        {
            get { return Configuration.LazyLoadingEnabled; }
            set { Configuration.LazyLoadingEnabled = value; }
        }

        /// <summary>
        /// Get the Entity Framework DbContext
        /// </summary>
        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// Set the DbSets
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            DbSet<TEntity> dbSet = DbSet<TEntity>();
            if (dbSet == null)
            {
                dbSet = base.Set<TEntity>();
                DbSets.Add(typeof(TEntity), dbSet);
            }
            
            return dbSet;
         }

        /// <summary>
        /// Set a repository
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IRepository<TEntity> Entity<TEntity>() where TEntity : class
        {
            Type entityType = typeof(TEntity);
            IRepository<TEntity> repo;
            if(entities.TryGetValue(entityType, out repo)) {

                return (IRepository<TEntity>)repo;

            } else
            {
                Repository<TEntity> repository = new Repository<TEntity>(this);
                entities.Add(entityType, repository);

                return repository;
            }          
        }

        #region Constructors

        /// <summary>
        /// Create a database with a given provider and settings
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="option"></param>
        public UniversalContext(DbConnection connection, bool option) : base(connection, option)
        {
            DbSets = new GenericDictionary();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public UniversalContext(string connectionString) : base(connectionString)
        {
            DbSets = new GenericDictionary();
        }
        #endregion

        #region On model creating
        /// <summary>
        /// Method called during the model creation
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            DataBaseUtils.CreateModel(modelBuilder, this);
        }
        #endregion

        /// <summary>
        /// Get a dbSet from an entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private DbSet<T> DbSet<T>() where T : class
        {
            Type entityType = typeof(T);
            DbSet<T> dbset;
            DbSets.TryGetValue(entityType, out dbset);
            return dbset;
        }
       

        /// <summary>
        /// Initialize the database
        /// </summary>
        public void Initialize()
        {
            var dbExist = false;

            switch (DbSettings.Provider)
            {
                case ProviderType.SQLite:

                    ISqLiteDatabase sqliteSetting = (ISqLiteDatabase)DbSettings;
                    FileInfo fileInfo = new FileInfo(sqliteSetting.DataSource);
                    dbExist = (File.Exists(sqliteSetting.DataSource) && fileInfo.Length != 0);
                    break;

                case ProviderType.MySQL:
                    dbExist = Database.Exists();
                    break;
            }

            if (!dbExist)
            {
                Database.Initialize(true);
                new DbInitializer().Seed(this);

            }
        }
    }
}

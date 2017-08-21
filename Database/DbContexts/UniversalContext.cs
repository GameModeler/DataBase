// <copyright file="UniversalContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DataBase.Database.DbContexts
{
    using System;
    using System.Data.Common;
    using System.Data.Entity;
    using System.IO;
    using DataBase.Database.DbContexts.Initializer;
    using DataBase.Database.DbContexts.Interfaces;
    using DataBase.Database.DbSettings.Interfaces;
    using DataBase.Database.Repositories;
    using DataBase.Database.Repositories.Interfaces;
    using DataBase.Database.Utils;
    using static DataBase.Database.Utils.GenericUtils;

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

        private GenericDictionary entities = new GenericDictionary();

        /// <summary>
        /// Initializes a new instance of the <see cref="UniversalContext"/> class.
        /// Create a database with a given provider and settings
        /// </summary>
        /// <param name="connection">connection string</param>
        /// <param name="option">option</param>
        public UniversalContext(DbConnection connection, bool option)
            : base(connection, option)
        {
            this.DbSets = new GenericDictionary();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniversalContext"/> class.
        /// </summary>
        /// <param name="connectionString">connection string</param>
        public UniversalContext(string connectionString)
            : base(connectionString)
        {
            this.DbSets = new GenericDictionary();
        }

        /// <summary>
        /// Gets database settings
        /// </summary>
        public IDbSettings DbSettings
        {
            get { return this.dbSettings; }
        }

        /// <summary>
        /// Gets or sets list of DbSet
        /// </summary>
        public GenericDictionary DbSets { get; set; }

        /// <summary>
        /// Gets repositories of the context
        /// </summary>
        public GenericDictionary Entities
        {
            get { return this.entities; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether enable Lazy Loading
        /// </summary>
        public bool EnableLazyLoading
        {
            get { return this.Configuration.LazyLoadingEnabled; }
            set { this.Configuration.LazyLoadingEnabled = value; }
        }

        /// <summary>
        /// Gets get the Entity Framework DbContext
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
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <returns>DbSet</returns>
        public new DbSet<TEntity> Set<TEntity>()
            where TEntity : class
        {
            DbSet<TEntity> dbSet = this.DbSet<TEntity>();
            if (dbSet == null)
            {
                dbSet = base.Set<TEntity>();
                this.DbSets.Add(typeof(TEntity), dbSet);
            }

            return dbSet;
         }

        /// <summary>
        /// Set a repository
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <returns>IRepository</returns>
        public IRepository<TEntity> Entity<TEntity>()
            where TEntity : class
        {
            Type entityType = typeof(TEntity);
            IRepository<TEntity> repo;
            if (this.entities.TryGetValue(entityType, out repo))
            {
                return repo;
            }
            else
            {
                Repository<TEntity> repository = new Repository<TEntity>(this);
                this.entities.Add(entityType, repository);

                return repository;
            }
        }

        /// <summary>
        /// Initialize the database
        /// </summary>
        public void Initialize()
        {
            var dbExist = false;

            switch (this.DbSettings.Provider)
            {
                case ProviderType.SQLite:

                    ISqLiteDatabase sqliteSetting = (ISqLiteDatabase)DbSettings;
                    FileInfo fileInfo = new FileInfo(sqliteSetting.DataSource);
                    dbExist = File.Exists(sqliteSetting.DataSource) && fileInfo.Length != 0;
                    break;

                case ProviderType.MySQL:
                    dbExist = this.Database.Exists();
                    break;
            }

            if (!dbExist)
            {
                this.Database.Initialize(true);
                new DbInitializer().Seed(this);

            }
        }

        /// <summary>
        /// Method called during the model creation
        /// </summary>
        /// <param name="modelBuilder">DbModelBuilder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            DataBaseUtils.CreateModel(modelBuilder, this);
        }

        /// <summary>
        /// Get a dbSet from an entity
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>DbSet</returns>
        private DbSet<T> DbSet<T>()
            where T : class
        {
            Type entityType = typeof(T);
            DbSet<T> dbset;
            this.DbSets.TryGetValue(entityType, out dbset);
            return dbset;
        }
    }
}

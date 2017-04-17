using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.DbSettings.Interfaces;
using DataBase.Database.Repositories;
using DataBase.Database.Repositories.Interfaces;
using DataBase.Database.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;

namespace DataBase.Database.DbContexts
{
    /// <summary>
    /// Universal Context
    /// </summary>
    public class UniversalContext : DbContext, IDbContext
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


        private Dictionary<Type, IRepository> entities = new Dictionary<Type, IRepository>();

        /// <summary>
        /// Repositories of the context
        /// </summary>
        public Dictionary<Type, IRepository> Entities
        {
            get { return entities; }
        }

        /// <summary>
        /// Set the DbSets
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// Set a repository
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public Repository<TEntity> Entity<TEntity>() where TEntity : class
        {
            IRepository repo;
            if(entities.TryGetValue(typeof(TEntity), out repo)) {

                return (Repository<TEntity>)repo;

            } else
            {
                Repository<TEntity> repository = new Repository<TEntity>(this);
                entities.Add(typeof(TEntity), repository);

                return repository;
            }          
        }

        #region Constructors
        /// <summary>
        /// Create a database with a given provider and settings
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="option"></param>
        protected UniversalContext(DbConnection connection, bool option) : base(connection, option)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        protected UniversalContext(string connectionString) : base(connectionString) {

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

    }
}

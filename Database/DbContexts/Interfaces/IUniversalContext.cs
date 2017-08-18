using DataBase.Database.DbSettings.Interfaces;
using DataBase.Database.Repositories.Interfaces;
using System;
using System.Data.Entity;
using static DataBase.Database.Utils.GenericUtils;

namespace DataBase.Database.DbContexts.Interfaces
{
    /// <summary>
    /// Universal context interface
    /// </summary>
    public interface IUniversalContext :IDisposable
    {

        /// <summary>
        /// Database settings
        /// </summary>
        IDbSettings DbSettings { get; }

        /// <summary>
        /// List of DbSets
        /// </summary>
        GenericDictionary DbSets { get; }

        /// <summary>
        /// Enable or disable lazy loading mode
        /// </summary>
        bool EnableLazyLoading { get; set; }

        /// <summary>
        /// All the entities
        /// </summary>
        GenericDictionary Entities { get;  }

        /// <summary>
        /// Get a repository from a given entity or create it if not exists 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IRepository<TEntity> Entity<TEntity>() where TEntity : class;

        /// <summary>
        /// Give access to the Entity framework DbContext methods
        /// </summary>
        DbContext DbContext { get; }

        /// <summary>
        /// Manually initialize a context
        /// </summary>
        void Initialize();

    }
}

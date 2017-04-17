using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.Repositories;
using DataBase.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace DataBase.Database.DbContexts
{
    /// <summary>
    /// Global context
    /// </summary>
    public class GlobalContext
    {
        DbManager dbManager = DbManager.Instance;

        private const string nsp = "DbContexts";

        private List<IDbContext> contexts;

        private Dictionary<Type, IRepository> globalRepos;

        /// <summary>
        /// List of conntexts
        /// </summary>
        public List<IDbContext> Contexts
        {
            get { return contexts; }
            set { contexts = value; }
        }

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public GlobalContext()
        {
            contexts = new List<IDbContext>();
            globalRepos = new Dictionary<Type, IRepository>();       
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public GlobalRepository<TEntity> Entity<TEntity>() where TEntity : class
        {
            IRepository repo;
            if(globalRepos.TryGetValue(typeof(TEntity), out repo))
            {
                return (GlobalRepository<TEntity>)repo;
            } else
            {
                //Instanciation des repositories pour chacun des contexts
                var repoGlobal = new GlobalRepository<TEntity>(this);
                globalRepos.Add(typeof(TEntity), repoGlobal);

                return repoGlobal;
            }

        }

        /// <summary>
        /// Add a context
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public GlobalContext Add(IDbContext context)
        {
            contexts.Add(context);
            return this;
        }

    }
}



using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.DbSettings.Interfaces;
using DataBase.Database.Repositories;
using DataBase.Database.Repositories.Interfaces;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static DataBase.Database.Utils.GenericUtils;

namespace DataBase.Database.DbContexts
{
    /// <summary>
    ///  Global context 
    /// </summary>
    public class GlobalContext : IGlobalContext
    {
        DbManager dbManager = DbManager.Instance;

        private const string nsp = "DbContexts";

        private HashSet<IUniversalContext> contextList;

        private GenericDictionary globalRepos;

        /// <summary>
        /// List of conntexts
        /// </summary>
        public HashSet<IUniversalContext> ContextList
        {
            get { return contextList; }
            set { contextList = value; }
        }

        #region Constructor
        
        /// <summary>
        /// Global Context Constructor
        /// </summary>
        public GlobalContext()
        {
            contextList = new HashSet<IUniversalContext>();
            contexts = new Dictionary<IDbSettings, IUniversalContext>();
            globalRepos = new GenericDictionary();       
        }

        private Dictionary<IDbSettings, IUniversalContext> contexts;

        public Dictionary<IDbSettings, IUniversalContext> Contexts
        {
            get { return contexts; }
            set { contexts = value; }
        }

        #endregion

        /// <summary>
        /// Create or get a global repository from the given entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>A global repository</returns>
        public IGlobalRepository<TEntity> Entity<TEntity>() where TEntity : class
        {
            IGlobalRepository<TEntity> repo;
            if(globalRepos.TryGetValue(typeof(TEntity), out repo))
            {
                return (IGlobalRepository<TEntity>)repo;
            } else
            {
                //Instantiation des repositories pour chacun des contexts
                var repoGlobal = new GlobalRepository<TEntity>(this);
                globalRepos.Add(typeof(TEntity), repoGlobal);

                return repoGlobal;
            }
        }

        /// <summary>
        /// Add a context
        /// </summary>
        /// <param na[Testme="context"></param>
        /// <returns></returns>
        public IGlobalContext Add(IUniversalContext context)
        {
            contextList.Add(context);
            contexts.Add(context.DbSettings, context);
            return this;
        }

        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        /// <summary>
        /// Public implementation of Dispose pattern callable by consumers.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of Dispoe pattern.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                //foreach(var context in ContextList)
                //{
                //    context.DbContext.Dispose();
                //}

                handle.Dispose();
            }

            // Free any unmanaged objects here.
            disposed = true;
        }
    }
}



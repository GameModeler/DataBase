// <copyright file="GlobalContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DataBase.Database.DbContexts
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using DataBase.Database.DbContexts.Interfaces;
    using DataBase.Database.DbSettings.Interfaces;
    using DataBase.Database.Repositories;
    using DataBase.Database.Repositories.Interfaces;
    using Microsoft.Win32.SafeHandles;
    using static DataBase.Database.Utils.GenericUtils;

    /// <summary>
    ///  Global context 
    /// </summary>
    public class GlobalContext : IGlobalContext
    {
        private const string Nsp = "DbContexts";

        private DbManager dbManager = DbManager.Instance;

        private HashSet<IUniversalContext> contextList;

        private GenericDictionary globalRepos;

        private Dictionary<IDbSettings, IUniversalContext> contexts;

        // Flag: Has Dispose already been called?
        private bool disposed = false;

        // Instantiate a SafeHandle instance.
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalContext"/> class.
        /// </summary>
        public GlobalContext()
        {
            this.contextList = new HashSet<IUniversalContext>();
            this.contexts = new Dictionary<IDbSettings, IUniversalContext>();
            this.globalRepos = new GenericDictionary();
        }

        /// <summary>
        /// Gets or sets list of conntexts
        /// </summary>
        public HashSet<IUniversalContext> ContextList
        {
            get { return this.contextList; }
            set { this.contextList = value; }
        }

        /// <summary>
        /// Gets or sets 
        /// </summary>
        public Dictionary<IDbSettings, IUniversalContext> Contexts
        {
            get { return this.contexts; }
            set { this.contexts = value; }
        }

        /// <summary>
        /// Create or get a global repository from the given entity
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <returns>A global repository</returns>
        public IGlobalRepository<TEntity> Entity<TEntity>()
            where TEntity : class
        {
            IGlobalRepository<TEntity> repo;
            if (this.globalRepos.TryGetValue(typeof(TEntity), out repo))
            {
                return (IGlobalRepository<TEntity>)repo;
            }
            else
            {
                // Instantiation des repositories pour chacun des contexts
                var repoGlobal = new GlobalRepository<TEntity>(this);
                this.globalRepos.Add(typeof(TEntity), repoGlobal);

                return repoGlobal;
            }
        }

        /// <summary>
        /// Add a context
        /// </summary>
        /// <param name="context">the context to add</param>
        /// <returns>IGlobalContext</returns>
        public IGlobalContext Add(IUniversalContext context)
        {
            this.contextList.Add(context);
            this.contexts.Add(context.DbSettings, context);
            return this;
        }

        /// <summary>
        /// Public implementation of Dispose pattern callable by consumers.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of Dispoe pattern.
        /// </summary>
        /// <param name="disposing">the disposing state</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                // foreach(var context in ContextList)
                // {
                //    context.DbContext.Dispose();
                // }

                this.handle.Dispose();
            }

            // Free any unmanaged objects here.
            this.disposed = true;
        }
    }
}
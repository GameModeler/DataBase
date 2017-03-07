using GMDataBase.Database.DbSettings.Interface;
using GMDataBase.Interfaces;
using GMDataBase.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMDataBase.Database.DbSettings.DbClasses
{
    public abstract class DbSettings : IDbSettings
    {
        #region Database basic settings
        public string DatabaseName {get; set;}
        public string ConnectionString { get; set; }

        // Provider
        public ProviderType Provider { get; set; }
        #endregion

        #region Constructor

        /// <summary>
        /// Default settings from a given provider
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="provider"></param>
        //public DbSettings(string dbName, ProviderType provider)
        //{
        //    DatabaseName = dbName;
        //    Provider = provider;           
        //}
        #endregion
    }
}

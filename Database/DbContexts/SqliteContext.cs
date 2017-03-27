using DataBase.Database.DbContexts.Interface;
using DataBase.Database.DbSettings.Interface;
using DataBase.Utils;
using System.Data.Entity;
using System.Data.SQLite;

namespace DataBase.Database.DbContexts
{
    [DbConfigurationType(typeof(SqliteConfiguration))]
    class SqliteContext<TEntity> : DbContextBase<TEntity>, IDbContexts where TEntity : class
    {

        private ProviderType provider;

        public ProviderType Provider
        {
            get { return provider; }
            set { provider = ProviderType.MySQL; }
        }

        #region Constructor

        /// <summary>
        /// Construcor
        /// </summary>
        /// <param name="settings"></param>
        public SqliteContext(IDbSettings settings) : base(new SQLiteConnection() { ConnectionString = ConnectionStringBuilder.BuildConnectionString(ProviderType.SQLite, settings) }, true)
        {
        }

        #endregion
    }
}
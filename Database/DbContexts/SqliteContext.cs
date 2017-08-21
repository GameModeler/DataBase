using System.Data.Entity;
using System.Data.SQLite;
using SQLite.CodeFirst;
using DataBase.Database.DbContexts.Configurations;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.Utils;
using DataBase.Database.DbSettings.Interfaces;

namespace DataBase.Database.DbContexts
{
    /// <summary>
    /// SqLite context
    /// </summary>
    [DbConfigurationType(typeof(SqliteConfiguration))]
    public class SqLiteContext : UniversalContext, IProvider
    {
        private ProviderType provider;
        /// <summary>
        /// The Provider
        /// </summary>
        public ProviderType Provider
        {
            get { return provider; }
            set { provider = ProviderType.SQLite; }
        }

        #region Constructor
        
        /// <summary>
        /// Construcor
        /// </summary>
        /// <param name="settings"></param>
        public SqLiteContext(IDbSettings settings) : base(new SQLiteConnection() { ConnectionString = ConnectionStringBuilder.BuildConnectionString(ProviderType.SQLite, settings) }, true)
        {
            dbSettings = settings;
            dbManager.ApplicationContexts.Add(this);
            Initialize();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="connection"></param>
        public SqLiteContext(IDbSettings settings, SQLiteConnection connection) : base(connection, true)
        {
            dbSettings = settings;
            dbManager.ApplicationContexts.Add(this);
            Initialize();
        }
        #endregion

        /// <summary>
        /// Method called during the creation of the model
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<SqLiteContext>(modelBuilder);
            System.Data.Entity.Database.SetInitializer(sqliteConnectionInitializer);

            DataBaseUtils.CreateModel(modelBuilder, this);
        }
    }
}

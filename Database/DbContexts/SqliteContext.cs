namespace DataBase.Database.DbContexts
{
    using System.Data.Entity;
    using System.Data.SQLite;
    using DataBase.Database.DbContexts.Configurations;
    using DataBase.Database.DbContexts.Interfaces;
    using DataBase.Database.Utils;
    using DbSettings.Interfaces;
    using SQLite.CodeFirst;

    /// <summary>
    /// SqLite context
    /// </summary>
    [DbConfigurationType(typeof(SqliteConfiguration))]
    public class SqLiteContext : UniversalContext, IProvider
    {
        private ProviderType provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqLiteContext"/> class.
        /// </summary>
        /// <param name="settings">The database settings</param>
        public SqLiteContext(IDbSettings settings)
            : base(new SQLiteConnection() { ConnectionString = ConnectionStringBuilder.BuildConnectionString(ProviderType.SQLite, settings) }, true)
        {
            this.dbSettings = settings;
            this.dbManager.ApplicationContexts.Add(this);
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqLiteContext"/> class.
        /// </summary>
        /// <param name="settings">The database settings</param>
        /// <param name="connection">The Sqlite connection</param>
        public SqLiteContext(IDbSettings settings, SQLiteConnection connection)
            : base(connection, true)
        {
            this.dbSettings = settings;
            this.dbManager.ApplicationContexts.Add(this);
            this.Initialize();
        }

        /// <summary>
        /// Gets or sets the Provider
        /// </summary>
        public ProviderType Provider
        {
            get { return this.provider; }
            set { this.provider = ProviderType.SQLite; }
        }

        /// <summary>
        /// Method called during the creation of the model
        /// </summary>
        /// <param name="modelBuilder">DbModelBuilder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<SqLiteContext>(modelBuilder);
            System.Data.Entity.Database.SetInitializer(sqliteConnectionInitializer);

            DataBaseUtils.CreateModel(modelBuilder, this);
        }
    }
}

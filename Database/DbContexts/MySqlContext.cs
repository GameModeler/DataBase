using DataBase.Utils;
using MySql.Data.MySqlClient;
using System.Data.Entity;
using DataBase.Database.DbContexts.Interface;
using DataBase.Database.DbSettings.Interface;

namespace DataBase.Database.DbContexts
{
    /// <summary>
    /// The MySql Database Context
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    [DbConfigurationType(typeof(MySQLConfiguration))]
    public partial class MySqlContext<TEntity> : DbContextBase<TEntity>, IDbContexts where TEntity : class
    {

        private ProviderType provider;
        /// <summary>
        /// The Provider
        /// </summary>
        public ProviderType Provider
        {
            get { return provider; }
            set { provider = ProviderType.MySQL; }
        }

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings"></param>
        public MySqlContext(IDbSettings settings) : base(new MySqlConnection(ConnectionStringBuilder.BuildConnectionString(ProviderType.MySQL, settings)), true)
        {
        }
        #endregion

        /// <summary>
        /// Method called during the model creation
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            DataBaseUtils.CreateModel(modelBuilder);
        }
    }
}

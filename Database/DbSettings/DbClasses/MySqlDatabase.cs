using GMDataBase.Database.DbSettings.Interface;
using GMDataBase.Utils;

namespace GMDataBase.Database.DbSettings.DbClasses
{
    public class MySqlDatabase : IDbSettings
    {
        #region Database basic settings
        public string DatabaseName { get; set; }
        public string Server { get; set; }
        public string Port { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string ConnectionString { get; set; }

        // Provider
        private ProviderType provider;

        public ProviderType Provider
        {
            get { return provider; }
            set { provider = ProviderType.MySQL; }
        }


        #endregion

        #region Constructor

        public MySqlDatabase() {

        }
        /// <summary>
        /// Constructor
        /// All params are optionals
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="port"></param>
        /// <param name="server"></param>
        /// <param name="connectionString"></param>
        public MySqlDatabase(
                          string databaseName = "",
                          string userId = "",
                          string password = "",
                          string port = "",
                          string server = "",
                          string connectionString = "")
        {
            DatabaseName = databaseName;
            Port = port;
            UserId = userId;
            Password = password;
            Port = port;
            Server = server;
            ConnectionString = "";

            GmDbManager.Instance.Databases.Add(DatabaseName, this);
        }

        /// <summary>
        /// Default settings from a given provider
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="provider"></param>
        public MySqlDatabase(string dbName)
        {
            MySqlDbDefault(dbName);
            GmDbManager.Instance.Databases.Add(DatabaseName, this);

        }
        #endregion

        /// <summary>
        /// MySQL default database settings
        /// </summary>
        /// <param name="dbName"></param>
        private void MySqlDbDefault(string dbName)
        {
            DatabaseName = dbName;
            Server = "localhost";
            Port = "";
            UserId = "root";
            Password = "";
            ConnectionString = "";

        }
    }
}

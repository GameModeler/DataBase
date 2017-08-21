using DataBase.Database.Utils;

namespace DataBase.Database.DbSettings.Interfaces
{
    /// <summary>
    /// General database settings interface
    /// </summary>
    public interface IDbSettings
    {
        /// <summary>
        /// Name of the database
        /// </summary>
        string DatabaseName { get; set; }

        /// <summary>
        /// Database connection string
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Database provider
        /// </summary>
        ProviderType Provider { get; }

        /// <summary>
        /// Build the database connection string
        /// </summary>
        /// <returns></returns>
        string ToConnectionString();

    }
}

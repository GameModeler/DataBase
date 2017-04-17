using DataBase.Database.DbSettings.Interfaces;
using DataBase.Database.Utils;

namespace DataBase.Database.Interfaces
{
    /// <summary>
    /// Connection string builder Interface
    /// </summary>
    public interface IConnectionStringBuilder
    {
        /// <summary>
        /// Configure the provider
        /// </summary>
        /// <param name="provider"></param>
        void ConfigPorvider(ProviderType provider);

        /// <summary>
        /// Build the connection string from the provider
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        string ProviderConnectionString(ProviderType provider, IDbSettings settings);
    }
}

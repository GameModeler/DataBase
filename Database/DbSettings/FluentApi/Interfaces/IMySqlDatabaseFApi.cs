using DataBase.Database.DbSettings.DbClasses;
using System;

namespace DataBase.Database.DbSettings.FluentApi.Interfaces
{
    /// <summary>
    /// MySql Fluent Interface
    /// </summary>
    public interface IMySqlDatabaseFApi : IDbSettingsFApi
    {
        /// <summary>
        /// Server adresse
        /// </summary>
        IMySqlDatabaseFApi Server(String server);

        /// <summary>
        /// Server's port
        /// </summary>
        IMySqlDatabaseFApi Port(int port);

        /// <summary>
        /// User's id
        /// </summary>
        IMySqlDatabaseFApi UserId(string userId);

        /// <summary>
        /// User's password
        /// </summary>
        IMySqlDatabaseFApi Password(string password);

        /// <summary>
        /// Convert to MySqlDatabse type
        /// </summary>
        MySqlDatabase ToMySqlDatabase { get; }

    }
}

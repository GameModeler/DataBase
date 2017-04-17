using System;

namespace DataBase.Database.DbSettings.FluentApi.Interfaces
{
    /// <summary>
    /// MySql Fluent Interface
    /// </summary>
    public interface IMySqlDatabaseFApi
    {
        /// <summary>
        /// Server adresse
        /// </summary>
        MySqlDatabaseFApi Server(String server);

        /// <summary>
        /// Server's port
        /// </summary>
        MySqlDatabaseFApi Port(int port);

        /// <summary>
        /// User's id
        /// </summary>
        MySqlDatabaseFApi UserId(string userId);

        /// <summary>
        /// User's password
        /// </summary>
        MySqlDatabaseFApi Password(string password);

    }
}

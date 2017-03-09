using DataBase.Database.DbSettings.DbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Database.DbSettings.FluentInterface
{
    /// <summary>
    /// MySql Fluent Interface
    /// </summary>
    public interface IMySqlDatabaseFI
    {
        /// <summary>
        /// Server adresse
        /// </summary>
        MySqlDatabaseFI Server(String server);

        /// <summary>
        /// Server's port
        /// </summary>
        MySqlDatabaseFI Port(int port);

        /// <summary>
        /// User's id
        /// </summary>
        MySqlDatabaseFI UserId(string userId);

        /// <summary>
        /// User's password
        /// </summary>
        MySqlDatabaseFI Password(string password);

    }
}

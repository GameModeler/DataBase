using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Interfaces
{
    public interface IDbSettings
    {
        string DatabaseName { get; set; }
        string Server { get; set; }
        string Port { get; set; }
        string UserId { get; set; }
        string Password { get; set; }
        string ConfigConnectionStringName { get; set; }
        string ConnectionString { get; set; }

        string GetCrible(string crible);

        void MySqlSettings(string dbName);
        void SQLiteSettings(string dbName);
        void LocalDbSettings(string dbName);       
    }
}

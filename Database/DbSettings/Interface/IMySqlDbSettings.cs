using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMDataBase.Database.DbSettings.Interface
{
    interface IMySqlDbSettings
    {
        string DatabaseName { get; set; }
        string Server { get; set; }
        string Port { get; set; }
        string UserId { get; set; }
        string Password { get; set; }
        string ConnectionString { get; set; }
    }
}

using DataBase.Database;
using DataBase.Utils;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Database
{
    public class MySQLConfiguration : DbConfiguration
    {
        public MySQLConfiguration()
        {
            SetProviderServices(MySqlProviderInvariantName.ProviderName, new MySqlProviderServices());
        }
    }
}

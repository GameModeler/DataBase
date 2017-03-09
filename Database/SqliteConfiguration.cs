using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite.EF6;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Database
{
    public class SqliteConfiguration : DbConfiguration
    {
        public SqliteConfiguration()
        {
            SetProviderServices("System.Data.SQLite.EF6", (DbProviderServices)SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices)));
        }
    }
}

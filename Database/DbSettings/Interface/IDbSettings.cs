using GMDataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMDataBase.Database.DbSettings.Interface
{
    public interface IDbSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
        ProviderType Provider { get; }

    }
}

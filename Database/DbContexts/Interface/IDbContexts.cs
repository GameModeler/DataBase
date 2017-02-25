using GMDataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMDataBase.Database.DbContexts.Interface
{
    public interface IDbContexts
    {
        ProviderType Provider { get; set; }
    }
}

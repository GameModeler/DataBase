using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Database.DbContexts.Interface
{
    public interface IDbContexts
    {
        ProviderType Provider { get; set; }
    }
}

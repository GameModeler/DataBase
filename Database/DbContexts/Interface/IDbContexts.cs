using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Database.DbContexts.Interface
{
    /// <summary>
    /// DbContext interface
    /// </summary>
    public interface IDbContexts
    {
        /// <summary>
        /// Provider
        /// </summary>
        ProviderType Provider { get; set; }

    }
}

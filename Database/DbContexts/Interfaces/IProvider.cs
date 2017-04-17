using DataBase.Database.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Database.DbContexts.Interfaces
{
    /// <summary>
    /// Provider interface
    /// </summary>
    public interface IProvider
    {
        /// <summary>
        /// Provider
        /// </summary>
        ProviderType Provider { get; set; }
    }
}

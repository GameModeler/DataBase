using GMDataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMDataBase.Interfaces
{
    public interface IConnectionStringBuilder
    {
        void ConfigPorvider(ProviderType provider);
        string ProviderConnectionString(ProviderType provider, IDbSettings settings);
    }
}

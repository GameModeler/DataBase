﻿using GMDataBase.Database.DbSettings.Interface;
using GMDataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMDataBase.Interfaces
{
    public interface IDatabaseManager<T>
    {

        ProviderType Provider { get; }

        T GetDatabase(String dbName);

        IEnumerable<T> SearchInDatabases(String crible, String value);
        
        Dictionary<string, IDbSettings> Databases { get;  }

        void SetProvider(ProviderType provider);
    }
}

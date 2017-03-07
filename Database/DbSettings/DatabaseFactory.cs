using GMDataBase.Database.DbSettings.Interface;
using GMDataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GMDataBase.Database.DbSettings
{
    public class DatabaseFactory
    {
        private const string nsp = "DbClasses";

        public static T DatabaseSettings<T>(string dnName) where T : IDbSettings, new()
        {
            T obj = new T();
            obj.DatabaseName = dnName;

            GmDbManager.Instance.Databases.Add(obj.DatabaseName, obj);

            return obj;
        }
    }
}

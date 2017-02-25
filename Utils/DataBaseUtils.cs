using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using GMDataBase.Interfaces;
using System.Data.Entity;
using GMDataBase.Database.DbSettings.Interface;

namespace GMDataBase.Utils
{
    public static class DataBaseUtils
    {
       /// <summary>
       /// Check if an object have all of its string properties empty
       /// </summary>
       /// <param name="myObject"></param>
       /// <returns>true if all of the object's string properties are empty,
       /// false otherwisse</returns>
        public static bool IsAllNullOrEmpty(IDbSettings myObject)
        {
            return !myObject.GetType().GetProperties()
                .Where(pi => pi.GetValue(myObject) is string)
                .Select(pi => (string)pi.GetValue(myObject))
                .Any(value => !string.IsNullOrEmpty(value));
        }

        public static string BuildDataSource(string server, string port)
        {
            return port != null ? server + ":" + port : server;
        }

        public static void CreateModel(DbModelBuilder modelBuilder)
        {
            var entityMethod = typeof(DbModelBuilder).GetMethod("Entity");

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var entityTypes = assembly
                  .GetTypes()
                  .Where(t =>
                    t.GetCustomAttributes(typeof(PersistentAttribute), inherit: true)
                    .Any());

                foreach (var type in entityTypes)
                {
                    entityMethod.MakeGenericMethod(type)
                      .Invoke(modelBuilder, new object[] { });
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using DataBase.Database.DbSettings.Interfaces;

namespace DataBase.Database.Utils
{
    /// <summary>
    ///  Util class for the DataBase Module
    /// </summary>
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

        /// <summary>
        /// Concatene database' server and port
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static string BuildDataSource(string server, string port)
        {
            return port != null ? server + ":" + port : server;
        }

        /// <summary>
        /// Create a model from a DbModelBuilder
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="context"></param>
        public static void CreateModel(DbModelBuilder modelBuilder, DbContext context)
        {
            var entityMethod = typeof(DbModelBuilder).GetMethod("Entity");

            var dbname = context.Database.Connection.Database;

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var entityTypes = assembly
                  .GetTypes()
                  .Where(t =>
                    t.GetCustomAttributes(typeof(PersistentAttribute), inherit: true)
                    .Any());

                foreach (var type in entityTypes)
                {
                    List<string> dbNames = GetAttribute(type);

                    if(dbNames.Count == 0 || dbNames.Contains(dbname)) {
                        entityMethod.MakeGenericMethod(type)
                        .Invoke(modelBuilder, new object[] { });
                    }
                }
            }
        }

        /// <summary>
        /// Gets all entities associated with a database
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static List<Type> GetEntities(IDbSettings settings)
        {
            string dbname = settings.DatabaseName;

            List<Type> entities = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var entityTypes = assembly
                  .GetTypes()
                  .Where(t =>
                    t.GetCustomAttributes(typeof(PersistentAttribute), inherit: true)
                    .Any());

                foreach (var type in entityTypes)
                {
                    List<string> dbNames = GetAttribute(type);

                    if (dbNames.Count == 0 || dbNames.Contains(dbname))
                    {
                        entities.Add(type);
                    }
                }
            }
            return entities;
        }

        /// <summary>
        /// Rename the key of a dictionary
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="fromKey"></param>
        /// <param name="toKey"></param>
        public static void UpdateKey<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey fromKey, TKey toKey)
        {
            TValue value = dic[fromKey];
            dic.Remove(fromKey);
            dic[toKey] = value;
        }

        public static List<string> GetAttribute(Type t)
        {
            // Get instance of the attribute.
            PersistentAttribute persistantAttributes =
                (PersistentAttribute)Attribute.GetCustomAttribute(t, typeof(PersistentAttribute));

            return persistantAttributes.DbNames.ToList<string>();

        }
    }
}

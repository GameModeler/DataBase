namespace DataBase.Database.Utils
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Text.RegularExpressions;
    using DataBase.Database.DbContexts;
    using DataBase.Database.DbSettings.Interfaces;

    /// <summary>
    ///  Util class for the DataBase Module
    /// </summary>
    public static class DataBaseUtils
    {
        /// <summary>
        /// Check if an object have all of its string properties empty
        /// </summary>
        /// <param name="myObject">Database settings</param>
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
        /// <param name="server">the server name</param>
        /// <param name="port">the port</param>
        /// <returns>dataSource string</returns>
        public static string BuildDataSource(string server, string port)
        {
            return port != null ? server + ":" + port : server;
        }

        /// <summary>
        /// Create a model from a DbModelBuilder
        /// </summary>
        /// <param name="modelBuilder">The model builder</param>
        /// <param name="context">The context</param>
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
                    entityMethod.MakeGenericMethod(type)
                       .Invoke(modelBuilder, new object[] { });
                }
            }
        }

        /// <summary>
        /// Rename the key of a dictionary
        /// </summary>
        /// <typeparam name="TKey">The Key</typeparam>
        /// <typeparam name="TValue">The Value</typeparam>
        /// <param name="dic">dic</param>
        /// <param name="fromKey">fromKey</param>
        /// <param name="toKey">toKey</param>
        public static void UpdateKey<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey fromKey, TKey toKey)
        {
            TValue value = dic[fromKey];
            dic.Remove(fromKey);
            dic[toKey] = value;
        }

        /// <summary>
        /// Get the Persistance Attributes values
        /// </summary>
        /// <param name="t">The type</param>
        /// <returns>List of entity with the persistance attribute</returns>
        public static List<string> GetPersistanceAttribute(Type t)
        {
            // Get instance of the attribute.
            PersistentAttribute persistantAttributes =
                (PersistentAttribute)Attribute.GetCustomAttribute(t, typeof(PersistentAttribute));

            return persistantAttributes.DbNames.ToList<string>();

            // List<PersistentAttribute> persistanceAttr = GenericUtils.GetAttribute<PersistentAttribute>(t);
            // return (persistanceAttr.Count > 0) ? persistanceAttr[0].DbNames.ToList<string>() : null;
        }

        /// <summary>
        /// Gets the Table Attributes values
        /// </summary>
        /// <param name="t">The type</param>
        /// <returns>The table value attribute</returns>
        public static string GetTableAttribute(Type t)
        {
            List<TableAttribute> tableAttr = GenericUtils.GetAttribute<TableAttribute>(t);
            return (tableAttr.Count > 0) ? tableAttr[0].Name : null;
        }

        /// <summary>
        /// Gets the table from a context
        /// </summary>
        /// <param name="context">The context</param>
        /// <returns>The list of tables</returns>
        public static List<EntitySet> GetTables(UniversalContext context)
        {
            var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

            var tables = metadata.GetItemCollection(DataSpace.SSpace)
                .GetItems<EntityContainer>()
                .Single()
                .BaseEntitySets
                .OfType<EntitySet>()
                .Where(s => !s.MetadataProperties.Contains("Type")
                || s.MetadataProperties["Type"].ToString() == "Tables");

            return tables.Where(tab => tab.MetadataProperties.Contains("Table")).ToList();
        }

        /// <summary>
        /// Split a camel case string
        /// </summary>
        /// <param name="input">string input</param>
        /// <returns>The list of words</returns>
        public static List<string> SplitCamelCase(string input)
        {
            var result = Regex.Replace(input, "([a-z](?=[A-Z0-9])|[A-Z](?=[A-Z][a-z]))", "$1 ");
            return result.Split(' ').ToList<string>();
        }
    }
}

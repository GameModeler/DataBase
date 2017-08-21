using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using DataBase.Database.DbSettings.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.DbContexts;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Text.RegularExpressions;

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
                    entityMethod.MakeGenericMethod(type)
                       .Invoke(modelBuilder, new object[] { });
                }
            }
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

        /// <summary>
        /// Get the Persistance Attributes values
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<string> GetPersistanceAttribute(Type t)
        {
            // Get instance of the attribute.
            PersistentAttribute persistantAttributes =
                (PersistentAttribute)Attribute.GetCustomAttribute(t, typeof(PersistentAttribute));

            return persistantAttributes.DbNames.ToList<string>();

            //List<PersistentAttribute> persistanceAttr = GenericUtils.GetAttribute<PersistentAttribute>(t);
            //return (persistanceAttr.Count > 0) ? persistanceAttr[0].DbNames.ToList<string>() : null;
        }

        /// <summary>
        /// Get the Table Attributes values
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetTableAttribute(Type t)
        {
            List<TableAttribute> tableAttr = GenericUtils.GetAttribute<TableAttribute>(t);
            return (tableAttr.Count > 0) ? tableAttr[0].Name : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static List<EntitySet> getTables(UniversalContext context)
        {
            //List<string> tablesNames = new List<string>();

            var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

            var tables = metadata.GetItemCollection(DataSpace.SSpace)
                .GetItems<EntityContainer>()
                .Single()
                .BaseEntitySets
                .OfType<EntitySet>()
                .Where(s => !s.MetadataProperties.Contains("Type")
                || s.MetadataProperties["Type"].ToString() == "Tables");

            return tables.Where(tab => tab.MetadataProperties.Contains("Table")).ToList();

            //foreach (var table in tables)
            //{
            //    var tableName = table.MetadataProperties.Contains("Table")
            //        && table.MetadataProperties["Table"].Value != null
            //        ? table.MetadataProperties["Table"].Value.ToString()
            //        : table.Name;

            //    tablesNames.Add(tableName);

            //    //var tableSchema = table.MetadataProperties["Schema"].Value.ToString();

            //    //Console.WriteLine(tableSchema + "." + tableName);
            //}

            //return tablesNames;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<string> SplitCamelCase(string input)
        {
            var result = Regex.Replace(input, "([a-z](?=[A-Z0-9])|[A-Z](?=[A-Z][a-z]))", "$1 ");
            return result.Split(' ').ToList<string>();
        }
        //switch(context.DbSettings.Provider)
        //{
        //    case ProviderType.MySQL:

        //        var result = context.Database.SqlQuery<string>("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_SCHEMA=@p0", dbName.ToLower()).ToList<string>(); ;

        //        break;

        //    case ProviderType.SQLite:

        //        var result2 = context.Database.ExecuteSqlCommand("SELECT name FROM sqlite_master WHERE type='table'");

        //        break;
        //}


    }
}

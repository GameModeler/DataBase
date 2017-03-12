using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using DataBase.Database.DbSettings.Interface;
using System.Threading.Tasks;

namespace DataBase.Utils
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
        /// Wait for DbSet.Local to be defined
        /// </summary>
        /// <param name="data"></param>
        //public static void waitFor(object data)
        //{

        //    try
        //    {
        //        var exist = data;
        //    }
        //    catch (Exception e)
        //    {
        //        var ex = e;
        //        Task task = waitData(data);
        //        task.Wait();
        //    }
        //}

        //private static async Task<object> waitData(object data)
        //{
        //    object result = null;
        //    await Task.Factory.StartNew(() =>
        //    {
        //        result = data;
        //    });

        //    return result;
        //}

        /// <summary>
        /// Wait for DbSet.Local to be defined
        /// </summary>
        /// <returns></returns>
        //public async Task<bool> waitForLocal()
        //{
        //    try
        //    {
        //        var localType = DbSetT.Local;
        //    }
        //    catch (Exception e)
        //    {
        //        var ex = e;
        //        Task task = waitLocal();
        //        task.Wait();
        //    }


        //    return true;
        //}

        //public async Task<ObservableCollection<TEntity>> waitLocal()
        //{
        //    ObservableCollection<TEntity> result = null;
        //    await Task.Factory.StartNew(() =>
        //    {
        //        result = DbSetT.Local;

        //    });

        //    return result;
        //}
    }
}

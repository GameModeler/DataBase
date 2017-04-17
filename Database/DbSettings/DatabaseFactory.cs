using DataBase.Database.DbSettings.DbClasses;
using DataBase.Database.DbSettings.Interfaces;

namespace DataBase.Database.DbSettings
{
    /// <summary>
    /// Database facotry
    /// </summary>
    public class DatabaseFactory
    {
        /// <summary>
        /// Sets a new Database settings from a IDbSettings
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dnName"></param>
        /// <returns></returns>
        public static T DatabaseSettings<T>(string dnName) where T : IDbSettings, new()
        {
            T obj = new T();
            obj.DatabaseName = dnName;
            return obj;
        }

        /// <summary>
        /// Sets a MySql database
        /// </summary>
        public static MySqlDatabase MySqlDb
        {
            get { return new MySqlDatabase(); }
        }

        /// <summary>
        /// Sets a SqLite database
        /// </summary>
        public static SqLiteDatabase SqLiteDb
        {
            get { return new SqLiteDatabase(); }
        }

    }
}

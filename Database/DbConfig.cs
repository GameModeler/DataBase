using DataBase.Interfaces;
using DataBase.Utils;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;
using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Database
{
    /// <summary>
    /// Used to set provider configuration and connection string building
    /// This class must be inherited from; you cannot instantiate it directly.
    /// </summary>
    public class DbConfig
    {

        public string GetDefaultDbName(int nb)
        {
            return Assembly.GetExecutingAssembly().GetName().Name + "_" + nb.ToString();
        }

        /// <summary>
        /// Set the database provider
        /// </summary>
        /// <param name="provider"></param>
        //public void ConfigPorvider(ProviderType provider)
        //{
        //    switch (provider)
        //    {
        //        case ProviderType.MySQL:
        //            try
        //            {
        //                DbConfiguration.SetConfiguration(new MySqlEFConfiguration());

        //            }
        //            catch (InvalidOperationException e)
        //            {
        //                Console.WriteLine(e);
        //                Log un provider a dejà été configuré
        //            }

        //            DatabaseManager.Instance.SetProvider(provider);

        //            break;
        //        case ProviderType.SQLite:
        //            try
        //            {
        //                SetProviderServices(provider.GetStringValue(), System.Data.Entity.SqlServer.SqlProviderServices.Instance);
        //            }
        //            catch (InvalidOperationException e)
        //            {
        //                Console.WriteLine(e);
        //                Log un provider a dejà été configuré
        //            }

        //            DatabaseManager.Instance.SetProvider(provider);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        /// <summary>
        /// Set the database connexion string
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public string ProviderConnectionString(ProviderType provider, IDbSettings settings)
        {
            if(string.IsNullOrEmpty(settings.ConnectionString))
            {           
                switch (provider)
                {
                    case ProviderType.MySQL:
                        return BuildMySQLConnectionString(settings);
                    case ProviderType.SQLite:
                        return BuildSQLiteConnectionString(settings);
                    default:
                        return BuildSQLConnectionString(settings);
                }           
            } else {                           
                return settings.ConnectionString;
            }
        }


        /// <summary>
        /// Build MySQL connection string
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public string BuildMySQLConnectionString(IDbSettings settings)
        {
            // Initialize the connection string builder for the
            // underlying provider.
            MySqlConnectionStringBuilder mysqlBuilder = new MySqlConnectionStringBuilder();

            if (!string.IsNullOrEmpty(settings.DatabaseName))
            {
                mysqlBuilder.Database = settings.DatabaseName;
            }
            if (!string.IsNullOrEmpty(settings.Server))
            {
                mysqlBuilder.Server = settings.Server;
            }
            if (!string.IsNullOrEmpty(settings.UserId))
            {
                mysqlBuilder.UserID = settings.UserId;
            }
            if (!string.IsNullOrEmpty(settings.Password))
            {
                mysqlBuilder.Password = settings.Password;
            }
            if (!string.IsNullOrEmpty(settings.Port))
            {
                mysqlBuilder.Port = Convert.ToUInt32(settings.Port, 16);
            }

            return mysqlBuilder.ToString();
        }

        /// <summary>
        /// Build SQL connection string
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public string BuildSQLConnectionString(IDbSettings settings)
        {
            // Initialize the connection string builder for the
            // underlying provider.
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();

            if (!string.IsNullOrEmpty(settings.DatabaseName))
            {
                sqlBuilder.InitialCatalog = settings.DatabaseName;
            }
            if (!string.IsNullOrEmpty(settings.Server))
            {
                sqlBuilder.DataSource = DataBaseUtils.BuildDataSource(settings.Server, settings.Port);
            }
            if (!string.IsNullOrEmpty(settings.UserId))
            {
                sqlBuilder.UserID = settings.UserId;
            }
            if (!string.IsNullOrEmpty(settings.Password))
            {
                sqlBuilder.Password = settings.Password;
            }

            sqlBuilder.IntegratedSecurity = true;

            sqlBuilder.MultipleActiveResultSets = true;

            // Build the SqlConnection connection string.
            return sqlBuilder.ToString();
        }

        /// <summary>
        /// Build SQLite connection string
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public string BuildSQLiteConnectionString(IDbSettings settings)
        {
            // Initialize the connection string builder for the
            // underlying provider.

            SQLiteConnectionStringBuilder sqlBuilder = new SQLiteConnectionStringBuilder();

            
           
            if (!string.IsNullOrEmpty(settings.DatabaseName))
            {
                sqlBuilder.DataSource = settings.DatabaseName;
            }
            if (!string.IsNullOrEmpty(settings.Server))
            {
                sqlBuilder.DataSource = settings.Server;
            }
            if (!string.IsNullOrEmpty(settings.Password))
            {
                sqlBuilder.Password = settings.Password;
            }

            //sqlBuilder.Version = 3;
            //sqlBuilder.

            //sqlBuilder.ForeignKeys = true;

            //sqlBuilder.


            // Build the SqlConnection connection string.
            return sqlBuilder.ToString();
        }
    }
}

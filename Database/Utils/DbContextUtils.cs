using DataBase.Database.DbContexts;
using DataBase.Database.DbContexts.Initializer;
using DataBase.Database.DbSettings.DbClasses;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;

namespace DataBase.Database.Utils
{
    /// <summary>
    /// DbContextUtils
    /// </summary>
    public class DbContextUtils
    {
        /// <summary>
        /// Initialize Database
        /// </summary>
        /// <param name="context"></param>
        public static void InitializeDatabase(UniversalContext context)
        {
            var dbExist = false;

            switch (context.DbSettings.Provider)
            {
                case ProviderType.SQLite:

                    SqLiteDatabase sqliteSetting = (SqLiteDatabase)context.DbSettings;
                    FileInfo fileInfo = new FileInfo(sqliteSetting.DataSource);
                    dbExist = (File.Exists(sqliteSetting.DataSource) && fileInfo.Length != 0);             
                    break;

                case ProviderType.MySQL:
                    dbExist = context.Database.Exists();
                    break;
            }

            if (!dbExist)
            {
                context.Database.Initialize(true);
                new DbInitializer().Seed(context);
            }
        }
    }
} 


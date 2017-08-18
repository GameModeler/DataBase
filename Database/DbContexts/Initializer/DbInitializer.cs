using DataBase.Database.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace DataBase.Database.DbContexts.Initializer
{
    /// <summary>
    /// Initialize the databases
    /// </summary>
    public class DbInitializer
    {
        /// <summary>
        /// Do Initialize the datatabases
        /// </summary>
        /// <param name="context"></param>
        public void Seed(UniversalContext context)
        {
            SeedDatabases(context);
        }

        /// <summary>
        /// Delete database tables that not part of the database 
        /// and create the repositories for the entities
        /// </summary>
        /// <param name="context"></param>
        private void SeedDatabases(UniversalContext context)
        {
            List<string> tablesNames = new List<string>();
            List<string> entitiesNames = new List<string>();

            // Database name
            var dbname = context.DbSettings.DatabaseName;

            // Get all tables names
            List<EntitySet> dbTables =  DataBaseUtils.getTables(context);       

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var entityTypes = assembly
                  .GetTypes()
                  .Where(t =>
                    t.GetCustomAttributes(typeof(PersistentAttribute), inherit: true)
                    .Any());          

                foreach (var type in entityTypes)
                {
                    // 1. Get the Persistance Attribute
                    List<string> dbNames = DataBaseUtils.GetPersistanceAttribute(type);
                    
                    // Get table name
                    var table = dbTables.Find(tb => tb.Name == type.Name);

                    if (dbNames.Count > 0 && !dbNames.Contains(dbname))
                    {
                        // Add table name into array
                        tablesNames.Add(table.Table);
                        entitiesNames.Add(type.Name);
                    } else
                    {
                        // Create repo for each entity
                        GenericUtils.CallMathodByReflection(typeof(UniversalContext), "Entity", type, context);
                    }

                    dbTables.Remove(table);
                } 
            }

            // Delete mapping tables
            if (dbTables.Count > 0)
            {

                foreach (var fkTable in dbTables)
                {
                    var entityNames = DataBaseUtils.SplitCamelCase(fkTable.Name);

                    if (entityNames.Count == 2 && entitiesNames.Contains(entityNames[0])
                                              && entitiesNames.Contains(entityNames[1]))
                    {
                        // suppression de la table
                        tablesNames.Add(fkTable.Table);

                    }
                }
            }

            // Delete tables
            if (tablesNames.Count > 0)
            {
                switch(context.DbSettings.Provider)
                {

                    case ProviderType.MySQL:

                        var tablesNamesStr = string.Join(",", tablesNames.ToArray());
                        context.Database.ExecuteSqlCommand("SET FOREIGN_KEY_CHECKS=0;DROP TABLE IF EXISTS " + tablesNamesStr + ";SET FOREIGN_KEY_CHECKS=1;");
                        break;

                    case ProviderType.SQLite:

                        foreach (var table in tablesNames)
                        {
                            context.Database.ExecuteSqlCommand("DROP TABLE IF EXISTS " + table);
                        }

                        break;
                }               
            }   
        }
    }
}

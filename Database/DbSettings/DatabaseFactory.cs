// <copyright file="DatabaseFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DataBase.Database.DbSettings
{
    using System;
    using System.Collections.Generic;
    using DbClasses;
    using DbContexts;
    using DbContexts.Interfaces;
    using Interfaces;
    using Utils;

    /// <summary>
    /// Database facotry
    /// </summary>
    public class DatabaseFactory
    {

        private const string Nsp = "DbContexts";

        /// <summary>
        /// Gets a MySql database
        /// </summary>
        public static MySqlDatabase MySqlDb
        {
            get { return new MySqlDatabase(); }
        }

        /// <summary>
        /// Gets a SqLite database
        /// </summary>
        public static SqLiteDatabase SqLiteDb
        {
            get { return new SqLiteDatabase(); }
        }

        /// <summary>
        /// Set a new Database settings from a IDbSettings
        /// </summary>
        /// <typeparam name="T">The database type</typeparam>
        /// <param name="dnName">The database name</param>
        /// <returns>T</returns>
        public static T DatabaseSettings<T>(string dnName)
            where T : IDbSettings, new()
        {
            T obj = new T();
            obj.DatabaseName = dnName;
            return obj;
        }

        /// <summary>
        /// Create a context
        /// </summary>
        /// <param name="settings">The database settings</param>
        /// <returns>IUniversalContext</returns>
        public static IUniversalContext CreateContext(IDbSettings settings)
        {
            ProviderType provider = settings.Provider;
            switch (provider)
            {
                case ProviderType.MySQL:

                    return new MySqlContext(settings);

                case ProviderType.SQLite:

                    return new SqLiteContext(settings);

                default:

                    // Get all classes from DbContexts namespace
                    List<Type> listTypeDbClasses = GenericUtils.AllClassesFromNamespace(Nsp);

                    // Get the class type to instantiate
                    var clazz = GenericUtils.GetClassesFromProperty(listTypeDbClasses, "Provider", provider);
                    return (IUniversalContext)Activator.CreateInstance(clazz, settings);
            }
        }

        /// <summary>
        /// Create a global context
        /// </summary>
        /// <returns>IGlobalContext</returns>
        public static IGlobalContext CreateGlobalContext()
        {
            return new GlobalContext();
        }
    }
}

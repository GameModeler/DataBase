// <copyright file="GenericUtils.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DataBase.Database.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Generic Utils
    /// </summary>
    public static class GenericUtils
    {
        /// <summary>
        /// Instantiate a generic class
        /// </summary>
        /// <param name="clazz">The class</param>
        /// <param name="entity">The entity</param>
        /// <param name="param">Parameters</param>
        /// <returns>object</returns>
        public static object InstantiateGeneric(Type clazz, Type entity, object[] param = null)
        {
            Type constructedType = clazz.MakeGenericType(entity);
            return Activator.CreateInstance(constructedType, param);
        }

        /// <summary>
        /// Get all classes from a namespace
        /// </summary>
        /// <param name="nspace">Namespace</param>
        /// <returns>List</returns>
        public static List<Type> AllClassesFromNamespace(string nspace)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(t => t.GetTypes())
                       .Where(t => t.IsClass && t.Namespace == nspace)
                       .ToList();
        }

        /// <summary>
        /// Get class from a property
        /// </summary>
        /// <param name="classList">List of classes</param>
        /// <param name="propName">Property name</param>
        /// <param name="propValue">Property value</param>
        /// <returns>Type</returns>
        public static Type GetClassesFromProperty(List<Type> classList, string propName, object propValue)
        {
            Type result = null;

            // Search for the class with the Provider property 
            // matching the one provided
            foreach (var clazz in classList)
            {
                var propElem = clazz.GetType().GetProperties().FirstOrDefault(prop => prop.Name == propName);

                if (propElem != null && propElem.GetValue(clazz, null) == propValue)
                {
                    result = clazz;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Call a method by reflection
        /// </summary>
        /// <param name="clazz">The class</param>
        /// <param name="methodName">The method name</param>
        /// <param name="typeArg">The type argument</param>
        /// <param name="instance">The instance</param>
        /// <param name="value">Parameters</param>
        /// <returns>object</returns>
        public static object CallMathodByReflection(Type clazz, string methodName, Type typeArg, object instance, object value = null)
        {
            MethodInfo method = clazz.GetMethod(methodName);
            MethodInfo generic = method.MakeGenericMethod(typeArg);
            var parameters = (value == null) ? new object[] { } : new object[] { value };
            return generic.Invoke(instance, parameters);
        }

        /// <summary>
        /// Gets all attributes from a specified type
        /// </summary>
        /// <typeparam name="T">Type of the attribute</typeparam>
        /// <param name="t">Type</param>
        /// <returns>List</returns>
        public static List<T> GetAttribute<T>(Type t)
            where T : Attribute
        {
            return t.GetCustomAttributes<T>(false).ToList<T>();
        }

        /// <summary>
        /// Generic Dictionary
        /// </summary>
        public class GenericDictionary
        {
            private Dictionary<Type, object> dict = new Dictionary<Type, object>();

            /// <summary>
            /// Add item to the generic dictionnary
            /// </summary>
            /// <typeparam name="K">Key</typeparam>
            /// <typeparam name="V">Value</typeparam>
            /// <param name="key">key</param>
            /// <param name="value">value</param>
            public void Add<V>(Type key, V value)
                where V : class
            {
                this.dict.Add(key, value);
            }

            /// <summary>
            /// Get value from the generic dictionnary
            /// </summary>
            /// <typeparam name="T">Value</typeparam>
            /// <typeparam name="K">Key</typeparam>
            /// <param name="key">key</param>
            /// <returns>T</returns>
            public T GetValue<T>(Type key)
                where T : class
            {
                return this.dict[key] as T;
            }

            /// <summary>
            /// Try Get Value
            /// </summary>
            /// <typeparam name="T">T</typeparam>
            /// <param name="key">Key</param>
            /// <param name="value">Value</param>
            /// <returns>bool</returns>
            public bool TryGetValue<T>(Type key, out T value)
                where T : class
            {
                value = null;
                if (this.dict.ContainsKey(key))
                {
                    value = this.dict[key] as T;
                    return true;
                }

                return false;
            }
        }
    }
}

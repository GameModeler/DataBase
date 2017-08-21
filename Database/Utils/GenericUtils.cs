using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataBase.Database.Utils
{
    /// <summary>
    /// Generic Utils
    /// </summary>
    public static class GenericUtils
    {
        /// <summary>
        /// Instantiate a generic class
        /// </summary>
        /// <param name="clazz"></param>
        /// <param name="entity"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object InstantiateGeneric(Type clazz, Type entity, object[] param = null)
        {
            Type constructedType = clazz.MakeGenericType(entity);
            return Activator.CreateInstance(constructedType, param);
        }

        /// <summary>
        /// Get all classes from a namespace
        /// </summary>
        /// <param name="nspace"></param>
        /// <returns></returns>
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
        /// <param name="classList"></param>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <returns></returns>
        public static Type GetClassesFromProperty(List<Type> classList, string propName, object propValue)
        {
            Type result = null;

            // Search for the class with the Provider property 
            // matching the one provided
            foreach (var clazz in classList)
            {
                var propElem = clazz.GetType().GetProperties().FirstOrDefault(prop => prop.Name == propName);

                if(propElem != null && propElem.GetValue(clazz, null) == propValue)
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
        /// <param name="clazz"></param>
        /// <param name="methodName"></param>
        /// <param name="typeArg"></param>
        /// <param name="instance"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object CallMathodByReflection(Type clazz, string methodName, Type typeArg, object instance, object value = null)
        {
            // Just for simplicity, assume it's public etc
            MethodInfo method = clazz.GetMethod(methodName);
            MethodInfo generic = method.MakeGenericMethod(typeArg);
            var parameters = (value == null) ? new object[] { } : new object[] { value };
            return generic.Invoke(instance, parameters); 
        }

        /// <summary>
        /// Gets all attributes from a specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<T> GetAttribute<T>(Type t) where T : Attribute
        {
            return t.GetCustomAttributes<T>(false).ToList<T>();
        }

        /// <summary>
        /// Generic Dictionary
        /// </summary>
        public class GenericDictionary
        {
            private Dictionary<Type, object> _dict = new Dictionary<Type, object>();

            /// <summary>
            /// Add item to the generic dictionnary
            /// </summary>
            /// <typeparam name="K"></typeparam>
            /// <typeparam name="V"></typeparam>
            /// <param name="key"></param>
            /// <param name="value"></param>
            public void Add<V>(Type key, V value) where V : class
            {
                _dict.Add(key, value);
            }

            /// <summary>
            /// Get value from the generic dictionnary
            /// </summary>
            /// <typeparam name="K"></typeparam>
            /// <typeparam name="T"></typeparam>
            /// <param name="key"></param>
            /// <returns></returns>
            public T GetValue<T>(Type key) where T : class
            {
                return _dict[key] as T;
            }

            /// <summary>
            /// Try Get Value
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="key"></param>
            /// <param name="value"></param>
            /// <returns></returns>
            public bool TryGetValue<T>(Type key, out T value) where T : class
            {
                value = null;
                if (_dict.ContainsKey(key))
                {
                    value = _dict[key] as T; // the type of result is Component!
                    return true;
                }

                return false;
            }
        }
    }
}

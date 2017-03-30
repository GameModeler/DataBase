using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Utils
{
    public static class GenericUtils
    {

        public static object InstantiateGeneric(Type clazz, Type entity, object[] param)
        {
            Type constructedType = clazz.MakeGenericType(entity);
            return Activator.CreateInstance(constructedType, param);
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        public static List<Type> AllClassesFromNamespace(string nspace)
        {

            return AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(t => t.GetTypes())
                       .Where(t => t.IsClass && t.Namespace == nspace)
                       .ToList();
        }

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

        public static object CallMathodByReflection(Type clazz, string methodName, Type typeArg, object value)
        {
            // Just for simplicity, assume it's public etc
            MethodInfo method = clazz.GetMethod(methodName);
            MethodInfo generic = method.MakeGenericMethod(typeArg);
           return generic.Invoke(null, new object[] { value });
        }


    }
}

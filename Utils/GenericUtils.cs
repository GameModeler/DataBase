using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}

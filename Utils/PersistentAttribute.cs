using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Utils
{
    /// <summary>
    /// Persistant attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class PersistentAttribute : Attribute
    {
    }

}

using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Criterias
{
    /// <summary>
    /// Database operation
    /// </summary>
    public enum DbDataBase
    {
        /// <summary>
        /// Create database
        /// </summary>
        [StringValue("CREATE")]
        CREATE,

        /// <summary>
        /// Drop database
        /// </summary>
        [StringValue("DROP")]
        DROP
    }
}

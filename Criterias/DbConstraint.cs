using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Criterias
{
    /// <summary>
    /// Database constraint
    /// </summary>
    public enum DbConstraint
    {
        /// <summary>
        /// Primary key constraint
        /// </summary>
        [StringValue("PRIMARY KEY")]
        PRIMARY_KEY
    }
}

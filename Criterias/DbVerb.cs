using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Criterias
{
    /// <summary>
    /// Database verb
    /// </summary>
    public enum DbVerb
    {
        /// <summary>
        /// And
        /// </summary>
        [StringValue("AND")]
        AND,

        /// <summary>
        /// Or
        /// </summary>
        [StringValue("OR")]
        OR,

        /// <summary>
        /// Empty
        /// </summary>
        [StringValue("")]
        EMPTY
    }
}

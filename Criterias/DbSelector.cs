using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Criterias
{
    /// <summary>
    /// Database selector
    /// </summary>
    public enum DbSelector
    {
        /// <summary>
        /// All
        /// </summary>
        [StringValue("*")]
        ALL,

        /// <summary>
        /// None
        /// </summary>
        [StringValue("")]
        NONE,
    }
}

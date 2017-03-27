using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Criterias
{
    /// <summary>
    /// Database links
    /// </summary>
    public enum DbLinks
    {
        /// <summary>
        /// Inner join
        /// </summary>
        [StringValue("INNER JOIN")]
        INNERJOIN,

        /// <summary>
        /// From
        /// </summary>
        [StringValue("FROM")]
        FROM
    }
}

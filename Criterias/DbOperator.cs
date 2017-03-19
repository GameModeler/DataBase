using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Criterias
{

    /// <summary>
    /// Database operator
    /// </summary>
    public enum DbOperator
    {
        /// <summary>
        /// Equal
        /// </summary>
        [StringValue("=")]
        EQUAL,

        /// <summary>
        /// Superior
        /// </summary>
        [StringValue(">")]
        SUPERIOR,

        /// <summary>
        /// Inferior
        /// </summary>
        [StringValue("<")]
        INFERIOR,

        /// <summary>
        /// Superior or equal
        /// </summary>
        [StringValue(">=")]
        SUPERIOREQUAL,

        /// <summary>
        /// Inferior or equal
        /// </summary>
        [StringValue("<=")]
        INFERIOREQUAL,

        /// <summary>
        /// In
        /// </summary>
        [StringValue("IN")]
        IN,

        /// <summary>
        /// Not in
        /// </summary>
        [StringValue("NOT IN")]
        NOTIN,

        /// <summary>
        /// Is null
        /// </summary>
        [StringValue("IS NULL")]
        ISNULL,
        /// <summary>
        /// Is not null
        /// </summary>
        [StringValue("IS NOT NULL")]
        ISNOTNULL,

        /// <summary>
        /// Like
        /// </summary>
        [StringValue("LIKE")]
        LIKE
    }
}

using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Criterias
{
    /// <summary>
    /// Database type
    /// </summary>
    public enum DbTypes
    {
        /// <summary>
        /// Varchar
        /// </summary>
        [StringValue("VARCHAR(255)")]
        VARCHAR,

        /// <summary>
        /// Int
        /// </summary>
        [StringValue("INT")]
        INT,

        /// <summary>
        /// Date
        /// </summary>
        [StringValue("DATE")]
        DATE
    }
}

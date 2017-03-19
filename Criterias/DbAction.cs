using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Criterias
{
    /// <summary>
    /// Database actions
    /// </summary>
    public enum DbAction
    {
        /// <summary>
        /// Select action
        /// </summary>
        [StringValue("SELECT")]
        SELECT,

        /// <summary>
        /// Delete action
        /// </summary>
        [StringValue("DELETE")]
        DELETE,

        /// <summary>
        /// Update action
        /// </summary>
        [StringValue("UPDATE")]
        UPDATE
    }
}

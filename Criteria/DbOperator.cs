using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Criteria
{
    public enum DbOperator
    {
        [StringValue("=")]
        EQUAL,
        [StringValue(">")]
        SUPERIOR,
        [StringValue("<")]
        INFERIOR,
        [StringValue(">=")]
        SUPERIOREQUAL,
        [StringValue("<=")]
        INFERIOREQUAL,
        [StringValue("IN")]
        IN,
        [StringValue("NOT IN")]
        NOTIN,
        [StringValue("IS NULL")]
        ISNULL,
        [StringValue("IS NOT NULL")]
        ISNOTNULL,
        [StringValue("LIKE")]
        LIKE
    }
}

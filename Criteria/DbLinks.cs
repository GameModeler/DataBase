using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Criteria
{
    public enum DbLinks
    {
        [StringValue("INNER JOIN")]
        INNERJOIN,
        [StringValue("FROM")]
        FROM
    }
}

using GMDataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMDataBase.Criteria
{
    public enum DbAction
    {
        [StringValue("SELECT")]
        SELECT,
        [StringValue("DELETE")]
        DELETE,
        [StringValue("UPDATE")]
        UPDATE
    }
}

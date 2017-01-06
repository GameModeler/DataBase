using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Criteria
{
    public enum DbTypes
    {
        [StringValue("VARCHAR(255)")]
        VARCHAR,
        [StringValue("INT")]
        INT,
        [StringValue("DATE")]
        DATE
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Criterias
{
    /// <summary>
    /// Field
    /// </summary>
    public class Field
    {
        string name { get; set; }
        List<DbConstraint> constraint { get; set; }

    }
}

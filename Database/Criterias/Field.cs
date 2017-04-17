using System.Collections.Generic;

namespace DataBase.Database.Criterias
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

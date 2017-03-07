using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMDataBase.Utils
{
    /// <summary>
    /// Enum of the different provider
    /// </summary>
    public enum ProviderType
    {
        [StringValue("System.Data.SqlClient")]
        Default,
        [StringValue("MySql.Data.MySqlClient")]
        MySQL,
        [StringValue("System.Data.SQLite.EF6")]
        SQLite
    }
}

using DataBase.Database.Utils;

namespace DataBase.Database.Criterias
{
    /// <summary>
    /// Database operation
    /// </summary>
    public enum DbDataBase
    {
        /// <summary>
        /// Create database
        /// </summary>
        [StringValue("CREATE")]
        CREATE,

        /// <summary>
        /// Drop database
        /// </summary>
        [StringValue("DROP")]
        DROP
    }
}

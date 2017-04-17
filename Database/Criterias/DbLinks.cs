using DataBase.Database.Utils;

namespace DataBase.Database.Criterias
{
    /// <summary>
    /// Database links
    /// </summary>
    public enum DbLinks
    {
        /// <summary>
        /// Inner join
        /// </summary>
        [StringValue("INNER JOIN")]
        INNERJOIN,

        /// <summary>
        /// From
        /// </summary>
        [StringValue("FROM")]
        FROM
    }
}

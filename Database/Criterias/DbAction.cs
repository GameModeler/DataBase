using DataBase.Database.Utils;

namespace DataBase.Database.Criterias
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

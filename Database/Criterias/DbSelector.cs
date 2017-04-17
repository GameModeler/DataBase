using DataBase.Database.Utils;

namespace DataBase.Database.Criterias
{
    /// <summary>
    /// Database selector
    /// </summary>
    public enum DbSelector
    {
        /// <summary>
        /// All
        /// </summary>
        [StringValue("*")]
        ALL,

        /// <summary>
        /// None
        /// </summary>
        [StringValue("")]
        NONE,
    }
}

using DataBase.Database.Utils;

namespace DataBase.Database.Criterias
{
    /// <summary>
    /// Database verb
    /// </summary>
    public enum DbVerb
    {
        /// <summary>
        /// And
        /// </summary>
        [StringValue("AND")]
        AND,

        /// <summary>
        /// Or
        /// </summary>
        [StringValue("OR")]
        OR,

        /// <summary>
        /// Empty
        /// </summary>
        [StringValue("")]
        EMPTY
    }
}

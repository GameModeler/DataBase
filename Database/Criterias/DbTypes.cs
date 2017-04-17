using DataBase.Database.Utils;

namespace DataBase.Database.Criterias
{
    /// <summary>
    /// Database type
    /// </summary>
    public enum DbTypes
    {
        /// <summary>
        /// Varchar
        /// </summary>
        [StringValue("VARCHAR(255)")]
        VARCHAR,

        /// <summary>
        /// Int
        /// </summary>
        [StringValue("INT")]
        INT,

        /// <summary>
        /// Date
        /// </summary>
        [StringValue("DATE")]
        DATE
    }
}

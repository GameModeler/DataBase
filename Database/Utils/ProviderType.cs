namespace DataBase.Database.Utils
{
    /// <summary>
    /// Enum of the different provider
    /// </summary>
    public enum ProviderType
    {
        /// <summary>
        /// Default
        /// </summary>
        [StringValue("System.Data.SqlClient")]
        Default,
        /// <summary>
        /// MySql
        /// </summary>
        [StringValue("MySql.Data.MySqlClient")]
        MySQL,
        /// <summary>
        /// SQLite
        /// </summary>
        [StringValue("System.Data.SQLite.EF6")]
        SQLite
    }
}

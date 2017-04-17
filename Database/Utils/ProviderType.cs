namespace DataBase.Database.Utils
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

namespace DataBase.Database.DbSettings.FluentApi.Interfaces
{
    /// <summary>
    /// Database settings Fluent Api
    /// </summary>
    public interface IDbSettingsFApi
    {
        /// <summary>
        /// Build the database connection string
        /// </summary>
        /// <returns></returns>
        string ToConnectionString();
    }
}

namespace DataBase.Database.DbSettings.Interface
{
    interface IDbSettingsFI
    {
        /// <summary>
        /// Build the database connection string
        /// </summary>
        /// <returns></returns>
        string ToConnectionString();
    }
}

namespace DataBase.Database.DbSettings.Interfaces
{
    /// <summary>
    /// MySql Database settings interface
    /// </summary>
    public interface IMySqlDatabase
    {
        /// <summary>
        /// Server adresse
        /// </summary>
        string Server { get; set; }

        /// <summary>
        /// Server's port
        /// </summary>
        string Port { get; set; }

        /// <summary>
        /// User's id
        /// </summary>
        string UserId { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        string Password { get; set; }

    }
}

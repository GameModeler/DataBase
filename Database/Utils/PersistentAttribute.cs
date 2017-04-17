using System;

namespace DataBase.Database.Utils
{
    /// <summary>
    /// Persistant attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class PersistentAttribute : Attribute
    {

        private string[] dbNames;

        /// <summary>
        /// Constructor
        /// </summary>
        public PersistentAttribute(params string[] dbNames)
        {
            this.dbNames = dbNames;
        }

        /// <summary>
        /// Define DbName property
        /// </summary>
        public virtual  string[] DbNames
        {
            get { return dbNames; }
        }

    }

}

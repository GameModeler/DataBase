using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Utils
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

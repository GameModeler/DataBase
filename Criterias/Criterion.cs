using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Criterias
{
    /// <summary>
    /// Criterion
    /// </summary>
    public class Criterion
    {
        private DbOperator dbOperator;

        private DbVerb verb;

        private object value;

        private String dbColumn;
        

        /// <summary>
        /// Constructor
        /// </summary>
        public Criterion()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="verb"></param>
        /// <param name="dbColumn"></param>
        /// <param name="dbOperator"></param>
        /// <param name="value"></param>
        public Criterion(DbVerb verb, String dbColumn, DbOperator dbOperator, object value)
        {
            this.verb = verb;
            this.dbColumn = dbColumn;
            this.dbOperator = dbOperator;
            this.value = value;
        }

        /// <summary>
        /// Compute the criterion into MySql query
        /// </summary>
        /// <returns></returns>
        public String MySQLCompute()
        {
            return EnumString.GetStringValue(this.verb) + " " + this.dbColumn + " " + EnumString.GetStringValue(this.dbOperator) + " " + this.value.ToString();
        }
    }
}

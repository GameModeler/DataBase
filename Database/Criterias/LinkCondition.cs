using System;

namespace DataBase.Database.Criterias
{
    /// <summary>
    /// Link condition
    /// </summary>
    public class LinkCondition
    {
        private String columnStart;

        private String columnArrive;

        /// <summary>
        /// Constructor
        /// </summary>
        public LinkCondition()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="columnStart"></param>
        /// <param name="columnArrive"></param>
        public LinkCondition(String columnStart, String columnArrive)
        {
            this.columnStart = columnStart;
            this.columnArrive = columnArrive;
        }

        /// <summary>
        /// Compute the link into MySql query
        /// </summary>
        /// <returns></returns>
        public String MySQLCompute()
        {
            return "ON " + this.columnStart + " = " + this.columnArrive;
        }
    }
}

using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Criterias
{
    /// <summary>
    /// Criteria
    /// </summary>
    public class Criteria
    {
        private List<Criterion> criterions;

        private DbAction dbAction;

        private DbTablesLinks dbTablesLinks;

        private String dbSelector;

        /// <summary>
        /// Constructor
        /// </summary>
        public Criteria()
        {
            this.criterions = new List<Criterion>();
            this.dbTablesLinks = new DbTablesLinks();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="action"></param>
        /// <param name="dbSelector"></param>
        public Criteria(DbAction action, String dbSelector)
        {
            this.criterions = new List<Criterion>();
            this.dbTablesLinks = new DbTablesLinks();
            this.dbAction = action;
            this.dbSelector = dbSelector;
        }

        /// <summary>
        /// Add a database link
        /// </summary>
        /// <param name="table"></param>
        /// <param name="link"></param>
        /// <param name="condition"></param>
        public void AddDbLink(String table, DbLinks link, LinkCondition condition = null)
        {
            this.dbTablesLinks.Add(table, link, condition);
        }

        /// <summary>
        /// Add a criterion
        /// </summary>
        /// <param name="criterion"></param>
        public void AddCriterion(Criterion criterion)
        {
            this.criterions.Add(criterion);
        }

        /// <summary>
        /// Compute the criteria into MySqL query
        /// </summary>
        /// <returns></returns>
        public String MySQLCompute()
        {
            String result = this.dbAction.GetStringValue();
            result += " ";
            result += this.dbSelector;
            result += " ";
            result += this.dbTablesLinks.MySQLCompute();

            if (criterions.Count > 0)
            {
                result += "WHERE";
                result += " ";
            }

            foreach (var item in criterions)
            {
                result += item.MySQLCompute();
                result += " ";
            }
            return result;
        }
    }
}

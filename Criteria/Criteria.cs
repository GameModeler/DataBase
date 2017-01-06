using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Criteria
{
    public class Criteria
    {
        private List<Criterion> criterions;

        private DbAction dbAction;

        private DbTablesLinks dbTablesLinks;

        private String dbSelector;


        public Criteria()
        {
            this.criterions = new List<Criterion>();
            this.dbTablesLinks = new DbTablesLinks();
        }

        public Criteria(DbAction action, String dbSelector)
        {
            this.criterions = new List<Criterion>();
            this.dbTablesLinks = new DbTablesLinks();
            this.dbAction = action;
            this.dbSelector = dbSelector;
        }

        public void AddDbLink(String table, DbLinks link, LinkCondition condition = null)
        {
            this.dbTablesLinks.Add(table, link, condition);
        }

        public void AddCriterion(Criterion criterion)
        {
            this.criterions.Add(criterion);
        }

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

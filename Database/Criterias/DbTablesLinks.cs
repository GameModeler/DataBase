using DataBase.Database.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBase.Database.Criterias
{
    /// <summary>
    /// Database tables links
    /// </summary>
    public class DbTablesLinks
    {
        private Dictionary<String, Dictionary<DbLinks, LinkCondition>> links;

        /// <summary>
        /// Constructor
        /// </summary>
        public DbTablesLinks()
        {
            this.links = new Dictionary<String, Dictionary<DbLinks, LinkCondition>>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="table"></param>
        /// <param name="link"></param>
        /// <param name="condition"></param>
        public DbTablesLinks(String table, DbLinks link, LinkCondition condition = null)
        {
            this.links = new Dictionary<String, Dictionary<DbLinks, LinkCondition>>();
            this.Add(table, link, condition);
        }

        /// <summary>
        /// Add a database link
        /// </summary>
        /// <param name="table"></param>
        /// <param name="link"></param>
        /// <param name="condition"></param>
        public void Add(String table, DbLinks link, LinkCondition condition = null)
        {
            Dictionary<DbLinks, LinkCondition> linker = new Dictionary<DbLinks, LinkCondition>();
            linker.Add(link, condition);
            this.links.Add(table, linker);
        }

        /// <summary>
        /// Compute the database link into MySql query
        /// </summary>
        /// <returns></returns>
        public String MySQLCompute()
        {
            String result = "";

            foreach (var item in links)
            {
                result += EnumString.GetStringValue(item.Value.First().Key) + " " + item.Key;
                if (item.Value.First().Value != null)
                {
                    result += " " + item.Value.First().Value.MySQLCompute();
                }
                result += " ";
            }

            return result;
        }
    }
}

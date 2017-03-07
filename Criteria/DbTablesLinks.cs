using GMDataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMDataBase.Criteria
{
    public class DbTablesLinks
    {
        private Dictionary<String, Dictionary<DbLinks, LinkCondition>> links;


        public DbTablesLinks()
        {
            this.links = new Dictionary<String, Dictionary<DbLinks, LinkCondition>>();
        }

        public DbTablesLinks(String table, DbLinks link, LinkCondition condition = null)
        {
            this.links = new Dictionary<String, Dictionary<DbLinks, LinkCondition>>();
            this.Add(table, link, condition);
        }

        public void Add(String table, DbLinks link, LinkCondition condition = null)
        {
            Dictionary<DbLinks, LinkCondition> linker = new Dictionary<DbLinks, LinkCondition>();
            linker.Add(link, condition);
            this.links.Add(table, linker);
        }

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

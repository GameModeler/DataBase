using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMDataBase.Criteria
{
    public class LinkCondition
    {
        private String columnStart;

        private String columnArrive;

        public LinkCondition()
        {

        }

        public LinkCondition(String columnStart, String columnArrive)
        {
            this.columnStart = columnStart;
            this.columnArrive = columnArrive;
        }

        public String MySQLCompute()
        {
            return "ON " + this.columnStart + " = " + this.columnArrive;
        }
    }
}

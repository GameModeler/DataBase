using GMDataBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMDataBase.Entities
{
    [Persistent]
    public class Cat
    {
        public int CatId { get; set; }

        public string Color { get; set; }

        public int Year { get; set; }

        public string Name { get; set; }
    }
}

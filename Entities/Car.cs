using DataBase.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    [Persistent("db2", "db3")]
    public class Car
    {
        public int CarId { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string Manufacturer { get; set; }
    }
}

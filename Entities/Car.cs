using DataBase.Utils;

namespace DataBase.Entities
{
    [Persistent]
    public class Car
    {
        public int CarId { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string Manufacturer { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    [Serializable]
    public class Class1
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public Boolean Status { get; set; }


        public new List<Class1> LoadMultipleItems()
        {
            List<Class1> result = new List<Class1>();
            for (int i = 0; i < 10; i++)
            {
                result.Add(LoadSingleItem());
            }
            return result;
        }

        public Class1 LoadSingleItem()
        {
            Class1 result = new Class1();
            result.Id = Faker.RandomNumber.Next();
            result.Name = Faker.Name.First();
            result.Status = true;
            return result;
        }


        public override string ToString()
        {
            return this.Id + " " + this.Name + " " + this.Status;
        }

        public string Print(string delimiter)
        {
            return "Id:" + this.Id + delimiter + "Name:" + this.Name + delimiter + "Status:" + this.Status;
        }

    }
}

using DataBase.Binary;
using DataBase.Character;
using DataBase.Csv;
using DataBase.Json;
using DataBase.Sql;
using DataBase.Xml;
using DataBase.Yaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Yaml.Serialization;

namespace DataBase
{
    class Program
    {

        static void Main()
        {
            Console.WriteLine("Hello World!");

            Class1 class1 = new Class1();
            class1.Id = 1;
            class1.Name = "test";
            class1.Status = true;


            // JSON
            JsonManager.WriteToJsonFile<Class1>(@"A:\", "test.json", class1);
            Class1 objJson = JsonManager.ReadFromJsonFile<Class1>(@"A:\test.json");
            Console.WriteLine("JSON => " + objJson.Id + " " + objJson.Name + " " + objJson.Status);

            // XML
            XmlManager.WriteToXmlFile<Class1>(@"A:\", "test.xml", class1);
            Class1 objXml = XmlManager.ReadFromXmlFile<Class1>(@"A:\test.xml");
            Console.WriteLine("XML => " + objXml.Id + " " + objXml.Name + " " + objXml.Status);

            // Binary
            BinaryManager.WriteToBinaryFile<Class1>(@"A:\", "test", class1);
            Class1 objBin = BinaryManager.ReadFromBinaryFile<Class1>(@"A:\test");
            Console.WriteLine("Binary => " + objBin.Id + " " + objBin.Name + " " + objBin.Status);

            // CHARACTER
            CharacterManager.WriteToCharacterFile<Class1>(@"A:\", "character.txt", class1, false);
            Class1 objCharacter = CharacterManager.ReadFromCharacterFile<Class1>(@"A:\character.txt");
            Console.WriteLine("Character => " + objCharacter.Id + " " + objCharacter.Name + " " + objCharacter.Status);

            // YAML
            YamlManager.WriteToYamlFile<Class1>(@"A:\", "yaml.txt", class1);
            Class1 objYaml = YamlManager.ReadFromYamlFile<Class1>(@"A:\savedList.txt");
            Console.WriteLine("Yaml => " + objYaml.Id + " " + objYaml.Name + " " + objYaml.Status);


            // CSV
            CsvManager.WriteToCsvFile<Class1>(@"A:\", "characterCSV.txt", class1);
            //Class1 test = CsvManager.ReadFromCsvFile<Class1>(@"A:\characterCSV.txt");
            //Console.WriteLine(test.Name);

            // SQL
            SqlManager.WriteToSqlFile<Class1>(@"A:\", "script.sql", class1, false, "Test3", true);



            /* Test List */
            List <Class1> listClass1 = class1.LoadMultipleItems();
            JsonManager.WriteToJsonFile<List<Class1>>(@"A:\", "test_list.json", listClass1);



            Console.ReadKey();
        }

    }
}

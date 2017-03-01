using DataBase.Binary;
using DataBase.Character;
using DataBase.Csv;
using DataBase.Dynamic;
using DataBase.Json;
using DataBase.Sql;
using DataBase.Xml;
using DataBase.Yaml;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
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
            Test2();
            Console.ReadKey();
        }

        public static void Test1()
        {
            Class1 class2 = new Class1();
            class2.Id = 10;
            class2.Name = "8787878";
            class2.Status = true;

            string script = SqlManager.ConvertObjectInScript<Class1>(class2, false, "test13", true);
            SqlManager.ExecuteStringSql(script);
            Console.WriteLine(script);
            SqlManager.WriteToSqlFile<Class1>(@"A:\", "script.sql", class2, false, "Test4", true);
            dynamic dynamicObject = new ExpandoObject();
            var dic = dynamicObject as IDictionary<string, object>;

            dic = Dynamic.DynamicManager.CreateObjectByDatabase("root", "", "test10", "class1");
            foreach (var t in dic)
            {
                Console.WriteLine(t.Key + " " + t.Value);
            }
        }

       
  

        public static void Test2()
        {

            Class1 class1 = new Class1();
            class1.Id = 1;
            class1.Name = "test";
            class1.Status = true;

            List<Class1> listClass1 = class1.LoadMultipleItems();            

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
            // Write one object
            CharacterManager.WriteToCharacterFile<Class1>(@"A:\", "character.txt", class1, true);
            // Write object list
            CharacterManager.WriteToCharacterFile<List<Class1>>(@"A:\", "list_character.txt", listClass1, false);
            List<Class1> objListCharacter = CharacterManager.ReadFromCharacterFile<Class1>(@"A:\", "character.txt");
            Console.WriteLine("Character_List => ");
            foreach (var objChar in objListCharacter)
            {
                Console.WriteLine(objChar.Id + " " + objChar.Name + " " + objChar.Status);
            }

            // YAML
            YamlManager.WriteToYamlFile<Class1>(@"A:\", "yaml.txt", class1);
            Class1 objYaml = YamlManager.ReadFromYamlFile<Class1>(@"A:\", "savedList.txt");
            Console.WriteLine("Yaml => " + objYaml.Id + " " + objYaml.Name + " " + objYaml.Status);


            // CSV
            CsvManager.WriteToCsvFile<Class1>(@"A:\", "characterCSV.txt", class1);
            Class1 test = CsvManager.ReadFromCsvFile<Class1>(@"A:\", "characterCSV.txt");


            // SQL
            SqlManager.WriteToSqlFile<Class1>(@"A:\", "script.sql", class1, false, "Test3", true);

            // Test List     
            JsonManager.WriteToJsonFile<List<Class1>>(@"A:\", "test_list.json", listClass1);
            List<Class1> objJsonList = JsonManager.ReadFromJsonFile<List<Class1>>(@"A:\test_list.json");
            Console.WriteLine("\nJSON_LIST =>");
            foreach (var obj in objJsonList)
            {
                Console.WriteLine("" + obj.Id + " " + obj.Name + " " + obj.Status);
            }

            Console.ReadKey();
        }
    }
}

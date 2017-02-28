using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Csv
{
    public class CsvManager
    {
        /// <summary>
        /// Write object in csv file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="objectToWrite"></param>
        /// <param name="append"></param>
        public static void WriteToCsvFile<T>(string path, string fileName, T objectToWrite, bool append = false) where T : new()
        {
            string filePath = path + fileName;
            TextWriter writer = null;
            try
            {
                writer = new StreamWriter(filePath, append);
                var csv = new CsvWriter(writer);
                csv.WriteRecord(objectToWrite);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
 
        public static T ReadFromCsvFile<T>(string filePath) where T : new()
        {

            TextReader reader = null;
            try
            {

                String[] values = File.ReadAllText(filePath).Split(',');
                Console.WriteLine(values);
            
                T obj = new T();

                int i = 0;
                foreach (var prop in values)
                {
                    if (string.IsNullOrEmpty(prop) == false)
                    {
                        // Property of obj
                        string objProp = obj.GetType().GetProperties()[i].Name;
                        // Type of obj property
                        Type typeProp = obj.GetType().GetProperties()[i].PropertyType;
                        // Type of obj
                        Type type = obj.GetType();
                        // Property of obj
                        PropertyInfo propToWrite = type.GetProperty(objProp);                 
                        // Set value on obj property (with cast on value, each save value is a string)
                        propToWrite.SetValue(obj, Convert.ChangeType(prop, typeProp), null);
                     
                    }
                    i += 1;
                }
                return obj;
                

                /*reader = new StreamReader(filePath);
                var csv = new CsvReader(reader);
                var test = csv.GetRecords<T>();
                Console.WriteLine(test);
                return (T)test;*/
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }   
    }
}

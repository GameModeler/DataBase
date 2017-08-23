using CsvHelper;
using System;
using System.IO;
using System.Reflection;

namespace DataBase.Csv
{
    public class CsvManager
    {
        /// <summary>
        /// Write object in csv file
        /// </summary>
        /// <typeparam name="T">T object</typeparam>
        /// <param name="path">path</param>
        /// <param name="fileName">file name</param>
        /// <param name="objectToWrite">object to write</param>
        /// <param name="append">append</param>
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
                {
                    writer.Close();
                }
            }
        }
 
        /// <summary>
        /// Read T object from csv file
        /// </summary>
        /// <typeparam name="T">T object</typeparam>
        /// <param name="path">path</param>
        /// <param name="fileName">file name</param>
        /// <returns>T object</returns>
        public static T ReadFromCsvFile<T>(string path, string fileName) where T : new()
        {
            string filePath = path + fileName;
            string[] values = File.ReadAllText(filePath).Split(',');
            T obj = new T();
            TextReader reader = null;
            try
            {
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
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
    }
}

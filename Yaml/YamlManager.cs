using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Yaml.Serialization;

namespace DataBase.Yaml
{
    public class YamlManager
    {

        /// <summary>
        /// Write T object to json file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="objectToWrite"></param>
        /// <param name="append"></param>
        public static void WriteToYamlFile<T>(string path, string fileName, T objectToWrite, bool append = false) where T : new()
        {
            string filePath = path + fileName;
            TextWriter writer = null;
            try
            {
                var serializer = new YamlSerializer();
                string contentsToWriteToFile = serializer.Serialize(objectToWrite);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        /// <summary>
        /// Read from json file and convert in T object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T ReadFromYamlFile<T>(string path, string fileName) where T : new()
        {
            string filePath = path + fileName;
            TextReader reader = null;
            try
            {
                var serializer = new YamlSerializer();
                Console.WriteLine(serializer.DeserializeFromFile(filePath));
                object obj = serializer.DeserializeFromFile(filePath)[0];
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}

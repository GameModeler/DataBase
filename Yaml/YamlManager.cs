using System;
using System.IO;
using System.Yaml.Serialization;

namespace DataBase.Yaml
{
    public class YamlManager
    {

        /// <summary>
        /// Write T object to json file
        /// </summary>
        /// <typeparam name="T">T object</typeparam>
        /// <param name="path">path</param>
        /// <param name="fileName">file name</param>
        /// <param name="objectToWrite">object to write</param>
        /// <param name="append">append</param>
        public static void WriteToYamlFile<T>(string path, string fileName, T objectToWrite, bool append = false) where T : new()
        {
            string filePath = path + fileName;
            TextWriter writer = null;

            var serializer = new YamlSerializer();
            string contentsToWriteToFile = serializer.Serialize(objectToWrite);
            writer = new StreamWriter(filePath, append);
            writer.Write(contentsToWriteToFile);
            writer.Close();
        }

        /// <summary>
        /// Read from json file and convert in T object
        /// </summary>
        /// <typeparam name="T">T object</typeparam>
        /// <param name="path">path</param>
        /// <param name="fileName">fileName</param>
        /// <returns>T object</returns>
        public static T ReadFromYamlFile<T>(string path, string fileName) where T : new()
        {
            string filePath = path + fileName;
            TextReader reader = null;
            var serializer = new YamlSerializer();
            object obj = serializer.DeserializeFromFile(filePath)[0];

            T ob = (T)Convert.ChangeType(obj, typeof(T));
            reader.Close();
            return ob;
        }
    }
}

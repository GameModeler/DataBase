using Newtonsoft.Json;
using System;
using System.IO;

namespace DataBase.Json
{
    [Serializable()]
    public partial class JsonManager
    {

        /// <summary>
        /// Write T object to json file
        /// </summary>
        /// <typeparam name="T">T object</typeparam>
        /// <param name="path">path</param>
        /// <param name="fileName">file name</param>
        /// <param name="objectToWrite">object to write</param>
        /// <param name="append">append</param>
        public static void WriteToJsonFile<T>(string path, string fileName, T objectToWrite, bool append = false) where T : new()
        {
            string filePath = path + fileName;
            TextWriter writer = null;

            var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
            writer = new StreamWriter(filePath, append);
            writer.Write(contentsToWriteToFile);
            writer.Close();
        }

        /// <summary>
        /// Read from json file and convert in T object
        /// </summary>
        /// <typeparam name="T">T object</typeparam>
        /// <param name="path">path</param>
        /// <param name="fileName">file name</param>
        /// <returns></returns>
        public static T ReadFromJsonFile<T>(string path, string fileName) where T : new()
        {
            string filePath = path + fileName;
            TextReader reader = null;

            reader = new StreamReader(filePath);
            var fileContents = reader.ReadToEnd();
            reader.Close();
            return JsonConvert.DeserializeObject<T>(fileContents);
        }
    }
}

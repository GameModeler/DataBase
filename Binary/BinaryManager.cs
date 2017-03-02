using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Binary
{
    class BinaryManager
    {
        /// <summary>
        /// Write T object in binary file
        /// </summary>
        /// <typeparam name="T">T object</typeparam>
        /// <param name="path">path</param>
        /// <param name="fileName">file name</param>
        /// <param name="objectToWrite">T object to write</param>
        /// <param name="append">append</param>
        public static void WriteToBinaryFile<T>(string path, string fileName, T objectToWrite, bool append = false)
        {
            string filePath = path + fileName;
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        /// <summary>
        /// Read from binary file and convert in T object
        /// </summary>
        /// <typeparam name="T">T object</typeparam>
        /// <param name="path">path</param>
        /// <param name="fileName">file name</param>
        /// <returns>T object</returns>
        public static T ReadFromBinaryFile<T>(string path, string fileName)
        {
            string filePath = path + fileName;
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
    }
}

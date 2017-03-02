using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataBase.Xml
{
    public class XmlManager
    {
        /// <summary>
        /// Write T object in xml file
        /// </summary>
        /// <typeparam name="T">T object</typeparam>
        /// <param name="path">path</param>
        /// <param name="fileName">file name</param>
        /// <param name="objectToWrite">object to write</param>
        /// <param name="append">append</param>
        public static void WriteToXmlFile<T>(string path, string fileName, T objectToWrite, bool append = false) where T : new()
        {
            string filePath = path + fileName;
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, append);
                serializer.Serialize(writer, objectToWrite);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        /// <summary>
        /// Read from xml file and convert in T object
        /// </summary>
        /// <typeparam name="T">T object</typeparam>
        /// <param name="path">path</param>
        /// <param name="fileName">fileName</param>
        /// <returns>T object</returns>
        public static T ReadFromXmlFile<T>(string path, string fileName) where T : new()
        {
            string filePath = path + fileName;
            TextReader reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(filePath);
                return (T)serializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}

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
    class YamlManager
    {


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


        public static T ReadFromYamlFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                var serializer = new YamlSerializer();
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Yaml.Serialization;

namespace DataBase.Yaml
{
    class YamlManager
    {


        public static void WriteToYamlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {

            TextWriter writer = null;
            try
            {
                var serializer = new YamlSerializer();
                var contentsToWriteToFile = serializer.Serialize(objectToWrite);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }      
        }

   

    }
}

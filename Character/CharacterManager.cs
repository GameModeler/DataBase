using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Character
{
    class CharacterManager
    {



        public CharacterManager()
        {
        }


        // TODO : handle objects list
        public static void WriteToCharacterFile<T>(string path, string fileName, T objectToWrite, bool append = false, string delimiter = "|") where T : new()
        {
            string filePath = path + fileName;
            TextWriter writer = null;
            string contentsToWriteToFile = "";
            try
            {
                contentsToWriteToFile += objectToWrite.GetType() + ">";
                // Catch object properties
                var properties = objectToWrite.GetType().GetProperties();
                foreach (var p in properties)
                {
                    string name = p.Name;
                    var value = p.GetValue(objectToWrite, null);
                    contentsToWriteToFile += name + ":" + value + delimiter;
                }
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

      

        // TODO : handle objects list
        public static T ReadFromCharacterFile<T>(string filePath, char delimiter = '|') where T : new()
        {
            TextReader reader = null;
            T obj = new T();
            
            try
            {
                reader = new StreamReader(filePath);
                string fileContents = reader.ReadToEnd();
                string[] res = fileContents.Split('>');

                // Class control
                if (Type.GetType(res[0]) == typeof(T))
                {
                    
                    string[] properties = res[1].Split(delimiter);
        
                    int i = 0;
                    foreach (var prop in properties)
                    {                      
                        if (string.IsNullOrEmpty(prop) == false)
                        {
                            // Property of obj
                            string objProp = obj.GetType().GetProperties()[i].Name;
                            // Property on file
                            string attribute = prop.Split(':')[0];
                            // Value on file
                            string value = prop.Split(':')[1];

                            // Type of obj property
                            Type typeProp = obj.GetType().GetProperties()[i].PropertyType;
                            // Type of obj
                            Type type = obj.GetType();
                            // Property of obj
                            PropertyInfo propToWrite = type.GetProperty(objProp);

                            // Property of obj == property on file && property is not null && property of obj is ready to write ?
                            if (objProp == attribute && (null != propToWrite && propToWrite.CanWrite))
                            {
                                // Set value on obj property (with cast on value, each save value is a string)
                                propToWrite.SetValue(obj, Convert.ChangeType(value, typeProp), null);
                       
                            }
                        }
                        i += 1;
                    }                 
                }
                return obj;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }


    }
}

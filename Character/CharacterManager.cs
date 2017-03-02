using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DataBase.Character
{
    class CharacterManager
    {
        /// <summary>
        /// Write T object in character in character file
        /// </summary>
        /// <typeparam name="T">T object</typeparam>
        /// <param name="path">path</param>
        /// <param name="fileName">file name</param>
        /// <param name="objectToWrite">T object to write</param>
        /// <param name="append">append</param>
        /// <param name="delimiter">delimite the differents attributes of T object</param>
        public static void WriteToCharacterFile<T>(string path, string fileName, T objectToWrite, bool append = false, string delimiter = "|") where T : new()
        {
            string filePath = path + fileName;
            TextWriter writer = null;
            string contentsToWriteToFile = "";
            try
            {
                var object_name = objectToWrite.GetType();
                contentsToWriteToFile += object_name + ">";
     
                // Catch object properties
                var properties = objectToWrite.GetType().GetProperties();
                int propCount = properties.Count();
                int i = 0;

                foreach (var p in properties)
                {
                    string name = p.Name;
                    var value = p.GetValue(objectToWrite, null);
                    contentsToWriteToFile += name + ":" + value;

                    i += 1;
                    if (i < propCount)
                        contentsToWriteToFile += delimiter;
                    else
                        contentsToWriteToFile += "\n";
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

        /// <summary>
        /// Read character from character file and convert in T object list
        /// </summary>
        /// <typeparam name="T">Object List to read</typeparam>
        /// <param name="path">path</param>
        /// <param name="fileName">file name</param>
        /// <param name="delimiter">delimite the differents attributes</param>
        /// <returns>list T object</returns>
        public static List<T> ReadFromCharacterFile<T>(string path, string fileName, char delimiter = '|') where T : new()
        {
            string filePath = path + fileName;
            List<T> objList = new List<T>();
            var lineCount = File.ReadLines(filePath).Count();
            string line;

            TextReader reader = null;
            try
            {

                reader = new StreamReader(filePath);
                while ((line = reader.ReadLine()) != null)
                {
 
                    T obj = new T();
     
                    string[] res = line.Split('>');
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
                        objList.Add(obj);
                    }
                }                     
                return objList;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}

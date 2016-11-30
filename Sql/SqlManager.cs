using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Sql
{
    class SqlManager
    {

        public static void WriteToSqlFile<T>(string path, string fileName, T objectToWrite, bool append = false, string dbName = "Test", bool createDB = false) where T : new()
        {
            string filePath = path + fileName;
           
            string contentsToWriteToFile = "";
            string insert = "";
            string createTable = "";
            if (append)
            {
                contentsToWriteToFile += "\n\n";
            }

            TextWriter writer = null;
            try
            {
                
                if(createDB)
                {
                    contentsToWriteToFile += "CREATE DATABASE " + dbName + ";\n";
                }
                contentsToWriteToFile += "USE " + dbName + ";\n";

                if (null != objectToWrite)
                {
                    var properties = objectToWrite.GetType().GetProperties();
                    int propCount = properties.Count();
                    string table = objectToWrite.GetType().Name;
                    int i = 0;
                    createTable = "CREATE TABLE " + table + " (\n";
                    insert += "INSERT INTO " + table + " VALUES(";
                    foreach (var p in properties)
                    {
                        i += 1;
                        // Create table
                        string name = p.Name;
                        if(name == "Id")
                        {
                            createTable += name + " INT PRIMARY KEY NOT NULL";
                        }
                        else
                        {
                            createTable += name + " VARCHAR(50)";
                        }
                       
                        // Insert into table
                        var value = p.GetValue(objectToWrite, null);
                        insert += "'" + value + "'";
                        if (i < propCount)
                        {
                            createTable += ",\n";
                            insert += ",";
                        }

                    }
                    createTable += "\n);\n";
                    insert += ");";
                }
                contentsToWriteToFile += createTable;
                contentsToWriteToFile += insert;

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

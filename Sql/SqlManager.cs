using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace DataBase.Sql
{

    public class SqlManager
    {


        /// <summary>
        /// Convert T object in sql script
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToWrite"></param>
        /// <param name="append"></param>
        /// <param name="dbName"></param>
        /// <param name="createDB"></param>
        /// <param name="execute"></param>
        /// <returns></returns>
        public static string ConvertObjectInScript<T>(T objectToWrite, bool append = false, string dbName = "Test", bool createDB = false, bool execute = false) where T : new()
        {
            string contentsToWriteToFile = "";
            string insert = "";
            string createTable = "";
            if (append)
            {
                contentsToWriteToFile += "\n\n";
            }      

            if (createDB)
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
                    // Create table
                    string name = p.Name;
                    Type typeProp = objectToWrite.GetType().GetProperties()[i].PropertyType;
                    String columnName = "";

                    // Insert into table
                    var value = p.GetValue(objectToWrite, null);

                    // Convert C# Type in SQL Type
                    switch (typeProp.Name)
                    {
                        case "Int32":
                            if (name == "Id")
                                columnName = "INT PRIMARY KEY NOT NULL";
                            else
                                columnName = "INT(11)";
                            break;
                        case "Boolean":
                            columnName = "TINYINT(1)";
                            break;
                        case "DateTime":
                            columnName = "DateTime";
                            value = "'" + value + "'";
                            break;
                        case "String":
                        default:
                            columnName = "VARCHAR(255)";
                            value = "'" + value + "'";
                            break;
                    }
                    createTable += name + " " + columnName;
                    insert += value;

                    if (i < propCount - 1)
                    {
                        createTable += ",\n";
                        insert += ",";
                    }
                    i += 1;
                }
                createTable += ");\n";
                insert += ");";
            }
            contentsToWriteToFile += createTable;
            contentsToWriteToFile += insert;
  
            return contentsToWriteToFile;
        }


        /// <summary>
        /// Execute Sql script
        /// </summary>
        /// <param name="script"></param>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        public static void ExecuteStringSql(string script, string user = "root", string pwd = "")
        {

            string myConnectionString = "server=127.0.0.1;Uid= " + user + ";Pwd= " + pwd + ";";
            try
            {
                MySqlConnection conn = new MySqlConnection(myConnectionString);
                conn.Open();
                MySqlCommand myCommand = new MySqlCommand(script, conn);
                MySqlDataReader rdr = myCommand.ExecuteReader();
                conn.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Write to Sql file and execute this script
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="objectToWrite"></param>
        /// <param name="append"></param>
        /// <param name="dbName"></param>
        /// <param name="createDB"></param>
        /// <param name="execute"></param>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        public static void WriteToSqlFile<T>(string path, string fileName, T objectToWrite, bool append = false, 
                                             string dbName = "Test", bool createDB = false, bool execute = false,
                                             string user = "root", string pwd = "") where T : new()
        {
            string filePath = path + fileName;
           
           

            TextWriter writer = null;
            try
            {
                string contentsToWriteToFile = ConvertObjectInScript<T>(objectToWrite, append, dbName, createDB);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
            if(execute)
            {

                
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();

                cmd.StandardInput.WriteLine("mysql -u"+user+" -p"+pwd+" < "+path+fileName);
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
                try
                {
                    Console.WriteLine(cmd.StandardOutput.ReadToEnd());
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
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
                       
                        // Create table
                        string name = p.Name;
                        Type typeProp = objectToWrite.GetType().GetProperties()[i].PropertyType;
                        String columnName = "";

                        // Insert into table
                        var value = p.GetValue(objectToWrite, null);

                        // Convert C# Type in SQL Type
                        switch(typeProp.Name)
                        {
                            case "Int32":
                                if (name == "Id")
                                    columnName = "INT PRIMARY KEY NOT NULL";
                                else
                                    columnName = "INT(11)";
                                break;
                            case "String":
                                columnName = "VARCHAR(255)";
                                value = "'" + value + "'";
                                break;
                            case "Boolean":
                                columnName = "TINYINT(1)";
                                break;
                            case "DateTime":
                                columnName = "DateTime";
                                value = "'" + value + "'";
                                break;
                            default:
                                columnName = "VARCHAR(255)";
                                value = "'" + value + "'";
                                break;
                        }
                        createTable += name + " " + columnName;
                        insert += value;

                        if (i < propCount-1)
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

                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public static Type GetClrType(SqlDbType sqlType)
        {
            switch (sqlType)
            {
                case SqlDbType.BigInt:
                    return typeof(long?);

                case SqlDbType.Binary:
                case SqlDbType.Image:
                case SqlDbType.Timestamp:
                case SqlDbType.VarBinary:
                    return typeof(byte[]);

                case SqlDbType.Bit:
                    return typeof(bool?);

                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                    return typeof(String);

                case SqlDbType.DateTime:
                case SqlDbType.SmallDateTime:
                case SqlDbType.Date:
                case SqlDbType.Time:
                case SqlDbType.DateTime2:
                    return typeof(DateTime?);

                case SqlDbType.Decimal:
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    return typeof(decimal?);

                case SqlDbType.Float:
                    return typeof(double?);

                case SqlDbType.Int:
                    return typeof(Int32?);

                case SqlDbType.Real:
                    return typeof(float?);

                case SqlDbType.UniqueIdentifier:
                    return typeof(Guid?);

                case SqlDbType.SmallInt:
                    return typeof(short?);

                case SqlDbType.TinyInt:
                    return typeof(Boolean);

                case SqlDbType.Variant:
                case SqlDbType.Udt:
                    return typeof(object);

                case SqlDbType.Structured:
                    return typeof(DataTable);

                case SqlDbType.DateTimeOffset:
                    return typeof(DateTimeOffset?);

                default:
                    throw new ArgumentOutOfRangeException("sqlType");
            }
        }



    }
}

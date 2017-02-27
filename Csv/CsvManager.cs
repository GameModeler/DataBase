using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Csv
{
    public class CsvManager
    {
        /// <summary>
        /// Write object in csv file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="objectToWrite"></param>
        /// <param name="append"></param>
        public static void WriteToCsvFile<T>(string path, string fileName, T objectToWrite, bool append = false) where T : new()
        {
            string filePath = path + fileName;
            TextWriter writer = null;
            try
            {
                writer = new StreamWriter(filePath, append);
                var csv = new CsvWriter(writer);
                csv.WriteRecord(objectToWrite);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
 
        public static void ReadFromCsvFile<T>(string filePath) where T : new()
        {

            TextReader reader = null;
            try
            {
               

                reader = new StreamReader(filePath);
                var csv = new CsvReader(reader);
                var test = csv.GetRecords<T>();
                Console.WriteLine(test);
                //return test;
 
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }   
    }
}

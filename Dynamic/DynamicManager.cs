﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace DataBase.Dynamic
{
    public class DynamicManager
    {
        /// <summary>
        /// Create IDictionnary object from mysql database
        /// </summary>
        /// <param name="user">mysql user</param>
        /// <param name="pwd">mysql password</param>
        /// <param name="database">mysql database</param>
        /// <param name="table">mysql table</param>
        /// <returns>IDictionnary object</returns>
        public static IDictionary<string, object> CreateObjectByDatabase(string user, string pwd, string database, string table)
        {

            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=127.0.0.1;Uid= " + user + ";Pwd= " + pwd + ";Database= " + database + ";";
            conn = new MySqlConnection();
            try
            {
                conn = new MySqlConnection(myConnectionString);
                conn.Open();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            string query = "USE " + database + "; SELECT * FROM " + table;
            dynamic dynamicObject = new ExpandoObject();
            Dictionary<string, string> results2 = CreateMySqlCommandDict(query, conn);
            var dic = dynamicObject as IDictionary<string, object>;
            foreach (var t in results2)
            {
                dic[t.Key] = t.Value;
            }
            return dic;
        }

        /// <summary>
        /// Create mysql command array
        /// </summary>
        /// <param name="myExecuteQuery">mysql query</param>
        /// <param name="myConnection">MysqlConnection connection</param>
        /// <param name="openclose">open and close mysql connection</param>
        /// <returns>array of mysql return</returns>
        public static string[] CreateMySqlCommandArray(string query, MySqlConnection connection, Boolean openclose = false)
        {
            MySqlCommand myCommand = new MySqlCommand(query, connection);
            if (openclose)
            {
                myCommand.Connection.Open();
            }

            List<string> results = new List<string>();
            using (var reader = myCommand.ExecuteReader())
            {
                int nbColumns = reader.VisibleFieldCount;
                while (reader.Read())
                {
                    for (int i = 0; i < nbColumns; i++)
                    {
                        results.Add(reader.GetString(i));
                    }
                }
            }

            string[] array = results.ToArray();

            if (openclose)
            {
                connection.Close();
            }

            return array;
        }

        /// <summary>
        /// Create mysql command Dictionnary
        /// </summary>
        /// <param name="myExecuteQuery">mysql query</param>
        /// <param name="myConnection">MysqlConnection connection</param>
        /// <param name="openclose">open and close mysql connection</param>
        /// <returns>Dictionnary of mysql return</returns>
        public static Dictionary<string, string> CreateMySqlCommandDict(string query, MySqlConnection connection, Boolean openclose = false)
        {
            MySqlCommand myCommand = new MySqlCommand(query, connection);
            if (openclose)
            {
                myCommand.Connection.Open();
            }

            Dictionary<string, string> results = new Dictionary<string, string>();
            using (var reader = myCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    int nbColumns = reader.VisibleFieldCount;
                    Console.WriteLine(nbColumns);
                    for (int i = 0; i < nbColumns; i++)
                    {
                        results.Add(reader.GetName(i), reader.GetString(i));
                    }
                }
            }

            if (openclose)
            {
                connection.Close();
            }

            return results;
        }

    }
}

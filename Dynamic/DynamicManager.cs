using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Dynamic
{
    public class DynamicManager
    {
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


        public static string[] CreateMySqlCommandArray(string myExecuteQuery, MySqlConnection myConnection, Boolean openclose = false)
        {
            MySqlCommand myCommand = new MySqlCommand(myExecuteQuery, myConnection);
            if (openclose)
                myCommand.Connection.Open();
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

            string[] tableau = results.ToArray();

            if (openclose)
                myConnection.Close();

            return tableau;
        }

        public static Dictionary<string, string> CreateMySqlCommandDict(string myExecuteQuery, MySqlConnection myConnection, Boolean openclose = false)
        {
            MySqlCommand myCommand = new MySqlCommand(myExecuteQuery, myConnection);
            if (openclose)
                myCommand.Connection.Open();
            Dictionary<string, string> results = new Dictionary<string, string>();
            using (var reader = myCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    int nbColumns = reader.VisibleFieldCount;
                    Console.WriteLine(nbColumns);
                    for(int i = 0; i < nbColumns; i++)
                    {                                         
                        results.Add(reader.GetName(i), reader.GetString(i));
                    }
                }
            }

            if (openclose)
                myConnection.Close();

            return results;
        }

    }
}

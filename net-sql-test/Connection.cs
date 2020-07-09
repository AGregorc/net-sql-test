using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_sql_test
{

    class Kos
    {
        public int id { get; set; }
        public int guid { get; set; }
        public string delovni_nalog { get; set; }
        public DateTime cas_vnosa { get; set; }

        public string Print()
        {
            return String.Format("Kos -> id: {0}, guid: {1}, delovni nalog: {2}, cas vnosa: {3}", id, guid, delovni_nalog, cas_vnosa);
        }
    }

    class Connection
    {
        string server;
        string port;
        string database;
        string username;
        string pass;
        MySqlConnection conn;
        MySqlCommand cmd;
        string time_format = "yyyy-MM-dd HH:mm:ss";

        public Connection()
        {
            // var myJsonString = File.ReadAllText("connection_settings.json");
            // dynamic jToken = JToken.Parse(myJsonString);
            server = "localhost";
            port = "3306";
            database = "ec38";
            username = "ec38";
            pass = "ec38";

            string connStr = "server="+server+";user="+username+";port="+port+";password="+pass+";";
            conn = new MySqlConnection(connStr);

            cmd = new MySqlCommand();
            cmd.Connection = conn;
        }

        public void Drop_and_create_new()
        {
            conn.Open();
            cmd.CommandText = "drop database if exists ec38; create database ec38; use ec38; ";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE kosi ( 
                                id              INT        NOT NULL     AUTO_INCREMENT,
	                            guid            INT,
                                delovni_nalog   VARCHAR(30),
	                            cas_vnosa       TIMESTAMP  NOT NULL     DEFAULT CURRENT_TIMESTAMP,
                                PRIMARY KEY(id)
                            ) ";
            cmd.ExecuteNonQuery();

            conn.Close();
            Console.WriteLine("Table kosi created");
        }

        public Boolean Insert_new_parts(int guid, string delovni_nalog, DateTime cas_vnosa)
        {
            conn.Open();
            cmd.CommandText = String.Format("INSERT INTO kosi(guid, delovni_nalog, cas_vnosa) " +
                                                "VALUES({0}, '{1}', '{2}')",
                                                guid, delovni_nalog, cas_vnosa.ToString(time_format));
            int n_of_rows_affected = cmd.ExecuteNonQuery();

            conn.Close();
            if (n_of_rows_affected == 1)
            {
                return true;
            }
            return false;
        }

        public Kos Get_kos(int guid)
        {
            string Query = "select * from kosi where guid="+guid+";";
            MySqlCommand cmd = new MySqlCommand(Query, conn);
            conn.Open();
            Kos result = null;
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    result = new Kos
                    {
                        id = Int32.Parse(reader["id"].ToString()),
                        guid = Int32.Parse(reader["guid"].ToString()),
                        delovni_nalog = reader["delovni_nalog"].ToString(),
                        cas_vnosa = DateTime.Parse(reader["cas_vnosa"].ToString()),
                    };
                }
            }

            conn.Close();
            return result;
        }

        public List<Kos> Select_all()
        {
            string Query = "select * from kosi;";
            MySqlCommand cmd = new MySqlCommand(Query, conn);
            conn.Open();
            List<Kos> result = new List<Kos>();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Kos kos = new Kos
                    {
                        id = Int32.Parse(reader["id"].ToString()),
                        guid = Int32.Parse(reader["guid"].ToString()),
                        delovni_nalog = reader["delovni_nalog"].ToString(),
                        cas_vnosa = DateTime.Parse(reader["cas_vnosa"].ToString()),
                    };
                    result.Add(kos);
                }
            }

            conn.Close();
            return result;
        }
    }
}

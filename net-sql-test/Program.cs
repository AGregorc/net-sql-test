using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_sql_test
{
    class Program
    {
        static void Main(string[] args)
        {
            Connection conn = new Connection();
            conn.Drop_and_create_new();
            conn.Insert_new_parts(113, "test1", DateTime.Now);
            conn.Insert_new_parts(6, "test2", DateTime.Now);
            conn.Insert_new_parts(4235, "test31", DateTime.Now);
            conn.Insert_new_parts(23, "test111", DateTime.Now);
            List<Kos> all_parts = conn.Select_all();
            Console.WriteLine("Vsi kosi v bazi:");
            foreach (Kos part in all_parts)
            {
                Console.WriteLine("\t" + part.Print());
            }

            GetAndWriteKos(conn, 113);
            GetAndWriteKos(conn, 214314);


            Console.ReadKey();
        }

        static void GetAndWriteKos(Connection conn, int my_guid)
        {
            Kos kos = conn.Get_kos(my_guid);
            Console.WriteLine("Kos z guid " + my_guid + ":");
            if (kos == null)
            {
                Console.WriteLine("\tTa kos ne obstaja!!");
            }
            else
            {
                Console.WriteLine("\t"+kos.Print());
            }
        }
    }
}

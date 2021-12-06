using System.Collections.Generic;
using System.Data.SqlClient;

namespace vis.TableGateway
{
    public class ObjednavkaGateway
    {
        public List<Objednavka> SelectAll()
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();

            var dotaz = "SELECT * FROM Objednavka";
            SqlCommand com = new SqlCommand(dotaz, conn);

            SqlDataReader data = com.ExecuteReader();

            List<Objednavka> list = new List<Objednavka>();


            while (data.Read())
            {
                Objednavka a = new Objednavka(data.GetInt32(0),data.GetInt32(1), data.GetDateTime(2), data.GetInt32(3));
                list.Add(a);

            }

            conn.Close();

            return list;
        }
    }
}
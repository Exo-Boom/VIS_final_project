using System.Collections.Generic;
using System.Data.SqlClient;

namespace vis.TableGateway
{
    public class UzivatelTableGateway
    {
        public List<Uzivatel> SelectAll()
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();

            var dotaz = "SELECT * FROM Uzivatel";
            SqlCommand com = new SqlCommand(dotaz, conn);

            SqlDataReader data = com.ExecuteReader();

            List<Uzivatel> list = new List<Uzivatel>();


            while (data.Read())
            {
                Uzivatel a = new Uzivatel(data.GetInt32(0),data.GetString(1), data.GetString(2), data.GetString(3), data.GetString(4), data.GetString(5)
                    , data.GetString(6), data.GetString(9), data.GetString(7), data.GetString(8), data.GetInt32(10));
                list.Add(a);

            }

            conn.Close();

            return list;
        }
    }
}
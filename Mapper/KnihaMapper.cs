using System.Data;
using System.Data.SqlClient;

namespace vis.Mapper
{
    public class KnihaMapper
    {
        public Kniha SelectById(int id)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "SELECT * FROM Kniha WHERE id_k = @id"; 
            SqlCommand com = new SqlCommand(dotaz,conn);
            com.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;
            SqlDataReader data = com.ExecuteReader();
            data.Read();
            Kniha a = new Kniha(data.GetInt32(0),data.GetString(1),data.GetString(2),data.GetString(3)
                ,data.GetInt32(4),data.GetInt32(5));

            conn.Close();

            return a;
        }
    }
}
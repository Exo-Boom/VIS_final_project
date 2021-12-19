using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataCoreLogic.Data.Logic;
using vis;

namespace DataCoreLogic.Data.Mapper
{
    public class PolozkaObjednavkyMapper
    {
        public List<PolozkaObjednavky> SelectByObjednavka(int id)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();

            var dotaz = "SELECT * FROM Polozky_objednavky WHERE Objednavka_id_obj = @id";
            SqlCommand com = new SqlCommand(dotaz, conn);
            
            com.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;

            SqlDataReader data = com.ExecuteReader();

            List<PolozkaObjednavky> list = new List<PolozkaObjednavky>();
            
            while (data.Read())
            {
                
                PolozkaObjednavky a = new PolozkaObjednavky(data.GetInt32(2),data.GetInt32(3), data.GetInt32(0), data.GetInt32(1));
                list.Add(a);

            }
            conn.Close();

            return list;
        }
        
        
        public List<PolozkaObjednavky> SelectByKniha(int id)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();

            var dotaz = "SELECT * FROM Polozky_objednavky WHERE Kniha_id_k = @id";
            SqlCommand com = new SqlCommand(dotaz, conn);
            
            com.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;

            SqlDataReader data = com.ExecuteReader();

            List<PolozkaObjednavky> list = new List<PolozkaObjednavky>();
            
            while (data.Read())
            {
                
                PolozkaObjednavky a = new PolozkaObjednavky(data.GetInt32(2),data.GetInt32(3), data.GetInt32(0), data.GetInt32(1));
                list.Add(a);

            }
            conn.Close();

            return list;
        }
        
        public int Delete(int id)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "DELETE FROM Polozky_objednavky WHERE Objednavka_id_obj = @Objednavka_id_obj"; 
            SqlCommand com = new SqlCommand(dotaz,conn);
            
            com.Parameters.Add(new SqlParameter("@Objednavka_id_obj", SqlDbType.Int)).Value = id;
            com.Prepare();
            
            int number = com.ExecuteNonQuery();
            
            conn.Close();
            return number;
        }
        
    }
}
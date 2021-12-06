using System;
using System.Data;
using System.Data.SqlClient;

namespace vis.Gateway
{
    public class Polozky_objednavkyGateway
    {
         public void Insert(int id_k,int id_o,int pocet, int cena)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "INSERT INTO Polozky_objednavky(Kniha_id_k,Objednavka_id_obj,pocet,cena) values(@Kniha_id_k,@Objednavka_id_obj,@pocet,@cena)";
            SqlCommand com = new SqlCommand(dotaz,conn);
            
            com.Parameters.Add(new SqlParameter("@Kniha_id_k", SqlDbType.Int)).Value = id_k;
            com.Parameters.Add(new SqlParameter("@Objednavka_id_obj", SqlDbType.Int)).Value = id_o;
            com.Parameters.Add(new SqlParameter("@pocet", SqlDbType.Int)).Value = pocet;
            com.Parameters.Add(new SqlParameter("@cena", SqlDbType.Int)).Value = cena;

            com.Prepare();

            com.ExecuteNonQuery();
            
            conn.Close();
        }
           
        public void Update(int id_k,int id_o,int pocet, int cena)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "UPDATE Polozky_objednavky set pocet = @pocet,cena = @cena WHERE Kniha_id_k = @Kniha_id_k AND Objednavka_id_obj = @Objednavka_id_obj "; 
            SqlCommand com = new SqlCommand(dotaz,conn);
            
            com.Parameters.Add(new SqlParameter("@Kniha_id_k", SqlDbType.Int)).Value = id_k;
            com.Parameters.Add(new SqlParameter("@Objednavka_id_obj", SqlDbType.Int)).Value = id_o;
            com.Parameters.Add(new SqlParameter("@pocet", SqlDbType.Int)).Value = pocet;
            com.Parameters.Add(new SqlParameter("@cena", SqlDbType.Int)).Value = cena;
            com.Prepare();
            
            com.ExecuteNonQuery();
            
            conn.Close();
        }
        
        public void DeleteSingle(int id_obj , int id_k)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "DELETE FROM Polozky_objednavky WHERE Objednavka_id_obj = @Objednavka_id_obj AND Kniha_id_k = @Kniha_id_k"; 
            SqlCommand com = new SqlCommand(dotaz,conn);
            
            com.Parameters.Add(new SqlParameter("@Objednavka_id_obj", SqlDbType.Int)).Value = id_obj;
            com.Parameters.Add(new SqlParameter("@Kniha_id_k", SqlDbType.Int)).Value = id_k;
            com.Prepare();
            
            com.ExecuteNonQuery();
            
            conn.Close();
        }
        
        public void DeleteWholeObjednavka(int id_obj)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "DELETE FROM Polozky_objednavky WHERE Objednavka_id_obj = @Objednavka_id_obj"; 
            SqlCommand com = new SqlCommand(dotaz,conn);
            
            com.Parameters.Add(new SqlParameter("@Objednavka_id_obj", SqlDbType.Int)).Value = id_obj;
            com.Prepare();
            
            com.ExecuteNonQuery();
            
            conn.Close();
        }
    }
}
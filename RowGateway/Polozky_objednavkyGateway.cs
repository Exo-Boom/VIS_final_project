using System;
using System.Data;
using System.Data.SqlClient;

namespace vis.Gateway
{
    public class Polozky_objednavkyGateway : RowGatewayInterface
    {
        public int Id {get; set;}
        public int id_o{get; set;}
        public int pocet{get; set;}
        public int cena{get; set;}
        

        public void Insert()
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "INSERT INTO Polozky_objednavky(Kniha_id_k,Objednavka_id_obj,pocet,cena) values(@Kniha_id_k,@Objednavka_id_obj,@pocet,@cena)";
            SqlCommand com = new SqlCommand(dotaz,conn);
            
            com.Parameters.Add(new SqlParameter("@Kniha_id_k", SqlDbType.Int)).Value = Id;
            com.Parameters.Add(new SqlParameter("@Objednavka_id_obj", SqlDbType.Int)).Value = id_o;
            com.Parameters.Add(new SqlParameter("@pocet", SqlDbType.Int)).Value = pocet;
            com.Parameters.Add(new SqlParameter("@cena", SqlDbType.Int)).Value = cena;

            com.Prepare();

            com.ExecuteNonQuery();
            
            conn.Close();
        }
           
        public void Update()
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "UPDATE Polozky_objednavky set pocet = @pocet,cena = @cena WHERE Kniha_id_k = @Kniha_id_k AND Objednavka_id_obj = @Objednavka_id_obj "; 
            SqlCommand com = new SqlCommand(dotaz,conn);
            
            com.Parameters.Add(new SqlParameter("@Kniha_id_k", SqlDbType.Int)).Value = Id;
            com.Parameters.Add(new SqlParameter("@Objednavka_id_obj", SqlDbType.Int)).Value = id_o;
            com.Parameters.Add(new SqlParameter("@pocet", SqlDbType.Int)).Value = pocet;
            com.Parameters.Add(new SqlParameter("@cena", SqlDbType.Int)).Value = cena;
            com.Prepare();
            
            com.ExecuteNonQuery();
            
            conn.Close();
        }
        
        public int Delete()
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "DELETE FROM Polozky_objednavky WHERE Objednavka_id_obj = @Objednavka_id_obj"; 
            SqlCommand com = new SqlCommand(dotaz,conn);
            
            com.Parameters.Add(new SqlParameter("@Objednavka_id_obj", SqlDbType.Int)).Value = Id;
            com.Prepare();
            
            int number = com.ExecuteNonQuery();
            
            conn.Close();
            return number;
        }
    }
}
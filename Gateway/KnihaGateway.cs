using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace vis.Gateway
{
    public class KnihaGateway
    {
        public int Insert(string Nazev, string ISBN, string Popis, int cena, int pocet)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "INSERT INTO Kniha(nazev,ISBN,Popis,cena,pocet) output inserted.id_k values(@nazev,@ISBN,@Popis,@cena,@pocet)";

            SqlCommand com = new SqlCommand(dotaz,conn);
            
            com.Parameters.Add(new SqlParameter("@Nazev", SqlDbType.VarChar, 50)).Value = Nazev;
            com.Parameters.Add(new SqlParameter("@ISBN", SqlDbType.VarChar, 50)).Value = ISBN;
            com.Parameters.Add(new SqlParameter("@Popis", SqlDbType.Text, 50)).Value = Popis;
            com.Parameters.Add(new SqlParameter("@cena", SqlDbType.Int)).Value = cena;
            com.Parameters.Add("@pocet", SqlDbType.Int).Value = pocet;
            com.Prepare();
            
            
            int new_id = (int) com.ExecuteScalar();
            
            conn.Close();

            return new_id;
        }
        public void Update(int id,string Nazev, string ISBN, string Popis, float cena, int pocet)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "UPDATE Kniha set Nazev=@Nazev,ISBN = @ISBN,Popis = @Popis,cena = @cena,pocet = @pocet WHERE id_k = @id_k"; 
            SqlCommand com = new SqlCommand(dotaz,conn);
            
            com.Parameters.Add(new SqlParameter("@id_k", SqlDbType.Int)).Value = id;
            com.Parameters.Add(new SqlParameter("@Nazev", SqlDbType.VarChar, 50)).Value = Nazev;
            com.Parameters.Add(new SqlParameter("@ISBN", SqlDbType.VarChar, 50)).Value = ISBN;
            com.Parameters.Add(new SqlParameter("@Popis", SqlDbType.Text, 50)).Value = Popis;
            com.Parameters.Add(new SqlParameter("@cena", SqlDbType.Float)).Value = cena;
            com.Parameters.Add("@pocet", SqlDbType.Int).Value = pocet;
            com.Prepare();
            
            com.ExecuteNonQuery();
            
            conn.Close();
        }
        public void Delete(int id)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "DELETE FROM Kniha WHERE id_k = @id_k"; 
            SqlCommand com = new SqlCommand(dotaz,conn);
            
            com.Parameters.Add(new SqlParameter("@id_k", SqlDbType.Int)).Value = id;

            com.Prepare();
            
            com.ExecuteNonQuery();
            
            conn.Close();
        }


        public List<Kniha> SelectAll()
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "SELECT * FROM Kniha"; 
            SqlCommand com = new SqlCommand(dotaz,conn);

            SqlDataReader data = com.ExecuteReader();
            
            List<Kniha> list = new List<Kniha>();
            
            
            while (data.Read())
            {
                Kniha a = new Kniha(data.GetInt32(0),data.GetString(1),data.GetString(2),data.GetString(3)
                    ,data.GetInt32(4),data.GetInt32(5));
                list.Add(a);
                
            }
            conn.Close();

            return list;
        }
        
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
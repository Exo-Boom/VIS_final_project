using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using vis.Mapper;

namespace vis.Gateway
{
    public class KnihaGateway : RowGatewayInterface
    {
        public int Id { get; set; }
        public string Nazev{ get; set; }
        public string ISBN{ get; set; }
        public string Popis{ get; set; }
        public int cena{ get; set; }
        public int pocet{ get; set; }
        
        public void Insert()
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
            
            
            Id = (int) com.ExecuteScalar();
            
            conn.Close();
            
        }
        public void Update()
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "UPDATE Kniha set Nazev=@Nazev,ISBN = @ISBN,Popis = @Popis,cena = @cena,pocet = @pocet WHERE id_k = @id_k"; 
            SqlCommand com = new SqlCommand(dotaz,conn);
            
            com.Parameters.Add(new SqlParameter("@id_k", SqlDbType.Int)).Value = Id;
            com.Parameters.Add(new SqlParameter("@Nazev", SqlDbType.VarChar, 50)).Value = Nazev;
            com.Parameters.Add(new SqlParameter("@ISBN", SqlDbType.VarChar, 50)).Value = ISBN;
            com.Parameters.Add(new SqlParameter("@Popis", SqlDbType.Text, 50)).Value = Popis;
            com.Parameters.Add(new SqlParameter("@cena", SqlDbType.Float)).Value = cena;
            com.Parameters.Add("@pocet", SqlDbType.Int).Value = pocet;
            com.Prepare();
            
            com.ExecuteNonQuery();
            
            conn.Close();
        }
        public int Delete()
        {
            PolozkaObjednavkyMapper mm = new PolozkaObjednavkyMapper();
            List<PolozkaObjednavky> po = mm.SelectByKniha(Id);
            
            if (po.Any())
            {
                pocet = 0;
                cena = 0;
                Popis = "Tato kniha již není v prodeji";
                Update();
                return 0;
            }
            else
            {
               SqlConnection conn = new SqlConnection(Database.connectionString);
               conn.Open();
               
               var dotaz = "DELETE FROM Kniha WHERE id_k = @id_k"; 
               SqlCommand com = new SqlCommand(dotaz,conn);
               
               com.Parameters.Add(new SqlParameter("@id_k", SqlDbType.Int)).Value = Id;
   
               com.Prepare();
               
               int number = com.ExecuteNonQuery();
            
               conn.Close();
               return number;
            }

        }
       
        
        
        public void SelectById()
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "SELECT * FROM Kniha WHERE id_k = @id"; 
            SqlCommand com = new SqlCommand(dotaz,conn);
            com.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = Id;
            SqlDataReader data = com.ExecuteReader();
            data.Read();

            Id = data.GetInt32(0);
            Nazev = data.GetString(1);
            ISBN = data.GetString(2);
            Popis = data.GetString(3);
            cena = data.GetInt32(4);
            pocet = data.GetInt32(5);
            
            conn.Close();

            
        }
    
    }
}
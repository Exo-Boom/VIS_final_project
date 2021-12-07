using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace vis.Gateway
{

    public class RoleGateway: RowGatewayInterface
    {   
        public int Id{ get; set;}
        public string nazev{ get; set;}
        public string popis{ get; set;}
        public void Insert()
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();

            var dotaz =
                "INSERT INTO Role(nazev,popis) output inserted.id_r values(@nazev,@popis)";

            SqlCommand com = new SqlCommand(dotaz, conn);

            com.Parameters.Add(new SqlParameter("@nazev", SqlDbType.VarChar, 30)).Value = nazev;
            com.Parameters.Add(new SqlParameter("@popis", SqlDbType.VarChar, 2000)).Value = popis;
            com.Prepare();


            Id = (int) com.ExecuteScalar();

            conn.Close();
            
        }
        
        public void Update()
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();

            var dotaz =
                "UPDATE Role set nazev=@nazev,popis = @popis WHERE id_r = @id_r";
            SqlCommand com = new SqlCommand(dotaz, conn);

            com.Parameters.Add(new SqlParameter("@id_r", SqlDbType.Int)).Value = Id;
            com.Parameters.Add(new SqlParameter("@nazev", SqlDbType.VarChar, 30)).Value = nazev;
            com.Parameters.Add(new SqlParameter("@popis", SqlDbType.VarChar, 2000)).Value = popis;

            com.Prepare();

            com.ExecuteNonQuery();

            conn.Close();
        }

        public void Delete()
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "DELETE FROM Role WHERE id_r = @id_r";
            SqlCommand com = new SqlCommand(dotaz, conn);

            com.Parameters.Add(new SqlParameter("@id_r", SqlDbType.Int)).Value = Id;

            com.Prepare();

            com.ExecuteNonQuery();

            conn.Close();
        }
        
        public void SelectById()
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();

            var dotaz = "SELECT * FROM Role WHERE id_r = @id";
            SqlCommand com = new SqlCommand(dotaz, conn);
            com.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = Id;
            SqlDataReader data = com.ExecuteReader();
            data.Read();
            
            Id = data.GetInt32(0);
            nazev = data.GetString(1);
            popis = data.GetString(2);
            

            conn.Close();

           
        }
        
    }
}
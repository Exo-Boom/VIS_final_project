using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace vis.Gateway
{
    public class RoleGateway
    {
        public int Insert(string nazev, string popis)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();

            var dotaz =
                "INSERT INTO Role(nazev,popis) output inserted.id_r values(@nazev,@popis)";

            SqlCommand com = new SqlCommand(dotaz, conn);

            com.Parameters.Add(new SqlParameter("@nazev", SqlDbType.VarChar, 30)).Value = nazev;
            com.Parameters.Add(new SqlParameter("@popis", SqlDbType.VarChar, 2000)).Value = popis;
            com.Prepare();


            int new_id = (int) com.ExecuteScalar();

            conn.Close();

            return new_id;
        }
        
        public void Update(int id, string nazev, string popis)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();

            var dotaz =
                "UPDATE Role set nazev=@nazev,popis = @popis WHERE id_r = @id_r";
            SqlCommand com = new SqlCommand(dotaz, conn);

            com.Parameters.Add(new SqlParameter("@id_r", SqlDbType.Int)).Value = id;
            com.Parameters.Add(new SqlParameter("@nazev", SqlDbType.VarChar, 30)).Value = nazev;
            com.Parameters.Add(new SqlParameter("@popis", SqlDbType.VarChar, 2000)).Value = popis;

            com.Prepare();

            com.ExecuteNonQuery();

            conn.Close();
        }

        public void Delete(int id)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "DELETE FROM Role WHERE id_r = @id_r";
            SqlCommand com = new SqlCommand(dotaz, conn);

            com.Parameters.Add(new SqlParameter("@id_r", SqlDbType.Int)).Value = id;

            com.Prepare();

            com.ExecuteNonQuery();

            conn.Close();
        }


        public List<Role> SelectAll()
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();

            var dotaz = "SELECT * FROM Role";
            SqlCommand com = new SqlCommand(dotaz, conn);

            SqlDataReader data = com.ExecuteReader();

            List<Role> list = new List<Role>();


            while (data.Read())
            {
                Role a = new Role(data.GetInt32(0), data.GetString(1), data.GetString(2));
                list.Add(a);

            }

            conn.Close();

            return list;
        }

        public Role SelectById(int id)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();

            var dotaz = "SELECT * FROM Role WHERE id_r = @id";
            SqlCommand com = new SqlCommand(dotaz, conn);
            com.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;
            SqlDataReader data = com.ExecuteReader();
            data.Read();
            Role a = new Role(data.GetInt32(0), data.GetString(1), data.GetString(2));

            conn.Close();

            return a;
        }
        
    }
}
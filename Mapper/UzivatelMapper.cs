using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace vis.Mapper
{
    public class UzivatelMapper
    {
        
        public Uzivatel SelectById(int id)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();

            var dotaz = "SELECT * FROM Uzivatel WHERE id_u = @id";
            SqlCommand com = new SqlCommand(dotaz, conn);
            
            com.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;
            SqlDataReader data = com.ExecuteReader();
            
            data.Read();
            
            Uzivatel a = new Uzivatel(data.GetInt32(0),data.GetString(1), data.GetString(2), data.GetString(3), data.GetString(4), data.GetString(5)
                , data.GetString(6), data.GetString(9), data.GetString(7), data.GetString(8), data.GetInt32(10));

            conn.Close();

            return a;
        }
        
        public List<Uzivatel> SelectByJmeno(string jmeno)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();

            var dotaz = "SELECT * FROM Uzivatel WHERE jmeno like @jmeno OR prijmeni like @jmeno";
            SqlCommand com = new SqlCommand(dotaz, conn);
            
            com.Parameters.Add(new SqlParameter("@jmeno", SqlDbType.Text, 40)).Value = "%"+jmeno+"%";

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
        
        public Uzivatel SelectByEmail(string email)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();

            var dotaz = "SELECT * FROM Uzivatel WHERE email Like @email";
            
            SqlCommand com = new SqlCommand(dotaz, conn);
            
            com.Parameters.Add(new SqlParameter("@email", SqlDbType.Text, 30)).Value = email;
            
            SqlDataReader data = com.ExecuteReader();
            
            data.Read();
            
            Uzivatel a = new Uzivatel(data.GetInt32(0),data.GetString(1), data.GetString(2), data.GetString(3), data.GetString(4), data.GetString(5)
                , data.GetString(6), data.GetString(9), data.GetString(7), data.GetString(8), data.GetInt32(10));

            conn.Close();

            return a;
        }
        public void Update(int id, string jmeno, string prijmeni, string email, string psc, string mesto, string zeme, string ulice, string heslo, string telefon, int roleId)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "UPDATE Uzivatel set jmeno = @jmeno,Prijmeni = @Prijmeni,email = @email,PSC = @PSC,mesto = @mesto, zeme = @zeme, heslo = @heslo, telefon = @telefon, ulice = @ulice, Role_id_r = @Role_id_r WHERE id_u = @id_u"; 
            SqlCommand com = new SqlCommand(dotaz,conn);
            
            com.Parameters.Add(new SqlParameter("@id_u", SqlDbType.Int)).Value = id;
            com.Parameters.Add(new SqlParameter("@jmeno", SqlDbType.VarChar, 30)).Value = jmeno;
            com.Parameters.Add(new SqlParameter("@Prijmeni", SqlDbType.VarChar, 30)).Value = prijmeni;
            com.Parameters.Add(new SqlParameter("@email", SqlDbType.Text, 30)).Value = email;
            com.Parameters.Add(new SqlParameter("@PSC", SqlDbType.Text, 7)).Value = psc;
            com.Parameters.Add(new SqlParameter("@mesto", SqlDbType.Text, 20)).Value = mesto;
            com.Parameters.Add(new SqlParameter("@zeme", SqlDbType.Text, 30)).Value = zeme;
            com.Parameters.Add(new SqlParameter("@heslo", SqlDbType.Text, 64)).Value = heslo;
            com.Parameters.Add(new SqlParameter("@telefon", SqlDbType.Text, 30)).Value = telefon;
            com.Parameters.Add(new SqlParameter("@ulice", SqlDbType.Text, 30)).Value = ulice;
            com.Parameters.Add(new SqlParameter("@Role_id_r", SqlDbType.Int)).Value = roleId;
            com.Prepare();
            
            com.ExecuteNonQuery();
            
            conn.Close();
        }
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
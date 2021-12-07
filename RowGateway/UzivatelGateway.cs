using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using vis.Mapper;

namespace vis.Gateway
{
    public class UzivatelGateway : RowGatewayInterface
    {
        public int Id { get; set; }
        public string jmeno{ get; set; }
        public string prijmeni{ get; set; }
        public string email{ get; set; }
        
        public Adresa Adr = new Adresa();
        public string heslo{ get; set; }
        public string telefon{ get; set; }
        public int roleId{ get; set; }
        
        public void Insert()
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "INSERT INTO Uzivatel(jmeno,Prijmeni,email,PSC,mesto,zeme,heslo,telefon,ulice,Role_id_r) output inserted.id_u values(@jmeno,@Prijmeni,@email,@PSC,@mesto,@zeme,@heslo,@telefon,@ulice,@Role_id_r)";
            SqlCommand com = new SqlCommand(dotaz,conn);

            com.Parameters.Add(new SqlParameter("@jmeno", SqlDbType.VarChar, 30)).Value = jmeno;
            com.Parameters.Add(new SqlParameter("@Prijmeni", SqlDbType.VarChar, 30)).Value = prijmeni;
            com.Parameters.Add(new SqlParameter("@email", SqlDbType.Text, 30)).Value = email;
            com.Parameters.Add(new SqlParameter("@PSC", SqlDbType.Text, 7)).Value = Adr.Psc;
            com.Parameters.Add(new SqlParameter("@mesto", SqlDbType.Text, 20)).Value = Adr.Mesto;
            com.Parameters.Add(new SqlParameter("@zeme", SqlDbType.Text, 30)).Value = Adr.Zeme;
            com.Parameters.Add(new SqlParameter("@heslo", SqlDbType.Text, 64)).Value = heslo;
            com.Parameters.Add(new SqlParameter("@telefon", SqlDbType.Text, 30)).Value = telefon;
            com.Parameters.Add(new SqlParameter("@ulice", SqlDbType.Text, 30)).Value = Adr.Ulice;
            com.Parameters.Add(new SqlParameter("@Role_id_r", SqlDbType.Int)).Value = roleId;

            com.Prepare();

            Id = (int) com.ExecuteScalar();
            
            conn.Close();


        }
           
        public void Update()
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "UPDATE Uzivatel set jmeno = @jmeno,Prijmeni = @Prijmeni,email = @email,PSC = @PSC,mesto = @mesto, zeme = @zeme, heslo = @heslo, telefon = @telefon, ulice = @ulice, Role_id_r = @Role_id_r WHERE id_u = @id_u"; 
            SqlCommand com = new SqlCommand(dotaz,conn);
            
            com.Parameters.Add(new SqlParameter("@id_u", SqlDbType.Int)).Value = Id;
            com.Parameters.Add(new SqlParameter("@jmeno", SqlDbType.VarChar, 30)).Value = jmeno;
            com.Parameters.Add(new SqlParameter("@Prijmeni", SqlDbType.VarChar, 30)).Value = prijmeni;
            com.Parameters.Add(new SqlParameter("@email", SqlDbType.Text, 30)).Value = email;
            com.Parameters.Add(new SqlParameter("@PSC", SqlDbType.Text, 7)).Value = Adr.Psc;
            com.Parameters.Add(new SqlParameter("@mesto", SqlDbType.Text, 20)).Value = Adr.Mesto;
            com.Parameters.Add(new SqlParameter("@zeme", SqlDbType.Text, 30)).Value = Adr.Zeme;
            com.Parameters.Add(new SqlParameter("@heslo", SqlDbType.Text, 64)).Value = heslo;
            com.Parameters.Add(new SqlParameter("@telefon", SqlDbType.Text, 30)).Value = telefon;
            com.Parameters.Add(new SqlParameter("@ulice", SqlDbType.Text, 30)).Value = Adr.Ulice;
            com.Parameters.Add(new SqlParameter("@Role_id_r", SqlDbType.Int)).Value = roleId;
            com.Prepare();
            
            com.ExecuteNonQuery();
            
            conn.Close();
        }
        
        public void Delete()
        {
            ObjednavkaMapper om = new ObjednavkaMapper();

            om.SelectByUser(Id);
            
            List<Objednavka> list = new List<Objednavka>();

            for (int i = 0; i < list.Count; i++)
            {
                om.Delete(list[i].Id);
            }
            
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "DELETE FROM Uzivatel WHERE id_u = @id_u"; 
            SqlCommand com = new SqlCommand(dotaz,conn);
            
            com.Parameters.Add(new SqlParameter("@id_u", SqlDbType.Int)).Value = Id;

            com.Prepare();
            
            com.ExecuteNonQuery();
            
            conn.Close();
        }

    }
}
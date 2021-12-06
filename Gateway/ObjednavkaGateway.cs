using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace vis.Gateway
{
    public class ObjednavkaGateway
    {
        public int Insert(DateTime datum, int id_u, List<(Kniha k,int i)> k)
        {
            int sum=0;
            for (int i = 0; i < k.Count; i++)
            {
                sum += (k[i].Item1.Cena)*k[i].Item2;
            }
            
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "INSERT INTO Objednavka(celkova_cena,datum,Uzivatel_id_u) output inserted.id_obj values(@celkova_cena,@datum,@Uzivatel_id_u)";
            SqlCommand com = new SqlCommand(dotaz,conn);

            com.Parameters.Add(new SqlParameter("@celkova_cena", SqlDbType.Int)).Value = sum;
            com.Parameters.Add(new SqlParameter("@datum", SqlDbType.DateTime)).Value = datum;
            com.Parameters.Add(new SqlParameter("@Uzivatel_id_u", SqlDbType.Int)).Value = id_u;

            com.Prepare();

            int new_id = (int) com.ExecuteScalar();
            
            conn.Close();
            KnihaGateway kg = new KnihaGateway();
            
            
            Polozky_objednavkyGateway p = new Polozky_objednavkyGateway();
            try
            {
                for (int i = 0; i < k.Count; i++)
                {
                    p.Insert(k[i].Item1.Id,new_id,k[i].Item2,k[i].Item1.Cena);
                    kg.Update(k[i].Item1.Id,k[i].Item1.Nazev,k[i].Item1.Isbn,k[i].Item1.Popis,k[i].Item1.Cena,k[i].Item1.Pocet - k[i].Item2);
                }
            }
            catch (SqlException e)
            {
                p.DeleteWholeObjednavka(new_id);
                Console.WriteLine("Došlo k chybě: \n"+ e);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Pokračujte stisknutím ENTER");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine();
                return -1;
            }



            return new_id;
        }
           
        public void Update(int id, int celkova_cena, DateTime datum, int id_u)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "UPDATE Objednavka set celkova_cena = @celkova_cena,datum = @datum,Uzivatel_id_u = @Uzivatel_id_u WHERE id_obj = @id_obj"; 
            SqlCommand com = new SqlCommand(dotaz,conn);
            
            com.Parameters.Add(new SqlParameter("@id_obj", SqlDbType.Int)).Value = id;
            com.Parameters.Add(new SqlParameter("@celkova_cena", SqlDbType.Int)).Value = celkova_cena;
            com.Parameters.Add(new SqlParameter("@datum", SqlDbType.DateTime)).Value = datum;
            com.Parameters.Add(new SqlParameter("@Uzivatel_id_u", SqlDbType.Int)).Value = id_u;
            com.Prepare();
            
            com.ExecuteNonQuery();
            
            conn.Close();
        }
        
        public void Delete(int id)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();
            
            var dotaz = "DELETE FROM Objednavka WHERE id_obj = @id_obj"; 
            SqlCommand com = new SqlCommand(dotaz,conn);
            
            com.Parameters.Add(new SqlParameter("@id_obj", SqlDbType.Int)).Value = id;

            com.Prepare();
            
            com.ExecuteNonQuery();
            
            conn.Close();
        }
        
        public List<Objednavka> SelectAll()
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();

            var dotaz = "SELECT * FROM Objednavka";
            SqlCommand com = new SqlCommand(dotaz, conn);

            SqlDataReader data = com.ExecuteReader();

            List<Objednavka> list = new List<Objednavka>();


            while (data.Read())
            {
                Objednavka a = new Objednavka(data.GetInt32(0),data.GetInt32(1), data.GetDateTime(2), data.GetInt32(3));
                list.Add(a);

            }

            conn.Close();

            return list;
        }

        public Objednavka SelectById(int id)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();

            var dotaz = "SELECT * FROM Objednavka WHERE id_obj = @id";
            SqlCommand com = new SqlCommand(dotaz, conn);
            
            com.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;
            SqlDataReader data = com.ExecuteReader();
            
            data.Read();
            
            Objednavka a = new Objednavka(data.GetInt32(0),data.GetInt32(1), data.GetDateTime(2), data.GetInt32(3));

            conn.Close();

            return a;
        }
        
        public List<Objednavka> SelectByUser(int id)
        {
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();

            var dotaz = "SELECT * FROM Objednavka WHERE Uzivatel_id_u = @id";
            SqlCommand com = new SqlCommand(dotaz, conn);
            
            com.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;

            SqlDataReader data = com.ExecuteReader();

            List<Objednavka> list = new List<Objednavka>();
            
            while (data.Read())
            {
                Objednavka a = new Objednavka(data.GetInt32(0),data.GetInt32(1), data.GetDateTime(2), data.GetInt32(3));
                list.Add(a);

            }
            conn.Close();

            return list;
        }
        
    }
}
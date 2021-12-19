using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataCoreLogic.Data.Gateway;
using DataCoreLogic.Data.Logic;
using vis;

namespace DataCoreLogic.Data.Mapper
{
    public class ObjednavkaMapper
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
                    p.id_o = new_id;
                    p.Id = k[i].Item1.Id;
                    p.pocet = k[i].Item2;
                    p.cena = k[i].Item1.Cena;
                    p.Insert();
                    kg.Id = k[i].Item1.Id;
                    kg.Nazev = k[i].Item1.Nazev;
                    kg.ISBN = k[i].Item1.Isbn;
                    kg.Popis = k[i].Item1.Popis;
                    kg.cena = k[i].Item1.Cena;
                    kg.pocet = k[i].Item1.Pocet - k[i].Item2;
                    kg.Update();
                }
            }
            catch (SqlException)
            {
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
        
        public int Delete(int id)
        {
            int number = 0;
            PolozkaObjednavkyMapper p = new PolozkaObjednavkyMapper();
            
            number = p.Delete(id);
            
            SqlConnection conn = new SqlConnection(Database.connectionString);
            conn.Open();            

            var dotaz = "DELETE FROM Objednavka WHERE id_obj = @id_obj"; 
            SqlCommand com = new SqlCommand(dotaz,conn);
            
            com.Parameters.Add(new SqlParameter("@id_obj", SqlDbType.Int)).Value = id;
            
            com.Prepare();

            number += com.ExecuteNonQuery();
            
            conn.Close();
            return number;
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
    }
}
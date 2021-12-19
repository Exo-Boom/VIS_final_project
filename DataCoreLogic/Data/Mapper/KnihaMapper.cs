using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataCoreLogic.Data.Logic;
using vis;

namespace DataCoreLogic.Data.Mapper
{
    public class KnihaMapper
    {
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
        
    }
}
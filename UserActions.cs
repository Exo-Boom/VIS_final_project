using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using vis.Gateway;
using vis.Mapper;

namespace vis
{
    public class UserActions
    {

        public Uzivatel Login(string email, string heslo)
        {
            UzivatelMapper um = new UzivatelMapper();
            
            try
            {
                Uzivatel logged = um.SelectByEmail(email);
                
                if (logged.Heslo == hash(heslo))
                {
                    return logged;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            return null;
        }
        
        
        public bool Register(string jmeno, string prijmeni, string email, string psc, string mesto, string zeme, string ulice, string heslo, string telefon, int role = 2)
        {
            UzivatelGateway ug = new UzivatelGateway();

            heslo = hash(heslo);

            try
            {
                ug.jmeno = jmeno;
                ug.prijmeni = prijmeni;
                ug.email = email;
                ug.Adr.Psc = psc;
                ug.Adr.Mesto = mesto;
                ug.Adr.Zeme = zeme;
                ug.Adr.Ulice = ulice;
                ug.heslo = heslo;
                ug.telefon = telefon;
                ug.roleId = role;
                ug.Insert();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }
        
        public static String hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())            
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
        
        
    }
}
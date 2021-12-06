namespace vis
{
    public class Uzivatel : Identifikator
    {
        public int Id { get; set; }

        public string Jmeno{ get; set; }
         
        public string Prijmeni{ get; set; }
         
        public string Email{ get; set; }

        public string Mesto{ get; set; }
      
        public string Zeme{ get; set; }

        public string Psc{ get; set; }
        public string Ulice{ get; set; }

        public string Heslo{ get; set; }
         
        public string Telefon{ get; set; }

        public int role_id{ get; set; }

        public int Objednavky
        {
            get => default;
            set
            {
            }
        }

        public Uzivatel(int idU, string jmeno, string prijmeni, string email, string psc, string mesto, string zeme, string ulice, string heslo, string telefon, int roleId)
        {
            Mesto = mesto;
            Zeme = zeme;
            Ulice = ulice;
            role_id = roleId;
            Id = idU;
            Jmeno = jmeno;
            Prijmeni = prijmeni;
            Email = email;
            Psc = psc;
            Heslo = heslo;
            Telefon = telefon;
        }

        public Uzivatel(string jmeno, string prijmeni, string email, string psc, string mesto, string zeme, string ulice, string heslo, string telefon, int roleId)
        {
            Mesto = mesto;
            Zeme = zeme;
            Ulice = ulice;
            role_id = roleId;
            Id = -1;
            Jmeno = jmeno;
            Prijmeni = prijmeni;
            Email = email;
            Psc = psc;
            Heslo = heslo;
            Telefon = telefon;
            Psc = psc;

            
        }

    }
}
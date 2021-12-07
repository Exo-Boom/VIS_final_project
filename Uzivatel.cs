namespace vis
{
    public class Uzivatel : Identifikator
    {
        public int Id { get; set; }

        public string Jmeno{ get; set; }
         
        public string Prijmeni{ get; set; }
         
        public string Email{ get; set; }
        
        public Adresa Adr{ get; set; }

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
            Adr.Mesto = mesto;
            Adr.Zeme = zeme;
            Adr.Ulice = ulice;
            role_id = roleId;
            Id = idU;
            Jmeno = jmeno;
            Prijmeni = prijmeni;
            Email = email;
            Adr.Psc = psc;
            Heslo = heslo;
            Telefon = telefon;
        }

        public Uzivatel(string jmeno, string prijmeni, string email, string psc, string mesto, string zeme, string ulice, string heslo, string telefon, int roleId)
        {
            Adr.Mesto = mesto;
            Adr.Zeme = zeme;
            Adr.Ulice = ulice;
            role_id = roleId;
            Id = -1;
            Jmeno = jmeno;
            Prijmeni = prijmeni;
            Email = email;
            Adr.Psc = psc;
            Heslo = heslo;
            Telefon = telefon;


        }

    }
}
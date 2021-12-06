namespace vis
{
    public class Kniha : Identifikator
        {

            public int Id { get; set; }
            public string Nazev{ get; set; }
            public string Isbn{ get; set; }
            public string Popis{ get; set; }
            
            public int Cena{ get; set; }
            
            public int Pocet{ get; set; }
        
        
            public Kniha(int idK, string nazev, string isbn, string popis,int Cena,int Pocet)
            {
                Id = idK;
                Nazev = nazev;
                Isbn = isbn;
                Popis = popis;
                this.Cena = Cena;
                this.Pocet = Pocet;
            }

            public Kniha(string nazev, string isbn, string popis,int Cena,int Pocet)
            {
                Id = -1;
                Nazev = nazev;
                Isbn = isbn;
                Popis = popis;
                this.Cena = Cena;
                this.Pocet = Pocet;
            }
    }
}
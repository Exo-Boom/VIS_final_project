using System.Xml.Serialization;

namespace DataCoreLogic.Data.Logic
{
    public class Kniha : Identifikator
        {
            [XmlAttribute]
            public int Id { get; set; }
            [XmlAttribute]
            public string Nazev{ get; set; }
            [XmlAttribute]
            public string Isbn{ get; set; }
            [XmlAttribute]
            public string Popis{ get; set; }
            [XmlAttribute]
            public int Cena{ get; set; }
            [XmlAttribute]
            
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

            private Kniha(){}
        }
}
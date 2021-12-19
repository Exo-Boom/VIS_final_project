using System;

namespace DataCoreLogic.Data.Logic
{
    public class Objednavka : Identifikator
    {
        public int Id{ get; set; }
        public int Celkova_cena{ get; set; }
        public DateTime Cas{ get; set; }
        public int Uzivatel_id_u{ get; set; }

        public Objednavka(int idObjednavka, int celkovaCena, DateTime cas, int uzivatelIdU)
        {
            Id = idObjednavka;
            Celkova_cena = celkovaCena;
            Cas = cas;
            Uzivatel_id_u = uzivatelIdU;
        }
        public Objednavka(int celkovaCena, DateTime cas, int uzivatelIdU)
        {
            Id = -1;
            Celkova_cena = celkovaCena;
            Cas = DateTime.Now;
            Uzivatel_id_u = uzivatelIdU;
        }
    }
}
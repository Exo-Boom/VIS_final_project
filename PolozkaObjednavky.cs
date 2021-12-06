namespace vis
{
    public class PolozkaObjednavky
    {
        public int Id {get; set;}
        public int id_o{get; set;}
        public int pocet{get; set;}
        public int cena{get; set;}

        public PolozkaObjednavky(int id, int idO, int pocet, int cena)
        {
            Id = id;
            id_o = idO;
            this.pocet = pocet;
            this.cena = cena;
        }
        
    }
}
namespace vis
{
    public class Role : Identifikator
    {
        public int Id { get; set; }

        public string Nazev{ get; set; }

        public string Popis{ get; set; }

        public Role(int idR, string nazev, string popis)
        {
            Id = idR;
            Nazev = nazev;
            Popis = popis;
        }
        
        public Role(string nazev, string popis)
        {
            Id = -1;
            Nazev = nazev;
            Popis = popis;
        }
    }
}
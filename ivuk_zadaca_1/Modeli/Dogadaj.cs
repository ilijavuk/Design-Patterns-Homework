namespace ivuk_zadaca_2.Modeli
{
    public class Dogadaj
    {
        public int Broj { get; set; }
        public string Min { get; set; }
        public int Vrsta { get; set; }
        public Klub Klub { get; set; }
        public Igrac Igrac { get; set; }
        public Igrac Zamjena { get; set; }

        public Dogadaj(int broj, string min, int vrsta, Klub klub, Igrac igrac, Igrac zamjena)
        {
            Broj = broj;
            Min = min;
            Vrsta = vrsta;
            Klub = klub;
            Igrac = igrac;
            Zamjena = zamjena;
        }
    }
}

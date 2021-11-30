using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_2.Modeli
{
    public class Sastav
    {
        public int Broj { get; set; }
        public Klub Klub { get; set; }
        public string Vrsta { get; set; }
        public Igrac Igrac { get; set; }
        public string Pozicija { get; set; }

        public Sastav(int broj, Klub klub, string vrsta, Igrac igrac, string pozicija)
        {
            Broj = broj;
            Klub = klub;
            Vrsta = vrsta;
            Igrac = igrac;
            Pozicija = pozicija;
        }
    }
}

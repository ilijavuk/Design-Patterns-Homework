using ivuk_zadaca_3.PomocneKlase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_3.Modeli
{
    public class Sastav: PrvenstvoComposite
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
            NazivRazine = NaziviRazina.Sastav;
        }

        public override void IspisiInfo()
        {
            Console.WriteLine($"Sastav: {Broj} {Klub.oznaka} {Vrsta} {Igrac.ImeIPrezime} {Pozicija}");
        }

        public string VratiInfo()
        {
            return $"{Pozicija} {Igrac.ImeIPrezime}";
        }
    }
}

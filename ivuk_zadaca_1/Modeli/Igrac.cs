using ivuk_zadaca_2.PomocneKlase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_2.Modeli
{
    public class Igrac: Osoba
    {
        public Klub Klub { get; set; }
        public string Pozicije { get; set; }
        public string Roden { get; set; }

        public Igrac(Klub klub, string imeIPrezime, string pozicije, string roden)
        {
            Klub = klub;
            ImeIPrezime = imeIPrezime;
            Pozicije = pozicije;
            Roden = roden;
            NazivRazine = NaziviRazina.Igrac;
        }

        public override string ToString()
        {
            return $"{Klub.oznaka} | {ImeIPrezime} | {Pozicije} | {Roden}" ;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_1.Modeli
{
    public class Igrac
    {
        public Klub Klub { get; set; }
        public string ImeIPrezime { get; set; }
        public string Pozicije { get; set; }
        public string Roden { get; set; }

        public Igrac(Klub klub, string imeIPrezime, string pozicije, string roden)
        {
            Klub = klub;
            ImeIPrezime = imeIPrezime;
            Pozicije = pozicije;
            Roden = roden;
        }

        public override string ToString()
        {
            return $"{Klub.oznaka} | {ImeIPrezime} | {Pozicije} | {Roden}" ;
        }
    }
}

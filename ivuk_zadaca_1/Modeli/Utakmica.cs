using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_1.Modeli
{
    public class Utakmica
    {
        public int Broj { get; set; }
        public int Kolo { get; set; }
        public Klub Domacin { get; set; }
        public Klub Gost { get; set; }
        public string Pocetak { get; set; }

        public Utakmica(int broj, int kolo, Klub domacin, Klub gost, string pocetak)
        {
            Broj = broj;
            Kolo = kolo;
            Domacin = domacin;
            Gost = gost;
            Pocetak = pocetak;
        }
    }
}

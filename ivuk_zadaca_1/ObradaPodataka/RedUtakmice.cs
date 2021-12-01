using ivuk_zadaca_2.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_2.ObradaPodataka
{
    public class RedUtakmice
    {
        public int Kolo { get; set; }
        public string DatumIVrijeme { get; set; }
        public Klub Domacin { get; set; }
        public Klub Gost { get; set; }
        public string Rezultat { get; set; }

        public RedUtakmice(int kolo, string datumIVrijeme, Klub domacin, Klub gost, string rezultat)
        {
            Kolo = kolo;
            DatumIVrijeme = datumIVrijeme;
            Domacin = domacin;
            Gost = gost;
            Rezultat = rezultat;
        }

        public override string ToString()
        {
            return string.Format("{0, 4}  {1, -20} {2, -20} {3,-20} {4,-20}",
                    Kolo, DatumIVrijeme, Domacin.naziv, Gost.naziv, Rezultat);
        }
    }
}

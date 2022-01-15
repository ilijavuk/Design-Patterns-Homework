using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_3.UcitavanjePodataka
{
    public class UcitavanjeDatotekaFactory
    {
        public UcitavanjeDatoteka DohvatiUcitavac(string argument)
        {
            switch (argument)
            {
                case "-k": return new UcitavanjeKlubova();
                case "-i": return new UcitavanjeIgraca();
                case "-s": return new UcitavanjeSastava();
                case "-u": return new UcitavanjeUtakmica();
                case "-d": return new UcitavanjeDogadaja();
                case "NU": return new UcitavanjeUtakmica();
                case "NS": return new UcitavanjeSastava();
                case "ND": return new UcitavanjeDogadaja();
                default: return null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_1.UcitavanjeDatoteka
{
    public class UcitavanjeDatoteka
    {
        public UcitavanjeDatoteka DohvatiUcitavac(string argument)
        {
            switch(argument)
            {
                case "-k": return new UcitavanjeKlubova();
                case "-i": return new UcitavanjeIgraca();
                case "-s": return new UcitavanjeSastava();
                case "-u": return new UcitavanjeUtakmica();
                case "-d": return new UcitavanjeDogadaja();
                default: return null;
            }
        }
    }
}

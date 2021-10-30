using ivuk_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_1
{
    public sealed class Prvenstvo
    {
        public List<Klub> listaKlubova;
        public List<Igrac> listaIgraca;
        public List<Sastav> listaSastava;
        public List<Utakmica> listaUtakmica;
        public List<Dogadaj> listaDogadaja;
        private Prvenstvo() {}

        public static Prvenstvo Instance { get { return Ugnijezdeno.instance; } }

        private class Ugnijezdeno
        {
            static Ugnijezdeno() {}
            internal static readonly Prvenstvo instance = new Prvenstvo();
        }

        internal void ispisiKola(string unos)
        {
            throw new NotImplementedException();
        }

        internal void ispisiStrijelce(string unos)
        {
            throw new NotImplementedException();
        }

        internal void ispisiKartone(string unos)
        {
            throw new NotImplementedException();
        }

        internal void ispisiRezultate(string unos)
        {
            throw new NotImplementedException();
        }
    }
}

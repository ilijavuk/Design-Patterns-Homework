using ivuk_zadaca_2.Modeli;
using ivuk_zadaca_2.ObradaPodataka;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ivuk_zadaca_2
{
    public sealed class Prvenstvo
    {
        public List<Klub> listaKlubova;
        public List<Igrac> listaIgraca;
        public List<Sastav> listaSastava;
        public List<Utakmica> listaUtakmica;
        public List<Dogadaj> listaDogadaja;
        private IspisTabliceFactory obradaFactory = new IspisTabliceFactory();
        private Prvenstvo() {}

        public static Prvenstvo Instance { get { return Ugnijezdeno.instance; } }

        private class Ugnijezdeno
        {
            static Ugnijezdeno() {}
            internal static readonly Prvenstvo instance = new Prvenstvo();
        }

        internal void ispisiTablicu(string unos)
        {
            string[] unosRazdvojen = unos.Split(' ');

            if (Regex.IsMatch(unos, @"^IZLAZ$")) Environment.Exit(0);
            
            if(!Regex.IsMatch(unos, @"^T( \d+)?$") &&
               !Regex.IsMatch(unos, @"^S( \d+)?$") &&
               !Regex.IsMatch(unos, @"^K( \d+)?$") &&
               !Regex.IsMatch(unos, @"^R [a-zA-Z]+( \d+)?$"))
            {
                Console.WriteLine("Ta opcija ne postoji"); return;
            }

            obradaFactory.DohvatiIspis(unosRazdvojen[0])
                .IspisiTablicu(unosRazdvojen, Instance);
        }
    }
}

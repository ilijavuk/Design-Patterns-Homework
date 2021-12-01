using ivuk_zadaca_2.Modeli;
using ivuk_zadaca_2.ObradaPodataka;
using ivuk_zadaca_2.UcitavanjePodataka;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ivuk_zadaca_2
{
    public sealed class Prvenstvo
    {
        public List<PrvenstvoComposite> listaKlubova;
        private readonly IspisTabliceFactory obradaFactory = new IspisTabliceFactory();
        private readonly UcitavanjeDatotekaFacade ucitavanjeFacade = new UcitavanjeDatotekaFacade();
        private Prvenstvo() {}

        public static Prvenstvo Instance { get { return Ugnijezdeno.instance; } }

        private class Ugnijezdeno
        {
            static Ugnijezdeno() {}
            internal static readonly Prvenstvo instance = new Prvenstvo();
        }

        internal void IspisiTablicu(string unos)
        {
            string[] unosRazdvojen = unos.Split(' ');

            if (Regex.IsMatch(unos, @"^IZLAZ$")) Environment.Exit(0);
            
            if(!Regex.IsMatch(unos, @"^(T|S|K)( \d+)?$") &&
               !Regex.IsMatch(unos, @"^R [a-zA-Z]+( \d+)?$") &&
               !Regex.IsMatch(unos, @"^(NU|NS|ND|) [\w\-.]+\.csv$") &&
               !Regex.IsMatch(unos, @"^D \d [a-zA-Z]+ [a-zA-Z]+ \d$"))
            {
                Console.WriteLine("Ta opcija ne postoji"); return;
            }

            if (unos[0] == 'N')
            {
                ucitavanjeFacade.NaknadnoUcitavanje(unos);
            } else
            {
                obradaFactory.DohvatiIspis(unosRazdvojen[0])
                    .IspisiTablicu(unosRazdvojen);
            }
        }
    }
}

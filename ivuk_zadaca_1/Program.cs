using ivuk_zadaca_1.Modeli;
using ivuk_zadaca_1.UcitavanjeDatoteka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ivuk_zadaca_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");
            string argumenti = string.Join(" ", args) + " ";
            Regex regex = new Regex(@"^(-(d|s|i|u|k) [\w\-.]+\.csv )+$");
            bool argumentiURedu = regex.IsMatch(argumenti);

            if(!argumentiURedu)
            {
                Console.WriteLine("Argumenti nisu u redu. Izlazim iz programa.");
                Environment.Exit(0);
            }

            Prvenstvo prvenstvo = Prvenstvo.Instance;
            UcitavanjeKlubova ucitavanjeKlubova = new UcitavanjeKlubova();
            UcitavanjeIgraca ucitavanjeIgraca = new UcitavanjeIgraca();
            UcitavanjeSastava ucitavanjeSastava = new UcitavanjeSastava();
            UcitavanjeUtakmica ucitavanjeUtakmica = new UcitavanjeUtakmica();
            UcitavanjeDogadaja ucitavanjeDogadaja = new UcitavanjeDogadaja();
            string unos;

            Console.WriteLine("Započinjem učitavanje datoteka");
            string kluboviDat = args[args.ToList().FindIndex(arg => arg == "-k")+1];
            prvenstvo.listaKlubova = ucitavanjeKlubova
                .DohvatiPodatke(kluboviDat, prvenstvo).Cast<Klub>().ToList();

            string igraciDat = args[args.ToList().FindIndex(arg => arg == "-i") + 1];
            prvenstvo.listaIgraca = ucitavanjeIgraca
                .DohvatiPodatke(igraciDat, prvenstvo).Cast<Igrac>().ToList();

            string sastaviDat = args[args.ToList().FindIndex(arg => arg == "-s") + 1];
            prvenstvo.listaSastava = ucitavanjeSastava
                .DohvatiPodatke(sastaviDat, prvenstvo).Cast<Sastav>().ToList();

            string utakmiceDat = args[args.ToList().FindIndex(arg => arg == "-u") + 1];
            prvenstvo.listaUtakmica = ucitavanjeUtakmica
                .DohvatiPodatke(utakmiceDat, prvenstvo).Cast<Utakmica>().ToList();

            string dogadajiDat = args[args.ToList().FindIndex(arg => arg == "-d") + 1];
            prvenstvo.listaDogadaja = ucitavanjeDogadaja
                .DohvatiPodatke(dogadajiDat, prvenstvo).Cast<Dogadaj>().ToList();

            while (true)
            {
                Console.WriteLine("\nOdaberite opciju (IZLAZ za izlaz):\n");
                unos = Console.ReadLine();
                switch(true)
                {
                    case bool _ when Regex.IsMatch(unos, @"^T( \d+)?$"):
                        prvenstvo.ispisiT(unos); break;
                    case bool _ when Regex.IsMatch(unos, @"^S( \d+)?$"):
                        prvenstvo.ispisiS(unos); break;
                    case bool _ when Regex.IsMatch(unos, @"^K( \d+)?$"):
                        prvenstvo.ispisiK(unos); break;
                    case bool _ when Regex.IsMatch(unos, @"^R [a-zA-Z]+( \d+)?$"):
                        prvenstvo.ispisiR(unos); break;
                    case bool _ when Regex.IsMatch(unos, @"^IZLAZ$"):
                        Environment.Exit(0); break;
                    default: Console.WriteLine("Ta opcija ne postoji"); break;

                }
            }

        }
    }
}

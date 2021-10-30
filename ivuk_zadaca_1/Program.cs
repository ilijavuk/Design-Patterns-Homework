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
            prvenstvo.listaKlubova = ucitavanjeKlubova.DohvatiKlubove(kluboviDat);

            string igraciDat = args[args.ToList().FindIndex(arg => arg == "-i") + 1];
            prvenstvo.listaIgraca = ucitavanjeIgraca
                .DohvatiIgrace(igraciDat, prvenstvo.listaKlubova);

            string sastaviDat = args[args.ToList().FindIndex(arg => arg == "-s") + 1];
            prvenstvo.listaSastava = ucitavanjeSastava
                .DohvatiSastave(sastaviDat, prvenstvo.listaKlubova, prvenstvo.listaIgraca);

            string utakmiceDat = args[args.ToList().FindIndex(arg => arg == "-u") + 1];
            prvenstvo.listaUtakmica = ucitavanjeUtakmica
                .DohvatiUtakmice(utakmiceDat, prvenstvo.listaKlubova);

            string dogadajiDat = args[args.ToList().FindIndex(arg => arg == "-d") + 1];
            prvenstvo.listaDogadaja = ucitavanjeDogadaja
                .DohvatiDogadaje(dogadajiDat, prvenstvo.listaKlubova, prvenstvo.listaIgraca);

            while (true)
            {
                Console.WriteLine("Odaberite opciju (IZLAZ za izlaz): ");
                unos = Console.ReadLine();
                switch(true)
                {
                    case bool _ when Regex.IsMatch(unos, @"^T( \d*)?$"):
                        prvenstvo.ispisiT(unos); break;
                    case bool _ when Regex.IsMatch(unos, @"^S( \d*)?$"):
                        prvenstvo.ispisiS(unos); break;
                    case bool _ when Regex.IsMatch(unos, @"^K( \d*)?$"):
                        prvenstvo.ispisiK(unos); break;
                    case bool _ when Regex.IsMatch(unos, @"^R [a-zA-Z]+( \d*)?"):
                        prvenstvo.ispisiR(unos); break;
                    case bool _ when Regex.IsMatch(unos, @"^IZLAZ$"):
                        Environment.Exit(0); break;
                    default: Console.WriteLine("Ta opcija ne postoji"); break;

                }
            }

        }
    }
}

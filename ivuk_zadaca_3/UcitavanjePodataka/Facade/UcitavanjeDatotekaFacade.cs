using System;
using System.Collections.Generic;
using System.Linq;
using ivuk_zadaca_3.PomocneKlase;
using System.Text.RegularExpressions;
using ivuk_zadaca_3.Modeli;

namespace ivuk_zadaca_3.UcitavanjePodataka
{
    public class UcitavanjeDatotekaFacade
    {
        public void UcitajDatoteke(string[] args)
        {
            Console.WriteLine("");
            string argumenti = string.Join(" ", args) + " ";
            Regex regex = new Regex(@"^(-(d|s|i|u|k) [\w\-.]+\.csv )+$");
            bool argumentiURedu = regex.IsMatch(argumenti);

            Dictionary<string, string> dnevnikArgs =
                new Dictionary<string, string>();

            if (!argumentiURedu)
            {
                Console.WriteLine("Argumenti nisu u redu. Izlazim iz programa.");
                Environment.Exit(0);
            }

            for (int i = 0; i < args.Length; i += 2)
            {
                dnevnikArgs.Add(args[i], args[i + 1]);
            }

            var kljucevi = dnevnikArgs.Keys.ToList();
            kljucevi.Sort(new Usporedivac().UsporediSa);

            UcitavanjeDatotekaFactory udFactory = new UcitavanjeDatotekaFactory();

            Console.WriteLine("Započinjem učitavanje datoteka");

            foreach (string kljuc in kljucevi)
            {
                UcitavanjeDatoteka ud = udFactory.DohvatiUcitavac(kljuc);
                ud.SpremiPodatkeUPrvenstvo(dnevnikArgs[kljuc]);
            }
        }

        public void NaknadnoUcitavanje(string unos)
        {
            UcitavanjeDatotekaFactory udFactory = new UcitavanjeDatotekaFactory();
            string[] vr = unos.Split(' ');
            UcitavanjeDatoteka ud = udFactory.DohvatiUcitavac(vr[0]);
            try
            {
                ud.SpremiPodatkeUPrvenstvo(vr[1]);

            } catch (Exception e)
            {
                Console.WriteLine("Došlo je do pogreške kod učitavanja");
            }
        }
    }
}

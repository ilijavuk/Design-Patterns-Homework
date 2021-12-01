using System;
using System.Collections.Generic;
using System.Linq;
using ivuk_zadaca_2.PomocneKlase;
using System.Text.RegularExpressions;

namespace ivuk_zadaca_2.UcitavanjePodataka
{
    public class UcitavanjeDatotekaFacade
    {
        public void ucitajDatoteke(String[] args)
        {
            Prvenstvo prvenstvo = Prvenstvo.Instance;
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
                ud.SpremiPodatkeUPrvenstvo(dnevnikArgs[kljuc], prvenstvo);
            }
        }
    }
}

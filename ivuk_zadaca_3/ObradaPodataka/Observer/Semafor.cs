using ivuk_zadaca_3.Modeli;
using ivuk_zadaca_3.PomocneKlase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_3.ObradaPodataka
{
    class Semafor: ISemaforObserver
    {
        public Semafor(ISemaforSubject ispisD)
        {
            ispisD.Register(this);
        }

        public void Update(string domaciGol, string gostiGol, string domaciOstalo, string gostiOstalo, Dogadaj d, Utakmica u)
        {
            string[] domaciGolSplit = domaciGol.Split(',');
            string[] gostiGolSplit = gostiGol.Split(',');
            EmptyLineConsole.IspisiCrtice(55);
            Console.WriteLine(string.Format("| {0, -51} |", ""));
            Console.WriteLine(string.Format("| {0, 27} {1, -23} |", "Vrijeme:", d.Min));
            Console.WriteLine(string.Format("| {0, -51} |", ""));
            Console.WriteLine(string.Format("| {0, 24} | {1, -24} |",
                $"{u.Domacin.naziv} {domaciGolSplit.Length - 1}", $"{gostiGolSplit.Length - 1} {u.Gost.naziv}"));
            Console.WriteLine(string.Format("| {0, 24} | {1, -24} |", "", ""));
            for (int i = 0; i < Math.Max(domaciGolSplit.Length, gostiGolSplit.Length); i++)
            {
                string a = domaciGolSplit.Length > i ? domaciGolSplit[i] : "";
                string b = gostiGolSplit.Length > i ? gostiGolSplit[i] : "";
                Console.WriteLine(string.Format("| {0, 24} | {1, -24} |", a, b));
            }
            Console.WriteLine(string.Format("| {0, 24} | {1, -24} |", "", ""));

            string[] domaciOstaloSplit = domaciOstalo.Split(',');
            string[] gostiOstaloSplit = gostiOstalo.Split(',');
            for (int i = 0; i < Math.Max(domaciOstaloSplit.Length, gostiOstaloSplit.Length); i++)
            {
                string a = domaciOstaloSplit.Length > i ? domaciOstaloSplit[i] : "";
                string b = gostiOstaloSplit.Length > i ? gostiOstaloSplit[i] : "";
                Console.WriteLine(string.Format("| {0, 24} | {1, -24} |", a, b));
            }

            Console.WriteLine(string.Format("| {0, 24} | {1, -24} |", "", ""));
            EmptyLineConsole.IspisiCrtice(55);
            Console.WriteLine('\n');
        }
    }
}

using ivuk_zadaca_2.PomocneKlase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_2.Modeli
{
    public class Utakmica: PrvenstvoComposite
    {
        public int Broj { get; set; }
        public int Kolo { get; set; }
        public Klub Domacin { get; set; }
        public Klub Gost { get; set; }
        public string Pocetak { get; set; }

        public Utakmica(int broj, int kolo, Klub domacin, Klub gost, string pocetak)
        {
            Broj = broj;
            Kolo = kolo;
            Domacin = domacin;
            Gost = gost;
            Pocetak = pocetak;
            NazivRazine = NaziviRazina.Utakmica;
            Djeca = new List<PrvenstvoComposite>();
        }

        public override void DodajDijete(PrvenstvoComposite prvenstvoComposite)
        {
            Djeca.Add(prvenstvoComposite);
        }

        public override void UkloniDijete(PrvenstvoComposite prvenstvoComposite)
        {
            Djeca.Remove(prvenstvoComposite);
        }

        public override List<PrvenstvoComposite> DohvatiDjecu()
        {
            return Djeca;
        }

        public override void IspisiInfo()
        {
            Console.WriteLine($"Utakmica: {Broj} {Kolo} {Domacin.oznaka} - {Gost.oznaka}");
            foreach(PrvenstvoComposite dijete in Djeca) {
                dijete.IspisiInfo();
            }
        }
    }
}

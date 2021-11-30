using ivuk_zadaca_2.PomocneKlase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_2.Modeli
{
    public class Klub: PrvenstvoComposite
    {
        public string oznaka;
        public string naziv;

        public Klub(string oznaka, string naziv)
        {
            this.oznaka = oznaka;
            this.naziv = naziv;
            NazivRazine = NaziviRazina.Klub;
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
            Console.WriteLine($"Klub: {oznaka} {naziv}");
            foreach(PrvenstvoComposite dijete in Djeca) {
                dijete.IspisiInfo();
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Klub klub &&
                   oznaka == klub.oznaka;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

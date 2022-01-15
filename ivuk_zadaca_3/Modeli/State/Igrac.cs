using ivuk_zadaca_3.Modeli.State;
using ivuk_zadaca_3.PomocneKlase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_3.Modeli
{
    public class Igrac: Osoba
    {
        public IIgracState igracState;

        public Klub Klub { get; set; }
        public string Pozicije { get; set; }
        public string Roden { get; set; }
        public int BrojZutih { get; set; } = 0;

        public Igrac(Klub klub, string imeIPrezime, string pozicije, string roden)
        {
            Klub = klub;
            ImeIPrezime = imeIPrezime;
            Pozicije = pozicije;
            Roden = roden;
            NazivRazine = NaziviRazina.Igrac;
            igracState = new NijeUIgri(this);
        }

        public override void IspisiInfo()
        {
            Console.WriteLine($"{NazivRazine}: {ImeIPrezime} {igracState}");
        }

        public override string ToString()
        {
            return $"{Klub.oznaka} | {ImeIPrezime} | {Pozicije} | {Roden}" ;
        }

        public void PromjeniStanje(IIgracState state)
        {
            igracState = state;
        }

        public bool ZabijGol()
        {
            return igracState.ZabijGol();
        }

        public bool Zamjena()
        {
            return igracState.Zamjena();
        }

        public bool ZutiKarton()
        {
            return igracState.ZutiKarton();
        }
        public bool CrveniKarton()
        {
            return igracState.CrveniKarton();
        }
    }
}

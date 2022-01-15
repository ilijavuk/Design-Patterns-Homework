using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_3.Modeli.State
{
    class Igra : IIgracState
    {
        public Igrac igrac { get; set; }

        public Igra(Igrac igrac)
        {
            this.igrac = igrac;
        }

        public bool ZabijGol()
        {
            return true;
        }

        public bool Zamjena()
        {
            igrac.PromjeniStanje(new Zamijenjen(igrac));
            return true;
        }

        public bool ZutiKarton()
        {
            igrac.BrojZutih++;
            if (igrac.BrojZutih == 2)
                igrac.PromjeniStanje(new Iskljucen(igrac));
            return true;
        }
        public bool CrveniKarton()
        {
            return true;
        }

        public override string ToString()
        {
            return "Igra";
        }
    }
}

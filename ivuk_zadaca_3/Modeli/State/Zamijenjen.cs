using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_3.Modeli.State
{
    class Zamijenjen : IIgracState
    {
        public Igrac igrac { get; set; }

        public Zamijenjen(Igrac igrac)
        {
            this.igrac = igrac;
        }

        public bool ZabijGol()
        {
            Console.WriteLine($"Igrač {igrac.ImeIPrezime} ne može zabiti gol jer je u pričuvi");
            return false;
        }

        public bool Zamjena()
        {
            igrac.PromjeniStanje(new Igra(igrac));
            return true;
        }

        public bool ZutiKarton()
        {
            Console.WriteLine($"Igrač {igrac.ImeIPrezime} ne može dobiti žuti karton jer je u pričuvi");
            return false;
        }
        public bool CrveniKarton()
        {
            Console.WriteLine($"Igrač {igrac.ImeIPrezime} ne može dobiti crveni karton jer je u pričuvi");
            return false;
        }

        public override string ToString()
        {
            return "Zamijenjen";
        }
    }
}

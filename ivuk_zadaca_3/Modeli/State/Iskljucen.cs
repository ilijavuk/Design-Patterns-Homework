using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_3.Modeli.State
{
    class Iskljucen : IIgracState
    {
        public Igrac igrac { get; set; }

        public Iskljucen(Igrac igrac)
        {
            this.igrac = igrac;
        }

        public bool ZabijGol()
        {
            Console.WriteLine($"Igrač {igrac.ImeIPrezime} ne može zabiti gol jer je isključen");
            return false;
        }

        public bool Zamjena()
        {
            Console.WriteLine($"Igrač {igrac.ImeIPrezime} ne može biti zamijenjen jer je isključen");
            return false;
        }

        public bool ZutiKarton()
        {
            Console.WriteLine($"Igrač {igrac.ImeIPrezime} ne može dobiti žuti kartin jer je isključen");
            return false;
        }
        public bool CrveniKarton()
        {
            Console.WriteLine($"Igrač {igrac.ImeIPrezime} ne može dobiti crveni karton jer je isključen");
            return false;
        }

        public override string ToString()
        {
            return "Isključen";
        }
    }
}

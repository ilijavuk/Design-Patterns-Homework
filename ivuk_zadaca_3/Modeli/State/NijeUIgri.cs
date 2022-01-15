using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_3.Modeli.State
{
    public class NijeUIgri : IIgracState
    {
        public Igrac igrac { get; set; }

        public NijeUIgri (Igrac igrac)
        {
            this.igrac = igrac;
        }

        public bool ZabijGol()
        {
            Console.WriteLine($"Igrač {igrac.ImeIPrezime} ne može zabiti gol jer nije u igri");
            return false;
        }

        public bool Zamjena()
        {
            Console.WriteLine($"Igrač {igrac.ImeIPrezime} ne može biti zamijenjen jer nije u igri");
            return false;
        }

        public bool ZutiKarton()
        {
            Console.WriteLine($"Igrač {igrac.ImeIPrezime} ne može dobiti žuti kartin jer nije u igri");
            return false;
        }
        public bool CrveniKarton()
        {
            Console.WriteLine($"Igrač {igrac.ImeIPrezime} ne može dobiti crveni karton jer nije u igri");
            return false;
        }

        public override string ToString()
        {
            return "Nije u igri";
        }
    }
}

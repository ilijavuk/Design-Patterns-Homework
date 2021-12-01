using ivuk_zadaca_2.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_2.ObradaPodataka
{
    public class RedLjestvice
    {
        public int BrojOdigranihKola { get; set; }
        public int BrojPobjeda { get; set; }
        public int BrojNerijesenih { get; set; }
        public int BrojPoraza { get; set; }
        public int BrojDanihGolova { get; set; }
        public int BrojPrimljenihGolova { get; set; }
        public int BrojBodova { get; set; }

        public override string ToString()
        {
            return string.Format("{0, 10} {1, 9} {2, 10} {3, 11} {4, 12} {5, 11} {6, 8} {7, 10}",
                    BrojOdigranihKola, BrojPobjeda, BrojNerijesenih, BrojPoraza, BrojDanihGolova,
                    BrojPrimljenihGolova, BrojDanihGolova - BrojPrimljenihGolova, BrojBodova);       
        }
    }
}

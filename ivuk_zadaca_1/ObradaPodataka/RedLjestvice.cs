using ivuk_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_1.ObradaPodataka
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
            return string.Format("{0,-12} {1,-12} {2,-12} {3,-12} {4,-12} {5,-12} {6,-12} {7,-12}",
                    BrojOdigranihKola, BrojPobjeda, BrojNerijesenih, BrojPoraza, BrojDanihGolova,
                    BrojPrimljenihGolova, BrojDanihGolova - BrojPrimljenihGolova, BrojBodova);       
        }
    }
}

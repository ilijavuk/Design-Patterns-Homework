using ivuk_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_1.ObradaPodataka
{
    public class RedKartona
    {
        public int BrojZutih { get; set; } = 0;
        public int BrojDrugihZutih { get; set; } = 0;
        public int BrojCrvenih { get; set; } = 0;

        public override string ToString()
        {
            return string.Format("{0,-12} {1,-12} {2,-12} {3,-12}",
                    BrojZutih, BrojDrugihZutih, BrojCrvenih,
                    BrojZutih+BrojDrugihZutih+BrojCrvenih);       
        }
    }
}

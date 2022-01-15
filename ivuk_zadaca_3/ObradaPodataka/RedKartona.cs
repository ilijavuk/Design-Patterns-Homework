using ivuk_zadaca_3.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_3.ObradaPodataka
{
    public class RedKartona
    {
        public int BrojZutih { get; set; } = 0;
        public int BrojDrugihZutih { get; set; } = 0;
        public int BrojCrvenih { get; set; } = 0;

        public override string ToString()
        {
            return string.Format("{0, 4} {1, 18} {2, 8} {3, 12}",
                    BrojZutih, BrojDrugihZutih, BrojCrvenih,
                    BrojZutih+BrojDrugihZutih+BrojCrvenih);       
        }
    }
}

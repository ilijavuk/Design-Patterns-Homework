﻿using ivuk_zadaca_2.PomocneKlase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_2.Modeli
{
    public class Trener: Osoba
    {
        public Trener(string imeIPrezime)
        {
            ImeIPrezime = imeIPrezime;
            NazivRazine = NaziviRazina.Trener;
        }
    }
}

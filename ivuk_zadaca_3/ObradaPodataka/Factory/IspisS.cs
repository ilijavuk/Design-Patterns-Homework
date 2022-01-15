using ivuk_zadaca_3.Modeli;
using ivuk_zadaca_3.PomocneKlase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ivuk_zadaca_3.ObradaPodataka
{
    public class IspisS : IIspisTabliceVisitable
    {
        public string[] Mogucnost { get; set; }

        public IspisS(string[] mogucnost)
        {
            Mogucnost = mogucnost;
        }

        public void Accept(IIspisTabliceVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

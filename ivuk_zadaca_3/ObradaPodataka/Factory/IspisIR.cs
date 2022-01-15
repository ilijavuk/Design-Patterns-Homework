using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_3.ObradaPodataka.Factory
{
    public class IspisIR : IIspisTabliceVisitable
    {
        public string[] Mogucnost { get; set; }

        public IspisIR(string[] mogucnost)
        {
            Mogucnost = mogucnost;
        }

        public void Accept(IIspisTabliceVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

using ivuk_zadaca_3.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_3.ObradaPodataka
{
    public interface ISemaforObserver
    {
        void Update(string domaciGol, string gostiGol, string domaciOstalo, string gostiOstalo, Dogadaj d, Utakmica u);
    }
}

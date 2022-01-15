using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_3.Modeli.State
{
    public interface IIgracState
    {
        Igrac igrac { get; set; }

        bool ZabijGol();
        bool Zamjena();
        bool ZutiKarton();
        bool CrveniKarton();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_3.ObradaPodataka
{
    public interface ISemaforSubject
    {
        void Register(ISemaforObserver o);
        void Unregister(ISemaforObserver o);
        void NotifyObservers();
    }
}

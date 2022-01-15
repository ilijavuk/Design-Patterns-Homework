using ivuk_zadaca_3.Modeli;
using ivuk_zadaca_3.PomocneKlase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_3.ObradaPodataka
{
    public class IspisD : IIspisTabliceVisitable, ISemaforSubject
    {
        public string[] Mogucnost { get; set; }

        public IspisD(string[] mogucnost)
        {
            Mogucnost = mogucnost;
        }

        readonly List<ISemaforObserver> observers = new List<ISemaforObserver>();
        public string domaciGol { get; set; } = "";
        public string gostiGol { get; set; } = "";
        public string domaciOstalo { get; set; } = "";
        public string gostiOstalo { get; set; } = "";
        public Utakmica u { get; set; }
        public Dogadaj dog { get; set; }

        public void NotifyObservers()
        {
            foreach(ISemaforObserver o in observers)
            {
                o.Update(domaciGol, gostiGol, domaciOstalo, gostiOstalo, dog, u);
            }
        }

        public void Register(ISemaforObserver o)
        {
            observers.Add(o);
        }

        public void Unregister(ISemaforObserver o)
        {
            observers.Remove(o);
        }

        public void Accept(IIspisTabliceVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

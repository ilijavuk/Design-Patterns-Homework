using ivuk_zadaca_2.Modeli;
using ivuk_zadaca_2.PomocneKlase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_2.ObradaPodataka
{
    public class IspisD : IspisTablice, ISemaforSubject
    {
        readonly List<ISemaforObserver> observers = new List<ISemaforObserver>();
        string domaciGol = "";
        string gostiGol = "";
        string domaciOstalo = "";
        string gostiOstalo = "";
        Utakmica u;
        Dogadaj dog;

        public override void IspisiTablicu(string[] mogucnost) 
        {
            Prvenstvo p = Prvenstvo.Instance;
            int kolo = int.Parse(mogucnost[1]);
            Klub klub1 = p.listaKlubova.ConvertAll(x => (Klub)x).Find(el => el.oznaka == mogucnost[2]);
            Klub klub2 = p.listaKlubova.ConvertAll(x => (Klub)x).Find(el => el.oznaka == mogucnost[3]);
            if (klub1 == null || klub2 == null)
            {
                Console.WriteLine("Klub ne postoji!");
                return;
            }
            u = klub1.DohvatiDjecu().FindAll(el => el.NazivRazine == NaziviRazina.Utakmica)
                .ConvertAll(x => (Utakmica)x).Find(el => el.Kolo == kolo &&
                ((el.Domacin == klub1 && el.Gost == klub2) || (el.Domacin == klub2 && el.Gost == klub1)));

            if (u == null)
            {
                Console.WriteLine("Ta utakmica ne postoji!");
                return;
            }

            Console.WriteLine($"Utakmica između {u.Domacin.naziv} - {u.Gost.naziv}");
            foreach (Dogadaj d in u.DohvatiDjecu().FindAll(el => el.NazivRazine == NaziviRazina.Dogadaj))
            {
                dog = d;
                domaciOstalo = "";
                gostiOstalo = "";
                if (d.Vrsta == 1 || d.Vrsta == 2)
                {
                    if (d.Klub == u.Domacin)
                    {
                        domaciGol += $"{d.Igrac.ImeIPrezime} ({d.Min}),";
                        domaciOstalo = "GOOOOL!!";
                    }
                    if (d.Klub == u.Gost)
                    {
                        gostiGol += $"{d.Igrac.ImeIPrezime} ({d.Min}) ";
                        gostiOstalo = "GOOOOL!!";
                    }
                }
                else if (d.Vrsta == 3)
                {
                    if (d.Klub == u.Domacin)
                    {
                        gostiGol += $"{d.Igrac.ImeIPrezime} ({d.Min}) ";
                        gostiOstalo = "GOOOOL!!";
                    }
                    if (d.Klub == u.Gost)
                    {
                        domaciGol += $"{d.Igrac.ImeIPrezime} ({d.Min}) ";
                        domaciOstalo = "GOOOOL!!";
                    }
                }
                else if (d.Vrsta == 10 || d.Vrsta == 11)
                {
                    if (d.Klub == u.Domacin) domaciOstalo = $"{(d.Vrsta == 10 ? "Žuti" : "Crveni")} Karton";
                    if (d.Klub == u.Gost) gostiOstalo = $"{(d.Vrsta == 10 ? "Žuti" : "Crveni")} Karton";
                }
                else if (d.Vrsta == 20)
                {
                    if (d.Klub == u.Domacin) domaciOstalo = $"{d.Igrac.ImeIPrezime},↕,{d.Zamjena.ImeIPrezime}";
                    if (d.Klub == u.Gost) gostiOstalo = $"{d.Igrac.ImeIPrezime},↕,{d.Zamjena.ImeIPrezime}";
                }

                NotifyObservers();
                System.Threading.Thread.Sleep(int.Parse(mogucnost[4]) * 1000);
            }
        }

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
    }
}

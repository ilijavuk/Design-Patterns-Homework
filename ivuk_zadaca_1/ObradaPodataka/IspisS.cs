using ivuk_zadaca_2.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ivuk_zadaca_2.ObradaPodataka
{
    public class IspisS : IspisTablice
    {
        public override void IspisiTablicu(string[] mogucnost, Prvenstvo p)
        {
            /*
            int brojKola = -1;
            if (mogucnost.Length == 2)
            {
                brojKola = int.Parse(mogucnost[1]);
            }

            IDictionary<Igrac, int> strijelci =
            new Dictionary<Igrac, int>();

            foreach (Utakmica utakmica in p.listaUtakmica)
            {
                if (brojKola != -1 && utakmica.Kolo > brojKola) continue;

                List<Dogadaj> dogadajiUtakmice = p.listaDogadaja.FindAll(d => d.Broj == utakmica.Broj);
                foreach (Dogadaj d in dogadajiUtakmice)
                {

                    if (d.Vrsta == 1 || d.Vrsta == 2 || d.Vrsta == 3)
                    {
                        if (!strijelci.TryGetValue(d.Igrac, out _))
                        {
                            strijelci.Add(d.Igrac, 0);
                        }
                        strijelci[d.Igrac]++;
                    }
                }
            }

            strijelci = strijelci.OrderByDescending(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
            Console.WriteLine(string.Format("{0,-25} {1,-20} {2,-5}",
                    "Ime i prezime", "Naziv kluba", "Broj golova"));

            foreach (var elem in strijelci)
            {
                Console.WriteLine(string.Format("{0,-25} {1,-20} {2,-5}",
                    elem.Key.ImeIPrezime, elem.Key.Klub.naziv, elem.Value));
            }
            */
        }
    }
}

using ivuk_zadaca_2.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_2.ObradaPodataka
{
    public class IspisR : IspisTablice
    {
        public override void IspisiTablicu(string[] mogucnost, Prvenstvo p)
        {
            int brojKola = -1;
            if (mogucnost.Length == 3)
            {
                brojKola = int.Parse(mogucnost[2]);
            }


            Klub klub = p.listaKlubova.Find(k => k.oznaka == mogucnost[1]);
            if (klub == null)
            {
                Console.WriteLine("Taj klub ne postoji");
                return;
            }

            IDictionary<Utakmica, RedUtakmice> dnevnikUtakmica =
            new Dictionary<Utakmica, RedUtakmice>();

            List<Utakmica> utakmiceZaKlub = p.listaUtakmica
                .FindAll(u => u.Domacin == klub || u.Gost == klub);
            foreach (Utakmica u in utakmiceZaKlub)
            {
                if (brojKola != -1 && u.Kolo > brojKola) continue;
                int golDom = 0, golGost = 0;

                List<Dogadaj> dogadajiUtakmice = p.listaDogadaja.FindAll(d => d.Broj == u.Broj);
                foreach (Dogadaj d in dogadajiUtakmice)
                {
                    if (d.Vrsta == 1 || d.Vrsta == 2)
                    {
                        if (d.Klub == u.Domacin)
                        {
                            golDom++;
                        }
                        else
                        {
                            golGost++;
                        }
                    }
                    else if (d.Vrsta == 3)
                    {
                        if (d.Klub == u.Domacin)
                        {
                            golGost++;
                        }
                        else
                        {
                            golDom++;
                        }
                    }
                }
                string rez = golDom.ToString() + ":" + golGost.ToString();
                RedUtakmice ru = new RedUtakmice(u.Kolo, u.Pocetak, u.Domacin, u.Gost, rez);
                dnevnikUtakmica.Add(u, ru);
            }

            Console.WriteLine("Rezultati za Dinamo");
            Console.WriteLine(string.Format("{0, 5} {1,-12} {2,-20} {3,-20} {4,-20}",
                "Kolo", "Vrijeme", "Domaćin", "Gost", "Rezultat"));

            foreach (var elem in dnevnikUtakmica)
            {
                Console.WriteLine(elem.Value);
            }
        }
    }
}

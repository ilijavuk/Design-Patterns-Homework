using ivuk_zadaca_2.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ivuk_zadaca_2.ObradaPodataka
{
    public class IspisT : IspisTablice
    {
        public override void IspisiTablicu(string[] mogucnost, Prvenstvo p)
        {
            int brojKola = -1;
            if (mogucnost.Length == 2)
            {
                brojKola = int.Parse(mogucnost[1]);
            }

            IDictionary<Klub, RedLjestvice> dnevnikTablice =
            new Dictionary<Klub, RedLjestvice>();
            IDictionary<Klub, List<int>> odigranaKola =
            new Dictionary<Klub, List<int>>();

            foreach (Klub klub in p.listaKlubova)
            {
                dnevnikTablice.Add(klub, new RedLjestvice());
                odigranaKola.Add(klub, new List<int>());
            }

            foreach (Utakmica utakmica in p.listaUtakmica)
            {
                if (brojKola != -1 && utakmica.Kolo > brojKola) continue;

                odigranaKola[utakmica.Domacin].Add(utakmica.Kolo);
                odigranaKola[utakmica.Gost].Add(utakmica.Kolo);
                int golDom = 0, golGost = 0;

                List<Dogadaj> dogadajiUtakmice = p.listaDogadaja.FindAll(d => d.Broj == utakmica.Broj);
                foreach (Dogadaj d in dogadajiUtakmice)
                {
                    if (d.Vrsta == 1 || d.Vrsta == 2)
                    {
                        if (d.Klub == utakmica.Domacin)
                        {
                            golDom++;
                            dnevnikTablice[utakmica.Domacin].BrojDanihGolova++;
                            dnevnikTablice[utakmica.Gost].BrojPrimljenihGolova++;
                        }
                        else
                        {
                            golGost++;
                            dnevnikTablice[utakmica.Gost].BrojDanihGolova++;
                            dnevnikTablice[utakmica.Domacin].BrojPrimljenihGolova++;
                        }
                    }
                    else if (d.Vrsta == 3)
                    {
                        if (d.Klub == utakmica.Domacin)
                        {
                            golGost++;
                            dnevnikTablice[utakmica.Gost].BrojDanihGolova++;
                            dnevnikTablice[utakmica.Domacin].BrojPrimljenihGolova++;
                        }
                        else
                        {
                            golDom++;
                            dnevnikTablice[utakmica.Domacin].BrojDanihGolova++;
                            dnevnikTablice[utakmica.Gost].BrojPrimljenihGolova++;
                        }
                    }
                }
                if (golDom > golGost)
                {
                    dnevnikTablice[utakmica.Domacin].BrojPobjeda++;
                    dnevnikTablice[utakmica.Gost].BrojPoraza++;
                    dnevnikTablice[utakmica.Domacin].BrojBodova += 3;
                }
                else if (golDom < golGost)
                {
                    dnevnikTablice[utakmica.Domacin].BrojPoraza++;
                    dnevnikTablice[utakmica.Gost].BrojPobjeda++;
                    dnevnikTablice[utakmica.Gost].BrojBodova += 3;
                }
                else
                {
                    dnevnikTablice[utakmica.Domacin].BrojNerijesenih++;
                    dnevnikTablice[utakmica.Gost].BrojNerijesenih++;
                    dnevnikTablice[utakmica.Domacin].BrojBodova++;
                    dnevnikTablice[utakmica.Gost].BrojBodova++;
                }
            }

            dnevnikTablice = dnevnikTablice.OrderByDescending(x => x.Value.BrojBodova)
                .ToDictionary(x => x.Key, x => x.Value);

            Console.WriteLine(string.Format("{0, -4} {1, -20} {2, -10}" +
                "{3,-12} {4,-12} {5,-12} {6,-12} {7,-12} {8,-12} {9,-12} {10,-12}",
                "Oznaka", "Naziv", "Trener", "brojOdigKol", "brojPobj", "brojNerij",
                    "brojPoraza", "brojDanihGol", "brojPrimGol", "golRazli",
                    "brojBodova"));

            foreach (var elem in dnevnikTablice)
            {
                elem.Value.BrojOdigranihKola = odigranaKola[elem.Key].Distinct().ToList().Count();
                Console.WriteLine(string.Format("{0, -4} {1, -20} {2, -20} {3, -40}", elem.Key.oznaka,
                   elem.Key.naziv, elem.Key.trener, elem.Value));
            }
        }
    }
}

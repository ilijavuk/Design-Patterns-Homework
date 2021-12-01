using ivuk_zadaca_2.Modeli;
using ivuk_zadaca_2.PomocneKlase;
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
            List<Utakmica> utakmice = new List<Utakmica>();

            foreach (Klub klub in p.listaKlubova)
            {
                dnevnikTablice.Add(klub, new RedLjestvice());
                odigranaKola.Add(klub, new List<int>());
                foreach (Utakmica u in klub.DohvatiDjecu().FindAll(el => el.NazivRazine == NaziviRazina.Utakmica))
                {
                    if (utakmice.Find(elem => elem.Broj == u.Broj) == null)
                        utakmice.Add(u);
                }
            }

            foreach (Utakmica utakmica in utakmice)
            {
                if (brojKola != -1 && utakmica.Kolo > brojKola) continue;

                odigranaKola[utakmica.Domacin].Add(utakmica.Kolo);
                odigranaKola[utakmica.Gost].Add(utakmica.Kolo);
                int golDom = 0, golGost = 0;

                List<Dogadaj> dogadajiUtakmice = utakmica.DohvatiDjecu()
                    .FindAll(el => el.NazivRazine == NaziviRazina.Dogadaj).ConvertAll(x => (Dogadaj)x);
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
                .ThenByDescending(x => x.Value.BrojDanihGolova - x.Value.BrojPrimljenihGolova)
                .ThenByDescending(x => x.Value.BrojDanihGolova)
                .ThenByDescending(x => x.Value.BrojPobjeda)
                .ToDictionary(x => x.Key, x => x.Value);

            Console.WriteLine(string.Format("{0, -6} {1, -25} {2, -20}" +
                "{3,-12} {4,-9} {5,-10} {6,-10} {7,-12} {8,-11} {9,-8} {10,-10}",
                "Oznaka", "Naziv", "Trener", "brojOdigKol", "brojPobj", "brojNerij",
                    "brojPoraza", "brojDanihGol", "brojPrimGol", "golRazli",
                    "brojBodova"));
            
            EmptyLineConsole.IspisiCrtice(142);

            RedLjestvice sum = new RedLjestvice();

            foreach (var elem in dnevnikTablice)
            {
                sum.BrojPobjeda += elem.Value.BrojPobjeda;
                sum.BrojNerijesenih += elem.Value.BrojNerijesenih;
                sum.BrojPoraza += elem.Value.BrojPoraza;
                sum.BrojDanihGolova += elem.Value.BrojDanihGolova;
                sum.BrojPrimljenihGolova += elem.Value.BrojPrimljenihGolova;
                sum.BrojBodova += elem.Value.BrojBodova;

                elem.Value.BrojOdigranihKola = odigranaKola[elem.Key].Distinct().ToList().Count();
                Console.WriteLine(string.Format("{0, -6} {1, -25} {2, -20} {3, -40}", elem.Key.oznaka,
                   elem.Key.naziv, elem.Key.DohvatiDjecu().Find(el=>el.NazivRazine == NaziviRazina.Trener).ToString(), elem.Value));
            }
            EmptyLineConsole.IspisiCrtice(142);
            Console.WriteLine(string.Format("{0,-53} {1, -40}",
                "Sumirano:", sum));
        }
    }
}

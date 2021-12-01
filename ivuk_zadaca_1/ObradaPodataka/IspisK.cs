using ivuk_zadaca_2.Modeli;
using ivuk_zadaca_2.PomocneKlase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ivuk_zadaca_2.ObradaPodataka
{
    public class IspisK : IspisTablice
    {
        public override void IspisiTablicu(string[] mogucnost, Prvenstvo p)
        {
            int brojKola = -1;
            if (mogucnost.Length == 2)
            {
                brojKola = int.Parse(mogucnost[1]);
            }

            IDictionary<Klub, RedKartona> dnevnikKartona =
                new Dictionary<Klub, RedKartona>();
            List<Utakmica> utakmice = new List<Utakmica>();

            foreach (Klub klub in p.listaKlubova)
            {
                dnevnikKartona.Add(klub, new RedKartona());
                foreach (Utakmica u in klub.DohvatiDjecu().FindAll(el => el.NazivRazine == NaziviRazina.Utakmica))
                {
                    if (utakmice.Find(elem => elem.Broj == u.Broj) == null)
                        utakmice.Add(u);
                }
            }

            foreach (Utakmica u in utakmice)
            {
                List<Igrac> igraciSKartonima = new List<Igrac>();
                if (brojKola != -1 && u.Kolo > brojKola) continue;

                List<Dogadaj> dogadajiUtakmice = u.DohvatiDjecu()
                    .FindAll(el => el.NazivRazine == NaziviRazina.Dogadaj).ConvertAll(x => (Dogadaj)x);
                foreach (Dogadaj d in dogadajiUtakmice)
                {
                    if (d.Vrsta == 10)
                    {
                        if (igraciSKartonima.Contains(d.Igrac))
                        {
                            dnevnikKartona[d.Klub].BrojDrugihZutih++;
                        }
                        else
                        {
                            dnevnikKartona[d.Klub].BrojZutih++;
                            igraciSKartonima.Add(d.Igrac);
                        }
                    }
                    else if (d.Vrsta == 11)
                    {
                        dnevnikKartona[d.Klub].BrojCrvenih++;
                    }
                }
            }

            dnevnikKartona = dnevnikKartona
                .OrderByDescending(x => (x.Value.BrojZutih + x.Value.BrojDrugihZutih + x.Value.BrojCrvenih))
                .ThenByDescending(x => x.Value.BrojCrvenih)
                .ThenByDescending(x => x.Value.BrojDrugihZutih)
                .ToDictionary(x => x.Key, x => x.Value);

            Console.WriteLine(string.Format("{0, -4} {1, -20} " +
                "{2,-12} {3,-12} {4,-12} {5,-12}",
                "Oznaka", "Naziv", "Žuti", "Drugi žuti", "Crveni", "Ukupno"));

            EmptyLineConsole.IspisiCrtice(73);
            RedKartona sum = new RedKartona();

            foreach (var elem in dnevnikKartona)
            {
                sum.BrojZutih += elem.Value.BrojZutih;
                sum.BrojDrugihZutih += elem.Value.BrojDrugihZutih;
                sum.BrojCrvenih += elem.Value.BrojCrvenih;           

                Console.WriteLine(string.Format("{0, -6} {1, -20} {2, -48}", elem.Key.oznaka,
                    elem.Key.naziv, elem.Value));
            }
            EmptyLineConsole.IspisiCrtice(73);
            Console.WriteLine(string.Format("{0, -27} {1, 4} {2, 18} {3, 8} {4, 12}",
                    "Sumirano: ", sum.BrojZutih, sum.BrojDrugihZutih, sum.BrojCrvenih,
                    sum.BrojZutih + sum.BrojDrugihZutih + sum.BrojCrvenih));
        }
    }
}

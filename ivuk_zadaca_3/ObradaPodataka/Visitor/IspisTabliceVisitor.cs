using ivuk_zadaca_3.Modeli;
using ivuk_zadaca_3.ObradaPodataka.Factory;
using ivuk_zadaca_3.PomocneKlase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ivuk_zadaca_3.ObradaPodataka
{
    public class IspisTabliceVisitor : IIspisTabliceVisitor
    {
        public void Visit(IspisT ispisT)
        {
            string[] mogucnost = ispisT.Mogucnost;
            Prvenstvo p = Prvenstvo.Instance;
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
                        if (d.Igrac.ZabijGol())
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
                    }
                    else if (d.Vrsta == 3)
                    {
                        if (d.Igrac.ZabijGol())
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
                   elem.Key.naziv, elem.Key.DohvatiDjecu().Find(el => el.NazivRazine == NaziviRazina.Trener).ToString(), elem.Value));
            }
            EmptyLineConsole.IspisiCrtice(142);
            Console.WriteLine(string.Format("{0,-53} {1, -40}",
                "Sumirano:", sum));
        }

        public void Visit(IspisS ispisS)
        {
            string[] mogucnost = ispisS.Mogucnost;
            Prvenstvo p = Prvenstvo.Instance;
            int brojKola = -1;
            if (mogucnost.Length == 2)
            {
                brojKola = int.Parse(mogucnost[1]);
            }

            IDictionary<Igrac, int> strijelci = new Dictionary<Igrac, int>();
            List<Utakmica> utakmice = new List<Utakmica>();

            foreach (Klub klub in p.listaKlubova)
            {
                foreach (Utakmica u in klub.DohvatiDjecu().FindAll(el => el.NazivRazine == NaziviRazina.Utakmica))
                {
                    if (utakmice.Find(elem => elem.Broj == u.Broj) == null)
                        utakmice.Add(u);
                }
            }

            foreach (Utakmica utakmica in utakmice)
            {
                if (brojKola != -1 && utakmica.Kolo > brojKola) continue;

                List<Dogadaj> dogadajiUtakmice = utakmica.DohvatiDjecu()
                    .FindAll(el => el.NazivRazine == NaziviRazina.Dogadaj).ConvertAll(x => (Dogadaj)x);

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

            EmptyLineConsole.IspisiCrtice(58);
            int sum = 0;

            foreach (var elem in strijelci)
            {
                sum += elem.Value;
                Console.WriteLine(string.Format("{0,-25} {1,-20} {2, 11}",
                    elem.Key.ImeIPrezime, elem.Key.Klub.naziv, elem.Value));
            }

            EmptyLineConsole.IspisiCrtice(58);
            Console.WriteLine(string.Format("{0,-45} {1, 12}",
                "Sumirano: ", sum));
        }

        public void Visit(IspisK ispisK)
        {
            string[] mogucnost = ispisK.Mogucnost;
            Prvenstvo p = Prvenstvo.Instance;
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

        public void Visit(IspisR ispisR)
        {
            string[] mogucnost = ispisR.Mogucnost;
            Prvenstvo p = Prvenstvo.Instance;
            int brojKola = -1;
            if (mogucnost.Length == 3)
            {
                brojKola = int.Parse(mogucnost[2]);
            }


            Klub klub = p.listaKlubova.ConvertAll(x => (Klub)x).Find(k => k.oznaka == mogucnost[1]);
            if (klub == null)
            {
                Console.WriteLine("Taj klub ne postoji");
                return;
            }
            IDictionary<Utakmica, RedUtakmice> dnevnikUtakmica =
            new Dictionary<Utakmica, RedUtakmice>();

            List<Utakmica> utakmiceZaKlub = klub.DohvatiDjecu().FindAll(el => el.NazivRazine == NaziviRazina.Utakmica).ConvertAll(x => (Utakmica)x);

            foreach (Utakmica u in utakmiceZaKlub)
            {
                if (brojKola != -1 && u.Kolo > brojKola) continue;
                int golDom = 0, golGost = 0;

                List<Dogadaj> dogadajiUtakmice = u.DohvatiDjecu()
                    .FindAll(el => el.NazivRazine == NaziviRazina.Dogadaj).ConvertAll(x => (Dogadaj)x);
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

            Console.WriteLine($"Rezultati za {klub.naziv}");
            Console.WriteLine(string.Format("{0, -5} {1, -20} {2,-20} {3,-20} {4,-20}",
                "Kolo", "Vrijeme", "Domaćin", "Gost", "Rezultat"));
            EmptyLineConsole.IspisiCrtice(77);
            foreach (var elem in dnevnikUtakmica)
            {
                Console.WriteLine(elem.Value);
            }
            EmptyLineConsole.IspisiCrtice(77);
        }

        public void Visit(IspisD ispisD)
        {
            string[] mogucnost = ispisD.Mogucnost;
            Prvenstvo p = Prvenstvo.Instance;
            int kolo = int.Parse(mogucnost[1]);
            Klub klub1 = p.listaKlubova.ConvertAll(x => (Klub)x).Find(el => el.oznaka == mogucnost[2]);
            Klub klub2 = p.listaKlubova.ConvertAll(x => (Klub)x).Find(el => el.oznaka == mogucnost[3]);
            if (klub1 == null || klub2 == null)
            {
                Console.WriteLine("Klub ne postoji!");
                return;
            }
            ispisD.u = klub1.DohvatiDjecu().FindAll(el => el.NazivRazine == NaziviRazina.Utakmica)
                .ConvertAll(x => (Utakmica)x).Find(el => el.Kolo == kolo &&
                ((el.Domacin == klub1 && el.Gost == klub2) || (el.Domacin == klub2 && el.Gost == klub1)));

            if (ispisD.u == null)
            {
                Console.WriteLine("Ta utakmica ne postoji!");
                return;
            }

            Console.WriteLine($"Utakmica između {ispisD.u.Domacin.naziv} - {ispisD.u.Gost.naziv}");
            foreach (Dogadaj d in ispisD.u.DohvatiDjecu().FindAll(el => el.NazivRazine == NaziviRazina.Dogadaj))
            {
                ispisD.dog = d;
                ispisD.domaciOstalo = "";
                ispisD.gostiOstalo = "";
                if (d.Vrsta == 1 || d.Vrsta == 2)
                {
                    if (d.Klub == ispisD.u.Domacin)
                    {
                        ispisD.domaciGol += $"{d.Igrac.ImeIPrezime} ({d.Min}),";
                        ispisD.domaciOstalo = "GOOOOL!!";
                    }
                    if (d.Klub == ispisD.u.Gost)
                    {
                        ispisD.gostiGol += $"{d.Igrac.ImeIPrezime} ({d.Min}) ";
                        ispisD.gostiOstalo = "GOOOOL!!";
                    }
                }
                else if (d.Vrsta == 3)
                {
                    if (d.Klub == ispisD.u.Domacin)
                    {
                        ispisD.gostiGol += $"{d.Igrac.ImeIPrezime} ({d.Min}) ";
                        ispisD.gostiOstalo = "GOOOOL!!";
                    }
                    if (d.Klub == ispisD.u.Gost)
                    {
                        ispisD.domaciGol += $"{d.Igrac.ImeIPrezime} ({d.Min}) ";
                        ispisD.domaciOstalo = "GOOOOL!!";
                    }
                }
                else if (d.Vrsta == 10 || d.Vrsta == 11)
                {
                    if (d.Klub == ispisD.u.Domacin) ispisD.domaciOstalo = $"{(d.Vrsta == 10 ? "Žuti" : "Crveni")} Karton";
                    if (d.Klub == ispisD.u.Gost) ispisD.gostiOstalo = $"{(d.Vrsta == 10 ? "Žuti" : "Crveni")} Karton";
                }
                else if (d.Vrsta == 20)
                {
                    if (d.Klub == ispisD.u.Domacin) ispisD.domaciOstalo = $"{d.Igrac.ImeIPrezime},↕,{d.Zamjena.ImeIPrezime}";
                    if (d.Klub == ispisD.u.Gost) ispisD.gostiOstalo = $"{d.Igrac.ImeIPrezime},↕,{d.Zamjena.ImeIPrezime}";
                }

                ispisD.NotifyObservers();
                System.Threading.Thread.Sleep(int.Parse(mogucnost[4]) * 1000);
            }
        }

        public void Visit(IspisSU ispisSU)
        {
            string[] mogucnost = ispisSU.Mogucnost;
            Prvenstvo p = Prvenstvo.Instance;
            int kolo = int.Parse(mogucnost[1]);
            Klub klub1 = p.listaKlubova.ConvertAll(x => (Klub)x).Find(el => el.oznaka == mogucnost[2]);
            Klub klub2 = p.listaKlubova.ConvertAll(x => (Klub)x).Find(el => el.oznaka == mogucnost[3]);
            if (klub1 == null || klub2 == null)
            {
                Console.WriteLine("Klub ne postoji!");
                return;
            }
            Utakmica u = klub1.DohvatiDjecu().FindAll(el => el.NazivRazine == NaziviRazina.Utakmica)
                .ConvertAll(x => (Utakmica)x).Find(el => el.Kolo == kolo &&
                ((el.Domacin == klub1 && el.Gost == klub2) || (el.Domacin == klub2 && el.Gost == klub1)));

            if (u == null)
            {
                Console.WriteLine("Ta utakmica ne postoji!");
                return;
            }

            List<Sastav> sastavK1 = u.DohvatiDjecu()
                .FindAll(el => el.NazivRazine == NaziviRazina.Sastav)
                .ConvertAll(x => (Sastav)x).FindAll(el => el.Klub == klub1);
            List<Sastav> sastavK2 = u.DohvatiDjecu()
                .FindAll(el => el.NazivRazine == NaziviRazina.Sastav)
                .ConvertAll(x => (Sastav)x).FindAll(el => el.Klub == klub2);

            sastavK1.Sort(new Usporedivac().UsporediPoziciju);
            sastavK2.Sort(new Usporedivac().UsporediPoziciju);

            Console.WriteLine($"Sastavi kluba");
            Console.WriteLine(string.Format("{0, -40} | {1, -40}", klub1.naziv, klub2.naziv));
            EmptyLineConsole.IspisiCrtice(80);
            for (int i = 0; i < sastavK2.Count; i++)
            {
                Console.WriteLine(string.Format("{0, -40} | {1, -40}", sastavK1[i].VratiInfo(), sastavK2[i].VratiInfo()));
            }
            EmptyLineConsole.IspisiCrtice(80);

            Console.WriteLine("Utakmica je u tijeku...");

            foreach (Dogadaj d in u.DohvatiDjecu()
                .FindAll(el => el.NazivRazine == NaziviRazina.Dogadaj)
                .ConvertAll(x => (Dogadaj)x)
                .FindAll(el => el.Vrsta == 20))
            {
                Console.WriteLine($"{d.Zamjena.igracState}  {d.Igrac.igracState}");
                if(d.Igrac.Zamjena() && d.Zamjena.Zamjena())
                {
                    Sastav pi = u.DohvatiDjecu()
                        .FindAll(el => el.NazivRazine == NaziviRazina.Sastav)
                        .ConvertAll(x => (Sastav)x).Find(el => el.Igrac == d.Igrac);
                    if (pi == null) continue;

                    Console.WriteLine($"{d.Klub.naziv}: {d.Igrac.ImeIPrezime} ↔ {d.Zamjena.ImeIPrezime}");
                    u.UkloniDijete(pi);
                    u.DodajDijete(new Sastav(pi.Broj, pi.Klub, pi.Vrsta, d.Igrac, pi.Pozicija));
                }
            }

            sastavK1 = u.DohvatiDjecu()
                .FindAll(el => el.NazivRazine == NaziviRazina.Sastav)
                .ConvertAll(x => (Sastav)x).FindAll(el => el.Klub == klub1);
            sastavK2 = u.DohvatiDjecu()
                .FindAll(el => el.NazivRazine == NaziviRazina.Sastav)
                .ConvertAll(x => (Sastav)x).FindAll(el => el.Klub == klub2);
            sastavK1.Sort(new Usporedivac().UsporediPoziciju);
            sastavK2.Sort(new Usporedivac().UsporediPoziciju);


            Console.WriteLine($"\nSastavi kluba");
            Console.WriteLine(string.Format("{0, -40} | {1, -40}", klub1.naziv, klub2.naziv));
            EmptyLineConsole.IspisiCrtice(80);
            for (int i = 0; i < sastavK1.Count; i++)
            {
                Console.WriteLine(string.Format("{0, -40} | {1, -40}", sastavK1[i].VratiInfo(), sastavK2[i].VratiInfo()));
            }
            EmptyLineConsole.IspisiCrtice(80);
        }

        private int VratiBrojSamoglasnika(string str)
        {
            int broj = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == 'a' || str[i] == 'e' || str[i] == 'i' || str[i] == 'o' || str[i] == 'u')
                {
                    broj++;
                }
            }
            return broj;
        }

        public void Visit(IspisGR ispisGR)
        {
            string[] mogucnost = ispisGR.Mogucnost;
            List<Klub> prvaPol = new List<Klub>();
            List<Klub> drugaPol = new List<Klub>();
            List<Klub> sviKlubovi = Prvenstvo.Instance.listaKlubova.ConvertAll(x => (Klub)x);

            var rand = new Random();
            if (mogucnost[1] == "1")
            {
                drugaPol = sviKlubovi;
                for (int i = 0; i < Prvenstvo.Instance.listaKlubova.Count / 2; i++)
                {
                    int indeks = rand.Next(0, drugaPol.Count);
                    prvaPol.Add(drugaPol[indeks]);
                    drugaPol.RemoveRange(indeks, 1);
                }
            } else if (mogucnost[1] == "2")
            {
                sviKlubovi = sviKlubovi.OrderBy(el => el.naziv).ToList();
                prvaPol.AddRange(sviKlubovi.GetRange(0, sviKlubovi.Count / 2));
                drugaPol.AddRange(sviKlubovi.GetRange(sviKlubovi.Count / 2, sviKlubovi.Count - sviKlubovi.Count / 2));
            } else if(mogucnost[1] == "3")
            {
                
                sviKlubovi = sviKlubovi.OrderBy(el => el.naziv)
                    .ThenByDescending(el => VratiBrojSamoglasnika(
                        ((Trener) el.DohvatiDjecu().Find(elem => elem.NazivRazine == NaziviRazina.Trener)).ImeIPrezime)
                    ).ToList();
      
                prvaPol.AddRange(sviKlubovi.GetRange(0, sviKlubovi.Count / 2));
                drugaPol.AddRange(sviKlubovi.GetRange(sviKlubovi.Count / 2, sviKlubovi.Count-sviKlubovi.Count/2));
            }

            Console.WriteLine($"Prva polovina:");
            EmptyLineConsole.IspisiCrtice(20);
            foreach (Klub k in prvaPol)
            {
                string ime = ((Trener)k.DohvatiDjecu().Find(elem => elem.NazivRazine == NaziviRazina.Trener)).ImeIPrezime;
                Console.WriteLine($"{k.naziv} {ime}");
            }
            Console.WriteLine($"\nDruga polovina:");
            EmptyLineConsole.IspisiCrtice(20);
            foreach (Klub k in drugaPol)
            {
                string ime = ((Trener)k.DohvatiDjecu().Find(elem => elem.NazivRazine == NaziviRazina.Trener)).ImeIPrezime;
                Console.WriteLine($"{k.naziv} {ime}");
            }

            Console.WriteLine($"\n:");
            EmptyLineConsole.IspisiCrtice(20);
            // Određivanje parova

            Dictionary<int, List<Utakmica>> prvenstvo = new Dictionary<int, List<Utakmica>>();
            int brojUtakmice = 1;
            int maxBrojKruga = 2;
            int prevInkr = -1;
            if (prvaPol.Count + drugaPol.Count < 10) maxBrojKruga = 4;
            for(int brojKruga = 0; brojKruga < maxBrojKruga; brojKruga++)
            {
                prvenstvo.Add(brojKruga, new List<Utakmica>());
                if(brojKruga % 2 != 0)
                {
                    List<Utakmica> listaTekmiProslogKruga = prvenstvo[brojKruga - 1].ToList();
                    foreach(Utakmica u in listaTekmiProslogKruga)
                    {
                        prvenstvo[brojKruga].Add(new Utakmica(brojUtakmice, u.Kolo, u.Gost, u.Domacin, ""));
                    }
                    continue;
                }
                int inkr;
                do
                {
                    inkr = rand.Next(0, prvaPol.Count);
                } while (prevInkr != -1 && prevInkr == inkr);
                prevInkr = inkr;
                for (int i = 0; i < prvaPol.Count; i++)
                {
                    List<Klub> tmp = drugaPol.ToList();
                    foreach (Klub k in prvaPol)
                    {
                        int indeks = (prvaPol.IndexOf(k) + inkr + i) % prvaPol.Count;
                        // Console.WriteLine($"{indeks} {tmp.Count}");
                        /*
                        do {
                            indeks = rand.Next(0, tmp.Count);
                            Console.WriteLine($"{indeks} {tmp.Count} {k.naziv}");
                        } while (listaUtakmicaKrug1.Find(el => 
                            (el.Domacin == k && el.Gost == tmp[indeks]) ||
                            (el.Domacin == tmp[indeks] && el.Gost == k)) != null);
                        */
                        // Console.WriteLine($"Tekma: {k.naziv}, {tmp[indeks].naziv}");
                        if (i % 2 == 0)
                        {
                            prvenstvo[brojKruga].Add(new Utakmica(brojUtakmice, i + 1, k, tmp[indeks], ""));
                        }
                        else
                        {
                            prvenstvo[brojKruga].Add(new Utakmica(brojUtakmice, i + 1, tmp[indeks], k, ""));
                        }

                        // tmp.RemoveAt(indeks);
                        brojUtakmice++;
                    }
                }
            }

            foreach(int i in prvenstvo.Keys)
            {
                Console.WriteLine($"Krug {i + 1}");
                EmptyLineConsole.IspisiCrtice(20);
                foreach (Utakmica u in prvenstvo[i])
                {
                    Console.WriteLine($"Kolo: {u.Kolo} | {u.Domacin.naziv} - {u.Gost.naziv}");
                }
                EmptyLineConsole.IspisiCrtice(20);
                Console.WriteLine();
            }
        }

        public void Visit(IspisIR ispisIR)
        {
            Console.WriteLine("IspisIR");
        }

        public void Visit(IspisIK ispisIK)
        {
            Console.WriteLine("IspisIK");
        }

        public void Visit(IspisIG ispisIG)
        {
            Console.WriteLine("IspisIG");
        }

        public void Visit(IspisVR ispisVR)
        {
            Console.WriteLine("IspisVR");
        }
    }
}

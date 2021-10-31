using ivuk_zadaca_1.Modeli;
using ivuk_zadaca_1.ObradaPodataka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_1
{
    public sealed class Prvenstvo
    {
        public List<Klub> listaKlubova;
        public List<Igrac> listaIgraca;
        public List<Sastav> listaSastava;
        public List<Utakmica> listaUtakmica;
        public List<Dogadaj> listaDogadaja;
        private Prvenstvo() {}

        public static Prvenstvo Instance { get { return Ugnijezdeno.instance; } }

        private class Ugnijezdeno
        {
            static Ugnijezdeno() {}
            internal static readonly Prvenstvo instance = new Prvenstvo();
        }

        internal void ispisiT(string unos)
        {
            int brojKola = -1;
            string[] unosRazdvojen = unos.Split(' ');
            if (unosRazdvojen.Length == 2)
            {
                brojKola = int.Parse(unosRazdvojen[1]);
            }

            IDictionary<Klub, RedLjestvice> dnevnikTablice =
            new Dictionary<Klub, RedLjestvice>();
            IDictionary<Klub, List<int>> odigranaKola =
            new Dictionary<Klub, List<int>>();

            foreach (Klub klub in listaKlubova)
            {
                dnevnikTablice.Add(klub, new RedLjestvice());
                odigranaKola.Add(klub, new List<int>());
            }

            foreach (Utakmica utakmica in listaUtakmica)
            {
                if (brojKola != -1 && utakmica.Kolo > brojKola) continue;

                odigranaKola[utakmica.Domacin].Add(utakmica.Kolo);
                odigranaKola[utakmica.Gost].Add(utakmica.Kolo);
                int golDom = 0, golGost = 0;

                List<Dogadaj> dogadajiUtakmice = listaDogadaja.FindAll(d => d.Broj == utakmica.Broj);
                foreach(Dogadaj d in dogadajiUtakmice)
                {
                    if(d.Vrsta == 1 || d.Vrsta == 2)
                    {
                        if(d.Klub == utakmica.Domacin)
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
                    } else if (d.Vrsta == 3)
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
                if(golDom > golGost)
                {
                    dnevnikTablice[utakmica.Domacin].BrojPobjeda++;
                    dnevnikTablice[utakmica.Gost].BrojPoraza++;
                    dnevnikTablice[utakmica.Domacin].BrojBodova+=3;
                } else if (golDom < golGost)
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

        internal void ispisiS(string unos)
        {
            int brojKola = -1;
            string[] unosRazdvojen = unos.Split(' ');
            if (unosRazdvojen.Length == 2)
            {
                brojKola = int.Parse(unosRazdvojen[1]);
            }

            IDictionary<Igrac, int> strijelci =
            new Dictionary<Igrac, int>();

            foreach (Utakmica utakmica in listaUtakmica)
            {
                if (brojKola != -1 && utakmica.Kolo > brojKola) continue;

                List<Dogadaj> dogadajiUtakmice = listaDogadaja.FindAll(d => d.Broj == utakmica.Broj);
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
        }

        internal void ispisiK(string unos)
        {
            int brojKola = -1;
            string[] unosRazdvojen = unos.Split(' ');
            if (unosRazdvojen.Length == 2)
            {
                brojKola = int.Parse(unosRazdvojen[1]);
            }

            IDictionary<Klub, RedKartona> dnevnikKartona =
            new Dictionary<Klub, RedKartona>();
            List<Igrac> igraciSKartonima = new List<Igrac>();

            foreach (Klub klub in listaKlubova)
            {
                dnevnikKartona.Add(klub, new RedKartona());
            }

            foreach (Utakmica utakmica in listaUtakmica)
            {
                if (brojKola != -1 && utakmica.Kolo > brojKola) continue;

                List<Dogadaj> dogadajiUtakmice = listaDogadaja.FindAll(d => d.Broj == utakmica.Broj);
                foreach (Dogadaj d in dogadajiUtakmice)
                {    
                    if (d.Vrsta == 10)
                    {
                        if(igraciSKartonima.Contains(d.Igrac))
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
                .OrderByDescending(x => (x.Value.BrojZutih+x.Value.BrojDrugihZutih+x.Value.BrojCrvenih))
                .ToDictionary(x => x.Key, x => x.Value);

            Console.WriteLine(string.Format("{0, -4} {1, -20} " +
                "{2,-12} {3,-12} {4,-12} {5,-12}",
                "Oznaka", "Naziv", "Žuti", "Drugi žuti", "Crveni", "Ukupno"));

            foreach (var elem in dnevnikKartona)
            {
                Console.WriteLine(string.Format("{0, -4} {1, -20} {2, -48}", elem.Key.oznaka,
                    elem.Key.naziv, elem.Value));
            }
        }

        internal void ispisiR(string unos)
        {
            int brojKola = -1;
            string[] unosRazdvojen = unos.Split(' ');
            if (unosRazdvojen.Length == 3)
            {
                brojKola = int.Parse(unosRazdvojen[2]);
            }

            Klub klub = listaKlubova.Find(k => k.oznaka == unosRazdvojen[1]);
            if (klub == null)
            {
                Console.WriteLine("Taj klub ne postoji");
                return;
            }

            IDictionary<Utakmica, RedUtakmice> dnevnikUtakmica =
            new Dictionary<Utakmica, RedUtakmice>();

            List<Utakmica> utakmiceZaKlub = listaUtakmica
                .FindAll(u => u.Domacin == klub || u.Gost == klub);
            foreach (Utakmica u in utakmiceZaKlub)
            {
                if (brojKola != -1 && u.Kolo > brojKola) continue;
                int golDom = 0, golGost = 0;
               
                List<Dogadaj> dogadajiUtakmice = listaDogadaja.FindAll(d => d.Broj == u.Broj);
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

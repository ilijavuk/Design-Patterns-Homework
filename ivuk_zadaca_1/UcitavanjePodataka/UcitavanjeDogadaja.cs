using ivuk_zadaca_2.Modeli;
using ivuk_zadaca_2.PomocneKlase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_2.UcitavanjePodataka
{
    public class UcitavanjeDogadaja : UcitavanjeDatoteka
    {
        private void IspisiPogresku(string vrsta, string[] vr, string razlogPogreske)
        {
            Console.WriteLine($"Pogrešan unos događaja: {vrsta} | {string.Join(" ", vr)} - {razlogPogreske}");
        }
        public override void SpremiPodatkeUPrvenstvo(string nazivDat, Prvenstvo p)
        {
            using (var citac = new StreamReader(nazivDat))
            {
                Console.WriteLine("\nUčitavam događaje \n");
                citac.ReadLine();
                while (!citac.EndOfStream)
                {
                    var vr = citac.ReadLine().Split(';');
                    string vrsta = vr[2];

                    if ((vr.Length == 6 || vr.Length == 5 ) && vr[0] != "" && vr[1] != "" && vrsta != "")
                    {
                        Klub klub = p.listaKlubova.ConvertAll(x => (Klub)x).Find(k => k.oznaka == vr[3]);
                        if (klub == null)
                        {
                            Console.WriteLine($"Pogrešan unos događaja: {string.Join(" ", vr)} - utakmica ne postoji!");
                            return;
                        }
                        List<Igrac> listaIgraca = klub.DohvatiDjecu().FindAll(el => el.NazivRazine == NaziviRazina.Igrac)
                            .ConvertAll(x => (Igrac)x);
                        Igrac igrac = listaIgraca.Find(i => i.ImeIPrezime == vr[4]);
                        Igrac zamjena = null;

                        Utakmica u = klub.DohvatiDjecu().FindAll(elem => elem.NazivRazine == NaziviRazina.Utakmica)
                            .ConvertAll(x => (Utakmica)x).Find(ut => ut.Broj == int.Parse(vr[0]));
                        if (u == null)
                        {
                            Console.WriteLine($"Pogrešan unos događaja: {string.Join(" ", vr)} - utakmica ne postoji!");
                            return;
                        }
                        if (vr.Length == 6) zamjena = listaIgraca.Find(z => z.ImeIPrezime == vr[5]);
                        try
                        {
                            Dogadaj d =
                                new Dogadaj(int.Parse(vr[0]), vr[1], int.Parse(vrsta), klub, igrac, zamjena);
                            if ((vrsta == "00" || vrsta == "99") && (klub != null || igrac != null || zamjena != null)){
                                IspisiPogresku("00 | 99", vr, "");
                            } 
                            else if ((vrsta == "1" || vrsta == "3" || vrsta == "10" || vrsta == "11") &&
                                (klub == null || igrac == null || zamjena != null)){
                                IspisiPogresku(vrsta, vr, $"{(klub == null ? "klub" : "")} {(igrac == null ? "igrac" : "")} {(zamjena != null ? "zamjena(+)" : "")}");
                            } else if((vrsta == "20" && (klub == null || igrac == null || zamjena == null)))
                            {
                                IspisiPogresku(vrsta, vr, $"{(klub == null ? "klub" : "")} {(igrac == null ? "igrac" : "")} {(zamjena == null ? "zamjena" : "")}");
                            }
                            else
                            {
                                u.DodajDijete(d);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    else {
                        Console.WriteLine($"Pogrešan unos Dogadaja: {vr[0]} {vr[1]} {vr[2]} - stupci");
                    }
                }
            }
        }
    }
}

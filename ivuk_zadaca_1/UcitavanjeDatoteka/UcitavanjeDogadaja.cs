using ivuk_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_1.UcitavanjeDatoteka
{
    public class UcitavanjeDogadaja : UcitavanjeDatoteka
    {
        private void IspisiPogresku(string vrsta, string[] vr, string razlogPogreske)
        {
            Console.WriteLine($"Pogrešan unos događaja: {vrsta} | broj: {vr[0]} min: {vr[1]} - {razlogPogreske}");
        }
        public List<Object> DohvatiPodatke(string nazivDat, Prvenstvo p)
        {
            List<Dogadaj> lista = new List<Dogadaj>();
            using (var citac = new StreamReader(nazivDat))
            {
                Console.WriteLine("\nUčitavam događaje \n");
                citac.ReadLine();
                while (!citac.EndOfStream)
                {
                    var vr = citac.ReadLine().Split(';');
                    string vrsta = vr[2];
                    // vrsta == 00 || vrsta == 99 - ništa nije obavezno
                    // vrsta == 1 || 3 | 10 | 11 - prvih 5 - 6. prazan

                    if ((vr.Length == 6 || vr.Length == 5 ) && vr[0] != "" && vr[1] != "" && vrsta != "")
                    {
                        Klub klub = p.listaKlubova.Find(k => k.oznaka == vr[3]);
                        Igrac igrac = p.listaIgraca.Find(i => i.ImeIPrezime == vr[4]);
                        Igrac zamjena = null;
                        if (vr.Length == 6) zamjena = p.listaIgraca.Find(z => z.ImeIPrezime == vr[5]);
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
                                lista.Add(d);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            // Console.WriteLine($"Pogrešan unos Dogadaja: {string.Join(" ", vr)} - parse");
                        }
                    }
                    else {
                        Console.WriteLine($"Pogrešan unos Dogadaja: {vr[0]} {vr[1]} {vr[2]} - stupci");
                    }
                }
            }
            return lista.Cast<Object>().ToList();
        }
    }
}

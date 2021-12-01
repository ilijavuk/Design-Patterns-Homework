using ivuk_zadaca_2.Modeli;
using ivuk_zadaca_2.PomocneKlase;
using System;
using System.Collections.Generic;
using System.IO;

namespace ivuk_zadaca_2.UcitavanjePodataka
{
    public class UcitavanjeSastava : UcitavanjeDatoteka
    {
        public override void SpremiPodatkeUPrvenstvo(string nazivDatoteke, Prvenstvo p)
        {
            Console.WriteLine("\nUčitavam sastave po utakmicama \n");
            using (var citac = new StreamReader(nazivDatoteke))
            {
                citac.ReadLine();
                while (!citac.EndOfStream)
                {
                    var vr = citac.ReadLine().Split(';');
                    Klub klub = p.listaKlubova.ConvertAll(x => (Klub)x).Find(k => k.oznaka == vr[1]);
                    Igrac igrac = klub.DohvatiDjecu().FindAll(elem => elem.NazivRazine == NaziviRazina.Igrac)
                        .ConvertAll(x => (Igrac)x).Find(i => i.ImeIPrezime == vr[3]);
                    Utakmica u = klub.DohvatiDjecu().FindAll(elem => elem.NazivRazine == NaziviRazina.Utakmica)
                        .ConvertAll(x => (Utakmica)x).Find(ut => ut.Broj == int.Parse(vr[0]));

                    if (vr.Length != 5 || 
                        Array.Exists(vr, element => element == "")) {
                        Console.WriteLine($"Pogrešan unos Sastava: '{string.Join(" ", vr)}' - prazan stupac");
                    }
                    else if (klub == null || igrac == null || u == null)
                    {
                        Console.WriteLine($"Pogrešan unos Sastava: '{string.Join(" ", vr)}' - klub, igrač ili utakmica ne postoje");
                    }
                    else
                    {
                        u.DodajDijete(new Sastav(int.Parse(vr[0]), klub, vr[2], igrac, vr[4]));
                    }
                }
            }
        }
    }
}

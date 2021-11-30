using ivuk_zadaca_2.Modeli;
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
            List<Sastav> lista = new List<Sastav>();
            using (var citac = new StreamReader(nazivDatoteke))
            {
                citac.ReadLine();
                while (!citac.EndOfStream)
                {
                    var vr = citac.ReadLine().Split(';');
                    Klub klub = p.listaKlubova.Find(k => k.oznaka == vr[1]);
                    Igrac igrac = p.listaIgraca.Find(i => i.ImeIPrezime == vr[3]);
                    if (vr.Length != 5 || 
                        Array.Exists(vr, element => element == "")) {
                        Console.WriteLine($"Pogrešan unos Sastava: {string.Join(" ", vr)} - prazan stupac");
                    }
                    else if (klub == null || igrac == null)
                    {
                        Console.WriteLine($"Pogrešan unos Sastava: {string.Join(" ", vr)} - klub ili igrač ne postoje");
                    }
                    else
                    {
                        lista.Add(new Sastav(int.Parse(vr[0]), klub, vr[2], igrac, vr[4]));
                    }
                }
            }
            p.listaSastava = lista;
        }
    }
}

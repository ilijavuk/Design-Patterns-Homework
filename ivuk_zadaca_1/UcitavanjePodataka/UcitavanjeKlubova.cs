using ivuk_zadaca_2.Modeli;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_2.UcitavanjePodataka
{
    public class UcitavanjeKlubova : UcitavanjeDatoteka
    {
        public override void SpremiPodatkeUPrvenstvo(string nazivDatoteke, Prvenstvo p)
        {
            Console.WriteLine("\nUčitavam klubove \n");
            List<PrvenstvoComposite> lista = new List<PrvenstvoComposite>();
            using (var citac = new StreamReader(nazivDatoteke))
            {
                citac.ReadLine();
                while (!citac.EndOfStream)
                {
                    var vr = citac.ReadLine().Split(';');
                    if (vr.Length != 3 || Array.Exists(vr, element => element == ""))
                    {
                        Console.WriteLine($"Pogrešan unos kluba: {string.Join(" ", vr)}");
                    }
                    else
                    {
                        Klub k = new Klub(vr[0], vr[1]);
                        k.DodajDijete(new Trener(vr[2]));
                        lista.Add(k);
                    }
                }
            }
            p.listaKlubova = lista;
        }
    }
}

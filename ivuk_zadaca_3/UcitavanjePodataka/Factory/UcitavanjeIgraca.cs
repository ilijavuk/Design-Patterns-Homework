using ivuk_zadaca_3.Modeli;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_3.UcitavanjePodataka
{
    public class UcitavanjeIgraca : UcitavanjeDatoteka
    {
        public override void SpremiPodatkeUPrvenstvo(string igraciDat)
        {
            Prvenstvo p = Prvenstvo.Instance;
            Console.WriteLine("Učitavam igrače \n");
            using (var citac = new StreamReader(igraciDat))
            {
                citac.ReadLine();
                while (!citac.EndOfStream)
                {
                    var vr = citac.ReadLine().Split(';');
                    Klub klub = p.listaKlubova.ConvertAll(x => (Klub)x).Find(k => k.oznaka == vr[0]);
                    if (vr.Length != 4 || Array.Exists(vr, element => element == "") || klub == null)
                    {
                        Console.WriteLine($"Pogrešan unos Igraca: '{string.Join(" ", vr)}'");
                    }
                    else
                    {
                        klub.DodajDijete(new Igrac(klub, vr[1], vr[2], vr[3]));
                    }
                }
            }
        }
    }
}

using ivuk_zadaca_3.Modeli;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_3.UcitavanjePodataka
{
    public class UcitavanjeUtakmica : UcitavanjeDatoteka
    {
        public override void SpremiPodatkeUPrvenstvo(string nazivDat)
        {
            Prvenstvo p = Prvenstvo.Instance;
            Console.WriteLine("\nUčitavam utakmice \n");
            using (var citac = new StreamReader(nazivDat))
            {
                citac.ReadLine();
                while (!citac.EndOfStream)
                {
                    var vr = citac.ReadLine().Split(';');
                    Klub domacin = p.listaKlubova.ConvertAll(x => (Klub)x).Find(k => k.oznaka == vr[2]);
                    Klub gost = p.listaKlubova.ConvertAll(x => (Klub)x).Find(k => k.oznaka == vr[3]);
                    if (vr.Length != 5 || Array.Exists(vr, element => element == ""))
                    {
                        Console.WriteLine($"Pogrešan unos Utakmice: '{string.Join(" ", vr)}' - stupci");
                    }
                    else if (domacin == null || gost == null)
                    {
                        Console.WriteLine($"Pogrešan unos Utakmice: '{string.Join(" ", vr)}' - klub ne postoji");
                    }
                    else
                    {
                        Utakmica u = new Utakmica(int.Parse(vr[0]), int.Parse(vr[1]), domacin, gost, vr[4]);
                        domacin.DodajDijete(u);
                        gost.DodajDijete(u);
                    }
                }
            }
        }
    }
}

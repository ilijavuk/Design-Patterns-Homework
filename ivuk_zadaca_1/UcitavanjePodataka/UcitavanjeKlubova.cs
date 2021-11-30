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
            List<Klub> lista = new List<Klub>();
            using (var citac = new StreamReader(nazivDatoteke))
            {
                citac.ReadLine();
                while (!citac.EndOfStream)
                {
                    var vrijednosti = citac.ReadLine().Split(';');
                    if (vrijednosti.Length != 3 || Array.Exists(vrijednosti, element => element == ""))
                    {
                        Console.WriteLine($"Pogrešan unos kluba: {string.Join(" ", vrijednosti)}");
                    }
                    else
                    {
                        lista.Add(new Klub(vrijednosti[0], vrijednosti[1], vrijednosti[2]));
                    }
                }
            }
            p.listaKlubova = lista;
        }
    }
}

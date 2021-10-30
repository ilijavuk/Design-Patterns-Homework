using ivuk_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_1.UcitavanjeDatoteka
{
    public class UcitavanjeUtakmica
    {
        public List<Utakmica> DohvatiUtakmice(string nazivDat, List<Klub> listaKlubova)
        {
            Console.WriteLine("\nUčitavam utakmice \n");
            List<Utakmica> lista = new List<Utakmica>();
            using (var citac = new StreamReader(nazivDat))
            {
                citac.ReadLine();
                while (!citac.EndOfStream)
                {
                    var vr = citac.ReadLine().Split(';');
                    Klub domacin = listaKlubova.Find(k => k.oznaka == vr[2]);
                    Klub gost = listaKlubova.Find(k => k.oznaka == vr[3]);
                    if (vr.Length != 5 || Array.Exists(vr, element => element == ""))
                    {
                        Console.WriteLine($"Pogrešan unos Utakmice: {string.Join(" ", vr)} - stupci");
                    }
                    else if (domacin == null || gost == null)
                    {
                        Console.WriteLine($"Pogrešan unos Utakmice: {string.Join(" ", vr)} - klub ne postoji");
                    }
                    else
                    {
                        lista.Add(new Utakmica(int.Parse(vr[0]), int.Parse(vr[1]), domacin, gost, vr[4]));
                    }
                }
            }
            return lista;
        }
    }
}

using ivuk_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_1.UcitavanjeDatoteka
{
    public class UcitavanjeKlubova
    {
        public List<Klub> DohvatiKlubove(string nazivDatoteke)
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
            return lista;
        }
    }
}

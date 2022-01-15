using ivuk_zadaca_3.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_3.PomocneKlase
{
    public class Usporedivac
    {
        private List<string> redoslijedArgumenata = new List<string>() { "-k", "-i", "-u", "-s", "-d" };
        private List<string> redoslijedPozicija = new List<string>() { "G", "B", "V", "N" };

        public int UsporediSa(string a, string b)
        {
            return redoslijedArgumenata.IndexOf(a) - redoslijedArgumenata.IndexOf(b);
        }

        public int UsporediPoziciju(Sastav a, Sastav b)
        {
            return redoslijedPozicija.IndexOf(a.Pozicija) - redoslijedPozicija.IndexOf(b.Pozicija);
        }
    }
}

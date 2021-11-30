using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_2.PomocneKlase
{
    public class Usporedivac
    {
        private List<string> redoslijedArgumenata = new List<string>() { "-k", "-i", "-u", "-s", "-d" };

        public int UsporediSa(string a, string b)
        {
            return redoslijedArgumenata.IndexOf(a) - redoslijedArgumenata.IndexOf(b);
        }
    }
}

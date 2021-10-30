using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_1.Modeli
{
    public class Klub
    {
        public string oznaka;
        public string naziv;
        public string trener;

        public Klub(string oznaka, string naziv, string trener)
        {
            this.oznaka = oznaka;
            this.naziv = naziv;
            this.trener = trener;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_2.Modeli
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

        public override bool Equals(object obj)
        {
            return obj is Klub klub &&
                   oznaka == klub.oznaka;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_2.Modeli
{
    public class Osoba: PrvenstvoComposite
    {
        public string ImeIPrezime { get; set; }

        public override void IspisiInfo()
        {
            Console.WriteLine($"{NazivRazine}: {ImeIPrezime}");
        }
    }
}

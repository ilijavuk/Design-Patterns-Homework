using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_2.PomocneKlase
{
    public static class EmptyLineConsole
    {
        public static void IspisiCrtice(int brojCrtica)
        {
            for(int i = 0; i < brojCrtica; i++)
            {
                Console.Write('-');
            }
            Console.Write('\n');
        }
    }
}

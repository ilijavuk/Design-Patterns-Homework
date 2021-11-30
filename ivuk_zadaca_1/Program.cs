
using ivuk_zadaca_2.UcitavanjePodataka;
using System;

namespace ivuk_zadaca_2
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Prvenstvo prvenstvo = Prvenstvo.Instance;
            UcitavanjeDatotekaFacade udf = new UcitavanjeDatotekaFacade();
            udf.ucitajDatoteke(args);

            string unos;
            while (true)
            {
                Console.WriteLine("\nOdaberite opciju (IZLAZ za izlaz):\n");
                unos = Console.ReadLine();
                prvenstvo.ispisiTablicu(unos);
            }

        }
    }
}

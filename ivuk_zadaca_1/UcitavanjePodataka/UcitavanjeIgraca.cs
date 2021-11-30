﻿using ivuk_zadaca_2.Modeli;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ivuk_zadaca_2.UcitavanjePodataka
{
    public class UcitavanjeIgraca : UcitavanjeDatoteka
    {
        public override void SpremiPodatkeUPrvenstvo(string igraciDat, Prvenstvo p)
        {
            Console.WriteLine("Učitavam igrače \n");
            List<Igrac> lista = new List<Igrac>();
            using (var citac = new StreamReader(igraciDat))
            {
                citac.ReadLine();
                while (!citac.EndOfStream)
                {
                    var vr = citac.ReadLine().Split(';');
                    Klub klub = p.listaKlubova.Find(k => k.oznaka == vr[0]);
                    if (vr.Length != 4 || Array.Exists(vr, element => element == "") || klub == null)
                    {
                        Console.WriteLine($"Pogrešan unos Igraca: {string.Join(" ", vr)}");
                    }
                    else
                    {
                        lista.Add(new Igrac(klub, vr[1], vr[2], vr[3]));
                    }
                }
            }
            p.listaIgraca = lista;
        }
    }
}
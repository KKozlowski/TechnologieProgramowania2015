using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class Wypozyczenie
    {
        public Wypozyczenie(Ksiazka ks, Czytelnik czyt)
        {
            Ksiazka = ks;
            Czytelnik = czyt;
            //Console.WriteLine(Zawartosc);
        }

        public Ksiazka Ksiazka { get; private set; }
        public Czytelnik Czytelnik { get; private set; }
        public string Zawartosc
        {
            get { return Ksiazka.Zawartosc + " -> " + Czytelnik.Zawartosc; }
        }

        public override string ToString() {
            return Zawartosc;
        }
    }
}

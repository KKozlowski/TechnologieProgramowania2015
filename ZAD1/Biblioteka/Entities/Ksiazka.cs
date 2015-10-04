using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class Ksiazka : IEntity
    {
        string tytul;
        public int numer { get; private set; }


        public Ksiazka(int numerr, string tytull) {
            tytul = tytull;
            numer = numerr;
        }

        public string Zawartosc
        {
            get { return tytul + " #" + numer; }
        }

        public override string ToString() {
            return Zawartosc;
        }
    }
}

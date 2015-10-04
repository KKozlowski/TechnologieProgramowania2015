﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class Reader : IEntity, IComparable
    {
        private static SortedSet<int> uzyteKlucze = new SortedSet<int>();

        public HashSet<Borrow> Borrows = new HashSet<Borrow>();
        public string Imie { get; private set; }
        public string Nazwisko { get; private set; }
        public int ID { get; private set; }

        public Reader(string imie, string nazwisko) {
            Imie = imie;
            Nazwisko = nazwisko;
            ID = uzyteKlucze.Max + 1;
            uzyteKlucze.Add(ID);
            //Console.WriteLine(Zawartosc);
        }

        public Reader(string imie, string nazwisko, int id) {
            Imie = imie;
            Nazwisko = nazwisko;
            if (IdIsUsed(id))
                throw new ArgumentException("Reader ID is already used");
            else {
                ID = id;
                uzyteKlucze.Add(id);
            }
                
        }

        public string Zawartosc
        {
            get { return Imie + " " + Nazwisko + " [" + ID + "]"; }
        }

        public override string ToString() {
            return Zawartosc;
        }

        public int CompareTo(object ob) {
            Reader cz = (Reader)ob;
            if (cz == null) return 1;
            else
                return this.ID.CompareTo(cz.ID);
        }

        public static bool IdIsUsed(int id) {
            return uzyteKlucze.Contains(id);
        }
    }
}

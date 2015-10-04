using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka.Serialization.Entities;

namespace Biblioteka
{
    public class Reader : IComparable
    {
        private static SortedSet<int> uzyteKlucze = new SortedSet<int>();

        public HashSet<Borrow> Borrows = new HashSet<Borrow>();
        public int ID { get; private set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

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
            ID = id;
            uzyteKlucze.Add(id);        
        }

        public Reader(SReader r) 
            : this(r.Name, r.Surname, r.ID) { }

        public override string ToString() {
            return Imie + " " + Nazwisko + " [" + ID + "]";
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

        public static void resetIDs() {
            uzyteKlucze.Clear();
        }

        public SReader Serialize() {
            SReader result = new SReader();
            result.ID = this.ID;
            result.Name = this.Imie;
            result.Surname = this.Nazwisko;
            return result;
        }
    }
}

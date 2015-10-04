using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka.Serialization.Entities;

namespace Biblioteka
{
    public class Borrow : IComparable
    {
        static int newestID = 0;
        public int ID { get; private set; }
        public Book Ksiazka { get; private set; }
        public Reader Czytelnik { get; private set; }

        private DateTime date = DateTime.Now.AddHours(1.0);
        public DateTime Data {
            get { return date; }
            private set { date = value; }
        }

        public Borrow(Book ks, Reader czyt)
        {
            ID = newestID++;
            Ksiazka = ks;
            Czytelnik = czyt;
            czyt.Borrows.Add(this);
            ks.Borrows.Add(this);
            //Console.WriteLine(Zawartosc);
        }

        public Borrow(Book ks, Reader czyt, DateTime data) 
            : this(ks,czyt) {
                Data = data;
        }
        
        public string Zawartosc
        {
            get { return Ksiazka.Zawartosc + " -> " + Czytelnik.Zawartosc + ", " + Data.ToString("g"); }
        }

        public override string ToString() {
            return Zawartosc;
        }

        public int CompareTo(object obj) {
            if (obj == null) return 1;

            Borrow other = obj as Borrow;
            if (other != null) {
                int result = other.Czytelnik.CompareTo(this.Czytelnik);
                if (result == 0) result = other.Ksiazka.CompareTo(this.Ksiazka);
                return result;
            } else
                throw new ArgumentException("Object is not a Borrow");
        }

        public static void resetIDs() {
            newestID = 0;
        }

        public SBorrow Serialize() {
            SBorrow result = new SBorrow();
            result.ID = this.ID;
            result.ReaderID = Czytelnik.ID;
            result.BookID = Ksiazka.Numer;
            result.Date = this.Data;
            return result;
        }
    }
}

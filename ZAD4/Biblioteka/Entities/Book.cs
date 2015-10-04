using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka.Serialization.Entities;

namespace Biblioteka
{
    public class Book : IComparable
    {
        public int Numer { get; private set; }
        public string Tytul { get; set; }
        

        private int rok = 2000;
        public int IssueYear {
            get { return rok; }
            set { rok = value; }
        }

        private string autor = "Janusz Nowak";
        public string Autor {
            get { return autor; }
            set { autor = value; }
        }

        public HashSet<Borrow> Borrows = new HashSet<Borrow>();

        public Book(int numerr, string tytull) {
            Tytul = tytull;
            Numer = numerr;
        }

        public Book(int numerr, string tytull, int rok) : this(numerr,tytull) {
            IssueYear = rok;
        }

        public Book(int numerr, string tytull, int rok, string autor)
            : this(numerr, tytull, rok) {
                Autor = autor;
        }

        public Book(SBook s)
            : this(s.ID, s.Title, s.Year, s.Author) { }

        public override string ToString() {
            return Tytul + " by " + Autor + " #" + Numer + ", " + IssueYear;
        }

        public int CompareTo(object obj) {
            if (obj == null) return 1;

            Book other=  obj as Book;
            if (other != null){
                int result = this.Tytul.CompareTo(other.Tytul);
                if (result == 0) result = this.Autor.CompareTo(other.Autor);
                return result;
            }
            else
                throw new ArgumentException("Object is not a Book");
        }

        public SBook Serialize() {
            SBook result = new SBook();
            result.ID = this.Numer;
            result.Title = this.Tytul;
            result.Year = this.IssueYear;
            result.Author = this.Autor;
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Biblioteka.Serialization;

namespace Biblioteka
{
    public class Baza : IBase
    {
        List<Reader> czytelnicy = new List<Reader>();
        Dictionary<int, Book> ksiazki = new Dictionary<int, Book>();
        ObservableCollection<Borrow> wypozyczenia = new ObservableCollection<Borrow>();
        public bool useLamb = false;

        public IFiller Filler { get; set; }
        public ISerializer Serializer { get; set; }

        public Baza() {
            Serializer = new BinarySerial("binbin.bin");
            wypozyczenia.CollectionChanged += new NotifyCollectionChangedEventHandler(this.CollectionChangedMethod);
        }

        public Baza(IFiller fillIt)
            : this() {
            this.Filler = fillIt;
            UseFiller();
            
        }

        public Baza(IFiller fillIt, ISerializer serial)
            : this(fillIt) {
                Serializer = serial;

        }

        public void UseFiller() {
            Filler.Fill(czytelnicy, ksiazki, wypozyczenia);
            
        }

        public void Serialize() {
            Serializer.SerializeAll(czytelnicy, ksiazki, wypozyczenia);
            
        }

        public void Deserialize() {
            Serializer.DeserializeAll(czytelnicy, ksiazki, wypozyczenia);
        }

        public void Add(Reader czyt) {
            if(Reader.IdIsUsed(czyt.ID))
                throw new ArgumentException("Reader ID is already used");
            else
                czytelnicy.Add(czyt);
        }

        public void Add(Book ks) {
                ksiazki.Add(ks.Numer, ks);
        }

        public void Add(Borrow wyp) {
            wypozyczenia.Add(wyp);
        }

        public void Remove(Reader czyt) {
            czytelnicy.Remove(czyt);
        }

        public void Remove(Book ks) {
            ksiazki.Remove(ks.Numer);
        }

        public void Remove(Borrow wyp) {
            wypozyczenia.Remove(wyp);
        }

        public void Write() {
            Console.WriteLine("\nCZYTELNICY:");
            foreach (var czyt in czytelnicy) Console.WriteLine(czyt);

            Console.WriteLine("\nKSIAZKI:");
            foreach (var ks in ksiazki) Console.WriteLine(ks.Value);

            Console.WriteLine("\nWYPOZYCZENIA:");
            foreach (var wyp in wypozyczenia) Console.WriteLine(wyp);
        }

        public int LiczbaCzytelnikow {
            get { return czytelnicy.Count; }
        }

        public int LiczbaKsiazek {
            get { return ksiazki.Count; }
        }

        public int LiczbaWypozyczen {
            get { return wypozyczenia.Count; }
        }

        public Book GetBookById(int id) {
            return ksiazki[id];
        }

        public Borrow GetRentByNumber(int nr) {
            return wypozyczenia[nr];
        }

        /*public List<int> GetBookIds() {
            List<int> ids = new List<int>();
            foreach (var b in ksiazki)
                ids.Add(b.Key);
            return ids;
        }*/

        public void RemoveBookWithId(int id){
            ksiazki.Remove(id);
        }

        public Reader GetReaderById(int id) {
            Reader found = null;
            foreach (Reader cz in czytelnicy) {
                if (cz.ID == id) {
                    found = cz;
                    break;
                }
            }
            if (found == null) throw new KeyNotFoundException();
            else
                return found;
        }

        /*public List<int> GetReaderIds() {
            List<int> ids = new List<int>();
            foreach (var cz in czytelnicy)
                ids.Add(cz.ID);
            return ids;
        }*/

        public void RemoveReaderWithId(int id) {
            Reader found = GetReaderById(id);
            if (found != null) czytelnicy.Remove(found);
        }

        public void AnulujWypozyczenieNr(int nr) {
            if (nr < wypozyczenia.Count)
                wypozyczenia.Remove(GetRentByNumber(nr));
        }

        private void CollectionChangedMethod(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action == NotifyCollectionChangedAction.Add) {

                Borrow nowe = e.NewItems[0] as Borrow;
                Console.WriteLine("Wypozyczono ksiazke: " + nowe.Ksiazka);
            } else if (e.Action == NotifyCollectionChangedAction.Remove) {
                Borrow nowe = e.OldItems[0] as Borrow;
                Console.WriteLine("Anulowano wypozyczenie ksiazki: " + nowe.Ksiazka);
            }
        }

        public List<Book> GetBooksWithSpecifiedTitle(string title) {
            if (useLamb) return ksiazki.GetBooksWithSpecifiedTitleLamb(title);
            return ksiazki.GetBooksWithSpecifiedTitle(title);
        }

        public List<Book> GetBooksWithSpecifiedIssueYear(int minYear, int maxYear) {
            if (useLamb) return ksiazki.GetBooksWithSpecifiedIssueYearLamb(minYear, maxYear);
            return ksiazki.GetBooksWithSpecifiedIssueYear(minYear, maxYear);
        }

        public List<SimpleClass> GetBooksWithSpecifiedIssueYear2(int minYear, int maxYear) {
            if (useLamb) return ksiazki.GetBooksWithSpecifiedIssueYear2Lamb(minYear, maxYear);
            return ksiazki.GetBooksWithSpecifiedIssueYear2(minYear, maxYear);
        }

        public List<string> GetAllAuthors() {
            if (useLamb) return ksiazki.GetAllAuthorsLamb();
            return ksiazki.GetAllAuthors();
        }

        public Book GetLatestBook() {
            if (useLamb) return ksiazki.GetLatestElementLamb();
            return ksiazki.GetLatestElementLamb();
        }

        public Reader [] GetReadersWithBorrows() {
            if (useLamb) return czytelnicy.GetReadersWithBorrowsLamb();
            return czytelnicy.GetReadersWithBorrows();
        }

        public List<Borrow> GetDistinctBorrows() {
            if (useLamb) return wypozyczenia.GetDistinctBorrowsLamb();
            return wypozyczenia.GetDistinctBorrows();
        }
    }


}


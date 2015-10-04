using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class Baza : IBase
    {
        //IDane to interfejst dla implementacji klas do wypełniania kolekcji.
        List<Czytelnik> czytelnicy = new List<Czytelnik>();
        Dictionary<int, Ksiazka> ksiazki = new Dictionary<int, Ksiazka>();
        ObservableCollection<Wypozyczenie> wypozyczenia = new ObservableCollection<Wypozyczenie>();

        public IFiller Filler { get; set; }

        public Baza() {
            wypozyczenia.CollectionChanged += new NotifyCollectionChangedEventHandler(this.CollectionChangedMethod);
        }

        public Baza(IFiller fillIt)
            : this() {
            this.Filler = fillIt;
            UseFiller();
        }

        public void UseFiller() {
            Filler.Fill(czytelnicy, ksiazki, wypozyczenia);
        }

        

        public void Add(Czytelnik czyt) {
            czytelnicy.Add(czyt);
        }

        public void Add(Ksiazka ks) {
                ksiazki.Add(ks.numer, ks);
        }

        public void Add(Wypozyczenie wyp) {
            wypozyczenia.Add(wyp);
        }

        public void Remove(Czytelnik czyt) {
            czytelnicy.Remove(czyt);
        }

        public void Remove(Ksiazka ks) {
            ksiazki.Remove(ks.numer);
        }

        public void Remove(Wypozyczenie wyp) {
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

        public Ksiazka GetBookById(int id) {
            return ksiazki[id];
        }

        public Wypozyczenie GetRentByNumber(int nr) {
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

        public Czytelnik GetReaderById(int id) {
            Czytelnik found = null;
            foreach (Czytelnik cz in czytelnicy) {
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
            Czytelnik found = GetReaderById(id);
            if (found != null) czytelnicy.Remove(found);
        }

        public void AnulujWypozyczenieNr(int nr) {
            if (nr < wypozyczenia.Count)
                wypozyczenia.Remove(GetRentByNumber(nr));
        }

        private void CollectionChangedMethod(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action == NotifyCollectionChangedAction.Add) {

                Wypozyczenie nowe = e.NewItems[0] as Wypozyczenie;
                Console.WriteLine("Wypozyczono ksiazke: " + nowe.Ksiazka);
            } else if (e.Action == NotifyCollectionChangedAction.Remove) {
                Wypozyczenie nowe = e.OldItems[0] as Wypozyczenie;
                Console.WriteLine("Anulowano wypozyczenie ksiazki: " + nowe.Ksiazka);
            }
        }
    }


}


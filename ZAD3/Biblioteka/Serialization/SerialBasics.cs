using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka.Serialization.Entities;

namespace Biblioteka.Serialization {
    class SerialBasics {
        public void ResetAll(List<Reader> czytelnicy, Dictionary<int, Book> ksiazki, ObservableCollection<Borrow> wypozyczenia) {
            Reader.resetIDs();
            Borrow.resetIDs();
            czytelnicy.Clear();
            ksiazki.Clear();
            wypozyczenia.Clear();
        }

        public void ConvertAll(List<Reader> czytelnicy, Dictionary<int, Book> ksiazki, ObservableCollection<Borrow> wypozyczenia, SBase baza) {
            foreach (SBook b in baza.books)
                ksiazki.Add(b.ID, new Book(b));

            foreach (SReader r in baza.readers)
                czytelnicy.Add(new Reader(r));

            foreach (SBorrow bo in baza.borrows) {
                List<Reader> matchR = czytelnicy.Where(x => x.ID == bo.ReaderID).ToList();
                List<Book> matchB = ksiazki.Where(x => x.Value.Numer == bo.BookID).Select(x => x.Value).ToList();
                wypozyczenia.Add(new Borrow(matchB[0], matchR[0], bo.Date));
            }
        }
    }
}

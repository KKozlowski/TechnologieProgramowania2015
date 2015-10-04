using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka {
    public interface IBase {
        IFiller Filler { get; set; }
        void UseFiller();
        void Add(Reader czyt);
        void Add(Book ks);
        void Add(Borrow wyp);

        void Remove(Reader czyt);
        void Remove(Book ks);
        void Remove(Borrow wyp);

        void Write();

        int LiczbaCzytelnikow { get; }

        int LiczbaKsiazek { get; }

        int LiczbaWypozyczen { get; }

        Book GetBookById(int id);
        void RemoveBookWithId(int id);

        Reader GetReaderById(int id);
        void RemoveReaderWithId(int id);

        void AnulujWypozyczenieNr(int nr);

        List<Book> GetBooksWithSpecifiedTitle(string title);
        List<Book> GetBooksWithSpecifiedIssueYear(int minYear, int maxYear);
        List<string> GetAllAuthors();
        Book GetLatestBook();
        Reader[] GetReadersWithBorrows();
        List<Borrow> GetDistinctBorrows();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka {
    public interface IBase {
        IFiller Filler { get; set; }
        void UseFiller();
        void Add(Czytelnik czyt);
        void Add(Ksiazka ks);
        void Add(Wypozyczenie wyp);

        void Remove(Czytelnik czyt);
        void Remove(Ksiazka ks);
        void Remove(Wypozyczenie wyp);

        void Write();

        int LiczbaCzytelnikow { get; }

        int LiczbaKsiazek { get; }

        int LiczbaWypozyczen { get; }

        Ksiazka GetBookById(int id);
        void RemoveBookWithId(int id);

        Czytelnik GetReaderById(int id);
        void RemoveReaderWithId(int id);

        void AnulujWypozyczenieNr(int nr);
    }
}

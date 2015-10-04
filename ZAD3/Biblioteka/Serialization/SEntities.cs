using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Biblioteka.Serialization.Entities {
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable()]
    public class SReader {
        [JsonProperty]
        public string Name;
        [JsonProperty]
        public string Surname;
        [JsonProperty]
        public int ID;
    }

    [Serializable()]
    public class SBook {
        public string Title;
        public string Author;
        public int Year;
        public int ID;
    }

    [Serializable()]
    public class SBorrow {
        public int ID;
        public int ReaderID;
        public int BookID;
        [JsonProperty]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.JavaScriptDateTimeConverter))]
        public DateTime Date;
    }

    [Serializable()]
    public class SBase {
        public List<SReader> readers;
        public List<SBook> books;
        public List<SBorrow> borrows;

        public SBase() {
            readers = new List<SReader>();
            books = new List<SBook>();
            borrows = new List<SBorrow>();
        }

        public SBase(List<SReader> r, List <SBook> boo, List<SBorrow> bor){
            readers = r;
            books = boo;
            borrows = bor;
        }

        public SBase(List<Reader> czytelnicy, Dictionary<int, Book> ksiazki, ObservableCollection<Borrow> wypozyczenia) {
            List<SReader> readerList = new List<SReader>();
            foreach (Reader r in czytelnicy) {
                readerList.Add(r.Serialize());
            }

            List<SBook> bookList = new List<SBook>();
            foreach (var b in ksiazki)
                bookList.Add(b.Value.Serialize());

            List<SBorrow> borrowList = new List<SBorrow>();
            foreach (var bo in wypozyczenia)
                borrowList.Add(bo.Serialize());

            readers = readerList;
            books = bookList;
            borrows = borrowList;

        }
    }
}

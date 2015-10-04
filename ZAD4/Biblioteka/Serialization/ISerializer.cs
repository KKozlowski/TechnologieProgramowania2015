using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Biblioteka.Serialization {
    public interface ISerializer {
        string Path { get; set; }
        void SerializeAll(List<Reader> czytelnicy, Dictionary<int, Book> ksiazki, ObservableCollection<Borrow> wypozyczenia);
        void DeserializeAll(List<Reader> czytelnicy, Dictionary<int, Book> ksiazki, ObservableCollection<Borrow> wypozyczenia);
    }
}

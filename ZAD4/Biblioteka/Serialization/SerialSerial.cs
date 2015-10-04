using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka.Serialization.Entities;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Biblioteka.Serialization {
    public class SerialSerial : ISerializer {
        public string Path { get; set; }
        private SerialBasics sb = new SerialBasics();
        public SerialSerial(string path) {
            Path = path;
        }

        public void SerializeAll(List<Reader> czytelnicy, Dictionary<int, Book> ksiazki, ObservableCollection<Borrow> wypozyczenia) {
            SBase bas = new SBase(czytelnicy, ksiazki, wypozyczenia);

            try {
                using (Stream stream = File.Open(Path, FileMode.Create)) {
                    BinaryFormatter bin = new BinaryFormatter();
                    
                    //readers
                    bin.Serialize(stream, bas.readers.Count);
                    for (int i = 0; i < bas.readers.Count; i++) {
                        bin.Serialize(stream, bas.readers[i].ID);
                        bin.Serialize(stream, bas.readers[i].Name);
                        bin.Serialize(stream, bas.readers[i].Surname);
                    }

                    //books
                    bin.Serialize(stream, bas.books.Count);
                    for (int i = 0; i < bas.books.Count; i++) {
                        bin.Serialize(stream, bas.books[i].ID);
                        bin.Serialize(stream, bas.books[i].Title);
                        bin.Serialize(stream, bas.books[i].Author);
                        bin.Serialize(stream, bas.books[i].Year);
                    }

                    //borrows
                    bin.Serialize(stream, bas.borrows.Count);
                    for (int i = 0; i < bas.borrows.Count; i++) {
                        bin.Serialize(stream, bas.borrows[i].ID);
                        bin.Serialize(stream, bas.borrows[i].ReaderID);
                        bin.Serialize(stream, bas.borrows[i].BookID);
                        bin.Serialize(stream, bas.borrows[i].Date.Ticks);
                    }
                }
            } catch (IOException io) {
                Console.WriteLine(io.Message);
            }
        }

        public void DeserializeAll(List<Reader> czytelnicy, Dictionary<int, Book> ksiazki, ObservableCollection<Borrow> wypozyczenia) {
                using (Stream stream = File.Open(Path, FileMode.Open)) {
                    BinaryFormatter bin = new BinaryFormatter();
                    SBase baza = new SBase();
                    int readerCount = (int)bin.Deserialize(stream);
                    
                    for (int i = 0; i < readerCount; i++) {
                        SReader s = new SReader();
                        s.ID = (int)bin.Deserialize(stream);
                        s.Name = (string)bin.Deserialize(stream);
                        s.Surname = (string)bin.Deserialize(stream);
                        baza.readers.Add(s);
                    }

                    int bookCount = (int)bin.Deserialize(stream);
                    for (int i = 0; i < bookCount; i++) {
                        SBook s = new SBook();
                        s.ID = (int)bin.Deserialize(stream);
                        s.Title = (string)bin.Deserialize(stream);
                        s.Author = (string)bin.Deserialize(stream);
                        s.Year = (int)bin.Deserialize(stream);
                        baza.books.Add(s);
                    }

                    int borrowCount = (int)bin.Deserialize(stream);
                    for (int i = 0; i < borrowCount; i++) {
                        SBorrow s = new SBorrow();
                        s.ID = (int)bin.Deserialize(stream);
                        s.ReaderID = (int)bin.Deserialize(stream);
                        s.BookID = (int)bin.Deserialize(stream);
                        s.Date = new DateTime((long)bin.Deserialize(stream));

                        baza.borrows.Add(s);
                    }
                    sb.ResetAll(czytelnicy, ksiazki, wypozyczenia);
                    sb.ConvertAll(czytelnicy, ksiazki, wypozyczenia, baza);
                }
            
        }

    }
}

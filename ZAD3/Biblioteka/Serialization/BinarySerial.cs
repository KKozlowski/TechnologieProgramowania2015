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
    public class BinarySerial : ISerializer {
        public string Path { get; set; }
        private SerialBasics sb = new SerialBasics();
        public BinarySerial(string path) {
            Path = path;
        }

        public void SerializeAll(List<Reader> czytelnicy, Dictionary<int, Book> ksiazki, ObservableCollection<Borrow> wypozyczenia) {
            SBase bas = new SBase(czytelnicy, ksiazki, wypozyczenia);

            try {
                using (Stream stream = File.Open(Path, FileMode.Create)) {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, bas);
                }
            } catch (IOException io) {
                Console.WriteLine(io.Message);
            }

        }
        public void DeserializeAll(List<Reader> czytelnicy, Dictionary<int, Book> ksiazki, ObservableCollection<Borrow> wypozyczenia) {
                using (Stream stream = File.Open(Path, FileMode.Open)) {
                    BinaryFormatter bin = new BinaryFormatter();
                    var baza = (SBase)bin.Deserialize(stream);
                    sb.ResetAll(czytelnicy, ksiazki, wypozyczenia);
                    sb.ConvertAll(czytelnicy, ksiazki, wypozyczenia, baza);
                }
        }

    }
}
